using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000374 RID: 884
	// (Invoke) Token: 0x0600263D RID: 9789
	internal delegate bool ShouldSuppressResubmission(IEnumerable<INextHopServer> relatedBridgeheads);
}
