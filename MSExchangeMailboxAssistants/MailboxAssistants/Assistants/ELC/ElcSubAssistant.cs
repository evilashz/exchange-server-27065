using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000045 RID: 69
	internal abstract class ElcSubAssistant : IDisposable
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
		public ElcSubAssistant(DatabaseInfo databaseInfo, ELCHealthMonitor healthMonitor)
		{
			this.databaseInfo = databaseInfo;
			this.healthMonitor = healthMonitor;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000EEDA File Offset: 0x0000D0DA
		internal DatabaseInfo DatabaseInfo
		{
			get
			{
				return this.databaseInfo;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000EEE2 File Offset: 0x0000D0E2
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000EEEA File Offset: 0x0000D0EA
		protected string DebugString { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000EEF3 File Offset: 0x0000D0F3
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000EEFB File Offset: 0x0000D0FB
		protected internal ELCAssistantType ElcAssistantType { get; protected set; }

		// Token: 0x06000282 RID: 642 RVA: 0x0000EF04 File Offset: 0x0000D104
		public virtual void Dispose()
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000EF08 File Offset: 0x0000D108
		public virtual void OnShutdown()
		{
			ElcSubAssistant.Tracer.TraceDebug<ElcSubAssistant>((long)this.GetHashCode(), "{0}: OnShutdown started", this);
			this.shutdown = true;
			ElcSubAssistant.Tracer.TraceDebug<ElcSubAssistant>((long)this.GetHashCode(), "{0}: OnShutdown completed", this);
			ElcSubAssistant.TracerPfd.TracePfd<int, ElcSubAssistant>((long)this.GetHashCode(), "PFD IWS {0} {1}: Shutdown", 23319, this);
		}

		// Token: 0x06000284 RID: 644
		internal abstract void OnWindowBegin();

		// Token: 0x06000285 RID: 645
		internal abstract void OnWindowEnd();

		// Token: 0x06000286 RID: 646 RVA: 0x0000EF66 File Offset: 0x0000D166
		internal Participant GetMSExchangeAccount(OrganizationId orgID)
		{
			return this.ElcAssistantType.AdCache.GetMSExchangeAccount(orgID);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000EF79 File Offset: 0x0000D179
		internal void ThrowIfShuttingDown(IExchangePrincipal mailboxOwner)
		{
			if (this.shutdown)
			{
				ElcSubAssistant.Tracer.TraceDebug<ElcSubAssistant, IExchangePrincipal>((long)this.GetHashCode(), "{0}: shutdown called during processing of mailbox '{1}'.", this, mailboxOwner);
				throw new ShutdownException();
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public void EnableLoadTrackingOnSession(MailboxSession mailboxSession)
		{
			this.healthMonitor.EnableLoadTrackingOnSession(mailboxSession);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000EFAF File Offset: 0x0000D1AF
		public void ThrottleStoreCall()
		{
			this.healthMonitor.ThrottleStoreCall();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000EFBD File Offset: 0x0000D1BD
		public void ThrottleStoreCallAndCheckForShutdown(IExchangePrincipal mailboxOwner)
		{
			this.ThrottleStoreCallAndCheckForShutdown(mailboxOwner, null);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000EFC7 File Offset: 0x0000D1C7
		public void ThrottleStoreCallAndCheckForShutdown(IExchangePrincipal mailboxOwner, List<ResourceKey> archiveResourceDependencies)
		{
			this.ThrowIfShuttingDown(mailboxOwner);
			this.healthMonitor.ThrottleStoreAndArchiveCall(archiveResourceDependencies);
			this.ThrowIfShuttingDown(mailboxOwner);
		}

		// Token: 0x0400022A RID: 554
		protected static readonly Trace Tracer = ExTraceGlobals.ELCAssistantTracer;

		// Token: 0x0400022B RID: 555
		protected static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x0400022C RID: 556
		private DatabaseInfo databaseInfo;

		// Token: 0x0400022D RID: 557
		private ELCHealthMonitor healthMonitor;

		// Token: 0x0400022E RID: 558
		private bool shutdown;
	}
}
