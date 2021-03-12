using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.JobQueues
{
	// Token: 0x0200073B RID: 1851
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class Job
	{
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x0004A303 File Offset: 0x00048503
		// (set) Token: 0x060023E8 RID: 9192 RVA: 0x0004A30B File Offset: 0x0004850B
		public Exception LastError { get; protected set; }

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x0004A314 File Offset: 0x00048514
		// (set) Token: 0x060023EA RID: 9194 RVA: 0x0004A31C File Offset: 0x0004851C
		public string ClientString { get; private set; }

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x0004A325 File Offset: 0x00048525
		public bool IsShuttingdown
		{
			get
			{
				return this.queue.IsShuttingdown;
			}
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x0004A332 File Offset: 0x00048532
		public Job(JobQueue queue, Configuration config, string clientString)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			this.ClientString = clientString;
			this.queue = queue;
			this.Config = config;
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x0004A36B File Offset: 0x0004856B
		// (set) Token: 0x060023EE RID: 9198 RVA: 0x0004A373 File Offset: 0x00048573
		public Configuration Config { get; private set; }

		// Token: 0x060023EF RID: 9199
		public abstract void Begin(object state);

		// Token: 0x060023F0 RID: 9200 RVA: 0x0004A37C File Offset: 0x0004857C
		protected virtual void End()
		{
			this.queue.OnJobCompletion(this);
		}

		// Token: 0x040021BD RID: 8637
		protected readonly JobQueue queue;
	}
}
