using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200050D RID: 1293
	internal class ImapClient : ProtocolClient
	{
		// Token: 0x06002E69 RID: 11881 RVA: 0x000B9B7C File Offset: 0x000B7D7C
		internal ImapClient(string hostName, int portNumber, ProtocolConnectionType connectionMode, bool trustAnySSLCertificate) : base(hostName, portNumber, connectionMode, trustAnySSLCertificate)
		{
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000B9B94 File Offset: 0x000B7D94
		internal void SelectFolder(DataCommunicator.GetCommandResponseDelegate callback, string folder)
		{
			this.GenerateCommandTag();
			string command = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				this.currentTag,
				string.Format(CultureInfo.InvariantCulture, "SELECT \"{0}\"\r\n", new object[]
				{
					folder
				})
			});
			base.Communicator.SendCommandAsync(command, new AsyncCallback(this.MultiLineResponseCallback), callback, null);
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000B9C04 File Offset: 0x000B7E04
		internal void FindMessagesBeforeTodayContainingSubject(DataCommunicator.GetCommandResponseDelegate callback, string subject)
		{
			this.GenerateCommandTag();
			string text = ExDateTime.Now.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
			string text2 = string.Format(CultureInfo.InvariantCulture, "SEARCH BEFORE \"{0}\" SUBJECT \"{1}\"\r\n", new object[]
			{
				text,
				subject
			});
			string command = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				this.currentTag,
				text2
			});
			base.Communicator.SendCommandAsync(command, new AsyncCallback(this.MultiLineResponseCallback), callback, null);
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000B9C98 File Offset: 0x000B7E98
		internal void FindMessageIDsBySubject(string subjectKey, DataCommunicator.GetCommandResponseDelegate callback)
		{
			this.GenerateCommandTag();
			string command = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				this.currentTag,
				string.Format(CultureInfo.InvariantCulture, "SEARCH SUBJECT \"{0}\"\r\n", new object[]
				{
					subjectKey
				})
			});
			base.Communicator.SendCommandAsync(command, new AsyncCallback(this.MultiLineResponseCallback), callback, null);
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000B9D08 File Offset: 0x000B7F08
		internal void StoreMessages(DataCommunicator.GetCommandResponseDelegate callback, string[] messageIDs)
		{
			StringBuilder stringBuilder = new StringBuilder(messageIDs[0]);
			for (int i = 1; i < messageIDs.Length; i++)
			{
				stringBuilder.AppendFormat(",{0}", messageIDs[i]);
			}
			this.GenerateCommandTag();
			string command = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				this.currentTag,
				string.Format(CultureInfo.InvariantCulture, "STORE {0} +FLAGS (\\Deleted)\r\n", new object[]
				{
					stringBuilder.ToString()
				})
			});
			base.Communicator.SendCommandAsync(command, new AsyncCallback(this.MultiLineResponseCallback), callback, null);
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000B9DA4 File Offset: 0x000B7FA4
		internal void ExpungeMessages(DataCommunicator.GetCommandResponseDelegate callback)
		{
			this.GenerateCommandTag();
			string command = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				this.currentTag,
				"EXPUNGE\r\n"
			});
			base.Communicator.SendCommandAsync(command, new AsyncCallback(this.MultiLineResponseCallback), callback, null);
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000B9DFC File Offset: 0x000B7FFC
		internal void LogOn(DataCommunicator.GetCommandResponseDelegate callback, string userID, string password)
		{
			this.GenerateCommandTag();
			string command = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				this.currentTag,
				string.Format(CultureInfo.InvariantCulture, "LOGIN {0} {1}\r\n", new object[]
				{
					userID,
					password
				})
			});
			base.Communicator.SendCommandAsync(command, new AsyncCallback(base.SingleLineResponseCallback), callback, null);
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000B9E70 File Offset: 0x000B8070
		internal void LogOff(DataCommunicator.GetCommandResponseDelegate callback)
		{
			this.GenerateCommandTag();
			string command = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				this.currentTag,
				"LOGOUT\r\n"
			});
			base.Communicator.SendCommandAsync(command, new AsyncCallback(this.MultiLineResponseCallback), callback, null);
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000B9EC8 File Offset: 0x000B80C8
		internal void Capability(DataCommunicator.GetCommandResponseDelegate callback)
		{
			this.GenerateCommandTag();
			string command = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				this.currentTag,
				"CAPABILITY\r\n"
			});
			base.Communicator.SendCommandAsync(command, new AsyncCallback(this.MultiLineResponseCallback), callback, null);
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x000B9F20 File Offset: 0x000B8120
		internal bool IsOkResponse(string response)
		{
			if (string.IsNullOrEmpty(response))
			{
				return false;
			}
			string[] array = response.Split(new string[]
			{
				"\r\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			string text = "* ";
			if (array.Length == 1 && array[0].StartsWith(text + "OK", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			int num = -1;
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].StartsWith(text, StringComparison.OrdinalIgnoreCase))
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return false;
			}
			string value = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				this.currentTag,
				"OK"
			});
			return array[num].StartsWith(value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x000B9FD8 File Offset: 0x000B81D8
		internal void StartTls(DataCommunicator.GetCommandResponseDelegate callback)
		{
			this.GenerateCommandTag();
			string command = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				this.currentTag,
				"STARTTLS\r\n"
			});
			base.Communicator.SendCommandAsync(command, new AsyncCallback(base.SingleLineResponseCallback), callback, null);
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x000BA030 File Offset: 0x000B8230
		private string GenerateCommandTag()
		{
			StringBuilder stringBuilder = new StringBuilder("a");
			for (int i = 0; i < 3; i++)
			{
				char value = (char)(65 + this.random.Next(26));
				stringBuilder.Append(value);
			}
			this.currentTag = stringBuilder.ToString();
			return stringBuilder.ToString();
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000BA080 File Offset: 0x000B8280
		private void MultiLineResponseCallback(IAsyncResult asyncResult)
		{
			DataCommunicator.State state = (DataCommunicator.State)asyncResult.AsyncState;
			try
			{
				int num = state.DataStream.EndRead(asyncResult);
				if (num > 0)
				{
					state.AppendReceivedData(num);
					if (!this.LastLineReceived(state.Response))
					{
						state.BeginRead();
						return;
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

		// Token: 0x06002E76 RID: 11894 RVA: 0x000BA134 File Offset: 0x000B8334
		private bool LastLineReceived(string response)
		{
			int num = response.LastIndexOf("\r\n", StringComparison.Ordinal);
			if (num < 0)
			{
				return false;
			}
			if (response.StartsWith("+", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			num = response.LastIndexOf("\r\n", num - 1, StringComparison.Ordinal);
			string text;
			if (num < 0)
			{
				text = response;
			}
			else
			{
				num += 2;
				text = response.Substring(num, response.Length - num);
			}
			return text.StartsWith("+", StringComparison.OrdinalIgnoreCase) || text.StartsWith(this.currentTag, StringComparison.OrdinalIgnoreCase) || text.StartsWith("* BYE", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04002132 RID: 8498
		internal const string InvalidImapState = "Command received in Invalid state.";

		// Token: 0x04002133 RID: 8499
		protected const string TlsCommandFormat = "STARTTLS\r\n";

		// Token: 0x04002134 RID: 8500
		private const string DateFormat = "dd-MMM-yyyy";

		// Token: 0x04002135 RID: 8501
		private const string CapabilityCommand = "CAPABILITY\r\n";

		// Token: 0x04002136 RID: 8502
		private const string InboxFolder = "INBOX";

		// Token: 0x04002137 RID: 8503
		private const string SelectFolderCommandFormat = "SELECT \"{0}\"\r\n";

		// Token: 0x04002138 RID: 8504
		private const string SearchSubjectCommandFormat = "SEARCH SUBJECT \"{0}\"\r\n";

		// Token: 0x04002139 RID: 8505
		private const string HeaderCommandFormat = "FETCH {0} rfc822.header\r\n";

		// Token: 0x0400213A RID: 8506
		private const string SearchDateCommandFormat = "SEARCH BEFORE \"{0}\"\r\n";

		// Token: 0x0400213B RID: 8507
		private const string LoginCommandFormat = "LOGIN {0} {1}\r\n";

		// Token: 0x0400213C RID: 8508
		private const string LogoutCommandFormat = "LOGOUT\r\n";

		// Token: 0x0400213D RID: 8509
		private const string ImapCommandFormat = "{0} {1}";

		// Token: 0x0400213E RID: 8510
		private const string SearchSubjectDateCommandFormat = "SEARCH BEFORE \"{0}\" SUBJECT \"{1}\"\r\n";

		// Token: 0x0400213F RID: 8511
		private const string MarkForDeletionCommandFormat = "STORE {0} +FLAGS (\\Deleted)\r\n";

		// Token: 0x04002140 RID: 8512
		private const string ExpungeCommandFormat = "EXPUNGE\r\n";

		// Token: 0x04002141 RID: 8513
		private const string OkResponse = "OK";

		// Token: 0x04002142 RID: 8514
		private const string NoResponse = "NO";

		// Token: 0x04002143 RID: 8515
		private const string BadResponse = "BAD";

		// Token: 0x04002144 RID: 8516
		private const int TagLength = 3;

		// Token: 0x04002145 RID: 8517
		private Random random = new Random();

		// Token: 0x04002146 RID: 8518
		private string currentTag;
	}
}
