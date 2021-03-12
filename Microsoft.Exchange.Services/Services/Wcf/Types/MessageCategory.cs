using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AC0 RID: 2752
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MessageCategory
	{
		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06004E48 RID: 20040 RVA: 0x00107B69 File Offset: 0x00105D69
		// (set) Token: 0x06004E49 RID: 20041 RVA: 0x00107B71 File Offset: 0x00105D71
		[DataMember]
		public int Color { get; set; }

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06004E4A RID: 20042 RVA: 0x00107B7A File Offset: 0x00105D7A
		// (set) Token: 0x06004E4B RID: 20043 RVA: 0x00107B82 File Offset: 0x00105D82
		[DataMember]
		public string Name { get; set; }

		// Token: 0x06004E4C RID: 20044 RVA: 0x00107B8B File Offset: 0x00105D8B
		public override string ToString()
		{
			return string.Format("Color = {0}, Name = {1}", this.Color, this.Name);
		}
	}
}
