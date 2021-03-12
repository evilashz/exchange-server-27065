using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006CE RID: 1742
	public class RemoveGlobalLocatorServiceDomainCommand : SyntheticCommandWithPipelineInputNoOutput<SmtpDomain>
	{
		// Token: 0x06005B46 RID: 23366 RVA: 0x0008E1CA File Offset: 0x0008C3CA
		private RemoveGlobalLocatorServiceDomainCommand() : base("Remove-GlobalLocatorServiceDomain")
		{
		}

		// Token: 0x06005B47 RID: 23367 RVA: 0x0008E1D7 File Offset: 0x0008C3D7
		public RemoveGlobalLocatorServiceDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005B48 RID: 23368 RVA: 0x0008E1E6 File Offset: 0x0008C3E6
		public virtual RemoveGlobalLocatorServiceDomainCommand SetParameters(RemoveGlobalLocatorServiceDomainCommand.DomainNameParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006CF RID: 1743
		public class DomainNameParameterSetParameters : ParametersBase
		{
			// Token: 0x17003963 RID: 14691
			// (set) Token: 0x06005B49 RID: 23369 RVA: 0x0008E1F0 File Offset: 0x0008C3F0
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003964 RID: 14692
			// (set) Token: 0x06005B4A RID: 23370 RVA: 0x0008E203 File Offset: 0x0008C403
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003965 RID: 14693
			// (set) Token: 0x06005B4B RID: 23371 RVA: 0x0008E21B File Offset: 0x0008C41B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003966 RID: 14694
			// (set) Token: 0x06005B4C RID: 23372 RVA: 0x0008E233 File Offset: 0x0008C433
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003967 RID: 14695
			// (set) Token: 0x06005B4D RID: 23373 RVA: 0x0008E24B File Offset: 0x0008C44B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003968 RID: 14696
			// (set) Token: 0x06005B4E RID: 23374 RVA: 0x0008E263 File Offset: 0x0008C463
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003969 RID: 14697
			// (set) Token: 0x06005B4F RID: 23375 RVA: 0x0008E27B File Offset: 0x0008C47B
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
