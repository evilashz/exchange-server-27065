using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200068F RID: 1679
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DelegateUserCollectionSaveResult
	{
		// Token: 0x060044C8 RID: 17608 RVA: 0x0012514D File Offset: 0x0012334D
		internal DelegateUserCollectionSaveResult()
		{
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x00125155 File Offset: 0x00123355
		internal DelegateUserCollectionSaveResult(Collection<KeyValuePair<DelegateSaveState, Exception>> problems)
		{
			this.problems = problems;
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x060044CA RID: 17610 RVA: 0x00125164 File Offset: 0x00123364
		public ICollection<KeyValuePair<DelegateSaveState, Exception>> Problems
		{
			get
			{
				return this.problems;
			}
		}

		// Token: 0x0400256E RID: 9582
		private Collection<KeyValuePair<DelegateSaveState, Exception>> problems;
	}
}
