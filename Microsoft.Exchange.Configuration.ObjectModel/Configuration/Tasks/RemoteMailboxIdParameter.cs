using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200017B RID: 379
	[Serializable]
	public class RemoteMailboxIdParameter : MailUserIdParameterBase
	{
		// Token: 0x06000DAA RID: 3498 RVA: 0x00028D94 File Offset: 0x00026F94
		public RemoteMailboxIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00028D9D File Offset: 0x00026F9D
		public RemoteMailboxIdParameter()
		{
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00028DA5 File Offset: 0x00026FA5
		public RemoteMailboxIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00028DAE File Offset: 0x00026FAE
		public RemoteMailboxIdParameter(RemoteMailbox remoteMailbox) : base(remoteMailbox.Id)
		{
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00028DBC File Offset: 0x00026FBC
		public RemoteMailboxIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00028DC8 File Offset: 0x00026FC8
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.AdditionalQueryFilter,
					RemoteMailboxIdParameter.GetRemoteMailboxFilter()
				});
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00028DF3 File Offset: 0x00026FF3
		public new static RemoteMailboxIdParameter Parse(string identity)
		{
			return new RemoteMailboxIdParameter(identity);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00028DFC File Offset: 0x00026FFC
		internal static QueryFilter GetRemoteMailboxFilter()
		{
			return RecipientIdParameter.GetRecipientTypeDetailsFilter(new RecipientTypeDetails[]
			{
				(RecipientTypeDetails)((ulong)int.MinValue),
				RecipientTypeDetails.RemoteRoomMailbox,
				RecipientTypeDetails.RemoteEquipmentMailbox,
				RecipientTypeDetails.RemoteSharedMailbox,
				RecipientTypeDetails.RemoteTeamMailbox
			});
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00028E4F File Offset: 0x0002704F
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeRemoteMailbox(id);
		}
	}
}
