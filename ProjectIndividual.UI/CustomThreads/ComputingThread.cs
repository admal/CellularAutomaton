using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndividual.UI.ViewModels;

namespace ProjectIndividual.UI.CustomThreads
{
    public class ComputingThread
    {
        private bool isRunning = true;
        private bool isPaused = false;
        private GridViewModel gridViewModel;

        public ComputingThread(GridViewModel gridViewModel)
        {
            this.gridViewModel = gridViewModel;
        }

        public void Start()
        {
            Stopwatch stopwatch = new Stopwatch();

            while (isRunning)
            {
                if (isPaused)
                {
                    continue;
                }
                stopwatch.Restart();
                gridViewModel.Update();
                var elapsed = stopwatch.ElapsedMilliseconds;
                Thread.Sleep(elapsed < 1000 ? 1000 - (int) elapsed : 0);
            }
            
        }

        public void RunPause()
        {
            isPaused = !isPaused;
        }

        public void Stop()
        {
            isRunning = false;
        }
    }
}
