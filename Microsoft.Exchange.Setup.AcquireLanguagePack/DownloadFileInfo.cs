using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000006 RID: 6
	internal sealed class DownloadFileInfo
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000022A1 File Offset: 0x000004A1
		public DownloadFileInfo(string uriLink) : this(uriLink, null, false)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022AC File Offset: 0x000004AC
		public DownloadFileInfo(string uriLink, string filePattern, bool ignoreInvalidFileName)
		{
			ValidationHelper.ThrowIfNull(uriLink, "uriLink");
			this.UriLink = new Uri(uriLink);
			this.FilePattern = filePattern;
			this.IgnoreInvalidFileName = ignoreInvalidFileName;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022D9 File Offset: 0x000004D9
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000022E1 File Offset: 0x000004E1
		public Uri UriLink { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022EA File Offset: 0x000004EA
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000022F2 File Offset: 0x000004F2
		public string FilePattern { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022FB File Offset: 0x000004FB
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002303 File Offset: 0x00000503
		public bool IgnoreInvalidFileName { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000230C File Offset: 0x0000050C
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002314 File Offset: 0x00000514
		public long FileSize { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000231D File Offset: 0x0000051D
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002325 File Offset: 0x00000525
		public string FilePath { get; set; }

		// Token: 0x06000018 RID: 24 RVA: 0x00002330 File Offset: 0x00000530
		public static bool IsFileNameValid(string filePath, string filePattern)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				return false;
			}
			string fileName = Path.GetFileName(filePath);
			return !string.IsNullOrEmpty(fileName) && (string.IsNullOrEmpty(filePattern) || Regex.IsMatch(fileName.ToLower(), filePattern));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000236E File Offset: 0x0000056E
		public bool IsFileNameValid()
		{
			return DownloadFileInfo.IsFileNameValid(this.FilePath, this.FilePattern);
		}
	}
}
