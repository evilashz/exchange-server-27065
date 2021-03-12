using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200031D RID: 797
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetServicePrincipalBySpnResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class GetServicePrincipalBySpnResponse : Response
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x0008B6AA File Offset: 0x000898AA
		// (set) Token: 0x06001548 RID: 5448 RVA: 0x0008B6B2 File Offset: 0x000898B2
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

		// Token: 0x04000FB0 RID: 4016
		private ServicePrincipal ReturnValueField;
	}
}
