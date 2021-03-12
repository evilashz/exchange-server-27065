using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000021 RID: 33
	public abstract class GetDynamicDistributionGroupBase : GetRecipientBase<DynamicGroupIdParameter, ADDynamicGroup>
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000931D File Offset: 0x0000751D
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetDynamicDistributionGroupBase.SortPropertiesArray;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00009324 File Offset: 0x00007524
		protected override RecipientType[] RecipientTypes
		{
			get
			{
				return GetDynamicDistributionGroupBase.RecipientTypesArray;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000932B File Offset: 0x0000752B
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<DynamicDistributionGroupSchema>();
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00009332 File Offset: 0x00007532
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00009349 File Offset: 0x00007549
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

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000935C File Offset: 0x0000755C
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = base.InternalFilter;
				if (this.managerId != null)
				{
					QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADDynamicGroupSchema.ManagedBy, this.managerId);
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

		// Token: 0x060001A3 RID: 419 RVA: 0x000093A8 File Offset: 0x000075A8
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
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000942A File Offset: 0x0000762A
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return DynamicDistributionGroup.FromDataObject((ADDynamicGroup)dataObject);
		}

		// Token: 0x0400003C RID: 60
		private ADObjectId managerId;

		// Token: 0x0400003D RID: 61
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.Alias
		};

		// Token: 0x0400003E RID: 62
		private static readonly RecipientType[] RecipientTypesArray = new RecipientType[]
		{
			RecipientType.DynamicDistributionGroup
		};
	}
}
