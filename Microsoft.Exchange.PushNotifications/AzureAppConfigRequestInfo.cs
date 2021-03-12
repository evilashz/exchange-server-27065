using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000014 RID: 20
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class AzureAppConfigRequestInfo : BasicNotification
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00003495 File Offset: 0x00001695
		public AzureAppConfigRequestInfo(string[] appIds) : base(null)
		{
			this.AppIds = appIds;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000034A5 File Offset: 0x000016A5
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000034AD File Offset: 0x000016AD
		[DataMember(Name = "appIds", EmitDefaultValue = false)]
		public string[] AppIds { get; private set; }

		// Token: 0x0600008F RID: 143 RVA: 0x000034B6 File Offset: 0x000016B6
		public override string ToJson()
		{
			return JsonConverter.Serialize<AzureAppConfigRequestInfo>(this, null);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000034BF File Offset: 0x000016BF
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("appIds:").Append(this.AppIds.ToNullableString(null)).Append("; ");
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000034EF File Offset: 0x000016EF
		protected override void InternalValidate(List<LocalizedString> errors)
		{
			base.InternalValidate(errors);
			if (this.AppIds == null || this.AppIds.Length == 0)
			{
				errors.Add(Strings.InvalidListOfAppIds);
			}
		}
	}
}
