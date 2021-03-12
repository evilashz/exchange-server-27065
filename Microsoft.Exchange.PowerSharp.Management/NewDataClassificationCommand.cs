using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ClassificationDefinitions;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200050B RID: 1291
	public class NewDataClassificationCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x06004601 RID: 17921 RVA: 0x000725E9 File Offset: 0x000707E9
		private NewDataClassificationCommand() : base("New-DataClassification")
		{
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x000725F6 File Offset: 0x000707F6
		public NewDataClassificationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x00072605 File Offset: 0x00070805
		public virtual NewDataClassificationCommand SetParameters(NewDataClassificationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200050C RID: 1292
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170027A4 RID: 10148
			// (set) Token: 0x06004604 RID: 17924 RVA: 0x0007260F File Offset: 0x0007080F
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170027A5 RID: 10149
			// (set) Token: 0x06004605 RID: 17925 RVA: 0x00072622 File Offset: 0x00070822
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x170027A6 RID: 10150
			// (set) Token: 0x06004606 RID: 17926 RVA: 0x00072635 File Offset: 0x00070835
			public virtual CultureInfo Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x170027A7 RID: 10151
			// (set) Token: 0x06004607 RID: 17927 RVA: 0x00072648 File Offset: 0x00070848
			public virtual MultiValuedProperty<Fingerprint> Fingerprints
			{
				set
				{
					base.PowerSharpParameters["Fingerprints"] = value;
				}
			}

			// Token: 0x170027A8 RID: 10152
			// (set) Token: 0x06004608 RID: 17928 RVA: 0x0007265B File Offset: 0x0007085B
			public virtual ClassificationRuleCollectionIdParameter ClassificationRuleCollectionIdentity
			{
				set
				{
					base.PowerSharpParameters["ClassificationRuleCollectionIdentity"] = value;
				}
			}

			// Token: 0x170027A9 RID: 10153
			// (set) Token: 0x06004609 RID: 17929 RVA: 0x0007266E File Offset: 0x0007086E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170027AA RID: 10154
			// (set) Token: 0x0600460A RID: 17930 RVA: 0x0007268C File Offset: 0x0007088C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170027AB RID: 10155
			// (set) Token: 0x0600460B RID: 17931 RVA: 0x0007269F File Offset: 0x0007089F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170027AC RID: 10156
			// (set) Token: 0x0600460C RID: 17932 RVA: 0x000726B7 File Offset: 0x000708B7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170027AD RID: 10157
			// (set) Token: 0x0600460D RID: 17933 RVA: 0x000726CF File Offset: 0x000708CF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170027AE RID: 10158
			// (set) Token: 0x0600460E RID: 17934 RVA: 0x000726E7 File Offset: 0x000708E7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170027AF RID: 10159
			// (set) Token: 0x0600460F RID: 17935 RVA: 0x000726FF File Offset: 0x000708FF
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
