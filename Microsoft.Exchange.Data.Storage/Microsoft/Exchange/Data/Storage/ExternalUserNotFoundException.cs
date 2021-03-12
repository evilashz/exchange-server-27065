using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200072C RID: 1836
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExternalUserNotFoundException : StoragePermanentException
	{
		// Token: 0x060047D1 RID: 18385 RVA: 0x001305C0 File Offset: 0x0012E7C0
		internal ExternalUserNotFoundException(SecurityIdentifier sid) : base(ServerStrings.ExternalUserNotFound((sid == null) ? string.Empty : sid.ToString()))
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			this.sid = sid;
		}

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x060047D2 RID: 18386 RVA: 0x001305FE File Offset: 0x0012E7FE
		public SecurityIdentifier UserSecurityIdentifier
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x04002731 RID: 10033
		private SecurityIdentifier sid;
	}
}
