using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000184 RID: 388
	[Serializable]
	public class NonMailEnabledGroupIdParameter : RecipientIdParameter
	{
		// Token: 0x06000E24 RID: 3620 RVA: 0x0002A34D File Offset: 0x0002854D
		public NonMailEnabledGroupIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0002A356 File Offset: 0x00028556
		public NonMailEnabledGroupIdParameter()
		{
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0002A35E File Offset: 0x0002855E
		public NonMailEnabledGroupIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0002A367 File Offset: 0x00028567
		public NonMailEnabledGroupIdParameter(WindowsGroup group) : base(group.Id)
		{
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0002A375 File Offset: 0x00028575
		public NonMailEnabledGroupIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0002A37E File Offset: 0x0002857E
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return NonMailEnabledGroupIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0002A388 File Offset: 0x00028588
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.AdditionalQueryFilter,
					RecipientIdParameter.GetRecipientTypeDetailsFilter(NonMailEnabledGroupIdParameter.AllowedRecipientTypeDetails)
				});
			}
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0002A3B8 File Offset: 0x000285B8
		public new static NonMailEnabledGroupIdParameter Parse(string identity)
		{
			return new NonMailEnabledGroupIdParameter(identity);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0002A3C0 File Offset: 0x000285C0
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeNonMailEnabledGroup(id);
		}

		// Token: 0x0400030A RID: 778
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.Group
		};

		// Token: 0x0400030B RID: 779
		internal static RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.NonUniversalGroup,
			RecipientTypeDetails.UniversalDistributionGroup,
			RecipientTypeDetails.UniversalSecurityGroup
		};
	}
}
