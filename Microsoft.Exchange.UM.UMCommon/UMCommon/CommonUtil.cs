﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000050 RID: 80
	internal class CommonUtil
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000B4B4 File Offset: 0x000096B4
		internal static ADUser ValidateAndReturnUMDataStorageOrgMbx(List<ADUser> users)
		{
			ValidateArgument.NotNull(users, "users");
			switch (users.Count)
			{
			case 0:
				throw new ObjectNotFoundException(Strings.UMDataStorageMailboxNotFound);
			case 1:
				return users[0];
			default:
				throw new NonUniqueRecipientException(users[0], new NonUniqueAddressError(Strings.MultupleUMDataStorageMailboxFound(users[0].Id.ToString(), users[1].Id.ToString()), users[0].Id, "OrganizationMailbox"));
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B540 File Offset: 0x00009740
		internal static QueryFilter GetCompatibleServersWithRole(VersionEnum version, ServerRole role)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			switch (version)
			{
			case VersionEnum.Compatible:
				list.Add(CommonConstants.UMVersions.CompatibleServersFilter);
				break;
			case VersionEnum.E12Legacy:
				list.Add(CommonConstants.UMVersions.E12LegacyServerFilter);
				break;
			case VersionEnum.E14Legacy:
				list.Add(CommonConstants.UMVersions.E14LegacyServerFilter);
				break;
			default:
				throw new ArgumentException(version.ToString());
			}
			list.Add(new BitMaskAndFilter(ServerSchema.CurrentServerRole, (ulong)role));
			if (role == ServerRole.UnifiedMessaging && (version == VersionEnum.E14Legacy || version == VersionEnum.E12Legacy))
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.Status, ServerStatus.Enabled));
			}
			return new AndFilter(list.ToArray());
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B5E2 File Offset: 0x000097E2
		internal static bool IsServerCompatible(Server server)
		{
			return CommonUtil.IsMajorVersionE15(server.VersionNumber);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B5EF File Offset: 0x000097EF
		internal static bool IsLegacyServer(Server server)
		{
			return CommonUtil.IsMajorVersionE12(server.VersionNumber);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B5FC File Offset: 0x000097FC
		internal static bool IsServerCompatible(int serverVersion)
		{
			return CommonUtil.IsMajorVersionE15(serverVersion);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B604 File Offset: 0x00009804
		internal static bool GetOperatorExtensionForAutoAttendant(UMAutoAttendant aa1, AutoAttendantSettings settings, UMDialPlan dialPlan, bool logEventForMissingOperatorExtension, out PhoneNumber operatorExtension)
		{
			operatorExtension = null;
			UMAutoAttendant umautoAttendant = null;
			if (aa1.DTMFFallbackAutoAttendant != null)
			{
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(aa1.OrganizationId);
				umautoAttendant = iadsystemConfigurationLookup.GetAutoAttendantFromId(aa1.DTMFFallbackAutoAttendant);
			}
			string operatorExtension2 = aa1.OperatorExtension;
			if (!settings.TransferToOperatorEnabled)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, "Operator Transfer Is Disabled in AA {0}.", new object[]
				{
					aa1.Name
				});
				return false;
			}
			string text = null;
			if (!string.IsNullOrEmpty(operatorExtension2))
			{
				text = operatorExtension2;
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, "Found operator number {0} from  autoattendant config {1}.", new object[]
				{
					text,
					aa1.Name
				});
			}
			else if (aa1.SpeechEnabled && umautoAttendant != null && !string.IsNullOrEmpty(umautoAttendant.OperatorExtension))
			{
				text = umautoAttendant.OperatorExtension;
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, "Using operator number {0} from dtmf fallback autoattendant {1}.", new object[]
				{
					text,
					umautoAttendant.Name
				});
			}
			else if (!string.IsNullOrEmpty(dialPlan.OperatorExtension))
			{
				text = dialPlan.OperatorExtension;
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, "Using operator number {0} from dialplan.", new object[]
				{
					text
				});
			}
			bool result;
			if (!PhoneNumber.TryParse(text, out operatorExtension))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, "Did not find any operator number to use. Disabling operator.", new object[0]);
				result = false;
				operatorExtension = null;
				if (logEventForMissingOperatorExtension)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_AAMissingOperatorExtension, null, new object[]
					{
						aa1.Name
					});
				}
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, "Enabling operator with extension: {0}.", new object[]
				{
					text
				});
				result = true;
			}
			return result;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B7A4 File Offset: 0x000099A4
		internal static ExTimeZone GetOwaTimeZone(MailboxSession sess)
		{
			UserConfiguration userConfiguration = null;
			ExTimeZone currentTimeZone = ExTimeZone.CurrentTimeZone;
			ExTimeZone result;
			try
			{
				userConfiguration = sess.UserConfigurationManager.GetMailboxConfiguration("OWA.UserOptions", UserConfigurationTypes.Dictionary);
				IDictionary dictionary = userConfiguration.GetDictionary();
				object obj = dictionary["timezone"];
				if (obj == null || !(obj is string))
				{
					result = currentTimeZone;
				}
				else
				{
					string timeZoneName = (string)obj;
					ExTimeZone exTimeZone = null;
					if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(timeZoneName, out exTimeZone))
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "Could not get timezone: OWA's options are corrupt!", new object[0]);
						result = currentTimeZone;
					}
					else
					{
						result = exTimeZone;
					}
				}
			}
			catch (InvalidDataException)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, null, "Could not get timezone: OWA's options are corrupt!", new object[0]);
				result = currentTimeZone;
			}
			catch (CorruptDataException)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, null, "Could not get timezone: OWA's options are corrupt!", new object[0]);
				result = currentTimeZone;
			}
			catch (InvalidOperationException)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, null, "Could not get timezone: OWA's options are corrupt!", new object[0]);
				result = currentTimeZone;
			}
			catch (ObjectNotFoundException)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, null, "Could not get timezone: OWA's options don't exist!", new object[0]);
				result = currentTimeZone;
			}
			finally
			{
				if (userConfiguration != null)
				{
					userConfiguration.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B8F4 File Offset: 0x00009AF4
		internal static void SetOwaTimeZone(MailboxSession session, string timeZoneKeyName)
		{
			CommonUtil.SetOwaProperty(session, "timezone", timeZoneKeyName);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B904 File Offset: 0x00009B04
		internal static string GetOwaTimeFormat(MailboxSession session)
		{
			string text = null;
			UserConfiguration userConfiguration = null;
			UserConfigurationManager userConfigurationManager = session.UserConfigurationManager;
			try
			{
				userConfiguration = userConfigurationManager.GetMailboxConfiguration("OWA.UserOptions", UserConfigurationTypes.Dictionary);
				IDictionary dictionary = userConfiguration.GetDictionary();
				text = (string)dictionary["timeformat"];
			}
			catch (InvalidDataException)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, null, "Could not get timeformat: OWA's options are corrupted.", new object[0]);
			}
			catch (InvalidCastException)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, null, "Could not get timeformat: OWA's options are corrupted.", new object[0]);
			}
			catch (ObjectNotFoundException)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, null, "Could not get timeformat: OWA's options don't exist!", new object[0]);
			}
			catch (InvalidOperationException ex)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, null, "Could not get timeformat: {0}.", new object[]
				{
					ex
				});
			}
			catch (CorruptDataException ex2)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, null, "Could not get timeformat: {0}.", new object[]
				{
					ex2
				});
			}
			finally
			{
				if (userConfiguration != null)
				{
					userConfiguration.Dispose();
				}
			}
			if (text == null)
			{
				return session.Culture.DateTimeFormat.ShortTimePattern;
			}
			return text;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000BA48 File Offset: 0x00009C48
		internal static void SetOwaTimeFormat(MailboxSession session, string timeFormat)
		{
			CommonUtil.SetOwaProperty(session, "timeformat", timeFormat);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000BA56 File Offset: 0x00009C56
		internal static bool Is24HourTimeFormat(string timePattern)
		{
			return timePattern.IndexOf('H') >= 0;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000BA68 File Offset: 0x00009C68
		internal static void GetStandardTimeFormats(CultureInfo cultureInfo, out string format12Hours, out string format24Hours)
		{
			format12Hours = null;
			format24Hours = null;
			foreach (string text in cultureInfo.DateTimeFormat.GetAllDateTimePatterns('t'))
			{
				if (CommonUtil.Is24HourTimeFormat(text))
				{
					if (format24Hours == null)
					{
						format24Hours = text;
					}
				}
				else if (format12Hours == null)
				{
					format12Hours = text;
				}
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		internal static bool GetEmailOOFStatus(MailboxSession session)
		{
			UserOofSettings userOofSettings = UserOofSettings.GetUserOofSettings(session);
			return userOofSettings.OofState != OofState.Disabled;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		internal static void SetEmailOOFStatus(MailboxSession session, bool enabled, string replyText)
		{
			UserOofSettings userOofSettings = UserOofSettings.GetUserOofSettings(session);
			if ((enabled && userOofSettings.OofState == OofState.Enabled) || (!enabled && userOofSettings.OofState == OofState.Disabled))
			{
				return;
			}
			if (enabled)
			{
				userOofSettings.OofState = OofState.Enabled;
				userOofSettings.InternalReply.Message = replyText;
				userOofSettings.ExternalReply.Message = replyText;
			}
			else
			{
				userOofSettings.OofState = OofState.Disabled;
			}
			userOofSettings.Save(session);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000BB80 File Offset: 0x00009D80
		internal static List<ExTimeZone> GetTimeZonesForLocalTime(TimeSpan targetLocalTime, TimeSpan errorSpan)
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			List<ExTimeZone> list = new List<ExTimeZone>();
			foreach (ExTimeZone exTimeZone in ExTimeZoneEnumerator.Instance)
			{
				TimeSpan timeOfDay = exTimeZone.ConvertDateTime(utcNow).TimeOfDay;
				TimeSpan t;
				if (targetLocalTime >= timeOfDay)
				{
					t = targetLocalTime.Subtract(timeOfDay);
				}
				else
				{
					t = timeOfDay.Subtract(targetLocalTime);
				}
				if (t <= errorSpan)
				{
					list.Add(exTimeZone);
				}
			}
			list.Sort(delegate(ExTimeZone tz1, ExTimeZone tz2)
			{
				if (tz1.TimeZoneInformation.StandardBias < tz2.TimeZoneInformation.StandardBias)
				{
					return -1;
				}
				if (tz1.TimeZoneInformation.StandardBias > tz2.TimeZoneInformation.StandardBias)
				{
					return 1;
				}
				return 0;
			});
			return list;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000BC3C File Offset: 0x00009E3C
		internal static string Base64Serialize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string result = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
				binaryFormatter.Serialize(memoryStream, obj);
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000BC98 File Offset: 0x00009E98
		internal static object Base64Deserialize(string base64String)
		{
			object result = null;
			byte[] buffer = Convert.FromBase64String(base64String);
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
				result = binaryFormatter.Deserialize(memoryStream);
			}
			return result;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		internal static void CopyStream(Stream source, Stream dest)
		{
			byte[] array = new byte[32768];
			int num;
			do
			{
				num = source.Read(array, 0, array.Length);
				if (0 < num)
				{
					dest.Write(array, 0, num);
				}
			}
			while (0 < num);
			dest.Flush();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000BD24 File Offset: 0x00009F24
		internal static string GetBase64StringFromStream(Stream source)
		{
			string result = null;
			if (source.Length > 0L)
			{
				byte[] array = new byte[source.Length];
				int num = 0;
				using (BinaryReader binaryReader = new BinaryReader(source))
				{
					int num3;
					do
					{
						int num2 = Math.Min(4096, array.Length - num);
						num3 = ((num2 > 0) ? binaryReader.Read(array, num, num2) : 0);
						num += num3;
					}
					while (num3 > 0);
				}
				result = Convert.ToBase64String(array, 0, num);
			}
			return result;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000BDAC File Offset: 0x00009FAC
		internal static void CopyBase64StringToSteam(string input, Stream dest)
		{
			byte[] array = Convert.FromBase64String(input);
			dest.SetLength(0L);
			dest.Write(array, 0, array.Length);
			dest.Flush();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000BDDC File Offset: 0x00009FDC
		internal static string ToEventLogString(object obj)
		{
			string text = (obj == null) ? string.Empty : obj.ToString();
			if (text.Length > 4096)
			{
				text = text.Substring(0, 4096);
			}
			return text;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000BE18 File Offset: 0x0000A018
		internal static bool ShouldAllowOVA(ADUser adUser)
		{
			try
			{
				using (ClientSecurityContext clientSecurityContext = CommonUtil.CreateClientSecurityContext(adUser))
				{
					return clientSecurityContext != null;
				}
			}
			catch (Exception ex)
			{
				if (!(ex is SecurityException) && !(ex is AuthzException))
				{
					throw;
				}
				PIIMessage data = PIIMessage.Create(PIIType._User, adUser);
				CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, 0, data, "[CommonUtil::ShouldAllowOVA] Exception encountered.  Recipient: _User, Exception: {0}", new object[]
				{
					ex
				});
			}
			return false;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
		internal static ClientSecurityContext CreateClientSecurityContext(ADUser adUser)
		{
			if (adUser == null)
			{
				throw new ArgumentNullException("adUser");
			}
			SecurityIdentifier sid = adUser.Sid;
			SecurityIdentifier securityIdentifier = RecipientHelper.TryGetMasterAccountSid(adUser);
			SecurityIdentifier securityIdentifier2 = securityIdentifier ?? sid;
			SecurityAccessToken securityAccessToken = new SecurityAccessToken();
			securityAccessToken.UserSid = securityIdentifier2.ToString();
			ClientSecurityContext result;
			try
			{
				PIIMessage[] data = new PIIMessage[]
				{
					PIIMessage.Create(PIIType._User, adUser),
					PIIMessage.Create(PIIType._PII, securityIdentifier2)
				};
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, data, "[CommonUtil::CreateClientSecurityContext] Constructing client security context with AuthzRequireS4ULogon.  Recipient: _User, Sid: _PII", new object[0]);
				result = new ClientSecurityContext(securityAccessToken, AuthzFlags.AuthzRequireS4ULogon);
			}
			catch (Exception ex)
			{
				PIIMessage data2 = PIIMessage.Create(PIIType._User, adUser);
				CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, 0, data2, "[CommonUtil::CreateClientSecurityContext] AuthzException encountered.  Recipient: _User, Exception: {0}", new object[]
				{
					ex
				});
				if (ex.InnerException != null)
				{
					Win32Exception ex2 = ex.InnerException as Win32Exception;
					if (ex2 != null && ex2.NativeErrorCode == 1327)
					{
						return null;
					}
				}
				if (!(securityIdentifier == securityIdentifier2))
				{
					throw;
				}
				result = CommonUtil.CreateClientSecurityContextFromResourceForest(securityIdentifier, sid);
			}
			return result;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
		private static void SetOwaProperty(MailboxSession session, string propertyName, object propertyValue)
		{
			UserConfiguration userConfiguration = null;
			UserConfigurationManager userConfigurationManager = session.UserConfigurationManager;
			try
			{
				try
				{
					userConfiguration = userConfigurationManager.GetMailboxConfiguration("OWA.UserOptions", UserConfigurationTypes.Dictionary);
				}
				catch (ObjectNotFoundException)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "OWA's configuration object doesn't exist -> creating it!", new object[0]);
					userConfiguration = userConfigurationManager.CreateMailboxConfiguration("OWA.UserOptions", UserConfigurationTypes.Dictionary);
				}
				IDictionary dictionary = userConfiguration.GetDictionary();
				dictionary[propertyName] = propertyValue;
				userConfiguration.Save();
			}
			finally
			{
				if (userConfiguration != null)
				{
					userConfiguration.Dispose();
				}
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C048 File Offset: 0x0000A248
		private static bool IsMajorVersionE15(int serverVersion)
		{
			return serverVersion >= Server.E15MinVersion;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000C055 File Offset: 0x0000A255
		private static bool IsMajorVersionE14(int serverVersion)
		{
			return serverVersion >= Server.E14MinVersion && serverVersion < Server.E15MinVersion;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000C069 File Offset: 0x0000A269
		private static bool IsMajorVersionE12(int serverVersion)
		{
			return serverVersion >= Server.E2007MinVersion && serverVersion < Server.E14MinVersion;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000C080 File Offset: 0x0000A280
		private static ClientSecurityContext CreateClientSecurityContextFromResourceForest(SecurityIdentifier masterAccountSid, SecurityIdentifier objectSid)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "[CommonUtil::CreateClientSecurityContextFromResourceForest] Constructing client security context from resource forest.  MasterAccountSid: {0}, Sid: {1}", new object[]
			{
				masterAccountSid,
				objectSid
			});
			SecurityAccessToken securityAccessToken = new SecurityAccessToken();
			securityAccessToken.UserSid = objectSid.ToString();
			using (ClientSecurityContext clientSecurityContext = new ClientSecurityContext(securityAccessToken, AuthzFlags.Default))
			{
				clientSecurityContext.SetSecurityAccessToken(securityAccessToken);
			}
			securityAccessToken.UserSid = masterAccountSid.ToString();
			return new ClientSecurityContext(securityAccessToken, AuthzFlags.AuthzSkipTokenGroups);
		}
	}
}
