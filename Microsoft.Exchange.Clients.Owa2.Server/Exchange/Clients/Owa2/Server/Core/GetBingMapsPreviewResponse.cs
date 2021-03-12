using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003C4 RID: 964
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetBingMapsPreviewResponse
	{
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x00076CBB File Offset: 0x00074EBB
		// (set) Token: 0x06001EE3 RID: 7907 RVA: 0x00076CC3 File Offset: 0x00074EC3
		[DataMember]
		public string Error { get; set; }

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x00076CCC File Offset: 0x00074ECC
		// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x00076CD4 File Offset: 0x00074ED4
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x00076CDD File Offset: 0x00074EDD
		// (set) Token: 0x06001EE7 RID: 7911 RVA: 0x00076CE5 File Offset: 0x00074EE5
		[DataMember]
		public AttachmentType Attachment { get; set; }

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x00076CEE File Offset: 0x00074EEE
		// (set) Token: 0x06001EE9 RID: 7913 RVA: 0x00076CF6 File Offset: 0x00074EF6
		[DataMember]
		public string ImageData { get; set; }

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x00076CFF File Offset: 0x00074EFF
		// (set) Token: 0x06001EEB RID: 7915 RVA: 0x00076D07 File Offset: 0x00074F07
		[DataMember]
		public int ImageDataSize { get; set; }

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x00076D10 File Offset: 0x00074F10
		// (set) Token: 0x06001EED RID: 7917 RVA: 0x00076D18 File Offset: 0x00074F18
		[DataMember]
		public string ImageDataType { get; set; }
	}
}
