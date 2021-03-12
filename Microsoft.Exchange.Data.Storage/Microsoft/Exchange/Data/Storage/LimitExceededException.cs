using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000745 RID: 1861
	[Serializable]
	public class LimitExceededException : StoragePermanentException
	{
		// Token: 0x06004819 RID: 18457 RVA: 0x0013099B File Offset: 0x0012EB9B
		public LimitExceededException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x001309A4 File Offset: 0x0012EBA4
		protected LimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
