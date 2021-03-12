using System;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000043 RID: 67
	internal static class ClientSecurityContextFactory
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x00006D94 File Offset: 0x00004F94
		public static ClientSecurityContext Create(byte[] serialization, Action<Exception> onHandledException)
		{
			if (serialization == null)
			{
				throw new ArgumentNullException("serialization");
			}
			if (onHandledException == null)
			{
				throw new ArgumentNullException("onHandledException");
			}
			ClientSecurityContext result;
			using (BufferReader bufferReader = new BufferReader(new ArraySegment<byte>(serialization)))
			{
				AuthenticationContext authenticationContext = null;
				try
				{
					authenticationContext = AuthenticationContext.Parse(bufferReader);
				}
				catch (BufferParseException obj)
				{
					onHandledException(obj);
					return null;
				}
				result = ClientSecurityContextFactory.Create(authenticationContext, onHandledException);
			}
			return result;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006E14 File Offset: 0x00005014
		public static ClientSecurityContext Create(AuthenticationContext authenticationContext, Action<Exception> onHandledException)
		{
			if (authenticationContext == null)
			{
				throw new ArgumentNullException("authenticationContext");
			}
			if (onHandledException == null)
			{
				throw new ArgumentNullException("onHandledException");
			}
			if (authenticationContext.RegularGroups.Length == 0)
			{
				return null;
			}
			if (authenticationContext.PrimaryGroupIndex < 0 && !authenticationContext.UserSid.IsWellKnown(WellKnownSidType.LocalSystemSid))
			{
				return null;
			}
			ISecurityAccessTokenEx securityAccessToken = ClientSecurityContextFactory.Normalize(authenticationContext);
			ClientSecurityContext result = null;
			try
			{
				result = new ClientSecurityContext(securityAccessToken, AuthzFlags.AuthzSkipTokenGroups);
			}
			catch (AuthzException obj)
			{
				onHandledException(obj);
			}
			catch (InvalidSidException obj2)
			{
				onHandledException(obj2);
			}
			return result;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006EA8 File Offset: 0x000050A8
		private static ISecurityAccessTokenEx Normalize(AuthenticationContext authenticationContext)
		{
			return new SecurityAccessTokenEx
			{
				UserSid = authenticationContext.UserSid,
				GroupSids = ClientSecurityContextFactory.Normalize(authenticationContext.RegularGroups, authenticationContext.PrimaryGroupIndex),
				RestrictedGroupSids = ClientSecurityContextFactory.Normalize(authenticationContext.RestrictedGroups, 0)
			};
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006EF4 File Offset: 0x000050F4
		private static SidBinaryAndAttributes[] Normalize(SidBinaryAndAttributes[] input, int primaryGroupIndex)
		{
			if (input == null || input.Length == 0)
			{
				return null;
			}
			SidBinaryAndAttributes[] array = new SidBinaryAndAttributes[input.Length];
			int num = 0;
			SidBinaryAndAttributes sidBinaryAndAttributes = (primaryGroupIndex >= 0) ? ClientSecurityContextFactory.Normalize(input[primaryGroupIndex]) : null;
			if (sidBinaryAndAttributes != null)
			{
				array[num++] = sidBinaryAndAttributes;
			}
			for (int num2 = 0; num2 != input.Length; num2++)
			{
				if (num2 != primaryGroupIndex)
				{
					sidBinaryAndAttributes = ClientSecurityContextFactory.Normalize(input[num2]);
					if (sidBinaryAndAttributes != null)
					{
						array[num++] = sidBinaryAndAttributes;
					}
				}
			}
			if (num != input.Length)
			{
				Array.Resize<SidBinaryAndAttributes>(ref array, num);
			}
			return array;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006F68 File Offset: 0x00005168
		private static SidBinaryAndAttributes Normalize(SidBinaryAndAttributes input)
		{
			SecurityIdentifier securityIdentifier = input.SecurityIdentifier;
			if (securityIdentifier.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid))
			{
				return null;
			}
			GroupAttributes groupAttributes = (GroupAttributes)input.Attributes;
			groupAttributes &= (GroupAttributes.Enabled | GroupAttributes.UseForDenyOnly | GroupAttributes.Integrity | GroupAttributes.IntegrityEnabled | GroupAttributes.IntegrityEnabledDesktop);
			return new SidBinaryAndAttributes(securityIdentifier, (uint)groupAttributes);
		}
	}
}
