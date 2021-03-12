using System;
using System.Threading;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200004C RID: 76
	public class AgentAsyncContext
	{
		// Token: 0x060001BE RID: 446 RVA: 0x0000640A File Offset: 0x0000460A
		internal AgentAsyncContext(Agent agent)
		{
			this.callback = agent.Session.GetAgentAsyncCallback();
			this.agent = agent;
			this.completed = 0;
			this.agent.Session.OnStartAsyncAgent();
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00006441 File Offset: 0x00004641
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00006449 File Offset: 0x00004649
		public Exception AsyncException
		{
			get
			{
				return this.asyncException;
			}
			set
			{
				this.asyncException = value;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006452 File Offset: 0x00004652
		public virtual void Resume()
		{
			this.agent.Session.ResumeAgent();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006464 File Offset: 0x00004664
		public virtual void Complete()
		{
			if (Interlocked.Increment(ref this.completed) != 1)
			{
				throw new InvalidOperationException(string.Format("Agent '{0}' ({1}) completed its asynchronous operation more than once while handling event '{2}'", this.agent.Name, this.agent.Id, this.agent.EventTopic));
			}
			if (this.agent.SnapshotWriter != null)
			{
				this.agent.SnapshotWriter.WriteProcessedData(this.agent.SnapshotPrefix, this.agent.EventArgId, this.agent.EventTopic, this.agent.Name, this.agent.MailItem);
			}
			this.agent.AsyncComplete();
			this.agent.MailItem = null;
			this.callback(this);
		}

		// Token: 0x04000170 RID: 368
		private Agent agent;

		// Token: 0x04000171 RID: 369
		private AgentAsyncCallback callback;

		// Token: 0x04000172 RID: 370
		private int completed;

		// Token: 0x04000173 RID: 371
		private Exception asyncException;
	}
}
