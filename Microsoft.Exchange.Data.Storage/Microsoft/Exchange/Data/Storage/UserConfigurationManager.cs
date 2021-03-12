using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Storage.Configuration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002B3 RID: 691
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UserConfigurationManager : IUserConfigurationManager
	{
		// Token: 0x06001CBE RID: 7358 RVA: 0x000848C0 File Offset: 0x00082AC0
		public UserConfigurationManager(MailboxSession mailboxSession)
		{
			this.mailboxSession = mailboxSession;
			this.userConfigurationCache = new UserConfigurationCache(mailboxSession);
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x000848E7 File Offset: 0x00082AE7
		public static bool IsValidName(string configurationName)
		{
			if (configurationName == null)
			{
				throw new ArgumentNullException("configurationName");
			}
			return UserConfigurationName.IsValidName(configurationName, ConfigurationNameKind.Name);
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0008491C File Offset: 0x00082B1C
		public IReadableUserConfiguration GetReadOnlyMailboxConfiguration(string configName, UserConfigurationTypes freefetchDataTypes)
		{
			IReadableUserConfiguration result = null;
			if (!this.TryGetAggregatedConfiguration(() => UserConfigurationDescriptor.CreateMailboxDescriptor(configName, freefetchDataTypes), out result))
			{
				result = this.GetMailboxConfiguration(configName, freefetchDataTypes);
			}
			return result;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x00084990 File Offset: 0x00082B90
		public IReadableUserConfiguration GetReadOnlyFolderConfiguration(string configName, UserConfigurationTypes freefetchDataTypes, StoreId folderId)
		{
			IReadableUserConfiguration result = null;
			if (!this.TryGetAggregatedConfiguration(() => UserConfigurationDescriptor.CreateFolderDescriptor(configName, freefetchDataTypes, StoreId.GetStoreObjectId(folderId)), out result))
			{
				result = this.GetFolderConfiguration(configName, freefetchDataTypes, folderId);
			}
			return result;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x000849EA File Offset: 0x00082BEA
		IUserConfiguration IUserConfigurationManager.GetMailboxConfiguration(string configName, UserConfigurationTypes freefetchDataTypes)
		{
			return this.GetMailboxConfiguration(configName, freefetchDataTypes);
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x000849F4 File Offset: 0x00082BF4
		IUserConfiguration IUserConfigurationManager.GetFolderConfiguration(string configName, UserConfigurationTypes freefetchDataTypes, StoreId folderId)
		{
			return this.GetFolderConfiguration(configName, freefetchDataTypes, folderId);
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x000849FF File Offset: 0x00082BFF
		IUserConfiguration IUserConfigurationManager.CreateMailboxConfiguration(string configurationName, UserConfigurationTypes dataTypes)
		{
			return this.CreateMailboxConfiguration(configurationName, dataTypes);
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00084A09 File Offset: 0x00082C09
		IUserConfiguration IUserConfigurationManager.CreateFolderConfiguration(string configurationName, UserConfigurationTypes dataTypes, StoreId folderId)
		{
			return this.CreateFolderConfiguration(configurationName, dataTypes, folderId);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x00084A14 File Offset: 0x00082C14
		OperationResult IUserConfigurationManager.DeleteMailboxConfigurations(params string[] configurationNames)
		{
			return this.DeleteMailboxConfigurations(configurationNames);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x00084A1D File Offset: 0x00082C1D
		OperationResult IUserConfigurationManager.DeleteFolderConfigurations(StoreId folderId, params string[] configurationNames)
		{
			return this.DeleteFolderConfigurations(folderId, configurationNames);
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x00084A27 File Offset: 0x00082C27
		IList<IStorePropertyBag> IUserConfigurationManager.FetchAllConfigurations(IFolder folder, SortBy[] sorts, int maxRow, params PropertyDefinition[] columns)
		{
			return UserConfiguration.FetchAllConfigurations(folder, sorts, maxRow, columns);
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x00084A33 File Offset: 0x00082C33
		IMailboxSession IUserConfigurationManager.MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x00084A3B File Offset: 0x00082C3B
		public UserConfiguration GetMailboxConfiguration(string configName, UserConfigurationTypes freefetchDataTypes)
		{
			EnumValidator.ThrowIfInvalid<UserConfigurationTypes>(freefetchDataTypes, "freefetchDataTypes");
			return this.InternalGetUserConfiguration(this.GetDefaultFolderId(DefaultFolderType.Configuration), new UserConfigurationName(configName, ConfigurationNameKind.Name), freefetchDataTypes);
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x00084A5E File Offset: 0x00082C5E
		public UserConfiguration GetFolderConfiguration(string configName, UserConfigurationTypes freefetchDataTypes, StoreId folderId)
		{
			EnumValidator.ThrowIfInvalid<UserConfigurationTypes>(freefetchDataTypes, "freefetchDataTypes");
			return this.InternalGetUserConfiguration(folderId, new UserConfigurationName(configName, ConfigurationNameKind.Name), freefetchDataTypes);
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x00084A7A File Offset: 0x00082C7A
		public UserConfiguration GetFolderConfiguration(string configName, UserConfigurationTypes freefetchDataTypes, StoreId folderId, StoreObjectId messageId)
		{
			EnumValidator.ThrowIfInvalid<UserConfigurationTypes>(freefetchDataTypes, "freefetchDataTypes");
			return this.InternalGetUserConfiguration(folderId, new UserConfigurationName(configName, ConfigurationNameKind.Name), freefetchDataTypes, messageId);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00084A98 File Offset: 0x00082C98
		public OperationResult DeleteMailboxConfigurations(params string[] configurationNames)
		{
			if (configurationNames == null || configurationNames.Length == 0)
			{
				return OperationResult.Succeeded;
			}
			return this.DeleteFolderConfigurations(this.GetDefaultFolderId(DefaultFolderType.Configuration), configurationNames);
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x00084AB4 File Offset: 0x00082CB4
		public OperationResult DeleteFolderConfigurations(StoreId folderId, params string[] configurationNames)
		{
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			if (configurationNames == null || configurationNames.Length == 0)
			{
				return OperationResult.Succeeded;
			}
			StringBuilder stringBuilder = new StringBuilder();
			using (Folder folder = Folder.Bind(this.mailboxSession, folderId))
			{
				for (int i = 0; i < configurationNames.Length; i++)
				{
					try
					{
						this.InternalDeleteUserConfiguration(folder, configurationNames[i]);
					}
					catch (InvalidOperationException arg)
					{
						stringBuilder.Append(string.Format("{0}: Failed to delete user configuration {1}. Exception = {2}\n", i, configurationNames[i], arg));
					}
				}
			}
			if (stringBuilder.Length != 0)
			{
				ExTraceGlobals.StorageTracer.TraceError<StringBuilder>((long)this.GetHashCode(), "UserConfigurationManager::DeleteFolderConfigurations. Operation failed due to exception(s). Exception(s) = {0}.", stringBuilder);
				return OperationResult.PartiallySucceeded;
			}
			return OperationResult.Succeeded;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x00084B70 File Offset: 0x00082D70
		public ICollection<UserConfiguration> FindMailboxConfigurations(string searchString, UserConfigurationSearchFlags searchFlags)
		{
			ICollection<UserConfiguration> result;
			using (Folder folder = Folder.Bind(this.mailboxSession, this.GetDefaultFolderId(DefaultFolderType.Configuration)))
			{
				result = this.InternalFindUserConfigurations(folder, searchString, searchFlags);
			}
			return result;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x00084BB8 File Offset: 0x00082DB8
		public ICollection<UserConfiguration> FindFolderConfigurations(string searchString, UserConfigurationSearchFlags searchFlags, StoreId folderId)
		{
			ICollection<UserConfiguration> result;
			using (Folder folder = Folder.Bind(this.mailboxSession, folderId))
			{
				result = this.InternalFindUserConfigurations(folder, searchString, searchFlags);
			}
			return result;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x00084BFC File Offset: 0x00082DFC
		public UserConfiguration CreateMailboxConfiguration(string configurationName, UserConfigurationTypes dataTypes)
		{
			EnumValidator.ThrowIfInvalid<UserConfigurationTypes>(dataTypes, "dataTypes");
			UserConfiguration result;
			using (Folder folder = Folder.Bind(this.mailboxSession, this.GetDefaultFolderId(DefaultFolderType.Configuration)))
			{
				result = this.InternalCreateUserConfiguration(folder, new UserConfigurationName(configurationName, ConfigurationNameKind.Name), dataTypes);
			}
			return result;
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x00084C58 File Offset: 0x00082E58
		public UserConfiguration CreateFolderConfiguration(string configurationName, UserConfigurationTypes dataTypes, StoreId folderId)
		{
			EnumValidator.ThrowIfInvalid<UserConfigurationTypes>(dataTypes, "dataTypes");
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			UserConfiguration result;
			using (Folder folder = Folder.Bind(this.mailboxSession, folderId))
			{
				result = this.InternalCreateUserConfiguration(folder, new UserConfigurationName(configurationName, ConfigurationNameKind.Name), dataTypes);
			}
			return result;
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x00084CB8 File Offset: 0x00082EB8
		public UserConfigurationManager.IAggregationContext AttachAggregator(AggregatedUserConfigurationDescriptor aggregatorDescription)
		{
			UserConfigurationManager.AggregationContext aggregationContext = null;
			if (ConfigurationItemSchema.IsEnabledForConfigurationAggregation(this.mailboxSession.MailboxOwner))
			{
				IAggregatedUserConfigurationReader reader = AggregatedUserConfiguration.GetReader(aggregatorDescription, this);
				aggregationContext = new UserConfigurationManager.AggregationContext(this, reader);
				lock (this.aggregators)
				{
					this.aggregators.Add(aggregationContext);
				}
			}
			return aggregationContext;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x00084D24 File Offset: 0x00082F24
		public UserConfigurationManager.IAggregationContext AttachAggregator(UserConfigurationManager.IAggregationContext ictx)
		{
			UserConfigurationManager.AggregationContext aggregationContext = null;
			if (ConfigurationItemSchema.IsEnabledForConfigurationAggregation(this.mailboxSession.MailboxOwner))
			{
				UserConfigurationManager.AggregationContext aggregationContext2 = ictx as UserConfigurationManager.AggregationContext;
				if (aggregationContext2 == null)
				{
					throw new ArgumentException("The shared context must be non-null and have been created by a UserConfigurationManager");
				}
				aggregationContext = aggregationContext2.Clone(this);
				lock (this.aggregators)
				{
					this.aggregators.Add(aggregationContext);
				}
			}
			return aggregationContext;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x00084D9C File Offset: 0x00082F9C
		private StoreId GetDefaultFolderId(DefaultFolderType defaultFolderType)
		{
			StoreId defaultFolderId = this.mailboxSession.GetDefaultFolderId(defaultFolderType);
			if (defaultFolderId == null)
			{
				throw new AccessDeniedException(ServerStrings.NotEnoughPermissionsToPerformOperation);
			}
			return defaultFolderId;
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x00084DC8 File Offset: 0x00082FC8
		private ICollection<UserConfiguration> InternalFindUserConfigurations(Folder folder, string searchString, UserConfigurationSearchFlags searchFlags)
		{
			EnumValidator.ThrowIfInvalid<UserConfigurationSearchFlags>(searchFlags, "searchFlags");
			if (searchString == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "Folder::FindUserConfigurations. Argument {0} is Null.", "searchString");
				throw new ArgumentNullException(ServerStrings.ExNullParameter("searchString", 1));
			}
			return UserConfiguration.Find(folder, searchString, searchFlags);
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x00084E1C File Offset: 0x0008301C
		private void InternalDeleteUserConfiguration(Folder folder, string configurationName)
		{
			ExTraceGlobals.StorageTracer.Information<string>((long)this.GetHashCode(), "Folder::DeleteUserConfiguration. configurationName = {0}.", (configurationName == null) ? "<Null>" : configurationName);
			if (configurationName == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "Folder::DeleteUserConfiguration. Argument {0} is Null.", "configurationName");
				throw new ArgumentNullException(ServerStrings.ExNullParameter("configurationName", 1));
			}
			if (configurationName.Length == 0)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "Folder::DeleteUserConfiguration. Argument {0} is Empty.", "configurationName");
				throw new ArgumentException(ServerStrings.ExInvalidParameter("configurationName", 1));
			}
			UserConfiguration.Delete(folder, configurationName, UserConfigurationSearchFlags.FullString);
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x00084EBF File Offset: 0x000830BF
		private UserConfiguration InternalGetUserConfiguration(StoreId folderId, UserConfigurationName configurationName, UserConfigurationTypes freefetchDataType)
		{
			return this.InternalGetUserConfiguration(folderId, configurationName, freefetchDataType, null);
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x00084ECC File Offset: 0x000830CC
		private UserConfiguration InternalGetUserConfiguration(StoreId folderId, UserConfigurationName configurationName, UserConfigurationTypes freefetchDataType, StoreObjectId messageId)
		{
			if (this.aggregators.Count > 0)
			{
				lock (this.aggregators)
				{
					foreach (UserConfigurationManager.AggregationContext aggregationContext in this.aggregators)
					{
						ExTraceGlobals.StorageTracer.TraceWarning<UserConfigurationName>((long)this.GetHashCode(), "UserConfigurationManager::InternalGetUserConfiguration cache miss = {0}.", configurationName);
						aggregationContext.FaiCacheMiss();
					}
				}
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			UserConfiguration userConfiguration = null;
			bool flag2 = false;
			UserConfiguration result;
			try
			{
				userConfiguration = this.userConfigurationCache.Get(configurationName, StoreId.GetStoreObjectId(folderId));
				if (userConfiguration == null)
				{
					if (messageId != null)
					{
						userConfiguration = this.GetMessageConfiguration(configurationName, freefetchDataType, messageId);
					}
					if (userConfiguration == null)
					{
						ExTraceGlobals.UserConfigurationTracer.TraceDebug<string>((long)this.GetHashCode(), "UserConfigurationManager::InternalBindAndGetUserConfiguration. Miss the cache. ConfigName = {0}.", configurationName.Name);
						userConfiguration = this.InternalBindAndGetUserConfiguration(folderId, configurationName, freefetchDataType);
					}
				}
				if ((userConfiguration.DataTypes & freefetchDataType) == (UserConfigurationTypes)0)
				{
					ExTraceGlobals.StorageTracer.TraceError(0L, "The configuration data's field has been corrupted. Field = UserConfigurationType.");
					throw new CorruptDataException(ServerStrings.ExConfigDataCorrupted("UserConfigurationType"));
				}
				flag2 = true;
				result = userConfiguration;
			}
			finally
			{
				if (!flag2 && userConfiguration != null)
				{
					userConfiguration.Dispose();
					userConfiguration = null;
				}
			}
			return result;
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x00085020 File Offset: 0x00083220
		private UserConfiguration InternalCreateUserConfiguration(Folder folder, UserConfigurationName configurationName, UserConfigurationTypes dataTypes)
		{
			ExTraceGlobals.StorageTracer.Information<UserConfigurationName, UserConfigurationTypes>((long)this.GetHashCode(), "UserConfigurationManager::InternalCreateUserConfiguration. configurationName = {0}, dataTypes = {1}.", configurationName, dataTypes);
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			if (!EnumValidator.IsValidValue<UserConfigurationTypes>(dataTypes))
			{
				ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), "UserConfigurationManager::InternalCreateUserConfiguration. dataTypes is invalid.");
				throw new ArgumentException("dataTypes");
			}
			return UserConfiguration.Create(folder, configurationName, dataTypes);
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x00085084 File Offset: 0x00083284
		private UserConfiguration GetMessageConfiguration(UserConfigurationName configurationName, UserConfigurationTypes freefetchDataTypes, StoreObjectId messageId)
		{
			UserConfiguration result = null;
			ConfigurationItem configurationItem = null;
			EnumValidator.ThrowIfInvalid<UserConfigurationTypes>(freefetchDataTypes, "freefetchDataTypes");
			try
			{
				configurationItem = ConfigurationItem.Bind(this.mailboxSession, messageId);
				result = new UserConfiguration(configurationItem, (StoreObjectId)configurationItem.TryGetProperty(StoreObjectSchema.ParentItemId), configurationName, freefetchDataTypes, true);
			}
			catch (ObjectNotFoundException arg)
			{
				result = null;
				if (configurationItem != null)
				{
					configurationItem.Dispose();
				}
				ExTraceGlobals.StorageTracer.TraceError<ObjectNotFoundException>(0L, "UserConfigurationManager::GetMessageConfiguration. Message object not found. Exception = {0}.", arg);
			}
			catch (Exception arg2)
			{
				result = null;
				if (configurationItem != null)
				{
					configurationItem.Dispose();
				}
				ExTraceGlobals.StorageTracer.TraceError<Exception>(0L, "UserConfigurationManager::GetMessageConfiguration. Unable to create user configuration. Exception = {0}.", arg2);
			}
			return result;
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x00085128 File Offset: 0x00083328
		private UserConfiguration InternalBindAndGetUserConfiguration(StoreId folderId, UserConfigurationName configurationName, UserConfigurationTypes freefetchDataType)
		{
			UserConfiguration result = null;
			using (Folder folder = Folder.Bind(this.mailboxSession, folderId))
			{
				if (folder.IsNew)
				{
					ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "UserConfigurationManager::InternalGetUserConfiguration. The folder Id is null maybe it is because the folder has not been saved yet. Id = {0}.", "null");
					throw new InvalidOperationException(ServerStrings.ExFolderWithoutMapiProp);
				}
				if (!EnumValidator.IsValidValue<UserConfigurationTypes>(freefetchDataType))
				{
					ExTraceGlobals.StorageTracer.TraceError<UserConfigurationTypes>((long)this.GetHashCode(), "UserConfigurationManager::InternalGetUserConfiguration. freefetchDataType is not allowed. freefetchDataType = {0}.", freefetchDataType);
					throw new ArgumentException("freefetchDataType");
				}
				ExTraceGlobals.UserConfigurationTracer.TraceDebug<UserConfigurationName>((long)this.GetHashCode(), "UserConfigurationManager::InternalGetUserConfiguration. Hit code GetIgnoringCache. ConfigName = {0}.", configurationName);
				result = UserConfiguration.GetIgnoringCache(this, folder, configurationName, freefetchDataType);
			}
			return result;
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x000851E0 File Offset: 0x000833E0
		internal UserConfigurationCache UserConfigurationCache
		{
			get
			{
				return this.userConfigurationCache;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x000851E8 File Offset: 0x000833E8
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x000851F0 File Offset: 0x000833F0
		private void RemoveAggregator(UserConfigurationManager.AggregationContext reader)
		{
			lock (this.aggregators)
			{
				this.aggregators.Remove(reader);
			}
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x00085238 File Offset: 0x00083438
		private bool TryGetAggregatedConfiguration(Func<UserConfigurationDescriptor> descriptorFactory, out IReadableUserConfiguration config)
		{
			config = null;
			if (this.aggregators.Count > 0)
			{
				lock (this.aggregators)
				{
					UserConfigurationDescriptor descriptor = descriptorFactory();
					foreach (UserConfigurationManager.AggregationContext aggregationContext in this.aggregators)
					{
						config = aggregationContext.Read(this.mailboxSession, descriptor);
						if (config != null)
						{
							break;
						}
					}
				}
			}
			return null != config;
		}

		// Token: 0x04001392 RID: 5010
		private UserConfigurationCache userConfigurationCache;

		// Token: 0x04001393 RID: 5011
		private MailboxSession mailboxSession;

		// Token: 0x04001394 RID: 5012
		private List<UserConfigurationManager.AggregationContext> aggregators = new List<UserConfigurationManager.AggregationContext>(8);

		// Token: 0x020002B4 RID: 692
		public interface IAggregationContext : IDisposable
		{
			// Token: 0x170008EA RID: 2282
			// (get) Token: 0x06001CE1 RID: 7393
			int FaiCacheHits { get; }

			// Token: 0x170008EB RID: 2283
			// (get) Token: 0x06001CE2 RID: 7394
			int FaiCacheMisses { get; }

			// Token: 0x170008EC RID: 2284
			// (get) Token: 0x06001CE3 RID: 7395
			int TypeCacheHits { get; }

			// Token: 0x170008ED RID: 2285
			// (get) Token: 0x06001CE4 RID: 7396
			int TypeCacheMisses { get; }

			// Token: 0x06001CE5 RID: 7397
			void Validate(IMailboxSession session, Action<IEnumerable<UserConfigurationDescriptor.MementoClass>, IEnumerable<string>> callback);

			// Token: 0x06001CE6 RID: 7398
			T ReadType<T>(string key, Func<T> factory) where T : SerializableDataBase;

			// Token: 0x06001CE7 RID: 7399
			void Detach();
		}

		// Token: 0x020002B5 RID: 693
		private class AggregationStats
		{
			// Token: 0x04001395 RID: 5013
			public int FaiCacheHits;

			// Token: 0x04001396 RID: 5014
			public int FaiCacheMisses;

			// Token: 0x04001397 RID: 5015
			public int TypeCacheHits;

			// Token: 0x04001398 RID: 5016
			public int TypeCacheMisses;
		}

		// Token: 0x020002B6 RID: 694
		private class AggregationContext : DisposableObject, UserConfigurationManager.IAggregationContext, IDisposable, IAggregationReValidator
		{
			// Token: 0x06001CE9 RID: 7401 RVA: 0x000852EC File Offset: 0x000834EC
			public AggregationContext(UserConfigurationManager manager, IAggregatedUserConfigurationReader reader) : this(manager, reader, new UserConfigurationManager.AggregationStats(), new ConcurrentDictionary<string, Func<SerializableDataBase>>())
			{
			}

			// Token: 0x06001CEA RID: 7402 RVA: 0x00085300 File Offset: 0x00083500
			private AggregationContext(UserConfigurationManager manager, IAggregatedUserConfigurationReader reader, UserConfigurationManager.AggregationStats stats, ConcurrentDictionary<string, Func<SerializableDataBase>> requestedTypes)
			{
				this.manager = manager;
				this.reader = reader;
				this.stats = stats;
				this.requestedTypes = requestedTypes;
				this.createdTypes = new ConcurrentDictionary<string, SerializableDataBase>();
			}

			// Token: 0x170008EE RID: 2286
			// (get) Token: 0x06001CEB RID: 7403 RVA: 0x00085330 File Offset: 0x00083530
			public int FaiCacheHits
			{
				get
				{
					return this.stats.FaiCacheHits;
				}
			}

			// Token: 0x170008EF RID: 2287
			// (get) Token: 0x06001CEC RID: 7404 RVA: 0x0008533D File Offset: 0x0008353D
			public int FaiCacheMisses
			{
				get
				{
					return this.stats.FaiCacheMisses;
				}
			}

			// Token: 0x170008F0 RID: 2288
			// (get) Token: 0x06001CED RID: 7405 RVA: 0x0008534A File Offset: 0x0008354A
			public int TypeCacheHits
			{
				get
				{
					return this.stats.TypeCacheHits;
				}
			}

			// Token: 0x170008F1 RID: 2289
			// (get) Token: 0x06001CEE RID: 7406 RVA: 0x00085357 File Offset: 0x00083557
			public int TypeCacheMisses
			{
				get
				{
					return this.stats.TypeCacheMisses;
				}
			}

			// Token: 0x06001CEF RID: 7407 RVA: 0x00085364 File Offset: 0x00083564
			public void FaiCacheHit()
			{
				Interlocked.Increment(ref this.stats.FaiCacheHits);
			}

			// Token: 0x06001CF0 RID: 7408 RVA: 0x00085377 File Offset: 0x00083577
			public void FaiCacheMiss()
			{
				Interlocked.Increment(ref this.stats.FaiCacheMisses);
			}

			// Token: 0x06001CF1 RID: 7409 RVA: 0x0008538A File Offset: 0x0008358A
			public void TypeCacheHit()
			{
				Interlocked.Increment(ref this.stats.TypeCacheHits);
			}

			// Token: 0x06001CF2 RID: 7410 RVA: 0x0008539D File Offset: 0x0008359D
			public void TypeCacheMiss()
			{
				Interlocked.Increment(ref this.stats.TypeCacheMisses);
			}

			// Token: 0x06001CF3 RID: 7411 RVA: 0x000853B0 File Offset: 0x000835B0
			public void Validate(IMailboxSession session, Action<IEnumerable<UserConfigurationDescriptor.MementoClass>, IEnumerable<string>> callback)
			{
				this.reader.Validate(session.UserConfigurationManager, XSOFactory.Default, this, callback);
			}

			// Token: 0x06001CF4 RID: 7412 RVA: 0x000853CA File Offset: 0x000835CA
			public void Detach()
			{
				this.manager.RemoveAggregator(this);
			}

			// Token: 0x06001CF5 RID: 7413 RVA: 0x000853D8 File Offset: 0x000835D8
			public bool IsTypeReValidationRequired()
			{
				return this.requestedTypes.Count > 0;
			}

			// Token: 0x06001CF6 RID: 7414 RVA: 0x000853E8 File Offset: 0x000835E8
			public IEnumerable<KeyValuePair<string, SerializableDataBase>> RevalidatedTypes()
			{
				List<KeyValuePair<string, SerializableDataBase>> list = new List<KeyValuePair<string, SerializableDataBase>>(this.requestedTypes.Count);
				foreach (KeyValuePair<string, Func<SerializableDataBase>> keyValuePair in this.requestedTypes)
				{
					list.Add(new KeyValuePair<string, SerializableDataBase>(keyValuePair.Key, keyValuePair.Value()));
				}
				return list;
			}

			// Token: 0x06001CF7 RID: 7415 RVA: 0x00085460 File Offset: 0x00083660
			public T ReadType<T>(string key, Func<T> factory) where T : SerializableDataBase
			{
				this.requestedTypes[key] = factory;
				T t = default(T);
				if (this.reader.TryRead<T>(key, out t))
				{
					this.TypeCacheHit();
				}
				else
				{
					SerializableDataBase serializableDataBase = null;
					if (this.createdTypes.TryGetValue(key, out serializableDataBase))
					{
						t = (serializableDataBase as T);
						this.TypeCacheHit();
					}
					if (t == null)
					{
						t = factory();
						this.createdTypes[key] = t;
						this.TypeCacheMiss();
					}
				}
				return t;
			}

			// Token: 0x06001CF8 RID: 7416 RVA: 0x000854E8 File Offset: 0x000836E8
			public IReadableUserConfiguration Read(IMailboxSession session, UserConfigurationDescriptor descriptor)
			{
				IReadableUserConfiguration readableUserConfiguration = this.reader.Read(session, descriptor);
				if (readableUserConfiguration != null)
				{
					this.FaiCacheHit();
				}
				return readableUserConfiguration;
			}

			// Token: 0x06001CF9 RID: 7417 RVA: 0x0008550D File Offset: 0x0008370D
			public UserConfigurationManager.AggregationContext Clone(UserConfigurationManager manager)
			{
				return new UserConfigurationManager.AggregationContext(manager, this.reader, this.stats, this.requestedTypes);
			}

			// Token: 0x06001CFA RID: 7418 RVA: 0x00085527 File Offset: 0x00083727
			protected override DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<UserConfigurationManager.AggregationContext>(this);
			}

			// Token: 0x06001CFB RID: 7419 RVA: 0x0008552F File Offset: 0x0008372F
			protected override void InternalDispose(bool disposing)
			{
				base.InternalDispose(disposing);
				if (disposing)
				{
					this.Detach();
				}
			}

			// Token: 0x04001399 RID: 5017
			private readonly UserConfigurationManager manager;

			// Token: 0x0400139A RID: 5018
			private readonly IAggregatedUserConfigurationReader reader;

			// Token: 0x0400139B RID: 5019
			private readonly UserConfigurationManager.AggregationStats stats;

			// Token: 0x0400139C RID: 5020
			private readonly ConcurrentDictionary<string, Func<SerializableDataBase>> requestedTypes;

			// Token: 0x0400139D RID: 5021
			private readonly ConcurrentDictionary<string, SerializableDataBase> createdTypes;
		}
	}
}
