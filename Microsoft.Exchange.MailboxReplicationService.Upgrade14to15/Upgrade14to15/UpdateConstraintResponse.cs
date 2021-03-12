using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200008B RID: 139
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UpdateConstraintResponse", Namespace = "http://tempuri.org/")]
	public class UpdateConstraintResponse : IExtensibleDataObject
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000372 RID: 882 RVA: 0x000043CC File Offset: 0x000025CC
		// (set) Token: 0x06000373 RID: 883 RVA: 0x000043D4 File Offset: 0x000025D4
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

		// Token: 0x04000195 RID: 405
		private ExtensionDataObject extensionDataField;
	}
}
