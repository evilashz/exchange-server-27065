using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.EntitySets.Commands;
using Microsoft.Exchange.Entities.EntitySets.Linq.ExtensionMethods;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x0200004C RID: 76
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FindEvents : FindStorageEntitiesCommand<Events, Event>
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000815D File Offset: 0x0000635D
		public override IDictionary<string, Microsoft.Exchange.Data.PropertyDefinition> PropertyMap
		{
			get
			{
				return FindEvents.EventPropertyMap;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00008164 File Offset: 0x00006364
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.FindEventsTracer;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000816C File Offset: 0x0000636C
		public static bool NeedsReread(IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition> propertyDefinitions, ITracer tracer)
		{
			foreach (Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition propertyDefinition in propertyDefinitions)
			{
				if (!FindEvents.SupportsProperty(propertyDefinition))
				{
					tracer.TraceDebug<string>(0L, "Need re-read due to property: {0}", propertyDefinition.Name);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000081D0 File Offset: 0x000063D0
		public static bool SupportsProperty(Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition propertyDefinition)
		{
			foreach (Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition propertyDefinition2 in FindEvents.HardCodedProperties)
			{
				if (propertyDefinition2.Name.Equals(propertyDefinition.Name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000820F File Offset: 0x0000640F
		protected override IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> GetRequestedPropertyDependencies()
		{
			if (FindEvents.hardCodedStorageProperties == null)
			{
				FindEvents.hardCodedStorageProperties = this.Scope.EventDataProvider.MapProperties(FindEvents.HardCodedProperties).Concat(FindEvents.HardCodedPropertyDependencies);
			}
			return FindEvents.hardCodedStorageProperties;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008278 File Offset: 0x00006478
		protected override IEnumerable<Event> OnExecute()
		{
			EventDataProvider eventDataProvider = this.Scope.EventDataProvider;
			Microsoft.Exchange.Data.PropertyDefinition[] array = base.Properties.ToArray<Microsoft.Exchange.Data.PropertyDefinition>();
			bool flag = this.Context != null && this.Context.RequestedProperties != null && FindEvents.NeedsReread(this.Context.RequestedProperties, this.Trace);
			IEnumerable<Event> source = eventDataProvider.Find(base.QueryFilter, base.SortColumns, flag ? this.Scope.EventDataProvider.MapProperties(FindEvents.IdProperty).ToArray<Microsoft.Exchange.Data.PropertyDefinition>() : array);
			IEnumerable<Event> source2 = base.QueryOptions.ApplySkipTakeTo(source.AsQueryable<Event>());
			if (flag)
			{
				int count = (this.Context != null) ? this.Context.PageSizeOnReread : 20;
				return (from x in source2.Take(count)
				select this.Scope.Read(x.Id, this.Context)).ToList<Event>();
			}
			EventTimeAdjuster adjuster = this.Scope.TimeAdjuster;
			ExTimeZone sessionTimeZone = this.Scope.Session.ExTimeZone;
			return from e in source2
			select adjuster.AdjustTimeProperties(e, sessionTimeZone);
		}

		// Token: 0x04000087 RID: 135
		public static readonly Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[] HardCodedProperties = new Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[]
		{
			SchematizedObject<EventSchema>.SchemaInstance.LastModifiedTimeProperty,
			SchematizedObject<EventSchema>.SchemaInstance.DateTimeCreatedProperty,
			SchematizedObject<EventSchema>.SchemaInstance.CategoriesProperty,
			SchematizedObject<EventSchema>.SchemaInstance.EndProperty,
			SchematizedObject<EventSchema>.SchemaInstance.HasAttachmentsProperty,
			SchematizedObject<EventSchema>.SchemaInstance.HasAttendeesProperty,
			SchematizedObject<EventSchema>.SchemaInstance.IdProperty,
			SchematizedObject<EventSchema>.SchemaInstance.IntendedEndTimeZoneIdProperty,
			SchematizedObject<EventSchema>.SchemaInstance.IntendedStartTimeZoneIdProperty,
			SchematizedObject<EventSchema>.SchemaInstance.IsAllDayProperty,
			SchematizedObject<EventSchema>.SchemaInstance.IsCancelledProperty,
			SchematizedObject<EventSchema>.SchemaInstance.IsOrganizerProperty,
			SchematizedObject<EventSchema>.SchemaInstance.ImportanceProperty,
			SchematizedObject<EventSchema>.SchemaInstance.LocationProperty,
			SchematizedObject<EventSchema>.SchemaInstance.OrganizerProperty,
			SchematizedObject<EventSchema>.SchemaInstance.ResponseStatusProperty,
			SchematizedObject<EventSchema>.SchemaInstance.ResponseRequestedProperty,
			SchematizedObject<EventSchema>.SchemaInstance.SensitivityProperty,
			SchematizedObject<EventSchema>.SchemaInstance.SeriesIdProperty,
			SchematizedObject<EventSchema>.SchemaInstance.SeriesMasterIdProperty,
			SchematizedObject<EventSchema>.SchemaInstance.ShowAsProperty,
			SchematizedObject<EventSchema>.SchemaInstance.StartProperty,
			SchematizedObject<EventSchema>.SchemaInstance.SubjectProperty,
			SchematizedObject<EventSchema>.SchemaInstance.TypeProperty
		};

		// Token: 0x04000088 RID: 136
		public static readonly Microsoft.Exchange.Data.PropertyDefinition[] HardCodedPropertyDependencies = new Microsoft.Exchange.Data.PropertyDefinition[]
		{
			CalendarItemInstanceSchema.PropertyChangeMetadataRaw
		};

		// Token: 0x04000089 RID: 137
		public static readonly Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[] IdProperty = new Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[]
		{
			SchematizedObject<EventSchema>.SchemaInstance.IdProperty
		};

		// Token: 0x0400008A RID: 138
		private static readonly Dictionary<string, Microsoft.Exchange.Data.PropertyDefinition> EventPropertyMap = new Dictionary<string, Microsoft.Exchange.Data.PropertyDefinition>
		{
			{
				"Subject",
				ItemSchema.Subject
			},
			{
				"Start",
				CalendarItemInstanceSchema.StartTime
			},
			{
				"End",
				CalendarItemInstanceSchema.EndTime
			},
			{
				"Importance",
				ItemSchema.Importance
			},
			{
				"IsAllDay",
				CalendarItemBaseSchema.IsAllDayEvent
			},
			{
				"ShowAs",
				InternalSchema.FreeBusyStatus
			},
			{
				"ResponseRequested",
				InternalSchema.IsResponseRequested
			}
		};

		// Token: 0x0400008B RID: 139
		private static IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> hardCodedStorageProperties;
	}
}
