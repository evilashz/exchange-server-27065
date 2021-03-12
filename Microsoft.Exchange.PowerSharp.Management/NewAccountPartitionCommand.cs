using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000860 RID: 2144
	public class NewAccountPartitionCommand : SyntheticCommandWithPipelineInput<AccountPartition, AccountPartition>
	{
		// Token: 0x06006A69 RID: 27241 RVA: 0x000A17CD File Offset: 0x0009F9CD
		private NewAccountPartitionCommand() : base("New-AccountPartition")
		{
		}

		// Token: 0x06006A6A RID: 27242 RVA: 0x000A17DA File Offset: 0x0009F9DA
		public NewAccountPartitionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006A6B RID: 27243 RVA: 0x000A17E9 File Offset: 0x0009F9E9
		public virtual NewAccountPartitionCommand SetParameters(NewAccountPartitionCommand.SecondaryParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006A6C RID: 27244 RVA: 0x000A17F3 File Offset: 0x0009F9F3
		public virtual NewAccountPartitionCommand SetParameters(NewAccountPartitionCommand.TrustParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006A6D RID: 27245 RVA: 0x000A17FD File Offset: 0x0009F9FD
		public virtual NewAccountPartitionCommand SetParameters(NewAccountPartitionCommand.LocalForestParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006A6E RID: 27246 RVA: 0x000A1807 File Offset: 0x0009FA07
		public virtual NewAccountPartitionCommand SetParameters(NewAccountPartitionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000861 RID: 2145
		public class SecondaryParameters : ParametersBase
		{
			// Token: 0x17004562 RID: 17762
			// (set) Token: 0x06006A6F RID: 27247 RVA: 0x000A1811 File Offset: 0x0009FA11
			public virtual Fqdn Trust
			{
				set
				{
					base.PowerSharpParameters["Trust"] = value;
				}
			}

			// Token: 0x17004563 RID: 17763
			// (set) Token: 0x06006A70 RID: 27248 RVA: 0x000A1824 File Offset: 0x0009FA24
			public virtual SwitchParameter Secondary
			{
				set
				{
					base.PowerSharpParameters["Secondary"] = value;
				}
			}

			// Token: 0x17004564 RID: 17764
			// (set) Token: 0x06006A71 RID: 27249 RVA: 0x000A183C File Offset: 0x0009FA3C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004565 RID: 17765
			// (set) Token: 0x06006A72 RID: 27250 RVA: 0x000A184F File Offset: 0x0009FA4F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004566 RID: 17766
			// (set) Token: 0x06006A73 RID: 27251 RVA: 0x000A1862 File Offset: 0x0009FA62
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004567 RID: 17767
			// (set) Token: 0x06006A74 RID: 27252 RVA: 0x000A187A File Offset: 0x0009FA7A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004568 RID: 17768
			// (set) Token: 0x06006A75 RID: 27253 RVA: 0x000A1892 File Offset: 0x0009FA92
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004569 RID: 17769
			// (set) Token: 0x06006A76 RID: 27254 RVA: 0x000A18AA File Offset: 0x0009FAAA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700456A RID: 17770
			// (set) Token: 0x06006A77 RID: 27255 RVA: 0x000A18C2 File Offset: 0x0009FAC2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000862 RID: 2146
		public class TrustParameters : ParametersBase
		{
			// Token: 0x1700456B RID: 17771
			// (set) Token: 0x06006A79 RID: 27257 RVA: 0x000A18E2 File Offset: 0x0009FAE2
			public virtual Fqdn Trust
			{
				set
				{
					base.PowerSharpParameters["Trust"] = value;
				}
			}

			// Token: 0x1700456C RID: 17772
			// (set) Token: 0x06006A7A RID: 27258 RVA: 0x000A18F5 File Offset: 0x0009FAF5
			public virtual SwitchParameter EnabledForProvisioning
			{
				set
				{
					base.PowerSharpParameters["EnabledForProvisioning"] = value;
				}
			}

			// Token: 0x1700456D RID: 17773
			// (set) Token: 0x06006A7B RID: 27259 RVA: 0x000A190D File Offset: 0x0009FB0D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700456E RID: 17774
			// (set) Token: 0x06006A7C RID: 27260 RVA: 0x000A1920 File Offset: 0x0009FB20
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700456F RID: 17775
			// (set) Token: 0x06006A7D RID: 27261 RVA: 0x000A1933 File Offset: 0x0009FB33
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004570 RID: 17776
			// (set) Token: 0x06006A7E RID: 27262 RVA: 0x000A194B File Offset: 0x0009FB4B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004571 RID: 17777
			// (set) Token: 0x06006A7F RID: 27263 RVA: 0x000A1963 File Offset: 0x0009FB63
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004572 RID: 17778
			// (set) Token: 0x06006A80 RID: 27264 RVA: 0x000A197B File Offset: 0x0009FB7B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004573 RID: 17779
			// (set) Token: 0x06006A81 RID: 27265 RVA: 0x000A1993 File Offset: 0x0009FB93
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000863 RID: 2147
		public class LocalForestParameters : ParametersBase
		{
			// Token: 0x17004574 RID: 17780
			// (set) Token: 0x06006A83 RID: 27267 RVA: 0x000A19B3 File Offset: 0x0009FBB3
			public virtual SwitchParameter LocalForest
			{
				set
				{
					base.PowerSharpParameters["LocalForest"] = value;
				}
			}

			// Token: 0x17004575 RID: 17781
			// (set) Token: 0x06006A84 RID: 27268 RVA: 0x000A19CB File Offset: 0x0009FBCB
			public virtual SwitchParameter EnabledForProvisioning
			{
				set
				{
					base.PowerSharpParameters["EnabledForProvisioning"] = value;
				}
			}

			// Token: 0x17004576 RID: 17782
			// (set) Token: 0x06006A85 RID: 27269 RVA: 0x000A19E3 File Offset: 0x0009FBE3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004577 RID: 17783
			// (set) Token: 0x06006A86 RID: 27270 RVA: 0x000A19F6 File Offset: 0x0009FBF6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004578 RID: 17784
			// (set) Token: 0x06006A87 RID: 27271 RVA: 0x000A1A09 File Offset: 0x0009FC09
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004579 RID: 17785
			// (set) Token: 0x06006A88 RID: 27272 RVA: 0x000A1A21 File Offset: 0x0009FC21
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700457A RID: 17786
			// (set) Token: 0x06006A89 RID: 27273 RVA: 0x000A1A39 File Offset: 0x0009FC39
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700457B RID: 17787
			// (set) Token: 0x06006A8A RID: 27274 RVA: 0x000A1A51 File Offset: 0x0009FC51
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700457C RID: 17788
			// (set) Token: 0x06006A8B RID: 27275 RVA: 0x000A1A69 File Offset: 0x0009FC69
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000864 RID: 2148
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700457D RID: 17789
			// (set) Token: 0x06006A8D RID: 27277 RVA: 0x000A1A89 File Offset: 0x0009FC89
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700457E RID: 17790
			// (set) Token: 0x06006A8E RID: 27278 RVA: 0x000A1A9C File Offset: 0x0009FC9C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700457F RID: 17791
			// (set) Token: 0x06006A8F RID: 27279 RVA: 0x000A1AAF File Offset: 0x0009FCAF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004580 RID: 17792
			// (set) Token: 0x06006A90 RID: 27280 RVA: 0x000A1AC7 File Offset: 0x0009FCC7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004581 RID: 17793
			// (set) Token: 0x06006A91 RID: 27281 RVA: 0x000A1ADF File Offset: 0x0009FCDF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004582 RID: 17794
			// (set) Token: 0x06006A92 RID: 27282 RVA: 0x000A1AF7 File Offset: 0x0009FCF7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004583 RID: 17795
			// (set) Token: 0x06006A93 RID: 27283 RVA: 0x000A1B0F File Offset: 0x0009FD0F
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
