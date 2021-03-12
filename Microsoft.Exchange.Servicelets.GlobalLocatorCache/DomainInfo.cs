using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x02000005 RID: 5
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class DomainInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000214F File Offset: 0x0000034F
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002157 File Offset: 0x00000357
		[DataMember]
		public string DomainKey { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002160 File Offset: 0x00000360
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002168 File Offset: 0x00000368
		[DataMember(IsRequired = true)]
		public string DomainName { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002171 File Offset: 0x00000371
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002179 File Offset: 0x00000379
		[DataMember(IsRequired = true)]
		public List<KeyValuePair<string, string>> Properties { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002182 File Offset: 0x00000382
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000218A File Offset: 0x0000038A
		[DataMember]
		public List<string> NoneExistNamespaces { get; set; }

		// Token: 0x0600000A RID: 10 RVA: 0x00002194 File Offset: 0x00000394
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			try
			{
				stringBuilder.AppendFormat("<DomainInfo>DomainName={0},DomainKey={1},Props=", this.DomainName, this.DomainKey);
				if (this.Properties != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in this.Properties)
					{
						stringBuilder.AppendFormat("{0}:{1};", keyValuePair.Key, keyValuePair.Value);
					}
				}
				stringBuilder.Append("</DomainInfo>");
			}
			catch (Exception ex)
			{
				stringBuilder.Append(" ??TraceErr" + ex.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
