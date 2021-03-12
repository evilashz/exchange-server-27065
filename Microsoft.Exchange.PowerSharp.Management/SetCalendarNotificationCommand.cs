using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000443 RID: 1091
	public class SetCalendarNotificationCommand : SyntheticCommandWithPipelineInputNoOutput<CalendarNotification>
	{
		// Token: 0x06003F16 RID: 16150 RVA: 0x000699DE File Offset: 0x00067BDE
		private SetCalendarNotificationCommand() : base("Set-CalendarNotification")
		{
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x000699EB File Offset: 0x00067BEB
		public SetCalendarNotificationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x000699FA File Offset: 0x00067BFA
		public virtual SetCalendarNotificationCommand SetParameters(SetCalendarNotificationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x00069A04 File Offset: 0x00067C04
		public virtual SetCalendarNotificationCommand SetParameters(SetCalendarNotificationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000444 RID: 1092
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002249 RID: 8777
			// (set) Token: 0x06003F1A RID: 16154 RVA: 0x00069A0E File Offset: 0x00067C0E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700224A RID: 8778
			// (set) Token: 0x06003F1B RID: 16155 RVA: 0x00069A2C File Offset: 0x00067C2C
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700224B RID: 8779
			// (set) Token: 0x06003F1C RID: 16156 RVA: 0x00069A44 File Offset: 0x00067C44
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700224C RID: 8780
			// (set) Token: 0x06003F1D RID: 16157 RVA: 0x00069A57 File Offset: 0x00067C57
			public virtual bool CalendarUpdateNotification
			{
				set
				{
					base.PowerSharpParameters["CalendarUpdateNotification"] = value;
				}
			}

			// Token: 0x1700224D RID: 8781
			// (set) Token: 0x06003F1E RID: 16158 RVA: 0x00069A6F File Offset: 0x00067C6F
			public virtual int NextDays
			{
				set
				{
					base.PowerSharpParameters["NextDays"] = value;
				}
			}

			// Token: 0x1700224E RID: 8782
			// (set) Token: 0x06003F1F RID: 16159 RVA: 0x00069A87 File Offset: 0x00067C87
			public virtual bool CalendarUpdateSendDuringWorkHour
			{
				set
				{
					base.PowerSharpParameters["CalendarUpdateSendDuringWorkHour"] = value;
				}
			}

			// Token: 0x1700224F RID: 8783
			// (set) Token: 0x06003F20 RID: 16160 RVA: 0x00069A9F File Offset: 0x00067C9F
			public virtual bool MeetingReminderNotification
			{
				set
				{
					base.PowerSharpParameters["MeetingReminderNotification"] = value;
				}
			}

			// Token: 0x17002250 RID: 8784
			// (set) Token: 0x06003F21 RID: 16161 RVA: 0x00069AB7 File Offset: 0x00067CB7
			public virtual bool MeetingReminderSendDuringWorkHour
			{
				set
				{
					base.PowerSharpParameters["MeetingReminderSendDuringWorkHour"] = value;
				}
			}

			// Token: 0x17002251 RID: 8785
			// (set) Token: 0x06003F22 RID: 16162 RVA: 0x00069ACF File Offset: 0x00067CCF
			public virtual bool DailyAgendaNotification
			{
				set
				{
					base.PowerSharpParameters["DailyAgendaNotification"] = value;
				}
			}

			// Token: 0x17002252 RID: 8786
			// (set) Token: 0x06003F23 RID: 16163 RVA: 0x00069AE7 File Offset: 0x00067CE7
			public virtual TimeSpan DailyAgendaNotificationSendTime
			{
				set
				{
					base.PowerSharpParameters["DailyAgendaNotificationSendTime"] = value;
				}
			}

			// Token: 0x17002253 RID: 8787
			// (set) Token: 0x06003F24 RID: 16164 RVA: 0x00069AFF File Offset: 0x00067CFF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002254 RID: 8788
			// (set) Token: 0x06003F25 RID: 16165 RVA: 0x00069B17 File Offset: 0x00067D17
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002255 RID: 8789
			// (set) Token: 0x06003F26 RID: 16166 RVA: 0x00069B2F File Offset: 0x00067D2F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002256 RID: 8790
			// (set) Token: 0x06003F27 RID: 16167 RVA: 0x00069B47 File Offset: 0x00067D47
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002257 RID: 8791
			// (set) Token: 0x06003F28 RID: 16168 RVA: 0x00069B5F File Offset: 0x00067D5F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000445 RID: 1093
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002258 RID: 8792
			// (set) Token: 0x06003F2A RID: 16170 RVA: 0x00069B7F File Offset: 0x00067D7F
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17002259 RID: 8793
			// (set) Token: 0x06003F2B RID: 16171 RVA: 0x00069B97 File Offset: 0x00067D97
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700225A RID: 8794
			// (set) Token: 0x06003F2C RID: 16172 RVA: 0x00069BAA File Offset: 0x00067DAA
			public virtual bool CalendarUpdateNotification
			{
				set
				{
					base.PowerSharpParameters["CalendarUpdateNotification"] = value;
				}
			}

			// Token: 0x1700225B RID: 8795
			// (set) Token: 0x06003F2D RID: 16173 RVA: 0x00069BC2 File Offset: 0x00067DC2
			public virtual int NextDays
			{
				set
				{
					base.PowerSharpParameters["NextDays"] = value;
				}
			}

			// Token: 0x1700225C RID: 8796
			// (set) Token: 0x06003F2E RID: 16174 RVA: 0x00069BDA File Offset: 0x00067DDA
			public virtual bool CalendarUpdateSendDuringWorkHour
			{
				set
				{
					base.PowerSharpParameters["CalendarUpdateSendDuringWorkHour"] = value;
				}
			}

			// Token: 0x1700225D RID: 8797
			// (set) Token: 0x06003F2F RID: 16175 RVA: 0x00069BF2 File Offset: 0x00067DF2
			public virtual bool MeetingReminderNotification
			{
				set
				{
					base.PowerSharpParameters["MeetingReminderNotification"] = value;
				}
			}

			// Token: 0x1700225E RID: 8798
			// (set) Token: 0x06003F30 RID: 16176 RVA: 0x00069C0A File Offset: 0x00067E0A
			public virtual bool MeetingReminderSendDuringWorkHour
			{
				set
				{
					base.PowerSharpParameters["MeetingReminderSendDuringWorkHour"] = value;
				}
			}

			// Token: 0x1700225F RID: 8799
			// (set) Token: 0x06003F31 RID: 16177 RVA: 0x00069C22 File Offset: 0x00067E22
			public virtual bool DailyAgendaNotification
			{
				set
				{
					base.PowerSharpParameters["DailyAgendaNotification"] = value;
				}
			}

			// Token: 0x17002260 RID: 8800
			// (set) Token: 0x06003F32 RID: 16178 RVA: 0x00069C3A File Offset: 0x00067E3A
			public virtual TimeSpan DailyAgendaNotificationSendTime
			{
				set
				{
					base.PowerSharpParameters["DailyAgendaNotificationSendTime"] = value;
				}
			}

			// Token: 0x17002261 RID: 8801
			// (set) Token: 0x06003F33 RID: 16179 RVA: 0x00069C52 File Offset: 0x00067E52
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002262 RID: 8802
			// (set) Token: 0x06003F34 RID: 16180 RVA: 0x00069C6A File Offset: 0x00067E6A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002263 RID: 8803
			// (set) Token: 0x06003F35 RID: 16181 RVA: 0x00069C82 File Offset: 0x00067E82
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002264 RID: 8804
			// (set) Token: 0x06003F36 RID: 16182 RVA: 0x00069C9A File Offset: 0x00067E9A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002265 RID: 8805
			// (set) Token: 0x06003F37 RID: 16183 RVA: 0x00069CB2 File Offset: 0x00067EB2
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
