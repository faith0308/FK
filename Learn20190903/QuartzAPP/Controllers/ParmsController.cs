using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quartz;

namespace QuartzAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParmsController : Controller
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;
        public ParmsController(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            //任务：
            //Job就是执行的作业，Job需要继承IJob接口，实现Execute方法。Job中执行的参数从Execute方法的参数中获取。
            // 触发器：
            //触发器常用的有两种：SimpleTrigger触发器和CronTrigger触发器。
            //SimpleTrigger:能是实现简单业务，如每隔几分钟，几小时触发执行，并限制执行次数。
            //CronTrigger:Cron表达式包含7个字段，秒 分 时 月内日期 月 周内日期 年(可选)。
            //1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            //2、开启调度器
            await _scheduler.Start();
            //3、创建一个触发器
            var trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(5)//5秒执行一次
                .RepeatForever())
                .UsingJobData("key1", 99)
                .UsingJobData("key2", "hello")
                .WithIdentity("trigger2", "group2")
                .Build();
            //4、创建任务
            var jobDetail = JobBuilder.Create<MyParmJob>()
                .WithIdentity("job2", "group2")
                .UsingJobData("key1", 99)
                .UsingJobData("key2", "hello1")
                .Build();
            //5、将触发器和任务器绑定到调度器中
            await _scheduler.ScheduleJob(jobDetail, trigger);

            return await Task.FromResult(new string[] { "valueParm1", "valueParm2" });
        }
    }
}