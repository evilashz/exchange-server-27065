using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200057C RID: 1404
	internal class PopTransaction : ProtocolTransaction
	{
		// Token: 0x0600316C RID: 12652 RVA: 0x000C90B6 File Offset: 0x000C72B6
		internal PopTransaction(ADUser user, string serverName, string password, int port) : base(user, serverName, password, port)
		{
			this.messagesToDelete = new Queue<string>();
			this.messageIdQueue = new Queue<string>();
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x000C90DC File Offset: 0x000C72DC
		internal override void Execute()
		{
			this.currentState = PopTransaction.State.Disconnected;
			base.LatencyTimer = Stopwatch.StartNew();
			if (!base.IsServiceRunning("MSExchangePOP3", base.CasServer))
			{
				base.Verbose(Strings.ServiceNotRunning("MSExchangePOP3"));
				base.UpdateTransactionResults(CasTransactionResultEnum.Failure, Strings.ServiceNotRunning("MSExchangePOP3"));
				base.CompleteTransaction();
				return;
			}
			this.popClient = new PopClient(base.CasServer, base.Port, base.ConnectionType, base.TrustAnySslCertificate);
			this.popClient.ExceptionReporter = new DataCommunicator.ExceptionReporterDelegate(this.ExceptionReporter);
			base.Verbose(Strings.PopImapConnecting);
			this.currentState = PopTransaction.State.Connecting;
			this.popClient.Connect(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse));
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000C91A8 File Offset: 0x000C73A8
		private static Queue<string> PutIDsInQueue(string response)
		{
			string pattern = "\r\n";
			string empty = string.Empty;
			char[] separator = new char[]
			{
				' '
			};
			string[] array = Regex.Split(response, pattern);
			if (array == null || array.Length < 3)
			{
				return new Queue<string>();
			}
			Queue<string> queue = new Queue<string>(array.Length - 2);
			for (int i = 1; i < array.Length - 1; i++)
			{
				string[] array2 = array[i].Split(separator);
				if (array2 != null && array2.Length > 1)
				{
					queue.Enqueue(array2[0]);
				}
			}
			return queue;
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x000C922C File Offset: 0x000C742C
		private void GetTheResponse(string response, object extraData)
		{
			switch (this.currentState)
			{
			case PopTransaction.State.SettingUpSecureStreamForTls:
			case PopTransaction.State.GettingMessageData:
			case PopTransaction.State.DeletingOlderMessages:
			case PopTransaction.State.LoggingOff:
			case PopTransaction.State.Disconnecting:
				break;
			default:
				if (!PopClient.IsOkResponse(response))
				{
					this.ReportErrorAndQuit(response);
					return;
				}
				break;
			}
			switch (this.currentState)
			{
			case PopTransaction.State.Disconnected:
			case PopTransaction.State.Disconnecting:
				return;
			case PopTransaction.State.Connecting:
				if (base.ConnectionType == ProtocolConnectionType.Tls)
				{
					this.currentState = PopTransaction.State.NegotiatingTls;
					this.popClient.Stls(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse));
					return;
				}
				break;
			case PopTransaction.State.Connected:
			case PopTransaction.State.SettingUpSecureStreamForTls:
				break;
			case PopTransaction.State.NegotiatingTls:
				this.currentState = PopTransaction.State.SettingUpSecureStreamForTls;
				this.popClient.SetUpSecureStreamForTls(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse));
				return;
			case PopTransaction.State.ReadyToLogIn:
				if (base.LightMode)
				{
					this.ReportSuccessAndLogOff();
					return;
				}
				base.Verbose(Strings.PopImapLoggingOn);
				this.currentState = PopTransaction.State.SendingUserCommand;
				if (!string.IsNullOrEmpty(base.User.UserPrincipalName))
				{
					this.popClient.UserCommand(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse), base.User.UserPrincipalName);
					return;
				}
				this.popClient.UserCommand(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse), base.User.SamAccountName);
				return;
			case PopTransaction.State.SendingUserCommand:
				this.currentState = PopTransaction.State.SendingPassCommand;
				this.popClient.PassCommand(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse), base.Password);
				return;
			case PopTransaction.State.SendingPassCommand:
				this.popClient.HasLoggedIn = true;
				this.currentState = PopTransaction.State.GettingMessageIds;
				base.Verbose(Strings.PopGettingMessageIDs);
				this.popClient.List(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse));
				return;
			case PopTransaction.State.GettingMessageIds:
				this.messageIdQueue = PopTransaction.PutIDsInQueue(response);
				if (this.messageIdQueue.Count == 0)
				{
					throw new ArgumentOutOfRangeException("this.messageIDQueue", Strings.PopImapNoMessagesToDelete);
				}
				this.messagesToDelete.Clear();
				this.currentState = PopTransaction.State.GettingMessageData;
				this.popClient.GetMessage(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse), this.messageIdQueue.Peek());
				return;
			case PopTransaction.State.GettingMessageData:
			{
				string item = this.messageIdQueue.Dequeue();
				if (PopClient.IsOkResponse(response))
				{
					bool flag = false;
					string text = ProtocolClient.GetSubjectOfMessage(response).Trim();
					if (string.IsNullOrEmpty(this.testMessageID) && base.MailSubject.Equals(text, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
						this.testMessageID = item;
					}
					if (!flag && text.StartsWith("Test-POPConnectivity-", StringComparison.OrdinalIgnoreCase))
					{
						ExDateTime dt = ExDateTime.Parse(ProtocolClient.GetDateOfMessage(response), CultureInfo.InvariantCulture);
						if ((ExDateTime.Now - dt).Days >= 1)
						{
							this.messagesToDelete.Enqueue(item);
						}
					}
				}
				if (this.messageIdQueue.Count > 0)
				{
					this.popClient.GetMessage(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse), this.messageIdQueue.Peek());
					return;
				}
				this.currentState = PopTransaction.State.DeletingTestMessage;
				this.DeleteTestMessage();
				return;
			}
			case PopTransaction.State.DeletingTestMessage:
				base.Verbose(Strings.PopImapDeleteOldMsgs);
				if (this.messagesToDelete.Count == 0)
				{
					base.Verbose(Strings.PopImapNoMessagesToDelete);
					this.ReportSuccessAndLogOff();
					return;
				}
				this.currentState = PopTransaction.State.DeletingOlderMessages;
				this.popClient.DeleteMessage(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse), this.messagesToDelete.Dequeue());
				return;
			case PopTransaction.State.DeletingOlderMessages:
				if (this.messagesToDelete.Count > 0)
				{
					this.popClient.DeleteMessage(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse), this.messagesToDelete.Dequeue());
					return;
				}
				this.ReportSuccessAndLogOff();
				return;
			case PopTransaction.State.LoggingOff:
				this.popClient.HasLoggedIn = false;
				this.currentState = PopTransaction.State.Disconnecting;
				this.Disconnect();
				return;
			default:
				throw new ArgumentOutOfRangeException(Strings.PopImapErrorUnexpectedValue(this.currentState.ToString()));
			}
			this.currentState = PopTransaction.State.ReadyToLogIn;
			this.popClient.Capa(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse));
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x000C9614 File Offset: 0x000C7814
		private void ExceptionReporter(Exception exception)
		{
			base.UpdateTransactionResults(CasTransactionResultEnum.Failure, exception);
			this.Disconnect();
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000C9624 File Offset: 0x000C7824
		private void LogOffAndDisconnectAfterError()
		{
			if (this.popClient == null)
			{
				this.currentState = PopTransaction.State.Disconnected;
				base.CompleteTransaction();
				return;
			}
			if (this.popClient.HasLoggedIn)
			{
				this.LogOff();
				return;
			}
			this.Disconnect();
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x000C9656 File Offset: 0x000C7856
		private void Disconnect()
		{
			base.Verbose(Strings.PopImapDisconnecting);
			this.popClient.Dispose();
			this.popClient = null;
			this.currentState = PopTransaction.State.Disconnected;
			base.CompleteTransaction();
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x000C9688 File Offset: 0x000C7888
		private void DeleteTestMessage()
		{
			this.currentState = PopTransaction.State.DeletingTestMessage;
			if (string.IsNullOrEmpty(this.testMessageID))
			{
				base.UpdateTransactionResults(CasTransactionResultEnum.Failure, Strings.ErrorMessageNotFound(base.MailSubject));
				this.LogOffAndDisconnectAfterError();
				return;
			}
			base.Verbose(Strings.PopImapDeleteMsg(int.Parse(this.testMessageID, CultureInfo.InvariantCulture)));
			this.popClient.DeleteMessage(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse), this.testMessageID);
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x000C9705 File Offset: 0x000C7905
		private void ReportSuccessAndLogOff()
		{
			base.LatencyTimer.Stop();
			base.UpdateTransactionResults(CasTransactionResultEnum.Success, null);
			this.LogOff();
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000C9720 File Offset: 0x000C7920
		private void LogOff()
		{
			this.currentState = PopTransaction.State.LoggingOff;
			base.Verbose(Strings.PopImapLoggingOff);
			this.popClient.LogOff(new DataCommunicator.GetCommandResponseDelegate(this.GetTheResponse));
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x000C9754 File Offset: 0x000C7954
		private void ReportErrorAndQuit(string response)
		{
			if (response.Contains("Command is not valid in this state."))
			{
				response = string.Format(CultureInfo.InvariantCulture, "{0}\r\n{1}", new object[]
				{
					response,
					Strings.CheckServerConfiguration
				});
			}
			base.UpdateTransactionResults(CasTransactionResultEnum.Failure, Strings.ErrorOfProtocolCommand("POP", response));
			this.LogOffAndDisconnectAfterError();
		}

		// Token: 0x04002301 RID: 8961
		private PopClient popClient;

		// Token: 0x04002302 RID: 8962
		private Queue<string> messageIdQueue;

		// Token: 0x04002303 RID: 8963
		private Queue<string> messagesToDelete;

		// Token: 0x04002304 RID: 8964
		private string testMessageID;

		// Token: 0x04002305 RID: 8965
		private PopTransaction.State currentState;

		// Token: 0x0200057D RID: 1405
		protected enum State
		{
			// Token: 0x04002307 RID: 8967
			Disconnected,
			// Token: 0x04002308 RID: 8968
			Connecting,
			// Token: 0x04002309 RID: 8969
			Connected,
			// Token: 0x0400230A RID: 8970
			NegotiatingTls,
			// Token: 0x0400230B RID: 8971
			SettingUpSecureStreamForTls,
			// Token: 0x0400230C RID: 8972
			ReadyToLogIn,
			// Token: 0x0400230D RID: 8973
			SendingUserCommand,
			// Token: 0x0400230E RID: 8974
			SendingPassCommand,
			// Token: 0x0400230F RID: 8975
			GettingMessageIds,
			// Token: 0x04002310 RID: 8976
			GettingMessageData,
			// Token: 0x04002311 RID: 8977
			DeletingTestMessage,
			// Token: 0x04002312 RID: 8978
			DeletingOlderMessages,
			// Token: 0x04002313 RID: 8979
			LoggingOff,
			// Token: 0x04002314 RID: 8980
			Disconnecting
		}
	}
}
