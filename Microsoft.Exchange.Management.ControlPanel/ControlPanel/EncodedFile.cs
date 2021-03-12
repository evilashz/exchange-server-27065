using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000200 RID: 512
	[DataContract]
	public class EncodedFile
	{
		// Token: 0x17001BE1 RID: 7137
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x000780AB File Offset: 0x000762AB
		// (set) Token: 0x0600269A RID: 9882 RVA: 0x000780B3 File Offset: 0x000762B3
		[DataMember]
		public string FileContent { get; set; }
	}
}
