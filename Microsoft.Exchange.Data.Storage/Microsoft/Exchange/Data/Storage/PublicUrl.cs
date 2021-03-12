using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DAA RID: 3498
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicUrl : PublishingUrl
	{
		// Token: 0x06007823 RID: 30755 RVA: 0x002123B8 File Offset: 0x002105B8
		private PublicUrl(Uri uri, SharingDataType dataType, SmtpAddress smtpAddress, string folderName) : base(uri, dataType, PublicUrl.CalculateIdentity(dataType, folderName))
		{
			this.smtpAddress = smtpAddress;
			this.folderName = folderName;
		}

		// Token: 0x17002023 RID: 8227
		// (get) Token: 0x06007824 RID: 30756 RVA: 0x002123D9 File Offset: 0x002105D9
		public SmtpAddress SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x17002024 RID: 8228
		// (get) Token: 0x06007825 RID: 30757 RVA: 0x002123E4 File Offset: 0x002105E4
		public override string Domain
		{
			get
			{
				return this.smtpAddress.Domain;
			}
		}

		// Token: 0x17002025 RID: 8229
		// (get) Token: 0x06007826 RID: 30758 RVA: 0x002123FF File Offset: 0x002105FF
		public string FolderName
		{
			get
			{
				return this.folderName;
			}
		}

		// Token: 0x17002026 RID: 8230
		// (get) Token: 0x06007827 RID: 30759 RVA: 0x00212408 File Offset: 0x00210608
		internal override string TraceInfo
		{
			get
			{
				return string.Concat(new string[]
				{
					"DataType=",
					base.DataType.ToString(),
					"; SmtpAddress=",
					this.SmtpAddress.ToString(),
					"; FolderName=",
					this.FolderName,
					"; Identity=",
					base.Identity
				});
			}
		}

		// Token: 0x06007828 RID: 30760 RVA: 0x0021247C File Offset: 0x0021067C
		public static PublicUrl Create(string externalUrl, SharingDataType dataType, SmtpAddress smtpAddress, string folderName, SharingAnonymousIdentityCollection sharingAnonymousIdentities)
		{
			Util.ThrowOnNullOrEmptyArgument(externalUrl, "externalUrl");
			Util.ThrowOnNullArgument(dataType, "dataType");
			Util.ThrowOnNullOrEmptyArgument(dataType.ExternalName, "dataType.ExternalName");
			Util.ThrowOnNullOrEmptyArgument(folderName, "folderName");
			string text = string.Join(string.Empty, folderName.Split(".*$&+,/:;=?@\"\\<>#%{}|\\^~[]`".ToCharArray()));
			if (string.IsNullOrEmpty(text))
			{
				text = "MyCalendar";
			}
			text = text.Replace(" ", "_");
			if (sharingAnonymousIdentities != null)
			{
				string text2 = text;
				int num = 0;
				for (;;)
				{
					string urlId = PublicUrl.CalculateIdentity(dataType, text2);
					if (!sharingAnonymousIdentities.Contains(urlId))
					{
						goto IL_CD;
					}
					if (++num > 50)
					{
						break;
					}
					ExTraceGlobals.SharingTracer.TraceDebug<string, int>(0L, "PublicUrl.Create(): {0} has been used in Sharing Anonymous Identities - Appending post fix: {1}.", text, num);
					text2 = string.Format("{0}({1})", text, num);
				}
				throw new CannotShareFolderException(ServerStrings.ExTooManyObjects("PublicUrl", num, 50));
				IL_CD:
				text = text2;
			}
			string uriString = string.Format("{0}/{1}/{2}/{3}/{1}", new object[]
			{
				externalUrl.TrimEnd(new char[]
				{
					'/'
				}),
				dataType.ExternalName,
				smtpAddress.ToString(),
				text
			});
			PublicUrl publicUrl = new PublicUrl(new Uri(uriString, UriKind.Absolute), dataType, smtpAddress, text);
			ExTraceGlobals.SharingTracer.TraceDebug<PublicUrl, string>(0L, "PublicUrl.Create(): Created an instance of PublicUrl: {0} - {1}.", publicUrl, publicUrl.TraceInfo);
			return publicUrl;
		}

		// Token: 0x06007829 RID: 30761 RVA: 0x002125D8 File Offset: 0x002107D8
		internal static bool TryParse(string url, out PublicUrl publicUrl)
		{
			publicUrl = null;
			Uri uri = null;
			if (!PublishingUrl.IsAbsoluteUriString(url, out uri))
			{
				ExTraceGlobals.SharingTracer.TraceError<string>(0L, "PublicUrl.TryParse(): The string '{0}' is not an valid Uri.", url);
				return false;
			}
			string localPath = uri.LocalPath;
			ExTraceGlobals.SharingTracer.TraceDebug<string>(0L, "PublicUrl.TryParse(): Get path of url: {0}", localPath);
			Match match = PublicUrl.PublicUrlRegex.Match(localPath);
			if (!match.Success)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<string>(0L, "PublicUrl.TryParse(): The string '{0}' is not PublicUrl.", url);
				return false;
			}
			SharingDataType dataType = SharingDataType.FromExternalName(match.Result("${datatype}"));
			string text = match.Result("${address}");
			if (!SmtpAddress.IsValidSmtpAddress(text))
			{
				ExTraceGlobals.SharingTracer.TraceDebug<string>(0L, "PublicUrl.TryParse(): {0} is not valid SMTP address.", text);
				return false;
			}
			publicUrl = new PublicUrl(uri, dataType, new SmtpAddress(text), match.Result("${name}"));
			ExTraceGlobals.SharingTracer.TraceDebug<string, string>(0L, "PublicUrl.TryParse(): The url {0} is parsed as PublicUrl {1}.", url, publicUrl.TraceInfo);
			return true;
		}

		// Token: 0x0600782A RID: 30762 RVA: 0x002126BA File Offset: 0x002108BA
		private static string CalculateIdentity(SharingDataType dataType, string folderName)
		{
			Util.ThrowOnNullArgument(dataType, "dataType");
			Util.ThrowOnNullOrEmptyArgument(folderName, "folderName");
			return dataType.PublishResourceName + "\\" + Uri.EscapeDataString(folderName);
		}

		// Token: 0x0600782B RID: 30763 RVA: 0x002126E8 File Offset: 0x002108E8
		internal override SharingAnonymousIdentityCacheKey CreateKey()
		{
			return new PublicUrlKey(this);
		}

		// Token: 0x04005327 RID: 21287
		private const string PublicUrlFormat = "{0}/{1}/{2}/{3}/{1}";

		// Token: 0x04005328 RID: 21288
		private const string SpecialCharacters = ".*";

		// Token: 0x04005329 RID: 21289
		private const string ReservedCharacters = "$&+,/:;=?@";

		// Token: 0x0400532A RID: 21290
		private const string UnsafeCharacters = "\"\\<>#%{}|\\^~[]`";

		// Token: 0x0400532B RID: 21291
		private const string RemoveCharacters = ".*$&+,/:;=?@\"\\<>#%{}|\\^~[]`";

		// Token: 0x0400532C RID: 21292
		private const string DefaultFolderName = "MyCalendar";

		// Token: 0x0400532D RID: 21293
		private const string FolderNameWithPostfix = "{0}({1})";

		// Token: 0x0400532E RID: 21294
		private const int MaxNumberForPostFix = 50;

		// Token: 0x0400532F RID: 21295
		private static readonly Regex PublicUrlRegex = new Regex("^/owa/(?<datatype>calendar|contacts)/(?<address>[^/ ]+)/(?<name>[^/ ]+)/(calendar|contacts)(.html|.ics)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04005330 RID: 21296
		private readonly SmtpAddress smtpAddress;

		// Token: 0x04005331 RID: 21297
		private readonly string folderName;
	}
}
