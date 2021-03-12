using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Mapi;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.Services
{
	// Token: 0x0200001F RID: 31
	internal class FaultInjection
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x0000898D File Offset: 0x00006B8D
		public static void GenerateFault(FaultInjection.LIDs faultLid)
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest((uint)faultLid);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000899C File Offset: 0x00006B9C
		public static T TraceTest<T>(FaultInjection.LIDs faultLid)
		{
			T result = default(T);
			ExTraceGlobals.FaultInjectionTracer.TraceTest<T>((uint)faultLid, ref result);
			return result;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000089BF File Offset: 0x00006BBF
		public static void FaultInjectionPoint(FaultInjection.LIDs faultLid, Action productAction, Action faultInjectionAction)
		{
			if (FaultInjection.TraceTest<bool>(faultLid))
			{
				faultInjectionAction();
				return;
			}
			productAction();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000089D8 File Offset: 0x00006BD8
		public static Exception Callback(string exceptionType)
		{
			Exception result = null;
			if (exceptionType != null && exceptionType != null)
			{
				if (<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x600019e-1 == null)
				{
					<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x600019e-1 = new Dictionary<string, int>(28)
					{
						{
							"Microsoft.Exchange.Data.Storage.EventNotFoundException",
							0
						},
						{
							"Microsoft.Exchange.Data.Storage.ReadEventsFailedException",
							1
						},
						{
							"Microsoft.Exchange.Data.Storage.ReadEventsFailedTransientException",
							2
						},
						{
							"Microsoft.Exchange.Data.Directory.SuitabilityDirectoryException",
							3
						},
						{
							"Microsoft.Exchange.Data.Directory.ADTransientException",
							4
						},
						{
							"System.Net.WebException",
							5
						},
						{
							"Microsoft.Exchange.Data.Directory.ADPossibleOperationException",
							6
						},
						{
							"Microsoft.Exchange.Data.Storage.StorageTransientException",
							7
						},
						{
							"Microsoft.Exchange.Data.Storage.MailboxInSiteFailoverException",
							8
						},
						{
							"Microsoft.Exchange.Data.Directory.SystemConfiguration.OverBudgetException",
							9
						},
						{
							"Microsoft.Exchange.Data.Storage.AccessDeniedException",
							10
						},
						{
							"Microsoft.Exchange.Data.Storage.AccountDisabledException",
							11
						},
						{
							"Microsoft.Exchange.Data.Storage.ConnectionFailedPermanentException",
							12
						},
						{
							"Microsoft.Exchange.Data.Storage.CorruptDataException",
							13
						},
						{
							"Microsoft.Exchange.Data.Storage.ObjectNotFoundException",
							14
						},
						{
							"Microsoft.Exchange.Data.Storage.ServerNotFoundException",
							15
						},
						{
							"Microsoft.Exchange.Data.Storage.ServerNotInSiteException",
							16
						},
						{
							"Microsoft.Exchange.Data.Storage.FinalEventException",
							17
						},
						{
							"Microsoft.Mapi.MapiExceptionAmbiguousAlias",
							18
						},
						{
							"System.SystemException",
							19
						},
						{
							"System.IO.IOException",
							20
						},
						{
							"Microsoft.Mapi.MapiExceptionCorruptData",
							21
						},
						{
							"Microsoft.Exchange.Data.Storage.IllegalCrossServerConnectionException",
							22
						},
						{
							"Microsoft.Mapi.MapiExceptionShutoffQuotaExceeded",
							23
						},
						{
							"Microsoft.Mapi.MapiExceptionNoSupport",
							24
						},
						{
							"Microsoft.Exchange.Data.TextConverters.TextConvertersException",
							25
						},
						{
							"Microsoft.Mapi.MapiExceptionNoAccess",
							26
						},
						{
							"Microsoft.Mapi.MapiExceptionTimeout",
							27
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x600019e-1.TryGetValue(exceptionType, out num))
				{
					switch (num)
					{
					case 0:
						result = new EventNotFoundException(new LocalizedString("EventNotFoundException"));
						break;
					case 1:
						result = new ReadEventsFailedException(new LocalizedString("ReadEventsFailedException"), null);
						break;
					case 2:
						result = new ReadEventsFailedTransientException(new LocalizedString("ReadEventsFailedTransientException"), null);
						break;
					case 3:
						result = new SuitabilityDirectoryException("FQDN", 1, "SuitabilityDirectoryException");
						break;
					case 4:
						result = new ADTransientException(new LocalizedString("ADTransientException"));
						break;
					case 5:
						result = new WebException("The request was aborted: The request was canceled.", null, WebExceptionStatus.RequestCanceled, null);
						break;
					case 6:
						result = new ADPossibleOperationException(new LocalizedString("ADPossibleOperationException"));
						break;
					case 7:
						result = new StorageTransientException(new LocalizedString("StorageTransientException"));
						break;
					case 8:
						result = new MailboxInSiteFailoverException(new LocalizedString("ConstMailboxInSiteFailoverException"));
						break;
					case 9:
						result = new OverBudgetException();
						break;
					case 10:
						result = new AccessDeniedException(new LocalizedString("AccessDeniedException"));
						break;
					case 11:
						result = new AccountDisabledException(new LocalizedString("AccountDisabledException"));
						break;
					case 12:
						result = new ConnectionFailedPermanentException(new LocalizedString("ConnectionFailedPermanentException"));
						break;
					case 13:
						result = new CorruptDataException(new LocalizedString("CorruptDataException"));
						break;
					case 14:
						result = new ObjectNotFoundException(new LocalizedString("ObjectNotFoundException"));
						break;
					case 15:
						result = new ServerNotFoundException("Server not Found", "ServerName");
						break;
					case 16:
						result = new ServerNotInSiteException("Server not in site", "ServerName");
						break;
					case 17:
					{
						MapiEventNative mapiEventNative = default(MapiEventNative);
						Event finalEvent = new Event(Guid.NewGuid(), new MapiEvent(ref mapiEventNative));
						result = new FinalEventException(finalEvent);
						break;
					}
					case 18:
					{
						MapiExceptionAmbiguousAlias innerException = new MapiExceptionAmbiguousAlias("MapiExceptionAmbiguousAlias", 0, 0, null, null);
						result = new StoragePermanentException(new LocalizedString("MapiExceptionAmbiguousAlias"), innerException);
						break;
					}
					case 19:
						result = new SystemException("SystemException");
						break;
					case 20:
						result = new IOException("IOException");
						break;
					case 21:
						result = new MapiExceptionCorruptData("MapiExceptionCorruptData", 0, 0, null, null);
						break;
					case 22:
						result = new IllegalCrossServerConnectionException(new LocalizedString("IllegalCrossServerConnectionException"));
						break;
					case 23:
						result = new MapiExceptionShutoffQuotaExceeded("MapiExceptionShutoffQuotaExceeded", 0, 0, null, null);
						break;
					case 24:
						result = new MapiExceptionNoSupport("MapiExceptionNoSupport", 0, 0, null, null);
						break;
					case 25:
						result = new TextConvertersException("TextConvertersException");
						break;
					case 26:
					{
						MapiExceptionNoAccess innerException2 = new MapiExceptionNoAccess("MapiExceptionNoAccess", 0, 0, null, null);
						result = new StoragePermanentException(new LocalizedString("MapiExceptionNoAccess"), innerException2);
						break;
					}
					case 27:
					{
						Thread.Sleep(2718);
						MapiExceptionTimeout exception = MapiExceptionHelper.TimeoutException("MapiExceptionTimeout", null);
						result = StorageGlobals.TranslateMapiException(new LocalizedString("MapiExceptionTimeout"), exception, null, null, "MapiExceptionTimeout", new object[0]);
						break;
					}
					}
				}
			}
			return result;
		}

		// Token: 0x02000020 RID: 32
		internal enum LIDs : uint
		{
			// Token: 0x04000160 RID: 352
			PushSubscriptionTermination = 2833657149U,
			// Token: 0x04000161 RID: 353
			ADExceptionDuringCallContextConstructor = 3286641981U,
			// Token: 0x04000162 RID: 354
			CallContextCreateCallContext = 3789958461U,
			// Token: 0x04000163 RID: 355
			ADTransientExceptionLogProxyFailure = 3454414141U,
			// Token: 0x04000164 RID: 356
			WebExceptionDuringXmlReaderCreate = 4259720509U,
			// Token: 0x04000165 RID: 357
			CallContextDispose = 3559271741U,
			// Token: 0x04000166 RID: 358
			GetInboxRules = 3274059069U,
			// Token: 0x04000167 RID: 359
			ServiceDiscoveryExceptionOnGetSite = 2703633725U,
			// Token: 0x04000168 RID: 360
			UpdateInboxRules = 2972069181U,
			// Token: 0x04000169 RID: 361
			EventSinkReadEventsFailure = 3534105917U,
			// Token: 0x0400016A RID: 362
			EwsBasicAuthWindowsPrincipalMappingError = 2544250173U,
			// Token: 0x0400016B RID: 363
			IOExceptionWhileProxyingBodyContent = 3594923325U,
			// Token: 0x0400016C RID: 364
			MapiExceptionWhileGettingPermissionSet = 3024497981U,
			// Token: 0x0400016D RID: 365
			WebClientQueryStringPropertyBase_GetMailboxParameter = 3158715709U,
			// Token: 0x0400016E RID: 366
			GetUserAvailability_GetUserAvailabilityFromRequest = 3309710653U,
			// Token: 0x0400016F RID: 367
			LogDatapoint = 3804638525U,
			// Token: 0x04000170 RID: 368
			FindCountLimit_ChangeValue = 3913690429U,
			// Token: 0x04000171 RID: 369
			SearchTimeoutInMilliseconds_ChangeValue = 3200658749U,
			// Token: 0x04000172 RID: 370
			ActAsUserRequirement_ChangeValue = 2328243517U,
			// Token: 0x04000173 RID: 371
			ConvertId_SleepTime_ChangeValue = 2647010621U,
			// Token: 0x04000174 RID: 372
			SkipTokenSerializationCheck_ChangeValue = 3167104317U,
			// Token: 0x04000175 RID: 373
			UploadItems_FastTransferProxyRequestFailure = 3351653693U,
			// Token: 0x04000176 RID: 374
			UploadItems_MapiMessageSaveChangesFailure = 3217435965U,
			// Token: 0x04000177 RID: 375
			GetFolder_IllegalCrossServerConnection = 3116772669U,
			// Token: 0x04000178 RID: 376
			ConversationPreviewFailure = 3137744189U,
			// Token: 0x04000179 RID: 377
			BodyConversionFailure = 3842387261U,
			// Token: 0x0400017A RID: 378
			NormalizedBodyConversionFailure = 2231774525U,
			// Token: 0x0400017B RID: 379
			UniqueBodyConversionFailure = 3305516349U,
			// Token: 0x0400017C RID: 380
			MapiExceptionNoAccessError = 3976604989U,
			// Token: 0x0400017D RID: 381
			GetItemCalendarIdRaiseException = 2969972029U,
			// Token: 0x0400017E RID: 382
			GetConversationItemsMailSubjectRaiseException = 4043713853U,
			// Token: 0x0400017F RID: 383
			GetItemMapiTimeoutError = 3238407485U,
			// Token: 0x04000180 RID: 384
			MultiStepServiceCommandPreExecuteMapiTimeoutError = 3716558141U,
			// Token: 0x04000181 RID: 385
			GetConversationItemsItemPartLoadFailed = 4177931581U,
			// Token: 0x04000182 RID: 386
			GetAttachmentSizeLimit_ChangeValue = 2659593533U,
			// Token: 0x04000183 RID: 387
			OAuthIdentityActAsUserNullSid = 2475044157U,
			// Token: 0x04000184 RID: 388
			AddAggregatedAccountNewSyncRequest_ChangeValue = 3951439165U,
			// Token: 0x04000185 RID: 389
			RemoveggregatedAccountNewSyncRequest_ChangeValue = 2340826429U,
			// Token: 0x04000186 RID: 390
			MapiExceptionMaxObjsExceededInGetItem_ChangeValue = 4186320189U
		}
	}
}
