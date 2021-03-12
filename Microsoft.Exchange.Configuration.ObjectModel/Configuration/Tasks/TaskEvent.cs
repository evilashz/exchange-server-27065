using System;
using System.Reflection;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Diagnostics.CmdletInfra;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000A4 RID: 164
	internal class TaskEvent : ITaskEvent
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600067A RID: 1658 RVA: 0x00018148 File Offset: 0x00016348
		// (remove) Token: 0x0600067B RID: 1659 RVA: 0x00018180 File Offset: 0x00016380
		public event EventHandler<EventArgs> PreInit;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600067C RID: 1660 RVA: 0x000181B8 File Offset: 0x000163B8
		// (remove) Token: 0x0600067D RID: 1661 RVA: 0x000181F0 File Offset: 0x000163F0
		public event EventHandler<EventArgs> InitCompleted;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600067E RID: 1662 RVA: 0x00018228 File Offset: 0x00016428
		// (remove) Token: 0x0600067F RID: 1663 RVA: 0x00018260 File Offset: 0x00016460
		public event EventHandler<EventArgs> PreIterate;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000680 RID: 1664 RVA: 0x00018298 File Offset: 0x00016498
		// (remove) Token: 0x06000681 RID: 1665 RVA: 0x000182D0 File Offset: 0x000164D0
		public event EventHandler<EventArgs> IterateCompleted;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000682 RID: 1666 RVA: 0x00018308 File Offset: 0x00016508
		// (remove) Token: 0x06000683 RID: 1667 RVA: 0x00018340 File Offset: 0x00016540
		public event EventHandler<EventArgs> PreRelease;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000684 RID: 1668 RVA: 0x00018378 File Offset: 0x00016578
		// (remove) Token: 0x06000685 RID: 1669 RVA: 0x000183B0 File Offset: 0x000165B0
		public event EventHandler<EventArgs> Release;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000686 RID: 1670 RVA: 0x000183E8 File Offset: 0x000165E8
		// (remove) Token: 0x06000687 RID: 1671 RVA: 0x00018420 File Offset: 0x00016620
		public event EventHandler<EventArgs> PreStop;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000688 RID: 1672 RVA: 0x00018458 File Offset: 0x00016658
		// (remove) Token: 0x06000689 RID: 1673 RVA: 0x00018490 File Offset: 0x00016690
		public event EventHandler<EventArgs> Stop;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600068A RID: 1674 RVA: 0x000184C8 File Offset: 0x000166C8
		// (remove) Token: 0x0600068B RID: 1675 RVA: 0x00018500 File Offset: 0x00016700
		public event EventHandler<GenericEventArg<TaskErrorEventArg>> Error;

		// Token: 0x0600068C RID: 1676 RVA: 0x00018535 File Offset: 0x00016735
		public TaskEvent(TaskContext taskContext)
		{
			this.taskContext = taskContext;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00018544 File Offset: 0x00016744
		public void OnPreInit(EventArgs e)
		{
			this.TriggerEvent<EventArgs>(this.PreInit, e, false);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00018554 File Offset: 0x00016754
		public void OnInitCompleted(EventArgs e)
		{
			this.TriggerEvent<EventArgs>(this.InitCompleted, e, true);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00018564 File Offset: 0x00016764
		public void OnPreIterate(EventArgs e)
		{
			this.TriggerEvent<EventArgs>(this.PreIterate, e, false);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00018574 File Offset: 0x00016774
		public void OnIterateCompleted(EventArgs e)
		{
			this.TriggerEvent<EventArgs>(this.IterateCompleted, e, true);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00018584 File Offset: 0x00016784
		public void OnPreRelease(EventArgs e)
		{
			this.TriggerEvent<EventArgs>(this.PreRelease, e, false);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00018594 File Offset: 0x00016794
		public void OnRelease(EventArgs e)
		{
			this.TriggerEvent<EventArgs>(this.Release, e, true);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000185A4 File Offset: 0x000167A4
		public void OnPreStop(EventArgs e)
		{
			this.TriggerEvent<EventArgs>(this.PreStop, e, false);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000185B4 File Offset: 0x000167B4
		public void OnStop(EventArgs e)
		{
			this.TriggerEvent<EventArgs>(this.Stop, e, true);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000185C4 File Offset: 0x000167C4
		public void OnError(GenericEventArg<TaskErrorEventArg> e)
		{
			this.TriggerEvent<GenericEventArg<TaskErrorEventArg>>(this.Error, e, true);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000185D4 File Offset: 0x000167D4
		private void TriggerEvent<T>(EventHandler<T> eventHandler, T e, bool inReversedOrder) where T : EventArgs
		{
			if (eventHandler != null)
			{
				Delegate[] invocationList = eventHandler.GetInvocationList();
				if (inReversedOrder)
				{
					for (int i = invocationList.Length - 1; i >= 0; i--)
					{
						this.ExecuteEventHandler<T>(e, invocationList[i]);
					}
					return;
				}
				for (int j = 0; j < invocationList.Length; j++)
				{
					this.ExecuteEventHandler<T>(e, invocationList[j]);
				}
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00018648 File Offset: 0x00016848
		private void ExecuteEventHandler<T>(T e, Delegate handler) where T : EventArgs
		{
			Type declaringType = handler.Method.DeclaringType;
			string str = (declaringType == null) ? "Global" : declaringType.Name;
			string name = handler.Method.Name;
			string text = str + "." + name;
			TaskLogger.Trace(Strings.LogFunctionEnter(declaringType, name, string.Join<ParameterInfo>(",", handler.Method.GetParameters())));
			using (new CmdletMonitoredScope(this.taskContext.UniqueId, "TaskModuleLatency", text, LoggerHelper.CmdletPerfMonitors))
			{
				ICriticalFeature feature = handler.Target as ICriticalFeature;
				feature.Execute(delegate
				{
					((EventHandler<T>)handler)(this, e);
				}, this.taskContext, text);
			}
			TaskLogger.Trace(Strings.LogFunctionExit(declaringType, name));
		}

		// Token: 0x04000160 RID: 352
		private readonly TaskContext taskContext;
	}
}
