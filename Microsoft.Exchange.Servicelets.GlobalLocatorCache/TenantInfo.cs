using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x0200001B RID: 27
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class TenantInfo
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000040F8 File Offset: 0x000022F8
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00004100 File Offset: 0x00002300
		[DataMember(IsRequired = true)]
		public Guid TenantId { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004109 File Offset: 0x00002309
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00004111 File Offset: 0x00002311
		[DataMember(IsRequired = true)]
		public List<KeyValuePair<string, string>> Properties { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000411A File Offset: 0x0000231A
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00004122 File Offset: 0x00002322
		[DataMember]
		public List<string> NoneExistNamespaces { get; set; }

		// Token: 0x0600008C RID: 140 RVA: 0x0000412C File Offset: 0x0000232C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			try
			{
				stringBuilder.AppendFormat("<TenantInfo>TenantId={0},Props=", this.TenantId.ToString());
				if (this.Properties != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in this.Properties)
					{
						stringBuilder.AppendFormat("{0}:{1};", keyValuePair.Key, keyValuePair.Value);
					}
				}
				stringBuilder.Append("</TenantInfo>");
			}
			catch (Exception ex)
			{
				stringBuilder.Append(" ??TraceErr" + ex.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
