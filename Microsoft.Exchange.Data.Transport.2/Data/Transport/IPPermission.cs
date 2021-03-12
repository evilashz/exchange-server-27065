using System;
using System.Net;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000072 RID: 114
	public abstract class IPPermission
	{
		// Token: 0x06000250 RID: 592 RVA: 0x000068F9 File Offset: 0x00004AF9
		internal IPPermission()
		{
		}

		// Token: 0x06000251 RID: 593
		public abstract void AddRestriction(IPAddress ipAddress, TimeSpan expiration, string comments);

		// Token: 0x06000252 RID: 594
		public abstract PermissionCheckResults Check(IPAddress ipAddress);
	}
}
