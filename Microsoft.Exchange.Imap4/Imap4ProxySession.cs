using System;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200000F RID: 15
	internal class Imap4ProxySession : ProxySession
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00004C1F File Offset: 0x00002E1F
		internal Imap4ProxySession(ResponseFactory responseFactory, NetworkConnection connection) : base(responseFactory, connection)
		{
		}

		// Token: 0x17000037 RID: 55
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00004C29 File Offset: 0x00002E29
		internal string PublicFolders
		{
			set
			{
				this.publicFolders = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00004C32 File Offset: 0x00002E32
		internal string PublicFolderNamespace
		{
			set
			{
				this.publicFolderNamespace = value;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004C3B File Offset: 0x00002E3B
		public override string GetUserConfigurationName()
		{
			return "IMAP4.UserConfiguration";
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004C44 File Offset: 0x00002E44
		protected override bool ForwardToClient(byte[] buf, int offset, int size)
		{
			if (!base.IncomingSession.ProxyToLegacyServer)
			{
				return true;
			}
			string text = Encoding.ASCII.GetString(buf, offset, size);
			if (text.StartsWith("* NAMESPACE ", StringComparison.OrdinalIgnoreCase) && this.publicFolderNamespace != null && text.IndexOf(this.publicFolderNamespace, StringComparison.OrdinalIgnoreCase) > -1)
			{
				text = text.Replace(this.publicFolderNamespace, "NIL");
				base.IncomingSession.SendToClient(new StringResponseItem(text));
				return false;
			}
			if (text.StartsWith("* LIST ", StringComparison.OrdinalIgnoreCase) && this.publicFolders != null && (text.IndexOf(") \"/\" \"" + this.publicFolders, StringComparison.OrdinalIgnoreCase) > -1 || text.IndexOf(") \"/\" " + this.publicFolders, StringComparison.OrdinalIgnoreCase) > -1))
			{
				return false;
			}
			if (text.StartsWith("* BYE ", StringComparison.OrdinalIgnoreCase))
			{
				base.IncomingSession.SendToClient(new StringResponseItem("* BYE Microsoft Exchange Server 2013 IMAP4 server signing off."));
				return false;
			}
			Match match = Imap4ProxySession.referralParser.Match(text);
			if (match.Success)
			{
				text = text.Substring(0, match.Index) + text.Substring(match.Index + match.Length);
				base.IncomingSession.SendToClient(new StringResponseItem(text));
				return false;
			}
			if (!string.IsNullOrEmpty(text) && text[text.Length - 1] == '}')
			{
				Imap4RequestWithStringParameters.TryParseLiteral(text, out this.bytesToProxy);
			}
			return true;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004DA4 File Offset: 0x00002FA4
		protected override void BeginRead(NetworkConnection networkConnection)
		{
			if (networkConnection == null)
			{
				return;
			}
			if ((this.bytesToProxy == 0 && base.IncomingSession.ProxyToLegacyServer) || base.State != ProxySession.ProxyState.completed)
			{
				networkConnection.BeginReadLine(new AsyncCallback(this.ReadLineCompleteCallback), networkConnection);
				return;
			}
			networkConnection.BeginRead(new AsyncCallback(this.ReadCompleteCallback), networkConnection);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004E00 File Offset: 0x00003000
		protected override void ReadCompleteCallback(IAsyncResult iar)
		{
			if (!base.IncomingSession.ProxyToLegacyServer)
			{
				base.ReadCompleteCallback(iar);
				return;
			}
			try
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string>(base.SessionId, "User {0} entering Imap4ProxySession.ReadCompleteCallback.", base.IncomingSession.GetUserNameForLogging());
				NetworkConnection networkConnection = (NetworkConnection)iar.AsyncState;
				byte[] src;
				int srcOffset;
				int num;
				object obj;
				networkConnection.EndRead(iar, out src, out srcOffset, out num, out obj);
				if (obj != null)
				{
					this.HandleError(obj, networkConnection);
				}
				else
				{
					int num2 = Math.Min(num, this.bytesToProxy);
					byte[] array = new byte[num2];
					Buffer.BlockCopy(src, srcOffset, array, 0, num2);
					if (base.IncomingSession.SendToClient(new BufferResponseItem(array)))
					{
						ProtocolBaseServices.Assert(num2 <= num, "bytesConsumed > amount", new object[0]);
						this.bytesToProxy -= num2;
						if (num2 < num)
						{
							networkConnection.PutBackReceivedBytes(num - num2);
						}
						base.IncomingSession.SendToClient(base.BeginReadResponseItem);
					}
				}
			}
			catch (Exception exception)
			{
				if (!this.CheckNonCriticalException(exception))
				{
					throw;
				}
				this.HandleError(null, iar.AsyncState as NetworkConnection);
			}
			finally
			{
				ProtocolBaseServices.InMemoryTraceOperationCompleted(base.SessionId);
			}
		}

		// Token: 0x04000043 RID: 67
		private static Regex referralParser = new Regex(" \\[REFERRAL [^\\]]+?\\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000044 RID: 68
		private string publicFolders;

		// Token: 0x04000045 RID: 69
		private string publicFolderNamespace;

		// Token: 0x04000046 RID: 70
		private int bytesToProxy;
	}
}
