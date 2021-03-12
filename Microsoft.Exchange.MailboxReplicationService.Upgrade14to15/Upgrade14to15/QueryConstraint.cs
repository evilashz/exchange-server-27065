using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000094 RID: 148
	[DataContract(Name = "QueryConstraint", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class QueryConstraint : IExtensibleDataObject
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00004535 File Offset: 0x00002735
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000453D File Offset: 0x0000273D
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

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00004546 File Offset: 0x00002746
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000454E File Offset: 0x0000274E
		[DataMember]
		public string[] constraintName
		{
			get
			{
				return this.constraintNameField;
			}
			set
			{
				this.constraintNameField = value;
			}
		}

		// Token: 0x040001A6 RID: 422
		private ExtensionDataObject extensionDataField;

		// Token: 0x040001A7 RID: 423
		private string[] constraintNameField;
	}
}
