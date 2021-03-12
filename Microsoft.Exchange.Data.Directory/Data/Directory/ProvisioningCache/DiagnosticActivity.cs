using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Directory.EventLog;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x020007A2 RID: 1954
	internal class DiagnosticActivity : Activity
	{
		// Token: 0x0600612C RID: 24876 RVA: 0x0014A847 File Offset: 0x00148A47
		public DiagnosticActivity(ProvisioningCache cache, string pipeName) : base(cache)
		{
			if (string.IsNullOrWhiteSpace(pipeName))
			{
				throw new ArgumentException("pipeName is null or empty.");
			}
			this.pipeName = pipeName;
			this.serverStream = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 4);
		}

		// Token: 0x170022C0 RID: 8896
		// (get) Token: 0x0600612D RID: 24877 RVA: 0x0014A878 File Offset: 0x00148A78
		public override string Name
		{
			get
			{
				return "ProvisioningCache Diagnostic command receiver";
			}
		}

		// Token: 0x0600612E RID: 24878 RVA: 0x0014A87F File Offset: 0x00148A7F
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.serverStream != null)
			{
				this.serverStream.Close();
				this.serverStream.Dispose();
				this.serverStream = null;
			}
		}

		// Token: 0x0600612F RID: 24879 RVA: 0x0014A8AC File Offset: 0x00148AAC
		protected override void InternalExecute()
		{
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCStartingToReceiveDiagnosticCommand, this.pipeName, new object[]
			{
				this.pipeName
			});
			try
			{
				while (!base.GotStopSignalFromTestCode)
				{
					try
					{
						this.isWaitForConnection = true;
						this.serverStream.WaitForConnection();
						this.ProcessClientAccept();
					}
					catch (InvalidOperationException ex)
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCFailedToReceiveDiagnosticCommand, this.pipeName, new object[]
						{
							this.pipeName,
							ex.ToString()
						});
						throw ex;
					}
					catch (IOException ex2)
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCFailedToReceiveDiagnosticCommand, this.pipeName, new object[]
						{
							this.pipeName,
							ex2.ToString()
						});
						throw ex2;
					}
					finally
					{
						this.isWaitForConnection = false;
					}
				}
			}
			finally
			{
				this.InternalDispose(true);
			}
		}

		// Token: 0x06006130 RID: 24880 RVA: 0x0014A9B0 File Offset: 0x00148BB0
		private void ProcessDiagnosticCommand(DiagnosticCommand command, PipeStream stream)
		{
			if (command.Command == DiagnosticType.Reset)
			{
				if (command.IsGlobalCacheEntryCommand)
				{
					base.ProvisioningCache.RemoveGlobalDatas(command.CacheEntries);
				}
				else if (command.Organizations == null)
				{
					base.ProvisioningCache.ResetOrganizationData();
				}
				else
				{
					foreach (Guid orgId in command.Organizations)
					{
						base.ProvisioningCache.RemoveOrganizationDatas(orgId, command.CacheEntries);
					}
				}
				byte[] bytes = Encoding.UTF8.GetBytes("Successfully reset the provisioning cache data");
				stream.Write(bytes, 0, bytes.Length);
				return;
			}
			if (command.Command == DiagnosticType.Dump)
			{
				foreach (CachedEntryObject cachedEntryObject in base.ProvisioningCache.DumpCachedEntries(command.Organizations, command.CacheEntries))
				{
					byte[] array = cachedEntryObject.ToSendMessage();
					stream.Write(array, 0, array.Length);
				}
			}
		}

		// Token: 0x06006131 RID: 24881 RVA: 0x0014AACC File Offset: 0x00148CCC
		private void ProcessClientAccept()
		{
			byte[] array = new byte[1000];
			Array.Clear(array, 0, array.Length);
			try
			{
				if (!base.GotStopSignalFromTestCode)
				{
					int bufLen = this.serverStream.Read(array, 0, array.Length);
					Exception ex;
					DiagnosticCommand command = DiagnosticCommand.TryFromReceivedData(array, bufLen, out ex);
					if (ex != null)
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCInvalidDiagnosticCommandReceived, this.pipeName, new object[]
						{
							this.pipeName,
							Encoding.UTF8.GetString(array, 0, array.Length),
							ex.ToString()
						});
					}
					else
					{
						this.ProcessDiagnosticCommand(command, this.serverStream);
						this.serverStream.Flush();
					}
				}
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCFailedToReceiveClientDiagnosticDommand, this.pipeName, new object[]
				{
					this.pipeName,
					ex2.ToString()
				});
				throw ex2;
			}
			catch (IOException ex3)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCFailedToReceiveClientDiagnosticDommand, this.pipeName, new object[]
				{
					this.pipeName,
					ex3.ToString()
				});
				throw ex3;
			}
			catch (ObjectDisposedException ex4)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCFailedToReceiveClientDiagnosticDommand, this.pipeName, new object[]
				{
					this.pipeName,
					ex4.ToString()
				});
				throw ex4;
			}
			catch (InvalidOperationException ex5)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCFailedToReceiveClientDiagnosticDommand, this.pipeName, new object[]
				{
					this.pipeName,
					ex5.ToString()
				});
				throw ex5;
			}
			catch (NotSupportedException ex6)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCFailedToReceiveClientDiagnosticDommand, this.pipeName, new object[]
				{
					this.pipeName,
					ex6.ToString()
				});
				throw ex6;
			}
			finally
			{
				this.serverStream.Disconnect();
			}
		}

		// Token: 0x06006132 RID: 24882 RVA: 0x0014AD28 File Offset: 0x00148F28
		internal override void StopExecute()
		{
			base.StopExecute();
			if (base.GotStopSignalFromTestCode && this.isWaitForConnection)
			{
				while (this.isWaitForConnection)
				{
					using (NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", this.pipeName, PipeDirection.InOut))
					{
						try
						{
							byte[] array = new DiagnosticCommand(DiagnosticType.Dump, Guid.NewGuid()).ToSendMessage();
							namedPipeClientStream.Connect();
							namedPipeClientStream.Write(array, 0, array.Length);
							namedPipeClientStream.Flush();
						}
						catch
						{
						}
						Thread.Sleep(10000);
					}
				}
				base.AsyncThread.Join();
			}
		}

		// Token: 0x0400413E RID: 16702
		private readonly string pipeName;

		// Token: 0x0400413F RID: 16703
		private NamedPipeServerStream serverStream;

		// Token: 0x04004140 RID: 16704
		private bool isWaitForConnection;
	}
}
