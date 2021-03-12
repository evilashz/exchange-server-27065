using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003CB RID: 971
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "LicenseOption", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class LicenseOption : IExtensibleDataObject
	{
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0008C9C5 File Offset: 0x0008ABC5
		// (set) Token: 0x06001791 RID: 6033 RVA: 0x0008C9CD File Offset: 0x0008ABCD
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

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x0008C9D6 File Offset: 0x0008ABD6
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x0008C9DE File Offset: 0x0008ABDE
		[DataMember]
		public AccountSkuIdentifier AccountSkuId
		{
			get
			{
				return this.AccountSkuIdField;
			}
			set
			{
				this.AccountSkuIdField = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x0008C9E7 File Offset: 0x0008ABE7
		// (set) Token: 0x06001795 RID: 6037 RVA: 0x0008C9EF File Offset: 0x0008ABEF
		[DataMember]
		public string[] DisabledServicePlans
		{
			get
			{
				return this.DisabledServicePlansField;
			}
			set
			{
				this.DisabledServicePlansField = value;
			}
		}

		// Token: 0x040010B0 RID: 4272
		private ExtensionDataObject extensionDataField;

		// Token: 0x040010B1 RID: 4273
		private AccountSkuIdentifier AccountSkuIdField;

		// Token: 0x040010B2 RID: 4274
		private string[] DisabledServicePlansField;
	}
}
