using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000715 RID: 1813
	[Serializable]
	public class AttachmentExceededException : StoragePermanentException
	{
		// Token: 0x0600478F RID: 18319 RVA: 0x0013022B File Offset: 0x0012E42B
		public AttachmentExceededException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x00130235 File Offset: 0x0012E435
		protected AttachmentExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
