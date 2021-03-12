using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000759 RID: 1881
	[Serializable]
	public class ObjectExistedException : StoragePermanentException
	{
		// Token: 0x0600485C RID: 18524 RVA: 0x00130EAB File Offset: 0x0012F0AB
		public ObjectExistedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x00130EB4 File Offset: 0x0012F0B4
		public ObjectExistedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600485E RID: 18526 RVA: 0x00130EBE File Offset: 0x0012F0BE
		protected ObjectExistedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
