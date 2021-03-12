using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x02000006 RID: 6
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class DomainQuery
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002268 File Offset: 0x00000468
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002270 File Offset: 0x00000470
		[DataMember(IsRequired = true)]
		public string DomainName { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002279 File Offset: 0x00000479
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002281 File Offset: 0x00000481
		[DataMember(IsRequired = false)]
		public List<string> PropertyNames { get; set; }

		// Token: 0x06000010 RID: 16 RVA: 0x0000228C File Offset: 0x0000048C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			try
			{
				stringBuilder.AppendFormat("<DomainQuery>DomainName={0},Props=", this.DomainName);
				if (this.PropertyNames != null)
				{
					foreach (string arg in this.PropertyNames)
					{
						stringBuilder.AppendFormat("{0};", arg);
					}
				}
				stringBuilder.Append("</DomainQuery>");
			}
			catch (Exception ex)
			{
				stringBuilder.Append(" ??TraceErr" + ex.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
