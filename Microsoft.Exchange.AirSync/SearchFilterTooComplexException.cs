using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000244 RID: 580
	[Serializable]
	internal class SearchFilterTooComplexException : AirSyncPermanentException
	{
		// Token: 0x06001541 RID: 5441 RVA: 0x0007C81D File Offset: 0x0007AA1D
		internal SearchFilterTooComplexException() : base(StatusCode.Sync_ObjectNotFound, false)
		{
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0007C827 File Offset: 0x0007AA27
		protected SearchFilterTooComplexException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
