using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C48 RID: 3144
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "DeleteUserRequest", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class DeleteUserRequest : IExtensibleDataObject
	{
		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x060044F4 RID: 17652 RVA: 0x000B712B File Offset: 0x000B532B
		// (set) Token: 0x060044F5 RID: 17653 RVA: 0x000B7133 File Offset: 0x000B5333
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

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x060044F6 RID: 17654 RVA: 0x000B713C File Offset: 0x000B533C
		// (set) Token: 0x060044F7 RID: 17655 RVA: 0x000B7144 File Offset: 0x000B5344
		[DataMember(IsRequired = true)]
		public UserQuery User
		{
			get
			{
				return this.UserField;
			}
			set
			{
				this.UserField = value;
			}
		}

		// Token: 0x04003A36 RID: 14902
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003A37 RID: 14903
		private UserQuery UserField;
	}
}
