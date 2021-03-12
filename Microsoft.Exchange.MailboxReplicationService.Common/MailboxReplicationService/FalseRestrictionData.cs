using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200005C RID: 92
	[DataContract]
	internal sealed class FalseRestrictionData : RestrictionData
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x00008BC8 File Offset: 0x00006DC8
		internal override Restriction GetRestriction()
		{
			return Restriction.False();
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00008BCF File Offset: 0x00006DCF
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new FalseFilter();
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00008BD6 File Offset: 0x00006DD6
		internal override string ToStringInternal()
		{
			return "FALSE";
		}
	}
}
