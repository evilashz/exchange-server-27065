using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C47 RID: 3143
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UserQuery", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[DebuggerStepThrough]
	public class UserQuery : IExtensibleDataObject
	{
		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x060044EF RID: 17647 RVA: 0x000B7101 File Offset: 0x000B5301
		// (set) Token: 0x060044F0 RID: 17648 RVA: 0x000B7109 File Offset: 0x000B5309
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

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x060044F1 RID: 17649 RVA: 0x000B7112 File Offset: 0x000B5312
		// (set) Token: 0x060044F2 RID: 17650 RVA: 0x000B711A File Offset: 0x000B531A
		[DataMember(IsRequired = true)]
		public string UserKey
		{
			get
			{
				return this.UserKeyField;
			}
			set
			{
				this.UserKeyField = value;
			}
		}

		// Token: 0x04003A34 RID: 14900
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003A35 RID: 14901
		private string UserKeyField;
	}
}
