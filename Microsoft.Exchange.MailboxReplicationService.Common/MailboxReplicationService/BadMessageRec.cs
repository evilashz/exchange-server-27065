using System;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000CF RID: 207
	[Serializable]
	public sealed class BadMessageRec : XMLSerializableBase
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0000D006 File Offset: 0x0000B206
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x0000D00E File Offset: 0x0000B20E
		[XmlIgnore]
		public BadItemKind Kind { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0000D017 File Offset: 0x0000B217
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x0000D01F File Offset: 0x0000B21F
		[XmlElement(ElementName = "Kind")]
		public int KindInt
		{
			get
			{
				return (int)this.Kind;
			}
			set
			{
				this.Kind = (BadItemKind)value;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0000D028 File Offset: 0x0000B228
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0000D030 File Offset: 0x0000B230
		[XmlElement(ElementName = "EntryId")]
		public byte[] EntryId { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0000D039 File Offset: 0x0000B239
		[XmlIgnore]
		public string EntryIdHex
		{
			get
			{
				return TraceUtils.DumpBytes(this.EntryId);
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0000D046 File Offset: 0x0000B246
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x0000D04E File Offset: 0x0000B24E
		[XmlElement(ElementName = "CloudId")]
		public string CloudId { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0000D057 File Offset: 0x0000B257
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x0000D05F File Offset: 0x0000B25F
		[XmlElement(ElementName = "Data")]
		public string XmlData { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0000D068 File Offset: 0x0000B268
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x0000D070 File Offset: 0x0000B270
		[XmlElement(ElementName = "FolderId")]
		public byte[] FolderId { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0000D079 File Offset: 0x0000B279
		[XmlIgnore]
		public string FolderIdHex
		{
			get
			{
				return TraceUtils.DumpBytes(this.FolderId);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0000D086 File Offset: 0x0000B286
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x0000D0A1 File Offset: 0x0000B2A1
		[XmlElement(ElementName = "FolderName")]
		public string FolderName
		{
			get
			{
				if (SuppressingPiiContext.NeedPiiSuppression)
				{
					return SuppressingPiiData.Redact(this.folderName);
				}
				return this.folderName;
			}
			set
			{
				this.folderName = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0000D0AA File Offset: 0x0000B2AA
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x0000D0B2 File Offset: 0x0000B2B2
		[XmlIgnore]
		public WellKnownFolderType WellKnownFolderType { get; set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0000D0BB File Offset: 0x0000B2BB
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0000D0C3 File Offset: 0x0000B2C3
		[XmlElement(ElementName = "WKFType")]
		public int WellKnownFolderTypeInt
		{
			get
			{
				return (int)this.WellKnownFolderType;
			}
			set
			{
				this.WellKnownFolderType = (WellKnownFolderType)value;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0000D0E7 File Offset: 0x0000B2E7
		[XmlElement(ElementName = "Sender")]
		public string Sender
		{
			get
			{
				if (SuppressingPiiContext.NeedPiiSuppression)
				{
					return SuppressingPiiData.Redact(this.sender);
				}
				return this.sender;
			}
			set
			{
				this.sender = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0000D0F0 File Offset: 0x0000B2F0
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x0000D10B File Offset: 0x0000B30B
		[XmlElement(ElementName = "Recipient")]
		public string Recipient
		{
			get
			{
				if (SuppressingPiiContext.NeedPiiSuppression)
				{
					return SuppressingPiiData.Redact(this.recipient);
				}
				return this.recipient;
			}
			set
			{
				this.recipient = value;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0000D114 File Offset: 0x0000B314
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0000D12F File Offset: 0x0000B32F
		[XmlElement(ElementName = "Subject")]
		public string Subject
		{
			get
			{
				if (SuppressingPiiContext.NeedPiiSuppression)
				{
					return SuppressingPiiData.Redact(this.subject);
				}
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0000D138 File Offset: 0x0000B338
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0000D140 File Offset: 0x0000B340
		[XmlElement(ElementName = "MessageClass")]
		public string MessageClass { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0000D149 File Offset: 0x0000B349
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0000D151 File Offset: 0x0000B351
		[XmlElement(ElementName = "MessageSize")]
		public int? MessageSize { get; set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0000D15A File Offset: 0x0000B35A
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x0000D162 File Offset: 0x0000B362
		[XmlElement(ElementName = "DateSent")]
		public DateTime? DateSent { get; set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0000D16B File Offset: 0x0000B36B
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x0000D173 File Offset: 0x0000B373
		[XmlElement(ElementName = "DateReceived")]
		public DateTime? DateReceived { get; set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0000D17C File Offset: 0x0000B37C
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x0000D184 File Offset: 0x0000B384
		[XmlElement(ElementName = "Failure")]
		public FailureRec Failure { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0000D18D File Offset: 0x0000B38D
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0000D195 File Offset: 0x0000B395
		[XmlElement(ElementName = "Category")]
		public string Category { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0000D19E File Offset: 0x0000B39E
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x0000D1A6 File Offset: 0x0000B3A6
		[XmlIgnore]
		internal Exception RawFailure { get; set; }

		// Token: 0x06000806 RID: 2054 RVA: 0x0000D1AF File Offset: 0x0000B3AF
		public override string ToString()
		{
			return this.ToLocalizedString();
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0000D1BC File Offset: 0x0000B3BC
		internal static BadMessageRec MissingItem(MessageRec msg)
		{
			return new BadMessageRec
			{
				Kind = BadItemKind.MissingItem,
				EntryId = msg.EntryId,
				FolderId = msg.FolderId
			};
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0000D1F0 File Offset: 0x0000B3F0
		internal static BadMessageRec MissingItem(Exception ex)
		{
			return new BadMessageRec
			{
				Kind = BadItemKind.MissingItem,
				Failure = FailureRec.Create(ex),
				EntryId = BadMessageRec.ComputeKey(BitConverter.GetBytes(ex.GetHashCode()), BadItemKind.MissingItem)
			};
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0000D230 File Offset: 0x0000B430
		internal static BadMessageRec MissingFolder(FolderRec folderRec, string folderPath, WellKnownFolderType wkfType)
		{
			return new BadMessageRec
			{
				Kind = BadItemKind.MissingFolder,
				EntryId = BadMessageRec.ComputeKey(folderRec.EntryId, BadItemKind.MissingFolder),
				FolderId = folderRec.EntryId,
				FolderName = folderPath,
				WellKnownFolderType = wkfType
			};
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0000D278 File Offset: 0x0000B478
		internal static BadMessageRec MisplacedFolder(FolderRec folderRec, string folderPath, WellKnownFolderType wkfType, byte[] destParentId)
		{
			return new BadMessageRec
			{
				Kind = BadItemKind.MisplacedFolder,
				EntryId = BadMessageRec.ComputeKey(folderRec.EntryId, BadItemKind.MisplacedFolder),
				FolderId = folderRec.EntryId,
				FolderName = folderPath,
				WellKnownFolderType = wkfType
			};
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		internal static BadMessageRec Item(MessageRec msgData, FolderRec folderRec, Exception exception)
		{
			BadMessageRec badMessageRec = new BadMessageRec();
			if (exception == null)
			{
				badMessageRec.Kind = BadItemKind.MissingItem;
			}
			else if (CommonUtils.ExceptionIs(exception, new WellKnownException[]
			{
				WellKnownException.MapiMaxSubmissionExceeded
			}))
			{
				badMessageRec.Kind = BadItemKind.LargeItem;
			}
			else
			{
				badMessageRec.Kind = BadItemKind.CorruptItem;
			}
			badMessageRec.EntryId = msgData.EntryId;
			badMessageRec.Sender = (msgData[PropTag.SenderName] as string);
			badMessageRec.Recipient = (msgData[PropTag.ReceivedByName] as string);
			badMessageRec.Subject = (msgData[PropTag.Subject] as string);
			badMessageRec.MessageClass = (msgData[PropTag.MessageClass] as string);
			badMessageRec.MessageSize = (int?)msgData[PropTag.MessageSize];
			badMessageRec.DateSent = (DateTime?)msgData[PropTag.ClientSubmitTime];
			badMessageRec.DateReceived = (DateTime?)msgData[PropTag.MessageDeliveryTime];
			badMessageRec.FolderId = msgData.FolderId;
			badMessageRec.FolderName = folderRec.FolderName;
			badMessageRec.Failure = FailureRec.Create(exception);
			badMessageRec.RawFailure = exception;
			return badMessageRec;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		internal static BadMessageRec Folder(FolderRec folderRec, BadItemKind kind, Exception failure)
		{
			return new BadMessageRec
			{
				Kind = kind,
				EntryId = BadMessageRec.ComputeKey(folderRec.EntryId, kind),
				FolderId = folderRec.EntryId,
				FolderName = folderRec.FolderName,
				Failure = FailureRec.Create(failure),
				RawFailure = failure
			};
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0000D430 File Offset: 0x0000B630
		internal static BadMessageRec InferenceData(Exception failure, byte[] entryId)
		{
			return new BadMessageRec
			{
				Kind = BadItemKind.CorruptInferenceProperties,
				Failure = FailureRec.Create(failure),
				EntryId = entryId,
				RawFailure = failure
			};
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0000D468 File Offset: 0x0000B668
		internal static BadMessageRec MailboxSetting(Exception failure, ItemPropertiesBase item)
		{
			return new BadMessageRec
			{
				Kind = BadItemKind.CorruptMailboxSetting,
				Failure = FailureRec.Create(failure),
				RawFailure = failure,
				MessageClass = item.GetType().ToString(),
				EntryId = item.GetId()
			};
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
		internal static BadMessageRec FolderProperty(FolderRec folderRec, PropTag propTag, string sourceValue, string destValue)
		{
			BadMessageRec badMessageRec = new BadMessageRec();
			badMessageRec.FolderId = folderRec.EntryId;
			badMessageRec.FolderName = folderRec.FolderName;
			badMessageRec.Kind = BadItemKind.FolderPropertyMismatch;
			badMessageRec.EntryId = BadMessageRec.ComputeKey(folderRec.EntryId, badMessageRec.Kind, propTag);
			PropertyMismatchException ex = new PropertyMismatchException((uint)propTag, sourceValue, destValue);
			badMessageRec.Failure = FailureRec.Create(ex);
			badMessageRec.RawFailure = ex;
			return badMessageRec;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0000D51C File Offset: 0x0000B71C
		private static byte[] ComputeKey(byte[] entryId, BadItemKind kind)
		{
			return CommonUtils.GetSHA512Hash(string.Format("{0}{1}", TraceUtils.DumpEntryId(entryId), kind));
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0000D539 File Offset: 0x0000B739
		private static byte[] ComputeKey(byte[] entryId, BadItemKind kind, PropTag propTag)
		{
			return CommonUtils.GetSHA512Hash(string.Format("{0}{1}{2}", TraceUtils.DumpEntryId(entryId), kind, propTag));
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0000D55C File Offset: 0x0000B75C
		internal static byte[] ComputeKey(PropValueData[] pvda)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (PropValueData propValueData in pvda)
			{
				stringBuilder.Append(propValueData.ToString());
			}
			return CommonUtils.GetSHA512Hash(stringBuilder.ToString());
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0000D59C File Offset: 0x0000B79C
		internal LocalizedString ToLocalizedString()
		{
			switch (this.Kind)
			{
			case BadItemKind.MissingItem:
				return MrsStrings.BadItemMissingItem(this.MessageClass, this.Subject, this.FolderName);
			case BadItemKind.LargeItem:
			{
				int num = this.MessageSize ?? 0;
				return MrsStrings.BadItemLarge(this.MessageClass, this.Subject, new ByteQuantifiedSize((ulong)((long)num)).ToString(), this.FolderName);
			}
			case BadItemKind.CorruptSearchFolderCriteria:
				return MrsStrings.BadItemSearchFolder(this.FolderName);
			case BadItemKind.CorruptFolderACL:
				return MrsStrings.BadItemFolderACL(this.FolderName);
			case BadItemKind.CorruptFolderRule:
				return MrsStrings.BadItemFolderRule(this.FolderName);
			case BadItemKind.MissingFolder:
				return MrsStrings.BadItemMissingFolder(this.FolderName);
			case BadItemKind.MisplacedFolder:
				return MrsStrings.BadItemMisplacedFolder(this.FolderName);
			case BadItemKind.CorruptFolderProperty:
				return MrsStrings.BadItemFolderProperty(this.FolderName);
			case BadItemKind.CorruptMailboxSetting:
				return MrsStrings.BadItemCorruptMailboxSetting(this.MessageClass);
			case BadItemKind.FolderPropertyMismatch:
				return MrsStrings.BadItemFolderPropertyMismatch(this.folderName, this.RawFailure.ToString());
			}
			return MrsStrings.BadItemCorrupt(this.MessageClass, this.Subject, this.FolderName);
		}

		// Token: 0x040004B7 RID: 1207
		private string sender;

		// Token: 0x040004B8 RID: 1208
		private string recipient;

		// Token: 0x040004B9 RID: 1209
		private string subject;

		// Token: 0x040004BA RID: 1210
		private string folderName;

		// Token: 0x040004BB RID: 1211
		internal static readonly PropTag[] BadItemPtags = new PropTag[]
		{
			PropTag.Subject,
			PropTag.SenderName,
			PropTag.ReceivedByName,
			PropTag.MessageClass,
			PropTag.MessageSize,
			PropTag.ClientSubmitTime,
			PropTag.MessageDeliveryTime
		};
	}
}
