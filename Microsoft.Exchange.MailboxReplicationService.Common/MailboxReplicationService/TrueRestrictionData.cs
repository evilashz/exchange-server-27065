using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000081 RID: 129
	[DataContract]
	internal sealed class TrueRestrictionData : RestrictionData
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x0000A6EF File Offset: 0x000088EF
		internal override Restriction GetRestriction()
		{
			return Restriction.True();
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000A6F6 File Offset: 0x000088F6
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new TrueFilter();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000A6FD File Offset: 0x000088FD
		internal override string ToStringInternal()
		{
			return "TRUE";
		}
	}
}
