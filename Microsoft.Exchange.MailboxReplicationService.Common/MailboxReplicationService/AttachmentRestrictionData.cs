using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000055 RID: 85
	[DataContract]
	internal sealed class AttachmentRestrictionData : HierRestrictionData
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x00008194 File Offset: 0x00006394
		public AttachmentRestrictionData()
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000819C File Offset: 0x0000639C
		internal AttachmentRestrictionData(Restriction.AttachmentRestriction r)
		{
			base.ParseRestrictions(new Restriction[]
			{
				r.Restriction
			});
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000081C6 File Offset: 0x000063C6
		internal AttachmentRestrictionData(StoreSession storeSession, SubFilter filter)
		{
			base.ParseQueryFilter(storeSession, filter.Filter);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000081DB File Offset: 0x000063DB
		internal override Restriction GetRestriction()
		{
			return Restriction.Sub(PropTag.MessageAttachments, base.GetRestrictions()[0]);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000081EF File Offset: 0x000063EF
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new SubFilter(SubFilterProperties.Attachments, base.GetQueryFilters(storeSession)[0]);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00008200 File Offset: 0x00006400
		internal override string ToStringInternal()
		{
			return string.Format("ATTACHMENT[{0}]", base.Restrictions[0].ToStringInternal());
		}
	}
}
