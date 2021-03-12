using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000096 RID: 150
	internal class SecurityContextKey : IEquatable<SecurityContextKey>
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x00026E46 File Offset: 0x00025046
		public static bool operator ==(SecurityContextKey left, SecurityContextKey right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00026E4F File Offset: 0x0002504F
		public static bool operator !=(SecurityContextKey left, SecurityContextKey right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00026E5C File Offset: 0x0002505C
		public SecurityContextKey(ClientSecurityContext securityContext)
		{
			Globals.AssertRetail(securityContext != null, "SecurityContext can't be null.");
			this.primarySecurityIdentity = securityContext.UserSid;
			foreach (IdentityReference identityReference in securityContext.GetGroups())
			{
				SecurityIdentifier securityIdentifier = identityReference as SecurityIdentifier;
				if (!(securityIdentifier == null) && securityIdentifier.Value.StartsWith("S-1-8"))
				{
					if (!(this.secondarySecurityIdentity == null))
					{
						throw new StoreException((LID)52092U, ErrorCodeValue.NotSupported, "Security context contains more than one group SIDs.");
					}
					this.secondarySecurityIdentity = securityIdentifier;
				}
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00026F18 File Offset: 0x00025118
		public bool Equals(SecurityContextKey other)
		{
			return this.primarySecurityIdentity == other.primarySecurityIdentity && this.secondarySecurityIdentity == other.secondarySecurityIdentity;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00026F40 File Offset: 0x00025140
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecurityContextKey);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00026F50 File Offset: 0x00025150
		public override int GetHashCode()
		{
			return ((this.primarySecurityIdentity != null) ? this.primarySecurityIdentity.GetHashCode() : 0) * 397 ^ ((this.secondarySecurityIdentity != null) ? this.secondarySecurityIdentity.GetHashCode() : 0);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00026F9C File Offset: 0x0002519C
		public override string ToString()
		{
			return string.Format("pSID:{0} sSID:{1}", this.primarySecurityIdentity, this.secondarySecurityIdentity);
		}

		// Token: 0x04000323 RID: 803
		private const string GroupPrefix = "S-1-8";

		// Token: 0x04000324 RID: 804
		private readonly SecurityIdentifier primarySecurityIdentity;

		// Token: 0x04000325 RID: 805
		private readonly SecurityIdentifier secondarySecurityIdentity;
	}
}
