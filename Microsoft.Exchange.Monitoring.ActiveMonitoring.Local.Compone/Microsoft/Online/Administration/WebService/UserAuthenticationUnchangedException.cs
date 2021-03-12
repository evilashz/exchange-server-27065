using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020003AD RID: 941
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UserAuthenticationUnchangedException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class UserAuthenticationUnchangedException : MsolAdministrationException
	{
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0008C2AB File Offset: 0x0008A4AB
		// (set) Token: 0x060016BA RID: 5818 RVA: 0x0008C2B3 File Offset: 0x0008A4B3
		[DataMember]
		public string ObjectKey
		{
			get
			{
				return this.ObjectKeyField;
			}
			set
			{
				this.ObjectKeyField = value;
			}
		}

		// Token: 0x04001021 RID: 4129
		private string ObjectKeyField;
	}
}
