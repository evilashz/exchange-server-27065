using System;
using System.Threading;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.EseRepl;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Search.Fast;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000291 RID: 657
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CiFileSeederProvider
	{
		// Token: 0x060019C1 RID: 6593 RVA: 0x0006BD66 File Offset: 0x00069F66
		internal CiFileSeederProvider(string sourceServerName, string targetServerName, Guid databaseGuid)
		{
			this.sourceServerFqdn = sourceServerName;
			this.targetServerFqdn = targetServerName;
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x0006BD83 File Offset: 0x00069F83
		// (set) Token: 0x060019C3 RID: 6595 RVA: 0x0006BD8B File Offset: 0x00069F8B
		internal string NetworkName { get; set; }

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x0006BD94 File Offset: 0x00069F94
		// (set) Token: 0x060019C5 RID: 6597 RVA: 0x0006BD9C File Offset: 0x00069F9C
		internal bool? CompressOverride { get; set; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x0006BDA5 File Offset: 0x00069FA5
		// (set) Token: 0x060019C7 RID: 6599 RVA: 0x0006BDAD File Offset: 0x00069FAD
		internal bool? EncryptOverride { get; set; }

		// Token: 0x060019C8 RID: 6600 RVA: 0x0006BDB6 File Offset: 0x00069FB6
		public override string ToString()
		{
			return string.Format("[CiFileSeederProvider: sourceServer={0}, targetServer={1}, databaseGuid={2}]", this.sourceServerFqdn, this.targetServerFqdn, this.databaseGuid);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0006BDD9 File Offset: 0x00069FD9
		internal void Close()
		{
			this.CloseChannel();
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0006BDE4 File Offset: 0x00069FE4
		internal void SeedCatalog(string endpoint, IReplicaSeederCallback callback, string reason)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "SeedCatalog {0} to {1}.", this.databaseGuid, endpoint);
			this.GetChannel();
			if (this.DoesSourceSupportExtensibleSeedingRequests())
			{
				SeedCiFileRequest2 seedCiFileRequest = new SeedCiFileRequest2(this.channel, this.databaseGuid, endpoint, reason);
				seedCiFileRequest.Send();
			}
			else
			{
				SeedCiFileRequest seedCiFileRequest2 = new SeedCiFileRequest(this.channel, this.databaseGuid, endpoint);
				seedCiFileRequest2.Send();
			}
			ReplayStopwatch replayStopwatch = new ReplayStopwatch();
			replayStopwatch.Start();
			NetworkChannelMessage message = this.channel.GetMessage();
			SeedCiFileReply seedCiFileReply = message as SeedCiFileReply;
			if (seedCiFileReply == null)
			{
				this.channel.ThrowUnexpectedMessage(message);
			}
			ExTraceGlobals.SeederServerTracer.TraceDebug<long>((long)this.GetHashCode(), "SeedCiFile response took: {0}ms", replayStopwatch.ElapsedMilliseconds);
			string handle = seedCiFileReply.Handle;
			ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "Get seeding handle: {0}", handle);
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2911251773U);
			this.UpdateProgress(handle, callback);
			replayStopwatch.Stop();
			ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "{0}: SeedCatalog succeeded: {1} for {2} after {3} ms", new object[]
			{
				ExDateTime.Now,
				endpoint,
				this.databaseGuid,
				replayStopwatch.ElapsedMilliseconds
			});
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0006BF30 File Offset: 0x0006A130
		internal void CancelSeeding()
		{
			try
			{
				ExTraceGlobals.SeederServerTracer.TraceError<Guid>((long)this.GetHashCode(), "CancelSeeding catalog for {0}.", this.databaseGuid);
				lock (this)
				{
					this.isAborted = true;
					if (this.channel != null)
					{
						this.channel.Abort();
					}
				}
			}
			finally
			{
				ReplayCrimsonEvents.SeedingErrorOnTarget.Log<Guid, string, LocalizedString>(this.databaseGuid, string.Empty, ReplayStrings.SeederOperationAborted);
			}
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0006BFC4 File Offset: 0x0006A1C4
		private void UpdateProgress(string seedingHandle, IReplicaSeederCallback callback)
		{
			ReplayStopwatch replayStopwatch = new ReplayStopwatch();
			ProgressCiFileRequest progressCiFileRequest = new ProgressCiFileRequest(this.channel, this.databaseGuid, seedingHandle);
			int num = -1;
			TimeSpan timeout = TimeSpan.FromSeconds((double)RegistryParameters.SeedCatalogProgressIntervalInSec);
			int progress;
			for (;;)
			{
				progressCiFileRequest.Send();
				replayStopwatch.Restart();
				NetworkChannelMessage message = this.channel.GetMessage();
				ProgressCiFileReply progressCiFileReply = message as ProgressCiFileReply;
				if (progressCiFileReply == null)
				{
					this.channel.ThrowUnexpectedMessage(message);
				}
				ExTraceGlobals.SeederServerTracer.TraceDebug<long>((long)this.GetHashCode(), "ProgressCiFile response took: {0}ms", replayStopwatch.ElapsedMilliseconds);
				progress = progressCiFileReply.Progress;
				ExTraceGlobals.SeederServerTracer.TraceDebug<int>((long)this.GetHashCode(), "Get seeding progress: {0}", progress);
				if (callback != null && callback.IsBackupCancelled())
				{
					break;
				}
				if (progress < 0)
				{
					goto Block_4;
				}
				if (progress > num)
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<Guid, int, bool>((long)this.GetHashCode(), "Updating progress for catalog '{0}'. Percent = {1}%. Callback = {2}", this.databaseGuid, progress, callback != null);
					if (callback != null)
					{
						callback.ReportProgress("IndexSystem", 102400L, (long)progress * 1024L, (long)progress * 1024L);
					}
					num = progress;
				}
				if (progress == 100)
				{
					return;
				}
				Thread.Sleep(timeout);
			}
			ExTraceGlobals.SeederServerTracer.TraceDebug<int>((long)this.GetHashCode(), "The seeding was cancelled at {0}%", num);
			throw new SeederOperationAbortedException();
			Block_4:
			Exception innerException = new CiSeederGenericException(this.sourceServerFqdn, this.targetServerFqdn, ReplayStrings.CiSeederExchangeSearchTransientException(string.Format("{0}", progress)));
			throw new PerformingFastOperationException(innerException);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0006C134 File Offset: 0x0006A334
		private void GetChannel()
		{
			lock (this)
			{
				if (this.isAborted)
				{
					throw new SeederOperationAbortedException();
				}
				if (this.channel == null)
				{
					NetworkPath networkPath = NetworkManager.ChooseNetworkPath(this.sourceServerFqdn, this.NetworkName, NetworkPath.ConnectionPurpose.Seeding);
					if (this.CompressOverride != null || this.EncryptOverride != null || !string.IsNullOrEmpty(this.NetworkName))
					{
						networkPath.Compress = (this.CompressOverride ?? networkPath.Compress);
						networkPath.Encrypt = (this.EncryptOverride ?? networkPath.Encrypt);
						networkPath.NetworkChoiceIsMandatory = true;
					}
					this.channel = NetworkChannel.Connect(networkPath, TcpChannel.GetDefaultTimeoutInMs(), false);
				}
			}
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0006C228 File Offset: 0x0006A428
		private void CloseChannel()
		{
			lock (this)
			{
				if (this.channel != null)
				{
					this.channel.Close();
					this.channel = null;
				}
			}
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0006C278 File Offset: 0x0006A478
		private bool DoesSourceSupportExtensibleSeedingRequests()
		{
			AmServerName serverName = new AmServerName(this.sourceServerFqdn);
			Exception ex;
			IADServer miniServer = AmBestCopySelectionHelper.GetMiniServer(serverName, out ex);
			return miniServer != null && ServerVersion.Compare(miniServer.AdminDisplayVersion, CiFileSeederProvider.FirstVersionSupportingExtensibleSeedingRequests) >= 0;
		}

		// Token: 0x04000A54 RID: 2644
		private static readonly ServerVersion FirstVersionSupportingExtensibleSeedingRequests = new ServerVersion(15, 0, 963, 0);

		// Token: 0x04000A55 RID: 2645
		private readonly string sourceServerFqdn;

		// Token: 0x04000A56 RID: 2646
		private readonly string targetServerFqdn;

		// Token: 0x04000A57 RID: 2647
		private readonly Guid databaseGuid;

		// Token: 0x04000A58 RID: 2648
		private NetworkChannel channel;

		// Token: 0x04000A59 RID: 2649
		private bool isAborted;
	}
}
