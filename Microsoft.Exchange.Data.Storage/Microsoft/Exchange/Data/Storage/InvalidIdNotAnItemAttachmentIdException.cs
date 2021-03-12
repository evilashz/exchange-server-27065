using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D7E RID: 3454
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InvalidIdNotAnItemAttachmentIdException : StoragePermanentException
	{
		// Token: 0x0600771D RID: 30493 RVA: 0x0020DB45 File Offset: 0x0020BD45
		internal InvalidIdNotAnItemAttachmentIdException() : base(LocalizedString.Empty)
		{
		}

		// Token: 0x0600771E RID: 30494 RVA: 0x0020DB52 File Offset: 0x0020BD52
		private InvalidIdNotAnItemAttachmentIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
