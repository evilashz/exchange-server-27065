using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C3C RID: 3132
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "UserInfo", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class UserInfo : IExtensibleDataObject
	{
		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x0600449D RID: 17565 RVA: 0x000B6E4D File Offset: 0x000B504D
		// (set) Token: 0x0600449E RID: 17566 RVA: 0x000B6E55 File Offset: 0x000B5055
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

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x0600449F RID: 17567 RVA: 0x000B6E5E File Offset: 0x000B505E
		// (set) Token: 0x060044A0 RID: 17568 RVA: 0x000B6E66 File Offset: 0x000B5066
		[DataMember(IsRequired = true)]
		public string MSAUserName
		{
			get
			{
				return this.MSAUserNameField;
			}
			set
			{
				this.MSAUserNameField = value;
			}
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x060044A1 RID: 17569 RVA: 0x000B6E6F File Offset: 0x000B506F
		// (set) Token: 0x060044A2 RID: 17570 RVA: 0x000B6E77 File Offset: 0x000B5077
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

		// Token: 0x04003A0B RID: 14859
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003A0C RID: 14860
		private string MSAUserNameField;

		// Token: 0x04003A0D RID: 14861
		private string UserKeyField;
	}
}
