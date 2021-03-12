using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A2C RID: 2604
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MigrationHelperBase
	{
		// Token: 0x17001A50 RID: 6736
		// (get) Token: 0x06005F99 RID: 24473 RVA: 0x00193893 File Offset: 0x00191A93
		public static bool TestIntegrationEnabled
		{
			get
			{
				return MigrationHelperBase.GetFlagValue("TestIntegrationEnabled", false, true);
			}
		}

		// Token: 0x06005F9A RID: 24474 RVA: 0x001938A4 File Offset: 0x00191AA4
		public static ExDateTime? GetExDateTimeProperty(IPropertyBag item, PropertyDefinition propertyDefinition)
		{
			object obj = item[propertyDefinition];
			ExDateTime? dateTimeValue = obj as ExDateTime?;
			if (dateTimeValue == null)
			{
				throw new InvalidDataException("Property error : " + propertyDefinition, null);
			}
			return MigrationHelperBase.GetValidExDateTime(dateTimeValue);
		}

		// Token: 0x06005F9B RID: 24475 RVA: 0x001938E8 File Offset: 0x00191AE8
		public static ExDateTime? GetValidExDateTime(ExDateTime? dateTimeValue)
		{
			if (dateTimeValue != null && dateTimeValue.Value == Util.Date1601Utc)
			{
				return null;
			}
			return dateTimeValue;
		}

		// Token: 0x06005F9C RID: 24476 RVA: 0x0019391C File Offset: 0x00191B1C
		public static void SetExDateTimeProperty(IPropertyBag item, PropertyDefinition propertyDefinition, ExDateTime? value)
		{
			if (value == null)
			{
				item[propertyDefinition] = Util.Date1601Utc;
				return;
			}
			item[propertyDefinition] = value;
		}

		// Token: 0x06005F9D RID: 24477 RVA: 0x00193946 File Offset: 0x00191B46
		public static StoreObjectId GetStoreObjectId(byte[] objectBytes)
		{
			return StoreObjectId.Parse(objectBytes, 0);
		}

		// Token: 0x06005F9E RID: 24478 RVA: 0x00193950 File Offset: 0x00191B50
		public static void DoAdCallAndTranslateExceptions(ADOperation call, bool expectObject, string debugContext)
		{
			Util.ThrowOnNullArgument(call, "call");
			try
			{
				ADNotificationAdapter.RunADOperation(call);
			}
			catch (DataValidationException innerException)
			{
				if (expectObject)
				{
					throw new ObjectNotFoundException(ServerStrings.ADUserNotFound, innerException);
				}
			}
			catch (DataSourceOperationException ex)
			{
				throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex, null, "MigrationHelperBase::{0}. Failed for [{1}], due to directory exception.", new object[]
				{
					ex,
					debugContext
				});
			}
			catch (DataSourceTransientException ex2)
			{
				throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex2, null, "MigrationHelperBase::{0}. Failed for [{1}], due to directory exception.", new object[]
				{
					ex2,
					debugContext
				});
			}
		}

		// Token: 0x06005F9F RID: 24479 RVA: 0x001939F4 File Offset: 0x00191BF4
		public static IRecipientSession CreateRecipientSession(TenantPartitionHint partitionHint)
		{
			ADSessionSettings sessionSettings;
			if (partitionHint != null)
			{
				sessionSettings = partitionHint.GetTenantScopedADSessionSettingsServiceOnly();
			}
			else
			{
				sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			}
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, sessionSettings, 189, "CreateRecipientSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Management\\Migration\\MigrationHelperBase.cs");
		}

		// Token: 0x06005FA0 RID: 24480 RVA: 0x00193A60 File Offset: 0x00191C60
		public static void GetManagementMailboxData(TenantPartitionHint tenantPartitionHint, out string mailboxLegacyDN, out string serverName)
		{
			IRecipientSession session = MigrationHelperBase.CreateRecipientSession(tenantPartitionHint);
			string connectionInformation = string.Format("Accessing management mailbox for organization: {0}", tenantPartitionHint);
			ADUser recipient = MigrationHelperBase.GetRecipient<ADUser>(session, MigrationHelperBase.ManagementMailboxFilter, (string msg) => MigrationHelperBase.CreatePermanentExceptionWithInternalData<MigrationMailboxNotFoundException>(connectionInformation), (string msg) => MigrationHelperBase.CreatePermanentExceptionWithInternalData<MultipleMigrationMailboxesFoundException>(connectionInformation), (string msg) => MigrationHelperBase.CreatePermanentExceptionWithInternalData<MigrationMailboxNotFoundException>(connectionInformation), connectionInformation);
			serverName = null;
			if (recipient.Database != null)
			{
				serverName = MigrationHelperBase.GetServerFqdn(recipient.Database.ObjectGuid);
			}
			mailboxLegacyDN = recipient.LegacyExchangeDN;
			if (serverName == null)
			{
				throw MigrationHelperBase.CreatePermanentExceptionWithInternalData<MigrationMailboxNotFoundException>(connectionInformation);
			}
		}

		// Token: 0x06005FA1 RID: 24481 RVA: 0x00193DE4 File Offset: 0x00191FE4
		public static IEnumerable<string> GetMigrationMailboxLegacyDN(TenantPartitionHint tenantPartitionHint, string localServer, QueryFilter optionalFilter = null)
		{
			IRecipientSession recipientSession = MigrationHelperBase.CreateRecipientSession(tenantPartitionHint);
			string connectionInformation = string.Format("Accessing migration mailboxes for organization: {0}", tenantPartitionHint);
			List<QueryFilter> filters = new List<QueryFilter>(3);
			filters.Add(OrganizationMailbox.OrganizationMailboxFilterBase);
			filters.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RawCapabilities, OrganizationCapability.Migration));
			if (optionalFilter != null)
			{
				filters.Add(optionalFilter);
			}
			IEnumerable<ADUser> users = MigrationHelperBase.GetRecipients<ADUser>(recipientSession, QueryFilter.AndTogether(filters.ToArray()), (string msg) => MigrationHelperBase.CreatePermanentExceptionWithInternalData<MigrationMailboxNotFoundException>(connectionInformation), (string msg) => MigrationHelperBase.CreatePermanentExceptionWithInternalData<MigrationMailboxNotFoundException>(connectionInformation), connectionInformation);
			foreach (ADUser user in users)
			{
				string serverName = null;
				if (user.Database != null)
				{
					serverName = MigrationHelperBase.GetServerFqdn(user.Database.ObjectGuid);
				}
				if (string.Equals(serverName, localServer, StringComparison.OrdinalIgnoreCase))
				{
					yield return user.LegacyExchangeDN;
				}
			}
			yield break;
		}

		// Token: 0x06005FA2 RID: 24482 RVA: 0x00193E10 File Offset: 0x00192010
		public static string GetParamValueStr(string valueName, string defaultValue)
		{
			if (!MigrationHelperBase.TestIntegrationEnabled)
			{
				return defaultValue;
			}
			string result;
			using (RegistryKey registryKey = MigrationHelperBase.OpenTestKey(false))
			{
				object value = registryKey.GetValue(valueName);
				if (value == null || !(value is string))
				{
					result = defaultValue;
				}
				else
				{
					result = (string)value;
				}
			}
			return result;
		}

		// Token: 0x06005FA3 RID: 24483 RVA: 0x00193E68 File Offset: 0x00192068
		public static bool GetFlagValue(string flagName, bool defaultValue, bool overrideTestIntegrationEnabled)
		{
			if (!overrideTestIntegrationEnabled && !MigrationHelperBase.TestIntegrationEnabled)
			{
				return defaultValue;
			}
			bool result;
			using (RegistryKey registryKey = MigrationHelperBase.OpenTestKey(false))
			{
				if (registryKey == null)
				{
					result = defaultValue;
				}
				else
				{
					object value = registryKey.GetValue(flagName);
					if (value == null || !(value is int))
					{
						result = defaultValue;
					}
					else
					{
						int num = (int)value;
						result = (num != 0);
					}
				}
			}
			return result;
		}

		// Token: 0x06005FA4 RID: 24484 RVA: 0x00193ED4 File Offset: 0x001920D4
		public static TMigrationPermanentException CreatePermanentExceptionWithInternalData<TMigrationPermanentException>(string internalInformation) where TMigrationPermanentException : MigrationPermanentException, new()
		{
			TMigrationPermanentException result = Activator.CreateInstance<TMigrationPermanentException>();
			result.InternalError = internalInformation;
			return result;
		}

		// Token: 0x06005FA5 RID: 24485 RVA: 0x00193EF8 File Offset: 0x001920F8
		public static T GetRecipient<T>(IRecipientSession session, QueryFilter filter, Func<string, LocalizedException> recipientNotFoundExceptionCallback, Func<string, LocalizedException> ambiguousRecipientExceptionCallback, Func<string, LocalizedException> invalidRecipientTypeExceptionCallback, string debugContext) where T : class
		{
			IEnumerable<T> recipients = MigrationHelperBase.GetRecipients<T>(session, filter, recipientNotFoundExceptionCallback, invalidRecipientTypeExceptionCallback, debugContext);
			T[] array = null;
			if (recipients != null)
			{
				array = recipients.ToArray<T>();
			}
			if (array.Length > 1)
			{
				string arg = string.Format("found multiple ({0}) {1} using filter {2} with scoped session for {3}", new object[]
				{
					array.Length,
					typeof(T),
					filter,
					debugContext
				});
				throw ambiguousRecipientExceptionCallback(arg);
			}
			return array[0];
		}

		// Token: 0x06005FA6 RID: 24486 RVA: 0x00193F68 File Offset: 0x00192168
		private static string GetServerFqdn(Guid databaseGuid)
		{
			DatabaseLocationInfo serverForDatabase = ActiveManager.GetCachingActiveManagerInstance().GetServerForDatabase(databaseGuid);
			if (serverForDatabase == null)
			{
				throw new MigrationMailboxDatabaseInfoNotAvailableException(databaseGuid.ToString());
			}
			return serverForDatabase.ServerFqdn;
		}

		// Token: 0x06005FA7 RID: 24487 RVA: 0x001942A8 File Offset: 0x001924A8
		private static IEnumerable<T> GetRecipients<T>(IRecipientSession session, QueryFilter filter, Func<string, LocalizedException> recipientNotFoundExceptionCallback, Func<string, LocalizedException> invalidRecipientTypeExceptionCallback, string debugContext) where T : class
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(filter, "filter");
			ADRecipient[] recipients = null;
			MigrationHelperBase.DoAdCallAndTranslateExceptions(delegate
			{
				recipients = session.Find(null, QueryScope.SubTree, filter, null, 0);
			}, true, debugContext);
			if (recipients == null || recipients.Length < 1)
			{
				string arg = string.Format("Could not find {0} using filter {1} with scoped session for {2}", typeof(T), filter, debugContext);
				throw recipientNotFoundExceptionCallback(arg);
			}
			foreach (ADRecipient recip in recipients)
			{
				T recipient = recip as T;
				if (recipient == null)
				{
					string arg2 = string.Format("Found an AD recipient that's not a {0} using filter {1} with scoped session for {2}", typeof(T), filter, debugContext);
					throw invalidRecipientTypeExceptionCallback(arg2);
				}
				yield return recipient;
			}
			yield break;
		}

		// Token: 0x06005FA8 RID: 24488 RVA: 0x001942E4 File Offset: 0x001924E4
		private static RegistryKey OpenTestKey(bool writable)
		{
			RegistryKey result = null;
			try
			{
				result = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Exchange_Test\\v15\\Migration", writable);
			}
			catch (SecurityException)
			{
			}
			catch (ArgumentException)
			{
			}
			return result;
		}

		// Token: 0x0400362D RID: 13869
		public const string RegKeyName = "SOFTWARE\\Microsoft\\Exchange_Test\\v15\\Migration";

		// Token: 0x0400362E RID: 13870
		public const string TestIntegrationEnabledName = "TestIntegrationEnabled";

		// Token: 0x0400362F RID: 13871
		public static readonly QueryFilter ManagementMailboxFilter = new AndFilter(new QueryFilter[]
		{
			OrganizationMailbox.OrganizationMailboxFilterBase,
			new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RawCapabilities, OrganizationCapability.Management)
		});
	}
}
