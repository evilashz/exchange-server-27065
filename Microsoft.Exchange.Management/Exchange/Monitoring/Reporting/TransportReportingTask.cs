using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring.Reporting
{
	// Token: 0x0200058E RID: 1422
	public abstract class TransportReportingTask : ReportingTask<OrganizationIdParameter>
	{
		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x000CB938 File Offset: 0x000C9B38
		// (set) Token: 0x0600321D RID: 12829 RVA: 0x000CB94F File Offset: 0x000C9B4F
		[Parameter(Mandatory = false)]
		public AdSiteIdParameter AdSite
		{
			get
			{
				return (AdSiteIdParameter)base.Fields["AdSite"];
			}
			set
			{
				base.Fields["AdSite"] = value;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x000CB962 File Offset: 0x000C9B62
		// (set) Token: 0x0600321F RID: 12831 RVA: 0x000CB988 File Offset: 0x000C9B88
		[Parameter(Mandatory = false)]
		public SwitchParameter DailyStatistics
		{
			get
			{
				return (SwitchParameter)(base.Fields["DailyStatistics"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DailyStatistics"] = value;
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x000CB9A0 File Offset: 0x000C9BA0
		// (set) Token: 0x06003221 RID: 12833 RVA: 0x000CB9B7 File Offset: 0x000C9BB7
		[Parameter(Mandatory = true, ParameterSetName = "StartEndDateTime")]
		public ExDateTime EndDate
		{
			get
			{
				return (ExDateTime)base.Fields["EndDate"];
			}
			set
			{
				base.Fields["EndDate"] = value;
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x000CB9CF File Offset: 0x000C9BCF
		// (set) Token: 0x06003223 RID: 12835 RVA: 0x000CB9E6 File Offset: 0x000C9BE6
		[Parameter(Mandatory = true, ParameterSetName = "StartEndDateTime")]
		public ExDateTime StartDate
		{
			get
			{
				return (ExDateTime)base.Fields["StartDate"];
			}
			set
			{
				base.Fields["StartDate"] = value;
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x000CB9FE File Offset: 0x000C9BFE
		// (set) Token: 0x06003225 RID: 12837 RVA: 0x000CBA15 File Offset: 0x000C9C15
		[Parameter(Mandatory = false, ParameterSetName = "ReportingPeriod")]
		public ReportingPeriod ReportingPeriod
		{
			get
			{
				return (ReportingPeriod)base.Fields["ReportingPeriod"];
			}
			set
			{
				base.Fields["ReportingPeriod"] = value;
			}
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x000CBA2D File Offset: 0x000C9C2D
		// (set) Token: 0x06003227 RID: 12839 RVA: 0x000CBA44 File Offset: 0x000C9C44
		[Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override OrganizationIdParameter Identity
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000CBA57 File Offset: 0x000C9C57
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000CBA60 File Offset: 0x000C9C60
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (base.ParameterSetName == "StartEndDateTime" && this.StartDate >= this.EndDate)
			{
				base.WriteError(new ArgumentException(Strings.InvalidTimeRange, "StartDate"), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000CBAB8 File Offset: 0x000C9CB8
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			if (dataObject != null)
			{
				ADOrganizationalUnit adorganizationalUnit = dataObject as ADOrganizationalUnit;
				if (adorganizationalUnit != null)
				{
					if (!adorganizationalUnit.OrganizationId.Equals(OrganizationId.ForestWideOrgId))
					{
						this.WriteStatistics(adorganizationalUnit.OrganizationId.OrganizationalUnit);
					}
				}
				else
				{
					base.WriteResult(dataObject);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000CBB08 File Offset: 0x000C9D08
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			TaskLogger.LogEnter();
			if (dataObjects != null)
			{
				if (this.Identity != null)
				{
					base.WriteResult<T>(dataObjects);
				}
				else if (base.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId))
				{
					this.WriteStatistics(null);
				}
				else
				{
					this.WriteStatistics(base.CurrentOrganizationId.OrganizationalUnit);
					base.WriteResult<T>(dataObjects);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600322C RID: 12844
		protected abstract void WriteStatistics(ADObjectId tenantId);

		// Token: 0x04002347 RID: 9031
		protected const int SqlNetworkError = 53;

		// Token: 0x0200058F RID: 1423
		internal abstract class ParameterSet
		{
			// Token: 0x04002348 RID: 9032
			internal const string ReportingPeriod = "ReportingPeriod";

			// Token: 0x04002349 RID: 9033
			internal const string StartEndDateTime = "StartEndDateTime";
		}
	}
}
