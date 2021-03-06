using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000DD RID: 221
	internal class NspiMigrationDataReader
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x00032116 File Offset: 0x00030316
		public NspiMigrationDataReader(ExchangeOutlookAnywhereEndpoint connectionSettings, MigrationJob migrationJob)
		{
			MigrationUtil.ThrowOnNullArgument(connectionSettings, "connectionSettings");
			this.connectionSettings = connectionSettings;
			this.reportData = ((migrationJob != null) ? migrationJob.ReportData : null);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00032144 File Offset: 0x00030344
		public static ExchangeMigrationRecipient TryCreateRecipient(PropRow row, long[] properties, bool isPAW = false)
		{
			MigrationUtil.ThrowOnNullArgument(row, "row");
			MigrationUtil.ThrowOnNullArgument(properties, "properties");
			if (row.Properties.Count != properties.Length)
			{
				throw new ArgumentOutOfRangeException("row", "row.Properties.Count != properties.Length");
			}
			MigrationUserRecipientType recipientType;
			uint num;
			NspiMigrationDataReader.TryGetRecipientType(row, out recipientType, out num);
			ExchangeMigrationRecipient exchangeMigrationRecipient;
			if (!ExchangeMigrationRecipient.TryCreate(recipientType, out exchangeMigrationRecipient, isPAW))
			{
				return null;
			}
			switch (num)
			{
			case 7U:
				exchangeMigrationRecipient.SetPropertyValue(PropTag.DisplayTypeEx, ExchangeResourceType.Room.ToString());
				break;
			case 8U:
				exchangeMigrationRecipient.SetPropertyValue(PropTag.DisplayTypeEx, ExchangeResourceType.Equipment.ToString());
				break;
			}
			for (int i = 0; i < properties.Length; i++)
			{
				PropTag propTag = (PropTag)properties[i];
				PropValue val = row.Properties[i];
				if (propTag != PropTag.DisplayType && propTag != PropTag.DisplayTypeEx && exchangeMigrationRecipient.IsPropertySupported(propTag))
				{
					int num2 = 0;
					PropType propType = val.PropType;
					bool flag;
					object obj;
					if (propType <= PropType.String)
					{
						if (propType != PropType.Int)
						{
							if (propType != PropType.String)
							{
								goto IL_154;
							}
							string text;
							flag = NspiMigrationDataReader.TryParseString(val, out text, out num2);
							obj = text;
						}
						else
						{
							int num3;
							flag = NspiMigrationDataReader.TryParseInt(val, out num3, out num2);
							obj = num3;
						}
					}
					else if (propType != PropType.Binary)
					{
						if (propType != PropType.StringArray)
						{
							goto IL_154;
						}
						string[] array;
						flag = NspiMigrationDataReader.TryParseStringArray(val, out array, out num2);
						obj = array;
					}
					else
					{
						byte[] array2;
						flag = NspiMigrationDataReader.TryParseBytes(val, out array2, out num2);
						obj = array2;
					}
					IL_15A:
					if (!flag)
					{
						MigrationLogger.Log(MigrationEventType.Error, "NspiMigrationDataReader.TryCreateRecipient. error code {0} (0 for Null-value) for PropTag {1}", new object[]
						{
							num2,
							propTag
						});
						goto IL_199;
					}
					if (obj != null)
					{
						exchangeMigrationRecipient.SetPropertyValue(propTag, obj);
						goto IL_199;
					}
					goto IL_199;
					IL_154:
					obj = null;
					flag = true;
					goto IL_15A;
				}
				IL_199:;
			}
			return exchangeMigrationRecipient;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000322F8 File Offset: 0x000304F8
		public static bool TryGetRecipientType(PropRow row, out MigrationUserRecipientType type, out uint displayTypeEx)
		{
			MigrationUtil.ThrowOnNullArgument(row, "row");
			type = MigrationUserRecipientType.Unsupported;
			displayTypeEx = 0U;
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			for (int i = 0; i < row.Properties.Count; i++)
			{
				if (row.Properties[i].PropTag.Id() == PropTag.DisplayType.Id())
				{
					num = i;
				}
				else if (row.Properties[i].PropTag.Id() == PropTag.DisplayTypeEx.Id())
				{
					num2 = i;
				}
				else if (row.Properties[i].PropTag.Id() == ((PropTag)2147876895U).Id())
				{
					num3 = i;
				}
				if (num != -1 && num2 != -1 && num3 != -1)
				{
					break;
				}
			}
			if (num == -1)
			{
				MigrationLogger.Log(MigrationEventType.Error, "Failed to parse DisplayType - didn't find PropTag.DisplayType.", new object[0]);
				return false;
			}
			int num4;
			if (!NspiMigrationDataReader.TryParseRecipientType(row.Properties[num], out type, out num4))
			{
				MigrationLogger.Log(MigrationEventType.Error, "Failed to parse DisplayType - error code = {0} (0 for null).", new object[]
				{
					num4
				});
				return false;
			}
			if (type != MigrationUserRecipientType.Mailbox)
			{
				return true;
			}
			bool flag = false;
			if (num2 == -1)
			{
				MigrationLogger.Log(MigrationEventType.Error, "Failed to parse DisplayTypeEx - didn't find PropTag.DisplayTypeEx.", new object[0]);
				return false;
			}
			if (!NspiMigrationDataReader.TryParseDisplayTypeEx(row.Properties[num2], out displayTypeEx, out num4))
			{
				if (num4 != -2147221233)
				{
					MigrationLogger.Log(MigrationEventType.Error, "Failed to parse DisplayTypeEx - error code = {0} (0 for null).", new object[]
					{
						num4
					});
					return false;
				}
				flag = true;
			}
			if (flag)
			{
				if (num3 == -1)
				{
					MigrationLogger.Log(MigrationEventType.Error, "Failed to parse HomeMDB - didn't find PropTagHomeMDB.", new object[0]);
					return false;
				}
				string value;
				if (!NspiMigrationDataReader.TryParseString(row.Properties[num3], out value, out num4) && num4 != -2147221233)
				{
					MigrationLogger.Log(MigrationEventType.Error, "Failed to parse HomeMDB - error code = {0} (0 for null).", new object[]
					{
						num4
					});
					return false;
				}
				displayTypeEx = (string.IsNullOrEmpty(value) ? 6U : 0U);
			}
			if (displayTypeEx == 6U)
			{
				type = MigrationUserRecipientType.Mailuser;
				displayTypeEx = 0U;
			}
			return true;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000324F0 File Offset: 0x000306F0
		public static bool TryGetSmtpAddress(PropRow row, out string smtpAddress)
		{
			MigrationUtil.ThrowOnNullArgument(row, "row");
			foreach (PropValue val in row.Properties)
			{
				if (val.PropTag.Id() == PropTag.SmtpAddress.Id())
				{
					int num;
					if (!NspiMigrationDataReader.TryParseString(val, out smtpAddress, out num))
					{
						MigrationLogger.Log(MigrationEventType.Verbose, "NspiMigrationDataReader.TryGetSmtpAddress. Error code {0} (0 for Null-value)", new object[]
						{
							num
						});
						return false;
					}
					return true;
				}
			}
			smtpAddress = null;
			return false;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00032594 File Offset: 0x00030794
		public static bool TryGetIdentifier(PropRow row, out string identifier)
		{
			MigrationUtil.ThrowOnNullArgument(row, "row");
			foreach (PropTag propTag in new PropTag[]
			{
				PropTag.SmtpAddress,
				PropTag.EmailAddress
			})
			{
				foreach (PropValue val in row.Properties)
				{
					if (val.PropTag.Id() == propTag.Id())
					{
						int num;
						if (NspiMigrationDataReader.TryParseString(val, out identifier, out num))
						{
							return true;
						}
						MigrationLogger.Log(MigrationEventType.Verbose, "NspiMigrationDataReader.TryGetIdentifier. Error code {0} (0 for Null-value)", new object[]
						{
							num
						});
					}
				}
			}
			identifier = null;
			return false;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00032670 File Offset: 0x00030870
		public void Ping()
		{
			IMigrationNspiClient nspiClient = MigrationServiceFactory.Instance.GetNspiClient(this.reportData);
			if (nspiClient.QueryRows(this.connectionSettings, new int?(1), new int?(0), NspiMigrationDataReader.DisplayTypeProperties) == null)
			{
				throw new MigrationTransientException(ServerStrings.MigrationNSPINoUsersFound(this.connectionSettings.NspiServer), "No recipients were found over NSPI.");
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x000326CC File Offset: 0x000308CC
		public MigrationObjectsCount GetCounts()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			bool publicFodlers = false;
			int config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MigrationSourceExchangeMailboxMaximumCount");
			int config2 = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MigrationSourceExchangeRecipientMaximumCount");
			int num5 = 0;
			do
			{
				IMigrationNspiClient nspiClient = MigrationServiceFactory.Instance.GetNspiClient(this.reportData);
				IList<PropRow> list = nspiClient.QueryRows(this.connectionSettings, new int?(1000), new int?(num5), NspiMigrationDataReader.DisplayTypeProperties);
				if (list == null)
				{
					break;
				}
				num5 += list.Count;
				for (int i = 0; i < list.Count; i++)
				{
					MigrationUserRecipientType migrationUserRecipientType;
					uint num6;
					if (NspiMigrationDataReader.TryGetRecipientType(list[i], out migrationUserRecipientType, out num6))
					{
						switch (migrationUserRecipientType)
						{
						case MigrationUserRecipientType.Mailbox:
							num++;
							num4++;
							break;
						case MigrationUserRecipientType.Contact:
						case MigrationUserRecipientType.Mailuser:
							num3++;
							num4++;
							break;
						case MigrationUserRecipientType.Group:
							num2++;
							num4++;
							break;
						case MigrationUserRecipientType.PublicFolder:
							publicFodlers = true;
							num4++;
							break;
						}
					}
				}
				MigrationLogger.Log(MigrationEventType.Verbose, "NspiMigrationDataReader GetCounts, current row count {0} processed {1} mailboxes {2} total {3} max recipient {4}", new object[]
				{
					list.Count,
					num5,
					num,
					num4,
					config2
				});
			}
			while (num <= config && num4 <= config2);
			return new MigrationObjectsCount(new int?(num), new int?(num2), new int?(num3), publicFodlers);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00032A94 File Offset: 0x00030C94
		public IEnumerable<IMigrationDataRow> GetItems(int delta, int max, bool discoverProvisioning = true)
		{
			if (1 > max || max > 1000)
			{
				max = 1000;
			}
			delta = ((-1 > delta) ? 0 : (delta + 1));
			IMigrationNspiClient nspiClient = MigrationServiceFactory.Instance.GetNspiClient(this.reportData);
			long[] propertiesToFind = NspiMigrationDataReader.BasicRecipientDiscoveryProperties;
			if (discoverProvisioning)
			{
				propertiesToFind = ExchangeMigrationRecipient.AllRecipientProperties;
			}
			IList<PropRow> rows = nspiClient.QueryRows(this.connectionSettings, new int?(max), new int?(delta), propertiesToFind);
			if (rows != null)
			{
				int i = 0;
				while (i < rows.Count)
				{
					int index = i + delta;
					IMigrationDataRow dataRow;
					MigrationBatchError error;
					if (NspiMigrationDataReader.TryCreate(rows[i], index, propertiesToFind, discoverProvisioning, out dataRow, out error))
					{
						goto IL_14D;
					}
					if (error != null)
					{
						dataRow = new InvalidDataRow(index, error, MigrationType.ExchangeOutlookAnywhere);
						goto IL_14D;
					}
					IL_188:
					i++;
					continue;
					IL_14D:
					ExchangeMigrationDataRow migrationDataRow = (ExchangeMigrationDataRow)dataRow;
					if (migrationDataRow.RecipientType != MigrationUserRecipientType.PublicFolder)
					{
						yield return dataRow;
						goto IL_188;
					}
					goto IL_188;
				}
			}
			yield break;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00032AC8 File Offset: 0x00030CC8
		public string[] GetMembers(string groupSmtpAddress)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(groupSmtpAddress, "groupSmtpAddress");
			List<string> list = new List<string>();
			IMigrationNspiClient nspiClient = MigrationServiceFactory.Instance.GetNspiClient(this.reportData);
			IList<PropRow> groupMembers = nspiClient.GetGroupMembers(this.connectionSettings, groupSmtpAddress);
			if (groupMembers != null)
			{
				for (int i = 0; i < groupMembers.Count; i++)
				{
					string item;
					if (NspiMigrationDataReader.TryGetSmtpAddress(groupMembers[i], out item))
					{
						list.Add(item);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00032B38 File Offset: 0x00030D38
		public IMigrationDataRow GetRecipient(string recipientSmtpAddress)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(recipientSmtpAddress, "recipientSmtpAddress");
			IMigrationDataRow result = null;
			MigrationBatchError migrationBatchError = null;
			IMigrationNspiClient nspiClient = MigrationServiceFactory.Instance.GetNspiClient(this.reportData);
			PropRow propRow = null;
			try
			{
				propRow = nspiClient.GetRecipient(this.connectionSettings, recipientSmtpAddress, ExchangeMigrationRecipient.AllRecipientProperties);
			}
			catch (SourceEmailAddressNotUniquePermanentException ex)
			{
				migrationBatchError = new MigrationBatchError();
				migrationBatchError.EmailAddress = recipientSmtpAddress;
				migrationBatchError.RowIndex = 0;
				migrationBatchError.LocalizedErrorMessage = ex.LocalizedString;
			}
			if (propRow != null && NspiMigrationDataRow.TryCreate(propRow, 0, ExchangeMigrationRecipient.AllRecipientProperties, out result, out migrationBatchError))
			{
				return result;
			}
			if (migrationBatchError == null)
			{
				migrationBatchError = new MigrationBatchError();
				migrationBatchError.EmailAddress = recipientSmtpAddress;
				migrationBatchError.RowIndex = 0;
				migrationBatchError.LocalizedErrorMessage = Strings.RecipientDoesNotExistAtSource(recipientSmtpAddress);
			}
			return new InvalidDataRow(migrationBatchError.RowIndex, migrationBatchError, MigrationType.ExchangeOutlookAnywhere);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00032BF8 File Offset: 0x00030DF8
		public ExchangeMigrationRecipient GetRecipientData(string recipientSmtpAddress, ProvisioningType provisioningType)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(recipientSmtpAddress, "recipientSmtpAddress");
			IMigrationNspiClient nspiClient = MigrationServiceFactory.Instance.GetNspiClient(this.reportData);
			long[] allRecipientProperties = ExchangeMigrationRecipient.AllRecipientProperties;
			PropRow recipient = nspiClient.GetRecipient(this.connectionSettings, recipientSmtpAddress, allRecipientProperties);
			if (recipient == null)
			{
				throw new SourceRecipientDoesNotExistException(recipientSmtpAddress);
			}
			ExchangeMigrationRecipient exchangeMigrationRecipient = NspiMigrationDataReader.TryCreateRecipient(recipient, allRecipientProperties, true);
			if (exchangeMigrationRecipient == null)
			{
				throw new SourceRecipientInvalidException(recipientSmtpAddress);
			}
			if (provisioningType == ProvisioningType.GroupMember && exchangeMigrationRecipient is ExchangeMigrationGroupRecipient)
			{
				ExchangeMigrationGroupRecipient exchangeMigrationGroupRecipient = (ExchangeMigrationGroupRecipient)exchangeMigrationRecipient;
				exchangeMigrationGroupRecipient.Members = this.GetMembers(recipientSmtpAddress);
				exchangeMigrationGroupRecipient.MembersChanged = true;
			}
			return exchangeMigrationRecipient;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00032C80 File Offset: 0x00030E80
		public ExchangeJobItemSubscriptionSettings GetSubscriptionSettings(string recipientSmtpAddress)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(recipientSmtpAddress, "recipientSmtpAddress");
			IMigrationNspiClient nspiClient = MigrationServiceFactory.Instance.GetNspiClient(this.reportData);
			long[] array = new long[]
			{
				805503007L,
				(long)((ulong)-2147090401)
			};
			PropRow recipient = nspiClient.GetRecipient(this.connectionSettings, recipientSmtpAddress, array);
			return NspiMigrationDataReader.CreateSubscriptionSettings(recipient, array);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00032CDC File Offset: 0x00030EDC
		public void SetRecipient(string recipientSmtpAddress, string recipientLegDN, string targetAddress)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(recipientSmtpAddress, "recipientSmtpAddress");
			MigrationUtil.ThrowOnNullOrEmptyArgument(recipientLegDN, "recipientLegDN");
			MigrationUtil.ThrowOnNullOrEmptyArgument(targetAddress, "targetAddress");
			IMigrationNspiClient nspiClient = MigrationServiceFactory.Instance.GetNspiClient(this.reportData);
			try
			{
				nspiClient.SetRecipient(this.connectionSettings, recipientSmtpAddress, recipientLegDN, new string[]
				{
					targetAddress
				}, new long[]
				{
					(long)((ulong)-2146369505)
				});
			}
			catch (MigrationTransientException ex)
			{
				throw new MigrationTransientException(Strings.FailedToUpdateRecipientSource(targetAddress), "Failed to update the on-premises mailbox with the target address.", ex);
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00032D6C File Offset: 0x00030F6C
		private static bool TryCreate(PropRow row, int index, long[] properties, bool discoverProvisioning, out IMigrationDataRow dataRow, out MigrationBatchError error)
		{
			if (discoverProvisioning)
			{
				return NspiMigrationDataRow.TryCreate(row, index, properties, out dataRow, out error);
			}
			MigrationUtil.ThrowOnNullArgument(row, "row");
			MigrationUtil.ThrowOnNullArgument(properties, "properties");
			if (row.Properties.Count != properties.Length)
			{
				throw new ArgumentOutOfRangeException("row", "row.Properties.Count != properties.Length");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			string text;
			if (!NspiMigrationDataReader.TryGetIdentifier(row, out text) || string.IsNullOrEmpty(text))
			{
				error = new MigrationBatchError
				{
					EmailAddress = null,
					RowIndex = index,
					LocalizedErrorMessage = ServerStrings.MigrationNSPIMissingRequiredField(PropTag.SmtpAddress)
				};
				dataRow = null;
				return false;
			}
			MigrationUserRecipientType recipientType;
			uint num;
			NspiMigrationDataReader.TryGetRecipientType(row, out recipientType, out num);
			error = null;
			dataRow = new ExchangeMigrationDataRow(index, text, recipientType);
			return true;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00032E2C File Offset: 0x0003102C
		private static ExchangeJobItemSubscriptionSettings CreateSubscriptionSettings(PropRow row, long[] properties)
		{
			MigrationUtil.ThrowOnNullArgument(row, "row");
			MigrationUtil.ThrowOnNullArgument(properties, "properties");
			if (row.Properties.Count != properties.Length)
			{
				throw new ArgumentOutOfRangeException("row", "row.Properties.Count != properties.Length");
			}
			int num = -1;
			int num2 = -1;
			for (int i = 0; i < row.Properties.Count; i++)
			{
				if (row.Properties[i].PropTag.Id() == PropTag.EmailAddress.Id())
				{
					num = i;
				}
				else if (row.Properties[i].PropTag.Id() == ((PropTag)2147876895U).Id())
				{
					num2 = i;
				}
				if (num != -1 && num2 != -1)
				{
					break;
				}
			}
			string mailboxDN = null;
			string text = null;
			int errorCode;
			bool flag = NspiMigrationDataReader.TryParseString(row.Properties[num], out mailboxDN, out errorCode);
			if (flag)
			{
				string text2 = null;
				flag = NspiMigrationDataReader.TryParseString(row.Properties[num2], out text2, out errorCode);
				flag &= !string.IsNullOrEmpty(text2);
				if (flag)
				{
					int num3 = text2.IndexOf("/cn=Microsoft Private MDB", StringComparison.OrdinalIgnoreCase);
					text = ((num3 >= 0) ? text2.Substring(0, num3) : null);
				}
			}
			if (flag)
			{
				return ExchangeJobItemSubscriptionSettings.CreateFromProperties(mailboxDN, text, text, null);
			}
			throw new NSPIDiscoveryFailedTransientException
			{
				ErrorCode = errorCode
			};
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00032F76 File Offset: 0x00031176
		private static bool TryParseInt(PropValue val, out int value, out int errorCode)
		{
			if (val.IsNull())
			{
				errorCode = 0;
				value = 0;
				return false;
			}
			if (val.IsError())
			{
				errorCode = val.GetErrorValue();
				value = 0;
				return false;
			}
			value = val.GetInt();
			errorCode = 0;
			return true;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00032FAD File Offset: 0x000311AD
		private static bool TryParseString(PropValue val, out string value, out int errorCode)
		{
			if (val.IsNull())
			{
				errorCode = 0;
				value = null;
				return false;
			}
			if (val.IsError())
			{
				errorCode = val.GetErrorValue();
				value = null;
				return false;
			}
			value = val.GetString();
			errorCode = 0;
			return true;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00032FE4 File Offset: 0x000311E4
		private static bool TryParseStringArray(PropValue val, out string[] value, out int errorCode)
		{
			if (val.IsNull())
			{
				errorCode = 0;
				value = null;
				return false;
			}
			if (val.IsError())
			{
				errorCode = val.GetErrorValue();
				value = null;
				return false;
			}
			value = val.GetStringArray();
			errorCode = 0;
			return true;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0003301B File Offset: 0x0003121B
		private static bool TryParseBytes(PropValue val, out byte[] value, out int errorCode)
		{
			if (val.IsNull())
			{
				errorCode = 0;
				value = null;
				return false;
			}
			if (val.IsError())
			{
				errorCode = val.GetErrorValue();
				value = null;
				return false;
			}
			value = val.GetBytes();
			errorCode = 0;
			return true;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00033054 File Offset: 0x00031254
		private static bool TryParseRecipientType(PropValue val, out MigrationUserRecipientType recipientType, out int errorCode)
		{
			int num;
			if (!NspiMigrationDataReader.TryParseInt(val, out num, out errorCode))
			{
				recipientType = MigrationUserRecipientType.Unsupported;
				return false;
			}
			switch (num)
			{
			case 0:
				recipientType = MigrationUserRecipientType.Mailbox;
				return true;
			case 1:
			case 5:
				recipientType = MigrationUserRecipientType.Group;
				return true;
			case 6:
				recipientType = MigrationUserRecipientType.Contact;
				return true;
			}
			recipientType = MigrationUserRecipientType.Unsupported;
			return true;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000330AC File Offset: 0x000312AC
		private static bool TryParseDisplayTypeEx(PropValue val, out uint displayTypeEx, out int errorCode)
		{
			int num;
			if (!NspiMigrationDataReader.TryParseInt(val, out num, out errorCode))
			{
				displayTypeEx = 0U;
				return false;
			}
			displayTypeEx = (uint)(num & 255);
			return true;
		}

		// Token: 0x04000465 RID: 1125
		public const uint UserMailboxType = 0U;

		// Token: 0x04000466 RID: 1126
		public const uint MailUserType = 6U;

		// Token: 0x04000467 RID: 1127
		public const uint RoomMailboxType = 7U;

		// Token: 0x04000468 RID: 1128
		public const uint EquipmentMailboxType = 8U;

		// Token: 0x04000469 RID: 1129
		private const int MaxRowCountPerBatch = 1000;

		// Token: 0x0400046A RID: 1130
		private const uint MailboxTypeBitmask = 255U;

		// Token: 0x0400046B RID: 1131
		private const int ErrorCodePropValueDoesntExist = -2147221233;

		// Token: 0x0400046C RID: 1132
		private static readonly long[] BasicRecipientDiscoveryProperties = new long[]
		{
			972947487L,
			805503007L,
			956301315L,
			956628995L,
			2147876895L
		};

		// Token: 0x0400046D RID: 1133
		private static readonly long[] DisplayTypeProperties = new long[]
		{
			956301315L,
			956628995L,
			2147876895L
		};

		// Token: 0x0400046E RID: 1134
		private readonly ExchangeOutlookAnywhereEndpoint connectionSettings;

		// Token: 0x0400046F RID: 1135
		private readonly ReportData reportData;
	}
}
