using System;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x0200001F RID: 31
	internal class RecipientHistory
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000B186 File Offset: 0x00009386
		internal IHistoryRecordFacade FirstRecord
		{
			get
			{
				return this.firstRecord;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000B18E File Offset: 0x0000938E
		internal RecipientP2Type P2Type
		{
			get
			{
				return this.p2Type;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000B198 File Offset: 0x00009398
		internal RecipientHistory(IHistoryFacade globalHistory, IHistoryFacade perRecipientHistory)
		{
			if (globalHistory != null)
			{
				this.p2Type = globalHistory.RecipientType;
				if (globalHistory.Records != null && globalHistory.Records.Count > 0)
				{
					this.firstRecord = globalHistory.Records[0];
					return;
				}
			}
			if (perRecipientHistory != null)
			{
				if (this.p2Type == RecipientP2Type.Unknown)
				{
					this.p2Type = perRecipientHistory.RecipientType;
				}
				if (perRecipientHistory.Records != null && perRecipientHistory.Records.Count > 0)
				{
					this.firstRecord = perRecipientHistory.Records[0];
				}
			}
		}

		// Token: 0x040000B9 RID: 185
		private IHistoryRecordFacade firstRecord;

		// Token: 0x040000BA RID: 186
		private RecipientP2Type p2Type;
	}
}
