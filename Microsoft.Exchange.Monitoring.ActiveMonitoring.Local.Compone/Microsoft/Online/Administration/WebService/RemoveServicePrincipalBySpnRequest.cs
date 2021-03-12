using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000319 RID: 793
	[DataContract(Name = "RemoveServicePrincipalBySpnRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class RemoveServicePrincipalBySpnRequest : Request
	{
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x0008B646 File Offset: 0x00089846
		// (set) Token: 0x0600153C RID: 5436 RVA: 0x0008B64E File Offset: 0x0008984E
		[DataMember]
		public string ServicePrincipalName
		{
			get
			{
				return this.ServicePrincipalNameField;
			}
			set
			{
				this.ServicePrincipalNameField = value;
			}
		}

		// Token: 0x04000FAC RID: 4012
		private string ServicePrincipalNameField;
	}
}
