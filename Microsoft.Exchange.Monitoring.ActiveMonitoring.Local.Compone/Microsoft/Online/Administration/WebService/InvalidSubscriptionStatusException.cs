using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000395 RID: 917
	[DataContract(Name = "InvalidSubscriptionStatusException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class InvalidSubscriptionStatusException : InvalidUserLicenseException
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0008C0FD File Offset: 0x0008A2FD
		// (set) Token: 0x06001686 RID: 5766 RVA: 0x0008C105 File Offset: 0x0008A305
		[DataMember]
		public SubscriptionStatus? SubscriptionStatus
		{
			get
			{
				return this.SubscriptionStatusField;
			}
			set
			{
				this.SubscriptionStatusField = value;
			}
		}

		// Token: 0x04001013 RID: 4115
		private SubscriptionStatus? SubscriptionStatusField;
	}
}
