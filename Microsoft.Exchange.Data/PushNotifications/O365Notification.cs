using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.Data.PushNotifications
{
	// Token: 0x0200026C RID: 620
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class O365Notification
	{
		// Token: 0x060014BA RID: 5306 RVA: 0x00042AA4 File Offset: 0x00040CA4
		public O365Notification(string channelId, string data)
		{
			this.ChannelId = channelId;
			this.Data = data;
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x00042ABA File Offset: 0x00040CBA
		// (set) Token: 0x060014BC RID: 5308 RVA: 0x00042AC2 File Offset: 0x00040CC2
		[DataMember(Name = "channelId", EmitDefaultValue = false, IsRequired = true)]
		public string ChannelId { get; private set; }

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00042ACB File Offset: 0x00040CCB
		// (set) Token: 0x060014BE RID: 5310 RVA: 0x00042AD3 File Offset: 0x00040CD3
		[DataMember(Name = "data", EmitDefaultValue = false, IsRequired = true)]
		public string Data { get; private set; }

		// Token: 0x060014BF RID: 5311 RVA: 0x00042ADC File Offset: 0x00040CDC
		public string ToJson()
		{
			return JsonConverter.Serialize<O365Notification>(this, null);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00042AE5 File Offset: 0x00040CE5
		public override string ToString()
		{
			return string.Format("channelId:{0}; data:{1}", this.ChannelId ?? "<null>", this.Data ?? "<null>");
		}

		// Token: 0x04000C22 RID: 3106
		public const string MonitoringChannelId = "::AE82E53440744F2798C276818CE8BD5C::";
	}
}
