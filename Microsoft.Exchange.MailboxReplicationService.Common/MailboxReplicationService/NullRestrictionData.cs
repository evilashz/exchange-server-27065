using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000061 RID: 97
	[DataContract]
	internal sealed class NullRestrictionData : RestrictionData
	{
		// Token: 0x060004C2 RID: 1218 RVA: 0x0000901B File Offset: 0x0000721B
		internal override Restriction GetRestriction()
		{
			return null;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000901E File Offset: 0x0000721E
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new NullFilter();
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00009025 File Offset: 0x00007225
		internal override string ToStringInternal()
		{
			return "NULL";
		}
	}
}
