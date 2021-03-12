using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000367 RID: 871
	[DataContract(Name = "UserAlreadyExistsException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class UserAlreadyExistsException : ObjectAlreadyExistsException
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x0008BE39 File Offset: 0x0008A039
		// (set) Token: 0x06001630 RID: 5680 RVA: 0x0008BE41 File Offset: 0x0008A041
		[DataMember]
		public bool UserCollisionInLive
		{
			get
			{
				return this.UserCollisionInLiveField;
			}
			set
			{
				this.UserCollisionInLiveField = value;
			}
		}

		// Token: 0x04000FFF RID: 4095
		private bool UserCollisionInLiveField;
	}
}
