using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;

namespace Microsoft.Exchange.Management.PolicyNudges
{
	// Token: 0x02000062 RID: 98
	[Cmdlet("Get", "PolicyTipConfig", DefaultParameterSetName = "Identity")]
	public sealed class GetPolicyTipConfig : GetMultitenancySystemConfigurationObjectTask<PolicyTipConfigIdParameter, PolicyTipMessageConfig>
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000D743 File Offset: 0x0000B943
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000D769 File Offset: 0x0000B969
		[Parameter(Mandatory = false, ParameterSetName = "Paramters")]
		public SwitchParameter Original
		{
			get
			{
				return (SwitchParameter)(base.Fields["Original"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Original"] = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000D781 File Offset: 0x0000B981
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000D798 File Offset: 0x0000B998
		[Parameter(Mandatory = false, ParameterSetName = "Paramters")]
		public CultureInfo Locale
		{
			get
			{
				return (CultureInfo)base.Fields["Locale"];
			}
			set
			{
				base.Fields["Locale"] = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000D7AB File Offset: 0x0000B9AB
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000D7C2 File Offset: 0x0000B9C2
		[Parameter(Mandatory = false, ParameterSetName = "Paramters")]
		public PolicyTipMessageConfigAction Action
		{
			get
			{
				return (PolicyTipMessageConfigAction)base.Fields["Action"];
			}
			set
			{
				base.Fields["Action"] = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		protected override ObjectId RootId
		{
			get
			{
				if (this.Identity != null)
				{
					return null;
				}
				ADObjectId orgContainerId = ((IConfigurationSession)base.DataSession).GetOrgContainerId();
				return orgContainerId.GetDescendantId(PolicyTipMessageConfig.PolicyTipMessageConfigContainer);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000D810 File Offset: 0x0000BA10
		protected override QueryFilter InternalFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.InternalFilter,
					base.Fields.IsModified("Action") ? new ComparisonFilter(ComparisonOperator.Equal, PolicyTipMessageConfigSchema.Action, this.Action) : null,
					(this.Locale != null) ? new ComparisonFilter(ComparisonOperator.Equal, PolicyTipMessageConfigSchema.Locale, this.Locale.Name) : null
				});
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000D894 File Offset: 0x0000BA94
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.Fields.IsModified("Action") && this.Action == PolicyTipMessageConfigAction.Url)
			{
				base.WriteError(new GetPolicyTipConfigUrlUsedAsActionFilterException(), ErrorCategory.InvalidArgument, null);
			}
			HashSet<int> expectedCultureLcids = LanguagePackInfo.expectedCultureLcids;
			if (this.Locale != null && !expectedCultureLcids.Contains(this.Locale.LCID))
			{
				string locales = string.Join(", ", from lcid in expectedCultureLcids
				select new CultureInfo(lcid).Name);
				base.WriteError(new NewPolicyTipConfigInvalidLocaleException(locales), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000D960 File Offset: 0x0000BB60
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.Original)
			{
				IEnumerable<PolicyTipMessageConfig> enumerable = PerTenantPolicyNudgeRulesCollection.PolicyTipMessages.builtInConfigs.Value;
				if (this.Locale != null)
				{
					enumerable = from c in enumerable
					where c.Locale == this.Locale.Name
					select c;
				}
				if (base.Fields.IsModified("Action"))
				{
					enumerable = from c in enumerable
					where c.Action == this.Action
					select c;
				}
				this.WriteResult<PolicyTipMessageConfig>(enumerable);
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x04000144 RID: 324
		private const string ParametersSetName = "Paramters";
	}
}
