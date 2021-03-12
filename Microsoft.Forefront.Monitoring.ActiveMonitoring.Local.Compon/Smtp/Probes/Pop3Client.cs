using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000237 RID: 567
	public class Pop3Client : TcpClient, IPop3Client, IDisposable
	{
		// Token: 0x060012C5 RID: 4805 RVA: 0x000370E8 File Offset: 0x000352E8
		public Pop3Client(CancellationToken cancellationToken)
		{
			this.cancellationToken = cancellationToken;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x000370F8 File Offset: 0x000352F8
		public void Connect(string server, int port, string username, string password, bool enableSsl, int readTimeout)
		{
			if (this.isConnected)
			{
				return;
			}
			try
			{
				if (!base.Connected)
				{
					Task task = base.ConnectAsync(server, port);
					task.Wait(this.cancellationToken);
				}
				if (enableSsl)
				{
					SslStream sslStream = new SslStream(base.GetStream(), false);
					Task task2 = sslStream.AuthenticateAsClientAsync(server);
					task2.Wait(this.cancellationToken);
					this.stream = sslStream;
				}
				else
				{
					this.stream = base.GetStream();
				}
				this.stream.ReadTimeout = readTimeout * 1000;
				this.reader = new StreamReader(this.stream, Encoding.ASCII);
				string text = this.Response();
				if (!this.ResponseOk(text))
				{
					throw new Pop3Exception(string.Format("Pop3 server response is not OK. Response: {0}", text), text, "Connect");
				}
			}
			catch (Exception ex)
			{
				if (ex is Pop3Exception)
				{
					throw;
				}
				if (ex is OperationCanceledException)
				{
					throw;
				}
				throw new Pop3Exception(string.Format("Pop3 server connection failure. Server: {0}, Port: {1}, EnableSSL: {2}, Timeout: {3}.", new object[]
				{
					server,
					port,
					enableSsl,
					readTimeout
				}), ex);
			}
			this.USER(username);
			this.PASS(password);
			this.isConnected = true;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00037238 File Offset: 0x00035438
		public void Disconnect()
		{
			if (base.Connected)
			{
				try
				{
					this.SendCommand("QUIT");
				}
				finally
				{
					this.isConnected = false;
				}
			}
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00037274 File Offset: 0x00035474
		public void USER(string username)
		{
			this.SendCommand(string.Format("USER {0}", username));
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00037287 File Offset: 0x00035487
		public void PASS(string password)
		{
			this.SendCommand(string.Format("{0} {1}", "PASS", password));
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x000372A0 File Offset: 0x000354A0
		public List<Pop3Message> List()
		{
			this.SendCommand("LIST");
			List<Pop3Message> list = new List<Pop3Message>();
			string text;
			while (this.GetNextResponseLine(out text))
			{
				string[] array = text.Split(new char[]
				{
					' '
				});
				if (array.Length == 2)
				{
					int num;
					int.TryParse(array[0], out num);
					int num2;
					int.TryParse(array[1], out num2);
					if (num != 0 && num2 != 0)
					{
						list.Add(new Pop3Message
						{
							Number = (long)num,
							Size = (long)num2
						});
					}
				}
			}
			return list;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00037327 File Offset: 0x00035527
		public void RetrieveHeader(Pop3Message message)
		{
			this.SendCommand(string.Format("TOP {0} 0", message.Number));
			message.HeaderComponents = this.GetNextResponseLines();
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00037350 File Offset: 0x00035550
		public void Retrieve(Pop3Message message)
		{
			this.SendCommand(string.Format("RETR {0}", message.Number));
			message.Components = this.GetNextResponseLines();
			int num = 0;
			for (int i = 0; i < message.Components.Count; i++)
			{
				if (string.IsNullOrEmpty(message.Components[i]))
				{
					num = i;
					break;
				}
			}
			for (int j = num + 1; j < message.Components.Count; j++)
			{
				message.BodyComponents.Add(message.Components[j]);
			}
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x000373E4 File Offset: 0x000355E4
		public void Retrieve(List<Pop3Message> messages)
		{
			foreach (Pop3Message message in messages)
			{
				this.Retrieve(message);
			}
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00037434 File Offset: 0x00035634
		public void Delete(Pop3Message message)
		{
			this.SendCommand(string.Format("DELE {0}", message.Number));
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00037451 File Offset: 0x00035651
		public new void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0003745C File Offset: 0x0003565C
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.disposed = true;
				if (this.isConnected)
				{
					this.Disconnect();
				}
				base.Close();
				base.Dispose(true);
				if (this.stream != null)
				{
					this.stream.Dispose();
				}
				if (this.reader != null)
				{
					this.reader.Dispose();
				}
				GC.SuppressFinalize(this);
			}
			this.disposed = true;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x000374CC File Offset: 0x000356CC
		private void SendCommand(string command)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(command + "\r\n");
			Task task = this.stream.WriteAsync(bytes, 0, bytes.Length);
			task.Wait(this.cancellationToken);
			string text = this.Response();
			if (!this.ResponseOk(text))
			{
				string message = string.Format("Pop3 server response is not OK. Command: {0}, Response: {1}", command.Contains("PASS") ? "PASS <Omitted>" : command, text);
				throw new Pop3Exception(message, text, command);
			}
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00037548 File Offset: 0x00035748
		private string Response()
		{
			Task<string> task = this.reader.ReadLineAsync();
			task.Wait(this.cancellationToken);
			return task.Result;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00037573 File Offset: 0x00035773
		private bool ResponseOk(string response)
		{
			return !string.IsNullOrEmpty(response) && response.Length >= 3 && response.Substring(0, 3) == "+OK";
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0003759A File Offset: 0x0003579A
		private bool GetNextResponseLine(out string response)
		{
			response = this.Response();
			if (response == ".")
			{
				return false;
			}
			if (response.StartsWith("."))
			{
				response = response.Substring(1);
			}
			return true;
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x000375D0 File Offset: 0x000357D0
		private List<string> GetNextResponseLines()
		{
			List<string> list = new List<string>();
			string item;
			while (this.GetNextResponseLine(out item))
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x040008BC RID: 2236
		private const string CRLF = "\r\n";

		// Token: 0x040008BD RID: 2237
		private const string PASSCMD = "PASS";

		// Token: 0x040008BE RID: 2238
		private readonly CancellationToken cancellationToken;

		// Token: 0x040008BF RID: 2239
		private bool isConnected;

		// Token: 0x040008C0 RID: 2240
		private Stream stream;

		// Token: 0x040008C1 RID: 2241
		private StreamReader reader;

		// Token: 0x040008C2 RID: 2242
		private bool disposed;
	}
}
