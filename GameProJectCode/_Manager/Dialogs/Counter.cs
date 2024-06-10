using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class Counter
    {
        public Counter prevCounter;
        public int CurrentCounter;
        public Counter(Counter counter=null)
        {
            prevCounter = counter;
            CurrentCounter = 0;
        }
    }

