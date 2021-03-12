using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000187 RID: 391
	internal sealed class PostItemFromProperty : FromProperty
	{
		// Token: 0x06000B1B RID: 2843 RVA: 0x000350DD File Offset: 0x000332DD
		public PostItemFromProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000350E6 File Offset: 0x000332E6
		public new static PostItemFromProperty CreateCommand(CommandContext commandContext)
		{
			return new PostItemFromProperty(commandContext);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000350EE File Offset: 0x000332EE
		protected override Participant GetParticipant(Item storeItem)
		{
			return ((PostItem)storeItem).From;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000350FB File Offset: 0x000332FB
		protected override void SetParticipant(Item storeItem, Participant participant)
		{
			((PostItem)storeItem).From = participant;
		}
	}
}
