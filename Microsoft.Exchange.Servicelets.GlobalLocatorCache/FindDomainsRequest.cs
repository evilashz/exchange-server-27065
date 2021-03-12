using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x0200000A RID: 10
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class FindDomainsRequest
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000025B4 File Offset: 0x000007B4
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000025BC File Offset: 0x000007BC
		[DataMember(IsRequired = true)]
		public List<string> DomainsName { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000025C5 File Offset: 0x000007C5
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000025CD File Offset: 0x000007CD
		[DataMember(IsRequired = true)]
		public List<string> DomainPropertyNames { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000025D6 File Offset: 0x000007D6
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000025DE File Offset: 0x000007DE
		[DataMember(IsRequired = false)]
		public List<string> TenantPropertyNames { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000025E7 File Offset: 0x000007E7
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000025EF File Offset: 0x000007EF
		[DataMember(IsRequired = false)]
		public int ReadFlag { get; set; }

		// Token: 0x0600002E RID: 46 RVA: 0x000025F8 File Offset: 0x000007F8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(300);
			try
			{
				stringBuilder.Append("<FindDomainsRequest>Domains=");
				foreach (string arg in this.DomainsName)
				{
					stringBuilder.AppendFormat("{0};", arg);
				}
				stringBuilder.Append(",Props=");
				foreach (string arg2 in this.DomainPropertyNames)
				{
					stringBuilder.AppendFormat("{0};", arg2);
				}
				stringBuilder.Append(",TenantProps=");
				foreach (string arg3 in this.TenantPropertyNames)
				{
					stringBuilder.AppendFormat("{0};", arg3);
				}
				stringBuilder.AppendFormat(",ReadFlag={0}", this.ReadFlag.ToString());
				stringBuilder.Append("</FindDomainsRequest>");
			}
			catch (Exception ex)
			{
				stringBuilder.Append(" ??TraceErr" + ex.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
