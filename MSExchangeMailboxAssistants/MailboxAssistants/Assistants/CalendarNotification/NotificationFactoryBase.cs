using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000EA RID: 234
	internal abstract class NotificationFactoryBase
	{
		// Token: 0x060009CB RID: 2507
		internal abstract bool IsFeatureEnabled(UserSettings settings);

		// Token: 0x060009CC RID: 2508
		internal abstract bool TryCreateEmitter(CalendarInfo calendarInfo, MailboxData mailboxData, out ICalendarNotificationEmitter emitter);

		// Token: 0x060009CD RID: 2509
		internal abstract bool IsReminderEnabled(UserSettings settings);

		// Token: 0x060009CE RID: 2510
		internal abstract bool IsSummaryEnabled(UserSettings settings);

		// Token: 0x060009CF RID: 2511
		internal abstract bool IsInterestedInCalendarChangeEvent(UserSettings settings);

		// Token: 0x060009D0 RID: 2512
		internal abstract bool IsInterestedInCalendarMeetingEvent(UserSettings settings);

		// Token: 0x060009D1 RID: 2513
		internal abstract void LoadUserSettingsFromMailboxSession(MailboxSession session, UserSettings settings);

		// Token: 0x060009D2 RID: 2514
		internal abstract string BuildSettingsItemBody(UserSettings settings);

		// Token: 0x060009D3 RID: 2515 RVA: 0x00041018 File Offset: 0x0003F218
		internal void UpdateSettingUnderSystemMailbox(UserSettings settings, SystemMailbox systemMailbox)
		{
			string settings2 = string.Empty;
			bool flag = this.IsFeatureEnabled(settings);
			if (flag)
			{
				settings2 = this.BuildSettingsItemBody(settings);
			}
			systemMailbox.Update(settings.ExternalDirectoryOrganizationId, settings.LegacyDN, settings2, this.GetSettingFolderId(systemMailbox));
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00041058 File Offset: 0x0003F258
		internal void GetAllUsersSettingsFromSystemMailbox(Dictionary<string, UserSettings> settingsDictionary, SystemMailbox systemMailbox, MailboxSession systemMailboxSession)
		{
			StoreObjectId settingFolderId = this.GetSettingFolderId(systemMailbox);
			using (Folder folder = Folder.Bind(systemMailboxSession, settingFolderId))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, SystemMailbox.SortByItemClass, SystemMailbox.RetrievingUserSettingsProperties))
				{
					if (queryResult.SeekToCondition(SeekReference.OriginBeginning, SystemMailbox.SettingsItemClassFilter))
					{
						bool flag = false;
						for (;;)
						{
							IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(10000);
							if (propertyBags.Length <= 0)
							{
								break;
							}
							foreach (IStorePropertyBag storePropertyBag in propertyBags)
							{
								if (!string.Equals(storePropertyBag.TryGetProperty(StoreObjectSchema.ItemClass) as string, "IPM.Configuration.UserCalendarNotification", StringComparison.OrdinalIgnoreCase))
								{
									flag = true;
									ExTraceGlobals.SystemMailboxTracer.TraceDebug((long)this.GetHashCode(), "traversed To different item class");
									break;
								}
								string text = storePropertyBag.TryGetProperty(ItemSchema.Subject) as string;
								if (string.Compare(text, "CalendarNotificationSettingsFolderValid") == 0)
								{
									ExTraceGlobals.SystemMailboxTracer.TraceDebug((long)this.GetHashCode(), "Find settigns folder valid flag");
								}
								else if (string.IsNullOrEmpty(text))
								{
									ExTraceGlobals.SystemMailboxTracer.TraceDebug((long)this.GetHashCode(), "Empty subject found, skipping.");
								}
								else if (text.Length < SystemMailbox.TenantGuidStringLength)
								{
									ExTraceGlobals.SystemMailboxTracer.TraceDebug<string>((long)this.GetHashCode(), "Subject value too small, skipped. Value {0}", text);
								}
								else
								{
									string text2 = text.Substring(0, SystemMailbox.TenantGuidStringLength);
									Guid externalDirectoryOrganizationId;
									if (!Guid.TryParse(text2, out externalDirectoryOrganizationId))
									{
										ExTraceGlobals.SystemMailboxTracer.TraceDebug<string>((long)this.GetHashCode(), "Invalid guid found in subject, skipped. Value {0}", text2);
									}
									else
									{
										string text3 = text.Substring(SystemMailbox.TenantGuidStringLength);
										if (string.IsNullOrEmpty(text3))
										{
											ExTraceGlobals.SystemMailboxTracer.TraceDebug((long)this.GetHashCode(), "Empty legacyDN found, skipped.");
										}
										else
										{
											string text4 = storePropertyBag.TryGetProperty(ItemSchema.TextBody) as string;
											if (string.IsNullOrEmpty(text4))
											{
												ExTraceGlobals.SystemMailboxTracer.TraceDebug<string>((long)this.GetHashCode(), "Setting body is corrupted for user {0}", text3);
											}
											else
											{
												UserSettings userSettings;
												if (!settingsDictionary.TryGetValue(text3, out userSettings))
												{
													userSettings = new UserSettings(text3, externalDirectoryOrganizationId);
													settingsDictionary[text3] = userSettings;
												}
												this.UpdateSettingsFromSettingsItemBody(userSettings, text4);
											}
										}
									}
								}
							}
							if (flag)
							{
								goto IL_21C;
							}
						}
						ExTraceGlobals.SystemMailboxTracer.TraceDebug((long)this.GetHashCode(), "PropertyBags length <= 0");
					}
					IL_21C:;
				}
			}
		}

		// Token: 0x060009D5 RID: 2517
		protected abstract StoreObjectId GetSettingFolderId(SystemMailbox systemMailbox);

		// Token: 0x060009D6 RID: 2518
		protected abstract void UpdateSettingsFromSettingsItemBody(UserSettings settings, string settingsItemBody);
	}
}
