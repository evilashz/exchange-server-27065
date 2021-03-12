using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000716 RID: 1814
	[Serializable]
	public class AttachmentOpenException : StoragePermanentException
	{
		// Token: 0x06004791 RID: 18321 RVA: 0x0013023F File Offset: 0x0012E43F
		public AttachmentOpenException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x00130249 File Offset: 0x0012E449
		protected AttachmentOpenException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
