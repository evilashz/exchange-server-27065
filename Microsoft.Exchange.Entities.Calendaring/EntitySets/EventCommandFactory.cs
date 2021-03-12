using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x02000036 RID: 54
	internal sealed class EventCommandFactory : IEventCommandFactory, IEntityCommandFactory<Events, Event>
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00005B28 File Offset: 0x00003D28
		private EventCommandFactory()
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005B54 File Offset: 0x00003D54
		public ICreateEntityCommand<Events, Event> CreateCreateCommand(Event entity, Events scope)
		{
			switch (entity.Type)
			{
			case EventType.SingleInstance:
				return this.singleInstanceEventCommandFactory.CreateCreateCommand(entity, scope);
			case EventType.Occurrence:
			case EventType.Exception:
				return this.nprInstanceCommandFactory.CreateCreateCommand(entity, scope);
			case EventType.SeriesMaster:
				return this.seriesEventCommandFactory.CreateCreateCommand(entity, scope);
			default:
				throw new ArgumentOutOfRangeException("entity.Type");
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005BC4 File Offset: 0x00003DC4
		public IDeleteEntityCommand<Events> CreateDeleteCommand(string key, Events scope)
		{
			return this.CreateCommand<IDeleteEntityCommand<Events>, VoidResult>(key, scope, () => new DeleteSeries(), () => new DeleteEvent());
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005C13 File Offset: 0x00003E13
		public IFindEntitiesCommand<Events, Event> CreateFindCommand(IEntityQueryOptions queryOptions, Events scope)
		{
			return this.singleInstanceEventCommandFactory.CreateFindCommand(queryOptions, scope);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005C68 File Offset: 0x00003E68
		public IReadEntityCommand<Events, Event> CreateReadCommand(string key, Events scope)
		{
			return this.CreateCommand<IReadEntityCommand<Events, Event>, Event>(key, scope, () => this.seriesEventCommandFactory.CreateReadCommand(key, scope), () => this.singleInstanceEventCommandFactory.CreateReadCommand(key, scope));
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005CBA File Offset: 0x00003EBA
		public IUpdateEntityCommand<Events, Event> CreateUpdateCommand(string key, Event entity, Events scope)
		{
			return this.CreateUpdateCommand(key, entity, scope, null);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005D28 File Offset: 0x00003F28
		public UpdateEventBase CreateUpdateCommand(string key, Event entity, Events scope, UpdateEventParameters updateEventParameters)
		{
			return this.CreateCommand<UpdateEventBase, Event>(key, scope, () => new UpdateSeries
			{
				Entity = entity,
				UpdateEventParameters = updateEventParameters
			}, () => new UpdateEvent
			{
				Entity = entity,
				UpdateEventParameters = updateEventParameters
			});
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005D6C File Offset: 0x00003F6C
		public ConvertSingleEventToNprSeries CreateConvertSingleEventToNprCommand(string key, Events scope)
		{
			return new ConvertSingleEventToNprSeries
			{
				EntityKey = key,
				Scope = scope
			};
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005D9C File Offset: 0x00003F9C
		public CancelEventBase CreateCancelCommand(string key, Events scope)
		{
			return this.CreateCommand<CancelEventBase, VoidResult>(key, scope, () => new CancelSeries(), () => new CancelEvent());
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005DEC File Offset: 0x00003FEC
		public GetCalendarView CreateGetCalendarViewCommand(ICalendarViewParameters parameters, Events scope)
		{
			return new GetCalendarView(parameters)
			{
				Scope = scope
			};
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005E18 File Offset: 0x00004018
		public RespondToEventBase CreateRespondToCommand(string key, Events scope)
		{
			return this.CreateCommand<RespondToEventBase, VoidResult>(key, scope, () => new RespondToSeries(), () => new RespondToEvent());
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005E78 File Offset: 0x00004078
		public ForwardEventBase CreateForwardCommand(string key, Events scope)
		{
			return this.CreateCommand<ForwardEventBase, VoidResult>(key, scope, () => new ForwardSeries(), () => new ForwardEvent());
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005ED8 File Offset: 0x000040D8
		public ExpandSeries CreateExpandCommand(string key, Events scope)
		{
			return this.CreateCommand<ExpandSeries, ExpandedEvent>(key, scope, () => new ExpandSeries(), () => new ExpandSeries());
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005F28 File Offset: 0x00004128
		private TCommand CreateCommand<TCommand, TResult>(string key, Events scope, Func<TCommand> createSeriesCommand, Func<TCommand> createInstanceCommand) where TCommand : IKeyedEntityCommand<Events, TResult>
		{
			StoreObjectId storeObjectId = scope.IdConverter.ToStoreObjectId(key);
			TCommand tcommand = (storeObjectId.ObjectType == StoreObjectType.CalendarItemSeries) ? createSeriesCommand() : createInstanceCommand();
			this.InitializeKeyedEntityCommand<TResult>(tcommand, key, scope);
			return tcommand;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005F6B File Offset: 0x0000416B
		private void InitializeKeyedEntityCommand<TResult>(IKeyedEntityCommand<Events, TResult> command, string key, Events scope)
		{
			command.EntityKey = key;
			command.Scope = scope;
		}

		// Token: 0x0400005C RID: 92
		public static readonly IEventCommandFactory Instance = new EventCommandFactory();

		// Token: 0x0400005D RID: 93
		private readonly IEntityCommandFactory<Events, Event> singleInstanceEventCommandFactory = EntityCommandFactory<Events, Event, CreateEvent, DeleteEvent, FindEvents, ReadEvent, UpdateEvent>.Instance;

		// Token: 0x0400005E RID: 94
		private readonly IEntityCommandFactory<Events, Event> seriesEventCommandFactory = EntityCommandFactory<Events, Event, CreateSeries, DeleteSeries, FindEvents, ReadEvent, UpdateSeries>.Instance;

		// Token: 0x0400005F RID: 95
		private readonly IEntityCommandFactory<Events, Event> nprInstanceCommandFactory = EntityCommandFactory<Events, Event, CreateNprInstance, DeleteEvent, FindEvents, ReadEvent, UpdateEvent>.Instance;
	}
}
