using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F12 RID: 3858
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ActivitySession : IActivitySession
	{
		// Token: 0x17002337 RID: 9015
		// (get) Token: 0x060084C8 RID: 33992 RVA: 0x0024470E File Offset: 0x0024290E
		private static IDictionary<DefaultFolderType, string> WellKnownFolderMapping
		{
			get
			{
				return ActivitySession.wellKnownFolderMapping.Member;
			}
		}

		// Token: 0x060084C9 RID: 33993 RVA: 0x0024471A File Offset: 0x0024291A
		private ActivitySession(MailboxSession session, ActivitySession.ClientInfo clientInfo, IActivityLogger activityLogger)
		{
			Util.ThrowOnNullArgument(activityLogger, "activityLogger");
			this.session = session;
			this.clientInfo = clientInfo;
			this.logger = activityLogger;
		}

		// Token: 0x060084CA RID: 33994 RVA: 0x00244758 File Offset: 0x00242958
		public static ActivitySession Create(MailboxSession session)
		{
			Util.ThrowOnNullArgument(session, "session");
			if (!ActivityLogHelper.IsActivityLoggingEnabled(false))
			{
				return null;
			}
			if (session.LogonType == LogonType.Delegated || session.LogonType == LogonType.DelegatedAdmin)
			{
				ExTraceGlobals.SessionTracer.TraceDebug(0L, "Skipping Activity Logging since session is from delegate.");
				return null;
			}
			IActivityLogger activityLogger = ActivityLogger.Create(session);
			if (activityLogger == null)
			{
				ExTraceGlobals.SessionTracer.TraceDebug(0L, "Skipping Activity Logging since the activity logger couldn't be created");
				return null;
			}
			ActivitySession.ClientInfo clientInfo = ActivitySession.ExtractClientInfo(session);
			return new ActivitySession(session, clientInfo, activityLogger);
		}

		// Token: 0x060084CB RID: 33995 RVA: 0x002447CC File Offset: 0x002429CC
		private static ActivitySession.ClientInfo ExtractClientInfo(MailboxSession mailboxSession)
		{
			ActivitySession.ClientInfo clientInfo = new ActivitySession.ClientInfo
			{
				Id = ClientId.Other,
				Version = "15.00.1497.010"
			};
			string text = (mailboxSession == null || mailboxSession.ClientInfoString == null) ? string.Empty : mailboxSession.ClientInfoString;
			if (text.Equals("Client=MSExchangeRPC", StringComparison.OrdinalIgnoreCase) && ActivityContext.GetCurrentActivityScope() != null)
			{
				clientInfo = ActivitySession.ExtractMapiClientInfo(ActivityContext.GetCurrentActivityScope().ClientInfo, clientInfo);
			}
			else if (text.IndexOf("macoutlook", StringComparison.OrdinalIgnoreCase) != -1)
			{
				clientInfo.Id = ClientId.MacOutlook;
			}
			else if (text.IndexOf("Client=OWA", StringComparison.OrdinalIgnoreCase) != -1)
			{
				clientInfo.Id = ClientId.Web;
			}
			else if (text.IndexOf("Client=ActiveSync", StringComparison.OrdinalIgnoreCase) != -1)
			{
				clientInfo.Id = ClientId.Mobile;
			}
			else if (text.StartsWith("Client=Hub Transport", StringComparison.OrdinalIgnoreCase))
			{
				clientInfo.Id = ClientId.Exchange;
			}
			else if (text.StartsWith("Client=POP3/IMAP4;Protocol=POP3", StringComparison.OrdinalIgnoreCase))
			{
				clientInfo.Id = ClientId.POP3;
			}
			else if (text.StartsWith("Client=POP3/IMAP4;Protocol=IMAP4", StringComparison.OrdinalIgnoreCase))
			{
				clientInfo.Id = ClientId.IMAP4;
			}
			else
			{
				clientInfo.Id = ClientId.Other;
			}
			clientInfo.Id = clientInfo.Id.GetServerSideInstrumentationVariant(true);
			return clientInfo;
		}

		// Token: 0x060084CC RID: 33996 RVA: 0x00244910 File Offset: 0x00242B10
		private static ActivitySession.ClientInfo ExtractMapiClientInfo(string mapiClientString, ActivitySession.ClientInfo info)
		{
			if (string.IsNullOrEmpty(mapiClientString))
			{
				return info;
			}
			string[] array = mapiClientString.Split(new char[]
			{
				'/'
			});
			ClientId id;
			if (ActivitySession.MapiClientIds.TryGetValue(array[0].ToLowerInvariant(), out id))
			{
				info.Id = id;
			}
			if (array.Length > 2 && !string.IsNullOrEmpty(array[1]))
			{
				info.Version = array[1];
			}
			return info;
		}

		// Token: 0x060084CD RID: 33997 RVA: 0x00244974 File Offset: 0x00242B74
		public void CaptureActivity(ActivityId activityId, StoreObjectId itemId, StoreObjectId previousItemId, IDictionary<string, string> customProperties)
		{
			Activity activity = new Activity(activityId, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, itemId, previousItemId, customProperties);
			this.Log(activity);
		}

		// Token: 0x060084CE RID: 33998 RVA: 0x002449C8 File Offset: 0x00242BC8
		public void CaptureActivityBeforeItemChange(ItemChangeOperation operation, StoreId objectId, CoreItem item)
		{
			if (item == null)
			{
				return;
			}
			Activity activity;
			if (operation == ItemChangeOperation.Create)
			{
				if (this.TryCreateNewItemActivity(item, out activity))
				{
					this.Log(activity);
					return;
				}
			}
			else if (operation == ItemChangeOperation.Update)
			{
				if (this.TryCreateFlagActivity(item, out activity))
				{
					this.Log(activity);
				}
				if (this.TryCreateReplyActivity(item, out activity))
				{
					this.Log(activity);
					return;
				}
			}
			else if (operation == ItemChangeOperation.Submit && this.TryCreateMeetingActivity(item, out activity))
			{
				this.Log(activity);
			}
		}

		// Token: 0x060084CF RID: 33999 RVA: 0x00244A30 File Offset: 0x00242C30
		public void CaptureActivityAfterFolderChange(FolderChangeOperation operation, FolderChangeOperationFlags flags, IList<StoreObjectId> itemIdsBeforeChange, IList<StoreObjectId> itemIdsAfterChange, StoreObjectId sourceFolder, StoreObjectId targetFolder)
		{
			if (itemIdsBeforeChange == null || itemIdsBeforeChange.Count <= 0)
			{
				return;
			}
			IList<Activity> activities;
			if ((operation == FolderChangeOperation.HardDelete || operation == FolderChangeOperation.SoftDelete || operation == FolderChangeOperation.MoveToDeletedItems) && this.TryCreateDeleteActivities(operation, flags, itemIdsBeforeChange, sourceFolder, out activities))
			{
				this.Log(activities);
			}
			if ((operation == FolderChangeOperation.HardDelete || operation == FolderChangeOperation.SoftDelete || operation == FolderChangeOperation.MoveToDeletedItems || operation == FolderChangeOperation.Move) && this.TryCreateMoveActivities(operation, itemIdsBeforeChange, itemIdsAfterChange, sourceFolder, targetFolder, out activities))
			{
				this.Log(activities);
			}
		}

		// Token: 0x060084D0 RID: 34000 RVA: 0x00244A98 File Offset: 0x00242C98
		public void CaptureMessageSent(StoreObjectId itemId, string itemSchemaType)
		{
			Dictionary<string, string> customProperties = new Dictionary<string, string>
			{
				{
					"ItemSchemaType",
					itemSchemaType
				}
			};
			Activity activity = new Activity(ActivityId.MessageSent, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, itemId, null, customProperties);
			this.Log(activity);
		}

		// Token: 0x060084D1 RID: 34001 RVA: 0x00244B00 File Offset: 0x00242D00
		public void CaptureMarkAsUnread(ICollection<StoreObjectId> itemIds)
		{
			if (itemIds == null || itemIds.Count <= 0)
			{
				return;
			}
			List<Activity> activities;
			if (this.TryCreateUnreadActivity(itemIds, out activities))
			{
				this.Log(activities);
			}
		}

		// Token: 0x060084D2 RID: 34002 RVA: 0x00244B2C File Offset: 0x00242D2C
		public void CaptureMarkAsUnread(ICoreItem item)
		{
			if (item == null || item.InternalStoreObjectId == null)
			{
				return;
			}
			this.CaptureMarkAsUnread(new List<StoreObjectId>
			{
				item.InternalStoreObjectId
			});
		}

		// Token: 0x060084D3 RID: 34003 RVA: 0x00244B60 File Offset: 0x00242D60
		public void CaptureMarkAsRead(ICollection<StoreObjectId> itemIds)
		{
			if (itemIds == null || itemIds.Count <= 0)
			{
				return;
			}
			List<Activity> activities;
			if (this.TryCreateReadActivity(itemIds, out activities))
			{
				this.Log(activities);
			}
		}

		// Token: 0x060084D4 RID: 34004 RVA: 0x00244B8C File Offset: 0x00242D8C
		public void CaptureMarkAsRead(ICoreItem item)
		{
			if (item == null || item.InternalStoreObjectId == null)
			{
				return;
			}
			this.CaptureMarkAsRead(new List<StoreObjectId>
			{
				item.InternalStoreObjectId
			});
		}

		// Token: 0x060084D5 RID: 34005 RVA: 0x00244BC0 File Offset: 0x00242DC0
		public void CaptureDelivery(StoreObjectId storeObjectId, IDictionary<string, string> deliveryActivityInfo)
		{
			if (storeObjectId == null || deliveryActivityInfo == null)
			{
				return;
			}
			Activity activity = new Activity(ActivityId.MessageDelivered, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, storeObjectId, null, deliveryActivityInfo);
			this.Log(activity);
		}

		// Token: 0x060084D6 RID: 34006 RVA: 0x00244C74 File Offset: 0x00242E74
		public void CaptureMarkAsClutterOrNotClutter(Dictionary<StoreObjectId, bool> itemClutterActions)
		{
			if (itemClutterActions == null)
			{
				return;
			}
			Activity[] array = (from idAndAction in itemClutterActions
			select new Activity(idAndAction.Value ? ActivityId.MarkMessageAsClutter : ActivityId.MarkMessageAsNotClutter, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, idAndAction.Key, null, null)).ToArray<Activity>();
			if (array != null && array.Length > 0)
			{
				this.Log(array);
			}
		}

		// Token: 0x060084D7 RID: 34007 RVA: 0x00244CB0 File Offset: 0x00242EB0
		public void CaptureRemoteSend(StoreObjectId storeObjectId)
		{
			if (storeObjectId == null)
			{
				return;
			}
			Activity activity = new Activity(ActivityId.RemoteSend, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, storeObjectId, null, null);
			this.Log(activity);
		}

		// Token: 0x060084D8 RID: 34008 RVA: 0x00244D08 File Offset: 0x00242F08
		public void CaptureCalendarEventActivity(ActivityId activityId, StoreObjectId id)
		{
			Activity activity = new Activity(activityId, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, id, null, null);
			this.Log(activity);
		}

		// Token: 0x060084D9 RID: 34009 RVA: 0x00244D58 File Offset: 0x00242F58
		public void CaptureClutterNotificationSent(StoreObjectId itemId, IDictionary<string, string> messageProperties)
		{
			Activity activity = new Activity(ActivityId.ClutterNotificationSent, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, itemId, null, messageProperties);
			this.Log(activity);
		}

		// Token: 0x060084DA RID: 34010 RVA: 0x00244DAC File Offset: 0x00242FAC
		public void CaptureServerLogonActivity(string result, string exceptionInfo, string userName, string clientIP, string userAgent)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("Result", string.IsNullOrEmpty(result) ? string.Empty : result);
			dictionary.Add("ExceptionInfo", string.IsNullOrEmpty(exceptionInfo) ? string.Empty : exceptionInfo);
			dictionary.Add("UserName", string.IsNullOrEmpty(userName) ? string.Empty : userName);
			dictionary.Add("ClientIP", string.IsNullOrEmpty(clientIP) ? string.Empty : clientIP);
			dictionary.Add("UserAgent", string.IsNullOrEmpty(userAgent) ? string.Empty : userAgent);
			Activity activity = new Activity(ActivityId.ServerLogon, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, null, null, dictionary);
			this.Log(activity);
		}

		// Token: 0x060084DB RID: 34011 RVA: 0x00244E90 File Offset: 0x00243090
		private bool TryCreateFlagActivity(CoreItem item, out Activity activity)
		{
			activity = null;
			if (item.Id == null)
			{
				return false;
			}
			if (!item.PropertyBag.IsPropertyDirty(InternalSchema.FlagStatus))
			{
				return false;
			}
			ActivityId id;
			switch (item.PropertyBag.GetValueOrDefault<FlagStatus>(InternalSchema.FlagStatus))
			{
			case FlagStatus.NotFlagged:
				id = ActivityId.FlagCleared;
				break;
			case FlagStatus.Complete:
				id = ActivityId.FlagComplete;
				break;
			case FlagStatus.Flagged:
				id = ActivityId.Flag;
				break;
			default:
				return false;
			}
			activity = new Activity(id, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, item.Id.ObjectId, null, new Dictionary<string, string>
			{
				{
					"ItemClass",
					item.ClassName()
				}
			});
			return true;
		}

		// Token: 0x060084DC RID: 34012 RVA: 0x00244F54 File Offset: 0x00243154
		private bool TryCreateNewItemActivity(CoreItem item, out Activity activity)
		{
			activity = null;
			ActivityId? activityId = null;
			Schema schema = ((IValidatable)item).Schema;
			if (schema is CalendarItemBaseSchema)
			{
				if (!item.PropertyBag.GetValueOrDefault<bool>(CalendarItemBaseSchema.IsMeeting, false))
				{
					activityId = new ActivityId?(ActivityId.CreateAppointment);
				}
			}
			else if (schema is TaskSchema)
			{
				activityId = new ActivityId?(ActivityId.CreateTask);
			}
			if (activityId == null)
			{
				return false;
			}
			activity = new Activity(activityId.Value, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, ((ICoreObject)item).InternalStoreObjectId, null, new Dictionary<string, string>
			{
				{
					"ItemClass",
					item.ClassName()
				}
			});
			return true;
		}

		// Token: 0x060084DD RID: 34013 RVA: 0x00245018 File Offset: 0x00243218
		private bool TryCreateMeetingActivity(CoreItem item, out Activity activity)
		{
			activity = null;
			ActivityId? activityId = null;
			Schema schema = ((IValidatable)item).Schema;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (schema is MeetingRequestSchema)
			{
				string valueOrDefault = item.PropertyBag.GetValueOrDefault<string>(InternalSchema.InReplyTo);
				dictionary.Add("InReplyTo", valueOrDefault);
				activityId = new ActivityId?(ActivityId.SendMeetingRequest);
			}
			else if (schema is MeetingResponseSchema)
			{
				switch (item.PropertyBag.GetValueOrDefault<ResponseType>(MeetingResponseSchema.ResponseType))
				{
				case ResponseType.Tentative:
					activityId = new ActivityId?(ActivityId.TentativeMeetingRequest);
					break;
				case ResponseType.Accept:
					activityId = new ActivityId?(ActivityId.AcceptedMeetingRequest);
					break;
				case ResponseType.Decline:
					activityId = new ActivityId?(ActivityId.DeclinedMeetingRequest);
					break;
				default:
					return false;
				}
				bool valueOrDefault2 = item.PropertyBag.GetValueOrDefault<bool>(MeetingResponseSchema.AppointmentCounterProposal);
				dictionary.Add("ProposeNewTime", valueOrDefault2 ? bool.TrueString : bool.FalseString);
			}
			else if (schema is MeetingMessageInstanceSchema)
			{
				int valueOrDefault3 = item.PropertyBag.GetValueOrDefault<int>(MessageItemSchema.AppointmentState);
				if ((valueOrDefault3 & 4) != 0)
				{
					activityId = new ActivityId?(ActivityId.CancelMeeting);
				}
			}
			if (activityId == null)
			{
				return false;
			}
			dictionary.Add("ItemClass", item.ClassName());
			activity = new Activity(activityId.Value, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, ((ICoreObject)item).InternalStoreObjectId, null, dictionary);
			return true;
		}

		// Token: 0x060084DE RID: 34014 RVA: 0x00245188 File Offset: 0x00243388
		private IList<Activity> TryExtractClutterMoveActivities(StoreObjectId newId, StoreObjectId oldId, StoreObjectId sourceFolder, StoreObjectId targetFolder)
		{
			List<Activity> list = new List<Activity>(2);
			List<ActivityId> list2 = new List<ActivityId>(2);
			if (sourceFolder != null)
			{
				if (this.session.IsDefaultFolderType(sourceFolder) == DefaultFolderType.Inbox)
				{
					list2.Add(ActivityId.MoveFromInbox);
				}
				else if (this.session.IsDefaultFolderType(sourceFolder) == DefaultFolderType.Clutter)
				{
					list2.Add(ActivityId.MoveFromClutter);
				}
			}
			if (targetFolder != null)
			{
				if (this.session.IsDefaultFolderType(targetFolder) == DefaultFolderType.Inbox)
				{
					list2.Add(ActivityId.MoveToInbox);
				}
				else if (this.session.IsDefaultFolderType(targetFolder) == DefaultFolderType.Clutter)
				{
					list2.Add(ActivityId.MoveToClutter);
				}
			}
			foreach (ActivityId id in list2)
			{
				list.Add(new Activity(id, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, newId, oldId, null));
			}
			return list;
		}

		// Token: 0x060084DF RID: 34015 RVA: 0x0024528C File Offset: 0x0024348C
		private bool TryCreateReplyActivity(CoreItem item, out Activity activity)
		{
			activity = null;
			if (item.Id == null)
			{
				return false;
			}
			if (!item.PropertyBag.IsPropertyDirty(MessageItemSchema.LastVerbExecuted))
			{
				return false;
			}
			LastAction valueOrDefault = item.PropertyBag.GetValueOrDefault<LastAction>(MessageItemSchema.LastVerbExecuted);
			ActivityId id;
			if (valueOrDefault == LastAction.ReplyToSender || (valueOrDefault >= LastAction.VotingOptionMin && valueOrDefault <= LastAction.VotingOptionMax))
			{
				id = ActivityId.Reply;
			}
			else if (valueOrDefault == LastAction.ReplyToAll)
			{
				id = ActivityId.ReplyAll;
			}
			else
			{
				if (valueOrDefault != LastAction.Forward)
				{
					return false;
				}
				id = ActivityId.Forward;
			}
			activity = new Activity(id, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, item.Id.ObjectId, null, null);
			return true;
		}

		// Token: 0x060084E0 RID: 34016 RVA: 0x0024533C File Offset: 0x0024353C
		private bool TryCreateDeleteActivities(FolderChangeOperation operation, FolderChangeOperationFlags flags, IList<StoreObjectId> itemIdsBeforeMove, StoreObjectId sourceFolder, out IList<Activity> activities)
		{
			activities = null;
			string value;
			switch (operation)
			{
			case FolderChangeOperation.MoveToDeletedItems:
				value = "MoveToDeletedItems";
				break;
			case FolderChangeOperation.SoftDelete:
				value = "SoftDelete";
				break;
			case FolderChangeOperation.HardDelete:
				value = "HardDelete";
				break;
			default:
				return false;
			}
			ActivityId? activityId = null;
			DefaultFolderType defaultFolderType = (sourceFolder == null) ? DefaultFolderType.None : this.session.IsDefaultFolderType(sourceFolder);
			if (defaultFolderType == DefaultFolderType.Inbox)
			{
				activityId = new ActivityId?(ActivityId.DeleteFromInbox);
			}
			else if (defaultFolderType == DefaultFolderType.Clutter)
			{
				activityId = new ActivityId?(ActivityId.DeleteFromClutter);
			}
			bool flag = (flags & FolderChangeOperationFlags.EmptyFolder) == FolderChangeOperationFlags.EmptyFolder;
			bool flag2 = (flags & FolderChangeOperationFlags.DeleteAllClutter) == FolderChangeOperationFlags.DeleteAllClutter;
			activities = new List<Activity>(itemIdsBeforeMove.Count);
			foreach (StoreObjectId storeObjectId in itemIdsBeforeMove)
			{
				if (storeObjectId != null)
				{
					Activity item = new Activity(ActivityId.Delete, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, storeObjectId, null, new Dictionary<string, string>
					{
						{
							"DeleteType",
							value
						},
						{
							"DeletedByEmptyFolder",
							flag ? bool.TrueString : bool.FalseString
						},
						{
							"DeletedByDeleteAllClutter",
							flag2 ? bool.TrueString : bool.FalseString
						},
						{
							"SourceDefaultFolderType",
							defaultFolderType.ToString()
						}
					});
					activities.Add(item);
					if (activityId != null)
					{
						activities.Add(new Activity(activityId.Value, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, storeObjectId, null, null));
					}
				}
			}
			return true;
		}

		// Token: 0x060084E1 RID: 34017 RVA: 0x0024553C File Offset: 0x0024373C
		private bool TryCreateMoveActivities(FolderChangeOperation operation, IList<StoreObjectId> itemIdsBeforeMove, IList<StoreObjectId> itemIdsAfterMove, StoreObjectId sourceFolder, StoreObjectId targetFolder, out IList<Activity> activities)
		{
			activities = null;
			if (itemIdsBeforeMove == null && itemIdsAfterMove == null)
			{
				return false;
			}
			if (itemIdsAfterMove == null)
			{
				itemIdsAfterMove = itemIdsBeforeMove;
			}
			string value;
			switch (operation)
			{
			case FolderChangeOperation.MoveToDeletedItems:
				value = "Delete";
				break;
			case FolderChangeOperation.SoftDelete:
				value = "SoftDelete";
				break;
			case FolderChangeOperation.HardDelete:
				value = "HardDelete";
				break;
			default:
				value = "Move";
				break;
			}
			activities = new List<Activity>(itemIdsAfterMove.Count);
			for (int i = 0; i < itemIdsAfterMove.Count; i++)
			{
				StoreObjectId storeObjectId = itemIdsAfterMove[i];
				StoreObjectId storeObjectId2 = (itemIdsBeforeMove == null || itemIdsBeforeMove.Count <= i) ? null : itemIdsBeforeMove[i];
				Activity item = new Activity(ActivityId.Move, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, storeObjectId, storeObjectId2, new Dictionary<string, string>
				{
					{
						"MoveType",
						value
					},
					{
						"SourceFolder",
						this.ConvertFolderIdToString(sourceFolder)
					},
					{
						"TargetFolder",
						this.ConvertFolderIdToString(targetFolder)
					}
				});
				activities.Add(item);
				((List<Activity>)activities).AddRange(this.TryExtractClutterMoveActivities(storeObjectId, storeObjectId2, sourceFolder, targetFolder));
			}
			return true;
		}

		// Token: 0x060084E2 RID: 34018 RVA: 0x00245678 File Offset: 0x00243878
		private bool TryCreateUnreadActivity(ICollection<StoreObjectId> itemIds, out List<Activity> activities)
		{
			activities = new List<Activity>(itemIds.Count);
			foreach (StoreObjectId storeObjectId in itemIds)
			{
				if (storeObjectId != null)
				{
					Activity item = new Activity(ActivityId.MarkAsUnread, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, storeObjectId, null, null);
					activities.Add(item);
				}
			}
			return activities.Count > 0;
		}

		// Token: 0x060084E3 RID: 34019 RVA: 0x0024571C File Offset: 0x0024391C
		private bool TryCreateReadActivity(ICollection<StoreObjectId> itemIds, out List<Activity> activities)
		{
			activities = new List<Activity>(itemIds.Count);
			foreach (StoreObjectId storeObjectId in itemIds)
			{
				if (storeObjectId != null)
				{
					Activity item = new Activity(ActivityId.MarkAsRead, this.clientInfo.Id, ExDateTime.UtcNow, this.clientSessionId, this.clientInfo.Version, Interlocked.Increment(ref this.sequenceNumber), this.session, storeObjectId, null, null);
					activities.Add(item);
				}
			}
			return activities.Count > 0;
		}

		// Token: 0x060084E4 RID: 34020 RVA: 0x002457C0 File Offset: 0x002439C0
		private void Log(IList<Activity> activities)
		{
			if (activities != null && activities.Count != 0)
			{
				this.logger.Log(activities);
			}
		}

		// Token: 0x060084E5 RID: 34021 RVA: 0x002457DC File Offset: 0x002439DC
		private void Log(Activity activity)
		{
			this.Log(new Activity[]
			{
				activity
			});
		}

		// Token: 0x060084E6 RID: 34022 RVA: 0x002457FC File Offset: 0x002439FC
		private string ConvertFolderIdToString(StoreObjectId folderId)
		{
			if (folderId == null)
			{
				return "null";
			}
			string result;
			if (ActivitySession.WellKnownFolderMapping.TryGetValue(this.session.IsDefaultFolderType(folderId), out result))
			{
				return result;
			}
			return folderId.ToBase64ProviderLevelItemId();
		}

		// Token: 0x040058F8 RID: 22776
		private const string ItemClassCustomProperty = "ItemClass";

		// Token: 0x040058F9 RID: 22777
		private static readonly IDictionary<string, ClientId> MapiClientIds = new Dictionary<string, ClientId>
		{
			{
				"outlook.exe",
				ClientId.Outlook
			},
			{
				"lync.exe",
				ClientId.Lync
			}
		};

		// Token: 0x040058FA RID: 22778
		private static readonly LazyMember<IDictionary<DefaultFolderType, string>> wellKnownFolderMapping = new LazyMember<IDictionary<DefaultFolderType, string>>(delegate()
		{
			DefaultFolderType[] array = (DefaultFolderType[])Enum.GetValues(typeof(DefaultFolderType));
			IDictionary<DefaultFolderType, string> dictionary = new Dictionary<DefaultFolderType, string>(array.Length);
			foreach (DefaultFolderType defaultFolderType in array)
			{
				if (defaultFolderType != DefaultFolderType.None)
				{
					dictionary.Add(defaultFolderType, defaultFolderType.ToString());
				}
			}
			return dictionary;
		});

		// Token: 0x040058FB RID: 22779
		private readonly MailboxSession session;

		// Token: 0x040058FC RID: 22780
		private readonly ActivitySession.ClientInfo clientInfo;

		// Token: 0x040058FD RID: 22781
		private readonly Guid clientSessionId = Guid.NewGuid();

		// Token: 0x040058FE RID: 22782
		private readonly IActivityLogger logger;

		// Token: 0x040058FF RID: 22783
		private long sequenceNumber = -1L;

		// Token: 0x02000F13 RID: 3859
		private struct ClientInfo
		{
			// Token: 0x04005901 RID: 22785
			public ClientId Id;

			// Token: 0x04005902 RID: 22786
			public string Version;
		}
	}
}
