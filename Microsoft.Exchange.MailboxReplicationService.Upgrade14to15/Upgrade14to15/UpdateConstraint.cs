using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200008A RID: 138
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "UpdateConstraint", Namespace = "http://tempuri.org/")]
	public class UpdateConstraint : IExtensibleDataObject
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600036D RID: 877 RVA: 0x000043A2 File Offset: 0x000025A2
		// (set) Token: 0x0600036E RID: 878 RVA: 0x000043AA File Offset: 0x000025AA
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

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000043B3 File Offset: 0x000025B3
		// (set) Token: 0x06000370 RID: 880 RVA: 0x000043BB File Offset: 0x000025BB
		[DataMember]
		public Constraint[] constraint
		{
			get
			{
				return this.constraintField;
			}
			set
			{
				this.constraintField = value;
			}
		}

		// Token: 0x04000193 RID: 403
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000194 RID: 404
		private Constraint[] constraintField;
	}
}
