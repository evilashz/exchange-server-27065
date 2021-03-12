using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002AC RID: 684
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UserConfiguration : IDisposeTrackable, IUserConfiguration, IReadableUserConfiguration, IDisposable
	{
		// Token: 0x06001C69 RID: 7273 RVA: 0x00082D68 File Offset: 0x00080F68
		public static UserConfiguration GetConfiguration(Folder folder, UserConfigurationName configurationName, UserConfigurationTypes type)
		{
			EnumValidator.ThrowIfInvalid<UserConfigurationTypes>(type, "type");
			try
			{
				return UserConfiguration.GetIgnoringCache(null, folder, configurationName, type);
			}
			catch (ObjectNotFoundException arg)
			{
				ExTraceGlobals.StorageTracer.TraceError<ObjectNotFoundException>(0L, "UserConfiguration::GetConfiguration. User Configuration object not found. Exception = {0}.", arg);
			}
			return UserConfiguration.Create(folder, configurationName, type);
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00082DBC File Offset: 0x00080FBC
		public static UserConfiguration Create(Folder folder, UserConfigurationName configurationName, UserConfigurationTypes type)
		{
			EnumValidator.ThrowIfInvalid<UserConfigurationTypes>(type, "type");
			IList<IStorePropertyBag> list = null;
			try
			{
				list = UserConfiguration.QueryConfigurations(folder, null, configurationName, UserConfigurationSearchFlags.FullString, 1, new PropertyDefinition[0]);
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidOperationException(ServerStrings.ExCannotQueryAssociatedTable, innerException);
			}
			if (list.Count > 0)
			{
				ExTraceGlobals.StorageTracer.TraceError<UserConfigurationName>(0L, "UserConfiguration::Create. The configuration object has existed. ConfigName = {0}.", configurationName);
				throw new ObjectExistedException(ServerStrings.ExConfigExisted(configurationName.Name));
			}
			ConfigurationItem configurationItem = null;
			bool flag = false;
			UserConfiguration result;
			try
			{
				configurationItem = ItemBuilder.CreateNewItem<ConfigurationItem>(folder.Session, folder.Id, ItemCreateInfo.ConfigurationItemInfo, CreateMessageType.Associated);
				bool openMode = false;
				UserConfiguration userConfiguration = new UserConfiguration(configurationItem, folder.StoreObjectId, configurationName, type, openMode);
				flag = true;
				result = userConfiguration;
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(configurationItem);
				}
			}
			return result;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00082E88 File Offset: 0x00081088
		internal static UserConfiguration GetIgnoringCache(UserConfigurationManager manager, Folder folder, UserConfigurationName configurationName, UserConfigurationTypes freefetchType)
		{
			EnumValidator.AssertValid<UserConfigurationTypes>(freefetchType);
			UserConfiguration userConfiguration = null;
			PropertyDefinition[] columns;
			if ((freefetchType & UserConfigurationTypes.Dictionary) > (UserConfigurationTypes)0)
			{
				columns = new PropertyDefinition[]
				{
					InternalSchema.UserConfigurationDictionary
				};
			}
			else
			{
				columns = UserConfiguration.ZeroProperties;
			}
			SortBy[] sorts = new SortBy[]
			{
				new SortBy(InternalSchema.CreationTime, SortOrder.Ascending),
				new SortBy(InternalSchema.MID, SortOrder.Ascending)
			};
			IList<IStorePropertyBag> list = null;
			try
			{
				ExTraceGlobals.UserConfigurationTracer.TraceDebug<UserConfigurationName>(0L, "UserConfiguration::GetIgnoringCache. Hit Query. ConfigName = {0}.", configurationName);
				list = UserConfiguration.QueryConfigurations(folder, sorts, configurationName, UserConfigurationSearchFlags.FullString, 2, columns);
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidOperationException(ServerStrings.ExCannotQueryAssociatedTable, innerException);
			}
			if (list.Count <= 0)
			{
				ExTraceGlobals.StorageTracer.TraceError<UserConfigurationName, UserConfigurationTypes>(0L, "UserConfiguration::GetIgnoringCache. The configuration object was not found. Name = {0}, type = {1}.", configurationName, freefetchType);
				throw new ObjectNotFoundException(ServerStrings.ExConfigurationNotFound(configurationName.Name));
			}
			ExTraceGlobals.UserConfigurationTracer.TraceDebug<UserConfigurationName>(0L, "UserConfiguration::GetIgnoringCache. Find the config item and build user config object from it. ConfigName = {0}.", configurationName);
			bool flag = false;
			UserConfiguration result2;
			try
			{
				userConfiguration = UserConfiguration.BuildConfigurationFromQueryItem(folder, list[0], (freefetchType & UserConfigurationTypes.Dictionary) > (UserConfigurationTypes)0);
				if (list.Count > 1)
				{
					ExTraceGlobals.UserConfigurationTracer.TraceDebug<UserConfigurationName>(0L, "UserConfiguration::GetIgnoringCache. Found duplicate items. Conflict resolution. ConfigName = {0}.", configurationName);
					List<VersionedId> list2 = new List<VersionedId>();
					list = UserConfiguration.QueryConfigurations(folder, sorts, configurationName, UserConfigurationSearchFlags.FullString, 100, new PropertyDefinition[0]);
					for (int i = 1; i < list.Count; i++)
					{
						list2.Add((VersionedId)list[i][InternalSchema.ItemId]);
					}
					if (list2.Count > 0)
					{
						AggregateOperationResult result = folder.Session.Delete(DeleteItemFlags.HardDelete, list2.ToArray());
						UserConfiguration.CheckOperationResults(result, "DeleteDuplicates");
					}
				}
				flag = true;
				result2 = userConfiguration;
			}
			finally
			{
				if (!flag && userConfiguration != null)
				{
					userConfiguration.Dispose();
				}
			}
			return result2;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x00083040 File Offset: 0x00081240
		internal static ICollection<UserConfiguration> Find(Folder folder, string searchString, UserConfigurationSearchFlags searchFlags)
		{
			EnumValidator.AssertValid<UserConfigurationSearchFlags>(searchFlags);
			List<UserConfiguration> list = new List<UserConfiguration>();
			if (searchFlags != UserConfigurationSearchFlags.SubString && searchFlags != UserConfigurationSearchFlags.FullString && searchFlags != UserConfigurationSearchFlags.Prefix)
			{
				ExTraceGlobals.StorageTracer.TraceError<UserConfigurationSearchFlags>(0L, "UserConfiguration::FindItems. The search flag is not supported. searchFlags = {0}.", searchFlags);
				throw new NotSupportedException(ServerStrings.ExNotSupportedConfigurationSearchFlags(searchFlags.ToString()));
			}
			IList<IStorePropertyBag> list2 = null;
			try
			{
				list2 = UserConfiguration.QueryConfigurations(folder, null, new UserConfigurationName(searchString, ConfigurationNameKind.PartialName), searchFlags, StorageLimits.Instance.UserConfigurationMaxSearched + 1, new PropertyDefinition[0]);
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidOperationException(ServerStrings.ExCannotQueryAssociatedTable, innerException);
			}
			if (list2.Count > StorageLimits.Instance.UserConfigurationMaxSearched)
			{
				ExTraceGlobals.StorageTracer.TraceError<int, int>(0L, "UserConfiguration::FindItems. There are too many user configuration objects created with the same name. Max. = {0}, Find = {1}.", StorageLimits.Instance.UserConfigurationMaxSearched, list2.Count);
				throw new TooManyConfigurationObjectsException(ServerStrings.ExTooManyObjects(searchString, list2.Count, StorageLimits.Instance.UserConfigurationMaxSearched));
			}
			if (list2.Count > 0)
			{
				UserConfiguration.BuildConfigurationsFromFilterQuery(list2, folder, list);
			}
			return list;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0008313C File Offset: 0x0008133C
		internal static void Delete(Folder folder, string searchString, UserConfigurationSearchFlags searchFlags)
		{
			EnumValidator.AssertValid<UserConfigurationSearchFlags>(searchFlags);
			if (searchFlags != UserConfigurationSearchFlags.SubString && searchFlags != UserConfigurationSearchFlags.FullString && searchFlags != UserConfigurationSearchFlags.Prefix)
			{
				ExTraceGlobals.StorageTracer.TraceError<UserConfigurationSearchFlags>(0L, "UserConfiguration::FindItems. The search flag is not supported. searchFlags = {0}.", searchFlags);
				throw new NotSupportedException();
			}
			IList<IStorePropertyBag> list = null;
			try
			{
				list = UserConfiguration.QueryConfigurations(folder, null, new UserConfigurationName(searchString, ConfigurationNameKind.PartialName), searchFlags, StorageLimits.Instance.UserConfigurationMaxSearched, new PropertyDefinition[0]);
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidOperationException(ServerStrings.ExCannotQueryAssociatedTable, innerException);
			}
			List<VersionedId> list2 = new List<VersionedId>();
			for (int i = 0; i < list.Count; i++)
			{
				list2.Add((VersionedId)list[i][InternalSchema.ItemId]);
				if (i == list.Count - 1 || list2.Count > 100)
				{
					AggregateOperationResult result = folder.Session.Delete(DeleteItemFlags.SoftDelete, list2.ToArray());
					UserConfiguration.CheckOperationResults(result, "Delete");
					list2.Clear();
				}
			}
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00083228 File Offset: 0x00081428
		ConfigurationDictionary IUserConfiguration.GetConfigurationDictionary()
		{
			return this.GetConfigurationDictionary();
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x00083230 File Offset: 0x00081430
		private UserConfiguration(UserConfigurationName configurationName, UserConfigurationTypes type, IStorePropertyBag queryPropertyBag)
		{
			EnumValidator.AssertValid<UserConfigurationTypes>(type);
			this.queryPropertyBag = queryPropertyBag;
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x00083260 File Offset: 0x00081460
		internal UserConfiguration(ConfigurationItem configurationItem, StoreObjectId folderId, UserConfigurationName configurationName, UserConfigurationTypes type, bool openMode) : this(configurationName, type, null)
		{
			this.storeSession = configurationItem.Session;
			this.configType = type;
			this.configName = configurationName;
			this.item = configurationItem;
			this.id = configurationItem.StoreObjectId;
			this.folderId = folderId;
			this.isOpenMode = openMode;
			if (!this.isOpenMode)
			{
				this.dictionaryPropValue = new PropertyError(InternalSchema.UserConfigurationDictionary, PropertyErrorCode.NotFound);
			}
			else
			{
				this.dictionaryPropValue = null;
			}
			this.isDisposed = false;
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x000832E0 File Offset: 0x000814E0
		private UserConfiguration(UserConfigurationName configurationName, StoreSession storeSession, StoreObjectId folderId, VersionedId versionedId, object dictionaryObject, UserConfigurationTypes type, IStorePropertyBag queryPropertyBag) : this(configurationName, type, queryPropertyBag)
		{
			PropertyError propertyError = dictionaryObject as PropertyError;
			if (propertyError != null)
			{
				if (propertyError.PropertyErrorCode != PropertyErrorCode.NotFound)
				{
					ExTraceGlobals.StorageTracer.TraceError<string, PropertyErrorCode>((long)this.GetHashCode(), "UserConfiguration::.ctor. The configuration data is corrupted. Field = {0}, PropertyErrorCode = {1}.", "Dictionary", propertyError.PropertyErrorCode);
					throw new CorruptDataException(ServerStrings.ExConfigDataCorrupted("Dictionary"));
				}
			}
			else if (dictionaryObject != null && !(dictionaryObject is byte[]))
			{
				ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "UserConfiguration::.ctor. The configuration data is corrupted. Field = {0}.", "Dictionary");
				throw new CorruptDataException(ServerStrings.ExConfigDataCorrupted("Dictionary"));
			}
			this.storeSession = storeSession;
			this.folderId = folderId;
			this.id = versionedId.ObjectId;
			this.configName = configurationName;
			this.configType = type;
			this.dictionaryPropValue = dictionaryObject;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x000833A8 File Offset: 0x000815A8
		private static IList<IStorePropertyBag> QueryConfigurations(Folder folder, SortBy[] sorts, UserConfigurationName configurationName, UserConfigurationSearchFlags searchFlags, int maxRow, params PropertyDefinition[] columns)
		{
			List<IStorePropertyBag> list = new List<IStorePropertyBag>();
			columns = UserConfiguration.BuildPrefetchProperties(columns);
			IList<IStorePropertyBag> list2 = UserConfiguration.FetchAllConfigurations(folder, sorts, 10000, columns);
			IComparer customComparer = UserConfigurationName.GetCustomComparer(searchFlags);
			foreach (IStorePropertyBag storePropertyBag in list2)
			{
				string x = storePropertyBag.TryGetProperty(InternalSchema.ItemClass) as string;
				if (customComparer.Compare(x, configurationName) == 0)
				{
					list.Add(storePropertyBag);
				}
			}
			if (folder.Session is MailboxSession)
			{
				if (folder.Session.LogonType == LogonType.Owner)
				{
					UserConfiguration.AddToConfigurationCache(folder, list);
				}
				else
				{
					UserConfiguration.AddToConfigurationCache(folder, list2);
				}
			}
			return list;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x00083464 File Offset: 0x00081664
		private static void AddToConfigurationCache(Folder folder, IList<IStorePropertyBag> items)
		{
			if (items == null || items.Count == 0)
			{
				return;
			}
			MailboxSession mailboxSession = folder.Session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new NotSupportedException();
			}
			UserConfigurationManager userConfigurationManager = mailboxSession.UserConfigurationManager;
			List<VersionedId> list = new List<VersionedId>();
			new List<int>();
			string a = null;
			for (int i = 0; i < items.Count; i++)
			{
				IStorePropertyBag storePropertyBag = items[i];
				if (PropertyError.IsPropertyError(storePropertyBag.TryGetProperty(InternalSchema.ItemClass)))
				{
					ExTraceGlobals.StorageTracer.TraceError(0L, "The configuration data's field has been corrupted. Field = ItemClass.");
					throw new CorruptDataException(ServerStrings.ExConfigDataCorrupted("ItemClass"));
				}
				if (PropertyError.IsPropertyError(storePropertyBag.TryGetProperty(InternalSchema.ItemId)))
				{
					ExTraceGlobals.StorageTracer.TraceError(0L, "The configuration data's field has been corrupted. Field = Id.");
					throw new CorruptDataException(ServerStrings.ExConfigDataCorrupted("Id"));
				}
				string b = storePropertyBag.TryGetProperty(InternalSchema.ItemClass) as string;
				if (a == b)
				{
					list.Add((VersionedId)storePropertyBag[InternalSchema.ItemId]);
					items.RemoveAt(i);
					i--;
				}
				else
				{
					string name = (string)storePropertyBag[InternalSchema.ItemClass];
					if (UserConfigurationName.IsValidName(name, ConfigurationNameKind.ItemClass))
					{
						userConfigurationManager.UserConfigurationCache.Add(folder.Id.ObjectId, new UserConfigurationName(name, ConfigurationNameKind.ItemClass), ((VersionedId)storePropertyBag[InternalSchema.ItemId]).ObjectId);
					}
				}
				a = (storePropertyBag.TryGetProperty(InternalSchema.ItemClass) as string);
			}
			if (folder.Session.LogonType == LogonType.Owner)
			{
				userConfigurationManager.UserConfigurationCache.Save();
			}
			AggregateOperationResult result = folder.Session.Delete(DeleteItemFlags.SoftDelete, list.ToArray());
			UserConfiguration.CheckOperationResults(result, "DeleteDuplicates");
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x00083618 File Offset: 0x00081818
		internal static IList<IStorePropertyBag> FetchAllConfigurations(IFolder folder, SortBy[] sorts, int maxRow, params PropertyDefinition[] columns)
		{
			List<IStorePropertyBag> list = new List<IStorePropertyBag>();
			SortBy[] array;
			if (sorts == null)
			{
				array = new SortBy[]
				{
					new SortBy(InternalSchema.ItemClass, SortOrder.Ascending)
				};
			}
			else
			{
				array = new SortBy[sorts.Length + 1];
				array[0] = new SortBy(InternalSchema.ItemClass, SortOrder.Ascending);
				Array.Copy(sorts, 0, array, 1, sorts.Length);
			}
			ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, InternalSchema.ItemClass, "IPM.Configuration.");
			IList<IStorePropertyBag> result;
			using (IQueryResult queryResult = folder.IItemQuery(ItemQueryType.Associated, null, array, columns))
			{
				queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
				IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(10000);
				bool flag = false;
				while (!flag && propertyBags.Length > 0)
				{
					for (int i = 0; i < propertyBags.Length; i++)
					{
						string text = propertyBags[i].TryGetProperty(InternalSchema.ItemClass) as string;
						if (text != null)
						{
							if (!text.StartsWith("IPM.Configuration.") || list.Count > maxRow)
							{
								flag = true;
								break;
							}
							list.Add(propertyBags[i]);
						}
					}
					propertyBags = queryResult.GetPropertyBags(10000);
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x00083738 File Offset: 0x00081938
		private static UserConfiguration BuildConfigurationFromQueryItem(Folder folder, IStorePropertyBag row, bool hasLoadedUserConfigurationData)
		{
			if (PropertyError.IsPropertyError(row.TryGetProperty(InternalSchema.ItemClass)))
			{
				ExTraceGlobals.StorageTracer.TraceError(0L, "The configuration data's field has been corrupted. Field = ItemClass.");
				throw new CorruptDataException(ServerStrings.ExConfigDataCorrupted("ItemClass"));
			}
			if (PropertyError.IsPropertyError(row.TryGetProperty(InternalSchema.ItemId)))
			{
				ExTraceGlobals.StorageTracer.TraceError(0L, "The configuration data's field has been corrupted. Field = Id.");
				throw new CorruptDataException(ServerStrings.ExConfigDataCorrupted("Id"));
			}
			if (PropertyError.IsPropertyError(row.TryGetProperty(InternalSchema.UserConfigurationType)))
			{
				ExTraceGlobals.StorageTracer.TraceError(0L, "The configuration data's field has been corrupted. Field = UserConfigurationType.");
				throw new CorruptDataException(ServerStrings.ExConfigDataCorrupted("UserConfigurationType"));
			}
			string name = (string)row[InternalSchema.ItemClass];
			if (!UserConfigurationName.IsValidName(name, ConfigurationNameKind.ItemClass))
			{
				return null;
			}
			UserConfigurationName configurationName = new UserConfigurationName(name, ConfigurationNameKind.ItemClass);
			if (hasLoadedUserConfigurationData)
			{
				object dictionaryObject = UserConfiguration.CheckDictionaryData(row.TryGetProperty(InternalSchema.UserConfigurationDictionary));
				return new UserConfiguration(configurationName, folder.Session, folder.Id.ObjectId, (VersionedId)row[InternalSchema.ItemId], dictionaryObject, UserConfiguration.CheckUserConfigurationType((int)row[InternalSchema.UserConfigurationType]), row);
			}
			return new UserConfiguration(configurationName, folder.Session, folder.Id.ObjectId, (VersionedId)row[InternalSchema.ItemId], null, UserConfiguration.CheckUserConfigurationType((int)row[InternalSchema.UserConfigurationType]), row);
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x00083894 File Offset: 0x00081A94
		private static void BuildConfigurationsFromFilterQuery(IList<IStorePropertyBag> rows, Folder folder, List<UserConfiguration> userConfigs)
		{
			for (int i = 0; i < rows.Count; i++)
			{
				UserConfiguration userConfiguration = UserConfiguration.BuildConfigurationFromQueryItem(folder, rows[i], false);
				userConfigs.Add(userConfiguration);
			}
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x000838C8 File Offset: 0x00081AC8
		private static object CheckDictionaryData(object propValue)
		{
			object result = null;
			PropertyError propertyError = propValue as PropertyError;
			if (propertyError != null)
			{
				if (propertyError.PropertyErrorCode != PropertyErrorCode.NotFound)
				{
					ExTraceGlobals.StorageTracer.TraceError<string, PropertyError>(0L, "UserConfiguration::GetItem. The configuration object data is corrupted. Field = {0}, PropertyError = {1}.", "Dictionary", propertyError);
					throw new CorruptDataException(ServerStrings.ExConfigDataCorrupted("Dictionary"));
				}
				result = propValue;
			}
			else
			{
				byte[] array = (byte[])propValue;
				if (array.Length != 510)
				{
					result = propValue;
				}
			}
			return result;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00083929 File Offset: 0x00081B29
		private static void CheckOperationResults(AggregateOperationResult result, string methodName)
		{
			if (result.OperationResult != OperationResult.Succeeded)
			{
				ExTraceGlobals.UserConfigurationTracer.TraceDebug<string, AggregateOperationResult>(0L, "UserConfiguration::{0}. Operation failed. Result = {1}.", methodName, result);
			}
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00083948 File Offset: 0x00081B48
		private static PropertyDefinition[] BuildPrefetchProperties(params PropertyDefinition[] columns)
		{
			PropertyDefinition[] array = new PropertyDefinition[columns.Length + UserConfiguration.fixedPrefetchProperties.Length];
			Array.Copy(columns, array, columns.Length);
			Array.Copy(UserConfiguration.fixedPrefetchProperties, 0, array, columns.Length, UserConfiguration.fixedPrefetchProperties.Length);
			return array;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x00083987 File Offset: 0x00081B87
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<UserConfiguration>(this);
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0008398F File Offset: 0x00081B8F
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x000839A4 File Offset: 0x00081BA4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x000839B3 File Offset: 0x00081BB3
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x000839D8 File Offset: 0x00081BD8
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.item != null)
				{
					this.item.Dispose();
					this.item = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x00083A0A File Offset: 0x00081C0A
		private void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x00083A2C File Offset: 0x00081C2C
		public IDictionary GetDictionary()
		{
			return this.GetConfigurationDictionary();
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x00083A34 File Offset: 0x00081C34
		public Stream GetStream()
		{
			this.CheckDisposed("GetStream");
			this.CheckConfigurationType(UserConfigurationTypes.Stream);
			return this.InternalGetStream(InternalSchema.UserConfigurationStream, ref this.isStreamCreated);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x00083A59 File Offset: 0x00081C59
		public Stream GetXmlStream()
		{
			this.CheckDisposed("GetXmlStream");
			this.CheckConfigurationType(UserConfigurationTypes.XML);
			return this.InternalGetStream(InternalSchema.UserConfigurationXml, ref this.isXmlStreamCreated);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x00083A7E File Offset: 0x00081C7E
		public void Save()
		{
			this.Save(SaveMode.NoConflictResolution);
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x00083A88 File Offset: 0x00081C88
		public ConflictResolutionResult Save(SaveMode saveMode)
		{
			this.CheckDisposed("Save");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "UserConfiguration::Save.");
			if (!this.isOpenMode)
			{
				this.Item[InternalSchema.ItemClass] = this.configName.FullName;
				this.Item[InternalSchema.UserConfigurationType] = (int)this.configType;
			}
			this.Item.OpenAsReadWrite();
			if (this.configDictionary != null && this.configDictionary.IsDirty)
			{
				using (Stream stream = this.Item.OpenPropertyStream(InternalSchema.UserConfigurationDictionary, PropertyOpenMode.Create))
				{
					using (XmlWriter xmlWriter = UserConfiguration.InternalGetXmlWriter(stream))
					{
						this.configDictionary.WriteXml(xmlWriter);
					}
				}
			}
			ConflictResolutionResult conflictResolutionResult = this.Item.Save(saveMode);
			if (conflictResolutionResult.SaveStatus == SaveResult.Success || conflictResolutionResult.SaveStatus == SaveResult.SuccessWithConflictResolution)
			{
				if (!this.isOpenMode)
				{
					this.id = this.Item.StoreObjectId;
					this.ConflictDetection();
				}
				this.isOpenMode = true;
			}
			return conflictResolutionResult;
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x00083BB4 File Offset: 0x00081DB4
		public string ConfigurationName
		{
			get
			{
				this.CheckDisposed("ConfigurationName::get");
				return this.configName.Name;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x00083BCC File Offset: 0x00081DCC
		public UserConfigurationTypes DataTypes
		{
			get
			{
				this.CheckDisposed("DataTypes::get");
				return this.configType;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x00083BDF File Offset: 0x00081DDF
		public StoreObjectId FolderId
		{
			get
			{
				this.CheckDisposed("FolderId::get");
				return this.folderId;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x00083BF2 File Offset: 0x00081DF2
		public ExDateTime LastModifiedTime
		{
			get
			{
				this.CheckDisposed("LastModifiedTime::get");
				if (this.queryPropertyBag != null)
				{
					return (ExDateTime)this.queryPropertyBag[InternalSchema.LastModifiedTime];
				}
				return this.Item.LastModifiedTime;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x00083C28 File Offset: 0x00081E28
		public StoreObjectId Id
		{
			get
			{
				this.CheckDisposed("Id::get");
				return this.id;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x00083C3B File Offset: 0x00081E3B
		public VersionedId VersionedId
		{
			get
			{
				this.CheckDisposed("VersionedId::get");
				return this.Item.Id;
			}
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x00083C54 File Offset: 0x00081E54
		internal static UserConfigurationTypes CheckUserConfigurationType(int type)
		{
			if (!EnumValidator.IsValidValue<UserConfigurationTypes>((UserConfigurationTypes)type))
			{
				ExTraceGlobals.StorageTracer.TraceError(0L, "The configuration data's field has been corrupted. Field = UserConfigurationType.");
				throw new CorruptDataException(ServerStrings.ExConfigDataCorrupted("UserConfigurationType"));
			}
			return (UserConfigurationTypes)type;
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00083C8D File Offset: 0x00081E8D
		private ConfigurationDictionary GetConfigurationDictionary()
		{
			this.CheckDisposed("GetDictionary");
			this.CheckConfigurationType(UserConfigurationTypes.Dictionary);
			this.configDictionary = this.InternalGetDictionary();
			return this.configDictionary;
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x00083CB3 File Offset: 0x00081EB3
		private ConfigurationItem Item
		{
			get
			{
				if (this.item == null)
				{
					this.item = ConfigurationItem.Bind(this.storeSession, this.Id);
				}
				else
				{
					this.item.Load(null);
				}
				return this.item;
			}
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00083CE8 File Offset: 0x00081EE8
		private Stream InternalGetStream(PropertyDefinition propertyDefinition, ref bool isStreamCreated)
		{
			this.Item.OpenAsReadWrite();
			if (!this.isOpenMode && !isStreamCreated)
			{
				isStreamCreated = true;
				return this.Item.OpenPropertyStream(propertyDefinition, PropertyOpenMode.Create);
			}
			Stream result = null;
			try
			{
				result = this.Item.OpenPropertyStream(propertyDefinition, PropertyOpenMode.Modify);
			}
			catch (ObjectNotFoundException)
			{
				result = this.Item.OpenPropertyStream(propertyDefinition, PropertyOpenMode.Create);
			}
			return result;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x00083D54 File Offset: 0x00081F54
		private object InternalFallbackGetProperty(PropertyDefinition propertyDefinition)
		{
			object result = null;
			try
			{
				this.Item.OpenAsReadWrite();
				using (Stream stream = this.Item.OpenPropertyStream(propertyDefinition, PropertyOpenMode.ReadOnly))
				{
					result = Util.StreamHandler.ReadBytesFromStream(stream);
				}
			}
			catch (ObjectNotFoundException)
			{
				result = new PropertyError(InternalSchema.UserConfigurationDictionary, PropertyErrorCode.NotFound);
			}
			return result;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00083DC0 File Offset: 0x00081FC0
		private ConfigurationDictionary DeserializeDictionary(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				this.configDictionary = new ConfigurationDictionary();
				this.configDictionary.ReadXml(UserConfiguration.InternalGetXmlReader(memoryStream));
			}
			return this.configDictionary;
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00083E18 File Offset: 0x00082018
		private ConfigurationDictionary InternalGetDictionary()
		{
			if (this.configDictionary != null)
			{
				return this.configDictionary;
			}
			if (this.dictionaryPropValue == null)
			{
				this.dictionaryPropValue = this.InternalFallbackGetProperty(InternalSchema.UserConfigurationDictionary);
			}
			if (this.dictionaryPropValue is PropertyError)
			{
				return new ConfigurationDictionary();
			}
			try
			{
				if (this.dictionaryPropValue != null)
				{
					this.configDictionary = this.DeserializeDictionary((byte[])this.dictionaryPropValue);
				}
			}
			catch (XmlException ex)
			{
				ExTraceGlobals.StorageTracer.TraceError<XmlException>((long)this.GetHashCode(), "Deserializing dictionary failed. Exception = {0}.", ex);
				throw new CorruptDataException(ServerStrings.ExConfigSerializeDictionaryFailed(ex));
			}
			return this.configDictionary;
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00083EBC File Offset: 0x000820BC
		private void ConflictDetection()
		{
			using (Folder folder = Folder.Bind(this.storeSession, this.FolderId))
			{
				SortBy[] sorts = new SortBy[]
				{
					new SortBy(InternalSchema.LastModifiedTime, SortOrder.Ascending),
					new SortBy(InternalSchema.CreationTime, SortOrder.Ascending),
					new SortBy(InternalSchema.MID, SortOrder.Ascending)
				};
				IList<IStorePropertyBag> list = UserConfiguration.QueryConfigurations(folder, sorts, this.configName, UserConfigurationSearchFlags.FullString, 2, new PropertyDefinition[0]);
				if (list.Count <= 0)
				{
					ExTraceGlobals.UserConfigurationTracer.TraceDebug((long)this.GetHashCode(), "UserConfiguration::ConflictDetection. The object I just created cannot be found.");
				}
				else
				{
					VersionedId versionedId = (VersionedId)list[0][InternalSchema.ItemId];
					if (list.Count >= 1 && !this.id.Equals(versionedId.ObjectId))
					{
						this.Dispose();
						AggregateOperationResult aggregateOperationResult = this.storeSession.Delete(DeleteItemFlags.SoftDelete, new StoreId[]
						{
							this.id
						});
						if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
						{
							ExTraceGlobals.UserConfigurationTracer.TraceDebug<UserConfigurationName>((long)this.GetHashCode(), "UserConfiguration::ConflictResolution. Failed to delete configuration item. ConfigName = {0}.", this.configName);
						}
						ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "UserConfiguration::Save. The same configuration object was created by someone prio to the current one. ConfigName = {0}.", this.configName.Name);
						throw new ObjectExistedException(ServerStrings.ExConfigExisted(this.configName.Name));
					}
				}
			}
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00084034 File Offset: 0x00082234
		private void CheckConfigurationType(UserConfigurationTypes type)
		{
			if ((type & this.configType) == (UserConfigurationTypes)0)
			{
				ExTraceGlobals.StorageTracer.TraceError<UserConfigurationTypes, UserConfigurationTypes>((long)this.GetHashCode(), "UserConfiguration::CheckDataType. The config type is not supported by the current user configuration. ConfigType(defined) = {0}, ConfigType(used) = {1}.", this.configType, type);
				throw new InvalidOperationException(ServerStrings.ExConfigTypeNotMatched(this.configType.ToString(), type.ToString()));
			}
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00084093 File Offset: 0x00082293
		private static XmlWriter InternalGetXmlWriter(Stream stream)
		{
			return new XmlTextWriter(stream, UserConfiguration.Encoding);
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000840A0 File Offset: 0x000822A0
		private static XmlReader InternalGetXmlReader(Stream stream)
		{
			return SafeXmlFactory.CreateSafeXmlTextReader(stream);
		}

		// Token: 0x04001372 RID: 4978
		private const int IterationCount = 100;

		// Token: 0x04001373 RID: 4979
		private const int MaxStringLengthMapiLimit = 510;

		// Token: 0x04001374 RID: 4980
		private bool isDisposed;

		// Token: 0x04001375 RID: 4981
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04001376 RID: 4982
		private UserConfigurationTypes configType;

		// Token: 0x04001377 RID: 4983
		private UserConfigurationName configName;

		// Token: 0x04001378 RID: 4984
		private ConfigurationDictionary configDictionary;

		// Token: 0x04001379 RID: 4985
		private object dictionaryPropValue;

		// Token: 0x0400137A RID: 4986
		private bool isOpenMode = true;

		// Token: 0x0400137B RID: 4987
		private bool isXmlStreamCreated;

		// Token: 0x0400137C RID: 4988
		private bool isStreamCreated;

		// Token: 0x0400137D RID: 4989
		private StoreSession storeSession;

		// Token: 0x0400137E RID: 4990
		private ConfigurationItem item;

		// Token: 0x0400137F RID: 4991
		private StoreObjectId id;

		// Token: 0x04001380 RID: 4992
		private StoreObjectId folderId;

		// Token: 0x04001381 RID: 4993
		private readonly IStorePropertyBag queryPropertyBag;

		// Token: 0x04001382 RID: 4994
		private static PropertyDefinition[] fixedPrefetchProperties = new PropertyDefinition[]
		{
			InternalSchema.ItemId,
			InternalSchema.ItemClass,
			InternalSchema.UserConfigurationType,
			InternalSchema.LastModifiedTime
		};

		// Token: 0x04001383 RID: 4995
		private static readonly PropertyDefinition[] ZeroProperties = Array<PropertyDefinition>.Empty;

		// Token: 0x04001384 RID: 4996
		private static readonly Encoding Encoding = new UTF8Encoding(false);
	}
}
