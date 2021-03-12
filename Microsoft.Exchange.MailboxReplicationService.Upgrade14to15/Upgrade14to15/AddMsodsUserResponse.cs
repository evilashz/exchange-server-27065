using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000045 RID: 69
	[DataContract(Name = "AddMsodsUserResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class AddMsodsUserResponse : IExtensibleDataObject
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00003558 File Offset: 0x00001758
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00003560 File Offset: 0x00001760
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00003569 File Offset: 0x00001769
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00003571 File Offset: 0x00001771
		[DataMember]
		public Guid AddMsodsUserResult
		{
			get
			{
				return this.AddMsodsUserResultField;
			}
			set
			{
				this.AddMsodsUserResultField = value;
			}
		}

		// Token: 0x040000CF RID: 207
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000D0 RID: 208
		private Guid AddMsodsUserResultField;
	}
}
