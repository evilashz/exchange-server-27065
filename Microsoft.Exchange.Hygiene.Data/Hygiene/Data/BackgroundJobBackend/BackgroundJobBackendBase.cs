using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x0200002C RID: 44
	internal abstract class BackgroundJobBackendBase : ConfigurablePropertyBag
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00005EF7 File Offset: 0x000040F7
		public override ObjectId Identity
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
