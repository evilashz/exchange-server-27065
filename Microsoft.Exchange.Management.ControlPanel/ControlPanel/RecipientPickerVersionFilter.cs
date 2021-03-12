using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000352 RID: 850
	[DataContract]
	public abstract class RecipientPickerVersionFilter : RecipientPickerFilterBase
	{
		// Token: 0x17001F03 RID: 7939
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x000912B1 File Offset: 0x0008F4B1
		protected virtual ExchangeObjectVersion MinVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001F04 RID: 7940
		// (get) Token: 0x06002F97 RID: 12183 RVA: 0x000912B8 File Offset: 0x0008F4B8
		protected virtual RecipientTypeDetails[] RecipientTypeDetailsWithoutVersionRestriction
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000912BC File Offset: 0x0008F4BC
		protected override void UpdateFilterProperty()
		{
			base.UpdateFilterProperty();
			if (this.MinVersion != null)
			{
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADObjectSchema.ExchangeVersion, this.MinVersion);
				if (!this.RecipientTypeDetailsWithoutVersionRestriction.IsNullOrEmpty())
				{
					QueryFilter recipientTypeDetailsFilter = RecipientIdParameter.GetRecipientTypeDetailsFilter(base.RecipientTypeDetailsList);
					if (recipientTypeDetailsFilter != null)
					{
						queryFilter = new AndFilter(new QueryFilter[]
						{
							recipientTypeDetailsFilter,
							queryFilter
						});
					}
					recipientTypeDetailsFilter = RecipientIdParameter.GetRecipientTypeDetailsFilter(this.RecipientTypeDetailsWithoutVersionRestriction);
					queryFilter = new OrFilter(new QueryFilter[]
					{
						recipientTypeDetailsFilter,
						queryFilter
					});
					base.RecipientTypeDetailsList = null;
				}
				string text = (string)base["Filter"];
				if (!text.IsNullOrBlank())
				{
					base["Filter"] = string.Empty;
					MonadFilter monadFilter = new MonadFilter(text, null, ObjectSchema.GetInstance<ReducedRecipientSchema>());
					queryFilter = new AndFilter(new QueryFilter[]
					{
						monadFilter.InnerFilter,
						queryFilter
					});
				}
				base["RecipientPreviewFilter"] = LdapFilterBuilder.LdapFilterFromQueryFilter(queryFilter);
			}
		}

		// Token: 0x04002311 RID: 8977
		public new const string RbacParameters = "?ResultSize&Filter&RecipientTypeDetails&Properties&RecipientPreviewFilter";
	}
}
