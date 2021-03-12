using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200000A RID: 10
	[DataContract(Name = "EmailAttribute", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.Email.Platform")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class EmailAttribute : IExtensibleDataObject
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000027C2 File Offset: 0x000009C2
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000027CA File Offset: 0x000009CA
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000027D3 File Offset: 0x000009D3
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000027DB File Offset: 0x000009DB
		[DataMember]
		public string Name
		{
			get
			{
				return this.NameField;
			}
			set
			{
				this.NameField = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000027E4 File Offset: 0x000009E4
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000027EC File Offset: 0x000009EC
		[DataMember]
		public string Value
		{
			get
			{
				return this.ValueField;
			}
			set
			{
				this.ValueField = value;
			}
		}

		// Token: 0x0400000C RID: 12
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400000D RID: 13
		private string NameField;

		// Token: 0x0400000E RID: 14
		private string ValueField;
	}
}
