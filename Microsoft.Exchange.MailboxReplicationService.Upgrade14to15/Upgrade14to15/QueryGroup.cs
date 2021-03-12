using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200008E RID: 142
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "QueryGroup", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class QueryGroup : IExtensibleDataObject
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00004439 File Offset: 0x00002639
		// (set) Token: 0x06000380 RID: 896 RVA: 0x00004441 File Offset: 0x00002641
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

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000444A File Offset: 0x0000264A
		// (set) Token: 0x06000382 RID: 898 RVA: 0x00004452 File Offset: 0x00002652
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

		// Token: 0x0400019A RID: 410
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400019B RID: 411
		private string[] groupNamesField;
	}
}
