﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Clutter
{
	// Token: 0x02000439 RID: 1081
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ClutterUtilities
	{
		// Token: 0x06003050 RID: 12368 RVA: 0x000C6558 File Offset: 0x000C4758
		static ClutterUtilities()
		{
			int num = 0;
			ClutterUtilities.FavoriteItemPropertiesMap = new Dictionary<PropertyDefinition, int>(ClutterUtilities.FavoriteItemProperties.Length);
			foreach (PropertyDefinition key in ClutterUtilities.FavoriteItemProperties)
			{
				ClutterUtilities.FavoriteItemPropertiesMap[key] = num++;
			}
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000C6668 File Offset: 0x000C4868
		public static bool IsClutterEligible(MailboxSession session, VariantConfigurationSnapshot snapshot)
		{
			snapshot = ClutterUtilities.LoadSnapshotIfNeeded(session, snapshot);
			return snapshot != null && snapshot.Inference.InferenceFolderBasedClutter.Enabled;
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000C6696 File Offset: 0x000C4896
		public static bool IsClutterEnabled(MailboxSession session, VariantConfigurationSnapshot snapshot)
		{
			return ClutterUtilities.IsClutterEligible(session, snapshot) && session.Mailbox.GetValueOrDefault<bool>(MailboxSchema.InferenceClutterEnabled, false);
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000C66B4 File Offset: 0x000C48B4
		public static bool IsClassificationEnabled(MailboxSession session, VariantConfigurationSnapshot snapshot)
		{
			return ClutterUtilities.IsClutterEnabled(session, snapshot) && session.Mailbox.GetValueOrDefault<bool>(MailboxSchema.InferenceClassificationEnabled, false);
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000C66D2 File Offset: 0x000C48D2
		public static StoreObjectId OptUserIn(MailboxSession session)
		{
			return ClutterUtilities.OptUserIn(session, null, null);
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000C66DC File Offset: 0x000C48DC
		public static StoreObjectId OptUserIn(MailboxSession session, VariantConfigurationSnapshot snapshot, IFrontEndLocator frontEndLocator)
		{
			snapshot = ClutterUtilities.LoadSnapshotIfNeeded(session, snapshot);
			StoreObjectId storeObjectId = null;
			if (!session.Mailbox.GetValueOrDefault<bool>(MailboxSchema.InferenceClutterEnabled, false) || !session.Mailbox.GetValueOrDefault<bool>(MailboxSchema.InferenceClassificationEnabled, false))
			{
				using (MailboxSession mailboxSession = ClutterUtilities.OpenAdminSession(session))
				{
					StoreObjectId storeObjectId2 = ClutterUtilities.SetClassificationEnabled(mailboxSession, true);
					ClutterUtilities.AddClutterToFavorite(session);
					ClutterUtilities.VerifyReadyVersionInCrumb(mailboxSession);
					storeObjectId = session.RefreshDefaultFolder(DefaultFolderType.Clutter);
					ClutterUtilities.RefreshInferenceProperties(session, true);
					using (NotificationManager notificationManager = new NotificationManager(session, snapshot, frontEndLocator))
					{
						notificationManager.SendNotification(ClutterNotificationType.OptedIn, DefaultFolderType.Inbox);
						notificationManager.ScheduleNotification(ClutterNotificationType.FirstReminder, 5, DayOfWeek.Monday);
						notificationManager.CancelScheduledNotification(ClutterNotificationType.AutoEnablementNotice);
						notificationManager.Save();
					}
					ExAssert.RetailAssert(mailboxSession.Mailbox.GetValueOrDefault<bool>(MailboxSchema.InferenceClutterEnabled, false), "InferenceClutterEnabled should have been set to true in admin session");
					ExAssert.RetailAssert(mailboxSession.Mailbox.GetValueOrDefault<bool>(MailboxSchema.InferenceClassificationEnabled, false), "InferenceClassificationEnabled should have been set to true in admin session");
					ExAssert.RetailAssert(storeObjectId2 != null, "Clutter folder id created in admin session should not be null");
					ExAssert.RetailAssert(storeObjectId2.Equals(storeObjectId), "Clutter folder id created in admin session should equal the id created by refreshing the user session");
					goto IL_FD;
				}
			}
			storeObjectId = ClutterUtilities.ValidateClutterFolder(session);
			IL_FD:
			ExAssert.RetailAssert(session.Mailbox.GetValueOrDefault<bool>(MailboxSchema.InferenceClutterEnabled, false), "InferenceClutterEnabled should have been set to true in user session");
			ExAssert.RetailAssert(session.Mailbox.GetValueOrDefault<bool>(MailboxSchema.InferenceClassificationEnabled, false), "InferenceClassificationEnabled should have been set to true in user session");
			ExAssert.RetailAssert(storeObjectId != null, "Clutter folder could not be created");
			return storeObjectId;
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000C684C File Offset: 0x000C4A4C
		public static void OptUserOut(MailboxSession session)
		{
			ClutterUtilities.OptUserOut(session, null, null);
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000C6858 File Offset: 0x000C4A58
		public static void OptUserOut(MailboxSession session, VariantConfigurationSnapshot snapshot, IFrontEndLocator frontEndLocator)
		{
			snapshot = ClutterUtilities.LoadSnapshotIfNeeded(session, snapshot);
			if (session.Mailbox.GetValueOrDefault<bool>(MailboxSchema.InferenceClassificationEnabled, false))
			{
				using (MailboxSession mailboxSession = ClutterUtilities.OpenAdminSession(session))
				{
					ClutterUtilities.SetClassificationEnabled(mailboxSession, false);
					ClutterUtilities.RefreshInferenceProperties(session, true);
					using (NotificationManager notificationManager = new NotificationManager(session, snapshot, frontEndLocator))
					{
						notificationManager.CancelScheduledNotifications();
						notificationManager.Save();
					}
				}
			}
			ExAssert.RetailAssert(!session.Mailbox.GetValueOrDefault<bool>(MailboxSchema.InferenceClassificationEnabled, false), "InferenceClassificationEnabled should have been set to false in user session");
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000C6900 File Offset: 0x000C4B00
		public static StoreObjectId ValidateClutterFolder(MailboxSession session)
		{
			StoreObjectId storeObjectId = session.GetDefaultFolderId(DefaultFolderType.Clutter);
			if (storeObjectId == null)
			{
				storeObjectId = session.CreateDefaultFolder(DefaultFolderType.Clutter);
			}
			else
			{
				session.TryFixDefaultFolderId(DefaultFolderType.Clutter, out storeObjectId);
			}
			return storeObjectId;
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x000C6930 File Offset: 0x000C4B30
		public static IClutterOverrideManager OpenOverrideManager(StoreSession session)
		{
			Util.ThrowOnNullArgument(session, "session");
			return new ClutterOverrideManager(session);
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000C6944 File Offset: 0x000C4B44
		public static void AddClutterToFavorite(MailboxSession session)
		{
			try
			{
				using (Folder folder = Folder.Bind(session, DefaultFolderType.Clutter))
				{
					if (folder != null)
					{
						StoreObjectId storeObjectId = folder.StoreObjectId;
						using (Folder folder2 = Folder.Bind(session, DefaultFolderType.CommonViews))
						{
							using (QueryResult queryResult = folder2.ItemQuery(ItemQueryType.Associated, ClutterUtilities.FavoriteItemQueryFilter, ClutterUtilities.FavoriteItemSortBy, ClutterUtilities.FavoriteItemProperties))
							{
								StoreObjectId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Inbox);
								if (defaultFolderId == null)
								{
									throw new InvalidOperationException("Inbox ID cannot be null.");
								}
								byte[] array = null;
								byte[] array2 = null;
								byte[] array3 = null;
								for (;;)
								{
									object[][] rows = queryResult.GetRows(10000);
									foreach (object[] array5 in rows)
									{
										byte[] array6 = array5[ClutterUtilities.FavoriteItemPropertiesMap[NavigationNodeSchema.Ordinal]] as byte[];
										array = (array ?? array6);
										StoreObjectId id = StoreObjectId.FromProviderSpecificIdOrNull(array5[ClutterUtilities.FavoriteItemPropertiesMap[NavigationNodeSchema.NodeEntryId]] as byte[]);
										if (storeObjectId.Equals(id))
										{
											goto Block_11;
										}
										if (array2 == null)
										{
											if (defaultFolderId.Equals(id))
											{
												array2 = array6;
											}
										}
										else if (array3 == null && !array6.SequenceEqual(array2))
										{
											array3 = array6;
										}
									}
									if (rows.Length <= 0)
									{
										goto Block_17;
									}
								}
								Block_11:
								return;
								Block_17:
								byte[] nodeBefore;
								byte[] nodeAfter;
								if (array2 != null)
								{
									nodeBefore = array2;
									nodeAfter = array3;
								}
								else
								{
									nodeBefore = null;
									nodeAfter = array;
								}
								using (FavoriteFolderEntry favoriteFolderEntry = FavoriteFolderEntry.Create(session, storeObjectId, FolderTreeDataType.NormalFolder))
								{
									favoriteFolderEntry.FolderDisplayName = folder.DisplayName;
									favoriteFolderEntry.SetNodeOrdinal(nodeBefore, nodeAfter);
									favoriteFolderEntry.Save(SaveMode.ResolveConflicts);
								}
							}
						}
					}
				}
			}
			catch (LocalizedException ex)
			{
				InferenceDiagnosticsLog.Log("AddClutterToFavorite", ex.ToString());
			}
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000C6B68 File Offset: 0x000C4D68
		public static ModelVersionBreadCrumb GetModelVersionBreadCrumb(MailboxSession session)
		{
			byte[] valueOrDefault = session.Mailbox.GetValueOrDefault<byte[]>(MailboxSchema.InferenceTrainedModelVersionBreadCrumb, null);
			return (valueOrDefault != null) ? new ModelVersionBreadCrumb(valueOrDefault) : new ModelVersionBreadCrumb(Array<byte>.Empty);
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000C6BA0 File Offset: 0x000C4DA0
		public static UserConfiguration GetInferenceSettingsConfiguration(MailboxSession session)
		{
			StoreId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Inbox);
			UserConfiguration userConfiguration = null;
			bool deleteOld = false;
			try
			{
				userConfiguration = session.UserConfigurationManager.GetFolderConfiguration("Inference.Settings", UserConfigurationTypes.Dictionary, defaultFolderId, null);
			}
			catch (ObjectNotFoundException arg)
			{
				InferenceDiagnosticsLog.Log("ClutterUtilities.GetInferenceSettingsConfiguration", string.Format("'{0}' is missing. Exception: {1}", "Inference.Settings", arg));
			}
			catch (CorruptDataException arg2)
			{
				deleteOld = true;
				InferenceDiagnosticsLog.Log("ClutterUtilities.GetInferenceSettingsConfiguration", string.Format("'{0}' is corrupt. Exception: {1}", "Inference.Settings", arg2));
			}
			if (userConfiguration == null || userConfiguration.GetDictionary() == null)
			{
				userConfiguration = ClutterUtilities.ResetInferenceSettingsConfiguration(session, deleteOld);
			}
			return userConfiguration;
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000C6C40 File Offset: 0x000C4E40
		internal static UserConfiguration ResetInferenceSettingsConfiguration(MailboxSession session, bool deleteOld)
		{
			StoreId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Inbox);
			if (deleteOld)
			{
				try
				{
					session.UserConfigurationManager.DeleteFolderConfigurations(defaultFolderId, new string[]
					{
						"Inference.Settings"
					});
				}
				catch (ObjectNotFoundException arg)
				{
					InferenceDiagnosticsLog.Log("ClutterUtilities.ResetInferenceSettingsConfiguration", string.Format("ObjectNotFoundException when deleting '{0}'. Exception: {1}", "Inference.Settings", arg));
				}
				catch (Exception arg2)
				{
					InferenceDiagnosticsLog.Log("ClutterUtilities.ResetInferenceSettingsConfiguration", string.Format("Error deleting '{0}'. Exception: {1}", "Inference.Settings", arg2));
					throw;
				}
			}
			UserConfiguration userConfiguration = null;
			try
			{
				userConfiguration = session.UserConfigurationManager.CreateFolderConfiguration("Inference.Settings", UserConfigurationTypes.Dictionary, defaultFolderId);
				userConfiguration.Save();
			}
			catch (Exception arg3)
			{
				InferenceDiagnosticsLog.Log("ClutterUtilities.ResetInferenceSettingsConfiguration", string.Format("Error creating '{0}'. Exception: {1}", "Inference.Settings", arg3));
				if (userConfiguration != null)
				{
					userConfiguration.Dispose();
				}
				throw;
			}
			return userConfiguration;
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x000C6D24 File Offset: 0x000C4F24
		internal static void SetClutterEnabled(MailboxSession adminSession)
		{
			adminSession.Mailbox[MailboxSchema.InferenceClutterEnabled] = true;
			adminSession.Mailbox.Save();
			ClutterUtilities.RefreshInferenceProperties(adminSession, false);
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000C6D50 File Offset: 0x000C4F50
		internal static StoreObjectId SetClassificationEnabled(MailboxSession adminSession, bool enabled)
		{
			bool flag;
			return ClutterUtilities.SetClassificationEnabled(adminSession, enabled, false, out flag);
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000C6D68 File Offset: 0x000C4F68
		internal static StoreObjectId SetClassificationEnabled(MailboxSession adminSession, bool enabled, bool checkNameClash, out bool hadUserFolderWithNameClash)
		{
			if (enabled)
			{
				adminSession.Mailbox[MailboxSchema.InferenceClutterEnabled] = true;
			}
			adminSession.Mailbox[MailboxSchema.InferenceClassificationEnabled] = enabled;
			adminSession.Mailbox.Save();
			ClutterUtilities.RefreshInferenceProperties(adminSession, false);
			if (enabled)
			{
				return ClutterUtilities.ValidateClutterFolder(adminSession, checkNameClash, out hadUserFolderWithNameClash);
			}
			hadUserFolderWithNameClash = false;
			return null;
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000C6DC8 File Offset: 0x000C4FC8
		internal static StoreObjectId ValidateClutterFolder(MailboxSession session, bool checkNameClash, out bool hadUserFolderWithNameClash)
		{
			hadUserFolderWithNameClash = false;
			if (checkNameClash && session.GetDefaultFolderId(DefaultFolderType.Clutter) == null)
			{
				using (Folder folder = Folder.Bind(session, DefaultFolderType.Root))
				{
					CultureInfo formatProvider = session.MailboxOwner.PreferredCultures.DefaultIfEmpty(session.InternalPreferedCulture).First<CultureInfo>();
					DefaultFolderInfo defaultFolderInfo = DefaultFolderInfo.Instance[68];
					string childFolderName = defaultFolderInfo.LocalizableDisplayName.ToString(formatProvider);
					if (folder.FindChildFolderByName(childFolderName) != null)
					{
						hadUserFolderWithNameClash = true;
					}
				}
			}
			return ClutterUtilities.ValidateClutterFolder(session);
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000C6E54 File Offset: 0x000C5054
		internal static void ValidateSmtpAddress(SmtpAddress smtpAddress)
		{
			if (!smtpAddress.IsValidAddress)
			{
				throw new ArgumentException(string.Format("Given an invalid SMTP Address: {0}", smtpAddress), "smtpAddress");
			}
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000C6E7A File Offset: 0x000C507A
		private static VariantConfigurationSnapshot LoadSnapshotIfNeeded(MailboxSession session, VariantConfigurationSnapshot snapshot)
		{
			if (snapshot == null)
			{
				InferenceDiagnosticsLog.Log("ClutterUtilities.LoadSnapshotIfNeeded", "VariantConfigurationSnapshot was not provided, so it is being loaded from the MailboxSession.MailboxOwner");
				return session.MailboxOwner.GetConfiguration();
			}
			return snapshot;
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000C6E9B File Offset: 0x000C509B
		private static MailboxSession OpenAdminSession(MailboxSession session)
		{
			return MailboxSession.OpenAsAdmin(session.MailboxOwner, session.Culture, "Client=TBA;Action=EnableClutter");
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000C6EB8 File Offset: 0x000C50B8
		private static void VerifyReadyVersionInCrumb(MailboxSession adminSession)
		{
			IServerModelConfiguration currentWrapper = ServerModelConfigurationWrapper.CurrentWrapper;
			List<short> second = (from i in currentWrapper.GetSupportedClassificationModelVersions()
			select (short)i).ToList<short>();
			ModelVersionBreadCrumb modelVersionBreadCrumb = ClutterUtilities.GetModelVersionBreadCrumb(adminSession);
			List<short> source = modelVersionBreadCrumb.GetVersions(ModelVersionBreadCrumb.VersionType.Ready).Intersect(second).ToList<short>();
			if (!source.Any<short>())
			{
				List<short> source2 = modelVersionBreadCrumb.GetVersions(ModelVersionBreadCrumb.VersionType.NotReady).Intersect(second).ToList<short>();
				if (source2.Any<short>())
				{
					short modelVersion = source2.Max<short>();
					modelVersionBreadCrumb.Add(modelVersion, ModelVersionBreadCrumb.VersionType.Ready);
					adminSession.Mailbox[MailboxSchema.InferenceTrainedModelVersionBreadCrumb] = modelVersionBreadCrumb.Serialize();
					adminSession.Mailbox.Save();
					adminSession.Mailbox.Load(new PropertyDefinition[]
					{
						MailboxSchema.InferenceTrainedModelVersionBreadCrumb
					});
				}
			}
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000C6F8C File Offset: 0x000C518C
		private static void RefreshInferenceProperties(MailboxSession session, bool force)
		{
			PropertyDefinition[] array = new PropertyDefinition[]
			{
				MailboxSchema.InferenceUserCapabilityFlags,
				MailboxSchema.InferenceClassificationEnabled,
				MailboxSchema.InferenceClutterEnabled
			};
			if (force)
			{
				session.Mailbox.ForceReload(array);
				return;
			}
			session.Mailbox.Load(array);
		}

		// Token: 0x04001A4D RID: 6733
		public const string InferenceConfigurationName = "Inference.Settings";

		// Token: 0x04001A4E RID: 6734
		private static readonly SortBy[] FavoriteItemSortBy = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(NavigationNodeSchema.GroupSection, SortOrder.Ascending),
			new SortBy(NavigationNodeSchema.Ordinal, SortOrder.Ascending)
		};

		// Token: 0x04001A4F RID: 6735
		private static readonly Dictionary<PropertyDefinition, int> FavoriteItemPropertiesMap = null;

		// Token: 0x04001A50 RID: 6736
		private static readonly PropertyDefinition[] FavoriteItemProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			ItemSchema.Subject,
			NavigationNodeSchema.GroupSection,
			NavigationNodeSchema.NodeEntryId,
			NavigationNodeSchema.Ordinal,
			NavigationNodeSchema.Type
		};

		// Token: 0x04001A51 RID: 6737
		private static readonly QueryFilter FavoriteItemQueryFilter = new AndFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Microsoft.WunderBar.Link"),
			new ComparisonFilter(ComparisonOperator.Equal, FolderTreeDataSchema.GroupSection, 1)
		});
	}
}
