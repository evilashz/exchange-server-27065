using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003B5 RID: 949
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ComplianceEntryType
	{
		// Token: 0x06001E5E RID: 7774 RVA: 0x000767F3 File Offset: 0x000749F3
		public ComplianceEntryType(string id, string name, string description)
		{
			this.Id = id;
			this.Name = name;
			this.Description = description;
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x00076810 File Offset: 0x00074A10
		// (set) Token: 0x06001E60 RID: 7776 RVA: 0x00076818 File Offset: 0x00074A18
		[DataMember]
		public string Id { get; set; }

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x00076821 File Offset: 0x00074A21
		// (set) Token: 0x06001E62 RID: 7778 RVA: 0x00076829 File Offset: 0x00074A29
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x00076832 File Offset: 0x00074A32
		// (set) Token: 0x06001E64 RID: 7780 RVA: 0x0007683A File Offset: 0x00074A3A
		[DataMember]
		public string Description { get; set; }
	}
}
