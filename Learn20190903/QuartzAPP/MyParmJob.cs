using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Quartz;

namespace QuartzAPP
{
    [PersistJobDataAfterExecution]//增加此特性可以对参数副本进行修改，下次执行将拿到新的参数
    public class MyParmJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            //获取Job中的参数
            var jobData = context.JobDetail.JobDataMap;
            //获取Trigger中的参数
            var triggerData = context.Trigger.JobDataMap;
            //获取Job和Trigger中合并的参数
            var data = context.MergedJobDataMap;

            //Job中的参数
            var v1 = jobData.GetInt("key1");
            var v2 = jobData.GetString("key2");
            //Trigger中的参数
            var v3 = triggerData.GetInt("key1");
            var v4 = triggerData.GetString("key2");
            //合并候的参数
            //当Job中的参数和Trigger中的参数名称一样时，用 context.MergedJobDataMap获取参数时，Trigger中的值会覆盖Job中的值
            var v5 = data.GetInt("key1");
            var v6 = data.GetString("key2");

            var dateString = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");

            Random r = new Random();
            jobData["key1"] = r.Next(0, 20);
            jobData["key2"] = dateString;

            var str = v1 + ":" + v2 + "|" + v3 + ":" + v4 + "|" + v5 + ":" + v6 + "时间：" + dateString;

            return Task.Run(() =>
            {
                using (StreamWriter sw = new StreamWriter(@"C:\Users\faith\Desktop\error1.log", true))
                {
                    sw.WriteLine(str);
                }
            });
        }
    }
}
