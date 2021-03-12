using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000773 RID: 1907
	[Serializable]
	public class ResourcesException : StorageTransientException
	{
		// Token: 0x060048B5 RID: 18613 RVA: 0x001316B8 File Offset: 0x0012F8B8
		public ResourcesException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x001316C1 File Offset: 0x0012F8C1
		public ResourcesException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x001316CB File Offset: 0x0012F8CB
		protected ResourcesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
