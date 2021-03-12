using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000034 RID: 52
	internal sealed class Imap4RequestFetch : Imap4RequestWithMessageSetSupport
	{
		// Token: 0x06000208 RID: 520 RVA: 0x0000E198 File Offset: 0x0000C398
		static Imap4RequestFetch()
		{
			ParticipantSchema.SupportedADProperties.CopyTo(Imap4RequestFetch.supportedADProperties, 0);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000E219 File Offset: 0x0000C419
		public Imap4RequestFetch(Imap4ResponseFactory factory, string tag, string data) : this(factory, tag, data, false)
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000E225 File Offset: 0x0000C425
		public Imap4RequestFetch(Imap4ResponseFactory factory, string tag, string data, bool useUids) : base(factory, tag, data, useUids, 2, 2)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_FETCH_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_FETCH_Failures;
			base.AllowedStates = Imap4State.Selected;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000E25D File Offset: 0x0000C45D
		public override bool AllowsExpungeNotifications
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000E260 File Offset: 0x0000C460
		public override bool NeedsStoreConnection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000E263 File Offset: 0x0000C463
		public override bool NeedToDelayStoreAction
		{
			get
			{
				return this.delayedActionsEnqueued;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
		public override ProtocolResponse Process()
		{
			if (base.Factory.SelectedMailbox.MailboxDoesNotExist)
			{
				base.Factory.SelectedMailbox = null;
				return new Imap4Response(this, Imap4Response.Type.no, "Mailbox has been deleted or moved.");
			}
			List<ProtocolMessage> messages = base.GetMessages(base.ArrayOfArguments[0]);
			if (this.ParseResult == ParseResult.invalidMessageSet)
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "[*] The specified message set is invalid.");
			}
			Imap4RequestFetch.DataItems dataItems = Imap4RequestFetch.DataItems.Parse(base.ArrayOfArguments[1], base.Factory.ExactRFC822SizeEnabled);
			if (dataItems == null)
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			if (base.Factory.SelectedMailbox.ExamineMode)
			{
				dataItems.SetSeenFlag = false;
			}
			ProtocolBaseServices.SessionTracer.TraceDebug<Imap4RequestFetch.DataItems>(base.Session.SessionId, "Data items: {0}", dataItems);
			StoreObjectId[] array = null;
			if (dataItems.LoadItem || dataItems.LoadItemBody || dataItems.SetSeenFlag)
			{
				array = base.Factory.SelectedMailbox.DataAccessView.GetStoreObjectIds(messages);
			}
			if (messages.Count > 0)
			{
				if (dataItems.LoadItem || dataItems.LoadItemBody)
				{
					OutboundConversionOptions outboundConversionOptions = this.GetOutboundConversionOptions();
					Imap4RequestFetch.MessageListResponseItem responseitem = new Imap4RequestFetch.MessageListResponseItem(messages, array, dataItems, outboundConversionOptions, base.UseUids);
					if (!base.Factory.Session.SendToClient(responseitem))
					{
						return null;
					}
					if (dataItems.SetSeenFlag)
					{
						DataProcessResponseItem responseitem2 = new DataProcessResponseItem(array, delegate(ProtocolSession session, DataProcessResponseItem responseItem)
						{
							StoreObjectId[] array2 = responseItem.StateData as StoreObjectId[];
							List<StoreObjectId> list = new List<StoreObjectId>(256);
							for (int i = 0; i < array2.Length; i++)
							{
								list.Add(array2[i]);
							}
							session.ResponseFactory.MarkAsRead(list.ToArray());
							((Imap4ResponseFactory)session.ResponseFactory).SelectedMailbox.UpdateMessageCounters();
						});
						if (!base.Factory.Session.SendToClient(responseitem2))
						{
							return null;
						}
					}
				}
				else
				{
					if (dataItems.UseItemQuery)
					{
						bool flag = !base.Factory.IsStoreConnected;
						IStandardBudget perCallBudget = null;
						try
						{
							ActivityContext.SetThreadScope(base.Session.ActivityScope);
							if (flag)
							{
								Monitor.Enter(base.Factory.Store);
								base.Factory.Store.Connect();
							}
							try
							{
								perCallBudget = base.Factory.AcquirePerCommandBudget();
							}
							catch (OverBudgetException exception)
							{
								return base.Factory.ProcessException(this, exception);
							}
							catch (ADTransientException exception2)
							{
								return base.Factory.ProcessException(this, exception2);
							}
							using (Folder folder = Folder.Bind(base.Factory.Store, base.Factory.SelectedMailbox.Uid))
							{
								using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, Imap4RequestFetch.SortByUid, Imap4RequestFetch.PropertiesToQuery))
								{
									if (!this.ProcessAllMessagesAtOnce(messages, queryResult, dataItems, base.UseUids))
									{
										return null;
									}
								}
							}
							goto IL_2C0;
						}
						finally
						{
							if (flag)
							{
								base.Factory.DisconnectFromTheStore();
							}
							base.Session.EnforceMicroDelayAndDisposeCostHandles(perCallBudget);
							if (flag)
							{
								Monitor.Exit(base.Session.ResponseFactory.Store);
							}
						}
					}
					if (!this.ProcessAllMessagesAtOnce(messages, null, dataItems, base.UseUids))
					{
						return null;
					}
				}
			}
			IL_2C0:
			this.delayedActionsEnqueued = true;
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.RowsProcessed = new int?(messages.Count);
			}
			if (base.DeletedMessages)
			{
				if (base.Session.LightLogSession != null)
				{
					base.Session.LightLogSession.Result = "[*] Some of the requested messages no longer exist.".Replace("[*]", "NO");
				}
				return new Imap4Response(this, Imap4Response.Type.no, "[*] Some of the requested messages no longer exist.");
			}
			if (base.MessageSetIsInvalid && !base.UseUids)
			{
				if (base.Session.LightLogSession != null)
				{
					base.Session.LightLogSession.Result = "[*] The specified message set is invalid.".Replace("[*]", "NO");
				}
				return new Imap4Response(this, Imap4Response.Type.no, "[*] The specified message set is invalid.");
			}
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.Result = "OK";
			}
			return new Imap4Response(this, Imap4Response.Type.ok, "[*] FETCH completed.");
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000E6D4 File Offset: 0x0000C8D4
		private static void AppendFetchResponseLine(StringBuilder lineBuilder, string arg0)
		{
			lineBuilder.Append("* ");
			lineBuilder.Append(arg0);
			lineBuilder.Append(" FETCH (");
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000E6F6 File Offset: 0x0000C8F6
		private static void AppendFetchResponseDataItem(StringBuilder lineBuilder, string arg0, string arg1)
		{
			lineBuilder.Append(arg0);
			lineBuilder.Append(" ");
			lineBuilder.Append(arg1);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000E714 File Offset: 0x0000C914
		private static void AppendFetchResponseDataItemWithQuotes(StringBuilder lineBuilder, string arg0, string arg1)
		{
			lineBuilder.Append(arg0);
			lineBuilder.Append(" \"");
			lineBuilder.Append(arg1);
			lineBuilder.Append("\"");
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000E73E File Offset: 0x0000C93E
		private static void AppendFetchResponseBodyPartLiteral(StringBuilder lineBuilder, string arg0, string arg1)
		{
			lineBuilder.Append(arg0);
			lineBuilder.Append(" {");
			lineBuilder.Append(arg1);
			lineBuilder.Append("}");
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000E768 File Offset: 0x0000C968
		private static void AppendFetchResponseBodyPartNIL(StringBuilder lineBuilder, string arg0)
		{
			lineBuilder.Append(arg0);
			lineBuilder.Append(" NIL");
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000E77E File Offset: 0x0000C97E
		private static void AppendFetchResponseBodyPartEmptyPart(StringBuilder lineBuilder, string arg0)
		{
			lineBuilder.Append(arg0);
			lineBuilder.Append(" \"\"");
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000E794 File Offset: 0x0000C994
		private static void AppendFetchResponseBodyPartAtTheEnd(StringBuilder lineBuilder, string arg0)
		{
			lineBuilder.Append(arg0);
			lineBuilder.Append(" {0}\r\n");
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000E7AA File Offset: 0x0000C9AA
		private static void AppendFetchResponseDataItemLiteral(StringBuilder lineBuilder, string arg0, string arg1)
		{
			lineBuilder.Append(arg0);
			lineBuilder.Append(" {");
			lineBuilder.Append(arg1);
			lineBuilder.Append("}");
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000F2F0 File Offset: 0x0000D4F0
		private static void ProcessMessage(ProtocolSession protocolSession, DataProcessResponseItem responseItem)
		{
			Imap4RequestFetch.<>c__DisplayClass4 CS$<>8__locals1 = new Imap4RequestFetch.<>c__DisplayClass4();
			CS$<>8__locals1.protocolSession = protocolSession;
			CS$<>8__locals1.responseItem = responseItem;
			CS$<>8__locals1.messageStateData = (CS$<>8__locals1.responseItem.StateData as Imap4RequestFetch.MessageStateData);
			CS$<>8__locals1.dataItems = CS$<>8__locals1.messageStateData.DataItems;
			if (!CS$<>8__locals1.dataItems.LoadItem && !CS$<>8__locals1.dataItems.LoadItemBody)
			{
				throw new InvalidOperationException("Should never get here without LoadItem or LoadItemBody = true");
			}
			CS$<>8__locals1.item = null;
			CS$<>8__locals1.messageStateData.ShouldReturn = false;
			ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ProcessMessage>b__2)), new FilterDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ProcessMessage>b__3)), null);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000F38E File Offset: 0x0000D58E
		private static void AddSpace(StringBuilder lineBuilder)
		{
			if (lineBuilder.Length == 0 || lineBuilder[lineBuilder.Length - 1] != '(')
			{
				lineBuilder.Append(" ");
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000F3B6 File Offset: 0x0000D5B6
		private static void WriteHeaders(OutboundConversionOptions options, string cultureName, MimePartHeaders headers, Stream stream)
		{
			Imap4RequestFetch.WriteHeaders(options, cultureName, headers, null, stream);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000F3C4 File Offset: 0x0000D5C4
		private static void WriteHeaders(OutboundConversionOptions options, string cultureName, MimePartHeaders headers, Imap4RequestFetch.BodyPart bodyPart, Stream stream)
		{
			EncodingFlags encodingFlags = EncodingFlags.None;
			if (options.QuoteDisplayNameBeforeRfc2047Encoding)
			{
				encodingFlags |= EncodingFlags.QuoteDisplayNameBeforeRfc2047Encoding;
			}
			MimeWriter mimeWriter = new MimeWriter(stream, false, new EncodingOptions(headers.Charset.Name, cultureName, encodingFlags));
			mimeWriter.StartPart();
			foreach (Header header in headers)
			{
				if (bodyPart == null)
				{
					header.WriteTo(mimeWriter);
				}
				else
				{
					bool flag = false;
					foreach (string strB in bodyPart.FieldNames)
					{
						if (string.Compare(header.Name, strB, StringComparison.OrdinalIgnoreCase) == 0)
						{
							flag = true;
							break;
						}
					}
					if (flag ^ bodyPart.BodyPiece == Imap4RequestFetch.BodyPart.BodyPieceType.headerFieldsNot)
					{
						header.WriteTo(mimeWriter);
					}
				}
			}
			mimeWriter.EndPart();
			mimeWriter.Flush();
			stream.Flush();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		private static long GetExactMimeSize(ResponseFactory factory, int id, object mimeOptionsOrError, object mimeSizeOrError)
		{
			if (!(mimeSizeOrError is PropertyError) && !(mimeOptionsOrError is PropertyError) && (long)mimeOptionsOrError == Imap4RequestFetch.GetImapMIMEOptionsHash(factory, id))
			{
				return (long)mimeSizeOrError;
			}
			return -1L;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000F4EC File Offset: 0x0000D6EC
		private static long GetExactMimeSize(ResponseFactory factory, Item item, int id)
		{
			object mimeOptionsOrError = item.TryGetProperty(ItemSchema.ImapMIMEOptions);
			object mimeSizeOrError = item.TryGetProperty(ItemSchema.ImapMIMESize);
			return Imap4RequestFetch.GetExactMimeSize(factory, id, mimeOptionsOrError, mimeSizeOrError);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000F51A File Offset: 0x0000D71A
		private static ExDateTime GetInternalDate(Item item)
		{
			return Imap4RequestFetch.GetInternalDate(item.TryGetProperty(ItemSchema.ImapInternalDate), item.TryGetProperty(ItemSchema.ReceivedTime));
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000F538 File Offset: 0x0000D738
		private static ExDateTime GetInternalDate(object imapInternalDateOrError, object receivedTimeOrError)
		{
			PropertyError propertyError = imapInternalDateOrError as PropertyError;
			if (propertyError == null)
			{
				return (ExDateTime)imapInternalDateOrError;
			}
			if (receivedTimeOrError is PropertyError)
			{
				return ResponseFactory.CurrentExTimeZone.ConvertDateTime(ExDateTime.UtcNow);
			}
			return (ExDateTime)receivedTimeOrError;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000F574 File Offset: 0x0000D774
		private static long GetImapMIMEOptionsHash(ResponseFactory factory, int id)
		{
			return (long)(factory.GetOutboundConversionOptions().InternetTextFormat + id);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000F584 File Offset: 0x0000D784
		private OutboundConversionOptions GetOutboundConversionOptions()
		{
			OutboundConversionOptions outboundConversionOptions = base.Factory.GetOutboundConversionOptions();
			if (outboundConversionOptions.RecipientCache == null)
			{
				lock (outboundConversionOptions)
				{
					if (outboundConversionOptions.RecipientCache == null)
					{
						outboundConversionOptions.RecipientCache = new ADRecipientCache<ADRawEntry>(Imap4RequestFetch.supportedADProperties, 0, base.Factory.ProtocolUser.OrganizationId);
					}
				}
			}
			base.ResponseFactory.OkToResetRecipientCache = false;
			outboundConversionOptions.BlockPlainTextConversion = !base.Factory.AllowPlainTextConversionWithoutUsingSkeleton;
			outboundConversionOptions.UseSkeleton = !base.Factory.AllowPlainTextConversionWithoutUsingSkeleton;
			return outboundConversionOptions;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000F62C File Offset: 0x0000D82C
		private bool ProcessAllMessagesAtOnce(List<ProtocolMessage> messages, QueryResult itemQuery, Imap4RequestFetch.DataItems dataItems, bool useUids)
		{
			if (dataItems.LoadItem || dataItems.LoadItemBody || dataItems.SetSeenFlag)
			{
				throw new InvalidOperationException("Can't do content conversion here!");
			}
			if (dataItems.UseItemQuery && itemQuery == null)
			{
				throw new InvalidOperationException("itemQuery should be not null!");
			}
			if (!dataItems.UseItemQuery && itemQuery != null)
			{
				throw new InvalidOperationException("itemQuery should be null!");
			}
			object[][] array = null;
			int num = -1;
			SeekReference reference = SeekReference.OriginBeginning;
			OutboundConversionOptions options = null;
			List<ProtocolMessage> list = null;
			foreach (ProtocolMessage protocolMessage in messages)
			{
				if (dataItems.UseItemQuery)
				{
					if (array != null)
					{
						num++;
						while (num < array.Length && protocolMessage.Id != (int)array[num][0])
						{
							num++;
						}
						if (num >= array.Length)
						{
							array = null;
							num = -1;
						}
					}
					if (array == null)
					{
						QueryFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.ImapId, protocolMessage.Id);
						if (!itemQuery.SeekToCondition(reference, seekFilter, SeekToConditionFlags.None))
						{
							base.DeletedMessages = true;
							continue;
						}
						reference = SeekReference.OriginCurrent;
						array = itemQuery.GetRows(10000);
						if (array.Length == 0)
						{
							base.DeletedMessages = true;
							break;
						}
						num = 0;
					}
				}
				bool flag = false;
				bool flag2 = false;
				StringBuilder stringBuilder = new StringBuilder();
				Imap4RequestFetch.AppendFetchResponseLine(stringBuilder, protocolMessage.Index.ToString());
				foreach (Imap4RequestFetch.DataItem dataItem in dataItems.DataItemList)
				{
					Imap4RequestFetch.DataItemType dataItemType = dataItem.DataItemType;
					switch (dataItemType)
					{
					case Imap4RequestFetch.DataItemType.Uid:
						Imap4RequestFetch.AddSpace(stringBuilder);
						Imap4RequestFetch.AppendFetchResponseDataItem(stringBuilder, "UID", protocolMessage.Id.ToString());
						flag2 = true;
						continue;
					case Imap4RequestFetch.DataItemType.Flags:
						Imap4RequestFetch.AddSpace(stringBuilder);
						Imap4RequestFetch.AppendFetchResponseDataItem(stringBuilder, "FLAGS", Imap4FlagsHelper.ToString(((Imap4Message)protocolMessage).Flags));
						((Imap4Message)protocolMessage).FlagsHaveBeenChanged = false;
						continue;
					case Imap4RequestFetch.DataItemType.Bodystructure:
					case Imap4RequestFetch.DataItemType.Envelope:
						break;
					case Imap4RequestFetch.DataItemType.Internaldate:
					{
						ExDateTime internalDate = Imap4RequestFetch.GetInternalDate(array[num][3], array[num][4]);
						Imap4RequestFetch.AddSpace(stringBuilder);
						Imap4RequestFetch.AppendFetchResponseDataItemWithQuotes(stringBuilder, "INTERNALDATE", Rfc822Date.Format(internalDate));
						continue;
					}
					default:
						if (dataItemType == Imap4RequestFetch.DataItemType.Rfc822Size)
						{
							long num2;
							if (!base.Factory.ExactRFC822SizeEnabled)
							{
								num2 = (long)protocolMessage.Size;
							}
							else
							{
								num2 = Imap4RequestFetch.GetExactMimeSize(base.Factory, protocolMessage.Id, array[num][2], array[num][1]);
								if (num2 <= 0L)
								{
									if (list == null)
									{
										list = new List<ProtocolMessage>();
									}
									list.Add(protocolMessage);
									flag = true;
									continue;
								}
								if (list != null && list.Count > 0)
								{
									if (!this.SendNeedItemBindMessagesToClient(options, list, dataItems))
									{
										return false;
									}
									list = null;
								}
							}
							Imap4RequestFetch.AddSpace(stringBuilder);
							Imap4RequestFetch.AppendFetchResponseDataItem(stringBuilder, "RFC822.SIZE", num2.ToString());
							continue;
						}
						break;
					}
					throw new InvalidOperationException(string.Format("DataItem {0} not supported", dataItem.DataItemType));
				}
				if (!flag)
				{
					if (useUids && !flag2)
					{
						Imap4RequestFetch.AddSpace(stringBuilder);
						Imap4RequestFetch.AppendFetchResponseDataItem(stringBuilder, "UID", protocolMessage.Id.ToString());
					}
					stringBuilder.Append(")");
					StringResponseItem responseitem = new StringResponseItem(stringBuilder.ToString());
					if (!base.Session.SendToClient(responseitem))
					{
						return false;
					}
				}
			}
			return this.SendNeedItemBindMessagesToClient(options, list, dataItems);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
		private bool SendNeedItemBindMessagesToClient(OutboundConversionOptions options, List<ProtocolMessage> needItemBindMessages, Imap4RequestFetch.DataItems dataItems)
		{
			if (needItemBindMessages != null && needItemBindMessages.Count > 0)
			{
				dataItems.LoadItemBody = true;
				StoreObjectId[] storeObjectIds = base.Factory.SelectedMailbox.DataAccessView.GetStoreObjectIds(needItemBindMessages);
				if (options == null)
				{
					options = this.GetOutboundConversionOptions();
				}
				Imap4RequestFetch.MessageListResponseItem responseitem = new Imap4RequestFetch.MessageListResponseItem(needItemBindMessages, storeObjectIds, dataItems, options, base.UseUids);
				if (!base.Session.SendToClient(responseitem))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000182 RID: 386
		private const string FetchResponseCompleted = "[*] FETCH completed.";

		// Token: 0x04000183 RID: 387
		private const string FetchResponseLineEnd = ")";

		// Token: 0x04000184 RID: 388
		private const string FetchResponseBodyPartHeader = "HEADER";

		// Token: 0x04000185 RID: 389
		private const string FetchResponseBodyPartHeaderFields = "HEADER.FIELDS";

		// Token: 0x04000186 RID: 390
		private const string FetchResponseBodyPartHeaderFieldsNot = "HEADER.FIELDS.NOT";

		// Token: 0x04000187 RID: 391
		private const string FetchResponseBodyPartText = "TEXT";

		// Token: 0x04000188 RID: 392
		private const string FetchResponseBodyPartMime = "MIME";

		// Token: 0x04000189 RID: 393
		private const string FetchResponseFlags = "FLAGS";

		// Token: 0x0400018A RID: 394
		private const string FetchResponseBody = "BODY";

		// Token: 0x0400018B RID: 395
		private const string FetchResponseBodystructure = "BODYSTRUCTURE";

		// Token: 0x0400018C RID: 396
		private const string FetchResponseEnvelope = "ENVELOPE";

		// Token: 0x0400018D RID: 397
		private const string FetchResponseInternaldate = "INTERNALDATE";

		// Token: 0x0400018E RID: 398
		private const string FetchResponseRfc822 = "RFC822";

		// Token: 0x0400018F RID: 399
		private const string FetchResponseRfc822Header = "RFC822.HEADER";

		// Token: 0x04000190 RID: 400
		private const string FetchResponseRfc822Size = "RFC822.SIZE";

		// Token: 0x04000191 RID: 401
		private const string FetchResponseRfc822Text = "RFC822.TEXT";

		// Token: 0x04000192 RID: 402
		private const string FetchResponseUid = "UID";

		// Token: 0x04000193 RID: 403
		private static readonly SortBy[] SortByUid = new SortBy[]
		{
			new SortBy(ItemSchema.ImapId, SortOrder.Ascending)
		};

		// Token: 0x04000194 RID: 404
		private static readonly PropertyDefinition[] PropertiesToQuery = new PropertyDefinition[]
		{
			ItemSchema.ImapId,
			ItemSchema.ImapMIMESize,
			ItemSchema.ImapMIMEOptions,
			ItemSchema.ImapInternalDate,
			ItemSchema.ReceivedTime
		};

		// Token: 0x04000195 RID: 405
		private static ADPropertyDefinition[] supportedADProperties = new ADPropertyDefinition[ParticipantSchema.SupportedADProperties.Count];

		// Token: 0x04000196 RID: 406
		private bool delayedActionsEnqueued;

		// Token: 0x02000035 RID: 53
		internal enum DataItemType
		{
			// Token: 0x04000199 RID: 409
			Body,
			// Token: 0x0400019A RID: 410
			Uid,
			// Token: 0x0400019B RID: 411
			Flags,
			// Token: 0x0400019C RID: 412
			Bodystructure,
			// Token: 0x0400019D RID: 413
			Envelope,
			// Token: 0x0400019E RID: 414
			Internaldate,
			// Token: 0x0400019F RID: 415
			Rfc822,
			// Token: 0x040001A0 RID: 416
			Rfc822Header,
			// Token: 0x040001A1 RID: 417
			Rfc822Size,
			// Token: 0x040001A2 RID: 418
			Rfc822Text,
			// Token: 0x040001A3 RID: 419
			BodyPart
		}

		// Token: 0x02000036 RID: 54
		private struct QueryPropertiesIndex
		{
			// Token: 0x040001A4 RID: 420
			public const int ImapId = 0;

			// Token: 0x040001A5 RID: 421
			public const int ImapMIMESize = 1;

			// Token: 0x040001A6 RID: 422
			public const int ImapMIMEOptions = 2;

			// Token: 0x040001A7 RID: 423
			public const int ImapInternalDate = 3;

			// Token: 0x040001A8 RID: 424
			public const int ReceivedTime = 4;
		}

		// Token: 0x02000037 RID: 55
		private class MessageListResponseItem : IResponseItem, IDisposeTrackable, IDisposable
		{
			// Token: 0x06000224 RID: 548 RVA: 0x0000FA34 File Offset: 0x0000DC34
			public MessageListResponseItem(List<ProtocolMessage> messages, StoreObjectId[] storeObjectIds, Imap4RequestFetch.DataItems dataItems, OutboundConversionOptions options, bool useUids)
			{
				this.disposeTracker = this.GetDisposeTracker();
				this.messageList = messages;
				this.storeObjectIds = storeObjectIds;
				this.dataItems = dataItems;
				this.messageDataBeingProcessed = new Imap4RequestFetch.MessageStateData(options, dataItems, useUids);
				this.currentMessageIndex = 0;
				this.itemBeingProcessed = new DataProcessResponseItem();
				this.BindItemBeingProcessedToData();
			}

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x06000225 RID: 549 RVA: 0x0000FA90 File Offset: 0x0000DC90
			public BaseSession.SendCompleteDelegate SendCompleteDelegate
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06000226 RID: 550 RVA: 0x0000FA94 File Offset: 0x0000DC94
			public int GetNextChunk(BaseSession session, out byte[] buffer, out int offset)
			{
				buffer = null;
				offset = 0;
				if (this.messageDataBeingProcessed.ShouldReturn)
				{
					return 0;
				}
				int nextChunk = this.itemBeingProcessed.GetNextChunk(session, out buffer, out offset);
				if (nextChunk == 0)
				{
					if (this.currentMessageIndex < this.messageList.Count)
					{
						this.BindItemBeingProcessedToData();
						return this.GetNextChunk(session, out buffer, out offset);
					}
					ResponseFactory responseFactory = ((ProtocolSession)session).ResponseFactory;
					if (!session.Disconnected && responseFactory != null)
					{
						responseFactory.OkToResetRecipientCache = true;
					}
				}
				return nextChunk;
			}

			// Token: 0x06000227 RID: 551 RVA: 0x0000FB0A File Offset: 0x0000DD0A
			public virtual DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<Imap4RequestFetch.MessageListResponseItem>(this);
			}

			// Token: 0x06000228 RID: 552 RVA: 0x0000FB12 File Offset: 0x0000DD12
			public void SuppressDisposeTracker()
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Suppress();
					this.disposeTracker = null;
				}
			}

			// Token: 0x06000229 RID: 553 RVA: 0x0000FB2E File Offset: 0x0000DD2E
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x0600022A RID: 554 RVA: 0x0000FB40 File Offset: 0x0000DD40
			protected virtual void Dispose(bool disposing)
			{
				if (disposing)
				{
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
					if (this.itemBeingProcessed != null)
					{
						this.itemBeingProcessed.Dispose();
						this.itemBeingProcessed = null;
					}
					if (this.messageDataBeingProcessed != null)
					{
						this.messageDataBeingProcessed.Dispose();
						this.messageDataBeingProcessed = null;
					}
				}
			}

			// Token: 0x0600022B RID: 555 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
			private void BindItemBeingProcessedToData()
			{
				ProtocolMessage message = this.messageList[this.currentMessageIndex];
				StoreObjectId storeObjectId = (this.storeObjectIds != null) ? this.storeObjectIds[this.currentMessageIndex] : null;
				StoreObjectId[] prereadIds = null;
				if (this.storeObjectIds != null && this.currentMessageIndex % 50 == 0 && this.currentMessageIndex < this.messageList.Count - 1)
				{
					int num = this.messageList.Count - this.currentMessageIndex;
					if (num > 50)
					{
						num = 50;
					}
					List<StoreObjectId> list = new List<StoreObjectId>(num);
					for (int i = this.currentMessageIndex; i < this.currentMessageIndex + num; i++)
					{
						StoreObjectId storeObjectId2 = this.storeObjectIds[i];
						if (storeObjectId2 != null)
						{
							list.Add(storeObjectId2);
						}
					}
					prereadIds = list.ToArray();
				}
				this.messageDataBeingProcessed.SetData(message, storeObjectId, prereadIds);
				this.currentMessageIndex++;
				this.itemBeingProcessed.BindData(this.messageDataBeingProcessed, Imap4RequestFetch.MessageListResponseItem.processMessageDelegate, this.dataItems.LoadItem || this.dataItems.LoadItemBody);
			}

			// Token: 0x040001A9 RID: 425
			private const int PrereadMessageCap = 50;

			// Token: 0x040001AA RID: 426
			private static Action<ProtocolSession, DataProcessResponseItem> processMessageDelegate = new Action<ProtocolSession, DataProcessResponseItem>(Imap4RequestFetch.ProcessMessage);

			// Token: 0x040001AB RID: 427
			private DataProcessResponseItem itemBeingProcessed;

			// Token: 0x040001AC RID: 428
			private Imap4RequestFetch.MessageStateData messageDataBeingProcessed;

			// Token: 0x040001AD RID: 429
			private List<ProtocolMessage> messageList;

			// Token: 0x040001AE RID: 430
			private StoreObjectId[] storeObjectIds;

			// Token: 0x040001AF RID: 431
			private Imap4RequestFetch.DataItems dataItems;

			// Token: 0x040001B0 RID: 432
			private int currentMessageIndex;

			// Token: 0x040001B1 RID: 433
			private DisposeTracker disposeTracker;
		}

		// Token: 0x02000038 RID: 56
		private class MessageStateData : IDisposeTrackable, IDisposable
		{
			// Token: 0x0600022D RID: 557 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
			public MessageStateData(OutboundConversionOptions options, Imap4RequestFetch.DataItems dataItems, bool useUids)
			{
				this.disposeTracker = this.GetDisposeTracker();
				this.options = options;
				this.dataItems = dataItems;
				this.useUids = useUids;
				this.lineBuilder = new StringBuilder(256);
				this.lineResponseItem = new StringResponseItem();
			}

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x0600022E RID: 558 RVA: 0x0000FD13 File Offset: 0x0000DF13
			public Imap4RequestFetch.DataItems DataItems
			{
				get
				{
					return this.dataItems;
				}
			}

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x0600022F RID: 559 RVA: 0x0000FD1B File Offset: 0x0000DF1B
			public OutboundConversionOptions ConversionOptions
			{
				get
				{
					return this.options;
				}
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x06000230 RID: 560 RVA: 0x0000FD23 File Offset: 0x0000DF23
			public ProtocolMessage Message
			{
				get
				{
					return this.message;
				}
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x06000231 RID: 561 RVA: 0x0000FD2B File Offset: 0x0000DF2B
			public StoreObjectId StoreObjectId
			{
				get
				{
					return this.storeObjectId;
				}
			}

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x06000232 RID: 562 RVA: 0x0000FD33 File Offset: 0x0000DF33
			public bool UseUids
			{
				get
				{
					return this.useUids;
				}
			}

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x06000233 RID: 563 RVA: 0x0000FD3B File Offset: 0x0000DF3B
			public StringBuilder LineBuilder
			{
				get
				{
					return this.lineBuilder;
				}
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x06000234 RID: 564 RVA: 0x0000FD43 File Offset: 0x0000DF43
			public StringResponseItem LineResponseItem
			{
				get
				{
					return this.lineResponseItem;
				}
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x06000235 RID: 565 RVA: 0x0000FD4B File Offset: 0x0000DF4B
			// (set) Token: 0x06000236 RID: 566 RVA: 0x0000FD53 File Offset: 0x0000DF53
			public int BackoffDelay
			{
				get
				{
					return this.backoffDelay;
				}
				set
				{
					this.backoffDelay = value;
				}
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000237 RID: 567 RVA: 0x0000FD5C File Offset: 0x0000DF5C
			// (set) Token: 0x06000238 RID: 568 RVA: 0x0000FD64 File Offset: 0x0000DF64
			public bool ShouldReturn
			{
				get
				{
					return this.shouldReturn;
				}
				set
				{
					this.shouldReturn = value;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000239 RID: 569 RVA: 0x0000FD6D File Offset: 0x0000DF6D
			// (set) Token: 0x0600023A RID: 570 RVA: 0x0000FD75 File Offset: 0x0000DF75
			public StoreObjectId[] PrereadIds
			{
				get
				{
					return this.prereadIds;
				}
				set
				{
					this.prereadIds = value;
				}
			}

			// Token: 0x0600023B RID: 571 RVA: 0x0000FD7E File Offset: 0x0000DF7E
			public void SetData(ProtocolMessage message, StoreObjectId storeObjectId, StoreObjectId[] prereadIds)
			{
				this.message = message;
				this.storeObjectId = storeObjectId;
				this.prereadIds = prereadIds;
			}

			// Token: 0x0600023C RID: 572 RVA: 0x0000FD95 File Offset: 0x0000DF95
			public virtual DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<Imap4RequestFetch.MessageStateData>(this);
			}

			// Token: 0x0600023D RID: 573 RVA: 0x0000FD9D File Offset: 0x0000DF9D
			public void SuppressDisposeTracker()
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Suppress();
					this.disposeTracker = null;
				}
			}

			// Token: 0x0600023E RID: 574 RVA: 0x0000FDB9 File Offset: 0x0000DFB9
			public void Dispose()
			{
				if (this.lineResponseItem != null)
				{
					this.lineResponseItem.Dispose();
					this.lineResponseItem = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
			}

			// Token: 0x040001B2 RID: 434
			private Imap4RequestFetch.DataItems dataItems;

			// Token: 0x040001B3 RID: 435
			private OutboundConversionOptions options;

			// Token: 0x040001B4 RID: 436
			private ProtocolMessage message;

			// Token: 0x040001B5 RID: 437
			private StoreObjectId storeObjectId;

			// Token: 0x040001B6 RID: 438
			private bool useUids;

			// Token: 0x040001B7 RID: 439
			private int backoffDelay;

			// Token: 0x040001B8 RID: 440
			private bool shouldReturn;

			// Token: 0x040001B9 RID: 441
			private StringBuilder lineBuilder;

			// Token: 0x040001BA RID: 442
			private StringResponseItem lineResponseItem;

			// Token: 0x040001BB RID: 443
			private DisposeTracker disposeTracker;

			// Token: 0x040001BC RID: 444
			private StoreObjectId[] prereadIds;
		}

		// Token: 0x02000039 RID: 57
		private class BodyPart
		{
			// Token: 0x0600023F RID: 575 RVA: 0x0000FDEF File Offset: 0x0000DFEF
			internal BodyPart()
			{
				this.BodyPiece = Imap4RequestFetch.BodyPart.BodyPieceType.all;
				this.Length = -1;
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000240 RID: 576 RVA: 0x0000FE05 File Offset: 0x0000E005
			// (set) Token: 0x06000241 RID: 577 RVA: 0x0000FE0D File Offset: 0x0000E00D
			internal uint[] Section { get; set; }

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000242 RID: 578 RVA: 0x0000FE16 File Offset: 0x0000E016
			// (set) Token: 0x06000243 RID: 579 RVA: 0x0000FE1E File Offset: 0x0000E01E
			internal Imap4RequestFetch.BodyPart.BodyPieceType BodyPiece { get; set; }

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000244 RID: 580 RVA: 0x0000FE27 File Offset: 0x0000E027
			// (set) Token: 0x06000245 RID: 581 RVA: 0x0000FE2F File Offset: 0x0000E02F
			internal List<string> FieldNames { get; set; }

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000246 RID: 582 RVA: 0x0000FE38 File Offset: 0x0000E038
			// (set) Token: 0x06000247 RID: 583 RVA: 0x0000FE40 File Offset: 0x0000E040
			internal int Offset { get; set; }

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000248 RID: 584 RVA: 0x0000FE49 File Offset: 0x0000E049
			// (set) Token: 0x06000249 RID: 585 RVA: 0x0000FE51 File Offset: 0x0000E051
			internal int Length { get; set; }

			// Token: 0x0600024A RID: 586 RVA: 0x0000FE5C File Offset: 0x0000E05C
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				stringBuilder.Append("BODY");
				stringBuilder.Append("[");
				if (this.Section != null)
				{
					foreach (int num in this.Section)
					{
						stringBuilder.Append(num.ToString());
						stringBuilder.Append(".");
					}
				}
				switch (this.BodyPiece)
				{
				case Imap4RequestFetch.BodyPart.BodyPieceType.all:
					if (stringBuilder[stringBuilder.Length - 1] == '.')
					{
						stringBuilder.Remove(stringBuilder.Length - 1, 1);
					}
					break;
				case Imap4RequestFetch.BodyPart.BodyPieceType.header:
					stringBuilder.Append("HEADER");
					break;
				case Imap4RequestFetch.BodyPart.BodyPieceType.headerFields:
					stringBuilder.Append("HEADER.FIELDS");
					break;
				case Imap4RequestFetch.BodyPart.BodyPieceType.headerFieldsNot:
					stringBuilder.Append("HEADER.FIELDS.NOT");
					break;
				case Imap4RequestFetch.BodyPart.BodyPieceType.text:
					stringBuilder.Append("TEXT");
					break;
				case Imap4RequestFetch.BodyPart.BodyPieceType.mime:
					stringBuilder.Append("MIME");
					break;
				}
				if (this.FieldNames != null)
				{
					StringBuilder stringBuilder2 = new StringBuilder(128);
					foreach (string text in this.FieldNames)
					{
						if (stringBuilder2.Length > 0)
						{
							stringBuilder2.Append(" ");
						}
						if (text.IndexOf(' ') == -1)
						{
							stringBuilder2.Append(text);
						}
						else
						{
							stringBuilder2.AppendFormat("\"{0}\"", text);
						}
					}
					stringBuilder.AppendFormat(" ({0})", stringBuilder2);
				}
				stringBuilder.Append("]");
				if (this.Length != -1)
				{
					stringBuilder.AppendFormat("<{0}>", this.Offset);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x0600024B RID: 587 RVA: 0x0001002C File Offset: 0x0000E22C
			internal static Imap4RequestFetch.BodyPart Parse(string bodyPartString)
			{
				return Imap4RequestFetch.BodyPart.Parse(bodyPartString, 0, -1);
			}

			// Token: 0x0600024C RID: 588 RVA: 0x00010038 File Offset: 0x0000E238
			internal static Imap4RequestFetch.BodyPart Parse(string bodyPartString, int offset, int length)
			{
				Imap4RequestFetch.BodyPart bodyPart = new Imap4RequestFetch.BodyPart();
				bodyPart.Offset = offset;
				bodyPart.Length = length;
				Match match = Imap4RequestFetch.BodyPart.sectionRegEx.Match(bodyPartString);
				if (!match.Success)
				{
					return null;
				}
				if (match.Groups["section"].Value.Length > 0)
				{
					bodyPart.Section = Imap4RequestFetch.BodyPart.ParseSection(match.Groups["section"].Value);
					if (bodyPart.Section == null)
					{
						return null;
					}
				}
				string a;
				if ((a = match.Groups["bodyPiece"].Value.ToUpper()) != null)
				{
					if (!(a == "HEADER"))
					{
						if (a == "TEXT")
						{
							bodyPart.BodyPiece = Imap4RequestFetch.BodyPart.BodyPieceType.text;
							return bodyPart;
						}
						if (a == "MIME")
						{
							bodyPart.BodyPiece = Imap4RequestFetch.BodyPart.BodyPieceType.mime;
							if (bodyPart.Section.Length == 0)
							{
								return null;
							}
							return bodyPart;
						}
					}
					else
					{
						if (match.Groups["fields"].Value.Length == 0)
						{
							bodyPart.BodyPiece = Imap4RequestFetch.BodyPart.BodyPieceType.header;
							return bodyPart;
						}
						bodyPart.FieldNames = Imap4RequestFetch.BodyPart.ParseFieldNames(match.Groups["value"].Value);
						if (bodyPart.FieldNames == null)
						{
							return null;
						}
						if (match.Groups["not"].Value.Length == 0)
						{
							bodyPart.BodyPiece = Imap4RequestFetch.BodyPart.BodyPieceType.headerFields;
							return bodyPart;
						}
						bodyPart.BodyPiece = Imap4RequestFetch.BodyPart.BodyPieceType.headerFieldsNot;
						return bodyPart;
					}
				}
				bodyPart.BodyPiece = Imap4RequestFetch.BodyPart.BodyPieceType.all;
				return bodyPart;
			}

			// Token: 0x0600024D RID: 589 RVA: 0x000101AC File Offset: 0x0000E3AC
			private static uint[] ParseSection(string sectionString)
			{
				List<uint> list = new List<uint>();
				uint num;
				for (int i = sectionString.IndexOf('.'); i > 0; i = sectionString.IndexOf('.'))
				{
					if (!uint.TryParse(sectionString.Substring(0, i), out num) || num < 1U)
					{
						return null;
					}
					list.Add(num);
					sectionString = sectionString.Substring(i + 1);
				}
				if (!uint.TryParse(sectionString, out num) || num < 1U)
				{
					return null;
				}
				list.Add(num);
				return list.ToArray();
			}

			// Token: 0x0600024E RID: 590 RVA: 0x00010220 File Offset: 0x0000E420
			private static List<string> ParseFieldNames(string fieldNamesString)
			{
				List<string> list = new List<string>();
				Match match = Imap4RequestFetch.BodyPart.fieldNamesRegEx.Match(fieldNamesString);
				if (!match.Success)
				{
					return null;
				}
				while (match.Success)
				{
					list.Add(match.Groups["fieldName"].Value);
					if (match.Groups["eol"].Value.Length != 0 && !match.NextMatch().Success)
					{
						return null;
					}
					match = match.NextMatch();
				}
				return list;
			}

			// Token: 0x040001BD RID: 445
			private static readonly Regex fieldNamesRegEx = new Regex("\\G((?<fieldName>[^ \"]+)|\"(?<fieldName>[^\"]+)\")(?<eol> |$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

			// Token: 0x040001BE RID: 446
			private static readonly Regex sectionRegEx = new Regex("\\A\\[((?<section>\\d+(\\.\\d+)*)?(((?<=\\d)\\.)?)(((?<bodyPiece>header)((?<fields>.fields)((?<not>.not))? \\((?<value>[^\\(\\)]+)\\))?)|(?<bodyPiece>text)|((?<=\\.)(?<bodyPiece>mime)))?)\\]\\z", RegexOptions.IgnoreCase | RegexOptions.Compiled);

			// Token: 0x0200003A RID: 58
			internal enum BodyPieceType
			{
				// Token: 0x040001C5 RID: 453
				all,
				// Token: 0x040001C6 RID: 454
				header,
				// Token: 0x040001C7 RID: 455
				headerFields,
				// Token: 0x040001C8 RID: 456
				headerFieldsNot,
				// Token: 0x040001C9 RID: 457
				text,
				// Token: 0x040001CA RID: 458
				mime
			}
		}

		// Token: 0x0200003B RID: 59
		private class DataItem
		{
			// Token: 0x06000250 RID: 592 RVA: 0x000102C3 File Offset: 0x0000E4C3
			internal DataItem(Imap4RequestFetch.DataItemType dataItemType)
			{
				this.DataItemType = dataItemType;
			}

			// Token: 0x06000251 RID: 593 RVA: 0x000102D2 File Offset: 0x0000E4D2
			internal DataItem(Imap4RequestFetch.BodyPart bodyPart)
			{
				this.DataItemType = Imap4RequestFetch.DataItemType.BodyPart;
				this.BodyPart = bodyPart;
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000252 RID: 594 RVA: 0x000102E9 File Offset: 0x0000E4E9
			// (set) Token: 0x06000253 RID: 595 RVA: 0x000102F1 File Offset: 0x0000E4F1
			internal Imap4RequestFetch.DataItemType DataItemType { get; set; }

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000254 RID: 596 RVA: 0x000102FA File Offset: 0x0000E4FA
			// (set) Token: 0x06000255 RID: 597 RVA: 0x00010302 File Offset: 0x0000E502
			internal Imap4RequestFetch.BodyPart BodyPart { get; set; }

			// Token: 0x06000256 RID: 598 RVA: 0x0001030B File Offset: 0x0000E50B
			public override string ToString()
			{
				if (this.BodyPart != null)
				{
					return this.BodyPart.ToString();
				}
				return this.DataItemType.ToString();
			}
		}

		// Token: 0x0200003C RID: 60
		private class DataItems
		{
			// Token: 0x06000257 RID: 599 RVA: 0x00010331 File Offset: 0x0000E531
			protected DataItems()
			{
				this.DataItemList = new Queue<Imap4RequestFetch.DataItem>();
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000258 RID: 600 RVA: 0x00010344 File Offset: 0x0000E544
			// (set) Token: 0x06000259 RID: 601 RVA: 0x0001034C File Offset: 0x0000E54C
			internal bool UseItemQuery { get; set; }

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x0600025A RID: 602 RVA: 0x00010355 File Offset: 0x0000E555
			// (set) Token: 0x0600025B RID: 603 RVA: 0x0001035D File Offset: 0x0000E55D
			internal bool LoadItem { get; set; }

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x0600025C RID: 604 RVA: 0x00010366 File Offset: 0x0000E566
			// (set) Token: 0x0600025D RID: 605 RVA: 0x0001036E File Offset: 0x0000E56E
			internal bool LoadItemBody { get; set; }

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x0600025E RID: 606 RVA: 0x00010377 File Offset: 0x0000E577
			// (set) Token: 0x0600025F RID: 607 RVA: 0x0001037F File Offset: 0x0000E57F
			internal bool SetSeenFlag { get; set; }

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06000260 RID: 608 RVA: 0x00010388 File Offset: 0x0000E588
			// (set) Token: 0x06000261 RID: 609 RVA: 0x00010390 File Offset: 0x0000E590
			internal Queue<Imap4RequestFetch.DataItem> DataItemList { get; set; }

			// Token: 0x06000262 RID: 610 RVA: 0x0001039C File Offset: 0x0000E59C
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(128);
				foreach (Imap4RequestFetch.DataItem dataItem in this.DataItemList)
				{
					stringBuilder.AppendFormat("\r\n\t{0}", dataItem.ToString());
				}
				return string.Format("LoadItem {0}\r\nfLoadItemBody {1}\r\nfUseItemQuery {2}\r\nfSetSeenFlag {3}\r\nDataItems:{4}", new object[]
				{
					this.LoadItem,
					this.LoadItemBody,
					this.UseItemQuery,
					this.SetSeenFlag,
					stringBuilder.ToString()
				});
			}

			// Token: 0x06000263 RID: 611 RVA: 0x00010458 File Offset: 0x0000E658
			internal static Imap4RequestFetch.DataItems Parse(string dataItemsString, bool exactRFC822SizeEnabled)
			{
				Imap4RequestFetch.DataItems dataItems = new Imap4RequestFetch.DataItems();
				if (dataItemsString.Length > 1 && dataItemsString[0] == '(' && dataItemsString[dataItemsString.Length - 1] == ')')
				{
					dataItemsString = dataItemsString.Substring(1, dataItemsString.Length - 2);
				}
				Match match = Imap4RequestFetch.DataItems.dataItemsRegEx.Match(dataItemsString);
				if (!match.Success)
				{
					return null;
				}
				while (match.Success)
				{
					string key;
					if ((key = match.Groups["dataItem"].Value.ToUpper()) != null)
					{
						if (<PrivateImplementationDetails>{80A8642A-8216-49A9-BE23-1E907903ED64}.$$method0x600024a-1 == null)
						{
							<PrivateImplementationDetails>{80A8642A-8216-49A9-BE23-1E907903ED64}.$$method0x600024a-1 = new Dictionary<string, int>(13)
							{
								{
									"BODY",
									0
								},
								{
									"UID",
									1
								},
								{
									"FLAGS",
									2
								},
								{
									"BODYSTRUCTURE",
									3
								},
								{
									"ENVELOPE",
									4
								},
								{
									"INTERNALDATE",
									5
								},
								{
									"RFC822",
									6
								},
								{
									"RFC822.HEADER",
									7
								},
								{
									"RFC822.SIZE",
									8
								},
								{
									"RFC822.TEXT",
									9
								},
								{
									"FAST",
									10
								},
								{
									"ALL",
									11
								},
								{
									"FULL",
									12
								}
							};
						}
						int num;
						if (<PrivateImplementationDetails>{80A8642A-8216-49A9-BE23-1E907903ED64}.$$method0x600024a-1.TryGetValue(key, out num))
						{
							switch (num)
							{
							case 0:
							{
								bool flag = match.Groups["peek"].Value.Length > 0;
								if (match.Groups["section"].Value.Length == 0)
								{
									if (flag)
									{
										return null;
									}
									dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Body));
									dataItems.LoadItem = true;
								}
								else
								{
									dataItems.SetSeenFlag |= !flag;
									Imap4RequestFetch.BodyPart bodyPart;
									if (match.Groups["offset"].Value.Length > 0)
									{
										int num2;
										if (!int.TryParse(match.Groups["offset"].Value, out num2) || num2 < 0)
										{
											return null;
										}
										int num3;
										if (!int.TryParse(match.Groups["length"].Value, out num3) || num3 < 0)
										{
											return null;
										}
										bodyPart = Imap4RequestFetch.BodyPart.Parse(match.Groups["section"].Value, num2, num3);
										if (bodyPart == null)
										{
											return null;
										}
										dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(bodyPart));
									}
									else
									{
										bodyPart = Imap4RequestFetch.BodyPart.Parse(match.Groups["section"].Value);
										if (bodyPart == null)
										{
											return null;
										}
										dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(bodyPart));
									}
									if (bodyPart != null)
									{
										if (bodyPart.BodyPiece == Imap4RequestFetch.BodyPart.BodyPieceType.header || bodyPart.BodyPiece == Imap4RequestFetch.BodyPart.BodyPieceType.headerFields || bodyPart.BodyPiece == Imap4RequestFetch.BodyPart.BodyPieceType.headerFieldsNot)
										{
											dataItems.LoadItem = true;
										}
										else
										{
											dataItems.LoadItemBody = true;
										}
									}
								}
								break;
							}
							case 1:
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Uid));
								break;
							case 2:
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Flags));
								break;
							case 3:
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Bodystructure));
								dataItems.LoadItem = true;
								break;
							case 4:
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Envelope));
								dataItems.LoadItem = true;
								break;
							case 5:
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Internaldate));
								dataItems.UseItemQuery = true;
								break;
							case 6:
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Rfc822));
								dataItems.SetSeenFlag = true;
								dataItems.LoadItemBody = true;
								break;
							case 7:
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Rfc822Header));
								dataItems.LoadItem = true;
								break;
							case 8:
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Rfc822Size));
								if (exactRFC822SizeEnabled)
								{
									dataItems.UseItemQuery = true;
								}
								break;
							case 9:
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Rfc822Text));
								dataItems.SetSeenFlag = true;
								dataItems.LoadItemBody = true;
								break;
							case 10:
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Flags));
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Internaldate));
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Rfc822Size));
								dataItems.UseItemQuery = true;
								if (match.NextMatch().Success)
								{
									return null;
								}
								break;
							case 11:
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Flags));
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Internaldate));
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Rfc822Size));
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Envelope));
								dataItems.LoadItem = true;
								if (match.NextMatch().Success)
								{
									return null;
								}
								break;
							case 12:
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Flags));
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Internaldate));
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Rfc822Size));
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Envelope));
								dataItems.DataItemList.Enqueue(new Imap4RequestFetch.DataItem(Imap4RequestFetch.DataItemType.Body));
								dataItems.LoadItem = true;
								if (match.NextMatch().Success)
								{
									return null;
								}
								break;
							default:
								goto IL_51E;
							}
							if (match.Groups["eol"].Value.Length != 0 && !match.NextMatch().Success)
							{
								return null;
							}
							match = match.NextMatch();
							continue;
						}
					}
					IL_51E:
					return null;
				}
				return dataItems;
			}

			// Token: 0x040001CD RID: 461
			private static readonly Regex dataItemsRegEx = new Regex("\\G((?<dataItem>bodystructure)|((?<dataItem>body)(?<peek>\\.peek)?(?<section>\\[[^\\[\\]]*\\])?)(\\<(?<offset>\\d+).(?<length>\\d+)\\>)?|(?<dataItem>[\\w\\.]+))+(?<eol> |$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		}
	}
}
