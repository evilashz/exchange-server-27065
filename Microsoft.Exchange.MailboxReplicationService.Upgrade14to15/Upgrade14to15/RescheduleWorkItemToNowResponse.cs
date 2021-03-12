using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000067 RID: 103
	[DataContract(Name = "RescheduleWorkItemToNowResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class RescheduleWorkItemToNowResponse : IExtensibleDataObject
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00003B06 File Offset: 0x00001D06
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00003B0E File Offset: 0x00001D0E
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

		// Token: 0x0400011D RID: 285
		private ExtensionDataObject extensionDataField;
	}
}
