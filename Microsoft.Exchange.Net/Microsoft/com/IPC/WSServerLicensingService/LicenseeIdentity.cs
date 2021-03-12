using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.com.IPC.WSServerLicensingService
{
	// Token: 0x02000A06 RID: 2566
	[DataContract(Name = "LicenseeIdentity", Namespace = "http://microsoft.com/IPC/WSServerLicensingService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	public class LicenseeIdentity : IExtensibleDataObject
	{
		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x060037EF RID: 14319 RVA: 0x0008D580 File Offset: 0x0008B780
		// (set) Token: 0x060037F0 RID: 14320 RVA: 0x0008D588 File Offset: 0x0008B788
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

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060037F1 RID: 14321 RVA: 0x0008D591 File Offset: 0x0008B791
		// (set) Token: 0x060037F2 RID: 14322 RVA: 0x0008D599 File Offset: 0x0008B799
		[DataMember(IsRequired = true)]
		public string Email
		{
			get
			{
				return this.EmailField;
			}
			set
			{
				this.EmailField = value;
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x060037F3 RID: 14323 RVA: 0x0008D5A2 File Offset: 0x0008B7A2
		// (set) Token: 0x060037F4 RID: 14324 RVA: 0x0008D5AA File Offset: 0x0008B7AA
		[DataMember(EmitDefaultValue = false)]
		public string[] ProxyAddresses
		{
			get
			{
				return this.ProxyAddressesField;
			}
			set
			{
				this.ProxyAddressesField = value;
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x060037F5 RID: 14325 RVA: 0x0008D5B3 File Offset: 0x0008B7B3
		// (set) Token: 0x060037F6 RID: 14326 RVA: 0x0008D5BB File Offset: 0x0008B7BB
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string[] GroupMemberships
		{
			get
			{
				return this.GroupMembershipsField;
			}
			set
			{
				this.GroupMembershipsField = value;
			}
		}

		// Token: 0x04002F61 RID: 12129
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002F62 RID: 12130
		private string EmailField;

		// Token: 0x04002F63 RID: 12131
		private string[] ProxyAddressesField;

		// Token: 0x04002F64 RID: 12132
		private string[] GroupMembershipsField;
	}
}
