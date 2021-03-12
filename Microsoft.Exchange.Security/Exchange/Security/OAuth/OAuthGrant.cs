using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000D8 RID: 216
	public static class OAuthGrant
	{
		// Token: 0x0600075D RID: 1885 RVA: 0x00033C48 File Offset: 0x00031E48
		public static string[] ExtractKnownGrants(string scope)
		{
			if (string.IsNullOrEmpty(scope))
			{
				return new string[0];
			}
			return (from s in scope.Split(new char[]
			{
				' '
			}, StringSplitOptions.RemoveEmptyEntries)
			where OAuthGrant.knownGrants.Contains(s)
			select s).ToArray<string>();
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00033CBC File Offset: 0x00031EBC
		public static string[] ExtractKnownGrantsFromRole(string role)
		{
			if (string.IsNullOrEmpty(role))
			{
				return new string[0];
			}
			return (from s in role.Split(new char[]
			{
				' '
			}, StringSplitOptions.RemoveEmptyEntries)
			where OAuthGrant.knownGrants.Contains(s) || string.Equals(s, Constants.ClaimValues.FullAccess, StringComparison.OrdinalIgnoreCase)
			select s).ToArray<string>();
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00033D13 File Offset: 0x00031F13
		public static string[] KnownGrants
		{
			get
			{
				return OAuthGrant.knownGrants;
			}
		}

		// Token: 0x040006FB RID: 1787
		public const string UserImpersonation = "user_impersonation";

		// Token: 0x040006FC RID: 1788
		public const string MailRead = "Mail.Read";

		// Token: 0x040006FD RID: 1789
		public const string MailReadWrite = "Mail.Write";

		// Token: 0x040006FE RID: 1790
		public const string MailSend = "Mail.Send";

		// Token: 0x040006FF RID: 1791
		public const string CalendarsRead = "Calendars.Read";

		// Token: 0x04000700 RID: 1792
		public const string CalendarsReadWrite = "Calendars.Write";

		// Token: 0x04000701 RID: 1793
		public const string ContactsRead = "Contacts.Read";

		// Token: 0x04000702 RID: 1794
		public const string ContactsReadWrite = "Contacts.Write";

		// Token: 0x04000703 RID: 1795
		public const string EasAccessAsUserAll = "EAS.AccessAsUser.All";

		// Token: 0x04000704 RID: 1796
		public const string EwsAccessAsUserAll = "EWS.AccessAsUser.All";

		// Token: 0x04000705 RID: 1797
		private static readonly string[] knownGrants = (from fieldInfo in typeof(OAuthGrant).GetFields(BindingFlags.Static | BindingFlags.Public)
		select fieldInfo.GetValue(null)).Cast<string>().ToArray<string>();
	}
}
