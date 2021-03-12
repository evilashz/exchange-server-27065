using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.StoreConfigurableType
{
	// Token: 0x020009F4 RID: 2548
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class UserConfigurationHelper
	{
		// Token: 0x06005D21 RID: 23841 RVA: 0x0018AC10 File Offset: 0x00188E10
		public static IReadableUserConfiguration GetReadOnlyMailboxConfiguration(MailboxSession session, string configuration, UserConfigurationTypes type, bool createIfNonexisting)
		{
			return UserConfigurationHelper.InternalGetMailboxConfiguration<IReadableUserConfiguration>(session, configuration, type, createIfNonexisting, new Func<string, UserConfigurationTypes, IReadableUserConfiguration>(session.UserConfigurationManager.GetReadOnlyMailboxConfiguration), new Func<MailboxSession, string, UserConfigurationTypes, IReadableUserConfiguration>(UserConfigurationHelper.CreateMailboxConfiguration));
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x0018AC38 File Offset: 0x00188E38
		public static UserConfiguration GetMailboxConfiguration(MailboxSession session, string configuration, UserConfigurationTypes type, bool createIfNonexisting)
		{
			return UserConfigurationHelper.InternalGetMailboxConfiguration<UserConfiguration>(session, configuration, type, createIfNonexisting, new Func<string, UserConfigurationTypes, UserConfiguration>(session.UserConfigurationManager.GetMailboxConfiguration), new Func<MailboxSession, string, UserConfigurationTypes, UserConfiguration>(UserConfigurationHelper.CreateMailboxConfiguration));
		}

		// Token: 0x06005D23 RID: 23843 RVA: 0x0018AC60 File Offset: 0x00188E60
		public static UserConfiguration GetCalendarConfiguration(MailboxSession session, string configuration, UserConfigurationTypes type, bool createIfNonexisting)
		{
			StoreId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Calendar);
			if (defaultFolderId != null)
			{
				return UserConfigurationHelper.GetFolderConfiguration(session, defaultFolderId, configuration, type, createIfNonexisting, false);
			}
			return null;
		}

		// Token: 0x06005D24 RID: 23844 RVA: 0x0018AC90 File Offset: 0x00188E90
		public static IReadableUserConfiguration GetReadOnlyCalendarConfiguration(MailboxSession session, string configuration, UserConfigurationTypes type, bool createIfNonexisting)
		{
			StoreId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Calendar);
			if (defaultFolderId != null)
			{
				return UserConfigurationHelper.InternalGetFolderConfiguration<IReadableUserConfiguration>(session, defaultFolderId, configuration, type, (UserConfigurationManager configManager, string configName, UserConfigurationTypes configType, StoreId id) => configManager.GetReadOnlyFolderConfiguration(configName, configType, id), new Func<UserConfigurationManager, string, UserConfigurationTypes, StoreId, bool, IReadableUserConfiguration>(UserConfigurationHelper.RecreateFolderConfiguration), createIfNonexisting, false);
			}
			return null;
		}

		// Token: 0x06005D25 RID: 23845 RVA: 0x0018ACDE File Offset: 0x00188EDE
		public static UserConfiguration GetPublishingConfiguration(MailboxSession session, StoreId folderId, bool createIfNonexisting)
		{
			return UserConfigurationHelper.GetFolderConfiguration(session, folderId, "Calendar.PublishOptions", UserConfigurationTypes.Dictionary, createIfNonexisting, false);
		}

		// Token: 0x06005D26 RID: 23846 RVA: 0x0018ACFA File Offset: 0x00188EFA
		public static UserConfiguration GetFolderConfiguration(MailboxSession mailboxSession, StoreId folderId, string configName, UserConfigurationTypes configType, bool createIfNonexisting, bool saveIfNonexisting = false)
		{
			return UserConfigurationHelper.InternalGetFolderConfiguration<UserConfiguration>(mailboxSession, folderId, configName, configType, (UserConfigurationManager manager, string name, UserConfigurationTypes type, StoreId id) => manager.GetFolderConfiguration(name, type, id), new Func<UserConfigurationManager, string, UserConfigurationTypes, StoreId, bool, UserConfiguration>(UserConfigurationHelper.RecreateFolderConfiguration), createIfNonexisting, saveIfNonexisting);
		}

		// Token: 0x06005D27 RID: 23847 RVA: 0x0018AD34 File Offset: 0x00188F34
		private static T InternalGetFolderConfiguration<T>(MailboxSession mailboxSession, StoreId folderId, string configName, UserConfigurationTypes configType, Func<UserConfigurationManager, string, UserConfigurationTypes, StoreId, T> getter, Func<UserConfigurationManager, string, UserConfigurationTypes, StoreId, bool, T> recreator, bool createIfNonexisting, bool saveIfNonexisting = false) where T : class, IReadableUserConfiguration
		{
			if (folderId == null)
			{
				throw new InvalidOperationException();
			}
			UserConfigurationManager userConfigurationManager = mailboxSession.UserConfigurationManager;
			T result = default(T);
			try
			{
				result = getter(userConfigurationManager, configName, configType, folderId);
			}
			catch (ObjectNotFoundException)
			{
				if (createIfNonexisting)
				{
					try
					{
						result = recreator(userConfigurationManager, configName, configType, folderId, saveIfNonexisting);
					}
					catch (ObjectExistedException)
					{
						result = getter(userConfigurationManager, configName, configType, folderId);
					}
				}
			}
			return result;
		}

		// Token: 0x06005D28 RID: 23848 RVA: 0x0018ADAC File Offset: 0x00188FAC
		private static UserConfiguration RecreateFolderConfiguration(UserConfigurationManager configManager, string configName, UserConfigurationTypes configType, StoreId folderId, bool saveIfNonexisting)
		{
			UserConfiguration userConfiguration = configManager.CreateFolderConfiguration(configName, configType, folderId);
			if (saveIfNonexisting)
			{
				bool flag = false;
				try
				{
					userConfiguration.Save();
					flag = true;
				}
				finally
				{
					if (!flag && userConfiguration != null)
					{
						userConfiguration.Dispose();
						userConfiguration = null;
					}
				}
			}
			return userConfiguration;
		}

		// Token: 0x06005D29 RID: 23849 RVA: 0x0018ADF4 File Offset: 0x00188FF4
		public static void DeleteMailboxConfiguration(MailboxSession session, string configuration)
		{
			session.UserConfigurationManager.DeleteMailboxConfigurations(new string[]
			{
				configuration
			});
		}

		// Token: 0x06005D2A RID: 23850 RVA: 0x0018AE1C File Offset: 0x0018901C
		private static T InternalGetMailboxConfiguration<T>(MailboxSession session, string configuration, UserConfigurationTypes type, bool createIfNonexisting, Func<string, UserConfigurationTypes, T> getter, Func<MailboxSession, string, UserConfigurationTypes, T> creator) where T : class, IReadableUserConfiguration
		{
			T result = default(T);
			try
			{
				result = getter(configuration, type);
			}
			catch (ObjectNotFoundException)
			{
				if (createIfNonexisting)
				{
					result = creator(session, configuration, type);
				}
			}
			catch (CorruptDataException)
			{
				session.UserConfigurationManager.DeleteMailboxConfigurations(new string[]
				{
					configuration
				});
			}
			return result;
		}

		// Token: 0x06005D2B RID: 23851 RVA: 0x0018AE88 File Offset: 0x00189088
		private static UserConfiguration CreateMailboxConfiguration(MailboxSession session, string configuration, UserConfigurationTypes type)
		{
			UserConfiguration userConfiguration = session.UserConfigurationManager.CreateMailboxConfiguration(configuration, type);
			userConfiguration.Save();
			return userConfiguration;
		}

		// Token: 0x04003413 RID: 13331
		internal const string AggregatedAccountConfigurationName = "AggregatedAccount";

		// Token: 0x04003414 RID: 13332
		internal const string AggregatedAccountListConfigurationName = "AggregatedAccountList";

		// Token: 0x04003415 RID: 13333
		internal const string OwaUserOptionConfigurationName = "OWA.UserOptions";

		// Token: 0x04003416 RID: 13334
		internal const string CalendarConfigurationName = "Calendar";

		// Token: 0x04003417 RID: 13335
		internal const string CalendarFolderConfigurationName = "Calendar.PublishOptions";
	}
}
