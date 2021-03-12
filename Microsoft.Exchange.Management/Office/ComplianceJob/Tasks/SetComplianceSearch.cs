using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Office.ComplianceJob.Tasks
{
	// Token: 0x0200075A RID: 1882
	[Cmdlet("Set", "ComplianceSearch", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetComplianceSearch : SetComplianceJob<ComplianceSearch>
	{
		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x06004310 RID: 17168 RVA: 0x0011390C File Offset: 0x00111B0C
		// (set) Token: 0x06004311 RID: 17169 RVA: 0x00113923 File Offset: 0x00111B23
		[Parameter(Mandatory = false)]
		public string KeywordQuery
		{
			get
			{
				return (string)base.Fields["KeywordQuery"];
			}
			set
			{
				base.Fields["KeywordQuery"] = value;
			}
		}

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x06004312 RID: 17170 RVA: 0x00113936 File Offset: 0x00111B36
		// (set) Token: 0x06004313 RID: 17171 RVA: 0x0011394D File Offset: 0x00111B4D
		[Parameter(Mandatory = false)]
		public DateTime? StartDate
		{
			get
			{
				return (DateTime?)base.Fields["StartDate"];
			}
			set
			{
				base.Fields["StartDate"] = value;
			}
		}

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x06004314 RID: 17172 RVA: 0x00113965 File Offset: 0x00111B65
		// (set) Token: 0x06004315 RID: 17173 RVA: 0x0011397C File Offset: 0x00111B7C
		[Parameter(Mandatory = false)]
		public DateTime? EndDate
		{
			get
			{
				return (DateTime?)base.Fields["EndDate"];
			}
			set
			{
				base.Fields["EndDate"] = value;
			}
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x00113994 File Offset: 0x00111B94
		// (set) Token: 0x06004317 RID: 17175 RVA: 0x001139AB File Offset: 0x00111BAB
		[Parameter(Mandatory = false)]
		public CultureInfo Language
		{
			get
			{
				return (CultureInfo)base.Fields["Language"];
			}
			set
			{
				base.Fields["Language"] = value;
			}
		}

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x06004318 RID: 17176 RVA: 0x001139BE File Offset: 0x00111BBE
		// (set) Token: 0x06004319 RID: 17177 RVA: 0x001139D5 File Offset: 0x00111BD5
		[Parameter(Mandatory = false)]
		public bool IncludeUnindexedItems
		{
			get
			{
				return (bool)base.Fields["IncludeUnindexedItems"];
			}
			set
			{
				base.Fields["IncludeUnindexedItems"] = value;
			}
		}

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x0600431A RID: 17178 RVA: 0x001139ED File Offset: 0x00111BED
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.SetComplianceSearchConfirmation(this.Identity.ToString());
			}
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x00113A00 File Offset: 0x00111C00
		protected override IConfigurable PrepareDataObject()
		{
			ComplianceSearch complianceSearch = (ComplianceSearch)base.PrepareDataObject();
			if (base.Fields.IsModified("StartDate"))
			{
				complianceSearch.StartDate = new DateTime?(this.StartDate.Value);
			}
			if (base.Fields.IsModified("EndDate"))
			{
				complianceSearch.EndDate = new DateTime?(this.EndDate.Value);
			}
			if (base.Fields.IsModified("KeywordQuery"))
			{
				complianceSearch.KeywordQuery = this.KeywordQuery;
			}
			if (base.Fields.IsModified("Language"))
			{
				complianceSearch.Language = this.Language;
			}
			if (base.Fields.IsModified("IncludeUnindexedItems"))
			{
				complianceSearch.IncludeUnindexedItems = this.IncludeUnindexedItems;
			}
			return complianceSearch;
		}

		// Token: 0x040029E4 RID: 10724
		private const string ParameterKeywordQuery = "KeywordQuery";

		// Token: 0x040029E5 RID: 10725
		private const string ParameterLanguage = "Language";

		// Token: 0x040029E6 RID: 10726
		private const string ParameterStatusMailRecipients = "StatusMailRecipients";

		// Token: 0x040029E7 RID: 10727
		private const string ParameterLogLevel = "LogLevel";

		// Token: 0x040029E8 RID: 10728
		private const string ParameterIncludeUnindexedItems = "IncludeUnindexedItems";

		// Token: 0x040029E9 RID: 10729
		private const string ParameterStartDate = "StartDate";

		// Token: 0x040029EA RID: 10730
		private const string ParameterEndDate = "EndDate";
	}
}
