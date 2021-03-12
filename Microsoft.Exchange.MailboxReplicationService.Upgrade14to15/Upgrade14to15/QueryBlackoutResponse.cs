using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000093 RID: 147
	[DataContract(Name = "QueryBlackoutResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class QueryBlackoutResponse : IExtensibleDataObject
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000450B File Offset: 0x0000270B
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00004513 File Offset: 0x00002713
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

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000451C File Offset: 0x0000271C
		// (set) Token: 0x0600039B RID: 923 RVA: 0x00004524 File Offset: 0x00002724
		[DataMember]
		public GroupBlackout[] QueryBlackoutResult
		{
			get
			{
				return this.QueryBlackoutResultField;
			}
			set
			{
				this.QueryBlackoutResultField = value;
			}
		}

		// Token: 0x040001A4 RID: 420
		private ExtensionDataObject extensionDataField;

		// Token: 0x040001A5 RID: 421
		private GroupBlackout[] QueryBlackoutResultField;
	}
}
