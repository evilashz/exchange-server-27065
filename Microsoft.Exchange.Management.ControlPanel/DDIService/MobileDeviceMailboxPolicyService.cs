using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000310 RID: 784
	public class MobileDeviceMailboxPolicyService : DDICodeBehind
	{
		// Token: 0x06002E75 RID: 11893 RVA: 0x0008CF28 File Offset: 0x0008B128
		public MobileDeviceMailboxPolicyService()
		{
			base.RegisterRbacDependency("MaxDevicePasswordFailedAttemptsString", new List<string>(new string[]
			{
				"MaxPasswordFailedAttempts"
			}));
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x0008CF5C File Offset: 0x0008B15C
		public static void GetListPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (DBNull.Value != dataRow["WhenChangedUTC"])
				{
					dataRow["Modified"] = ((DateTime?)dataRow["WhenChangedUTC"]).UtcToUserDateTimeString();
				}
			}
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x0008CFE0 File Offset: 0x0008B1E0
		public static void GetObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			MobileMailboxPolicy mobileMailboxPolicy = store.GetDataObject("MobileMailboxPolicy") as MobileMailboxPolicy;
			if (dataTable.Rows.Count == 1 && mobileMailboxPolicy != null)
			{
				DataRow dataRow = dataTable.Rows[0];
				dataRow["IsMinPasswordLengthSet"] = (mobileMailboxPolicy.PasswordEnabled && mobileMailboxPolicy.MinPasswordLength != null);
				dataRow["MinPasswordLength"] = ((mobileMailboxPolicy.MinPasswordLength != null) ? ((int)dataRow["MinPasswordLength"]) : 4);
				dataRow["IsMaxPasswordFailedAttemptsSet"] = (mobileMailboxPolicy.PasswordEnabled && !mobileMailboxPolicy.MaxPasswordFailedAttempts.IsUnlimited);
				dataRow["MaxPasswordFailedAttempts"] = (mobileMailboxPolicy.MaxPasswordFailedAttempts.IsUnlimited ? "8" : mobileMailboxPolicy.MaxPasswordFailedAttempts.Value.ToString(CultureInfo.InvariantCulture));
				dataRow["IsMaxInactivityTimeLockSet"] = (mobileMailboxPolicy.PasswordEnabled && !mobileMailboxPolicy.MaxInactivityTimeLock.IsUnlimited);
				dataRow["MaxInactivityTimeLock"] = (mobileMailboxPolicy.MaxInactivityTimeLock.IsUnlimited ? "15" : mobileMailboxPolicy.MaxInactivityTimeLock.Value.TotalMinutes.ToString(CultureInfo.InvariantCulture));
				dataRow["IsPasswordExpirationSet"] = (mobileMailboxPolicy.PasswordEnabled && !mobileMailboxPolicy.PasswordExpiration.IsUnlimited);
				dataRow["PasswordExpiration"] = (mobileMailboxPolicy.PasswordExpiration.IsUnlimited ? "90" : mobileMailboxPolicy.PasswordExpiration.Value.Days.ToString());
				dataRow["PasswordRequirementsString"] = MobileDeviceMailboxPolicyService.PasswordRequirementsString(dataRow);
				dataRow["HasAdditionalCustomSettings"] = MobileDeviceMailboxPolicyService.HasAdditionalCustomSettings(mobileMailboxPolicy);
			}
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x0008D1FC File Offset: 0x0008B3FC
		private static string PasswordRequirementsString(DataRow row)
		{
			if (!(bool)row["PasswordEnabled"])
			{
				return Strings.PasswordNotRequired;
			}
			if ((bool)row["IsMaxInactivityTimeLockSet"])
			{
				if ((bool)row["AlphanumericPasswordRequired"])
				{
					return string.Format(Strings.RequiredAlphaLockingPassword, (int)row["MinPasswordLength"], (string)row["MaxInactivityTimeLock"]);
				}
				if ((bool)row["IsMinPasswordLengthSet"])
				{
					return string.Format(Strings.RequiredPinLockingPassword, (int)row["MinPasswordLength"], (string)row["MaxInactivityTimeLock"]);
				}
				return string.Format(Strings.RequiredLockingPassword, (string)row["MaxInactivityTimeLock"]);
			}
			else
			{
				if ((bool)row["AlphanumericPasswordRequired"])
				{
					return string.Format(Strings.RequiredAlphaPassword, (int)row["MinPasswordLength"]);
				}
				if ((bool)row["IsMinPasswordLengthSet"])
				{
					return string.Format(Strings.RequiredPinPassword, (int)row["MinPasswordLength"]);
				}
				return Strings.PasswordRequired;
			}
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x0008D38C File Offset: 0x0008B58C
		public static void SetObjectPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			List<string> list = new List<string>();
			if (DBNull.Value.Equals(inputRow["MinPasswordComplexCharacters"]))
			{
				inputRow["MinPasswordComplexCharacters"] = "3";
				list.Add("MinPasswordComplexCharacters");
			}
			if (DBNull.Value != inputRow["MaxPasswordFailedAttempts"] || DBNull.Value != inputRow["IsMaxPasswordFailedAttemptsSet"])
			{
				bool isValueSet = DBNull.Value.Equals(inputRow["IsMaxPasswordFailedAttemptsSet"]) || (bool)inputRow["IsMaxPasswordFailedAttemptsSet"];
				string value = (DBNull.Value != inputRow["MaxPasswordFailedAttempts"]) ? ((string)inputRow["MaxPasswordFailedAttempts"]) : "8";
				object value2;
				if (MobileDeviceMailboxPolicyService.CheckAndParseParams<int>(true, isValueSet, value, (string x) => int.Parse(x), out value2))
				{
					inputRow["MaxPasswordFailedAttempts"] = value2;
					list.Add("MaxPasswordFailedAttempts");
					list.Add("IsMaxPasswordFailedAttemptsSet");
				}
			}
			if (DBNull.Value != inputRow["PasswordExpiration"] || DBNull.Value != inputRow["IsPasswordExpirationSet"])
			{
				bool isValueSet = DBNull.Value.Equals(inputRow["IsPasswordExpirationSet"]) || (bool)inputRow["IsPasswordExpirationSet"];
				string value = (DBNull.Value != inputRow["PasswordExpiration"]) ? ((string)inputRow["PasswordExpiration"]) : "90";
				object value2;
				if (MobileDeviceMailboxPolicyService.CheckAndParseParams<EnhancedTimeSpan>(true, isValueSet, value, (string x) => EnhancedTimeSpan.FromDays(double.Parse(x)), out value2))
				{
					inputRow["PasswordExpiration"] = value2;
					list.Add("PasswordExpiration");
					list.Add("IsPasswordExpirationSet");
				}
			}
			if (DBNull.Value != inputRow["MaxInactivityTimeLock"] || DBNull.Value != inputRow["IsMaxInactivityTimeLockSet"])
			{
				bool isValueSet = DBNull.Value.Equals(inputRow["IsMaxInactivityTimeLockSet"]) || (bool)inputRow["IsMaxInactivityTimeLockSet"];
				string value = (DBNull.Value != inputRow["MaxInactivityTimeLock"]) ? ((string)inputRow["MaxInactivityTimeLock"]) : "15";
				object value2;
				if (MobileDeviceMailboxPolicyService.CheckAndParseParams<EnhancedTimeSpan>(true, isValueSet, value, (string x) => EnhancedTimeSpan.FromMinutes(double.Parse(x)), out value2))
				{
					inputRow["MaxInactivityTimeLock"] = value2;
					list.Add("MaxInactivityTimeLock");
					list.Add("IsMaxInactivityTimeLockSet");
				}
			}
			if (DBNull.Value != inputRow["MinPasswordLength"] || DBNull.Value != inputRow["IsMinPasswordLengthSet"])
			{
				bool isValueSet = DBNull.Value.Equals(inputRow["IsMinPasswordLengthSet"]) || (bool)inputRow["IsMinPasswordLengthSet"];
				string value = (DBNull.Value != inputRow["MinPasswordLength"]) ? ((string)inputRow["MinPasswordLength"]) : 4.ToString();
				object value2;
				if (MobileDeviceMailboxPolicyService.CheckAndParseParams<int>(false, isValueSet, value, (string x) => int.Parse(x), out value2))
				{
					inputRow["MinPasswordLength"] = value2;
					list.Add("MinPasswordLength");
					list.Add("IsMinPasswordLengthSet");
				}
			}
			if (list.Count > 0)
			{
				store.SetModifiedColumns(list);
			}
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x0008D704 File Offset: 0x0008B904
		private static bool CheckAndParseParams<T>(bool isUnlimited, bool isValueSet, string value, Func<string, T> convert, out object result) where T : struct, IComparable
		{
			isValueSet = (!DBNull.Value.Equals(isValueSet) && isValueSet);
			bool result2 = false;
			result = null;
			if (!isValueSet)
			{
				if (isUnlimited)
				{
					result = Unlimited<T>.UnlimitedString;
				}
				else
				{
					result = null;
				}
				result2 = true;
			}
			else if (isValueSet)
			{
				try
				{
					if (isUnlimited)
					{
						result = new Unlimited<T>(convert(value));
					}
					else
					{
						result = convert(value);
					}
					result2 = true;
				}
				catch (Exception)
				{
					result2 = false;
				}
			}
			return result2;
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x0008D78C File Offset: 0x0008B98C
		private static bool HasAdditionalCustomSettings(MobileMailboxPolicy policy)
		{
			foreach (ADPropertyDefinition adpropertyDefinition in MobileDeviceMailboxPolicyService.AdditionalProperties)
			{
				if ((adpropertyDefinition.Flags & ADPropertyDefinitionFlags.MultiValued) == ADPropertyDefinitionFlags.MultiValued)
				{
					if (adpropertyDefinition.DefaultValue != null)
					{
						throw new NotSupportedException("Non-empty default value for multivalued property is not supported!");
					}
					if (policy[adpropertyDefinition] != null && ((IList)policy[adpropertyDefinition]).Count > 0)
					{
						return true;
					}
				}
				else if (adpropertyDefinition.Type == typeof(bool))
				{
					if ((bool)policy[adpropertyDefinition] != (bool)adpropertyDefinition.DefaultValue)
					{
						return true;
					}
				}
				else if (adpropertyDefinition.Type == typeof(int))
				{
					if ((int)policy[adpropertyDefinition] != (int)adpropertyDefinition.DefaultValue)
					{
						return true;
					}
				}
				else if (adpropertyDefinition.Type == typeof(Unlimited<int>))
				{
					if ((Unlimited<int>)policy[adpropertyDefinition] != (Unlimited<int>)adpropertyDefinition.DefaultValue)
					{
						return true;
					}
				}
				else if (adpropertyDefinition.Type == typeof(Unlimited<EnhancedTimeSpan>))
				{
					if ((Unlimited<EnhancedTimeSpan>)policy[adpropertyDefinition] != (Unlimited<EnhancedTimeSpan>)adpropertyDefinition.DefaultValue)
					{
						return true;
					}
				}
				else if (adpropertyDefinition.Type == typeof(Unlimited<ByteQuantifiedSize>))
				{
					if ((Unlimited<ByteQuantifiedSize>)policy[adpropertyDefinition] != (Unlimited<ByteQuantifiedSize>)adpropertyDefinition.DefaultValue)
					{
						return true;
					}
				}
				else if (policy[adpropertyDefinition] != adpropertyDefinition.DefaultValue)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x0008D93C File Offset: 0x0008BB3C
		public static void GetDefaultPolicyPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.BeginLoadData();
			List<DataRow> list = new List<DataRow>();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				if (false.Equals(dataRow["IsDefault"]))
				{
					list.Add(dataRow);
				}
				else
				{
					MobileDeviceMailboxPolicyService.UpdateDefaultPolicy(dataRow);
				}
			}
			foreach (DataRow row in list)
			{
				dataTable.Rows.Remove(row);
			}
			dataTable.EndLoadData();
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x0008D9EC File Offset: 0x0008BBEC
		private static void UpdateDefaultPolicy(DataRow row)
		{
			bool flag = false;
			if (!DBNull.Value.Equals(row["PasswordEnabled"]))
			{
				flag = (bool)row["PasswordEnabled"];
			}
			row["IsMinPasswordLengthSet"] = (flag && !DBNull.Value.Equals(row["MinPasswordLength"]));
			if (DBNull.Value.Equals(row["MinPasswordLength"]))
			{
				row["MinPasswordLength"] = 4;
			}
			if (!DBNull.Value.Equals(row["MaxPasswordFailedAttempts"]))
			{
				Unlimited<int> unlimited = (Unlimited<int>)row["MaxPasswordFailedAttempts"];
				row["IsMaxPasswordFailedAttemptsSet"] = (flag && !unlimited.IsUnlimited);
				row["MaxPasswordFailedAttempts"] = (unlimited.IsUnlimited ? "8" : unlimited.Value.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				row["MaxPasswordFailedAttempts"] = "8";
				row["IsMaxPasswordFailedAttemptsSet"] = false;
			}
			if (!DBNull.Value.Equals(row["MaxInactivityTimeLock"]))
			{
				Unlimited<EnhancedTimeSpan> unlimited2 = (Unlimited<EnhancedTimeSpan>)row["MaxInactivityTimeLock"];
				row["IsMaxInactivityTimeLockSet"] = (flag && !unlimited2.IsUnlimited);
				row["MaxInactivityTimeLock"] = (unlimited2.IsUnlimited ? "15" : unlimited2.Value.TotalMinutes.ToString(CultureInfo.InvariantCulture));
				return;
			}
			row["MaxInactivityTimeLock"] = "15";
			row["IsMaxInactivityTimeLockSet"] = false;
		}

		// Token: 0x040022A1 RID: 8865
		internal const string DefaultMaxPasswordFailedAttempts = "8";

		// Token: 0x040022A2 RID: 8866
		internal const string DefaultPasswordExpiration = "90";

		// Token: 0x040022A3 RID: 8867
		internal const string DefaultMaxInactivityTimeLock = "15";

		// Token: 0x040022A4 RID: 8868
		internal const string DefaultMinPasswordComplexCharacters = "3";

		// Token: 0x040022A5 RID: 8869
		internal const int DefaultMinPasswordLength = 4;

		// Token: 0x040022A6 RID: 8870
		internal const string IsMinPasswordLengthSetColumnName = "IsMinPasswordLengthSet";

		// Token: 0x040022A7 RID: 8871
		internal const string MinPasswordLengthColumnName = "MinPasswordLength";

		// Token: 0x040022A8 RID: 8872
		internal const string IsMaxPasswordFailedAttemptsSetColumnName = "IsMaxPasswordFailedAttemptsSet";

		// Token: 0x040022A9 RID: 8873
		internal const string HasAdditionalCustomSettingsColumnName = "HasAdditionalCustomSettings";

		// Token: 0x040022AA RID: 8874
		internal const string MaxPasswordFailedAttemptsColumnName = "MaxPasswordFailedAttempts";

		// Token: 0x040022AB RID: 8875
		internal const string IsMaxInactivityTimeLockSetColumnName = "IsMaxInactivityTimeLockSet";

		// Token: 0x040022AC RID: 8876
		internal const string MaxInactivityTimeLockColumnName = "MaxInactivityTimeLock";

		// Token: 0x040022AD RID: 8877
		internal const string IsPasswordExpirationSetColumnName = "IsPasswordExpirationSet";

		// Token: 0x040022AE RID: 8878
		internal const string PasswordExpirationColumnName = "PasswordExpiration";

		// Token: 0x040022AF RID: 8879
		internal const string PasswordRequirementsStringColumnName = "PasswordRequirementsString";

		// Token: 0x040022B0 RID: 8880
		internal const string PasswordEnabledColumnName = "PasswordEnabled";

		// Token: 0x040022B1 RID: 8881
		internal const string WhenChangedUTCColumnName = "WhenChangedUTC";

		// Token: 0x040022B2 RID: 8882
		internal const string ModifiedColumnName = "Modified";

		// Token: 0x040022B3 RID: 8883
		internal const string AlphanumericPasswordRequiredColumnName = "AlphanumericPasswordRequired";

		// Token: 0x040022B4 RID: 8884
		internal const string MinPasswordComplexCharactersColumnName = "MinPasswordComplexCharacters";

		// Token: 0x040022B5 RID: 8885
		private static ADPropertyDefinition[] AdditionalProperties = new ADPropertyDefinition[]
		{
			MobileMailboxPolicySchema.UnapprovedInROMApplicationList,
			MobileMailboxPolicySchema.ADApprovedApplicationList,
			MobileMailboxPolicySchema.AttachmentsEnabled,
			MobileMailboxPolicySchema.RequireStorageCardEncryption,
			MobileMailboxPolicySchema.PasswordRecoveryEnabled,
			MobileMailboxPolicySchema.DevicePolicyRefreshInterval,
			MobileMailboxPolicySchema.MaxAttachmentSize,
			MobileMailboxPolicySchema.WSSAccessEnabled,
			MobileMailboxPolicySchema.UNCAccessEnabled,
			MobileMailboxPolicySchema.DenyApplePushNotifications,
			MobileMailboxPolicySchema.AllowStorageCard,
			MobileMailboxPolicySchema.AllowCamera,
			MobileMailboxPolicySchema.AllowUnsignedApplications,
			MobileMailboxPolicySchema.AllowUnsignedInstallationPackages,
			MobileMailboxPolicySchema.AllowWiFi,
			MobileMailboxPolicySchema.AllowTextMessaging,
			MobileMailboxPolicySchema.AllowPOPIMAPEmail,
			MobileMailboxPolicySchema.AllowIrDA,
			MobileMailboxPolicySchema.RequireManualSyncWhenRoaming,
			MobileMailboxPolicySchema.AllowDesktopSync,
			MobileMailboxPolicySchema.AllowHTMLEmail,
			MobileMailboxPolicySchema.RequireSignedSMIMEMessages,
			MobileMailboxPolicySchema.RequireEncryptedSMIMEMessages,
			MobileMailboxPolicySchema.AllowSMIMESoftCerts,
			MobileMailboxPolicySchema.AllowBrowser,
			MobileMailboxPolicySchema.AllowConsumerEmail,
			MobileMailboxPolicySchema.AllowRemoteDesktop,
			MobileMailboxPolicySchema.AllowInternetSharing,
			MobileMailboxPolicySchema.AllowBluetooth,
			MobileMailboxPolicySchema.MaxCalendarAgeFilter,
			MobileMailboxPolicySchema.MaxEmailAgeFilter,
			MobileMailboxPolicySchema.RequireSignedSMIMEAlgorithm,
			MobileMailboxPolicySchema.RequireEncryptionSMIMEAlgorithm,
			MobileMailboxPolicySchema.AllowSMIMEEncryptionAlgorithmNegotiation,
			MobileMailboxPolicySchema.MaxEmailBodyTruncationSize,
			MobileMailboxPolicySchema.MaxEmailHTMLBodyTruncationSize,
			MobileMailboxPolicySchema.AllowExternalDeviceManagement,
			MobileMailboxPolicySchema.MobileOTAUpdateMode,
			MobileMailboxPolicySchema.AllowMobileOTAUpdate,
			MobileMailboxPolicySchema.IrmEnabled
		};
	}
}
