using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000023 RID: 35
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "AddWorkItemResponse", Namespace = "http://tempuri.org/")]
	public class AddWorkItemResponse : IExtensibleDataObject
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00002E92 File Offset: 0x00001092
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00002E9A File Offset: 0x0000109A
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00002EA3 File Offset: 0x000010A3
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00002EAB File Offset: 0x000010AB
		[DataMember]
		public string AddWorkItemResult
		{
			get
			{
				return this.AddWorkItemResultField;
			}
			set
			{
				this.AddWorkItemResultField = value;
			}
		}

		// Token: 0x04000079 RID: 121
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400007A RID: 122
		private string AddWorkItemResultField;
	}
}
