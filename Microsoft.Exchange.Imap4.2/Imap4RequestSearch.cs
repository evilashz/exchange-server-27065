using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200003D RID: 61
	internal sealed class Imap4RequestSearch : Imap4RequestWithMessageSetSupport
	{
		// Token: 0x06000265 RID: 613 RVA: 0x000109D6 File Offset: 0x0000EBD6
		public Imap4RequestSearch(Imap4ResponseFactory factory, string tag, string parameters) : this(factory, tag, parameters, false)
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000109E4 File Offset: 0x0000EBE4
		public Imap4RequestSearch(Imap4ResponseFactory factory, string tag, string parameters, bool useUids) : base(factory, tag, parameters, useUids, 1, int.MaxValue)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_SEARCH_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_SEARCH_Failures;
			base.AllowedStates = Imap4State.Selected;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00010A46 File Offset: 0x0000EC46
		public override bool AllowsExpungeNotifications
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00010A4C File Offset: 0x0000EC4C
		public QueryFilter CreateMessageSubquery(string messageList, bool parseAsUids)
		{
			base.UseUids = parseAsUids;
			List<ProtocolMessage> messages = base.GetMessages(messageList);
			if (this.ParseResult == ParseResult.invalidMessageSet)
			{
				return null;
			}
			if (string.Equals("1:*", messageList, StringComparison.OrdinalIgnoreCase))
			{
				return Imap4RequestSearch.trueFilter;
			}
			if (messages.Count == 0)
			{
				return Imap4RequestSearch.falseFilter;
			}
			List<QueryFilter> list = new List<QueryFilter>(messages.Count);
			int index = messages[0].Index;
			int id = messages[0].Id;
			for (int i = 0; i < messages.Count - 1; i++)
			{
				if (messages[i + 1].Index != messages[i].Index + 1)
				{
					if (index == messages[i].Index)
					{
						list.Add(this.InternalCreateComparisonFilter(ComparisonOperator.Equal, ItemSchema.ImapId, messages[i].Id));
					}
					else
					{
						list.Add(new AndFilter(new QueryFilter[]
						{
							this.InternalCreateComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.ImapId, id),
							this.InternalCreateComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.ImapId, messages[i].Id)
						}));
					}
					index = messages[i + 1].Index;
					id = messages[i + 1].Id;
				}
			}
			if (index == messages[messages.Count - 1].Index)
			{
				list.Add(this.InternalCreateComparisonFilter(ComparisonOperator.Equal, ItemSchema.ImapId, messages[messages.Count - 1].Id));
			}
			else
			{
				list.Add(new AndFilter(new QueryFilter[]
				{
					this.InternalCreateComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.ImapId, id),
					this.InternalCreateComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.ImapId, messages[messages.Count - 1].Id)
				}));
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return new OrFilter(list.ToArray());
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00010C50 File Offset: 0x0000EE50
		public QueryFilter CreateSearchFilter()
		{
			this.fetchProperties.TryAdd(ItemSchema.DocumentId);
			QueryFilter result = this.InternalCreateAllSearchFilters(base.ArrayOfArguments, 0);
			if (this.sortByList.Count == 0)
			{
				foreach (SortBy item in Imap4Message.SortBys)
				{
					this.sortByList.TryAdd(item);
				}
			}
			return result;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00010CB0 File Offset: 0x0000EEB0
		public override ProtocolResponse Process()
		{
			if (base.Factory.SelectedMailbox.MailboxDoesNotExist)
			{
				base.Factory.SelectedMailbox = null;
				return new Imap4Response(this, Imap4Response.Type.no, "Mailbox has been deleted or moved.");
			}
			bool useUids = base.UseUids;
			Imap4Response imap4Response = new Imap4Response(this, Imap4Response.Type.ok);
			QueryFilter queryFilter = this.CreateSearchFilter();
			if (queryFilter != null)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<QueryFilter>(base.Session.SessionId, "filter = \r\n{0}", queryFilter);
				imap4Response.Append("* SEARCH");
				if (base.Session.LightLogSession != null)
				{
					base.Session.LightLogSession.SearchType = new int?((int)this.options);
					base.Session.LightLogSession.RowsProcessed = new int?(0);
				}
				switch (this.options)
				{
				case Imap4RequestSearch.SearchOptions.InMemorySearch:
				{
					List<Imap4Message> messages = ((Imap4ResponseFactory)base.ResponseFactory).SelectedMailbox.Messages;
					for (int i = 0; i < messages.Count; i++)
					{
						if (!messages[i].IsDeleted && EvaluatableFilter.Evaluate(queryFilter, messages[i]))
						{
							imap4Response.Append(" ");
							if (useUids)
							{
								imap4Response.Append(messages[i].Id.ToString());
							}
							else
							{
								imap4Response.Append(messages[i].Index.ToString());
							}
							if (base.Session.LightLogSession != null)
							{
								base.Session.LightLogSession.RowsProcessed++;
							}
						}
					}
					goto IL_504;
				}
				case Imap4RequestSearch.SearchOptions.SeekToConditionSearch:
				{
					SortBy[] array = this.sortByList.ToArray();
					if (array.Length > 6)
					{
						Array.Resize<SortBy>(ref array, 6);
					}
					List<int> list = new List<int>();
					using (Folder folder = Folder.Bind(base.Factory.Store, base.Factory.SelectedMailbox.Uid, Imap4Mailbox.FolderProperties))
					{
						using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, array, this.fetchProperties.ToArray()))
						{
							Imap4RequestSearch.PropertyBagForInMemorySearch propertyBagForInMemorySearch = new Imap4RequestSearch.PropertyBagForInMemorySearch(this.fetchProperties);
							while (queryResult.SeekToCondition(SeekReference.OriginCurrent, queryFilter, SeekToConditionFlags.AllowExtendedFilters))
							{
								object[][] rows = queryResult.GetRows(10000);
								list.Add((int)rows[0][Imap4RequestSearch.DocumentIdIndex]);
								int j;
								for (j = 1; j < rows.Length; j++)
								{
									propertyBagForInMemorySearch.SetProperties(this.fetchProperties, rows[j]);
									if (EvaluatableFilter.Evaluate(queryFilter, propertyBagForInMemorySearch))
									{
										list.Add((int)rows[j][Imap4RequestSearch.DocumentIdIndex]);
									}
									else if (!this.continueSeeking)
									{
										break;
									}
								}
								if (j < rows.Length && !this.continueSeeking)
								{
									break;
								}
							}
						}
						List<int> list2 = new List<int>(list.Count);
						foreach (int docId in list)
						{
							Imap4Message message = base.Factory.SelectedMailbox.GetMessage(docId);
							if (message != null)
							{
								if (useUids)
								{
									list2.Add(message.Id);
								}
								else
								{
									list2.Add(message.Index);
								}
							}
						}
						list2.Sort();
						foreach (int num in list2)
						{
							imap4Response.Append(" ");
							imap4Response.Append(num.ToString());
							if (base.Session.LightLogSession != null)
							{
								base.Session.LightLogSession.RowsProcessed++;
							}
						}
						goto IL_504;
					}
					break;
				}
				case Imap4RequestSearch.SearchOptions.FullScanSearch:
					break;
				default:
					goto IL_504;
				}
				using (Folder folder2 = Folder.Bind(base.Factory.Store, base.Factory.SelectedMailbox.Uid, Imap4Mailbox.FolderProperties))
				{
					using (QueryResult queryResult2 = folder2.ItemQuery(ItemQueryType.None, queryFilter, Imap4Mailbox.SortBy, Imap4RequestSearch.FullSearchProperties))
					{
						object[][] rows2;
						do
						{
							rows2 = queryResult2.GetRows(10000);
							for (int k = 0; k < rows2.Length; k++)
							{
								this.AddOneMessage(imap4Response, useUids, (int)rows2[k][Imap4RequestSearch.DocumentIdIndex]);
								if (base.Session.LightLogSession != null)
								{
									base.Session.LightLogSession.RowsProcessed++;
								}
							}
						}
						while (rows2.Length > 0);
					}
				}
				base.Imap4CountersInstance.PerfCounter_SEARCHFOLDER_CREATION_Total.Increment();
				IL_504:
				imap4Response.Append(Strings.CRLF);
				imap4Response.Append("[*] SEARCH completed.");
				return imap4Response;
			}
			if (this.ParseResult != ParseResult.success)
			{
				imap4Response.Dispose();
				return base.Factory.ProcessParseError(this);
			}
			imap4Response.Dispose();
			return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0001126C File Offset: 0x0000F46C
		private bool TryGetNextArgumentAsString(List<string> arrayOfArguments, ref int position, out string value)
		{
			if (arrayOfArguments.Count <= position + 1)
			{
				this.ParseResult = ParseResult.invalidNumberOfArguments;
				value = null;
				return false;
			}
			value = arrayOfArguments[++position];
			return true;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000112A4 File Offset: 0x0000F4A4
		private bool TryGetNextArgumentAsDate(List<string> arrayOfArguments, ref int position, out ExDateTime date)
		{
			if (arrayOfArguments.Count <= position + 1)
			{
				this.ParseResult = ParseResult.invalidNumberOfArguments;
				date = ResponseFactory.CurrentExTimeZone.ConvertDateTime(ExDateTime.UtcNow);
				return false;
			}
			return ExDateTime.TryParseExact(ResponseFactory.CurrentExTimeZone, arrayOfArguments[++position], "d-MMM-yyyy", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeLocal, out date);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0001130C File Offset: 0x0000F50C
		private bool TryGetNextArgumentAsInt(List<string> arrayOfArguments, ref int position, out int intA)
		{
			if (arrayOfArguments.Count <= position + 1)
			{
				this.ParseResult = ParseResult.invalidNumberOfArguments;
				intA = 0;
				return false;
			}
			return int.TryParse(arrayOfArguments[++position], out intA);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0001134C File Offset: 0x0000F54C
		private QueryFilter InternalCreateAllSearchFilters(List<string> args, int recursionLevel)
		{
			if (args == null)
			{
				this.ParseResult = ParseResult.invalidArgument;
				return null;
			}
			if (recursionLevel >= 10)
			{
				this.ParseResult = ParseResult.invalidNumberOfArguments;
				return null;
			}
			for (int i = 0; i < args.Count; i++)
			{
				if (string.Equals(args[i], "NOT", StringComparison.OrdinalIgnoreCase))
				{
					if (i == args.Count - 1)
					{
						ProtocolBaseServices.SessionTracer.TraceError(base.Session.SessionId, "Last parameter is NOT");
						return null;
					}
					string value;
					if (Imap4RequestSearch.NotTokens.TryGetValue(args[i + 1], out value))
					{
						args[i] = value;
						args.RemoveAt(i + 1);
						i++;
					}
				}
			}
			List<QueryFilter> list = new List<QueryFilter>(args.Count);
			for (int j = 0; j < args.Count; j++)
			{
				QueryFilter queryFilter = this.InternalCreateSingleSearchFilter(args, ref j, recursionLevel);
				if (queryFilter == null)
				{
					return null;
				}
				list.Add(queryFilter);
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			QueryFilter[] filters = list.ToArray();
			return new AndFilter(filters);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00011448 File Offset: 0x0000F648
		private QueryFilter InternalCreateSingleSearchFilter(List<string> args, ref int position, int recursionLevel)
		{
			if (args == null || position < 0 || position >= args.Count)
			{
				return null;
			}
			if (recursionLevel >= 10)
			{
				this.ParseResult = ParseResult.invalidNumberOfArguments;
				return null;
			}
			int lastSeenArticleId = base.Factory.SelectedMailbox.LastSeenArticleId;
			string key;
			string text;
			QueryFilter queryFilter2;
			switch (key = args[position].ToUpper())
			{
			case "CHARSET":
				if (!this.TryGetNextArgumentAsString(args, ref position, out text))
				{
					return null;
				}
				if (!string.Equals(text, "US-ASCII", StringComparison.OrdinalIgnoreCase))
				{
					this.ParseResult = ParseResult.invalidCharset;
					return null;
				}
				return Imap4RequestSearch.trueFilter;
			case "ANSWERED":
				return this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.MessageAnswered, true);
			case "DELETED":
				return this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.MessageDelMarked, true);
			case "DRAFT":
				return this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.IsDraft, true);
			case "FLAGGED":
				return this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.MessageTagged, true);
			case "KEYWORD":
			{
				string strA;
				if (!this.TryGetNextArgumentAsString(args, ref position, out strA))
				{
					return null;
				}
				if (string.Compare(strA, "$MDNSent", StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.ParseResult = ParseResult.invalidArgument;
					return null;
				}
				return new AndFilter(new QueryFilter[]
				{
					this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.MessageDeliveryNotificationSent, true),
					this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.MessageDelMarked, false)
				});
			}
			case "NEW":
				return new AndFilter(new QueryFilter[]
				{
					this.InternalCreateComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.ImapId, lastSeenArticleId),
					this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.IsRead, false)
				});
			case "OLD":
				return this.InternalCreateComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.ImapId, lastSeenArticleId);
			case "RECENT":
				return this.InternalCreateComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.ImapId, lastSeenArticleId);
			case "SEEN":
				return this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.IsRead, true);
			case "UNANSWERED":
				return this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.MessageAnswered, false);
			case "UNDELETED":
				return this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.MessageDelMarked, false);
			case "UNDRAFT":
				return this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.IsDraft, false);
			case "UNFLAGGED":
				return this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.MessageTagged, false);
			case "UNKEYWORD":
			{
				string strA;
				if (!this.TryGetNextArgumentAsString(args, ref position, out strA))
				{
					return null;
				}
				if (string.Compare(strA, "$MDNSent", StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.ParseResult = ParseResult.invalidArgument;
					return null;
				}
				return new AndFilter(new QueryFilter[]
				{
					this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.MessageDeliveryNotificationSent, false),
					this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.MessageDelMarked, false)
				});
			}
			case "UNSEEN":
				return this.InternalCreateComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.IsRead, false);
			case "BCC":
			{
				if (!this.TryGetNextArgumentAsString(args, ref position, out text))
				{
					return null;
				}
				QueryFilter queryFilter = new AndFilter(new QueryFilter[]
				{
					this.InternalCreateComparisonFilter(ComparisonOperator.Equal, RecipientSchema.RecipientType, 3),
					new TextFilter(StoreObjectSchema.DisplayName, text, MatchOptions.SubString, MatchFlags.Loose)
				});
				queryFilter2 = new SubFilter(SubFilterProperties.Recipients, queryFilter);
				this.options = Imap4RequestSearch.SearchOptions.FullScanSearch;
				return queryFilter2;
			}
			case "BODY":
			{
				if (!this.TryGetNextArgumentAsString(args, ref position, out text))
				{
					return null;
				}
				QueryFilter queryFilter3 = new TextFilter(ItemSchema.TextBody, text, MatchOptions.SubString, MatchFlags.Loose);
				queryFilter2 = (base.Factory.RefreshSearchFolderItemsEnabled ? new CountFilter(250U, queryFilter3) : queryFilter3);
				this.options = Imap4RequestSearch.SearchOptions.FullScanSearch;
				return queryFilter2;
			}
			case "CC":
			{
				if (!this.TryGetNextArgumentAsString(args, ref position, out text))
				{
					return null;
				}
				QueryFilter queryFilter = new AndFilter(new QueryFilter[]
				{
					this.InternalCreateComparisonFilter(ComparisonOperator.Equal, RecipientSchema.RecipientType, 2),
					new TextFilter(StoreObjectSchema.DisplayName, text, MatchOptions.SubString, MatchFlags.Loose)
				});
				queryFilter2 = new SubFilter(SubFilterProperties.Recipients, queryFilter);
				this.options = Imap4RequestSearch.SearchOptions.FullScanSearch;
				return queryFilter2;
			}
			case "FROM":
				if (!this.TryGetNextArgumentAsString(args, ref position, out text))
				{
					return null;
				}
				queryFilter2 = new TextFilter(ItemSchema.SentRepresentingDisplayName, text, MatchOptions.SubString, MatchFlags.Loose);
				this.options = Imap4RequestSearch.SearchOptions.FullScanSearch;
				return queryFilter2;
			case "HEADER":
			{
				string text2;
				if (!this.TryGetNextArgumentAsString(args, ref position, out text2))
				{
					return null;
				}
				if (!this.TryGetNextArgumentAsString(args, ref position, out text))
				{
					return null;
				}
				if (string.Equals(text2, "Message-ID", StringComparison.OrdinalIgnoreCase))
				{
					if (!string.IsNullOrEmpty(text) && text[0] == '<' && text[text.Length - 1] == '>')
					{
						queryFilter2 = this.InternalCreateComparisonFilter(ComparisonOperator.Equal, ItemSchema.InternetMessageId, text);
					}
					else
					{
						queryFilter2 = new TextFilter(ItemSchema.InternetMessageId, text, MatchOptions.SubString, MatchFlags.IgnoreCase);
						this.continueSeeking = true;
						this.fetchProperties.TryAdd(ItemSchema.InternetMessageId);
					}
					if (this.options < Imap4RequestSearch.SearchOptions.FullScanSearch)
					{
						this.options = Imap4RequestSearch.SearchOptions.SeekToConditionSearch;
						return queryFilter2;
					}
					return queryFilter2;
				}
				else
				{
					if (!string.Equals(text2, "Subject", StringComparison.OrdinalIgnoreCase))
					{
						queryFilter2 = new AndFilter(new QueryFilter[]
						{
							new TextFilter(MessageItemSchema.TransportMessageHeaders, "\n" + text2 + ":", MatchOptions.SubString, MatchFlags.Loose),
							new TextFilter(MessageItemSchema.TransportMessageHeaders, text, MatchOptions.SubString, MatchFlags.Loose)
						});
						this.options = Imap4RequestSearch.SearchOptions.FullScanSearch;
						return queryFilter2;
					}
					queryFilter2 = new TextFilter(ItemSchema.Subject, text, MatchOptions.SubString, MatchFlags.IgnoreCase);
					this.continueSeeking = true;
					if (this.options < Imap4RequestSearch.SearchOptions.FullScanSearch)
					{
						this.options = Imap4RequestSearch.SearchOptions.SeekToConditionSearch;
						this.fetchProperties.TryAdd(ItemSchema.Subject);
						return queryFilter2;
					}
					return queryFilter2;
				}
				break;
			}
			case "LARGER":
			{
				int num2;
				if (!this.TryGetNextArgumentAsInt(args, ref position, out num2))
				{
					return null;
				}
				QueryFilter queryFilter = new AndFilter(new QueryFilter[]
				{
					new ExistsFilter(ItemSchema.ImapMIMESize),
					this.InternalCreateComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.ImapMIMESize, (long)num2)
				});
				QueryFilter queryFilter4 = new AndFilter(new QueryFilter[]
				{
					new NotFilter(new ExistsFilter(ItemSchema.ImapMIMESize)),
					this.InternalCreateComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.Size, num2)
				});
				queryFilter2 = new OrFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter4
				});
				this.continueSeeking = true;
				if (this.options < Imap4RequestSearch.SearchOptions.FullScanSearch)
				{
					this.options = Imap4RequestSearch.SearchOptions.SeekToConditionSearch;
					return queryFilter2;
				}
				return queryFilter2;
			}
			case "SMALLER":
			{
				int num2;
				if (!this.TryGetNextArgumentAsInt(args, ref position, out num2))
				{
					return null;
				}
				QueryFilter queryFilter = new AndFilter(new QueryFilter[]
				{
					new ExistsFilter(ItemSchema.ImapMIMESize),
					this.InternalCreateComparisonFilter(ComparisonOperator.LessThan, ItemSchema.ImapMIMESize, (long)num2)
				});
				QueryFilter queryFilter4 = new AndFilter(new QueryFilter[]
				{
					new NotFilter(new ExistsFilter(ItemSchema.ImapMIMESize)),
					this.InternalCreateComparisonFilter(ComparisonOperator.LessThan, ItemSchema.Size, num2)
				});
				queryFilter2 = new OrFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter4
				});
				this.continueSeeking = true;
				if (this.options < Imap4RequestSearch.SearchOptions.FullScanSearch)
				{
					this.options = Imap4RequestSearch.SearchOptions.SeekToConditionSearch;
					return queryFilter2;
				}
				return queryFilter2;
			}
			case "SUBJECT":
				if (args.Count <= position + 1)
				{
					this.ParseResult = ParseResult.invalidNumberOfArguments;
					return null;
				}
				text = args[++position];
				queryFilter2 = new TextFilter(ItemSchema.Subject, text, MatchOptions.SubString, MatchFlags.IgnoreCase);
				this.continueSeeking = true;
				if (this.options < Imap4RequestSearch.SearchOptions.FullScanSearch)
				{
					this.options = Imap4RequestSearch.SearchOptions.SeekToConditionSearch;
					this.fetchProperties.TryAdd(ItemSchema.Subject);
					return queryFilter2;
				}
				return queryFilter2;
			case "TEXT":
				if (!this.TryGetNextArgumentAsString(args, ref position, out text))
				{
					return null;
				}
				queryFilter2 = new OrFilter(new QueryFilter[]
				{
					new TextFilter(ItemSchema.TextBody, text, MatchOptions.SubString, MatchFlags.Loose),
					new TextFilter(MessageItemSchema.TransportMessageHeaders, text, MatchOptions.SubString, MatchFlags.Loose)
				});
				this.options = Imap4RequestSearch.SearchOptions.FullScanSearch;
				return queryFilter2;
			case "TO":
			{
				if (!this.TryGetNextArgumentAsString(args, ref position, out text))
				{
					return null;
				}
				QueryFilter queryFilter = new AndFilter(new QueryFilter[]
				{
					this.InternalCreateComparisonFilter(ComparisonOperator.Equal, RecipientSchema.RecipientType, 1),
					new TextFilter(StoreObjectSchema.DisplayName, text, MatchOptions.SubString, MatchFlags.Loose)
				});
				queryFilter2 = new SubFilter(SubFilterProperties.Recipients, queryFilter);
				this.options = Imap4RequestSearch.SearchOptions.FullScanSearch;
				return queryFilter2;
			}
			case "BEFORE":
			{
				ExDateTime exDateTime;
				if (!this.TryGetNextArgumentAsDate(args, ref position, out exDateTime))
				{
					return null;
				}
				QueryFilter queryFilter = new AndFilter(new QueryFilter[]
				{
					new ExistsFilter(ItemSchema.ImapInternalDate),
					this.InternalCreateComparisonFilter(ComparisonOperator.LessThan, ItemSchema.ImapInternalDate, exDateTime)
				});
				QueryFilter queryFilter4 = new AndFilter(new QueryFilter[]
				{
					new NotFilter(new ExistsFilter(ItemSchema.ImapInternalDate)),
					this.InternalCreateComparisonFilter(ComparisonOperator.LessThan, ItemSchema.ReceivedTime, exDateTime)
				});
				queryFilter2 = new OrFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter4
				});
				this.continueSeeking = true;
				if (this.options < Imap4RequestSearch.SearchOptions.FullScanSearch)
				{
					this.options = Imap4RequestSearch.SearchOptions.SeekToConditionSearch;
					return queryFilter2;
				}
				return queryFilter2;
			}
			case "ON":
			{
				ExDateTime exDateTime;
				if (!this.TryGetNextArgumentAsDate(args, ref position, out exDateTime))
				{
					return null;
				}
				ExDateTime exDateTime2 = exDateTime.AddDays(1.0);
				QueryFilter queryFilter = new AndFilter(new QueryFilter[]
				{
					new ExistsFilter(ItemSchema.ImapInternalDate),
					this.InternalCreateComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.ImapInternalDate, exDateTime),
					this.InternalCreateComparisonFilter(ComparisonOperator.LessThan, ItemSchema.ImapInternalDate, exDateTime2)
				});
				QueryFilter queryFilter4 = new AndFilter(new QueryFilter[]
				{
					new NotFilter(new ExistsFilter(ItemSchema.ImapInternalDate)),
					this.InternalCreateComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.ReceivedTime, exDateTime),
					this.InternalCreateComparisonFilter(ComparisonOperator.LessThan, ItemSchema.ReceivedTime, exDateTime2)
				});
				queryFilter2 = new OrFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter4
				});
				this.continueSeeking = true;
				if (this.options < Imap4RequestSearch.SearchOptions.FullScanSearch)
				{
					this.options = Imap4RequestSearch.SearchOptions.SeekToConditionSearch;
					return queryFilter2;
				}
				return queryFilter2;
			}
			case "SENTBEFORE":
			{
				ExDateTime exDateTime;
				if (!this.TryGetNextArgumentAsDate(args, ref position, out exDateTime))
				{
					return null;
				}
				queryFilter2 = this.InternalCreateComparisonFilter(ComparisonOperator.LessThan, ItemSchema.SentTime, exDateTime);
				if (this.options < Imap4RequestSearch.SearchOptions.FullScanSearch)
				{
					this.options = Imap4RequestSearch.SearchOptions.SeekToConditionSearch;
					return queryFilter2;
				}
				return queryFilter2;
			}
			case "SENTON":
			{
				ExDateTime exDateTime;
				if (!this.TryGetNextArgumentAsDate(args, ref position, out exDateTime))
				{
					return null;
				}
				ExDateTime exDateTime2 = exDateTime.AddDays(1.0);
				queryFilter2 = new AndFilter(new QueryFilter[]
				{
					this.InternalCreateComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.SentTime, exDateTime),
					this.InternalCreateComparisonFilter(ComparisonOperator.LessThan, ItemSchema.SentTime, exDateTime2)
				});
				this.continueSeeking = true;
				if (this.options < Imap4RequestSearch.SearchOptions.FullScanSearch)
				{
					this.options = Imap4RequestSearch.SearchOptions.SeekToConditionSearch;
					return queryFilter2;
				}
				return queryFilter2;
			}
			case "SENTSINCE":
			{
				ExDateTime exDateTime;
				if (!this.TryGetNextArgumentAsDate(args, ref position, out exDateTime))
				{
					return null;
				}
				queryFilter2 = this.InternalCreateComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.SentTime, exDateTime);
				if (this.options < Imap4RequestSearch.SearchOptions.FullScanSearch)
				{
					this.options = Imap4RequestSearch.SearchOptions.SeekToConditionSearch;
					return queryFilter2;
				}
				return queryFilter2;
			}
			case "SINCE":
			{
				ExDateTime exDateTime;
				if (!this.TryGetNextArgumentAsDate(args, ref position, out exDateTime))
				{
					return null;
				}
				QueryFilter queryFilter = new AndFilter(new QueryFilter[]
				{
					new ExistsFilter(ItemSchema.ImapInternalDate),
					this.InternalCreateComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.ImapInternalDate, exDateTime)
				});
				QueryFilter queryFilter4 = new AndFilter(new QueryFilter[]
				{
					new NotFilter(new ExistsFilter(ItemSchema.ImapInternalDate)),
					this.InternalCreateComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.ReceivedTime, exDateTime)
				});
				queryFilter2 = new OrFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter4
				});
				this.continueSeeking = true;
				if (this.options < Imap4RequestSearch.SearchOptions.FullScanSearch)
				{
					this.options = Imap4RequestSearch.SearchOptions.SeekToConditionSearch;
					return queryFilter2;
				}
				return queryFilter2;
			}
			case "NOT":
				position++;
				queryFilter2 = this.InternalCreateSingleSearchFilter(args, ref position, recursionLevel + 1);
				if (queryFilter2 != null)
				{
					return new NotFilter(queryFilter2);
				}
				this.ParseResult = ParseResult.invalidArgument;
				return queryFilter2;
			case "OR":
			{
				queryFilter2 = null;
				position++;
				QueryFilter queryFilter5 = this.InternalCreateSingleSearchFilter(args, ref position, recursionLevel + 1);
				if (queryFilter5 != null)
				{
					position++;
					QueryFilter queryFilter6 = this.InternalCreateSingleSearchFilter(args, ref position, recursionLevel + 1);
					if (queryFilter6 != null)
					{
						queryFilter2 = new OrFilter(new QueryFilter[]
						{
							queryFilter5,
							queryFilter6
						});
					}
				}
				if (queryFilter2 == null)
				{
					this.ParseResult = ParseResult.invalidNumberOfArguments;
				}
				this.continueSeeking = true;
				return queryFilter2;
			}
			case "ALL":
				return Imap4RequestSearch.trueFilter;
			case "UID":
				if (!this.TryGetNextArgumentAsString(args, ref position, out text))
				{
					return null;
				}
				queryFilter2 = this.CreateMessageSubquery(text, true);
				if (queryFilter2 == null)
				{
					return null;
				}
				return queryFilter2;
			}
			text = args[position];
			if (this.IsBracketedString(text))
			{
				queryFilter2 = this.ParseBracketedArgs(text, recursionLevel);
			}
			else
			{
				queryFilter2 = this.CreateMessageSubquery(text, false);
				if (queryFilter2 == null)
				{
					return null;
				}
			}
			return queryFilter2;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000122AC File Offset: 0x000104AC
		private QueryFilter InternalCreateComparisonFilter(ComparisonOperator comparisonOperator, PropertyDefinition propertyDefinition, object propertyValue)
		{
			PropertyDefinition columnDefinition;
			if (Imap4RequestSearch.NativeProperties.TryGetValue(propertyDefinition, out columnDefinition))
			{
				this.sortByList.TryAdd(new SortBy(columnDefinition, SortOrder.Ascending));
				this.continueSeeking = true;
			}
			else
			{
				this.sortByList.TryAdd(new SortBy(propertyDefinition, SortOrder.Ascending));
			}
			this.fetchProperties.TryAdd(propertyDefinition);
			return new ComparisonFilter(comparisonOperator, propertyDefinition, propertyValue);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0001230C File Offset: 0x0001050C
		private bool IsBracketedString(string arg)
		{
			return !string.IsNullOrEmpty(arg) && '(' == arg[0] && ')' == arg[arg.Length - 1];
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00012338 File Offset: 0x00010538
		private QueryFilter ParseBracketedArgs(string argString, int recursionLevel)
		{
			if (!this.IsBracketedString(argString))
			{
				this.ParseResult = ParseResult.invalidArgument;
				return null;
			}
			List<string> args = new List<string>();
			if (!Imap4RequestWithStringParameters.TryParseArguments(ref args, argString.Substring(1, argString.Length - 2)))
			{
				this.ParseResult = ParseResult.invalidArgument;
				return null;
			}
			return this.InternalCreateAllSearchFilters(args, recursionLevel + 1);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00012388 File Offset: 0x00010588
		private void AddOneMessage(Imap4Response response, bool returnUids, int documentId)
		{
			Imap4Message message = base.Factory.SelectedMailbox.GetMessage(documentId);
			if (message == null)
			{
				return;
			}
			response.Append(" ");
			if (returnUids)
			{
				response.Append(message.Id.ToString());
				return;
			}
			response.Append(message.Index.ToString());
		}

		// Token: 0x040001D3 RID: 467
		internal const string InvalidCharset = "[BADCHARSET (US-ASCII)] The specified charset is not supported.";

		// Token: 0x040001D4 RID: 468
		private const string SEARCHBegining = "* SEARCH";

		// Token: 0x040001D5 RID: 469
		private const string SEARCHResponseCompleted = "[*] SEARCH completed.";

		// Token: 0x040001D6 RID: 470
		private const int MaxRecursionLevel = 10;

		// Token: 0x040001D7 RID: 471
		private const uint MaxNoOfSearchHitsToReturn = 250U;

		// Token: 0x040001D8 RID: 472
		private const string DateFormat = "d-MMM-yyyy";

		// Token: 0x040001D9 RID: 473
		private static readonly FalseFilter falseFilter = new FalseFilter();

		// Token: 0x040001DA RID: 474
		private static readonly TrueFilter trueFilter = new TrueFilter();

		// Token: 0x040001DB RID: 475
		private static readonly int DocumentIdIndex = 0;

		// Token: 0x040001DC RID: 476
		private static readonly PropertyDefinition[] FullSearchProperties = new PropertyDefinition[]
		{
			ItemSchema.DocumentId
		};

		// Token: 0x040001DD RID: 477
		private static readonly Dictionary<string, string> NotTokens = new Dictionary<string, string>
		{
			{
				"DELETED",
				"UNDELETED"
			},
			{
				"SEEN",
				"UNSEEN"
			},
			{
				"ANSWERED",
				"UNANSWERED"
			},
			{
				"DRAFT",
				"UNDRAFT"
			},
			{
				"FLAGGED",
				"UNFLAGGED"
			},
			{
				"KEYWORD",
				"UNKEYWORD"
			},
			{
				"UNDELETED",
				"DELETED"
			},
			{
				"UNSEEN",
				"SEEN"
			},
			{
				"UNANSWERED",
				"ANSWERED"
			},
			{
				"UNDRAFT",
				"DRAFT"
			},
			{
				"UNFLAGGED",
				"FLAGGED"
			},
			{
				"UNKEYWORD",
				"KEYWORD"
			}
		};

		// Token: 0x040001DE RID: 478
		private static readonly Dictionary<PropertyDefinition, PropertyDefinition> NativeProperties = new Dictionary<PropertyDefinition, PropertyDefinition>
		{
			{
				MessageItemSchema.IsDraft,
				CoreItemSchema.Flags
			},
			{
				MessageItemSchema.IsRead,
				CoreItemSchema.Flags
			},
			{
				MessageItemSchema.MessageAnswered,
				ItemSchema.MessageStatus
			},
			{
				MessageItemSchema.MessageDeliveryNotificationSent,
				ItemSchema.MessageStatus
			},
			{
				MessageItemSchema.MessageDelMarked,
				ItemSchema.MessageStatus
			},
			{
				MessageItemSchema.MessageTagged,
				ItemSchema.MessageStatus
			}
		};

		// Token: 0x040001DF RID: 479
		private Imap4RequestSearch.SearchOptions options;

		// Token: 0x040001E0 RID: 480
		private HashSet<SortBy> sortByList = new HashSet<SortBy>(Imap4RequestSearch.SortByComparer.Instance);

		// Token: 0x040001E1 RID: 481
		private HashSet<PropertyDefinition> fetchProperties = new HashSet<PropertyDefinition>();

		// Token: 0x040001E2 RID: 482
		private bool continueSeeking;

		// Token: 0x0200003E RID: 62
		internal enum SearchOptions
		{
			// Token: 0x040001E4 RID: 484
			InMemorySearch,
			// Token: 0x040001E5 RID: 485
			SeekToConditionSearch,
			// Token: 0x040001E6 RID: 486
			FullScanSearch
		}

		// Token: 0x0200003F RID: 63
		internal class PropertyBagForInMemorySearch : IPropertyBag, IReadOnlyPropertyBag
		{
			// Token: 0x06000275 RID: 629 RVA: 0x00012558 File Offset: 0x00010758
			public PropertyBagForInMemorySearch(ICollection<PropertyDefinition> propertyDefinitionArray)
			{
				this.propertyDefinitionArray = propertyDefinitionArray;
				this.propertyIndexes = new Dictionary<PropertyDefinition, int>(propertyDefinitionArray.Count);
				int num = 0;
				foreach (PropertyDefinition key in propertyDefinitionArray)
				{
					this.propertyIndexes[key] = num++;
				}
			}

			// Token: 0x06000276 RID: 630 RVA: 0x000125CC File Offset: 0x000107CC
			public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
			{
				return this.propertyValuesArray;
			}

			// Token: 0x170000CD RID: 205
			public object this[PropertyDefinition propertyDefinition]
			{
				get
				{
					int num;
					if (!this.propertyIndexes.TryGetValue(propertyDefinition, out num))
					{
						return new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
					}
					return this.propertyValuesArray[num];
				}
				set
				{
					int num;
					if (!this.propertyIndexes.TryGetValue(propertyDefinition, out num))
					{
						throw new ArgumentException(string.Format("Property {0} is not in the property bag.", propertyDefinition));
					}
					this.propertyValuesArray[num] = value;
				}
			}

			// Token: 0x06000279 RID: 633 RVA: 0x0001263B File Offset: 0x0001083B
			public void SetProperties(ICollection<PropertyDefinition> propertyDefinitionArray, object[] propertyValuesArray)
			{
				if (this.propertyDefinitionArray != propertyDefinitionArray)
				{
					throw new ArgumentException("Invalid use of SetProperties. propertyDefinitionArray should be the same as the used to create the bag");
				}
				if (propertyValuesArray.Length != this.propertyDefinitionArray.Count)
				{
					throw new ArgumentException("Unexpected array length");
				}
				this.propertyValuesArray = propertyValuesArray;
			}

			// Token: 0x040001E7 RID: 487
			private Dictionary<PropertyDefinition, int> propertyIndexes;

			// Token: 0x040001E8 RID: 488
			private ICollection<PropertyDefinition> propertyDefinitionArray;

			// Token: 0x040001E9 RID: 489
			private object[] propertyValuesArray;
		}

		// Token: 0x02000040 RID: 64
		internal class SortByComparer : IEqualityComparer<SortBy>
		{
			// Token: 0x0600027A RID: 634 RVA: 0x00012673 File Offset: 0x00010873
			public bool Equals(SortBy x, SortBy y)
			{
				return string.Equals(x.ColumnDefinition.Name, y.ColumnDefinition.Name, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x0600027B RID: 635 RVA: 0x00012691 File Offset: 0x00010891
			public int GetHashCode(SortBy obj)
			{
				return obj.ColumnDefinition.Name.GetHashCode();
			}

			// Token: 0x040001EA RID: 490
			public static readonly Imap4RequestSearch.SortByComparer Instance = new Imap4RequestSearch.SortByComparer();
		}
	}
}
