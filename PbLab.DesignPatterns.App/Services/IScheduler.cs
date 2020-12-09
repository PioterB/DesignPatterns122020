using System;
using System.Collections.Generic;
using System.Linq;

namespace PbLab.DesignPatterns.Services
{
    public interface IScheduler<TCollectionItem, TResult>
    {
        IEnumerable<TResult> Schedule(IEnumerable<TCollectionItem> collection, Func<TCollectionItem, IEnumerable<TResult>> processor);
    }

    public class TasksScheduler<TCollectionItem, TResult> : IScheduler<TCollectionItem, TResult>
    {
        public IEnumerable<TResult> Schedule(IEnumerable<TCollectionItem> collection, Func<TCollectionItem, IEnumerable<TResult>> processor)
        {
            return collection.AsParallel().SelectMany(processor).ToArray();
        }
    }

    public class LinearScheduler<TCollectionItem, TResult> : IScheduler<TCollectionItem, TResult>
    {
        public IEnumerable<TResult> Schedule(IEnumerable<TCollectionItem> collection, Func<TCollectionItem, IEnumerable<TResult>> processor)
        {
            List<TResult> result = new List<TResult>();

            foreach (var item in collection)
            {
                result.AddRange(processor(item));
            }

            return result;
        }
    }
}