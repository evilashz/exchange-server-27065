using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Conversations;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Conversations
{
	// Token: 0x02000023 RID: 35
	internal static class BodyTagProcessor
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00005D25 File Offset: 0x00003F25
		internal static bool IsEventInteresting(MapiEvent mapiEvent)
		{
			return (mapiEvent.EventMask & MapiEventTypeFlags.MailboxMoveSucceeded) != (MapiEventTypeFlags)0;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005D38 File Offset: 0x00003F38
		internal static void HandleEventInternal(MailboxSession session, StoreObject storeItem)
		{
			BodyTagProcessor.Tracer.TraceDebug(0L, "{0}: Calling BodyTagProcessor.HandleEventInternal", new object[]
			{
				TraceContext.Get()
			});
			if (session.MailboxOwner == null)
			{
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_BodyTagProcessingFailed, null, new object[]
				{
					string.IsNullOrEmpty(session.MailboxOwnerLegacyDN) ? string.Empty : session.MailboxOwnerLegacyDN,
					"MailboxOwner returned null for this session",
					"<null>"
				});
				return;
			}
			bool flag = false;
			string text = null;
			if (session.MailboxOwner.RecipientType != RecipientType.UserMailbox)
			{
				text = "Skipping System/System Attendant/Unknown Mailbox ";
				flag = true;
			}
			else if (session.MailboxOwner.MailboxInfo.IsAggregated)
			{
				text = "Aggregated Mailbox";
				flag = true;
			}
			if (flag)
			{
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_BodyTagProcessingSkipped, null, new object[]
				{
					session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
					text
				});
				return;
			}
			BodyTagProcessor.MessageProcessor state = new BodyTagProcessor.MessageProcessor(session.MailboxOwner);
			using (ActivityContext.SuppressThreadScope())
			{
				BodyTagProcessor.throttle.QueueUserWorkItem(BodyTagProcessor.processorCallback, state);
			}
			Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_BodyTagProcessingRequestQueued, null, new object[]
			{
				session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
			});
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005EBC File Offset: 0x000040BC
		private static void ProcessBodyTag(object state)
		{
			((BodyTagProcessor.MessageProcessor)state).ProcessBodyTag();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005ECC File Offset: 0x000040CC
		public static T GetProperty<T>(IStorePropertyBag propertyBag, PropertyDefinition propertyDefinition, T defaultValue)
		{
			object obj = propertyBag.TryGetProperty(propertyDefinition);
			if (obj is PropertyError || obj == null)
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x0400010E RID: 270
		private const int ThrottleValue = 4;

		// Token: 0x0400010F RID: 271
		private static readonly Trace Tracer = ExTraceGlobals.GeneralTracer;

		// Token: 0x04000110 RID: 272
		private static Throttle throttle = new Throttle("BodyTagProcessor", 4);

		// Token: 0x04000111 RID: 273
		private static WaitCallback processorCallback = new WaitCallback(BodyTagProcessor.ProcessBodyTag);

		// Token: 0x02000024 RID: 36
		internal class MessageProcessor
		{
			// Token: 0x06000109 RID: 265 RVA: 0x00005F21 File Offset: 0x00004121
			public MessageProcessor(IExchangePrincipal owner)
			{
				this.principal = owner;
			}

			// Token: 0x0600010A RID: 266 RVA: 0x0000677A File Offset: 0x0000497A
			public void ProcessBodyTag()
			{
				ExWatson.SendReportOnUnhandledException(delegate()
				{
					List<IStorePropertyBag> propertyBagList = new List<IStorePropertyBag>(100);
					ExDateTime toProcessDate = ExDateTime.UtcNow.Subtract(TimeSpan.FromDays(15.0));
					int indexTrackingCounter = 0;
					bool trackingCheckRequired = true;
					string text = null;
					string text2 = null;
					string text3 = null;
					bool flag = false;
					try
					{
						using (MailboxSession session = MailboxSession.OpenAsAdmin(this.principal, CultureInfo.InvariantCulture, "Client=TBA;Action=Conversation Assistant"))
						{
							text = session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
							AllItemsFolderHelper.RunQueryOnAllItemsFolder<bool>(session, AllItemsFolderHelper.SupportedSortBy.ReceivedTime, delegate(QueryResult queryResults)
							{
								bool flag2 = false;
								int num = 0;
								while (!flag2)
								{
									IStorePropertyBag[] propertyBags = queryResults.GetPropertyBags(50);
									if (propertyBags.Length <= 0)
									{
										break;
									}
									foreach (IStorePropertyBag storePropertyBag2 in propertyBags)
									{
										num++;
										if (num >= 10000)
										{
											BodyTagProcessor.Tracer.TraceDebug<string>(0L, "Skip querying as we have reached our maximum limit of emails we want to process for {0}", session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
											flag2 = true;
											break;
										}
										string text5 = storePropertyBag2.TryGetProperty(StoreObjectSchema.ItemClass) as string;
										if (!string.IsNullOrEmpty(text5) && ObjectClass.IsMessage(text5, false))
										{
											if (!(BodyTagProcessor.GetProperty<ExDateTime>(storePropertyBag2, ItemSchema.ReceivedTime, ExDateTime.MinValue) >= toProcessDate))
											{
												flag2 = true;
												break;
											}
											if (trackingCheckRequired && !(storePropertyBag2.TryGetProperty(ItemSchema.ConversationIndexTracking) is PropertyError))
											{
												indexTrackingCounter++;
												if (indexTrackingCounter >= 10)
												{
													BodyTagProcessor.Tracer.TraceDebug<string>(0L, "Not processing BodyTag for mailbox {0}", session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
													flag2 = true;
													break;
												}
											}
											if (storePropertyBag2.TryGetProperty(ItemSchema.BodyTag) is PropertyError || storePropertyBag2.TryGetProperty(ItemSchema.BodyTag) == null)
											{
												propertyBagList.Add(storePropertyBag2);
											}
										}
									}
									trackingCheckRequired = false;
								}
								return true;
							}, new PropertyDefinition[]
							{
								ItemSchema.Id,
								StoreObjectSchema.ItemClass,
								ItemSchema.ReceivedTime,
								ItemSchema.BodyTag,
								ItemSchema.ConversationIndexTracking
							});
							if (propertyBagList.Count == 0 || indexTrackingCounter >= 10)
							{
								string text4 = (propertyBagList.Count == 0) ? " 0 messages to process" : " IndexTrackingCounter reached its limit";
								Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_BodyTagProcessingSkipped, null, new object[]
								{
									text,
									text4
								});
								return;
							}
							Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_TotalNumberOfItemsForBodyTagProcessing, null, new object[]
							{
								propertyBagList.Count,
								text
							});
							foreach (IStorePropertyBag storePropertyBag in propertyBagList)
							{
								try
								{
									using (MessageItem messageItem = MessageItem.Bind(session, storePropertyBag[ItemSchema.Id] as StoreId))
									{
										if (!(messageItem is RightsManagedMessageItem))
										{
											messageItem.OpenAsReadWrite();
											messageItem[ItemSchema.BodyTag] = messageItem.Body.CalculateBodyTag();
											messageItem.Save(SaveMode.ResolveConflicts);
										}
									}
								}
								catch (ObjectNotFoundException)
								{
									BodyTagProcessor.Tracer.TraceDebug<object, string>(0L, "ObjectNotFound exception thrown while processing item - {0} in mailbox - {1}", storePropertyBag[ItemSchema.Id], text);
								}
								catch (AccessDeniedException)
								{
									BodyTagProcessor.Tracer.TraceDebug<object, string>(0L, "AccessDenied exception thrown while processing item - {0} in mailbox - {1}", storePropertyBag[ItemSchema.Id], text);
								}
								catch (CorruptDataException)
								{
									BodyTagProcessor.Tracer.TraceDebug<object, string>(0L, "Corrupt data exception thrown while processing item - {0} in mailbox - {1}", storePropertyBag[ItemSchema.Id], text);
								}
								catch (VirusException)
								{
									BodyTagProcessor.Tracer.TraceDebug<object, string>(0L, "Virus exception thrown while processing item - {0} in mailbox - {1}", storePropertyBag[ItemSchema.Id], text);
								}
								catch (PropertyErrorException)
								{
									BodyTagProcessor.Tracer.TraceDebug<object, string>(0L, "PropertyErrorException thrown while processing item - {0} in mailbox - {1}", storePropertyBag[ItemSchema.Id], text);
								}
								catch (StoragePermanentException ex)
								{
									if (!(ex.InnerException is MapiExceptionCallFailed))
									{
										throw;
									}
									BodyTagProcessor.Tracer.TraceDebug<object, string>(0L, "MapiExceptionCallFailed thrown while processing item - {0} in mailbox - {1}", storePropertyBag[ItemSchema.Id], text);
								}
							}
						}
						Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_BodyTagProcessingSucceeded, null, new object[]
						{
							text
						});
					}
					catch (ObjectNotFoundException ex2)
					{
						flag = true;
						text2 = ex2.Message;
						text3 = ex2.StackTrace;
					}
					catch (CorruptDataException ex3)
					{
						flag = true;
						text2 = ex3.Message;
						text3 = ex3.StackTrace;
					}
					catch (QuotaExceededException ex4)
					{
						flag = true;
						text2 = ex4.Message;
						text3 = ex4.StackTrace;
					}
					catch (MessageSubmissionExceededException ex5)
					{
						flag = true;
						text2 = ex5.Message;
						text3 = ex5.StackTrace;
					}
					catch (ConnectionFailedPermanentException ex6)
					{
						flag = true;
						text2 = ex6.Message;
						text3 = ex6.StackTrace;
					}
					catch (MailboxUnavailableException ex7)
					{
						flag = true;
						text2 = ex7.Message;
						text3 = ex7.StackTrace;
					}
					catch (StorageTransientException ex8)
					{
						flag = true;
						text2 = ex8.Message;
						text3 = ex8.StackTrace;
					}
					catch (StoragePermanentException ex9)
					{
						if (!(ex9.InnerException is MapiExceptionJetErrorLogDiskFull))
						{
							throw;
						}
						flag = true;
						text2 = ex9.Message;
						text3 = ex9.StackTrace;
					}
					finally
					{
						if (flag)
						{
							Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_BodyTagProcessingFailed, null, new object[]
							{
								text,
								text2 ?? "<null>",
								text3 ?? "<null>"
							});
						}
					}
				}, delegate(object exception)
				{
					Exception ex = exception as Exception;
					if (ex != null)
					{
						Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_BodyTagProcessingFailed, null, new object[]
						{
							this.principal.MailboxInfo.PrimarySmtpAddress.ToString(),
							ex.Message ?? "<null>",
							ex.StackTrace ?? "<null>"
						});
					}
					return true;
				}, ReportOptions.None);
			}

			// Token: 0x04000112 RID: 274
			private const int IndexTrackingCounterLimit = 10;

			// Token: 0x04000113 RID: 275
			private const int QueryFetchSize = 50;

			// Token: 0x04000114 RID: 276
			private const double NumberOfDays = 15.0;

			// Token: 0x04000115 RID: 277
			private const int MaxEmailsToProcess = 10000;

			// Token: 0x04000116 RID: 278
			private IExchangePrincipal principal;
		}
	}
}
