using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200075D RID: 1885
	public class SetIPAllowListProvidersConfigCommand : SyntheticCommandWithPipelineInputNoOutput<IPAllowListProviderConfig>
	{
		// Token: 0x06005FFE RID: 24574 RVA: 0x000941A3 File Offset: 0x000923A3
		private SetIPAllowListProvidersConfigCommand() : base("Set-IPAllowListProvidersConfig")
		{
		}

		// Token: 0x06005FFF RID: 24575 RVA: 0x000941B0 File Offset: 0x000923B0
		public SetIPAllowListProvidersConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006000 RID: 24576 RVA: 0x000941BF File Offset: 0x000923BF
		public virtual SetIPAllowListProvidersConfigCommand SetParameters(SetIPAllowListProvidersConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200075E RID: 1886
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003CFD RID: 15613
			// (set) Token: 0x06006001 RID: 24577 RVA: 0x000941C9 File Offset: 0x000923C9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003CFE RID: 15614
			// (set) Token: 0x06006002 RID: 24578 RVA: 0x000941DC File Offset: 0x000923DC
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003CFF RID: 15615
			// (set) Token: 0x06006003 RID: 24579 RVA: 0x000941F4 File Offset: 0x000923F4
			public virtual bool ExternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003D00 RID: 15616
			// (set) Token: 0x06006004 RID: 24580 RVA: 0x0009420C File Offset: 0x0009240C
			public virtual bool InternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003D01 RID: 15617
			// (set) Token: 0x06006005 RID: 24581 RVA: 0x00094224 File Offset: 0x00092424
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003D02 RID: 15618
			// (set) Token: 0x06006006 RID: 24582 RVA: 0x0009423C File Offset: 0x0009243C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003D03 RID: 15619
			// (set) Token: 0x06006007 RID: 24583 RVA: 0x00094254 File Offset: 0x00092454
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003D04 RID: 15620
			// (set) Token: 0x06006008 RID: 24584 RVA: 0x0009426C File Offset: 0x0009246C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003D05 RID: 15621
			// (set) Token: 0x06006009 RID: 24585 RVA: 0x00094284 File Offset: 0x00092484
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
