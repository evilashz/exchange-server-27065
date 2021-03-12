using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000169 RID: 361
	internal abstract class NavigationNode
	{
		// Token: 0x06000C52 RID: 3154 RVA: 0x00054ADD File Offset: 0x00052CDD
		protected NavigationNode(MemoryPropertyBag propertyBag)
		{
			this.propertyBag = propertyBag;
			this.propertyBag.SetAllPropertiesLoaded();
			this.isNew = false;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00054B08 File Offset: 0x00052D08
		protected NavigationNode(PropertyDefinition[] additionalProperties, object[] values, Dictionary<PropertyDefinition, int> propertyMap)
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>(NavigationNode.coreProperties);
			hashSet.UnionWith(additionalProperties);
			ICollection<NativeStorePropertyDefinition> nativePropertyDefinitions = StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.AllRead, hashSet);
			object[] array = new object[nativePropertyDefinitions.Count];
			int num = 0;
			foreach (NativeStorePropertyDefinition key in nativePropertyDefinitions)
			{
				array[num++] = values[propertyMap[key]];
			}
			this.propertyBag = new MemoryPropertyBag();
			this.propertyBag.PreLoadStoreProperty<NativeStorePropertyDefinition>(nativePropertyDefinitions, array);
			this.propertyBag.SetAllPropertiesLoaded();
			this.ClearDirty();
			this.isNew = false;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00054BC8 File Offset: 0x00052DC8
		protected NavigationNode(NavigationNodeType navigationNodeType, string subject, NavigationNodeGroupSection navigationNodeGroupSection)
		{
			this.propertyBag = new MemoryPropertyBag();
			this.propertyBag.SetAllPropertiesLoaded();
			this.NavigationNodeType = navigationNodeType;
			this.Subject = subject;
			this.NavigationNodeFlags = NavigationNodeFlags.None;
			this.NavigationNodeClassId = NavigationNode.GetFolderTypeClassId(navigationNodeGroupSection);
			this.NavigationNodeGroupSection = navigationNodeGroupSection;
			this.propertyBag.Load(null);
			this.isNew = true;
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00054C33 File Offset: 0x00052E33
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x00054C4A File Offset: 0x00052E4A
		public virtual string Subject
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<string>(ItemSchema.Subject, string.Empty);
			}
			set
			{
				if (!string.IsNullOrEmpty(value.Trim()))
				{
					this.propertyBag.SetOrDeleteProperty(ItemSchema.Subject, value);
				}
			}
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00054C6C File Offset: 0x00052E6C
		internal static string GetFolderClass(NavigationNodeGroupSection groupSection)
		{
			switch (groupSection)
			{
			case NavigationNodeGroupSection.Calendar:
				return "IPF.Appointment";
			case NavigationNodeGroupSection.Contacts:
				return "IPF.Contact";
			case NavigationNodeGroupSection.Tasks:
				return "IPF.Task";
			default:
				return "IPF.Note";
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00054CA8 File Offset: 0x00052EA8
		internal VersionedId NavigationNodeId
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x00054CBA File Offset: 0x00052EBA
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x00054CCD File Offset: 0x00052ECD
		internal NavigationNodeType NavigationNodeType
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<NavigationNodeType>(NavigationNodeSchema.Type, NavigationNodeType.NormalFolder);
			}
			private set
			{
				this.propertyBag.SetOrDeleteProperty(NavigationNodeSchema.Type, value);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x00054CE5 File Offset: 0x00052EE5
		// (set) Token: 0x06000C5C RID: 3164 RVA: 0x00054CF7 File Offset: 0x00052EF7
		protected NavigationNodeFlags NavigationNodeFlags
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<NavigationNodeFlags>(NavigationNodeSchema.Flags);
			}
			set
			{
				this.propertyBag.SetOrDeleteProperty(NavigationNodeSchema.Flags, value);
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00054D0F File Offset: 0x00052F0F
		internal bool IsFlagSet(NavigationNodeFlags flag)
		{
			return Utilities.IsFlagSet((int)this.NavigationNodeFlags, (int)flag);
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00054D1D File Offset: 0x00052F1D
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x00054D2F File Offset: 0x00052F2F
		internal byte[] NavigationNodeOrdinal
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<byte[]>(NavigationNodeSchema.Ordinal);
			}
			set
			{
				this.propertyBag.SetOrDeleteProperty(NavigationNodeSchema.Ordinal, value);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00054D42 File Offset: 0x00052F42
		internal ExDateTime LastModifiedTime
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<ExDateTime>(StoreObjectSchema.LastModifiedTime, ExDateTime.MinValue);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00054D59 File Offset: 0x00052F59
		internal bool IsDirty
		{
			get
			{
				return this.propertyBag.IsDirty;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00054D66 File Offset: 0x00052F66
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x00054D6E File Offset: 0x00052F6E
		internal bool IsNew
		{
			get
			{
				return this.isNew;
			}
			set
			{
				this.isNew = value;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00054D77 File Offset: 0x00052F77
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00054D89 File Offset: 0x00052F89
		internal NavigationNodeGroupSection NavigationNodeGroupSection
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<NavigationNodeGroupSection>(NavigationNodeSchema.GroupSection);
			}
			private set
			{
				this.propertyBag.SetOrDeleteProperty(NavigationNodeSchema.GroupSection, value);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00054DA4 File Offset: 0x00052FA4
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00054DD2 File Offset: 0x00052FD2
		private byte[] NavigationNodeClassId
		{
			get
			{
				byte[] array = this.propertyBag.GetValueOrDefault<byte[]>(NavigationNodeSchema.ClassId);
				if (array == null)
				{
					array = NavigationNode.GetFolderTypeClassId(this.NavigationNodeGroupSection);
				}
				return array;
			}
			set
			{
				this.propertyBag.SetOrDeleteProperty(NavigationNodeSchema.ClassId, value);
			}
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00054DE8 File Offset: 0x00052FE8
		internal void Save(MailboxSession session)
		{
			MessageItem messageItem = null;
			try
			{
				if (this.NavigationNodeId == null)
				{
					messageItem = MessageItem.CreateAssociated(session, session.GetDefaultFolderId(DefaultFolderType.CommonViews));
					messageItem[StoreObjectSchema.ItemClass] = "IPM.Microsoft.WunderBar.Link";
				}
				else
				{
					messageItem = MessageItem.Bind(session, this.NavigationNodeId);
				}
				this.UpdateMessage(messageItem);
				messageItem.Save(SaveMode.NoConflictResolution);
				this.ClearDirty();
				this.isNew = false;
			}
			catch (StorageTransientException ex)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "NavigationNode.Save. Unable to save navigation node. Exception: {0}.", ex.Message);
			}
			catch (StoragePermanentException ex2)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "NavigationNode.Save. Unable to get save navigation node. Exception: {0}.", ex2.Message);
			}
			finally
			{
				if (messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00054EC0 File Offset: 0x000530C0
		protected virtual void UpdateMessage(MessageItem message)
		{
			message[ItemSchema.Subject] = this.Subject;
			message[NavigationNodeSchema.OutlookTagId] = NavigationNode.UpdateOutlookTagId();
			message[NavigationNodeSchema.Type] = this.NavigationNodeType;
			message[NavigationNodeSchema.Flags] = this.NavigationNodeFlags;
			message[NavigationNodeSchema.Ordinal] = this.NavigationNodeOrdinal;
			message[NavigationNodeSchema.ClassId] = this.NavigationNodeClassId;
			message[NavigationNodeSchema.GroupSection] = this.NavigationNodeGroupSection;
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00054F57 File Offset: 0x00053157
		protected void ClearDirty()
		{
			this.propertyBag.ClearChangeInfo();
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00054F64 File Offset: 0x00053164
		private static int UpdateOutlookTagId()
		{
			return NavigationNode.random.Next();
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00054F70 File Offset: 0x00053170
		private static byte[] GetFolderTypeClassId(NavigationNodeGroupSection navigationNodeGroupSection)
		{
			switch (navigationNodeGroupSection)
			{
			case NavigationNodeGroupSection.First:
				return NavigationNode.MailFavoritesClassId.ToByteArray();
			case NavigationNodeGroupSection.Mail:
				return NavigationNode.MailClassId.ToByteArray();
			case NavigationNodeGroupSection.Calendar:
				return NavigationNode.CalendarClassId.ToByteArray();
			case NavigationNodeGroupSection.Contacts:
				return NavigationNode.ContactsClassId.ToByteArray();
			case NavigationNodeGroupSection.Tasks:
				return NavigationNode.TasksClassId.ToByteArray();
			case NavigationNodeGroupSection.Notes:
				return NavigationNode.NotesClassId.ToByteArray();
			case NavigationNodeGroupSection.Journal:
				return NavigationNode.JournalClassId.ToByteArray();
			default:
				throw new ArgumentOutOfRangeException("navigationNodeGroupSection");
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00055015 File Offset: 0x00053215
		protected void GuidSetter(PropertyDefinition propertyDefinition, Guid guid)
		{
			this.propertyBag.SetOrDeleteProperty(propertyDefinition, guid.Equals(Guid.Empty) ? null : guid.ToByteArray());
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0005503C File Offset: 0x0005323C
		protected Guid GuidGetter(PropertyDefinition propertyDefinition)
		{
			byte[] valueOrDefault = this.propertyBag.GetValueOrDefault<byte[]>(propertyDefinition);
			if (valueOrDefault != null)
			{
				return new Guid(valueOrDefault);
			}
			return Guid.Empty;
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00055065 File Offset: 0x00053265
		protected bool IsFavorites
		{
			get
			{
				return this.NavigationNodeGroupSection == NavigationNodeGroupSection.First;
			}
		}

		// Token: 0x040008E5 RID: 2277
		internal const string OutlookWunderBarLinkAssociatedMessageClass = "IPM.Microsoft.WunderBar.Link";

		// Token: 0x040008E6 RID: 2278
		private static readonly Guid MailFavoritesClassId = new Guid("{00067800-0000-0000-C000-000000000046}");

		// Token: 0x040008E7 RID: 2279
		private static readonly Guid MailClassId = new Guid("{0006780D-0000-0000-C000-000000000046}");

		// Token: 0x040008E8 RID: 2280
		private static readonly Guid CalendarClassId = new Guid("{00067802-0000-0000-c000-000000000046}");

		// Token: 0x040008E9 RID: 2281
		private static readonly Guid ContactsClassId = new Guid("{00067801-0000-0000-C000-000000000046}");

		// Token: 0x040008EA RID: 2282
		private static readonly Guid TasksClassId = new Guid("{00067803-0000-0000-C000-000000000046}");

		// Token: 0x040008EB RID: 2283
		private static readonly Guid NotesClassId = new Guid("{00067804-0000-0000-C000-000000000046}");

		// Token: 0x040008EC RID: 2284
		private static readonly Guid JournalClassId = new Guid("{00067808-0000-0000-C000-000000000046}");

		// Token: 0x040008ED RID: 2285
		private static readonly PropertyDefinition[] coreProperties = new PropertyDefinition[]
		{
			NavigationNodeSchema.Type,
			StoreObjectSchema.ItemClass,
			NavigationNodeSchema.OutlookTagId,
			NavigationNodeSchema.Flags,
			NavigationNodeSchema.Ordinal,
			NavigationNodeSchema.ClassId,
			NavigationNodeSchema.GroupSection,
			ItemSchema.Id,
			ItemSchema.Subject,
			StoreObjectSchema.LastModifiedTime
		};

		// Token: 0x040008EE RID: 2286
		protected MemoryPropertyBag propertyBag;

		// Token: 0x040008EF RID: 2287
		private static readonly Random random = new Random((int)ExDateTime.UtcNow.UtcTicks);

		// Token: 0x040008F0 RID: 2288
		private bool isNew = true;
	}
}
