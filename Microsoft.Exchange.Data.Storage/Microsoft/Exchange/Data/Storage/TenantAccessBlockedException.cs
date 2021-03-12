using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000782 RID: 1922
	[Serializable]
	public class TenantAccessBlockedException : StoragePermanentException
	{
		// Token: 0x060048E0 RID: 18656 RVA: 0x0013193A File Offset: 0x0012FB3A
		public TenantAccessBlockedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x00131943 File Offset: 0x0012FB43
		public TenantAccessBlockedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x0013194D File Offset: 0x0012FB4D
		protected TenantAccessBlockedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
