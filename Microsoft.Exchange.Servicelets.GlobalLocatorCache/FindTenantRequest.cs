using System;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x0200000C RID: 12
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class FindTenantRequest
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002880 File Offset: 0x00000A80
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002888 File Offset: 0x00000A88
		[DataMember(IsRequired = true)]
		public TenantQuery Tenant { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002891 File Offset: 0x00000A91
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002899 File Offset: 0x00000A99
		[DataMember(IsRequired = false)]
		public int ReadFlag { get; set; }

		// Token: 0x06000038 RID: 56 RVA: 0x000028A4 File Offset: 0x00000AA4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(300);
			try
			{
				stringBuilder.Append("<FindTenantRequest>");
				stringBuilder.Append(this.Tenant.ToString());
				stringBuilder.AppendFormat("ReadFlag={0}", this.ReadFlag);
				stringBuilder.Append("</FindTenantRequest>");
			}
			catch (Exception ex)
			{
				stringBuilder.Append(" ??TraceErr" + ex.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
