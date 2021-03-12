using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x0200001C RID: 28
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class TenantQuery
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004208 File Offset: 0x00002408
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00004210 File Offset: 0x00002410
		[DataMember(IsRequired = true)]
		public Guid TenantId { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004219 File Offset: 0x00002419
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00004221 File Offset: 0x00002421
		[DataMember(IsRequired = false)]
		public List<string> PropertyNames { get; set; }

		// Token: 0x06000092 RID: 146 RVA: 0x0000422C File Offset: 0x0000242C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			try
			{
				stringBuilder.AppendFormat("<TenantQuery>TenantId={0},Props=", this.TenantId.ToString());
				if (this.PropertyNames != null)
				{
					foreach (string arg in this.PropertyNames)
					{
						stringBuilder.AppendFormat("{0};", arg);
					}
				}
				stringBuilder.Append("</TenantQuery>");
			}
			catch (Exception ex)
			{
				stringBuilder.Append(" ??TraceErr" + ex.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
