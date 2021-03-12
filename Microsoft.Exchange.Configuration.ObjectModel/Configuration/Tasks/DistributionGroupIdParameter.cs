using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000168 RID: 360
	[Serializable]
	public class DistributionGroupIdParameter : RecipientIdParameter
	{
		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002808F File Offset: 0x0002628F
		public DistributionGroupIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00028098 File Offset: 0x00026298
		public DistributionGroupIdParameter()
		{
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x000280A0 File Offset: 0x000262A0
		public DistributionGroupIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x000280A9 File Offset: 0x000262A9
		public DistributionGroupIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x000280B2 File Offset: 0x000262B2
		public DistributionGroupIdParameter(DistributionGroup dl) : base(dl.Id)
		{
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x000280C0 File Offset: 0x000262C0
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return DistributionGroupIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x000280C7 File Offset: 0x000262C7
		public new static DistributionGroupIdParameter Parse(string identity)
		{
			return new DistributionGroupIdParameter(identity);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000280CF File Offset: 0x000262CF
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeDistributionGroup(id);
		}

		// Token: 0x040002E8 RID: 744
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.MailUniversalDistributionGroup,
			RecipientType.MailUniversalSecurityGroup,
			RecipientType.MailNonUniversalGroup
		};
	}
}
