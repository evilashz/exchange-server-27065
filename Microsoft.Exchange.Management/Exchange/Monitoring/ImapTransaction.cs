using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200056F RID: 1391
	internal class ImapTransaction : ProtocolTransaction
	{
		// Token: 0x06003111 RID: 12561 RVA: 0x000C7710 File Offset: 0x000C5910
		internal ImapTransaction(ADUser user, string serverName, string password, int port) : base(user, serverName, password, port)
		{
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000C7720 File Offset: 0x000C5920
		internal override void Execute()
		{
			this.currentState = ImapTransaction.State.Disconnected;
			base.LatencyTimer = Stopwatch.StartNew();
			if (!base.IsServiceRunning("MSExchangeIMAP4", base.CasServer))
			{
				base.Verbose(Strings.ServiceNotRunning("MSExchangeIMAP4"));
				base.UpdateTransactionResults(CasTransactionResultEnum.Failure, Strings.ServiceNotRunning("MSExchangeIMAP4"));
				base.CompleteTransaction();
				return;
			}
			this.imapClient = new ImapClient(base.CasServer, base.Port, base.ConnectionType, base.TrustAnySslCertificate);
			this.imapClient.ExceptionReporter = new DataCommunicator.ExceptionReporterDelegate(this.ExceptionReporter);
			this.imapClient.HasLoggedIn = false;
			base.Verbose(Strings.PopImapConnecting);
			this.currentState = ImapTransaction.State.Connecting;
			this.imapClient.Connect(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse));
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000C77F8 File Offset: 0x000C59F8
		private static string[] ParseMessageIDs(string response)
		{
			string[] array = new string[0];
			string[] array2 = Regex.Split(response, "\r\n");
			if (array2 != null && array2.Length > 0)
			{
				string[] array3 = array2[0].Split(new char[]
				{
					' '
				});
				if (array3 != null && array3.Length > 2)
				{
					array = new string[array3.Length - 2];
					Array.Copy(array3, 2, array, 0, array3.Length - 2);
				}
			}
			return array;
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000C785C File Offset: 0x000C5A5C
		private void GetTheResponse(string response, object extraArgs)
		{
			ImapTransaction.State state = this.currentState;
			if (state != ImapTransaction.State.SettingUpSecureStreamForTls)
			{
				switch (state)
				{
				default:
					if (!this.imapClient.IsOkResponse(response))
					{
						this.ReportErrorAndQuit(response);
						return;
					}
					break;
				case ImapTransaction.State.LoggingOff:
				case ImapTransaction.State.Disconnecting:
					break;
				}
			}
			switch (this.currentState)
			{
			case ImapTransaction.State.Disconnected:
			case ImapTransaction.State.Disconnecting:
				return;
			case ImapTransaction.State.Connecting:
				if (base.ConnectionType == ProtocolConnectionType.Tls)
				{
					this.currentState = ImapTransaction.State.NegotiatingTls;
					this.imapClient.StartTls(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse));
					return;
				}
				break;
			case ImapTransaction.State.Connected:
				break;
			case ImapTransaction.State.NegotiatingTls:
				this.currentState = ImapTransaction.State.SettingUpSecureStreamForTls;
				this.imapClient.SetUpSecureStreamForTls(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse));
				return;
			case ImapTransaction.State.SettingUpSecureStreamForTls:
			case ImapTransaction.State.ReadyToLogIn:
				if (base.LightMode)
				{
					this.ReportSuccessAndLogOff();
					return;
				}
				this.currentState = ImapTransaction.State.LoggingOn;
				base.Verbose(Strings.PopImapLoggingOn);
				if (!string.IsNullOrEmpty(base.User.UserPrincipalName))
				{
					this.imapClient.LogOn(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse), base.User.UserPrincipalName, base.Password);
					return;
				}
				this.imapClient.LogOn(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse), base.User.SamAccountName, base.Password);
				return;
			case ImapTransaction.State.LoggingOn:
			{
				this.imapClient.HasLoggedIn = true;
				this.currentState = ImapTransaction.State.SelectingFolder;
				string text = "INBOX";
				base.Verbose(Strings.ImapSelectingFolder(text));
				this.imapClient.SelectFolder(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse), text);
				return;
			}
			case ImapTransaction.State.SelectingFolder:
				this.currentState = ImapTransaction.State.FindingTestMessage;
				base.Verbose(Strings.PopImapSearchForTestMsg(base.MailSubject));
				this.imapClient.FindMessageIDsBySubject(base.MailSubject, new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse));
				return;
			case ImapTransaction.State.FindingTestMessage:
			{
				int num = 0;
				string[] array = ImapTransaction.ParseMessageIDs(response);
				if (array != null && 1 == array.Length)
				{
					num = int.Parse(array[0], CultureInfo.InvariantCulture);
				}
				if (num <= 0)
				{
					base.UpdateTransactionResults(CasTransactionResultEnum.Failure, Strings.ErrorMessageNotFound(base.MailSubject));
					this.LogOffAndDisconnectAfterError();
					return;
				}
				this.currentState = ImapTransaction.State.DeletingTestMessage;
				base.Verbose(Strings.PopImapDeleteMsg(num));
				this.DeleteMessage(num);
				return;
			}
			case ImapTransaction.State.DeletingTestMessage:
				this.currentState = ImapTransaction.State.GettingOlderMessages;
				base.Verbose(Strings.PopImapDeleteOldMsgs);
				this.imapClient.FindMessagesBeforeTodayContainingSubject(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse), "Test-ImapConnectivity-");
				return;
			case ImapTransaction.State.GettingOlderMessages:
			{
				string[] array2 = ImapTransaction.ParseMessageIDs(response);
				if (array2 != null && array2.Length > 0)
				{
					this.currentState = ImapTransaction.State.DeletingOlderMessages;
					this.DeleteMessages(array2);
					return;
				}
				base.Verbose(Strings.PopImapNoMessagesToDelete);
				goto IL_2C3;
			}
			case ImapTransaction.State.DeletingOlderMessages:
				goto IL_2C3;
			case ImapTransaction.State.LoggingOff:
				this.imapClient.HasLoggedIn = false;
				this.currentState = ImapTransaction.State.Disconnecting;
				this.Disconnect();
				return;
			default:
				throw new ArgumentOutOfRangeException(Strings.PopImapErrorUnexpectedValue(this.currentState.ToString()));
			}
			this.currentState = ImapTransaction.State.ReadyToLogIn;
			this.imapClient.Capability(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse));
			return;
			IL_2C3:
			this.ReportSuccessAndLogOff();
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000C7B6E File Offset: 0x000C5D6E
		private void ExceptionReporter(Exception exception)
		{
			base.UpdateTransactionResults(CasTransactionResultEnum.Failure, exception);
			this.Disconnect();
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000C7B7E File Offset: 0x000C5D7E
		private void LogOffAndDisconnectAfterError()
		{
			if (this.imapClient == null)
			{
				this.currentState = ImapTransaction.State.Disconnected;
				base.CompleteTransaction();
				return;
			}
			if (this.imapClient.HasLoggedIn)
			{
				this.LogOff();
				return;
			}
			this.Disconnect();
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000C7BB0 File Offset: 0x000C5DB0
		private void Disconnect()
		{
			base.Verbose(Strings.PopImapDisconnecting);
			this.imapClient.Dispose();
			this.imapClient = null;
			this.currentState = ImapTransaction.State.Disconnected;
			base.CompleteTransaction();
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000C7BE4 File Offset: 0x000C5DE4
		private void DeleteMessage(int messageID)
		{
			this.DeleteMessages(new string[]
			{
				messageID.ToString(CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000C7C0E File Offset: 0x000C5E0E
		private void DeleteMessages(string[] messageIDs)
		{
			this.imapClient.StoreMessages(new DataCommunicator.GetCommandResponseDelegate(this.AfterStoreCommand), messageIDs);
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000C7C28 File Offset: 0x000C5E28
		private void AfterStoreCommand(string response, object extraArgs)
		{
			if (!this.imapClient.IsOkResponse(response))
			{
				this.ReportErrorAndQuit(response);
				return;
			}
			this.imapClient.ExpungeMessages(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse));
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000C7C57 File Offset: 0x000C5E57
		private void ReportSuccessAndLogOff()
		{
			base.LatencyTimer.Stop();
			base.UpdateTransactionResults(CasTransactionResultEnum.Success, null);
			this.LogOff();
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000C7C72 File Offset: 0x000C5E72
		private void LogOff()
		{
			this.currentState = ImapTransaction.State.LoggingOff;
			base.Verbose(Strings.PopImapLoggingOff);
			this.imapClient.LogOff(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse));
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000C7CA4 File Offset: 0x000C5EA4
		private void ReportErrorAndQuit(string response)
		{
			if (response.Contains("Command received in Invalid state."))
			{
				response = string.Format(CultureInfo.InvariantCulture, "{0}\r\n{1}", new object[]
				{
					response,
					Strings.CheckServerConfiguration
				});
			}
			base.UpdateTransactionResults(CasTransactionResultEnum.Failure, Strings.ErrorOfProtocolCommand("IMAP", response));
			this.LogOffAndDisconnectAfterError();
		}

		// Token: 0x040022B8 RID: 8888
		private const string EndOfLine = "\r\n";

		// Token: 0x040022B9 RID: 8889
		private static readonly char[] space = new char[]
		{
			' '
		};

		// Token: 0x040022BA RID: 8890
		private ImapClient imapClient;

		// Token: 0x040022BB RID: 8891
		private ImapTransaction.State currentState;

		// Token: 0x02000570 RID: 1392
		protected enum State
		{
			// Token: 0x040022BD RID: 8893
			Disconnected,
			// Token: 0x040022BE RID: 8894
			Connecting,
			// Token: 0x040022BF RID: 8895
			Connected,
			// Token: 0x040022C0 RID: 8896
			NegotiatingTls,
			// Token: 0x040022C1 RID: 8897
			SettingUpSecureStreamForTls,
			// Token: 0x040022C2 RID: 8898
			ReadyToLogIn,
			// Token: 0x040022C3 RID: 8899
			LoggingOn,
			// Token: 0x040022C4 RID: 8900
			SelectingFolder,
			// Token: 0x040022C5 RID: 8901
			FindingTestMessage,
			// Token: 0x040022C6 RID: 8902
			DeletingTestMessage,
			// Token: 0x040022C7 RID: 8903
			GettingOlderMessages,
			// Token: 0x040022C8 RID: 8904
			DeletingOlderMessages,
			// Token: 0x040022C9 RID: 8905
			LoggingOff,
			// Token: 0x040022CA RID: 8906
			Disconnecting
		}
	}
}
