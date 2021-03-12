using System;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000093 RID: 147
	internal class LocalMoveJob : MoveBaseJob
	{
		// Token: 0x06000748 RID: 1864 RVA: 0x00031BFD File Offset: 0x0002FDFD
		protected override void MigrateSecurityDescriptors()
		{
			MrsTracer.Service.Function("LocalMoveJob.MigrateSecurityDescriptors", new object[0]);
			base.ForeachMailboxContext(delegate(MailboxMover mbxCtx)
			{
				RawSecurityDescriptor mailboxSecurityDescriptor = mbxCtx.SourceMailbox.GetMailboxSecurityDescriptor();
				MrsTracer.Service.Debug("Loaded MailboxSD: {0}", new object[]
				{
					CommonUtils.GetSDDLString(mailboxSecurityDescriptor)
				});
				if (base.CleanupMailboxSD)
				{
					base.RemoveExchangeServersDenyACE(mailboxSecurityDescriptor, mbxCtx);
				}
				mbxCtx.DestMailbox.SetMailboxSecurityDescriptor(mailboxSecurityDescriptor);
			});
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00031CA0 File Offset: 0x0002FEA0
		protected override void UpdateMovedMailbox()
		{
			if (CommonUtils.IsImplicitSplit(base.CachedRequestJob.Flags, base.GetRootMailboxContext().SourceMailbox.GetADUser()))
			{
				throw new ImplicitSplitPermanentException();
			}
			ReportEntry[] entries = null;
			ADUser adUser = base.GetRootMailboxContext().DestMailbox.GetADUser();
			ConfigurableObjectXML configObj = ConfigurableObjectXML.Create(adUser);
			base.Report.Append(MrsStrings.ReportMailboxBeforeFinalization2(adUser.ToString(), adUser.OriginatingServer), configObj, ReportEntryFlags.Source | ReportEntryFlags.Before);
			try
			{
				MrsTracer.Service.Debug("Updating destination mailbox only (local move)...", new object[0]);
				UpdateMovedMailboxOperation op = base.CachedRequestJob.ArchiveOnly ? UpdateMovedMailboxOperation.UpdateArchiveOnly : UpdateMovedMailboxOperation.UpdateMailbox;
				Guid newArchiveDatabaseGuid;
				if (base.CachedRequestJob.ArchiveIsMoving)
				{
					newArchiveDatabaseGuid = base.CachedRequestJob.TargetArchiveMDBGuid;
				}
				else
				{
					newArchiveDatabaseGuid = ((adUser.ArchiveDatabase != null) ? adUser.ArchiveDatabase.ObjectGuid : Guid.Empty);
				}
				UpdateMovedMailboxFlags updateMovedMailboxFlags = UpdateMovedMailboxFlags.None;
				if (base.CachedRequestJob.SkipMailboxReleaseCheck)
				{
					updateMovedMailboxFlags |= UpdateMovedMailboxFlags.SkipMailboxReleaseCheck;
				}
				if (base.CachedRequestJob.SkipProvisioningCheck)
				{
					updateMovedMailboxFlags |= UpdateMovedMailboxFlags.SkipProvisioningCheck;
				}
				base.GetRootMailboxContext().DestMailbox.UpdateMovedMailbox(op, null, base.CachedRequestJob.DestDomainControllerToUpdate, out entries, base.CachedRequestJob.TargetMDBGuid, newArchiveDatabaseGuid, (adUser.ArchiveDomain != null) ? adUser.ArchiveDomain.ToString() : null, adUser.ArchiveStatus, updateMovedMailboxFlags, base.CachedRequestJob.TargetContainerGuid, base.CachedRequestJob.TargetUnifiedMailboxId);
			}
			finally
			{
				base.AppendReportEntries(entries);
			}
			CommonUtils.CatchKnownExceptions(delegate
			{
				adUser = this.GetRootMailboxContext().DestMailbox.GetADUser();
				configObj = ConfigurableObjectXML.Create(adUser);
				this.Report.Append(MrsStrings.ReportMailboxAfterFinalization2(adUser.ToString(), adUser.OriginatingServer), configObj, ReportEntryFlags.Target | ReportEntryFlags.After);
			}, null);
		}
	}
}
