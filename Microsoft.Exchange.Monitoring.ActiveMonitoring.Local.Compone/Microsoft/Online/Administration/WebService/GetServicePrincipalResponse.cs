using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200031B RID: 795
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetServicePrincipalResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class GetServicePrincipalResponse : Response
	{
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x0008B678 File Offset: 0x00089878
		// (set) Token: 0x06001542 RID: 5442 RVA: 0x0008B680 File Offset: 0x00089880
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

		// Token: 0x04000FAE RID: 4014
		private ServicePrincipal ReturnValueField;
	}
}
