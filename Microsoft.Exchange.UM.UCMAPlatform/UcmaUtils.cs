using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;
using Microsoft.Exchange.UM.UMCommon.FaultInjection;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Collaboration.AudioVideo;
using Microsoft.Rtc.Signaling;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Recognition.SrgsGrammar;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x0200004A RID: 74
	internal class UcmaUtils
	{
		// Token: 0x06000356 RID: 854 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		public static string GetUserAgent()
		{
			return string.Format(CultureInfo.InvariantCulture, "MSExchangeUM/{0}", new object[]
			{
				FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion
			});
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000DBE3 File Offset: 0x0000BDE3
		public static string GetOwnerUri()
		{
			return "sip:MSExchangeUM@" + Utils.GetOwnerHostFqdn();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
		public static string GetTlsError(TlsFailureException e)
		{
			ValidateArgument.NotNull(e, "TlsFailureException");
			switch (e.FailureReason)
			{
			case 0:
				return Strings.TlsOther(e.ErrorCode, e.Message);
			case 1:
				return Strings.TlsLocalCertificateNotFound(e.ErrorCode, e.Message);
			case 2:
				return Strings.TlsUntrustedRemoteCertificate(e.ErrorCode, e.Message);
			case 3:
				return Strings.TlsIncorrectNameInRemoteCertificate(e.ErrorCode, e.Message);
			case 4:
				return Strings.TlsCertificateExpired(e.ErrorCode, e.Message);
			case 5:
				return Strings.TlsTlsNegotiationFailure(e.ErrorCode, e.Message);
			case 6:
				return Strings.TlsRemoteDisconnected(e.ErrorCode, e.Message);
			case 7:
				return Strings.TlsRemoteCertificateRevoked(e.ErrorCode, e.Message);
			case 8:
				return Strings.TlsRemoteCertificateInvalidUsage(e.ErrorCode, e.Message);
			default:
				return e.Message;
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000DD17 File Offset: 0x0000BF17
		public static CollaborationPlatform GetTCPPlatform(int port, AddressFamilyHint addressFamilyHint, bool optimizeForNoMediaHandling, DiagnosticHelper diag, IPAddress ipAddress = null)
		{
			return new CollaborationPlatform(UcmaUtils.GetServerPlatformSettings(1, diag, port, addressFamilyHint, optimizeForNoMediaHandling, ipAddress));
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000DD2C File Offset: 0x0000BF2C
		public static CollaborationPlatform GetTLSPlatform(int port, AddressFamilyHint addressFamilyHint, bool optimizeForNoMediaHandling, DiagnosticHelper diag, EventHandler<ErrorEventArgs> tlsFailureHandler, IPAddress ipAddress = null)
		{
			ValidateArgument.NotNull(diag, "DiagnosticHelper");
			ValidateArgument.NotNull(tlsFailureHandler, "tlsFailureHandler");
			ServerPlatformSettings serverPlatformSettings = UcmaUtils.GetServerPlatformSettings(2, diag, port, addressFamilyHint, optimizeForNoMediaHandling, ipAddress);
			CollaborationPlatform collaborationPlatform = new CollaborationPlatform(serverPlatformSettings);
			RealTimeServerTlsConnectionManager realTimeServerTlsConnectionManager = (RealTimeServerTlsConnectionManager)collaborationPlatform.ConnectionManager;
			realTimeServerTlsConnectionManager.DnsLoadBalancingDisabled = false;
			realTimeServerTlsConnectionManager.IncomingTlsNegotiationFailed += tlsFailureHandler;
			return collaborationPlatform;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000DD80 File Offset: 0x0000BF80
		public static InfoMessage.PlatformMessageReceivedEventArgs GetPlatformMessageReceivedEventArgs(UcmaCallInfo callInfo, MessageReceivedEventArgs e)
		{
			InfoMessage infoMessage = new InfoMessage();
			infoMessage.Body = e.GetBody();
			infoMessage.ContentType = e.ContentType;
			foreach (SignalingHeader signalingHeader in e.RequestData.SignalingHeaders)
			{
				infoMessage.Headers[signalingHeader.Name] = signalingHeader.GetValue();
			}
			return new InfoMessage.PlatformMessageReceivedEventArgs(callInfo, infoMessage, e.MessageType == 2);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000DE10 File Offset: 0x0000C010
		public static void StartPlatform(CollaborationPlatform platform)
		{
			ValidateArgument.NotNull(platform, "CollaborationPlatform");
			IAsyncResult asyncResult = platform.BeginStartup(null, null);
			platform.EndStartup(asyncResult);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000DE38 File Offset: 0x0000C038
		public static void StartEndpoint(ApplicationEndpoint endpoint)
		{
			ValidateArgument.NotNull(endpoint, "ApplicationEndpoint");
			IAsyncResult asyncResult = endpoint.BeginEstablish(null, null);
			endpoint.EndEstablish(asyncResult);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000DE64 File Offset: 0x0000C064
		public static void StopPlatform(CollaborationPlatform platform)
		{
			ValidateArgument.NotNull(platform, "CollaborationPlatform");
			IAsyncResult asyncResult = platform.BeginShutdown(null, null);
			platform.EndShutdown(asyncResult);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000DE8C File Offset: 0x0000C08C
		public static void ChangeCertificate(CollaborationPlatform platform)
		{
			ValidateArgument.NotNull(platform, "CollaborationPlatform");
			IAsyncResult asyncResult = platform.BeginChangeCertificate(CertificateUtils.UMCertificate, null, null);
			platform.EndChangeCertificate(asyncResult);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000DEB9 File Offset: 0x0000C0B9
		public static bool IsPortValid(int port)
		{
			return port > 0 && port < 65535;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000DEC9 File Offset: 0x0000C0C9
		public static ConnectionContext CreateConnectionContext(string host, int port)
		{
			return new ConnectionContext(host, port);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000DF18 File Offset: 0x0000C118
		public static void HandleMessageReceived(DiagnosticHelper diagnostics, MessageReceivedEventArgs e, HandleMessageReceivedDelegate handler)
		{
			RealTimeConnection connection = e.Connection;
			if (connection == null)
			{
				diagnostics.Trace("HandleMessageReceived: connection is null. Ignoring message.", new object[0]);
				return;
			}
			int responseCode = 403;
			SignalingHeader[] responseHeaders = null;
			try
			{
				diagnostics.Trace("HandleMessageReceived: Type:{0}. Body:{1}.", new object[]
				{
					e.MessageType,
					e.HasTextBody ? e.TextBody : string.Empty
				});
				if (e.MessageType == 4 || e.MessageType == 2)
				{
					UcmaCallInfo ucmaCallInfo = new UcmaCallInfo(e, connection);
					InfoMessage.PlatformMessageReceivedEventArgs args = UcmaUtils.GetPlatformMessageReceivedEventArgs(ucmaCallInfo, e);
					args.ResponseCode = responseCode;
					PlatformSignalingHeader platformSignalingHeader;
					Exception ex = UcmaUtils.ProcessPlatformRequestAndReportErrors(delegate
					{
						handler(args);
					}, ucmaCallInfo.CallId, out platformSignalingHeader);
					if (ex != null)
					{
						CallRejectedException ex2 = ex as CallRejectedException;
						args.ResponseCode = ((ex2 != null && ex2.Reason.ErrorCode == CallEndingReason.PipelineFull.ErrorCode) ? 503 : 500);
						responseHeaders = new SignalingHeader[]
						{
							new SignalingHeader(platformSignalingHeader.Name, platformSignalingHeader.Value)
						};
					}
					else if (args.ResponseContactUri != null)
					{
						string text = string.Format(CultureInfo.InvariantCulture, "<{0}>", new object[]
						{
							args.ResponseContactUri.ToString()
						});
						responseHeaders = new SignalingHeader[]
						{
							new SignalingHeader("Contact", text)
						};
					}
					else
					{
						platformSignalingHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.SipMessageReceived, null, new object[0]);
						responseHeaders = new SignalingHeader[]
						{
							new SignalingHeader(platformSignalingHeader.Name, platformSignalingHeader.Value)
						};
					}
					responseCode = args.ResponseCode;
				}
			}
			finally
			{
				string signalingHeadersLogString = UcmaUtils.GetSignalingHeadersLogString(responseHeaders);
				diagnostics.Trace("A {0} message was received and processed: From:{1}. To:{2}. Response Code:{3}. Response Headers:{4}.", new object[]
				{
					e.MessageType,
					e.RequestData.FromHeader.Uri,
					e.RequestData.ToHeader.Uri,
					responseCode,
					signalingHeadersLogString
				});
				if (responseCode != 200)
				{
					ExEventLog.EventTuple tuple = (e.MessageType == 4) ? UMEventLogConstants.Tuple_ServiceRequestRejected : UMEventLogConstants.Tuple_OptionsMessageRejected;
					string periodicKey = string.Format(CultureInfo.InvariantCulture, "{0}:{1}:{2}", new object[]
					{
						e.RequestData.FromHeader.Uri,
						e.RequestData.ToHeader.Uri,
						responseCode
					});
					UmGlobals.ExEvent.LogEvent(tuple, periodicKey, new object[]
					{
						CommonUtil.ToEventLogString(e.RequestData.FromHeader.Uri),
						CommonUtil.ToEventLogString(e.RequestData.ToHeader.Uri),
						CommonUtil.ToEventLogString(responseCode),
						CommonUtil.ToEventLogString(signalingHeadersLogString)
					});
				}
				UcmaUtils.CatchRealtimeErrors(delegate
				{
					e.SendResponse(responseCode, null, null, responseHeaders);
				}, diagnostics);
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000E334 File Offset: 0x0000C534
		public static void SetPingInfoErrorResult(PingInfo info, SipResponseData data, Exception ex)
		{
			info.Error = ex;
			info.Diagnostics = string.Empty;
			info.ResponseCode = 0;
			info.ResponseText = string.Empty;
			FailureResponseException ex2 = ex as FailureResponseException;
			if (data != null)
			{
				info.ResponseCode = data.ResponseCode;
				info.ResponseText = data.ResponseText;
				SignalingHeader signalingHeader = data.SignalingHeaders.FirstOrDefault((SignalingHeader x) => x.Name.Equals("ms-diagnostics", StringComparison.OrdinalIgnoreCase));
				info.Diagnostics = ((signalingHeader != null) ? signalingHeader.GetValue() : string.Empty);
				return;
			}
			if (ex2 != null)
			{
				info.ResponseCode = ex2.ResponseData.ResponseCode;
				info.ResponseText = ex2.ResponseData.ResponseText;
				return;
			}
			if (ex.Message != null)
			{
				int num = ex.Message.IndexOf(Environment.NewLine);
				info.ResponseText = ((num > 0) ? ex.Message.Substring(0, num) : ex.Message);
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000E428 File Offset: 0x0000C628
		public static void HandleSIPPeerListChanged(CollaborationPlatform tlsPlatform, DiagnosticHelper diag)
		{
			ValidateArgument.NotNull(tlsPlatform, "TLS CollaborationPlatform");
			ValidateArgument.NotNull(diag, "DiagnosticHelper");
			diag.Trace("UcmaUtils: HandleSIPPeerListChanged updating TrustedDomain list", new object[0]);
			Dictionary<TrustedDomain, bool> dictionary = new Dictionary<TrustedDomain, bool>(16, new UcmaUtils.TrustedDomainComparer());
			foreach (TrustedDomain key in tlsPlatform.GetTrustedDomains())
			{
				dictionary[key] = false;
			}
			foreach (UMSipPeer umsipPeer in SipPeerManager.Instance.GetSecuredSipPeers())
			{
				string text = umsipPeer.Address.ToString();
				TrustedDomainMode trustedDomainMode = umsipPeer.IsOcs ? 0 : 1;
				TrustedDomain key2 = new TrustedDomain(text, trustedDomainMode);
				dictionary[key2] = true;
			}
			foreach (KeyValuePair<TrustedDomain, bool> keyValuePair in dictionary)
			{
				if (!keyValuePair.Value)
				{
					tlsPlatform.RemoveTrustedDomain(keyValuePair.Key);
					diag.Trace("UcmaVoipPlatform removed TrustedDomain {0}:{1}", new object[]
					{
						keyValuePair.Key,
						keyValuePair.Value
					});
				}
			}
			foreach (KeyValuePair<TrustedDomain, bool> keyValuePair2 in dictionary)
			{
				if (keyValuePair2.Value)
				{
					tlsPlatform.AddTrustedDomain(keyValuePair2.Key);
					diag.Trace("UcmaVoipPlatform added/retained TrustedDomain {0}:{1}", new object[]
					{
						keyValuePair2.Key,
						keyValuePair2.Value
					});
				}
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000E684 File Offset: 0x0000C884
		public static Exception CatchRealtimeErrors(GrayException.UserCodeDelegate function, DiagnosticHelper diag)
		{
			ValidateArgument.NotNull(function, "GrayException.UserCodeDelegate");
			ValidateArgument.NotNull(diag, "DiagnosticHelper");
			Exception error = null;
			try
			{
				ExceptionHandling.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						function();
					}
					catch (RealTimeException error2)
					{
						error = error2;
					}
					catch (InvalidOperationException error3)
					{
						error = error3;
					}
					catch (ArgumentException error4)
					{
						error = error4;
					}
				});
			}
			catch (UMGrayException error)
			{
				UMGrayException error5;
				error = error5;
			}
			if (error != null)
			{
				diag.Trace("CatchRealTimeErrors caught e={0}", new object[]
				{
					error
				});
			}
			return error;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000EB30 File Offset: 0x0000CD30
		public static Exception ProcessPlatformRequestAndReportErrors(GrayException.UserCodeDelegate function, string callId, out PlatformSignalingHeader diagnosticHeader)
		{
			ValidateArgument.NotNull(callId, "Call Id");
			ValidateArgument.NotNull(function, "GrayException UserCodeDelegate");
			Exception error = null;
			PlatformSignalingHeader diagHeader = null;
			try
			{
				ExceptionHandling.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						function();
					}
					catch (CallRejectedException ex)
					{
						error = ex;
						diagHeader = ex.DiagnosticHeader;
					}
					catch (CryptographicException error2)
					{
						error = error2;
					}
					catch (InvalidSIPHeaderException ex2)
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_InvalidSipHeader, null, new object[]
						{
							callId,
							CommonUtil.ToEventLogString(ex2)
						});
						error = ex2;
						diagHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.InvalidSIPheader, null, new object[0]);
					}
					catch (FormatException ex3)
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_InvalidSipHeader, null, new object[]
						{
							callId,
							CommonUtil.ToEventLogString(ex3)
						});
						error = ex3;
						diagHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.InvalidRequest, null, new object[0]);
					}
					catch (MessageParsingException ex4)
					{
						error = ex4;
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_InvalidSipHeader, null, new object[]
						{
							callId,
							CommonUtil.ToEventLogString(ex4)
						});
						diagHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.InvalidRequest, null, new object[0]);
					}
					catch (ADTransientException ex5)
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_ADTransientError, null, new object[]
						{
							callId,
							CommonUtil.ToEventLogString(ex5)
						});
						error = ex5;
						diagHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.ADError, ex5.GetType().FullName, new object[0]);
					}
					catch (StorageTransientException ex6)
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_FailedToConnectToMailbox, null, new object[]
						{
							callId,
							CommonUtil.ToEventLogString(ex6)
						});
						error = ex6;
						diagHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.StorageError, ex6.GetType().FullName, new object[0]);
					}
					catch (StoragePermanentException ex7)
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_FailedToConnectToMailbox, null, new object[]
						{
							callId,
							CommonUtil.ToEventLogString(ex7)
						});
						error = ex7;
						diagHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.StorageError, ex7.GetType().FullName, new object[0]);
					}
					catch (ExchangeServerNotFoundException ex8)
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_ADTransientError, null, new object[]
						{
							callId,
							CommonUtil.ToEventLogString(ex8)
						});
						error = ex8;
						diagHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.ADError, ex8.GetType().FullName, new object[0]);
					}
					catch (DataSourceOperationException ex9)
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_ADPermanentError, null, new object[]
						{
							callId,
							CommonUtil.ToEventLogString(ex9)
						});
						error = ex9;
						diagHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.ADError, ex9.GetType().FullName, new object[0]);
					}
					catch (DataValidationException ex10)
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_ADDataError, null, new object[]
						{
							callId,
							CommonUtil.ToEventLogString(ex10)
						});
						error = ex10;
						diagHeader = CallRejectedException.RenderDiagnosticHeader(CallEndingReason.ADError, ex10.GetType().FullName, new object[0]);
					}
				}, new GrayException.IsGrayExceptionDelegate(GrayException.IsGrayException));
			}
			catch (UMGrayException error)
			{
				UMGrayException error3;
				error = error3;
			}
			diagnosticHeader = diagHeader;
			return error;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		public static Grammar CreateGrammar(UMGrammar grammar)
		{
			Grammar result;
			if (string.IsNullOrEmpty(grammar.Script))
			{
				if (grammar.BaseUri != null)
				{
					result = new Grammar(grammar.Path, grammar.RuleName, grammar.BaseUri);
				}
				else
				{
					result = new Grammar(grammar.Path, grammar.RuleName);
				}
			}
			else
			{
				SrgsDocument srgsDocument = new SrgsDocument(grammar.Path);
				foreach (SrgsRule srgsRule in srgsDocument.Rules)
				{
					srgsRule.Elements.Insert(0, new SrgsSemanticInterpretationTag(grammar.Script));
				}
				if (grammar.BaseUri != null)
				{
					result = new Grammar(srgsDocument, grammar.RuleName, grammar.BaseUri);
				}
				else
				{
					result = new Grammar(srgsDocument, grammar.RuleName);
				}
			}
			return result;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
		public static AddressFamilyHint MapIPAddressFamilyToHint(IPAddressFamily ipAddressFamily)
		{
			switch (ipAddressFamily)
			{
			case IPAddressFamily.IPv4Only:
				return 1;
			case IPAddressFamily.IPv6Only:
				return 2;
			case IPAddressFamily.Any:
				return 0;
			default:
				throw new ArgumentException("Unsupported value of IPAddressFamily", "ipAddressFamily");
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		private static string GetSignalingHeadersLogString(IEnumerable<SignalingHeader> headers)
		{
			string result = string.Empty;
			if (headers != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (SignalingHeader signalingHeader in headers)
				{
					stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[]
					{
						signalingHeader.Name,
						signalingHeader.GetValue()
					}));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000ED7C File Offset: 0x0000CF7C
		private static ServerPlatformSettings GetServerPlatformSettings(SipTransportType mode, DiagnosticHelper diag, int port, AddressFamilyHint addressFamilyHint, bool optimizeForNoMediaHandling, IPAddress ipAddress)
		{
			string ownerHostFqdn = Utils.GetOwnerHostFqdn();
			string userAgent = UcmaUtils.GetUserAgent();
			ServerPlatformSettings serverPlatformSettings;
			if (mode == 1)
			{
				diag.Trace("Creating TcpServerPlatformSettings.  Agent={0}, Fqdn={1}, Port={2}", new object[]
				{
					userAgent,
					ownerHostFqdn,
					port
				});
				serverPlatformSettings = new ServerPlatformSettings(userAgent, ownerHostFqdn, port, null);
			}
			else
			{
				diag.Trace("Creating TlsServerPlatformSettings.  Agent={0}, Fqdn={1}, Port={2}, Cert thumbprint={3} Cert SubjectName={4}", new object[]
				{
					userAgent,
					ownerHostFqdn,
					port,
					CertificateUtils.UMCertificate.Thumbprint,
					CertificateUtils.UMCertificate.SubjectName.Name
				});
				serverPlatformSettings = new ServerPlatformSettings(UcmaUtils.GetUserAgent(), Utils.GetOwnerHostFqdn(), port, null, CertificateUtils.UMCertificate);
				serverPlatformSettings.TrustedDomains.Clear();
				foreach (UMSipPeer umsipPeer in SipPeerManager.Instance.GetSecuredSipPeers())
				{
					string text = umsipPeer.Address.ToString();
					TrustedDomainMode trustedDomainMode = umsipPeer.IsOcs ? 0 : 1;
					serverPlatformSettings.TrustedDomains.Add(new TrustedDomain(text, trustedDomainMode));
					diag.Trace("Added TrustedDomain address '{0}', mode '{1}'", new object[]
					{
						text,
						mode
					});
				}
			}
			if (optimizeForNoMediaHandling)
			{
				serverPlatformSettings.DefaultAudioVideoProviderEnabled = false;
			}
			if (ipAddress != null)
			{
				serverPlatformSettings.ListeningIPAddress = ipAddress;
			}
			else
			{
				serverPlatformSettings.OutboundConnectionConfiguration.DefaultAddressFamilyHint = new AddressFamilyHint?(addressFamilyHint);
				bool flag;
				bool flag2;
				Utils.GetLocalIPv4IPv6Support(out flag, out flag2);
				if (flag2 && !flag)
				{
					serverPlatformSettings.ListeningIPAddress = IPAddress.IPv6Any;
				}
			}
			return serverPlatformSettings;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000EF1C File Offset: 0x0000D11C
		private static CallProvisionalResponseOptions CreateProvisionalResponseDiagnosticInfo(string headerValue, DiagnosticHelper diag)
		{
			CallProvisionalResponseOptions callProvisionalResponseOptions = new CallProvisionalResponseOptions();
			string text = "ms-diagnostics-public";
			diag.Trace("{0}:{1}", new object[]
			{
				text,
				headerValue
			});
			callProvisionalResponseOptions.Headers.Add(new SignalingHeader(text, headerValue));
			callProvisionalResponseOptions.ResponseText = "Diagnostics";
			return callProvisionalResponseOptions;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000EF70 File Offset: 0x0000D170
		internal static void SendDiagnosticsInfoCallReceived(AudioVideoCall call, DiagnosticHelper diag)
		{
			diag.Trace("Sending 101 Diagnostic message for ActiveMonitoring client to track call received", new object[0]);
			string headerValue = Util.FormatDiagnosticsInfoCallReceived();
			CallProvisionalResponseOptions callProvisionalResponseOptions = UcmaUtils.CreateProvisionalResponseDiagnosticInfo(headerValue, diag);
			call.SendProvisionalResponse(101, callProvisionalResponseOptions);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		internal static void SendDiagnosticsInfoCallRedirect(AudioVideoCall call, string uri, DiagnosticHelper diag)
		{
			diag.Trace("Sending 101 Diagnostic message for ActiveMonitoring client to track Redirects", new object[0]);
			string headerValue = Util.FormatDiagnosticsInfoRedirect(uri);
			CallProvisionalResponseOptions callProvisionalResponseOptions = UcmaUtils.CreateProvisionalResponseDiagnosticInfo(headerValue, diag);
			call.SendProvisionalResponse(101, callProvisionalResponseOptions);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000EFFC File Offset: 0x0000D1FC
		private static void SendDiagnosticsInfoServerHealth(AudioVideoCall call, DiagnosticHelper diag)
		{
			diag.Trace("Sending 101 Diagnostic message for ActiveMonitoring client to track server health", new object[0]);
			string headerValue = Util.FormatDiagnosticsInfoServerHealth();
			CallProvisionalResponseOptions provisionalResponseOptions = UcmaUtils.CreateProvisionalResponseDiagnosticInfo(headerValue, diag);
			UcmaUtils.CatchRealtimeErrors(delegate
			{
				call.SendProvisionalResponse(101, provisionalResponseOptions);
			}, diag);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000F068 File Offset: 0x0000D268
		private static void DeclineCallWithTimeout(AudioVideoCall call, DiagnosticHelper diag)
		{
			diag.Trace("Sending 504 time out with Diagnostic message for ActiveMonitoring client to track server timeout", new object[0]);
			string text = Util.FormatDiagnosticsInfoCallTimeout();
			string text2 = "ms-diagnostics-public";
			diag.Trace("{0}:{1}", new object[]
			{
				text2,
				text
			});
			CallDeclineOptions options = new CallDeclineOptions();
			options.ResponseCode = 504;
			options.ResponseText = "Server time-out";
			options.Headers.Add(new SignalingHeader(text2, text));
			UcmaUtils.CatchRealtimeErrors(delegate
			{
				call.Decline(options);
			}, diag);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000F14C File Offset: 0x0000D34C
		internal static void CreateDiagnosticsTimers(AudioVideoCall call, Action<Timer> trackTimer, DiagnosticHelper diag)
		{
			Timer obj = UcmaUtils.CreateDiagnosticsTimer(0L, 8000L, call, (AudioVideoCall c) => c.State == 1, delegate(AudioVideoCall c)
			{
				UcmaUtils.SendDiagnosticsInfoServerHealth(c, diag);
			});
			Timer obj2 = UcmaUtils.CreateDiagnosticsTimer(30000L, -1L, call, (AudioVideoCall c) => c.State == 1, delegate(AudioVideoCall c)
			{
				UcmaUtils.DeclineCallWithTimeout(c, diag);
			});
			if (trackTimer != null)
			{
				trackTimer(obj);
				trackTimer(obj2);
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000F234 File Offset: 0x0000D434
		private static Timer CreateDiagnosticsTimer(long dueTime, long period, AudioVideoCall call, Predicate<AudioVideoCall> condition, Action<AudioVideoCall> action)
		{
			return new Timer(delegate(object o)
			{
				AudioVideoCall audioVideoCall = o as AudioVideoCall;
				if (audioVideoCall != null && condition != null && action != null && condition(audioVideoCall))
				{
					action(audioVideoCall);
				}
			}, call, dueTime, period);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000F26C File Offset: 0x0000D46C
		internal static void AddCallDelayFaultInjectionInTestMode(uint faultInjectionLid)
		{
			int num = 0;
			FaultInjectionUtils.FaultInjectChangeValue<int>(faultInjectionLid, ref num);
			if (num > 0)
			{
				Thread.Sleep(TimeSpan.FromSeconds((double)num));
			}
		}

		// Token: 0x0400010C RID: 268
		private const string UserAgentFormat = "MSExchangeUM/{0}";

		// Token: 0x0400010D RID: 269
		private const string OwnerUriPrefix = "sip:MSExchangeUM@";

		// Token: 0x0200004B RID: 75
		private class TrustedDomainComparer : IEqualityComparer<TrustedDomain>
		{
			// Token: 0x06000377 RID: 887 RVA: 0x0000F29B File Offset: 0x0000D49B
			public bool Equals(TrustedDomain x, TrustedDomain y)
			{
				return x.DomainName.Equals(y.DomainName, StringComparison.OrdinalIgnoreCase) && x.DomainMode == y.DomainMode;
			}

			// Token: 0x06000378 RID: 888 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
			public int GetHashCode(TrustedDomain obj)
			{
				string text = obj.DomainName.ToLowerInvariant();
				return text.GetHashCode() ^ obj.DomainMode.GetHashCode();
			}
		}
	}
}
