using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003A8 RID: 936
	[KnownType(typeof(OneDriveProScope))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AttachmentDataProviderScope
	{
		// Token: 0x06001DE0 RID: 7648 RVA: 0x000763BA File Offset: 0x000745BA
		public AttachmentDataProviderScope(string displayName, string ariaLabel)
		{
			this.DisplayName = displayName;
			this.AriaLabel = ariaLabel;
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x000763D0 File Offset: 0x000745D0
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x000763D8 File Offset: 0x000745D8
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x000763E1 File Offset: 0x000745E1
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x000763E9 File Offset: 0x000745E9
		[DataMember]
		public string AriaLabel { get; set; }
	}
}
