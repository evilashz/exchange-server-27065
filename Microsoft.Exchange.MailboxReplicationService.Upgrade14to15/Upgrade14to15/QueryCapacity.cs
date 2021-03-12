using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000090 RID: 144
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "QueryCapacity", Namespace = "http://tempuri.org/")]
	public class QueryCapacity : IExtensibleDataObject
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000448D File Offset: 0x0000268D
		// (set) Token: 0x0600038A RID: 906 RVA: 0x00004495 File Offset: 0x00002695
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

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000449E File Offset: 0x0000269E
		// (set) Token: 0x0600038C RID: 908 RVA: 0x000044A6 File Offset: 0x000026A6
		[DataMember]
		public string[] groupNames
		{
			get
			{
				return this.groupNamesField;
			}
			set
			{
				this.groupNamesField = value;
			}
		}

		// Token: 0x0400019E RID: 414
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400019F RID: 415
		private string[] groupNamesField;
	}
}
