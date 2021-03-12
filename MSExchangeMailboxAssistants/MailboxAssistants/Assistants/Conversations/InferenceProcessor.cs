using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Conversations;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.Mdb;
using Microsoft.Exchange.Inference.Mdb.OutlookActivity;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Conversations
{
	// Token: 0x0200002A RID: 42
	internal static class InferenceProcessor
	{
		// Token: 0x06000132 RID: 306 RVA: 0x000079B8 File Offset: 0x00005BB8
		internal static bool IsEventInteresting(MapiEvent mapiEvent, MailboxData mailboxData)
		{
			return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Inference.InferenceEventBasedAssistant.Enabled && (InferenceProcessor.IsInferenceProcessingNeeded(mapiEvent) || InferenceProcessor.IsOutlookActivityProcessingNeeded(mapiEvent, mailboxData));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000079F8 File Offset: 0x00005BF8
		internal static void HandleEventInternal(MapiEvent mapiEvent, MailboxSession session, StoreObject storeItem, MailboxData mailboxData, List<KeyValuePair<string, object>> customDataToLog)
		{
			InferenceProcessor.Log(customDataToLog, "InferenceDiagnostics", mapiEvent.ToString());
			Exception ex = null;
			try
			{
				ArgumentValidator.ThrowIfNull("session", session);
				if (storeItem == null)
				{
					InferenceProcessor.Log(customDataToLog, "InferenceDiagnostics", "NullStoreItem");
				}
				else
				{
					MessageItem messageItem = storeItem as MessageItem;
					if (messageItem == null)
					{
						InferenceProcessor.Log(customDataToLog, "InferenceDiagnostics", "StoreItemIsNotMessageItem");
					}
					else
					{
						if (InferenceProcessor.IsInferenceProcessingNeeded(mapiEvent))
						{
							InferenceProcessor.Log(customDataToLog, "InferenceDiagnostics", "InferenceProcessingNeeded");
							if (!messageItem.GetValueOrDefault<bool>(ItemSchema.InferenceProcessingNeeded, false))
							{
								return;
							}
							try
							{
								messageItem.DeleteProperties(new PropertyDefinition[]
								{
									ItemSchema.InferenceProcessingNeeded
								});
								InferenceProcessingActions valueOrDefault = (InferenceProcessingActions)messageItem.GetValueOrDefault<long>(ItemSchema.InferenceProcessingActions, 0L);
								messageItem.DeleteProperties(new PropertyDefinition[]
								{
									ItemSchema.InferenceProcessingActions
								});
								if (valueOrDefault.HasFlag(InferenceProcessingActions.ProcessImplicitMarkAsNotClutter))
								{
									InferenceProcessor.HandleImplicitMarkAsNotClutter(mapiEvent, session, messageItem, customDataToLog);
									goto IL_1C6;
								}
								throw new ArgumentException("No actionable flag is set on InferenceProcessingActions, but InferenceProcessingNeeded is true");
							}
							finally
							{
								if (messageItem.IsDirty)
								{
									messageItem.Save(SaveMode.ResolveConflicts);
								}
							}
						}
						if (InferenceProcessor.IsOutlookActivityProcessingNeeded(mapiEvent, mailboxData))
						{
							InferenceProcessor.Log(customDataToLog, "InferenceDiagnostics", "ProcessOutlookActivity");
							if (session.IsDefaultFolderType(messageItem.ParentId) != DefaultFolderType.Inbox)
							{
								InferenceProcessor.Log(customDataToLog, "InferenceDiagnostics", "ActivityItemNotInExpectedFolder");
								return;
							}
							try
							{
								int num = OutlookActivityProcessor.Process(session, messageItem);
								InferenceProcessor.Log(customDataToLog, "NumOfOutlookActivityItemsProcessed", num);
							}
							catch (OutlookActivityParsingException value)
							{
								InferenceProcessor.Log(customDataToLog, "OutlookActivityParsingException", value);
							}
							using (Folder folder = Folder.Bind(session, DefaultFolderType.Inbox))
							{
								folder.DeleteObjects(DeleteItemFlags.HardDelete, new StoreId[]
								{
									messageItem.Id
								});
								goto IL_1C6;
							}
						}
						throw new ArgumentException("We should not get an event that's not interested by InferenceProcessor" + mapiEvent.ToString());
						IL_1C6:;
					}
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				if (ex != null)
				{
					InferenceProcessor.Log(customDataToLog, "Exception", ex.ToString());
				}
				InferenceProcessor.Log(customDataToLog, "InferenceDiagnostics", (ex == null) ? "InvokeSucceeded" : "InvokeFailed");
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007C84 File Offset: 0x00005E84
		private static void HandleImplicitMarkAsNotClutter(MapiEvent mapiEvent, MailboxSession session, MessageItem message, List<KeyValuePair<string, object>> customDataToLog)
		{
			ConversationId valueOrDefault = message.GetValueOrDefault<ConversationId>(ItemSchema.ConversationId, null);
			if (valueOrDefault == null)
			{
				customDataToLog.Add(new KeyValuePair<string, object>("InferenceDiagnostics", "ConversationIdNull"));
				return;
			}
			message.Load(new PropertyDefinition[]
			{
				ItemSchema.ConversationFamilyId
			});
			ConversationId valueOrDefault2 = message.GetValueOrDefault<ConversationId>(ItemSchema.ConversationFamilyId, null);
			if (valueOrDefault2 == null)
			{
				customDataToLog.Add(new KeyValuePair<string, object>("InferenceDiagnostics", "ConversationFamilyIdNull"));
				return;
			}
			VariantConfigurationSnapshot flightFeatures = FlightModule.GetFlightFeatures(session);
			StoreObjectId[] folderIds = new StoreObjectId[]
			{
				session.GetDefaultFolderId(DefaultFolderType.Drafts)
			};
			ICoreConversation coreConversation;
			if (flightFeatures != null && flightFeatures.DataStorage.ModernMailInfra.Enabled && !flightFeatures.DataStorage.ThreadedConversation.Enabled)
			{
				ConversationFamilyFactory conversationFamilyFactory = new ConversationFamilyFactory(session, valueOrDefault2);
				coreConversation = conversationFamilyFactory.CreateConversation(valueOrDefault, folderIds, true, false, InferenceProcessor.ConversationMessagePropertiesToLoad);
			}
			else
			{
				coreConversation = Conversation.Load(session, valueOrDefault, folderIds, true, false, InferenceProcessor.ConversationMessagePropertiesToLoad);
			}
			ConversationClutterInformation conversationClutterInformation = MdbConversationClutterInformationFactory.GetConversationClutterInformation(null, session, flightFeatures, coreConversation);
			if (conversationClutterInformation != null)
			{
				conversationClutterInformation.MarkItemsAsNotClutter(true);
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007D88 File Offset: 0x00005F88
		private static bool IsInferenceProcessingNeeded(MapiEvent mapiEvent)
		{
			return mapiEvent.ItemType == ObjectType.MAPI_MESSAGE && mapiEvent.ExtendedEventFlags.Contains(MapiExtendedEventFlags.InferenceProcessingNeeded);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007DAC File Offset: 0x00005FAC
		private static bool IsOutlookActivityProcessingNeeded(MapiEvent mapiEvent, MailboxData mailboxData)
		{
			return ActivityLogHelper.IsActivityLoggingEnabled(false) && CommonConfiguration.Singleton.OutlookActivityProcessingEnabledInEba && mapiEvent.ObjectClass == "IPM.Activity" && mapiEvent.EventFlags.HasFlag(MapiEventFlags.FolderAssociated) && (mailboxData == null || mailboxData.MatchCachedDefaultFolderType(mapiEvent.ParentEntryId) == DefaultFolderType.Inbox) && mapiEvent.EventMask.HasFlag(MapiEventTypeFlags.ObjectCreated) && mapiEvent.ClientType != MapiEventClientTypes.EventBasedAssistants;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007E2D File Offset: 0x0000602D
		private static void Log(List<KeyValuePair<string, object>> customDataToLog, string key, object value)
		{
			customDataToLog.Add(new KeyValuePair<string, object>(key, value));
		}

		// Token: 0x04000124 RID: 292
		private static readonly PropertyDefinition[] ConversationMessagePropertiesToLoad = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ParentItemId,
			ItemSchema.IsClutter
		};

		// Token: 0x04000125 RID: 293
		private static readonly Trace Tracer = ExTraceGlobals.GeneralTracer;

		// Token: 0x0200002B RID: 43
		private static class CustomDataStrings
		{
			// Token: 0x04000126 RID: 294
			public const string DiagnosticsKeyName = "InferenceDiagnostics";

			// Token: 0x04000127 RID: 295
			public const string NullConversationId = "ConversationIdNull";

			// Token: 0x04000128 RID: 296
			public const string NullConversationFamilyId = "ConversationFamilyIdNull";

			// Token: 0x04000129 RID: 297
			public const string NullStoreItem = "NullStoreItem";

			// Token: 0x0400012A RID: 298
			public const string StoreItemIsNotMessageItem = "StoreItemIsNotMessageItem";

			// Token: 0x0400012B RID: 299
			public const string ActivityItemNotInExpectedFolder = "ActivityItemNotInExpectedFolder";

			// Token: 0x0400012C RID: 300
			public const string InvokeFailed = "InvokeFailed";

			// Token: 0x0400012D RID: 301
			public const string InvokeSucceeded = "InvokeSucceeded";

			// Token: 0x0400012E RID: 302
			public const string ExceptionKeyName = "Exception";

			// Token: 0x0400012F RID: 303
			public const string ProcessOutlookActivity = "ProcessOutlookActivity";

			// Token: 0x04000130 RID: 304
			public const string InferenceProcessingNeeded = "InferenceProcessingNeeded";

			// Token: 0x04000131 RID: 305
			public const string ActivitiesExtractedKeyName = "ActivitiesExtracted";

			// Token: 0x04000132 RID: 306
			public const string CannotInstantiateOutlookActivityManager = "CannotInstantiateOutlookActivityManager";

			// Token: 0x04000133 RID: 307
			public const string OutlookActivityParsingException = "OutlookActivityParsingException";

			// Token: 0x04000134 RID: 308
			public const string NumOfOutlookActivityItemsProcessed = "NumOfOutlookActivityItemsProcessed";
		}
	}
}
