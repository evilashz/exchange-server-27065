using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Storage.Contacts.ChangeLogger;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200062D RID: 1581
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class COWContactLogging : ICOWNotification
	{
		// Token: 0x06004127 RID: 16679 RVA: 0x00112080 File Offset: 0x00110280
		internal COWContactLogging()
		{
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x00112088 File Offset: 0x00110288
		public bool SkipItemOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, COWTriggerActionState state, StoreSession session, StoreObjectId itemId, CoreItem item, bool onBeforeNotification, bool onDumpster, bool success, CallbackContext callbackContext)
		{
			if (!COWContactLogging.COWContactLoggingConfiguration.Instance.IsLoggingEnabled())
			{
				return true;
			}
			Util.ThrowOnNullArgument(session, "session");
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<COWTriggerActionState>(state, "state");
			if (item == null)
			{
				COWContactLogging.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLogging.SkipItemOperation: Item is null");
				return true;
			}
			if (!onBeforeNotification)
			{
				COWContactLogging.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLogging.SkipItemOperation: Not onBeforeNotification");
				return true;
			}
			string valueOrDefault = item.PropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
			COWContactLogging.Tracer.TraceDebug<string>((long)this.GetHashCode(), "COWContactLogging.SkipItemOperation: ItemClass: {0}", valueOrDefault);
			if (ObjectClass.IsPlace(valueOrDefault))
			{
				return true;
			}
			if (!ObjectClass.IsContact(valueOrDefault) && !ObjectClass.IsDistributionList(valueOrDefault) && !ObjectClass.IsContactsFolder(valueOrDefault))
			{
				return true;
			}
			foreach (IContactChangeTracker contactChangeTracker in COWContactLogging.ChangeTrackers)
			{
				if (contactChangeTracker.ShouldLoadPropertiesForFurtherCheck(operation, valueOrDefault, itemId, item))
				{
					COWContactLogging.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLogging.SkipItemOperation: A tracker interested.");
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x00112198 File Offset: 0x00110398
		public void ItemOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, COWTriggerActionState state, StoreSession session, StoreObjectId itemId, CoreItem item, CoreFolder folder, bool onBeforeNotification, OperationResult result, CallbackContext callbackContext)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(item, "item");
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<COWTriggerActionState>(state, "state");
			EnumValidator.ThrowIfInvalid<OperationResult>(result, "result");
			COWContactLogging.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLogging.ItemOperation: Start.");
			string valueOrDefault = item.PropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
			COWContactLogging.Tracer.TraceDebug<string>((long)this.GetHashCode(), "COWContactLogging.ItemOperation: ItemClass: {0}", valueOrDefault);
			HashSet<StorePropertyDefinition> hashSet = new HashSet<StorePropertyDefinition>();
			List<IContactChangeTracker> list = new List<IContactChangeTracker>(COWContactLogging.ChangeTrackers.Length);
			foreach (IContactChangeTracker contactChangeTracker in COWContactLogging.ChangeTrackers)
			{
				if (contactChangeTracker.ShouldLoadPropertiesForFurtherCheck(operation, valueOrDefault, itemId, item))
				{
					COWContactLogging.Tracer.TraceDebug<string>((long)this.GetHashCode(), "COWContactLogging.ItemOperation: Tracker {0} interested.", contactChangeTracker.Name);
					list.Add(contactChangeTracker);
					StorePropertyDefinition[] properties = contactChangeTracker.GetProperties(itemId, item);
					if (properties != null && properties.Length > 0)
					{
						COWContactLogging.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "COWContactLogging.ItemOperation: Tracker {0} returned {1} properties for tracking", contactChangeTracker.Name, properties.Length);
						hashSet.UnionWith(properties);
					}
				}
			}
			if (hashSet.Count == 0)
			{
				COWContactLogging.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLogging.ItemOperation: No properties marked as interesting by any tracker.");
				return;
			}
			hashSet.Add(ItemSchema.Id);
			hashSet.Add(StoreObjectSchema.ItemClass);
			StorePropertyDefinition[] array = new StorePropertyDefinition[hashSet.Count];
			hashSet.CopyTo(array, 0, hashSet.Count);
			item.PropertyBag.Load(array);
			bool flag = false;
			foreach (IContactChangeTracker contactChangeTracker2 in list)
			{
				if (contactChangeTracker2.ShouldLogContact(itemId, item))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				COWContactLogging.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLogging.ItemOperation: No trackers are interested after doing further checks.");
				return;
			}
			COWContactLogging.ContactChangeLogEvent contactChangeLogEvent = new COWContactLogging.ContactChangeLogEvent();
			contactChangeLogEvent.Add("ClientInfo", session.ClientInfoString);
			contactChangeLogEvent.Add("Action", operation.ToString());
			COWContactLogging.Tracer.TraceDebug<COWTriggerAction, string, int>((long)this.GetHashCode(), "COWContactLogging.ItemOperation: Tracking change {0} made by client {1} across a total of {2} properties", operation, session.ClientInfoString, hashSet.Count);
			this.LogInterestingPropertyValues(contactChangeLogEvent, operation, item, hashSet);
			ContactChangeLogger contactChangeLogger = new ContactChangeLogger(session);
			contactChangeLogger.LogEvent(contactChangeLogEvent);
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x0011240C File Offset: 0x0011060C
		public CowClientOperationSensitivity SkipGroupOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, bool onBeforeNotification, bool onDumpster, CallbackContext callbackContext)
		{
			if (!COWContactLogging.COWContactLoggingConfiguration.Instance.IsLoggingEnabled())
			{
				return CowClientOperationSensitivity.Skip;
			}
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<FolderChangeOperationFlags>(flags, "flags");
			Util.ThrowOnNullArgument(sourceSession, "sourceSession");
			if (!onBeforeNotification)
			{
				COWContactLogging.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLogging.SkipGroupOperation: not OnBeforeNotification");
				return CowClientOperationSensitivity.Skip;
			}
			COWContactLogging.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLogging.SkipGroupOperation: SourceFolderId.ObjectType={0}, DestinationFolderId.ObjectType={1}", new object[]
			{
				(sourceFolderId == null) ? "<<Null>>" : sourceFolderId.ObjectType,
				(destinationFolderId == null) ? "<<Null>>" : destinationFolderId.ObjectType
			});
			bool flag = true;
			if ((sourceFolderId != null && sourceFolderId.ObjectType == StoreObjectType.ContactsFolder) || (destinationFolderId != null && destinationFolderId.ObjectType == StoreObjectType.ContactsFolder))
			{
				flag = false;
			}
			else if (itemIds != null)
			{
				foreach (StoreObjectId storeObjectId in itemIds)
				{
					COWContactLogging.Tracer.TraceDebug<StoreObjectType>((long)this.GetHashCode(), "COWContactLogging.SkipGroupOperation: ItemId.ObjectType = {0}", storeObjectId.ObjectType);
					if (storeObjectId.ObjectType == StoreObjectType.Contact || storeObjectId.ObjectType == StoreObjectType.DistributionList)
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				foreach (IContactChangeTracker contactChangeTracker in COWContactLogging.ChangeTrackers)
				{
					if (contactChangeTracker.ShouldLogGroupOperation(operation, sourceSession, sourceFolderId, destinationSession, destinationFolderId, itemIds))
					{
						COWContactLogging.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLogging.SkipGroupOperation: A tracker interested.");
						COWContactLogging.ContactChangeLogEvent contactChangeLogEvent = new COWContactLogging.ContactChangeLogEvent();
						contactChangeLogEvent.Add("Action", operation.ToString());
						contactChangeLogEvent.Add("FolderChangeOperationFlags", flags.ToString());
						contactChangeLogEvent.Add("ClientInfo", sourceSession.ClientInfoString);
						if (destinationSession != null && destinationSession.ClientInfoString != sourceSession.ClientInfoString)
						{
							contactChangeLogEvent.Add("DestinationClientInfo", destinationSession.ClientInfoString);
						}
						if (sourceFolderId != null)
						{
							contactChangeLogEvent.Add("FolderId", sourceFolderId.ToString());
						}
						if (destinationFolderId != null && !object.Equals(sourceFolderId, destinationFolderId))
						{
							contactChangeLogEvent.Add("DestinationFolderId", destinationFolderId.ToString());
						}
						if (destinationSession != null && !object.Equals(sourceSession.MailboxGuid, destinationSession.MailboxGuid))
						{
							contactChangeLogEvent.Add("DestinationMailboxGuid", destinationSession.MailboxGuid.ToString());
						}
						string value = this.SanitizePropertyValueForLogging(itemIds);
						contactChangeLogEvent.Add("ItemIds", value);
						ContactChangeLogger contactChangeLogger = new ContactChangeLogger(sourceSession);
						contactChangeLogger.LogEvent(contactChangeLogEvent);
						break;
					}
				}
			}
			return CowClientOperationSensitivity.Skip;
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x001126C0 File Offset: 0x001108C0
		public void GroupOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId destinationFolderId, StoreObjectId[] itemIds, GroupOperationResult result, bool onBeforeNotification, CallbackContext callbackContext)
		{
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<FolderChangeOperationFlags>(flags, "flags");
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x001126DC File Offset: 0x001108DC
		private void LogInterestingPropertyValues(COWContactLogging.ContactChangeLogEvent loggingEvent, COWTriggerAction operation, CoreItem item, IEnumerable<StorePropertyDefinition> allUniqueInterestingProperties)
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			foreach (StorePropertyDefinition storePropertyDefinition in allUniqueInterestingProperties)
			{
				stringBuilder.Clear();
				object obj = this.GetPropertyValueForLogging(item, storePropertyDefinition);
				obj = this.SanitizePropertyValueForLogging(obj);
				stringBuilder.Append("[New=");
				stringBuilder.Append(obj);
				bool flag = item.PropertyBag.IsPropertyDirty(storePropertyDefinition);
				if (flag && operation != COWTriggerAction.Create)
				{
					object obj2 = this.GetOriginalPropertyValueForLogging(item, storePropertyDefinition);
					obj2 = this.SanitizePropertyValueForLogging(obj2);
					if (obj2 != obj)
					{
						stringBuilder.Append(",Old=");
						stringBuilder.Append(obj2);
					}
				}
				stringBuilder.Append("]");
				string key = SpecialCharacters.SanitizeForLogging(storePropertyDefinition.Name);
				loggingEvent.Add(key, stringBuilder.ToString());
			}
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x001127C8 File Offset: 0x001109C8
		private object GetPropertyValueForLogging(CoreItem item, StorePropertyDefinition interestingProperty)
		{
			if ((interestingProperty.PropertyFlags & PropertyFlags.Streamable) == PropertyFlags.Streamable)
			{
				COWContactLogging.Tracer.TraceDebug<string>((long)this.GetHashCode(), "COWContactLogging.GetPropertyValueForLogging: Skipping retrieval of value for streamable property {0}", interestingProperty.Name);
				return null;
			}
			return item.PropertyBag.GetValueOrDefault<object>(interestingProperty, null);
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x00112804 File Offset: 0x00110A04
		private object GetOriginalPropertyValueForLogging(CoreItem item, StorePropertyDefinition interestingProperty)
		{
			if ((interestingProperty.PropertyFlags & PropertyFlags.Streamable) == PropertyFlags.Streamable)
			{
				COWContactLogging.Tracer.TraceDebug<string>((long)this.GetHashCode(), "COWContactLogging.GetOriginalPropertyValueForLogging: Skipping retrieval of value for streamable property {0}", interestingProperty.Name);
				return null;
			}
			IValidatablePropertyBag validatablePropertyBag = item.PropertyBag as IValidatablePropertyBag;
			if (validatablePropertyBag == null)
			{
				COWContactLogging.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLogging.GetOriginalPropertyValueForLogging: Skipping retrieval of value as property bag doesn't track original values.");
				return null;
			}
			PropertyValueTrackingData originalPropertyInformation = validatablePropertyBag.GetOriginalPropertyInformation(interestingProperty);
			return originalPropertyInformation.OriginalPropertyValue;
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x00112871 File Offset: 0x00110A71
		private string SanitizePropertyValueForLogging(object propertyValue)
		{
			if (propertyValue == null)
			{
				return string.Empty;
			}
			return ContactLinkingStrings.GetValueString(propertyValue);
		}

		// Token: 0x040023EF RID: 9199
		private static readonly Trace Tracer = ExTraceGlobals.ContactChangeLoggingTracer;

		// Token: 0x040023F0 RID: 9200
		private static readonly IContactChangeTracker[] ChangeTrackers = new IContactChangeTracker[]
		{
			new ContactEmailChangeLogger(),
			new UcsTracker(),
			new ContactTracker()
		};

		// Token: 0x0200062E RID: 1582
		private sealed class ContactChangeLogEvent : ILogEvent
		{
			// Token: 0x06004131 RID: 16689 RVA: 0x001128C0 File Offset: 0x00110AC0
			public ContactChangeLogEvent()
			{
				this.data = new Dictionary<string, object>(StringComparer.Ordinal);
			}

			// Token: 0x17001352 RID: 4946
			// (get) Token: 0x06004132 RID: 16690 RVA: 0x001128D8 File Offset: 0x00110AD8
			public string EventId
			{
				get
				{
					return "ChangeTracker";
				}
			}

			// Token: 0x06004133 RID: 16691 RVA: 0x001128DF File Offset: 0x00110ADF
			public ICollection<KeyValuePair<string, object>> GetEventData()
			{
				return this.data;
			}

			// Token: 0x06004134 RID: 16692 RVA: 0x001128E7 File Offset: 0x00110AE7
			public void Add(string key, string value)
			{
				this.data.Add(key, value);
			}

			// Token: 0x040023F1 RID: 9201
			private readonly Dictionary<string, object> data;
		}

		// Token: 0x0200062F RID: 1583
		private sealed class COWContactLoggingConfiguration
		{
			// Token: 0x06004135 RID: 16693 RVA: 0x001128F6 File Offset: 0x00110AF6
			private COWContactLoggingConfiguration()
			{
			}

			// Token: 0x17001353 RID: 4947
			// (get) Token: 0x06004136 RID: 16694 RVA: 0x001128FE File Offset: 0x00110AFE
			public static COWContactLogging.COWContactLoggingConfiguration Instance
			{
				get
				{
					return COWContactLogging.COWContactLoggingConfiguration.instance;
				}
			}

			// Token: 0x06004137 RID: 16695 RVA: 0x00112905 File Offset: 0x00110B05
			public bool IsLoggingEnabled()
			{
				if (this.loggingEnabled == null)
				{
					this.loggingEnabled = new int?(Util.GetRegistryValueOrDefault(COWContactLogging.COWContactLoggingConfiguration.RegistryKeysLocation, COWContactLogging.COWContactLoggingConfiguration.EnableCOWContactLogging, 1, COWContactLogging.Tracer));
				}
				return this.loggingEnabled.Value == 1;
			}

			// Token: 0x040023F2 RID: 9202
			private const int LoggingEnabledValue = 1;

			// Token: 0x040023F3 RID: 9203
			private static readonly string RegistryKeysLocation = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\People";

			// Token: 0x040023F4 RID: 9204
			private static readonly string EnableCOWContactLogging = "EnableCOWContactLogging";

			// Token: 0x040023F5 RID: 9205
			private static COWContactLogging.COWContactLoggingConfiguration instance = new COWContactLogging.COWContactLoggingConfiguration();

			// Token: 0x040023F6 RID: 9206
			private int? loggingEnabled;
		}
	}
}
