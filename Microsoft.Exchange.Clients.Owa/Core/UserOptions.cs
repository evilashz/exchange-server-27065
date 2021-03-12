using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000289 RID: 649
	public class UserOptions
	{
		// Token: 0x060016D1 RID: 5841 RVA: 0x000852B6 File Offset: 0x000834B6
		private UserOptions(UserContext userContext, MailboxSession mailboxSession)
		{
			this.userContext = userContext;
			this.mailboxSession = mailboxSession;
			this.CreateOptionsProperties();
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x000852D4 File Offset: 0x000834D4
		private void CreateOptionsProperties()
		{
			this.optionProperties = new Dictionary<UserOptionPropertyDefinition, UserOptions.UserOptionPropertyValue>();
			for (int i = 0; i < UserOptionPropertySchema.Count; i++)
			{
				this.optionProperties.Add(UserOptionPropertySchema.GetPropertyDefinition(i), null);
			}
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0008530E File Offset: 0x0008350E
		public UserOptions(UserContext userContext) : this(userContext, null)
		{
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00085318 File Offset: 0x00083518
		internal static UserOptions CreateTemporaryInstance(MailboxSession mailboxSession)
		{
			return new UserOptions(null, mailboxSession);
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x00085321 File Offset: 0x00083521
		private MailboxSession MailboxSession
		{
			get
			{
				if (this.userContext != null)
				{
					return this.userContext.MailboxSession;
				}
				return this.mailboxSession;
			}
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0008533D File Offset: 0x0008353D
		public void ReloadAll()
		{
			this.CreateOptionsProperties();
			this.LoadAll();
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0008534C File Offset: 0x0008354C
		public void LoadAll()
		{
			IList<UserOptionPropertyDefinition> properties = new List<UserOptionPropertyDefinition>(this.optionProperties.Keys);
			this.Load(properties);
			this.isSynced = true;
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00085378 File Offset: 0x00083578
		public void CommitChanges()
		{
			if (this.isSynced)
			{
				return;
			}
			IList<UserOptionPropertyDefinition> list = new List<UserOptionPropertyDefinition>();
			foreach (UserOptionPropertyDefinition userOptionPropertyDefinition in this.optionProperties.Keys)
			{
				if (this.optionProperties[userOptionPropertyDefinition] != null && this.optionProperties[userOptionPropertyDefinition].IsModified)
				{
					list.Add(userOptionPropertyDefinition);
				}
			}
			this.Commit(list);
			this.isSynced = true;
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x00085410 File Offset: 0x00083610
		public bool IsSynced
		{
			get
			{
				return this.isSynced;
			}
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00085418 File Offset: 0x00083618
		private UserConfiguration GetUserConfiguration()
		{
			UserConfiguration userConfiguration = null;
			try
			{
				userConfiguration = this.MailboxSession.UserConfigurationManager.GetMailboxConfiguration("OWA.UserOptions", UserConfigurationTypes.Dictionary);
			}
			catch (AccessDeniedException ex)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Logon user does not have access permissions to user configuration object. Error: {0}. Stack: {1}.", ex.Message, ex.StackTrace);
				if (this.userContext.IsWebPartRequest)
				{
					return null;
				}
				throw;
			}
			catch (InvalidDataException ex2)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Recreating User Options due to invalid data. Error: {0}. Stack: {1}.", ex2.Message, ex2.StackTrace);
				return this.RecreateUserConfiguration(true);
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug((long)this.GetHashCode(), "Creating User Options because it does not exist.");
				return this.RecreateUserConfiguration(false);
			}
			bool flag = false;
			UserConfiguration result;
			try
			{
				userConfiguration.GetDictionary();
				flag = true;
				result = userConfiguration;
			}
			catch (InvalidOperationException ex3)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Recreating User Options due to no dictionary in UserConfiguration. Error: {0}. Stack: {1}.", ex3.Message, ex3.StackTrace);
				result = this.RecreateUserConfiguration(true);
			}
			catch (InvalidDataException ex4)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Recreating User Options due corrupt dictionary. Error: {0}. Stack: {1}.", ex4.Message, ex4.StackTrace);
				result = this.RecreateUserConfiguration(true);
			}
			finally
			{
				if (!flag && userConfiguration != null)
				{
					userConfiguration.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x000855A0 File Offset: 0x000837A0
		private UserConfiguration RecreateUserConfiguration(bool deleteFirst)
		{
			UserConfiguration userConfiguration = null;
			bool flag = false;
			UserConfiguration result;
			try
			{
				if (deleteFirst)
				{
					this.MailboxSession.UserConfigurationManager.DeleteMailboxConfigurations(new string[]
					{
						"OWA.UserOptions"
					});
				}
				userConfiguration = this.MailboxSession.UserConfigurationManager.CreateMailboxConfiguration("OWA.UserOptions", UserConfigurationTypes.Dictionary);
				userConfiguration.Save();
				flag = true;
				result = userConfiguration;
			}
			catch (StoragePermanentException ex)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Failed to recreate configuration data. Error: {0}. Stack: {1}.", ex.Message, ex.StackTrace);
				throw;
			}
			catch (StorageTransientException ex2)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Failed to recreate configuration data. Error: {0}. Stack: {1}.", ex2.Message, ex2.StackTrace);
				throw;
			}
			finally
			{
				if (!flag && userConfiguration != null)
				{
					userConfiguration.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x00085680 File Offset: 0x00083880
		private void Load(IList<UserOptionPropertyDefinition> properties)
		{
			using (UserConfiguration userConfiguration = this.GetUserConfiguration())
			{
				if (userConfiguration != null)
				{
					IDictionary dictionary = userConfiguration.GetDictionary();
					for (int i = 0; i < properties.Count; i++)
					{
						UserOptionPropertyDefinition userOptionPropertyDefinition = properties[i];
						string propertyName = userOptionPropertyDefinition.PropertyName;
						object originalValue = dictionary[userOptionPropertyDefinition.PropertyName];
						this.optionProperties[userOptionPropertyDefinition] = new UserOptions.UserOptionPropertyValue(userOptionPropertyDefinition.GetValidatedProperty(originalValue), false);
						ExTraceGlobals.UserOptionsDataTracer.TraceDebug((long)this.GetHashCode(), "Loaded property: {0}", new object[]
						{
							this.optionProperties[userOptionPropertyDefinition].Value
						});
					}
				}
				else
				{
					string value = WebPartUtilities.TryGetLocalMachineTimeZone();
					UserOptionPropertyDefinition propertyDefinition = UserOptionPropertySchema.GetPropertyDefinition(UserOptionPropertySchema.UserOptionPropertyID.TimeZone);
					this.optionProperties[propertyDefinition] = new UserOptions.UserOptionPropertyValue(value, false);
				}
			}
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0008576C File Offset: 0x0008396C
		private void Commit(IList<UserOptionPropertyDefinition> properties)
		{
			using (UserConfiguration userConfiguration = this.GetUserConfiguration())
			{
				IDictionary dictionary = userConfiguration.GetDictionary();
				Type typeFromHandle = typeof(int);
				for (int i = 0; i < properties.Count; i++)
				{
					UserOptionPropertyDefinition userOptionPropertyDefinition = properties[i];
					string propertyName = userOptionPropertyDefinition.PropertyName;
					if (userOptionPropertyDefinition.PropertyType == typeFromHandle)
					{
						dictionary[userOptionPropertyDefinition.PropertyName] = (int)this.optionProperties[userOptionPropertyDefinition].Value;
					}
					else
					{
						dictionary[userOptionPropertyDefinition.PropertyName] = this.optionProperties[userOptionPropertyDefinition].Value;
					}
					this.optionProperties[userOptionPropertyDefinition].IsModified = false;
					ExTraceGlobals.UserOptionsDataTracer.TraceDebug((long)this.GetHashCode(), "Committed property: {0}", new object[]
					{
						this.optionProperties[userOptionPropertyDefinition].Value
					});
				}
				try
				{
					userConfiguration.Save();
				}
				catch (StoragePermanentException ex)
				{
					ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Failed to save configuration data. Error: {0}. Stack: {1}.", ex.Message, ex.StackTrace);
					throw;
				}
				catch (StorageTransientException ex2)
				{
					ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Failed to save configuration data. Error: {0}. Stack: {1}.", ex2.Message, ex2.StackTrace);
					throw;
				}
			}
		}

		// Token: 0x17000636 RID: 1590
		private object this[UserOptionPropertySchema.UserOptionPropertyID propertyID]
		{
			get
			{
				UserOptionPropertyDefinition propertyDefinition = UserOptionPropertySchema.GetPropertyDefinition(propertyID);
				object obj;
				if (this.optionProperties.ContainsKey(propertyDefinition) && this.optionProperties[propertyDefinition] != null)
				{
					obj = this.optionProperties[propertyDefinition].Value;
				}
				else
				{
					obj = propertyDefinition.GetValidatedProperty(null);
				}
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, object>((long)this.GetHashCode(), "Get property: '{0}'; value: '{1}'", propertyDefinition.PropertyName, obj);
				return obj;
			}
			set
			{
				UserOptionPropertyDefinition propertyDefinition = UserOptionPropertySchema.GetPropertyDefinition(propertyID);
				UserOptions.UserOptionPropertyValue userOptionPropertyValue = new UserOptions.UserOptionPropertyValue(propertyDefinition.GetValidatedProperty(value), true);
				if (!this.optionProperties.ContainsKey(propertyDefinition))
				{
					this.optionProperties.Add(propertyDefinition, userOptionPropertyValue);
					this.isSynced = false;
					ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, object>((long)this.GetHashCode(), "Set property: '{0}'; value: '{1}'", propertyDefinition.PropertyName, this.optionProperties[propertyDefinition].Value);
					return;
				}
				if (this.optionProperties[propertyDefinition] == null || !userOptionPropertyValue.Value.Equals(this.optionProperties[propertyDefinition].Value))
				{
					this.optionProperties[propertyDefinition] = userOptionPropertyValue;
					this.isSynced = false;
					ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, object>((long)this.GetHashCode(), "Set property: '{0}'; value: '{1}'", propertyDefinition.PropertyName, this.optionProperties[propertyDefinition].Value);
				}
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x00085A60 File Offset: 0x00083C60
		// (set) Token: 0x060016E1 RID: 5857 RVA: 0x00085A6E File Offset: 0x00083C6E
		public string TimeZone
		{
			get
			{
				return (string)this[UserOptionPropertySchema.UserOptionPropertyID.TimeZone];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.TimeZone] = value;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x00085A78 File Offset: 0x00083C78
		// (set) Token: 0x060016E3 RID: 5859 RVA: 0x00085A86 File Offset: 0x00083C86
		public string TimeFormat
		{
			get
			{
				return (string)this[UserOptionPropertySchema.UserOptionPropertyID.TimeFormat];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.TimeFormat] = value;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x00085A90 File Offset: 0x00083C90
		// (set) Token: 0x060016E5 RID: 5861 RVA: 0x00085A9E File Offset: 0x00083C9E
		public string DateFormat
		{
			get
			{
				return (string)this[UserOptionPropertySchema.UserOptionPropertyID.DateFormat];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.DateFormat] = value;
			}
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00085AA8 File Offset: 0x00083CA8
		public string GetDateFormatNoYear()
		{
			Match match = UserOptions.dateFormatNoYearRegEx.Match(this.DateFormat);
			if (match.Success)
			{
				return match.Groups[0].Value;
			}
			return this.DateFormat;
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x00085AE6 File Offset: 0x00083CE6
		// (set) Token: 0x060016E8 RID: 5864 RVA: 0x00085AF4 File Offset: 0x00083CF4
		public DayOfWeek WeekStartDay
		{
			get
			{
				return (DayOfWeek)this[UserOptionPropertySchema.UserOptionPropertyID.WeekStartDay];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.WeekStartDay] = value;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x00085B03 File Offset: 0x00083D03
		// (set) Token: 0x060016EA RID: 5866 RVA: 0x00085B11 File Offset: 0x00083D11
		public int HourIncrement
		{
			get
			{
				return (int)this[UserOptionPropertySchema.UserOptionPropertyID.HourIncrement];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.HourIncrement] = value;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x00085B20 File Offset: 0x00083D20
		// (set) Token: 0x060016EC RID: 5868 RVA: 0x00085B2E File Offset: 0x00083D2E
		public bool ShowWeekNumbers
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.ShowWeekNumbers];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.ShowWeekNumbers] = value;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x00085B3D File Offset: 0x00083D3D
		// (set) Token: 0x060016EE RID: 5870 RVA: 0x00085B5F File Offset: 0x00083D5F
		public bool CheckNameInContactsFirst
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.CheckNameInContactsFirst] || !this.userContext.IsFeatureEnabled(Feature.GlobalAddressList);
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.CheckNameInContactsFirst] = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x00085B6E File Offset: 0x00083D6E
		// (set) Token: 0x060016F0 RID: 5872 RVA: 0x00085B7D File Offset: 0x00083D7D
		public string ThemeStorageId
		{
			get
			{
				return (string)this[UserOptionPropertySchema.UserOptionPropertyID.ThemeStorageId];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.ThemeStorageId] = value;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x00085B88 File Offset: 0x00083D88
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x00085B96 File Offset: 0x00083D96
		public int FirstWeekOfYear
		{
			get
			{
				return (int)this[UserOptionPropertySchema.UserOptionPropertyID.FirstWeekOfYear];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.FirstWeekOfYear] = value;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00085BA5 File Offset: 0x00083DA5
		// (set) Token: 0x060016F4 RID: 5876 RVA: 0x00085BB3 File Offset: 0x00083DB3
		public bool EnableReminders
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.EnableReminders];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.EnableReminders] = value;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00085BC2 File Offset: 0x00083DC2
		// (set) Token: 0x060016F6 RID: 5878 RVA: 0x00085BD1 File Offset: 0x00083DD1
		public bool EnableReminderSound
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.EnableReminderSound];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.EnableReminderSound] = value;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x00085BE1 File Offset: 0x00083DE1
		// (set) Token: 0x060016F8 RID: 5880 RVA: 0x00085BF0 File Offset: 0x00083DF0
		public NewNotification NewItemNotify
		{
			get
			{
				return (NewNotification)this[UserOptionPropertySchema.UserOptionPropertyID.NewItemNotify];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.NewItemNotify] = value;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x00085C00 File Offset: 0x00083E00
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x00085C0F File Offset: 0x00083E0F
		public int ViewRowCount
		{
			get
			{
				return (int)this[UserOptionPropertySchema.UserOptionPropertyID.ViewRowCount];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.ViewRowCount] = value;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x00085C1F File Offset: 0x00083E1F
		// (set) Token: 0x060016FC RID: 5884 RVA: 0x00085C2E File Offset: 0x00083E2E
		public int BasicViewRowCount
		{
			get
			{
				return (int)this[UserOptionPropertySchema.UserOptionPropertyID.BasicViewRowCount];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.BasicViewRowCount] = value;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x00085C3E File Offset: 0x00083E3E
		// (set) Token: 0x060016FE RID: 5886 RVA: 0x00085C4D File Offset: 0x00083E4D
		public int SpellingDictionaryLanguage
		{
			get
			{
				return (int)this[UserOptionPropertySchema.UserOptionPropertyID.SpellingDictionaryLanguage];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.SpellingDictionaryLanguage] = value;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x00085C5D File Offset: 0x00083E5D
		// (set) Token: 0x06001700 RID: 5888 RVA: 0x00085C6C File Offset: 0x00083E6C
		public bool SpellingIgnoreUppercase
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.SpellingIgnoreUppercase];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.SpellingIgnoreUppercase] = value;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x00085C7C File Offset: 0x00083E7C
		// (set) Token: 0x06001702 RID: 5890 RVA: 0x00085C8B File Offset: 0x00083E8B
		public bool SpellingIgnoreMixedDigits
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.SpellingIgnoreMixedDigits];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.SpellingIgnoreMixedDigits] = value;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x00085C9B File Offset: 0x00083E9B
		// (set) Token: 0x06001704 RID: 5892 RVA: 0x00085CAA File Offset: 0x00083EAA
		public bool SpellingCheckBeforeSend
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.SpellingCheckBeforeSend];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.SpellingCheckBeforeSend] = value;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x00085CBA File Offset: 0x00083EBA
		// (set) Token: 0x06001706 RID: 5894 RVA: 0x00085CC9 File Offset: 0x00083EC9
		public bool SmimeEncrypt
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.SmimeEncrypt];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.SmimeEncrypt] = value;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x00085CD9 File Offset: 0x00083ED9
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x00085CE8 File Offset: 0x00083EE8
		public bool SmimeSign
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.SmimeSign];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.SmimeSign] = value;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x00085CF8 File Offset: 0x00083EF8
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x00085D07 File Offset: 0x00083F07
		public bool AlwaysShowBcc
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.AlwaysShowBcc];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.AlwaysShowBcc] = value;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x00085D17 File Offset: 0x00083F17
		// (set) Token: 0x0600170C RID: 5900 RVA: 0x00085D26 File Offset: 0x00083F26
		public bool AlwaysShowFrom
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.AlwaysShowFrom];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.AlwaysShowFrom] = value;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x00085D36 File Offset: 0x00083F36
		// (set) Token: 0x0600170E RID: 5902 RVA: 0x00085D45 File Offset: 0x00083F45
		public Markup ComposeMarkup
		{
			get
			{
				return (Markup)this[UserOptionPropertySchema.UserOptionPropertyID.ComposeMarkup];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.ComposeMarkup] = value;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x00085D55 File Offset: 0x00083F55
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x00085D64 File Offset: 0x00083F64
		public string ComposeFontName
		{
			get
			{
				return (string)this[UserOptionPropertySchema.UserOptionPropertyID.ComposeFontName];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.ComposeFontName] = value;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00085D6F File Offset: 0x00083F6F
		// (set) Token: 0x06001712 RID: 5906 RVA: 0x00085D7E File Offset: 0x00083F7E
		public int ComposeFontSize
		{
			get
			{
				return (int)this[UserOptionPropertySchema.UserOptionPropertyID.ComposeFontSize];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.ComposeFontSize] = value;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x00085D8E File Offset: 0x00083F8E
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x00085D9D File Offset: 0x00083F9D
		public string ComposeFontColor
		{
			get
			{
				return (string)this[UserOptionPropertySchema.UserOptionPropertyID.ComposeFontColor];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.ComposeFontColor] = value;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x00085DA8 File Offset: 0x00083FA8
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x00085DB7 File Offset: 0x00083FB7
		public FontFlags ComposeFontFlags
		{
			get
			{
				return (FontFlags)this[UserOptionPropertySchema.UserOptionPropertyID.ComposeFontFlags];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.ComposeFontFlags] = value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x00085DC7 File Offset: 0x00083FC7
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x00085DD6 File Offset: 0x00083FD6
		public bool AutoAddSignature
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.AutoAddSignature];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.AutoAddSignature] = value;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x00085DE6 File Offset: 0x00083FE6
		// (set) Token: 0x0600171A RID: 5914 RVA: 0x00085DF5 File Offset: 0x00083FF5
		public string SignatureText
		{
			get
			{
				return (string)this[UserOptionPropertySchema.UserOptionPropertyID.SignatureText];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.SignatureText] = value;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x00085E00 File Offset: 0x00084000
		// (set) Token: 0x0600171C RID: 5916 RVA: 0x00085E0F File Offset: 0x0008400F
		public string SignatureHtml
		{
			get
			{
				return (string)this[UserOptionPropertySchema.UserOptionPropertyID.SignatureHtml];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.SignatureHtml] = value;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x00085E1A File Offset: 0x0008401A
		// (set) Token: 0x0600171E RID: 5918 RVA: 0x00085E29 File Offset: 0x00084029
		public bool BlockExternalContent
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.BlockExternalContent];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.BlockExternalContent] = value;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x00085E39 File Offset: 0x00084039
		// (set) Token: 0x06001720 RID: 5920 RVA: 0x00085E48 File Offset: 0x00084048
		public MarkAsRead PreviewMarkAsRead
		{
			get
			{
				return (MarkAsRead)this[UserOptionPropertySchema.UserOptionPropertyID.PreviewMarkAsRead];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.PreviewMarkAsRead] = value;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x00085E58 File Offset: 0x00084058
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x00085E67 File Offset: 0x00084067
		public int MarkAsReadDelaytime
		{
			get
			{
				return (int)this[UserOptionPropertySchema.UserOptionPropertyID.MarkAsReadDelaytime];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.MarkAsReadDelaytime] = value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x00085E77 File Offset: 0x00084077
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x00085E86 File Offset: 0x00084086
		public NextSelectionDirection NextSelection
		{
			get
			{
				return (NextSelectionDirection)this[UserOptionPropertySchema.UserOptionPropertyID.NextSelection];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.NextSelection] = value;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x00085E96 File Offset: 0x00084096
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x00085EA5 File Offset: 0x000840A5
		public ReadReceiptResponse ReadReceipt
		{
			get
			{
				return (ReadReceiptResponse)this[UserOptionPropertySchema.UserOptionPropertyID.ReadReceipt];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.ReadReceipt] = value;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x00085EB5 File Offset: 0x000840B5
		// (set) Token: 0x06001728 RID: 5928 RVA: 0x00085EC4 File Offset: 0x000840C4
		public bool EmptyDeletedItemsOnLogoff
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.EmptyDeletedItemsOnLogoff];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.EmptyDeletedItemsOnLogoff] = value;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x00085ED4 File Offset: 0x000840D4
		// (set) Token: 0x0600172A RID: 5930 RVA: 0x00085EE3 File Offset: 0x000840E3
		public int NavigationBarWidth
		{
			get
			{
				return (int)this[UserOptionPropertySchema.UserOptionPropertyID.NavigationBarWidth];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.NavigationBarWidth] = value;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x00085EF3 File Offset: 0x000840F3
		// (set) Token: 0x0600172C RID: 5932 RVA: 0x00085F02 File Offset: 0x00084102
		public bool IsMiniBarVisible
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.IsMiniBarVisible];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.IsMiniBarVisible] = value;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x00085F12 File Offset: 0x00084112
		// (set) Token: 0x0600172E RID: 5934 RVA: 0x00085F21 File Offset: 0x00084121
		public bool IsQuickLinksBarVisible
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.IsQuickLinksBarVisible];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.IsQuickLinksBarVisible] = value;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x00085F31 File Offset: 0x00084131
		// (set) Token: 0x06001730 RID: 5936 RVA: 0x00085F40 File Offset: 0x00084140
		public bool IsTaskDetailsVisible
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.IsTaskDetailsVisible];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.IsTaskDetailsVisible] = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x00085F50 File Offset: 0x00084150
		// (set) Token: 0x06001732 RID: 5938 RVA: 0x00085F5F File Offset: 0x0008415F
		public bool IsDocumentFavoritesVisible
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.IsDocumentFavoritesVisible];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.IsDocumentFavoritesVisible] = value;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x00085F6F File Offset: 0x0008416F
		// (set) Token: 0x06001734 RID: 5940 RVA: 0x00085F7E File Offset: 0x0008417E
		public FormatBarButtonGroups FormatBarState
		{
			get
			{
				return (FormatBarButtonGroups)this[UserOptionPropertySchema.UserOptionPropertyID.FormatBarState];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.FormatBarState] = value;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x00085F8E File Offset: 0x0008418E
		// (set) Token: 0x06001736 RID: 5942 RVA: 0x00085F9D File Offset: 0x0008419D
		public string MruFonts
		{
			get
			{
				return (string)this[UserOptionPropertySchema.UserOptionPropertyID.MruFonts];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.MruFonts] = value;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x00085FA8 File Offset: 0x000841A8
		// (set) Token: 0x06001738 RID: 5944 RVA: 0x00085FB7 File Offset: 0x000841B7
		public bool PrimaryNavigationCollapsed
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.PrimaryNavigationCollapsed];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.PrimaryNavigationCollapsed] = value;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x00085FC7 File Offset: 0x000841C7
		// (set) Token: 0x0600173A RID: 5946 RVA: 0x00085FD6 File Offset: 0x000841D6
		public bool MailFindBarOn
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.MailFindBarOn];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.MailFindBarOn] = value;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x00085FE6 File Offset: 0x000841E6
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x00085FF5 File Offset: 0x000841F5
		public bool CalendarFindBarOn
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.CalendarFindBarOn];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.CalendarFindBarOn] = value;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00086005 File Offset: 0x00084205
		// (set) Token: 0x0600173E RID: 5950 RVA: 0x00086014 File Offset: 0x00084214
		public bool ContactsFindBarOn
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.ContactsFindBarOn];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.ContactsFindBarOn] = value;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x00086024 File Offset: 0x00084224
		// (set) Token: 0x06001740 RID: 5952 RVA: 0x00086033 File Offset: 0x00084233
		public string SendAddressDefault
		{
			get
			{
				return (string)this[UserOptionPropertySchema.UserOptionPropertyID.SendAddressDefault];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.SendAddressDefault] = value;
			}
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00086040 File Offset: 0x00084240
		internal SearchScope GetSearchScope(OutlookModule outlookModule)
		{
			if (!this.MailboxSession.Mailbox.IsContentIndexingEnabled)
			{
				return SearchScope.SelectedFolder;
			}
			switch (outlookModule)
			{
			case OutlookModule.Tasks:
				return (SearchScope)this[UserOptionPropertySchema.UserOptionPropertyID.TasksSearchScope];
			case OutlookModule.Contacts:
				return (SearchScope)this[UserOptionPropertySchema.UserOptionPropertyID.ContactsSearchScope];
			default:
				return (SearchScope)this[UserOptionPropertySchema.UserOptionPropertyID.SearchScope];
			}
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x000860A0 File Offset: 0x000842A0
		internal void SetSearchScope(OutlookModule outlookModule, SearchScope searchScope)
		{
			switch (outlookModule)
			{
			case OutlookModule.Tasks:
				this[UserOptionPropertySchema.UserOptionPropertyID.TasksSearchScope] = searchScope;
				return;
			case OutlookModule.Contacts:
				this[UserOptionPropertySchema.UserOptionPropertyID.ContactsSearchScope] = searchScope;
				return;
			default:
				this[UserOptionPropertySchema.UserOptionPropertyID.SearchScope] = searchScope;
				return;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x000860ED File Offset: 0x000842ED
		// (set) Token: 0x06001744 RID: 5956 RVA: 0x000860FC File Offset: 0x000842FC
		public bool IsOptimizedForAccessibility
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.IsOptimizedForAccessibility];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.IsOptimizedForAccessibility] = value;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x0008610C File Offset: 0x0008430C
		// (set) Token: 0x06001746 RID: 5958 RVA: 0x0008611B File Offset: 0x0008431B
		public PontType EnabledPonts
		{
			get
			{
				return (PontType)this[UserOptionPropertySchema.UserOptionPropertyID.NewEnabledPonts];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.NewEnabledPonts] = value;
			}
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0008612B File Offset: 0x0008432B
		public bool IsPontEnabled(PontType pontType)
		{
			return (this.EnabledPonts & pontType) == pontType;
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x00086138 File Offset: 0x00084338
		// (set) Token: 0x06001749 RID: 5961 RVA: 0x00086147 File Offset: 0x00084347
		public FlagAction FlagAction
		{
			get
			{
				return (FlagAction)this[UserOptionPropertySchema.UserOptionPropertyID.FlagAction];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.FlagAction] = value;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x00086157 File Offset: 0x00084357
		// (set) Token: 0x0600174B RID: 5963 RVA: 0x00086166 File Offset: 0x00084366
		public bool AddRecipientsToAutoCompleteCache
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.AddRecipientsToAutoCompleteCache];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.AddRecipientsToAutoCompleteCache] = value;
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00086178 File Offset: 0x00084378
		private string FormatDateTime(string dateTimeFormat, bool useFullWeekdayFormat)
		{
			string text = useFullWeekdayFormat ? "dddd" : "ddd";
			CultureInfo userCulture = this.userContext.UserCulture;
			int lcid = userCulture.LCID;
			switch (lcid)
			{
			case 1041:
				if (dateTimeFormat.Contains("ddd"))
				{
					return dateTimeFormat;
				}
				return dateTimeFormat + " (" + text + ")";
			case 1042:
				break;
			default:
				if (lcid != 1055 && lcid != 1063)
				{
					return text + " " + dateTimeFormat;
				}
				break;
			}
			return dateTimeFormat + " (" + text + ")";
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0008620D File Offset: 0x0008440D
		public string GetWeekdayDateTimeFormat(bool useFullWeekdayFormat)
		{
			return this.GetWeekdayDateFormat(useFullWeekdayFormat) + " " + this.TimeFormat;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00086226 File Offset: 0x00084426
		public string GetWeekdayDateFormat(bool useFullWeekdayFormat)
		{
			return this.FormatDateTime(this.DateFormat, useFullWeekdayFormat);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00086235 File Offset: 0x00084435
		public string GetWeekdayTimeFormat(bool useFullWeekdayFormat)
		{
			return this.FormatDateTime(this.TimeFormat, useFullWeekdayFormat);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00086244 File Offset: 0x00084444
		public string GetWeekdayDateNoYearFormat(bool useFullWeekdayFormat)
		{
			return this.FormatDateTime(this.GetDateFormatNoYear(), useFullWeekdayFormat);
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x00086253 File Offset: 0x00084453
		// (set) Token: 0x06001752 RID: 5970 RVA: 0x00086262 File Offset: 0x00084462
		public bool ManuallyPickCertificate
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.ManuallyPickCertificate];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.ManuallyPickCertificate] = value;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x00086272 File Offset: 0x00084472
		// (set) Token: 0x06001754 RID: 5972 RVA: 0x00086281 File Offset: 0x00084481
		public string SigningCertificateSubject
		{
			get
			{
				return (string)this[UserOptionPropertySchema.UserOptionPropertyID.SigningCertificateSubject];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.SigningCertificateSubject] = value;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0008628C File Offset: 0x0008448C
		// (set) Token: 0x06001756 RID: 5974 RVA: 0x0008629B File Offset: 0x0008449B
		public string SigningCertificateId
		{
			get
			{
				return (string)this[UserOptionPropertySchema.UserOptionPropertyID.SigningCertificateId];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.SigningCertificateId] = value;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x000862A6 File Offset: 0x000844A6
		public bool UseManuallyPickedSigningCertificate
		{
			get
			{
				return OwaRegistryKeys.AllowUserChoiceOfSigningCertificate && this.ManuallyPickCertificate && !string.IsNullOrEmpty(this.SigningCertificateId);
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x000862C7 File Offset: 0x000844C7
		// (set) Token: 0x06001759 RID: 5977 RVA: 0x000862D6 File Offset: 0x000844D6
		public int UseDataCenterCustomTheme
		{
			get
			{
				return (int)this[UserOptionPropertySchema.UserOptionPropertyID.UseDataCenterCustomTheme];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.UseDataCenterCustomTheme] = value;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x000862E6 File Offset: 0x000844E6
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x000862F5 File Offset: 0x000844F5
		public ConversationSortOrder ConversationSortOrder
		{
			get
			{
				return (ConversationSortOrder)this[UserOptionPropertySchema.UserOptionPropertyID.ConversationSortOrder];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.ConversationSortOrder] = (int)value;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00086305 File Offset: 0x00084505
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x00086314 File Offset: 0x00084514
		public bool ShowTreeInListView
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.ShowTreeInListView];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.ShowTreeInListView] = value;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00086324 File Offset: 0x00084524
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x00086333 File Offset: 0x00084533
		public bool HideDeletedItems
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.HideDeletedItems];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.HideDeletedItems] = value;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x00086343 File Offset: 0x00084543
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x00086352 File Offset: 0x00084552
		public bool HideMailTipsByDefault
		{
			get
			{
				return (bool)this[UserOptionPropertySchema.UserOptionPropertyID.HideMailTipsByDefault];
			}
			set
			{
				this[UserOptionPropertySchema.UserOptionPropertyID.HideMailTipsByDefault] = value;
			}
		}

		// Token: 0x040011F7 RID: 4599
		private const string ConfigurationName = "OWA.UserOptions";

		// Token: 0x040011F8 RID: 4600
		private const string DateFormatNoYearExpression = "(M{1,2}[\\/\\.\\- ]d{1,2})|(d{1,2}[\\/\\.\\- ]M{1,2})";

		// Token: 0x040011F9 RID: 4601
		private static readonly Regex dateFormatNoYearRegEx = new Regex("(M{1,2}[\\/\\.\\- ]d{1,2})|(d{1,2}[\\/\\.\\- ]M{1,2})", RegexOptions.Compiled);

		// Token: 0x040011FA RID: 4602
		private MailboxSession mailboxSession;

		// Token: 0x040011FB RID: 4603
		private UserContext userContext;

		// Token: 0x040011FC RID: 4604
		private bool isSynced;

		// Token: 0x040011FD RID: 4605
		private Dictionary<UserOptionPropertyDefinition, UserOptions.UserOptionPropertyValue> optionProperties;

		// Token: 0x0200028A RID: 650
		private class UserOptionPropertyValue
		{
			// Token: 0x06001763 RID: 5987 RVA: 0x00086374 File Offset: 0x00084574
			internal UserOptionPropertyValue(object value, bool isModified)
			{
				this.propertyValue = value;
				this.isModified = isModified;
			}

			// Token: 0x17000674 RID: 1652
			// (get) Token: 0x06001764 RID: 5988 RVA: 0x0008638A File Offset: 0x0008458A
			internal object Value
			{
				get
				{
					return this.propertyValue;
				}
			}

			// Token: 0x17000675 RID: 1653
			// (get) Token: 0x06001765 RID: 5989 RVA: 0x00086392 File Offset: 0x00084592
			// (set) Token: 0x06001766 RID: 5990 RVA: 0x0008639A File Offset: 0x0008459A
			internal bool IsModified
			{
				get
				{
					return this.isModified;
				}
				set
				{
					this.isModified = value;
				}
			}

			// Token: 0x040011FE RID: 4606
			private object propertyValue;

			// Token: 0x040011FF RID: 4607
			private bool isModified;
		}
	}
}
