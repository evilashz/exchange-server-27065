using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000C6 RID: 198
	[Serializable]
	public class ObjectNotFoundException : StoragePermanentException
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x00067959 File Offset: 0x00065B59
		public ObjectNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00067962 File Offset: 0x00065B62
		public ObjectNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0006796C File Offset: 0x00065B6C
		protected ObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
