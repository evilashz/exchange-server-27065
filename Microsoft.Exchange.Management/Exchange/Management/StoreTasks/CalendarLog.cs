using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200078E RID: 1934
	[Serializable]
	public class CalendarLog : IConfigurable, IComparer<CalendarLog>
	{
		// Token: 0x060043FD RID: 17405 RVA: 0x00117140 File Offset: 0x00115340
		public CalendarLog()
		{
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x00117148 File Offset: 0x00115348
		internal CalendarLog(Item item, string user)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.id = new CalendarLogId(item, user);
			this.OriginalLastModifiedTime = item.LastModifiedTime.ToUtc().UniversalTime;
			this.ItemVersion = item.GetProperty(CalendarItemBaseSchema.ItemVersion);
			this.InternalGlobalObjectId = item.GetProperty(CalendarItemBaseSchema.CleanGlobalObjectId);
			this.NormalizedSubject = item.GetValueOrDefault<string>(ItemSchema.NormalizedSubject, string.Empty);
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x001171CA File Offset: 0x001153CA
		internal CalendarLog(Item item, FileInfo log, string user) : this(item, user)
		{
			if (log == null)
			{
				throw new ArgumentNullException("item");
			}
			this.id = new CalendarLogId(log.FullName);
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x001171F4 File Offset: 0x001153F4
		internal static CalendarLog[] Parse(string identity)
		{
			string[] array = identity.Split(new char[]
			{
				'|'
			}, StringSplitOptions.RemoveEmptyEntries);
			List<CalendarLog> list = new List<CalendarLog>();
			CalendarLog calendarLog = null;
			foreach (string text in array)
			{
				if (Directory.Exists(text))
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(text);
					FileInfo[] files = directoryInfo.GetFiles("*.msg");
					foreach (FileInfo logFile in files)
					{
						if (CalendarLog.TryParse(logFile, out calendarLog))
						{
							list.Add(calendarLog);
						}
					}
				}
				else if (File.Exists(text))
				{
					if (CalendarLog.TryParse(new FileInfo(text), out calendarLog))
					{
						list.Add(calendarLog);
					}
				}
				else
				{
					string text2 = text.Substring(0, text.LastIndexOf('\\'));
					string value = text.Substring(text2.Length + 1);
					if (Directory.Exists(text2))
					{
						DirectoryInfo directoryInfo2 = new DirectoryInfo(text2);
						FileInfo[] files2 = directoryInfo2.GetFiles("*.msg");
						foreach (FileInfo logFile2 in files2)
						{
							if (CalendarLog.TryParse(logFile2, out calendarLog) && calendarLog.CleanGlobalObjectId.Equals(value))
							{
								list.Add(calendarLog);
							}
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x00117344 File Offset: 0x00115544
		internal static CalendarLog FromFile(FileInfo file)
		{
			if (file.Exists)
			{
				using (MessageItem messageItem = MessageItem.CreateInMemory(StoreObjectSchema.ContentConversionProperties))
				{
					using (FileStream fileStream = file.OpenRead())
					{
						ItemConversion.ConvertMsgStorageToItem(fileStream, messageItem, new InboundConversionOptions(new EmptyRecipientCache(), null));
						return new CalendarLog(messageItem, file, null);
					}
				}
			}
			return null;
		}

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x06004402 RID: 17410 RVA: 0x001173BC File Offset: 0x001155BC
		public bool IsFileLink
		{
			get
			{
				return new UriHandler(this.id.Uri).IsFileLink;
			}
		}

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x06004403 RID: 17411 RVA: 0x001173D3 File Offset: 0x001155D3
		// (set) Token: 0x06004404 RID: 17412 RVA: 0x001173DB File Offset: 0x001155DB
		public ObjectId Identity
		{
			get
			{
				return this.id;
			}
			private set
			{
				this.id = (value as CalendarLogId);
			}
		}

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x06004405 RID: 17413 RVA: 0x001173EC File Offset: 0x001155EC
		public string LogDate
		{
			get
			{
				return this.OriginalLastModifiedTime.ToString("yyyy-MM-dd, h:mm:ss tt");
			}
		}

		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x06004406 RID: 17414 RVA: 0x0011740C File Offset: 0x0011560C
		// (set) Token: 0x06004407 RID: 17415 RVA: 0x00117414 File Offset: 0x00115614
		internal DateTime OriginalLastModifiedTime { get; private set; }

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x06004408 RID: 17416 RVA: 0x0011741D File Offset: 0x0011561D
		// (set) Token: 0x06004409 RID: 17417 RVA: 0x00117425 File Offset: 0x00115625
		internal int ItemVersion { get; private set; }

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x0600440A RID: 17418 RVA: 0x0011742E File Offset: 0x0011562E
		// (set) Token: 0x0600440B RID: 17419 RVA: 0x00117436 File Offset: 0x00115636
		public string NormalizedSubject { get; private set; }

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x0600440C RID: 17420 RVA: 0x0011743F File Offset: 0x0011563F
		public string CleanGlobalObjectId
		{
			get
			{
				return this.InternalGlobalObjectId.To64BitString();
			}
		}

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x0600440D RID: 17421 RVA: 0x0011744C File Offset: 0x0011564C
		// (set) Token: 0x0600440E RID: 17422 RVA: 0x00117454 File Offset: 0x00115654
		internal byte[] InternalGlobalObjectId { get; private set; }

		// Token: 0x0600440F RID: 17423 RVA: 0x00117460 File Offset: 0x00115660
		internal static bool TryParse(FileInfo logFile, out CalendarLog log)
		{
			log = null;
			try
			{
				log = CalendarLog.FromFile(logFile);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x00117494 File Offset: 0x00115694
		internal static IComparer<CalendarLog> GetComparer()
		{
			return new CalendarLog();
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x0011749B File Offset: 0x0011569B
		internal int CompareTo(CalendarLog other)
		{
			return this.Compare(this, other);
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x001174A8 File Offset: 0x001156A8
		public int Compare(CalendarLog c0, CalendarLog c1)
		{
			int num = c0.ItemVersion.CompareTo(c1.ItemVersion);
			if (num == 0)
			{
				num = c0.OriginalLastModifiedTime.CompareTo(c1.OriginalLastModifiedTime);
			}
			return num;
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x001174E4 File Offset: 0x001156E4
		public ValidationError[] Validate()
		{
			if (this.validationErrors == null)
			{
				List<ValidationError> list = new List<ValidationError>();
				if (this.Identity == null)
				{
					list.Add(new ObjectValidationError(Strings.CalendarLogIdentityNotSpecified, this.Identity, "Identity"));
				}
				else
				{
					UriHandler uriHandler = new UriHandler(this.id.Uri);
					if (!uriHandler.IsValidLink)
					{
						list.Add(new ObjectValidationError(Strings.InvalidLogIdentityFormat(uriHandler.Uri.ToString()), this.Identity, "Identity"));
					}
					if (uriHandler.IsFileLink)
					{
						FileInfo fileInfo = new FileInfo(uriHandler.Uri.LocalPath);
						if (!fileInfo.Exists)
						{
							list.Add(new ObjectValidationError(Strings.CalendarLogFileDoesNotExist(uriHandler.Uri.ToString()), this.Identity, "Identity"));
						}
					}
				}
				if (list.Count == 0)
				{
					this.validationErrors = ValidationError.None;
				}
			}
			return this.validationErrors;
		}

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x001175C9 File Offset: 0x001157C9
		public bool IsValid
		{
			get
			{
				return new UriHandler(this.id.Uri).IsValidLink && this.Validate().Length == 0;
			}
		}

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x06004415 RID: 17429 RVA: 0x001175EF File Offset: 0x001157EF
		public ObjectState ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x001175F2 File Offset: 0x001157F2
		public void CopyChangesFrom(IConfigurable source)
		{
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x001175F4 File Offset: 0x001157F4
		public void ResetChangeTracking()
		{
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x001175F6 File Offset: 0x001157F6
		public override string ToString()
		{
			return this.id.ToString();
		}

		// Token: 0x04002A44 RID: 10820
		private CalendarLogId id;

		// Token: 0x04002A45 RID: 10821
		private ValidationError[] validationErrors;
	}
}
