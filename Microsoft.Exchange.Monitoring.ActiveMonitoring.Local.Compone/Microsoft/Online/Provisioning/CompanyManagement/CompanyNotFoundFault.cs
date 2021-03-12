using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Provisioning.CompanyManagement
{
	// Token: 0x020002A3 RID: 675
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "CompanyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	[DebuggerStepThrough]
	public class CompanyNotFoundFault : CompanyManagementFault
	{
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x000859F0 File Offset: 0x00083BF0
		// (set) Token: 0x0600132E RID: 4910 RVA: 0x000859F8 File Offset: 0x00083BF8
		[DataMember(IsRequired = true, EmitDefaultValue = false)]
		public Guid ContextId
		{
			get
			{
				return this.ContextIdField;
			}
			set
			{
				this.ContextIdField = value;
			}
		}

		// Token: 0x04000E87 RID: 3719
		private Guid ContextIdField;
	}
}
