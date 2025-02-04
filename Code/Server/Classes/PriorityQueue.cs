using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Server.Classes;
using BankingEnumeration;
namespace Server {
	public class PriorityQueue
	{
        private readonly SortedDictionary<RequestPriority, Queue<Request>> priorityQueue;
        private readonly object _lock = new object(); // Thread safety lock
        public PriorityQueue()
		{
            priorityQueue = new SortedDictionary<RequestPriority, Queue<Request>>();
            foreach (RequestPriority priority in Enum.GetValues(typeof(RequestPriority)))
            {
                priorityQueue[priority] = new Queue<Request>();
            }
        }

        #region Methods

        #endregion

        /// <summary>
        /// Adds a new request to the queue based on its priority.
        /// </summary>
        public void AddRequest(Request request)
        {

            RequestPriority priority = request.Priority;

            lock (_lock) // Thread-safe operation
            {
                priorityQueue[priority].Enqueue(request);
            }
            Console.WriteLine($"Request {request.CommandToExecute.CommandType} added with priority {priority}");
        }
   
        /// <summary>
        /// Removes and returns the highest-priority request.
        /// </summary>
        private Request Dequeue()
        {
            lock (_lock)
            {

                //Order the priorityLevel in ascending order so Highest commands execute and then high and so on...
                foreach (var priorityLevel in priorityQueue.Keys.OrderBy(p => p))
                {
                    if (priorityQueue[priorityLevel].Count > 0)
                    {
                        Request request = priorityQueue[priorityLevel].Dequeue();
                        Console.WriteLine($"Dequeued {request.CommandToExecute.CommandType} with priority {priorityLevel}");
                        return request;
                    }
                }
            }
            throw new InvalidOperationException("PriorityQueue is empty.");
        }

        /// <summary>
        /// Checks if the queue is empty.
        /// </summary>
        public bool IsEmptyQueue()
        {
            lock (_lock)
            {
                return priorityQueue.Values.All(q => q.Count == 0);
            }
        }

        /// <summary>
        /// Processes the next request in the queue.
        /// </summary>
        public Request GetNextRequest()
        {
            if (IsEmptyQueue())
                throw new Exception("No requests to process.");
            return Dequeue();
        }

        /// <summary>
        /// Assigns priority based on the command type.
        /// </summary>
        public static RequestPriority SetPriorityQueue(ClientCommandType commandType)
        {
            switch (commandType)
            {
                case ClientCommandType.Log_In:
                case ClientCommandType.Log_Out:
                case ClientCommandType.Register:
                    return RequestPriority.Highest;
                case ClientCommandType.Deposit:
                case ClientCommandType.Withdraw:
                case ClientCommandType.Transfer:
                case ClientCommandType.Transaction_History:
                   return RequestPriority.Medium;
                default:
                    throw new ArgumentException($"Unhandled client command type: {commandType}");
            }
        }

	}


}