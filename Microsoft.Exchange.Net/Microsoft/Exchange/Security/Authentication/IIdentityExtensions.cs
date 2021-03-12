using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200063D RID: 1597
	internal static class IIdentityExtensions
	{
		// Token: 0x06001CEC RID: 7404 RVA: 0x0003419C File Offset: 0x0003239C
		public static string GetSafeName(this IIdentity identity, bool fallbackToSidForWinId = true)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			string result;
			try
			{
				result = identity.Name;
			}
			catch (SystemException)
			{
				WindowsIdentity windowsIdentity = identity as WindowsIdentity;
				if (windowsIdentity == null || !fallbackToSidForWinId)
				{
					throw;
				}
				result = windowsIdentity.User.ToString();
			}
			return result;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x000341F0 File Offset: 0x000323F0
		public static SecurityIdentifier GetSecurityIdentifier(this IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (identity is WindowsIdentity)
			{
				return ((WindowsIdentity)identity).User;
			}
			if (identity is ClientSecurityContextIdentity)
			{
				return ((ClientSecurityContextIdentity)identity).Sid;
			}
			if (identity is GenericIdentity)
			{
				return new SecurityIdentifier(identity.Name);
			}
			throw new NotSupportedException("GetSecurityIdentifier does not support " + identity.GetType().ToString());
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x00034264 File Offset: 0x00032464
		public static ClientSecurityContext CreateClientSecurityContext(this IIdentity identity, bool retainIdentity = true)
		{
			if (identity is WindowsIdentity)
			{
				return new ClientSecurityContext((WindowsIdentity)identity, retainIdentity);
			}
			if (identity is ClientSecurityContextIdentity)
			{
				return ((ClientSecurityContextIdentity)identity).CreateClientSecurityContext();
			}
			GenericSidIdentity genericSidIdentity = new GenericSidIdentity(identity.GetSafeName(true), identity.AuthenticationType, identity.GetSecurityIdentifier());
			return genericSidIdentity.CreateClientSecurityContext();
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x000342BC File Offset: 0x000324BC
		public static SerializedAccessToken GetAccessToken(this IIdentity identity)
		{
			if (identity is SerializedIdentity)
			{
				return ((SerializedIdentity)identity).AccessToken;
			}
			SerializedAccessToken result;
			using (ClientSecurityContext clientSecurityContext = identity.CreateClientSecurityContext(true))
			{
				result = new SerializedAccessToken(identity.GetSafeName(true), identity.AuthenticationType, clientSecurityContext);
			}
			return result;
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x00034320 File Offset: 0x00032520
		public static ICollection<SecurityIdentifier> GetGroupAccountsSIDs(this IIdentity identity)
		{
			IEnumerable<SecurityIdentifier> source = from sid in identity.GetGroupSIDs()
			where sid.IsAccountSid()
			select sid;
			return source.ToArray<SecurityIdentifier>();
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0003435C File Offset: 0x0003255C
		public static ICollection<SecurityIdentifier> GetGroupSIDs(this IIdentity identity)
		{
			SerializedAccessToken accessToken = identity.GetAccessToken();
			IEnumerable<SecurityIdentifier> sids = accessToken.GroupSids.GetSids();
			IEnumerable<SecurityIdentifier> sids2 = accessToken.RestrictedGroupSids.GetSids();
			return sids.Concat(sids2).ToArray<SecurityIdentifier>();
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0003451C File Offset: 0x0003271C
		private static IEnumerable<SecurityIdentifier> GetSids(this SidStringAndAttributes[] groups)
		{
			if (groups != null)
			{
				foreach (SidStringAndAttributes group in groups)
				{
					yield return new SecurityIdentifier(group.SecurityIdentifier);
				}
			}
			yield break;
		}
	}
}
