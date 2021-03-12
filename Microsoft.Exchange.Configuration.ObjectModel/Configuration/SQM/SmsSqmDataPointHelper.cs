using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Security.Compliance;
using Microsoft.Exchange.Sqm;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Configuration.SQM
{
	// Token: 0x0200026F RID: 623
	internal class SmsSqmDataPointHelper
	{
		// Token: 0x0600156B RID: 5483 RVA: 0x0004F14C File Offset: 0x0004D34C
		public static string Generate64BitUserID(string legacyDN)
		{
			byte[] array = null;
			byte[] bytes = Encoding.Default.GetBytes(legacyDN);
			lock (SmsSqmDataPointHelper.md5Hasher)
			{
				array = SmsSqmDataPointHelper.md5Hasher.ComputeHash(bytes);
			}
			StringBuilder stringBuilder = new StringBuilder(16);
			for (int i = 4; i < array.Length - 4; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0004F1DC File Offset: 0x0004D3DC
		public static string GetDeploymentType(ADObjectId id)
		{
			string result = string.Empty;
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled)
			{
				string name = id.Parent.Name;
				ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAllTenantsPartitionId(id.GetPartitionId()), 84, "GetDeploymentType", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\SQM\\SmsSqmDataPointHelper.cs");
				ExchangeConfigurationUnit exchangeConfigurationUnitByName = tenantConfigurationSession.GetExchangeConfigurationUnitByName(name);
				result = exchangeConfigurationUnitByName.ProgramId;
			}
			else
			{
				result = "On-Premises";
			}
			return result;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0004F258 File Offset: 0x0004D458
		public static void AddDeviceInfoReceivedDataPoint(SmsSqmSession instance, ADObjectId id, string legacyDN, string deviceType, string versionString)
		{
			if (instance.Enabled)
			{
				string deploymentType = SmsSqmDataPointHelper.GetDeploymentType(id);
				string text = SmsSqmDataPointHelper.Generate64BitUserID(legacyDN);
				instance.AddToStreamDataPoint(SqmDataID.SMS_ST_EASESTABLISHMENT, new object[]
				{
					deploymentType,
					text,
					deviceType,
					versionString
				});
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0004F2A0 File Offset: 0x0004D4A0
		public static void AddEasConfigurationDataPoint(SmsSqmSession instance, ADObjectId id, string legacyDN, string deviceType, bool notificationEnabled, string versionString)
		{
			if (instance.Enabled)
			{
				string deploymentType = SmsSqmDataPointHelper.GetDeploymentType(id);
				string text = SmsSqmDataPointHelper.Generate64BitUserID(legacyDN);
				instance.AddToStreamDataPoint(SqmDataID.SMS_ST_EASCONFIGURATION, new object[]
				{
					deploymentType,
					text,
					deviceType,
					notificationEnabled,
					versionString
				});
			}
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0004F2F4 File Offset: 0x0004D4F4
		public static void AddEasMessageDataPoint(SmsSqmSession instance, ADObjectId id, string legacyDN, int messageCount, bool hasUnicode, int recipientNumber, bool isFromOutlook)
		{
			if (instance.Enabled)
			{
				SMSSendingClient smssendingClient;
				if (isFromOutlook)
				{
					smssendingClient = SMSSendingClient.Outlook;
				}
				else
				{
					smssendingClient = SMSSendingClient.OWA;
				}
				string deploymentType = SmsSqmDataPointHelper.GetDeploymentType(id);
				string text = SmsSqmDataPointHelper.Generate64BitUserID(legacyDN);
				instance.AddToStreamDataPoint(SqmDataID.SMS_ST_EASMESSAGES, new object[]
				{
					deploymentType,
					text,
					messageCount,
					hasUnicode,
					recipientNumber,
					(int)smssendingClient
				});
			}
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0004F368 File Offset: 0x0004D568
		public static void AddNotificationTurningOnDataPoint(SmsSqmSession instance, ADObjectId id, string legacyDN, TextMessagingAccount account)
		{
			if (instance.Enabled)
			{
				string deploymentType = SmsSqmDataPointHelper.GetDeploymentType(id);
				string text = SmsSqmDataPointHelper.Generate64BitUserID(legacyDN);
				RegionInfo regionInfo = (RegionInfo)account[TextMessagingAccountSchema.CountryRegionId];
				int num = (int)account[TextMessagingAccountSchema.MobileOperatorId];
				instance.AddToStreamDataPoint(SqmDataID.SMS_ST_NOTIFICATIONTURNINGON, new object[]
				{
					deploymentType,
					text,
					regionInfo.ToString(),
					num
				});
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0004F3E0 File Offset: 0x0004D5E0
		public static void AddNotificationConfigDataPoint(SmsSqmSession instance, ADObjectId id, string legacyDN, SMSNotificationType notificationType)
		{
			if (notificationType == SMSNotificationType.None)
			{
				return;
			}
			if (instance.Enabled)
			{
				string deploymentType = SmsSqmDataPointHelper.GetDeploymentType(id);
				string text = SmsSqmDataPointHelper.Generate64BitUserID(legacyDN);
				instance.AddToStreamDataPoint(SqmDataID.SMS_ST_NOTIFICATIONCONFIGURATION, new object[]
				{
					deploymentType,
					text,
					(int)notificationType
				});
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0004F42B File Offset: 0x0004D62B
		public static void AddNotificationConfigDataPoint(SmsSqmSession instance, ADObjectId id, string legacyDN, CalendarNotification calendarNotification)
		{
			if (instance.Enabled)
			{
				if (calendarNotification.CalendarUpdateNotification)
				{
					SmsSqmDataPointHelper.AddNotificationConfigDataPoint(instance, id, legacyDN, SMSNotificationType.CalendarUpdate);
				}
				if (calendarNotification.MeetingReminderNotification)
				{
					SmsSqmDataPointHelper.AddNotificationConfigDataPoint(instance, id, legacyDN, SMSNotificationType.CalendarReminder);
				}
				if (calendarNotification.DailyAgendaNotification)
				{
					SmsSqmDataPointHelper.AddNotificationConfigDataPoint(instance, id, legacyDN, SMSNotificationType.CalendarAgenda);
				}
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0004F46C File Offset: 0x0004D66C
		public static void AddNotificationMessageDataPoint(SmsSqmSession instance, ADObjectId id, string legacyDN, SMSNotificationType messageType, bool isVoicemailMessage, bool isRuleAlertMessage, bool isSystemMessage, int messageCount, string carrier, string region)
		{
			if (instance.Enabled)
			{
				string deploymentType = SmsSqmDataPointHelper.GetDeploymentType(id);
				string text = SmsSqmDataPointHelper.Generate64BitUserID(legacyDN);
				if (messageType == SMSNotificationType.None)
				{
					if (isVoicemailMessage)
					{
						messageType = SMSNotificationType.VoiceMail;
					}
					else if (isRuleAlertMessage)
					{
						messageType = SMSNotificationType.Email;
					}
					else
					{
						if (!isSystemMessage)
						{
							return;
						}
						messageType = SMSNotificationType.System;
					}
				}
				instance.AddToStreamDataPoint(SqmDataID.SMS_ST_NOTIFICATIONMESSAGE, new object[]
				{
					deploymentType,
					text,
					(int)messageType,
					messageCount,
					Convert.ToInt32(carrier),
					region
				});
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0004F4F4 File Offset: 0x0004D6F4
		public static void AddNdrDataPoint(SmsSqmSession instance, SMSNotificationType messageType, bool isEASMessage, bool isVoicemailMessage, bool isRuleAlertMessage, bool isSystemMessage, string errorMessage)
		{
			if (instance.Enabled)
			{
				int num;
				if (isVoicemailMessage)
				{
					num = 20;
				}
				else if (isRuleAlertMessage)
				{
					num = 10;
				}
				else if (isSystemMessage)
				{
					num = 40;
				}
				else if (isEASMessage)
				{
					num = 101;
				}
				else
				{
					num = (int)messageType;
				}
				instance.AddToStreamDataPoint(SqmDataID.SMS_ST_NDRS, new object[]
				{
					num,
					errorMessage.Substring(0, Math.Min(50, errorMessage.Length))
				});
			}
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0004F564 File Offset: 0x0004D764
		public static SMSNotificationType TranslateEnumForSqm<T>(T original) where T : IConvertible
		{
			SMSNotificationType result = SMSNotificationType.None;
			if (original is UMSMSNotificationOptions)
			{
				switch (original.ToInt32(null))
				{
				case 0:
					break;
				case 1:
					result = SMSNotificationType.VoiceMail;
					break;
				case 2:
					result = SMSNotificationType.VoiceMailAndMissedCalls;
					break;
				default:
					result = SMSNotificationType.None;
					break;
				}
			}
			else if (original is CalendarNotificationType)
			{
				switch (original.ToInt32(null))
				{
				case 0:
					break;
				case 1:
					result = SMSNotificationType.CalendarAgenda;
					break;
				case 2:
					result = SMSNotificationType.CalendarReminder;
					break;
				case 3:
				case 4:
				case 5:
					result = SMSNotificationType.CalendarUpdate;
					break;
				default:
					result = SMSNotificationType.None;
					break;
				}
			}
			return result;
		}

		// Token: 0x0400068A RID: 1674
		private const string OnPremisesDeployment = "On-Premises";

		// Token: 0x0400068B RID: 1675
		private static MessageDigestForNonCryptographicPurposes md5Hasher = new MessageDigestForNonCryptographicPurposes();
	}
}
