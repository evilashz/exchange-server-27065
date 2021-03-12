using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000074 RID: 116
	[DebuggerStepThrough]
	[DataContract(Name = "PickWorkItems", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class PickWorkItems : IExtensibleDataObject
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00003E16 File Offset: 0x00002016
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00003E1E File Offset: 0x0000201E
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

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00003E27 File Offset: 0x00002027
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00003E2F File Offset: 0x0000202F
		[DataMember]
		public int pageSize
		{
			get
			{
				return this.pageSizeField;
			}
			set
			{
				this.pageSizeField = value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00003E38 File Offset: 0x00002038
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00003E40 File Offset: 0x00002040
		[DataMember]
		public TimeSpan visibilityTimeout
		{
			get
			{
				return this.visibilityTimeoutField;
			}
			set
			{
				this.visibilityTimeoutField = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00003E49 File Offset: 0x00002049
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00003E51 File Offset: 0x00002051
		[DataMember(Order = 2)]
		public byte[] bookmark
		{
			get
			{
				return this.bookmarkField;
			}
			set
			{
				this.bookmarkField = value;
			}
		}

		// Token: 0x04000145 RID: 325
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000146 RID: 326
		private int pageSizeField;

		// Token: 0x04000147 RID: 327
		private TimeSpan visibilityTimeoutField;

		// Token: 0x04000148 RID: 328
		private byte[] bookmarkField;
	}
}
