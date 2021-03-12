using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x020001C4 RID: 452
	internal class UserADCrawler : ADCrawler
	{
		// Token: 0x0600116F RID: 4463 RVA: 0x00066300 File Offset: 0x00064500
		public UserADCrawler(RunData runData) : base(runData)
		{
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x00066309 File Offset: 0x00064509
		public override string ADEntriesFileName
		{
			get
			{
				return "User";
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x00066310 File Offset: 0x00064510
		public override QueryFilter RecipientFilter
		{
			get
			{
				return GrammarRecipientHelper.GetUserFilter();
			}
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00066318 File Offset: 0x00064518
		protected override void UpdateRecipientCount(int recipientCount)
		{
			base.Logger.TraceDebug(this, "UpdateRecipientCount recipientCount={0}", new object[]
			{
				recipientCount
			});
			base.RunData.UserCount = recipientCount;
		}

		// Token: 0x04000AE3 RID: 2787
		public const string UserResultsFileName = "User";
	}
}
