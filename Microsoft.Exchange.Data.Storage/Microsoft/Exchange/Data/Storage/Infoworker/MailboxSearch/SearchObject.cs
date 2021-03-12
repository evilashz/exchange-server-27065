using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D2B RID: 3371
	[Serializable]
	public sealed class SearchObject : SearchObjectBase
	{
		// Token: 0x06007475 RID: 29813 RVA: 0x002056BC File Offset: 0x002038BC
		internal static object AqsQueryGetter(IPropertyBag propertyBag)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			int num = 0;
			foreach (string text in ((MultiValuedProperty<string>)propertyBag[SearchObjectSchema.Senders]))
			{
				stringBuilder.Append("\"");
				stringBuilder.Append(text.Replace("\\", "\\\\"));
				stringBuilder.Append("\"*");
				num = stringBuilder.Length;
				stringBuilder.Append(" OR ");
			}
			if (num > 0)
			{
				stringBuilder.Length = num;
			}
			StringBuilder stringBuilder2 = new StringBuilder(64);
			num = 0;
			foreach (string text2 in ((MultiValuedProperty<string>)propertyBag[SearchObjectSchema.Recipients]))
			{
				stringBuilder2.Append("\"");
				stringBuilder2.Append(text2.Replace("\\", "\\\\"));
				stringBuilder2.Append("\"*");
				num = stringBuilder2.Length;
				stringBuilder2.Append(" OR ");
			}
			if (num > 0)
			{
				stringBuilder2.Length = num;
			}
			string text3 = string.Empty;
			ExDateTime? exDateTime = (ExDateTime?)propertyBag[SearchObjectSchema.StartDate];
			ExDateTime? exDateTime2 = SearchObject.RoundEndDate((ExDateTime?)propertyBag[SearchObjectSchema.EndDate]);
			if (exDateTime != null && exDateTime != null)
			{
				text3 = text3 + " >=" + exDateTime.Value.ToString();
			}
			if (exDateTime2 != null && exDateTime2 != null)
			{
				text3 = text3 + " <=" + exDateTime2.Value.ToString();
			}
			StringBuilder stringBuilder3 = new StringBuilder(64);
			num = 0;
			foreach (KindKeyword key in ((MultiValuedProperty<KindKeyword>)propertyBag[SearchObjectSchema.MessageTypes]))
			{
				stringBuilder3.Append(AqsParser.CanonicalKindKeys[key]);
				num = stringBuilder3.Length;
				stringBuilder3.Append(" OR ");
			}
			if (num > 0)
			{
				stringBuilder3.Length = num;
			}
			string text4 = stringBuilder.ToString();
			string text5 = stringBuilder2.ToString();
			string text6 = stringBuilder3.ToString();
			string str = string.Empty;
			text4 = (string.IsNullOrEmpty(text4) ? string.Empty : string.Format("{0}:({1}) ", AqsParser.CanonicalKeywords[PropertyKeyword.From], text4));
			text5 = (string.IsNullOrEmpty(text5) ? string.Empty : string.Format("{0}:({1}) ", AqsParser.CanonicalKeywords[PropertyKeyword.Participants], text5));
			if (!string.IsNullOrEmpty(text4) && !string.IsNullOrEmpty(text5))
			{
				str = string.Format("({0} OR {1}) ", text4, text5);
			}
			else if (!string.IsNullOrEmpty(text4))
			{
				str = text4;
			}
			else if (!string.IsNullOrEmpty(text5))
			{
				str = text5;
			}
			return str + (string.IsNullOrEmpty(text3) ? string.Empty : (AqsParser.CanonicalKeywords[PropertyKeyword.Received] + ":(" + text3 + ") ")) + (string.IsNullOrEmpty(text6) ? string.Empty : (AqsParser.CanonicalKeywords[PropertyKeyword.Kind] + ":(" + text6 + ") ")) + (string)propertyBag[SearchObjectSchema.SearchQuery];
		}

		// Token: 0x17001F05 RID: 7941
		// (get) Token: 0x06007476 RID: 29814 RVA: 0x00205A50 File Offset: 0x00203C50
		// (set) Token: 0x06007477 RID: 29815 RVA: 0x00205A62 File Offset: 0x00203C62
		public ADObjectId CreatedBy
		{
			get
			{
				return (ADObjectId)this[SearchObjectSchema.CreatedBy];
			}
			set
			{
				this[SearchObjectSchema.CreatedBy] = value;
			}
		}

		// Token: 0x17001F06 RID: 7942
		// (get) Token: 0x06007478 RID: 29816 RVA: 0x00205A70 File Offset: 0x00203C70
		// (set) Token: 0x06007479 RID: 29817 RVA: 0x00205A82 File Offset: 0x00203C82
		public string CreatedByEx
		{
			get
			{
				return (string)this[SearchObjectSchema.CreatedByEx];
			}
			set
			{
				this[SearchObjectSchema.CreatedByEx] = value;
			}
		}

		// Token: 0x17001F07 RID: 7943
		// (get) Token: 0x0600747A RID: 29818 RVA: 0x00205A90 File Offset: 0x00203C90
		// (set) Token: 0x0600747B RID: 29819 RVA: 0x00205AA2 File Offset: 0x00203CA2
		public MultiValuedProperty<ADObjectId> SourceMailboxes
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[SearchObjectSchema.SourceMailboxes];
			}
			set
			{
				this[SearchObjectSchema.SourceMailboxes] = value;
			}
		}

		// Token: 0x17001F08 RID: 7944
		// (get) Token: 0x0600747C RID: 29820 RVA: 0x00205AB0 File Offset: 0x00203CB0
		// (set) Token: 0x0600747D RID: 29821 RVA: 0x00205AC2 File Offset: 0x00203CC2
		public ADObjectId TargetMailbox
		{
			get
			{
				return (ADObjectId)this[SearchObjectSchema.TargetMailbox];
			}
			set
			{
				this[SearchObjectSchema.TargetMailbox] = value;
			}
		}

		// Token: 0x17001F09 RID: 7945
		// (get) Token: 0x0600747E RID: 29822 RVA: 0x00205AD0 File Offset: 0x00203CD0
		// (set) Token: 0x0600747F RID: 29823 RVA: 0x00205AE2 File Offset: 0x00203CE2
		[Parameter(Mandatory = false)]
		public string SearchQuery
		{
			get
			{
				return (string)this[SearchObjectSchema.SearchQuery];
			}
			set
			{
				this[SearchObjectSchema.SearchQuery] = value;
			}
		}

		// Token: 0x17001F0A RID: 7946
		// (get) Token: 0x06007480 RID: 29824 RVA: 0x00205AF0 File Offset: 0x00203CF0
		// (set) Token: 0x06007481 RID: 29825 RVA: 0x00205B02 File Offset: 0x00203D02
		[Parameter(Mandatory = false)]
		public CultureInfo Language
		{
			get
			{
				return (CultureInfo)this[SearchObjectSchema.Language];
			}
			set
			{
				this[SearchObjectSchema.Language] = value;
			}
		}

		// Token: 0x17001F0B RID: 7947
		// (get) Token: 0x06007482 RID: 29826 RVA: 0x00205B10 File Offset: 0x00203D10
		// (set) Token: 0x06007483 RID: 29827 RVA: 0x00205B22 File Offset: 0x00203D22
		public MultiValuedProperty<string> Senders
		{
			get
			{
				return (MultiValuedProperty<string>)this[SearchObjectSchema.Senders];
			}
			set
			{
				this[SearchObjectSchema.Senders] = value;
			}
		}

		// Token: 0x17001F0C RID: 7948
		// (get) Token: 0x06007484 RID: 29828 RVA: 0x00205B30 File Offset: 0x00203D30
		// (set) Token: 0x06007485 RID: 29829 RVA: 0x00205B42 File Offset: 0x00203D42
		public MultiValuedProperty<string> Recipients
		{
			get
			{
				return (MultiValuedProperty<string>)this[SearchObjectSchema.Recipients];
			}
			set
			{
				this[SearchObjectSchema.Recipients] = value;
			}
		}

		// Token: 0x17001F0D RID: 7949
		// (get) Token: 0x06007486 RID: 29830 RVA: 0x00205B50 File Offset: 0x00203D50
		// (set) Token: 0x06007487 RID: 29831 RVA: 0x00205B62 File Offset: 0x00203D62
		[Parameter(Mandatory = false)]
		public ExDateTime? StartDate
		{
			get
			{
				return (ExDateTime?)this[SearchObjectSchema.StartDate];
			}
			set
			{
				this[SearchObjectSchema.StartDate] = value;
			}
		}

		// Token: 0x17001F0E RID: 7950
		// (get) Token: 0x06007488 RID: 29832 RVA: 0x00205B75 File Offset: 0x00203D75
		// (set) Token: 0x06007489 RID: 29833 RVA: 0x00205B8C File Offset: 0x00203D8C
		[Parameter(Mandatory = false)]
		public ExDateTime? EndDate
		{
			get
			{
				return SearchObject.RoundEndDate((ExDateTime?)this[SearchObjectSchema.EndDate]);
			}
			set
			{
				this[SearchObjectSchema.EndDate] = value;
			}
		}

		// Token: 0x17001F0F RID: 7951
		// (get) Token: 0x0600748A RID: 29834 RVA: 0x00205B9F File Offset: 0x00203D9F
		// (set) Token: 0x0600748B RID: 29835 RVA: 0x00205BB1 File Offset: 0x00203DB1
		public MultiValuedProperty<KindKeyword> MessageTypes
		{
			get
			{
				return (MultiValuedProperty<KindKeyword>)this[SearchObjectSchema.MessageTypes];
			}
			set
			{
				this[SearchObjectSchema.MessageTypes] = value;
			}
		}

		// Token: 0x17001F10 RID: 7952
		// (get) Token: 0x0600748C RID: 29836 RVA: 0x00205BBF File Offset: 0x00203DBF
		// (set) Token: 0x0600748D RID: 29837 RVA: 0x00205BD1 File Offset: 0x00203DD1
		[Parameter(Mandatory = false)]
		public bool SearchDumpster
		{
			get
			{
				return (bool)this[SearchObjectSchema.SearchDumpster];
			}
			set
			{
				this[SearchObjectSchema.SearchDumpster] = value;
			}
		}

		// Token: 0x17001F11 RID: 7953
		// (get) Token: 0x0600748E RID: 29838 RVA: 0x00205BE4 File Offset: 0x00203DE4
		// (set) Token: 0x0600748F RID: 29839 RVA: 0x00205BF6 File Offset: 0x00203DF6
		[Parameter(Mandatory = false)]
		public LoggingLevel LogLevel
		{
			get
			{
				return (LoggingLevel)this[SearchObjectSchema.LogLevel];
			}
			set
			{
				this[SearchObjectSchema.LogLevel] = value;
			}
		}

		// Token: 0x17001F12 RID: 7954
		// (get) Token: 0x06007490 RID: 29840 RVA: 0x00205C09 File Offset: 0x00203E09
		// (set) Token: 0x06007491 RID: 29841 RVA: 0x00205C1B File Offset: 0x00203E1B
		[Parameter(Mandatory = false)]
		public bool IncludeUnsearchableItems
		{
			get
			{
				return (bool)this[SearchObjectSchema.IncludeUnsearchableItems];
			}
			set
			{
				this[SearchObjectSchema.IncludeUnsearchableItems] = value;
			}
		}

		// Token: 0x17001F13 RID: 7955
		// (get) Token: 0x06007492 RID: 29842 RVA: 0x00205C2E File Offset: 0x00203E2E
		// (set) Token: 0x06007493 RID: 29843 RVA: 0x00205C40 File Offset: 0x00203E40
		public bool IncludePersonalArchive
		{
			get
			{
				return (bool)this[SearchObjectSchema.IncludePersonalArchive];
			}
			set
			{
				this[SearchObjectSchema.IncludePersonalArchive] = value;
			}
		}

		// Token: 0x17001F14 RID: 7956
		// (get) Token: 0x06007494 RID: 29844 RVA: 0x00205C53 File Offset: 0x00203E53
		// (set) Token: 0x06007495 RID: 29845 RVA: 0x00205C65 File Offset: 0x00203E65
		internal bool IncludeRemoteAccounts
		{
			get
			{
				return (bool)this[SearchObjectSchema.IncludeRemoteAccounts];
			}
			set
			{
				this[SearchObjectSchema.IncludeRemoteAccounts] = value;
			}
		}

		// Token: 0x17001F15 RID: 7957
		// (get) Token: 0x06007496 RID: 29846 RVA: 0x00205C78 File Offset: 0x00203E78
		// (set) Token: 0x06007497 RID: 29847 RVA: 0x00205C8A File Offset: 0x00203E8A
		public MultiValuedProperty<ADObjectId> StatusMailRecipients
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[SearchObjectSchema.StatusMailRecipients];
			}
			set
			{
				this[SearchObjectSchema.StatusMailRecipients] = value;
			}
		}

		// Token: 0x17001F16 RID: 7958
		// (get) Token: 0x06007498 RID: 29848 RVA: 0x00205C98 File Offset: 0x00203E98
		// (set) Token: 0x06007499 RID: 29849 RVA: 0x00205CAA File Offset: 0x00203EAA
		public MultiValuedProperty<ADObjectId> ManagedBy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[SearchObjectSchema.ManagedBy];
			}
			set
			{
				this[SearchObjectSchema.ManagedBy] = value;
			}
		}

		// Token: 0x17001F17 RID: 7959
		// (get) Token: 0x0600749A RID: 29850 RVA: 0x00205CB8 File Offset: 0x00203EB8
		// (set) Token: 0x0600749B RID: 29851 RVA: 0x00205CCA File Offset: 0x00203ECA
		public bool EstimateOnly
		{
			get
			{
				return (bool)this[SearchObjectSchema.EstimateOnly];
			}
			set
			{
				this[SearchObjectSchema.EstimateOnly] = value;
			}
		}

		// Token: 0x17001F18 RID: 7960
		// (get) Token: 0x0600749C RID: 29852 RVA: 0x00205CDD File Offset: 0x00203EDD
		// (set) Token: 0x0600749D RID: 29853 RVA: 0x00205CEF File Offset: 0x00203EEF
		[Parameter(Mandatory = false)]
		public bool ExcludeDuplicateMessages
		{
			get
			{
				return (bool)this[SearchObjectSchema.ExcludeDuplicateMessages];
			}
			set
			{
				this[SearchObjectSchema.ExcludeDuplicateMessages] = value;
			}
		}

		// Token: 0x17001F19 RID: 7961
		// (get) Token: 0x0600749E RID: 29854 RVA: 0x00205D02 File Offset: 0x00203F02
		// (set) Token: 0x0600749F RID: 29855 RVA: 0x00205D14 File Offset: 0x00203F14
		public bool Resume
		{
			get
			{
				return (bool)this[SearchObjectSchema.Resume];
			}
			set
			{
				this[SearchObjectSchema.Resume] = value;
			}
		}

		// Token: 0x17001F1A RID: 7962
		// (get) Token: 0x060074A0 RID: 29856 RVA: 0x00205D27 File Offset: 0x00203F27
		// (set) Token: 0x060074A1 RID: 29857 RVA: 0x00205D39 File Offset: 0x00203F39
		public bool IncludeKeywordStatistics
		{
			get
			{
				return (bool)this[SearchObjectSchema.IncludeKeywordStatistics];
			}
			set
			{
				this[SearchObjectSchema.IncludeKeywordStatistics] = value;
			}
		}

		// Token: 0x17001F1B RID: 7963
		// (get) Token: 0x060074A2 RID: 29858 RVA: 0x00205D4C File Offset: 0x00203F4C
		// (set) Token: 0x060074A3 RID: 29859 RVA: 0x00205D5E File Offset: 0x00203F5E
		public bool KeywordStatisticsDisabled
		{
			get
			{
				return (bool)this[SearchObjectSchema.KeywordStatisticsDisabled];
			}
			set
			{
				this[SearchObjectSchema.KeywordStatisticsDisabled] = value;
			}
		}

		// Token: 0x17001F1C RID: 7964
		// (get) Token: 0x060074A4 RID: 29860 RVA: 0x00205D71 File Offset: 0x00203F71
		// (set) Token: 0x060074A5 RID: 29861 RVA: 0x00205D83 File Offset: 0x00203F83
		public MultiValuedProperty<string> Information
		{
			get
			{
				return (MultiValuedProperty<string>)this[SearchObjectSchema.Information];
			}
			set
			{
				this[SearchObjectSchema.Information] = value;
			}
		}

		// Token: 0x17001F1D RID: 7965
		// (get) Token: 0x060074A6 RID: 29862 RVA: 0x00205D91 File Offset: 0x00203F91
		public string AqsQuery
		{
			get
			{
				return (string)this[SearchObjectSchema.AqsQuery];
			}
		}

		// Token: 0x17001F1E RID: 7966
		// (get) Token: 0x060074A7 RID: 29863 RVA: 0x00205DA3 File Offset: 0x00203FA3
		internal override SearchObjectBaseSchema Schema
		{
			get
			{
				return SearchObject.schema;
			}
		}

		// Token: 0x17001F1F RID: 7967
		// (get) Token: 0x060074A8 RID: 29864 RVA: 0x00205DAA File Offset: 0x00203FAA
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.SearchObject;
			}
		}

		// Token: 0x17001F20 RID: 7968
		// (get) Token: 0x060074A9 RID: 29865 RVA: 0x00205DAD File Offset: 0x00203FAD
		// (set) Token: 0x060074AA RID: 29866 RVA: 0x00205DB5 File Offset: 0x00203FB5
		internal SearchStatus SearchStatus { get; set; }

		// Token: 0x060074AB RID: 29867 RVA: 0x00205DC0 File Offset: 0x00203FC0
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.SourceMailboxes.Contains(base.Id.MailboxOwnerId))
			{
				errors.Add(new PropertyValidationError(ServerStrings.DiscoveryMailboxCannotBeSourceOrTarget(base.Id.MailboxOwnerId.DistinguishedName), SearchObjectSchema.SourceMailboxes, this.SourceMailboxes));
			}
			if (this.TargetMailbox != null)
			{
				if (this.SourceMailboxes.Contains(this.TargetMailbox))
				{
					errors.Add(new PropertyValidationError(ServerStrings.SearchTargetInSource, SearchObjectSchema.TargetMailbox, this.TargetMailbox));
				}
				if (this.TargetMailbox.Equals(base.Id.MailboxOwnerId))
				{
					errors.Add(new PropertyValidationError(ServerStrings.DiscoveryMailboxCannotBeSourceOrTarget(base.Id.MailboxOwnerId.DistinguishedName), SearchObjectSchema.TargetMailbox, this.TargetMailbox));
				}
			}
			if (this.StartDate != null && this.EndDate != null && this.StartDate > this.EndDate)
			{
				errors.Add(new PropertyValidationError(ServerStrings.InvalidateDateRange, SearchObjectSchema.StartDate, this.StartDate));
			}
			KindKeyword[] second = new KindKeyword[]
			{
				KindKeyword.faxes,
				KindKeyword.voicemail,
				KindKeyword.rssfeeds,
				KindKeyword.posts
			};
			if (this.MessageTypes.Intersect(second).Count<KindKeyword>() > 0)
			{
				errors.Add(new PropertyValidationError(ServerStrings.UnsupportedKindKeywords, SearchObjectSchema.MessageTypes, this.MessageTypes));
			}
			if (!string.IsNullOrEmpty(this.SearchQuery))
			{
				try
				{
					AqsParser aqsParser = new AqsParser();
					aqsParser.Parse(this.SearchQuery, AqsParser.ParseOption.None, this.Language).Dispose();
				}
				catch (ParserException ex)
				{
					errors.Add(new PropertyValidationError(ex.LocalizedString, SearchObjectSchema.SearchQuery, this.SearchQuery));
				}
			}
		}

		// Token: 0x060074AD RID: 29869 RVA: 0x00205FC0 File Offset: 0x002041C0
		public static ExDateTime? RoundEndDate(ExDateTime? value)
		{
			ExDateTime? result = null;
			if (value != null)
			{
				ExDateTime value2 = value.Value;
				if (value2.Hour == 0 && value2.Minute == 0 && value2.Second == 0)
				{
					result = new ExDateTime?(new ExDateTime(value2.TimeZone, value2.Year, value2.Month, value2.Day, DateTime.MaxValue.Hour, DateTime.MaxValue.Minute, DateTime.MaxValue.Second));
				}
				else if (value2.Second == 0)
				{
					result = new ExDateTime?(new ExDateTime(value2.TimeZone, value2.Year, value2.Month, value2.Day, value2.Hour, value2.Minute, DateTime.MaxValue.Second));
				}
				else
				{
					result = new ExDateTime?(value2);
				}
			}
			return result;
		}

		// Token: 0x04005141 RID: 20801
		private static SearchObjectSchema schema = ObjectSchema.GetInstance<SearchObjectSchema>();
	}
}
