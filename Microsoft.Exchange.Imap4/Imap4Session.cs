using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PopImap.Core;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200000E RID: 14
	internal sealed class Imap4Session : ProtocolSession
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003C84 File Offset: 0x00001E84
		public Imap4Session(NetworkConnection connection, VirtualServer virtualServer) : base(connection, virtualServer)
		{
			base.ResponseFactory = new Imap4ResponseFactory(this);
			this.needToRebuildFolderTable = true;
			base.WorkloadSettings = Imap4Session.DefaultWorkloadSettings;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003CD4 File Offset: 0x00001ED4
		internal HashSet<StoreObjectId> Imap4Folders
		{
			get
			{
				return this.imap4Folders;
			}
		}

		// Token: 0x17000036 RID: 54
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003CDC File Offset: 0x00001EDC
		internal bool NeedToRebuildFolderTable
		{
			set
			{
				this.needToRebuildFolderTable = value;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public override string BannerString()
		{
			string result;
			if (base.VirtualServer.Banner != null)
			{
				result = string.Format("* OK {0}", base.VirtualServer.Banner);
			}
			else
			{
				result = string.Format("* OK {0} Microsoft IMAP4 MAIL Service, ready at {1}", ProtocolBaseServices.ServerName, Rfc822Date.Format(ResponseFactory.CurrentExTimeZone.ConvertDateTime(ExDateTime.UtcNow)));
			}
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003D3F File Offset: 0x00001F3F
		public override string GetUserConfigurationName()
		{
			return "IMAP4.UserConfiguration";
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003D48 File Offset: 0x00001F48
		internal new static int GetNextToken(byte[] buffer, int beginIndex, int size, out int tokenEnd)
		{
			int nextToken = BaseSession.GetNextToken(buffer, beginIndex, size, out tokenEnd);
			if (tokenEnd == beginIndex + size)
			{
				return nextToken;
			}
			if (buffer[nextToken] == 34)
			{
				while ((tokenEnd <= beginIndex + size && (buffer[tokenEnd - 1] != 34 || Imap4Session.LastQuoteIsEscaped(buffer, nextToken, tokenEnd))) || tokenEnd <= nextToken + 1)
				{
					if (tokenEnd == beginIndex + size)
					{
						return tokenEnd;
					}
					BaseSession.GetNextToken(buffer, tokenEnd, size - (tokenEnd - beginIndex), out tokenEnd);
				}
			}
			return nextToken;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003DB0 File Offset: 0x00001FB0
		internal override void HandleCommandTooLongError(NetworkConnection nc, byte[] buf, int offset, int size)
		{
			int num = 0;
			while (num < size && buf[offset + num] != 32)
			{
				num++;
			}
			bool flag = true;
			if (num >= size)
			{
				num = 1;
				flag = false;
			}
			byte[] array = new byte[num + 1 + Imap4Session.CommandTooLong.Length];
			if (flag)
			{
				Buffer.BlockCopy(buf, offset, array, 0, num + 1);
			}
			else
			{
				array[0] = 42;
				array[1] = 32;
			}
			Buffer.BlockCopy(Imap4Session.CommandTooLong, 0, array, num + 1, Imap4Session.CommandTooLong.Length);
			base.SendToClient(new BufferResponseItem(array, new BaseSession.SendCompleteDelegate(base.EndShutdown)));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003E3C File Offset: 0x0000203C
		internal override void HandleIdleTimeout()
		{
			ProtocolBaseServices.SessionTracer.TraceDebug<Imap4Session>(base.SessionId, "Idle timeout reached. Attempting to end session: {0}.", this);
			if (base.NegotiatingTls)
			{
				base.Connection.Shutdown();
				return;
			}
			lock (this.LockObject)
			{
				Imap4ResponseFactory imap4ResponseFactory = (Imap4ResponseFactory)base.ResponseFactory;
				if (imap4ResponseFactory != null)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug<Imap4Session>(base.SessionId, "Ending session due to idle timeout: {0}.", this);
					base.BeginShutdown(imap4ResponseFactory.TimeoutErrorString);
				}
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003ED4 File Offset: 0x000020D4
		internal bool TryGetMailbox(string path, out Imap4Mailbox mailbox, bool nearestParentOk)
		{
			ProtocolBaseServices.Assert(string.IsNullOrEmpty(path) || (path[0] != '/' && path[path.Length - 1] != '/'), "Path is not normalized.", new object[0]);
			mailbox = null;
			if (this.needToRebuildFolderTable)
			{
				Imap4ResponseFactory factory = (Imap4ResponseFactory)base.ResponseFactory;
				ICollection<Imap4Mailbox> mailboxTree = Imap4Mailbox.GetMailboxTree(factory);
				this.PopulateInternalMailboxes(factory, mailboxTree);
			}
			if (nearestParentOk)
			{
				while (!this.folderTable.ContainsKey(path))
				{
					int num = path.LastIndexOf('/');
					if (num > -1)
					{
						path = path.Substring(0, num);
					}
					else
					{
						path = string.Empty;
					}
				}
				ProtocolBaseServices.Assert(this.folderTable.ContainsKey(path), "Mailbox not found", new object[0]);
				mailbox = this.folderTable[path];
				return true;
			}
			if (!this.folderTable.ContainsKey(path))
			{
				return false;
			}
			mailbox = this.folderTable[path];
			return true;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003FC4 File Offset: 0x000021C4
		internal void PopulateInternalMailboxes(Imap4ResponseFactory factory, ICollection<Imap4Mailbox> mailboxes)
		{
			this.imap4Folders.Clear();
			Imap4Mailbox selectedMailbox = ((Imap4ResponseFactory)base.ResponseFactory).SelectedMailbox;
			foreach (Imap4Mailbox imap4Mailbox in this.folderTable.Values)
			{
				if (selectedMailbox == null || !object.ReferenceEquals(imap4Mailbox, selectedMailbox))
				{
					imap4Mailbox.Dispose();
				}
			}
			this.folderTable.Clear();
			bool flag = false;
			foreach (Imap4Mailbox imap4Mailbox2 in mailboxes)
			{
				if (selectedMailbox != null && selectedMailbox.Uid.Equals(imap4Mailbox2.Uid))
				{
					flag = true;
					if (string.Compare(selectedMailbox.FullPath, imap4Mailbox2.FullPath, StringComparison.OrdinalIgnoreCase) != 0)
					{
						selectedMailbox.FullPath = imap4Mailbox2.FullPath;
					}
					imap4Mailbox2.Dispose();
					this.folderTable.Add(selectedMailbox.FullPath, selectedMailbox);
				}
				else
				{
					this.folderTable.Add(imap4Mailbox2.FullPath, imap4Mailbox2);
				}
				this.imap4Folders.Add(imap4Mailbox2.Uid);
			}
			if (selectedMailbox != null && !flag)
			{
				selectedMailbox.MailboxDoesNotExist = true;
			}
			this.needToRebuildFolderTable = false;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004114 File Offset: 0x00002314
		protected override void InternalDispose()
		{
			lock (this.LockObject)
			{
				try
				{
					foreach (Imap4Mailbox imap4Mailbox in this.folderTable.Values)
					{
						imap4Mailbox.Dispose();
					}
					this.folderTable.Clear();
				}
				finally
				{
					base.InternalDispose();
				}
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000042CC File Offset: 0x000024CC
		protected override void HandleCommand(NetworkConnection nc, byte[] buf, int offset, int size)
		{
			ProtocolResponse protocolResponse = null;
			IStandardBudget perCallBudget = null;
			try
			{
				ResponseFactory responseFactory = base.ResponseFactory;
				if (responseFactory == null || responseFactory.Disposed)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug(base.SessionId, "ResponseFactory is disposed.");
					return;
				}
				if (base.ProxySession != null)
				{
					this.HandleProxyCommand(nc, buf, offset, size);
					return;
				}
				try
				{
					responseFactory.RecordCommandStart();
					perCallBudget = responseFactory.AcquirePerCommandBudget();
					if (ProtocolBaseServices.SessionTracer.IsTraceEnabled(TraceType.InfoTrace) || base.VerifyMailboxLogEnabled() || base.LightLogSession != null)
					{
						if (responseFactory.IsInAuthenticationMode)
						{
							base.LogReceive(Imap4Session.AuthBlob, 0, Imap4Session.AuthBlob.Length);
						}
						else
						{
							int num;
							int nextToken = Imap4Session.GetNextToken(buf, offset, size, out num);
							int num2;
							int nextToken2 = Imap4Session.GetNextToken(buf, num, size - (num - offset), out num2);
							if (nextToken == offset && nextToken2 == num + 1 && BaseSession.CompareArg(Imap4ResponseFactory.LoginBuf, buf, nextToken2, num2 - nextToken2))
							{
								nextToken = Imap4Session.GetNextToken(buf, num2, size - (num2 - offset), out num);
								if (num < offset + size)
								{
									byte[] array = new byte[num - offset + 6];
									Buffer.BlockCopy(buf, offset, array, 0, num - offset + 1);
									for (int i = num - offset + 1; i < array.Length; i++)
									{
										array[i] = 42;
									}
									base.LogReceive(array, 0, array.Length);
								}
								else
								{
									base.LogReceive(buf, offset, size);
								}
							}
							else
							{
								base.LogReceive(buf, offset, size);
							}
						}
					}
					if (responseFactory.IsInAuthenticationMode)
					{
						ProtocolBaseServices.SessionTracer.TraceDebug(base.SessionId, "Authentication mode.");
						protocolResponse = responseFactory.ProcessAuthentication(buf, offset, size);
					}
					else if (this.Is7BitCommand(buf, offset, size))
					{
						protocolResponse = responseFactory.ProcessCommand(buf, offset, size);
					}
					else
					{
						protocolResponse = responseFactory.CommandIsNotAllASCII(buf, offset, size);
					}
					if (!base.OkToIssueRead)
					{
						base.OkToIssueRead = true;
						return;
					}
				}
				catch (Exception exception)
				{
					if (protocolResponse != null)
					{
						protocolResponse.Dispose();
					}
					if (!this.CheckNonCriticalException(exception))
					{
						throw;
					}
					protocolResponse = responseFactory.ProcessException(null, exception);
				}
				finally
				{
					base.EnforceMicroDelayAndDisposeCostHandles(perCallBudget);
				}
				if (protocolResponse != null)
				{
					if (protocolResponse.IsDisconnectResponse)
					{
						if (protocolResponse.NeedToDelayStoreAction && ((Imap4ResponseFactory)responseFactory).SessionState > Imap4State.Nonauthenticated)
						{
							DataProcessResponseItem responseitem = new DataProcessResponseItem(protocolResponse, delegate(ProtocolSession session, DataProcessResponseItem responseItem)
							{
								ProtocolResponse protocolResponse2 = responseItem.StateData as ProtocolResponse;
								string dataToSend = protocolResponse2.DataToSend;
								if (!string.IsNullOrEmpty(dataToSend))
								{
									responseItem.EnqueueResponseItem(new StringResponseItem(dataToSend, delegate()
									{
										session.EndShutdown();
									}));
									return;
								}
								responseItem.EnqueueResponseItem(new BufferResponseItem(ProtocolSession.EmptyBuffer, 0, 0, delegate()
								{
									session.EndShutdown();
								}));
							}, delegate(DataProcessResponseItem responseItem)
							{
								ProtocolResponse protocolResponse2 = responseItem.StateData as ProtocolResponse;
								if (protocolResponse2 != null)
								{
									protocolResponse2.Dispose();
								}
							});
							protocolResponse = null;
							base.SendToClient(responseitem);
							return;
						}
						base.BeginShutdown(protocolResponse.DataToSend);
						return;
					}
					else if (protocolResponse.NeedToDelayStoreAction && ((Imap4ResponseFactory)responseFactory).SessionState > Imap4State.Nonauthenticated)
					{
						DataProcessResponseItem responseitem2 = new DataProcessResponseItem(protocolResponse, delegate(ProtocolSession session, DataProcessResponseItem responseItem)
						{
							ProtocolResponse protocolResponse2 = responseItem.StateData as ProtocolResponse;
							responseItem.EnqueueResponseItem(new StringResponseItem(protocolResponse2.DataToSend));
						}, delegate(DataProcessResponseItem responseItem)
						{
							ProtocolResponse protocolResponse2 = responseItem.StateData as ProtocolResponse;
							if (protocolResponse2 != null)
							{
								protocolResponse2.Dispose();
							}
						});
						protocolResponse = null;
						if (!base.SendToClient(responseitem2))
						{
							return;
						}
					}
					else if (!base.SendToClient(new StringResponseItem(protocolResponse.DataToSend)))
					{
						return;
					}
				}
			}
			finally
			{
				if (protocolResponse != null)
				{
					base.SetDiagnosticValue(PopImapConditionalHandlerSchema.Response, protocolResponse.DataToSend);
					base.SetDiagnosticValue(PopImapConditionalHandlerSchema.Message, protocolResponse.MessageString);
					this.LogCommand(protocolResponse, protocolResponse.DataToSend);
					protocolResponse.Dispose();
				}
			}
			base.SendToClient(new EndResponseItem(new BaseSession.SendCompleteDelegate(base.EndCommandProcess)));
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004660 File Offset: 0x00002860
		protected override void BeginRead(NetworkConnection nc)
		{
			if (nc == null)
			{
				return;
			}
			if (base.ResponseFactory.IncompleteRequest == null || base.ResponseFactory.IsInAuthenticationMode)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(base.SessionId, "Request was complete -- reading next command.");
				base.BeginRead(nc);
				return;
			}
			if (this.bytesToProxy > 0)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(base.SessionId, "Proxy request is incomplete -- reading next chunk.");
				nc.BeginRead(new AsyncCallback(this.ProxyReadCompleteCallback), nc);
				return;
			}
			ProtocolBaseServices.SessionTracer.TraceDebug(base.SessionId, "Request is incomplete -- reading next chunk.");
			nc.BeginRead(new AsyncCallback(this.ReadCompleteCallback), nc);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004704 File Offset: 0x00002904
		private static bool LastQuoteIsEscaped(byte[] buffer, int tokenBegin, int tokenEnd)
		{
			int num = 0;
			if (tokenEnd >= 2 && buffer[tokenEnd - 1] == 34 && buffer[tokenEnd - 2] == 92)
			{
				num++;
				int num2 = tokenEnd - 3;
				while (num2 > tokenBegin && buffer[num2] == 92)
				{
					num2--;
					num++;
				}
			}
			return num % 2 == 1;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000474C File Offset: 0x0000294C
		private void LogCommand(ProtocolResponse response, string message)
		{
			LightWeightLogSession lightLogSession = base.LightLogSession;
			if (lightLogSession != null)
			{
				if (response.IsCommandFailedResponse)
				{
					lightLogSession.Result = message;
					return;
				}
				Imap4Response imap4Response = response as Imap4Response;
				if (imap4Response != null)
				{
					lightLogSession.Result = imap4Response.ResponseType.ToString();
				}
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004794 File Offset: 0x00002994
		private void ReadCompleteCallback(IAsyncResult iar)
		{
			IStandardBudget perCallBudget = null;
			try
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string>(base.SessionId, "User {0} entering Imap4Session.ReadCompleteCallback.", this.GetUserNameForLogging());
				NetworkConnection networkConnection = (NetworkConnection)iar.AsyncState;
				byte[] data;
				int offset;
				int num;
				object obj;
				networkConnection.EndRead(iar, out data, out offset, out num, out obj);
				if (obj != null)
				{
					this.HandleError(obj, networkConnection);
				}
				else
				{
					perCallBudget = base.ResponseFactory.AcquirePerCommandBudget();
					base.EnterCommandProcessing();
					try
					{
						ProtocolResponse protocolResponse;
						try
						{
							ProtocolBaseServices.SessionTracer.TraceDebug<int>(base.SessionId, "RawDataReceived {0} bytes", num);
							Imap4ResponseFactory imap4ResponseFactory = (Imap4ResponseFactory)base.ResponseFactory;
							int num2;
							protocolResponse = imap4ResponseFactory.ProcessRawData(data, offset, num, out num2);
							if (base.LightLogSession != null)
							{
								base.LightLogSession.RequestSize += (long)num2;
							}
							ProtocolBaseServices.Assert(num2 <= num, "bytesConsumed > amount", new object[0]);
							if (num2 < num)
							{
								networkConnection.PutBackReceivedBytes(num - num2);
							}
						}
						catch (Exception exception)
						{
							if (!this.CheckNonCriticalException(exception))
							{
								throw;
							}
							protocolResponse = base.ResponseFactory.ProcessException(null, exception);
						}
						if (protocolResponse != null)
						{
							try
							{
								string dataToSend = protocolResponse.DataToSend;
								this.LogCommand(protocolResponse, dataToSend);
								if (protocolResponse.IsDisconnectResponse)
								{
									base.BeginShutdown(dataToSend);
									return;
								}
								if (!base.SendToClient(new StringResponseItem(dataToSend)))
								{
									return;
								}
							}
							finally
							{
								protocolResponse.Dispose();
								protocolResponse = null;
							}
						}
						this.BeginRead(networkConnection);
					}
					finally
					{
						base.LeaveCommandProcessing();
						base.EnforceMicroDelayAndDisposeCostHandles(perCallBudget);
					}
				}
			}
			catch (Exception exception2)
			{
				if (!this.CheckNonCriticalException(exception2))
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

		// Token: 0x0600008A RID: 138 RVA: 0x000049AC File Offset: 0x00002BAC
		private void HandleProxyCommand(NetworkConnection nc, byte[] buf, int offset, int size)
		{
			if (size > 0 && buf[offset + size - 1] == 125)
			{
				string @string = Encoding.ASCII.GetString(buf, offset, size);
				Imap4RequestWithStringParameters.TryParseLiteral(@string, out this.bytesToProxy);
			}
			if (base.LightLogSession != null)
			{
				base.LightLogSession.RequestSize += (long)size;
				base.LightLogSession.FlushProxyTraffic();
			}
			ProxySession proxySession = base.ProxySession;
			if (proxySession != null)
			{
				proxySession.SendBufferAsCommand(buf, offset, size);
				proxySession.SendToClient(base.BeginReadResponseItem);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004A30 File Offset: 0x00002C30
		private void ProxyReadCompleteCallback(IAsyncResult iar)
		{
			try
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string>(base.SessionId, "User {0} entering Imap4Session.ProxyReadCompleteCallback.", this.GetUserNameForLogging());
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
					byte[] array;
					if (num2 > base.ProxyBuffer.Length)
					{
						array = new byte[num2];
					}
					else
					{
						array = base.ProxyBuffer;
					}
					Buffer.BlockCopy(src, srcOffset, array, 0, num2);
					if (base.ProxySession.SendToClient(new BufferResponseItem(array, 0, num2)))
					{
						ProtocolBaseServices.Assert(num2 <= num, "bytesConsumed > amount", new object[0]);
						this.bytesToProxy -= num2;
						if (num2 < num)
						{
							networkConnection.PutBackReceivedBytes(num - num2);
						}
						if (base.LightLogSession != null)
						{
							base.LightLogSession.RequestSize += (long)num2;
							base.LightLogSession.FlushProxyTraffic();
						}
						base.ProxySession.SendToClient(base.BeginReadResponseItem);
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

		// Token: 0x0600008C RID: 140 RVA: 0x00004BB4 File Offset: 0x00002DB4
		private bool Is7BitCommand(byte[] buf, int offset, int size)
		{
			int num;
			int nextToken = Imap4Session.GetNextToken(buf, offset, size, out num);
			return BaseSession.CompareArg(Imap4ResponseFactory.LoginBuf, buf, nextToken, num - nextToken) || base.Is7BitString(buf, offset, size);
		}

		// Token: 0x04000034 RID: 52
		public const string UserConfigurationName = "IMAP4.UserConfiguration";

		// Token: 0x04000035 RID: 53
		private const string BannerDefault = "* OK {0}";

		// Token: 0x04000036 RID: 54
		private const string Banner = "* OK {0} Microsoft IMAP4 MAIL Service, ready at {1}";

		// Token: 0x04000037 RID: 55
		private const string BannerDebug = "* OK {0} Microsoft IMAP4 MAIL Service, Version: {1} ready at {2}";

		// Token: 0x04000038 RID: 56
		private static readonly WorkloadSettings DefaultWorkloadSettings = new WorkloadSettings(WorkloadType.Imap, false);

		// Token: 0x04000039 RID: 57
		private static readonly byte[] CommandTooLong = Encoding.ASCII.GetBytes("BAD Command Error. 10\r\n* BYE Connection closed.");

		// Token: 0x0400003A RID: 58
		private static readonly byte[] AuthBlob = Encoding.ASCII.GetBytes("<auth blob>");

		// Token: 0x0400003B RID: 59
		private int bytesToProxy;

		// Token: 0x0400003C RID: 60
		private bool needToRebuildFolderTable;

		// Token: 0x0400003D RID: 61
		private Dictionary<string, Imap4Mailbox> folderTable = new Dictionary<string, Imap4Mailbox>(20, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400003E RID: 62
		private HashSet<StoreObjectId> imap4Folders = new HashSet<StoreObjectId>();
	}
}
