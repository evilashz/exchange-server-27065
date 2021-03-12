using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001C8 RID: 456
	internal class PusherLogEvent : ILogEvent
	{
		// Token: 0x06001014 RID: 4116 RVA: 0x0003DD9C File Offset: 0x0003BF9C
		public PusherLogEvent(PusherEventType eventType)
		{
			this.EventType = eventType;
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x0003DDAB File Offset: 0x0003BFAB
		public string EventId
		{
			get
			{
				return "Pusher";
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0003DDB2 File Offset: 0x0003BFB2
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x0003DDBA File Offset: 0x0003BFBA
		public PusherEventType EventType { get; set; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0003DDC3 File Offset: 0x0003BFC3
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x0003DDCB File Offset: 0x0003BFCB
		public string OriginationUserContextKey { get; set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x0003DDD4 File Offset: 0x0003BFD4
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x0003DDDC File Offset: 0x0003BFDC
		public int DestinationCount { get; set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x0003DDE5 File Offset: 0x0003BFE5
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x0003DDED File Offset: 0x0003BFED
		public string Destination { get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x0003DDF6 File Offset: 0x0003BFF6
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x0003DDFE File Offset: 0x0003BFFE
		public int PayloadCount { get; set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0003DE07 File Offset: 0x0003C007
		// (set) Token: 0x06001021 RID: 4129 RVA: 0x0003DE0F File Offset: 0x0003C00F
		public bool OverLimit { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x0003DE18 File Offset: 0x0003C018
		// (set) Token: 0x06001023 RID: 4131 RVA: 0x0003DE20 File Offset: 0x0003C020
		public int InTransitCount { get; set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0003DE29 File Offset: 0x0003C029
		// (set) Token: 0x06001025 RID: 4133 RVA: 0x0003DE31 File Offset: 0x0003C031
		public Exception HandledException { get; set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0003DE3A File Offset: 0x0003C03A
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x0003DE42 File Offset: 0x0003C042
		public int ThreadId { get; set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0003DE4B File Offset: 0x0003C04B
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x0003DE53 File Offset: 0x0003C053
		public int TaskCount { get; set; }

		// Token: 0x0600102A RID: 4138 RVA: 0x0003DE5C File Offset: 0x0003C05C
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			ICollection<KeyValuePair<string, object>> collection = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("ET", this.EventType.ToString())
			};
			switch (this.EventType)
			{
			case PusherEventType.Distribute:
				collection.Add(new KeyValuePair<string, object>("OCK", this.OriginationUserContextKey));
				collection.Add(new KeyValuePair<string, object>("DC", this.DestinationCount));
				collection.Add(new KeyValuePair<string, object>("PC", this.PayloadCount));
				break;
			case PusherEventType.Push:
				collection.Add(new KeyValuePair<string, object>("D", this.Destination));
				collection.Add(new KeyValuePair<string, object>("PC", this.PayloadCount));
				break;
			case PusherEventType.PushFailed:
				collection.Add(new KeyValuePair<string, object>("D", this.Destination));
				this.AddExceptionData(collection);
				break;
			case PusherEventType.ConcurrentLimit:
				collection.Add(new KeyValuePair<string, object>("OL", this.OverLimit.ToString()));
				collection.Add(new KeyValuePair<string, object>("ITC", this.InTransitCount));
				break;
			case PusherEventType.PusherThreadStart:
				collection.Add(new KeyValuePair<string, object>("TI", this.ThreadId));
				break;
			case PusherEventType.PusherThreadCleanup:
				collection.Add(new KeyValuePair<string, object>("TI", this.ThreadId));
				collection.Add(new KeyValuePair<string, object>("TC", this.TaskCount));
				break;
			case PusherEventType.PusherThreadEnd:
				collection.Add(new KeyValuePair<string, object>("TI", this.ThreadId));
				this.AddExceptionData(collection);
				break;
			}
			return collection;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0003E01C File Offset: 0x0003C21C
		private void AddExceptionData(ICollection<KeyValuePair<string, object>> eventData)
		{
			if (this.HandledException != null)
			{
				eventData.Add(new KeyValuePair<string, object>("Ex", this.HandledException.Message));
				eventData.Add(new KeyValuePair<string, object>("ExS", this.HandledException.StackTrace));
				Exception innerException = this.HandledException.InnerException;
				int num = 1;
				while (innerException != null && num <= 10)
				{
					eventData.Add(new KeyValuePair<string, object>("Ex" + num, innerException.Message ?? "Not specified"));
					eventData.Add(new KeyValuePair<string, object>("ExS" + num, innerException.StackTrace ?? "Not specified"));
					innerException = innerException.InnerException;
					num++;
				}
			}
		}
	}
}
