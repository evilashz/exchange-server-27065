using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x02000068 RID: 104
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UserInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[DebuggerStepThrough]
	public class UserInfo : IExtensibleDataObject
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000DC3E File Offset: 0x0000BE3E
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000DC46 File Offset: 0x0000BE46
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000DC4F File Offset: 0x0000BE4F
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000DC57 File Offset: 0x0000BE57
		[DataMember]
		public string BecContextToken
		{
			get
			{
				return this.BecContextTokenField;
			}
			set
			{
				this.BecContextTokenField = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000DC60 File Offset: 0x0000BE60
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000DC68 File Offset: 0x0000BE68
		[DataMember]
		public string Puid
		{
			get
			{
				return this.PuidField;
			}
			set
			{
				this.PuidField = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000DC71 File Offset: 0x0000BE71
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000DC79 File Offset: 0x0000BE79
		[DataMember]
		public string UserPrincipalName
		{
			get
			{
				return this.UserPrincipalNameField;
			}
			set
			{
				this.UserPrincipalNameField = value;
			}
		}

		// Token: 0x040001A7 RID: 423
		private ExtensionDataObject extensionDataField;

		// Token: 0x040001A8 RID: 424
		private string BecContextTokenField;

		// Token: 0x040001A9 RID: 425
		private string PuidField;

		// Token: 0x040001AA RID: 426
		private string UserPrincipalNameField;
	}
}
