using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C23 RID: 3107
	public class NewEOPMailUserCommand : SyntheticCommandWithPipelineInputNoOutput<WindowsLiveId>
	{
		// Token: 0x060097C8 RID: 38856 RVA: 0x000DCBC6 File Offset: 0x000DADC6
		private NewEOPMailUserCommand() : base("New-EOPMailUser")
		{
		}

		// Token: 0x060097C9 RID: 38857 RVA: 0x000DCBD3 File Offset: 0x000DADD3
		public NewEOPMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060097CA RID: 38858 RVA: 0x000DCBE2 File Offset: 0x000DADE2
		public virtual NewEOPMailUserCommand SetParameters(NewEOPMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C24 RID: 3108
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006B3B RID: 27451
			// (set) Token: 0x060097CB RID: 38859 RVA: 0x000DCBEC File Offset: 0x000DADEC
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006B3C RID: 27452
			// (set) Token: 0x060097CC RID: 38860 RVA: 0x000DCBFF File Offset: 0x000DADFF
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006B3D RID: 27453
			// (set) Token: 0x060097CD RID: 38861 RVA: 0x000DCC12 File Offset: 0x000DAE12
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17006B3E RID: 27454
			// (set) Token: 0x060097CE RID: 38862 RVA: 0x000DCC25 File Offset: 0x000DAE25
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17006B3F RID: 27455
			// (set) Token: 0x060097CF RID: 38863 RVA: 0x000DCC38 File Offset: 0x000DAE38
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17006B40 RID: 27456
			// (set) Token: 0x060097D0 RID: 38864 RVA: 0x000DCC4B File Offset: 0x000DAE4B
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17006B41 RID: 27457
			// (set) Token: 0x060097D1 RID: 38865 RVA: 0x000DCC5E File Offset: 0x000DAE5E
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17006B42 RID: 27458
			// (set) Token: 0x060097D2 RID: 38866 RVA: 0x000DCC71 File Offset: 0x000DAE71
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006B43 RID: 27459
			// (set) Token: 0x060097D3 RID: 38867 RVA: 0x000DCC84 File Offset: 0x000DAE84
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17006B44 RID: 27460
			// (set) Token: 0x060097D4 RID: 38868 RVA: 0x000DCC97 File Offset: 0x000DAE97
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006B45 RID: 27461
			// (set) Token: 0x060097D5 RID: 38869 RVA: 0x000DCCAF File Offset: 0x000DAEAF
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006B46 RID: 27462
			// (set) Token: 0x060097D6 RID: 38870 RVA: 0x000DCCCD File Offset: 0x000DAECD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006B47 RID: 27463
			// (set) Token: 0x060097D7 RID: 38871 RVA: 0x000DCCE5 File Offset: 0x000DAEE5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006B48 RID: 27464
			// (set) Token: 0x060097D8 RID: 38872 RVA: 0x000DCCFD File Offset: 0x000DAEFD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006B49 RID: 27465
			// (set) Token: 0x060097D9 RID: 38873 RVA: 0x000DCD15 File Offset: 0x000DAF15
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
