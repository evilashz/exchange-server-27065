using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000299 RID: 665
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AddImGroup
	{
		// Token: 0x060011B2 RID: 4530 RVA: 0x00055C9C File Offset: 0x00053E9C
		public AddImGroup(IMailboxSession session, string displayName, IXSOFactory xsoFactory, IUnifiedContactStoreConfiguration configuration)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (displayName == null)
			{
				throw new ArgumentNullException("displayName");
			}
			if (displayName.Length == 0)
			{
				throw new ArgumentException("displayName that was passed in was empty.", "displayName");
			}
			if (xsoFactory == null)
			{
				throw new ArgumentNullException("xsoFactory");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.unifiedContactStoreUtilities = new UnifiedContactStoreUtilities(session, xsoFactory, configuration);
			this.displayName = displayName;
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00055D16 File Offset: 0x00053F16
		public RawImGroup Execute()
		{
			return this.unifiedContactStoreUtilities.GetUserImGroupWith(this.displayName);
		}

		// Token: 0x04000CC5 RID: 3269
		private readonly UnifiedContactStoreUtilities unifiedContactStoreUtilities;

		// Token: 0x04000CC6 RID: 3270
		private readonly string displayName;
	}
}
