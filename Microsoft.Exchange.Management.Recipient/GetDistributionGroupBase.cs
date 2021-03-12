using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200001E RID: 30
	public abstract class GetDistributionGroupBase : GetRecipientWithAddressListBase<DistributionGroupIdParameter, ADGroup>
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00008EF8 File Offset: 0x000070F8
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetDistributionGroupBase.SortPropertiesArray;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00008EFF File Offset: 0x000070FF
		protected override string SystemAddressListRdn
		{
			get
			{
				return "All Groups(VLV)";
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00008F06 File Offset: 0x00007106
		protected override RecipientType[] RecipientTypes
		{
			get
			{
				return GetDistributionGroupBase.RecipientTypesArray;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00008F0D File Offset: 0x0000710D
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return this.RecipientTypeDetails;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00008F15 File Offset: 0x00007115
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<DistributionGroupSchema>();
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00008F1C File Offset: 0x0000711C
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00008F33 File Offset: 0x00007133
		[Parameter]
		[ValidateNotNullOrEmpty]
		public RecipientTypeDetails[] RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails[])base.Fields["RecipientTypeDetails"];
			}
			set
			{
				base.VerifyValues<RecipientTypeDetails>(GetDistributionGroupBase.AllowedRecipientTypeDetails, value);
				base.Fields["RecipientTypeDetails"] = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00008F52 File Offset: 0x00007152
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00008F69 File Offset: 0x00007169
		[Parameter(ParameterSetName = "ManagedBySet")]
		public GeneralRecipientIdParameter ManagedBy
		{
			get
			{
				return (GeneralRecipientIdParameter)base.Fields["ManagedBy"];
			}
			set
			{
				base.Fields["ManagedBy"] = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00008F7C File Offset: 0x0000717C
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = base.InternalFilter;
				if (base.Fields.IsModified("ManagedBy"))
				{
					QueryFilter queryFilter2;
					if (this.managerId != null)
					{
						queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADGroupSchema.ManagedBy, this.managerId);
					}
					else
					{
						queryFilter2 = new AndFilter(new QueryFilter[]
						{
							new NotFilter(new ExistsFilter(ADGroupSchema.RawManagedBy)),
							new NotFilter(new ExistsFilter(ADGroupSchema.CoManagedBy))
						});
					}
					if (queryFilter != null)
					{
						queryFilter = new AndFilter(new QueryFilter[]
						{
							queryFilter2,
							queryFilter
						});
					}
					else
					{
						queryFilter = queryFilter2;
					}
				}
				return queryFilter;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00009010 File Offset: 0x00007210
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.ManagedBy != null)
			{
				ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(this.ManagedBy, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.RecipientNotFoundException(this.ManagedBy.ToString())), new LocalizedString?(Strings.RecipientNotUniqueException(this.ManagedBy.ToString())));
				if (base.HasErrors)
				{
					return;
				}
				this.managerId = (ADObjectId)adrecipient.Identity;
				if (!base.CurrentOrganizationId.Equals(adrecipient.OrganizationId))
				{
					base.CurrentOrganizationId = adrecipient.OrganizationId;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000090B1 File Offset: 0x000072B1
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return DistributionGroup.FromDataObject((ADGroup)dataObject);
		}

		// Token: 0x04000038 RID: 56
		private ADObjectId managerId;

		// Token: 0x04000039 RID: 57
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.MailNonUniversalGroup,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.MailUniversalDistributionGroup,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.MailUniversalSecurityGroup,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.RoomList
		};

		// Token: 0x0400003A RID: 58
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.Alias
		};

		// Token: 0x0400003B RID: 59
		private static readonly RecipientType[] RecipientTypesArray = new RecipientType[]
		{
			RecipientType.MailUniversalDistributionGroup,
			RecipientType.MailUniversalSecurityGroup,
			RecipientType.MailNonUniversalGroup
		};
	}
}
