using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200064A RID: 1610
	public class NewEdgeSubscriptionCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x06005127 RID: 20775 RVA: 0x0008055C File Offset: 0x0007E75C
		private NewEdgeSubscriptionCommand() : base("New-EdgeSubscription")
		{
		}

		// Token: 0x06005128 RID: 20776 RVA: 0x00080569 File Offset: 0x0007E769
		public NewEdgeSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005129 RID: 20777 RVA: 0x00080578 File Offset: 0x0007E778
		public virtual NewEdgeSubscriptionCommand SetParameters(NewEdgeSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200064B RID: 1611
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700304C RID: 12364
			// (set) Token: 0x0600512A RID: 20778 RVA: 0x00080582 File Offset: 0x0007E782
			public virtual TimeSpan AccountExpiryDuration
			{
				set
				{
					base.PowerSharpParameters["AccountExpiryDuration"] = value;
				}
			}

			// Token: 0x1700304D RID: 12365
			// (set) Token: 0x0600512B RID: 20779 RVA: 0x0008059A File Offset: 0x0007E79A
			public virtual LongPath FileName
			{
				set
				{
					base.PowerSharpParameters["FileName"] = value;
				}
			}

			// Token: 0x1700304E RID: 12366
			// (set) Token: 0x0600512C RID: 20780 RVA: 0x000805AD File Offset: 0x0007E7AD
			public virtual string Site
			{
				set
				{
					base.PowerSharpParameters["Site"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x1700304F RID: 12367
			// (set) Token: 0x0600512D RID: 20781 RVA: 0x000805CB File Offset: 0x0007E7CB
			public virtual bool CreateInternetSendConnector
			{
				set
				{
					base.PowerSharpParameters["CreateInternetSendConnector"] = value;
				}
			}

			// Token: 0x17003050 RID: 12368
			// (set) Token: 0x0600512E RID: 20782 RVA: 0x000805E3 File Offset: 0x0007E7E3
			public virtual bool CreateInboundSendConnector
			{
				set
				{
					base.PowerSharpParameters["CreateInboundSendConnector"] = value;
				}
			}

			// Token: 0x17003051 RID: 12369
			// (set) Token: 0x0600512F RID: 20783 RVA: 0x000805FB File Offset: 0x0007E7FB
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17003052 RID: 12370
			// (set) Token: 0x06005130 RID: 20784 RVA: 0x00080613 File Offset: 0x0007E813
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x17003053 RID: 12371
			// (set) Token: 0x06005131 RID: 20785 RVA: 0x0008062B File Offset: 0x0007E82B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003054 RID: 12372
			// (set) Token: 0x06005132 RID: 20786 RVA: 0x0008063E File Offset: 0x0007E83E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003055 RID: 12373
			// (set) Token: 0x06005133 RID: 20787 RVA: 0x00080656 File Offset: 0x0007E856
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003056 RID: 12374
			// (set) Token: 0x06005134 RID: 20788 RVA: 0x0008066E File Offset: 0x0007E86E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003057 RID: 12375
			// (set) Token: 0x06005135 RID: 20789 RVA: 0x00080686 File Offset: 0x0007E886
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003058 RID: 12376
			// (set) Token: 0x06005136 RID: 20790 RVA: 0x0008069E File Offset: 0x0007E89E
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
