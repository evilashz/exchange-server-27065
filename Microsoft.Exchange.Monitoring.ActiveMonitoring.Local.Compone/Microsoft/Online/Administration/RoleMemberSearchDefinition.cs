using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003BA RID: 954
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "RoleMemberSearchDefinition", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	public class RoleMemberSearchDefinition : SearchDefinition
	{
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0008C471 File Offset: 0x0008A671
		// (set) Token: 0x060016F0 RID: 5872 RVA: 0x0008C479 File Offset: 0x0008A679
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

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0008C482 File Offset: 0x0008A682
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x0008C48A File Offset: 0x0008A68A
		[DataMember]
		public Guid RoleObjectId
		{
			get
			{
				return this.RoleObjectIdField;
			}
			set
			{
				this.RoleObjectIdField = value;
			}
		}

		// Token: 0x04001043 RID: 4163
		private string[] IncludedPropertiesField;

		// Token: 0x04001044 RID: 4164
		private Guid RoleObjectIdField;
	}
}
