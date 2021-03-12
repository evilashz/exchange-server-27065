using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200022F RID: 559
	internal class RestrictionConverter : IDataConverter<Restriction, RestrictionData>
	{
		// Token: 0x06001DE9 RID: 7657 RVA: 0x0003DADD File Offset: 0x0003BCDD
		Restriction IDataConverter<Restriction, RestrictionData>.GetNativeRepresentation(RestrictionData rd)
		{
			return rd.GetRestriction();
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0003DAE5 File Offset: 0x0003BCE5
		RestrictionData IDataConverter<Restriction, RestrictionData>.GetDataRepresentation(Restriction r)
		{
			return RestrictionData.GetRestrictionData(r);
		}
	}
}
