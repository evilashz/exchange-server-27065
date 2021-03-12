using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.com.IPC.WSService
{
	// Token: 0x020009FE RID: 2558
	[DataContract(Name = "ActiveFederationFault", Namespace = "http://microsoft.com/IPC/WSService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	public class ActiveFederationFault : IExtensibleDataObject
	{
		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x060037CC RID: 14284 RVA: 0x0008D246 File Offset: 0x0008B446
		// (set) Token: 0x060037CD RID: 14285 RVA: 0x0008D24E File Offset: 0x0008B44E
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

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x060037CE RID: 14286 RVA: 0x0008D257 File Offset: 0x0008B457
		// (set) Token: 0x060037CF RID: 14287 RVA: 0x0008D25F File Offset: 0x0008B45F
		[DataMember]
		public RmsErrors ErrorCode
		{
			get
			{
				return this.ErrorCodeField;
			}
			set
			{
				this.ErrorCodeField = value;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x0008D268 File Offset: 0x0008B468
		// (set) Token: 0x060037D1 RID: 14289 RVA: 0x0008D270 File Offset: 0x0008B470
		[DataMember]
		public bool IsPermanentFailure
		{
			get
			{
				return this.IsPermanentFailureField;
			}
			set
			{
				this.IsPermanentFailureField = value;
			}
		}

		// Token: 0x04002F3F RID: 12095
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002F40 RID: 12096
		private RmsErrors ErrorCodeField;

		// Token: 0x04002F41 RID: 12097
		private bool IsPermanentFailureField;
	}
}
