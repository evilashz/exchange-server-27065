using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000221 RID: 545
	internal class DatabaseConnectedPassiveChecks : DatabaseValidationMultiChecks
	{
		// Token: 0x06001491 RID: 5265 RVA: 0x00052985 File Offset: 0x00050B85
		protected override void DefineChecks()
		{
			base.DefineChecks();
			base.AddCheck(new DatabaseCheckPassiveConnected());
		}
	}
}
