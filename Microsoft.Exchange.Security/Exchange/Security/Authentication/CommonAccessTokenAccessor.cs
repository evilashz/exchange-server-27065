using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200003C RID: 60
	internal abstract class CommonAccessTokenAccessor
	{
		// Token: 0x06000192 RID: 402 RVA: 0x0000C686 File Offset: 0x0000A886
		protected CommonAccessTokenAccessor()
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000C68E File Offset: 0x0000A88E
		protected CommonAccessTokenAccessor(CommonAccessToken token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			this.CheckTokenType(token);
			this.Token = token;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000194 RID: 404
		public abstract AccessTokenType TokenType { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000C6B2 File Offset: 0x0000A8B2
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000C6BA File Offset: 0x0000A8BA
		protected CommonAccessToken Token { get; set; }

		// Token: 0x06000197 RID: 407 RVA: 0x0000C6C3 File Offset: 0x0000A8C3
		public virtual CommonAccessToken GetToken()
		{
			return this.Token;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000C6CB File Offset: 0x0000A8CB
		public static string SerializeOrganizationId(OrganizationId organizationId)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			if (organizationId.OrganizationalUnit != null)
			{
				organizationId.EnsureFullyPopulated();
			}
			return Convert.ToBase64String(organizationId.GetBytes(Encoding.UTF8));
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000C700 File Offset: 0x0000A900
		public static OrganizationId DeserializeOrganizationId(string serializedString)
		{
			if (string.IsNullOrEmpty(serializedString))
			{
				throw new ArgumentNullException("serializedString");
			}
			OrganizationId result = null;
			byte[] bytes = Convert.FromBase64String(serializedString);
			if (!OrganizationId.TryCreateFromBytes(bytes, Encoding.UTF8, out result))
			{
				throw new FormatException("Invalid OrganizationId format.");
			}
			return result;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000C744 File Offset: 0x0000A944
		public static string SerializeGroupMembershipSids(IEnumerable<string> groupSids)
		{
			if (groupSids != null)
			{
				return string.Join(";", groupSids);
			}
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaServer.ShouldSkipAdfsGroupReadOnFrontend.Enabled)
			{
				return string.Empty;
			}
			throw new ArgumentNullException("groupSids");
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000C790 File Offset: 0x0000A990
		public static string SerializeIsPublicSession(bool isPublicSession)
		{
			return isPublicSession.ToString();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000C79C File Offset: 0x0000A99C
		public static IEnumerable<string> DeserializeGroupMembershipSids(string serializedString)
		{
			if (string.IsNullOrEmpty(serializedString))
			{
				throw new ArgumentNullException("serializedString");
			}
			return serializedString.Split(new string[]
			{
				";"
			}, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000C7D3 File Offset: 0x0000A9D3
		public static bool DeserializIsPublicSession(string serializedString)
		{
			if (string.IsNullOrEmpty(serializedString))
			{
				throw new ArgumentNullException("serializedString");
			}
			return bool.Parse(serializedString);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000C7EE File Offset: 0x0000A9EE
		protected void CheckTokenType(CommonAccessToken token)
		{
			if (!string.Equals(token.TokenType, this.TokenType.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException("token");
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000C81C File Offset: 0x0000AA1C
		protected string SafeGetValue(string key)
		{
			string result = null;
			this.Token.ExtensionData.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000C840 File Offset: 0x0000AA40
		protected void SafeSetValue(string key, string value)
		{
			this.Token.ExtensionData[key] = value;
		}

		// Token: 0x040001C1 RID: 449
		private const string GroupMembershipSidDelimiter = ";";
	}
}
