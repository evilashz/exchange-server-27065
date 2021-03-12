using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020001E8 RID: 488
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public class MonitoringEvent
	{
		// Token: 0x06001183 RID: 4483 RVA: 0x00035A20 File Offset: 0x00033C20
		internal MonitoringEvent(string eventSource, int eventIdentifier, EventTypeEnumeration eventType, string eventMessage) : this(eventSource, eventIdentifier, eventType, eventMessage, null)
		{
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00035A30 File Offset: 0x00033C30
		internal MonitoringEvent(string eventSource, int eventIdentifier, EventTypeEnumeration eventType, string eventMessage, string eventInstanceName)
		{
			if (string.IsNullOrEmpty(eventSource))
			{
				throw new ArgumentNullException("eventSource");
			}
			if (string.IsNullOrEmpty(eventMessage))
			{
				throw new ArgumentNullException("eventMessage");
			}
			this.eventSource = eventSource;
			this.eventIdentifier = eventIdentifier;
			this.eventType = eventType;
			this.eventMessage = eventMessage;
			this.eventInstanceName = eventInstanceName;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x00035A8F File Offset: 0x00033C8F
		public string EventSource
		{
			get
			{
				return this.eventSource;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00035A97 File Offset: 0x00033C97
		public int EventIdentifier
		{
			get
			{
				return this.eventIdentifier;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x00035A9F File Offset: 0x00033C9F
		public EventTypeEnumeration EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00035AA7 File Offset: 0x00033CA7
		public string EventMessage
		{
			get
			{
				return this.eventMessage;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x00035AAF File Offset: 0x00033CAF
		public string EventInstanceName
		{
			get
			{
				return this.eventInstanceName;
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00035AB8 File Offset: 0x00033CB8
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.EventInstanceName))
			{
				return Strings.MonitoringEventString(this.EventSource, this.EventIdentifier, Enum.GetName(this.EventType.GetType(), this.EventType), this.EventMessage);
			}
			return Strings.MonitoringEventStringWithInstanceName(this.EventSource, this.EventIdentifier, Enum.GetName(this.EventType.GetType(), this.EventType), this.EventMessage, this.EventInstanceName);
		}

		// Token: 0x040003EF RID: 1007
		public const string EventSourcePrefix = "MSExchange Monitoring ";

		// Token: 0x040003F0 RID: 1008
		private string eventSource;

		// Token: 0x040003F1 RID: 1009
		private int eventIdentifier;

		// Token: 0x040003F2 RID: 1010
		private EventTypeEnumeration eventType;

		// Token: 0x040003F3 RID: 1011
		private string eventMessage;

		// Token: 0x040003F4 RID: 1012
		private string eventInstanceName;
	}
}
