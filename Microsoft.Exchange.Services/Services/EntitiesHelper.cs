using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities;
using Microsoft.Exchange.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.TypeConversion.Converters;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services
{
	// Token: 0x02000014 RID: 20
	internal class EntitiesHelper
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003D89 File Offset: 0x00001F89
		public EntitiesHelper(CallContext context)
		{
			this.edmIdConverter = IdConverter.Instance;
			this.Context = context;
			this.ewsIdConverter = new IdConverter(this.Context);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003DB4 File Offset: 0x00001FB4
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003DBC File Offset: 0x00001FBC
		public CallContext Context { get; private set; }

		// Token: 0x0600009C RID: 156 RVA: 0x00003DC8 File Offset: 0x00001FC8
		public TEntity Execute<TInput, TEntity>(Func<TInput, CommandContext, TEntity> function, StoreSession session, BasicTypes type, TInput input) where TInput : class where TEntity : class
		{
			CommandContext arg = this.TransformEwsIdsToEntityIds<TInput>(input, type);
			TEntity tentity = function(input, arg);
			this.TransformEntityIdsToEwsIds<TEntity>(tentity, session);
			return tentity;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003E10 File Offset: 0x00002010
		public void Execute(Action<string, CommandContext> action, StoreSession session, BasicTypes type, string ewsId, string changeKey = null)
		{
			this.Execute<VoidResult>(delegate(string id, CommandContext context)
			{
				action(id, context);
				return VoidResult.Value;
			}, session, type, ewsId, changeKey);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003E60 File Offset: 0x00002060
		public void Execute(Action<string, CommandContext> action, StoreSession session, BaseItemId itemId)
		{
			this.Execute<VoidResult>(delegate(string id, CommandContext context)
			{
				action(id, context);
				return VoidResult.Value;
			}, session, itemId);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003E90 File Offset: 0x00002090
		public TResult Execute<TResult>(Func<string, CommandContext, TResult> function, StoreSession session, BasicTypes type, string ewsId, string changeKey = null) where TResult : class
		{
			CommandContext arg2;
			string arg = this.ToEntityId(ewsId, changeKey, type, out arg2);
			TResult tresult = function(arg, arg2);
			this.TransformEntityIdsToEwsIds<TResult>(tresult, session);
			return tresult;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003EC0 File Offset: 0x000020C0
		public TResult Execute<TResult>(Func<string, CommandContext, TResult> function, StoreSession session, BaseItemId itemId) where TResult : class
		{
			CommandContext arg2;
			string arg = this.ToEntityId(itemId, out arg2);
			TResult tresult = function(arg, arg2);
			this.TransformEntityIdsToEwsIds<TResult>(tresult, session);
			return tresult;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003EEC File Offset: 0x000020EC
		public ICalendarGroups GetCalendarGroups(IStoreSession session)
		{
			CalendaringContainer calendaringContainer = EntitiesHelper.GetCalendaringContainer(session);
			return calendaringContainer.CalendarGroups;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003F08 File Offset: 0x00002108
		public IEvents GetEvents(BaseFolderId calendarFolderId, out StoreSession session)
		{
			IdAndSession idAndSession = this.ewsIdConverter.ConvertFolderIdToIdAndSessionReadOnly(calendarFolderId);
			session = idAndSession.Session;
			if (calendarFolderId is DistinguishedFolderId)
			{
				calendarFolderId = IdConverter.GetFolderIdFromStoreId(idAndSession.Id, new MailboxId(session.MailboxGuid));
			}
			CalendaringContainer calendaringContainer = EntitiesHelper.GetCalendaringContainer(idAndSession.Session);
			return calendaringContainer.Calendars[calendarFolderId.GetId()].Events;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003F70 File Offset: 0x00002170
		private static CalendaringContainer GetCalendaringContainer(IStoreSession session)
		{
			return new CalendaringContainer(session, null);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003F88 File Offset: 0x00002188
		private string ToEntityId(string ewsId, string ewsChangeKey, BasicTypes type, out CommandContext commandContext)
		{
			IdConverter.ConvertOption convertOption = string.IsNullOrEmpty(ewsChangeKey) ? (IdConverter.ConvertOption.IgnoreChangeKey | IdConverter.ConvertOption.NoBind) : IdConverter.ConvertOption.NoBind;
			IdAndSession idAndSession;
			switch (type)
			{
			case BasicTypes.Folder:
				idAndSession = this.ewsIdConverter.ConvertFolderIdToIdAndSession(new FolderId(ewsId, ewsChangeKey), convertOption);
				break;
			case BasicTypes.Item:
				idAndSession = this.ewsIdConverter.ConvertItemIdToIdAndSession(new ItemId(ewsId, ewsChangeKey), convertOption, type);
				break;
			case BasicTypes.Attachment:
				idAndSession = this.ewsIdConverter.ConvertItemIdToIdAndSession(new AttachmentIdType(ewsId), convertOption, type);
				break;
			default:
				throw new ArgumentOutOfRangeException("type", type, "The value is not supported.");
			}
			string text;
			string result = this.edmIdConverter.ToStringId(idAndSession.Id, idAndSession.Session, out text);
			commandContext = (string.IsNullOrEmpty(text) ? null : new CommandContext
			{
				IfMatchETag = text
			});
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004050 File Offset: 0x00002250
		private string ToEntityId(BaseItemId itemId, out CommandContext commandContext)
		{
			IdConverter.ConvertOption convertOption = string.IsNullOrEmpty(itemId.GetChangeKey()) ? (IdConverter.ConvertOption.IgnoreChangeKey | IdConverter.ConvertOption.NoBind) : IdConverter.ConvertOption.NoBind;
			IdAndSession idAndSession = this.ewsIdConverter.ConvertItemIdToIdAndSession(itemId, convertOption, BasicTypes.Item);
			string text;
			string result = this.edmIdConverter.ToStringId(idAndSession.Id, idAndSession.Session, out text);
			commandContext = (string.IsNullOrEmpty(text) ? null : new CommandContext
			{
				IfMatchETag = text
			});
			return result;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000040B8 File Offset: 0x000022B8
		private ConcatenatedIdAndChangeKey ToEwsConcatenatedId(string entityId, StoreSession session, string changeKey = null)
		{
			StoreId storeId = this.edmIdConverter.ToStoreId(entityId, changeKey);
			IdAndSession idAndSession = new IdAndSession(storeId, session);
			return IdConverter.GetConcatenatedId(storeId, idAndSession, null);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000040E4 File Offset: 0x000022E4
		public TObject TransformEntityIdsToEwsIds<TObject>(TObject value, StoreSession session) where TObject : class
		{
			if (value == null)
			{
				return default(TObject);
			}
			IEntity entity = value as IEntity;
			if (entity != null)
			{
				IStorageEntity storageEntity = entity as IStorageEntity;
				ConcatenatedIdAndChangeKey concatenatedIdAndChangeKey;
				if (storageEntity != null)
				{
					concatenatedIdAndChangeKey = this.ToEwsConcatenatedId(storageEntity.Id, session, storageEntity.ChangeKey);
					storageEntity.Id = concatenatedIdAndChangeKey.Id;
					storageEntity.ChangeKey = concatenatedIdAndChangeKey.ChangeKey;
					Event @event = storageEntity as Event;
					if (@event != null && @event.IsPropertySet(@event.Schema.SeriesMasterIdProperty))
					{
						@event.SeriesMasterId = this.ToEwsConcatenatedId(@event.SeriesMasterId, session, null).Id;
					}
				}
				else
				{
					concatenatedIdAndChangeKey = this.ToEwsConcatenatedId(entity.Id, session, null);
				}
				entity.Id = concatenatedIdAndChangeKey.Id;
			}
			else
			{
				IList list = value as IList;
				if (list != null)
				{
					using (IEnumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object value2 = enumerator.Current;
							this.TransformEntityIdsToEwsIds<object>(value2, session);
						}
						return value;
					}
				}
				ExpandedEvent expandedEvent = value as ExpandedEvent;
				if (expandedEvent != null && expandedEvent.CancelledOccurrences != null)
				{
					for (int i = 0; i < expandedEvent.CancelledOccurrences.Count; i++)
					{
						string entityId = expandedEvent.CancelledOccurrences[i];
						ConcatenatedIdAndChangeKey concatenatedIdAndChangeKey2 = this.ToEwsConcatenatedId(entityId, session, null);
						expandedEvent.CancelledOccurrences[i] = concatenatedIdAndChangeKey2.Id;
					}
					this.TransformEntityIdsToEwsIds<IList<Event>>(expandedEvent.Occurrences, session);
					this.TransformEntityIdsToEwsIds<Event>(expandedEvent.RecurrenceMaster, session);
				}
			}
			return value;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004294 File Offset: 0x00002494
		private CommandContext TransformEwsIdsToEntityIds<TObject>(TObject value, BasicTypes type) where TObject : class
		{
			if (value == null)
			{
				return null;
			}
			IEntity entity = value as IEntity;
			CommandContext result = null;
			if (entity != null)
			{
				string id = entity.Id;
				IVersioned versioned = entity as IVersioned;
				string ewsChangeKey;
				if (versioned == null)
				{
					ewsChangeKey = null;
				}
				else
				{
					Event @event = versioned as Event;
					if (@event != null && !string.IsNullOrEmpty(@event.SeriesMasterId))
					{
						CommandContext commandContext;
						@event.SeriesMasterId = this.ToEntityId(@event.SeriesMasterId, null, BasicTypes.Item, out commandContext);
					}
					ewsChangeKey = versioned.ChangeKey;
				}
				if (!string.IsNullOrEmpty(id))
				{
					entity.Id = this.ToEntityId(id, ewsChangeKey, type, out result);
				}
			}
			else
			{
				IList list = value as IList;
				if (list != null)
				{
					foreach (object value2 in list)
					{
						this.TransformEwsIdsToEntityIds<object>(value2, type);
					}
				}
			}
			return result;
		}

		// Token: 0x04000030 RID: 48
		private readonly IdConverter edmIdConverter;

		// Token: 0x04000031 RID: 49
		private readonly IdConverter ewsIdConverter;
	}
}
