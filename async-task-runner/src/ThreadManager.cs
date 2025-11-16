using System;
using System.Diagnostics.Contracts;

/*
- need to find number of cores (true threads)
- need 1 semaphore for each true thread
- need 1 queue that will contain all tasks
- need 1 mutex to make task queue access safe for all threads
- want database integration, holding task metadata
- 
*/

namespace AsyncTaskRunner {
    public class ThreadManager {
        int thread_count = 0;
        List<Thread> _threads = [];
        Queue<ICallable> _task_queue = new Queue<ICallable>();
        Mutex _task_mut = new Mutex();
        Semaphore _pool;

        public void ThreadLoop() {
            ICallable current_task;

            while (true) {
                _pool.WaitOne();

                _task_mut.WaitOne();
                current_task = _task_queue.Peek();
                // should consider Dequeuing later
                _task_queue.Dequeue();
                _task_mut.ReleaseMutex();

                current_task.Invoke();
            }
        }

        public ThreadManager() {
            // get number of threads from OS
            // thread_count = ;
            // for now, do not need to enfore maximum
            _pool = new Semaphore(initialCount: 0, maximumCount: int.MaxValue);

            for (int i = 0; i < thread_count; i++) {
                _threads.Add(new Thread(ThreadLoop));
            }

        }

        public void AddTask(ICallable callable) {
            _task_mut.WaitOne();
            _task_queue.Enqueue(callable);
            _task_mut.ReleaseMutex();

            _pool.Release();
        }
    }
}