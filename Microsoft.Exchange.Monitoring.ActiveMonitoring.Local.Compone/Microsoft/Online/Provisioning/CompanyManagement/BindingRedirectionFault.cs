using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Provisioning.CompanyManagement
{
	// Token: 0x020002A1 RID: 673
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "BindingRedirectionFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	public class BindingRedirectionFault : CompanyManagementFault
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x000859CF File Offset: 0x00083BCF
		// (set) Token: 0x0600132A RID: 4906 RVA: 0x000859D7 File Offset: 0x00083BD7
		[DataMember]
		public string Location
		{
			get
			{
				return this.LocationField;
			}
			set
			{
				this.LocationField = value;
			}
		}

		// Token: 0x04000E86 RID: 3718
		private string LocationField;
	}
}
