using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.DispatchPipe.Ews;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DEE RID: 3566
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class ExchangeServiceTest : IExchangeServiceTest
	{
		// Token: 0x06005C55 RID: 23637 RVA: 0x0011EEF0 File Offset: 0x0011D0F0
		public void CreateItem(string folderId, string subject, string body)
		{
			try
			{
				this.Initialization();
				MessageType messageType = new MessageType
				{
					Body = new BodyContentType
					{
						BodyType = BodyType.Text,
						Value = body
					},
					Subject = subject
				};
				CreateItemRequest request = new CreateItemRequest
				{
					ItemsArray = new MessageType[]
					{
						messageType
					},
					MessageDisposition = "SaveOnly",
					SavedItemFolderId = new TargetFolderId(new FolderId(folderId, IdConverter.BuildChangeKeyString(null, StoreObjectType.Folder)))
				};
				using (IDisposableResponse<CreateItemResponse> disposableResponse = this.exchangeService.CreateItem(request, null))
				{
					CreateItemResponse response = disposableResponse.Response;
					if (response.ResponseMessages == null || response.ResponseMessages.Items == null || response.ResponseMessages.Items.Length != 1 || response.ResponseMessages.Items[0].ResponseClass != ResponseClass.Success)
					{
						throw new Exception("CreateItem failed.");
					}
				}
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x06005C56 RID: 23638 RVA: 0x0011F004 File Offset: 0x0011D204
		public string CreateUnifiedMailbox()
		{
			this.CloseStopWatch();
			string userPrincipalName;
			using (CallContext callContext = this.CreateCallContext(HttpContext.Current, EwsOperationContextBase.Current.RequestMessage))
			{
				using (IExchangeService exchangeService = ExchangeServiceFactory.Default.CreateForEws(callContext))
				{
					CreateUnifiedMailboxRequest request = new CreateUnifiedMailboxRequest();
					CreateUnifiedMailboxResponse createUnifiedMailboxResponse = exchangeService.CreateUnifiedMailbox(request, null);
					userPrincipalName = createUnifiedMailboxResponse.UserPrincipalName;
				}
			}
			return userPrincipalName;
		}

		// Token: 0x06005C57 RID: 23639 RVA: 0x0011F084 File Offset: 0x0011D284
		public bool IsOffice365Domain(string emailAddress)
		{
			bool isOffice365Domain;
			try
			{
				this.Initialization();
				IsOffice365DomainRequest request = new IsOffice365DomainRequest
				{
					EmailAddress = emailAddress
				};
				IsOffice365DomainResponse isOffice365DomainResponse = this.exchangeService.IsOffice365Domain(request, null);
				isOffice365Domain = isOffice365DomainResponse.IsOffice365Domain;
			}
			finally
			{
				this.Cleanup();
			}
			return isOffice365Domain;
		}

		// Token: 0x06005C58 RID: 23640 RVA: 0x0011F0FC File Offset: 0x0011D2FC
		public AggregatedAccountType AddAggregatedAccount(string emailAddress, string userName, string password, string incomingServer, string incomingPort, string incomingProtocol, string security, string authentication, string outgoingServer, string outgoingPort, string outgoingProtocol, string interval)
		{
			AggregatedAccountType result;
			try
			{
				this.Initialization();
				AddAggregatedAccountRequest request = new AddAggregatedAccountRequest
				{
					EmailAddress = emailAddress,
					UserName = userName,
					Password = password,
					IncomingServer = incomingServer,
					IncomingPort = incomingPort,
					IncomingProtocol = incomingProtocol,
					Security = security,
					OutgoingServer = outgoingServer,
					OutgoingPort = outgoingPort,
					OutgoingProtocol = outgoingProtocol
				};
				this.AssignIfParameterSet(authentication, delegate(string val)
				{
					request.Authentication = val;
				});
				this.AssignIfParameterSet(interval, delegate(string val)
				{
					request.IncrementalSyncInterval = val;
				});
				AddAggregatedAccountResponse addAggregatedAccountResponse = this.exchangeService.AddAggregatedAccount(request, null);
				result = ((addAggregatedAccountResponse != null) ? addAggregatedAccountResponse.Account : null);
			}
			finally
			{
				this.Cleanup();
			}
			return result;
		}

		// Token: 0x06005C59 RID: 23641 RVA: 0x0011F1D4 File Offset: 0x0011D3D4
		public void RemoveAggregatedAccount(string emailAddress)
		{
			try
			{
				this.Initialization();
				RemoveAggregatedAccountRequest request = new RemoveAggregatedAccountRequest
				{
					EmailAddress = emailAddress
				};
				this.exchangeService.RemoveAggregatedAccount(request, null);
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x06005C5A RID: 23642 RVA: 0x0011F298 File Offset: 0x0011D498
		public void SetAggregatedAccount(string authentication, string emailAddress, string userName, string password, string interval, string incomingServer, string incomingPort, string incomingProtocol, string security)
		{
			try
			{
				this.Initialization();
				SetAggregatedAccountRequest request = new SetAggregatedAccountRequest();
				this.AssignIfParameterSet(authentication, delegate(string val)
				{
					request.Authentication = val;
				});
				this.AssignIfParameterSet(emailAddress, delegate(string val)
				{
					request.EmailAddress = val;
				});
				this.AssignIfParameterSet(userName, delegate(string val)
				{
					request.UserName = val;
				});
				this.AssignIfParameterSet(incomingPort, delegate(string val)
				{
					request.IncomingPort = val;
				});
				this.AssignIfParameterSet(incomingServer, delegate(string val)
				{
					request.IncomingServer = val;
				});
				this.AssignIfParameterSet(interval, delegate(string val)
				{
					request.IncrementalSyncInterval = val;
				});
				this.AssignIfParameterSet(password, delegate(string val)
				{
					request.Password = val;
				});
				this.AssignIfParameterSet(security, delegate(string val)
				{
					request.Security = val;
				});
				this.exchangeService.SetAggregatedAccount(request, null);
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x06005C5B RID: 23643 RVA: 0x0011F388 File Offset: 0x0011D588
		public AggregatedAccountType[] GetAggregatedAccount()
		{
			AggregatedAccountType[] aggregatedAccounts;
			try
			{
				this.Initialization();
				GetAggregatedAccountRequest request = new GetAggregatedAccountRequest();
				GetAggregatedAccountResponse aggregatedAccount = this.exchangeService.GetAggregatedAccount(request, null);
				aggregatedAccounts = aggregatedAccount.AggregatedAccounts;
			}
			finally
			{
				this.Cleanup();
			}
			return aggregatedAccounts;
		}

		// Token: 0x06005C5C RID: 23644 RVA: 0x0011F3F8 File Offset: 0x0011D5F8
		public ConversationType[] FindConversation(string parentFolderId)
		{
			return this.InternalFindConversationForUnifiedMailbox(delegate(FindConversationRequest request)
			{
				request.ParentFolderId = new TargetFolderId(new FolderId(parentFolderId, IdConverter.BuildChangeKeyString(null, StoreObjectType.Folder)));
			});
		}

		// Token: 0x06005C5D RID: 23645 RVA: 0x0011F464 File Offset: 0x0011D664
		public ConversationType[] FindConversation(Guid[] mailboxGuids, DistinguishedFolderIdName defaultFolder)
		{
			return this.InternalFindConversationForUnifiedMailbox(delegate(FindConversationRequest request)
			{
				request.ParentFolderId = new TargetFolderId(new DistinguishedFolderId
				{
					Id = defaultFolder
				});
				request.MailboxGuids = mailboxGuids;
			});
		}

		// Token: 0x06005C5E RID: 23646 RVA: 0x0011F498 File Offset: 0x0011D698
		public ItemType[] FindItem(string parentFolderId)
		{
			ItemType[] items;
			try
			{
				this.Initialization();
				FindItemRequest findItemRequest = new FindItemRequest();
				findItemRequest.Paging = new IndexedPageView
				{
					Origin = BasePagingType.PagingOrigin.Beginning,
					MaxRows = 100,
					Offset = 0
				};
				findItemRequest.ParentFolderIds = new BaseFolderId[]
				{
					new FolderId(parentFolderId, IdConverter.BuildChangeKeyString(null, StoreObjectType.Folder))
				};
				findItemRequest.ItemShape = this.UseOutlookServiceItemShape();
				FindItemResponse findItemResponse = this.exchangeService.FindItem(findItemRequest, null);
				items = (findItemResponse.ResponseMessages.Items[0] as FindItemResponseMessage).ParentFolder.Items;
			}
			finally
			{
				this.Cleanup();
			}
			return items;
		}

		// Token: 0x06005C5F RID: 23647 RVA: 0x0011F5A0 File Offset: 0x0011D7A0
		public ItemType[] FindItem(string[] folderIds)
		{
			return this.InternalFindItemForUnifiedMailbox(delegate(FindItemRequest request)
			{
				request.ParentFolderIds = new FolderId[folderIds.Length];
				for (int i = 0; i < folderIds.Length; i++)
				{
					request.ParentFolderIds[i] = new FolderId(folderIds[i], IdConverter.BuildChangeKeyString(null, StoreObjectType.Folder));
				}
			});
		}

		// Token: 0x06005C60 RID: 23648 RVA: 0x0011F610 File Offset: 0x0011D810
		public BaseFolderType[] FindFolder(DistinguishedFolderIdName distinguishedFolder)
		{
			return this.InternalFindFolder(delegate(FindFolderRequest request)
			{
				request.MailboxGuid = Guid.Empty;
				request.ParentFolderIds = new BaseFolderId[]
				{
					new DistinguishedFolderId
					{
						Id = distinguishedFolder
					}
				};
			});
		}

		// Token: 0x06005C61 RID: 23649 RVA: 0x0011F660 File Offset: 0x0011D860
		public BaseFolderType[] FindFolder(string mailboxGuid)
		{
			return this.InternalFindFolder(delegate(FindFolderRequest request)
			{
				request.MailboxGuid = new Guid(mailboxGuid);
				request.ParentFolderIds = null;
			});
		}

		// Token: 0x06005C62 RID: 23650 RVA: 0x0011F68C File Offset: 0x0011D88C
		public Guid GetMailboxGuid()
		{
			Guid mailboxGuid;
			try
			{
				this.Initialization();
				mailboxGuid = this.callContext.SessionCache.GetMailboxIdentityMailboxSession().MailboxGuid;
			}
			finally
			{
				this.Cleanup();
			}
			return mailboxGuid;
		}

		// Token: 0x06005C63 RID: 23651 RVA: 0x0011F700 File Offset: 0x0011D900
		public void SubscribeToConversationChanges(string subscriptionId, string parentFolderId)
		{
			this.InternalSubscribeToConversationChanges(subscriptionId, delegate(SubscribeToConversationChangesRequest request)
			{
				if (parentFolderId != null)
				{
					request.ParentFolderId = new TargetFolderId(new FolderId(parentFolderId, IdConverter.BuildChangeKeyString(null, StoreObjectType.Folder)));
				}
			});
		}

		// Token: 0x06005C64 RID: 23652 RVA: 0x0011F770 File Offset: 0x0011D970
		public void SubscribeToConversationChanges(string subscriptionId, Guid[] aggregatedMailboxGuids, DistinguishedFolderIdName defaultFolder)
		{
			this.InternalSubscribeToConversationChanges(subscriptionId, delegate(SubscribeToConversationChangesRequest request)
			{
				request.MailboxGuids = aggregatedMailboxGuids;
				request.ParentFolderId = new TargetFolderId(new DistinguishedFolderId
				{
					Id = defaultFolder
				});
			});
		}

		// Token: 0x06005C65 RID: 23653 RVA: 0x0011F7A4 File Offset: 0x0011D9A4
		public ConversationNotification GetNextConversationChange(string subscriptionId)
		{
			ExDateTime t = ExDateTime.UtcNow.AddMinutes(2.0);
			while (ExDateTime.UtcNow < t)
			{
				lock (ExchangeServiceTest.conversationChanges)
				{
					Queue<ConversationNotification> queue = null;
					if (ExchangeServiceTest.conversationChanges.TryGetValue(subscriptionId, out queue) && queue.Count > 0)
					{
						return queue.Dequeue();
					}
				}
				Thread.Sleep(100);
			}
			return null;
		}

		// Token: 0x06005C66 RID: 23654 RVA: 0x0011F8AC File Offset: 0x0011DAAC
		public void SubscribeToCalendarChanges(string subscriptionId, string parentFolderId)
		{
			try
			{
				this.Initialization();
				SubscribeToCalendarChangesRequest subscribeToCalendarChangesRequest = new SubscribeToCalendarChangesRequest();
				subscribeToCalendarChangesRequest.SubscriptionId = subscriptionId;
				if (parentFolderId != null)
				{
					subscribeToCalendarChangesRequest.ParentFolderId = new TargetFolderId(new FolderId(parentFolderId, IdConverter.BuildChangeKeyString(null, StoreObjectType.Folder)));
				}
				Action<CalendarChangeNotificationType> callback = delegate(CalendarChangeNotificationType type)
				{
					lock (ExchangeServiceTest.calendarChanges)
					{
						Queue<CalendarChangeNotificationType> queue = null;
						if (!ExchangeServiceTest.calendarChanges.TryGetValue(subscriptionId, out queue))
						{
							queue = new Queue<CalendarChangeNotificationType>();
							ExchangeServiceTest.calendarChanges.Add(subscriptionId, queue);
						}
						queue.Enqueue(type);
					}
				};
				this.exchangeService.SubscribeToCalendarChanges(subscribeToCalendarChangesRequest, callback, null);
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x06005C67 RID: 23655 RVA: 0x0011F938 File Offset: 0x0011DB38
		public CalendarChangeNotificationType? GetNextCalendarChange(string subscriptionId)
		{
			ExDateTime t = ExDateTime.UtcNow.AddMinutes(2.0);
			while (ExDateTime.UtcNow < t)
			{
				lock (ExchangeServiceTest.calendarChanges)
				{
					Queue<CalendarChangeNotificationType> queue = null;
					if (ExchangeServiceTest.calendarChanges.TryGetValue(subscriptionId, out queue) && queue.Count > 0)
					{
						return new CalendarChangeNotificationType?(queue.Dequeue());
					}
				}
				Thread.Sleep(100);
			}
			return null;
		}

		// Token: 0x06005C68 RID: 23656 RVA: 0x0011FA58 File Offset: 0x0011DC58
		public InstantSearchPayloadType PerformInstantSearch(string deviceId, string searchSessionId, string kqlQuery, FolderId[] folderScope)
		{
			InstantSearchPayloadType payload2;
			try
			{
				PerformInstantSearchRequest request = new PerformInstantSearchRequest();
				request.DeviceId = deviceId;
				request.SearchSessionId = searchSessionId;
				request.KqlQuery = kqlQuery;
				request.FolderScope = folderScope;
				request.MaximumResultCount = 10;
				request.ItemType = InstantSearchItemType.MailConversation;
				request.QueryOptions = QueryOptionsType.Results;
				this.Initialization();
				Action<InstantSearchPayloadType> searchPayloadCallback = delegate(InstantSearchPayloadType payload)
				{
					lock (ExchangeServiceTest.instantSearchPayloads)
					{
						Queue<InstantSearchPayloadType> queue = null;
						if (!ExchangeServiceTest.instantSearchPayloads.TryGetValue(request.SearchSessionId, out queue))
						{
							queue = new Queue<InstantSearchPayloadType>();
							ExchangeServiceTest.instantSearchPayloads.Add(request.SearchSessionId, queue);
						}
						queue.Enqueue(payload);
					}
				};
				PerformInstantSearchResponse performInstantSearchResponse = this.exchangeService.PerformInstantSearch(request, searchPayloadCallback, null);
				payload2 = performInstantSearchResponse.Payload;
			}
			finally
			{
				this.Cleanup();
			}
			return payload2;
		}

		// Token: 0x06005C69 RID: 23657 RVA: 0x0011FB14 File Offset: 0x0011DD14
		public InstantSearchPayloadType GetNextInstantSearchPayload(string sessionId)
		{
			ExDateTime t = ExDateTime.UtcNow.AddMinutes(2.0);
			while (ExDateTime.UtcNow < t)
			{
				lock (ExchangeServiceTest.instantSearchPayloads)
				{
					Queue<InstantSearchPayloadType> queue = null;
					if (ExchangeServiceTest.instantSearchPayloads.TryGetValue(sessionId, out queue) && queue.Count > 0)
					{
						return queue.Dequeue();
					}
				}
				Thread.Sleep(100);
			}
			return null;
		}

		// Token: 0x06005C6A RID: 23658 RVA: 0x0011FBA4 File Offset: 0x0011DDA4
		public bool EndInstantSearchSession(string deviceId, string sessionId)
		{
			bool result;
			try
			{
				this.Initialization();
				this.exchangeService.EndInstantSearchSession(deviceId, sessionId, null);
				result = true;
			}
			finally
			{
				this.Cleanup();
			}
			return result;
		}

		// Token: 0x06005C6B RID: 23659 RVA: 0x0011FBE4 File Offset: 0x0011DDE4
		public ConversationType[] InternalFindConversationForUnifiedMailbox(Action<FindConversationRequest> initializeRequest)
		{
			ConversationType[] conversations;
			try
			{
				this.Initialization();
				FindConversationRequest findConversationRequest = new FindConversationRequest();
				initializeRequest(findConversationRequest);
				findConversationRequest.Paging = new IndexedPageView
				{
					Origin = BasePagingType.PagingOrigin.Beginning,
					MaxRows = 100,
					Offset = 0
				};
				findConversationRequest.ConversationShape = this.UseOutlookServiceConversationShape();
				FindConversationResponseMessage findConversationResponseMessage = this.exchangeService.FindConversation(findConversationRequest, null);
				conversations = findConversationResponseMessage.Conversations;
			}
			finally
			{
				this.Cleanup();
			}
			return conversations;
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x0011FC64 File Offset: 0x0011DE64
		public ItemType[] InternalFindItemForUnifiedMailbox(Action<FindItemRequest> initializeRequest)
		{
			ItemType[] items;
			try
			{
				this.Initialization();
				FindItemRequest findItemRequest = new FindItemRequest();
				initializeRequest(findItemRequest);
				findItemRequest.Paging = new IndexedPageView
				{
					Origin = BasePagingType.PagingOrigin.Beginning,
					MaxRows = 100,
					Offset = 0
				};
				findItemRequest.ItemShape = this.UseOutlookServiceItemShape();
				FindItemResponse findItemResponse = this.exchangeService.FindItem(findItemRequest, null);
				items = (findItemResponse.ResponseMessages.Items[0] as FindItemResponseMessage).ParentFolder.Items;
			}
			finally
			{
				this.Cleanup();
			}
			return items;
		}

		// Token: 0x06005C6D RID: 23661 RVA: 0x0011FCF8 File Offset: 0x0011DEF8
		private BaseFolderType[] InternalFindFolder(Action<FindFolderRequest> initializeRequest)
		{
			BaseFolderType[] folders;
			try
			{
				this.Initialization();
				FindFolderRequest findFolderRequest = new FindFolderRequest();
				initializeRequest(findFolderRequest);
				findFolderRequest.Traversal = FolderQueryTraversal.Deep;
				PropertyPath[] additionalProperties = new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.ParentFolderId),
					new PropertyUri(PropertyUriEnum.FolderDisplayName),
					new PropertyUri(PropertyUriEnum.FolderClass),
					new PropertyUri(PropertyUriEnum.DistinguishedFolderId)
				};
				findFolderRequest.FolderShape = new FolderResponseShape(ShapeEnum.IdOnly, additionalProperties);
				FindFolderResponse findFolderResponse = this.exchangeService.FindFolder(findFolderRequest, null);
				FindFolderResponseMessage findFolderResponseMessage = findFolderResponse.ResponseMessages.Items[0] as FindFolderResponseMessage;
				folders = findFolderResponseMessage.RootFolder.Folders;
			}
			finally
			{
				this.Cleanup();
			}
			return folders;
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x0011FE24 File Offset: 0x0011E024
		public void InternalSubscribeToConversationChanges(string subscriptionId, Action<SubscribeToConversationChangesRequest> initializeRequest)
		{
			try
			{
				this.Initialization();
				SubscribeToConversationChangesRequest subscribeToConversationChangesRequest = new SubscribeToConversationChangesRequest();
				subscribeToConversationChangesRequest.SubscriptionId = subscriptionId;
				subscribeToConversationChangesRequest.ConversationShape = this.UseOutlookServiceConversationShape();
				initializeRequest(subscribeToConversationChangesRequest);
				Action<ConversationNotification> callback = delegate(ConversationNotification notification)
				{
					lock (ExchangeServiceTest.conversationChanges)
					{
						Queue<ConversationNotification> queue = null;
						if (!ExchangeServiceTest.conversationChanges.TryGetValue(subscriptionId, out queue))
						{
							queue = new Queue<ConversationNotification>();
							ExchangeServiceTest.conversationChanges.Add(subscriptionId, queue);
						}
						queue.Enqueue(notification);
					}
				};
				this.exchangeService.SubscribeToConversationChanges(subscribeToConversationChangesRequest, callback, null);
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x06005C6F RID: 23663 RVA: 0x0011FEA8 File Offset: 0x0011E0A8
		private CallContext CreateCallContext(HttpContext httpContext, Message message)
		{
			JsonMessageHeaderProcessor headerProcessor = new JsonMessageHeaderProcessor();
			message.Properties.Add("WebMethodEntry", WebMethodEntry.JsonWebMethodEntry);
			MSAIdentity msaidentity = httpContext.User.Identity as MSAIdentity;
			CallContext callContext;
			if (msaidentity == null)
			{
				callContext = CallContext.CreateFromRequest(headerProcessor, message);
			}
			else
			{
				ExchangeServiceTest.InitIfNeeded(httpContext);
				BudgetKey key = new StringBudgetKey(msaidentity.MemberName, false, BudgetType.Ews);
				callContext = CallContext.CreateForExchangeService(httpContext, ExchangeServiceTest.appWideStoreSessionCache, ExchangeServiceTest.acceptedDomainCache, ExchangeServiceTest.userWorkloadManager, EwsBudget.Acquire(key), Thread.CurrentThread.CurrentCulture);
			}
			HttpContext.Current.Items["CallContext"] = callContext;
			return callContext;
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x0011FF40 File Offset: 0x0011E140
		private void CloseStopWatch()
		{
			HttpContext.Current.Items["ServicesStopwatch"] = null;
		}

		// Token: 0x06005C71 RID: 23665 RVA: 0x0011FF57 File Offset: 0x0011E157
		private void AssignIfParameterSet(string parameter, Action<string> assignValue)
		{
			if (!string.IsNullOrEmpty(parameter))
			{
				assignValue(parameter);
			}
		}

		// Token: 0x06005C72 RID: 23666 RVA: 0x0011FF68 File Offset: 0x0011E168
		private void Initialization()
		{
			this.CloseStopWatch();
			this.callContext = this.CreateCallContext(HttpContext.Current, EwsOperationContextBase.Current.RequestMessage);
			ExchangeVersion.Current = ExchangeVersion.Latest;
			this.exchangeService = ExchangeServiceFactory.Default.CreateForEws(this.callContext);
		}

		// Token: 0x06005C73 RID: 23667 RVA: 0x0011FFB6 File Offset: 0x0011E1B6
		private void Cleanup()
		{
			this.callContext.Dispose();
			this.exchangeService.Dispose();
		}

		// Token: 0x06005C74 RID: 23668 RVA: 0x0011FFD0 File Offset: 0x0011E1D0
		private static void InitIfNeeded(HttpContext httpContext)
		{
			if (!ExchangeServiceTest.initialized)
			{
				lock (ExchangeServiceTest.staticLock)
				{
					if (!ExchangeServiceTest.initialized)
					{
						HttpApplicationState application = httpContext.Application;
						ExchangeServiceTest.appWideStoreSessionCache = (application["WS_APPWideMailboxCacheKey"] as AppWideStoreSessionCache);
						ExchangeServiceTest.acceptedDomainCache = (application["WS_AcceptedDomainCacheKey"] as AcceptedDomainCache);
						ExchangeServiceTest.userWorkloadManager = (application["WS_WorkloadManagerKey"] as UserWorkloadManager);
						ExchangeServiceTest.initialized = true;
					}
				}
			}
		}

		// Token: 0x06005C75 RID: 23669 RVA: 0x00120064 File Offset: 0x0011E264
		private ConversationResponseShape UseOutlookServiceConversationShape()
		{
			return new ConversationResponseShape
			{
				BaseShape = ShapeEnum.IdOnly,
				AdditionalProperties = new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.ItemParentId),
					new PropertyUri(PropertyUriEnum.ConversationGuidId),
					new PropertyUri(PropertyUriEnum.Topic),
					new PropertyUri(PropertyUriEnum.ConversationPreview),
					new PropertyUri(PropertyUriEnum.ConversationGlobalUniqueSenders),
					new PropertyUri(PropertyUriEnum.ConversationLastModifiedTime),
					new PropertyUri(PropertyUriEnum.ConversationGlobalUnreadCount),
					new PropertyUri(PropertyUriEnum.ConversationGlobalFlagStatus),
					new PropertyUri(PropertyUriEnum.ConversationGlobalHasIrm),
					new PropertyUri(PropertyUriEnum.ConversationGlobalLastDeliveryOrRenewTime),
					new PropertyUri(PropertyUriEnum.InstanceKey),
					new PropertyUri(PropertyUriEnum.ConversationGlobalItemIds),
					new PropertyUri(PropertyUriEnum.ConversationGlobalRichContent),
					new PropertyUri(PropertyUriEnum.ConversationLastDeliveryTime)
				}
			};
		}

		// Token: 0x06005C76 RID: 23670 RVA: 0x00120144 File Offset: 0x0011E344
		private ItemResponseShape UseOutlookServiceItemShape()
		{
			return new ItemResponseShape
			{
				BaseShape = ShapeEnum.Default
			};
		}

		// Token: 0x06005C77 RID: 23671 RVA: 0x00120160 File Offset: 0x0011E360
		public bool GetFolderFidAndMailboxFromEwsId(string ewsId, out long fid, out Guid mailboxGuid)
		{
			bool folderFidAndMailboxFromEwsId;
			try
			{
				this.Initialization();
				folderFidAndMailboxFromEwsId = this.exchangeService.GetFolderFidAndMailboxFromEwsId(ewsId, out fid, out mailboxGuid);
			}
			finally
			{
				this.Cleanup();
			}
			return folderFidAndMailboxFromEwsId;
		}

		// Token: 0x06005C78 RID: 23672 RVA: 0x0012019C File Offset: 0x0011E39C
		public long GetFolderFidFromEwsId(string ewsId)
		{
			long folderFidFromEwsId;
			try
			{
				this.Initialization();
				folderFidFromEwsId = this.exchangeService.GetFolderFidFromEwsId(ewsId);
			}
			finally
			{
				this.Cleanup();
			}
			return folderFidFromEwsId;
		}

		// Token: 0x06005C79 RID: 23673 RVA: 0x001201D8 File Offset: 0x0011E3D8
		public string GetEwsIdFromFolderFid(long fid, Guid mailboxGuid)
		{
			string ewsIdFromFolderFid;
			try
			{
				this.Initialization();
				ewsIdFromFolderFid = this.exchangeService.GetEwsIdFromFolderFid(fid, mailboxGuid);
			}
			finally
			{
				this.Cleanup();
			}
			return ewsIdFromFolderFid;
		}

		// Token: 0x06005C7A RID: 23674 RVA: 0x0012028C File Offset: 0x0011E48C
		public void SubscribeToHierarchyChanges(string subscriptionId, Guid mailboxGuid)
		{
			try
			{
				this.Initialization();
				SubscribeToHierarchyChangesRequest subscribeToHierarchyChangesRequest = new SubscribeToHierarchyChangesRequest();
				subscribeToHierarchyChangesRequest.SubscriptionId = subscriptionId;
				subscribeToHierarchyChangesRequest.MailboxGuid = mailboxGuid;
				Action<HierarchyNotification> callback = delegate(HierarchyNotification notification)
				{
					lock (ExchangeServiceTest.hierarchyChanges)
					{
						Queue<HierarchyNotification> queue = null;
						if (!ExchangeServiceTest.hierarchyChanges.TryGetValue(subscriptionId, out queue))
						{
							queue = new Queue<HierarchyNotification>();
							ExchangeServiceTest.hierarchyChanges.Add(subscriptionId, queue);
						}
						queue.Enqueue(notification);
					}
				};
				this.exchangeService.SubscribeToHierarchyChanges(subscribeToHierarchyChangesRequest, callback, null);
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x06005C7B RID: 23675 RVA: 0x00120304 File Offset: 0x0011E504
		public HierarchyNotification GetNextHierarchyChange(string subscriptionId)
		{
			ExDateTime t = ExDateTime.UtcNow.AddMinutes(2.0);
			while (ExDateTime.UtcNow < t)
			{
				lock (ExchangeServiceTest.hierarchyChanges)
				{
					Queue<HierarchyNotification> queue = null;
					if (ExchangeServiceTest.hierarchyChanges.TryGetValue(subscriptionId, out queue) && queue.Count > 0)
					{
						return queue.Dequeue();
					}
				}
				Thread.Sleep(100);
			}
			return null;
		}

		// Token: 0x06005C7C RID: 23676 RVA: 0x001203C4 File Offset: 0x0011E5C4
		public void SubscribeToMessageChanges(string subscriptionId, string parentFolderId)
		{
			this.InternalSubscribeToMessageChanges(subscriptionId, delegate(SubscribeToMessageChangesRequest request)
			{
				if (parentFolderId != null)
				{
					request.ParentFolderId = new TargetFolderId(new FolderId(parentFolderId, IdConverter.BuildChangeKeyString(null, StoreObjectType.Folder)));
				}
			});
		}

		// Token: 0x06005C7D RID: 23677 RVA: 0x00120434 File Offset: 0x0011E634
		public void SubscribeToMessageChanges(string subscriptionId, Guid[] aggregatedMailboxGuids, DistinguishedFolderIdName defaultFolder)
		{
			this.InternalSubscribeToMessageChanges(subscriptionId, delegate(SubscribeToMessageChangesRequest request)
			{
				request.MailboxGuids = aggregatedMailboxGuids;
				request.ParentFolderId = new TargetFolderId(new DistinguishedFolderId
				{
					Id = defaultFolder
				});
			});
		}

		// Token: 0x06005C7E RID: 23678 RVA: 0x00120468 File Offset: 0x0011E668
		public MessageNotification GetNextMessageChange(string subscriptionId)
		{
			ExDateTime t = ExDateTime.UtcNow.AddMinutes(2.0);
			while (ExDateTime.UtcNow < t)
			{
				lock (ExchangeServiceTest.messageChanges)
				{
					Queue<MessageNotification> queue = null;
					if (ExchangeServiceTest.messageChanges.TryGetValue(subscriptionId, out queue) && queue.Count > 0)
					{
						return queue.Dequeue();
					}
				}
				Thread.Sleep(100);
			}
			return null;
		}

		// Token: 0x06005C7F RID: 23679 RVA: 0x00120570 File Offset: 0x0011E770
		public void InternalSubscribeToMessageChanges(string subscriptionId, Action<SubscribeToMessageChangesRequest> initializeRequest)
		{
			try
			{
				this.Initialization();
				SubscribeToMessageChangesRequest subscribeToMessageChangesRequest = new SubscribeToMessageChangesRequest();
				subscribeToMessageChangesRequest.SubscriptionId = subscriptionId;
				subscribeToMessageChangesRequest.MessageShape = this.UseOutlookServiceItemShape();
				initializeRequest(subscribeToMessageChangesRequest);
				Action<MessageNotification> callback = delegate(MessageNotification notification)
				{
					lock (ExchangeServiceTest.messageChanges)
					{
						Queue<MessageNotification> queue = null;
						if (!ExchangeServiceTest.messageChanges.TryGetValue(subscriptionId, out queue))
						{
							queue = new Queue<MessageNotification>();
							ExchangeServiceTest.messageChanges.Add(subscriptionId, queue);
						}
						queue.Enqueue(notification);
					}
				};
				this.exchangeService.SubscribeToMessageChanges(subscribeToMessageChangesRequest, callback, null);
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x0400322B RID: 12843
		private static object staticLock = new object();

		// Token: 0x0400322C RID: 12844
		private static bool initialized = false;

		// Token: 0x0400322D RID: 12845
		private static AppWideStoreSessionCache appWideStoreSessionCache;

		// Token: 0x0400322E RID: 12846
		private static AcceptedDomainCache acceptedDomainCache;

		// Token: 0x0400322F RID: 12847
		private static UserWorkloadManager userWorkloadManager;

		// Token: 0x04003230 RID: 12848
		private CallContext callContext;

		// Token: 0x04003231 RID: 12849
		private IExchangeService exchangeService;

		// Token: 0x04003232 RID: 12850
		private static Dictionary<string, Queue<ConversationNotification>> conversationChanges = new Dictionary<string, Queue<ConversationNotification>>();

		// Token: 0x04003233 RID: 12851
		private static Dictionary<string, Queue<CalendarChangeNotificationType>> calendarChanges = new Dictionary<string, Queue<CalendarChangeNotificationType>>();

		// Token: 0x04003234 RID: 12852
		private static Dictionary<string, Queue<HierarchyNotification>> hierarchyChanges = new Dictionary<string, Queue<HierarchyNotification>>();

		// Token: 0x04003235 RID: 12853
		private static Dictionary<string, Queue<MessageNotification>> messageChanges = new Dictionary<string, Queue<MessageNotification>>();

		// Token: 0x04003236 RID: 12854
		private static Dictionary<string, Queue<InstantSearchPayloadType>> instantSearchPayloads = new Dictionary<string, Queue<InstantSearchPayloadType>>();
	}
}
