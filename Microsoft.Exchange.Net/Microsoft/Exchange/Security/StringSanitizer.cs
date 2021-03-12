using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000075 RID: 117
	internal static class StringSanitizer<SanitizingPolicy> where SanitizingPolicy : ISanitizingPolicy, new()
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x000111F8 File Offset: 0x0000F3F8
		public static bool TrustedStringsInitialized
		{
			get
			{
				return StringSanitizer<SanitizingPolicy>.trustedStrings.Count > 0;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00011207 File Offset: 0x0000F407
		public static SanitizingPolicy PolicyObject
		{
			get
			{
				return StringSanitizer<SanitizingPolicy>.policy;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00011210 File Offset: 0x0000F410
		public static bool InitializeTrustedStrings(params string[] assemblies)
		{
			bool flag = true;
			HashSet<string> hashSet = new HashSet<string>();
			for (int i = 0; i < assemblies.Length; i++)
			{
			}
			hashSet.Add(string.Empty);
			if (flag)
			{
				StringSanitizer<SanitizingPolicy>.trustedStrings = hashSet;
			}
			return flag;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001124C File Offset: 0x0000F44C
		public static void ExcludeStrings(params Regex[] regexes)
		{
			HashSet<string> hashSet = StringSanitizer<SanitizingPolicy>.trustedStrings;
			HashSet<string> hashSet2 = new HashSet<string>();
			foreach (string text in hashSet)
			{
				bool flag = true;
				foreach (Regex regex in regexes)
				{
					if (regex == null)
					{
						throw new ArgumentNullException("regexes");
					}
					if (regex.IsMatch(text))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					hashSet2.Add(text);
				}
			}
			hashSet2.Add(string.Empty);
			StringSanitizer<SanitizingPolicy>.trustedStrings = hashSet2;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000112F4 File Offset: 0x0000F4F4
		public static string Sanitize(string str)
		{
			SanitizingPolicy sanitizingPolicy = StringSanitizer<SanitizingPolicy>.policy;
			return sanitizingPolicy.Sanitize(str);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00011318 File Offset: 0x0000F518
		public static string SanitizeFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
			SanitizingPolicy sanitizingPolicy = StringSanitizer<SanitizingPolicy>.policy;
			return sanitizingPolicy.SanitizeFormat(formatProvider, format, args);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001133C File Offset: 0x0000F53C
		public static void Sanitize(TextWriter writer, string str)
		{
			SanitizingPolicy sanitizingPolicy = StringSanitizer<SanitizingPolicy>.policy;
			sanitizingPolicy.Sanitize(writer, str);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001135E File Offset: 0x0000F55E
		public static bool IsTrustedString(string str)
		{
			return object.ReferenceEquals(string.IsInterned(str), str);
		}

		// Token: 0x040001F7 RID: 503
		private static readonly SanitizingPolicy policy = (default(SanitizingPolicy) == null) ? Activator.CreateInstance<SanitizingPolicy>() : default(SanitizingPolicy);

		// Token: 0x040001F8 RID: 504
		private static HashSet<string> trustedStrings = new HashSet<string>();
	}
}
