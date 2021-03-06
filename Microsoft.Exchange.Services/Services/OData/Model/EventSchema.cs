using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E69 RID: 3689
	internal class EventSchema : ItemSchema
	{
		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x06005FF4 RID: 24564 RVA: 0x0012B285 File Offset: 0x00129485
		public new static EventSchema SchemaInstance
		{
			get
			{
				return EventSchema.EventSchemaInstance.Member;
			}
		}

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x06005FF5 RID: 24565 RVA: 0x0012B291 File Offset: 0x00129491
		public override EdmEntityType EdmEntityType
		{
			get
			{
				return Event.EdmEntityType;
			}
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x06005FF6 RID: 24566 RVA: 0x0012B298 File Offset: 0x00129498
		public override ReadOnlyCollection<PropertyDefinition> DeclaredProperties
		{
			get
			{
				return EventSchema.DeclaredEventProperties;
			}
		}

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06005FF7 RID: 24567 RVA: 0x0012B29F File Offset: 0x0012949F
		public override ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				return EventSchema.AllEventProperties;
			}
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06005FF8 RID: 24568 RVA: 0x0012B2A6 File Offset: 0x001294A6
		public override ReadOnlyCollection<PropertyDefinition> DefaultProperties
		{
			get
			{
				return EventSchema.DefaultEventProperties;
			}
		}

		// Token: 0x06005FF9 RID: 24569 RVA: 0x0012B2B0 File Offset: 0x001294B0
		public override void RegisterEdmModel(EdmModel model)
		{
			base.RegisterEdmModel(model);
			CustomActions.RegisterAction(model, Event.EdmEntityType, null, "Accept", new Dictionary<string, IEdmTypeReference>
			{
				{
					"Comment",
					EdmCoreModel.Instance.GetString(true)
				}
			});
			CustomActions.RegisterAction(model, Event.EdmEntityType, null, "Decline", new Dictionary<string, IEdmTypeReference>
			{
				{
					"Comment",
					EdmCoreModel.Instance.GetString(true)
				}
			});
			CustomActions.RegisterAction(model, Event.EdmEntityType, null, "TentativelyAccept", new Dictionary<string, IEdmTypeReference>
			{
				{
					"Comment",
					EdmCoreModel.Instance.GetString(true)
				}
			});
		}

		// Token: 0x06005FFB RID: 24571 RVA: 0x0012B668 File Offset: 0x00129868
		// Note: this type is marked as 'beforefieldinit'.
		static EventSchema()
		{
			PropertyDefinition propertyDefinition = new PropertyDefinition("Start", typeof(DateTimeOffset));
			propertyDefinition.EdmType = EdmCoreModel.Instance.GetDateTimeOffset(true);
			propertyDefinition.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition2 = propertyDefinition;
			DataEntityPropertyProvider<Event> dataEntityPropertyProvider = new DataEntityPropertyProvider<Event>("Start");
			dataEntityPropertyProvider.Getter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				e[ep] = d.Start.ToDateTimeOffset();
			};
			dataEntityPropertyProvider.Setter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				d.Start = ((DateTimeOffset)e[ep]).ToExDateTime();
			};
			dataEntityPropertyProvider.QueryConstantBuilder = ((object o) => Expression.Constant(((DateTimeOffset)o).ToExDateTime()));
			propertyDefinition2.DataEntityPropertyProvider = dataEntityPropertyProvider;
			EventSchema.Start = propertyDefinition;
			PropertyDefinition propertyDefinition3 = new PropertyDefinition("End", typeof(DateTimeOffset));
			propertyDefinition3.EdmType = EdmCoreModel.Instance.GetDateTimeOffset(true);
			propertyDefinition3.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition4 = propertyDefinition3;
			DataEntityPropertyProvider<Event> dataEntityPropertyProvider2 = new DataEntityPropertyProvider<Event>("End");
			dataEntityPropertyProvider2.Getter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				e[ep] = d.End.ToDateTimeOffset();
			};
			dataEntityPropertyProvider2.Setter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				d.End = ((DateTimeOffset)e[ep]).ToExDateTime();
			};
			dataEntityPropertyProvider2.QueryConstantBuilder = ((object o) => Expression.Constant(((DateTimeOffset)o).ToExDateTime()));
			propertyDefinition4.DataEntityPropertyProvider = dataEntityPropertyProvider2;
			EventSchema.End = propertyDefinition3;
			PropertyDefinition propertyDefinition5 = new PropertyDefinition("Location", typeof(Location));
			propertyDefinition5.EdmType = new EdmComplexTypeReference(Microsoft.Exchange.Services.OData.Model.Location.EdmComplexType.Member, true);
			propertyDefinition5.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition6 = propertyDefinition5;
			DataEntityPropertyProvider<Event> dataEntityPropertyProvider3 = new DataEntityPropertyProvider<Event>("Location");
			dataEntityPropertyProvider3.Getter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				e[ep] = d.Location.ToLocation();
			};
			dataEntityPropertyProvider3.Setter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				d.Location = ((Location)e[ep]).ToDataEntityLocation();
			};
			propertyDefinition6.DataEntityPropertyProvider = dataEntityPropertyProvider3;
			propertyDefinition5.ODataPropertyValueConverter = new LocationODataConverter();
			EventSchema.Location = propertyDefinition5;
			PropertyDefinition propertyDefinition7 = new PropertyDefinition("IsAllDay", typeof(bool));
			propertyDefinition7.EdmType = EdmCoreModel.Instance.GetBoolean(true);
			propertyDefinition7.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition8 = propertyDefinition7;
			DataEntityPropertyProvider<Event> dataEntityPropertyProvider4 = new DataEntityPropertyProvider<Event>("IsAllDay");
			dataEntityPropertyProvider4.Getter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				e[ep] = d.IsAllDay;
			};
			dataEntityPropertyProvider4.Setter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				d.IsAllDay = (bool)e[ep];
			};
			propertyDefinition8.DataEntityPropertyProvider = dataEntityPropertyProvider4;
			EventSchema.IsAllDay = propertyDefinition7;
			PropertyDefinition propertyDefinition9 = new PropertyDefinition("IsCancelled", typeof(bool));
			propertyDefinition9.EdmType = EdmCoreModel.Instance.GetBoolean(true);
			propertyDefinition9.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition10 = propertyDefinition9;
			DataEntityPropertyProvider<Event> dataEntityPropertyProvider5 = new DataEntityPropertyProvider<Event>("IsCancelled");
			dataEntityPropertyProvider5.Getter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				e[ep] = d.IsCancelled;
			};
			dataEntityPropertyProvider5.Setter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				d.IsCancelled = (bool)e[ep];
			};
			propertyDefinition10.DataEntityPropertyProvider = dataEntityPropertyProvider5;
			EventSchema.IsCancelled = propertyDefinition9;
			PropertyDefinition propertyDefinition11 = new PropertyDefinition("IsOrganizer", typeof(bool));
			propertyDefinition11.EdmType = EdmCoreModel.Instance.GetBoolean(true);
			propertyDefinition11.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition12 = propertyDefinition11;
			DataEntityPropertyProvider<Event> dataEntityPropertyProvider6 = new DataEntityPropertyProvider<Event>("IsOrganizer");
			dataEntityPropertyProvider6.Getter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				e[ep] = d.IsOrganizer;
			};
			dataEntityPropertyProvider6.Setter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				d.IsOrganizer = (bool)e[ep];
			};
			propertyDefinition12.DataEntityPropertyProvider = dataEntityPropertyProvider6;
			EventSchema.IsOrganizer = propertyDefinition11;
			PropertyDefinition propertyDefinition13 = new PropertyDefinition("Recurrence", typeof(PatternedRecurrence));
			propertyDefinition13.EdmType = new EdmComplexTypeReference(PatternedRecurrence.EdmComplexType.Member, true);
			propertyDefinition13.Flags = (PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition14 = propertyDefinition13;
			DataEntityPropertyProvider<Event> dataEntityPropertyProvider7 = new DataEntityPropertyProvider<Event>("PatternedRecurrence");
			dataEntityPropertyProvider7.Getter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				e[ep] = d.PatternedRecurrence.ToPatternedRecurrence();
			};
			dataEntityPropertyProvider7.Setter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				d.PatternedRecurrence = ((PatternedRecurrence)e[ep]).ToDataEntityPatternedRecurrence();
			};
			propertyDefinition14.DataEntityPropertyProvider = dataEntityPropertyProvider7;
			propertyDefinition13.ODataPropertyValueConverter = new PatternedRecurrenceODataConverter();
			EventSchema.Recurrence = propertyDefinition13;
			PropertyDefinition propertyDefinition15 = new PropertyDefinition("ResponseRequested", typeof(bool));
			propertyDefinition15.EdmType = EdmCoreModel.Instance.GetBoolean(true);
			propertyDefinition15.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition16 = propertyDefinition15;
			DataEntityPropertyProvider<Event> dataEntityPropertyProvider8 = new DataEntityPropertyProvider<Event>("ResponseRequested");
			dataEntityPropertyProvider8.Getter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				e[ep] = d.ResponseRequested;
			};
			dataEntityPropertyProvider8.Setter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				d.ResponseRequested = (bool)e[ep];
			};
			propertyDefinition16.DataEntityPropertyProvider = dataEntityPropertyProvider8;
			EventSchema.ResponseRequested = propertyDefinition15;
			PropertyDefinition propertyDefinition17 = new PropertyDefinition("SeriesId", typeof(string));
			propertyDefinition17.EdmType = EdmCoreModel.Instance.GetString(true);
			propertyDefinition17.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition18 = propertyDefinition17;
			DataEntityPropertyProvider<Event> dataEntityPropertyProvider9 = new DataEntityPropertyProvider<Event>("SeriesId");
			dataEntityPropertyProvider9.Getter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				e[ep] = d.SeriesId;
			};
			dataEntityPropertyProvider9.Setter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				d.SeriesId = (string)e[ep];
			};
			propertyDefinition18.DataEntityPropertyProvider = dataEntityPropertyProvider9;
			EventSchema.SeriesId = propertyDefinition17;
			PropertyDefinition propertyDefinition19 = new PropertyDefinition("SeriesMasterId", typeof(string));
			propertyDefinition19.EdmType = EdmCoreModel.Instance.GetString(true);
			propertyDefinition19.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition20 = propertyDefinition19;
			DataEntityPropertyProvider<Event> dataEntityPropertyProvider10 = new DataEntityPropertyProvider<Event>("SeriesMasterId");
			dataEntityPropertyProvider10.Getter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				e[ep] = ((d.SeriesMasterId == null) ? null : EwsIdConverter.EwsIdToODataId(d.SeriesMasterId));
			};
			dataEntityPropertyProvider10.Setter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				d.SeriesMasterId = ((e[ep] == null) ? null : EwsIdConverter.ODataIdToEwsId((string)e[ep]));
			};
			propertyDefinition20.DataEntityPropertyProvider = dataEntityPropertyProvider10;
			EventSchema.SeriesMasterId = propertyDefinition19;
			PropertyDefinition propertyDefinition21 = new PropertyDefinition("ShowAs", typeof(FreeBusyStatus));
			propertyDefinition21.EdmType = new EdmEnumTypeReference(EnumTypes.GetEdmEnumType(typeof(FreeBusyStatus)), true);
			propertyDefinition21.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition22 = propertyDefinition21;
			DataEntityPropertyProvider<Event> dataEntityPropertyProvider11 = new DataEntityPropertyProvider<Event>("ShowAs");
			dataEntityPropertyProvider11.Getter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				e[ep] = EnumConverter.CastEnumType<FreeBusyStatus>(d.ShowAs);
			};
			dataEntityPropertyProvider11.Setter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				d.ShowAs = EnumConverter.CastEnumType<FreeBusyStatus>(e[ep]);
			};
			propertyDefinition22.DataEntityPropertyProvider = dataEntityPropertyProvider11;
			EventSchema.ShowAs = propertyDefinition21;
			PropertyDefinition propertyDefinition23 = new PropertyDefinition("Type", typeof(EventType));
			propertyDefinition23.EdmType = new EdmEnumTypeReference(EnumTypes.GetEdmEnumType(typeof(EventType)), true);
			propertyDefinition23.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition24 = propertyDefinition23;
			DataEntityPropertyProvider<Event> dataEntityPropertyProvider12 = new DataEntityPropertyProvider<Event>("Type");
			dataEntityPropertyProvider12.Getter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				e[ep] = EnumConverter.CastEnumType<EventType>(d.Type);
			};
			dataEntityPropertyProvider12.Setter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				d.Type = EnumConverter.CastEnumType<EventType>(e[ep]);
			};
			propertyDefinition24.DataEntityPropertyProvider = dataEntityPropertyProvider12;
			EventSchema.Type = propertyDefinition23;
			PropertyDefinition propertyDefinition25 = new PropertyDefinition("Attendees", typeof(Attendee[]));
			propertyDefinition25.EdmType = new EdmCollectionTypeReference(new EdmCollectionType(new EdmComplexTypeReference(Attendee.EdmComplexType.Member, true)));
			propertyDefinition25.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition26 = propertyDefinition25;
			DataEntityPropertyProvider<Event> dataEntityPropertyProvider13 = new DataEntityPropertyProvider<Event>("Attendees");
			dataEntityPropertyProvider13.Getter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				object value;
				if (d.Attendees != null)
				{
					value = (from x in d.Attendees
					select x.ToAttendee()).ToArray<Attendee>();
				}
				else
				{
					value = null;
				}
				e[ep] = value;
			};
			dataEntityPropertyProvider13.Setter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				d.Attendees = (from x in (Attendee[])e[ep]
				select x.ToDataEntityAttendee()).ToList<Attendee>();
			};
			propertyDefinition26.DataEntityPropertyProvider = dataEntityPropertyProvider13;
			propertyDefinition25.ODataPropertyValueConverter = new AttendeesODataConverter();
			EventSchema.Attendees = propertyDefinition25;
			PropertyDefinition propertyDefinition27 = new PropertyDefinition("Organizer", typeof(Recipient));
			propertyDefinition27.EdmType = new EdmComplexTypeReference(Recipient.EdmComplexType.Member, true);
			propertyDefinition27.Flags = PropertyDefinitionFlags.CanFilter;
			PropertyDefinition propertyDefinition28 = propertyDefinition27;
			DataEntityPropertyProvider<Event> dataEntityPropertyProvider14 = new DataEntityPropertyProvider<Event>("Organizer");
			dataEntityPropertyProvider14.Getter = delegate(Entity e, PropertyDefinition ep, Event d)
			{
				e[ep] = ((d.Organizer == null) ? null : d.Organizer.ToRecipient());
			};
			propertyDefinition28.DataEntityPropertyProvider = dataEntityPropertyProvider14;
			propertyDefinition27.ODataPropertyValueConverter = new RecipientODataConverter();
			EventSchema.Organizer = propertyDefinition27;
			EventSchema.Calendar = new PropertyDefinition("Calendar", typeof(Calendar))
			{
				Flags = PropertyDefinitionFlags.Navigation,
				NavigationTargetEntity = Microsoft.Exchange.Services.OData.Model.Calendar.EdmEntityType
			};
			EventSchema.DeclaredEventProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				EventSchema.Start,
				EventSchema.End,
				EventSchema.Location,
				EventSchema.ShowAs,
				EventSchema.IsAllDay,
				EventSchema.IsCancelled,
				EventSchema.IsOrganizer,
				EventSchema.ResponseRequested,
				EventSchema.Type,
				EventSchema.SeriesId,
				EventSchema.SeriesMasterId,
				EventSchema.Attendees,
				EventSchema.Recurrence,
				EventSchema.Organizer,
				ItemSchema.DateTimeCreated,
				ItemSchema.LastModifiedTime,
				EventSchema.Calendar
			});
			EventSchema.AllEventProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(ItemSchema.AllItemProperties.Union(EventSchema.DeclaredEventProperties)));
			EventSchema.DefaultEventProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(ItemSchema.DefaultItemProperties)
			{
				EventSchema.Start,
				EventSchema.End,
				EventSchema.Location,
				EventSchema.ShowAs,
				EventSchema.IsAllDay,
				EventSchema.IsCancelled,
				EventSchema.IsOrganizer,
				EventSchema.ResponseRequested,
				EventSchema.Type,
				EventSchema.SeriesId,
				EventSchema.SeriesMasterId,
				EventSchema.Attendees,
				EventSchema.Recurrence,
				ItemSchema.DateTimeCreated,
				ItemSchema.LastModifiedTime,
				EventSchema.Organizer
			});
			EventSchema.EventSchemaInstance = new LazyMember<EventSchema>(() => new EventSchema());
			EventSchema.ODataToEdmPropertyMap = new Dictionary<PropertyDefinition, PropertyDefinition>
			{
				{
					EventSchema.Start,
					SchematizedObject<EventSchema>.SchemaInstance.StartProperty
				},
				{
					EventSchema.End,
					SchematizedObject<EventSchema>.SchemaInstance.EndProperty
				},
				{
					EventSchema.Location,
					SchematizedObject<EventSchema>.SchemaInstance.LocationProperty
				},
				{
					EventSchema.ShowAs,
					SchematizedObject<EventSchema>.SchemaInstance.ShowAsProperty
				},
				{
					EventSchema.IsAllDay,
					SchematizedObject<EventSchema>.SchemaInstance.IsAllDayProperty
				},
				{
					EventSchema.IsCancelled,
					SchematizedObject<EventSchema>.SchemaInstance.IsCancelledProperty
				},
				{
					EventSchema.IsOrganizer,
					SchematizedObject<EventSchema>.SchemaInstance.IsOrganizerProperty
				},
				{
					EventSchema.ResponseRequested,
					SchematizedObject<EventSchema>.SchemaInstance.ResponseRequestedProperty
				},
				{
					EventSchema.Type,
					SchematizedObject<EventSchema>.SchemaInstance.TypeProperty
				},
				{
					EventSchema.SeriesId,
					SchematizedObject<EventSchema>.SchemaInstance.SeriesIdProperty
				},
				{
					EventSchema.SeriesMasterId,
					SchematizedObject<EventSchema>.SchemaInstance.SeriesMasterIdProperty
				},
				{
					EventSchema.Attendees,
					SchematizedObject<EventSchema>.SchemaInstance.AttendeesProperty
				},
				{
					EventSchema.Recurrence,
					SchematizedObject<EventSchema>.SchemaInstance.PatternedRecurrenceProperty
				},
				{
					EventSchema.Calendar,
					SchematizedObject<EventSchema>.SchemaInstance.CalendarProperty
				},
				{
					ItemSchema.ChangeKey,
					SchematizedObject<EventSchema>.SchemaInstance.ChangeKeyProperty
				},
				{
					ItemSchema.Subject,
					SchematizedObject<EventSchema>.SchemaInstance.SubjectProperty
				},
				{
					ItemSchema.Body,
					SchematizedObject<EventSchema>.SchemaInstance.BodyProperty
				},
				{
					ItemSchema.BodyPreview,
					SchematizedObject<EventSchema>.SchemaInstance.PreviewProperty
				},
				{
					ItemSchema.Importance,
					SchematizedObject<EventSchema>.SchemaInstance.ImportanceProperty
				},
				{
					ItemSchema.Categories,
					SchematizedObject<EventSchema>.SchemaInstance.CategoriesProperty
				},
				{
					ItemSchema.HasAttachments,
					SchematizedObject<EventSchema>.SchemaInstance.HasAttachmentsProperty
				},
				{
					EntitySchema.Id,
					SchematizedObject<EventSchema>.SchemaInstance.IdProperty
				}
			};
		}

		// Token: 0x040033FD RID: 13309
		public static readonly PropertyDefinition Start;

		// Token: 0x040033FE RID: 13310
		public static readonly PropertyDefinition End;

		// Token: 0x040033FF RID: 13311
		public static readonly PropertyDefinition Location;

		// Token: 0x04003400 RID: 13312
		public static readonly PropertyDefinition IsAllDay;

		// Token: 0x04003401 RID: 13313
		public static readonly PropertyDefinition IsCancelled;

		// Token: 0x04003402 RID: 13314
		public static readonly PropertyDefinition IsOrganizer;

		// Token: 0x04003403 RID: 13315
		public static readonly PropertyDefinition Recurrence;

		// Token: 0x04003404 RID: 13316
		public static readonly PropertyDefinition ResponseRequested;

		// Token: 0x04003405 RID: 13317
		public static readonly PropertyDefinition SeriesId;

		// Token: 0x04003406 RID: 13318
		public static readonly PropertyDefinition SeriesMasterId;

		// Token: 0x04003407 RID: 13319
		public static readonly PropertyDefinition ShowAs;

		// Token: 0x04003408 RID: 13320
		public static readonly PropertyDefinition Type;

		// Token: 0x04003409 RID: 13321
		public static readonly PropertyDefinition Attendees;

		// Token: 0x0400340A RID: 13322
		public static readonly PropertyDefinition Organizer;

		// Token: 0x0400340B RID: 13323
		public static readonly PropertyDefinition Calendar;

		// Token: 0x0400340C RID: 13324
		public static readonly ReadOnlyCollection<PropertyDefinition> DeclaredEventProperties;

		// Token: 0x0400340D RID: 13325
		public static readonly ReadOnlyCollection<PropertyDefinition> AllEventProperties;

		// Token: 0x0400340E RID: 13326
		public static readonly ReadOnlyCollection<PropertyDefinition> DefaultEventProperties;

		// Token: 0x0400340F RID: 13327
		private static readonly LazyMember<EventSchema> EventSchemaInstance;

		// Token: 0x04003410 RID: 13328
		internal static Dictionary<PropertyDefinition, PropertyDefinition> ODataToEdmPropertyMap;
	}
}
