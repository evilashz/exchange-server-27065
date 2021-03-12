using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000352 RID: 850
	[DataContract(Name = "UserIdentityHeader", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class UserIdentityHeader : IExtensibleDataObject
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x0008BCC5 File Offset: 0x00089EC5
		// (set) Token: 0x06001603 RID: 5635 RVA: 0x0008BCCD File Offset: 0x00089ECD
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x0008BCD6 File Offset: 0x00089ED6
		// (set) Token: 0x06001605 RID: 5637 RVA: 0x0008BCDE File Offset: 0x00089EDE
		[DataMember]
		public string LiveToken
		{
			get
			{
				return this.LiveTokenField;
			}
			set
			{
				this.LiveTokenField = value;
			}
		}

		// Token: 0x04000FF3 RID: 4083
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000FF4 RID: 4084
		private string LiveTokenField;
	}
}
