using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000026 RID: 38
	internal class ApnsFeedbackFileId : IEquatable<ApnsFeedbackFileId>
	{
		// Token: 0x06000179 RID: 377 RVA: 0x000061C4 File Offset: 0x000043C4
		internal ApnsFeedbackFileId(ExDateTime date, string appId, string extension, string directory)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("appId", appId);
			ArgumentValidator.ThrowIfNullOrEmpty("extension", extension);
			this.Date = date;
			this.Directory = (directory ?? string.Empty);
			foreach (string text in ApnsFeedbackFileId.WellKnownExtensions)
			{
				if (extension.Equals(text, StringComparison.OrdinalIgnoreCase))
				{
					this.Extension = text;
					break;
				}
			}
			if (this.Extension == null)
			{
				throw new ApnsFeedbackException(Strings.ApnsFeedbackFileIdInvalidExtension(extension));
			}
			if (appId.Equals("feedback", StringComparison.OrdinalIgnoreCase))
			{
				if (!(this.Extension == "zip") && !(this.Extension == "metadata"))
				{
					throw new ApnsFeedbackException(Strings.ApnsFeedbackFileIdInvalidPseudoAppId(this.Extension));
				}
				this.AppId = "feedback";
			}
			else
			{
				this.AppId = appId;
			}
			if (this.Extension != "zip")
			{
				string packageExtractionFolderName = this.GetPackageExtractionFolderName();
				if (!this.Directory.EndsWith(packageExtractionFolderName, StringComparison.OrdinalIgnoreCase))
				{
					throw new ApnsFeedbackException(Strings.ApnsFeedbackFileIdInvalidDirectory(packageExtractionFolderName));
				}
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000062CF File Offset: 0x000044CF
		// (set) Token: 0x0600017B RID: 379 RVA: 0x000062D7 File Offset: 0x000044D7
		internal ExDateTime Date { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000062E0 File Offset: 0x000044E0
		// (set) Token: 0x0600017D RID: 381 RVA: 0x000062E8 File Offset: 0x000044E8
		internal string AppId { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000062F1 File Offset: 0x000044F1
		// (set) Token: 0x0600017F RID: 383 RVA: 0x000062F9 File Offset: 0x000044F9
		internal string Extension { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00006302 File Offset: 0x00004502
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000630A File Offset: 0x0000450A
		internal string Directory { get; private set; }

		// Token: 0x06000182 RID: 386 RVA: 0x00006313 File Offset: 0x00004513
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ApnsFeedbackFileId);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006324 File Offset: 0x00004524
		public bool Equals(ApnsFeedbackFileId other)
		{
			return other != null && this.Date.Equals(other.Date) && this.AppId.Equals(other.AppId, StringComparison.OrdinalIgnoreCase) && this.Extension == other.Extension;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006371 File Offset: 0x00004571
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006380 File Offset: 0x00004580
		public override string ToString()
		{
			if (this.toString == null)
			{
				string path = string.Format("{0}.{1}.{2}", this.Date.ToString("yyyy_MM_dd_H_mm"), this.AppId, this.Extension);
				this.toString = Path.Combine(this.Directory, path);
			}
			return this.toString;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000063D8 File Offset: 0x000045D8
		internal static ApnsFeedbackFileId Parse(string serializedId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("serializedId", serializedId);
			string directory = null;
			string text = null;
			string text2 = null;
			try
			{
				directory = Path.GetDirectoryName(serializedId);
				text = Path.GetFileNameWithoutExtension(serializedId);
				text2 = Path.GetExtension(serializedId);
			}
			catch (ArgumentException ex)
			{
				throw new ApnsFeedbackException(Strings.ApnsFeedbackFileIdInvalidCharacters(serializedId, ex.Message), ex);
			}
			if (!string.IsNullOrEmpty(text2) && text2.StartsWith("."))
			{
				text2 = text2.Substring(1);
			}
			int num = (text == null) ? -1 : text.IndexOf('.');
			if (string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text) || num <= 0 || num + 1 >= text.Length)
			{
				throw new ApnsFeedbackException(Strings.ApnsFeedbackFileIdInsufficientComponents(serializedId));
			}
			DateTime dateTime = default(DateTime);
			try
			{
				dateTime = DateTime.ParseExact(text.Substring(0, num), "yyyy_MM_dd_H_mm", DateTimeFormatInfo.CurrentInfo, DateTimeStyles.AssumeUniversal).ToUniversalTime();
			}
			catch (FormatException ex2)
			{
				throw new ApnsFeedbackException(Strings.ApnsFeedbackFileIdInvalidDate(serializedId, ex2.Message), ex2);
			}
			string appId = text.Substring(num + 1, text.Length - num - 1);
			return new ApnsFeedbackFileId((ExDateTime)dateTime, appId, text2, directory)
			{
				toString = serializedId
			};
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006510 File Offset: 0x00004710
		internal string GetFeedbackFileSearchPattern()
		{
			return string.Format("{0}*.txt", this.Date.ToString("yyyy_MM_dd_H_mm"));
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000653C File Offset: 0x0000473C
		internal ApnsFeedbackFileId GetPackageId()
		{
			if (this.Extension == "zip")
			{
				return this;
			}
			return new ApnsFeedbackFileId(this.Date, "feedback", "zip", Path.GetDirectoryName(this.Directory));
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006580 File Offset: 0x00004780
		internal string GetPackageExtractionFolder()
		{
			if (this.packageExtractionFolder == null)
			{
				this.packageExtractionFolder = ((this.Extension == "zip") ? Path.Combine(this.Directory, this.GetPackageExtractionFolderName()) : this.Directory);
			}
			return this.packageExtractionFolder;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000065CC File Offset: 0x000047CC
		private string GetPackageExtractionFolderName()
		{
			return this.Date.ToString("yyyy_MM_dd_H_mm");
		}

		// Token: 0x04000089 RID: 137
		internal const string PackageExtension = "zip";

		// Token: 0x0400008A RID: 138
		internal const string MetadataExtension = "metadata";

		// Token: 0x0400008B RID: 139
		internal const string FeedbackExtension = "txt";

		// Token: 0x0400008C RID: 140
		internal const string DateFormat = "yyyy_MM_dd_H_mm";

		// Token: 0x0400008D RID: 141
		internal const string AllPackageSearchPattern = "*.zip";

		// Token: 0x0400008E RID: 142
		internal const string AllMetadataSearchPattern = "*.metadata";

		// Token: 0x0400008F RID: 143
		private const string FeedbackSearchPatternTemplate = "{0}*.txt";

		// Token: 0x04000090 RID: 144
		private const string StarDot = "*.";

		// Token: 0x04000091 RID: 145
		private const string FileNameFormat = "{0}.{1}.{2}";

		// Token: 0x04000092 RID: 146
		private const string FeedbackPseudoAppId = "feedback";

		// Token: 0x04000093 RID: 147
		private static readonly string[] WellKnownExtensions = new string[]
		{
			"txt",
			"metadata",
			"zip"
		};

		// Token: 0x04000094 RID: 148
		private string toString;

		// Token: 0x04000095 RID: 149
		private string packageExtractionFolder;
	}
}
