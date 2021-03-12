using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B14 RID: 2836
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetTaskFoldersResponse : TaskFolderActionResponse
	{
		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06005082 RID: 20610 RVA: 0x00109B80 File Offset: 0x00107D80
		// (set) Token: 0x06005083 RID: 20611 RVA: 0x00109B88 File Offset: 0x00107D88
		[DataMember]
		public TaskGroup[] TaskGroups { get; internal set; }
	}
}
