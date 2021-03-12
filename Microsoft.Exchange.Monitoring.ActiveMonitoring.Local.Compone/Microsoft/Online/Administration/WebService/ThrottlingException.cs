using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200038D RID: 909
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ThrottlingException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ThrottlingException : MsolAdministrationException
	{
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0008C024 File Offset: 0x0008A224
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x0008C02C File Offset: 0x0008A22C
		[DataMember]
		public int RetryWaitPeriod
		{
			get
			{
				return this.RetryWaitPeriodField;
			}
			set
			{
				this.RetryWaitPeriodField = value;
			}
		}

		// Token: 0x0400100A RID: 4106
		private int RetryWaitPeriodField;
	}
}
