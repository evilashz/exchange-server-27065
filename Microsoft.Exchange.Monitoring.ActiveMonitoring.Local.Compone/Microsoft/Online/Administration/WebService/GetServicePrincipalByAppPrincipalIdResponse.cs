using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200031C RID: 796
	[DataContract(Name = "GetServicePrincipalByAppPrincipalIdResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetServicePrincipalByAppPrincipalIdResponse : Response
	{
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x0008B691 File Offset: 0x00089891
		// (set) Token: 0x06001545 RID: 5445 RVA: 0x0008B699 File Offset: 0x00089899
		[DataMember]
		public ServicePrincipal ReturnValue
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

		// Token: 0x04000FAF RID: 4015
		private ServicePrincipal ReturnValueField;
	}
}
