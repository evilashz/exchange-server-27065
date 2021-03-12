using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000674 RID: 1652
	[Cmdlet("Get", "ManagementRole", DefaultParameterSetName = "Identity")]
	public sealed class GetManagementRole : GetMultitenancySystemConfigurationObjectTask<RoleIdParameter, ExchangeRole>
	{
		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x000F6CA8 File Offset: 0x000F4EA8
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x06003A6E RID: 14958 RVA: 0x000F6CAB File Offset: 0x000F4EAB
		// (set) Token: 0x06003A6F RID: 14959 RVA: 0x000F6CB3 File Offset: 0x000F4EB3
		[Parameter(Mandatory = false, ParameterSetName = "Script", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[Parameter(Mandatory = false, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[Parameter(Mandatory = true, ParameterSetName = "GetChildren", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[Parameter(Mandatory = true, ParameterSetName = "Recurse", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override RoleIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x06003A70 RID: 14960 RVA: 0x000F6CBC File Offset: 0x000F4EBC
		// (set) Token: 0x06003A71 RID: 14961 RVA: 0x000F6CD3 File Offset: 0x000F4ED3
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public string Cmdlet
		{
			get
			{
				return (string)base.Fields["Cmdlet"];
			}
			set
			{
				CmdletRoleEntry.ValidateName(value);
				base.Fields["Cmdlet"] = value;
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06003A72 RID: 14962 RVA: 0x000F6CEC File Offset: 0x000F4EEC
		// (set) Token: 0x06003A73 RID: 14963 RVA: 0x000F6D03 File Offset: 0x000F4F03
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public string[] CmdletParameters
		{
			get
			{
				return (string[])base.Fields["CmdletParameters"];
			}
			set
			{
				RoleEntry.FormatParameters(value);
				base.Fields["CmdletParameters"] = value;
			}
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06003A74 RID: 14964 RVA: 0x000F6D1C File Offset: 0x000F4F1C
		// (set) Token: 0x06003A75 RID: 14965 RVA: 0x000F6D33 File Offset: 0x000F4F33
		[Parameter(Mandatory = false, ParameterSetName = "Script")]
		public string Script
		{
			get
			{
				return (string)base.Fields["Script"];
			}
			set
			{
				RoleEntry.ValidateName(value);
				base.Fields["Script"] = value;
			}
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x000F6D4C File Offset: 0x000F4F4C
		// (set) Token: 0x06003A77 RID: 14967 RVA: 0x000F6D63 File Offset: 0x000F4F63
		[Parameter(Mandatory = false, ParameterSetName = "Script")]
		public string[] ScriptParameters
		{
			get
			{
				return (string[])base.Fields["ScriptParameters"];
			}
			set
			{
				RoleEntry.FormatParameters(value);
				base.Fields["ScriptParameters"] = value;
			}
		}

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06003A78 RID: 14968 RVA: 0x000F6D7C File Offset: 0x000F4F7C
		// (set) Token: 0x06003A79 RID: 14969 RVA: 0x000F6DA2 File Offset: 0x000F4FA2
		[Parameter(Mandatory = true, ParameterSetName = "Recurse")]
		public SwitchParameter Recurse
		{
			get
			{
				return (SwitchParameter)(base.Fields["Recurse"] ?? false);
			}
			set
			{
				base.Fields["Recurse"] = value;
			}
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06003A7A RID: 14970 RVA: 0x000F6DBA File Offset: 0x000F4FBA
		// (set) Token: 0x06003A7B RID: 14971 RVA: 0x000F6DE0 File Offset: 0x000F4FE0
		[Parameter(Mandatory = true, ParameterSetName = "GetChildren")]
		public SwitchParameter GetChildren
		{
			get
			{
				return (SwitchParameter)(base.Fields["GetChildren"] ?? false);
			}
			set
			{
				base.Fields["GetChildren"] = value;
			}
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06003A7C RID: 14972 RVA: 0x000F6DF8 File Offset: 0x000F4FF8
		// (set) Token: 0x06003A7D RID: 14973 RVA: 0x000F6E0F File Offset: 0x000F500F
		[Parameter(Mandatory = false, ParameterSetName = "GetChildren")]
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[Parameter(Mandatory = false, ParameterSetName = "Recurse")]
		public RoleType RoleType
		{
			get
			{
				return (RoleType)base.Fields["RoleType"];
			}
			set
			{
				base.VerifyValues<RoleType>(GetManagementRole.AllowedRoleTypes, value);
				base.Fields["RoleType"] = value;
			}
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x000F6E33 File Offset: 0x000F5033
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06003A7F RID: 14975 RVA: 0x000F6E36 File Offset: 0x000F5036
		protected override QueryFilter InternalFilter
		{
			get
			{
				return this.internalFilter;
			}
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x000F6E40 File Offset: 0x000F5040
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			List<QueryFilter> list = new List<QueryFilter>();
			if (base.Fields.IsModified("RoleType"))
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleSchema.RoleType, this.RoleType));
			}
			if (base.Fields.IsModified("Cmdlet"))
			{
				list.Add(RBACHelper.ConstructRoleEntryFilter(this.Cmdlet, ManagementRoleEntryType.Cmdlet));
			}
			if (base.Fields.IsModified("Script"))
			{
				list.Add(RBACHelper.ConstructRoleEntryFilter(this.Script, ManagementRoleEntryType.Script));
			}
			if (this.CmdletParameters != null)
			{
				list.Add(RBACHelper.ConstructRoleEntryParameterFilter(this.CmdletParameters));
			}
			if (this.ScriptParameters != null)
			{
				list.Add(RBACHelper.ConstructRoleEntryParameterFilter(this.ScriptParameters));
			}
			if (this.ScriptParameters != null || base.Fields.IsModified("Script"))
			{
				list.Add(RBACHelper.ScriptEnabledRoleEntryTypeFilter);
			}
			if (1 < list.Count)
			{
				this.internalFilter = new AndFilter(list.ToArray());
			}
			else if (1 == list.Count)
			{
				this.internalFilter = list[0];
			}
			else
			{
				this.internalFilter = null;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x000F6F6C File Offset: 0x000F516C
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			ExchangeRole exchangeRole = (ExchangeRole)dataObject;
			bool flag = base.Fields.IsModified("RoleType") && this.RoleType != exchangeRole.RoleType;
			if (this.Cmdlet != null || this.CmdletParameters != null)
			{
				flag |= !RoleHelper.DoesRoleMatchingNameAndParameters(exchangeRole, 'c', this.Cmdlet, this.CmdletParameters);
			}
			else if (this.Script != null || this.ScriptParameters != null)
			{
				flag |= !RoleHelper.DoesRoleMatchingNameAndParameters(exchangeRole, 's', this.Script, this.ScriptParameters);
			}
			if (flag)
			{
				base.WriteVerbose(Strings.VerboseSkipObject(exchangeRole.DistinguishedName));
			}
			else
			{
				base.WriteResult(dataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x000F703C File Offset: 0x000F523C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			if (this.Recurse.IsPresent || this.GetChildren.IsPresent)
			{
				ExchangeRole exchangeRole = (ExchangeRole)base.GetDataObject<ExchangeRole>(this.Identity, this.ConfigurationSession, this.RootId, new LocalizedString?(Strings.ErrorRoleNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorRoleNotUnique(this.Identity.ToString())));
				this.WriteResult<ExchangeRole>(this.ConfigurationSession.FindPaged<ExchangeRole>(exchangeRole.Id, this.Recurse ? QueryScope.SubTree : QueryScope.OneLevel, this.InternalFilter, null, 0));
			}
			else
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04002655 RID: 9813
		private const string ParameterCmdlet = "Cmdlet";

		// Token: 0x04002656 RID: 9814
		private const string ParameterCmdletParameters = "CmdletParameters";

		// Token: 0x04002657 RID: 9815
		private const string ParameterScript = "Script";

		// Token: 0x04002658 RID: 9816
		private const string ParameterScriptParameters = "ScriptParameters";

		// Token: 0x04002659 RID: 9817
		private const string ParameterRoleType = "RoleType";

		// Token: 0x0400265A RID: 9818
		private const string ParameterRecurse = "Recurse";

		// Token: 0x0400265B RID: 9819
		private const string ParameterGetChildren = "GetChildren";

		// Token: 0x0400265C RID: 9820
		private const string ParameterSetScript = "Script";

		// Token: 0x0400265D RID: 9821
		private const string ParameterSetRecurse = "Recurse";

		// Token: 0x0400265E RID: 9822
		private const string ParameterSetGetChildren = "GetChildren";

		// Token: 0x0400265F RID: 9823
		private QueryFilter internalFilter;

		// Token: 0x04002660 RID: 9824
		private static readonly RoleType[] AllowedRoleTypes = (RoleType[])Enum.GetValues(typeof(RoleType));
	}
}
