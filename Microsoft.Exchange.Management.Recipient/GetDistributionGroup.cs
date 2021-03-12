using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200001F RID: 31
	[Cmdlet("Get", "DistributionGroup", DefaultParameterSetName = "Identity")]
	[OutputType(new Type[]
	{
		typeof(DistributionGroup)
	})]
	public sealed class GetDistributionGroup : GetDistributionGroupBase
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00009145 File Offset: 0x00007345
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000914D File Offset: 0x0000734D
		[Parameter(Mandatory = false)]
		public new long UsnForReconciliationSearch
		{
			get
			{
				return base.UsnForReconciliationSearch;
			}
			set
			{
				base.UsnForReconciliationSearch = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00009158 File Offset: 0x00007358
		protected override QueryFilter InternalFilter
		{
			get
			{
				ComparisonFilter comparisonFilter = new ComparisonFilter(ComparisonOperator.NotEqual, ADRecipientSchema.RecipientTypeDetails, Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.GroupMailbox);
				ComparisonFilter comparisonFilter2 = new ComparisonFilter(ComparisonOperator.NotEqual, ADRecipientSchema.RecipientTypeDetails, Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.RemoteGroupMailbox);
				return new AndFilter(new QueryFilter[]
				{
					base.InternalFilter,
					comparisonFilter,
					comparisonFilter2
				});
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000091B8 File Offset: 0x000073B8
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			ADGroup adgroup = dataObject as ADGroup;
			if (adgroup != null && (adgroup.RecipientTypeDetails == Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.GroupMailbox || adgroup.RecipientTypeDetails == Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.RemoteGroupMailbox))
			{
				base.WriteError(new RecipientTaskException(Strings.NotAValidDistributionGroup), ExchangeErrorCategory.Client, this.Identity.ToString());
			}
			return base.ConvertDataObjectToPresentationObject(dataObject);
		}
	}
}
