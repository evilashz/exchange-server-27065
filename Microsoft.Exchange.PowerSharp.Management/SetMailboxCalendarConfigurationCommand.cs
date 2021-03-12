using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200047F RID: 1151
	public class SetMailboxCalendarConfigurationCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxCalendarConfiguration>
	{
		// Token: 0x0600412B RID: 16683 RVA: 0x0006C4DA File Offset: 0x0006A6DA
		private SetMailboxCalendarConfigurationCommand() : base("Set-MailboxCalendarConfiguration")
		{
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x0006C4E7 File Offset: 0x0006A6E7
		public SetMailboxCalendarConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x0006C4F6 File Offset: 0x0006A6F6
		public virtual SetMailboxCalendarConfigurationCommand SetParameters(SetMailboxCalendarConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x0006C500 File Offset: 0x0006A700
		public virtual SetMailboxCalendarConfigurationCommand SetParameters(SetMailboxCalendarConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000480 RID: 1152
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170023E6 RID: 9190
			// (set) Token: 0x0600412F RID: 16687 RVA: 0x0006C50A File Offset: 0x0006A70A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170023E7 RID: 9191
			// (set) Token: 0x06004130 RID: 16688 RVA: 0x0006C51D File Offset: 0x0006A71D
			public virtual DaysOfWeek WorkDays
			{
				set
				{
					base.PowerSharpParameters["WorkDays"] = value;
				}
			}

			// Token: 0x170023E8 RID: 9192
			// (set) Token: 0x06004131 RID: 16689 RVA: 0x0006C535 File Offset: 0x0006A735
			public virtual TimeSpan WorkingHoursStartTime
			{
				set
				{
					base.PowerSharpParameters["WorkingHoursStartTime"] = value;
				}
			}

			// Token: 0x170023E9 RID: 9193
			// (set) Token: 0x06004132 RID: 16690 RVA: 0x0006C54D File Offset: 0x0006A74D
			public virtual TimeSpan WorkingHoursEndTime
			{
				set
				{
					base.PowerSharpParameters["WorkingHoursEndTime"] = value;
				}
			}

			// Token: 0x170023EA RID: 9194
			// (set) Token: 0x06004133 RID: 16691 RVA: 0x0006C565 File Offset: 0x0006A765
			public virtual ExTimeZoneValue WorkingHoursTimeZone
			{
				set
				{
					base.PowerSharpParameters["WorkingHoursTimeZone"] = value;
				}
			}

			// Token: 0x170023EB RID: 9195
			// (set) Token: 0x06004134 RID: 16692 RVA: 0x0006C578 File Offset: 0x0006A778
			public virtual Microsoft.Exchange.Data.Storage.Management.DayOfWeek WeekStartDay
			{
				set
				{
					base.PowerSharpParameters["WeekStartDay"] = value;
				}
			}

			// Token: 0x170023EC RID: 9196
			// (set) Token: 0x06004135 RID: 16693 RVA: 0x0006C590 File Offset: 0x0006A790
			public virtual bool ShowWeekNumbers
			{
				set
				{
					base.PowerSharpParameters["ShowWeekNumbers"] = value;
				}
			}

			// Token: 0x170023ED RID: 9197
			// (set) Token: 0x06004136 RID: 16694 RVA: 0x0006C5A8 File Offset: 0x0006A7A8
			public virtual FirstWeekRules FirstWeekOfYear
			{
				set
				{
					base.PowerSharpParameters["FirstWeekOfYear"] = value;
				}
			}

			// Token: 0x170023EE RID: 9198
			// (set) Token: 0x06004137 RID: 16695 RVA: 0x0006C5C0 File Offset: 0x0006A7C0
			public virtual HourIncrement TimeIncrement
			{
				set
				{
					base.PowerSharpParameters["TimeIncrement"] = value;
				}
			}

			// Token: 0x170023EF RID: 9199
			// (set) Token: 0x06004138 RID: 16696 RVA: 0x0006C5D8 File Offset: 0x0006A7D8
			public virtual bool RemindersEnabled
			{
				set
				{
					base.PowerSharpParameters["RemindersEnabled"] = value;
				}
			}

			// Token: 0x170023F0 RID: 9200
			// (set) Token: 0x06004139 RID: 16697 RVA: 0x0006C5F0 File Offset: 0x0006A7F0
			public virtual bool ReminderSoundEnabled
			{
				set
				{
					base.PowerSharpParameters["ReminderSoundEnabled"] = value;
				}
			}

			// Token: 0x170023F1 RID: 9201
			// (set) Token: 0x0600413A RID: 16698 RVA: 0x0006C608 File Offset: 0x0006A808
			public virtual TimeSpan DefaultReminderTime
			{
				set
				{
					base.PowerSharpParameters["DefaultReminderTime"] = value;
				}
			}

			// Token: 0x170023F2 RID: 9202
			// (set) Token: 0x0600413B RID: 16699 RVA: 0x0006C620 File Offset: 0x0006A820
			public virtual bool WeatherEnabled
			{
				set
				{
					base.PowerSharpParameters["WeatherEnabled"] = value;
				}
			}

			// Token: 0x170023F3 RID: 9203
			// (set) Token: 0x0600413C RID: 16700 RVA: 0x0006C638 File Offset: 0x0006A838
			public virtual WeatherTemperatureUnit WeatherUnit
			{
				set
				{
					base.PowerSharpParameters["WeatherUnit"] = value;
				}
			}

			// Token: 0x170023F4 RID: 9204
			// (set) Token: 0x0600413D RID: 16701 RVA: 0x0006C650 File Offset: 0x0006A850
			public virtual MultiValuedProperty<string> WeatherLocations
			{
				set
				{
					base.PowerSharpParameters["WeatherLocations"] = value;
				}
			}

			// Token: 0x170023F5 RID: 9205
			// (set) Token: 0x0600413E RID: 16702 RVA: 0x0006C663 File Offset: 0x0006A863
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170023F6 RID: 9206
			// (set) Token: 0x0600413F RID: 16703 RVA: 0x0006C67B File Offset: 0x0006A87B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170023F7 RID: 9207
			// (set) Token: 0x06004140 RID: 16704 RVA: 0x0006C693 File Offset: 0x0006A893
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170023F8 RID: 9208
			// (set) Token: 0x06004141 RID: 16705 RVA: 0x0006C6AB File Offset: 0x0006A8AB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170023F9 RID: 9209
			// (set) Token: 0x06004142 RID: 16706 RVA: 0x0006C6C3 File Offset: 0x0006A8C3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000481 RID: 1153
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170023FA RID: 9210
			// (set) Token: 0x06004144 RID: 16708 RVA: 0x0006C6E3 File Offset: 0x0006A8E3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170023FB RID: 9211
			// (set) Token: 0x06004145 RID: 16709 RVA: 0x0006C701 File Offset: 0x0006A901
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170023FC RID: 9212
			// (set) Token: 0x06004146 RID: 16710 RVA: 0x0006C714 File Offset: 0x0006A914
			public virtual DaysOfWeek WorkDays
			{
				set
				{
					base.PowerSharpParameters["WorkDays"] = value;
				}
			}

			// Token: 0x170023FD RID: 9213
			// (set) Token: 0x06004147 RID: 16711 RVA: 0x0006C72C File Offset: 0x0006A92C
			public virtual TimeSpan WorkingHoursStartTime
			{
				set
				{
					base.PowerSharpParameters["WorkingHoursStartTime"] = value;
				}
			}

			// Token: 0x170023FE RID: 9214
			// (set) Token: 0x06004148 RID: 16712 RVA: 0x0006C744 File Offset: 0x0006A944
			public virtual TimeSpan WorkingHoursEndTime
			{
				set
				{
					base.PowerSharpParameters["WorkingHoursEndTime"] = value;
				}
			}

			// Token: 0x170023FF RID: 9215
			// (set) Token: 0x06004149 RID: 16713 RVA: 0x0006C75C File Offset: 0x0006A95C
			public virtual ExTimeZoneValue WorkingHoursTimeZone
			{
				set
				{
					base.PowerSharpParameters["WorkingHoursTimeZone"] = value;
				}
			}

			// Token: 0x17002400 RID: 9216
			// (set) Token: 0x0600414A RID: 16714 RVA: 0x0006C76F File Offset: 0x0006A96F
			public virtual Microsoft.Exchange.Data.Storage.Management.DayOfWeek WeekStartDay
			{
				set
				{
					base.PowerSharpParameters["WeekStartDay"] = value;
				}
			}

			// Token: 0x17002401 RID: 9217
			// (set) Token: 0x0600414B RID: 16715 RVA: 0x0006C787 File Offset: 0x0006A987
			public virtual bool ShowWeekNumbers
			{
				set
				{
					base.PowerSharpParameters["ShowWeekNumbers"] = value;
				}
			}

			// Token: 0x17002402 RID: 9218
			// (set) Token: 0x0600414C RID: 16716 RVA: 0x0006C79F File Offset: 0x0006A99F
			public virtual FirstWeekRules FirstWeekOfYear
			{
				set
				{
					base.PowerSharpParameters["FirstWeekOfYear"] = value;
				}
			}

			// Token: 0x17002403 RID: 9219
			// (set) Token: 0x0600414D RID: 16717 RVA: 0x0006C7B7 File Offset: 0x0006A9B7
			public virtual HourIncrement TimeIncrement
			{
				set
				{
					base.PowerSharpParameters["TimeIncrement"] = value;
				}
			}

			// Token: 0x17002404 RID: 9220
			// (set) Token: 0x0600414E RID: 16718 RVA: 0x0006C7CF File Offset: 0x0006A9CF
			public virtual bool RemindersEnabled
			{
				set
				{
					base.PowerSharpParameters["RemindersEnabled"] = value;
				}
			}

			// Token: 0x17002405 RID: 9221
			// (set) Token: 0x0600414F RID: 16719 RVA: 0x0006C7E7 File Offset: 0x0006A9E7
			public virtual bool ReminderSoundEnabled
			{
				set
				{
					base.PowerSharpParameters["ReminderSoundEnabled"] = value;
				}
			}

			// Token: 0x17002406 RID: 9222
			// (set) Token: 0x06004150 RID: 16720 RVA: 0x0006C7FF File Offset: 0x0006A9FF
			public virtual TimeSpan DefaultReminderTime
			{
				set
				{
					base.PowerSharpParameters["DefaultReminderTime"] = value;
				}
			}

			// Token: 0x17002407 RID: 9223
			// (set) Token: 0x06004151 RID: 16721 RVA: 0x0006C817 File Offset: 0x0006AA17
			public virtual bool WeatherEnabled
			{
				set
				{
					base.PowerSharpParameters["WeatherEnabled"] = value;
				}
			}

			// Token: 0x17002408 RID: 9224
			// (set) Token: 0x06004152 RID: 16722 RVA: 0x0006C82F File Offset: 0x0006AA2F
			public virtual WeatherTemperatureUnit WeatherUnit
			{
				set
				{
					base.PowerSharpParameters["WeatherUnit"] = value;
				}
			}

			// Token: 0x17002409 RID: 9225
			// (set) Token: 0x06004153 RID: 16723 RVA: 0x0006C847 File Offset: 0x0006AA47
			public virtual MultiValuedProperty<string> WeatherLocations
			{
				set
				{
					base.PowerSharpParameters["WeatherLocations"] = value;
				}
			}

			// Token: 0x1700240A RID: 9226
			// (set) Token: 0x06004154 RID: 16724 RVA: 0x0006C85A File Offset: 0x0006AA5A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700240B RID: 9227
			// (set) Token: 0x06004155 RID: 16725 RVA: 0x0006C872 File Offset: 0x0006AA72
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700240C RID: 9228
			// (set) Token: 0x06004156 RID: 16726 RVA: 0x0006C88A File Offset: 0x0006AA8A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700240D RID: 9229
			// (set) Token: 0x06004157 RID: 16727 RVA: 0x0006C8A2 File Offset: 0x0006AAA2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700240E RID: 9230
			// (set) Token: 0x06004158 RID: 16728 RVA: 0x0006C8BA File Offset: 0x0006AABA
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
