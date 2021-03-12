using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000902 RID: 2306
	public class EnableOutlookAnywhereCommand : SyntheticCommandWithPipelineInput<ADRpcHttpVirtualDirectory, ADRpcHttpVirtualDirectory>
	{
		// Token: 0x06007509 RID: 29961 RVA: 0x000AFD4F File Offset: 0x000ADF4F
		private EnableOutlookAnywhereCommand() : base("Enable-OutlookAnywhere")
		{
		}

		// Token: 0x0600750A RID: 29962 RVA: 0x000AFD5C File Offset: 0x000ADF5C
		public EnableOutlookAnywhereCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600750B RID: 29963 RVA: 0x000AFD6B File Offset: 0x000ADF6B
		public virtual EnableOutlookAnywhereCommand SetParameters(EnableOutlookAnywhereCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000903 RID: 2307
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004EBE RID: 20158
			// (set) Token: 0x0600750C RID: 29964 RVA: 0x000AFD75 File Offset: 0x000ADF75
			public virtual bool SSLOffloading
			{
				set
				{
					base.PowerSharpParameters["SSLOffloading"] = value;
				}
			}

			// Token: 0x17004EBF RID: 20159
			// (set) Token: 0x0600750D RID: 29965 RVA: 0x000AFD8D File Offset: 0x000ADF8D
			public virtual Hostname ExternalHostname
			{
				set
				{
					base.PowerSharpParameters["ExternalHostname"] = value;
				}
			}

			// Token: 0x17004EC0 RID: 20160
			// (set) Token: 0x0600750E RID: 29966 RVA: 0x000AFDA0 File Offset: 0x000ADFA0
			public virtual Hostname InternalHostname
			{
				set
				{
					base.PowerSharpParameters["InternalHostname"] = value;
				}
			}

			// Token: 0x17004EC1 RID: 20161
			// (set) Token: 0x0600750F RID: 29967 RVA: 0x000AFDB3 File Offset: 0x000ADFB3
			public virtual AuthenticationMethod DefaultAuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["DefaultAuthenticationMethod"] = value;
				}
			}

			// Token: 0x17004EC2 RID: 20162
			// (set) Token: 0x06007510 RID: 29968 RVA: 0x000AFDCB File Offset: 0x000ADFCB
			public virtual AuthenticationMethod ExternalClientAuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["ExternalClientAuthenticationMethod"] = value;
				}
			}

			// Token: 0x17004EC3 RID: 20163
			// (set) Token: 0x06007511 RID: 29969 RVA: 0x000AFDE3 File Offset: 0x000ADFE3
			public virtual AuthenticationMethod InternalClientAuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["InternalClientAuthenticationMethod"] = value;
				}
			}

			// Token: 0x17004EC4 RID: 20164
			// (set) Token: 0x06007512 RID: 29970 RVA: 0x000AFDFB File Offset: 0x000ADFFB
			public virtual MultiValuedProperty<AuthenticationMethod> IISAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["IISAuthenticationMethods"] = value;
				}
			}

			// Token: 0x17004EC5 RID: 20165
			// (set) Token: 0x06007513 RID: 29971 RVA: 0x000AFE0E File Offset: 0x000AE00E
			public virtual Uri XropUrl
			{
				set
				{
					base.PowerSharpParameters["XropUrl"] = value;
				}
			}

			// Token: 0x17004EC6 RID: 20166
			// (set) Token: 0x06007514 RID: 29972 RVA: 0x000AFE21 File Offset: 0x000AE021
			public virtual bool ExternalClientsRequireSsl
			{
				set
				{
					base.PowerSharpParameters["ExternalClientsRequireSsl"] = value;
				}
			}

			// Token: 0x17004EC7 RID: 20167
			// (set) Token: 0x06007515 RID: 29973 RVA: 0x000AFE39 File Offset: 0x000AE039
			public virtual bool InternalClientsRequireSsl
			{
				set
				{
					base.PowerSharpParameters["InternalClientsRequireSsl"] = value;
				}
			}

			// Token: 0x17004EC8 RID: 20168
			// (set) Token: 0x06007516 RID: 29974 RVA: 0x000AFE51 File Offset: 0x000AE051
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17004EC9 RID: 20169
			// (set) Token: 0x06007517 RID: 29975 RVA: 0x000AFE69 File Offset: 0x000AE069
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x17004ECA RID: 20170
			// (set) Token: 0x06007518 RID: 29976 RVA: 0x000AFE7C File Offset: 0x000AE07C
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x17004ECB RID: 20171
			// (set) Token: 0x06007519 RID: 29977 RVA: 0x000AFE8F File Offset: 0x000AE08F
			public virtual VirtualDirectoryRole Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x17004ECC RID: 20172
			// (set) Token: 0x0600751A RID: 29978 RVA: 0x000AFEA7 File Offset: 0x000AE0A7
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004ECD RID: 20173
			// (set) Token: 0x0600751B RID: 29979 RVA: 0x000AFEBA File Offset: 0x000AE0BA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004ECE RID: 20174
			// (set) Token: 0x0600751C RID: 29980 RVA: 0x000AFECD File Offset: 0x000AE0CD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004ECF RID: 20175
			// (set) Token: 0x0600751D RID: 29981 RVA: 0x000AFEE5 File Offset: 0x000AE0E5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004ED0 RID: 20176
			// (set) Token: 0x0600751E RID: 29982 RVA: 0x000AFEFD File Offset: 0x000AE0FD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004ED1 RID: 20177
			// (set) Token: 0x0600751F RID: 29983 RVA: 0x000AFF15 File Offset: 0x000AE115
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004ED2 RID: 20178
			// (set) Token: 0x06007520 RID: 29984 RVA: 0x000AFF2D File Offset: 0x000AE12D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
