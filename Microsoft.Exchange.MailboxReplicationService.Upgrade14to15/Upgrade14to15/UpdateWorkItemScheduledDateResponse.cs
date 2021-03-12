using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200002F RID: 47
	[DataContract(Name = "UpdateWorkItemScheduledDateResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class UpdateWorkItemScheduledDateResponse : IExtensibleDataObject
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00003068 File Offset: 0x00001268
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00003070 File Offset: 0x00001270
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

		// Token: 0x0400008F RID: 143
		private ExtensionDataObject extensionDataField;
	}
}
