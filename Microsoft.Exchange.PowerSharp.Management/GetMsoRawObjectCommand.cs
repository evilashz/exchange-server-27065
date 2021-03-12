using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000121 RID: 289
	public class GetMsoRawObjectCommand : SyntheticCommandWithPipelineInputNoOutput<RecipientOrOrganizationIdParameter>
	{
		// Token: 0x06001F9F RID: 8095 RVA: 0x00040B46 File Offset: 0x0003ED46
		private GetMsoRawObjectCommand() : base("Get-MsoRawObject")
		{
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x00040B53 File Offset: 0x0003ED53
		public GetMsoRawObjectCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x00040B62 File Offset: 0x0003ED62
		public virtual GetMsoRawObjectCommand SetParameters(GetMsoRawObjectCommand.ExchangeIdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00040B6C File Offset: 0x0003ED6C
		public virtual GetMsoRawObjectCommand SetParameters(GetMsoRawObjectCommand.SyncObjectIdParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x00040B76 File Offset: 0x0003ED76
		public virtual GetMsoRawObjectCommand SetParameters(GetMsoRawObjectCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000122 RID: 290
		public class ExchangeIdentityParameters : ParametersBase
		{
			// Token: 0x17000916 RID: 2326
			// (set) Token: 0x06001FA4 RID: 8100 RVA: 0x00040B80 File Offset: 0x0003ED80
			public virtual RecipientOrOrganizationIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17000917 RID: 2327
			// (set) Token: 0x06001FA5 RID: 8101 RVA: 0x00040B93 File Offset: 0x0003ED93
			public virtual SwitchParameter IncludeBackLinks
			{
				set
				{
					base.PowerSharpParameters["IncludeBackLinks"] = value;
				}
			}

			// Token: 0x17000918 RID: 2328
			// (set) Token: 0x06001FA6 RID: 8102 RVA: 0x00040BAB File Offset: 0x0003EDAB
			public virtual SwitchParameter IncludeForwardLinks
			{
				set
				{
					base.PowerSharpParameters["IncludeForwardLinks"] = value;
				}
			}

			// Token: 0x17000919 RID: 2329
			// (set) Token: 0x06001FA7 RID: 8103 RVA: 0x00040BC3 File Offset: 0x0003EDC3
			public virtual int LinksResultSize
			{
				set
				{
					base.PowerSharpParameters["LinksResultSize"] = value;
				}
			}

			// Token: 0x1700091A RID: 2330
			// (set) Token: 0x06001FA8 RID: 8104 RVA: 0x00040BDB File Offset: 0x0003EDDB
			public virtual SwitchParameter PopulateRawObject
			{
				set
				{
					base.PowerSharpParameters["PopulateRawObject"] = value;
				}
			}

			// Token: 0x1700091B RID: 2331
			// (set) Token: 0x06001FA9 RID: 8105 RVA: 0x00040BF3 File Offset: 0x0003EDF3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700091C RID: 2332
			// (set) Token: 0x06001FAA RID: 8106 RVA: 0x00040C0B File Offset: 0x0003EE0B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700091D RID: 2333
			// (set) Token: 0x06001FAB RID: 8107 RVA: 0x00040C23 File Offset: 0x0003EE23
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700091E RID: 2334
			// (set) Token: 0x06001FAC RID: 8108 RVA: 0x00040C3B File Offset: 0x0003EE3B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000123 RID: 291
		public class SyncObjectIdParameters : ParametersBase
		{
			// Token: 0x1700091F RID: 2335
			// (set) Token: 0x06001FAE RID: 8110 RVA: 0x00040C5B File Offset: 0x0003EE5B
			public virtual SyncObjectId ExternalObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalObjectId"] = value;
				}
			}

			// Token: 0x17000920 RID: 2336
			// (set) Token: 0x06001FAF RID: 8111 RVA: 0x00040C6E File Offset: 0x0003EE6E
			public virtual string ServiceInstanceId
			{
				set
				{
					base.PowerSharpParameters["ServiceInstanceId"] = value;
				}
			}

			// Token: 0x17000921 RID: 2337
			// (set) Token: 0x06001FB0 RID: 8112 RVA: 0x00040C81 File Offset: 0x0003EE81
			public virtual SwitchParameter IncludeBackLinks
			{
				set
				{
					base.PowerSharpParameters["IncludeBackLinks"] = value;
				}
			}

			// Token: 0x17000922 RID: 2338
			// (set) Token: 0x06001FB1 RID: 8113 RVA: 0x00040C99 File Offset: 0x0003EE99
			public virtual SwitchParameter IncludeForwardLinks
			{
				set
				{
					base.PowerSharpParameters["IncludeForwardLinks"] = value;
				}
			}

			// Token: 0x17000923 RID: 2339
			// (set) Token: 0x06001FB2 RID: 8114 RVA: 0x00040CB1 File Offset: 0x0003EEB1
			public virtual int LinksResultSize
			{
				set
				{
					base.PowerSharpParameters["LinksResultSize"] = value;
				}
			}

			// Token: 0x17000924 RID: 2340
			// (set) Token: 0x06001FB3 RID: 8115 RVA: 0x00040CC9 File Offset: 0x0003EEC9
			public virtual SwitchParameter PopulateRawObject
			{
				set
				{
					base.PowerSharpParameters["PopulateRawObject"] = value;
				}
			}

			// Token: 0x17000925 RID: 2341
			// (set) Token: 0x06001FB4 RID: 8116 RVA: 0x00040CE1 File Offset: 0x0003EEE1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000926 RID: 2342
			// (set) Token: 0x06001FB5 RID: 8117 RVA: 0x00040CF9 File Offset: 0x0003EEF9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000927 RID: 2343
			// (set) Token: 0x06001FB6 RID: 8118 RVA: 0x00040D11 File Offset: 0x0003EF11
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000928 RID: 2344
			// (set) Token: 0x06001FB7 RID: 8119 RVA: 0x00040D29 File Offset: 0x0003EF29
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000124 RID: 292
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000929 RID: 2345
			// (set) Token: 0x06001FB9 RID: 8121 RVA: 0x00040D49 File Offset: 0x0003EF49
			public virtual SwitchParameter IncludeBackLinks
			{
				set
				{
					base.PowerSharpParameters["IncludeBackLinks"] = value;
				}
			}

			// Token: 0x1700092A RID: 2346
			// (set) Token: 0x06001FBA RID: 8122 RVA: 0x00040D61 File Offset: 0x0003EF61
			public virtual SwitchParameter IncludeForwardLinks
			{
				set
				{
					base.PowerSharpParameters["IncludeForwardLinks"] = value;
				}
			}

			// Token: 0x1700092B RID: 2347
			// (set) Token: 0x06001FBB RID: 8123 RVA: 0x00040D79 File Offset: 0x0003EF79
			public virtual int LinksResultSize
			{
				set
				{
					base.PowerSharpParameters["LinksResultSize"] = value;
				}
			}

			// Token: 0x1700092C RID: 2348
			// (set) Token: 0x06001FBC RID: 8124 RVA: 0x00040D91 File Offset: 0x0003EF91
			public virtual SwitchParameter PopulateRawObject
			{
				set
				{
					base.PowerSharpParameters["PopulateRawObject"] = value;
				}
			}

			// Token: 0x1700092D RID: 2349
			// (set) Token: 0x06001FBD RID: 8125 RVA: 0x00040DA9 File Offset: 0x0003EFA9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700092E RID: 2350
			// (set) Token: 0x06001FBE RID: 8126 RVA: 0x00040DC1 File Offset: 0x0003EFC1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700092F RID: 2351
			// (set) Token: 0x06001FBF RID: 8127 RVA: 0x00040DD9 File Offset: 0x0003EFD9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000930 RID: 2352
			// (set) Token: 0x06001FC0 RID: 8128 RVA: 0x00040DF1 File Offset: 0x0003EFF1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
