using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200026F RID: 623
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateTaskFolderRequestWrapper
	{
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x00053956 File Offset: 0x00051B56
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x0005395E File Offset: 0x00051B5E
		[DataMember(Name = "newTaskFolderName")]
		public string NewTaskFolderName { get; set; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x00053967 File Offset: 0x00051B67
		// (set) Token: 0x06001723 RID: 5923 RVA: 0x0005396F File Offset: 0x00051B6F
		[DataMember(Name = "parentGroupGuid")]
		public string ParentGroupGuid { get; set; }
	}
}
