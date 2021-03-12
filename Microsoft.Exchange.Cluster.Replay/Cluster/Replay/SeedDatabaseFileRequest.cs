using System;
using System.IO;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.ReplicaSeeder;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002A2 RID: 674
	internal class SeedDatabaseFileRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x06001A51 RID: 6737 RVA: 0x0006EDB0 File Offset: 0x0006CFB0
		internal SeedDatabaseFileRequest(NetworkChannel channel, Guid dbGuid, uint readSizeHint) : base(channel, NetworkChannelMessage.MessageType.SeedDatabaseFileRequest, dbGuid)
		{
			this.m_readHintSize = readSizeHint;
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x0006EDC6 File Offset: 0x0006CFC6
		internal SeedDatabaseFileRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.SeedDatabaseFileRequest, packetContent)
		{
			this.m_readHintSize = base.Packet.ExtractUInt32();
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x0006EDE6 File Offset: 0x0006CFE6
		// (set) Token: 0x06001A54 RID: 6740 RVA: 0x0006EDEE File Offset: 0x0006CFEE
		internal uint ReadSizeHint
		{
			get
			{
				return this.m_readHintSize;
			}
			set
			{
				this.m_readHintSize = value;
			}
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x0006EDF8 File Offset: 0x0006CFF8
		public override void Execute()
		{
			string databaseName = base.Channel.MonitoredDatabase.DatabaseName;
			string partnerNodeName = base.Channel.PartnerNodeName;
			ExTraceGlobals.SeederServerTracer.TraceDebug<string, uint>((long)this.GetHashCode(), "SeedDatabaseFileRequest({0}): readHintSize ({1}).", databaseName, this.m_readHintSize);
			bool flag = false;
			base.Channel.IsSeeding = true;
			Exception ex = null;
			try
			{
				base.Channel.CreateSeederServerContext(base.DatabaseGuid, null, SeedType.Database);
				ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedDatabaseFileRequest. Opening up the backup context for {0}.", base.DatabaseGuid);
				long fileSize;
				this.m_backupHandle = SeedDatabaseFileRequest.OpenSeedStreamer(base.DatabaseGuid, this.m_readHintSize, out fileSize);
				SeedDatabaseFileReply seedDatabaseFileReply = new SeedDatabaseFileReply(base.Channel);
				seedDatabaseFileReply.FileSize = fileSize;
				seedDatabaseFileReply.LastWriteUtc = DateTime.UtcNow;
				seedDatabaseFileReply.Send();
				ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedDatabaseFileRequest. Sending the data for {0}.", base.DatabaseGuid);
				ExTraceGlobals.FaultInjectionTracer.TraceTest(3343265085U);
				base.Channel.SendSeedingDataTransferReply(seedDatabaseFileReply, new ReadDatabaseCallback(this.ReadDbCallback));
				flag = true;
			}
			catch (OperationCanceledException ex2)
			{
				ex = ex2;
			}
			catch (SeedingChannelIsClosedException ex3)
			{
				ex = ex3;
			}
			catch (SeedingSourceReplicaInstanceNotFoundException ex4)
			{
				ex = ex4;
			}
			catch (IOException ex5)
			{
				ex = ex5;
			}
			catch (FailedToOpenBackupFileHandleException ex6)
			{
				ex = ex6;
			}
			catch (FileIOonSourceException ex7)
			{
				ex = ex7;
			}
			catch (NetworkTransportException ex8)
			{
				ex = ex8;
			}
			catch (ADExternalException ex9)
			{
				ex = ex9;
			}
			catch (ADOperationException ex10)
			{
				ex = ex10;
			}
			catch (ADTransientException ex11)
			{
				ex = ex11;
			}
			catch (TaskServerException ex12)
			{
				ex = ex12;
			}
			catch (TransientException ex13)
			{
				ex = ex13;
			}
			finally
			{
				if (ex != null)
				{
					ExTraceGlobals.SeederServerTracer.TraceError<string, Exception>((long)this.GetHashCode(), "SeedDatabaseFileRequest({0}): Failed: {1}", databaseName, ex);
					ReplayCrimsonEvents.ActiveSeedSourceFailedToSendEDB.Log<string, string, string, string>(databaseName, Environment.MachineName, partnerNodeName, ex.ToString());
				}
				if (this.m_backupHandle != null)
				{
					int lastError = flag ? 0 : 42;
					this.m_backupHandle.SetLastError(lastError);
					this.m_backupHandle.Close();
					this.m_backupHandle = null;
				}
				if (ex != null)
				{
					if (ex is NetworkTransportException)
					{
						base.Channel.KeepAlive = false;
					}
					else
					{
						base.Channel.SendException(ex);
					}
				}
			}
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x0006F144 File Offset: 0x0006D344
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_readHintSize);
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x0006F160 File Offset: 0x0006D360
		private static EseDatabaseBackupReader OpenSeedStreamer(Guid dbGuid, uint readHintSize, out long initialFileSizeBytes)
		{
			string machineName = Environment.MachineName;
			initialFileSizeBytes = 0L;
			string dbName;
			string sourceFileToBackupFullPath;
			SeedHelper.GetDatabaseNameAndPath(dbGuid, out dbName, out sourceFileToBackupFullPath);
			CReplicaSeederInterop.SetupNativeLogger();
			EseDatabaseBackupReader esedatabaseBackupReader = EseDatabaseBackupReader.GetESEDatabaseBackupReader(machineName, dbName, dbGuid, null, sourceFileToBackupFullPath, readHintSize);
			initialFileSizeBytes = esedatabaseBackupReader.SourceFileSizeBytes;
			return esedatabaseBackupReader;
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x0006F19C File Offset: 0x0006D39C
		private int ReadDbCallback(byte[] buf, ulong readOffset, int bytesToRead)
		{
			if (buf == null)
			{
				throw new ArgumentNullException("buf");
			}
			if (buf.Length != 65536)
			{
				throw new ArgumentException("buf");
			}
			if (this.m_backupHandle == null)
			{
				throw new ArgumentNullException("m_backupHandle");
			}
			return this.m_backupHandle.PerformDatabaseRead((long)readOffset, buf, bytesToRead);
		}

		// Token: 0x04000A8C RID: 2700
		private uint m_readHintSize;

		// Token: 0x04000A8D RID: 2701
		private EseDatabaseBackupReader m_backupHandle;
	}
}
