using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000408 RID: 1032
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class OneDriveProGroupsPagingMetadata : OneDriveProItemsPagingMetadata
	{
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x0007EDF5 File Offset: 0x0007CFF5
		// (set) Token: 0x0600224A RID: 8778 RVA: 0x0007EDFD File Offset: 0x0007CFFD
		[DataMember]
		public string GroupSmtpAddress { get; set; }

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x0007EE06 File Offset: 0x0007D006
		// (set) Token: 0x0600224C RID: 8780 RVA: 0x0007EE0E File Offset: 0x0007D00E
		[DataMember]
		public string GroupEndpointUrl { get; set; }
	}
}
