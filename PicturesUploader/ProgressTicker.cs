using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesUploader
{
    public class ProgressData
    {
        public int Progress { get; }
        public int TicksDone { get; }
        public int TicksTotal { get; }
        public ProgressData(int progress, int ticksDone, int ticksTotal)
        {
            this.Progress = progress;
            this.TicksDone = ticksDone;
            this.TicksTotal = ticksTotal;
        }
    }
    public class ProgressTicker
    {
        public delegate void ProgressChangedEventHandler(ProgressData data);
        public event ProgressChangedEventHandler ProgressChanged;

        int Length;
        int Step;
        int Flag;
        int Updated;
        public int Ticks { get { return Updated; } }

        public ProgressTicker(int expectedTicks, int stepInPersent)
        {
            this.Length = expectedTicks;
            this.Step = stepInPersent * expectedTicks / 100;
            this.Flag = this.Updated = 0;
        }
        public void Tick()
        {
            this.Updated++;
            this.Flag++;
            if (Flag >= Step || Updated == Length)
            {
                ProgressChanged?.Invoke(new ProgressData(Updated * 100 / Length, Updated, Length));
                Flag = 0;
            }
        }
    }
}
