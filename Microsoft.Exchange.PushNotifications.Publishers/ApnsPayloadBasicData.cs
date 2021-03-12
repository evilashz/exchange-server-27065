using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000036 RID: 54
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal sealed class ApnsPayloadBasicData
	{
		// Token: 0x06000213 RID: 531 RVA: 0x000082C4 File Offset: 0x000064C4
		public ApnsPayloadBasicData(int? badge = null, string sound = null, ApnsAlert alert = null, int contentAvailable = 0)
		{
			this.Badge = badge;
			this.Sound = sound;
			this.Alert = alert;
			this.ContentAvailable = contentAvailable;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000082E9 File Offset: 0x000064E9
		// (set) Token: 0x06000215 RID: 533 RVA: 0x000082F1 File Offset: 0x000064F1
		[DataMember(Name = "badge", EmitDefaultValue = false, Order = 1)]
		public int? Badge { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000082FA File Offset: 0x000064FA
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00008302 File Offset: 0x00006502
		[DataMember(Name = "sound", EmitDefaultValue = false, Order = 2)]
		public string Sound { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000830B File Offset: 0x0000650B
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00008313 File Offset: 0x00006513
		[DataMember(Name = "alert", EmitDefaultValue = false, Order = 3)]
		public ApnsAlert Alert { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000831C File Offset: 0x0000651C
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00008324 File Offset: 0x00006524
		[DataMember(Name = "content-available", EmitDefaultValue = false, Order = 4)]
		public int ContentAvailable { get; private set; }

		// Token: 0x0600021C RID: 540 RVA: 0x0000832D File Offset: 0x0000652D
		public string ToJson()
		{
			return JsonConverter.Serialize<ApnsPayloadBasicData>(this, null);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008338 File Offset: 0x00006538
		public override string ToString()
		{
			if (this.toStringCache == null)
			{
				this.toStringCache = string.Format("{{badge:{0}; sound:{1}; alert:{2}; content-available:{3}}}", new object[]
				{
					this.Badge.ToNullableString<int>(),
					this.Sound.ToNullableString(),
					this.Alert.ToNullableString(null),
					this.ContentAvailable
				});
			}
			return this.toStringCache;
		}

		// Token: 0x040000CF RID: 207
		private string toStringCache;
	}
}
