using UnityEngine;
using System.Collections.Generic;
using System.IO.Compression;

public class Golem : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float stopDistance = 0.5f;
    public float switchTargetThreshold = 0.5f;

    private Transform currentTarget;

    void Update()
    {
        UpdateNearestPlayer();

        if (currentTarget != null)
        {
            float distance = Vector3.Distance(transform.position, currentTarget.position);
            Debug.Log($"[Golem] Current target: {currentTarget.name}, Distance: {distance}");

            if (distance > stopDistance)
            {
                Vector3 direction = (currentTarget.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                Debug.Log($"[Golem] Moving toward {currentTarget.name} with direction {direction}");
            }
            else
            {
                Debug.Log($"[Golem] Reached target {currentTarget.name}, stopping.");
            }
        }
        else
        {
            Debug.Log("[Golem] No target found.");
        }
    }

    void UpdateNearestPlayer()
    {
        string[] playerTags = { "Hans", "Grete" }; 
        List<GameObject> allPlayers = new List<GameObject>();


        foreach (string tag in playerTags)
        {
            GameObject[] taggedPlayers = GameObject.FindGameObjectsWithTag(tag);
            allPlayers.AddRange(taggedPlayers);
        }

        float closestDistance = Mathf.Infinity;
        Transform closestPlayer = null;

        foreach (GameObject player in allPlayers)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            Debug.Log($"[Golem] Found player {player.name} at distance {distance}");

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player.transform;
            }
        }

        if (closestPlayer != null)
        {
            float currentDistance = currentTarget != null
                ? Vector3.Distance(transform.position, currentTarget.position)
                : Mathf.Infinity;

            if (currentTarget == null || closestDistance + switchTargetThreshold < currentDistance)
            {
                Debug.Log($"[Golem] Switching target to {closestPlayer.name}");
                currentTarget = closestPlayer;
            }
            else
            {
                Debug.Log($"[Golem] Keeping current target {currentTarget.name}");
            }
        }
    }
}
/*
    public float speed = 2f;
    public float detectionRange = 10f;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    public float wanderRadius = 5f;
    public float pathUpdateInterval = 0.5f;

    private enum State { Wandering, Chasing }
    private State currentState = State.Wandering;
    private Transform targetPlayer;
    private Vector3[] path;
    private int pathIndex = 0;
    private float pathUpdateTimer = 0f;
    private Vector3 wanderTarget;

    void Start()
    {
        PickNewWanderTarget();
    }

    void Update()
    {
        pathUpdateTimer += Time.deltaTime;
        if (currentState == State.Wandering)
        {
            DetectPlayer();
            if (Vector3.Distance(transform.position, wanderTarget) < 0.5f)
                PickNewWanderTarget();
            if (pathUpdateTimer > pathUpdateInterval)
            {
                path = FindPath(transform.position, wanderTarget);
                pathIndex = 0;
                pathUpdateTimer = 0f;
            }
            FollowPath();
        }
        else if (currentState == State.Chasing)
        {
            if (targetPlayer == null || !CanReachPlayer(targetPlayer))
            {
                currentState = State.Wandering;
                PickNewWanderTarget();
                return;
            }
            if (pathUpdateTimer > pathUpdateInterval)
            {
                path = FindPath(transform.position, targetPlayer.position);
                pathIndex = 0;
                pathUpdateTimer = 0f;
            }
            FollowPath();
        }
    }

    void DetectPlayer()
    {
        Debug.Log("Detecting player...");
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange, playerLayer);
        foreach (var hit in hits)
        {
            if (CanSeePlayer(hit.transform))
            {
                targetPlayer = hit.transform;
                currentState = State.Chasing;
                break;
            }
        }
    }

    bool CanSeePlayer(Transform player)
    {
        Debug.Log("Checking visibility...");
        Vector2 start = new Vector2(transform.position.x, transform.position.z);
        Vector2 end = new Vector2(player.position.x, player.position.z);
        RaycastHit2D hit = Physics2D.Linecast(start, end, obstacleLayer);
        return hit.collider == null;
    }

    bool CanReachPlayer(Transform player)
    {
        Debug.Log("Checking reachability...");
        // Simple check: can see player and within detection range
        return Vector3.Distance(transform.position, player.position) < detectionRange && CanSeePlayer(player);
    }

    void PickNewWanderTarget()
    {
        Debug.Log("Picking new wander target...");
        Vector2 rand = Random.insideUnitCircle * wanderRadius;
        wanderTarget = new Vector3(transform.position.x + rand.x, transform.position.y, transform.position.z + rand.y);
        path = FindPath(transform.position, wanderTarget);
        pathIndex = 0;
    }

    void FollowPath()
    {
        Debug.Log("Following path...");
        if (path == null || path.Length == 0) return;
        if (pathIndex >= path.Length) return;
        Vector3 target = path[pathIndex];
        Vector3 moveDir = (target - transform.position).normalized;
        if (moveDir != Vector3.zero)
        {
            // Flip sprite based on movement direction (assumes sprite faces right by default)
            if (Mathf.Abs(moveDir.x) > 0.01f)
            {
                Vector3 localScale = transform.localScale;
                localScale.x = Mathf.Sign(moveDir.x) * Mathf.Abs(localScale.x);
                transform.localScale = localScale;
            }
        }
        // Remove rotation for 2D sprite flipping
        // transform.rotation = Quaternion.Euler(0, 0, -angle);
        transform.position += speed * Time.deltaTime * moveDir;
        if (Vector3.Distance(transform.position, target) < 0.2f)
            pathIndex++;
    }

    // --- Custom grid-based A* pathfinding implementation ---
    class Node
    {
        public Vector2Int pos;
        public Node parent;
        public float gCost, hCost;
        public float fCost => gCost + hCost;
        public Node(Vector2Int pos, Node parent, float gCost, float hCost)
        {
            this.pos = pos;
            this.parent = parent;
            this.gCost = gCost;
            this.hCost = hCost;
        }
    }

    Vector3[] FindPath(Vector3 start, Vector3 end)
    {
        Debug.Log("Finding path...");
        Vector2Int startCell = WorldToGrid(start);
        Vector2Int endCell = WorldToGrid(end);
        HashSet<Vector2Int> closed = new HashSet<Vector2Int>();
        List<Node> open = new List<Node>
        {
            new Node(startCell, null, 0, Heuristic(startCell, endCell))
        };

        int maxIterations = 1000; // Prevent infinite loops
        while (open.Count > 0 && maxIterations-- > 0)
        {
            // Get node with lowest fCost
            Node current = open[0];
            foreach (var n in open)
                if (n.fCost < current.fCost || (n.fCost == current.fCost && n.hCost < current.hCost))
                    current = n;
            open.Remove(current);
            closed.Add(current.pos);

            if (current.pos == endCell)
            {
                // Reconstruct path
                List<Vector3> path = new List<Vector3>();
                Node node = current;
                while (node != null)
                {
                    path.Add(GridToWorld(node.pos));
                    node = node.parent;
                }
                path.Reverse();
                return path.ToArray();
            }

            foreach (var neighbor in GetNeighbors(current.pos))
            {
                if (closed.Contains(neighbor) || !IsWalkable(neighbor)) continue;
                float gCost = current.gCost + 1;
                Node existing = open.Find(n => n.pos == neighbor);
                if (existing == null)
                {
                    open.Add(new Node(neighbor, current, gCost, Heuristic(neighbor, endCell)));
                }
                else if (gCost < existing.gCost)
                {
                    existing.gCost = gCost;
                    existing.parent = current;
                }
            }
        }
        // No path found
        return new Vector3[] { end };
    }

    Vector2Int WorldToGrid(Vector3 pos)
    {
        return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z));
    }
    Vector3 GridToWorld(Vector2Int cell)
    {
        return new Vector3(cell.x, transform.position.y, cell.y);
    }
    float Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y); // Manhattan distance
    }
    List<Vector2Int> GetNeighbors(Vector2Int cell)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        foreach (var d in dirs)
            neighbors.Add(cell + d);
        return neighbors;
    }
    bool IsWalkable(Vector2Int cell)
    {
        Vector3 world = GridToWorld(cell);
        if (TryGetComponent<CapsuleCollider2D>(out var golemCollider))
        {
            // Temporarily move the golem's collider to the test position
            Vector3 originalPosition = transform.position;
            transform.position = world;
            bool blocked = false;
            Collider[] hits = Physics.OverlapBox(world, golemCollider.bounds.extents, Quaternion.identity, obstacleLayer);
            foreach (var hit in hits)
            {
                if (hit != golemCollider)
                {
                    blocked = true;
                    break;
                }
            }
            transform.position = originalPosition;
            return !blocked;
        }
        else
        {
            // Fallback: use bounding box as before
            Collider[] hits = Physics.OverlapBox(world, Vector3.one * 0.45f, Quaternion.identity, obstacleLayer);
            return hits.Length == 0;
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // Draw current path
        if (path != null && path.Length > 1)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < path.Length - 1; i++)
            {
                Gizmos.DrawLine(path[i], path[i + 1]);
                Gizmos.DrawSphere(path[i], 0.1f);
            }
            Gizmos.DrawSphere(path[path.Length - 1], 0.1f);
        }
        // Draw grid walkability around the golem
        Gizmos.color = Color.red;
        Vector2Int center = WorldToGrid(transform.position);
        for (int x = -5; x <= 5; x++)
        {
            for (int y = -5; y <= 5; y++)
            {
                Vector2Int cell = center + new Vector2Int(x, y);
                Vector3 world = GridToWorld(cell);
                if (!IsWalkable(cell))
                {
                    Gizmos.DrawCube(world, Vector3.one * 0.9f);
                }
            }
        }
    }
#endif
}
*/