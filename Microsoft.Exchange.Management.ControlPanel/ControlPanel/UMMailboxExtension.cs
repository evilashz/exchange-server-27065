using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200046F RID: 1135
	[DataContract]
	public class UMMailboxExtension : BaseRow
	{
		// Token: 0x170022AC RID: 8876
		// (get) Token: 0x06003954 RID: 14676 RVA: 0x000AE67E File Offset: 0x000AC87E
		// (set) Token: 0x06003955 RID: 14677 RVA: 0x000AE686 File Offset: 0x000AC886
		[DataMember]
		public string DisplayName { get; set; }
	}
}
