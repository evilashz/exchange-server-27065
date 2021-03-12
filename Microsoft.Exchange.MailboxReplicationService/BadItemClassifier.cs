using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000066 RID: 102
	internal class BadItemClassifier
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0001E268 File Offset: 0x0001C468
		public void Classify(BadMessageRec item, TestIntegration testIntegration)
		{
			foreach (BadItemCategory badItemCategory in BadItemClassifier.categories)
			{
				if (badItemCategory.IsMatch(item, testIntegration))
				{
					item.Category = badItemCategory.Name;
					break;
				}
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001E2CC File Offset: 0x0001C4CC
		public int GetLimit(string categoryName)
		{
			if (this.categoryLimits.Count == 0)
			{
				foreach (BadItemCategory badItemCategory in BadItemClassifier.categories)
				{
					this.categoryLimits.Add(badItemCategory.Name, badItemCategory.GetLimit());
				}
			}
			int result;
			if (!this.categoryLimits.TryGetValue(categoryName, out result))
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x04000217 RID: 535
		private static readonly List<BadItemCategory> categories = new List<BadItemCategory>(new BadItemCategory[]
		{
			new BadItemClassifier.FaultInjectionCorruption(),
			new BadItemClassifier.SimpleBadItem("FolderPropertyMismatchCorruption", new WellKnownException?(WellKnownException.MrsPropertyMismatch), new BadItemKind?(BadItemKind.FolderPropertyMismatch), "BadItemLimitFolderPropertyMismatchCorruption"),
			new BadItemClassifier.SimpleBadItem("FolderPropertyCorruption", BadItemKind.CorruptFolderProperty, "BadItemLimiFolderPropertyCorruption"),
			new BadItemClassifier.Contact(),
			new BadItemClassifier.DistributionList(),
			new BadItemClassifier.CalendarRecurrenceCorruption(),
			new BadItemClassifier.StartGreaterThanEndCalendarCorruption(),
			new BadItemClassifier.ConflictEntryIdCorruption(),
			new BadItemClassifier.NonCanonicalAclCorruption(),
			new BadItemClassifier.UnifiedMessagingReportRecipientCorruption(),
			new BadItemClassifier.RecipientCorruption(),
			new BadItemClassifier.StringArrayCorruption(),
			new BadItemClassifier.InvalidMultivalueElementCorruption(),
			new BadItemClassifier.NonUnicodeValueCorruption(),
			new BadItemClassifier.InDumpster(),
			new BadItemClassifier.OldNonContact(),
			new BadItemClassifier.Default()
		});

		// Token: 0x04000218 RID: 536
		private Dictionary<string, int> categoryLimits = new Dictionary<string, int>();

		// Token: 0x02000067 RID: 103
		private class Default : BadItemCategory
		{
			// Token: 0x0600050C RID: 1292 RVA: 0x0001E426 File Offset: 0x0001C626
			public Default() : base("Default", "BadItemLimitDefault")
			{
			}

			// Token: 0x0600050D RID: 1293 RVA: 0x0001E438 File Offset: 0x0001C638
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return true;
			}
		}

		// Token: 0x02000068 RID: 104
		private class SimpleBadItem : BadItemCategory
		{
			// Token: 0x0600050E RID: 1294 RVA: 0x0001E43C File Offset: 0x0001C63C
			public SimpleBadItem(string name, WellKnownException wke, string configName) : this(name, new WellKnownException?(wke), null, configName)
			{
			}

			// Token: 0x0600050F RID: 1295 RVA: 0x0001E460 File Offset: 0x0001C660
			public SimpleBadItem(string name, BadItemKind kind, string configName) : this(name, null, new BadItemKind?(kind), configName)
			{
			}

			// Token: 0x06000510 RID: 1296 RVA: 0x0001E484 File Offset: 0x0001C684
			public SimpleBadItem(string name, WellKnownException? wke, BadItemKind? badItemKind, string configName) : base(name, configName)
			{
				this.wke = wke;
				this.badItemKind = badItemKind;
			}

			// Token: 0x06000511 RID: 1297 RVA: 0x0001E4A0 File Offset: 0x0001C6A0
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				if (message == null)
				{
					return false;
				}
				bool result = true;
				if (this.badItemKind != null && message.Kind != this.badItemKind.Value)
				{
					result = false;
				}
				if (this.wke != null && !CommonUtils.ExceptionIs(message.RawFailure, new WellKnownException[]
				{
					this.wke.Value
				}))
				{
					result = false;
				}
				return result;
			}

			// Token: 0x04000219 RID: 537
			private WellKnownException? wke;

			// Token: 0x0400021A RID: 538
			private BadItemKind? badItemKind;
		}

		// Token: 0x02000069 RID: 105
		private class Contact : BadItemCategory
		{
			// Token: 0x06000512 RID: 1298 RVA: 0x0001E509 File Offset: 0x0001C709
			public Contact() : base("Contact", "BadItemLimitContact")
			{
			}

			// Token: 0x06000513 RID: 1299 RVA: 0x0001E51B File Offset: 0x0001C71B
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return message != null && message.MessageClass != null && ObjectClass.IsContact(message.MessageClass);
			}
		}

		// Token: 0x0200006A RID: 106
		private class DistributionList : BadItemCategory
		{
			// Token: 0x06000514 RID: 1300 RVA: 0x0001E535 File Offset: 0x0001C735
			public DistributionList() : base("DistributionList", "BadItemLimitDistributionList")
			{
			}

			// Token: 0x06000515 RID: 1301 RVA: 0x0001E547 File Offset: 0x0001C747
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return message != null && message.MessageClass != null && ObjectClass.IsDistributionList(message.MessageClass);
			}
		}

		// Token: 0x0200006B RID: 107
		private class OldNonContact : BadItemCategory
		{
			// Token: 0x06000516 RID: 1302 RVA: 0x0001E561 File Offset: 0x0001C761
			public OldNonContact() : base("OldNonContact", "BadItemLimitOldNonContact")
			{
				this.ageLimit = ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("OldItemAge");
			}

			// Token: 0x06000517 RID: 1303 RVA: 0x0001E584 File Offset: 0x0001C784
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return message != null && (message.MessageClass == null || !ObjectClass.IsContact(message.MessageClass)) && message.DateSent != null && message.DateSent.Value < DateTime.UtcNow - this.ageLimit;
			}

			// Token: 0x0400021B RID: 539
			private readonly TimeSpan ageLimit;
		}

		// Token: 0x0200006C RID: 108
		private class InDumpster : BadItemCategory
		{
			// Token: 0x06000518 RID: 1304 RVA: 0x0001E5DE File Offset: 0x0001C7DE
			public InDumpster() : base("InDumpster", "BadItemLimitInDumpster")
			{
			}

			// Token: 0x06000519 RID: 1305 RVA: 0x0001E5F0 File Offset: 0x0001C7F0
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return message != null && FolderFilterParser.IsDumpster(message.WellKnownFolderType);
			}
		}

		// Token: 0x0200006D RID: 109
		private class CalendarRecurrenceCorruption : BadItemCategory
		{
			// Token: 0x0600051A RID: 1306 RVA: 0x0001E602 File Offset: 0x0001C802
			public CalendarRecurrenceCorruption() : base("CalendarRecurrenceCorruption", "BadItemLimitCalendarRecurrenceCorruption")
			{
			}

			// Token: 0x0600051B RID: 1307 RVA: 0x0001E614 File Offset: 0x0001C814
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return message != null && message.Failure != null && message.Failure.DataContext != null && message.Failure.Message != null && message.Failure.FailureSideInt == 2 && !(message.Failure.FailureType != "ObjectValidationException") && message.Failure.DataContext.Contains("ISourceMailbox.ExportMessages") && message.Failure.Message.Contains("Microsoft.Exchange.Data.Storage.RecurrenceBlobConstraint");
			}
		}

		// Token: 0x0200006E RID: 110
		private class StartGreaterThanEndCalendarCorruption : BadItemCategory
		{
			// Token: 0x0600051C RID: 1308 RVA: 0x0001E6A2 File Offset: 0x0001C8A2
			public StartGreaterThanEndCalendarCorruption() : base("StartGreaterThanEndCalendarCorruption", "BadItemLimitStartGreaterThanEndCalendarCorruption")
			{
			}

			// Token: 0x0600051D RID: 1309 RVA: 0x0001E6C4 File Offset: 0x0001C8C4
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return message.Failure != null && message.Failure.DataContext != null && message.Failure.Message != null && message.Failure.FailureSideInt == 2 && !(message.Failure.FailureType != "ObjectValidationException") && message.Failure.DataContext.Contains("ISourceMailbox.ExportMessages") && message.Failure.Message.Contains(this.failureSignature);
			}

			// Token: 0x0400021C RID: 540
			private readonly string failureSignature = CalendarItemInstanceSchema.StartTimeMustBeLessThanOrEqualToEndTimeConstraint.ToString();
		}

		// Token: 0x0200006F RID: 111
		private class FaultInjectionCorruption : BadItemCategory
		{
			// Token: 0x0600051E RID: 1310 RVA: 0x0001E750 File Offset: 0x0001C950
			public FaultInjectionCorruption() : base("FaultInjection", "FaultInjection")
			{
			}

			// Token: 0x0600051F RID: 1311 RVA: 0x0001E762 File Offset: 0x0001C962
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return testIntegration.ClassifyBadItemFaults && (message != null && message.Failure != null && message.Failure.Message != null) && message.Failure.Message.Contains("Lid: 48184   StoreEc: 0x8000400");
			}

			// Token: 0x06000520 RID: 1312 RVA: 0x0001E79D File Offset: 0x0001C99D
			public override int GetLimit()
			{
				return 2;
			}
		}

		// Token: 0x02000070 RID: 112
		private class ConflictEntryIdCorruption : BadItemCategory
		{
			// Token: 0x06000521 RID: 1313 RVA: 0x0001E7A0 File Offset: 0x0001C9A0
			public ConflictEntryIdCorruption() : base("ConflictEntryIdCorruption", "BadItemLimitConflictEntryIdCorruption")
			{
			}

			// Token: 0x06000522 RID: 1314 RVA: 0x0001E7B4 File Offset: 0x0001C9B4
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return message != null && message.RawFailure != null && CommonUtils.ExceptionIs(message.RawFailure, new WellKnownException[]
				{
					WellKnownException.ConflictEntryIdCorruption
				});
			}
		}

		// Token: 0x02000071 RID: 113
		private class RecipientCorruption : BadItemCategory
		{
			// Token: 0x06000523 RID: 1315 RVA: 0x0001E7E9 File Offset: 0x0001C9E9
			public RecipientCorruption() : base("RecipientCorruption", "BadItemLimitRecipientCorruption")
			{
			}

			// Token: 0x06000524 RID: 1316 RVA: 0x0001E7FC File Offset: 0x0001C9FC
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return message != null && message.RawFailure != null && CommonUtils.ExceptionIs(message.RawFailure, new WellKnownException[]
				{
					WellKnownException.CorruptRecipient
				});
			}
		}

		// Token: 0x02000072 RID: 114
		private class UnifiedMessagingReportRecipientCorruption : BadItemCategory
		{
			// Token: 0x06000525 RID: 1317 RVA: 0x0001E831 File Offset: 0x0001CA31
			public UnifiedMessagingReportRecipientCorruption() : base("UnifiedMessagingReportRecipientCorruption", "BadItemLimitUnifiedMessagingReportRecipientCorruption")
			{
			}

			// Token: 0x06000526 RID: 1318 RVA: 0x0001E844 File Offset: 0x0001CA44
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return message != null && message.RawFailure != null && CommonUtils.ExceptionIs(message.RawFailure, new WellKnownException[]
				{
					WellKnownException.CorruptRecipient
				}) && message.MessageClass == "IPM.Note.Microsoft.CDR.UM";
			}
		}

		// Token: 0x02000073 RID: 115
		private class NonCanonicalAclCorruption : BadItemCategory
		{
			// Token: 0x06000527 RID: 1319 RVA: 0x0001E88D File Offset: 0x0001CA8D
			public NonCanonicalAclCorruption() : base("NonCanonicalAcl", "BadItemLimitNonCanonicalAclCorruption")
			{
			}

			// Token: 0x06000528 RID: 1320 RVA: 0x0001E8A0 File Offset: 0x0001CAA0
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return message != null && message.RawFailure != null && CommonUtils.ExceptionIs(message.RawFailure, new WellKnownException[]
				{
					WellKnownException.NonCanonicalACL
				});
			}
		}

		// Token: 0x02000074 RID: 116
		private class StringArrayCorruption : BadItemCategory
		{
			// Token: 0x06000529 RID: 1321 RVA: 0x0001E8D2 File Offset: 0x0001CAD2
			public StringArrayCorruption() : base("StringArrayCorruption", "BadItemLimitStringArrayCorruption")
			{
			}

			// Token: 0x0600052A RID: 1322 RVA: 0x0001E8E4 File Offset: 0x0001CAE4
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return message != null && message.RawFailure != null && message.Failure != null && message.Failure.Message != null && CommonUtils.ExceptionIs(message.RawFailure, new WellKnownException[]
				{
					WellKnownException.CorruptData
				}) && message.Failure.Message.Contains("[String]") && (message.Failure.Message.Contains("[{00062008-0000-0000-c000-000000000046}:0x853a]") || message.Failure.Message.Contains("[{00020329-0000-0000-c000-000000000046}:'Keywords']"));
			}
		}

		// Token: 0x02000075 RID: 117
		private class InvalidMultivalueElementCorruption : BadItemCategory
		{
			// Token: 0x0600052B RID: 1323 RVA: 0x0001E974 File Offset: 0x0001CB74
			public InvalidMultivalueElementCorruption() : base("InvalidMultivalueElement", "BadItemLimitInvalidMultivalueElementCorruption")
			{
			}

			// Token: 0x0600052C RID: 1324 RVA: 0x0001E988 File Offset: 0x0001CB88
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return message != null && message.RawFailure != null && CommonUtils.ExceptionIs(message.RawFailure, new WellKnownException[]
				{
					WellKnownException.InvalidMultivalueElement
				});
			}
		}

		// Token: 0x02000076 RID: 118
		private class NonUnicodeValueCorruption : BadItemCategory
		{
			// Token: 0x0600052D RID: 1325 RVA: 0x0001E9BD File Offset: 0x0001CBBD
			public NonUnicodeValueCorruption() : base("NonUnicodeValueCorruption", "BadItemLimitNonUnicodeValueCorruption")
			{
			}

			// Token: 0x0600052E RID: 1326 RVA: 0x0001E9D0 File Offset: 0x0001CBD0
			public override bool IsMatch(BadMessageRec message, TestIntegration testIntegration)
			{
				return message != null && message.Failure != null && message.Failure.Message != null && message.Failure.Message.Contains("Lid: 37736   dwParam: 0x") && message.Failure.Message.Contains("Lid: 55288   StoreEc: 0x80040117");
			}
		}
	}
}
