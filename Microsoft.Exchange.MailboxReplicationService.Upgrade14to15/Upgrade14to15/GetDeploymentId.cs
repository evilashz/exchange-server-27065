using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200003A RID: 58
	[DataContract(Name = "GetDeploymentId", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetDeploymentId : IExtensibleDataObject
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00003357 File Offset: 0x00001557
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000335F File Offset: 0x0000155F
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

		// Token: 0x040000B6 RID: 182
		private ExtensionDataObject extensionDataField;
	}
}
