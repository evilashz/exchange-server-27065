using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000A0 RID: 160
	internal sealed class SysCleanupEnforcerManager
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x0002F499 File Offset: 0x0002D699
		internal SysCleanupEnforcerManager(SysCleanupSubAssistant sysCleanupSubAssistant)
		{
			this.sysCleanupSubAssistant = sysCleanupSubAssistant;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0002F4A8 File Offset: 0x0002D6A8
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "System Cleanup EnforcerManager manager for " + this.sysCleanupSubAssistant.DatabaseInfo.ToString();
			}
			return this.toString;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0002F4D8 File Offset: 0x0002D6D8
		internal void Invoke(MailboxDataForTags mailboxDataForTags, ElcParameters parameters)
		{
			SysCleanupEnforcerManager.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Invoke called.", new object[]
			{
				TraceContext.Get()
			});
			ICollection<SysCleanupEnforcerBase> collection;
			if (parameters != ElcParameters.None)
			{
				collection = new List<SysCleanupEnforcerBase>();
				if ((parameters & ElcParameters.HoldCleanup) == ElcParameters.HoldCleanup)
				{
					collection.Add(new HoldCleanupEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant));
				}
				if ((parameters & ElcParameters.EHAHiddenFolderCleanup) == ElcParameters.EHAHiddenFolderCleanup)
				{
					collection.Add(new EHAHiddenFolderCleanupEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant));
				}
			}
			else
			{
				collection = new List<SysCleanupEnforcerBase>(new SysCleanupEnforcerBase[]
				{
					new MigrateToArchiveEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant),
					new DumpsterExpirationEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant),
					new CalendarLogExpirationEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant),
					new AuditExpirationEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant),
					new DumpsterQuotaEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant),
					new AuditQuotaEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant),
					new SupplementExpirationEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant),
					new EHAQuotaWarningEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant),
					new EHAMigratedMessageMoveEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant),
					new EHAMigratedMessageDeletionEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant),
					new DiscoveryHoldEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant)
				});
				if (mailboxDataForTags.HoldCleanupFolderType != DefaultFolderType.None)
				{
					collection.Add(new HoldCleanupEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant));
				}
				if (mailboxDataForTags.IsEHAHiddenFolderWatermarkSet())
				{
					collection.Add(new EHAHiddenFolderCleanupEnforcer(mailboxDataForTags, this.sysCleanupSubAssistant));
				}
			}
			foreach (SysCleanupEnforcerBase sysCleanupEnforcerBase in collection)
			{
				this.sysCleanupSubAssistant.ThrowIfShuttingDown(mailboxDataForTags.MailboxSession.MailboxOwner);
				SysCleanupEnforcerManager.Tracer.TraceDebug<object, SysCleanupEnforcerBase>((long)this.GetHashCode(), "{0}: Calling enabled enforcer '{1}'.", TraceContext.Get(), sysCleanupEnforcerBase);
				mailboxDataForTags.StatisticsLogEntry.LastProcessedEnforcer = sysCleanupEnforcerBase.GetType().Name;
				sysCleanupEnforcerBase.Invoke();
			}
		}

		// Token: 0x0400048C RID: 1164
		private static readonly Trace Tracer = ExTraceGlobals.CommonCleanupEnforcerOperationsTracer;

		// Token: 0x0400048D RID: 1165
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x0400048E RID: 1166
		private SysCleanupSubAssistant sysCleanupSubAssistant;

		// Token: 0x0400048F RID: 1167
		private string toString;
	}
}
