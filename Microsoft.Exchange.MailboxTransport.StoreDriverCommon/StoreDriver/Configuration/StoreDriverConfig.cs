using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriver;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver.Configuration
{
	// Token: 0x02000029 RID: 41
	internal sealed class StoreDriverConfig
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00007744 File Offset: 0x00005944
		private StoreDriverConfig()
		{
			StoreDriverParameters storeDriverParameters = new StoreDriverParameters();
			storeDriverParameters.Load(new Dictionary<string, StoreDriverParameterHandler>
			{
				{
					"AlwaysSetReminderOnAppointment",
					new StoreDriverParameterHandler(this.StoreDriverParameterParser)
				},
				{
					"IsAutoAcceptForGroupAndSelfForwardedEventEnabled",
					new StoreDriverParameterHandler(this.StoreDriverGroupMailboxProcessFlagParser)
				},
				{
					"IsGroupEscalationAgentEnabled",
					new StoreDriverParameterHandler(this.StoreDriverGroupEscalationAgentFlagParser)
				},
				{
					"MeetingHijackPreventionEnabled",
					new StoreDriverParameterHandler(this.StoreDriverMeetingHijackPreventionEnabledFlagParser)
				}
			});
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000077E4 File Offset: 0x000059E4
		internal static StoreDriverConfig Instance
		{
			get
			{
				if (StoreDriverConfig.instance == null)
				{
					lock (StoreDriverConfig.syncRoot)
					{
						if (StoreDriverConfig.instance == null)
						{
							StoreDriverConfig.instance = new StoreDriverConfig();
						}
					}
				}
				return StoreDriverConfig.instance;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000783C File Offset: 0x00005A3C
		internal bool AlwaysSetReminderOnAppointment
		{
			get
			{
				return this.alwaysSetReminderOnAppointment;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00007844 File Offset: 0x00005A44
		internal bool IsAutoAcceptForGroupAndSelfForwardedEventEnabled
		{
			get
			{
				return this.isAutoAcceptForGroupAndSelfForwardedEventEnabled;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000784C File Offset: 0x00005A4C
		internal bool IsGroupEscalationAgentEnabled
		{
			get
			{
				return this.isGroupEscalationAgentEnabled;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00007854 File Offset: 0x00005A54
		internal bool MeetingHijackPreventionEnabled
		{
			get
			{
				return this.meetingHijackPreventionEnabled;
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000785C File Offset: 0x00005A5C
		private void StoreDriverParameterParser(string key, string value)
		{
			if (string.Compare("AlwaysSetReminderOnAppointment", key, StringComparison.OrdinalIgnoreCase) == 0 && !bool.TryParse(value, out this.alwaysSetReminderOnAppointment))
			{
				this.alwaysSetReminderOnAppointment = true;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007881 File Offset: 0x00005A81
		private void StoreDriverGroupMailboxProcessFlagParser(string key, string value)
		{
			if (string.Compare("IsAutoAcceptForGroupAndSelfForwardedEventEnabled", key, StringComparison.OrdinalIgnoreCase) == 0 && !bool.TryParse(value, out this.isAutoAcceptForGroupAndSelfForwardedEventEnabled))
			{
				this.isAutoAcceptForGroupAndSelfForwardedEventEnabled = true;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000078A6 File Offset: 0x00005AA6
		private void StoreDriverGroupEscalationAgentFlagParser(string key, string value)
		{
			if (string.Compare("IsGroupEscalationAgentEnabled", key, StringComparison.OrdinalIgnoreCase) == 0 && !bool.TryParse(value, out this.isGroupEscalationAgentEnabled))
			{
				this.isGroupEscalationAgentEnabled = true;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000078CB File Offset: 0x00005ACB
		private void StoreDriverMeetingHijackPreventionEnabledFlagParser(string key, string value)
		{
			if (string.Compare("MeetingHijackPreventionEnabled", key, StringComparison.OrdinalIgnoreCase) == 0 && !bool.TryParse(value, out this.meetingHijackPreventionEnabled))
			{
				this.meetingHijackPreventionEnabled = true;
			}
		}

		// Token: 0x04000089 RID: 137
		private const string IsAutoAcceptForGroupAndSelfForwardedEventEnabledKey = "IsAutoAcceptForGroupAndSelfForwardedEventEnabled";

		// Token: 0x0400008A RID: 138
		private const string AlwaysSetReminderOnAppointmentKey = "AlwaysSetReminderOnAppointment";

		// Token: 0x0400008B RID: 139
		private const string IsGroupEscalationAgentEnabledKey = "IsGroupEscalationAgentEnabled";

		// Token: 0x0400008C RID: 140
		private const string MeetingHijackPreventionEnabledKey = "MeetingHijackPreventionEnabled";

		// Token: 0x0400008D RID: 141
		private static readonly Trace diag = ExTraceGlobals.StoreDriverTracer;

		// Token: 0x0400008E RID: 142
		private static object syncRoot = new object();

		// Token: 0x0400008F RID: 143
		private static StoreDriverConfig instance;

		// Token: 0x04000090 RID: 144
		private bool alwaysSetReminderOnAppointment = true;

		// Token: 0x04000091 RID: 145
		private bool isAutoAcceptForGroupAndSelfForwardedEventEnabled = true;

		// Token: 0x04000092 RID: 146
		private bool isGroupEscalationAgentEnabled = true;

		// Token: 0x04000093 RID: 147
		private bool meetingHijackPreventionEnabled = true;
	}
}
