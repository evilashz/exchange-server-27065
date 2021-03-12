using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Search.KqlParser;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D12 RID: 3346
	[Serializable]
	public sealed class MailboxDiscoverySearch : DiscoverySearchBase
	{
		// Token: 0x17001EAB RID: 7851
		// (get) Token: 0x0600733A RID: 29498 RVA: 0x001FED3B File Offset: 0x001FCF3B
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxDiscoverySearch.schema;
			}
		}

		// Token: 0x17001EAC RID: 7852
		// (get) Token: 0x0600733B RID: 29499 RVA: 0x001FED42 File Offset: 0x001FCF42
		internal override string ItemClass
		{
			get
			{
				return "IPM.Configuration.MailboxDiscoverySearch";
			}
		}

		// Token: 0x17001EAD RID: 7853
		// (get) Token: 0x0600733C RID: 29500 RVA: 0x001FED49 File Offset: 0x001FCF49
		// (set) Token: 0x0600733D RID: 29501 RVA: 0x001FED5B File Offset: 0x001FCF5B
		public string Target
		{
			get
			{
				return (string)this[MailboxDiscoverySearchSchema.Target];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.Target] = value;
			}
		}

		// Token: 0x17001EAE RID: 7854
		// (get) Token: 0x0600733E RID: 29502 RVA: 0x001FED69 File Offset: 0x001FCF69
		// (set) Token: 0x0600733F RID: 29503 RVA: 0x001FED7B File Offset: 0x001FCF7B
		public SearchState Status
		{
			get
			{
				return (SearchState)this[MailboxDiscoverySearchSchema.Status];
			}
			private set
			{
				this[MailboxDiscoverySearchSchema.Status] = value;
			}
		}

		// Token: 0x17001EAF RID: 7855
		// (get) Token: 0x06007340 RID: 29504 RVA: 0x001FED8E File Offset: 0x001FCF8E
		// (set) Token: 0x06007341 RID: 29505 RVA: 0x001FEDA0 File Offset: 0x001FCFA0
		public bool StatisticsOnly
		{
			get
			{
				return (bool)this[MailboxDiscoverySearchSchema.StatisticsOnly];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.StatisticsOnly] = value;
			}
		}

		// Token: 0x17001EB0 RID: 7856
		// (get) Token: 0x06007342 RID: 29506 RVA: 0x001FEDB3 File Offset: 0x001FCFB3
		// (set) Token: 0x06007343 RID: 29507 RVA: 0x001FEDC5 File Offset: 0x001FCFC5
		[Parameter(Mandatory = false)]
		public bool IncludeUnsearchableItems
		{
			get
			{
				return (bool)this[MailboxDiscoverySearchSchema.IncludeUnsearchableItems];
			}
			set
			{
				this[MailboxDiscoverySearchSchema.IncludeUnsearchableItems] = value;
			}
		}

		// Token: 0x17001EB1 RID: 7857
		// (get) Token: 0x06007344 RID: 29508 RVA: 0x001FEDD8 File Offset: 0x001FCFD8
		// (set) Token: 0x06007345 RID: 29509 RVA: 0x001FEDEA File Offset: 0x001FCFEA
		public bool Resume
		{
			get
			{
				return (bool)this[MailboxDiscoverySearchSchema.Resume];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.Resume] = value;
			}
		}

		// Token: 0x17001EB2 RID: 7858
		// (get) Token: 0x06007346 RID: 29510 RVA: 0x001FEDFD File Offset: 0x001FCFFD
		// (set) Token: 0x06007347 RID: 29511 RVA: 0x001FEE0F File Offset: 0x001FD00F
		public DateTime LastStartTime
		{
			get
			{
				return (DateTime)this[MailboxDiscoverySearchSchema.LastStartTime];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.LastStartTime] = value;
			}
		}

		// Token: 0x17001EB3 RID: 7859
		// (get) Token: 0x06007348 RID: 29512 RVA: 0x001FEE22 File Offset: 0x001FD022
		// (set) Token: 0x06007349 RID: 29513 RVA: 0x001FEE34 File Offset: 0x001FD034
		public DateTime LastEndTime
		{
			get
			{
				return (DateTime)this[MailboxDiscoverySearchSchema.LastEndTime];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.LastEndTime] = value;
			}
		}

		// Token: 0x17001EB4 RID: 7860
		// (get) Token: 0x0600734A RID: 29514 RVA: 0x001FEE47 File Offset: 0x001FD047
		// (set) Token: 0x0600734B RID: 29515 RVA: 0x001FEE59 File Offset: 0x001FD059
		public int NumberOfMailboxes
		{
			get
			{
				return (int)this[MailboxDiscoverySearchSchema.NumberOfMailboxes];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.NumberOfMailboxes] = value;
			}
		}

		// Token: 0x17001EB5 RID: 7861
		// (get) Token: 0x0600734C RID: 29516 RVA: 0x001FEE6C File Offset: 0x001FD06C
		// (set) Token: 0x0600734D RID: 29517 RVA: 0x001FEE7E File Offset: 0x001FD07E
		public int PercentComplete
		{
			get
			{
				return (int)this[MailboxDiscoverySearchSchema.PercentComplete];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.PercentComplete] = value;
			}
		}

		// Token: 0x17001EB6 RID: 7862
		// (get) Token: 0x0600734E RID: 29518 RVA: 0x001FEE91 File Offset: 0x001FD091
		// (set) Token: 0x0600734F RID: 29519 RVA: 0x001FEEA3 File Offset: 0x001FD0A3
		public long ResultItemCountCopied
		{
			get
			{
				return (long)this[MailboxDiscoverySearchSchema.ResultItemCountCopied];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.ResultItemCountCopied] = value;
			}
		}

		// Token: 0x17001EB7 RID: 7863
		// (get) Token: 0x06007350 RID: 29520 RVA: 0x001FEEB6 File Offset: 0x001FD0B6
		// (set) Token: 0x06007351 RID: 29521 RVA: 0x001FEEC8 File Offset: 0x001FD0C8
		public long ResultItemCountEstimate
		{
			get
			{
				return (long)this[MailboxDiscoverySearchSchema.ResultItemCountEstimate];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.ResultItemCountEstimate] = value;
			}
		}

		// Token: 0x17001EB8 RID: 7864
		// (get) Token: 0x06007352 RID: 29522 RVA: 0x001FEEDB File Offset: 0x001FD0DB
		// (set) Token: 0x06007353 RID: 29523 RVA: 0x001FEEED File Offset: 0x001FD0ED
		public long ResultUnsearchableItemCountEstimate
		{
			get
			{
				return (long)this[MailboxDiscoverySearchSchema.ResultUnsearchableItemCountEstimate];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.ResultUnsearchableItemCountEstimate] = value;
			}
		}

		// Token: 0x17001EB9 RID: 7865
		// (get) Token: 0x06007354 RID: 29524 RVA: 0x001FEF00 File Offset: 0x001FD100
		// (set) Token: 0x06007355 RID: 29525 RVA: 0x001FEF12 File Offset: 0x001FD112
		public long ResultSizeCopied
		{
			get
			{
				return (long)this[MailboxDiscoverySearchSchema.ResultSizeCopied];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.ResultSizeCopied] = value;
			}
		}

		// Token: 0x17001EBA RID: 7866
		// (get) Token: 0x06007356 RID: 29526 RVA: 0x001FEF25 File Offset: 0x001FD125
		// (set) Token: 0x06007357 RID: 29527 RVA: 0x001FEF37 File Offset: 0x001FD137
		public long ResultSizeEstimate
		{
			get
			{
				return (long)this[MailboxDiscoverySearchSchema.ResultSizeEstimate];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.ResultSizeEstimate] = value;
			}
		}

		// Token: 0x17001EBB RID: 7867
		// (get) Token: 0x06007358 RID: 29528 RVA: 0x001FEF4A File Offset: 0x001FD14A
		// (set) Token: 0x06007359 RID: 29529 RVA: 0x001FEF5C File Offset: 0x001FD15C
		public string ResultsPath
		{
			get
			{
				return (string)this[MailboxDiscoverySearchSchema.ResultsPath];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.ResultsPath] = value;
			}
		}

		// Token: 0x17001EBC RID: 7868
		// (get) Token: 0x0600735A RID: 29530 RVA: 0x001FEF6A File Offset: 0x001FD16A
		// (set) Token: 0x0600735B RID: 29531 RVA: 0x001FEF7C File Offset: 0x001FD17C
		public string ResultsLink
		{
			get
			{
				return (string)this[MailboxDiscoverySearchSchema.ResultsLink];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.ResultsLink] = value;
			}
		}

		// Token: 0x17001EBD RID: 7869
		// (get) Token: 0x0600735C RID: 29532 RVA: 0x001FEF8A File Offset: 0x001FD18A
		// (set) Token: 0x0600735D RID: 29533 RVA: 0x001FEF9C File Offset: 0x001FD19C
		public string PreviewResultsLink
		{
			get
			{
				return (string)this[MailboxDiscoverySearchSchema.PreviewResultsLink];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.PreviewResultsLink] = value;
			}
		}

		// Token: 0x17001EBE RID: 7870
		// (get) Token: 0x0600735E RID: 29534 RVA: 0x001FEFAA File Offset: 0x001FD1AA
		// (set) Token: 0x0600735F RID: 29535 RVA: 0x001FEFBC File Offset: 0x001FD1BC
		[Parameter(Mandatory = false)]
		public LoggingLevel LogLevel
		{
			get
			{
				return (LoggingLevel)this[MailboxDiscoverySearchSchema.LogLevel];
			}
			set
			{
				this[MailboxDiscoverySearchSchema.LogLevel] = value;
			}
		}

		// Token: 0x17001EBF RID: 7871
		// (get) Token: 0x06007360 RID: 29536 RVA: 0x001FEFCF File Offset: 0x001FD1CF
		// (set) Token: 0x06007361 RID: 29537 RVA: 0x001FEFE1 File Offset: 0x001FD1E1
		public MultiValuedProperty<string> CompletedMailboxes
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxDiscoverySearchSchema.CompletedMailboxes];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.CompletedMailboxes] = value;
			}
		}

		// Token: 0x17001EC0 RID: 7872
		// (get) Token: 0x06007362 RID: 29538 RVA: 0x001FEFEF File Offset: 0x001FD1EF
		// (set) Token: 0x06007363 RID: 29539 RVA: 0x001FF001 File Offset: 0x001FD201
		public MultiValuedProperty<ADObjectId> StatusMailRecipients
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailboxDiscoverySearchSchema.StatusMailRecipients];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.StatusMailRecipients] = value;
			}
		}

		// Token: 0x17001EC1 RID: 7873
		// (get) Token: 0x06007364 RID: 29540 RVA: 0x001FF00F File Offset: 0x001FD20F
		// (set) Token: 0x06007365 RID: 29541 RVA: 0x001FF021 File Offset: 0x001FD221
		public MultiValuedProperty<ADObjectId> ManagedBy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailboxDiscoverySearchSchema.ManagedBy];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.ManagedBy] = value;
			}
		}

		// Token: 0x17001EC2 RID: 7874
		// (get) Token: 0x06007366 RID: 29542 RVA: 0x001FF02F File Offset: 0x001FD22F
		// (set) Token: 0x06007367 RID: 29543 RVA: 0x001FF044 File Offset: 0x001FD244
		public string Query
		{
			get
			{
				return (string)this[MailboxDiscoverySearchSchema.Query];
			}
			internal set
			{
				string a = (string)this[MailboxDiscoverySearchSchema.Query];
				this[MailboxDiscoverySearchSchema.Query] = value;
				if (a != value)
				{
					this.internalQueryFilter = null;
					this.CalculatedQuery = null;
				}
			}
		}

		// Token: 0x17001EC3 RID: 7875
		// (get) Token: 0x06007368 RID: 29544 RVA: 0x001FF085 File Offset: 0x001FD285
		// (set) Token: 0x06007369 RID: 29545 RVA: 0x001FF098 File Offset: 0x001FD298
		public MultiValuedProperty<string> Senders
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxDiscoverySearchSchema.Senders];
			}
			internal set
			{
				MultiValuedProperty<string> col = (MultiValuedProperty<string>)this[MailboxDiscoverySearchSchema.Senders];
				if (!MailboxDiscoverySearch.AreCollectionsEqual<string>(col, value))
				{
					this.internalQueryFilter = null;
					this.CalculatedQuery = null;
				}
				this[MailboxDiscoverySearchSchema.Senders] = value;
			}
		}

		// Token: 0x17001EC4 RID: 7876
		// (get) Token: 0x0600736A RID: 29546 RVA: 0x001FF0D9 File Offset: 0x001FD2D9
		// (set) Token: 0x0600736B RID: 29547 RVA: 0x001FF0EC File Offset: 0x001FD2EC
		public MultiValuedProperty<string> Recipients
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxDiscoverySearchSchema.Recipients];
			}
			internal set
			{
				MultiValuedProperty<string> col = (MultiValuedProperty<string>)this[MailboxDiscoverySearchSchema.Recipients];
				if (!MailboxDiscoverySearch.AreCollectionsEqual<string>(col, value))
				{
					this.internalQueryFilter = null;
					this.CalculatedQuery = null;
				}
				this[MailboxDiscoverySearchSchema.Recipients] = value;
			}
		}

		// Token: 0x17001EC5 RID: 7877
		// (get) Token: 0x0600736C RID: 29548 RVA: 0x001FF12D File Offset: 0x001FD32D
		// (set) Token: 0x0600736D RID: 29549 RVA: 0x001FF140 File Offset: 0x001FD340
		public ExDateTime? StartDate
		{
			get
			{
				return (ExDateTime?)this[MailboxDiscoverySearchSchema.StartDate];
			}
			set
			{
				if ((ExDateTime?)this[MailboxDiscoverySearchSchema.StartDate] != value)
				{
					this.internalQueryFilter = null;
					this.CalculatedQuery = null;
				}
				this[MailboxDiscoverySearchSchema.StartDate] = value;
			}
		}

		// Token: 0x17001EC6 RID: 7878
		// (get) Token: 0x0600736E RID: 29550 RVA: 0x001FF1B5 File Offset: 0x001FD3B5
		// (set) Token: 0x0600736F RID: 29551 RVA: 0x001FF1CC File Offset: 0x001FD3CC
		public ExDateTime? EndDate
		{
			get
			{
				return SearchObject.RoundEndDate((ExDateTime?)this[MailboxDiscoverySearchSchema.EndDate]);
			}
			set
			{
				if ((ExDateTime?)this[MailboxDiscoverySearchSchema.EndDate] != value)
				{
					this.internalQueryFilter = null;
					this.CalculatedQuery = null;
				}
				this[MailboxDiscoverySearchSchema.EndDate] = value;
			}
		}

		// Token: 0x17001EC7 RID: 7879
		// (get) Token: 0x06007370 RID: 29552 RVA: 0x001FF244 File Offset: 0x001FD444
		// (set) Token: 0x06007371 RID: 29553 RVA: 0x001FF2D0 File Offset: 0x001FD4D0
		public MultiValuedProperty<KindKeyword> MessageTypes
		{
			get
			{
				if (this[MailboxDiscoverySearchSchema.MessageTypes] != null)
				{
					MultiValuedProperty<KindKeyword> multiValuedProperty = new MultiValuedProperty<KindKeyword>();
					MultiValuedProperty<string> multiValuedProperty2 = (MultiValuedProperty<string>)this[MailboxDiscoverySearchSchema.MessageTypes];
					foreach (string value in multiValuedProperty2)
					{
						multiValuedProperty.Add((KindKeyword)Enum.Parse(typeof(KindKeyword), value, true));
					}
					return multiValuedProperty;
				}
				return null;
			}
			internal set
			{
				MultiValuedProperty<string> multiValuedProperty = null;
				if (value != null)
				{
					multiValuedProperty = new MultiValuedProperty<string>();
					foreach (KindKeyword kindKeyword in value)
					{
						multiValuedProperty.Add(kindKeyword.ToString());
					}
				}
				MultiValuedProperty<string> col = this[MailboxDiscoverySearchSchema.MessageTypes] as MultiValuedProperty<string>;
				if (!MailboxDiscoverySearch.AreCollectionsEqual<string>(col, multiValuedProperty))
				{
					this.internalQueryFilter = null;
					this.CalculatedQuery = null;
				}
				this[MailboxDiscoverySearchSchema.MessageTypes] = multiValuedProperty;
			}
		}

		// Token: 0x17001EC8 RID: 7880
		// (get) Token: 0x06007372 RID: 29554 RVA: 0x001FF368 File Offset: 0x001FD568
		// (set) Token: 0x06007373 RID: 29555 RVA: 0x001FF37A File Offset: 0x001FD57A
		public string CalculatedQuery
		{
			get
			{
				return (string)this[MailboxDiscoverySearchSchema.CalculatedQuery];
			}
			private set
			{
				this[MailboxDiscoverySearchSchema.CalculatedQuery] = value;
			}
		}

		// Token: 0x17001EC9 RID: 7881
		// (get) Token: 0x06007374 RID: 29556 RVA: 0x001FF388 File Offset: 0x001FD588
		// (set) Token: 0x06007375 RID: 29557 RVA: 0x001FF39A File Offset: 0x001FD59A
		public MultiValuedProperty<string> UserKeywords
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxDiscoverySearchSchema.UserKeywords];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.UserKeywords] = value;
			}
		}

		// Token: 0x17001ECA RID: 7882
		// (get) Token: 0x06007376 RID: 29558 RVA: 0x001FF3A8 File Offset: 0x001FD5A8
		// (set) Token: 0x06007377 RID: 29559 RVA: 0x001FF3BA File Offset: 0x001FD5BA
		public string KeywordsQuery
		{
			get
			{
				return (string)this[MailboxDiscoverySearchSchema.KeywordsQuery];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.KeywordsQuery] = value;
			}
		}

		// Token: 0x17001ECB RID: 7883
		// (get) Token: 0x06007378 RID: 29560 RVA: 0x001FF3C8 File Offset: 0x001FD5C8
		// (set) Token: 0x06007379 RID: 29561 RVA: 0x001FF3DA File Offset: 0x001FD5DA
		[Parameter(Mandatory = false)]
		public string Language
		{
			get
			{
				return (string)this[MailboxDiscoverySearchSchema.Language];
			}
			set
			{
				this[MailboxDiscoverySearchSchema.Language] = value;
				this.queryCulture = null;
			}
		}

		// Token: 0x17001ECC RID: 7884
		// (get) Token: 0x0600737A RID: 29562 RVA: 0x001FF3EF File Offset: 0x001FD5EF
		// (set) Token: 0x0600737B RID: 29563 RVA: 0x001FF401 File Offset: 0x001FD601
		public MultiValuedProperty<string> Sources
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxDiscoverySearchSchema.Sources];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.Sources] = value;
			}
		}

		// Token: 0x17001ECD RID: 7885
		// (get) Token: 0x0600737C RID: 29564 RVA: 0x001FF40F File Offset: 0x001FD60F
		// (set) Token: 0x0600737D RID: 29565 RVA: 0x001FF421 File Offset: 0x001FD621
		public bool AllSourceMailboxes
		{
			get
			{
				return (bool)this[MailboxDiscoverySearchSchema.AllSourceMailboxes];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.AllSourceMailboxes] = value;
			}
		}

		// Token: 0x17001ECE RID: 7886
		// (get) Token: 0x0600737E RID: 29566 RVA: 0x001FF434 File Offset: 0x001FD634
		// (set) Token: 0x0600737F RID: 29567 RVA: 0x001FF446 File Offset: 0x001FD646
		public MultiValuedProperty<string> PublicFolderSources
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxDiscoverySearchSchema.PublicFolderSources];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.PublicFolderSources] = value;
			}
		}

		// Token: 0x17001ECF RID: 7887
		// (get) Token: 0x06007380 RID: 29568 RVA: 0x001FF454 File Offset: 0x001FD654
		// (set) Token: 0x06007381 RID: 29569 RVA: 0x001FF466 File Offset: 0x001FD666
		public bool AllPublicFolderSources
		{
			get
			{
				return (bool)this[MailboxDiscoverySearchSchema.AllPublicFolderSources];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.AllPublicFolderSources] = value;
			}
		}

		// Token: 0x17001ED0 RID: 7888
		// (get) Token: 0x06007382 RID: 29570 RVA: 0x001FF479 File Offset: 0x001FD679
		// (set) Token: 0x06007383 RID: 29571 RVA: 0x001FF48B File Offset: 0x001FD68B
		public MultiValuedProperty<DiscoverySearchStats> SearchStatistics
		{
			get
			{
				return (MultiValuedProperty<DiscoverySearchStats>)this[MailboxDiscoverySearchSchema.SearchStatistics];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.SearchStatistics] = value;
			}
		}

		// Token: 0x17001ED1 RID: 7889
		// (get) Token: 0x06007384 RID: 29572 RVA: 0x001FF499 File Offset: 0x001FD699
		// (set) Token: 0x06007385 RID: 29573 RVA: 0x001FF4AB File Offset: 0x001FD6AB
		public SearchObjectVersion Version
		{
			get
			{
				return (SearchObjectVersion)this[MailboxDiscoverySearchSchema.Version];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.Version] = value;
			}
		}

		// Token: 0x17001ED2 RID: 7890
		// (get) Token: 0x06007386 RID: 29574 RVA: 0x001FF4BE File Offset: 0x001FD6BE
		public DateTime CreatedTime
		{
			get
			{
				return (DateTime)this[MailboxDiscoverySearchSchema.CreatedTime];
			}
		}

		// Token: 0x17001ED3 RID: 7891
		// (get) Token: 0x06007387 RID: 29575 RVA: 0x001FF4D0 File Offset: 0x001FD6D0
		// (set) Token: 0x06007388 RID: 29576 RVA: 0x001FF4E2 File Offset: 0x001FD6E2
		public string CreatedBy
		{
			get
			{
				return (string)this[MailboxDiscoverySearchSchema.CreatedBy];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.CreatedBy] = value;
			}
		}

		// Token: 0x17001ED4 RID: 7892
		// (get) Token: 0x06007389 RID: 29577 RVA: 0x001FF4F0 File Offset: 0x001FD6F0
		public DateTime LastModifiedTime
		{
			get
			{
				return (DateTime)this[MailboxDiscoverySearchSchema.LastModifiedTime];
			}
		}

		// Token: 0x17001ED5 RID: 7893
		// (get) Token: 0x0600738A RID: 29578 RVA: 0x001FF502 File Offset: 0x001FD702
		// (set) Token: 0x0600738B RID: 29579 RVA: 0x001FF514 File Offset: 0x001FD714
		public string LastModifiedBy
		{
			get
			{
				return (string)this[MailboxDiscoverySearchSchema.LastModifiedBy];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.LastModifiedBy] = value;
			}
		}

		// Token: 0x17001ED6 RID: 7894
		// (get) Token: 0x0600738C RID: 29580 RVA: 0x001FF522 File Offset: 0x001FD722
		// (set) Token: 0x0600738D RID: 29581 RVA: 0x001FF534 File Offset: 0x001FD734
		[Parameter(Mandatory = false)]
		public bool ExcludeDuplicateMessages
		{
			get
			{
				return (bool)this[MailboxDiscoverySearchSchema.ExcludeDuplicateMessages];
			}
			set
			{
				this[MailboxDiscoverySearchSchema.ExcludeDuplicateMessages] = value;
			}
		}

		// Token: 0x17001ED7 RID: 7895
		// (get) Token: 0x0600738E RID: 29582 RVA: 0x001FF547 File Offset: 0x001FD747
		// (set) Token: 0x0600738F RID: 29583 RVA: 0x001FF559 File Offset: 0x001FD759
		public MultiValuedProperty<string> Errors
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxDiscoverySearchSchema.Errors];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.Errors] = value;
			}
		}

		// Token: 0x17001ED8 RID: 7896
		// (get) Token: 0x06007390 RID: 29584 RVA: 0x001FF567 File Offset: 0x001FD767
		// (set) Token: 0x06007391 RID: 29585 RVA: 0x001FF579 File Offset: 0x001FD779
		public MultiValuedProperty<KeywordHit> KeywordHits
		{
			get
			{
				return (MultiValuedProperty<KeywordHit>)this[MailboxDiscoverySearchSchema.KeywordHits];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.KeywordHits] = value;
			}
		}

		// Token: 0x17001ED9 RID: 7897
		// (get) Token: 0x06007392 RID: 29586 RVA: 0x001FF587 File Offset: 0x001FD787
		// (set) Token: 0x06007393 RID: 29587 RVA: 0x001FF599 File Offset: 0x001FD799
		public MultiValuedProperty<string> Information
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxDiscoverySearchSchema.Information];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.Information] = value;
			}
		}

		// Token: 0x17001EDA RID: 7898
		// (get) Token: 0x06007394 RID: 29588 RVA: 0x001FF5A7 File Offset: 0x001FD7A7
		// (set) Token: 0x06007395 RID: 29589 RVA: 0x001FF5B9 File Offset: 0x001FD7B9
		public bool KeywordStatisticsDisabled
		{
			get
			{
				return (bool)this[MailboxDiscoverySearchSchema.KeywordStatisticsDisabled];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.KeywordStatisticsDisabled] = value;
			}
		}

		// Token: 0x17001EDB RID: 7899
		// (get) Token: 0x06007396 RID: 29590 RVA: 0x001FF5CC File Offset: 0x001FD7CC
		// (set) Token: 0x06007397 RID: 29591 RVA: 0x001FF5DE File Offset: 0x001FD7DE
		public bool PreviewDisabled
		{
			get
			{
				return (bool)this[MailboxDiscoverySearchSchema.PreviewDisabled];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.PreviewDisabled] = value;
			}
		}

		// Token: 0x17001EDC RID: 7900
		// (get) Token: 0x06007398 RID: 29592 RVA: 0x001FF5F1 File Offset: 0x001FD7F1
		// (set) Token: 0x06007399 RID: 29593 RVA: 0x001FF603 File Offset: 0x001FD803
		public string ScenarioId
		{
			get
			{
				return this[MailboxDiscoverySearchSchema.ScenarioId] as string;
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.ScenarioId] = value;
			}
		}

		// Token: 0x17001EDD RID: 7901
		// (get) Token: 0x0600739A RID: 29594 RVA: 0x001FF611 File Offset: 0x001FD811
		// (set) Token: 0x0600739B RID: 29595 RVA: 0x001FF623 File Offset: 0x001FD823
		[Parameter]
		public bool InPlaceHoldEnabled
		{
			get
			{
				return (bool)this[MailboxDiscoverySearchSchema.InPlaceHoldEnabled];
			}
			set
			{
				this[MailboxDiscoverySearchSchema.InPlaceHoldEnabled] = value;
			}
		}

		// Token: 0x17001EDE RID: 7902
		// (get) Token: 0x0600739C RID: 29596 RVA: 0x001FF636 File Offset: 0x001FD836
		// (set) Token: 0x0600739D RID: 29597 RVA: 0x001FF648 File Offset: 0x001FD848
		[Parameter]
		public Unlimited<EnhancedTimeSpan> ItemHoldPeriod
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)this[MailboxDiscoverySearchSchema.ItemHoldPeriod];
			}
			set
			{
				this[MailboxDiscoverySearchSchema.ItemHoldPeriod] = value;
			}
		}

		// Token: 0x17001EDF RID: 7903
		// (get) Token: 0x0600739E RID: 29598 RVA: 0x001FF65B File Offset: 0x001FD85B
		// (set) Token: 0x0600739F RID: 29599 RVA: 0x001FF66D File Offset: 0x001FD86D
		[Parameter]
		public string Description
		{
			get
			{
				return (string)this[MailboxDiscoverySearchSchema.Description];
			}
			set
			{
				this[MailboxDiscoverySearchSchema.Description] = value;
			}
		}

		// Token: 0x17001EE0 RID: 7904
		// (get) Token: 0x060073A0 RID: 29600 RVA: 0x001FF67B File Offset: 0x001FD87B
		// (set) Token: 0x060073A1 RID: 29601 RVA: 0x001FF68D File Offset: 0x001FD88D
		public string InPlaceHoldIdentity
		{
			get
			{
				return (string)this[MailboxDiscoverySearchSchema.InPlaceHoldIdentity];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.InPlaceHoldIdentity] = value;
			}
		}

		// Token: 0x17001EE1 RID: 7905
		// (get) Token: 0x060073A2 RID: 29602 RVA: 0x001FF69B File Offset: 0x001FD89B
		// (set) Token: 0x060073A3 RID: 29603 RVA: 0x001FF6AD File Offset: 0x001FD8AD
		public string LegacySearchObjectIdentity
		{
			get
			{
				return (string)this[MailboxDiscoverySearchSchema.LegacySearchObjectIdentity];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.LegacySearchObjectIdentity] = value;
			}
		}

		// Token: 0x17001EE2 RID: 7906
		// (get) Token: 0x060073A4 RID: 29604 RVA: 0x001FF6BB File Offset: 0x001FD8BB
		// (set) Token: 0x060073A5 RID: 29605 RVA: 0x001FF6CD File Offset: 0x001FD8CD
		public MultiValuedProperty<string> FailedToHoldMailboxes
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxDiscoverySearchSchema.FailedToHoldMailboxes];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.FailedToHoldMailboxes] = value;
			}
		}

		// Token: 0x17001EE3 RID: 7907
		// (get) Token: 0x060073A6 RID: 29606 RVA: 0x001FF6DB File Offset: 0x001FD8DB
		// (set) Token: 0x060073A7 RID: 29607 RVA: 0x001FF6ED File Offset: 0x001FD8ED
		public MultiValuedProperty<string> InPlaceHoldErrors
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxDiscoverySearchSchema.InPlaceHoldErrors];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.InPlaceHoldErrors] = value;
			}
		}

		// Token: 0x17001EE4 RID: 7908
		// (get) Token: 0x060073A8 RID: 29608 RVA: 0x001FF6FB File Offset: 0x001FD8FB
		// (set) Token: 0x060073A9 RID: 29609 RVA: 0x001FF70D File Offset: 0x001FD90D
		public string ManagedByOrganization
		{
			get
			{
				return (string)this[MailboxDiscoverySearchSchema.ManagedByOrganization];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.ManagedByOrganization] = value;
			}
		}

		// Token: 0x17001EE5 RID: 7909
		// (get) Token: 0x060073AA RID: 29610 RVA: 0x001FF71B File Offset: 0x001FD91B
		internal string FlightedFeatures
		{
			get
			{
				if (this.flightedFeatures == null)
				{
					return string.Empty;
				}
				return string.Join(",", this.flightedFeatures);
			}
		}

		// Token: 0x17001EE6 RID: 7910
		// (get) Token: 0x060073AB RID: 29611 RVA: 0x001FF73C File Offset: 0x001FD93C
		private Dictionary<SearchState, Dictionary<SearchStateTransition, SearchState>> StateMachine
		{
			get
			{
				if (this.stateMachine == null)
				{
					Dictionary<SearchState, Dictionary<SearchStateTransition, SearchState>> value = new Dictionary<SearchState, Dictionary<SearchStateTransition, SearchState>>
					{
						{
							SearchState.InProgress,
							new Dictionary<SearchStateTransition, SearchState>
							{
								{
									SearchStateTransition.StopSearch,
									SearchState.Stopped
								},
								{
									SearchStateTransition.MoveToNextState,
									SearchState.Succeeded
								},
								{
									SearchStateTransition.MoveToNextPartialSuccessState,
									SearchState.PartiallySucceeded
								},
								{
									SearchStateTransition.Fail,
									SearchState.Failed
								}
							}
						},
						{
							SearchState.Failed,
							new Dictionary<SearchStateTransition, SearchState>
							{
								{
									SearchStateTransition.StartSearch,
									SearchState.Queued
								},
								{
									SearchStateTransition.ResetSearch,
									SearchState.NotStarted
								},
								{
									SearchStateTransition.DeleteSearch,
									SearchState.DeletionInProgress
								}
							}
						},
						{
							SearchState.Stopped,
							new Dictionary<SearchStateTransition, SearchState>
							{
								{
									SearchStateTransition.StartSearch,
									SearchState.Queued
								},
								{
									SearchStateTransition.ResetSearch,
									SearchState.NotStarted
								},
								{
									SearchStateTransition.DeleteSearch,
									SearchState.DeletionInProgress
								},
								{
									SearchStateTransition.StopSearch,
									SearchState.Stopped
								}
							}
						},
						{
							SearchState.Succeeded,
							new Dictionary<SearchStateTransition, SearchState>
							{
								{
									SearchStateTransition.StartSearch,
									SearchState.Queued
								},
								{
									SearchStateTransition.ResetSearch,
									SearchState.NotStarted
								},
								{
									SearchStateTransition.DeleteSearch,
									SearchState.DeletionInProgress
								}
							}
						},
						{
							SearchState.PartiallySucceeded,
							new Dictionary<SearchStateTransition, SearchState>
							{
								{
									SearchStateTransition.StartSearch,
									SearchState.Queued
								},
								{
									SearchStateTransition.ResetSearch,
									SearchState.NotStarted
								},
								{
									SearchStateTransition.DeleteSearch,
									SearchState.DeletionInProgress
								}
							}
						},
						{
							SearchState.EstimateInProgress,
							new Dictionary<SearchStateTransition, SearchState>
							{
								{
									SearchStateTransition.DeleteSearch,
									SearchState.DeletionInProgress
								},
								{
									SearchStateTransition.StopSearch,
									SearchState.EstimateStopped
								},
								{
									SearchStateTransition.MoveToNextState,
									SearchState.EstimateSucceeded
								},
								{
									SearchStateTransition.MoveToNextPartialSuccessState,
									SearchState.EstimatePartiallySucceeded
								},
								{
									SearchStateTransition.Fail,
									SearchState.EstimateFailed
								}
							}
						},
						{
							SearchState.EstimateSucceeded,
							new Dictionary<SearchStateTransition, SearchState>
							{
								{
									SearchStateTransition.StartSearch,
									SearchState.Queued
								},
								{
									SearchStateTransition.ResetSearch,
									SearchState.NotStarted
								},
								{
									SearchStateTransition.DeleteSearch,
									SearchState.DeletionInProgress
								}
							}
						},
						{
							SearchState.EstimateFailed,
							new Dictionary<SearchStateTransition, SearchState>
							{
								{
									SearchStateTransition.StartSearch,
									SearchState.Queued
								},
								{
									SearchStateTransition.ResetSearch,
									SearchState.NotStarted
								},
								{
									SearchStateTransition.DeleteSearch,
									SearchState.DeletionInProgress
								}
							}
						},
						{
							SearchState.EstimateStopped,
							new Dictionary<SearchStateTransition, SearchState>
							{
								{
									SearchStateTransition.StartSearch,
									SearchState.Queued
								},
								{
									SearchStateTransition.ResetSearch,
									SearchState.NotStarted
								},
								{
									SearchStateTransition.DeleteSearch,
									SearchState.DeletionInProgress
								},
								{
									SearchStateTransition.StopSearch,
									SearchState.Stopped
								}
							}
						},
						{
							SearchState.EstimatePartiallySucceeded,
							new Dictionary<SearchStateTransition, SearchState>
							{
								{
									SearchStateTransition.StartSearch,
									SearchState.Queued
								},
								{
									SearchStateTransition.ResetSearch,
									SearchState.NotStarted
								},
								{
									SearchStateTransition.DeleteSearch,
									SearchState.DeletionInProgress
								}
							}
						},
						{
							SearchState.DeletionInProgress,
							new Dictionary<SearchStateTransition, SearchState>()
						},
						{
							SearchState.NotStarted,
							new Dictionary<SearchStateTransition, SearchState>
							{
								{
									SearchStateTransition.StartSearch,
									SearchState.Queued
								},
								{
									SearchStateTransition.ResetSearch,
									SearchState.NotStarted
								},
								{
									SearchStateTransition.DeleteSearch,
									SearchState.DeletionInProgress
								}
							}
						},
						{
							SearchState.Queued,
							new Dictionary<SearchStateTransition, SearchState>
							{
								{
									SearchStateTransition.DeleteSearch,
									SearchState.DeletionInProgress
								},
								{
									SearchStateTransition.StopSearch,
									this.StatisticsOnly ? SearchState.EstimateStopped : SearchState.Stopped
								},
								{
									SearchStateTransition.MoveToNextState,
									this.StatisticsOnly ? SearchState.EstimateInProgress : SearchState.InProgress
								},
								{
									SearchStateTransition.Fail,
									SearchState.Failed
								}
							}
						}
					};
					Interlocked.CompareExchange<Dictionary<SearchState, Dictionary<SearchStateTransition, SearchState>>>(ref this.stateMachine, value, null);
				}
				return this.stateMachine;
			}
		}

		// Token: 0x17001EE7 RID: 7911
		// (get) Token: 0x060073AC RID: 29612 RVA: 0x001FF9E8 File Offset: 0x001FDBE8
		// (set) Token: 0x060073AD RID: 29613 RVA: 0x001FF9FA File Offset: 0x001FDBFA
		public bool IncludeKeywordStatistics
		{
			get
			{
				return (bool)this[MailboxDiscoverySearchSchema.IncludeKeywordStatistics];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.IncludeKeywordStatistics] = value;
			}
		}

		// Token: 0x17001EE8 RID: 7912
		// (get) Token: 0x060073AE RID: 29614 RVA: 0x001FFA0D File Offset: 0x001FDC0D
		// (set) Token: 0x060073AF RID: 29615 RVA: 0x001FFA1F File Offset: 0x001FDC1F
		public int StatisticsStartIndex
		{
			get
			{
				return (int)this[MailboxDiscoverySearchSchema.StatisticsStartIndex];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.StatisticsStartIndex] = value;
			}
		}

		// Token: 0x17001EE9 RID: 7913
		// (get) Token: 0x060073B0 RID: 29616 RVA: 0x001FFA32 File Offset: 0x001FDC32
		// (set) Token: 0x060073B1 RID: 29617 RVA: 0x001FFA44 File Offset: 0x001FDC44
		public int TotalKeywords
		{
			get
			{
				return (int)this[MailboxDiscoverySearchSchema.TotalKeywords];
			}
			internal set
			{
				this[MailboxDiscoverySearchSchema.TotalKeywords] = value;
			}
		}

		// Token: 0x17001EEA RID: 7914
		// (get) Token: 0x060073B2 RID: 29618 RVA: 0x001FFA74 File Offset: 0x001FDC74
		// (set) Token: 0x060073B3 RID: 29619 RVA: 0x001FFAE0 File Offset: 0x001FDCE0
		internal QueryFilter InternalQueryFilter
		{
			get
			{
				if (string.IsNullOrEmpty(this.CalculatedQuery))
				{
					this.UpdateCalculatedQuery();
				}
				bool flag = this.CalculatedQuery == MailboxDiscoverySearch.EmptyQueryReplacement;
				if (this.internalQueryFilter == null && !flag)
				{
					this.internalQueryFilter = MailboxDiscoverySearch.CalculateQueryFilter(this.CalculatedQuery, this.QueryCulture, delegate(Exception ex)
					{
						ExTraceGlobals.StorageTracer.TraceError<string, Exception>(0L, "Failed to parse the query string in the MailboxDiscoverySearch.InternalQueryFilter getter. The query is: '{0}'. Exception: {1}", this.CalculatedQuery, ex);
					});
				}
				return this.internalQueryFilter;
			}
			set
			{
				this.internalQueryFilter = value;
			}
		}

		// Token: 0x17001EEB RID: 7915
		// (get) Token: 0x060073B4 RID: 29620 RVA: 0x001FFAEC File Offset: 0x001FDCEC
		internal CultureInfo QueryCulture
		{
			get
			{
				if (this.queryCulture == null)
				{
					this.queryCulture = CultureInfo.InvariantCulture;
					this.languageIsInvalid = false;
					if (!string.IsNullOrEmpty(this.Language))
					{
						try
						{
							this.queryCulture = new CultureInfo(this.Language);
						}
						catch (CultureNotFoundException)
						{
							this.languageIsInvalid = true;
							ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "Culture info: \"{0}\" returns CultureNotFoundException", this.Language);
						}
					}
				}
				return this.queryCulture;
			}
		}

		// Token: 0x060073B5 RID: 29621 RVA: 0x001FFB70 File Offset: 0x001FDD70
		internal bool IsFeatureFlighted(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("MailboxDiscoverySearch.IsFeatureFlighted - invalid feature name");
			}
			if (this.flightedFeatures == null)
			{
				return false;
			}
			foreach (string value in this.flightedFeatures)
			{
				if (name.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060073B6 RID: 29622 RVA: 0x001FFBEC File Offset: 0x001FDDEC
		internal void AddFlightedFeature(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("MailboxDiscoverySearch.AddFlightedFeature - invalid feature name");
			}
			if (this.flightedFeatures == null)
			{
				this.flightedFeatures = new List<string>();
			}
			if (!this.IsFeatureFlighted(name))
			{
				this.flightedFeatures.Add(name);
			}
		}

		// Token: 0x060073B7 RID: 29623 RVA: 0x001FFC29 File Offset: 0x001FDE29
		internal static bool IsInProgressState(SearchState searchState)
		{
			return searchState == SearchState.Queued || searchState == SearchState.InProgress || searchState == SearchState.EstimateInProgress;
		}

		// Token: 0x060073B8 RID: 29624 RVA: 0x001FFC3A File Offset: 0x001FDE3A
		internal static bool IsInDeletionState(SearchState searchState)
		{
			return searchState == SearchState.DeletionInProgress;
		}

		// Token: 0x060073B9 RID: 29625 RVA: 0x001FFC44 File Offset: 0x001FDE44
		internal static QueryFilter CalculateQueryFilter(string query, CultureInfo queryCulture, Action<Exception> exceptionHandler)
		{
			QueryFilter result;
			try
			{
				result = KqlParser.ParseAndBuildQuery(query, KqlParser.ParseOption.DisablePrefixMatch | KqlParser.ParseOption.AllowShortWildcards | KqlParser.ParseOption.EDiscoveryMode, queryCulture, RescopedAll.Default, null, null);
			}
			catch (ParserException obj)
			{
				exceptionHandler(obj);
				result = null;
			}
			return result;
		}

		// Token: 0x060073BA RID: 29626 RVA: 0x001FFC80 File Offset: 0x001FDE80
		internal static IDisposable SetAllowSettingStatus(bool? setStatus)
		{
			return MailboxDiscoverySearch.AllowSettingStatus.SetTestHook(setStatus);
		}

		// Token: 0x060073BB RID: 29627 RVA: 0x001FFC8D File Offset: 0x001FDE8D
		internal static QueryFilter GetRetentionPeriodFilter(EnhancedTimeSpan period)
		{
			return new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.ReceivedTime, ExDateTime.UtcNow - period);
		}

		// Token: 0x060073BC RID: 29628 RVA: 0x001FFCCC File Offset: 0x001FDECC
		internal static string GetKeywordPhrase(QueryFilter filter, string userQuery, ref int position)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(?i)");
			IEnumerable<string> enumerable = filter.Keywords();
			stringBuilder.Append("\\(*");
			int num = 0;
			foreach (string text in enumerable)
			{
				stringBuilder.Append("[\\\"]?");
				stringBuilder.Append(text.TrimEnd(new char[]
				{
					'?'
				}).Replace("*", "\\*"));
				stringBuilder.Append("\\*?\\??[\\\"]?.*?");
				if (++num < enumerable.Count<string>() - 1)
				{
					stringBuilder.Append("\\b");
				}
			}
			stringBuilder.Append("\\)*");
			Regex regex = new Regex(stringBuilder.ToString());
			Match match = regex.Match(userQuery, position);
			string text2;
			if (match.Success)
			{
				position = match.Index + match.Length;
				int num2 = match.Value.Count((char c) => c == '(');
				int num3 = match.Value.Count((char c) => c == ')');
				if (num2 < num3)
				{
					text2 = new string('(', num3 - num2) + match.Value;
				}
				else if (num3 < num2)
				{
					text2 = match.Value + new string(')', num2 - num3);
				}
				else
				{
					text2 = match.Value;
				}
			}
			else
			{
				text2 = filter.Keywords().Aggregate((string i, string j) => i + " " + j);
			}
			if (filter is NotFilter)
			{
				text2 = ServerStrings.NotOperator + " " + filter.PropertyName + text2;
			}
			else
			{
				text2 = filter.PropertyName + text2;
			}
			return text2;
		}

		// Token: 0x060073BD RID: 29629 RVA: 0x001FFEE8 File Offset: 0x001FE0E8
		internal static void AddInPlaceHold(ADRecipient recipient, string holdId, IRecipientSession updateSession)
		{
			if (recipient == null)
			{
				throw new ArgumentException("recipient");
			}
			if (updateSession == null)
			{
				throw new ArgumentException("updateSession");
			}
			if (recipient.InPlaceHolds == null)
			{
				recipient.InPlaceHolds = new MultiValuedProperty<string>();
			}
			if (!recipient.InPlaceHolds.Contains(holdId))
			{
				recipient.InPlaceHolds.Add(holdId);
				if (recipient is ADUser && recipient.InPlaceHolds.Count == 1)
				{
					RecoverableItemsQuotaHelper.IncreaseRecoverableItemsQuotaIfNeeded((ADUser)recipient);
				}
				if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && recipient.IsSoftDeleted && !recipient.IsInactiveMailbox && recipient.InPlaceHolds.Count == 1)
				{
					((ADUser)recipient).UpdateSoftDeletedStatusForHold(true);
				}
				updateSession.Save(recipient, true);
			}
		}

		// Token: 0x060073BE RID: 29630 RVA: 0x001FFFAC File Offset: 0x001FE1AC
		internal static void RemoveInPlaceHold(ADRecipient recipient, string holdId, IRecipientSession updateSession)
		{
			if (updateSession == null)
			{
				throw new ArgumentException("updateSession");
			}
			if (recipient != null && recipient.InPlaceHolds != null && recipient.InPlaceHolds.Contains(holdId))
			{
				recipient.InPlaceHolds.Remove(holdId);
				if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && recipient.IsSoftDeleted && recipient.IsInactiveMailbox && !((ADUser)recipient).IsInLitigationHoldOrInplaceHold)
				{
					((ADUser)recipient).UpdateSoftDeletedStatusForHold(false);
				}
				updateSession.Save(recipient, true);
			}
		}

		// Token: 0x060073BF RID: 29631 RVA: 0x00200038 File Offset: 0x001FE238
		private static bool AreCollectionsEqual<T>(ICollection<T> col1, ICollection<T> col2)
		{
			if (col1 == null && col2 == null)
			{
				return true;
			}
			if (col1 != null != (col2 != null))
			{
				return false;
			}
			if (col1.Count != col2.Count)
			{
				return false;
			}
			foreach (T item in col1)
			{
				if (!col2.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060073C0 RID: 29632 RVA: 0x002000B4 File Offset: 0x001FE2B4
		private static void ReportJobsProgress(int totalPossibleJobs, ref int totalProcessedJobs, int jobsDone, Action<int> reportProgress)
		{
			if (reportProgress != null)
			{
				int num = totalProcessedJobs * 100 / totalPossibleJobs;
				totalProcessedJobs += jobsDone;
				int num2 = totalProcessedJobs * 100 / totalPossibleJobs;
				if (num != num2)
				{
					reportProgress(num2);
				}
			}
		}

		// Token: 0x060073C1 RID: 29633 RVA: 0x002000E8 File Offset: 0x001FE2E8
		private static bool CanSetStatus()
		{
			bool result = false;
			if (MailboxDiscoverySearch.AllowSettingStatus.Value != null && MailboxDiscoverySearch.AllowSettingStatus.Value == true)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060073C2 RID: 29634 RVA: 0x00200130 File Offset: 0x001FE330
		internal void UpdateState(SearchStateTransition action)
		{
			ExAssert.RetailAssert(this.StateMachine.ContainsKey(this.Status), "The state machine does not contain the current state transitions");
			Dictionary<SearchStateTransition, SearchState> dictionary = this.StateMachine[this.Status];
			string formatString = string.Format("The action {0} is not recognized by the state machine for the state {1}", action.ToString(), this.Status.ToString());
			ExAssert.RetailAssert(dictionary.ContainsKey(action), formatString);
			if (dictionary.ContainsKey(action))
			{
				this.Status = dictionary[action];
			}
		}

		// Token: 0x060073C3 RID: 29635 RVA: 0x002001B3 File Offset: 0x001FE3B3
		internal void SetStatus(SearchState status)
		{
			if (!MailboxDiscoverySearch.CanSetStatus())
			{
				throw new Exception("Can set status only in tests. Please use UpdateState");
			}
			this.Status = status;
		}

		// Token: 0x060073C4 RID: 29636 RVA: 0x002001CE File Offset: 0x001FE3CE
		internal void SynchronizeHoldSettings(DiscoverySearchDataProvider dataProvider, IRecipientSession recipientSession, bool holdEnabled)
		{
			this.SynchronizeHoldSettings(dataProvider, recipientSession, holdEnabled, null);
		}

		// Token: 0x060073C5 RID: 29637 RVA: 0x002001DC File Offset: 0x001FE3DC
		internal LocalizedString SynchronizeHoldSettings(DiscoverySearchDataProvider dataProvider, IRecipientSession recipientSession, bool holdEnabled, Action<int> reportProgress)
		{
			if (!"b5d6efcd-1aee-42b9-b168-6fef285fe613".Equals(this.ManagedByOrganization ?? string.Empty, StringComparison.OrdinalIgnoreCase))
			{
				bool includeSoftDeletedObjects = recipientSession.SessionSettings.IncludeSoftDeletedObjects;
				bool includeInactiveMailbox = recipientSession.SessionSettings.IncludeInactiveMailbox;
				try
				{
					if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
					{
						recipientSession.SessionSettings.IncludeSoftDeletedObjects = true;
					}
					else
					{
						recipientSession.SessionSettings.IncludeSoftDeletedObjects = false;
						recipientSession.SessionSettings.IncludeInactiveMailbox = false;
					}
					Dictionary<string, ADObjectId> dictionary = this.FetchCurrentOnHoldMailboxes(dataProvider, recipientSession);
					int totalPossibleJobs = dictionary.Count + this.Sources.Count + 2;
					int num = 0;
					MailboxDiscoverySearch.ReportJobsProgress(totalPossibleJobs, ref num, 1, reportProgress);
					this.InPlaceHoldErrors = new MultiValuedProperty<string>();
					this.FailedToHoldMailboxes = new MultiValuedProperty<string>();
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.PartiallyConsistent, recipientSession.SessionSettings, 1455, "SynchronizeHoldSettings", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Search\\DiscoverySearch\\MailboxDiscoverySearch.cs");
					if (holdEnabled)
					{
						List<string> list = this.Sources.Intersect(dictionary.Keys).ToList<string>();
						string[] array = this.Sources.Except(dictionary.Keys).ToArray<string>();
						foreach (string key in list)
						{
							dictionary.Remove(key);
							MailboxDiscoverySearch.ReportJobsProgress(totalPossibleJobs, ref num, 2, reportProgress);
						}
						if (array.Length > 0)
						{
							Result<ADRawEntry>[] array2 = recipientSession.FindByLegacyExchangeDNs(array, new PropertyDefinition[]
							{
								ADRecipientSchema.LegacyExchangeDN,
								ADObjectSchema.Id
							});
							if (array2 != null && array2.Length > 0)
							{
								foreach (Result<ADRawEntry> result in array2)
								{
									string text = (string)result.Data[ADRecipientSchema.LegacyExchangeDN];
									ADObjectId entryId = (ADObjectId)result.Data[ADObjectSchema.Id];
									try
									{
										ADRecipient adrecipient = tenantOrRootOrgRecipientSession.Read(entryId);
										if (adrecipient == null)
										{
											throw new LocalizedException(ServerStrings.ErrorADUserFoundByReadOnlyButNotWrite(text));
										}
										MailboxDiscoverySearch.AddInPlaceHold(adrecipient, this.InPlaceHoldIdentity, tenantOrRootOrgRecipientSession);
									}
									catch (LocalizedException ex)
									{
										this.InPlaceHoldErrors.Add(ex.Message);
										this.FailedToHoldMailboxes.Add(text);
										ExTraceGlobals.StorageTracer.TraceError<string, LocalizedException>(0L, "Failed to place hold on mailbox '{0}'. Exception: {1}", text, ex);
									}
									MailboxDiscoverySearch.ReportJobsProgress(totalPossibleJobs, ref num, 1, reportProgress);
								}
							}
							else
							{
								foreach (string text2 in array)
								{
									this.InPlaceHoldErrors.Add(ServerStrings.ADUserNotFoundId(text2));
									this.FailedToHoldMailboxes.Add(text2);
									ExTraceGlobals.StorageTracer.TraceError(0L, string.Format("Failed to find mailbox '{0}' to be placed on hold.", text2));
									MailboxDiscoverySearch.ReportJobsProgress(totalPossibleJobs, ref num, 1, reportProgress);
								}
							}
						}
					}
					foreach (KeyValuePair<string, ADObjectId> keyValuePair in dictionary)
					{
						string key2 = keyValuePair.Key;
						try
						{
							ADRecipient recipient = tenantOrRootOrgRecipientSession.Read(keyValuePair.Value);
							MailboxDiscoverySearch.RemoveInPlaceHold(recipient, this.InPlaceHoldIdentity, tenantOrRootOrgRecipientSession);
						}
						catch (LocalizedException ex2)
						{
							this.InPlaceHoldErrors.Add(ex2.Message);
							this.FailedToHoldMailboxes.Add(key2);
							ExTraceGlobals.StorageTracer.TraceError<string, LocalizedException>(0L, "Failed to remove hold from mailbox '{0}'. Exception: {1}", key2, ex2);
						}
						MailboxDiscoverySearch.ReportJobsProgress(totalPossibleJobs, ref num, 1, reportProgress);
					}
					dataProvider.Save(this);
					MailboxDiscoverySearch.ReportJobsProgress(totalPossibleJobs, ref num, 1, reportProgress);
					SearchEventLogger.Instance.LogInPlaceHoldSettingsSynchronizedEvent(this);
				}
				finally
				{
					recipientSession.SessionSettings.IncludeSoftDeletedObjects = includeSoftDeletedObjects;
					recipientSession.SessionSettings.IncludeInactiveMailbox = includeInactiveMailbox;
				}
				return LocalizedString.Empty;
			}
			return ServerStrings.ErrorCannotSyncHoldObjectManagedByOtherOrg(base.Name, dataProvider.OrganizationId.ToString(), this.ManagedByOrganization);
		}

		// Token: 0x060073C6 RID: 29638 RVA: 0x00200620 File Offset: 0x001FE820
		internal bool ShouldWarnForInactiveOnHold(DiscoverySearchDataProvider dataProvider, IRecipientSession recipientSession, bool holdEnabled)
		{
			if (!"b5d6efcd-1aee-42b9-b168-6fef285fe613".Equals(this.ManagedByOrganization ?? string.Empty, StringComparison.OrdinalIgnoreCase))
			{
				try
				{
					if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
					{
						recipientSession.SessionSettings.IncludeSoftDeletedObjects = true;
					}
					Dictionary<string, ADObjectId> dictionary = this.FetchCurrentOnHoldMailboxes(dataProvider, recipientSession);
					if (holdEnabled)
					{
						List<string> list = this.Sources.Intersect(dictionary.Keys).ToList<string>();
						foreach (string key in list)
						{
							dictionary.Remove(key);
						}
					}
					foreach (KeyValuePair<string, ADObjectId> keyValuePair in dictionary)
					{
						string key2 = keyValuePair.Key;
						try
						{
							ADRecipient adrecipient = recipientSession.Read(keyValuePair.Value);
							if (adrecipient != null && adrecipient.InPlaceHolds != null && adrecipient.InPlaceHolds.Contains(this.InPlaceHoldIdentity) && adrecipient.IsInactiveMailbox)
							{
								return true;
							}
						}
						catch (LocalizedException ex)
						{
							this.InPlaceHoldErrors.Add(ex.Message);
							this.FailedToHoldMailboxes.Add(key2);
							ExTraceGlobals.StorageTracer.TraceError<string, LocalizedException>(0L, "Failed to read hold information for '{0}'. Exception: {1}", key2, ex);
						}
					}
					return false;
				}
				catch (LocalizedException ex2)
				{
					this.InPlaceHoldErrors.Add(ex2.Message);
					ExTraceGlobals.StorageTracer.TraceError<EwsStoreObjectId, LocalizedException>(0L, "Failed to see if we should warn for this discovery search:'{0}'. Exception: {1}", base.Identity, ex2);
				}
				return false;
			}
			return false;
		}

		// Token: 0x060073C7 RID: 29639 RVA: 0x0020081C File Offset: 0x001FEA1C
		internal Dictionary<string, ADObjectId> FetchCurrentOnHoldMailboxes(DiscoverySearchDataProvider dataProvider, IRecipientSession recipientSession)
		{
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.InPlaceHolds, this.InPlaceHoldIdentity);
			IEnumerable<ADRawEntry> enumerable = recipientSession.FindPagedADRawEntry(null, QueryScope.SubTree, filter, null, 0, new PropertyDefinition[]
			{
				ADRecipientSchema.LegacyExchangeDN,
				ADObjectSchema.Id
			});
			Dictionary<string, ADObjectId> dictionary = new Dictionary<string, ADObjectId>();
			if (enumerable != null)
			{
				foreach (ADRawEntry adrawEntry in enumerable)
				{
					string key = (string)adrawEntry[ADRecipientSchema.LegacyExchangeDN];
					if (!dictionary.ContainsKey(key))
					{
						dictionary.Add(key, (ADObjectId)adrawEntry[ADObjectSchema.Id]);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060073C8 RID: 29640 RVA: 0x002008DC File Offset: 0x001FEADC
		internal void UpdateCalculatedQuery()
		{
			string query = this.Query;
			string additionalFilter = this.GetAdditionalFilter();
			if (string.IsNullOrEmpty(additionalFilter))
			{
				this.CalculatedQuery = (string.IsNullOrEmpty(query) ? MailboxDiscoverySearch.EmptyQueryReplacement : query);
				return;
			}
			string str = string.Empty;
			if (!string.IsNullOrEmpty(query))
			{
				if (query.IndexOf(" OR ", StringComparison.OrdinalIgnoreCase) != -1)
				{
					str = string.Format("({0}) ", query);
				}
				else
				{
					str = string.Format("{0} ", query);
				}
			}
			this.CalculatedQuery = str + additionalFilter;
		}

		// Token: 0x060073C9 RID: 29641 RVA: 0x0020095C File Offset: 0x001FEB5C
		internal void UpdateKeywordsQuery(List<ValidationError> errors)
		{
			this.TotalKeywords = 0;
			this.UserKeywords = new MultiValuedProperty<string>();
			this.KeywordsQuery = string.Empty;
			if (!string.IsNullOrEmpty(this.Query) && this.StatisticsStartIndex > 0)
			{
				try
				{
					QueryFilter queryFilter = KqlParser.ParseAndBuildQuery(this.Query, KqlParser.ParseOption.ImplicitOr | KqlParser.ParseOption.UseCiKeywordOnly | KqlParser.ParseOption.DisablePrefixMatch | KqlParser.ParseOption.QueryPreserving | KqlParser.ParseOption.AllowShortWildcards, this.QueryCulture, RescopedAll.Default, null, null);
					ICollection<QueryFilter> collection;
					if (queryFilter.GetType() == typeof(OrFilter))
					{
						collection = AqsParser.FlattenQueryFilter(queryFilter);
					}
					else
					{
						collection = new List<QueryFilter>(1);
						collection.Add(queryFilter);
					}
					if (collection != null && collection.Count > 0)
					{
						this.TotalKeywords = collection.Count;
						if (this.TotalKeywords < this.StatisticsStartIndex)
						{
							errors.Add(new PropertyValidationError(ServerStrings.ErrorStatisticsStartIndexIsOutOfBound(this.StatisticsStartIndex, this.TotalKeywords), MailboxDiscoverySearchSchema.StatisticsStartIndex, this.StatisticsStartIndex));
						}
						else
						{
							string additionalFilter = this.GetAdditionalFilter();
							if (this.TotalKeywords > 1)
							{
								StringBuilder stringBuilder = new StringBuilder();
								int num = 0;
								int num2 = this.StatisticsStartIndex - 1;
								int num3 = 0;
								while (num2 < this.TotalKeywords && num3 < 25)
								{
									QueryFilter filter = collection.ElementAt(num2);
									string text = MailboxDiscoverySearch.GetKeywordPhrase(filter, this.Query, ref num);
									if (!this.UserKeywords.Contains(text))
									{
										this.UserKeywords.Add(text);
									}
									if (!string.IsNullOrEmpty(additionalFilter))
									{
										text = string.Format("({0} AND {1})", text, additionalFilter);
									}
									stringBuilder.Append(text);
									stringBuilder.Append(" OR ");
									num2++;
									num3++;
								}
								this.KeywordsQuery = stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 4);
							}
							else if (this.TotalKeywords == 1)
							{
								this.UserKeywords.Add(this.Query);
								string text2 = this.Query;
								if (!string.IsNullOrEmpty(additionalFilter))
								{
									text2 = string.Format("({0} AND {1})", text2, additionalFilter);
								}
								this.KeywordsQuery = text2;
							}
						}
					}
				}
				catch (ParserException ex)
				{
					errors.Add(new PropertyValidationError(ServerStrings.ErrorInvalidQuery(base.Name, ex.Message), MailboxDiscoverySearchSchema.Query, this.Query));
				}
			}
		}

		// Token: 0x060073CA RID: 29642 RVA: 0x00200BA0 File Offset: 0x001FEDA0
		internal void UpdateSearchStats(DiscoverySearchStats stats)
		{
			stats.EstimatedItems = this.ResultItemCountEstimate - this.ResultUnsearchableItemCountEstimate;
			stats.TotalItemsCopied = this.ResultItemCountCopied;
			this.SearchStatistics = new MultiValuedProperty<DiscoverySearchStats>();
			this.SearchStatistics.Add(stats);
		}

		// Token: 0x060073CB RID: 29643 RVA: 0x00200C18 File Offset: 0x001FEE18
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (!this.ItemHoldPeriod.IsUnlimited && this.ItemHoldPeriod.Value <= EnhancedTimeSpan.Zero)
			{
				errors.Add(new PropertyValidationError(ServerStrings.ErrorInvalidItemHoldPeriod(base.Name), MailboxDiscoverySearchSchema.ItemHoldPeriod, this.ItemHoldPeriod));
			}
			if (string.IsNullOrEmpty(this.CalculatedQuery))
			{
				this.UpdateCalculatedQuery();
			}
			if (this.CalculatedQuery.Length > 10240)
			{
				errors.Add(new PropertyValidationError(ServerStrings.ErrorInvalidQueryTooLong(base.Name), MailboxDiscoverySearchSchema.CalculatedQuery, this.CalculatedQuery));
			}
			bool flag = this.CalculatedQuery == MailboxDiscoverySearch.EmptyQueryReplacement;
			if (this.internalQueryFilter == null && !flag)
			{
				this.internalQueryFilter = MailboxDiscoverySearch.CalculateQueryFilter(this.CalculatedQuery, this.QueryCulture, delegate(Exception ex)
				{
					errors.Add(new PropertyValidationError(ServerStrings.ErrorInvalidQuery(this.Name, ex.Message), MailboxDiscoverySearchSchema.CalculatedQuery, this.CalculatedQuery));
				});
			}
			this.UpdateKeywordsQuery(errors);
			if (base.ObjectState == ObjectState.New)
			{
				if (string.IsNullOrEmpty(this.InPlaceHoldIdentity))
				{
					this.InPlaceHoldIdentity = Guid.NewGuid().ToString("N");
				}
			}
			else if (base.IsChanged(MailboxDiscoverySearchSchema.InPlaceHoldIdentity))
			{
				errors.Add(new PropertyValidationError(ServerStrings.ErrorInPlaceHoldIdentityChanged(base.Name), MailboxDiscoverySearchSchema.InPlaceHoldIdentity, this.InPlaceHoldIdentity));
			}
			else if (string.IsNullOrEmpty(this.InPlaceHoldIdentity))
			{
				errors.Add(new PropertyValidationError(ServerStrings.ErrorInvalidInPlaceHoldIdentity(base.Name), MailboxDiscoverySearchSchema.InPlaceHoldIdentity, this.InPlaceHoldIdentity));
			}
			if (this.StartDate != null && this.EndDate != null && this.StartDate > this.EndDate)
			{
				errors.Add(new PropertyValidationError(ServerStrings.InvalidateDateRange, MailboxDiscoverySearchSchema.StartDate, this.StartDate));
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
				errors.Add(new PropertyValidationError(ServerStrings.UnsupportedKindKeywords, MailboxDiscoverySearchSchema.MessageTypes, this.MessageTypes));
			}
			if (this.StatisticsStartIndex <= 0)
			{
				errors.Add(new PropertyValidationError(ServerStrings.ErrorInvalidStatisticsStartIndex(base.Name), MailboxDiscoverySearchSchema.StatisticsStartIndex, this.StatisticsStartIndex));
			}
			if (this.languageIsInvalid)
			{
				errors.Add(new PropertyValidationError(ServerStrings.ErrorInvalidQueryLanguage(base.Name, this.Language), MailboxDiscoverySearchSchema.Language, this.Language));
			}
		}

		// Token: 0x060073CC RID: 29644 RVA: 0x00200F14 File Offset: 0x001FF114
		private string BuildAddressesQuery(MultiValuedProperty<string> addresses, string[] addressTypes)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			if (addresses != null)
			{
				int num = 0;
				foreach (string value in addressTypes)
				{
					foreach (string text in addresses)
					{
						stringBuilder.Append(value);
						stringBuilder.Append("\"");
						stringBuilder.Append(text.Replace("\\", "\\\\"));
						stringBuilder.Append("*\"");
						num = stringBuilder.Length;
						stringBuilder.Append(" OR ");
					}
				}
				if (num > 0)
				{
					stringBuilder.Length = num;
				}
			}
			string text2 = stringBuilder.ToString();
			if (text2.Contains(" OR "))
			{
				text2 = string.Format("({0})", text2);
			}
			return text2;
		}

		// Token: 0x060073CD RID: 29645 RVA: 0x0020100C File Offset: 0x001FF20C
		private string GetAdditionalFilter()
		{
			MultiValuedProperty<string> senders = this.Senders;
			MultiValuedProperty<string> recipients = this.Recipients;
			MultiValuedProperty<KindKeyword> messageTypes = this.MessageTypes;
			ExDateTime? startDate = this.StartDate;
			ExDateTime? endDate = this.EndDate;
			if ((senders == null || senders.Count == 0) && (recipients == null || recipients.Count == 0) && (messageTypes == null || messageTypes.Count == 0) && (startDate == null || startDate == null) && (endDate == null || endDate == null))
			{
				return null;
			}
			int num = 0;
			string text = string.Empty;
			if (startDate != null && startDate != null)
			{
				text = string.Format("received>=\"{0}\"", startDate.Value.ToUtc().ToString(this.QueryCulture));
			}
			if (endDate != null && endDate != null)
			{
				string text2 = string.Format("received<=\"{0}\"", SearchObject.RoundEndDate(endDate).Value.ToUtc().ToString(this.QueryCulture));
				if (!string.IsNullOrEmpty(text))
				{
					text = string.Format("({0} AND {1})", text, text2);
				}
				else
				{
					text = text2;
				}
			}
			StringBuilder stringBuilder = new StringBuilder(64);
			if (messageTypes != null)
			{
				num = 0;
				foreach (KindKeyword kindKeyword in messageTypes)
				{
					stringBuilder.Append(string.Format("kind:{0}", kindKeyword.ToString()));
					num = stringBuilder.Length;
					stringBuilder.Append(" OR ");
				}
				if (num > 0)
				{
					stringBuilder.Length = num;
				}
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			string text3 = this.BuildAddressesQuery(senders, new string[]
			{
				"from:"
			});
			string text4 = this.BuildAddressesQuery(recipients, new string[]
			{
				"to:",
				"cc:",
				"bcc:"
			});
			string text5 = stringBuilder.ToString();
			if (text5.Contains(" OR "))
			{
				text5 = string.Format("({0})", text5);
			}
			if (!string.IsNullOrEmpty(text5))
			{
				stringBuilder2.Append(text5);
			}
			string value = string.Empty;
			if (!string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4))
			{
				value = string.Format("({0} OR {1}) ", text3, text4);
			}
			else if (!string.IsNullOrEmpty(text3))
			{
				value = text3;
			}
			else if (!string.IsNullOrEmpty(text4))
			{
				value = text4;
			}
			if (stringBuilder2.Length > 0 && !string.IsNullOrEmpty(value))
			{
				stringBuilder2.Append(" AND ");
			}
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder2.Append(value);
			}
			if (stringBuilder2.Length > 0 && !string.IsNullOrEmpty(text))
			{
				stringBuilder2.Append(" AND ");
			}
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder2.Append(text);
			}
			return stringBuilder2.ToString();
		}

		// Token: 0x04005060 RID: 20576
		public const string ManagedByRemoteExchangeOrganization = "b5d6efcd-1aee-42b9-b168-6fef285fe613";

		// Token: 0x04005061 RID: 20577
		internal const string FlightFeatureSearchStats = "SearchStatsFlighted";

		// Token: 0x04005062 RID: 20578
		internal const string FlightFeatureSearchScale = "SearchScaleFlighted";

		// Token: 0x04005063 RID: 20579
		internal const string FlightFeaturePublicFolderSearch = "PublicFolderSearchFlighted";

		// Token: 0x04005064 RID: 20580
		internal const string FlightFeatureDocIdHint = "DocIdHintFlighted";

		// Token: 0x04005065 RID: 20581
		internal const string FlightFeatureUrlRebind = "UrlRebindFlighted";

		// Token: 0x04005066 RID: 20582
		internal const string FlightFeatureExcludeFolders = "ExcludeFoldersFlighted";

		// Token: 0x04005067 RID: 20583
		private const string OrSeparator = " OR ";

		// Token: 0x04005068 RID: 20584
		private const int MaxQueryLength = 10240;

		// Token: 0x04005069 RID: 20585
		private const int MaxKeywordCountPerStatisticsSearch = 25;

		// Token: 0x0400506A RID: 20586
		internal static string EmptyQueryReplacement = "size>=0";

		// Token: 0x0400506B RID: 20587
		private static readonly Hookable<bool?> AllowSettingStatus = Hookable<bool?>.Create(true, null);

		// Token: 0x0400506C RID: 20588
		private static ObjectSchema schema = new MailboxDiscoverySearchSchema();

		// Token: 0x0400506D RID: 20589
		private QueryFilter internalQueryFilter;

		// Token: 0x0400506E RID: 20590
		private Dictionary<SearchState, Dictionary<SearchStateTransition, SearchState>> stateMachine;

		// Token: 0x0400506F RID: 20591
		private CultureInfo queryCulture;

		// Token: 0x04005070 RID: 20592
		private bool languageIsInvalid;

		// Token: 0x04005071 RID: 20593
		private List<string> flightedFeatures;
	}
}
