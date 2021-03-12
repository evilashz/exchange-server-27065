using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000070 RID: 112
	internal sealed class TagProvisioner
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x0001C6AE File Offset: 0x0001A8AE
		internal TagProvisioner(ElcTagSubAssistant assistant)
		{
			this.elcAssistant = assistant;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001C6BD File Offset: 0x0001A8BD
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Tag provisioner for " + this.elcAssistant.DatabaseInfo.ToString();
			}
			return this.toString;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001C6ED File Offset: 0x0001A8ED
		internal void OnWindowBegin()
		{
			TagProvisioner.Tracer.TraceDebug<TagProvisioner>((long)this.GetHashCode(), "{0}: OnWindowBegin called.", this);
			TagProvisioner.TracerPfd.TracePfd<int, TagProvisioner>((long)this.GetHashCode(), "PFD IWE {0} {1}: OnWindowBegin called ", 23831, this);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001C722 File Offset: 0x0001A922
		internal void OnWindowEnd()
		{
			TagProvisioner.Tracer.TraceDebug<TagProvisioner>((long)this.GetHashCode(), "{0}: OnWindowEnd called.", this);
			TagProvisioner.Tracer.TraceDebug<int, TagProvisioner>((long)this.GetHashCode(), "PFD IWE {0} {1}: OnWindowEnd called.", 23191, this);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001C758 File Offset: 0x0001A958
		internal bool Invoke(MailboxDataForTags mailboxDataForTags)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			bool result;
			try
			{
				result = this.InvokeInternal(mailboxDataForTags);
			}
			finally
			{
				mailboxDataForTags.StatisticsLogEntry.TagProvisionerProcessingTime = stopwatch.ElapsedMilliseconds;
			}
			return result;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001C798 File Offset: 0x0001A998
		private bool InvokeInternal(MailboxDataForTags mailboxDataForTags)
		{
			TagProvisioner.Tracer.TraceDebug<TagProvisioner, IExchangePrincipal>((long)this.GetHashCode(), "{0}: TagProvisioner invoked for for mailbox '{1}'.", this, mailboxDataForTags.MailboxSession.MailboxOwner);
			ElcUserTagInformation elcUserTagInformation = (ElcUserTagInformation)mailboxDataForTags.ElcUserInformation;
			bool flag = false;
			TagChange tagChange = new TagChange();
			if (mailboxDataForTags.MailboxSession.MailboxOwner.RecipientType != RecipientType.MailUser)
			{
				MailboxUpgrader mailboxUpgrader = new MailboxUpgrader(elcUserTagInformation);
				UpgradeStatus upgradeStatus = mailboxUpgrader.UpgradeIfNecessary();
				ADChangeDetector adchangeDetector = new ADChangeDetector(mailboxDataForTags);
				tagChange = adchangeDetector.Detect();
				if (tagChange.ChangeType != ChangeType.None || (upgradeStatus & UpgradeStatus.AppliedFolderTag) != UpgradeStatus.None)
				{
					flag = true;
					elcUserTagInformation.FullCrawlRequired = true;
				}
			}
			else if (elcUserTagInformation.FullCrawlRequired)
			{
				flag = true;
			}
			mailboxDataForTags.StatisticsLogEntry.IsFullCrawlNeeded = flag;
			Synchronizer synchronizer = new Synchronizer(mailboxDataForTags, this.elcAssistant, flag);
			synchronizer.Invoke();
			return elcUserTagInformation.SaveConfigItem(mailboxDataForTags.ArchiveProcessor);
		}

		// Token: 0x0400032E RID: 814
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.TagProvisionerTracer;

		// Token: 0x0400032F RID: 815
		private static readonly Microsoft.Exchange.Diagnostics.Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x04000330 RID: 816
		private ElcTagSubAssistant elcAssistant;

		// Token: 0x04000331 RID: 817
		private string toString;
	}
}
