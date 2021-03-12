using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000045 RID: 69
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SupportShellLogger : ScomAlertLogger
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x000066E8 File Offset: 0x000048E8
		public override void TaskStarted(ITaskDescriptor task)
		{
			this.previousValues.Push(base.GetPropertyFeed(task, ContextProperty.AccessMode.Set).ToDictionary((KeyValuePair<ContextProperty, string> pair) => pair.Key, (KeyValuePair<ContextProperty, string> pair) => pair.Value));
			base.TaskStarted(task);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000674E File Offset: 0x0000494E
		public override void TaskCompleted(ITaskDescriptor task, TaskResult result)
		{
			base.TaskCompleted(task, result);
			this.previousValues.Pop();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006790 File Offset: 0x00004990
		protected override IEnumerable<KeyValuePair<ContextProperty, string>> GetPropertyFeed(ITaskDescriptor task, ContextProperty.AccessMode forAccessMode)
		{
			IEnumerable<KeyValuePair<ContextProperty, string>> enumerable = base.GetPropertyFeed(task, forAccessMode);
			if (forAccessMode == ContextProperty.AccessMode.Set)
			{
				enumerable = from newValuePair in enumerable
				where SupportShellLogger.outputPropertiesToAlwaysOutput.Contains(newValuePair.Key) || !this.previousValues.Peek().Contains(newValuePair)
				select newValuePair;
			}
			return enumerable;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000067C5 File Offset: 0x000049C5
		protected override bool ShouldLogTask(ITaskDescriptor task)
		{
			return task.TaskType != TaskType.Infrastructure;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00006806 File Offset: 0x00004A06
		public SupportShellLogger() : base(null)
		{
		}

		// Token: 0x040000D0 RID: 208
		private static readonly ContextProperty[] outputPropertiesToAlwaysOutput = new ContextProperty[]
		{
			ContextPropertySchema.TaskStarted,
			ContextPropertySchema.TaskFinished,
			ContextPropertySchema.Latency
		};

		// Token: 0x040000D1 RID: 209
		private readonly Stack<IDictionary<ContextProperty, string>> previousValues = new Stack<IDictionary<ContextProperty, string>>();
	}
}
