using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000B9 RID: 185
	[ImmutableObject(true)]
	[Serializable]
	public abstract class ProxyAddressPrefix
	{
		// Token: 0x060004CE RID: 1230 RVA: 0x00011185 File Offset: 0x0000F385
		protected ProxyAddressPrefix(string prefixString)
		{
			ProxyAddressPrefix.CheckPrefixString(prefixString, true);
			this.primaryPrefix = prefixString.ToUpperInvariant();
			this.secondaryPrefix = prefixString.ToLowerInvariant();
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000111B0 File Offset: 0x0000F3B0
		private static bool CheckPrefixString(string prefixString, bool canThrow)
		{
			Exception ex;
			if (prefixString == null)
			{
				ex = new ArgumentNullException("prefixString");
			}
			else if (-1 != prefixString.IndexOf(':'))
			{
				ex = new ArgumentException(DataStrings.ColonPrefix, "prefixString");
			}
			else if (prefixString.Length != 0 && string.IsNullOrEmpty(prefixString.Trim()))
			{
				ex = new ArgumentOutOfRangeException(DataStrings.ProxyAddressPrefixShouldNotBeAllSpace, null);
			}
			else if (prefixString.Length > 9)
			{
				ex = new ArgumentOutOfRangeException(DataStrings.ProxyAddressPrefixTooLong, null);
			}
			else if (!ProxyAddressPrefix.asciiRegex.IsMatch(prefixString))
			{
				ex = new ArgumentOutOfRangeException(DataStrings.ConstraintViolationStringDoesContainsNonASCIICharacter(prefixString), null);
			}
			else
			{
				ex = null;
			}
			if (ex != null && canThrow)
			{
				throw ex;
			}
			return ex == null;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00011266 File Offset: 0x0000F466
		public static bool IsPrefixStringValid(string prefixString)
		{
			return ProxyAddressPrefix.CheckPrefixString(prefixString, false);
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001126F File Offset: 0x0000F46F
		public virtual string DisplayName
		{
			get
			{
				return this.PrimaryPrefix;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00011277 File Offset: 0x0000F477
		public string PrimaryPrefix
		{
			get
			{
				return this.primaryPrefix;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0001127F File Offset: 0x0000F47F
		public string SecondaryPrefix
		{
			get
			{
				return this.secondaryPrefix;
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00011287 File Offset: 0x0000F487
		public sealed override string ToString()
		{
			return this.PrimaryPrefix;
		}

		// Token: 0x060004D5 RID: 1237
		public abstract ProxyAddress GetProxyAddress(string address, bool isPrimaryAddress);

		// Token: 0x060004D6 RID: 1238
		public abstract ProxyAddressTemplate GetProxyAddressTemplate(string addressTemplate, bool isPrimaryAddressTemplate);

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001128F File Offset: 0x0000F48F
		public sealed override int GetHashCode()
		{
			return this.PrimaryPrefix.GetHashCode();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001129C File Offset: 0x0000F49C
		public sealed override bool Equals(object obj)
		{
			return this == obj as ProxyAddressPrefix;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000112AA File Offset: 0x0000F4AA
		public static bool operator ==(ProxyAddressPrefix a, ProxyAddressPrefix b)
		{
			return a == b || (a != null && b != null && a.PrimaryPrefix == b.PrimaryPrefix);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000112CB File Offset: 0x0000F4CB
		public static bool operator !=(ProxyAddressPrefix a, ProxyAddressPrefix b)
		{
			return !(a == b);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000112D8 File Offset: 0x0000F4D8
		public static ProxyAddressPrefix GetPrefix(string prefixString)
		{
			if (prefixString == null)
			{
				throw new ArgumentNullException("prefixString");
			}
			ProxyAddressPrefix result;
			if (!ProxyAddressPrefix.standardPrefixes.TryGetValue(prefixString, out result))
			{
				result = new CustomProxyAddressPrefix(prefixString);
			}
			return result;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001130A File Offset: 0x0000F50A
		public static ProxyAddressPrefix GetCustomProxyAddressPrefix()
		{
			return new CustomProxyAddressPrefix("", DataStrings.CustomProxyAddressPrefixDisplayName);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00011320 File Offset: 0x0000F520
		public static ProxyAddressPrefix[] GetStandardPrefixes()
		{
			ProxyAddressPrefix[] array = new ProxyAddressPrefix[ProxyAddressPrefix.standardPrefixes.Count + 1];
			ProxyAddressPrefix.standardPrefixes.Values.CopyTo(array, 0);
			array[array.Length - 1] = ProxyAddressPrefix.GetCustomProxyAddressPrefix();
			return array;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00011360 File Offset: 0x0000F560
		static ProxyAddressPrefix()
		{
			ProxyAddressPrefix.standardPrefixes = new Dictionary<string, ProxyAddressPrefix>(8, StringComparer.OrdinalIgnoreCase);
			ProxyAddressPrefix.standardPrefixes.Add(ProxyAddressPrefix.Smtp.PrimaryPrefix, ProxyAddressPrefix.Smtp);
			ProxyAddressPrefix.standardPrefixes.Add(ProxyAddressPrefix.LegacyDN.PrimaryPrefix, ProxyAddressPrefix.LegacyDN);
			ProxyAddressPrefix.standardPrefixes.Add(ProxyAddressPrefix.X500.PrimaryPrefix, ProxyAddressPrefix.X500);
			ProxyAddressPrefix.standardPrefixes.Add(ProxyAddressPrefix.X400.PrimaryPrefix, ProxyAddressPrefix.X400);
			ProxyAddressPrefix.standardPrefixes.Add(ProxyAddressPrefix.MsMail.PrimaryPrefix, ProxyAddressPrefix.MsMail);
			ProxyAddressPrefix.standardPrefixes.Add(ProxyAddressPrefix.CcMail.PrimaryPrefix, ProxyAddressPrefix.CcMail);
			ProxyAddressPrefix.standardPrefixes.Add(ProxyAddressPrefix.Notes.PrimaryPrefix, ProxyAddressPrefix.Notes);
			ProxyAddressPrefix.standardPrefixes.Add(ProxyAddressPrefix.GroupWise.PrimaryPrefix, ProxyAddressPrefix.GroupWise);
			ProxyAddressPrefix.standardPrefixes.Add(ProxyAddressPrefix.UM.PrimaryPrefix, ProxyAddressPrefix.UM);
			ProxyAddressPrefix.standardPrefixes.Add(ProxyAddressPrefix.Meum.PrimaryPrefix, ProxyAddressPrefix.Meum);
			ProxyAddressPrefix.standardPrefixes.Add(ProxyAddressPrefix.Invalid.PrimaryPrefix, ProxyAddressPrefix.Invalid);
			ProxyAddressPrefix.standardPrefixes.Add(ProxyAddressPrefix.JRNL.primaryPrefix, ProxyAddressPrefix.JRNL);
		}

		// Token: 0x040002EC RID: 748
		public const int MaxLength = 9;

		// Token: 0x040002ED RID: 749
		public const int MaxAddressTypeLength = 64;

		// Token: 0x040002EE RID: 750
		public const string AllowedCharacters = "[^\u0080-￿]";

		// Token: 0x040002EF RID: 751
		internal const string SipPrefix = "sip:";

		// Token: 0x040002F0 RID: 752
		private static readonly Regex asciiRegex = new Regex(string.Format("^{0}*$", "[^\u0080-￿]"), RegexOptions.Compiled);

		// Token: 0x040002F1 RID: 753
		private readonly string primaryPrefix;

		// Token: 0x040002F2 RID: 754
		private readonly string secondaryPrefix;

		// Token: 0x040002F3 RID: 755
		private static readonly Dictionary<string, ProxyAddressPrefix> standardPrefixes;

		// Token: 0x040002F4 RID: 756
		public static readonly ProxyAddressPrefix Smtp = new SmtpProxyAddressPrefix();

		// Token: 0x040002F5 RID: 757
		public static readonly ProxyAddressPrefix LegacyDN = new CustomProxyAddressPrefix("EX", DataStrings.LegacyDNProxyAddressPrefixDisplayName);

		// Token: 0x040002F6 RID: 758
		public static readonly ProxyAddressPrefix X500 = new CustomProxyAddressPrefix("X500");

		// Token: 0x040002F7 RID: 759
		public static readonly ProxyAddressPrefix X400 = new X400ProxyAddressPrefix();

		// Token: 0x040002F8 RID: 760
		public static readonly ProxyAddressPrefix MsMail = new CustomProxyAddressPrefix("MS", DataStrings.MsMailProxyAddressPrefixDisplayName);

		// Token: 0x040002F9 RID: 761
		public static readonly ProxyAddressPrefix CcMail = new CustomProxyAddressPrefix("CCMAIL", DataStrings.CcMailProxyAddressPrefixDisplayName);

		// Token: 0x040002FA RID: 762
		public static readonly ProxyAddressPrefix Notes = new CustomProxyAddressPrefix("NOTES", DataStrings.NotesProxyAddressPrefixDisplayName);

		// Token: 0x040002FB RID: 763
		public static readonly ProxyAddressPrefix GroupWise = new CustomProxyAddressPrefix("GWISE", DataStrings.GroupWiseProxyAddressPrefixDisplayName);

		// Token: 0x040002FC RID: 764
		public static readonly ProxyAddressPrefix UM = new EumProxyAddressPrefix();

		// Token: 0x040002FD RID: 765
		public static readonly ProxyAddressPrefix ASUM = new CustomProxyAddressPrefix("ASUM", DataStrings.AirSyncProxyAddressPrefixDisplayName);

		// Token: 0x040002FE RID: 766
		public static readonly ProxyAddressPrefix Meum = new MeumProxyAddressPrefix();

		// Token: 0x040002FF RID: 767
		public static readonly ProxyAddressPrefix SIP = new CustomProxyAddressPrefix("SIP");

		// Token: 0x04000300 RID: 768
		public static readonly ProxyAddressPrefix Invalid = new CustomProxyAddressPrefix("INVALID");

		// Token: 0x04000301 RID: 769
		public static readonly ProxyAddressPrefix JRNL = new CustomProxyAddressPrefix("JRNL");
	}
}
