using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006D0 RID: 1744
	public class SetGlobalLocatorServiceDomainCommand : SyntheticCommandWithPipelineInputNoOutput<SmtpDomain>
	{
		// Token: 0x06005B51 RID: 23377 RVA: 0x0008E29B File Offset: 0x0008C49B
		private SetGlobalLocatorServiceDomainCommand() : base("Set-GlobalLocatorServiceDomain")
		{
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x0008E2A8 File Offset: 0x0008C4A8
		public SetGlobalLocatorServiceDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005B53 RID: 23379 RVA: 0x0008E2B7 File Offset: 0x0008C4B7
		public virtual SetGlobalLocatorServiceDomainCommand SetParameters(SetGlobalLocatorServiceDomainCommand.DomainNameParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006D1 RID: 1745
		public class DomainNameParameterSetParameters : ParametersBase
		{
			// Token: 0x1700396A RID: 14698
			// (set) Token: 0x06005B54 RID: 23380 RVA: 0x0008E2C1 File Offset: 0x0008C4C1
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x1700396B RID: 14699
			// (set) Token: 0x06005B55 RID: 23381 RVA: 0x0008E2D4 File Offset: 0x0008C4D4
			public virtual GlsDomainFlags DomainFlags
			{
				set
				{
					base.PowerSharpParameters["DomainFlags"] = value;
				}
			}

			// Token: 0x1700396C RID: 14700
			// (set) Token: 0x06005B56 RID: 23382 RVA: 0x0008E2EC File Offset: 0x0008C4EC
			public virtual bool DomainInUse
			{
				set
				{
					base.PowerSharpParameters["DomainInUse"] = value;
				}
			}

			// Token: 0x1700396D RID: 14701
			// (set) Token: 0x06005B57 RID: 23383 RVA: 0x0008E304 File Offset: 0x0008C504
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700396E RID: 14702
			// (set) Token: 0x06005B58 RID: 23384 RVA: 0x0008E31C File Offset: 0x0008C51C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700396F RID: 14703
			// (set) Token: 0x06005B59 RID: 23385 RVA: 0x0008E334 File Offset: 0x0008C534
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003970 RID: 14704
			// (set) Token: 0x06005B5A RID: 23386 RVA: 0x0008E34C File Offset: 0x0008C54C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003971 RID: 14705
			// (set) Token: 0x06005B5B RID: 23387 RVA: 0x0008E364 File Offset: 0x0008C564
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003972 RID: 14706
			// (set) Token: 0x06005B5C RID: 23388 RVA: 0x0008E37C File Offset: 0x0008C57C
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
