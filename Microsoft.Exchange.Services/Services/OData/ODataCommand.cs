using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E27 RID: 3623
	internal abstract class ODataCommand : DisposeTrackableBase
	{
		// Token: 0x06005D69 RID: 23913
		public abstract object Execute();
	}
}
