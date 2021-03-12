using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200070C RID: 1804
	// (Invoke) Token: 0x060054F4 RID: 21748
	internal delegate IEnumerable<ExtendedSecurityPrincipal> ExtendedSecurityPrincipalSearcher(IConfigDataProvider session, ADObjectId rootId, QueryFilter targetFilter);
}
