using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x02000950 RID: 2384
	[Serializable]
	public class PublicFolderSyncTransientException : StorageTransientException
	{
		// Token: 0x060058A9 RID: 22697 RVA: 0x0016CAE1 File Offset: 0x0016ACE1
		public PublicFolderSyncTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060058AA RID: 22698 RVA: 0x0016CAEA File Offset: 0x0016ACEA
		public PublicFolderSyncTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060058AB RID: 22699 RVA: 0x0016CAF4 File Offset: 0x0016ACF4
		protected PublicFolderSyncTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
