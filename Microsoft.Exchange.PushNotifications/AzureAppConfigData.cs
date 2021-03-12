using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000010 RID: 16
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class AzureAppConfigData : BasicDataContract
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003024 File Offset: 0x00001224
		public AzureAppConfigData(string appId, string serializedToken, string partition)
		{
			this.AppId = appId;
			this.SerializedToken = serializedToken;
			this.Partition = partition;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003041 File Offset: 0x00001241
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003049 File Offset: 0x00001249
		[DataMember(Name = "appId", EmitDefaultValue = false)]
		public string AppId { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003052 File Offset: 0x00001252
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000305A File Offset: 0x0000125A
		[DataMember(Name = "serializedToken", EmitDefaultValue = false)]
		public string SerializedToken { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003063 File Offset: 0x00001263
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000306B File Offset: 0x0000126B
		[DataMember(Name = "partition", EmitDefaultValue = false)]
		public string Partition { get; private set; }

		// Token: 0x06000077 RID: 119 RVA: 0x00003074 File Offset: 0x00001274
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("appId:").Append(this.AppId.ToNullableString()).Append("; ");
			sb.Append("serializedToken:").Append(this.SerializedToken.ToNullableString()).Append("; ");
			sb.Append("partition:").Append(this.Partition.ToNullableString()).Append("; ");
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000030FA File Offset: 0x000012FA
		protected override void InternalValidate(List<LocalizedString> errors)
		{
			base.InternalValidate(errors);
			if (string.IsNullOrWhiteSpace(this.AppId))
			{
				errors.Add(Strings.InvalidAppId);
			}
			if (string.IsNullOrWhiteSpace(this.SerializedToken))
			{
				errors.Add(Strings.InvalidSerializedToken);
			}
		}

		// Token: 0x0400002A RID: 42
		public const int DefaultTokenExpirationTimeInSeconds = 93600;
	}
}
