using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ForwardSyncTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000116 RID: 278
	public class GetMsoDiagnosticsCommand : SyntheticCommandWithPipelineInput<DeltaSyncSummary, DeltaSyncSummary>
	{
		// Token: 0x06001F4E RID: 8014 RVA: 0x0004050B File Offset: 0x0003E70B
		private GetMsoDiagnosticsCommand() : base("Get-MsoDiagnostics")
		{
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00040518 File Offset: 0x0003E718
		public GetMsoDiagnosticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x00040527 File Offset: 0x0003E727
		public virtual GetMsoDiagnosticsCommand SetParameters(GetMsoDiagnosticsCommand.GetChangesParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x00040531 File Offset: 0x0003E731
		public virtual GetMsoDiagnosticsCommand SetParameters(GetMsoDiagnosticsCommand.GetContextParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x0004053B File Offset: 0x0003E73B
		public virtual GetMsoDiagnosticsCommand SetParameters(GetMsoDiagnosticsCommand.EstimateBacklogParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x00040545 File Offset: 0x0003E745
		public virtual GetMsoDiagnosticsCommand SetParameters(GetMsoDiagnosticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000117 RID: 279
		public class GetChangesParameters : ParametersBase
		{
			// Token: 0x170008DB RID: 2267
			// (set) Token: 0x06001F54 RID: 8020 RVA: 0x0004054F File Offset: 0x0003E74F
			public virtual byte Cookie
			{
				set
				{
					base.PowerSharpParameters["Cookie"] = value;
				}
			}

			// Token: 0x170008DC RID: 2268
			// (set) Token: 0x06001F55 RID: 8021 RVA: 0x00040567 File Offset: 0x0003E767
			public virtual SwitchParameter DeltaSync
			{
				set
				{
					base.PowerSharpParameters["DeltaSync"] = value;
				}
			}

			// Token: 0x170008DD RID: 2269
			// (set) Token: 0x06001F56 RID: 8022 RVA: 0x0004057F File Offset: 0x0003E77F
			public virtual int SampleCountForStatistics
			{
				set
				{
					base.PowerSharpParameters["SampleCountForStatistics"] = value;
				}
			}

			// Token: 0x170008DE RID: 2270
			// (set) Token: 0x06001F57 RID: 8023 RVA: 0x00040597 File Offset: 0x0003E797
			public virtual SwitchParameter UseLastCommittedCookie
			{
				set
				{
					base.PowerSharpParameters["UseLastCommittedCookie"] = value;
				}
			}

			// Token: 0x170008DF RID: 2271
			// (set) Token: 0x06001F58 RID: 8024 RVA: 0x000405AF File Offset: 0x0003E7AF
			public virtual int MaxNumberOfBatches
			{
				set
				{
					base.PowerSharpParameters["MaxNumberOfBatches"] = value;
				}
			}

			// Token: 0x170008E0 RID: 2272
			// (set) Token: 0x06001F59 RID: 8025 RVA: 0x000405C7 File Offset: 0x0003E7C7
			public virtual string ServiceInstanceId
			{
				set
				{
					base.PowerSharpParameters["ServiceInstanceId"] = value;
				}
			}

			// Token: 0x170008E1 RID: 2273
			// (set) Token: 0x06001F5A RID: 8026 RVA: 0x000405DA File Offset: 0x0003E7DA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170008E2 RID: 2274
			// (set) Token: 0x06001F5B RID: 8027 RVA: 0x000405F2 File Offset: 0x0003E7F2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170008E3 RID: 2275
			// (set) Token: 0x06001F5C RID: 8028 RVA: 0x0004060A File Offset: 0x0003E80A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170008E4 RID: 2276
			// (set) Token: 0x06001F5D RID: 8029 RVA: 0x00040622 File Offset: 0x0003E822
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000118 RID: 280
		public class GetContextParameters : ParametersBase
		{
			// Token: 0x170008E5 RID: 2277
			// (set) Token: 0x06001F5F RID: 8031 RVA: 0x00040642 File Offset: 0x0003E842
			public virtual byte Cookie
			{
				set
				{
					base.PowerSharpParameters["Cookie"] = value;
				}
			}

			// Token: 0x170008E6 RID: 2278
			// (set) Token: 0x06001F60 RID: 8032 RVA: 0x0004065A File Offset: 0x0003E85A
			public virtual string ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170008E7 RID: 2279
			// (set) Token: 0x06001F61 RID: 8033 RVA: 0x0004066D File Offset: 0x0003E86D
			public virtual byte PageToken
			{
				set
				{
					base.PowerSharpParameters["PageToken"] = value;
				}
			}

			// Token: 0x170008E8 RID: 2280
			// (set) Token: 0x06001F62 RID: 8034 RVA: 0x00040685 File Offset: 0x0003E885
			public virtual int SampleCountForStatistics
			{
				set
				{
					base.PowerSharpParameters["SampleCountForStatistics"] = value;
				}
			}

			// Token: 0x170008E9 RID: 2281
			// (set) Token: 0x06001F63 RID: 8035 RVA: 0x0004069D File Offset: 0x0003E89D
			public virtual SwitchParameter TenantSync
			{
				set
				{
					base.PowerSharpParameters["TenantSync"] = value;
				}
			}

			// Token: 0x170008EA RID: 2282
			// (set) Token: 0x06001F64 RID: 8036 RVA: 0x000406B5 File Offset: 0x0003E8B5
			public virtual SwitchParameter UseLastCommittedCookie
			{
				set
				{
					base.PowerSharpParameters["UseLastCommittedCookie"] = value;
				}
			}

			// Token: 0x170008EB RID: 2283
			// (set) Token: 0x06001F65 RID: 8037 RVA: 0x000406CD File Offset: 0x0003E8CD
			public virtual int MaxNumberOfBatches
			{
				set
				{
					base.PowerSharpParameters["MaxNumberOfBatches"] = value;
				}
			}

			// Token: 0x170008EC RID: 2284
			// (set) Token: 0x06001F66 RID: 8038 RVA: 0x000406E5 File Offset: 0x0003E8E5
			public virtual string ServiceInstanceId
			{
				set
				{
					base.PowerSharpParameters["ServiceInstanceId"] = value;
				}
			}

			// Token: 0x170008ED RID: 2285
			// (set) Token: 0x06001F67 RID: 8039 RVA: 0x000406F8 File Offset: 0x0003E8F8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170008EE RID: 2286
			// (set) Token: 0x06001F68 RID: 8040 RVA: 0x00040710 File Offset: 0x0003E910
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170008EF RID: 2287
			// (set) Token: 0x06001F69 RID: 8041 RVA: 0x00040728 File Offset: 0x0003E928
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170008F0 RID: 2288
			// (set) Token: 0x06001F6A RID: 8042 RVA: 0x00040740 File Offset: 0x0003E940
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000119 RID: 281
		public class EstimateBacklogParameters : ParametersBase
		{
			// Token: 0x170008F1 RID: 2289
			// (set) Token: 0x06001F6C RID: 8044 RVA: 0x00040760 File Offset: 0x0003E960
			public virtual SwitchParameter EstimateBacklog
			{
				set
				{
					base.PowerSharpParameters["EstimateBacklog"] = value;
				}
			}

			// Token: 0x170008F2 RID: 2290
			// (set) Token: 0x06001F6D RID: 8045 RVA: 0x00040778 File Offset: 0x0003E978
			public virtual byte PageToken
			{
				set
				{
					base.PowerSharpParameters["PageToken"] = value;
				}
			}

			// Token: 0x170008F3 RID: 2291
			// (set) Token: 0x06001F6E RID: 8046 RVA: 0x00040790 File Offset: 0x0003E990
			public virtual int MaxNumberOfBatches
			{
				set
				{
					base.PowerSharpParameters["MaxNumberOfBatches"] = value;
				}
			}

			// Token: 0x170008F4 RID: 2292
			// (set) Token: 0x06001F6F RID: 8047 RVA: 0x000407A8 File Offset: 0x0003E9A8
			public virtual string ServiceInstanceId
			{
				set
				{
					base.PowerSharpParameters["ServiceInstanceId"] = value;
				}
			}

			// Token: 0x170008F5 RID: 2293
			// (set) Token: 0x06001F70 RID: 8048 RVA: 0x000407BB File Offset: 0x0003E9BB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170008F6 RID: 2294
			// (set) Token: 0x06001F71 RID: 8049 RVA: 0x000407D3 File Offset: 0x0003E9D3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170008F7 RID: 2295
			// (set) Token: 0x06001F72 RID: 8050 RVA: 0x000407EB File Offset: 0x0003E9EB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170008F8 RID: 2296
			// (set) Token: 0x06001F73 RID: 8051 RVA: 0x00040803 File Offset: 0x0003EA03
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200011A RID: 282
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170008F9 RID: 2297
			// (set) Token: 0x06001F75 RID: 8053 RVA: 0x00040823 File Offset: 0x0003EA23
			public virtual int MaxNumberOfBatches
			{
				set
				{
					base.PowerSharpParameters["MaxNumberOfBatches"] = value;
				}
			}

			// Token: 0x170008FA RID: 2298
			// (set) Token: 0x06001F76 RID: 8054 RVA: 0x0004083B File Offset: 0x0003EA3B
			public virtual string ServiceInstanceId
			{
				set
				{
					base.PowerSharpParameters["ServiceInstanceId"] = value;
				}
			}

			// Token: 0x170008FB RID: 2299
			// (set) Token: 0x06001F77 RID: 8055 RVA: 0x0004084E File Offset: 0x0003EA4E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170008FC RID: 2300
			// (set) Token: 0x06001F78 RID: 8056 RVA: 0x00040866 File Offset: 0x0003EA66
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170008FD RID: 2301
			// (set) Token: 0x06001F79 RID: 8057 RVA: 0x0004087E File Offset: 0x0003EA7E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170008FE RID: 2302
			// (set) Token: 0x06001F7A RID: 8058 RVA: 0x00040896 File Offset: 0x0003EA96
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
