using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000095 RID: 149
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "QueryConstraintResponse", Namespace = "http://tempuri.org/")]
	public class QueryConstraintResponse : IExtensibleDataObject
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000455F File Offset: 0x0000275F
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00004567 File Offset: 0x00002767
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

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00004570 File Offset: 0x00002770
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x00004578 File Offset: 0x00002778
		[DataMember]
		public Constraint[] QueryConstraintResult
		{
			get
			{
				return this.QueryConstraintResultField;
			}
			set
			{
				this.QueryConstraintResultField = value;
			}
		}

		// Token: 0x040001A8 RID: 424
		private ExtensionDataObject extensionDataField;

		// Token: 0x040001A9 RID: 425
		private Constraint[] QueryConstraintResultField;
	}
}
