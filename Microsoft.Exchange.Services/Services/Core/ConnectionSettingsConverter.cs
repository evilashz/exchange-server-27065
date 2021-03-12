using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Imap;
using Microsoft.Exchange.Connections.Pop;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery.Connections;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002AE RID: 686
	internal abstract class ConnectionSettingsConverter
	{
		// Token: 0x06001261 RID: 4705 RVA: 0x0005A61A File Offset: 0x0005881A
		public static void UpdateNewSyncRequestCmdlet(Microsoft.Exchange.Data.SmtpAddress email, ConnectionSettings connectionSettings, NewSyncRequest newSyncRequest)
		{
			ConnectionSettingsConverter.GetConversionHelper(connectionSettings).UpdateNewSyncRequestCmdlet((string)email, connectionSettings.IncomingConnectionSettings, connectionSettings.OutgoingConnectionSettings, newSyncRequest);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0005A63A File Offset: 0x0005883A
		public static void UpdateSetSyncRequestCmdlet(ConnectionSettings connectionSettings, SetSyncRequest setSyncRequest)
		{
			ConnectionSettingsConverter.GetConversionHelper(connectionSettings).UpdateSetSyncRequestCmdlet(connectionSettings.IncomingConnectionSettings, connectionSettings.OutgoingConnectionSettings, setSyncRequest);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0005A654 File Offset: 0x00058854
		public static ConnectionSettingsInfo BuildPublicRepresentation(ConnectionSettings connectionSettings)
		{
			return ConnectionSettingsConverter.GetConversionHelper(connectionSettings).BuildPublicRepresentation(connectionSettings.IncomingConnectionSettings, connectionSettings.OutgoingConnectionSettings);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0005A66D File Offset: 0x0005886D
		public static ConnectionSettings BuildUpdateConnectionSettings(Fqdn incomingServer, int? incomingPort, string security, string authentication, ConnectionSettings originalSettings)
		{
			return ConnectionSettingsConverter.GetConversionHelper(originalSettings).BuildUpdateConnectionSettings(incomingServer, incomingPort, security, authentication, originalSettings.IncomingConnectionSettings, originalSettings.OutgoingConnectionSettings);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0005A690 File Offset: 0x00058890
		public static ConnectionSettings BuildConnectionSettings(AddAggregatedAccountRequest addAccountRequest)
		{
			if (string.IsNullOrEmpty(addAccountRequest.IncomingProtocol))
			{
				return null;
			}
			ConnectionSettingsConverter conversionHelper = ConnectionSettingsConverter.GetConversionHelper(addAccountRequest.IncomingProtocol, addAccountRequest.OutgoingProtocol);
			return conversionHelper.BuildConnectionSettingsFromRequest(addAccountRequest);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0005A6C8 File Offset: 0x000588C8
		public static ConnectionSettings BuildConnectionSettings(SyncRequestStatistics syncRequest)
		{
			string outSettingsType = "SMTP";
			string inSettingsType;
			switch (syncRequest.SyncProtocol)
			{
			case SyncProtocol.Imap:
				inSettingsType = "IMAP";
				break;
			case SyncProtocol.Eas:
				outSettingsType = null;
				inSettingsType = "EXCHANGEACTIVESYNC";
				break;
			case SyncProtocol.Pop:
				inSettingsType = "POP";
				break;
			default:
				throw new ArgumentException("Could not determine the protocol from the SyncRequest properties.");
			}
			ConnectionSettingsConverter conversionHelper = ConnectionSettingsConverter.GetConversionHelper(inSettingsType, outSettingsType);
			return conversionHelper.BuildConnectionSettingsFromRequest(syncRequest);
		}

		// Token: 0x06001267 RID: 4711
		protected abstract void UpdateNewSyncRequestCmdlet(string email, ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings, NewSyncRequest newSyncRequest);

		// Token: 0x06001268 RID: 4712
		protected abstract void UpdateSetSyncRequestCmdlet(ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings, SetSyncRequest setSyncRequest);

		// Token: 0x06001269 RID: 4713
		protected abstract ConnectionSettingsInfo BuildPublicRepresentation(ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings);

		// Token: 0x0600126A RID: 4714
		protected abstract ConnectionSettings BuildConnectionSettingsFromRequest(AddAggregatedAccountRequest addAccountRequest);

		// Token: 0x0600126B RID: 4715
		protected abstract ConnectionSettings BuildUpdateConnectionSettings(Fqdn incomingServer, int? incomingPort, string security, string authentication, ProtocolSpecificConnectionSettings inOriginalSettings, SmtpConnectionSettings outOriginalSettings);

		// Token: 0x0600126C RID: 4716
		protected abstract ConnectionSettings BuildConnectionSettingsFromRequest(SyncRequestStatistics syncRequest);

		// Token: 0x0600126D RID: 4717 RVA: 0x0005A730 File Offset: 0x00058930
		protected virtual SendConnectionSettingsInfo BuildPublicRepresentation(SmtpConnectionSettings outSettings)
		{
			return new SendConnectionSettingsInfo
			{
				ServerName = outSettings.ServerName,
				Port = outSettings.Port
			};
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0005A764 File Offset: 0x00058964
		private static ConnectionSettingsConverter GetConversionHelper(ConnectionSettings connectionSettings)
		{
			if (connectionSettings == null)
			{
				throw new ArgumentNullException("connectionSettings", "The connectionSettings argument cannot be null.");
			}
			string inSettingsType = connectionSettings.IncomingConnectionSettings.ConnectionType.ToString();
			string outSettingsType = (connectionSettings.OutgoingConnectionSettings != null) ? connectionSettings.OutgoingConnectionSettings.ConnectionType.ToString() : null;
			return ConnectionSettingsConverter.GetConversionHelper(inSettingsType, outSettingsType);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0005A7C4 File Offset: 0x000589C4
		private static ConnectionSettingsConverter GetConversionHelper(string inSettingsType, string outSettingsType)
		{
			outSettingsType = ((!string.IsNullOrEmpty(outSettingsType)) ? outSettingsType.ToUpperInvariant() : null);
			string a;
			if ((a = inSettingsType.ToUpperInvariant()) != null)
			{
				if (!(a == "OFFICE365"))
				{
					if (!(a == "EXCHANGEACTIVESYNC"))
					{
						if (!(a == "IMAP"))
						{
							if (!(a == "POP"))
							{
								if (a == "SMTP")
								{
									throw new ArgumentException("Smtp is an outgoing protocol. It cannot be used for inSettingsType.");
								}
							}
							else
							{
								if (outSettingsType == null)
								{
									throw new ArgumentNullException("outSettingsType", "PopConnectionSettings are for unidirectional use. The outSettingsType argument must not be null in this case.");
								}
								if (outSettingsType != "SMTP")
								{
									throw new ArgumentException("The outSettingsType argument must be of type Smtp when the inSettingsType is Pop.", "outSettingsType");
								}
								return new ConnectionSettingsConverter.PopSmtpSettingsConverter();
							}
						}
						else
						{
							if (outSettingsType == null)
							{
								throw new ArgumentNullException("outSettingsType", "ImapConnectionSettings are for unidirectional use. The outSettingsType argument must not be null in this case.");
							}
							if (outSettingsType != "SMTP")
							{
								throw new ArgumentException("The outSettingsType argument must be of type Smtp when the inSettingsType is Imap.", "outSettingsType");
							}
							return new ConnectionSettingsConverter.ImapSmtpSettingsConverter();
						}
					}
					else
					{
						if (outSettingsType != null)
						{
							throw new ArgumentException("ExchangeActiveSyncConnectionSettings are for bidirectional use. The outSettingsType argument must be null in this case.", "outSettingsType");
						}
						return new ConnectionSettingsConverter.EasSettingsConverter();
					}
				}
				else
				{
					if (outSettingsType != null)
					{
						throw new ArgumentException("Office365ConnectionSettings are for bidirectional use. The outSettingsType argument must be null in this case.", "outSettingsType");
					}
					return new ConnectionSettingsConverter.Office365SettingsConverter();
				}
			}
			throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Unexpected outSettingsType encountered: {0}.", new object[]
			{
				outSettingsType
			}));
		}

		// Token: 0x020002AF RID: 687
		private class EasSettingsConverter : ConnectionSettingsConverter
		{
			// Token: 0x06001271 RID: 4721 RVA: 0x0005A90C File Offset: 0x00058B0C
			protected override void UpdateNewSyncRequestCmdlet(string email, ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings, NewSyncRequest newSyncRequest)
			{
				ExchangeActiveSyncConnectionSettings exchangeActiveSyncConnectionSettings = (ExchangeActiveSyncConnectionSettings)inSettings;
				newSyncRequest.Eas = new SwitchParameter(true);
				newSyncRequest.RemoteServerName = Fqdn.Parse((!string.IsNullOrEmpty(exchangeActiveSyncConnectionSettings.EndpointAddressOverride)) ? exchangeActiveSyncConnectionSettings.EndpointAddressOverride : ConnectionSettingsConverter.EasSettingsConverter.GetDomainName(email));
			}

			// Token: 0x06001272 RID: 4722 RVA: 0x0005A954 File Offset: 0x00058B54
			protected override void UpdateSetSyncRequestCmdlet(ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings, SetSyncRequest setSyncRequest)
			{
				ExchangeActiveSyncConnectionSettings exchangeActiveSyncConnectionSettings = (ExchangeActiveSyncConnectionSettings)inSettings;
			}

			// Token: 0x06001273 RID: 4723 RVA: 0x0005A960 File Offset: 0x00058B60
			protected override ConnectionSettingsInfo BuildPublicRepresentation(ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings)
			{
				return new ConnectionSettingsInfo
				{
					ConnectionType = ConnectionSettingsInfoType.ExchangeActiveSync,
					ServerName = ((ExchangeActiveSyncConnectionSettings)inSettings).EndpointAddressOverride
				};
			}

			// Token: 0x06001274 RID: 4724 RVA: 0x0005A990 File Offset: 0x00058B90
			protected override ConnectionSettings BuildUpdateConnectionSettings(Fqdn incomingServer, int? incomingPort, string security, string authentication, ProtocolSpecificConnectionSettings inOriginalSettings, SmtpConnectionSettings outOriginalSettings)
			{
				ExchangeActiveSyncConnectionSettings exchangeActiveSyncConnectionSettings = new ExchangeActiveSyncConnectionSettings();
				ExchangeActiveSyncConnectionSettings exchangeActiveSyncConnectionSettings2 = (ExchangeActiveSyncConnectionSettings)inOriginalSettings;
				if (string.Compare(exchangeActiveSyncConnectionSettings.EndpointAddressOverride, exchangeActiveSyncConnectionSettings2.EndpointAddressOverride, StringComparison.OrdinalIgnoreCase) != 0)
				{
					if (incomingServer != null)
					{
						exchangeActiveSyncConnectionSettings.EndpointAddressOverride = incomingServer.ToString();
					}
					else
					{
						exchangeActiveSyncConnectionSettings.EndpointAddressOverride = null;
					}
				}
				return new ConnectionSettings(exchangeActiveSyncConnectionSettings, outOriginalSettings);
			}

			// Token: 0x06001275 RID: 4725 RVA: 0x0005A9E0 File Offset: 0x00058BE0
			protected override ConnectionSettings BuildConnectionSettingsFromRequest(AddAggregatedAccountRequest addAccountRequest)
			{
				ExchangeActiveSyncConnectionSettings exchangeActiveSyncConnectionSettings = new ExchangeActiveSyncConnectionSettings();
				if (!string.IsNullOrEmpty(addAccountRequest.IncomingServer))
				{
					exchangeActiveSyncConnectionSettings.EndpointAddressOverride = addAccountRequest.IncomingServer;
				}
				return new ConnectionSettings(exchangeActiveSyncConnectionSettings, null);
			}

			// Token: 0x06001276 RID: 4726 RVA: 0x0005AA14 File Offset: 0x00058C14
			protected override ConnectionSettings BuildConnectionSettingsFromRequest(SyncRequestStatistics syncRequest)
			{
				ExchangeActiveSyncConnectionSettings exchangeActiveSyncConnectionSettings = new ExchangeActiveSyncConnectionSettings();
				if (!string.IsNullOrEmpty(syncRequest.RemoteServerName))
				{
					exchangeActiveSyncConnectionSettings.EndpointAddressOverride = syncRequest.RemoteServerName;
				}
				return new ConnectionSettings(exchangeActiveSyncConnectionSettings, null);
			}

			// Token: 0x06001277 RID: 4727 RVA: 0x0005AA48 File Offset: 0x00058C48
			private static string GetDomainName(string emailAddress)
			{
				if (string.IsNullOrWhiteSpace(emailAddress))
				{
					return null;
				}
				return new Microsoft.Exchange.Data.SmtpAddress(emailAddress).Domain;
			}
		}

		// Token: 0x020002B0 RID: 688
		private class ImapSmtpSettingsConverter : ConnectionSettingsConverter
		{
			// Token: 0x06001279 RID: 4729 RVA: 0x0005AA78 File Offset: 0x00058C78
			protected override void UpdateNewSyncRequestCmdlet(string email, ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings, NewSyncRequest newSyncRequest)
			{
				ImapConnectionSettings imapConnectionSettings = (ImapConnectionSettings)inSettings;
				newSyncRequest.Imap = new SwitchParameter(true);
				newSyncRequest.RemoteServerName = imapConnectionSettings.ServerName;
				newSyncRequest.RemoteServerPort = imapConnectionSettings.Port;
				newSyncRequest.Security = ConnectionSettingsConverter.ImapSmtpSettingsConverter.ConvertToIMAPSecurityMechanism(imapConnectionSettings.Security);
			}

			// Token: 0x0600127A RID: 4730 RVA: 0x0005AAC8 File Offset: 0x00058CC8
			protected override void UpdateSetSyncRequestCmdlet(ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings, SetSyncRequest setSyncRequest)
			{
				ImapConnectionSettings imapConnectionSettings = (ImapConnectionSettings)inSettings;
				setSyncRequest.RemoteServerName = Fqdn.Parse(imapConnectionSettings.ServerName);
				setSyncRequest.RemoteServerPort = imapConnectionSettings.Port;
				setSyncRequest.Security = ConnectionSettingsConverter.ImapSmtpSettingsConverter.ConvertToIMAPSecurityMechanism(imapConnectionSettings.Security);
			}

			// Token: 0x0600127B RID: 4731 RVA: 0x0005AB14 File Offset: 0x00058D14
			protected override ConnectionSettingsInfo BuildPublicRepresentation(ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings)
			{
				ImapConnectionSettings imapConnectionSettings = (ImapConnectionSettings)inSettings;
				return new ConnectionSettingsInfo
				{
					ConnectionType = ConnectionSettingsInfoType.Imap,
					SendConnectionSettings = this.BuildPublicRepresentation(outSettings),
					ServerName = imapConnectionSettings.ServerName,
					Port = imapConnectionSettings.Port,
					Authentication = ImapHelperMethods.ToStringParameterValue(imapConnectionSettings.Authentication),
					Security = ImapHelperMethods.ToStringParameterValue(imapConnectionSettings.Security)
				};
			}

			// Token: 0x0600127C RID: 4732 RVA: 0x0005AB84 File Offset: 0x00058D84
			protected override ConnectionSettings BuildUpdateConnectionSettings(Fqdn incomingServer, int? incomingPort, string security, string authentication, ProtocolSpecificConnectionSettings inOriginalSettings, SmtpConnectionSettings outOriginalSettings)
			{
				ImapConnectionSettings imapConnectionSettings = (ImapConnectionSettings)inOriginalSettings;
				ImapConnectionSettings incomingSettings = new ImapConnectionSettings((incomingServer != null) ? incomingServer : imapConnectionSettings.ServerName, (incomingPort != null) ? incomingPort.Value : imapConnectionSettings.Port, (!string.IsNullOrEmpty(authentication)) ? ImapHelperMethods.ToImapAuthenticationMechanism(authentication) : imapConnectionSettings.Authentication, (!string.IsNullOrEmpty(security)) ? ImapHelperMethods.ToImapSecurityMechanism(security) : imapConnectionSettings.Security);
				return new ConnectionSettings(incomingSettings, outOriginalSettings);
			}

			// Token: 0x0600127D RID: 4733 RVA: 0x0005ABFC File Offset: 0x00058DFC
			protected override ConnectionSettings BuildConnectionSettingsFromRequest(AddAggregatedAccountRequest addAccountRequest)
			{
				ImapConnectionSettings incomingSettings = new ImapConnectionSettings(Fqdn.Parse(addAccountRequest.IncomingServer), int.Parse(addAccountRequest.IncomingPort), ImapHelperMethods.ToImapAuthenticationMechanism(addAccountRequest.Authentication), ImapHelperMethods.ToImapSecurityMechanism(addAccountRequest.Security));
				SmtpConnectionSettings outgoingSettings = new SmtpConnectionSettings(Fqdn.Parse(addAccountRequest.OutgoingServer), int.Parse(addAccountRequest.OutgoingPort));
				return new ConnectionSettings(incomingSettings, outgoingSettings);
			}

			// Token: 0x0600127E RID: 4734 RVA: 0x0005AC60 File Offset: 0x00058E60
			protected override ConnectionSettings BuildConnectionSettingsFromRequest(SyncRequestStatistics syncRequestStatistics)
			{
				ImapConnectionSettings incomingSettings = new ImapConnectionSettings(Fqdn.Parse(syncRequestStatistics.RemoteServerName), syncRequestStatistics.RemoteServerPort, ConnectionSettingsConverter.ImapSmtpSettingsConverter.ConvertToImapAuthenticationMechanism(syncRequestStatistics.AuthenticationMethod), ConnectionSettingsConverter.ImapSmtpSettingsConverter.ConvertToImapSecurityMechanism(syncRequestStatistics.SecurityMechanism));
				SmtpConnectionSettings outgoingSettings = new SmtpConnectionSettings(Fqdn.Parse("dummy.smtp.srv"), 1);
				return new ConnectionSettings(incomingSettings, outgoingSettings);
			}

			// Token: 0x0600127F RID: 4735 RVA: 0x0005ACB4 File Offset: 0x00058EB4
			public static IMAPSecurityMechanism ConvertToIMAPSecurityMechanism(ImapSecurityMechanism imapSecurityMechanism)
			{
				IMAPSecurityMechanism result;
				switch (imapSecurityMechanism)
				{
				case ImapSecurityMechanism.None:
					result = IMAPSecurityMechanism.None;
					break;
				case ImapSecurityMechanism.Ssl:
					result = IMAPSecurityMechanism.Ssl;
					break;
				case ImapSecurityMechanism.Tls:
					result = IMAPSecurityMechanism.Tls;
					break;
				default:
					throw new NotSupportedException(string.Format("Value {0} is not supported as IMAPSecurityMechanism", imapSecurityMechanism));
				}
				return result;
			}

			// Token: 0x06001280 RID: 4736 RVA: 0x0005ACFC File Offset: 0x00058EFC
			public static ImapSecurityMechanism ConvertToImapSecurityMechanism(IMAPSecurityMechanism imapSecurityMechanism)
			{
				ImapSecurityMechanism result;
				switch (imapSecurityMechanism)
				{
				case IMAPSecurityMechanism.None:
					result = ImapSecurityMechanism.None;
					break;
				case IMAPSecurityMechanism.Ssl:
					result = ImapSecurityMechanism.Ssl;
					break;
				case IMAPSecurityMechanism.Tls:
					result = ImapSecurityMechanism.Tls;
					break;
				default:
					throw new NotSupportedException(string.Format("Value {0} is not supported as ImapSecurityMechanism", imapSecurityMechanism));
				}
				return result;
			}

			// Token: 0x06001281 RID: 4737 RVA: 0x0005AD44 File Offset: 0x00058F44
			public static ImapAuthenticationMechanism ConvertToImapAuthenticationMechanism(AuthenticationMethod? authenticationMethod)
			{
				ImapAuthenticationMechanism result = ImapAuthenticationMechanism.Basic;
				if (authenticationMethod != null)
				{
					switch (authenticationMethod.Value)
					{
					case AuthenticationMethod.Basic:
						return ImapAuthenticationMechanism.Basic;
					case AuthenticationMethod.Ntlm:
						return ImapAuthenticationMechanism.Ntlm;
					}
					throw new NotSupportedException(string.Format("Value {0} is not supported as ImapAuthenticationMechanism", authenticationMethod.Value));
				}
				return result;
			}
		}

		// Token: 0x020002B1 RID: 689
		private class Office365SettingsConverter : ConnectionSettingsConverter
		{
			// Token: 0x06001283 RID: 4739 RVA: 0x0005ADA9 File Offset: 0x00058FA9
			protected override void UpdateNewSyncRequestCmdlet(string email, ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings, NewSyncRequest newSyncRequest)
			{
				throw new NotSupportedException("We do not create SyncRequests for Office365ConnectionSettings.");
			}

			// Token: 0x06001284 RID: 4740 RVA: 0x0005ADB5 File Offset: 0x00058FB5
			protected override void UpdateSetSyncRequestCmdlet(ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings, SetSyncRequest setSyncRequest)
			{
				throw new NotSupportedException("We do not create SyncRequests for Office365ConnectionSettings.");
			}

			// Token: 0x06001285 RID: 4741 RVA: 0x0005ADC4 File Offset: 0x00058FC4
			protected override ConnectionSettingsInfo BuildPublicRepresentation(ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings)
			{
				Office365ConnectionSettings office365ConnectionSettings = (Office365ConnectionSettings)inSettings;
				return new ConnectionSettingsInfo
				{
					ConnectionType = ConnectionSettingsInfoType.Office365,
					Office365UserFound = (office365ConnectionSettings.AdUser != null)
				};
			}

			// Token: 0x06001286 RID: 4742 RVA: 0x0005ADF9 File Offset: 0x00058FF9
			protected override ConnectionSettings BuildUpdateConnectionSettings(Fqdn incomingServer, int? incomingPort, string security, string authentication, ProtocolSpecificConnectionSettings inOriginalSettings, SmtpConnectionSettings outOriginalSettings)
			{
				throw new NotImplementedException("The user cannot specify Office365ConnectionSettings when calling the AggregatedAccount service commands so this operation is not necessary.");
			}

			// Token: 0x06001287 RID: 4743 RVA: 0x0005AE05 File Offset: 0x00059005
			protected override ConnectionSettings BuildConnectionSettingsFromRequest(AddAggregatedAccountRequest addAccountRequest)
			{
				throw new NotImplementedException("The user cannot specify Office365ConnectionSettings when calling the AggregatedAccount service commands.");
			}

			// Token: 0x06001288 RID: 4744 RVA: 0x0005AE11 File Offset: 0x00059011
			protected override ConnectionSettings BuildConnectionSettingsFromRequest(SyncRequestStatistics syncRequest)
			{
				throw new NotSupportedException("We do not create SyncRequests for Office365ConnectionSettings.");
			}
		}

		// Token: 0x020002B2 RID: 690
		private class PopSmtpSettingsConverter : ConnectionSettingsConverter
		{
			// Token: 0x0600128A RID: 4746 RVA: 0x0005AE28 File Offset: 0x00059028
			protected override void UpdateNewSyncRequestCmdlet(string email, ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings, NewSyncRequest newSyncRequest)
			{
				PopConnectionSettings popConnectionSettings = (PopConnectionSettings)inSettings;
				newSyncRequest.RemoteServerName = popConnectionSettings.ServerName;
				newSyncRequest.RemoteServerPort = popConnectionSettings.Port;
				newSyncRequest.Security = ConnectionSettingsConverter.PopSmtpSettingsConverter.ConvertToIMAPSecurityMechanism(popConnectionSettings.Security);
				newSyncRequest.Pop = new SwitchParameter(true);
			}

			// Token: 0x0600128B RID: 4747 RVA: 0x0005AE78 File Offset: 0x00059078
			protected override void UpdateSetSyncRequestCmdlet(ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings, SetSyncRequest setSyncRequest)
			{
				PopConnectionSettings popConnectionSettings = (PopConnectionSettings)inSettings;
				setSyncRequest.RemoteServerName = popConnectionSettings.ServerName;
				setSyncRequest.RemoteServerPort = popConnectionSettings.Port;
				setSyncRequest.Security = ConnectionSettingsConverter.PopSmtpSettingsConverter.ConvertToIMAPSecurityMechanism(popConnectionSettings.Security);
			}

			// Token: 0x0600128C RID: 4748 RVA: 0x0005AEBC File Offset: 0x000590BC
			protected override ConnectionSettingsInfo BuildPublicRepresentation(ProtocolSpecificConnectionSettings inSettings, SmtpConnectionSettings outSettings)
			{
				PopConnectionSettings popConnectionSettings = (PopConnectionSettings)inSettings;
				return new ConnectionSettingsInfo
				{
					ConnectionType = ConnectionSettingsInfoType.Pop,
					SendConnectionSettings = this.BuildPublicRepresentation(outSettings),
					ServerName = popConnectionSettings.ServerName,
					Port = popConnectionSettings.Port,
					Authentication = PopHelperMethods.ToStringParameterValue(popConnectionSettings.Authentication),
					Security = PopHelperMethods.ToStringParameterValue(popConnectionSettings.Security)
				};
			}

			// Token: 0x0600128D RID: 4749 RVA: 0x0005AF2C File Offset: 0x0005912C
			protected override ConnectionSettings BuildUpdateConnectionSettings(Fqdn incomingServer, int? incomingPort, string security, string authentication, ProtocolSpecificConnectionSettings inOriginalSettings, SmtpConnectionSettings outOriginalSettings)
			{
				PopConnectionSettings popConnectionSettings = (PopConnectionSettings)inOriginalSettings;
				PopConnectionSettings incomingSettings = new PopConnectionSettings((incomingServer != null) ? incomingServer : popConnectionSettings.ServerName, (incomingPort != null) ? incomingPort.Value : popConnectionSettings.Port, (!string.IsNullOrEmpty(authentication)) ? PopHelperMethods.ToPopAuthenticationMechanism(authentication) : popConnectionSettings.Authentication, (!string.IsNullOrEmpty(security)) ? PopHelperMethods.ToPopSecurityMechanism(security) : popConnectionSettings.Security);
				return new ConnectionSettings(incomingSettings, outOriginalSettings);
			}

			// Token: 0x0600128E RID: 4750 RVA: 0x0005AFA4 File Offset: 0x000591A4
			protected override ConnectionSettings BuildConnectionSettingsFromRequest(AddAggregatedAccountRequest addAccountRequest)
			{
				PopConnectionSettings incomingSettings = new PopConnectionSettings(Fqdn.Parse(addAccountRequest.IncomingServer), int.Parse(addAccountRequest.IncomingPort), PopHelperMethods.ToPopAuthenticationMechanism(addAccountRequest.Authentication), PopHelperMethods.ToPopSecurityMechanism(addAccountRequest.Security));
				SmtpConnectionSettings outgoingSettings = new SmtpConnectionSettings(Fqdn.Parse(addAccountRequest.OutgoingServer), int.Parse(addAccountRequest.OutgoingPort));
				return new ConnectionSettings(incomingSettings, outgoingSettings);
			}

			// Token: 0x0600128F RID: 4751 RVA: 0x0005B008 File Offset: 0x00059208
			protected override ConnectionSettings BuildConnectionSettingsFromRequest(SyncRequestStatistics syncRequestStatistics)
			{
				PopConnectionSettings incomingSettings = new PopConnectionSettings(Fqdn.Parse(syncRequestStatistics.RemoteServerName), syncRequestStatistics.RemoteServerPort, ConnectionSettingsConverter.PopSmtpSettingsConverter.ConvertToPop3AuthenticationMechanism(syncRequestStatistics.AuthenticationMethod), ConnectionSettingsConverter.PopSmtpSettingsConverter.ConvertToPop3SecurityMechanism(syncRequestStatistics.SecurityMechanism));
				SmtpConnectionSettings outgoingSettings = new SmtpConnectionSettings(Fqdn.Parse("dummy.smtp.srv"), 1);
				return new ConnectionSettings(incomingSettings, outgoingSettings);
			}

			// Token: 0x06001290 RID: 4752 RVA: 0x0005B05C File Offset: 0x0005925C
			public static IMAPSecurityMechanism ConvertToIMAPSecurityMechanism(Pop3SecurityMechanism pop3SecurityMechanism)
			{
				IMAPSecurityMechanism result;
				switch (pop3SecurityMechanism)
				{
				case Pop3SecurityMechanism.None:
					result = IMAPSecurityMechanism.None;
					break;
				case Pop3SecurityMechanism.Ssl:
					result = IMAPSecurityMechanism.Ssl;
					break;
				case Pop3SecurityMechanism.Tls:
					result = IMAPSecurityMechanism.Tls;
					break;
				default:
					throw new NotSupportedException(string.Format("Value {0} is not supported as IMAPSecurityMechanism", pop3SecurityMechanism));
				}
				return result;
			}

			// Token: 0x06001291 RID: 4753 RVA: 0x0005B0A4 File Offset: 0x000592A4
			public static Pop3SecurityMechanism ConvertToPop3SecurityMechanism(IMAPSecurityMechanism imapSecurityMechanism)
			{
				Pop3SecurityMechanism result;
				switch (imapSecurityMechanism)
				{
				case IMAPSecurityMechanism.None:
					result = Pop3SecurityMechanism.None;
					break;
				case IMAPSecurityMechanism.Ssl:
					result = Pop3SecurityMechanism.Ssl;
					break;
				case IMAPSecurityMechanism.Tls:
					result = Pop3SecurityMechanism.Tls;
					break;
				default:
					throw new NotSupportedException(string.Format("Value {0} is not supported as Pop3SecurityMechanism", imapSecurityMechanism));
				}
				return result;
			}

			// Token: 0x06001292 RID: 4754 RVA: 0x0005B0EC File Offset: 0x000592EC
			public static Pop3AuthenticationMechanism ConvertToPop3AuthenticationMechanism(AuthenticationMethod? authenticationMethod)
			{
				Pop3AuthenticationMechanism result = Pop3AuthenticationMechanism.Basic;
				if (authenticationMethod != null)
				{
					AuthenticationMethod value = authenticationMethod.Value;
					if (value != AuthenticationMethod.Basic)
					{
						throw new NotSupportedException(string.Format("Value {0} is not supported as Pop3AuthenticationMechanism", authenticationMethod.Value));
					}
					result = Pop3AuthenticationMechanism.Basic;
				}
				return result;
			}
		}
	}
}
