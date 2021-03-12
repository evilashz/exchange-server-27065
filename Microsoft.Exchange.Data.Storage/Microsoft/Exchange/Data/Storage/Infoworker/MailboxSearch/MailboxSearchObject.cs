using System;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D2F RID: 3375
	[Serializable]
	public class MailboxSearchObject : IConfigurable
	{
		// Token: 0x17001F34 RID: 7988
		// (get) Token: 0x060074D9 RID: 29913 RVA: 0x00206CC0 File Offset: 0x00204EC0
		public string Identity
		{
			get
			{
				if (this.discoverySearch != null)
				{
					return this.discoverySearch.Identity.ToString();
				}
				if (this.searchObject != null && this.searchObject.Identity != null)
				{
					return this.searchObject.Identity.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17001F35 RID: 7989
		// (get) Token: 0x060074DA RID: 29914 RVA: 0x00206D11 File Offset: 0x00204F11
		public string Name
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.Name;
				}
				return this.discoverySearch.Name;
			}
		}

		// Token: 0x17001F36 RID: 7990
		// (get) Token: 0x060074DB RID: 29915 RVA: 0x00206D34 File Offset: 0x00204F34
		public string CreatedBy
		{
			get
			{
				if (this.discoverySearch != null)
				{
					return this.discoverySearch.CreatedBy;
				}
				if (!string.IsNullOrEmpty(this.searchObject.CreatedByEx))
				{
					return this.searchObject.CreatedByEx;
				}
				if (this.searchObject.CreatedBy != null)
				{
					return this.searchObject.CreatedBy.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17001F37 RID: 7991
		// (get) Token: 0x060074DC RID: 29916 RVA: 0x00206D96 File Offset: 0x00204F96
		public MultiValuedProperty<ADObjectId> SourceMailboxes
		{
			get
			{
				if (this.discoverySearch != null)
				{
					return this.sourceMailboxes;
				}
				return this.searchObject.SourceMailboxes;
			}
		}

		// Token: 0x17001F38 RID: 7992
		// (get) Token: 0x060074DD RID: 29917 RVA: 0x00206DB2 File Offset: 0x00204FB2
		public MultiValuedProperty<string> Sources
		{
			get
			{
				if (this.discoverySearch != null)
				{
					return this.discoverySearch.Sources;
				}
				return null;
			}
		}

		// Token: 0x17001F39 RID: 7993
		// (get) Token: 0x060074DE RID: 29918 RVA: 0x00206DC9 File Offset: 0x00204FC9
		public bool AllSourceMailboxes
		{
			get
			{
				return this.discoverySearch != null && this.discoverySearch.AllSourceMailboxes;
			}
		}

		// Token: 0x17001F3A RID: 7994
		// (get) Token: 0x060074DF RID: 29919 RVA: 0x00206DE0 File Offset: 0x00204FE0
		public MultiValuedProperty<string> PublicFolderSources
		{
			get
			{
				if (this.discoverySearch != null)
				{
					return this.discoverySearch.PublicFolderSources;
				}
				return null;
			}
		}

		// Token: 0x17001F3B RID: 7995
		// (get) Token: 0x060074E0 RID: 29920 RVA: 0x00206DF7 File Offset: 0x00204FF7
		public bool AllPublicFolderSources
		{
			get
			{
				return this.discoverySearch != null && this.discoverySearch.AllPublicFolderSources;
			}
		}

		// Token: 0x17001F3C RID: 7996
		// (get) Token: 0x060074E1 RID: 29921 RVA: 0x00206E0E File Offset: 0x0020500E
		public MultiValuedProperty<DiscoverySearchStats> SearchStatistics
		{
			get
			{
				if (this.discoverySearch != null)
				{
					return this.discoverySearch.SearchStatistics;
				}
				return null;
			}
		}

		// Token: 0x17001F3D RID: 7997
		// (get) Token: 0x060074E2 RID: 29922 RVA: 0x00206E25 File Offset: 0x00205025
		public SearchObjectVersion Version
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return SearchObjectVersion.Original;
				}
				return this.discoverySearch.Version;
			}
		}

		// Token: 0x17001F3E RID: 7998
		// (get) Token: 0x060074E3 RID: 29923 RVA: 0x00206E3C File Offset: 0x0020503C
		public ADObjectId TargetMailbox
		{
			get
			{
				if (this.discoverySearch != null)
				{
					return this.targetMailbox;
				}
				return this.searchObject.TargetMailbox;
			}
		}

		// Token: 0x17001F3F RID: 7999
		// (get) Token: 0x060074E4 RID: 29924 RVA: 0x00206E58 File Offset: 0x00205058
		public string Target
		{
			get
			{
				if (this.discoverySearch != null)
				{
					return this.discoverySearch.Target;
				}
				return null;
			}
		}

		// Token: 0x17001F40 RID: 8000
		// (get) Token: 0x060074E5 RID: 29925 RVA: 0x00206E6F File Offset: 0x0020506F
		public string SearchQuery
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.SearchQuery;
				}
				return this.discoverySearch.Query;
			}
		}

		// Token: 0x17001F41 RID: 8001
		// (get) Token: 0x060074E6 RID: 29926 RVA: 0x00206E90 File Offset: 0x00205090
		public CultureInfo Language
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.Language;
				}
				return new CultureInfo(this.discoverySearch.Language);
			}
		}

		// Token: 0x17001F42 RID: 8002
		// (get) Token: 0x060074E7 RID: 29927 RVA: 0x00206EB6 File Offset: 0x002050B6
		public MultiValuedProperty<string> Senders
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.Senders;
				}
				return this.discoverySearch.Senders;
			}
		}

		// Token: 0x17001F43 RID: 8003
		// (get) Token: 0x060074E8 RID: 29928 RVA: 0x00206ED7 File Offset: 0x002050D7
		public MultiValuedProperty<string> Recipients
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.Recipients;
				}
				return this.discoverySearch.Recipients;
			}
		}

		// Token: 0x17001F44 RID: 8004
		// (get) Token: 0x060074E9 RID: 29929 RVA: 0x00206EF8 File Offset: 0x002050F8
		public ExDateTime? StartDate
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.StartDate;
				}
				return this.discoverySearch.StartDate;
			}
		}

		// Token: 0x17001F45 RID: 8005
		// (get) Token: 0x060074EA RID: 29930 RVA: 0x00206F19 File Offset: 0x00205119
		public ExDateTime? EndDate
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.EndDate;
				}
				return this.discoverySearch.EndDate;
			}
		}

		// Token: 0x17001F46 RID: 8006
		// (get) Token: 0x060074EB RID: 29931 RVA: 0x00206F3A File Offset: 0x0020513A
		public MultiValuedProperty<KindKeyword> MessageTypes
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.MessageTypes;
				}
				return this.discoverySearch.MessageTypes;
			}
		}

		// Token: 0x17001F47 RID: 8007
		// (get) Token: 0x060074EC RID: 29932 RVA: 0x00206F5B File Offset: 0x0020515B
		public bool IncludeUnsearchableItems
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.IncludeUnsearchableItems;
				}
				return this.discoverySearch.IncludeUnsearchableItems;
			}
		}

		// Token: 0x17001F48 RID: 8008
		// (get) Token: 0x060074ED RID: 29933 RVA: 0x00206F7C File Offset: 0x0020517C
		public bool EstimateOnly
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.EstimateOnly;
				}
				return this.discoverySearch.StatisticsOnly;
			}
		}

		// Token: 0x17001F49 RID: 8009
		// (get) Token: 0x060074EE RID: 29934 RVA: 0x00206F9D File Offset: 0x0020519D
		public bool ExcludeDuplicateMessages
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.ExcludeDuplicateMessages;
				}
				return this.discoverySearch.ExcludeDuplicateMessages;
			}
		}

		// Token: 0x17001F4A RID: 8010
		// (get) Token: 0x060074EF RID: 29935 RVA: 0x00206FBE File Offset: 0x002051BE
		public bool Resume
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.Resume;
				}
				return this.discoverySearch.Resume;
			}
		}

		// Token: 0x17001F4B RID: 8011
		// (get) Token: 0x060074F0 RID: 29936 RVA: 0x00206FDF File Offset: 0x002051DF
		public bool IncludeKeywordStatistics
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.IncludeKeywordStatistics;
				}
				return this.discoverySearch.IncludeKeywordStatistics;
			}
		}

		// Token: 0x17001F4C RID: 8012
		// (get) Token: 0x060074F1 RID: 29937 RVA: 0x00207000 File Offset: 0x00205200
		public bool KeywordStatisticsDisabled
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.KeywordStatisticsDisabled;
				}
				return this.discoverySearch.KeywordStatisticsDisabled;
			}
		}

		// Token: 0x17001F4D RID: 8013
		// (get) Token: 0x060074F2 RID: 29938 RVA: 0x00207021 File Offset: 0x00205221
		public bool PreviewDisabled
		{
			get
			{
				return this.discoverySearch != null && this.discoverySearch.PreviewDisabled;
			}
		}

		// Token: 0x17001F4E RID: 8014
		// (get) Token: 0x060074F3 RID: 29939 RVA: 0x00207038 File Offset: 0x00205238
		public MultiValuedProperty<string> Information
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.Information;
				}
				return this.discoverySearch.Information;
			}
		}

		// Token: 0x17001F4F RID: 8015
		// (get) Token: 0x060074F4 RID: 29940 RVA: 0x00207059 File Offset: 0x00205259
		public int StatisticsStartIndex
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return 1;
				}
				return this.discoverySearch.StatisticsStartIndex;
			}
		}

		// Token: 0x17001F50 RID: 8016
		// (get) Token: 0x060074F5 RID: 29941 RVA: 0x00207070 File Offset: 0x00205270
		public int TotalKeywords
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return 0;
				}
				return this.discoverySearch.TotalKeywords;
			}
		}

		// Token: 0x17001F51 RID: 8017
		// (get) Token: 0x060074F6 RID: 29942 RVA: 0x00207087 File Offset: 0x00205287
		public LoggingLevel LogLevel
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.LogLevel;
				}
				return this.discoverySearch.LogLevel;
			}
		}

		// Token: 0x17001F52 RID: 8018
		// (get) Token: 0x060074F7 RID: 29943 RVA: 0x002070A8 File Offset: 0x002052A8
		public MultiValuedProperty<ADObjectId> StatusMailRecipients
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.StatusMailRecipients;
				}
				return this.discoverySearch.StatusMailRecipients;
			}
		}

		// Token: 0x17001F53 RID: 8019
		// (get) Token: 0x060074F8 RID: 29944 RVA: 0x002070C9 File Offset: 0x002052C9
		public SearchState Status
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchStatus.Status;
				}
				return this.discoverySearch.Status;
			}
		}

		// Token: 0x17001F54 RID: 8020
		// (get) Token: 0x060074F9 RID: 29945 RVA: 0x002070EC File Offset: 0x002052EC
		public string LastRunBy
		{
			get
			{
				if (this.discoverySearch != null)
				{
					return this.discoverySearch.LastModifiedBy;
				}
				if (!string.IsNullOrEmpty(this.searchStatus.LastRunByEx))
				{
					return this.searchStatus.LastRunByEx;
				}
				if (this.searchStatus.LastRunBy != null)
				{
					return this.searchStatus.LastRunBy.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17001F55 RID: 8021
		// (get) Token: 0x060074FA RID: 29946 RVA: 0x00207150 File Offset: 0x00205350
		public ExDateTime? LastStartTime
		{
			get
			{
				ExDateTime? result = (this.searchStatus != null) ? this.searchStatus.LastStartTime : null;
				if (this.discoverySearch != null && this.discoverySearch.LastStartTime != default(DateTime))
				{
					result = new ExDateTime?((ExDateTime)this.discoverySearch.LastStartTime);
				}
				return result;
			}
		}

		// Token: 0x17001F56 RID: 8022
		// (get) Token: 0x060074FB RID: 29947 RVA: 0x002071B8 File Offset: 0x002053B8
		public ExDateTime? LastEndTime
		{
			get
			{
				ExDateTime? result = (this.searchStatus != null) ? this.searchStatus.LastEndTime : null;
				if (this.discoverySearch != null && this.discoverySearch.LastEndTime != default(DateTime))
				{
					result = new ExDateTime?((ExDateTime)this.discoverySearch.LastEndTime);
				}
				return result;
			}
		}

		// Token: 0x17001F57 RID: 8023
		// (get) Token: 0x060074FC RID: 29948 RVA: 0x0020721F File Offset: 0x0020541F
		public int NumberMailboxesToSearch
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchStatus.NumberMailboxesToSearch;
				}
				return this.discoverySearch.NumberOfMailboxes;
			}
		}

		// Token: 0x17001F58 RID: 8024
		// (get) Token: 0x060074FD RID: 29949 RVA: 0x00207240 File Offset: 0x00205440
		public int PercentComplete
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchStatus.PercentComplete;
				}
				return this.discoverySearch.PercentComplete;
			}
		}

		// Token: 0x17001F59 RID: 8025
		// (get) Token: 0x060074FE RID: 29950 RVA: 0x00207261 File Offset: 0x00205461
		public long ResultNumber
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchStatus.ResultNumber;
				}
				return this.discoverySearch.ResultItemCountCopied;
			}
		}

		// Token: 0x17001F5A RID: 8026
		// (get) Token: 0x060074FF RID: 29951 RVA: 0x00207282 File Offset: 0x00205482
		public long ResultNumberEstimate
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchStatus.ResultNumberEstimate;
				}
				return this.discoverySearch.ResultItemCountEstimate;
			}
		}

		// Token: 0x17001F5B RID: 8027
		// (get) Token: 0x06007500 RID: 29952 RVA: 0x002072A3 File Offset: 0x002054A3
		public ByteQuantifiedSize ResultSize
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchStatus.ResultSize;
				}
				return new ByteQuantifiedSize((ulong)this.discoverySearch.ResultSizeCopied);
			}
		}

		// Token: 0x17001F5C RID: 8028
		// (get) Token: 0x06007501 RID: 29953 RVA: 0x002072C9 File Offset: 0x002054C9
		public ByteQuantifiedSize ResultSizeEstimate
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchStatus.ResultSizeEstimate;
				}
				return new ByteQuantifiedSize((ulong)this.discoverySearch.ResultSizeEstimate);
			}
		}

		// Token: 0x17001F5D RID: 8029
		// (get) Token: 0x06007502 RID: 29954 RVA: 0x002072EF File Offset: 0x002054EF
		public ByteQuantifiedSize ResultSizeCopied
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchStatus.ResultSizeCopied;
				}
				return new ByteQuantifiedSize((ulong)this.discoverySearch.ResultSizeCopied);
			}
		}

		// Token: 0x17001F5E RID: 8030
		// (get) Token: 0x06007503 RID: 29955 RVA: 0x00207315 File Offset: 0x00205515
		public string ResultsLink
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchStatus.ResultsLink;
				}
				return this.discoverySearch.ResultsLink;
			}
		}

		// Token: 0x17001F5F RID: 8031
		// (get) Token: 0x06007504 RID: 29956 RVA: 0x00207336 File Offset: 0x00205536
		public string PreviewResultsLink
		{
			get
			{
				if (this.discoverySearch != null && this.Status != SearchState.NotStarted && this.Status != SearchState.EstimateFailed && this.Status != SearchState.Failed)
				{
					return this.discoverySearch.PreviewResultsLink;
				}
				return null;
			}
		}

		// Token: 0x17001F60 RID: 8032
		// (get) Token: 0x06007505 RID: 29957 RVA: 0x00207369 File Offset: 0x00205569
		public MultiValuedProperty<string> Errors
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchStatus.Errors;
				}
				return this.discoverySearch.Errors;
			}
		}

		// Token: 0x17001F61 RID: 8033
		// (get) Token: 0x06007506 RID: 29958 RVA: 0x0020738A File Offset: 0x0020558A
		public bool InPlaceHoldEnabled
		{
			get
			{
				return this.discoverySearch != null && this.discoverySearch.InPlaceHoldEnabled;
			}
		}

		// Token: 0x17001F62 RID: 8034
		// (get) Token: 0x06007507 RID: 29959 RVA: 0x002073A1 File Offset: 0x002055A1
		public Unlimited<EnhancedTimeSpan> ItemHoldPeriod
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return Unlimited<EnhancedTimeSpan>.UnlimitedValue;
				}
				return this.discoverySearch.ItemHoldPeriod;
			}
		}

		// Token: 0x17001F63 RID: 8035
		// (get) Token: 0x06007508 RID: 29960 RVA: 0x002073BC File Offset: 0x002055BC
		public string InPlaceHoldIdentity
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return string.Empty;
				}
				return this.discoverySearch.InPlaceHoldIdentity;
			}
		}

		// Token: 0x17001F64 RID: 8036
		// (get) Token: 0x06007509 RID: 29961 RVA: 0x002073D7 File Offset: 0x002055D7
		public string ManagedByOrganization
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return string.Empty;
				}
				if (!(this.discoverySearch.ManagedByOrganization == "b5d6efcd-1aee-42b9-b168-6fef285fe613"))
				{
					return this.discoverySearch.ManagedByOrganization;
				}
				return ServerStrings.ManagedByRemoteExchangeOrganization;
			}
		}

		// Token: 0x17001F65 RID: 8037
		// (get) Token: 0x0600750A RID: 29962 RVA: 0x00207414 File Offset: 0x00205614
		public MultiValuedProperty<string> FailedToHoldMailboxes
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return null;
				}
				return this.discoverySearch.FailedToHoldMailboxes;
			}
		}

		// Token: 0x17001F66 RID: 8038
		// (get) Token: 0x0600750B RID: 29963 RVA: 0x0020742B File Offset: 0x0020562B
		public MultiValuedProperty<string> InPlaceHoldErrors
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return null;
				}
				return this.discoverySearch.InPlaceHoldErrors;
			}
		}

		// Token: 0x17001F67 RID: 8039
		// (get) Token: 0x0600750C RID: 29964 RVA: 0x00207442 File Offset: 0x00205642
		public string Description
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return ServerStrings.LegacyMailboxSearchDescription;
				}
				return this.discoverySearch.Description;
			}
		}

		// Token: 0x17001F68 RID: 8040
		// (get) Token: 0x0600750D RID: 29965 RVA: 0x00207464 File Offset: 0x00205664
		public ExDateTime? LastModifiedTime
		{
			get
			{
				if (this.discoverySearch != null)
				{
					DateTime lastModifiedTime = this.discoverySearch.LastModifiedTime;
					return new ExDateTime?((ExDateTime)this.discoverySearch.LastModifiedTime.ToUniversalTime());
				}
				if (this.searchStatus != null && this.searchStatus.LastStartTime != null)
				{
					return this.searchStatus.LastStartTime;
				}
				return null;
			}
		}

		// Token: 0x17001F69 RID: 8041
		// (get) Token: 0x0600750E RID: 29966 RVA: 0x002074FC File Offset: 0x002056FC
		public MultiValuedProperty<KeywordHit> KeywordHits
		{
			get
			{
				if (this.discoverySearch != null)
				{
					return new MultiValuedProperty<KeywordHit>((from hit in this.discoverySearch.KeywordHits
					where hit.Phrase != "652beee2-75f7-4ca0-8a02-0698a3919cb9"
					select hit).ToArray<KeywordHit>());
				}
				return new MultiValuedProperty<KeywordHit>((from hit in this.searchStatus.KeywordHits
				where hit.Phrase != "652beee2-75f7-4ca0-8a02-0698a3919cb9"
				select hit).ToArray<KeywordHit>());
			}
		}

		// Token: 0x17001F6A RID: 8042
		// (get) Token: 0x0600750F RID: 29967 RVA: 0x00207580 File Offset: 0x00205780
		internal MultiValuedProperty<KeywordHit> AllKeywordHits
		{
			get
			{
				if (this.discoverySearch != null)
				{
					return new MultiValuedProperty<KeywordHit>(this.discoverySearch.KeywordHits);
				}
				return this.searchStatus.KeywordHits;
			}
		}

		// Token: 0x06007510 RID: 29968 RVA: 0x002075A6 File Offset: 0x002057A6
		public MailboxSearchObject() : this(new SearchObject(), new SearchStatus())
		{
		}

		// Token: 0x06007511 RID: 29969 RVA: 0x002075B8 File Offset: 0x002057B8
		internal MailboxSearchObject(SearchObject searchObject) : this(searchObject, new SearchStatus())
		{
		}

		// Token: 0x06007512 RID: 29970 RVA: 0x002075C6 File Offset: 0x002057C6
		internal MailboxSearchObject(SearchObject searchObject, SearchStatus searchStatus)
		{
			if (searchObject == null)
			{
				throw new ArgumentNullException("searchObject");
			}
			if (searchStatus == null)
			{
				throw new ArgumentException("searchStatus");
			}
			this.searchObject = searchObject;
			this.searchStatus = searchStatus;
		}

		// Token: 0x06007513 RID: 29971 RVA: 0x002075F8 File Offset: 0x002057F8
		internal MailboxSearchObject(MailboxDiscoverySearch discoverySearch, OrganizationId orgId)
		{
			if (discoverySearch == null)
			{
				throw new ArgumentNullException("discoverySearch");
			}
			this.discoverySearch = discoverySearch;
			if (orgId != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), orgId, null, false);
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 791, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Search\\MailboxSearch\\MailboxSearchObject.cs");
				if (this.discoverySearch.Sources != null && this.discoverySearch.Sources.Count > 0)
				{
					if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
					{
						tenantOrRootOrgRecipientSession.SessionSettings.IncludeInactiveMailbox = true;
					}
					Result<ADRawEntry>[] array = tenantOrRootOrgRecipientSession.FindByLegacyExchangeDNs(this.discoverySearch.Sources.ToArray(), null);
					if (array != null && array.Length > 0)
					{
						this.sourceMailboxes = new MultiValuedProperty<ADObjectId>();
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i].Data != null)
							{
								this.sourceMailboxes.Add(array[i].Data.Id);
							}
						}
					}
				}
				if (!string.IsNullOrEmpty(this.discoverySearch.Target))
				{
					tenantOrRootOrgRecipientSession.SessionSettings.IncludeInactiveMailbox = false;
					ADRawEntry adrawEntry = tenantOrRootOrgRecipientSession.FindByLegacyExchangeDN(this.discoverySearch.Target);
					if (adrawEntry != null)
					{
						this.targetMailbox = adrawEntry.Id;
					}
				}
			}
		}

		// Token: 0x17001F6B RID: 8043
		// (get) Token: 0x06007514 RID: 29972 RVA: 0x00207749 File Offset: 0x00205949
		ObjectId IConfigurable.Identity
		{
			get
			{
				if (this.discoverySearch == null)
				{
					return this.searchObject.Identity;
				}
				return this.discoverySearch.Identity;
			}
		}

		// Token: 0x06007515 RID: 29973 RVA: 0x0020776A File Offset: 0x0020596A
		ValidationError[] IConfigurable.Validate()
		{
			return Array<ValidationError>.Empty;
		}

		// Token: 0x17001F6C RID: 8044
		// (get) Token: 0x06007516 RID: 29974 RVA: 0x00207771 File Offset: 0x00205971
		bool IConfigurable.IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F6D RID: 8045
		// (get) Token: 0x06007517 RID: 29975 RVA: 0x00207774 File Offset: 0x00205974
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x06007518 RID: 29976 RVA: 0x00207777 File Offset: 0x00205977
		void IConfigurable.ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06007519 RID: 29977 RVA: 0x0020777E File Offset: 0x0020597E
		void IConfigurable.CopyChangesFrom(IConfigurable changedObject)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400516F RID: 20847
		private SearchObject searchObject;

		// Token: 0x04005170 RID: 20848
		private SearchStatus searchStatus;

		// Token: 0x04005171 RID: 20849
		private MailboxDiscoverySearch discoverySearch;

		// Token: 0x04005172 RID: 20850
		private MultiValuedProperty<ADObjectId> sourceMailboxes;

		// Token: 0x04005173 RID: 20851
		private ADObjectId targetMailbox;
	}
}
