using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000188 RID: 392
	internal sealed class PostItemSenderProperty : SenderProperty
	{
		// Token: 0x06000B1F RID: 2847 RVA: 0x00035109 File Offset: 0x00033309
		public PostItemSenderProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00035112 File Offset: 0x00033312
		public new static PostItemSenderProperty CreateCommand(CommandContext commandContext)
		{
			return new PostItemSenderProperty(commandContext);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0003511A File Offset: 0x0003331A
		protected override Participant GetParticipant(Item storeItem)
		{
			return ((PostItem)storeItem).Sender;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00035127 File Offset: 0x00033327
		protected override void SetParticipant(Item storeItem, Participant participant)
		{
			((PostItem)storeItem).Sender = participant;
		}
	}
}
