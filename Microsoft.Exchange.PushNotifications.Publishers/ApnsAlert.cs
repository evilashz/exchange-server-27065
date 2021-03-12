using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000019 RID: 25
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class ApnsAlert
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00004117 File Offset: 0x00002317
		public ApnsAlert(string body = null, string launchImage = null, string actionLocKey = null, string locKey = null, string[] locArgs = null)
		{
			this.Body = body;
			this.LaunchImage = launchImage;
			this.ActionLocKey = actionLocKey;
			this.LocKey = locKey;
			this.LocArgs = locArgs;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004144 File Offset: 0x00002344
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x0000414C File Offset: 0x0000234C
		[DataMember(Name = "body", EmitDefaultValue = false, Order = 1)]
		public string Body { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004155 File Offset: 0x00002355
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x0000415D File Offset: 0x0000235D
		[DataMember(Name = "launch-image", EmitDefaultValue = false, Order = 2)]
		public string LaunchImage { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004166 File Offset: 0x00002366
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x0000416E File Offset: 0x0000236E
		[DataMember(Name = "action-loc-key", EmitDefaultValue = false, Order = 3)]
		public string ActionLocKey { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004177 File Offset: 0x00002377
		// (set) Token: 0x060000DB RID: 219 RVA: 0x0000417F File Offset: 0x0000237F
		[DataMember(Name = "loc-key", EmitDefaultValue = false, Order = 4)]
		public string LocKey { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004188 File Offset: 0x00002388
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004190 File Offset: 0x00002390
		[DataMember(Name = "loc-args", EmitDefaultValue = false, Order = 5)]
		public string[] LocArgs { get; private set; }

		// Token: 0x060000DE RID: 222 RVA: 0x00004199 File Offset: 0x00002399
		public string ToJson()
		{
			return JsonConverter.Serialize<ApnsAlert>(this, null);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000041A4 File Offset: 0x000023A4
		public override string ToString()
		{
			if (this.toStringCache == null)
			{
				this.toStringCache = string.Format("{{body:{0}; launch-image:{1}; action-loc-key:{2}; loc-key:{3}; loc-args:{4}}}", new object[]
				{
					this.Body.ToNullableString(),
					this.LaunchImage.ToNullableString(),
					this.ActionLocKey.ToNullableString(),
					this.LocKey.ToNullableString(),
					this.LocArgs.ToNullableString(null)
				});
			}
			return this.toStringCache;
		}

		// Token: 0x0400003D RID: 61
		private string toStringCache;
	}
}
