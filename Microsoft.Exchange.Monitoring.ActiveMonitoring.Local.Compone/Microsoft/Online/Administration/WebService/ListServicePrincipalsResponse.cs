using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200031E RID: 798
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ListServicePrincipalsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListServicePrincipalsResponse : Response
	{
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0008B6C3 File Offset: 0x000898C3
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x0008B6CB File Offset: 0x000898CB
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

		// Token: 0x04000FB1 RID: 4017
		private ListServicePrincipalResults ReturnValueField;
	}
}
