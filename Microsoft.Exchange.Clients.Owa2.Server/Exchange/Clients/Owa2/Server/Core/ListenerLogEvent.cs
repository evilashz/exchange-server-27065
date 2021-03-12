using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001C5 RID: 453
	internal class ListenerLogEvent : ILogEvent
	{
		// Token: 0x06000FFD RID: 4093 RVA: 0x0003D714 File Offset: 0x0003B914
		public ListenerLogEvent(ListenerEventType eventType)
		{
			this.EventType = eventType;
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x0003D723 File Offset: 0x0003B923
		public string EventId
		{
			get
			{
				return "Listener";
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x0003D72A File Offset: 0x0003B92A
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x0003D732 File Offset: 0x0003B932
		public ListenerEventType EventType { get; set; }

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x0003D73B File Offset: 0x0003B93B
		// (set) Token: 0x06001002 RID: 4098 RVA: 0x0003D743 File Offset: 0x0003B943
		public string OriginationServer { get; set; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x0003D74C File Offset: 0x0003B94C
		// (set) Token: 0x06001004 RID: 4100 RVA: 0x0003D754 File Offset: 0x0003B954
		public Exception HandledException { get; set; }

		// Token: 0x06001005 RID: 4101 RVA: 0x0003D760 File Offset: 0x0003B960
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			ICollection<KeyValuePair<string, object>> collection = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("ET", this.EventType.ToString())
			};
			switch (this.EventType)
			{
			case ListenerEventType.Listen:
				collection.Add(new KeyValuePair<string, object>("OS", this.OriginationServer));
				break;
			case ListenerEventType.ListenFailed:
				collection.Add(new KeyValuePair<string, object>("OS", this.OriginationServer));
				if (this.HandledException != null)
				{
					collection.Add(new KeyValuePair<string, object>("Ex", this.HandledException.Message));
					collection.Add(new KeyValuePair<string, object>("ExS", this.HandledException.StackTrace));
					Exception innerException = this.HandledException.InnerException;
					int num = 1;
					while (innerException != null && num <= 10)
					{
						collection.Add(new KeyValuePair<string, object>("Ex" + num, innerException.Message ?? "Not specified"));
						collection.Add(new KeyValuePair<string, object>("ExS" + num, innerException.StackTrace ?? "Not specified"));
						innerException = innerException.InnerException;
						num++;
					}
				}
				break;
			}
			return collection;
		}
	}
}
