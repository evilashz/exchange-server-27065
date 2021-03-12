using System;
using Microsoft.Exchange.Extensibility.EventLog;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x0200007A RID: 122
	internal sealed class ExecutionMonitor
	{
		// Token: 0x060003CF RID: 975 RVA: 0x00012E30 File Offset: 0x00011030
		public ExecutionMonitor(int executionTimeLimit, Dispatcher dispatcher)
		{
			this.executionTimeLimit = ((executionTimeLimit > 0) ? executionTimeLimit : 0);
			if (executionTimeLimit > 0)
			{
				this.dispatcher = dispatcher;
				this.dispatcher.OnAgentInvokeStart += this.AgentInvokeStartEventHandler;
				this.dispatcher.OnAgentInvokeEnd += this.AgentInvokeEndEventHandler;
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00012E8C File Offset: 0x0001108C
		public void Shutdown()
		{
			if (this.executionTimeLimit > 0 && this.dispatcher != null)
			{
				this.dispatcher.OnAgentInvokeStart -= this.AgentInvokeStartEventHandler;
				this.dispatcher.OnAgentInvokeEnd -= this.AgentInvokeEndEventHandler;
				this.dispatcher = null;
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00012EE0 File Offset: 0x000110E0
		internal void AgentInvokeStartEventHandler(object source, MExSession context)
		{
			this.agentName = context.ExecutingAgentName;
			this.eventTopic = context.OutstandingEventTopic;
			MailItem mailItem = context.CurrentAgent.Instance.MailItem;
			if (mailItem != null)
			{
				this.internetMessageId = mailItem.InternetMessageId;
			}
			this.agentInvocationTime = DateTime.UtcNow;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00012F30 File Offset: 0x00011130
		internal void AgentInvokeEndEventHandler(object source, MExSession context)
		{
			double totalMilliseconds = DateTime.UtcNow.Subtract(this.agentInvocationTime).TotalMilliseconds;
			if (totalMilliseconds > (double)this.executionTimeLimit && this.agentName != null && this.eventTopic != null)
			{
				MExDiagnostics.EventLog.LogEvent(EdgeExtensibilityEventLogConstants.Tuple_MExAgentTooSlow, null, new object[]
				{
					this.agentName,
					totalMilliseconds,
					this.eventTopic,
					this.internetMessageId ?? "Not Available"
				});
			}
			this.agentName = null;
			this.eventTopic = null;
			this.internetMessageId = null;
		}

		// Token: 0x04000485 RID: 1157
		private readonly int executionTimeLimit;

		// Token: 0x04000486 RID: 1158
		private Dispatcher dispatcher;

		// Token: 0x04000487 RID: 1159
		private string agentName;

		// Token: 0x04000488 RID: 1160
		private string eventTopic;

		// Token: 0x04000489 RID: 1161
		private string internetMessageId;

		// Token: 0x0400048A RID: 1162
		private DateTime agentInvocationTime;
	}
}
