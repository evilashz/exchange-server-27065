using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.DiscoverySearch;
using Microsoft.Exchange.EDiscovery.MailboxSearch;
using Microsoft.Exchange.InfoWorker.Common.SearchService;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.MailboxSearch;
using Microsoft.Mapi;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DiscoverySearch
{
	// Token: 0x020001FE RID: 510
	internal class DiscoverySearchEventBasedAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x06001391 RID: 5009 RVA: 0x0007226E File Offset: 0x0007046E
		public DiscoverySearchEventBasedAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0007227C File Offset: 0x0007047C
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			DiscoverySearchEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "MapiEvent is dispatched: {0}", mapiEvent);
			if (!ObjectClass.IsMailboxDiscoverySearchRequest(mapiEvent.ObjectClass) || (mapiEvent.EventMask & MapiEventTypeFlags.ObjectCreated) == (MapiEventTypeFlags)0)
			{
				DiscoverySearchEventBasedAssistant.Tracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "Only the discovery search request created message is relevent. Mapievent: {0}", mapiEvent);
				return false;
			}
			return true;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0007258C File Offset: 0x0007078C
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			if (ObjectClass.IsMailboxDiscoverySearchRequest(mapiEvent.ObjectClass) && (mapiEvent.EventMask & MapiEventTypeFlags.ObjectCreated) == MapiEventTypeFlags.ObjectCreated && itemStore != null && item != null)
			{
				DiscoverySearchEventBasedAssistant.<>c__DisplayClassf CS$<>8__locals2 = new DiscoverySearchEventBasedAssistant.<>c__DisplayClassf();
				DiscoverySearchEventBasedAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Processing discovery search arbitration mailbox: {0}", itemStore.MailboxOwnerLegacyDN);
				CS$<>8__locals2.name = item.GetValueOrDefault<string>(DiscoverySearchEventBasedAssistant.AlternativeId, null);
				if (string.IsNullOrEmpty(CS$<>8__locals2.name))
				{
					return;
				}
				ActionRequestType valueOrDefault = item.GetValueOrDefault<ActionRequestType>(DiscoverySearchEventBasedAssistant.AsynchronousActionRequest, ActionRequestType.None);
				string rbacContext = null;
				try
				{
					rbacContext = item.GetValueOrDefault<string>(DiscoverySearchEventBasedAssistant.AsynchronousActionRbacContext, null);
				}
				catch (PropertyTooBigException)
				{
					using (Stream stream = item.PropertyBag.OpenPropertyStream(DiscoverySearchEventBasedAssistant.AsynchronousActionRbacContext, PropertyOpenMode.ReadOnly))
					{
						using (StreamReader streamReader = new StreamReader(stream, Encoding.Unicode))
						{
							rbacContext = streamReader.ReadToEnd();
						}
					}
				}
				SearchEventLogger.Instance.LogDiscoverySearchRequestPickedUpEvent(CS$<>8__locals2.name, valueOrDefault.ToString(), rbacContext, itemStore.MailboxOwner.MailboxInfo.OrganizationId.ToString());
				switch (valueOrDefault)
				{
				case ActionRequestType.Start:
					this.CallSearchService(itemStore, CS$<>8__locals2.name, (Item)item, delegate(MailboxSearchClient client, SearchId searchId)
					{
						DiscoverySearchEventBasedAssistant.RpcCallWithRetry(delegate()
						{
							client.StartEx(searchId, rbacContext);
						});
					});
					SearchEventLogger.Instance.LogDiscoverySearchStartRequestProcessedEvent(CS$<>8__locals2.name, rbacContext, itemStore.MailboxOwner.MailboxInfo.OrganizationId.ToString());
					break;
				case ActionRequestType.Stop:
					this.CallSearchService(itemStore, CS$<>8__locals2.name, (Item)item, delegate(MailboxSearchClient client, SearchId searchId)
					{
						DiscoverySearchEventBasedAssistant.RpcCallWithRetry(delegate()
						{
							client.AbortEx(searchId, rbacContext);
						});
					});
					SearchEventLogger.Instance.LogDiscoverySearchStopRequestProcessedEvent(CS$<>8__locals2.name, rbacContext, itemStore.MailboxOwner.MailboxInfo.OrganizationId.ToString());
					break;
				case ActionRequestType.Restart:
					this.CallSearchService(itemStore, CS$<>8__locals2.name, (Item)item, delegate(MailboxSearchClient client, SearchId searchId)
					{
						DiscoverySearchEventBasedAssistant.RpcCallWithRetry(delegate()
						{
							client.Remove(searchId, false);
						});
						SearchEventLogger.Instance.LogDiscoverySearchRemoveRequestProcessedEvent(CS$<>8__locals2.name, rbacContext, itemStore.MailboxOwner.MailboxInfo.OrganizationId.ToString());
						DiscoverySearchEventBasedAssistant.RpcCallWithRetry(delegate()
						{
							client.StartEx(searchId, rbacContext);
						});
						SearchEventLogger.Instance.LogDiscoverySearchStartRequestProcessedEvent(CS$<>8__locals2.name, rbacContext, itemStore.MailboxOwner.MailboxInfo.OrganizationId.ToString());
					});
					break;
				case ActionRequestType.Delete:
					this.CallSearchService(itemStore, CS$<>8__locals2.name, null, delegate(MailboxSearchClient client, SearchId searchId)
					{
						DiscoverySearchEventBasedAssistant.RpcCallWithRetry(delegate()
						{
							client.Remove(searchId, true);
						});
					});
					SearchEventLogger.Instance.LogDiscoverySearchRemoveRequestProcessedEvent(CS$<>8__locals2.name, rbacContext, itemStore.MailboxOwner.MailboxInfo.OrganizationId.ToString());
					break;
				case ActionRequestType.UpdateStatus:
					this.CallSearchService(itemStore, CS$<>8__locals2.name, null, delegate(MailboxSearchClient client, SearchId searchId)
					{
						DiscoverySearchEventBasedAssistant.RpcCallWithRetry(delegate()
						{
							client.UpdateStatus(searchId);
						});
					});
					break;
				}
				try
				{
					itemStore.Delete(DeleteItemFlags.HardDelete, new StoreId[]
					{
						item.Id
					});
					return;
				}
				catch (StoragePermanentException ex)
				{
					DiscoverySearchEventBasedAssistant.Tracer.TraceError<string, StoragePermanentException>((long)this.GetHashCode(), "Failed to remove mailbox discovery search request item. Name: '{0}'. Exception: '{1}'", CS$<>8__locals2.name, ex);
					SearchEventLogger.Instance.LogDiscoverySearchServerErrorEvent("Failed to remove mailbox discovery search request item", CS$<>8__locals2.name, itemStore.ServerFullyQualifiedDomainName, ex);
					return;
				}
				catch (StorageTransientException ex2)
				{
					DiscoverySearchEventBasedAssistant.Tracer.TraceError<string, StorageTransientException>((long)this.GetHashCode(), "Failed to remove mailbox discovery search request item. Name: '{0}'. Exception: '{1}'", CS$<>8__locals2.name, ex2);
					SearchEventLogger.Instance.LogDiscoverySearchServerErrorEvent("Failed to remove mailbox discovery search request item", CS$<>8__locals2.name, itemStore.ServerFullyQualifiedDomainName, ex2);
					return;
				}
			}
			DiscoverySearchEventBasedAssistant.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Skipping discovery search object from non-arbitration mailbox: {0}", itemStore.MailboxOwnerLegacyDN);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x000729CC File Offset: 0x00070BCC
		private void CallSearchService(MailboxSession arbitrationMailbox, string discoverySearchName, Item requestItem, Action<MailboxSearchClient, SearchId> serviceCall)
		{
			Exception ex = null;
			try
			{
				SearchId arg = new SearchId(arbitrationMailbox.MailboxOwner.ObjectId.DistinguishedName, arbitrationMailbox.MailboxOwner.ObjectId.ObjectGuid, discoverySearchName);
				using (MailboxSearchClient mailboxSearchClient = new MailboxSearchClient(arbitrationMailbox.ServerFullyQualifiedDomainName))
				{
					serviceCall(mailboxSearchClient, arg);
				}
			}
			catch (RpcConnectionException ex2)
			{
				ex = ex2;
			}
			catch (RpcException ex3)
			{
				ex = ex3;
			}
			catch (SearchServerException ex4)
			{
				if (ex4.ErrorCode == 262657 || ex4.ErrorCode == 262658)
				{
					DiscoverySearchEventBasedAssistant.Tracer.TraceError<SearchServerException>((long)this.GetHashCode(), "Exception caught but the certain exceptions are still consider operation successful. Exception: '{0}'.", ex4);
				}
				else
				{
					ex = ex4;
				}
			}
			if (ex != null)
			{
				DiscoverySearchEventBasedAssistant.Tracer.TraceError<string, string, Exception>((long)this.GetHashCode(), "Exception happened when calling mailbox search RPC call. Search Name: '{0}'. Mailbox Server: '{1}'. Exception: '{2}'", discoverySearchName, arbitrationMailbox.ServerFullyQualifiedDomainName, ex);
				SearchEventLogger.Instance.LogDiscoverySearchServerErrorEvent("Exception occured when calling mailbox search RPC call", discoverySearchName, arbitrationMailbox.ServerFullyQualifiedDomainName, ex);
				if (requestItem != null)
				{
					Exception ex5 = null;
					try
					{
						using (Folder folder = Folder.Bind(arbitrationMailbox, requestItem.ParentId))
						{
							QueryFilter queryFilter = new AndFilter(new QueryFilter[]
							{
								new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Configuration.MailboxDiscoverySearch"),
								new ComparisonFilter(ComparisonOperator.Equal, DiscoverySearchEventBasedAssistant.AlternativeId, discoverySearchName)
							});
							using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, queryFilter, null, new PropertyDefinition[]
							{
								ItemSchema.Id
							}))
							{
								object[][] rows = queryResult.GetRows(1);
								if (rows != null && rows.Length > 0 && rows[0].Length > 0)
								{
									StoreId storeId = rows[0][0] as StoreId;
									if (storeId != null)
									{
										using (Item item = Item.Bind(arbitrationMailbox, storeId))
										{
											StreamAttachment streamAttachment = null;
											try
											{
												foreach (AttachmentHandle handle in item.AttachmentCollection)
												{
													StreamAttachment streamAttachment2 = (StreamAttachment)item.AttachmentCollection.Open(handle);
													if (streamAttachment2.FileName == MailboxDiscoverySearchSchema.Errors.Name)
													{
														streamAttachment = streamAttachment2;
														break;
													}
													streamAttachment2.Dispose();
												}
												if (streamAttachment == null)
												{
													streamAttachment = (StreamAttachment)item.AttachmentCollection.Create(AttachmentType.Stream);
													streamAttachment.FileName = MailboxDiscoverySearchSchema.Errors.Name;
												}
												using (Stream contentStream = streamAttachment.GetContentStream())
												{
													ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null).Serialize(contentStream, new string[]
													{
														ex.Message
													});
												}
												streamAttachment.Save();
												item.SetOrDeleteProperty(DiscoverySearchEventBasedAssistant.Status, SearchState.Failed);
												item.Save(SaveMode.ResolveConflicts);
											}
											finally
											{
												if (streamAttachment != null)
												{
													streamAttachment.Dispose();
												}
											}
										}
									}
								}
							}
						}
					}
					catch (StorageTransientException ex6)
					{
						ex5 = ex6;
					}
					catch (StoragePermanentException ex7)
					{
						ex5 = ex7;
					}
					if (ex5 != null)
					{
						DiscoverySearchEventBasedAssistant.Tracer.TraceError<Exception, Exception>((long)this.GetHashCode(), "Exception when saving error to the discovery object.\n\nThe exception is : {0}.\n\n The error to be saved is : {1}.", ex5, ex);
						SearchEventLogger.Instance.LogDiscoverySearchServerErrorEvent("Exception occured when saving error to the discovery object", discoverySearchName, arbitrationMailbox.ServerFullyQualifiedDomainName, ex5);
					}
				}
			}
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00072DF0 File Offset: 0x00070FF0
		private static void RpcCallWithRetry(Action rpcDelegate)
		{
			DiscoverySearchEventBasedAssistant.RpcCallWithRetry(rpcDelegate, 3);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00072DFC File Offset: 0x00070FFC
		private static void RpcCallWithRetry(Action rpcDelegate, int maxRetry)
		{
			bool flag = false;
			if (maxRetry <= 0)
			{
				throw new ArgumentException("maxRetry should be greater than 0");
			}
			for (int i = 0; i < maxRetry - 1; i++)
			{
				try
				{
					rpcDelegate();
					return;
				}
				catch (RpcException ex)
				{
					DiscoverySearchEventBasedAssistant.Tracer.TraceError<RpcException, int>((long)rpcDelegate.GetHashCode(), "RPC call to mailbox search failed (retry times: {1}): {0}", ex, i);
					if (!flag)
					{
						flag = true;
						MailboxSearchServer.RestartServer();
						SearchEventLogger.Instance.LogDiscoverySearchRpcServerRestartedEvent(ex);
					}
					else if (!(ex is RpcConnectionException))
					{
						EventNotificationItem.Publish(ExchangeComponent.EdiscoveryProtocol.Name, "MailboxSearch.RPCFailureEvents.Monitor", null, ex.ToString(), ResultSeverityLevel.Error, false);
						throw;
					}
				}
			}
			try
			{
				rpcDelegate();
			}
			catch (RpcException ex2)
			{
				DiscoverySearchEventBasedAssistant.Tracer.TraceError<RpcException>((long)rpcDelegate.GetHashCode(), "RPC call to mailbox search failed: {0}", ex2);
				EventNotificationItem.Publish(ExchangeComponent.EdiscoveryProtocol.Name, "MailboxSearch.RPCFailureEvents.Monitor", null, ex2.ToString(), ResultSeverityLevel.Error, false);
				throw;
			}
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00072F45 File Offset: 0x00071145
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00072F4D File Offset: 0x0007114D
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00072F55 File Offset: 0x00071155
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000BF6 RID: 3062
		private const int MaxRpcRetryCount = 3;

		// Token: 0x04000BF7 RID: 3063
		private static readonly Trace Tracer = ExTraceGlobals.DiscoverySearchEventBasedAssistantTracer;

		// Token: 0x04000BF8 RID: 3064
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x04000BF9 RID: 3065
		internal static readonly GuidNamePropertyDefinition AlternativeId = MailboxDiscoverySearchRequestSchema.CreateStorePropertyDefinition(EwsStoreObjectSchema.AlternativeId);

		// Token: 0x04000BFA RID: 3066
		internal static readonly GuidNamePropertyDefinition AsynchronousActionRequest = MailboxDiscoverySearchRequestSchema.CreateStorePropertyDefinition(MailboxDiscoverySearchRequestSchema.AsynchronousActionRequest);

		// Token: 0x04000BFB RID: 3067
		internal static readonly GuidNamePropertyDefinition AsynchronousActionRbacContext = MailboxDiscoverySearchRequestSchema.CreateStorePropertyDefinition(MailboxDiscoverySearchRequestSchema.AsynchronousActionRbacContext);

		// Token: 0x04000BFC RID: 3068
		internal static readonly GuidNamePropertyDefinition Status = MailboxDiscoverySearchRequestSchema.CreateStorePropertyDefinition(MailboxDiscoverySearchSchema.Status);
	}
}
