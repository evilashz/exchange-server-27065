using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200005A RID: 90
	public interface IConfigurableTableContents
	{
		// Token: 0x060003E7 RID: 999
		void Configure(bool backwards, StartStopKey startKey);
	}
}
