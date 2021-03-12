using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200078D RID: 1933
	internal class CalendarDiagnosticLogFileWriter
	{
		// Token: 0x060043F4 RID: 17396 RVA: 0x00116D92 File Offset: 0x00114F92
		public CalendarDiagnosticLogFileWriter(string logLocation, string displayName, string acceptedDomain)
		{
			this.outputDirectoryPath = Path.Combine(logLocation, LocalLongFullPath.ConvertInvalidCharactersInFileName(displayName));
			this.acceptedDomain = acceptedDomain;
		}

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x060043F5 RID: 17397 RVA: 0x00116DC4 File Offset: 0x00114FC4
		private OutboundConversionOptions OutboundConversionOptions
		{
			get
			{
				if (this._outboundConversionOptions == null)
				{
					this._outboundConversionOptions = new OutboundConversionOptions(this.acceptedDomain);
					this._outboundConversionOptions.Limits.MaxBodyPartsTotal = 2048;
					this._outboundConversionOptions.Limits.MaxMimeRecipients *= 2;
					this._outboundConversionOptions.FilterAttachmentHandler = ((Item item, Attachment attachment) => attachment.AttachmentType == AttachmentType.EmbeddedMessage);
					this._outboundConversionOptions.FilterBodyHandler = ((Item item) => false);
					this._outboundConversionOptions.UserADSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(this.acceptedDomain), 79, "OutboundConversionOptions", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\GetCalendarDiagnosticLog\\CalendarDiagnosticLogFileWriter.cs");
				}
				return this._outboundConversionOptions;
			}
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x00116EA0 File Offset: 0x001150A0
		public static string CheckAndCreateLogLocation(string logLocation)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CalendarLogs"))
			{
				if (registryKey != null)
				{
					string text = registryKey.GetValue("LogLocation", string.Empty) as string;
					if (!string.IsNullOrEmpty(text))
					{
						logLocation = text;
					}
				}
			}
			if (string.IsNullOrEmpty(logLocation))
			{
				throw new ArgumentException(Strings.LogLocationError("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CalendarLogs", "LogLocation"));
			}
			logLocation = LocalLongFullPath.ConvertInvalidCharactersInPathName(logLocation);
			if (!Directory.Exists(logLocation))
			{
				Directory.CreateDirectory(logLocation);
			}
			return logLocation;
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x00116F38 File Offset: 0x00115138
		public FileInfo LogItem(Item item, out string result)
		{
			result = null;
			try
			{
				if (item != null)
				{
					FileInfo fileInfo = new FileInfo(this.GetFullFileName(item));
					if (fileInfo.Exists)
					{
						return fileInfo;
					}
					if (!fileInfo.Directory.Exists)
					{
						fileInfo.Directory.Create();
					}
					using (Stream stream = fileInfo.Open(FileMode.CreateNew, FileAccess.ReadWrite))
					{
						item.Load(StoreObjectSchema.ContentConversionProperties);
						ItemConversion.ConvertItemToMsgStorage(item, stream, this.OutboundConversionOptions);
					}
					return fileInfo;
				}
			}
			catch (ConversionFailedException ex)
			{
				result = string.Format("{0} {1}", ex.Message, item.Id.ToBase64String());
			}
			return null;
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x00116FF0 File Offset: 0x001151F0
		private string GetFullFileName(Item log)
		{
			string text = LocalLongFullPath.ConvertInvalidCharactersInFileName(log.GetValueOrDefault<string>(ItemSchema.NormalizedSubject, string.Empty));
			int num = 0;
			string valueOrDefault = log.PropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass);
			if (!ObjectClass.IsMeetingInquiry(valueOrDefault))
			{
				num = log.GetValueOrDefault<int>(CalendarItemBaseSchema.AppointmentSequenceNumber, 0);
			}
			string text2 = string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				log.LastModifiedTime.UtcTicks,
				num,
				this.GetParentFolderName(log),
				text
			});
			if (text2.Length > 120)
			{
				text2.Substring(0, 120 - ".msg".Length);
			}
			return Path.Combine(this.outputDirectoryPath, text2 + ".msg");
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x001170B8 File Offset: 0x001152B8
		private string GetParentFolderName(Item itemToLog)
		{
			string text = null;
			string key = itemToLog.ParentId.ToBase64String();
			if (!CalendarDiagnosticLogFileWriter.parentFolderIdCache.ContainsKey(key))
			{
				using (Folder folder = Folder.Bind(itemToLog.Session, itemToLog.ParentId))
				{
					text = LocalLongFullPath.ConvertInvalidCharactersInFileName(folder.DisplayName);
					CalendarDiagnosticLogFileWriter.parentFolderIdCache.Add(key, text);
					return text;
				}
			}
			text = CalendarDiagnosticLogFileWriter.parentFolderIdCache[key];
			return text;
		}

		// Token: 0x04002A38 RID: 10808
		private const int MaxSubjectLength = 20;

		// Token: 0x04002A39 RID: 10809
		private const int MaxFileNameLength = 120;

		// Token: 0x04002A3A RID: 10810
		private const int MaxBodyParts = 2048;

		// Token: 0x04002A3B RID: 10811
		private const string LogRegistryPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CalendarLogs";

		// Token: 0x04002A3C RID: 10812
		private const string LogRegistryValue = "LogLocation";

		// Token: 0x04002A3D RID: 10813
		private const string FileNameFormat = "{0}.{1}.{2}.{3}";

		// Token: 0x04002A3E RID: 10814
		private static Dictionary<string, string> parentFolderIdCache = new Dictionary<string, string>();

		// Token: 0x04002A3F RID: 10815
		private OutboundConversionOptions _outboundConversionOptions;

		// Token: 0x04002A40 RID: 10816
		private readonly string acceptedDomain;

		// Token: 0x04002A41 RID: 10817
		private readonly string outputDirectoryPath;
	}
}
