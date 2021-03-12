using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200033A RID: 826
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "MsolConnectResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class MsolConnectResponse : Response
	{
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x0008BA29 File Offset: 0x00089C29
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x0008BA31 File Offset: 0x00089C31
		[DataMember]
		public bool UpdateAvailable
		{
			get
			{
				return this.UpdateAvailableField;
			}
			set
			{
				this.UpdateAvailableField = value;
			}
		}

		// Token: 0x04000FD7 RID: 4055
		private bool UpdateAvailableField;
	}
}
