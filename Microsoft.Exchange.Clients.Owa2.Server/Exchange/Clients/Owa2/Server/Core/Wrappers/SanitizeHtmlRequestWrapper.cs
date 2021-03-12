using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002A9 RID: 681
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SanitizeHtmlRequestWrapper
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x00054065 File Offset: 0x00052265
		// (set) Token: 0x060017F9 RID: 6137 RVA: 0x0005406D File Offset: 0x0005226D
		[DataMember(Name = "input")]
		public string Input { get; set; }
	}
}
