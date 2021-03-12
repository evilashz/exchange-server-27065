using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200073E RID: 1854
	public class GetIPAllowListEntryCommand : SyntheticCommandWithPipelineInput<IPAllowListEntry, IPAllowListEntry>
	{
		// Token: 0x06005F25 RID: 24357 RVA: 0x000930FF File Offset: 0x000912FF
		private GetIPAllowListEntryCommand() : base("Get-IPAllowListEntry")
		{
		}

		// Token: 0x06005F26 RID: 24358 RVA: 0x0009310C File Offset: 0x0009130C
		public GetIPAllowListEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005F27 RID: 24359 RVA: 0x0009311B File Offset: 0x0009131B
		public virtual GetIPAllowListEntryCommand SetParameters(GetIPAllowListEntryCommand.IPAddressParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005F28 RID: 24360 RVA: 0x00093125 File Offset: 0x00091325
		public virtual GetIPAllowListEntryCommand SetParameters(GetIPAllowListEntryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005F29 RID: 24361 RVA: 0x0009312F File Offset: 0x0009132F
		public virtual GetIPAllowListEntryCommand SetParameters(GetIPAllowListEntryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200073F RID: 1855
		public class IPAddressParameters : ParametersBase
		{
			// Token: 0x17003C62 RID: 15458
			// (set) Token: 0x06005F2A RID: 24362 RVA: 0x00093139 File Offset: 0x00091339
			public virtual IPAddress IPAddress
			{
				set
				{
					base.PowerSharpParameters["IPAddress"] = value;
				}
			}

			// Token: 0x17003C63 RID: 15459
			// (set) Token: 0x06005F2B RID: 24363 RVA: 0x0009314C File Offset: 0x0009134C
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C64 RID: 15460
			// (set) Token: 0x06005F2C RID: 24364 RVA: 0x0009315F File Offset: 0x0009135F
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17003C65 RID: 15461
			// (set) Token: 0x06005F2D RID: 24365 RVA: 0x00093177 File Offset: 0x00091377
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C66 RID: 15462
			// (set) Token: 0x06005F2E RID: 24366 RVA: 0x0009318F File Offset: 0x0009138F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C67 RID: 15463
			// (set) Token: 0x06005F2F RID: 24367 RVA: 0x000931A7 File Offset: 0x000913A7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C68 RID: 15464
			// (set) Token: 0x06005F30 RID: 24368 RVA: 0x000931BF File Offset: 0x000913BF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000740 RID: 1856
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003C69 RID: 15465
			// (set) Token: 0x06005F32 RID: 24370 RVA: 0x000931DF File Offset: 0x000913DF
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C6A RID: 15466
			// (set) Token: 0x06005F33 RID: 24371 RVA: 0x000931F2 File Offset: 0x000913F2
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17003C6B RID: 15467
			// (set) Token: 0x06005F34 RID: 24372 RVA: 0x0009320A File Offset: 0x0009140A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C6C RID: 15468
			// (set) Token: 0x06005F35 RID: 24373 RVA: 0x00093222 File Offset: 0x00091422
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C6D RID: 15469
			// (set) Token: 0x06005F36 RID: 24374 RVA: 0x0009323A File Offset: 0x0009143A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C6E RID: 15470
			// (set) Token: 0x06005F37 RID: 24375 RVA: 0x00093252 File Offset: 0x00091452
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000741 RID: 1857
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003C6F RID: 15471
			// (set) Token: 0x06005F39 RID: 24377 RVA: 0x00093272 File Offset: 0x00091472
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new IPListEntryIdentity(value) : null);
				}
			}

			// Token: 0x17003C70 RID: 15472
			// (set) Token: 0x06005F3A RID: 24378 RVA: 0x00093290 File Offset: 0x00091490
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C71 RID: 15473
			// (set) Token: 0x06005F3B RID: 24379 RVA: 0x000932A3 File Offset: 0x000914A3
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17003C72 RID: 15474
			// (set) Token: 0x06005F3C RID: 24380 RVA: 0x000932BB File Offset: 0x000914BB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C73 RID: 15475
			// (set) Token: 0x06005F3D RID: 24381 RVA: 0x000932D3 File Offset: 0x000914D3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C74 RID: 15476
			// (set) Token: 0x06005F3E RID: 24382 RVA: 0x000932EB File Offset: 0x000914EB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C75 RID: 15477
			// (set) Token: 0x06005F3F RID: 24383 RVA: 0x00093303 File Offset: 0x00091503
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
