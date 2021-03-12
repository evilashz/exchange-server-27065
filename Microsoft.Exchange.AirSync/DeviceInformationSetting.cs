using System;
using System.Globalization;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Configuration.SQM;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200006D RID: 109
	internal class DeviceInformationSetting : SettingsBase
	{
		// Token: 0x060005C4 RID: 1476 RVA: 0x000222DA File Offset: 0x000204DA
		internal DeviceInformationSetting(XmlNode request, XmlNode response, Command command, ProtocolLogger protocolLogger) : base(request, response, protocolLogger)
		{
			this.command = command;
			this.mailboxSession = this.command.MailboxSession;
			this.syncStateStorage = this.command.SyncStateStorage;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00022318 File Offset: 0x00020518
		public override void Execute()
		{
			using (this.command.Context.Tracker.Start(TimeId.DeviceInfoExecute))
			{
				try
				{
					XmlNode firstChild = base.Request.FirstChild;
					string localName;
					if ((localName = firstChild.LocalName) != null && localName == "Set")
					{
						this.ProcessSet(firstChild);
					}
					else
					{
						this.status = SettingsBase.ErrorCode.ProtocolError;
					}
					this.ReportStatus();
				}
				catch (Exception exception)
				{
					if (!this.ProcessException(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000223B4 File Offset: 0x000205B4
		private void ReportStatus()
		{
			XmlNode xmlNode = base.Response.OwnerDocument.CreateElement("Status", "Settings:");
			int num = (int)this.status;
			xmlNode.InnerText = num.ToString(CultureInfo.InvariantCulture);
			base.Response.AppendChild(xmlNode);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00022404 File Offset: 0x00020604
		private bool ProcessException(Exception exception)
		{
			bool result;
			using (this.command.Context.Tracker.Start(TimeId.DeviceInfoProcessException))
			{
				Command.CurrentCommand.PartialFailure = true;
				if (exception is FormatException)
				{
					base.ProtocolLogger.AppendValue(ProtocolLoggerData.Error, "DIS:FormatException");
					this.status = SettingsBase.ErrorCode.ProtocolError;
					this.ReportStatus();
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00022480 File Offset: 0x00020680
		private void ProcessSet(XmlNode setNode)
		{
			using (this.command.Context.Tracker.Start(TimeId.DeviceInfoProcessSet))
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Processing DeviceInformation - Set");
				foreach (object obj in setNode.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string innerText = xmlNode.InnerText;
					string localName;
					switch (localName = xmlNode.LocalName)
					{
					case "Model":
						this.deviceModel = DeviceClassCache.NormalizeDeviceClass(innerText);
						break;
					case "IMEI":
						this.deviceImei = innerText;
						break;
					case "FriendlyName":
						this.deviceFriendlyName = innerText;
						break;
					case "OS":
						this.deviceOS = innerText;
						break;
					case "OSLanguage":
						this.deviceOSLanguage = innerText;
						break;
					case "PhoneNumber":
						this.devicePhoneNumber = innerText;
						break;
					case "UserAgent":
						this.deviceUserAgent = innerText;
						break;
					case "EnableOutboundSMS":
					{
						string a;
						if (this.command.User.IsConsumerOrganizationUser)
						{
							this.deviceEnableOutboundSMS = false;
						}
						else if ((a = innerText) != null)
						{
							if (!(a == "0"))
							{
								if (a == "1")
								{
									this.deviceEnableOutboundSMS = true;
								}
							}
							else
							{
								this.deviceEnableOutboundSMS = false;
							}
						}
						break;
					}
					case "MobileOperator":
						this.deviceMobileOperator = innerText;
						break;
					case "Annotations":
						this.command.RequestAnnotations.ParseWLAnnotations(xmlNode, "DeviceInformation");
						break;
					}
				}
				if (this.command.RequestAnnotations.ContainsAnnotation("CreateChatsFolder", "DeviceInformation"))
				{
					this.CreateSmsAndChatsSyncFolder();
				}
				bool flag = false;
				GlobalInfo globalInfo = this.command.GlobalInfo;
				globalInfo.DeviceModel = this.deviceModel;
				globalInfo.DeviceImei = this.deviceImei;
				globalInfo.DeviceFriendlyName = this.deviceFriendlyName;
				globalInfo.UserAgent = this.deviceUserAgent;
				string text;
				if (this.command.Context.Request.DeviceIdentity.DeviceType.ToUpper().Contains("SAMSUNG") && this.command.TryParseDeviceOSFromUserAgent(out text))
				{
					this.deviceOS = text;
				}
				globalInfo.DeviceOS = this.deviceOS;
				globalInfo.DeviceOSLanguage = this.deviceOSLanguage;
				globalInfo.DevicePhoneNumber = this.devicePhoneNumber;
				string text2 = string.IsNullOrEmpty(this.devicePhoneNumber) ? globalInfo.DevicePhoneNumberForSms : this.devicePhoneNumber;
				if (string.IsNullOrEmpty(text2))
				{
					text2 = Guid.NewGuid().GetHashCode().ToString("D3", CultureInfo.InvariantCulture);
					globalInfo.DevicePhoneNumberForSms = text2;
				}
				else
				{
					flag |= (string.Compare(text2, globalInfo.DevicePhoneNumberForSms, StringComparison.Ordinal) != 0);
				}
				flag |= (this.deviceEnableOutboundSMS != globalInfo.DeviceEnableOutboundSMS);
				globalInfo.DeviceMobileOperator = this.deviceMobileOperator;
				globalInfo.DeviceInformationReceived = true;
				SmsSqmDataPointHelper.AddDeviceInfoReceivedDataPoint(SmsSqmSession.Instance, this.mailboxSession.MailboxOwner.ObjectId, this.mailboxSession.MailboxOwner.LegacyDn, this.command.Request.DeviceIdentity.DeviceType, this.command.Request.VersionString);
				if (flag)
				{
					try
					{
						using (VersionedXmlDataProvider versionedXmlDataProvider = new VersionedXmlDataProvider(this.mailboxSession))
						{
							TextMessagingAccount textMessagingAccount = (TextMessagingAccount)versionedXmlDataProvider.Read<TextMessagingAccount>(this.mailboxSession.MailboxOwner.ObjectId);
							IRecipientSession adrecipientSession = this.mailboxSession.GetADRecipientSession(false, ConsistencyMode.FullyConsistent);
							ADRecipient adrecipient = adrecipientSession.Read(this.mailboxSession.MailboxOwner.ObjectId);
							this.command.Context.ProtocolLogger.SetValue(ProtocolLoggerData.DomainController, adrecipientSession.LastUsedDc);
							if (adrecipient == null)
							{
								throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.ServerError, new LocalizedString("Cannot find AD Recipient correlated to the text messaging account"), false)
								{
									ErrorStringForProtocolLogger = "NoUserAccountForSms"
								};
							}
							E164Number e164Number = null;
							E164Number.TryParse(text2, out e164Number);
							if (this.deviceEnableOutboundSMS)
							{
								if (e164Number == null)
								{
									throw new AirSyncPermanentException(StatusCode.Sync_ServerError, new LocalizedString(string.Format("Cannot parse phone number {0} into a E164 number.", text2)), false)
									{
										ErrorStringForProtocolLogger = "BadSmsPhoneNum"
									};
								}
								bool notificationEnabled = textMessagingAccount.NotificationPhoneNumber != null && textMessagingAccount.NotificationPhoneNumberVerified;
								textMessagingAccount.SetEasEnabled(e164Number, this.syncStateStorage.DeviceIdentity.Protocol, this.syncStateStorage.DeviceIdentity.DeviceType, this.syncStateStorage.DeviceIdentity.DeviceId, this.deviceFriendlyName);
								TextMessagingHelper.SaveTextMessagingAccount(textMessagingAccount, versionedXmlDataProvider, adrecipient, adrecipientSession);
								this.command.Context.ProtocolLogger.SetValue(ProtocolLoggerData.DomainController, adrecipientSession.LastUsedDc);
								SmsSqmDataPointHelper.AddEasConfigurationDataPoint(SmsSqmSession.Instance, this.mailboxSession.MailboxOwner.ObjectId, this.mailboxSession.MailboxOwner.LegacyDn, this.command.Request.DeviceIdentity.DeviceType, notificationEnabled, this.command.Request.VersionString);
							}
							else if (textMessagingAccount.EasEnabled && textMessagingAccount.EasPhoneNumber == e164Number)
							{
								textMessagingAccount.SetEasDisabled();
								TextMessagingHelper.SaveTextMessagingAccount(textMessagingAccount, versionedXmlDataProvider, adrecipient, adrecipientSession);
								this.command.Context.ProtocolLogger.SetValue(ProtocolLoggerData.DomainController, adrecipientSession.LastUsedDc);
							}
						}
						globalInfo.DevicePhoneNumberForSms = text2;
						globalInfo.DeviceEnableOutboundSMS = this.deviceEnableOutboundSMS;
					}
					catch (StoragePermanentException innerException)
					{
						throw new AirSyncPermanentException(StatusCode.ServerError, new LocalizedString("Server Error when trying to update SMS settings."), innerException, false)
						{
							ErrorStringForProtocolLogger = "SmsSettingsSaveError"
						};
					}
				}
				this.OutputToIISLog();
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00022B2C File Offset: 0x00020D2C
		private void CreateSmsAndChatsSyncFolder()
		{
			if (this.mailboxSession.GetDefaultFolderId(DefaultFolderType.SmsAndChatsSync) == null && this.mailboxSession.CreateDefaultFolder(DefaultFolderType.SmsAndChatsSync) == null)
			{
				throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.ServerError, new LocalizedString("Failed to create the SmsAndChatsSync folder"), false)
				{
					ErrorStringForProtocolLogger = "CreateSmsAndChatsSyncFolderFailure"
				};
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00022B80 File Offset: 0x00020D80
		private void OutputToIISLog()
		{
			base.ProtocolLogger.SetTrimmedValue(ProtocolLoggerData.DeviceInfoModel, this.deviceModel, 50);
			base.ProtocolLogger.SetTrimmedValue(ProtocolLoggerData.DeviceInfoIMEI, this.deviceImei, 50);
			base.ProtocolLogger.SetTrimmedValue(ProtocolLoggerData.DeviceInfoFriendlyName, this.deviceFriendlyName, 50);
			base.ProtocolLogger.SetTrimmedValue(ProtocolLoggerData.DeviceInfoOS, this.deviceOS, 50);
			base.ProtocolLogger.SetTrimmedValue(ProtocolLoggerData.DeviceInfoOSLanguage, this.deviceOSLanguage, 50);
			base.ProtocolLogger.SetTrimmedValue(ProtocolLoggerData.DeviceInfoUserAgent, this.deviceUserAgent, 50);
			base.ProtocolLogger.SetValue(ProtocolLoggerData.DeviceInfoEnableOutboundSMS, this.deviceEnableOutboundSMS ? "1" : "0");
			base.ProtocolLogger.SetTrimmedValue(ProtocolLoggerData.DeviceInfoMobileOperator, this.deviceMobileOperator, 50);
		}

		// Token: 0x040003FE RID: 1022
		internal const int MaxParamLength = 50;

		// Token: 0x040003FF RID: 1023
		internal const string DeviceInfoAnnotationManagerGroupName = "DeviceInformation";

		// Token: 0x04000400 RID: 1024
		private SettingsBase.ErrorCode status = SettingsBase.ErrorCode.Success;

		// Token: 0x04000401 RID: 1025
		private MailboxSession mailboxSession;

		// Token: 0x04000402 RID: 1026
		private SyncStateStorage syncStateStorage;

		// Token: 0x04000403 RID: 1027
		private string deviceModel;

		// Token: 0x04000404 RID: 1028
		private string deviceImei;

		// Token: 0x04000405 RID: 1029
		private string deviceFriendlyName;

		// Token: 0x04000406 RID: 1030
		private string deviceOS;

		// Token: 0x04000407 RID: 1031
		private string deviceOSLanguage;

		// Token: 0x04000408 RID: 1032
		private string devicePhoneNumber;

		// Token: 0x04000409 RID: 1033
		private string deviceUserAgent;

		// Token: 0x0400040A RID: 1034
		private bool deviceEnableOutboundSMS;

		// Token: 0x0400040B RID: 1035
		private string deviceMobileOperator;

		// Token: 0x0400040C RID: 1036
		private Command command;
	}
}
