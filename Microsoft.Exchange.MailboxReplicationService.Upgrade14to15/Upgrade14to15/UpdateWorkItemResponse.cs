using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000025 RID: 37
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UpdateWorkItemResponse", Namespace = "http://tempuri.org/")]
	public class UpdateWorkItemResponse : IExtensibleDataObject
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00002EE6 File Offset: 0x000010E6
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00002EEE File Offset: 0x000010EE
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

		// Token: 0x0400007D RID: 125
		private ExtensionDataObject extensionDataField;
	}
}
