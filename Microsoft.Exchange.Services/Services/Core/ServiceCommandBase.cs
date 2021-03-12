using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200028A RID: 650
	internal abstract class ServiceCommandBase
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x00052624 File Offset: 0x00050824
		// (set) Token: 0x060010E3 RID: 4323 RVA: 0x0005262C File Offset: 0x0005082C
		internal int CurrentStep { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x00052635 File Offset: 0x00050835
		// (set) Token: 0x060010E5 RID: 4325 RVA: 0x0005263D File Offset: 0x0005083D
		internal CallContext CallContext { get; private set; }

		// Token: 0x060010E6 RID: 4326 RVA: 0x00052646 File Offset: 0x00050846
		protected virtual IParticipantResolver ConstructParticipantResolver()
		{
			return Microsoft.Exchange.Services.Core.Types.ParticipantResolver.Create(this.CallContext, int.MaxValue);
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00052658 File Offset: 0x00050858
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x00052660 File Offset: 0x00050860
		private protected IdConverter IdConverter { protected get; private set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00052669 File Offset: 0x00050869
		protected IParticipantResolver ParticipantResolver
		{
			get
			{
				if (this.participantResolver == null)
				{
					this.participantResolver = this.ConstructParticipantResolver();
				}
				return this.participantResolver;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x00052685 File Offset: 0x00050885
		protected MailboxSession MailboxIdentityMailboxSession
		{
			get
			{
				if (this.mailboxIdentityMailboxSession == null)
				{
					this.mailboxIdentityMailboxSession = this.GetMailboxIdentityMailboxSession();
				}
				return this.mailboxIdentityMailboxSession;
			}
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x000526A1 File Offset: 0x000508A1
		public ServiceCommandBase(CallContext callContext)
		{
			ServiceCommandBase.ThrowIfNull(callContext, "callContext", "ServiceCommandBase::ServiceCommandBase");
			this.CallContext = callContext;
			this.IdConverter = new IdConverter(callContext);
			this.InternalInitialize();
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x000526D2 File Offset: 0x000508D2
		// (set) Token: 0x060010ED RID: 4333 RVA: 0x000526DA File Offset: 0x000508DA
		internal bool PreExecuteSucceeded { get; private set; }

		// Token: 0x060010EE RID: 4334 RVA: 0x000526E4 File Offset: 0x000508E4
		internal static Item GetXsoItemForUpdate(IdAndSession idAndSession, ToXmlPropertyList propertyList)
		{
			Item xsoItem = ServiceCommandBase.GetXsoItem(idAndSession.Session, idAndSession.Id, propertyList);
			return ServiceCommandBase.OpenForUpdate(xsoItem);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0005270C File Offset: 0x0005090C
		internal static string GetFolderLogString(StoreObjectId folderId, StoreSession session)
		{
			string result;
			if (!(session is MailboxSession))
			{
				result = "Other";
			}
			else if (ServiceCommandBase.IsDefaultFolderId(folderId, session, DefaultFolderType.Inbox))
			{
				result = "Inbox";
			}
			else if (ServiceCommandBase.IsDefaultFolderId(folderId, session, DefaultFolderType.Drafts))
			{
				result = "Drafts";
			}
			else if (ServiceCommandBase.IsDefaultFolderId(folderId, session, DefaultFolderType.SentItems))
			{
				result = "SentItems";
			}
			else if (ServiceCommandBase.IsDefaultFolderId(folderId, session, DefaultFolderType.DeletedItems))
			{
				result = "DeletedItems";
			}
			else
			{
				result = "Other";
			}
			return result;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0005277C File Offset: 0x0005097C
		internal static Item GetXsoItem(StoreSession session, StoreId id, params PropertyDefinition[] properties)
		{
			StoreObjectId asStoreObjectId = IdConverter.GetAsStoreObjectId(id);
			StoreObjectType objectType = asStoreObjectId.ObjectType;
			if (!IdConverter.IsItemId(asStoreObjectId))
			{
				throw new CannotUseFolderIdForItemIdException();
			}
			Item item = null;
			bool flag = false;
			Item result;
			try
			{
				try
				{
					item = Item.Bind(session, id, properties);
					if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
					{
						ServiceCommandBase.RebindAsMessageIfNecessary(ref item, properties);
					}
					XsoDataConverter.VerifyObjectTypeAssumptions(objectType, item);
				}
				catch (WrongObjectTypeException innerException)
				{
					throw new InvalidStoreIdException(CoreResources.IDs.ErrorInvalidId, innerException);
				}
				catch (StoragePermanentException ex)
				{
					if (ex.InnerException is MapiExceptionNoSupport || ex.InnerException is MapiExceptionCallFailed)
					{
						if (ExTraceGlobals.ServiceCommandBaseCallTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder = new StringBuilder();
							if (properties == null)
							{
								stringBuilder.AppendLine("<Property Array is NULL>");
							}
							else
							{
								foreach (PropertyDefinition propertyDefinition in properties)
								{
									stringBuilder.AppendLine((propertyDefinition == null) ? "<NULL>" : propertyDefinition.ToString());
								}
							}
							ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug<string, string, string>(0L, "[ServiceCommandBase::GetXsoItem] Encountered StoragePermanentException when trying to bind to an item.  Inner exception class: {0}\r\n, Inner exception message: {1}\r\n,Properties fetched: \r\n{2}", ex.InnerException.GetType().FullName, ex.InnerException.Message, stringBuilder.ToString());
						}
						throw new ObjectCorruptException(ex, true);
					}
					if (ExTraceGlobals.ServiceCommandBaseCallTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						string arg = (ex.InnerException == null) ? string.Empty : string.Format(CultureInfo.InvariantCulture, ".  Inner exception was Class: {0}, Message {1}", new object[]
						{
							ex.InnerException.GetType().FullName,
							ex.InnerException.Message
						});
						ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug<string, string, string>(0L, "[ServiceCommandBase::GetXsoItem] encountered exception - Class: {0}, Message: {1}{2}", ex.GetType().FullName, ex.Message, arg);
					}
					throw;
				}
				XsoDataConverter.VerifyObjectTypeAssumptions(objectType, item);
				flag = true;
				result = item;
			}
			finally
			{
				if (!flag && item != null)
				{
					item.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00052998 File Offset: 0x00050B98
		protected static bool IsDefaultFolderId(StoreObjectId folderId, StoreSession session, DefaultFolderType type)
		{
			StoreObjectId defaultFolderId = session.GetDefaultFolderId(type);
			return defaultFolderId != null && defaultFolderId.Equals(folderId);
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x000529BC File Offset: 0x00050BBC
		protected static ExDateTime? ParseExDateTimeString(string dateTime)
		{
			if (!string.IsNullOrEmpty(dateTime))
			{
				return new ExDateTime?(ExDateTimeConverter.ParseTimeZoneRelated(dateTime, EWSSettings.RequestTimeZone));
			}
			return null;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x000529EC File Offset: 0x00050BEC
		protected static DateTime? GetUtcDateTime(ExDateTime? exDateTime)
		{
			if (exDateTime != null)
			{
				return new DateTime?(exDateTime.Value.UniversalTime);
			}
			return null;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00052A20 File Offset: 0x00050C20
		private static void RebindAsMessageIfNecessary(ref Item item, PropertyDefinition[] properties)
		{
			if (item is CalendarItemBase || item is ContactBase || item is MessageItem || item is PostItem || item is Task)
			{
				return;
			}
			using (Item item2 = item)
			{
				item = Item.BindAsMessage(item2.Session, item2.Id, properties);
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00052A90 File Offset: 0x00050C90
		internal static Item GetXsoItem(StoreSession session, StoreId id, ToXmlPropertyList propertyList)
		{
			return ServiceCommandBase.GetXsoItem(session, id, propertyList.GetPropertyDefinitions());
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00052A9F File Offset: 0x00050C9F
		internal static Folder GetXsoFolder(StoreSession session, StoreId id, ToXmlPropertyList propertyList)
		{
			return ServiceCommandBase.GetXsoFolder(session, id, ref propertyList);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00052AAC File Offset: 0x00050CAC
		internal static Folder GetXsoFolder(StoreSession session, StoreId id, ref ToXmlPropertyList propertyList)
		{
			StoreObjectId asStoreObjectId = IdConverter.GetAsStoreObjectId(id);
			StoreObjectType objectType = asStoreObjectId.ObjectType;
			if (!asStoreObjectId.IsFolderId)
			{
				throw new CannotUseItemIdForFolderIdException();
			}
			Folder folder = Folder.Bind(session, id, propertyList.GetPropertyDefinitions());
			if (folder is SearchFolder)
			{
				propertyList = XsoDataConverter.GetPropertyList(folder, propertyList.ResponseShape);
				folder.Load(propertyList.GetPropertyDefinitions());
			}
			try
			{
				XsoDataConverter.VerifyObjectTypeAssumptions(objectType, folder);
			}
			catch (ObjectNotFoundException)
			{
				folder.Dispose();
				throw;
			}
			if (objectType == StoreObjectType.Folder && folder.Id.ObjectId.ObjectType != objectType)
			{
				propertyList = XsoDataConverter.GetPropertyList(folder, propertyList.ResponseShape);
				folder.Load(propertyList.GetPropertyDefinitions());
			}
			return folder;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00052B60 File Offset: 0x00050D60
		internal static Folder GetXsoFolder(StoreSession session, StoreId id, ref ToServiceObjectPropertyList propertyList)
		{
			StoreObjectId asStoreObjectId = IdConverter.GetAsStoreObjectId(id);
			StoreObjectType objectType = asStoreObjectId.ObjectType;
			if (!asStoreObjectId.IsFolderId)
			{
				throw new CannotUseItemIdForFolderIdException();
			}
			Folder folder = Folder.Bind(session, id, propertyList.GetPropertyDefinitions());
			if (folder is SearchFolder)
			{
				propertyList = XsoDataConverter.GetToServiceObjectPropertyList(folder, propertyList.ResponseShape);
				folder.Load(propertyList.GetPropertyDefinitions());
			}
			try
			{
				XsoDataConverter.VerifyObjectTypeAssumptions(objectType, folder);
			}
			catch (ObjectNotFoundException)
			{
				folder.Dispose();
				throw;
			}
			if (objectType == StoreObjectType.Folder && folder.Id.ObjectId.ObjectType != objectType)
			{
				propertyList = XsoDataConverter.GetToServiceObjectPropertyList(folder, propertyList.ResponseShape);
				folder.Load(propertyList.GetPropertyDefinitions());
			}
			return folder;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00052C14 File Offset: 0x00050E14
		internal FolderId GetServiceFolderIdFromStoreId(StoreId storeId, IdAndSession idAndSession)
		{
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeId, idAndSession, null);
			return new FolderId
			{
				Id = concatenatedId.Id,
				ChangeKey = concatenatedId.ChangeKey
			};
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00052C4C File Offset: 0x00050E4C
		internal ItemId GetServiceItemIdFromStoreId(StoreId storeId, IdAndSession idAndSession)
		{
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeId, idAndSession, null);
			return new ItemId
			{
				Id = concatenatedId.Id,
				ChangeKey = concatenatedId.ChangeKey
			};
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00052C83 File Offset: 0x00050E83
		public IEwsBudget CallerBudget
		{
			get
			{
				return this.CallContext.Budget;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00052C90 File Offset: 0x00050E90
		public int ObjectsChanged
		{
			get
			{
				return this.objectsChanged;
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00052C98 File Offset: 0x00050E98
		internal static void ThrowIfNull(object objectToCheck, string parameterName, string methodName)
		{
			if (objectToCheck == null)
			{
				string message = ServiceCommandBase.BuildExceptionMessage(methodName, parameterName, "is null.");
				throw new ArgumentNullException(parameterName, message);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00052CBD File Offset: 0x00050EBD
		internal virtual bool SupportsExternalUsers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00052CC0 File Offset: 0x00050EC0
		internal virtual Offer ExpectedOffer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x00052CC3 File Offset: 0x00050EC3
		internal virtual bool IsDelayExecuted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x00052CC8 File Offset: 0x00050EC8
		internal virtual TimeSpan? MaxExecutionTime
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00052CDE File Offset: 0x00050EDE
		internal static void ThrowIfNullOrEmpty(string stringToCheck, string parameterName, string methodName)
		{
			ServiceCommandBase.ThrowIfNull(stringToCheck, parameterName, methodName);
			ServiceCommandBase.ThrowIfEmpty(stringToCheck, parameterName, methodName);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00052CF0 File Offset: 0x00050EF0
		protected static void ThrowIfNullOrEmpty<T>(IList<T> list, string parameterName, string methodName)
		{
			ServiceCommandBase.ThrowIfNull(list, parameterName, methodName);
			ServiceCommandBase.ThrowIfEmpty<T>(list, parameterName, methodName);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00052D04 File Offset: 0x00050F04
		protected static void ThrowIfEmpty<T>(IList<T> list, string parameterName, string methodName)
		{
			if (list.Count == 0)
			{
				string message = ServiceCommandBase.BuildExceptionMessage(methodName, parameterName, "is empty.");
				throw new ArgumentException(message, parameterName);
			}
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00052D30 File Offset: 0x00050F30
		protected static void ThrowIfEmpty(string stringToCheck, string parameterName, string methodName)
		{
			if (stringToCheck.Length == 0)
			{
				string message = ServiceCommandBase.BuildExceptionMessage(methodName, parameterName, "is empty.");
				throw new ArgumentException(message, parameterName);
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00052D5C File Offset: 0x00050F5C
		protected static void RequireUpToDateItem(StoreId incomingId, Item item)
		{
			if (item.Id == null)
			{
				item.OpenAsReadWrite();
				VersionedId id = item.Id;
			}
			ServiceCommandBase.RequireUpToDateObject(incomingId, item);
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00052D88 File Offset: 0x00050F88
		protected static bool IsAssociated(Item storeItem)
		{
			MessageFlags messageFlags = (MessageFlags)storeItem[MessageItemSchema.Flags];
			return (messageFlags & MessageFlags.IsAssociated) == MessageFlags.IsAssociated;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00052DB0 File Offset: 0x00050FB0
		protected static Item GetXsoItemForUpdate(IdAndSession idAndSession, params PropertyDefinition[] properties)
		{
			Item xsoItem = ServiceCommandBase.GetXsoItem(idAndSession.Session, idAndSession.Id, properties);
			return ServiceCommandBase.OpenForUpdate(xsoItem);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00052DD6 File Offset: 0x00050FD6
		protected static bool IsOrganizerMeeting(CalendarItemBase calendarItemBase)
		{
			return calendarItemBase != null && calendarItemBase.IsOrganizer();
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00052DE3 File Offset: 0x00050FE3
		protected DelegateSessionHandleWrapper GetDelegateSessionHandleWrapper(IdAndSession idAndSession)
		{
			return this.GetDelegateSessionHandleWrapper(idAndSession, false);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00052DF0 File Offset: 0x00050FF0
		protected DelegateSessionHandleWrapper GetDelegateSessionHandleWrapper(IdAndSession idAndSession, bool checkSameAccountForOwnerLogon)
		{
			if (idAndSession == null)
			{
				return null;
			}
			MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
			if (mailboxSession == null || this.CallContext.AccessingPrincipal == null || (mailboxSession.LogonType == LogonType.Owner && (!checkSameAccountForOwnerLogon || string.Equals(mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), this.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), StringComparison.OrdinalIgnoreCase))) || mailboxSession.LogonType == LogonType.Admin || mailboxSession.LogonType == LogonType.SystemService || !ExchangeVersionDeterminer.MatchesLocalServerVersion(this.CallContext.AccessingPrincipal.MailboxInfo.Location.ServerVersion))
			{
				return null;
			}
			this.LogDelegateSession(mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
			return new DelegateSessionHandleWrapper(this.MailboxIdentityMailboxSession.GetDelegateSessionHandleForEWS(mailboxSession.MailboxOwner));
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00052EE7 File Offset: 0x000510E7
		protected MailboxSession GetMailboxSession(string smtpAddress)
		{
			return this.CallContext.SessionCache.GetMailboxSessionBySmtpAddress(smtpAddress);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00052EFA File Offset: 0x000510FA
		protected MailboxSession GetMailboxIdentityMailboxSession()
		{
			return this.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00052F0C File Offset: 0x0005110C
		protected void SetProperties(StoreObject storeObject, ServiceObject serviceObject)
		{
			if (serviceObject.LoadedProperties.Count > 0)
			{
				string className = storeObject.ClassName;
				XsoDataConverter.SetProperties(storeObject, serviceObject, this.IdConverter);
				ServiceCommandBase.ValidateClassChange(storeObject, className);
			}
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00052F42 File Offset: 0x00051142
		protected virtual void UpdateProperties(StoreObject storeObject, PropertyUpdate[] propertyUpdates, bool suppressReadReceipts)
		{
			this.UpdateProperties(storeObject, propertyUpdates, suppressReadReceipts, null);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00052F50 File Offset: 0x00051150
		protected virtual void UpdateProperties(StoreObject storeObject, PropertyUpdate[] propertyUpdates, bool suppressReadReceipts, IFeaturesManager featuresManager)
		{
			string className = storeObject.ClassName;
			try
			{
				XsoDataConverter.UpdateProperties(storeObject, propertyUpdates, this.IdConverter, suppressReadReceipts, featuresManager);
			}
			catch (PropertyValidationException ex)
			{
				throw new PropertyUpdateException(SearchSchemaMap.GetPropertyPaths(ex.PropertyValidationErrors), ex);
			}
			ServiceCommandBase.ValidateClassChange(storeObject, className);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00052FA0 File Offset: 0x000511A0
		protected MailboxTarget GetMailboxTarget(StoreSession session)
		{
			if (Util.IsArchiveMailbox(session))
			{
				return MailboxTarget.Archive;
			}
			if (session is PublicFolderSession)
			{
				return MailboxTarget.PublicFolder;
			}
			if (session is MailboxSession && !object.Equals(this.MailboxIdentityMailboxSession, session))
			{
				return MailboxTarget.SharedFolder;
			}
			return MailboxTarget.Primary;
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00052FD0 File Offset: 0x000511D0
		internal static void ValidateClassChange(StoreObject storeObject, string preUpdateClassName)
		{
			StoreObjectType objectType = ObjectClass.GetObjectType(preUpdateClassName);
			StoreObjectType objectType2 = ObjectClass.GetObjectType(storeObject.ClassName);
			ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug<string, string>(0L, "ServiceCommandBase.ValidateClassChange().  pre update class name: '{0}' post update class name: {1}", preUpdateClassName, storeObject.ClassName);
			if (objectType != objectType2)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericError(CallContext.Current.ProtocolLog, "ObjectTypeChanged", string.Format("{0}:{1} {2}:{3}", new object[]
				{
					objectType,
					objectType2,
					preUpdateClassName,
					storeObject.ClassName
				}));
				if (objectType == StoreObjectType.Unknown)
				{
					if (ServiceCommandBase.IsBaseObjectTypeChange(storeObject, objectType2))
					{
						ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug(0L, "ServiceCommandBase.ValidateClassChange() detected change to base object.");
						throw new ObjectTypeChangedException();
					}
				}
				else
				{
					if (objectType == StoreObjectType.Message)
					{
						if (objectType2 == StoreObjectType.MeetingMessage || objectType2 == StoreObjectType.MeetingRequest || objectType2 == StoreObjectType.MeetingResponse || objectType2 == StoreObjectType.MeetingCancellation)
						{
							return;
						}
						if (objectType2 == StoreObjectType.Report)
						{
							return;
						}
					}
					if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
					{
						if (objectType == StoreObjectType.Unknown && objectType2 == StoreObjectType.Message)
						{
							return;
						}
						if (objectType == StoreObjectType.Message && objectType2 == StoreObjectType.Unknown)
						{
							return;
						}
					}
					if (ObjectClass.IsCalendarItemOccurrence(preUpdateClassName) && ObjectClass.IsRecurrenceException(storeObject.ClassName))
					{
						return;
					}
					ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug(0L, "ServiceCommandBase.ValidateClassChange() detected change of object type.");
					throw new ObjectTypeChangedException();
				}
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000530E8 File Offset: 0x000512E8
		private static bool IsBaseObjectTypeChange(StoreObject storeObject, StoreObjectType postUpdateObjectType)
		{
			bool flag = storeObject is Folder;
			bool flag2 = false;
			switch (postUpdateObjectType)
			{
			case StoreObjectType.Folder:
			case StoreObjectType.CalendarFolder:
			case StoreObjectType.ContactsFolder:
			case StoreObjectType.TasksFolder:
			case StoreObjectType.NotesFolder:
			case StoreObjectType.JournalFolder:
			case StoreObjectType.SearchFolder:
			case StoreObjectType.OutlookSearchFolder:
				flag2 = true;
				break;
			}
			return flag != flag2 || (!flag && ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1) && postUpdateObjectType != StoreObjectType.Unknown && postUpdateObjectType != StoreObjectType.Message);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00053154 File Offset: 0x00051354
		private static string BuildExceptionMessage(string methodName, string parameterName, string message)
		{
			if (!string.IsNullOrEmpty(methodName))
			{
				return string.Format(CultureInfo.InvariantCulture, "[{0}] {1} {2}", new object[]
				{
					methodName,
					parameterName,
					message
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "{1} {2}", new object[]
			{
				parameterName,
				message
			});
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000531AC File Offset: 0x000513AC
		private static void RequireUpToDateObject(StoreId incomingId, StoreObject storeObject)
		{
			VersionedId versionedId = incomingId as VersionedId;
			if (versionedId == null)
			{
				throw new StaleObjectException();
			}
			VersionedId id = storeObject.Id;
			byte[] array = id.ChangeKeyAsByteArray();
			byte[] array2 = versionedId.ChangeKeyAsByteArray();
			if (array.Length != array2.Length)
			{
				throw new StaleObjectException();
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != array2[i])
				{
					throw new StaleObjectException();
				}
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00053210 File Offset: 0x00051410
		private static Item OpenForUpdate(Item storeItem)
		{
			bool flag = false;
			try
			{
				storeItem.OpenAsReadWrite();
				flag = true;
			}
			finally
			{
				if (!flag && storeItem != null)
				{
					storeItem.Dispose();
				}
			}
			return storeItem;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00053248 File Offset: 0x00051448
		protected virtual void SaveXsoFolder(Folder xsoFolder)
		{
			this.ExecuteFolderSave(xsoFolder);
			xsoFolder.Load(null);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00053258 File Offset: 0x00051458
		protected virtual void LogDelegateSession(string principal)
		{
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00053278 File Offset: 0x00051478
		private void ExecuteFolderSave(Folder folder)
		{
			FolderSaveResult folderSaveResult = null;
			this.ExecuteStoreObjectSave(delegate
			{
				folderSaveResult = folder.Save();
			}, false);
			this.ValidateFolderSaveResult(folderSaveResult);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000532B8 File Offset: 0x000514B8
		private void ValidateFolderSaveResult(FolderSaveResult folderSaveResult)
		{
			if (folderSaveResult.OperationResult == OperationResult.Succeeded)
			{
				return;
			}
			string arg = (folderSaveResult.OperationResult == OperationResult.Failed) ? "FAILED" : "PARTIALLY SUCCESSFUL";
			if (folderSaveResult.PropertyErrors == null)
			{
				ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug<string>((long)this.GetHashCode(), "ServiceCommandBase.SaveXsoFolder Folder.Save() had a '{0}' operation result with <NULL> property errors.", arg);
			}
			else
			{
				ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug<string, int>((long)this.GetHashCode(), "ServiceCommandBase.SaveXsoFolder Folder.Save() had a '{0}' operation result with '{1}' property errors.", arg, folderSaveResult.PropertyErrors.Length);
			}
			if (folderSaveResult.PropertyErrors != null)
			{
				foreach (PropertyError propertyError in folderSaveResult.PropertyErrors)
				{
					ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug((long)this.GetHashCode(), "PropertyError: Class:{0}, PropDefName: {1}, PropErrorCode: {2}, ErrorDescription: {3}", new object[]
					{
						propertyError.GetType(),
						(propertyError.PropertyDefinition == null) ? "NULL" : propertyError.PropertyDefinition.Name,
						propertyError.PropertyErrorCode,
						propertyError.PropertyErrorDescription
					});
				}
				throw new ObjectSavePropertyErrorException(SearchSchemaMap.GetPropertyPaths(folderSaveResult.PropertyErrors), PropertyError.ToException(folderSaveResult.PropertyErrors), false);
			}
			throw new FolderSaveException();
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x000533D0 File Offset: 0x000515D0
		protected void ExecuteStoreObjectSave(ServiceCommandBase.SaveStoreObject saveStoreObject, bool useItemError)
		{
			try
			{
				saveStoreObject();
			}
			catch (DumpsterOperationException innerException)
			{
				throw new ObjectSaveException(innerException, useItemError);
			}
			catch (PropertyErrorException ex)
			{
				throw new ObjectSavePropertyErrorException(SearchSchemaMap.GetPropertyPaths(ex.PropertyErrors), ex, useItemError);
			}
			catch (PropertyValidationException ex2)
			{
				throw new ObjectSavePropertyErrorException(SearchSchemaMap.GetPropertyPaths(ex2.PropertyValidationErrors), ex2, useItemError);
			}
			catch (ObjectValidationException ex3)
			{
				throw new ObjectSavePropertyErrorException(SearchSchemaMap.GetPropertyPaths(ex3.Errors), ex3, useItemError);
			}
			catch (StoragePermanentException ex4)
			{
				bool flag = ex4.InnerException != null;
				ExTraceGlobals.ExceptionTracer.TraceError<StoragePermanentException>((long)this.GetHashCode(), "[ServiceCommandBase::ExecuteStoreObjectSave] encountered StoragePermanentException: {0}", ex4);
				if (!flag)
				{
					throw;
				}
				if (ex4.InnerException is MapiExceptionCrossPostDenied)
				{
					throw new ObjectSaveException(ex4, useItemError);
				}
				if (ex4.InnerException is MapiExceptionInvalidParameter)
				{
					throw new ObjectSaveException(ex4, useItemError);
				}
				if (ex4.InnerException is MapiExceptionJetErrorColumnTooBig)
				{
					throw new ObjectSaveException(ex4, useItemError);
				}
				if (ex4.InnerException is MapiExceptionJetWarningColumnMaxTruncated)
				{
					throw new ObjectSaveException(ex4, useItemError);
				}
				if (ex4.InnerException is MapiExceptionUnexpectedType)
				{
					throw new ObjectCorruptException(ex4, useItemError);
				}
				if (ex4.InnerException is MapiExceptionFailCallback)
				{
					throw new ObjectSaveException(ex4, useItemError);
				}
				throw;
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00053550 File Offset: 0x00051750
		protected ConflictResolutionResult ExecuteItemSave(ServiceCommandBase.SaveItem saveItem, ConflictResolutionType conflictResolutionType)
		{
			ConflictResolutionResult conflictResolutionResult = null;
			SaveMode saveMode = this.GetSaveMode(conflictResolutionType);
			this.ExecuteStoreObjectSave(delegate
			{
				conflictResolutionResult = saveItem(saveMode);
			}, true);
			this.ValidateConflictResolutionResult(conflictResolutionResult, saveMode);
			return conflictResolutionResult;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000535AC File Offset: 0x000517AC
		protected void ValidateConflictResolutionResult(ConflictResolutionResult conflictResolutionResult, SaveMode saveMode)
		{
			if (saveMode != SaveMode.NoConflictResolution)
			{
				if (conflictResolutionResult.PropertyConflicts != null)
				{
					ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug<int>((long)this.GetHashCode(), "ServiceCommandBase.SaveXsoItem item.Save returned '{0}' property conflicts.", conflictResolutionResult.PropertyConflicts.Length);
					foreach (PropertyConflict propertyConflict in conflictResolutionResult.PropertyConflicts)
					{
						ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug((long)this.GetHashCode(), "Property conflict: DisplayName: '{0}', Resolvable: '{1}', OriginalValue: '{2}', ClientValue: '{3}', ServerValue: '{4}', ResolvedValue: '{5}'", new object[]
						{
							(propertyConflict.PropertyDefinition != null) ? propertyConflict.PropertyDefinition.Name : ServiceDiagnostics.HandleNullObjectTrace(propertyConflict.PropertyDefinition),
							propertyConflict.ConflictResolvable,
							ServiceDiagnostics.HandleNullObjectTrace(propertyConflict.OriginalValue),
							ServiceDiagnostics.HandleNullObjectTrace(propertyConflict.ClientValue),
							ServiceDiagnostics.HandleNullObjectTrace(propertyConflict.ServerValue),
							ServiceDiagnostics.HandleNullObjectTrace(propertyConflict.ResolvedValue)
						});
					}
				}
				if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					throw new IrresolvableConflictException(conflictResolutionResult.PropertyConflicts);
				}
				SaveResult saveStatus = conflictResolutionResult.SaveStatus;
			}
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000536C4 File Offset: 0x000518C4
		protected ConflictResolutionResult SaveXsoItem(Item xsoItem, ConflictResolutionType conflictResolutionType)
		{
			return this.SaveXsoItem(xsoItem, (SaveMode saveModeDelegate) => xsoItem.Save(saveModeDelegate), conflictResolutionType, null);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000536F8 File Offset: 0x000518F8
		protected ConflictResolutionResult SaveXsoItem(Item xsoItem, ServiceCommandBase.SaveItem saveItem, ConflictResolutionType conflictResolutionType, PropertyDefinition[] propsToLoad)
		{
			ConflictResolutionResult result;
			if (xsoItem.IsDirty)
			{
				result = this.ExecuteItemSave(saveItem, conflictResolutionType);
				xsoItem.Load(propsToLoad);
			}
			else
			{
				result = new ConflictResolutionResult(SaveResult.Success, null);
			}
			List<IPostSavePropertyCommand> list;
			if (EWSSettings.PostSavePropertyCommands.TryGetValue(xsoItem.StoreObjectId, out list))
			{
				foreach (IPostSavePropertyCommand postSavePropertyCommand in list)
				{
					postSavePropertyCommand.ExecutePostSaveOperation(xsoItem);
				}
			}
			return result;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00053780 File Offset: 0x00051980
		protected SaveMode GetSaveMode(ConflictResolutionType conflictResolutionType)
		{
			SaveMode result = SaveMode.FailOnAnyConflict;
			switch (conflictResolutionType)
			{
			case ConflictResolutionType.NeverOverwrite:
				result = SaveMode.FailOnAnyConflict;
				break;
			case ConflictResolutionType.AutoResolve:
				result = SaveMode.ResolveConflicts;
				break;
			case ConflictResolutionType.AlwaysOverwrite:
				result = SaveMode.NoConflictResolutionForceSave;
				break;
			}
			return result;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x000537B0 File Offset: 0x000519B0
		protected void LoadServiceObject(ServiceObject serviceObject, StoreObject storeObject, IdAndSession idAndSession, ResponseShape responseShape)
		{
			ServiceCommandBase.LoadServiceObject(serviceObject, storeObject, idAndSession, responseShape, null);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x000537C0 File Offset: 0x000519C0
		internal static void LoadServiceObject(ServiceObject serviceObject, StoreObject storeObject, IdAndSession idAndSession, ResponseShape responseShape, ToServiceObjectPropertyList toServiceObjectPropertyList)
		{
			if (toServiceObjectPropertyList == null)
			{
				toServiceObjectPropertyList = XsoDataConverter.GetToServiceObjectPropertyList(storeObject, responseShape);
			}
			PropertyDefinition[] propertyDefinitions = toServiceObjectPropertyList.GetPropertyDefinitions();
			storeObject.Load(propertyDefinitions);
			toServiceObjectPropertyList.ConvertStoreObjectPropertiesToServiceObject(idAndSession, storeObject, serviceObject);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x000537F4 File Offset: 0x000519F4
		protected void RequireExchange14OrLater()
		{
			ExchangePrincipal accessingPrincipal = this.CallContext.AccessingPrincipal;
			if (accessingPrincipal == null)
			{
				ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug((long)this.GetHashCode(), "ServiceCommandBase.RequireExchange14OrLater: AccessingPrincipal is null, caller is allowed.");
				return;
			}
			ServerVersion serverVersion = new ServerVersion(accessingPrincipal.MailboxInfo.Location.ServerVersion);
			ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug<ServerVersion, ServerVersion>((long)this.GetHashCode(), "ServiceCommandBase.RequireExchange14OrLater: caller version is {0}, required server version is {1}", serverVersion, ServiceCommandBase.exchange14ServerVersion);
			if (serverVersion.Major < ServiceCommandBase.exchange14ServerVersion.Major)
			{
				ExTraceGlobals.GetMailTipsCallTracer.TraceError((long)this.GetHashCode(), "ServiceCommandBase.RequireExchange14OrLater: access not allowed.");
				throw new ServiceInvalidOperationException((CoreResources.IDs)3336001063U);
			}
			ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug((long)this.GetHashCode(), "ServiceCommandBase.RequireExchange14OrLater: access allowed.");
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000538A7 File Offset: 0x00051AA7
		protected void SafeSetProtocolLogMetadata(Enum key, object value)
		{
			if (this.CallContext != null)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(this.CallContext.ProtocolLog, key, value);
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x000538C3 File Offset: 0x00051AC3
		protected void SafeAppendLogGenericInfo(string key, object value)
		{
			if (this.CallContext != null)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(this.CallContext.ProtocolLog, key, value);
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000539C0 File Offset: 0x00051BC0
		internal bool PreExecute()
		{
			bool success = false;
			ServiceDiagnostics.SendWatsonReportOnUnhandledException(delegate
			{
				this.serviceRequestId = Trace.TraceCasStart(CasTraceEventType.Ews);
				ExternalCallContext externalCallContext = this.CallContext as ExternalCallContext;
				if (externalCallContext != null)
				{
					bool flag = true;
					if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
					{
						ExTraceGlobals.ExternalUserTracer.TraceError(0L, "External user calls are only supported on Exchange2010 version and later");
						flag = false;
					}
					if (!this.SupportsExternalUsers)
					{
						ExTraceGlobals.ExternalUserTracer.TraceError<ServiceCommandBase>(0L, "Service command {0} does not support external user calls", this);
						flag = false;
					}
					if (externalCallContext.Offer != this.ExpectedOffer)
					{
						ExTraceGlobals.ExternalUserTracer.TraceError<ServiceCommandBase, Offer>(0L, "Service command {0} expects offer {1}, but received other offer instead.", this, this.ExpectedOffer);
						flag = false;
					}
					if (!flag)
					{
						throw FaultExceptionUtilities.CreateFault(new ServiceAccessDeniedException(), FaultParty.Sender);
					}
				}
				success = this.InternalPreExecute();
			});
			this.PreExecuteSucceeded = success;
			return this.PreExecuteSucceeded;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x00053A04 File Offset: 0x00051C04
		internal virtual int StepCount
		{
			get
			{
				throw new NotImplementedException("ServiceCommandBase.StepCount");
			}
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00053A26 File Offset: 0x00051C26
		internal TaskExecuteResult ExecuteStep()
		{
			return this.ExecuteHelper(delegate
			{
				bool result;
				this.InternalExecuteStep(out result);
				return result;
			});
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00053A3A File Offset: 0x00051C3A
		internal virtual bool InternalPreExecute()
		{
			return true;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00053A3D File Offset: 0x00051C3D
		internal virtual void InternalPostExecute()
		{
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00053A3F File Offset: 0x00051C3F
		internal virtual void InternalExecuteStep(out bool isBatchStopResponse)
		{
			throw new NotImplementedException("ServiceCommandBase.InternalExecuteStep");
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00053A78 File Offset: 0x00051C78
		internal TaskExecuteResult CancelStep(LocalizedException exception)
		{
			return this.ExecuteHelper(delegate
			{
				bool result;
				this.InternalCancelStep(exception, out result);
				return result;
			});
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00053AAB File Offset: 0x00051CAB
		internal virtual void InternalCancelStep(LocalizedException exception, out bool isBatchStopResponse)
		{
			throw new NotImplementedException("ServiceCommandBase.InternalCancelStep");
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00053B04 File Offset: 0x00051D04
		private TaskExecuteResult ExecuteHelper(Func<bool> action)
		{
			bool isBatchStopResponse = false;
			if (this.StepCount > 0)
			{
				ServiceDiagnostics.SendWatsonReportOnUnhandledException(delegate
				{
					try
					{
						isBatchStopResponse = action();
					}
					finally
					{
						this.CurrentStep++;
					}
				});
			}
			if (this.CurrentStep < this.StepCount && !isBatchStopResponse)
			{
				return TaskExecuteResult.StepComplete;
			}
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00053B68 File Offset: 0x00051D68
		internal IExchangeWebMethodResponse PostExecute()
		{
			if (this.PreExecuteSucceeded)
			{
				this.InternalPostExecute();
			}
			IExchangeWebMethodResponse response = this.GetResponse();
			if (response != null)
			{
				this.UpdatePerformanceCounters(response);
				if (!this.IsDelayExecuted)
				{
					this.LogResponseCode(response);
				}
			}
			if (ETWTrace.ShouldTraceCasStop(this.serviceRequestId))
			{
				Global.TraceCasStop(base.GetType(), this.CallContext, this.serviceRequestId);
			}
			return response;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00053BC8 File Offset: 0x00051DC8
		internal virtual void UpdatePerformanceCounters(IExchangeWebMethodResponse response)
		{
			PerformanceMonitor.UpdateResponseCounters(response, this.ObjectsChanged);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00053BD8 File Offset: 0x00051DD8
		internal void LogResponseCode(IExchangeWebMethodResponse response)
		{
			ResponseCodeType errorCodeToLog = response.GetErrorCodeToLog();
			if (errorCodeToLog != ResponseCodeType.NoError)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(this.CallContext.ProtocolLog, ServiceCommonMetadata.ErrorCode, errorCodeToLog);
			}
		}

		// Token: 0x06001132 RID: 4402
		internal abstract IExchangeWebMethodResponse GetResponse();

		// Token: 0x06001133 RID: 4403
		internal abstract ResourceKey[] GetResources();

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x00053C0C File Offset: 0x00051E0C
		// (set) Token: 0x06001135 RID: 4405 RVA: 0x00053C14 File Offset: 0x00051E14
		private protected bool IsRequestTracingEnabled { protected get; private set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x00053C1D File Offset: 0x00051E1D
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x00053C25 File Offset: 0x00051E25
		private protected XmlDocument XmlDocument { protected get; private set; }

		// Token: 0x06001138 RID: 4408 RVA: 0x00053C2E File Offset: 0x00051E2E
		protected void InternalInitialize()
		{
			this.XmlDocument = new SafeXmlDocument();
			this.IsRequestTracingEnabled = this.CallContext.IsRequestTracingEnabled;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00053C4C File Offset: 0x00051E4C
		protected virtual void LogTracesForCurrentRequest()
		{
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00053C4E File Offset: 0x00051E4E
		protected void LogRequestTraces()
		{
			if (this.IsRequestTracingEnabled)
			{
				this.LogTracesForCurrentRequest();
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00053C60 File Offset: 0x00051E60
		protected void SetProperties(StoreObject storeObject, XmlNode serviceItem)
		{
			if (serviceItem.ChildNodes.Count > 0)
			{
				string className = storeObject.ClassName;
				XsoDataConverter.SetProperties(storeObject, (XmlElement)serviceItem, this.IdConverter);
				ServiceCommandBase.ValidateClassChange(storeObject, className);
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00053C9B File Offset: 0x00051E9B
		protected XmlElement CreateServiceItemXml(StoreObject storeObject, IdAndSession idAndSession, ResponseShape responseShape)
		{
			return this.CreateServiceItemXml(storeObject, idAndSession, responseShape, null);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00053CA8 File Offset: 0x00051EA8
		internal XmlElement CreateServiceItemXml(StoreObject storeObject, IdAndSession idAndSession, ResponseShape responseShape, ToXmlPropertyList toXmlPropertyList)
		{
			if (toXmlPropertyList == null)
			{
				toXmlPropertyList = XsoDataConverter.GetPropertyList(storeObject, responseShape);
			}
			PropertyDefinition[] propertyDefinitions = toXmlPropertyList.GetPropertyDefinitions();
			storeObject.Load(propertyDefinitions);
			return toXmlPropertyList.ConvertStoreObjectPropertiesToXml(idAndSession, storeObject, this.XmlDocument);
		}

		// Token: 0x04000C7F RID: 3199
		private Guid serviceRequestId;

		// Token: 0x04000C80 RID: 3200
		private IParticipantResolver participantResolver;

		// Token: 0x04000C81 RID: 3201
		protected int objectsChanged;

		// Token: 0x04000C82 RID: 3202
		protected static readonly ItemResponseShape DefaultItemResponseShape = new ItemResponseShape(ShapeEnum.IdOnly, BodyResponseType.Best, false, null);

		// Token: 0x04000C83 RID: 3203
		protected static readonly ItemResponseShape DefaultItemResponseShapeWithAttachments = new ItemResponseShape(ShapeEnum.IdOnly, BodyResponseType.Best, false, new PropertyPath[]
		{
			new PropertyUri(PropertyUriEnum.Attachments)
		});

		// Token: 0x04000C84 RID: 3204
		protected static readonly FolderResponseShape DefaultFolderResponseShape = new FolderResponseShape(ShapeEnum.IdOnly, null);

		// Token: 0x04000C85 RID: 3205
		private static readonly ServerVersion exchange14ServerVersion = new ServerVersion(14, 0, 0, 0);

		// Token: 0x04000C86 RID: 3206
		private MailboxSession mailboxIdentityMailboxSession;

		// Token: 0x04000C87 RID: 3207
		protected static readonly TraceToHeadersLoggerFactory TraceLoggerFactory = new TraceToHeadersLoggerFactory(VariantConfiguration.InvariantNoFlightingSnapshot.Diagnostics.TraceToHeadersLogger.Enabled);

		// Token: 0x0200028B RID: 651
		// (Invoke) Token: 0x06001141 RID: 4417
		internal delegate void SaveStoreObject();

		// Token: 0x0200028C RID: 652
		// (Invoke) Token: 0x06001145 RID: 4421
		internal delegate ConflictResolutionResult SaveItem(SaveMode saveMode);
	}
}
