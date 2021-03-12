using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Hygiene.Data.MessageTrace;

namespace Microsoft.Exchange.Hygiene.Data.SystemProbe
{
	// Token: 0x0200022F RID: 559
	internal sealed class SystemProbeData
	{
		// Token: 0x060016A3 RID: 5795 RVA: 0x00045CE0 File Offset: 0x00043EE0
		public static List<Guid> GetProbes(DateTimeOffset start, DateTimeOffset end)
		{
			MessageTraceSession messageTraceSession = new MessageTraceSession();
			MessageTrace[] array = messageTraceSession.FindPagedTrace(SystemProbeConstants.TenantID, start.DateTime, end.DateTime, null, null, null, null, null, 0, -1);
			List<Guid> list = new List<Guid>(array.Length);
			foreach (MessageTrace messageTrace in array)
			{
				list.Add(messageTrace.ExMessageId);
			}
			return list;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00045D64 File Offset: 0x00043F64
		public static List<object> GetProbeEvents(Guid probe)
		{
			MessageTraceSession messageTraceSession = new MessageTraceSession();
			MessageTrace messageTrace = messageTraceSession.Read(SystemProbeConstants.TenantID, probe);
			List<object> events = new List<object>();
			if (messageTrace != null)
			{
				messageTrace.Events.ForEach(delegate(MessageEvent e)
				{
					events.Add(SystemProbeData.CreateEventRecord(e));
				});
			}
			return events;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00045DBC File Offset: 0x00043FBC
		private static SystemProbeEvent CreateEventRecord(MessageEvent messageEvent)
		{
			return new SystemProbeEvent
			{
				MessageId = messageEvent.ExMessageId,
				EventId = messageEvent.EventId,
				TimeStamp = messageEvent.TimeStamp,
				ServerHostName = messageEvent.GetExtendedProperty("SysProbe", "Server").PropertyValueString,
				ComponentName = messageEvent.GetExtendedProperty("SysProbe", "Component").PropertyValueString,
				EventStatus = (SystemProbeEvent.Status)Enum.Parse(typeof(SystemProbeEvent.Status), messageEvent.GetExtendedProperty("SysProbe", "Status").PropertyValueString, true),
				EventMessage = messageEvent.GetExtendedProperty("SysProbe", "MessageBlob").PropertyValueBlob.Value
			};
		}
	}
}
