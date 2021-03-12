using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200008F RID: 143
	[DebuggerStepThrough]
	[DataContract(Name = "QueryGroupResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class QueryGroupResponse : IExtensibleDataObject
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00004463 File Offset: 0x00002663
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000446B File Offset: 0x0000266B
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

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00004474 File Offset: 0x00002674
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0000447C File Offset: 0x0000267C
		[DataMember]
		public Group[] QueryGroupResult
		{
			get
			{
				return this.QueryGroupResultField;
			}
			set
			{
				this.QueryGroupResultField = value;
			}
		}

		// Token: 0x0400019C RID: 412
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400019D RID: 413
		private Group[] QueryGroupResultField;
	}
}
