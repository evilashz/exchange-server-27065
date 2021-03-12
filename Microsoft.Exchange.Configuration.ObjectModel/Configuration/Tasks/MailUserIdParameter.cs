using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000175 RID: 373
	[Serializable]
	public class MailUserIdParameter : MailUserIdParameterBase
	{
		// Token: 0x06000D70 RID: 3440 RVA: 0x00028A26 File Offset: 0x00026C26
		public MailUserIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00028A2F File Offset: 0x00026C2F
		public MailUserIdParameter()
		{
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00028A37 File Offset: 0x00026C37
		public MailUserIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00028A40 File Offset: 0x00026C40
		public MailUserIdParameter(MailUser user) : base(user.Id)
		{
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00028A4E File Offset: 0x00026C4E
		public MailUserIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00028A58 File Offset: 0x00026C58
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.AdditionalQueryFilter,
					MailUserIdParameter.GetMailUserRecipientTypeDetailsFilter()
				});
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00028A83 File Offset: 0x00026C83
		public new static MailUserIdParameter Parse(string identity)
		{
			return new MailUserIdParameter(identity);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00028A8C File Offset: 0x00026C8C
		internal static QueryFilter GetMailUserRecipientTypeDetailsFilter()
		{
			return RecipientIdParameter.GetRecipientTypeDetailsFilter(new RecipientTypeDetails[]
			{
				RecipientTypeDetails.MailUser
			});
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00028AAF File Offset: 0x00026CAF
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeMailUser(id);
		}
	}
}
