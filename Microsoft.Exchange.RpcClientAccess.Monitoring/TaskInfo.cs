using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200007B RID: 123
	internal class TaskInfo
	{
		// Token: 0x0600025D RID: 605 RVA: 0x0000A110 File Offset: 0x00008310
		private TaskInfo(Type taskType)
		{
			this.Dependencies = TaskInfo.GetDependencies(taskType);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A124 File Offset: 0x00008324
		public static TaskInfo Get(Type taskType)
		{
			TaskInfo result;
			if (!TaskInfo.taskTypeToInfo.TryGetValue(taskType, out result))
			{
				TaskInfo taskInfo = new TaskInfo(taskType);
				lock (TaskInfo.taskTypeToInfoLock)
				{
					if (!TaskInfo.taskTypeToInfo.TryGetValue(taskType, out result))
					{
						TaskInfo.taskTypeToInfo.Add(taskType, taskInfo);
						result = taskInfo;
					}
				}
			}
			return result;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A1BC File Offset: 0x000083BC
		private static ICollection<ContextProperty> GetDependencies(Type taskType)
		{
			return (from field in taskType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)
			where typeof(ContextProperty).IsAssignableFrom(field.FieldType)
			select (ContextProperty)field.GetValue(null)).ToArray<ContextProperty>();
		}

		// Token: 0x0400015C RID: 348
		public readonly ICollection<ContextProperty> Dependencies;

		// Token: 0x0400015D RID: 349
		private static readonly IDictionary<Type, TaskInfo> taskTypeToInfo = new Dictionary<Type, TaskInfo>();

		// Token: 0x0400015E RID: 350
		private static readonly object taskTypeToInfoLock = new object();
	}
}
