using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Assistants;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000016 RID: 22
	internal sealed class AssistantCollectionEntry : Base
	{
		// Token: 0x06000069 RID: 105 RVA: 0x000041F7 File Offset: 0x000023F7
		public AssistantCollectionEntry(AssistantType assistantType, DatabaseInfo databaseInfo)
		{
			this.assistantType = assistantType;
			this.Instance = assistantType.CreateInstance(databaseInfo);
			this.State = AssistantCollectionEntry.AssistantState.Constructed;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000421A File Offset: 0x0000241A
		public AssistantType Type
		{
			get
			{
				return this.assistantType;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004222 File Offset: 0x00002422
		// (set) Token: 0x0600006C RID: 108 RVA: 0x0000422A File Offset: 0x0000242A
		public IEventBasedAssistant Instance { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004233 File Offset: 0x00002433
		public Guid Identity
		{
			get
			{
				return this.assistantType.Identity;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00004240 File Offset: 0x00002440
		public string Name
		{
			get
			{
				return this.Instance.Name;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004252 File Offset: 0x00002452
		public PerformanceCountersPerAssistantInstance PerformanceCounters
		{
			get
			{
				return this.assistantType.PerformanceCounters;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000425F File Offset: 0x0000245F
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00004267 File Offset: 0x00002467
		private AssistantCollectionEntry.AssistantState State { get; set; }

		// Token: 0x06000072 RID: 114 RVA: 0x00004270 File Offset: 0x00002470
		public void Start(EventBasedStartInfo startInfo)
		{
			AIBreadcrumbs.StartupTrail.Drop("Starting event assistant: " + this.Instance.Name);
			this.State = AssistantCollectionEntry.AssistantState.Starting;
			this.Instance.OnStart(startInfo);
			this.State = AssistantCollectionEntry.AssistantState.Started;
			AIBreadcrumbs.StartupTrail.Drop("Finished starting " + this.Instance.Name);
			base.TracePfd("PFD AIS {0} Start request for {1} ", new object[]
			{
				24983,
				this.Instance
			});
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004308 File Offset: 0x00002508
		public void Shutdown()
		{
			if (this.State == AssistantCollectionEntry.AssistantState.Started)
			{
				base.TracePfd("PFD AIS {0} Shutdown requested for assistant {1}", new object[]
				{
					16791,
					this.Instance
				});
				this.State = AssistantCollectionEntry.AssistantState.Stopping;
				AIBreadcrumbs.ShutdownTrail.Drop("Shutting down event assistant: " + this.Instance.Name);
				this.Instance.OnShutdown();
				this.State = AssistantCollectionEntry.AssistantState.Stopped;
				AIBreadcrumbs.ShutdownTrail.Drop("Finished shutting down " + this.Instance.Name);
				return;
			}
			ExTraceGlobals.EventBasedAssistantCollectionTracer.TraceDebug<LocalizedString, AssistantCollectionEntry.AssistantState>((long)this.GetHashCode(), "Entry for {0}: Shutdown requested for this assistant while current state was {1}.", this.Instance.Name, this.State);
		}

		// Token: 0x040000AA RID: 170
		private AssistantType assistantType;

		// Token: 0x02000017 RID: 23
		private enum AssistantState
		{
			// Token: 0x040000AE RID: 174
			Constructed,
			// Token: 0x040000AF RID: 175
			Starting,
			// Token: 0x040000B0 RID: 176
			Started,
			// Token: 0x040000B1 RID: 177
			Stopping,
			// Token: 0x040000B2 RID: 178
			Stopped
		}
	}
}
