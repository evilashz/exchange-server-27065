using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000700 RID: 1792
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EventCondition
	{
		// Token: 0x060046F9 RID: 18169 RVA: 0x0012DE80 File Offset: 0x0012C080
		public EventCondition()
		{
			this.containerFolderIds = new List<StoreObjectId>();
			this.objectIds = new List<StoreObjectId>();
			this.classNames = new List<string>();
		}

		// Token: 0x060046FA RID: 18170 RVA: 0x0012DEB0 File Offset: 0x0012C0B0
		public EventCondition(EventCondition condition)
		{
			this.classNames = new List<string>(condition.classNames);
			this.containerFolderIds = new List<StoreObjectId>(condition.containerFolderIds);
			this.objectIds = new List<StoreObjectId>(condition.objectIds);
			this.eventType = condition.eventType;
			this.objectType = condition.objectType;
			this.eventFlags = condition.eventFlags;
			this.eventSubtreeFlags = condition.eventSubtreeFlags;
		}

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x0012DF2D File Offset: 0x0012C12D
		// (set) Token: 0x060046FC RID: 18172 RVA: 0x0012DF35 File Offset: 0x0012C135
		public EventSubtreeFlag EventSubtree
		{
			get
			{
				return this.eventSubtreeFlags;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<EventSubtreeFlag>(value);
				if ((value & EventSubtreeFlag.All) == (EventSubtreeFlag)0)
				{
					throw new ArgumentOutOfRangeException("value", ServerStrings.BadEnumValue(typeof(EventSubtreeFlag)));
				}
				this.eventSubtreeFlags = value;
			}
		}

		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x060046FD RID: 18173 RVA: 0x0012DF68 File Offset: 0x0012C168
		// (set) Token: 0x060046FE RID: 18174 RVA: 0x0012DF70 File Offset: 0x0012C170
		public EventType EventType
		{
			get
			{
				return this.eventType;
			}
			set
			{
				if ((value & (EventType.NewMail | EventType.ObjectCreated | EventType.ObjectDeleted | EventType.ObjectModified | EventType.ObjectMoved | EventType.ObjectCopied | EventType.FreeBusyChanged)) != value)
				{
					throw new EnumOutOfRangeException("value", ServerStrings.BadEnumValue(typeof(EventType)));
				}
				this.eventType = value;
			}
		}

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x060046FF RID: 18175 RVA: 0x0012DF9F File Offset: 0x0012C19F
		public ICollection<StoreObjectId> ContainerFolderIds
		{
			get
			{
				return this.containerFolderIds;
			}
		}

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x06004700 RID: 18176 RVA: 0x0012DFA7 File Offset: 0x0012C1A7
		public ICollection<StoreObjectId> ObjectIds
		{
			get
			{
				return this.objectIds;
			}
		}

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x06004701 RID: 18177 RVA: 0x0012DFAF File Offset: 0x0012C1AF
		public ICollection<string> ClassNames
		{
			get
			{
				return this.classNames;
			}
		}

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x06004702 RID: 18178 RVA: 0x0012DFB7 File Offset: 0x0012C1B7
		// (set) Token: 0x06004703 RID: 18179 RVA: 0x0012DFBF File Offset: 0x0012C1BF
		public EventObjectType ObjectType
		{
			get
			{
				return this.objectType;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<EventObjectType>(value);
				this.objectType = value;
			}
		}

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x06004704 RID: 18180 RVA: 0x0012DFCE File Offset: 0x0012C1CE
		// (set) Token: 0x06004705 RID: 18181 RVA: 0x0012DFD6 File Offset: 0x0012C1D6
		public EventFlags EventFlags
		{
			get
			{
				return this.eventFlags;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<EventFlags>(value);
				this.eventFlags = value;
			}
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x0012DFE8 File Offset: 0x0012C1E8
		public override string ToString()
		{
			return string.Format("EventType = {0}. ObjectType = {1}. ContainerFolderIdCount = {2}. ObjectIdCount= {3}. ClassNameCount ={4}.", new object[]
			{
				this.eventType,
				this.objectType,
				(this.containerFolderIds != null) ? this.containerFolderIds.Count : 0,
				(this.objectIds != null) ? this.objectIds.Count : 0,
				(this.classNames != null) ? this.classNames.Count : 0
			});
		}

		// Token: 0x040026CE RID: 9934
		private const EventType ValidEventTypesForCondition = EventType.NewMail | EventType.ObjectCreated | EventType.ObjectDeleted | EventType.ObjectModified | EventType.ObjectMoved | EventType.ObjectCopied | EventType.FreeBusyChanged;

		// Token: 0x040026CF RID: 9935
		private readonly List<StoreObjectId> containerFolderIds;

		// Token: 0x040026D0 RID: 9936
		private readonly List<StoreObjectId> objectIds;

		// Token: 0x040026D1 RID: 9937
		private readonly List<string> classNames;

		// Token: 0x040026D2 RID: 9938
		private EventType eventType;

		// Token: 0x040026D3 RID: 9939
		private EventObjectType objectType;

		// Token: 0x040026D4 RID: 9940
		private EventFlags eventFlags;

		// Token: 0x040026D5 RID: 9941
		private EventSubtreeFlag eventSubtreeFlags = EventSubtreeFlag.All;
	}
}
