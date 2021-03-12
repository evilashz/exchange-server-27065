using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AFC RID: 2812
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class ImportContactListRequest : BaseJsonRequest
	{
		// Token: 0x06004FF1 RID: 20465 RVA: 0x00109155 File Offset: 0x00107355
		public ImportContactListRequest()
		{
			this.ImportedContactList = new ImportContactList();
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x06004FF2 RID: 20466 RVA: 0x00109168 File Offset: 0x00107368
		// (set) Token: 0x06004FF3 RID: 20467 RVA: 0x00109170 File Offset: 0x00107370
		[DataMember(IsRequired = true)]
		public ImportContactList ImportedContactList { get; set; }

		// Token: 0x06004FF4 RID: 20468 RVA: 0x00109179 File Offset: 0x00107379
		public override string ToString()
		{
			return string.Format("ImportContactListRequest: {0}", this.ImportedContactList);
		}
	}
}
