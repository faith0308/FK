using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using System.IO;

namespace QuartzAPP
{
    public class MyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                using (StreamWriter sw = new StreamWriter(@"C:\Users\faith\Desktop\error.log", true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                }
            });
        }
    }
}
