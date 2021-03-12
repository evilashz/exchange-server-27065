using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000320 RID: 800
	[DebuggerStepThrough]
	[DataContract(Name = "ListServicePrincipalCredentialsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ListServicePrincipalCredentialsResponse : Response
	{
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x0008B6F5 File Offset: 0x000898F5
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x0008B6FD File Offset: 0x000898FD
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

		// Token: 0x04000FB3 RID: 4019
		private ServicePrincipalCredential[] ReturnValueField;
	}
}
