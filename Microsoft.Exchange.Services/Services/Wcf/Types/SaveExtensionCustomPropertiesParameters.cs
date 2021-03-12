using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A00 RID: 2560
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SaveExtensionCustomPropertiesParameters
	{
		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06004850 RID: 18512 RVA: 0x00101663 File Offset: 0x000FF863
		// (set) Token: 0x06004851 RID: 18513 RVA: 0x0010166B File Offset: 0x000FF86B
		[DataMember(IsRequired = true, Order = 1)]
		public string ExtensionId { get; set; }

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06004852 RID: 18514 RVA: 0x00101674 File Offset: 0x000FF874
		// (set) Token: 0x06004853 RID: 18515 RVA: 0x0010167C File Offset: 0x000FF87C
		[DataMember(IsRequired = true, Order = 2)]
		public string ItemId { get; set; }

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06004854 RID: 18516 RVA: 0x00101685 File Offset: 0x000FF885
		// (set) Token: 0x06004855 RID: 18517 RVA: 0x0010168D File Offset: 0x000FF88D
		[DataMember(IsRequired = true, Order = 3)]
		public string CustomProperties { get; set; }
	}
}
