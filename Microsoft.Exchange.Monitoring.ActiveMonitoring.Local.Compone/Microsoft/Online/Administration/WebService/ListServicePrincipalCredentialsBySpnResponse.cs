using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000322 RID: 802
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListServicePrincipalCredentialsBySpnResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class ListServicePrincipalCredentialsBySpnResponse : Response
	{
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0008B727 File Offset: 0x00089927
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x0008B72F File Offset: 0x0008992F
		[DataMember]
		public ServicePrincipalCredential[] ReturnValue
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

		// Token: 0x04000FB5 RID: 4021
		private ServicePrincipalCredential[] ReturnValueField;
	}
}
