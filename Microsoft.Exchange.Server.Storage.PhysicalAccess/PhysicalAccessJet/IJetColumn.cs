using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x02000099 RID: 153
	internal interface IJetColumn : IColumn
	{
		// Token: 0x0600066F RID: 1647
		byte[] GetValueAsBytes(IJetSimpleQueryOperator cursor);
	}
}
