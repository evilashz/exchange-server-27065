using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Mapi.Security;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000628 RID: 1576
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class IdentityHelper
	{
		// Token: 0x060040F8 RID: 16632 RVA: 0x00111202 File Offset: 0x0010F402
		public static SecurityIdentifier SidFromAuxiliaryIdentity(GenericIdentity auxiliaryIdentity)
		{
			if (auxiliaryIdentity == null)
			{
				return null;
			}
			if (auxiliaryIdentity is GenericSidIdentity)
			{
				return ((GenericSidIdentity)auxiliaryIdentity).Sid;
			}
			return null;
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x00111220 File Offset: 0x0010F420
		public static SecurityIdentifier SidFromLogonIdentity(object identity)
		{
			Util.ThrowOnNullArgument(identity, "identity");
			SecurityIdentifier result = null;
			if (identity is WindowsIdentity)
			{
				result = ((WindowsIdentity)identity).User;
			}
			else if (identity is ClientIdentityInfo)
			{
				result = ((ClientIdentityInfo)identity).sidUser;
			}
			return result;
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x00111268 File Offset: 0x0010F468
		public static SecurityIdentifier GetEffectiveLogonSid(MailboxSession session)
		{
			Util.ThrowOnNullArgument(session, "session");
			SecurityIdentifier securityIdentifier = IdentityHelper.SidFromAuxiliaryIdentity(session.AuxiliaryIdentity);
			if (securityIdentifier == null)
			{
				securityIdentifier = IdentityHelper.SidFromLogonIdentity(session.Identity);
			}
			return securityIdentifier;
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x001112A4 File Offset: 0x0010F4A4
		public static IdentityPair GetIdentityPair(MailboxSession session)
		{
			Util.ThrowOnNullArgument(session, "session");
			IdentityPair result = default(IdentityPair);
			if (session.AuxiliaryIdentity != null)
			{
				if (session.AuxiliaryIdentity is GenericSidIdentity)
				{
					result.LogonUserSid = ((GenericSidIdentity)session.AuxiliaryIdentity).Sid.Value;
				}
				else
				{
					result.LogonUserDisplayName = session.AuxiliaryIdentity.Name;
				}
			}
			else
			{
				result.LogonUserSid = IdentityHelper.SidFromLogonIdentity(session.Identity).Value;
			}
			return result;
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x00111324 File Offset: 0x0010F524
		public static SecurityIdentifier CalculateEffectiveSid(SecurityIdentifier userSid, SecurityIdentifier masterAccountSid)
		{
			SecurityIdentifier result;
			if (masterAccountSid == null)
			{
				result = userSid;
			}
			else if (masterAccountSid.IsWellKnown(WellKnownSidType.SelfSid))
			{
				result = userSid;
			}
			else
			{
				result = masterAccountSid;
			}
			return result;
		}
	}
}
