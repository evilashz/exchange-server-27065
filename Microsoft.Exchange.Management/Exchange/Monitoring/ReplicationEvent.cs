using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200055E RID: 1374
	internal class ReplicationEvent
	{
		// Token: 0x060030D6 RID: 12502 RVA: 0x000C5C15 File Offset: 0x000C3E15
		public ReplicationEvent(ReplicationEventBaseInfo eventInfo)
		{
			this.m_EventInfo = eventInfo;
			this.m_EventMessages = new List<string>();
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x060030D7 RID: 12503 RVA: 0x000C5C2F File Offset: 0x000C3E2F
		public bool IsMomEvent
		{
			get
			{
				return this.m_EventInfo.EventType == ReplicationEventType.MOM;
			}
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000C5C3F File Offset: 0x000C3E3F
		public void AddEvent(string eventMessage)
		{
			this.m_EventMessages.Add(eventMessage);
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000C5C50 File Offset: 0x000C3E50
		public string GetEventMessage(bool appendMachineInfo)
		{
			if (this.m_EventMessages.Count == 0)
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug((long)this.GetHashCode(), "GetEventMessageForMom(): No events were logged. m_EventMessages.Count = 0");
				return null;
			}
			if (!this.m_EventInfo.ShouldBeRolledUp && this.IsMomEvent)
			{
				return this.m_EventMessages[0];
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (this.m_EventMessages.Count == 1)
			{
				stringBuilder.AppendLine(this.m_EventMessages[0]);
			}
			else
			{
				foreach (string value in this.m_EventMessages)
				{
					stringBuilder.AppendLine(value);
				}
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			if (!appendMachineInfo)
			{
				StringBuilder stringBuilder3 = stringBuilder2;
				string format = "{0}{1}{2}";
				LocalizedString? baseEventMessage = this.m_EventInfo.BaseEventMessage;
				stringBuilder3.AppendFormat(format, (baseEventMessage != null) ? baseEventMessage.GetValueOrDefault() : string.Empty, Environment.NewLine, stringBuilder.ToString());
			}
			else
			{
				StringBuilder stringBuilder4 = stringBuilder2;
				string format2 = "{0} {1}{2}{3}";
				object[] array = new object[4];
				object[] array2 = array;
				int num = 0;
				LocalizedString? baseEventMessage2 = this.m_EventInfo.BaseEventMessage;
				array2[num] = ((baseEventMessage2 != null) ? baseEventMessage2.GetValueOrDefault() : string.Empty);
				array[1] = TestReplicationHealth.GetMachineConfigurationString(ReplicationCheckGlobals.ServerConfiguration);
				array[2] = Environment.NewLine;
				array[3] = stringBuilder.ToString();
				stringBuilder4.AppendFormat(format2, array);
			}
			return stringBuilder2.ToString();
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000C5DD0 File Offset: 0x000C3FD0
		public MonitoringEvent ConvertToMonitoringEvent(string momEventSource)
		{
			return this.ConvertToMonitoringEvent(momEventSource, false);
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000C5DDC File Offset: 0x000C3FDC
		public MonitoringEvent ConvertToMonitoringEvent(string momEventSource, bool appendMachineInfo)
		{
			if (!this.IsMomEvent)
			{
				return null;
			}
			string eventMessage = this.GetEventMessage(appendMachineInfo);
			if (eventMessage == null)
			{
				return null;
			}
			MomEventInfo momEventInfo = (MomEventInfo)this.m_EventInfo;
			return new MonitoringEvent(momEventSource, momEventInfo.MomEventId, momEventInfo.MomEventType, eventMessage);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000C5E24 File Offset: 0x000C4024
		public void PublishToApplicationEventLog(bool appendMachineInfo, string instanceIdentity)
		{
			if (this.m_EventInfo.EventType != ReplicationEventType.AppLog)
			{
				return;
			}
			string eventMessage = this.GetEventMessage(appendMachineInfo);
			ApplicationEventInfo applicationEventInfo = (ApplicationEventInfo)this.m_EventInfo;
			if (string.IsNullOrEmpty(eventMessage))
			{
				applicationEventInfo.EventTuple.LogEvent(instanceIdentity, new object[]
				{
					instanceIdentity
				});
				return;
			}
			applicationEventInfo.EventTuple.LogEvent(instanceIdentity, new object[]
			{
				instanceIdentity,
				eventMessage
			});
		}

		// Token: 0x040022AD RID: 8877
		private const string CompleteEventMessageFormatString = "{0}{1}{2}";

		// Token: 0x040022AE RID: 8878
		private const string CompleteEventMachineInfoFormatString = "{0} {1}{2}{3}";

		// Token: 0x040022AF RID: 8879
		private readonly ReplicationEventBaseInfo m_EventInfo;

		// Token: 0x040022B0 RID: 8880
		private List<string> m_EventMessages;
	}
}
