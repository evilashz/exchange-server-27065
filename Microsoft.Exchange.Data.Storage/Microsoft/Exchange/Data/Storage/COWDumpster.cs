﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000630 RID: 1584
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class COWDumpster : ICOWNotification
	{
		// Token: 0x06004139 RID: 16697 RVA: 0x0011299C File Offset: 0x00110B9C
		public static bool IsItemLegallyDirty(StoreSession session, CoreItem item, bool verifyLegallyDirty, out List<string> dirtyProperties)
		{
			dirtyProperties = null;
			if (COWSettings.IsCalendarRepairAssistantAction(session))
			{
				ExTraceGlobals.SessionTracer.TraceWarning<CoreItem>((long)session.GetHashCode(), "Update Item {0} skipped because the action was generated by Calendar Repair Assistant.", item);
				return false;
			}
			if (item.PropertyBag.GetValueOrDefault<bool>(InternalSchema.IsDraft))
			{
				ExTraceGlobals.SessionTracer.TraceWarning<CoreItem>((long)session.GetHashCode(), "Update Item {0} skipped because it is a draft.", item);
				return false;
			}
			if (item.Id == null)
			{
				ExTraceGlobals.SessionTracer.TraceWarning<CoreItem>((long)session.GetHashCode(), "Update Item {0} skipped because it does not have an id.", item);
				return false;
			}
			if (item.PropertyBag.GetValueOrDefault<bool>(InternalSchema.IsAssociated))
			{
				ExTraceGlobals.SessionTracer.TraceWarning<CoreItem>((long)session.GetHashCode(), "Update Item {0} skipped because it is an associated item.", item);
				return false;
			}
			if (item.Id == null)
			{
				ExTraceGlobals.SessionTracer.TraceWarning<CoreItem>((long)session.GetHashCode(), "Update Item {0} skipped because the id was null.", item);
				return false;
			}
			string valueOrDefault = item.PropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
			if (ObjectClass.IsOfClass(valueOrDefault, "IPM.Post.RSS"))
			{
				ExTraceGlobals.SessionTracer.TraceWarning<CoreItem>((long)session.GetHashCode(), "Update Item {0} skipped because it is a RSS item.", item);
				return false;
			}
			Schema schema = ObjectClass.GetSchema(valueOrDefault);
			if (!(schema is ItemSchema))
			{
				schema = ItemSchema.Instance;
			}
			bool isLegallyDirty = item.IsLegallyDirty;
			CoreRecipientCollection recipients = ((ICoreItem)item).Recipients;
			CoreAttachmentCollection attachmentCollection = ((ICoreItem)item).AttachmentCollection;
			bool flag = (recipients != null && recipients.IsDirty) || item.IsLegallyDirtyProperty("RecipientCollection");
			bool flag2 = (attachmentCollection != null && attachmentCollection.IsDirty) || item.IsLegallyDirtyProperty("AttachmentCollection");
			if (!isLegallyDirty && !item.PropertyBag.IsDirty && !flag && !flag2)
			{
				ExTraceGlobals.SessionTracer.TraceWarning<CoreItem>((long)session.GetHashCode(), "Update Item {0} skipped because the property bag is not dirty.", item);
				return false;
			}
			StorePropertyDefinition[] array = (from x in schema.LegalTrackingProperties
			where item.PropertyBag.IsPropertyDirty(x) || item.IsLegallyDirtyProperty(x.Name)
			select x).ToArray<StorePropertyDefinition>();
			if (array.Length == 0 && !flag && !flag2)
			{
				ExTraceGlobals.SessionTracer.TraceWarning<CoreItem>((long)session.GetHashCode(), "Update Item {0} skipped because the property bag is not legally dirty.", item);
				return false;
			}
			dirtyProperties = new List<string>();
			if (!verifyLegallyDirty)
			{
				dirtyProperties = (from x in array
				select x.Name).ToList<string>();
				if (flag)
				{
					dirtyProperties.Add("RecipientCollection");
				}
				if (flag2)
				{
					dirtyProperties.Add("AttachmentCollection");
				}
			}
			else
			{
				using (CoreItem coreItem = CoreItem.Bind(session, item.Id, array))
				{
					CoreRecipientCollection recipients2 = coreItem.Recipients;
					if (flag)
					{
						flag = false;
						int num = (recipients == null) ? 0 : recipients.Count;
						int num2 = (recipients2 == null) ? 0 : recipients2.Count;
						if (num == num2)
						{
							if (recipients == null || recipients2 == null)
							{
								goto IL_36E;
							}
							using (IEnumerator<CoreRecipient> enumerator = recipients.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									CoreRecipient value = enumerator.Current;
									if (!recipients2.Contains(value, COWDumpster.CoreRecipientParticipantEqualityComparer.Default))
									{
										flag = true;
										break;
									}
								}
								goto IL_36E;
							}
						}
						flag = true;
					}
					else
					{
						int num3 = (recipients == null) ? 0 : recipients.Count;
						int num4 = (recipients2 == null) ? 0 : recipients2.Count;
						flag = (num3 != num4);
					}
					IL_36E:
					if (flag)
					{
						ExTraceGlobals.SessionTracer.TraceWarning<CoreItem>((long)session.GetHashCode(), "Update Item {0} is dirty because the recipient collection is dirty.", item);
						dirtyProperties.Add("RecipientCollection");
					}
					if (!flag2)
					{
						CoreAttachmentCollection attachmentCollection2 = coreItem.AttachmentCollection;
						int num5 = (attachmentCollection == null) ? 0 : attachmentCollection.Count;
						int num6 = (attachmentCollection2 == null) ? 0 : attachmentCollection2.Count;
						flag2 = (num5 != num6);
					}
					if (flag2)
					{
						ExTraceGlobals.SessionTracer.TraceWarning<CoreItem>((long)session.GetHashCode(), "Update Item {0} is dirty because the attachment collection is dirty.", item);
						dirtyProperties.Add("AttachmentCollection");
					}
					foreach (StorePropertyDefinition propertyDefinition in array)
					{
						object x2 = item.PropertyBag.TryGetProperty(propertyDefinition);
						object y = coreItem.PropertyBag.TryGetProperty(propertyDefinition);
						if (!COWDumpster.PropertyValuesAreEqual(propertyDefinition, x2, y))
						{
							ExTraceGlobals.SessionTracer.TraceWarning<CoreItem, string, PropertyDefinition>((long)session.GetHashCode(), "Update Item {0}, class {1} is dirty because the {2} property is dirty.", item, valueOrDefault, propertyDefinition);
							dirtyProperties.Add(propertyDefinition.Name);
						}
					}
				}
			}
			if (dirtyProperties.Count > 0)
			{
				ExTraceGlobals.SessionTracer.TraceWarning((long)session.GetHashCode(), "Update Item {0}, class {1} is {2} dirty with {3} properties changed", new object[]
				{
					item,
					valueOrDefault,
					verifyLegallyDirty ? "verified" : "not verified",
					dirtyProperties.Count
				});
				return true;
			}
			ExTraceGlobals.SessionTracer.TraceWarning<CoreItem, string, int>((long)session.GetHashCode(), "Update Item {0}, class {1} skipped because no legal tracking property was found dirty out of {2} modified legal tracking properties.", item, valueOrDefault, array.Length);
			dirtyProperties = null;
			return false;
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x00112EF0 File Offset: 0x001110F0
		public bool SkipItemOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, COWTriggerActionState state, StoreSession session, StoreObjectId itemId, CoreItem item, bool onBeforeNotification, bool onDumpster, bool success, CallbackContext callbackContext)
		{
			Util.ThrowOnNullArgument(settings, "settings");
			Util.ThrowOnNullArgument(session, "session");
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<COWTriggerActionState>(state, "state");
			if (onDumpster)
			{
				return true;
			}
			if (COWSettings.IsImapPoisonMessage(onBeforeNotification, operation, session, item))
			{
				return true;
			}
			switch (operation)
			{
			case COWTriggerAction.Create:
			case COWTriggerAction.ItemBind:
			case COWTriggerAction.Submit:
				break;
			case COWTriggerAction.Update:
				return !settings.LegalHoldEnabled();
			default:
				if (operation != COWTriggerAction.FolderBind)
				{
					return true;
				}
				break;
			}
			return true;
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x00112F6C File Offset: 0x0011116C
		public void ItemOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, COWTriggerActionState state, StoreSession session, StoreObjectId itemId, CoreItem item, CoreFolder folder, bool onBeforeNotification, OperationResult result, CallbackContext callbackContext)
		{
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<OperationResult>(result, "result");
			EnumValidator.ThrowIfInvalid<COWTriggerActionState>(state, "state");
			StoreObjectId storeObjectId = ((ICoreObject)item).StoreObjectId;
			if (storeObjectId is OccurrenceStoreObjectId)
			{
				return;
			}
			if (onBeforeNotification)
			{
				List<string> legallyDirtyProperties = item.GetLegallyDirtyProperties();
				if (legallyDirtyProperties != null && legallyDirtyProperties.Count > 0)
				{
					COWSettings.AddMetadata(settings, item, operation);
					if (state == COWTriggerActionState.Save)
					{
						StoreObjectId destinationFolderId;
						if (settings.IsOnlyInPlaceHoldEnabled())
						{
							settings.Session.CowSession.CheckAndCreateDiscoveryHoldsFolder(callbackContext.SessionWithBestAccess);
							destinationFolderId = dumpster.RecoverableItemsDiscoveryHoldsFolderId;
						}
						else
						{
							destinationFolderId = dumpster.RecoverableItemsVersionsFolderId;
						}
						StoreObjectId copyOnWriteGeneratedId = dumpster.CopyItemToDumpster(callbackContext.SessionWithBestAccess, destinationFolderId, item);
						dumpster.Results.CopyOnWriteGeneratedId = copyOnWriteGeneratedId;
						return;
					}
				}
			}
			else if (operation == COWTriggerAction.Update && state == COWTriggerActionState.Save && result == OperationResult.Failed)
			{
				dumpster.RollbackItemVersion(callbackContext.SessionWithBestAccess, item, dumpster.Results.CopyOnWriteGeneratedId);
				dumpster.Results.CopyOnWriteGeneratedId = null;
			}
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x00113060 File Offset: 0x00111260
		public CowClientOperationSensitivity SkipGroupOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, bool onBeforeNotification, bool onDumpster, CallbackContext callbackContext)
		{
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<FolderChangeOperationFlags>(flags, "flags");
			Util.ThrowOnNullArgument(settings, "settings");
			Util.ThrowOnNullArgument(sourceSession, "sourceSession");
			if (onDumpster)
			{
				return CowClientOperationSensitivity.Skip;
			}
			if (!onBeforeNotification)
			{
				return CowClientOperationSensitivity.Skip;
			}
			if (operation != COWTriggerAction.SoftDelete)
			{
				return CowClientOperationSensitivity.Skip;
			}
			if (COWSession.IsDelegateSession(sourceSession))
			{
				return CowClientOperationSensitivity.Capture;
			}
			return CowClientOperationSensitivity.CaptureAndPerformOperation;
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x001130BC File Offset: 0x001112BC
		public void GroupOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId destinationFolderId, StoreObjectId[] itemIds, GroupOperationResult result, bool onBeforeNotification, CallbackContext callbackContext)
		{
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<FolderChangeOperationFlags>(flags, "flags");
			int num = 0;
			if (itemIds != null)
			{
				foreach (StoreObjectId storeObjectId in itemIds)
				{
					if (!(storeObjectId is OccurrenceStoreObjectId))
					{
						num++;
					}
				}
			}
			if (itemIds == null || num == itemIds.Length)
			{
				dumpster.MoveItemsToDumpster(callbackContext.SessionWithBestAccess, dumpster.RecoverableItemsDeletionsFolderId, itemIds);
				return;
			}
			if (num > 0)
			{
				StoreObjectId[] array = new StoreObjectId[num];
				num = 0;
				foreach (StoreObjectId storeObjectId2 in itemIds)
				{
					if (!(storeObjectId2 is OccurrenceStoreObjectId))
					{
						array[num] = storeObjectId2;
						num++;
					}
				}
				dumpster.MoveItemsToDumpster(callbackContext.SessionWithBestAccess, dumpster.RecoverableItemsDeletionsFolderId, array);
			}
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x00113180 File Offset: 0x00111380
		private static bool PropertyValuesAreEqual(PropertyDefinition property, object x, object y)
		{
			if (!PropertyError.IsPropertyError(x) && !PropertyError.IsPropertyError(y))
			{
				if (property.Type.IsArray)
				{
					Array array = x as Array;
					Array array2 = y as Array;
					if (array == null || array2 == null || array.Length != array2.Length)
					{
						return false;
					}
					for (int i = 0; i < array2.Length; i++)
					{
						if (!array.GetValue(i).Equals(array2.GetValue(i)))
						{
							return false;
						}
					}
				}
				else if (!x.Equals(y))
				{
					return false;
				}
			}
			else
			{
				if (PropertyError.IsPropertyNotFound(x) && !PropertyError.IsPropertyNotFound(y))
				{
					return false;
				}
				if (PropertyError.IsPropertyNotFound(y) && !PropertyError.IsPropertyNotFound(x))
				{
					return false;
				}
				if (PropertyError.IsPropertyValueTooBig(x) || PropertyError.IsPropertyValueTooBig(y))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040023F7 RID: 9207
		private const string RecipientCollection = "RecipientCollection";

		// Token: 0x040023F8 RID: 9208
		private const string AttachmentCollection = "AttachmentCollection";

		// Token: 0x02000631 RID: 1585
		private class CoreRecipientParticipantEqualityComparer : IEqualityComparer<CoreRecipient>
		{
			// Token: 0x06004141 RID: 16705 RVA: 0x00113242 File Offset: 0x00111442
			public bool Equals(CoreRecipient x, CoreRecipient y)
			{
				return x != null && y != null && Participant.HasSameEmail(x.Participant, y.Participant, false);
			}

			// Token: 0x06004142 RID: 16706 RVA: 0x0011325E File Offset: 0x0011145E
			public int GetHashCode(CoreRecipient x)
			{
				if (x == null || !(x.Participant != null))
				{
					return 0;
				}
				return x.Participant.GetHashCode();
			}

			// Token: 0x040023FA RID: 9210
			public static COWDumpster.CoreRecipientParticipantEqualityComparer Default = new COWDumpster.CoreRecipientParticipantEqualityComparer();
		}
	}
}
