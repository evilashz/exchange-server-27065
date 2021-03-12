using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000089 RID: 137
	[DataContract(Name = "UpdateGroupResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class UpdateGroupResponse : IExtensibleDataObject
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00004389 File Offset: 0x00002589
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00004391 File Offset: 0x00002591
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

		// Token: 0x04000192 RID: 402
		private ExtensionDataObject extensionDataField;
	}
}
