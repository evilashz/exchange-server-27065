using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200031F RID: 799
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "NavigateListServicePrincipalsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class NavigateListServicePrincipalsResponse : Response
	{
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x0008B6DC File Offset: 0x000898DC
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x0008B6E4 File Offset: 0x000898E4
		[DataMember]
		public ListServicePrincipalResults ReturnValue
		{
			get
			{
				return this.ReturnValueField;
			}
			set
			{
				this.ReturnValueField = value;
			}
		}

		// Token: 0x04000FB2 RID: 4018
		private ListServicePrincipalResults ReturnValueField;
	}
}
