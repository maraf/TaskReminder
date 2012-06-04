using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using Quartz.Util;

namespace TaskReminder.Web.Core
{
    public class SchedulingService
    {
        public static IScheduler GetScheduler()
        {
            NameValueCollection properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "RemoteServer";

            // set thread pool info
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "5";
            properties["quartz.threadPool.threadPriority"] = "Normal";

            ISchedulerFactory sf = new StdSchedulerFactory(properties);
            IScheduler sched = sf.GetScheduler();
            sched.Start();

            return sched;
        }

        public static void CreateDailyJob<T>()
            where T : IJob
        {
            IJobDetail job = new JobDetailImpl(typeof(T).FullName, null, typeof(T));
            ITrigger trigger = TriggerBuilder.Create().WithDailyTimeIntervalSchedule(b => b.OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(23, 59))).Build();
            GetScheduler().ScheduleJob(job, trigger);
        }
    }
}