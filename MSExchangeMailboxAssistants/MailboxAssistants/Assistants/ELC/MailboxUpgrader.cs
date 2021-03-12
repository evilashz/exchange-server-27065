using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200006C RID: 108
	internal sealed class MailboxUpgrader
	{
		// Token: 0x060003DA RID: 986 RVA: 0x0001BE31 File Offset: 0x0001A031
		internal MailboxUpgrader(ElcUserTagInformation dataForTags)
		{
			this.dataForTags = dataForTags;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001BE40 File Offset: 0x0001A040
		public override string ToString()
		{
			return null;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001BE44 File Offset: 0x0001A044
		internal UpgradeStatus UpgradeIfNecessary()
		{
			UpgradeStatus result = UpgradeStatus.None;
			if (this.MailboxNeedsUpgrading())
			{
				ElcMailboxHelper.UpgradeElcMailbox(this.dataForTags.MailboxSession, this.dataForTags.AllAdTags, out result);
			}
			return result;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001BE7A File Offset: 0x0001A07A
		private bool MailboxNeedsUpgrading()
		{
			return true;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001BE7D File Offset: 0x0001A07D
		private void ClearManagedFolderProperties()
		{
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001BE7F File Offset: 0x0001A07F
		private void DeleteEmptyFolders()
		{
		}

		// Token: 0x0400030F RID: 783
		private static readonly Trace Tracer = ExTraceGlobals.TagProvisionerTracer;

		// Token: 0x04000310 RID: 784
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x04000311 RID: 785
		private ElcUserTagInformation dataForTags;
	}
}
