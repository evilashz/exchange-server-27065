using System;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x02000008 RID: 8
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class ResponseBase
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002430 File Offset: 0x00000630
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002438 File Offset: 0x00000638
		[DataMember]
		public string TransactionID { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002441 File Offset: 0x00000641
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002449 File Offset: 0x00000649
		[DataMember]
		public string Diagnostics { get; set; }

		// Token: 0x0600001E RID: 30 RVA: 0x00002454 File Offset: 0x00000654
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(300);
			try
			{
				stringBuilder.Append("<ResponseBase>");
				stringBuilder.AppendFormat("TransactionID={0},Diagnostics={1}", this.TransactionID, this.Diagnostics);
				stringBuilder.Append("</ResponseBase>");
			}
			catch (Exception ex)
			{
				stringBuilder.Append(" ??TraceErr" + ex.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
