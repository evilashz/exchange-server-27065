using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000091 RID: 145
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "QueryCapacityResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class QueryCapacityResponse : IExtensibleDataObject
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600038E RID: 910 RVA: 0x000044B7 File Offset: 0x000026B7
		// (set) Token: 0x0600038F RID: 911 RVA: 0x000044BF File Offset: 0x000026BF
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

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000390 RID: 912 RVA: 0x000044C8 File Offset: 0x000026C8
		// (set) Token: 0x06000391 RID: 913 RVA: 0x000044D0 File Offset: 0x000026D0
		[DataMember]
		public GroupCapacity[] QueryCapacityResult
		{
			get
			{
				return this.QueryCapacityResultField;
			}
			set
			{
				this.QueryCapacityResultField = value;
			}
		}

		// Token: 0x040001A0 RID: 416
		private ExtensionDataObject extensionDataField;

		// Token: 0x040001A1 RID: 417
		private GroupCapacity[] QueryCapacityResultField;
	}
}
