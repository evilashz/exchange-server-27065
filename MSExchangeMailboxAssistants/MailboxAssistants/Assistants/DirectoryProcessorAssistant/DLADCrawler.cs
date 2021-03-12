using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x0200019D RID: 413
	internal class DLADCrawler : ADCrawler
	{
		// Token: 0x0600103E RID: 4158 RVA: 0x0005EBAC File Offset: 0x0005CDAC
		public DLADCrawler(RunData runData) : base(runData)
		{
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0005EBB5 File Offset: 0x0005CDB5
		public override string ADEntriesFileName
		{
			get
			{
				return "DistributionList";
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0005EBBC File Offset: 0x0005CDBC
		public override QueryFilter RecipientFilter
		{
			get
			{
				return GrammarRecipientHelper.GetDLFilter();
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0005EBC3 File Offset: 0x0005CDC3
		protected override void UpdateRecipientCount(int recipientCount)
		{
		}

		// Token: 0x04000A3A RID: 2618
		public const string DLResultsFileName = "DistributionList";
	}
}
