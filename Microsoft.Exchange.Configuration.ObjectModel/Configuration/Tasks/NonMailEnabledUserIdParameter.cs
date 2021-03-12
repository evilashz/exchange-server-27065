using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000183 RID: 387
	[Serializable]
	public class NonMailEnabledUserIdParameter : RecipientIdParameter
	{
		// Token: 0x06000E1A RID: 3610 RVA: 0x0002A296 File Offset: 0x00028496
		public NonMailEnabledUserIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0002A29F File Offset: 0x0002849F
		public NonMailEnabledUserIdParameter()
		{
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0002A2A7 File Offset: 0x000284A7
		public NonMailEnabledUserIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0002A2B0 File Offset: 0x000284B0
		public NonMailEnabledUserIdParameter(User user) : base(user.Id)
		{
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0002A2BE File Offset: 0x000284BE
		public NonMailEnabledUserIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0002A2C7 File Offset: 0x000284C7
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return NonMailEnabledUserIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0002A2D0 File Offset: 0x000284D0
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.AdditionalQueryFilter,
					RecipientIdParameter.GetRecipientTypeDetailsFilter(NonMailEnabledUserIdParameter.AllowedRecipientTypeDetails)
				});
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0002A300 File Offset: 0x00028500
		public new static NonMailEnabledUserIdParameter Parse(string identity)
		{
			return new NonMailEnabledUserIdParameter(identity);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0002A308 File Offset: 0x00028508
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeNonMailEnabledUser(id);
		}

		// Token: 0x04000308 RID: 776
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.User
		};

		// Token: 0x04000309 RID: 777
		internal static RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.User,
			RecipientTypeDetails.DisabledUser
		};
	}
}
