using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002F3 RID: 755
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct RoutingTypeOptionsData
	{
		// Token: 0x06002260 RID: 8800 RVA: 0x00089FA3 File Offset: 0x000881A3
		internal RoutingTypeOptionsData(byte[] messageData, byte[] recipientData, byte[] helpFileName, byte[] helpFileData)
		{
			this.MessageData = messageData;
			this.RecipientData = recipientData;
			this.HelpFileName = helpFileName;
			this.HelpFileData = helpFileData;
		}

		// Token: 0x040013F1 RID: 5105
		public readonly byte[] MessageData;

		// Token: 0x040013F2 RID: 5106
		public readonly byte[] RecipientData;

		// Token: 0x040013F3 RID: 5107
		public readonly byte[] HelpFileName;

		// Token: 0x040013F4 RID: 5108
		public readonly byte[] HelpFileData;
	}
}
