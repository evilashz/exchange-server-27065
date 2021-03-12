using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000213 RID: 531
	public class RequestJobTimestampData
	{
		// Token: 0x06001B67 RID: 7015 RVA: 0x0003A38E File Offset: 0x0003858E
		public RequestJobTimestampData()
		{
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0003A396 File Offset: 0x00038596
		public RequestJobTimestampData(RequestJobTimestamp id, DateTime timestamp)
		{
			this.id = id;
			this.timestamp = timestamp;
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x0003A3AC File Offset: 0x000385AC
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x0003A3B4 File Offset: 0x000385B4
		[XmlIgnore]
		public RequestJobTimestamp Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x0003A3BD File Offset: 0x000385BD
		// (set) Token: 0x06001B6C RID: 7020 RVA: 0x0003A3C5 File Offset: 0x000385C5
		[XmlAttribute("Id")]
		public int IdInt
		{
			get
			{
				return (int)this.id;
			}
			set
			{
				this.id = (RequestJobTimestamp)value;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x0003A3CE File Offset: 0x000385CE
		// (set) Token: 0x06001B6E RID: 7022 RVA: 0x0003A3D6 File Offset: 0x000385D6
		[XmlAttribute("T")]
		public DateTime Timestamp
		{
			get
			{
				return this.timestamp;
			}
			set
			{
				this.timestamp = value;
			}
		}

		// Token: 0x04000BBA RID: 3002
		private RequestJobTimestamp id;

		// Token: 0x04000BBB RID: 3003
		private DateTime timestamp;
	}
}
