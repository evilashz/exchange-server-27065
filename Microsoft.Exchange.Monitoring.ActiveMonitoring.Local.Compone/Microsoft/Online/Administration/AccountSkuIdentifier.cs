using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003C2 RID: 962
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "AccountSkuIdentifier", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	public class AccountSkuIdentifier : IExtensibleDataObject
	{
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x0008C69F File Offset: 0x0008A89F
		// (set) Token: 0x06001732 RID: 5938 RVA: 0x0008C6A7 File Offset: 0x0008A8A7
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

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x0008C6B0 File Offset: 0x0008A8B0
		// (set) Token: 0x06001734 RID: 5940 RVA: 0x0008C6B8 File Offset: 0x0008A8B8
		[DataMember]
		public string AccountName
		{
			get
			{
				return this.AccountNameField;
			}
			set
			{
				this.AccountNameField = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x0008C6C1 File Offset: 0x0008A8C1
		// (set) Token: 0x06001736 RID: 5942 RVA: 0x0008C6C9 File Offset: 0x0008A8C9
		[DataMember]
		public string SkuPartNumber
		{
			get
			{
				return this.SkuPartNumberField;
			}
			set
			{
				this.SkuPartNumberField = value;
			}
		}

		// Token: 0x04001068 RID: 4200
		private ExtensionDataObject extensionDataField;

		// Token: 0x04001069 RID: 4201
		private string AccountNameField;

		// Token: 0x0400106A RID: 4202
		private string SkuPartNumberField;
	}
}
