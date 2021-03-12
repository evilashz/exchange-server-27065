using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Mwi
{
	// Token: 0x02000104 RID: 260
	internal sealed class MwiAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x06000AAA RID: 2730 RVA: 0x00045928 File Offset: 0x00043B28
		public MwiAssistant(DatabaseInfo databaseInfo, ServerPickerBase<IMwiTarget, Guid> serverPicker, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			this.rootOrgConfigSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 152, ".ctor", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\mwi\\MwiAssistant.cs");
			this.loadBalancer = new MwiLoadBalancer(ExTraceGlobals.MWITracer, serverPicker, new UMServerMwiErrorStrategy());
			this.syncThreadRunning = new AutoResetEvent(true);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000459C4 File Offset: 0x00043BC4
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			return (MwiAssistant.IsVoicemailSearchFolderEvent(mapiEvent) && mapiEvent.CreateTime >= (DateTime)this.assistantUtcStartTime && (mapiEvent.ItemCount != -1L || mapiEvent.UnreadItemCount != -1L)) || (mapiEvent.ItemType == ObjectType.MAPI_MESSAGE && mapiEvent.EventMask == MapiEventTypeFlags.NewMail && (MwiAssistant.IsMissedCallMessageEvent(mapiEvent) || MwiAssistant.IsVoiceMessageEvent(mapiEvent)));
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00045A2C File Offset: 0x00043C2C
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			MailboxInfo mailbox = null;
			try
			{
				mailbox = new MailboxInfo(mapiEvent, itemStore.MailboxOwner.MailboxInfo.OrganizationId);
				this.ProcessMailbox(mailbox, itemStore, item);
			}
			catch (Exception error)
			{
				if (!this.TryHandleException(mailbox, error))
				{
					throw;
				}
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00045A7C File Offset: 0x00043C7C
		protected override void OnStartInternal(EventBasedStartInfo startInfo)
		{
			this.assistantUtcStartTime = ExDateTime.UtcNow;
			ExTraceGlobals.MWITracer.TraceDebug<ExDateTime>((long)this.GetHashCode(), "MwiAssistant.OnStartInternal(startTime={0})", this.assistantUtcStartTime);
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
			{
				TimeSpan timeSpan;
				if (MwiAssistant.TryReadTimeSpanValue(registryKey, "MwiSyncIntervalInSec", out timeSpan))
				{
					this.syncInterval = timeSpan;
				}
				if (MwiAssistant.TryReadTimeSpanValue(registryKey, "MwiMinUpdateIntervalInSec", out timeSpan))
				{
					this.minUpdateInterval = timeSpan;
				}
			}
			this.delayTable = new MwiDelayTable(this.minUpdateInterval, MwiAssistant.DelayQueueTimerResolution, new ProcessMailboxDelegate(this.DelayTableCallback));
			if (this.syncInterval.TotalSeconds > 0.0)
			{
				this.syncTimer = new Timer(new TimerCallback(this.SyncTimerCallback));
				this.syncTimer.Change(0, -1);
			}
			base.OnStartInternal(startInfo);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00045B6C File Offset: 0x00043D6C
		protected override void OnShutdownInternal()
		{
			ExTraceGlobals.MWITracer.TraceDebug((long)this.GetHashCode(), "MwiAssistant.OnShutdownInternal(); Starting shutdown");
			TimeSpan shutdownTimeout = MwiLoadBalancer.ShutdownTimeout;
			if (this.syncThreadRunning.WaitOne(shutdownTimeout, false))
			{
				ExTraceGlobals.MWITracer.TraceDebug((long)this.GetHashCode(), "MwiAssistant.OnShutdownInternal(); Disposing sync timer");
				if (this.syncTimer != null)
				{
					this.syncTimer.Dispose();
					this.syncTimer = null;
				}
				if (this.mailboxesToProcess != null)
				{
					this.mailboxesToProcess = null;
				}
			}
			else
			{
				ExTraceGlobals.MWITracer.TraceError(0L, "OnShutdownInternal: Timed out waiting for sync resource.");
			}
			if (this.delayTable != null)
			{
				this.delayTable.Shutdown(shutdownTimeout);
			}
			this.loadBalancer.Shutdown();
			base.OnShutdownInternal();
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00045C1C File Offset: 0x00043E1C
		private static bool TryReadTimeSpanValue(RegistryKey key, string name, out TimeSpan timeSpan)
		{
			timeSpan = TimeSpan.Zero;
			int? num = key.GetValue(name) as int?;
			if (num != null && num.Value >= 0)
			{
				ExTraceGlobals.MWITracer.TraceDebug<string, int>(0L, "MwiAssistant: Read {0} value from registry = {1}", name, num.Value);
				timeSpan = TimeSpan.FromSeconds((double)num.Value);
				return true;
			}
			return false;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00045C88 File Offset: 0x00043E88
		private static bool TryReadTextMessagingAccount(MailboxSession session, out TextMessagingAccount account)
		{
			using (VersionedXmlDataProvider versionedXmlDataProvider = new VersionedXmlDataProvider(session))
			{
				account = (TextMessagingAccount)versionedXmlDataProvider.Read<TextMessagingAccount>(session.MailboxOwner.ObjectId);
			}
			return account != null;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00045CD8 File Offset: 0x00043ED8
		private static bool TryGetNotificationPhoneNumber(MailboxInfo mailbox, MailboxSession session, ADUser user, out string phoneNumber, out CultureInfo notificationPreferredCulture)
		{
			phoneNumber = null;
			notificationPreferredCulture = null;
			TextMessagingAccount textMessagingAccount = null;
			if (MwiAssistant.TryReadTextMessagingAccount(session, out textMessagingAccount))
			{
				bool flag = TextMessagingHelper.IsMachineToPersonTextingOnlyAccount(textMessagingAccount);
				ExTraceGlobals.MWITracer.TraceDebug(0L, "MwiAssistant: TryGetNotificationPhoneNumber User = {0}, EASEnabled = {1}, Phone Number Verified = {2}, Phone Number = {3}, Can send sms = {4}", new object[]
				{
					user.DistinguishedName,
					textMessagingAccount.EasEnabled,
					textMessagingAccount.NotificationPhoneNumberVerified,
					(textMessagingAccount.NotificationPhoneNumber != null && textMessagingAccount.NotificationPhoneNumber.Number != null) ? textMessagingAccount.NotificationPhoneNumber.Number : "<null>",
					flag
				});
				if (flag)
				{
					phoneNumber = textMessagingAccount.NotificationPhoneNumber.Number;
					notificationPreferredCulture = textMessagingAccount.NotificationPreferredCulture;
				}
			}
			else
			{
				ExTraceGlobals.MWITracer.TraceWarning<string>(0L, "MwiAssistant: TryGetNotificationPhoneNumber Cannot send SMS because TextMessagingAccount does not exist for User {0}", user.DistinguishedName);
			}
			return phoneNumber != null;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00045DB4 File Offset: 0x00043FB4
		private static StoreObjectId GetUMSearchFolderId(MailboxSession itemStore)
		{
			StoreObjectId storeObjectId = itemStore.GetDefaultFolderId(DefaultFolderType.UMVoicemail);
			if (storeObjectId == null)
			{
				ExTraceGlobals.MWITracer.TraceDebug<string>(0L, "MwiAssistant.GetUMSearchFolderId(): Creating search folder for user {0}", itemStore.MailboxOwner.MailboxInfo.DisplayName);
				storeObjectId = itemStore.CreateDefaultFolder(DefaultFolderType.UMVoicemail);
			}
			return storeObjectId;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00045DF8 File Offset: 0x00043FF8
		private static bool ShouldLogException(Exception exception)
		{
			return !(exception is ObjectNotFoundException);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00045E05 File Offset: 0x00044005
		private static bool IsVoicemailSearchFolderEvent(MapiEvent mapiEvent)
		{
			return ObjectClass.IsVoicemailSearchFolder(mapiEvent.ObjectClass);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00045E12 File Offset: 0x00044012
		private static bool IsMissedCallMessageEvent(MapiEvent mapiEvent)
		{
			return ObjectClass.IsMissedCall(mapiEvent.ObjectClass);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00045E1F File Offset: 0x0004401F
		private static bool IsVoiceMessageEvent(MapiEvent mapiEvent)
		{
			return ObjectClass.IsVoiceMessage(mapiEvent.ObjectClass);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00045E2C File Offset: 0x0004402C
		private void SyncTimerCallback(object state)
		{
			bool flag = false;
			MailboxInfo mailbox = null;
			try
			{
				flag = this.syncThreadRunning.WaitOne(0, false);
				if (!flag)
				{
					ExTraceGlobals.MWITracer.TraceWarning((long)this.GetHashCode(), "MwiAssistant.SyncTimerCallback(); Overlaping timer calls.");
				}
				else if (this.mailboxesToProcess != null && this.mailboxesToProcess.Count > 0)
				{
					mailbox = this.mailboxesToProcess[0];
					this.mailboxesToProcess.RemoveAt(0);
					this.ProcessMailbox(mailbox, null, null);
				}
				else
				{
					ExTraceGlobals.MWITracer.TraceDebug((long)this.GetHashCode(), "SyncTimerCallback; We need to get a new mailbox list");
					this.SetupSyncTimer();
				}
			}
			catch (Exception error)
			{
				if (!this.TryHandleException(mailbox, error))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this.syncThreadRunning.Set();
				}
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00045EFC File Offset: 0x000440FC
		private void DelayTableCallback(MailboxInfo mailbox, MailboxSession session)
		{
			bool flag = false;
			try
			{
				ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo>((long)this.GetHashCode(), "MwiAssistant.DelayTableCallback running for {0}", mailbox);
				if (session == null)
				{
					ExchangePrincipal principal = ExchangePrincipal.FromLocalServerMailboxGuid(mailbox.OrganizationId.ToADSessionSettings(), base.DatabaseInfo.Guid, mailbox.Guid);
					session = base.DatabaseInfo.GetMailbox(principal, ClientType.TimeBased, "MwiAssistant Sync Thread");
					flag = true;
				}
				ExTraceGlobals.MWITracer.TraceDebug<MailboxSession, MailboxInfo>((long)this.GetHashCode(), "MwiAssistant.DelayTableCallback: Created session {0} for {1}", session, mailbox);
				this.SendMwiMessage(mailbox, session);
			}
			catch (Exception error)
			{
				if (!this.TryHandleException(mailbox, error))
				{
					throw;
				}
			}
			finally
			{
				if (flag && session != null)
				{
					session.Dispose();
					session = null;
				}
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00045FBC File Offset: 0x000441BC
		private void ProcessMailbox(MailboxInfo mailbox, MailboxSession session, StoreObject item)
		{
			ADUser aduser;
			UMDialPlan umdialPlan;
			bool flag;
			bool flag2;
			if (this.TryReadUserConfiguration(mailbox, out aduser, out umdialPlan, out flag, out flag2))
			{
				mailbox.DialPlanGuid = aduser.UMRecipientDialPlanId.ObjectGuid;
				mailbox.UMExtension = aduser.UMExtension;
				mailbox.DisplayName = aduser.DisplayName;
				ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo, UMSubscriberType, MapiEvent>((long)this.GetHashCode(), "MwiAssistant.ProcessMailbox: User:{0}. SubsType:{1} MapiEvent:{2}", mailbox, umdialPlan.SubscriberType, mailbox.MapiEvent);
				if (flag2)
				{
					this.ProcessMailboxForMWI(mailbox, session, item, umdialPlan);
				}
				else
				{
					ExTraceGlobals.MWITracer.TraceDebug<string>((long)this.GetHashCode(), "MwiAssistant.ProcessMailbox: Skip ProcessMailboxForMWI because UMMailboxPolicy.AllowMessageWaitingIndicator is false for User {0}}", aduser.DistinguishedName);
				}
				if (flag)
				{
					this.ProcessMailboxForSMS(mailbox, session, item, aduser, umdialPlan);
					return;
				}
				ExTraceGlobals.MWITracer.TraceDebug<string>((long)this.GetHashCode(), "MwiAssistant.ProcessMailbox: Skip ProcessMailboxForSMS because UMMailboxPolicy.AllowSMSNotification is false for User {0}}", aduser.DistinguishedName);
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00046080 File Offset: 0x00044280
		private void ProcessMailboxForMWI(MailboxInfo mailbox, MailboxSession session, StoreObject item, UMDialPlan dialPlan)
		{
			bool flag = mailbox.MapiEvent == null;
			if (dialPlan.SubscriberType == UMSubscriberType.Consumer)
			{
				ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo>((long)this.GetHashCode(), "MwiAssistant.ProcessMailboxForMWI: Skipping for {0} (MWI N/A for consumer dialplan)", mailbox);
				return;
			}
			if (!flag && !MwiAssistant.IsVoicemailSearchFolderEvent(mailbox.MapiEvent))
			{
				ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo>((long)this.GetHashCode(), "MwiAssistant.ProcessMailboxForMWI: Skipping for {0} (MWI only av. for healing/search folder events)", mailbox);
				return;
			}
			mailbox.EventTimeUtc = (flag ? ExDateTime.UtcNow : new ExDateTime(ExTimeZone.UtcTimeZone, mailbox.MapiEvent.CreateTime));
			this.delayTable.EnqueueMailbox(mailbox, session);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00046114 File Offset: 0x00044314
		private void ProcessMailboxForSMS(MailboxInfo mailbox, MailboxSession session, StoreObject item, ADUser user, UMDialPlan dialPlan)
		{
			if (mailbox.MapiEvent == null || session == null || item == null)
			{
				ExTraceGlobals.MWITracer.TraceDebug((long)this.GetHashCode(), "MwiAssistant.ProcessMailboxForSMS: Skipping for {0} (event={1}, session={2}, item={3})", new object[]
				{
					mailbox,
					mailbox.MapiEvent,
					session,
					item
				});
				return;
			}
			UMMailbox ummailbox = new UMMailbox(user);
			if (ummailbox.UMSMSNotificationOption == UMSMSNotificationOptions.None)
			{
				ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo>((long)this.GetHashCode(), "MwiAssistant.ProcessMailboxForSMS: Skipping {0}. UMSMSNotificationOptions=None", mailbox);
				return;
			}
			if (MwiAssistant.IsMissedCallMessageEvent(mailbox.MapiEvent) && ummailbox.UMSMSNotificationOption != UMSMSNotificationOptions.VoiceMailAndMissedCalls)
			{
				ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo, string>((long)this.GetHashCode(), "MwiAssistant.ProcessMailboxForSMS: Skipping {0}. UMSMSNotificationOptions={1}", mailbox, ummailbox.UMSMSNotificationOption.ToString());
				return;
			}
			this.SendSMSMessage(mailbox, session, item, user, dialPlan);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000461D8 File Offset: 0x000443D8
		private List<MailboxInfo> GetAllLocalUserMailboxes()
		{
			ExTraceGlobals.MWITracer.TraceDebug<string>((long)this.GetHashCode(), "GetAllLocalUserMailboxes(): Getting user mailboxes for database {0}", base.DatabaseInfo.DisplayName);
			LocalizedException ex = null;
			List<MailboxInfo> list = new List<MailboxInfo>();
			try
			{
				Hashtable umenabledUsersFromAD = this.GetUMEnabledUsersFromAD();
				ExTraceGlobals.MWITracer.TraceDebug<int>((long)this.GetHashCode(), "GetAllLocalUserMailboxes:GetUMEnabledUsersFromAD returned {0} records.", umenabledUsersFromAD.Count);
				if (umenabledUsersFromAD.Count != 0)
				{
					using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=EBA", null, null, null, null))
					{
						PropValue[][] mailboxTable = exRpcAdmin.GetMailboxTable(base.DatabaseInfo.Guid, new PropTag[]
						{
							PropTag.UserGuid,
							PropTag.DisplayName,
							PropTag.DateDiscoveredAbsentInDS
						});
						ExTraceGlobals.MWITracer.TraceError<string, int>((long)this.GetHashCode(), "GetAllLocalUserMailboxes: Database({0}).GetMailboxTable returned {1} records", base.DatabaseInfo.DisplayName, mailboxTable.Length);
						foreach (PropValue[] array2 in mailboxTable)
						{
							if (array2.Length == 3 && array2[0].PropTag == PropTag.UserGuid && array2[1].PropTag == PropTag.DisplayName && array2[2].PropTag != PropTag.DateDiscoveredAbsentInDS)
							{
								Guid guid = new Guid(array2[0].GetBytes());
								if (base.DatabaseInfo.IsUserMailbox(guid) && umenabledUsersFromAD.Contains(guid))
								{
									MailboxInfo mailboxInfo = new MailboxInfo(guid, array2[1].GetString(), (OrganizationId)umenabledUsersFromAD[guid]);
									ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo>((long)this.GetHashCode(), "GetAllLocalUserMailboxes(): Adding {0} to the mailbox list", mailboxInfo);
									list.Add(mailboxInfo);
								}
							}
						}
						goto IL_1CF;
					}
				}
				ExTraceGlobals.MWITracer.TraceError((long)this.GetHashCode(), "GetAllLocalUserMailboxes:GetUMEnabledUsersFromAD returned 0 records.");
				IL_1CF:;
			}
			catch (MapiPermanentException ex2)
			{
				ex = ex2;
			}
			catch (MapiRetryableException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ExTraceGlobals.MWITracer.TraceError<string, LocalizedException>((long)this.GetHashCode(), "TryGetMailboxEnumerator: Could not get the list of mailboxes for DB {0}: {1}", base.DatabaseInfo.DisplayName, ex);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MwiQueryDatabaseFailed, null, new object[]
				{
					base.DatabaseInfo.DisplayName,
					this.syncInterval.TotalMinutes,
					CommonUtil.ToEventLogString(ex)
				});
			}
			return list;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00046480 File Offset: 0x00044680
		private void SendMwiMessage(MailboxInfo mailbox, MailboxSession itemStore)
		{
			StoreObjectId umsearchFolderId = MwiAssistant.GetUMSearchFolderId(itemStore);
			if (umsearchFolderId == null)
			{
				ExTraceGlobals.MWITracer.TraceWarning<MailboxInfo>((long)this.GetHashCode(), "MwiAssistant.SendMwiMessage(): Skipping event. Could not get Voice Mail search folder for {0}.", mailbox);
				return;
			}
			using (SearchFolder searchFolder = SearchFolder.Bind(itemStore, umsearchFolderId, new PropertyDefinition[]
			{
				FolderSchema.ItemCount,
				FolderSchema.UnreadCount
			}))
			{
				int itemCount = searchFolder.ItemCount;
				int num = (int)searchFolder[FolderSchema.UnreadCount];
				ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo, int, int>((long)this.GetHashCode(), "MwiAssistant.SendMwiMessage(): Sending MWI for user {0}, itemCount={1}, unreadCount={2}", mailbox, itemCount, num);
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(mailbox.OrganizationId);
				Guid externalDirectoryOrganizationId = iadsystemConfigurationLookup.GetExternalDirectoryOrganizationId();
				MwiMessage message = new MwiMessage(mailbox.Guid, mailbox.DialPlanGuid, mailbox.DisplayName, mailbox.UMExtension, num, itemCount, MwiAssistant.MessageExpirationTime, mailbox.EventTimeUtc, externalDirectoryOrganizationId);
				this.loadBalancer.SendMessage(message);
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00046574 File Offset: 0x00044774
		private void SendSMSMessage(MailboxInfo mailbox, MailboxSession session, StoreObject item, ADUser user, UMDialPlan dialPlan)
		{
			ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo, MailboxSession, StoreObject>((long)this.GetHashCode(), "MwiAssistant.SendSMSMessage: Mailbox:{0} Session={1} Item={2}", mailbox, session, item);
			string text;
			CultureInfo preferredCulture;
			if (MwiAssistant.TryGetNotificationPhoneNumber(mailbox, session, user, out text, out preferredCulture))
			{
				StoreObjectId storeObjectId = session.GetDefaultFolderId(DefaultFolderType.Drafts) ?? session.GetDefaultFolderId(DefaultFolderType.Outbox);
				using (Folder.Bind(session, storeObjectId))
				{
					using (MessageItem messageItem = MessageItem.Create(session, storeObjectId))
					{
						messageItem.ClassName = "IPM.Note.Mobile.SMS.Alert.Voicemail";
						if (MwiAssistant.IsVoiceMessageEvent(mailbox.MapiEvent))
						{
							SmsVoicemailNotification smsVoicemailNotification = new SmsVoicemailNotification(session, preferredCulture, item, dialPlan);
							smsVoicemailNotification.PrepareSmsMessage(messageItem);
						}
						else
						{
							if (!MwiAssistant.IsMissedCallMessageEvent(mailbox.MapiEvent))
							{
								throw new ArgumentException("MapiEvent.ObjectClass");
							}
							SmsMissedCallNotification smsMissedCallNotification = new SmsMissedCallNotification(session, preferredCulture, item, dialPlan);
							smsMissedCallNotification.PrepareSmsMessage(messageItem);
						}
						Participant participant = new Participant(null, text, "MOBILE");
						messageItem.Recipients.Add(participant, RecipientItemType.To);
						string subject = messageItem.Subject;
						string text2 = string.Format("{0}:{1}", participant.RoutingType, participant.EmailAddress);
						ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo, string, string>((long)this.GetHashCode(), "MwiAssistant.SendSMSMessage: Mailbox:{0} Recipient:{1} Subject={2}. Sending...", mailbox, text2, subject);
						messageItem.Send();
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MwiTextMessageSent, null, new object[]
						{
							subject,
							mailbox,
							text,
							text2
						});
					}
				}
			}
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00046714 File Offset: 0x00044914
		private bool TryReadUserConfiguration(MailboxInfo mailbox, out ADUser user, out UMDialPlan dialPlan, out bool sendSms, out bool sendMsgWaitingIndicator)
		{
			user = null;
			dialPlan = null;
			sendSms = false;
			sendMsgWaitingIndicator = false;
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(mailbox.OrganizationId, null);
			user = (iadrecipientLookup.LookupByExchangeGuid(mailbox.Guid) as ADUser);
			if (user == null)
			{
				ExTraceGlobals.MWITracer.TraceError<Guid>((long)this.GetHashCode(), "MwiAssistant.TryReadUserConfiguration: Could not find ADUser for mailbox {0}", mailbox.Guid);
				return false;
			}
			if (!user.UMEnabled || user.UMMailboxPolicy == null || user.UMRecipientDialPlanId == null)
			{
				ExTraceGlobals.MWITracer.TraceError((long)this.GetHashCode(), "MwiAssistant.TryReadUserConfiguration: ({0}): Invalid user({1}) UMEnabled({2}) UMMbxPol({3}) DialPlan({4})", new object[]
				{
					mailbox.Guid,
					user.DistinguishedName,
					user.UMEnabled,
					user.UMMailboxPolicy,
					user.UMRecipientDialPlanId
				});
				return false;
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(user.UMMailboxPolicy), 1108, "TryReadUserConfiguration", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\mwi\\MwiAssistant.cs");
			UMMailboxPolicy ummailboxPolicy = tenantOrTopologyConfigurationSession.Read<UMMailboxPolicy>(user.UMMailboxPolicy);
			if (ummailboxPolicy == null)
			{
				ExTraceGlobals.MWITracer.TraceError<ADObjectId, string>((long)this.GetHashCode(), "MwiAssistant.TryReadUserConfiguration: Could not find UMMailboxPolicy: {0}, User {1}", user.UMMailboxPolicy, user.DistinguishedName);
				return false;
			}
			sendSms = ummailboxPolicy.AllowSMSNotification;
			sendMsgWaitingIndicator = ummailboxPolicy.AllowMessageWaitingIndicator;
			ExTraceGlobals.MWITracer.TraceDebug<bool, bool, string>((long)this.GetHashCode(), "MwiAssistant.TryReadUserConfiguration: AllowSMSNotification={0}, AllowMessageWaitingIndicator={1}, User {2}", sendSms, sendMsgWaitingIndicator, user.DistinguishedName);
			bool flag = sendSms || sendMsgWaitingIndicator;
			if (flag)
			{
				dialPlan = tenantOrTopologyConfigurationSession.Read<UMDialPlan>(user.UMRecipientDialPlanId);
				if (dialPlan == null)
				{
					ExTraceGlobals.MWITracer.TraceError<ADObjectId>((long)this.GetHashCode(), "MwiAssistant.TryReadUserConfiguration: Could not find UMDialPlan: {0}", user.UMRecipientDialPlanId);
					return false;
				}
				ExTraceGlobals.MWITracer.TraceDebug<string, ADObjectId, UMSubscriberType>((long)this.GetHashCode(), "MwiAssistant.TryReadUserConfiguration User {0} DialPlan{1}.SubscriberType={2}.", user.DistinguishedName, dialPlan.Id, dialPlan.SubscriberType);
			}
			return flag;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000468F4 File Offset: 0x00044AF4
		private void SetupSyncTimer()
		{
			List<MailboxInfo> allLocalUserMailboxes = this.GetAllLocalUserMailboxes();
			if (allLocalUserMailboxes.Count > 0)
			{
				TimeSpan timeSpan = TimeSpan.FromSeconds(this.syncInterval.TotalSeconds / (double)allLocalUserMailboxes.Count);
				if (timeSpan < this.minSyncInterval)
				{
					ExTraceGlobals.MWITracer.TraceDebug<double, double>((long)this.GetHashCode(), "SyncThread: intervalBetweenMailboxUpdates({0}s) < minSyncInterval({1}s)", timeSpan.TotalSeconds, this.minSyncInterval.TotalSeconds);
					timeSpan = this.minSyncInterval;
				}
				ExTraceGlobals.MWITracer.TraceDebug<string, int, double>((long)this.GetHashCode(), "SyncThread: Database {0} has {1} mailboxes, intervalBetweenMailboxes={2} seconds", base.DatabaseInfo.DisplayName, allLocalUserMailboxes.Count, timeSpan.TotalSeconds);
				this.mailboxesToProcess = allLocalUserMailboxes;
				this.syncTimer.Change(TimeSpan.Zero, timeSpan);
				return;
			}
			ExTraceGlobals.MWITracer.TraceDebug<double>((long)this.GetHashCode(), "SetupSyncTimer(); Could not query database or db has no users. We will try again in {0} mins.", this.syncInterval.TotalMinutes);
			this.syncTimer.Change(this.syncInterval, this.syncInterval);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000469EC File Offset: 0x00044BEC
		private Hashtable GetUMEnabledUsersFromAD()
		{
			MailboxDatabase database = this.rootOrgConfigSession.Read<MailboxDatabase>(new ADObjectId(base.DatabaseInfo.Guid));
			if (VariantConfiguration.InvariantNoFlightingSnapshot.MailboxAssistants.MwiAssistantGetUMEnabledUsersFromDatacenter.Enabled)
			{
				ExTraceGlobals.MWITracer.TraceDebug((long)this.GetHashCode(), "GetUMEnabledUsersFromAD() - Microsoft Hosted Topology.");
				return this.GetUMEnabledUsersDatacenter(database);
			}
			if (Datacenter.IsPartnerHostedOnly(true))
			{
				ExTraceGlobals.MWITracer.TraceDebug((long)this.GetHashCode(), "GetUMEnabledUsersFromAD() - Partner Hosted Topology.");
				return new Hashtable();
			}
			ExTraceGlobals.MWITracer.TraceDebug((long)this.GetHashCode(), "GetUMEnabledUsersFromAD() - Enterprise Topology.");
			return this.GetUMEnabledUsersEnterprise(database);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00046A90 File Offset: 0x00044C90
		private Hashtable GetUMEnabledUsersEnterprise(MailboxDatabase database)
		{
			Hashtable hashtable = new Hashtable();
			if (database != null)
			{
				QueryFilter umenabledUserQueryFilter = UMMailbox.GetUMEnabledUserQueryFilter(database);
				ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
				IRecipientSession recipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 1260, "GetUMEnabledUsersEnterprise", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\mwi\\MwiAssistant.cs");
				ADPagedReader<ADRecipient> adpagedReader = recipientSession.FindPaged(null, QueryScope.SubTree, umenabledUserQueryFilter, null, 0);
				foreach (ADRecipient adrecipient in adpagedReader)
				{
					ADUser aduser = adrecipient as ADUser;
					if (aduser != null)
					{
						hashtable.Add(aduser.ExchangeGuid, aduser.OrganizationId);
					}
				}
			}
			return hashtable;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00046B44 File Offset: 0x00044D44
		private Hashtable GetUMEnabledUsersDatacenter(MailboxDatabase database)
		{
			Hashtable hashtable = new Hashtable();
			if (database != null)
			{
				foreach (ADUser aduser in PartitionDataAggregator.GetUMEnabledUsersInDatabase(database))
				{
					hashtable[aduser.ExchangeGuid] = aduser.OrganizationId;
				}
			}
			return hashtable;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00046BAC File Offset: 0x00044DAC
		private bool TryHandleException(MailboxInfo mailbox, Exception error)
		{
			ExTraceGlobals.MWITracer.TraceError<MailboxInfo, string, Exception>((long)this.GetHashCode(), "MwiAssistant.TryHandleException: Mailbox {0} in database {1}: {2}", mailbox, base.DatabaseInfo.DisplayName, error);
			if (error is StorageTransientException || error is StoragePermanentException || error is LocalServerException || error is DataValidationException || error is DataSourceTransientException || error is DataSourceOperationException)
			{
				if (MwiAssistant.ShouldLogException(error))
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MwiSyncMailboxFailed, null, new object[]
					{
						mailbox,
						base.DatabaseInfo.DisplayName,
						CommonUtil.ToEventLogString(error)
					});
				}
				return true;
			}
			if (GrayException.IsGrayException(error))
			{
				ExWatson.SendReport(error, ReportOptions.None, null);
				return true;
			}
			return false;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00046C85 File Offset: 0x00044E85
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00046C8D File Offset: 0x00044E8D
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00046C95 File Offset: 0x00044E95
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x040006D8 RID: 1752
		private const string MobileRoutingType = "MOBILE";

		// Token: 0x040006D9 RID: 1753
		private const string MwiSyncIntervalName = "MwiSyncIntervalInSec";

		// Token: 0x040006DA RID: 1754
		private const string MwiMinUpdateItervalName = "MwiMinUpdateIntervalInSec";

		// Token: 0x040006DB RID: 1755
		private static readonly TimeSpan MessageExpirationTime = TimeSpan.FromSeconds(30.0);

		// Token: 0x040006DC RID: 1756
		private static readonly TimeSpan DelayQueueTimerResolution = TimeSpan.FromSeconds(5.0);

		// Token: 0x040006DD RID: 1757
		private IConfigurationSession rootOrgConfigSession;

		// Token: 0x040006DE RID: 1758
		private ExDateTime assistantUtcStartTime;

		// Token: 0x040006DF RID: 1759
		private MwiLoadBalancer loadBalancer;

		// Token: 0x040006E0 RID: 1760
		private Timer syncTimer;

		// Token: 0x040006E1 RID: 1761
		private List<MailboxInfo> mailboxesToProcess;

		// Token: 0x040006E2 RID: 1762
		private AutoResetEvent syncThreadRunning;

		// Token: 0x040006E3 RID: 1763
		private TimeSpan syncInterval = TimeSpan.FromHours(12.0);

		// Token: 0x040006E4 RID: 1764
		private TimeSpan minSyncInterval = TimeSpan.FromSeconds(10.0);

		// Token: 0x040006E5 RID: 1765
		private TimeSpan minUpdateInterval = TimeSpan.FromSeconds(40.0);

		// Token: 0x040006E6 RID: 1766
		private MwiDelayTable delayTable;
	}
}
