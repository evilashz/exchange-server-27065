using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000514 RID: 1300
	internal class PopClient : ProtocolClient
	{
		// Token: 0x06002E97 RID: 11927 RVA: 0x000BA8B7 File Offset: 0x000B8AB7
		internal PopClient(string hostName, int portNumber, ProtocolConnectionType connectionMode, bool trustAnySSLCertificate) : base(hostName, portNumber, connectionMode, trustAnySSLCertificate)
		{
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000BA8C4 File Offset: 0x000B8AC4
		internal static bool IsOkResponse(string response)
		{
			return !string.IsNullOrEmpty(response) && response.StartsWith("+OK", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000BA8DC File Offset: 0x000B8ADC
		internal void GetMessage(DataCommunicator.GetCommandResponseDelegate callback, string messageNumber)
		{
			string command = string.Format(CultureInfo.InvariantCulture, "RETR {0}\r\n", new object[]
			{
				messageNumber
			});
			base.Communicator.SendCommandAsync(command, new AsyncCallback(this.MultiLineResponseCallback), callback, null);
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000BA920 File Offset: 0x000B8B20
		internal void List(DataCommunicator.GetCommandResponseDelegate callback)
		{
			string command = "LIST\r\n";
			base.Communicator.SendCommandAsync(command, new AsyncCallback(this.MultiLineResponseCallback), callback, null);
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000BA950 File Offset: 0x000B8B50
		internal void DeleteMessage(DataCommunicator.GetCommandResponseDelegate callback, string messageNumber)
		{
			string command = string.Format(CultureInfo.InvariantCulture, "DELE {0}\r\n", new object[]
			{
				messageNumber
			});
			base.Communicator.SendCommandAsync(command, new AsyncCallback(base.SingleLineResponseCallback), callback, null);
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000BA994 File Offset: 0x000B8B94
		internal void UserCommand(DataCommunicator.GetCommandResponseDelegate callback, string userID)
		{
			string command = string.Format(CultureInfo.InvariantCulture, "USER {0}\r\n", new object[]
			{
				userID
			});
			base.Communicator.SendCommandAsync(command, new AsyncCallback(base.SingleLineResponseCallback), callback, null);
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000BA9D8 File Offset: 0x000B8BD8
		internal void PassCommand(DataCommunicator.GetCommandResponseDelegate callback, string password)
		{
			string command = string.Format(CultureInfo.InvariantCulture, "PASS {0}\r\n", new object[]
			{
				password
			});
			base.Communicator.SendCommandAsync(command, new AsyncCallback(base.SingleLineResponseCallback), callback, null);
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000BAA1B File Offset: 0x000B8C1B
		internal void LogOff(DataCommunicator.GetCommandResponseDelegate callback)
		{
			base.Communicator.SendCommandAsync("QUIT\r\n", new AsyncCallback(base.SingleLineResponseCallback), callback, null);
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x000BAA3C File Offset: 0x000B8C3C
		internal void Stls(DataCommunicator.GetCommandResponseDelegate callback)
		{
			string command = string.Format(CultureInfo.InvariantCulture, "STLS\r\n", new object[0]);
			base.Communicator.SendCommandAsync(command, new AsyncCallback(base.SingleLineResponseCallback), callback, null);
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000BAA7C File Offset: 0x000B8C7C
		internal void Capa(DataCommunicator.GetCommandResponseDelegate callback)
		{
			string command = string.Format(CultureInfo.InvariantCulture, "CAPA\r\n", new object[0]);
			base.Communicator.SendCommandAsync(command, new AsyncCallback(this.MultiLineResponseCallback), callback, null);
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000BAABC File Offset: 0x000B8CBC
		internal void MultiLineResponseCallback(IAsyncResult asyncResult)
		{
			DataCommunicator.State state = (DataCommunicator.State)asyncResult.AsyncState;
			try
			{
				int num = state.DataStream.EndRead(asyncResult);
				if (num > 0)
				{
					state.AppendReceivedData(num);
					if (!PopClient.IsOkResponse(state.Response))
					{
						if (!state.Response.EndsWith("\r\n", StringComparison.Ordinal))
						{
							state.ReadDataCallback = new AsyncCallback(base.SingleLineResponseCallback);
							state.BeginRead();
							return;
						}
					}
					else
					{
						bool flag = false;
						if (!state.Response.EndsWith("\r\n.\r\n", StringComparison.Ordinal))
						{
							flag = true;
						}
						if (flag)
						{
							state.BeginRead();
							return;
						}
					}
				}
			}
			catch (InvalidOperationException exception)
			{
				if (base.Communicator.HasTimedOut)
				{
					base.Communicator.HandleException(DataCommunicator.CreateTimeoutException());
					return;
				}
				base.Communicator.HandleException(exception);
			}
			catch (IOException exception2)
			{
				base.Communicator.HandleException(exception2);
				return;
			}
			base.Communicator.StopTimer();
			state.LaunchResponseDelegate();
		}

		// Token: 0x0400214F RID: 8527
		internal const string InvalidPopState = "Command is not valid in this state.";

		// Token: 0x04002150 RID: 8528
		private const string MultiLineTerminator = "\r\n.\r\n";

		// Token: 0x04002151 RID: 8529
		private const string RetriveMessageCommandFormat = "RETR {0}\r\n";

		// Token: 0x04002152 RID: 8530
		private const string DeleteMessageCommandFormat = "DELE {0}\r\n";

		// Token: 0x04002153 RID: 8531
		private const string QuitCommandFormat = "QUIT\r\n";

		// Token: 0x04002154 RID: 8532
		private const string UserCommandFormat = "USER {0}\r\n";

		// Token: 0x04002155 RID: 8533
		private const string PassCommandFormat = "PASS {0}\r\n";

		// Token: 0x04002156 RID: 8534
		private const string ListCommandFormat = "LIST\r\n";

		// Token: 0x04002157 RID: 8535
		private const string CapabilityCommand = "CAPA\r\n";

		// Token: 0x04002158 RID: 8536
		private const string TlsCommand = "STLS\r\n";

		// Token: 0x04002159 RID: 8537
		private const string OkResponse = "+OK";

		// Token: 0x0400215A RID: 8538
		private const string ErrorResponse = "-ERR";
	}
}
