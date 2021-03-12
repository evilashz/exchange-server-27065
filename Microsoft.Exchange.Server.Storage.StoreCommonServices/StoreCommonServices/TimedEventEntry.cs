using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200014E RID: 334
	public class TimedEventEntry
	{
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00040D19 File Offset: 0x0003EF19
		public DateTime EventTime
		{
			get
			{
				return this.eventTime;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00040D21 File Offset: 0x0003EF21
		public long? UniqueId
		{
			get
			{
				return this.uniqueId;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00040D29 File Offset: 0x0003EF29
		public int? MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00040D31 File Offset: 0x0003EF31
		public Guid EventSource
		{
			get
			{
				return this.eventSource;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00040D39 File Offset: 0x0003EF39
		public int EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00040D41 File Offset: 0x0003EF41
		public TimedEventEntry.QualityOfServiceType QualityOfService
		{
			get
			{
				return this.qualityOfService;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00040D49 File Offset: 0x0003EF49
		public byte[] EventData
		{
			get
			{
				return this.eventData;
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00040D51 File Offset: 0x0003EF51
		internal TimedEventEntry(DateTime datetime, int? mailboxNumber, Guid eventSource, int eventType, byte[] eventData)
		{
			this.eventTime = datetime;
			this.uniqueId = null;
			this.mailboxNumber = mailboxNumber;
			this.eventSource = eventSource;
			this.eventType = eventType;
			this.eventData = eventData;
			this.qualityOfService = TimedEventEntry.QualityOfServiceType.BestEfforts;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00040D94 File Offset: 0x0003EF94
		internal TimedEventEntry(DateTime datetime, long uniqueId, int? mailboxNumber, Guid eventSource, int eventType, TimedEventEntry.QualityOfServiceType qos, byte[] eventData)
		{
			this.eventTime = datetime;
			this.uniqueId = new long?(uniqueId);
			this.mailboxNumber = mailboxNumber;
			this.eventSource = eventSource;
			this.eventType = eventType;
			this.qualityOfService = qos;
			this.eventData = eventData;
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00040DE4 File Offset: 0x0003EFE4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.Append("(EventTime:");
			stringBuilder.Append(this.EventTime);
			stringBuilder.Append(")(UniqueId:");
			stringBuilder.Append((this.UniqueId != null) ? this.UniqueId.Value.ToString() : "<null>");
			stringBuilder.Append(")(MailboxNumber:");
			stringBuilder.Append((this.MailboxNumber != null) ? this.MailboxNumber.Value.ToString() : "<null>");
			stringBuilder.Append(")(EventSource:");
			stringBuilder.Append(this.EventSource);
			stringBuilder.Append(")(EventType:");
			stringBuilder.Append(this.EventType);
			stringBuilder.Append(")(QoS:");
			stringBuilder.Append(this.QualityOfService);
			if (this.EventData != null)
			{
				stringBuilder.Append(")(EventData:[Length=");
				stringBuilder.Append(this.EventData.Length);
				stringBuilder.Append("]");
				stringBuilder.AppendAsString(this.EventData, 0, (this.EventData.Length > 64) ? 64 : this.EventData.Length);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x04000735 RID: 1845
		private readonly DateTime eventTime;

		// Token: 0x04000736 RID: 1846
		private readonly long? uniqueId;

		// Token: 0x04000737 RID: 1847
		private readonly int? mailboxNumber;

		// Token: 0x04000738 RID: 1848
		private readonly Guid eventSource;

		// Token: 0x04000739 RID: 1849
		private readonly int eventType;

		// Token: 0x0400073A RID: 1850
		private readonly TimedEventEntry.QualityOfServiceType qualityOfService;

		// Token: 0x0400073B RID: 1851
		private readonly byte[] eventData;

		// Token: 0x0200014F RID: 335
		public enum QualityOfServiceType
		{
			// Token: 0x0400073D RID: 1853
			BestEfforts
		}
	}
}
