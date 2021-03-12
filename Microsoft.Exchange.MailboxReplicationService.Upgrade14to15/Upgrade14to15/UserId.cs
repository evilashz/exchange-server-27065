using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000056 RID: 86
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "UserId", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.SuiteService")]
	public class UserId : IExtensibleDataObject
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000037DE File Offset: 0x000019DE
		// (set) Token: 0x06000215 RID: 533 RVA: 0x000037E6 File Offset: 0x000019E6
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

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000037EF File Offset: 0x000019EF
		// (set) Token: 0x06000217 RID: 535 RVA: 0x000037F7 File Offset: 0x000019F7
		[DataMember]
		public Guid Id
		{
			get
			{
				return this.IdField;
			}
			set
			{
				this.IdField = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00003800 File Offset: 0x00001A00
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00003808 File Offset: 0x00001A08
		[DataMember]
		public string Upn
		{
			get
			{
				return this.UpnField;
			}
			set
			{
				this.UpnField = value;
			}
		}

		// Token: 0x040000ED RID: 237
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000EE RID: 238
		private Guid IdField;

		// Token: 0x040000EF RID: 239
		private string UpnField;
	}
}
