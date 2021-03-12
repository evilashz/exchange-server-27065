using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003D9 RID: 985
	[KnownType(typeof(MailboxSearch))]
	[DataContract]
	public class MailboxSearch : MailboxSearchRow
	{
		// Token: 0x0600328B RID: 12939 RVA: 0x0009CF74 File Offset: 0x0009B174
		public MailboxSearch(MailboxSearchObject searchObject) : base(searchObject)
		{
		}

		// Token: 0x17001FC4 RID: 8132
		// (get) Token: 0x0600328C RID: 12940 RVA: 0x0009D0C8 File Offset: 0x0009B2C8
		// (set) Token: 0x0600328D RID: 12941 RVA: 0x0009D0DA File Offset: 0x0009B2DA
		[DataMember]
		public string Caption
		{
			get
			{
				return base.MailboxSearch.Name.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FC5 RID: 8133
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x0009D0E1 File Offset: 0x0009B2E1
		// (set) Token: 0x0600328F RID: 12943 RVA: 0x0009D0F4 File Offset: 0x0009B2F4
		[DataMember]
		public string SearchAllMailboxes
		{
			get
			{
				return this.SearchAllMailboxesCalculated.ToJsonString(null);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FC6 RID: 8134
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x0009D0FB File Offset: 0x0009B2FB
		public bool SearchAllMailboxesCalculated
		{
			get
			{
				return base.MailboxSearch.SourceMailboxes == null || base.MailboxSearch.SourceMailboxes.Count == 0;
			}
		}

		// Token: 0x17001FC7 RID: 8135
		// (get) Token: 0x06003291 RID: 12945 RVA: 0x0009D120 File Offset: 0x0009B320
		// (set) Token: 0x06003292 RID: 12946 RVA: 0x0009D166 File Offset: 0x0009B366
		[DataMember]
		public string SearchAllDates
		{
			get
			{
				return (base.MailboxSearch.StartDate == null && base.MailboxSearch.EndDate == null).ToJsonString(null);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FC8 RID: 8136
		// (get) Token: 0x06003293 RID: 12947 RVA: 0x0009D16D File Offset: 0x0009B36D
		// (set) Token: 0x06003294 RID: 12948 RVA: 0x0009D17A File Offset: 0x0009B37A
		[DataMember]
		public bool IncludeUnsearchableItems
		{
			get
			{
				return base.MailboxSearch.IncludeUnsearchableItems;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FC9 RID: 8137
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x0009D181 File Offset: 0x0009B381
		// (set) Token: 0x06003296 RID: 12950 RVA: 0x0009D193 File Offset: 0x0009B393
		[DataMember]
		public string SearchStartDate
		{
			get
			{
				return base.MailboxSearch.StartDate.ToUserDateTimeGeneralFormatString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FCA RID: 8138
		// (get) Token: 0x06003297 RID: 12951 RVA: 0x0009D19A File Offset: 0x0009B39A
		// (set) Token: 0x06003298 RID: 12952 RVA: 0x0009D1AC File Offset: 0x0009B3AC
		[DataMember]
		public string SearchEndDate
		{
			get
			{
				return base.MailboxSearch.EndDate.ToUserDateTimeGeneralFormatString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FCB RID: 8139
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x0009D1B3 File Offset: 0x0009B3B3
		// (set) Token: 0x0600329A RID: 12954 RVA: 0x0009D1CF File Offset: 0x0009B3CF
		[DataMember]
		public bool SendMeEmailOnComplete
		{
			get
			{
				return base.MailboxSearch.StatusMailRecipients.Contains(RbacPrincipal.Current.ExecutingUserId);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FCC RID: 8140
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x0009D1D6 File Offset: 0x0009B3D6
		// (set) Token: 0x0600329C RID: 12956 RVA: 0x0009D1E6 File Offset: 0x0009B3E6
		[DataMember]
		public bool EnableFullLogging
		{
			get
			{
				return base.MailboxSearch.LogLevel == LoggingLevel.Full;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FCD RID: 8141
		// (get) Token: 0x0600329D RID: 12957 RVA: 0x0009D200 File Offset: 0x0009B400
		// (set) Token: 0x0600329E RID: 12958 RVA: 0x0009D294 File Offset: 0x0009B494
		[DataMember]
		public string ResultNumber
		{
			get
			{
				KeywordHit keywordHit = base.MailboxSearch.AllKeywordHits.Find((KeywordHit hit) => hit.Phrase == "652beee2-75f7-4ca0-8a02-0698a3919cb9");
				long num = base.MailboxSearch.EstimateOnly ? base.MailboxSearch.ResultNumberEstimate : base.MailboxSearch.ResultNumber;
				if (!this.IncludeUnsearchableItems || keywordHit == null)
				{
					return num.ToString();
				}
				return string.Format(Strings.MailboxSeachCountIncludeUnsearchable, num, keywordHit.Count);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FCE RID: 8142
		// (get) Token: 0x0600329F RID: 12959 RVA: 0x0009D29C File Offset: 0x0009B49C
		// (set) Token: 0x060032A0 RID: 12960 RVA: 0x0009D2EC File Offset: 0x0009B4EC
		[DataMember]
		public string EstimatedItems
		{
			get
			{
				if (base.MailboxSearch.SearchStatistics == null || base.MailboxSearch.SearchStatistics.Count == 0)
				{
					return "0";
				}
				return base.MailboxSearch.SearchStatistics[0].EstimatedItems.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FCF RID: 8143
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x0009D2F4 File Offset: 0x0009B4F4
		// (set) Token: 0x060032A2 RID: 12962 RVA: 0x0009D34B File Offset: 0x0009B54B
		[DataMember]
		public string UnsearchableItemsAdded
		{
			get
			{
				if (base.MailboxSearch.SearchStatistics == null || base.MailboxSearch.SearchStatistics.Count == 0)
				{
					return "0";
				}
				return string.Format("+{0}", base.MailboxSearch.SearchStatistics[0].UnsearchableItemsAdded);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FD0 RID: 8144
		// (get) Token: 0x060032A3 RID: 12963 RVA: 0x0009D354 File Offset: 0x0009B554
		// (set) Token: 0x060032A4 RID: 12964 RVA: 0x0009D3AB File Offset: 0x0009B5AB
		[DataMember]
		public string DuplicatesRemoved
		{
			get
			{
				if (base.MailboxSearch.SearchStatistics == null || base.MailboxSearch.SearchStatistics.Count == 0)
				{
					return "0";
				}
				return string.Format("-{0}", base.MailboxSearch.SearchStatistics[0].TotalDuplicateItems);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FD1 RID: 8145
		// (get) Token: 0x060032A5 RID: 12965 RVA: 0x0009D3B4 File Offset: 0x0009B5B4
		// (set) Token: 0x060032A6 RID: 12966 RVA: 0x0009D40B File Offset: 0x0009B60B
		[DataMember]
		public string SkippedErrorItems
		{
			get
			{
				if (base.MailboxSearch.SearchStatistics == null || base.MailboxSearch.SearchStatistics.Count == 0)
				{
					return "0";
				}
				return string.Format("-{0}", base.MailboxSearch.SearchStatistics[0].SkippedErrorItems);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FD2 RID: 8146
		// (get) Token: 0x060032A7 RID: 12967 RVA: 0x0009D414 File Offset: 0x0009B614
		// (set) Token: 0x060032A8 RID: 12968 RVA: 0x0009D46B File Offset: 0x0009B66B
		[DataMember]
		public string TotalCopiedItems
		{
			get
			{
				if (base.MailboxSearch.SearchStatistics == null || base.MailboxSearch.SearchStatistics.Count == 0)
				{
					return "0";
				}
				return string.Format("={0}", base.MailboxSearch.SearchStatistics[0].TotalItemsCopied);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FD3 RID: 8147
		// (get) Token: 0x060032A9 RID: 12969 RVA: 0x0009D472 File Offset: 0x0009B672
		// (set) Token: 0x060032AA RID: 12970 RVA: 0x0009D47F File Offset: 0x0009B67F
		[DataMember]
		public string ResultsLink
		{
			get
			{
				return base.MailboxSearch.ResultsLink;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FD4 RID: 8148
		// (get) Token: 0x060032AB RID: 12971 RVA: 0x0009D488 File Offset: 0x0009B688
		// (set) Token: 0x060032AC RID: 12972 RVA: 0x0009D4E2 File Offset: 0x0009B6E2
		[DataMember]
		public string TargetMailboxDisplayName
		{
			get
			{
				if (base.MailboxSearch.TargetMailbox == null)
				{
					return Strings.TargetMailboxRemoved;
				}
				RecipientObjectResolverRow recipientObjectResolverRow = RecipientObjectResolver.Instance.ResolveObjects(new ADObjectId[]
				{
					base.MailboxSearch.TargetMailbox
				}).FirstOrDefault<RecipientObjectResolverRow>();
				if (recipientObjectResolverRow == null)
				{
					return string.Empty;
				}
				return recipientObjectResolverRow.DisplayName;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FD5 RID: 8149
		// (get) Token: 0x060032AD RID: 12973 RVA: 0x0009D4EC File Offset: 0x0009B6EC
		// (set) Token: 0x060032AE RID: 12974 RVA: 0x0009D547 File Offset: 0x0009B747
		[DataMember]
		public string TargetMailboxSmtp
		{
			get
			{
				if (base.MailboxSearch.TargetMailbox == null)
				{
					return Strings.TargetMailboxRemoved;
				}
				RecipientObjectResolverRow recipientObjectResolverRow = RecipientObjectResolver.Instance.ResolveObjects(new ADObjectId[]
				{
					base.MailboxSearch.TargetMailbox
				}).FirstOrDefault<RecipientObjectResolverRow>();
				if (recipientObjectResolverRow == null)
				{
					return this.TargetMailboxDisplayName;
				}
				return recipientObjectResolverRow.PrimarySmtpAddress;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FD6 RID: 8150
		// (get) Token: 0x060032AF RID: 12975 RVA: 0x0009D54E File Offset: 0x0009B74E
		// (set) Token: 0x060032B0 RID: 12976 RVA: 0x0009D564 File Offset: 0x0009B764
		[DataMember]
		public string LastRunByDisplayName
		{
			get
			{
				return base.MailboxSearch.LastRunBy ?? string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FD7 RID: 8151
		// (get) Token: 0x060032B1 RID: 12977 RVA: 0x0009D56B File Offset: 0x0009B76B
		// (set) Token: 0x060032B2 RID: 12978 RVA: 0x0009D578 File Offset: 0x0009B778
		[DataMember]
		public int PercentComplete
		{
			get
			{
				return base.MailboxSearch.PercentComplete;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FD8 RID: 8152
		// (get) Token: 0x060032B3 RID: 12979 RVA: 0x0009D57F File Offset: 0x0009B77F
		// (set) Token: 0x060032B4 RID: 12980 RVA: 0x0009D5A5 File Offset: 0x0009B7A5
		[DataMember]
		public IEnumerable<RecipientObjectResolverRow> SourceMailboxes
		{
			get
			{
				if (this.SearchAllMailboxesCalculated)
				{
					return null;
				}
				return RecipientObjectResolver.Instance.ResolveObjects(base.MailboxSearch.SourceMailboxes.ToArray());
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FD9 RID: 8153
		// (get) Token: 0x060032B5 RID: 12981 RVA: 0x0009D5AC File Offset: 0x0009B7AC
		// (set) Token: 0x060032B6 RID: 12982 RVA: 0x0009D608 File Offset: 0x0009B808
		[DataMember]
		public Identity TargetMailbox
		{
			get
			{
				if (base.MailboxSearch.TargetMailbox == null)
				{
					return null;
				}
				RecipientObjectResolverRow recipientObjectResolverRow = RecipientObjectResolver.Instance.ResolveObjects(new ADObjectId[]
				{
					base.MailboxSearch.TargetMailbox
				}).FirstOrDefault<RecipientObjectResolverRow>();
				if (recipientObjectResolverRow == null)
				{
					return base.MailboxSearch.TargetMailbox.ToIdentity();
				}
				return recipientObjectResolverRow.Identity;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FDA RID: 8154
		// (get) Token: 0x060032B7 RID: 12983 RVA: 0x0009D60F File Offset: 0x0009B80F
		// (set) Token: 0x060032B8 RID: 12984 RVA: 0x0009D61C File Offset: 0x0009B81C
		[DataMember]
		public string SearchQuery
		{
			get
			{
				return base.MailboxSearch.SearchQuery;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FDB RID: 8155
		// (get) Token: 0x060032B9 RID: 12985 RVA: 0x0009D623 File Offset: 0x0009B823
		// (set) Token: 0x060032BA RID: 12986 RVA: 0x0009D657 File Offset: 0x0009B857
		[DataMember]
		public string Senders
		{
			get
			{
				if (base.MailboxSearch.Senders.Count == 0)
				{
					return string.Empty;
				}
				return string.Join(", ", base.MailboxSearch.Senders.ToArray<string>());
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FDC RID: 8156
		// (get) Token: 0x060032BB RID: 12987 RVA: 0x0009D65E File Offset: 0x0009B85E
		// (set) Token: 0x060032BC RID: 12988 RVA: 0x0009D692 File Offset: 0x0009B892
		[DataMember]
		public string Recipients
		{
			get
			{
				if (base.MailboxSearch.Recipients.Count == 0)
				{
					return string.Empty;
				}
				return string.Join(", ", base.MailboxSearch.Recipients.ToArray<string>());
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FDD RID: 8157
		// (get) Token: 0x060032BD RID: 12989 RVA: 0x0009D699 File Offset: 0x0009B899
		// (set) Token: 0x060032BE RID: 12990 RVA: 0x0009D6B0 File Offset: 0x0009B8B0
		[DataMember]
		public string LogLevel
		{
			get
			{
				return base.MailboxSearch.LogLevel.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FDE RID: 8158
		// (get) Token: 0x060032BF RID: 12991 RVA: 0x0009D6B7 File Offset: 0x0009B8B7
		// (set) Token: 0x060032C0 RID: 12992 RVA: 0x0009D6CF File Offset: 0x0009B8CF
		[DataMember]
		public string EstimateOnly
		{
			get
			{
				return base.MailboxSearch.EstimateOnly.ToJsonString(null);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FDF RID: 8159
		// (get) Token: 0x060032C1 RID: 12993 RVA: 0x0009D6D6 File Offset: 0x0009B8D6
		// (set) Token: 0x060032C2 RID: 12994 RVA: 0x0009D6E3 File Offset: 0x0009B8E3
		[DataMember]
		public bool ExcludeDuplicateMessages
		{
			get
			{
				return base.MailboxSearch.ExcludeDuplicateMessages;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FE0 RID: 8160
		// (get) Token: 0x060032C3 RID: 12995 RVA: 0x0009D6EC File Offset: 0x0009B8EC
		// (set) Token: 0x060032C4 RID: 12996 RVA: 0x0009D73D File Offset: 0x0009B93D
		[DataMember]
		public string Errors
		{
			get
			{
				if (base.MailboxSearch.Errors != null && base.MailboxSearch.Errors.Count != 0)
				{
					return base.MailboxSearch.Errors.ToStringArray().StringArrayJoin(", ");
				}
				return Strings.NoErrors;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FE1 RID: 8161
		// (get) Token: 0x060032C5 RID: 12997 RVA: 0x0009D744 File Offset: 0x0009B944
		// (set) Token: 0x060032C6 RID: 12998 RVA: 0x0009D8E1 File Offset: 0x0009BAE1
		[DataMember]
		public string ErrorsOneLine
		{
			get
			{
				this.SetErrorArrays();
				int num = this.errorOneLinerStrings.Length;
				StringBuilder stringBuilder = new StringBuilder(string.Empty);
				this.totalErrorsFound = 0;
				if (this.errorMailboxes != null && this.errorMailboxes.Length >= num)
				{
					for (int i = 1; i < num; i++)
					{
						if (this.errorMailboxes[i] != null && this.errorMailboxes[i].Count > 0)
						{
							this.totalErrorsFound += this.errorMailboxes[i].Count;
							stringBuilder.AppendLine(this.errorOneLinerStrings[i]);
							if (this.errorMailboxes[i].Count > 1)
							{
								stringBuilder.AppendLine(string.Format("{0} {1}", this.errorMailboxes[i].Count, Strings.EDiscoveryInstances));
							}
							else
							{
								stringBuilder.AppendLine(string.Format("1 {0}", Strings.EDiscoveryInstance));
							}
						}
					}
				}
				if (this.errorUndefined != null && this.errorUndefined.Count > 0)
				{
					stringBuilder.AppendLine(this.errorOneLinerStrings[0]);
					if (this.errorUndefined.Count > 1)
					{
						stringBuilder.AppendLine(string.Format("{0} {1}", this.errorUndefined.Count, Strings.EDiscoveryInstances));
					}
					else
					{
						stringBuilder.AppendLine(string.Format("1 {0}", Strings.EDiscoveryInstance));
					}
				}
				if (this.totalErrorsFound + this.errorUndefined.Count <= 0)
				{
					return Strings.NoErrors;
				}
				return stringBuilder.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FE2 RID: 8162
		// (get) Token: 0x060032C7 RID: 12999 RVA: 0x0009D8E8 File Offset: 0x0009BAE8
		// (set) Token: 0x060032C8 RID: 13000 RVA: 0x0009D99C File Offset: 0x0009BB9C
		[DataMember]
		public string ErrorsUndefinedSummary
		{
			get
			{
				this.SetErrorArrays();
				StringBuilder stringBuilder = new StringBuilder(string.Empty);
				if (this.errorUndefined != null && this.errorUndefined.Count > 0)
				{
					stringBuilder.AppendLine(this.errorOneLinerDetailStrings[0]);
					if (this.errorUndefined.Count > 1)
					{
						stringBuilder.AppendLine(string.Format("{0} {1}", this.errorUndefined.Count, Strings.EDiscoveryInstances));
					}
					else
					{
						stringBuilder.AppendLine(string.Format("1 {0}", Strings.EDiscoveryInstance));
					}
					stringBuilder.Append("\n");
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FE3 RID: 8163
		// (get) Token: 0x060032C9 RID: 13001 RVA: 0x0009D9A4 File Offset: 0x0009BBA4
		// (set) Token: 0x060032CA RID: 13002 RVA: 0x0009DBF8 File Offset: 0x0009BDF8
		[DataMember]
		public string ErrorsOneLineDetails
		{
			get
			{
				this.SetErrorArrays();
				int num = this.errorOneLinerDetailStrings.Length;
				StringBuilder stringBuilder = new StringBuilder(string.Empty);
				this.totalErrorsFound = 0;
				if (this.errorMailboxes == null || this.errorItems == null || this.errorDocumentIds == null || this.errorServers == null)
				{
					return Strings.NoErrors;
				}
				for (int i = 1; i < num; i++)
				{
					if (this.errorMailboxes[i] != null && this.errorMailboxes[i].Count > 0)
					{
						this.totalErrorsFound++;
						stringBuilder.AppendLine(this.errorOneLinerDetailStrings[i]);
						for (int j = 0; j < this.errorMailboxes[i].Count; j++)
						{
							if (!string.IsNullOrEmpty(this.errorMailboxes[i][j]))
							{
								stringBuilder.AppendLine(string.Format("{0}\t{1}", Strings.EDiscoveryMailbox, this.errorMailboxes[i][j]));
							}
							if (this.errorItems[i] != null && this.errorItems[i].Count > j && !string.IsNullOrEmpty(this.errorItems[i][j]))
							{
								stringBuilder.AppendLine(string.Format("{0}\t\t{1}", Strings.EDiscoveryItem, this.errorItems[i][j]));
							}
							if (this.errorDocumentIds[i] != null && this.errorDocumentIds[i].Count > j && !string.IsNullOrEmpty(this.errorDocumentIds[i][j]))
							{
								stringBuilder.AppendLine(string.Format("{0}\t{1}", Strings.EDiscoveryDocumentId, this.errorDocumentIds[i][j]));
							}
							if (this.errorServers[i] != null && this.errorServers[i].Count > j && !string.IsNullOrEmpty(this.errorServers[i][j]))
							{
								stringBuilder.AppendLine(string.Format("{0}\t{1}", Strings.EDiscoveryServer, this.errorServers[i][j]));
							}
							stringBuilder.Append("\n");
						}
					}
				}
				if (this.totalErrorsFound <= 0 && (this.errorUndefined == null || this.errorUndefined.Count <= 0))
				{
					return Strings.NoErrors;
				}
				return stringBuilder.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FE4 RID: 8164
		// (get) Token: 0x060032CB RID: 13003 RVA: 0x0009DC00 File Offset: 0x0009BE00
		// (set) Token: 0x060032CC RID: 13004 RVA: 0x0009DC7A File Offset: 0x0009BE7A
		[DataMember]
		public string ErrorsUndefinedDetails
		{
			get
			{
				this.SetErrorArrays();
				if (this.errorUndefined != null && this.errorUndefined.Count > 0)
				{
					StringBuilder stringBuilder = new StringBuilder(string.Empty);
					for (int i = 0; i < this.errorUndefined.Count; i++)
					{
						stringBuilder.AppendLine(this.errorUndefined[i]);
						stringBuilder.Append("\n");
					}
					return stringBuilder.ToString();
				}
				return Strings.NoErrors;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FE5 RID: 8165
		// (get) Token: 0x060032CD RID: 13005 RVA: 0x0009DC84 File Offset: 0x0009BE84
		// (set) Token: 0x060032CE RID: 13006 RVA: 0x0009DCD0 File Offset: 0x0009BED0
		[DataMember]
		public string Information
		{
			get
			{
				if (base.MailboxSearch.Information != null && base.MailboxSearch.Information.Count != 0)
				{
					return base.MailboxSearch.Information.ToStringArray().StringArrayJoin(", ");
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FE6 RID: 8166
		// (get) Token: 0x060032CF RID: 13007 RVA: 0x0009DCDF File Offset: 0x0009BEDF
		// (set) Token: 0x060032D0 RID: 13008 RVA: 0x0009DD0E File Offset: 0x0009BF0E
		[DataMember]
		public IEnumerable<KeywordHitRow> KeywordHits
		{
			get
			{
				return from kwh in base.MailboxSearch.KeywordHits
				select new KeywordHitRow(kwh);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FE7 RID: 8167
		// (get) Token: 0x060032D1 RID: 13009 RVA: 0x0009DD15 File Offset: 0x0009BF15
		// (set) Token: 0x060032D2 RID: 13010 RVA: 0x0009DD22 File Offset: 0x0009BF22
		[DataMember]
		public string Description
		{
			get
			{
				return base.MailboxSearch.Description;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FE8 RID: 8168
		// (get) Token: 0x060032D3 RID: 13011 RVA: 0x0009DD2C File Offset: 0x0009BF2C
		// (set) Token: 0x060032D4 RID: 13012 RVA: 0x0009DDF3 File Offset: 0x0009BFF3
		[DataMember]
		public string InPlaceHoldDescription
		{
			get
			{
				string result = Strings.MailboxSearchInPlaceHoldDescriptionNone;
				if (base.MailboxSearch.InPlaceHoldEnabled)
				{
					if (base.MailboxSearch.ItemHoldPeriod.IsUnlimited || base.MailboxSearch.ItemHoldPeriod.Value.Days == 0)
					{
						result = Strings.MailboxSearchInPlaceHoldDescriptionIndefinitely;
					}
					else if (base.MailboxSearch.ItemHoldPeriod.Value.Days > 1)
					{
						result = string.Format(Strings.MailboxSearchInplaceHoldDescriptionDays, base.MailboxSearch.ItemHoldPeriod.Value.Days);
					}
					else
					{
						result = Strings.MailboxSearchInPlaceHoldDescriptionOneDay;
					}
				}
				return result;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FE9 RID: 8169
		// (get) Token: 0x060032D5 RID: 13013 RVA: 0x0009DDFC File Offset: 0x0009BFFC
		// (set) Token: 0x060032D6 RID: 13014 RVA: 0x0009DE48 File Offset: 0x0009C048
		[DataMember]
		public string InPlaceHoldErrors
		{
			get
			{
				if (base.MailboxSearch.InPlaceHoldErrors != null && base.MailboxSearch.InPlaceHoldErrors.Count != 0)
				{
					return base.MailboxSearch.InPlaceHoldErrors.ToStringArray().StringArrayJoin(", ");
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FEA RID: 8170
		// (get) Token: 0x060032D7 RID: 13015 RVA: 0x0009DE4F File Offset: 0x0009C04F
		// (set) Token: 0x060032D8 RID: 13016 RVA: 0x0009DE5C File Offset: 0x0009C05C
		[DataMember]
		public int StatisticsStartIndex
		{
			get
			{
				return base.MailboxSearch.StatisticsStartIndex;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FEB RID: 8171
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x0009DE63 File Offset: 0x0009C063
		// (set) Token: 0x060032DA RID: 13018 RVA: 0x0009DE70 File Offset: 0x0009C070
		[DataMember]
		public int TotalKeywords
		{
			get
			{
				return base.MailboxSearch.TotalKeywords;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FEC RID: 8172
		// (get) Token: 0x060032DB RID: 13019 RVA: 0x0009DE77 File Offset: 0x0009C077
		// (set) Token: 0x060032DC RID: 13020 RVA: 0x0009DE86 File Offset: 0x0009C086
		[DataMember]
		public int TotalKnownErrors
		{
			get
			{
				string errorsOneLine = this.ErrorsOneLine;
				return this.totalErrorsFound;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FED RID: 8173
		// (get) Token: 0x060032DD RID: 13021 RVA: 0x0009DE8D File Offset: 0x0009C08D
		// (set) Token: 0x060032DE RID: 13022 RVA: 0x0009DEAA File Offset: 0x0009C0AA
		[DataMember]
		public int TotalUndefinedErrors
		{
			get
			{
				this.SetErrorArrays();
				if (this.errorUndefined != null)
				{
					return this.errorUndefined.Count;
				}
				return 0;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x0009DEB4 File Offset: 0x0009C0B4
		private void SetErrorArrays()
		{
			if (this.errorArraysAreSet)
			{
				return;
			}
			int num = this.errorOneLinerDetailStrings.Length;
			if (this.errorMailboxes == null)
			{
				this.errorMailboxes = new List<string>[num];
			}
			if (this.errorItems == null)
			{
				this.errorItems = new List<string>[num];
			}
			if (this.errorDocumentIds == null)
			{
				this.errorDocumentIds = new List<string>[num];
			}
			if (this.errorServers == null)
			{
				this.errorServers = new List<string>[num];
			}
			if (this.errorUndefined == null)
			{
				this.errorUndefined = new List<string>();
			}
			foreach (string text in base.MailboxSearch.Errors)
			{
				int num2 = -1;
				int num3 = 0;
				int num4 = text.IndexOf("EDiscoveryError:E");
				if (num4 >= 0)
				{
					num3 = num4 + 17;
					int num5 = text.IndexOf("::", num3);
					if (num5 <= num3)
					{
						this.errorUndefined.Add(text);
						continue;
					}
					string s = text.Substring(num3, num5 - num3);
					int.TryParse(s, out num2);
				}
				if (num2 < 1 || num2 > num - 1)
				{
					if (text.Contains("[NotFound]") || text.Contains("'NotFound'"))
					{
						num2 = 1;
					}
					else if (text.Contains("The server cannot service this request right now. Try again later") || text.Contains("ErrorServerBusy") || text.Contains("ServerBusyException"))
					{
						num2 = 3;
					}
					else if (text.Contains("Preview search failed due to transient error 'MapiExceptionMultiMailboxSearchFailed: Multi Mailbox Search failed"))
					{
						num2 = 4;
					}
					else if (text.Contains("The mailbox database is temporarily unavailable"))
					{
						num2 = 6;
					}
					else if (text.Contains("The SMTP address has no mailbox associated with it"))
					{
						num2 = 9;
					}
				}
				if (num2 < 1 || num2 > num - 1)
				{
					this.errorUndefined.Add(text);
				}
				else
				{
					num3 = 0;
					string[] array = new string[]
					{
						"Mailbox:",
						"Item:",
						"DocumentId:",
						"Server:"
					};
					this.SetOneErrorArray(text, array[0], ref num3, ref this.errorMailboxes[num2]);
					this.SetOneErrorArray(text, array[1], ref num3, ref this.errorItems[num2]);
					this.SetOneErrorArray(text, array[2], ref num3, ref this.errorDocumentIds[num2]);
					num3 = 0;
					this.SetOneErrorArray(text, array[3], ref num3, ref this.errorServers[num2]);
				}
			}
			this.errorArraysAreSet = true;
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x0009E128 File Offset: 0x0009C328
		private void SetOneErrorArray(string strError, string search, ref int iRefStart, ref List<string> errorArray)
		{
			if (errorArray == null)
			{
				errorArray = new List<string>();
			}
			int num = iRefStart;
			int num2 = strError.IndexOf(search, num);
			if (num2 < 0)
			{
				errorArray.Add(string.Empty);
				return;
			}
			num = num2 + search.Length;
			int num3 = strError.IndexOf("::", num);
			if (num3 > num)
			{
				int num4 = strError.IndexOf(search, num + search.Length);
				while (num4 > 0 && num4 < num3)
				{
					int num5 = strError.IndexOf(search, num4 + search.Length);
					if (num5 <= 0 || num5 >= num3)
					{
						break;
					}
					num4 = num5;
				}
				if (num4 > 0 && num4 < num3)
				{
					num = num4;
				}
				errorArray.Add(strError.Substring(num, num3 - num));
				iRefStart = num3 + 2;
				return;
			}
			errorArray.Add(string.Empty);
		}

		// Token: 0x0400248F RID: 9359
		private List<string>[] errorDocumentIds;

		// Token: 0x04002490 RID: 9360
		private List<string>[] errorItems;

		// Token: 0x04002491 RID: 9361
		private List<string>[] errorMailboxes;

		// Token: 0x04002492 RID: 9362
		private List<string>[] errorServers;

		// Token: 0x04002493 RID: 9363
		private List<string> errorUndefined;

		// Token: 0x04002494 RID: 9364
		private bool errorArraysAreSet;

		// Token: 0x04002495 RID: 9365
		private int totalErrorsFound;

		// Token: 0x04002496 RID: 9366
		private string[] errorOneLinerStrings = new string[]
		{
			Strings.EDiscoveryE000OneLiner,
			Strings.EDiscoveryE001OneLiner,
			Strings.EDiscoveryE002OneLiner,
			Strings.EDiscoveryE003OneLiner,
			Strings.EDiscoveryE004OneLiner,
			Strings.EDiscoveryE005OneLiner,
			Strings.EDiscoveryE006OneLiner,
			Strings.EDiscoveryE007OneLiner,
			Strings.EDiscoveryE008OneLiner,
			Strings.EDiscoveryE009OneLiner,
			Strings.EDiscoveryE010OneLiner
		};

		// Token: 0x04002497 RID: 9367
		private string[] errorOneLinerDetailStrings = new string[]
		{
			Strings.EDiscoveryE000FullMessage,
			Strings.EDiscoveryE001FullMessage,
			Strings.EDiscoveryE002FullMessage,
			Strings.EDiscoveryE003FullMessage,
			Strings.EDiscoveryE004FullMessage,
			Strings.EDiscoveryE005FullMessage,
			Strings.EDiscoveryE006FullMessage,
			Strings.EDiscoveryE007FullMessage,
			Strings.EDiscoveryE008FullMessage,
			Strings.EDiscoveryE009FullMessage,
			Strings.EDiscoveryE010FullMessage
		};
	}
}
