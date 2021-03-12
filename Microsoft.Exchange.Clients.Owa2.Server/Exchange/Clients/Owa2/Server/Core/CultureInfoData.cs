using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003B9 RID: 953
	[DataContract(Name = "CultureInfoData", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CultureInfoData
	{
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x000769E2 File Offset: 0x00074BE2
		// (set) Token: 0x06001E97 RID: 7831 RVA: 0x000769EA File Offset: 0x00074BEA
		[DataMember(Name = "Name", IsRequired = false)]
		public string Name { get; set; }

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x000769F3 File Offset: 0x00074BF3
		// (set) Token: 0x06001E99 RID: 7833 RVA: 0x000769FB File Offset: 0x00074BFB
		[DataMember(Name = "NativeName", IsRequired = false)]
		public string NativeName { get; set; }

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x00076A04 File Offset: 0x00074C04
		// (set) Token: 0x06001E9B RID: 7835 RVA: 0x00076A0C File Offset: 0x00074C0C
		[DataMember(Name = "LCID", IsRequired = false)]
		public int LCID { get; set; }
	}
}
