using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200043C RID: 1084
	public class GetCalendarDiagnosticLogCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06003ECD RID: 16077 RVA: 0x00069407 File Offset: 0x00067607
		private GetCalendarDiagnosticLogCommand() : base("Get-CalendarDiagnosticLog")
		{
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x00069414 File Offset: 0x00067614
		public GetCalendarDiagnosticLogCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x00069423 File Offset: 0x00067623
		public virtual GetCalendarDiagnosticLogCommand SetParameters(GetCalendarDiagnosticLogCommand.ExportToMsgParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x0006942D File Offset: 0x0006762D
		public virtual GetCalendarDiagnosticLogCommand SetParameters(GetCalendarDiagnosticLogCommand.DoNotExportParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x00069437 File Offset: 0x00067637
		public virtual GetCalendarDiagnosticLogCommand SetParameters(GetCalendarDiagnosticLogCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200043D RID: 1085
		public class ExportToMsgParameterSetParameters : ParametersBase
		{
			// Token: 0x1700220E RID: 8718
			// (set) Token: 0x06003ED2 RID: 16082 RVA: 0x00069441 File Offset: 0x00067641
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700220F RID: 8719
			// (set) Token: 0x06003ED3 RID: 16083 RVA: 0x0006945F File Offset: 0x0006765F
			public virtual string LogLocation
			{
				set
				{
					base.PowerSharpParameters["LogLocation"] = value;
				}
			}

			// Token: 0x17002210 RID: 8720
			// (set) Token: 0x06003ED4 RID: 16084 RVA: 0x00069472 File Offset: 0x00067672
			public virtual string Subject
			{
				set
				{
					base.PowerSharpParameters["Subject"] = value;
				}
			}

			// Token: 0x17002211 RID: 8721
			// (set) Token: 0x06003ED5 RID: 16085 RVA: 0x00069485 File Offset: 0x00067685
			public virtual string MeetingID
			{
				set
				{
					base.PowerSharpParameters["MeetingID"] = value;
				}
			}

			// Token: 0x17002212 RID: 8722
			// (set) Token: 0x06003ED6 RID: 16086 RVA: 0x00069498 File Offset: 0x00067698
			public virtual ExDateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17002213 RID: 8723
			// (set) Token: 0x06003ED7 RID: 16087 RVA: 0x000694B0 File Offset: 0x000676B0
			public virtual ExDateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17002214 RID: 8724
			// (set) Token: 0x06003ED8 RID: 16088 RVA: 0x000694C8 File Offset: 0x000676C8
			public virtual SwitchParameter Latest
			{
				set
				{
					base.PowerSharpParameters["Latest"] = value;
				}
			}

			// Token: 0x17002215 RID: 8725
			// (set) Token: 0x06003ED9 RID: 16089 RVA: 0x000694E0 File Offset: 0x000676E0
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17002216 RID: 8726
			// (set) Token: 0x06003EDA RID: 16090 RVA: 0x000694F3 File Offset: 0x000676F3
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17002217 RID: 8727
			// (set) Token: 0x06003EDB RID: 16091 RVA: 0x0006950B File Offset: 0x0006770B
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17002218 RID: 8728
			// (set) Token: 0x06003EDC RID: 16092 RVA: 0x00069523 File Offset: 0x00067723
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002219 RID: 8729
			// (set) Token: 0x06003EDD RID: 16093 RVA: 0x00069536 File Offset: 0x00067736
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700221A RID: 8730
			// (set) Token: 0x06003EDE RID: 16094 RVA: 0x0006954E File Offset: 0x0006774E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700221B RID: 8731
			// (set) Token: 0x06003EDF RID: 16095 RVA: 0x00069566 File Offset: 0x00067766
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700221C RID: 8732
			// (set) Token: 0x06003EE0 RID: 16096 RVA: 0x0006957E File Offset: 0x0006777E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200043E RID: 1086
		public class DoNotExportParameterSetParameters : ParametersBase
		{
			// Token: 0x1700221D RID: 8733
			// (set) Token: 0x06003EE2 RID: 16098 RVA: 0x0006959E File Offset: 0x0006779E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700221E RID: 8734
			// (set) Token: 0x06003EE3 RID: 16099 RVA: 0x000695BC File Offset: 0x000677BC
			public virtual string Subject
			{
				set
				{
					base.PowerSharpParameters["Subject"] = value;
				}
			}

			// Token: 0x1700221F RID: 8735
			// (set) Token: 0x06003EE4 RID: 16100 RVA: 0x000695CF File Offset: 0x000677CF
			public virtual string MeetingID
			{
				set
				{
					base.PowerSharpParameters["MeetingID"] = value;
				}
			}

			// Token: 0x17002220 RID: 8736
			// (set) Token: 0x06003EE5 RID: 16101 RVA: 0x000695E2 File Offset: 0x000677E2
			public virtual ExDateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17002221 RID: 8737
			// (set) Token: 0x06003EE6 RID: 16102 RVA: 0x000695FA File Offset: 0x000677FA
			public virtual ExDateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17002222 RID: 8738
			// (set) Token: 0x06003EE7 RID: 16103 RVA: 0x00069612 File Offset: 0x00067812
			public virtual SwitchParameter Latest
			{
				set
				{
					base.PowerSharpParameters["Latest"] = value;
				}
			}

			// Token: 0x17002223 RID: 8739
			// (set) Token: 0x06003EE8 RID: 16104 RVA: 0x0006962A File Offset: 0x0006782A
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17002224 RID: 8740
			// (set) Token: 0x06003EE9 RID: 16105 RVA: 0x0006963D File Offset: 0x0006783D
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17002225 RID: 8741
			// (set) Token: 0x06003EEA RID: 16106 RVA: 0x00069655 File Offset: 0x00067855
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17002226 RID: 8742
			// (set) Token: 0x06003EEB RID: 16107 RVA: 0x0006966D File Offset: 0x0006786D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002227 RID: 8743
			// (set) Token: 0x06003EEC RID: 16108 RVA: 0x00069680 File Offset: 0x00067880
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002228 RID: 8744
			// (set) Token: 0x06003EED RID: 16109 RVA: 0x00069698 File Offset: 0x00067898
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002229 RID: 8745
			// (set) Token: 0x06003EEE RID: 16110 RVA: 0x000696B0 File Offset: 0x000678B0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700222A RID: 8746
			// (set) Token: 0x06003EEF RID: 16111 RVA: 0x000696C8 File Offset: 0x000678C8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200043F RID: 1087
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700222B RID: 8747
			// (set) Token: 0x06003EF1 RID: 16113 RVA: 0x000696E8 File Offset: 0x000678E8
			public virtual string Subject
			{
				set
				{
					base.PowerSharpParameters["Subject"] = value;
				}
			}

			// Token: 0x1700222C RID: 8748
			// (set) Token: 0x06003EF2 RID: 16114 RVA: 0x000696FB File Offset: 0x000678FB
			public virtual string MeetingID
			{
				set
				{
					base.PowerSharpParameters["MeetingID"] = value;
				}
			}

			// Token: 0x1700222D RID: 8749
			// (set) Token: 0x06003EF3 RID: 16115 RVA: 0x0006970E File Offset: 0x0006790E
			public virtual ExDateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x1700222E RID: 8750
			// (set) Token: 0x06003EF4 RID: 16116 RVA: 0x00069726 File Offset: 0x00067926
			public virtual ExDateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x1700222F RID: 8751
			// (set) Token: 0x06003EF5 RID: 16117 RVA: 0x0006973E File Offset: 0x0006793E
			public virtual SwitchParameter Latest
			{
				set
				{
					base.PowerSharpParameters["Latest"] = value;
				}
			}

			// Token: 0x17002230 RID: 8752
			// (set) Token: 0x06003EF6 RID: 16118 RVA: 0x00069756 File Offset: 0x00067956
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17002231 RID: 8753
			// (set) Token: 0x06003EF7 RID: 16119 RVA: 0x00069769 File Offset: 0x00067969
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17002232 RID: 8754
			// (set) Token: 0x06003EF8 RID: 16120 RVA: 0x00069781 File Offset: 0x00067981
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17002233 RID: 8755
			// (set) Token: 0x06003EF9 RID: 16121 RVA: 0x00069799 File Offset: 0x00067999
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002234 RID: 8756
			// (set) Token: 0x06003EFA RID: 16122 RVA: 0x000697AC File Offset: 0x000679AC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002235 RID: 8757
			// (set) Token: 0x06003EFB RID: 16123 RVA: 0x000697C4 File Offset: 0x000679C4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002236 RID: 8758
			// (set) Token: 0x06003EFC RID: 16124 RVA: 0x000697DC File Offset: 0x000679DC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002237 RID: 8759
			// (set) Token: 0x06003EFD RID: 16125 RVA: 0x000697F4 File Offset: 0x000679F4
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
