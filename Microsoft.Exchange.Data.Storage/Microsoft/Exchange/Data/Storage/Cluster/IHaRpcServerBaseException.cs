using System;

namespace Microsoft.Exchange.Data.Storage.Cluster
{
	// Token: 0x020000BF RID: 191
	public interface IHaRpcServerBaseException
	{
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600122E RID: 4654
		string ErrorMessage { get; }

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600122F RID: 4655
		string OriginatingServer { get; }

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001230 RID: 4656
		string OriginatingStackTrace { get; }

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001231 RID: 4657
		string StackTrace { get; }

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001232 RID: 4658
		string DatabaseName { get; }

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001233 RID: 4659
		Exception InnerException { get; }
	}
}
