using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002D2 RID: 722
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ListServicePrincipalsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListServicePrincipalsRequest : Request
	{
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x0008ABC1 File Offset: 0x00088DC1
		// (set) Token: 0x060013FB RID: 5115 RVA: 0x0008ABC9 File Offset: 0x00088DC9
		[DataMember]
		public ServicePrincipalSearchDefinition SearchDefinition
		{
			get
			{
				return this.SearchDefinitionField;
			}
			set
			{
				this.SearchDefinitionField = value;
			}
		}

		// Token: 0x04000F2F RID: 3887
		private ServicePrincipalSearchDefinition SearchDefinitionField;
	}
}
