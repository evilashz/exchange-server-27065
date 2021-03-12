using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000092 RID: 146
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "QueryBlackout", Namespace = "http://tempuri.org/")]
	public class QueryBlackout : IExtensibleDataObject
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000393 RID: 915 RVA: 0x000044E1 File Offset: 0x000026E1
		// (set) Token: 0x06000394 RID: 916 RVA: 0x000044E9 File Offset: 0x000026E9
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

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000395 RID: 917 RVA: 0x000044F2 File Offset: 0x000026F2
		// (set) Token: 0x06000396 RID: 918 RVA: 0x000044FA File Offset: 0x000026FA
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

		// Token: 0x040001A2 RID: 418
		private ExtensionDataObject extensionDataField;

		// Token: 0x040001A3 RID: 419
		private string[] groupNamesField;
	}
}
