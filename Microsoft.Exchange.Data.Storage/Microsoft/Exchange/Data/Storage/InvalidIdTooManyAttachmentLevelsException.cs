using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D7C RID: 3452
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InvalidIdTooManyAttachmentLevelsException : StoragePermanentException
	{
		// Token: 0x06007719 RID: 30489 RVA: 0x0020DB17 File Offset: 0x0020BD17
		internal InvalidIdTooManyAttachmentLevelsException() : base(LocalizedString.Empty)
		{
		}

		// Token: 0x0600771A RID: 30490 RVA: 0x0020DB24 File Offset: 0x0020BD24
		private InvalidIdTooManyAttachmentLevelsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
