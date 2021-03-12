using System;
using System.Security.Principal;
using System.Text;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public class SecurityAccessToken : ISecurityAccessToken
	{
		// Token: 0x0600010E RID: 270 RVA: 0x0000600A File Offset: 0x0000420A
		public SecurityAccessToken()
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006012 File Offset: 0x00004212
		public SecurityAccessToken(ISecurityAccessToken securityAccessToken)
		{
			if (securityAccessToken == null)
			{
				throw new ArgumentNullException("securityAccessToken");
			}
			this.UserSid = securityAccessToken.UserSid;
			this.GroupSids = securityAccessToken.GroupSids;
			this.RestrictedGroupSids = securityAccessToken.RestrictedGroupSids;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000604C File Offset: 0x0000424C
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00006054 File Offset: 0x00004254
		public string UserSid { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000605D File Offset: 0x0000425D
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00006065 File Offset: 0x00004265
		public SidStringAndAttributes[] GroupSids { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000606E File Offset: 0x0000426E
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00006076 File Offset: 0x00004276
		public SidStringAndAttributes[] RestrictedGroupSids { get; set; }

		// Token: 0x06000116 RID: 278 RVA: 0x0000607F File Offset: 0x0000427F
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006088 File Offset: 0x00004288
		public string ToString(bool resolveIdentities)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.AppendLine("UserSid=" + SecurityAccessToken.SecurityIdentifierToString(this.UserSid, resolveIdentities));
			this.AddToString(stringBuilder, "GroupSid", this.GroupSids, resolveIdentities);
			this.AddToString(stringBuilder, "RestrictedGroupSid", this.RestrictedGroupSids, resolveIdentities);
			return stringBuilder.ToString();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000060EC File Offset: 0x000042EC
		private static string SecurityIdentifierToString(string sid, bool resolveIdentities)
		{
			if (sid == null)
			{
				return "<null>";
			}
			if (resolveIdentities)
			{
				try
				{
					SecurityIdentifier securityIdentifier = new SecurityIdentifier(sid);
					if (securityIdentifier.IsValidTargetType(typeof(NTAccount)))
					{
						NTAccount ntaccount = (NTAccount)securityIdentifier.Translate(typeof(NTAccount));
						return string.Concat(new object[]
						{
							sid,
							"(",
							ntaccount,
							")"
						});
					}
				}
				catch (ArgumentException)
				{
				}
				catch (IdentityNotMappedException)
				{
				}
				catch (SystemException)
				{
				}
				return sid;
			}
			return sid;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006194 File Offset: 0x00004394
		private void AddToString(StringBuilder token, string name, SidStringAndAttributes[] sidStringAndAttributesArray, bool resolveIdentities)
		{
			if (sidStringAndAttributesArray == null || sidStringAndAttributesArray.Length == 0)
			{
				token.AppendLine("<no " + name + ">");
				return;
			}
			foreach (SidStringAndAttributes sidStringAndAttributes in sidStringAndAttributesArray)
			{
				token.AppendLine(string.Concat(new object[]
				{
					name,
					"=",
					SecurityAccessToken.SecurityIdentifierToString(sidStringAndAttributes.SecurityIdentifier, resolveIdentities),
					", Attributes=",
					sidStringAndAttributes.Attributes
				}));
			}
		}
	}
}
