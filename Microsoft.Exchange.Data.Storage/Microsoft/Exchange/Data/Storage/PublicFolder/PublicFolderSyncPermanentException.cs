using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x0200094F RID: 2383
	[Serializable]
	public class PublicFolderSyncPermanentException : StoragePermanentException
	{
		// Token: 0x060058A6 RID: 22694 RVA: 0x0016CAC4 File Offset: 0x0016ACC4
		public PublicFolderSyncPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060058A7 RID: 22695 RVA: 0x0016CACD File Offset: 0x0016ACCD
		public PublicFolderSyncPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060058A8 RID: 22696 RVA: 0x0016CAD7 File Offset: 0x0016ACD7
		protected PublicFolderSyncPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
