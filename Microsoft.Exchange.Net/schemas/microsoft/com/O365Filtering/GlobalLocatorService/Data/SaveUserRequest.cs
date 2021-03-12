using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C45 RID: 3141
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SaveUserRequest", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class SaveUserRequest : IExtensibleDataObject
	{
		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x060044DD RID: 17629 RVA: 0x000B7069 File Offset: 0x000B5269
		// (set) Token: 0x060044DE RID: 17630 RVA: 0x000B7071 File Offset: 0x000B5271
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

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x060044DF RID: 17631 RVA: 0x000B707A File Offset: 0x000B527A
		// (set) Token: 0x060044E0 RID: 17632 RVA: 0x000B7082 File Offset: 0x000B5282
		[DataMember]
		public string CustomTag
		{
			get
			{
				return this.CustomTagField;
			}
			set
			{
				this.CustomTagField = value;
			}
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x000B708B File Offset: 0x000B528B
		// (set) Token: 0x060044E2 RID: 17634 RVA: 0x000B7093 File Offset: 0x000B5293
		[DataMember(IsRequired = true)]
		public Guid TenantId
		{
			get
			{
				return this.TenantIdField;
			}
			set
			{
				this.TenantIdField = value;
			}
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x060044E3 RID: 17635 RVA: 0x000B709C File Offset: 0x000B529C
		// (set) Token: 0x060044E4 RID: 17636 RVA: 0x000B70A4 File Offset: 0x000B52A4
		[DataMember(IsRequired = true)]
		public UserInfo UserInfo
		{
			get
			{
				return this.UserInfoField;
			}
			set
			{
				this.UserInfoField = value;
			}
		}

		// Token: 0x04003A2C RID: 14892
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003A2D RID: 14893
		private string CustomTagField;

		// Token: 0x04003A2E RID: 14894
		private Guid TenantIdField;

		// Token: 0x04003A2F RID: 14895
		private UserInfo UserInfoField;
	}
}
