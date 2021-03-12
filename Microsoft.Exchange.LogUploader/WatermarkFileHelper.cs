using System;
using System.IO;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000029 RID: 41
	internal class WatermarkFileHelper : IWatermarkFileHelper
	{
		// Token: 0x06000210 RID: 528 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public WatermarkFileHelper(string logDirectory, string wmkDirectory = null)
		{
			if (string.IsNullOrEmpty(logDirectory))
			{
				throw new ArgumentException(string.Format("{0} is not a valid directory name", logDirectory));
			}
			this.logfileDirectory = logDirectory.TrimEnd(new char[]
			{
				'\\'
			});
			if (string.IsNullOrEmpty(wmkDirectory))
			{
				this.watermarkFileDirectory = this.logfileDirectory;
				return;
			}
			this.watermarkFileDirectory = wmkDirectory.TrimEnd(new char[]
			{
				'\\'
			});
			if (!Directory.Exists(this.watermarkFileDirectory))
			{
				Directory.CreateDirectory(this.watermarkFileDirectory);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		public string WatermarkFileDirectory
		{
			get
			{
				return this.watermarkFileDirectory;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		public string LogFileDirectory
		{
			get
			{
				return this.logfileDirectory;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000ADE8 File Offset: 0x00008FE8
		public string DeduceLogFullFileNameFromDoneOrWatermarkFileName(string fileFullName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("fileFullName", fileFullName);
			string text = Path.GetFileName(fileFullName);
			ArgumentValidator.ThrowIfNullOrEmpty("fileName", text);
			text = Path.ChangeExtension(text, "log");
			return Path.Combine(this.logfileDirectory, text);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000AE2A File Offset: 0x0000902A
		public string DeduceDoneFileFullNameFromLogFileName(string logFileName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("logFileName", logFileName);
			logFileName = Path.GetFileName(logFileName);
			ArgumentValidator.ThrowIfNullOrEmpty("logFileName", logFileName);
			return Path.Combine(this.watermarkFileDirectory, Path.ChangeExtension(logFileName, "done"));
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000AE60 File Offset: 0x00009060
		public string DeduceWatermarkFileFullNameFromLogFileName(string logFileName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("logFileName", logFileName);
			logFileName = Path.GetFileName(logFileName);
			ArgumentValidator.ThrowIfNullOrEmpty("logFileName", logFileName);
			return Path.Combine(this.watermarkFileDirectory, Path.ChangeExtension(logFileName, "wmk"));
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000AE98 File Offset: 0x00009098
		public IWatermarkFile CreateWaterMarkFileObj(string logFileName, string logPrefix)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("logFileName", logFileName);
			ArgumentValidator.ThrowIfNullOrEmpty("logPrefix", logPrefix);
			string fileName = Path.GetFileName(logFileName);
			ArgumentValidator.ThrowIfNullOrEmpty("fileName", fileName);
			string logFileName2 = Path.Combine(this.logfileDirectory, fileName);
			return new WatermarkFile(logFileName2, this.DeduceWatermarkFileFullNameFromLogFileName(fileName), logPrefix);
		}

		// Token: 0x0400013E RID: 318
		private readonly string watermarkFileDirectory;

		// Token: 0x0400013F RID: 319
		private readonly string logfileDirectory;
	}
}
