using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Pathfinder
{
    public class BinaryHeap
    {
        private Node[] nodes;
        public int currentNodeCount { get; private set; }

        public BinaryHeap(int BinaryHeapsize)
        {
            nodes = new Node[BinaryHeapsize];
        }

        public void Add(Node node)
        {
            node.index = currentNodeCount;
            nodes[currentNodeCount] = node;
            heapifyUp(node);
            currentNodeCount++;
        }

        public Node Extract()
        {
            Node firstNode = nodes[0];
            currentNodeCount--;
            nodes[0] = nodes[currentNodeCount];
            nodes[0].index = 0;
            heapifyDown(nodes[0]);
            return firstNode;
        }

        public void UpdateNode(Node node)
        {
            heapifyUp(node);
        }

        public bool Contains(Node node)
        {
            return Equals(nodes[node.index], node);
        }

        private void heapifyUp(Node node)
        {
            int parentIndex = (node.index - 1) / 2;

            while (true) //change
            {
                Node parent = nodes[parentIndex];
                if (node.CompareTo(parent) > 0)
                {
                    Swap(node, parent);
                }
                else
                {
                    break; // was BREAK
                }
                parentIndex = (node.index - 1) / 2;
            }
        }

        private void heapifyDown(Node node)
        {
            while (true)
            {
                int childIndexLeft = node.index * 2 + 1;
                int childIndexRight = node.index * 2 + 2;
                int swapIndex;

                if (childIndexLeft < currentNodeCount)
                {
                    swapIndex = childIndexLeft;

                    if (childIndexRight < currentNodeCount)
                    {
                        if (nodes[childIndexLeft].CompareTo(nodes[childIndexRight]) < 0)
                        {
                            swapIndex = childIndexRight;
                        }
                    }

                    if (node.CompareTo(nodes[swapIndex]) < 0)
                    {
                        Swap(node, nodes[swapIndex]);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void Swap(Node node1, Node node2)
        {
            nodes[node1.index] = node2;
            nodes[node2.index] = node1;
            int tmpIndex = node1.index;
            node1.index = node2.index;
            node2.index = tmpIndex;
        }
    }

}