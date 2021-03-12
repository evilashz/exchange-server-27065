using System;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x0200000D RID: 13
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class FindTenantResponse : ResponseBase
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002938 File Offset: 0x00000B38
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002940 File Offset: 0x00000B40
		[DataMember]
		public TenantInfo TenantInfo { get; set; }

		// Token: 0x0600003C RID: 60 RVA: 0x0000294C File Offset: 0x00000B4C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(300);
			try
			{
				stringBuilder.Append("<FindTenantResponse>");
				if (this.TenantInfo != null)
				{
					stringBuilder.Append(this.TenantInfo.ToString());
				}
				stringBuilder.Append(base.ToString());
				stringBuilder.Append("</FindTenantResponse>");
			}
			catch (Exception ex)
			{
				stringBuilder.Append(" ??TraceErr" + ex.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
