using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000D2 RID: 210
	internal class Initiating : ScheduledAction<DatabaseInfo>
	{
		// Token: 0x060008F4 RID: 2292 RVA: 0x0003C822 File Offset: 0x0003AA22
		public Initiating(ExDateTime expectedTime, DatabaseInfo databaseInfo) : base(expectedTime, databaseInfo)
		{
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0003C82C File Offset: 0x0003AA2C
		protected override void OnPerforming(long cookie)
		{
			bool flag = false;
			ThreadPriority priority = Thread.CurrentThread.Priority;
			try
			{
				Thread.CurrentThread.Priority = ThreadPriority.Lowest;
				if (base.ShouldContinue(cookie))
				{
					this.InitializeUserSettings(cookie);
					if (base.ShouldContinue(cookie))
					{
						this.SyncTextMessagingStates(cookie);
						flag = true;
					}
				}
			}
			catch (TransientMailboxException)
			{
				ExTraceGlobals.AssistantTracer.TraceError((long)this.GetHashCode(), "TransientMailboxException in Initiating.OnPerforming()");
			}
			finally
			{
				if (!flag && !base.ShouldContinue(cookie))
				{
					CalendarNotificationInitiator.ScheduleAction(new Initiating(base.ExpectedTime + Initiating.RetryInterval, base.Context), "retrying to initiate database");
				}
				Thread.CurrentThread.Priority = priority;
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0003C8F0 File Offset: 0x0003AAF0
		protected sealed override void OnPerformed(long cookie)
		{
			CalendarNotificationInitiator.CompleteAction(this, base.GetType().Name);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0003C903 File Offset: 0x0003AB03
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Initiating>(this);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0003C90C File Offset: 0x0003AB0C
		private void InitializeUserSettings(long cookie)
		{
			SystemMailbox instance = SystemMailbox.GetInstance(base.Context);
			if (!instance.IsValid())
			{
				this.ReloadSystemMailbox(cookie, instance);
			}
			Dictionary<string, UserSettings> allCalendarNotificationUsers = SystemMailbox.GetInstance(base.Context).GetAllCalendarNotificationUsers();
			foreach (KeyValuePair<string, UserSettings> keyValuePair in allCalendarNotificationUsers)
			{
				UserSettings value = keyValuePair.Value;
				if (!base.ShouldContinue(cookie))
				{
					break;
				}
				if (value.Voice != null || value.Text != null)
				{
					MailboxData mailboxData = null;
					try
					{
						mailboxData = MailboxData.CreateFromUserSettings(value);
						if (mailboxData != null)
						{
							if (!base.ShouldContinue(cookie))
							{
								break;
							}
							CalendarNotificationInitiator.CountInMailbox(mailboxData.DatabaseGuid, mailboxData.MailboxGuid);
							ExDateTime now = value.TimeZone.ExTimeZone.ConvertDateTime(base.ExpectedTime);
							MailboxData mailboxData2 = null;
							using (mailboxData.CreateReadLock())
							{
								mailboxData2 = MailboxData.UpdateCache(ref mailboxData);
							}
							using (mailboxData2.CreateReadLock())
							{
								using (mailboxData2.Actions.SyncObj.CreateWriteLock())
								{
									CalendarNotificationInitiator.InitiateEmittingReminder(now, mailboxData2);
									CalendarNotificationInitiator.InitiateEmittingSummary(now, mailboxData2);
								}
							}
							ExTraceGlobals.AssistantTracer.TraceDebug<string>((long)this.GetHashCode(), "notif initialized, user: {0}", value.LegacyDN);
						}
					}
					catch (Exception ex)
					{
						if (!CalendarNotificationAssistant.TryHandleException((long)this.GetHashCode(), "initiating", value.LegacyDN, ex))
						{
							throw;
						}
					}
					finally
					{
						if (mailboxData != null)
						{
							mailboxData.Dispose();
							mailboxData = null;
						}
					}
				}
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0003CB40 File Offset: 0x0003AD40
		internal void UpdateAllUserSettings(long cookie, Initiating.ActOnUserSettingsDelegate updateUserSettings, Predicate<ADRawEntry> validate)
		{
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.MailboxAssistants.CalendarNotificationAssistantSkipUserSettingsUpdate.Enabled)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
				IRecipientSession recipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(false, ConsistencyMode.IgnoreInvalid, sessionSettings, 664, "UpdateAllUserSettings", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\CalendarNotification\\Actions.cs");
				this.UpdateAllUserSettings(recipientSession, cookie, updateUserSettings, validate);
			}
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0003CBC0 File Offset: 0x0003ADC0
		internal void UpdateAllUserSettings(IRecipientSession recipientSession, long cookie, Initiating.ActOnUserSettingsDelegate updateUserSettings, Predicate<ADRawEntry> validate)
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.Database, new ADObjectId(base.Context.Guid)),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.UserMailbox)
			});
			ADPagedReader<ADRawEntry> adpagedReader = recipientSession.FindPagedADRawEntry(null, QueryScope.SubTree, filter, null, 0, new PropertyDefinition[]
			{
				ADObjectSchema.Id,
				ADRecipientSchema.TextMessagingState,
				IADMailStorageSchema.ExchangeGuid
			});
			using (IEnumerator<ADRawEntry> enumerator = adpagedReader.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ADRawEntry recipient = enumerator.Current;
					if (!base.ShouldContinue(cookie))
					{
						break;
					}
					if (validate == null || validate(recipient))
					{
						if (this.mailboxTable == null)
						{
							this.mailboxTable = base.Context.GetMailboxTable(ClientType.EventBased, new PropertyTagPropertyDefinition[0]);
						}
						if (!this.mailboxTable.Exists((MailboxInformation mbxInfo) => mbxInfo.MailboxGuid == (Guid)recipient[IADMailStorageSchema.ExchangeGuid]))
						{
							ADUser aduser = recipientSession.FindADUserByObjectId((ADObjectId)recipient[ADObjectSchema.Id]);
							if (aduser != null)
							{
								ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(aduser, null);
								MailboxSession mailboxSession = null;
								try
								{
									mailboxSession = base.Context.GetMailbox(exchangePrincipal, ClientType.EventBased, "UpdateAllUserSettings");
									if (!base.ShouldContinue(cookie))
									{
										break;
									}
									updateUserSettings(mailboxSession, recipientSession, aduser);
								}
								catch (NotSupportedException arg)
								{
									ExTraceGlobals.AssistantTracer.TraceError<string, NotSupportedException>((long)this.GetHashCode(), "Exception is caught during syncing text messaging settings to AD user object for {0}: {1}", exchangePrincipal.LegacyDn, arg);
								}
								catch (Exception ex)
								{
									if (!CalendarNotificationAssistant.TryHandleException((long)this.GetHashCode(), "Update all user's settings", exchangePrincipal.LegacyDn, ex))
									{
										throw;
									}
								}
								finally
								{
									if (mailboxSession != null)
									{
										mailboxSession.Dispose();
										mailboxSession = null;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0003CDFC File Offset: 0x0003AFFC
		internal void ReloadSystemMailbox(long cookie, SystemMailbox systemMailbox)
		{
			if (!systemMailbox.MoveCalendarNotificationSettingsToSettingsFolder())
			{
				ExTraceGlobals.AssistantTracer.TraceDebug((long)this.GetHashCode(), "SystemMailbox is invalid, the calendar notification settings may be lost");
			}
			systemMailbox.SetValid();
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0003CE9C File Offset: 0x0003B09C
		private void SyncTextMessagingStates(long cookie)
		{
			this.UpdateAllUserSettings(cookie, delegate(MailboxSession mailboxSession, IRecipientSession recipientSession, ADUser user)
			{
				using (VersionedXmlDataProvider versionedXmlDataProvider = new VersionedXmlDataProvider(mailboxSession))
				{
					TextMessagingAccount account = (TextMessagingAccount)versionedXmlDataProvider.Read<TextMessagingAccount>(user.Identity);
					if (this.ShouldContinue(cookie))
					{
						TextMessagingHelper.UpdateAndSaveTextMessgaingStateOnAdUser(account, user, recipientSession);
					}
				}
			}, (ADRawEntry user) => user[ADRecipientSchema.TextMessagingState] == null);
		}

		// Token: 0x04000621 RID: 1569
		private static readonly TimeSpan RetryInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000622 RID: 1570
		private List<MailboxInformation> mailboxTable;

		// Token: 0x020000D3 RID: 211
		// (Invoke) Token: 0x06000900 RID: 2304
		internal delegate void ActOnUserSettingsDelegate(MailboxSession mailboxSession, IRecipientSession recipientSession, ADUser user);
	}
}
