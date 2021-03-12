using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AA5 RID: 2725
	[Cmdlet("Get", "AcceptedDomain", DefaultParameterSetName = "Identity")]
	public sealed class GetAcceptedDomain : GetMultitenancySystemConfigurationObjectTask<AcceptedDomainIdParameter, AcceptedDomain>
	{
		// Token: 0x17001D2D RID: 7469
		// (get) Token: 0x06006068 RID: 24680 RVA: 0x00191C2C File Offset: 0x0018FE2C
		// (set) Token: 0x06006069 RID: 24681 RVA: 0x00191C34 File Offset: 0x0018FE34
		[Parameter(Mandatory = false)]
		public long UsnForReconciliationSearch
		{
			get
			{
				return this.usnForReconciliationSearch;
			}
			set
			{
				this.usnForReconciliationSearch = value;
			}
		}

		// Token: 0x17001D2E RID: 7470
		// (get) Token: 0x0600606A RID: 24682 RVA: 0x00191C3D File Offset: 0x0018FE3D
		// (set) Token: 0x0600606B RID: 24683 RVA: 0x00191C54 File Offset: 0x0018FE54
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string Filter
		{
			get
			{
				return (string)base.Fields["Filter"];
			}
			set
			{
				MonadFilter monadFilter = new MonadFilter(value, this, this.FilterableObjectSchema);
				this.inputFilter = monadFilter.InnerFilter;
				base.OptionalIdentityData.AdditionalFilter = monadFilter.InnerFilter;
				base.Fields["Filter"] = value;
			}
		}

		// Token: 0x17001D2F RID: 7471
		// (get) Token: 0x0600606C RID: 24684 RVA: 0x00191C9D File Offset: 0x0018FE9D
		internal ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<AcceptedDomainSchema>();
			}
		}

		// Token: 0x17001D30 RID: 7472
		// (get) Token: 0x0600606D RID: 24685 RVA: 0x00191CA4 File Offset: 0x0018FEA4
		// (set) Token: 0x0600606E RID: 24686 RVA: 0x00191CAC File Offset: 0x0018FEAC
		[Parameter(Mandatory = false)]
		public new AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return base.AccountPartition;
			}
			set
			{
				base.AccountPartition = value;
			}
		}

		// Token: 0x17001D31 RID: 7473
		// (get) Token: 0x0600606F RID: 24687 RVA: 0x00191CB8 File Offset: 0x0018FEB8
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = base.InternalFilter;
				if (this.inputFilter != null)
				{
					if (queryFilter != null)
					{
						queryFilter = new AndFilter(new QueryFilter[]
						{
							queryFilter,
							this.inputFilter
						});
					}
					else
					{
						queryFilter = this.inputFilter;
					}
				}
				if (this.UsnForReconciliationSearch >= 0L)
				{
					ComparisonFilter comparisonFilter = new ComparisonFilter(ComparisonOperator.GreaterThan, AcceptedDomainSchema.UsnCreated, this.UsnForReconciliationSearch);
					if (queryFilter != null)
					{
						queryFilter = new AndFilter(new QueryFilter[]
						{
							queryFilter,
							comparisonFilter
						});
					}
					else
					{
						queryFilter = comparisonFilter;
					}
				}
				return queryFilter;
			}
		}

		// Token: 0x06006070 RID: 24688 RVA: 0x00191D3C File Offset: 0x0018FF3C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.UsnForReconciliationSearch >= 0L)
			{
				if (base.DomainController == null)
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.ErrorDomainControllerNotSpecifiedWithUsnForReconciliationSearch), ErrorCategory.InvalidArgument, null);
				}
				base.InternalResultSize = Unlimited<uint>.UnlimitedValue;
				base.OptionalIdentityData.AdditionalFilter = new ComparisonFilter(ComparisonOperator.GreaterThan, AcceptedDomainSchema.UsnCreated, this.UsnForReconciliationSearch);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x17001D32 RID: 7474
		// (get) Token: 0x06006071 RID: 24689 RVA: 0x00191DAE File Offset: 0x0018FFAE
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04003545 RID: 13637
		private QueryFilter inputFilter;

		// Token: 0x04003546 RID: 13638
		private long usnForReconciliationSearch = -1L;
	}
}
