using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000321 RID: 801
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListServicePrincipalCredentialsByAppPrincipalIdResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListServicePrincipalCredentialsByAppPrincipalIdResponse : Response
	{
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x0008B70E File Offset: 0x0008990E
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x0008B716 File Offset: 0x00089916
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

		// Token: 0x04000FB4 RID: 4020
		private ServicePrincipalCredential[] ReturnValueField;
	}
}
