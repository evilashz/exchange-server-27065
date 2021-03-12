using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public sealed class UncFileSharePath : LongPath
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000406C File Offset: 0x0000226C
		public string ShareName
		{
			get
			{
				return this.shareName;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004074 File Offset: 0x00002274
		public new static UncFileSharePath Parse(string pathName)
		{
			return (UncFileSharePath)LongPath.ParseInternal(pathName, new UncFileSharePath());
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004086 File Offset: 0x00002286
		public static bool TryParse(string path, out UncFileSharePath resultObject)
		{
			resultObject = (UncFileSharePath)LongPath.TryParseInternal(path, new UncFileSharePath());
			return null != resultObject;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000040A4 File Offset: 0x000022A4
		protected override bool ParseCore(string path, bool nothrow)
		{
			if (base.ParseCore(path, nothrow))
			{
				if (!base.IsUnc)
				{
					base.IsValid = false;
					if (!nothrow)
					{
						throw new ArgumentException(DataStrings.ErrorUncPathMustBeUncPath(path), "path");
					}
				}
				else
				{
					if (path.Length >= 260)
					{
						throw new PathTooLongException(DataStrings.ErrorUncPathTooLong(path));
					}
					Match match = UncFileSharePath.s_regexUncShare.Match(base.PathName);
					if (!match.Success)
					{
						base.IsValid = false;
						if (!nothrow)
						{
							throw new ArgumentException(DataStrings.ErrorUncPathMustBeUncPathOnly(path), "path");
						}
					}
					else
					{
						IPAddress ipaddress;
						if (IPAddress.TryParse(match.Groups[1].ToString(), out ipaddress))
						{
							base.IsValid = false;
							if (!nothrow)
							{
								throw new ArgumentException(DataStrings.ErrorUncPathMustUseServerName(path), "path");
							}
						}
						this.shareName = match.Groups[2].ToString();
					}
					match = UncFileSharePath.s_regexLeadingWhitespace.Match(base.PathName);
					bool success = match.Success;
					match = UncFileSharePath.s_regexTrailingWhitespace.Match(base.PathName);
					if (match.Success || success)
					{
						base.IsValid = false;
						if (!nothrow)
						{
							throw new ArgumentException(DataStrings.ConstraintViolationNoLeadingOrTrailingWhitespace, "path");
						}
					}
				}
			}
			return base.IsValid;
		}

		// Token: 0x04000039 RID: 57
		private const int MaxPath = 260;

		// Token: 0x0400003A RID: 58
		private static Regex s_regexUncShare = new Regex("^\\\\\\\\([^\\\\]+)\\\\([^\\\\]+)$", RegexOptions.CultureInvariant);

		// Token: 0x0400003B RID: 59
		private static Regex s_regexLeadingWhitespace = new Regex("^\\s+", RegexOptions.CultureInvariant);

		// Token: 0x0400003C RID: 60
		private static Regex s_regexTrailingWhitespace = new Regex("\\s+$", RegexOptions.CultureInvariant);

		// Token: 0x0400003D RID: 61
		private string shareName;
	}
}
