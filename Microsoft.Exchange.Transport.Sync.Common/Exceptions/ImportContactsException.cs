using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Exceptions
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ImportContactsException : LocalizedException
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00004F54 File Offset: 0x00003154
		public ImportContactsException(LocalizedString localizedString) : base(localizedString)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004F5D File Offset: 0x0000315D
		public ImportContactsException(LocalizedString localizedString, Exception innerException) : base(localizedString, innerException)
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004F67 File Offset: 0x00003167
		protected ImportContactsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
