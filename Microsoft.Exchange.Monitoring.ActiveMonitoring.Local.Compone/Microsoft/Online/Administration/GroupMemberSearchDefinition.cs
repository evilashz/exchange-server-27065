using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003BF RID: 959
	[DataContract(Name = "GroupMemberSearchDefinition", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class GroupMemberSearchDefinition : SearchDefinition
	{
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x0008C675 File Offset: 0x0008A875
		// (set) Token: 0x0600172D RID: 5933 RVA: 0x0008C67D File Offset: 0x0008A87D
		[DataMember]
		public Guid GroupObjectId
		{
			get
			{
				return this.GroupObjectIdField;
			}
			set
			{
				this.GroupObjectIdField = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x0008C686 File Offset: 0x0008A886
		// (set) Token: 0x0600172F RID: 5935 RVA: 0x0008C68E File Offset: 0x0008A88E
		[DataMember]
		public string[] IncludedProperties
		{
			get
			{
				return this.IncludedPropertiesField;
			}
			set
			{
				this.IncludedPropertiesField = value;
			}
		}

		// Token: 0x0400105F RID: 4191
		private Guid GroupObjectIdField;

		// Token: 0x04001060 RID: 4192
		private string[] IncludedPropertiesField;
	}
}
