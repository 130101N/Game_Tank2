namespace WindowsGame3
{

    // Represents the search state of a Node

    public enum NodeState
    {
        
        Untested,       // The node has not yet been considered in any possible paths

        Open,           // The node has been identified as a possible step in a path

        Closed          // The node has already been included in a path and will not be considered again
    }
}