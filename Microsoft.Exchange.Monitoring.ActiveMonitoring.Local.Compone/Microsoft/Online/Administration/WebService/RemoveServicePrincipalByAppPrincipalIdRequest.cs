using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002F9 RID: 761
	[DebuggerStepThrough]
	[DataContract(Name = "RemoveServicePrincipalByAppPrincipalIdRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class RemoveServicePrincipalByAppPrincipalIdRequest : Request
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x0008B26B File Offset: 0x0008946B
		// (set) Token: 0x060014C6 RID: 5318 RVA: 0x0008B273 File Offset: 0x00089473
		[DataMember]
		public Guid AppPrincipalId
		{
			get
			{
				return this.AppPrincipalIdField;
			}
			set
			{
				this.AppPrincipalIdField = value;
			}
		}

		// Token: 0x04000F81 RID: 3969
		private Guid AppPrincipalIdField;
	}
}
