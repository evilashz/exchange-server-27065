﻿using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200062C RID: 1580
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class COWContactLinking : ICOWNotification
	{
		// Token: 0x0600411E RID: 16670 RVA: 0x00111BB3 File Offset: 0x0010FDB3
		internal COWContactLinking()
		{
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x00111BBC File Offset: 0x0010FDBC
		public bool SkipItemOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, COWTriggerActionState state, StoreSession session, StoreObjectId itemId, CoreItem item, bool onBeforeNotification, bool onDumpster, bool success, CallbackContext callbackContext)
		{
			Util.ThrowOnNullArgument(session, "session");
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<COWTriggerActionState>(state, "state");
			switch (callbackContext.ContactLinkingProcessingState)
			{
			case ContactLinkingProcessingState.DoNotProcess:
				COWContactLinking.Tracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "COWContactLinking.SkipItemOperation: skipping notification for item {0} because it should not be processed.", itemId);
				return true;
			case ContactLinkingProcessingState.ProcessBeforeSave:
				return !onBeforeNotification || state != COWTriggerActionState.Flush;
			case ContactLinkingProcessingState.ProcessAfterSave:
				return onBeforeNotification;
			case ContactLinkingProcessingState.Processed:
				COWContactLinking.Tracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "COWContactLinking.SkipItemOperation: skipping notification for item {0} because it has already been processed.", itemId);
				return true;
			}
			callbackContext.ContactLinkingProcessingState = this.InspectNotification(operation, session, item, onBeforeNotification, onDumpster);
			COWContactLinking.Tracer.TraceDebug<StoreObjectId, ContactLinkingProcessingState>((long)this.GetHashCode(), "COWContactLinking.SkipItemOperation: inspected item {0} and result is {1}.", itemId, callbackContext.ContactLinkingProcessingState);
			return callbackContext.ContactLinkingProcessingState != ContactLinkingProcessingState.ProcessBeforeSave || !onBeforeNotification || state != COWTriggerActionState.Flush;
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x00111CA4 File Offset: 0x0010FEA4
		public void ItemOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, COWTriggerActionState state, StoreSession session, StoreObjectId itemId, CoreItem item, CoreFolder folder, bool onBeforeNotification, OperationResult result, CallbackContext callbackContext)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(item, "item");
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<COWTriggerActionState>(state, "state");
			EnumValidator.ThrowIfInvalid<OperationResult>(result, "result");
			COWContactLinking.Tracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "COWContactLinking.ItemOperation: processing contact linking for item {0}.", itemId);
			MailboxSession mailboxSession = (MailboxSession)session;
			MailboxInfoForLinking mailboxInfo = MailboxInfoForLinking.CreateFromMailboxSession(mailboxSession);
			ContactLinkingPerformanceTracker performanceTracker = new ContactLinkingPerformanceTracker(mailboxSession);
			DirectoryPersonSearcher directoryPersonSearcher = new DirectoryPersonSearcher(mailboxSession.MailboxOwner);
			ContactStoreForContactLinking contactStoreForContactLinking = new ContactStoreForCowContactLinking(mailboxSession, performanceTracker);
			ContactLinkingLogger logger = new ContactLinkingLogger("COWContactLinking", mailboxInfo);
			AutomaticLink automaticLink = new AutomaticLink(mailboxInfo, logger, performanceTracker, directoryPersonSearcher, contactStoreForContactLinking);
			automaticLink.LinkNewOrUpdatedContactBeforeSave(item, new Func<ContactInfoForLinking, IContactStoreForContactLinking, IEnumerable<ContactInfoForLinking>>(this.GetOtherContactsEnumeratorForCOW));
			if (!onBeforeNotification)
			{
				item.SaveFlags |= PropertyBagSaveFlags.ForceNotificationPublish;
				try
				{
					item.Save(SaveMode.NoConflictResolution);
				}
				finally
				{
					item.SaveFlags &= ~PropertyBagSaveFlags.ForceNotificationPublish;
				}
			}
			callbackContext.ContactLinkingProcessingState = ContactLinkingProcessingState.Processed;
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x00111DA8 File Offset: 0x0010FFA8
		public CowClientOperationSensitivity SkipGroupOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, bool onBeforeNotification, bool onDumpster, CallbackContext callbackContext)
		{
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<FolderChangeOperationFlags>(flags, "flags");
			return CowClientOperationSensitivity.Skip;
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x00111DC2 File Offset: 0x0010FFC2
		public void GroupOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId destinationFolderId, StoreObjectId[] itemIds, GroupOperationResult result, bool onBeforeNotification, CallbackContext callbackContext)
		{
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<FolderChangeOperationFlags>(flags, "flags");
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x00111DF8 File Offset: 0x0010FFF8
		private ContactLinkingProcessingState InspectNotification(COWTriggerAction operation, StoreSession session, CoreItem item, bool onBeforeNotification, bool onDumpster)
		{
			if (onDumpster)
			{
				return ContactLinkingProcessingState.DoNotProcess;
			}
			if (!onBeforeNotification)
			{
				return ContactLinkingProcessingState.DoNotProcess;
			}
			if (item == null)
			{
				return ContactLinkingProcessingState.Unknown;
			}
			if (!(session is MailboxSession))
			{
				COWContactLinking.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLinking.InspectNotification: not a mailbox session.");
				return ContactLinkingProcessingState.DoNotProcess;
			}
			if (operation != COWTriggerAction.Create && operation != COWTriggerAction.Update)
			{
				COWContactLinking.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLinking.InspectNotification: not an Create or Update operation.");
				return ContactLinkingProcessingState.DoNotProcess;
			}
			PersistablePropertyBag persistablePropertyBag = CoreObject.GetPersistablePropertyBag(item);
			switch (persistablePropertyBag.Context.AutomaticContactLinkingAction)
			{
			case AutomaticContactLinkingAction.ClientBased:
				if (!this.IsClientAllowed(session.ClientInfoString))
				{
					COWContactLinking.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLinking.InspectNotification: not allowed client session.");
					return ContactLinkingProcessingState.DoNotProcess;
				}
				break;
			case AutomaticContactLinkingAction.Ignore:
				COWContactLinking.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLinking.InspectNotification: IgnoreAutomaticContactLinking=true.");
				return ContactLinkingProcessingState.DoNotProcess;
			}
			if (session.LogonType != LogonType.Owner && session.LogonType != LogonType.Delegated)
			{
				COWContactLinking.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLinking.InspectNotification: logon session is not user or delegated.");
				return ContactLinkingProcessingState.DoNotProcess;
			}
			string valueOrDefault = item.PropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
			if (string.IsNullOrEmpty(valueOrDefault))
			{
				return ContactLinkingProcessingState.Unknown;
			}
			if (ObjectClass.IsPlace(valueOrDefault))
			{
				COWContactLinking.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLinking.InspectNotification: place item class are not processed.");
				return ContactLinkingProcessingState.DoNotProcess;
			}
			if (!ObjectClass.IsContact(valueOrDefault))
			{
				COWContactLinking.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLinking.InspectNotification: item class is not contact.");
				return ContactLinkingProcessingState.DoNotProcess;
			}
			if (operation == COWTriggerAction.Update && !Array.Exists<StorePropertyDefinition>(COWContactLinking.NotificationProperties, (StorePropertyDefinition property) => item.PropertyBag.IsPropertyDirty(property)))
			{
				COWContactLinking.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLinking.InspectNotification: no relevant properties changed");
				return ContactLinkingProcessingState.Unknown;
			}
			if (ClientInfo.MOMT.IsMatch(session.ClientInfoString))
			{
				return ContactLinkingProcessingState.ProcessAfterSave;
			}
			return ContactLinkingProcessingState.ProcessBeforeSave;
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x00111FAC File Offset: 0x001101AC
		private bool IsClientAllowed(string clientInfoString)
		{
			if (ClientInfo.OWA.IsMatch(clientInfoString))
			{
				if (AutomaticLinkConfiguration.IsOWAEnabled)
				{
					return true;
				}
				COWContactLinking.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLinking.InspectNotification: automatic linking has been disabled for OWA.");
				return false;
			}
			else
			{
				if (!ClientInfo.MOMT.IsMatch(clientInfoString))
				{
					return false;
				}
				if (AutomaticLinkConfiguration.IsMOMTEnabled)
				{
					return true;
				}
				COWContactLinking.Tracer.TraceDebug((long)this.GetHashCode(), "COWContactLinking.InspectNotification: automatic linking has been disabled for MOMT.");
				return false;
			}
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x00112016 File Offset: 0x00110216
		private IEnumerable<ContactInfoForLinking> GetOtherContactsEnumeratorForCOW(ContactInfoForLinking contactInfoContactBeingSaved, IContactStoreForContactLinking contactStoreForContactLinking)
		{
			return contactStoreForContactLinking.GetAllContactsPerCriteria(contactInfoContactBeingSaved.EmailAddresses, contactInfoContactBeingSaved.IMAddress);
		}

		// Token: 0x040023ED RID: 9197
		internal static readonly StorePropertyDefinition[] NotificationProperties = new StorePropertyDefinition[]
		{
			ContactSchema.Email1EmailAddress,
			ContactSchema.Email2EmailAddress,
			ContactSchema.Email3EmailAddress,
			ContactSchema.GALLinkID,
			ContactSchema.SmtpAddressCache,
			ContactSchema.IMAddress
		};

		// Token: 0x040023EE RID: 9198
		internal static readonly Trace Tracer = ExTraceGlobals.ContactLinkingTracer;
	}
}
