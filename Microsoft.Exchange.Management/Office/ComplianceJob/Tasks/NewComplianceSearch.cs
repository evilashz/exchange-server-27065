using System;
using System.Globalization;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Office.ComplianceJob.Tasks
{
	// Token: 0x02000757 RID: 1879
	[Cmdlet("New", "ComplianceSearch", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class NewComplianceSearch : NewComplianceJob<ComplianceSearch>
	{
		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x060042FD RID: 17149 RVA: 0x001137B4 File Offset: 0x001119B4
		// (set) Token: 0x060042FE RID: 17150 RVA: 0x001137C1 File Offset: 0x001119C1
		[Parameter(Mandatory = false)]
		public CultureInfo Language
		{
			get
			{
				return this.objectToSave.Language;
			}
			set
			{
				this.objectToSave.Language = value;
			}
		}

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x060042FF RID: 17151 RVA: 0x001137CF File Offset: 0x001119CF
		// (set) Token: 0x06004300 RID: 17152 RVA: 0x001137E1 File Offset: 0x001119E1
		[Parameter(Mandatory = false)]
		public string[] StatusMailRecipients
		{
			get
			{
				return this.objectToSave.StatusMailRecipients.ToArray();
			}
			set
			{
				this.objectToSave.StatusMailRecipients = value;
			}
		}

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x06004301 RID: 17153 RVA: 0x001137F4 File Offset: 0x001119F4
		// (set) Token: 0x06004302 RID: 17154 RVA: 0x00113801 File Offset: 0x00111A01
		[Parameter(Mandatory = false)]
		public ComplianceJobLogLevel LogLevel
		{
			get
			{
				return this.objectToSave.LogLevel;
			}
			set
			{
				this.objectToSave.LogLevel = value;
			}
		}

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x0011380F File Offset: 0x00111A0F
		// (set) Token: 0x06004304 RID: 17156 RVA: 0x0011381C File Offset: 0x00111A1C
		[Parameter(Mandatory = false)]
		public bool IncludeUnindexedItems
		{
			get
			{
				return this.objectToSave.IncludeUnindexedItems;
			}
			set
			{
				this.objectToSave.IncludeUnindexedItems = value;
			}
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x06004305 RID: 17157 RVA: 0x0011382A File Offset: 0x00111A2A
		// (set) Token: 0x06004306 RID: 17158 RVA: 0x00113837 File Offset: 0x00111A37
		[Parameter(Mandatory = false)]
		public string KeywordQuery
		{
			get
			{
				return this.objectToSave.KeywordQuery;
			}
			set
			{
				this.objectToSave.KeywordQuery = value;
			}
		}

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x06004307 RID: 17159 RVA: 0x00113845 File Offset: 0x00111A45
		// (set) Token: 0x06004308 RID: 17160 RVA: 0x00113852 File Offset: 0x00111A52
		[Parameter(Mandatory = false)]
		public DateTime? StartDate
		{
			get
			{
				return this.objectToSave.StartDate;
			}
			set
			{
				this.objectToSave.StartDate = value;
			}
		}

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x06004309 RID: 17161 RVA: 0x00113860 File Offset: 0x00111A60
		// (set) Token: 0x0600430A RID: 17162 RVA: 0x0011386D File Offset: 0x00111A6D
		[Parameter(Mandatory = false)]
		public DateTime? EndDate
		{
			get
			{
				return this.objectToSave.EndDate;
			}
			set
			{
				this.objectToSave.EndDate = value;
			}
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x00113884 File Offset: 0x00111A84
		protected override IConfigDataProvider CreateSession()
		{
			if (base.ExchangeRunspaceConfig == null)
			{
				base.ThrowTerminatingError(new ComplianceJobTaskException(Strings.UnableToDetermineExecutingUser), ErrorCategory.InvalidOperation, null);
			}
			byte[] bytes = base.ExchangeRunspaceConfig.OrganizationId.GetBytes(Encoding.UTF8);
			OrganizationId organizationId;
			if (OrganizationId.TryCreateFromBytes(bytes, Encoding.UTF8, out organizationId))
			{
				TaskLogger.LogEnter();
			}
			return new ComplianceJobProvider(base.ExchangeRunspaceConfig.OrganizationId);
		}
	}
}
