using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000295 RID: 661
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AddDistributionGroupToImList
	{
		// Token: 0x060011A7 RID: 4519 RVA: 0x000559C8 File Offset: 0x00053BC8
		public AddDistributionGroupToImList(IMailboxSession session, string smtpAddress, string displayName, IXSOFactory xsoFactory, IUnifiedContactStoreConfiguration configuration)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			this.ThrowIfNullOrEmpty(smtpAddress, "smtpAddress");
			this.ThrowIfNullOrEmpty(displayName, "displayName");
			if (xsoFactory == null)
			{
				throw new ArgumentNullException("xsoFactory");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.unifiedContactStoreUtilities = new UnifiedContactStoreUtilities(session, xsoFactory, configuration);
			this.smtpAddress = smtpAddress;
			this.displayName = displayName;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00055A3D File Offset: 0x00053C3D
		public RawImGroup Execute()
		{
			return this.unifiedContactStoreUtilities.GetUserImDGWith(this.smtpAddress, this.displayName);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00055A56 File Offset: 0x00053C56
		private void ThrowIfNullOrEmpty(string parameterValue, string parameterName)
		{
			if (parameterValue == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (parameterValue.Length == 0)
			{
				throw new ArgumentException(parameterName + " was empty", parameterName);
			}
		}

		// Token: 0x04000CBC RID: 3260
		private readonly UnifiedContactStoreUtilities unifiedContactStoreUtilities;

		// Token: 0x04000CBD RID: 3261
		private readonly string smtpAddress;

		// Token: 0x04000CBE RID: 3262
		private readonly string displayName;
	}
}
