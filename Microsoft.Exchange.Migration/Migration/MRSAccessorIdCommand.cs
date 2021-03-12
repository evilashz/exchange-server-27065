using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000153 RID: 339
	internal class MRSAccessorIdCommand : MrsAccessorCommand
	{
		// Token: 0x060010D8 RID: 4312 RVA: 0x00047004 File Offset: 0x00045204
		public MRSAccessorIdCommand(string name, ICollection<Type> ignoreExceptions, ICollection<Type> transientExceptions, object identity) : base(name, ignoreExceptions, transientExceptions)
		{
			base.Identity = identity;
		}
	}
}
