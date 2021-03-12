using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B1F RID: 2847
	[Cmdlet("Get", "ThrottlingPolicyAssociation", DefaultParameterSetName = "Identity")]
	public sealed class GetThrottlingPolicyAssociation : GetRecipientBase<ThrottlingPolicyAssociationIdParameter, ADRecipient>
	{
		// Token: 0x17001EC1 RID: 7873
		// (get) Token: 0x0600650B RID: 25867 RVA: 0x001A57BF File Offset: 0x001A39BF
		// (set) Token: 0x0600650C RID: 25868 RVA: 0x001A57C6 File Offset: 0x001A39C6
		private new AccountPartitionIdParameter AccountPartition
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001EC2 RID: 7874
		// (get) Token: 0x0600650D RID: 25869 RVA: 0x001A57CD File Offset: 0x001A39CD
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001EC3 RID: 7875
		// (get) Token: 0x0600650E RID: 25870 RVA: 0x001A57D0 File Offset: 0x001A39D0
		// (set) Token: 0x0600650F RID: 25871 RVA: 0x001A57E7 File Offset: 0x001A39E7
		[Parameter]
		public ThrottlingPolicyIdParameter ThrottlingPolicy
		{
			get
			{
				return (ThrottlingPolicyIdParameter)base.Fields[ADRecipientSchema.ThrottlingPolicy];
			}
			set
			{
				base.Fields[ADRecipientSchema.ThrottlingPolicy] = value;
			}
		}

		// Token: 0x17001EC4 RID: 7876
		// (get) Token: 0x06006510 RID: 25872 RVA: 0x001A57FA File Offset: 0x001A39FA
		protected override QueryFilter InternalFilter
		{
			get
			{
				return base.OptionalIdentityData.AdditionalFilter;
			}
		}

		// Token: 0x17001EC5 RID: 7877
		// (get) Token: 0x06006511 RID: 25873 RVA: 0x001A5807 File Offset: 0x001A3A07
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<ThrottlingPolicyAssociationSchema>();
			}
		}

		// Token: 0x17001EC6 RID: 7878
		// (get) Token: 0x06006512 RID: 25874 RVA: 0x001A580E File Offset: 0x001A3A0E
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetThrottlingPolicyAssociation.SortPropertiesArray;
			}
		}

		// Token: 0x06006513 RID: 25875 RVA: 0x001A5818 File Offset: 0x001A3A18
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			ADRecipient dataObject2 = (ADRecipient)dataObject;
			return ThrottlingPolicyAssociation.FromDataObject(dataObject2);
		}

		// Token: 0x06006514 RID: 25876 RVA: 0x001A5834 File Offset: 0x001A3A34
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (base.Fields.IsModified(ADRecipientSchema.ThrottlingPolicy))
			{
				QueryFilter queryFilter;
				if (this.ThrottlingPolicy == null)
				{
					queryFilter = new NotFilter(new ExistsFilter(ADRecipientSchema.ThrottlingPolicy));
				}
				else
				{
					ThrottlingPolicy throttlingPolicy = (ThrottlingPolicy)base.GetDataObject<ThrottlingPolicy>(this.ThrottlingPolicy, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorThrottlingPolicyNotFound(this.ThrottlingPolicy.ToString())), new LocalizedString?(Strings.ErrorThrottlingPolicyNotUnique(this.ThrottlingPolicy.ToString())), ExchangeErrorCategory.Client);
					queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ThrottlingPolicy, (ADObjectId)throttlingPolicy.Identity);
				}
				base.OptionalIdentityData.AdditionalFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					base.OptionalIdentityData.AdditionalFilter,
					queryFilter
				});
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003641 RID: 13889
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ThrottlingPolicyAssociationSchema.ThrottlingPolicyId
		};
	}
}
