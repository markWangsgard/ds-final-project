using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ds_final_projectLogic;

public static class ChallengesBST
{
    public static BSTNode RootNode;

    public static bool nodeExists(int targetId)
    {
        if (RootNode == null)
        {
            return false;
        }
        BSTNode currentNode = RootNode;
        do
        {
            if (currentNode.Data.ID < targetId)
            {
                currentNode = currentNode.Right;
            }
            else
            {
                currentNode = currentNode.Left;
            }
            if (currentNode == null)
            {
                return false;
            }
        }
        while (currentNode.Data.ID != targetId);
        return true;
    }
    static int GetHeight(BSTNode node)
    {
        return node == null ? 0 : node.Height;
    }

    static int GetBalance(BSTNode node)
    {
        return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
    }
    public static void Insert(Challenge challenge)
    {
        RootNode = InsertNode(RootNode, challenge);
    }
    private static BSTNode InsertNode(BSTNode node, Challenge challenge)
    {
        if (node == null)
        {
            return new BSTNode(challenge);
        }

        if (challenge.ID < node.Data.ID)
        {
            node.Left = InsertNode(node.Left, challenge);
        }
        else if (challenge.ID > node.Data.ID)
        {
            node.Right = InsertNode(node.Right, challenge);
        }

        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        int balance = GetBalance(node);

        // left left
        // if left unbalanced, coming from left
        if (balance > 1 && GetBalance(node.Left) >= 0)
        {
            return RightRotate(node);
        }

        // left right
        // if left unbalanced, coming from right
        if (balance > 1 && GetBalance(node.Left) < 0)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        // right right
        // if right unbalanced, coming from right
        if (balance < -1 && GetBalance(node.Right) <= 0)
        {
            return LeftRotate(node);
        }

        // right left
        // if right unbalanced, coming from left
        if (balance < -1 && GetBalance(node.Right) > 0)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }


        return node;
    }
    public static void Delete(int targetID)
    {
        DeleteNode(RootNode, targetID);
    }
    private static BSTNode DeleteNode(BSTNode node, int targetID)
    {
        //navigation
        if (node == null)
        {
            return node;
        }

        if (targetID < node.Data.ID)
        {
            node.Left = DeleteNode(node.Left, targetID);
        }
        else if (targetID > node.Data.ID)
        {
            node.Right = DeleteNode(node.Right, targetID);
        }
        //deletion
        else
        {
            //0 or 1 children
            if (node.Left == null)
            {
                BSTNode temp = node.Right;
                node = null;
                return temp;
            }
            else if ((targetID < node.Data.ID))
            {
                BSTNode temp = node.Right;
                node = null;
                return temp;
            }

            //2 children
            BSTNode largestLeft = getLargestLeft(node.Left);

            node.Data = largestLeft.Data;

            node.Right = DeleteNode(node.Left, largestLeft.Data.ID);
        }

        //checking for null exception error
        if (node == null)
        {
            return node;
        }

        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

        int balance = GetBalance(node);

        // left left
        // if left unbalanced, and left left is also unbalanced
        if (balance > 1 && GetBalance(node.Left) >= 0)
        {
            return RightRotate(node);
        }

        // left right
        // if left unbalanced, and left right is unbalanced
        if (balance > 1 && GetBalance(node.Left) < 0)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        // right right
        // if right unbalanced, and right right is unbalanced
        if (balance < -1 && GetBalance(node.Right) <= 0)
        {
            return LeftRotate(node);
        }

        // right left
        // if right unbalanced, and right left is unbalanced
        if (balance < -1 && GetBalance(node.Right) > 0)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node;

    }

    private static BSTNode getLargestLeft(BSTNode node)
    {
        if (node.Right == null)
        {
            return node;
        }
        else
        {
            return getLargestLeft(node.Right);
        }
    }

    private static BSTNode RightRotate(BSTNode y)
    {
        BSTNode x = y.Left;
        BSTNode T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));
        x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));

        return x;
    }
    private static BSTNode LeftRotate(BSTNode x)
    {
        BSTNode y = x.Right;
        BSTNode T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));
        y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

        return y;
    }

    public static Challenge GetChallenge(int challengeId)
    {
        BSTNode currentNode = RootNode;
        while (currentNode.Data.ID != challengeId)
        {
            if (currentNode == null)
            {
                return null;
            }
            if (currentNode.Data.ID < challengeId)
            {
                currentNode = currentNode.Right;
            }
            else
            {
                currentNode = currentNode.Left;
            }
        }
        return currentNode.Data;
    }

}
