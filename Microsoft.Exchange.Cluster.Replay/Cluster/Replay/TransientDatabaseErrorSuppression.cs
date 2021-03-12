using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000234 RID: 564
	internal class TransientDatabaseErrorSuppression : TransientErrorSuppression<Guid>
	{
		// Token: 0x0600156B RID: 5483 RVA: 0x00055667 File Offset: 0x00053867
		protected override void InitializeTable()
		{
			this.m_errorTable = new Dictionary<Guid, TransientErrorInfo>(48);
		}
	}
}
