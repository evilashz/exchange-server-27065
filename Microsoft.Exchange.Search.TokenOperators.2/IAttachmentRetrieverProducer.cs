using System;
using Microsoft.Ceres.Evaluation.DataModel;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x0200000F RID: 15
	internal interface IAttachmentRetrieverProducer
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D1 RID: 209
		int ErrorCodeIndex { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D2 RID: 210
		int LastAttemptTimeIndex { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D3 RID: 211
		int AttachmentFileNamesIndex { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D4 RID: 212
		int AttachmentsIndex { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D5 RID: 213
		IUpdateableRecord Holder { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D6 RID: 214
		string FlowIdentifier { get; }
	}
}
