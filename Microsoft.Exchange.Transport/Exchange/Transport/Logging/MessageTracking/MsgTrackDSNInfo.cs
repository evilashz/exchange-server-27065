using System;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Logging.MessageTracking
{
	// Token: 0x0200008A RID: 138
	internal class MsgTrackDSNInfo
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x0001472B File Offset: 0x0001292B
		public MsgTrackDSNInfo(string origMessageId, DsnFlags dsnType) : this(origMessageId, dsnType, string.Empty)
		{
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001473A File Offset: 0x0001293A
		public MsgTrackDSNInfo(string origMessageId, DsnFlags dsnType, string originalDsnSender)
		{
			this.origMessageId = origMessageId;
			this.dsnType = dsnType;
			this.originalDsnSender = originalDsnSender;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00014757 File Offset: 0x00012957
		internal string OrigMessageID
		{
			get
			{
				return this.origMessageId;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0001475F File Offset: 0x0001295F
		internal DsnFlags DsnType
		{
			get
			{
				return this.dsnType;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00014767 File Offset: 0x00012967
		internal string OriginalDsnSender
		{
			get
			{
				return this.originalDsnSender;
			}
		}

		// Token: 0x0400025F RID: 607
		private readonly string origMessageId;

		// Token: 0x04000260 RID: 608
		private readonly DsnFlags dsnType;

		// Token: 0x04000261 RID: 609
		private readonly string originalDsnSender;
	}
}
