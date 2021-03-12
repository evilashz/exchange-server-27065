using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200014E RID: 334
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ItemHasAttachmentRule : ActivationRule
	{
		// Token: 0x06000AF4 RID: 2804 RVA: 0x00022A35 File Offset: 0x00020C35
		public ItemHasAttachmentRule() : base("ItemHasAttachment")
		{
		}
	}
}
