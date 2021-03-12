using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.MailboxAssistants.Assistants.Approval;
using Microsoft.Exchange.MailboxAssistants.Assistants.BirthdayAssistant;
using Microsoft.Exchange.MailboxAssistants.Assistants.Calendar;
using Microsoft.Exchange.MailboxAssistants.Assistants.CalendarInterop;
using Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair;
using Microsoft.Exchange.MailboxAssistants.Assistants.Conversations;
using Microsoft.Exchange.MailboxAssistants.Assistants.Dar;
using Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant;
using Microsoft.Exchange.MailboxAssistants.Assistants.DiscoverySearch;
using Microsoft.Exchange.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.MailboxAssistants.Assistants.GroupMailbox;
using Microsoft.Exchange.MailboxAssistants.Assistants.InferenceDataCollection;
using Microsoft.Exchange.MailboxAssistants.Assistants.InferenceTraining;
using Microsoft.Exchange.MailboxAssistants.Assistants.JunkEmailOptions;
using Microsoft.Exchange.MailboxAssistants.Assistants.MailboxAssociation;
using Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor;
using Microsoft.Exchange.MailboxAssistants.Assistants.Mwi;
using Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator;
using Microsoft.Exchange.MailboxAssistants.Assistants.PeopleCentricTriage;
using Microsoft.Exchange.MailboxAssistants.Assistants.PeopleRelevance;
using Microsoft.Exchange.MailboxAssistants.Assistants.ProbeTimeBasedAssistant;
using Microsoft.Exchange.MailboxAssistants.Assistants.ProvisioningAssistant;
using Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder;
using Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications;
using Microsoft.Exchange.MailboxAssistants.Assistants.RecipientDLExpansion;
using Microsoft.Exchange.MailboxAssistants.Assistants.Reminders;
using Microsoft.Exchange.MailboxAssistants.Assistants.ResourceBooking;
using Microsoft.Exchange.MailboxAssistants.Assistants.Search;
using Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore;
using Microsoft.Exchange.MailboxAssistants.Assistants.SharingFolderAssistant;
using Microsoft.Exchange.MailboxAssistants.Assistants.SiteMailbox;
using Microsoft.Exchange.MailboxAssistants.Assistants.StoreIntegrityCheck;
using Microsoft.Exchange.MailboxAssistants.Assistants.StoreMaintenance;
using Microsoft.Exchange.MailboxAssistants.Assistants.TopN;
using Microsoft.Exchange.MailboxAssistants.Assistants.UM;
using Microsoft.Exchange.MailboxAssistants.Assistants.UMReporting;
using Microsoft.Exchange.MailboxAssistants.CalendarSync;
using Microsoft.Exchange.MailboxAssistants.SharingPolicyAssistant;
using Microsoft.Mapi;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000003 RID: 3
	internal static class InfoworkerAssistants
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000272E File Offset: 0x0000092E
		public static IEventBasedAssistantType[] CreateEventBasedAssistantTypes()
		{
			InfoworkerAssistants.DeleteWatermarksForDisabledAndDeprecatedAssistants(null);
			return InfoworkerAssistants.CreateAssistantTypes<IEventBasedAssistantType>(InfoworkerAssistants.EventBasedAssistantTypes);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002740 File Offset: 0x00000940
		public static ITimeBasedAssistantType[] CreateTimeBasedAssistantTypes()
		{
			return InfoworkerAssistants.CreateAssistantTypes<ITimeBasedAssistantType>(InfoworkerAssistants.TimeBasedAssistantTypes);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000274C File Offset: 0x0000094C
		private static bool IsDisabled(string assistantName)
		{
			if (InfoworkerAssistants.disabledAssistantsNames == null)
			{
				InfoworkerAssistants.disabledAssistantsNames = InfoworkerAssistants.ReadDisabledAssistants();
			}
			return InfoworkerAssistants.disabledAssistantsNames.Contains(assistantName);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000028D4 File Offset: 0x00000AD4
		internal static void DeleteWatermarksForDisabledAndDeprecatedAssistants(object stateNotUsed)
		{
			List<Guid> disabledEventAssistants = new List<Guid>(InfoworkerAssistants.EventBasedAssistantTypes.Length);
			foreach (InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor in InfoworkerAssistants.EventBasedAssistantTypes)
			{
				if (InfoworkerAssistants.IsDisabled(eventAssistantConstructor.Name))
				{
					ExTraceGlobals.AssistantBaseTracer.TraceDebug<string>(0L, "[InfoworkerAssistants]: Event assistant '{0}' is disabled.", eventAssistantConstructor.Name);
					disabledEventAssistants.Add(eventAssistantConstructor.Identity);
				}
			}
			disabledEventAssistants.AddRange(InfoworkerAssistants.deprecatedAssistants);
			if (disabledEventAssistants.Count == 0)
			{
				ExTraceGlobals.AssistantBaseTracer.TraceDebug(0L, "[InfoworkerAssistants]: There are no disabled or deprecated assistants.");
				return;
			}
			ExTraceGlobals.AssistantBaseTracer.TraceDebug<int>(0L, "[InfoworkerAssistants]: There are {0} disabled and/or deprecated assistants.", disabledEventAssistants.Count);
			Exception exception = null;
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=EBA", null, null, null, null))
						{
							MdbStatus[] array = exRpcAdmin.ListMdbStatus(false);
							foreach (MdbStatus mdbStatus in array)
							{
								if (DatabaseManager.IsOnlineDatabase(mdbStatus))
								{
									foreach (Guid guid in disabledEventAssistants)
									{
										MapiEventManager mapiEventManager = MapiEventManager.Create(exRpcAdmin, guid, mdbStatus.MdbGuid);
										ExTraceGlobals.AssistantBaseTracer.TraceDebug<Guid, string>(0L, "[InfoworkerAssistants]: Deleting watermarks for consumer {0} on database {1}.", guid, mdbStatus.MdbName);
										mapiEventManager.DeleteWatermarks();
									}
								}
							}
						}
					}
					catch (MapiExceptionVersion exception2)
					{
						exception = exception2;
					}
					catch (MapiExceptionMdbOffline exception3)
					{
						exception = exception3;
					}
					catch (MapiExceptionExiting exception4)
					{
						exception = exception4;
					}
					catch (MapiExceptionNetworkError exception5)
					{
						exception = exception5;
					}
					catch (MapiExceptionADUnavailable exception6)
					{
						exception = exception6;
					}
					catch (MapiExceptionNoAccess exception7)
					{
						exception = exception7;
					}
				});
			}
			catch (GrayException exception)
			{
				GrayException exception8;
				exception = exception8;
			}
			if (exception != null)
			{
				ExTraceGlobals.AssistantBaseTracer.TraceError<Exception>(0L, "[InfoworkerAssistants]: Failed to cleanup the watermarks at this time. Exception: {0}", exception);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000029F4 File Offset: 0x00000BF4
		internal static T[] CreateAssistantTypes<T>(InfoworkerAssistants.AssistantConstructor<T>[] assistantTypes)
		{
			List<T> list = new List<T>();
			foreach (InfoworkerAssistants.AssistantConstructor<T> assistantConstructor in assistantTypes)
			{
				if (!InfoworkerAssistants.IsDisabled(assistantConstructor.Name) && (assistantConstructor.IsEnabledDelegate == null || assistantConstructor.IsEnabledDelegate()))
				{
					bool flag = false;
					Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_CreatingAssistant, null, new object[]
					{
						assistantConstructor.Name
					});
					try
					{
						list.Add(assistantConstructor.ConstructorDelegate());
						flag = true;
					}
					finally
					{
						if (!flag)
						{
							Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_FailCreateAssistant, null, new object[]
							{
								assistantConstructor.Name
							});
						}
					}
					InfoworkerAssistants.TracerPfd.TracePfd<int, string>(3L, "PFD IWS {0} Assistant {1} is Enabled", 18839, assistantConstructor.Name);
				}
				else
				{
					Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_DisabledAssistant, null, new object[]
					{
						assistantConstructor.Name
					});
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002B18 File Offset: 0x00000D18
		private static HashSet<string> ReadDisabledAssistants()
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
			{
				object value = registryKey.GetValue("DisabledAssistants");
				if (value is string[])
				{
					foreach (string item in (string[])value)
					{
						hashSet.Add(item);
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002CD8 File Offset: 0x00000ED8
		// Note: this type is marked as 'beforefieldinit'.
		static InfoworkerAssistants()
		{
			InfoworkerAssistants.EventAssistantConstructor[] array = new InfoworkerAssistants.EventAssistantConstructor[18];
			InfoworkerAssistants.EventAssistantConstructor[] array2 = array;
			int num = 0;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor.Name = "CalendarAssistant";
			eventAssistantConstructor.ConstructorDelegate = (() => new CalendarAssistantType());
			eventAssistantConstructor.Identity = AssistantIdentity.CalendarAssistant;
			array2[num] = eventAssistantConstructor;
			InfoworkerAssistants.EventAssistantConstructor[] array3 = array;
			int num2 = 1;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor2 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor2.Name = "ResourceBookingAssistant";
			eventAssistantConstructor2.ConstructorDelegate = (() => new ResourceBookingAssistantType());
			eventAssistantConstructor2.Identity = AssistantIdentity.ResourceBookingAssistant;
			array3[num2] = eventAssistantConstructor2;
			InfoworkerAssistants.EventAssistantConstructor[] array4 = array;
			int num3 = 2;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor3 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor3.Name = "JunkEmailOptionsAssistant";
			eventAssistantConstructor3.ConstructorDelegate = (() => new JunkEmailOptionsAssistantType());
			eventAssistantConstructor3.Identity = AssistantIdentity.JunkEmailOptionsAssistant;
			array4[num3] = eventAssistantConstructor3;
			InfoworkerAssistants.EventAssistantConstructor[] array5 = array;
			int num4 = 3;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor4 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor4.Name = "ApprovalAssistant";
			eventAssistantConstructor4.ConstructorDelegate = (() => new ApprovalAssistantType());
			eventAssistantConstructor4.Identity = AssistantIdentity.ApprovalAssistant;
			array5[num4] = eventAssistantConstructor4;
			InfoworkerAssistants.EventAssistantConstructor[] array6 = array;
			int num5 = 4;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor5 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor5.Name = "ConversationsAssistant";
			eventAssistantConstructor5.ConstructorDelegate = (() => new ConversationsAssistantType());
			eventAssistantConstructor5.Identity = AssistantIdentity.ConversationsAssistant;
			array6[num5] = eventAssistantConstructor5;
			InfoworkerAssistants.EventAssistantConstructor[] array7 = array;
			int num6 = 5;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor6 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor6.Name = "ElcEventBasedAssistant";
			eventAssistantConstructor6.ConstructorDelegate = (() => new ElcEventBasedAssistantType());
			eventAssistantConstructor6.Identity = AssistantIdentity.ElcEventBasedAssistant;
			array7[num6] = eventAssistantConstructor6;
			InfoworkerAssistants.EventAssistantConstructor[] array8 = array;
			int num7 = 6;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor7 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor7.Name = "MessageWaitingIndicatorAssistant";
			eventAssistantConstructor7.ConstructorDelegate = (() => new MwiAssistantType());
			eventAssistantConstructor7.Identity = AssistantIdentity.MwiAssistant;
			array8[num7] = eventAssistantConstructor7;
			InfoworkerAssistants.EventAssistantConstructor[] array9 = array;
			int num8 = 7;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor8 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor8.Name = "UMPartnerMessageAssistant";
			eventAssistantConstructor8.ConstructorDelegate = (() => new UMPartnerMessageAssistantType());
			eventAssistantConstructor8.Identity = AssistantIdentity.UmPartnerMessageAssistant;
			array9[num8] = eventAssistantConstructor8;
			InfoworkerAssistants.EventAssistantConstructor[] array10 = array;
			int num9 = 8;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor9 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor9.Name = "ProvisioningAssistant";
			eventAssistantConstructor9.ConstructorDelegate = (() => new ProvisioningAssistantType());
			eventAssistantConstructor9.Identity = AssistantIdentity.ProvisioningAssistant;
			array10[num9] = eventAssistantConstructor9;
			InfoworkerAssistants.EventAssistantConstructor[] array11 = array;
			int num10 = 9;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor10 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor10.Name = "CalendarNotificationAssistant";
			eventAssistantConstructor10.ConstructorDelegate = (() => new CalendarNotificationAssistantType());
			eventAssistantConstructor10.Identity = AssistantIdentity.CalendarNotificationAssistant;
			array11[num10] = eventAssistantConstructor10;
			InfoworkerAssistants.EventAssistantConstructor[] array12 = array;
			int num11 = 10;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor11 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor11.Name = "SharingFolderAssistant";
			eventAssistantConstructor11.ConstructorDelegate = (() => new SharingFolderAssistantType());
			eventAssistantConstructor11.Identity = AssistantIdentity.SharingFolderAssistant;
			array12[num11] = eventAssistantConstructor11;
			InfoworkerAssistants.EventAssistantConstructor[] array13 = array;
			int num12 = 11;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor12 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor12.Name = "DiscoverySearchEventBasedAssistant";
			eventAssistantConstructor12.ConstructorDelegate = (() => new DiscoverySearchEventBasedAssistantType());
			eventAssistantConstructor12.Identity = AssistantIdentity.DiscoverySearchEventBasedAssistant;
			array13[num12] = eventAssistantConstructor12;
			InfoworkerAssistants.EventAssistantConstructor[] array14 = array;
			int num13 = 12;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor13 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor13.Name = "DarTaskStoreEventBasedAssistant";
			eventAssistantConstructor13.ConstructorDelegate = (() => new TaskStoreEventBasedAssistantType());
			eventAssistantConstructor13.Identity = AssistantIdentity.DarTaskStoreEventBasedAssistant;
			array14[num13] = eventAssistantConstructor13;
			InfoworkerAssistants.EventAssistantConstructor[] array15 = array;
			int num14 = 13;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor14 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor14.Name = "PushNotificationAssistant";
			eventAssistantConstructor14.ConstructorDelegate = (() => new PushNotificationAssistantType());
			eventAssistantConstructor14.Identity = AssistantIdentity.PushNotitificationEventBasedAssistant;
			array15[num14] = eventAssistantConstructor14;
			InfoworkerAssistants.EventAssistantConstructor[] array16 = array;
			int num15 = 14;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor15 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor15.Name = "RemindersAssistant";
			eventAssistantConstructor15.ConstructorDelegate = (() => new RemindersAssistantType());
			eventAssistantConstructor15.Identity = AssistantIdentity.RemindersEventBasedAssistant;
			array16[num15] = eventAssistantConstructor15;
			InfoworkerAssistants.EventAssistantConstructor[] array17 = array;
			int num16 = 15;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor16 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor16.Name = "RecipientDLExpansionEventBasedAssistant";
			eventAssistantConstructor16.ConstructorDelegate = (() => new RecipientDLExpansionEventBasedAssistantType());
			eventAssistantConstructor16.Identity = AssistantIdentity.RecipientDLExpansionEventBasedAssistant;
			array17[num16] = eventAssistantConstructor16;
			InfoworkerAssistants.EventAssistantConstructor[] array18 = array;
			int num17 = 16;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor17 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor17.Name = "CalendarInteropAssistant";
			eventAssistantConstructor17.ConstructorDelegate = (() => new CalendarInteropAssistantType());
			eventAssistantConstructor17.Identity = AssistantIdentity.CalendarInteropAssistant;
			array18[num17] = eventAssistantConstructor17;
			InfoworkerAssistants.EventAssistantConstructor[] array19 = array;
			int num18 = 17;
			InfoworkerAssistants.EventAssistantConstructor eventAssistantConstructor18 = new InfoworkerAssistants.EventAssistantConstructor();
			eventAssistantConstructor18.Name = "BirthdayAssistant";
			eventAssistantConstructor18.ConstructorDelegate = (() => new BirthdayAssistantType());
			eventAssistantConstructor18.Identity = AssistantIdentity.BirthdayAssistant;
			array19[num18] = eventAssistantConstructor18;
			InfoworkerAssistants.EventBasedAssistantTypes = array;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array20 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[27];
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array21 = array20;
			int num19 = 0;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor.Name = "ElcAssistant";
			assistantConstructor.ConstructorDelegate = (() => new ELCAssistantType());
			array21[num19] = assistantConstructor;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array22 = array20;
			int num20 = 1;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor2 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor2.Name = "JunkEmailOptionsCommitterAssistant";
			assistantConstructor2.ConstructorDelegate = (() => new JunkEmailOptionsCommiterAssistantType());
			array22[num20] = assistantConstructor2;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array23 = array20;
			int num21 = 2;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor3 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor3.Name = "CalendarRepairAssistant";
			assistantConstructor3.ConstructorDelegate = (() => new CalendarRepairAssistantType());
			array23[num21] = assistantConstructor3;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array24 = array20;
			int num22 = 3;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor4 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor4.Name = "SharingPolicyAssistant";
			assistantConstructor4.ConstructorDelegate = (() => new SharingPolicyAssistantType());
			array24[num22] = assistantConstructor4;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array25 = array20;
			int num23 = 4;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor5 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor5.Name = "TopNWordsAssistant";
			assistantConstructor5.ConstructorDelegate = (() => new TopNAssistantType());
			array25[num23] = assistantConstructor5;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array26 = array20;
			int num24 = 5;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor6 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor6.Name = "CalendarSyncAssistant";
			assistantConstructor6.ConstructorDelegate = (() => new CalendarSyncAssistantType());
			array26[num24] = assistantConstructor6;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array27 = array20;
			int num25 = 6;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor7 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor7.Name = "UMReportingAssistant";
			assistantConstructor7.ConstructorDelegate = (() => new UMReportingAssistantType());
			array27[num25] = assistantConstructor7;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array28 = array20;
			int num26 = 7;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor8 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor8.Name = "InferenceTrainingAssistant";
			assistantConstructor8.ConstructorDelegate = (() => new InferenceTrainingAssistantType());
			assistantConstructor8.IsEnabledDelegate = new InfoworkerAssistants.IsEnabledDelegate(InferenceTrainingAssistantType.IsAssistantEnabled);
			array28[num26] = assistantConstructor8;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array29 = array20;
			int num27 = 8;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor9 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor9.Name = "StoreMaintenanceAssistant";
			assistantConstructor9.ConstructorDelegate = (() => new StoreMaintenanceAssistantType());
			array29[num27] = assistantConstructor9;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array30 = array20;
			int num28 = 9;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor10 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor10.Name = "StoreDSMaintenanceAssistant";
			assistantConstructor10.ConstructorDelegate = (() => new StoreDirectoryServiceMaintenanceAssistantType());
			array30[num28] = assistantConstructor10;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array31 = array20;
			int num29 = 10;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor11 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor11.Name = "StoreUrgentMaintenanceAssistant";
			assistantConstructor11.ConstructorDelegate = (() => new StoreUrgentMaintenanceAssistantType());
			array31[num29] = assistantConstructor11;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array32 = array20;
			int num30 = 11;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor12 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor12.Name = DirectoryProcessorAssistantType.AssistantName;
			assistantConstructor12.ConstructorDelegate = (() => new DirectoryProcessorAssistantType());
			array32[num30] = assistantConstructor12;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array33 = array20;
			int num31 = 12;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor13 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor13.Name = "PublicFolderAssistant";
			assistantConstructor13.ConstructorDelegate = (() => new PublicFolderAssistantType());
			array33[num31] = assistantConstructor13;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array34 = array20;
			int num32 = 13;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor14 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor14.Name = "OABGeneratorAssistant";
			assistantConstructor14.ConstructorDelegate = (() => new OABGeneratorAssistantType());
			array34[num32] = assistantConstructor14;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array35 = array20;
			int num33 = 14;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor15 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor15.Name = "InferenceDataCollectionAssistant";
			assistantConstructor15.ConstructorDelegate = (() => new InferenceDataCollectionAssistantType());
			assistantConstructor15.IsEnabledDelegate = new InfoworkerAssistants.IsEnabledDelegate(InferenceDataCollectionAssistantType.IsAssistantEnabled);
			array35[num33] = assistantConstructor15;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array36 = array20;
			int num34 = 15;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor16 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor16.Name = "PeopleRelevanceAssistant";
			assistantConstructor16.ConstructorDelegate = (() => new PeopleRelevanceAssistantType());
			array36[num34] = assistantConstructor16;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array37 = array20;
			int num35 = 16;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor17 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor17.Name = "SharePointSignalStoreAssistant";
			assistantConstructor17.ConstructorDelegate = (() => new SharePointSignalStoreAssistantType());
			array37[num35] = assistantConstructor17;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array38 = array20;
			int num36 = 17;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor18 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor18.Name = "Site Mailbox Assistant";
			assistantConstructor18.ConstructorDelegate = (() => new SiteMailboxAssistantType());
			array38[num36] = assistantConstructor18;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array39 = array20;
			int num37 = 18;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor19 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor19.Name = "StoreIntegrityCheckAssistant";
			assistantConstructor19.ConstructorDelegate = (() => new StoreIntegrityCheckAssistantType());
			array39[num37] = assistantConstructor19;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array40 = array20;
			int num38 = 19;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor20 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor20.Name = "StoreScheduledIntegrityCheckAssistant";
			assistantConstructor20.ConstructorDelegate = (() => new StoreScheduledIntegrityCheckAssistantType());
			array40[num38] = assistantConstructor20;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array41 = array20;
			int num39 = 20;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor21 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor21.Name = "Mailbox Processor Assistant";
			assistantConstructor21.ConstructorDelegate = (() => new MailboxProcessorAssistantType());
			array41[num39] = assistantConstructor21;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array42 = array20;
			int num40 = 21;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor22 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor22.Name = "MailboxAssociationReplicationAssistant";
			assistantConstructor22.ConstructorDelegate = (() => new MailboxAssociationReplicationAssistantType());
			array42[num40] = assistantConstructor22;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array43 = array20;
			int num41 = 22;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor23 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor23.Name = "GroupMailboxAssistant";
			assistantConstructor23.ConstructorDelegate = (() => new GroupMailboxAssistantType());
			array43[num41] = assistantConstructor23;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array44 = array20;
			int num42 = 23;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor24 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor24.Name = "PeopleCentricTriageAssistant";
			assistantConstructor24.ConstructorDelegate = (() => new PeopleCentricTriageAssistantType());
			array44[num42] = assistantConstructor24;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array45 = array20;
			int num43 = 24;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor25 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor25.Name = "ProbeTimeBasedAssistant";
			assistantConstructor25.ConstructorDelegate = (() => new ProbeTimeBasedAssistantType());
			array45[num43] = assistantConstructor25;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array46 = array20;
			int num44 = 25;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor26 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor26.Name = "SearchIndexRepairAssistant";
			assistantConstructor26.ConstructorDelegate = (() => new SearchIndexRepairAssistantType());
			array46[num44] = assistantConstructor26;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] array47 = array20;
			int num45 = 26;
			InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType> assistantConstructor27 = new InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>();
			assistantConstructor27.Name = "DarTaskStoreTimeBasedAssistant";
			assistantConstructor27.ConstructorDelegate = (() => new TaskStoreTimeBasedAssistantType());
			array47[num45] = assistantConstructor27;
			InfoworkerAssistants.TimeBasedAssistantTypes = array20;
		}

		// Token: 0x0400000F RID: 15
		private const string DisabledAssistantsRegistryName = "DisabledAssistants";

		// Token: 0x04000010 RID: 16
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x04000011 RID: 17
		private static HashSet<string> disabledAssistantsNames;

		// Token: 0x04000012 RID: 18
		private static readonly Guid[] deprecatedAssistants = new Guid[]
		{
			AssistantIdentity.TransportSyncAssistant
		};

		// Token: 0x04000013 RID: 19
		private static readonly InfoworkerAssistants.EventAssistantConstructor[] EventBasedAssistantTypes;

		// Token: 0x04000014 RID: 20
		private static readonly InfoworkerAssistants.AssistantConstructor<ITimeBasedAssistantType>[] TimeBasedAssistantTypes;

		// Token: 0x02000004 RID: 4
		// (Invoke) Token: 0x06000043 RID: 67
		internal delegate T ConstructorDelegate<T>();

		// Token: 0x02000005 RID: 5
		// (Invoke) Token: 0x06000047 RID: 71
		internal delegate bool IsEnabledDelegate();

		// Token: 0x02000006 RID: 6
		internal class AssistantConstructor<T>
		{
			// Token: 0x04000042 RID: 66
			public string Name;

			// Token: 0x04000043 RID: 67
			public InfoworkerAssistants.ConstructorDelegate<T> ConstructorDelegate;

			// Token: 0x04000044 RID: 68
			public InfoworkerAssistants.IsEnabledDelegate IsEnabledDelegate;
		}

		// Token: 0x02000007 RID: 7
		private sealed class EventAssistantConstructor : InfoworkerAssistants.AssistantConstructor<IEventBasedAssistantType>
		{
			// Token: 0x04000045 RID: 69
			public Guid Identity;
		}
	}
}
