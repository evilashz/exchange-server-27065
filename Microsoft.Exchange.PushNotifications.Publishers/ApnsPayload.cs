using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000035 RID: 53
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class ApnsPayload
	{
		// Token: 0x06000208 RID: 520 RVA: 0x000081EB File Offset: 0x000063EB
		public ApnsPayload(ApnsPayloadBasicData aps, string storeObjectId = null, string backgroundSyncType = null)
		{
			this.Aps = aps;
			this.StoreObjectId = storeObjectId;
			this.BackgroundSyncType = backgroundSyncType;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00008208 File Offset: 0x00006408
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00008210 File Offset: 0x00006410
		[DataMember(Name = "aps", EmitDefaultValue = false, IsRequired = true, Order = 1)]
		public ApnsPayloadBasicData Aps { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00008219 File Offset: 0x00006419
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00008221 File Offset: 0x00006421
		[DataMember(Name = "o", EmitDefaultValue = false, IsRequired = false, Order = 2)]
		public string StoreObjectId { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000822A File Offset: 0x0000642A
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00008232 File Offset: 0x00006432
		[DataMember(Name = "t", EmitDefaultValue = false, IsRequired = false, Order = 3)]
		public string BackgroundSyncType { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000823B File Offset: 0x0000643B
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00008243 File Offset: 0x00006443
		[DataMember(Name = "n", EmitDefaultValue = false, IsRequired = false, Order = 4)]
		public string NotificationId { get; internal set; }

		// Token: 0x06000211 RID: 529 RVA: 0x0000824C File Offset: 0x0000644C
		public string ToJson()
		{
			return JsonConverter.Serialize<ApnsPayload>(this, null);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008258 File Offset: 0x00006458
		public override string ToString()
		{
			if (this.toStringCache == null)
			{
				this.toStringCache = string.Format("{{aps:{0}, o:{1}, t:{2}, n:{3}}}", new object[]
				{
					this.Aps.ToNullableString(null),
					this.StoreObjectId.ToNullableString(),
					this.BackgroundSyncType.ToNullableString(),
					this.NotificationId.ToNullableString()
				});
			}
			return this.toStringCache;
		}

		// Token: 0x040000CA RID: 202
		private string toStringCache;
	}
}
