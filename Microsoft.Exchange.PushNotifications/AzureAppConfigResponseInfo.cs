using System;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000013 RID: 19
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class AzureAppConfigResponseInfo : BasicDataContract
	{
		// Token: 0x06000084 RID: 132 RVA: 0x000033CF File Offset: 0x000015CF
		public AzureAppConfigResponseInfo(AzureAppConfigData[] data, string hubName)
		{
			this.AppData = data;
			this.HubName = hubName;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000033E5 File Offset: 0x000015E5
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000033ED File Offset: 0x000015ED
		[DataMember(Name = "appData", EmitDefaultValue = false)]
		public AzureAppConfigData[] AppData { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000033F6 File Offset: 0x000015F6
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000033FE File Offset: 0x000015FE
		[DataMember(Name = "hubName", EmitDefaultValue = false)]
		public string HubName { get; private set; }

		// Token: 0x06000089 RID: 137 RVA: 0x00003407 File Offset: 0x00001607
		public override string ToJson()
		{
			return JsonConverter.Serialize<AzureAppConfigResponseInfo>(this, null);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003418 File Offset: 0x00001618
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("hubName:").Append(this.HubName.ToNullableString()).Append("; ");
			sb.Append("appData:").Append(this.AppData.ToNullableString((AzureAppConfigData x) => x.ToFullString())).Append("; ");
		}
	}
}
