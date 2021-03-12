using System;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x02000009 RID: 9
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class FindDomainResponse : ResponseBase
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000024D8 File Offset: 0x000006D8
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000024E0 File Offset: 0x000006E0
		[DataMember]
		public DomainInfo DomainInfo { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000024E9 File Offset: 0x000006E9
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000024F1 File Offset: 0x000006F1
		[DataMember]
		public TenantInfo TenantInfo { get; set; }

		// Token: 0x06000024 RID: 36 RVA: 0x000024FC File Offset: 0x000006FC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(300);
			try
			{
				stringBuilder.Append("<FindDomainResponse>");
				if (this.DomainInfo != null)
				{
					stringBuilder.Append(this.DomainInfo.ToString());
				}
				if (this.TenantInfo != null)
				{
					stringBuilder.Append(this.TenantInfo.ToString());
				}
				stringBuilder.AppendFormat("TransactionID={0},Diagnostics={1}", base.TransactionID, base.Diagnostics);
				stringBuilder.Append("</FindDomainResponse>");
			}
			catch (Exception ex)
			{
				stringBuilder.Append(" ??TraceErr" + ex.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
