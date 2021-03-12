using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200064E RID: 1614
	public class NewHttpContainerCommand : SyntheticCommandWithPipelineInput<HttpContainer, HttpContainer>
	{
		// Token: 0x06005142 RID: 20802 RVA: 0x00080772 File Offset: 0x0007E972
		private NewHttpContainerCommand() : base("New-HttpContainer")
		{
		}

		// Token: 0x06005143 RID: 20803 RVA: 0x0008077F File Offset: 0x0007E97F
		public NewHttpContainerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005144 RID: 20804 RVA: 0x0008078E File Offset: 0x0007E98E
		public virtual NewHttpContainerCommand SetParameters(NewHttpContainerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200064F RID: 1615
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700305F RID: 12383
			// (set) Token: 0x06005145 RID: 20805 RVA: 0x00080798 File Offset: 0x0007E998
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003060 RID: 12384
			// (set) Token: 0x06005146 RID: 20806 RVA: 0x000807AB File Offset: 0x0007E9AB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003061 RID: 12385
			// (set) Token: 0x06005147 RID: 20807 RVA: 0x000807C3 File Offset: 0x0007E9C3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003062 RID: 12386
			// (set) Token: 0x06005148 RID: 20808 RVA: 0x000807DB File Offset: 0x0007E9DB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003063 RID: 12387
			// (set) Token: 0x06005149 RID: 20809 RVA: 0x000807F3 File Offset: 0x0007E9F3
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
