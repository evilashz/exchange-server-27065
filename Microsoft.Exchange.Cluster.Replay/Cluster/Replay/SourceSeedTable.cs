using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002B8 RID: 696
	internal class SourceSeedTable
	{
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x00075260 File Offset: 0x00073460
		internal static SourceSeedTable Instance
		{
			get
			{
				return SourceSeedTable.s_mgr;
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x00075268 File Offset: 0x00073468
		public SeederServerContext TryGetContext(Guid guid)
		{
			SeederServerContext result = null;
			lock (this.locker)
			{
				if (this.activeSeeds.TryGetValue(guid, out result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000752BC File Offset: 0x000734BC
		public void RegisterSeed(SeederServerContext newCtx)
		{
			SeederServerContext seederServerContext = null;
			lock (this.locker)
			{
				SourceSeedTable.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "RegisterSeed {0}", newCtx.DatabaseGuid);
				if (this.activeSeeds.TryGetValue(newCtx.DatabaseGuid, out seederServerContext) && seederServerContext != null)
				{
					ReplayCrimsonEvents.SeedingSourceError.Log<Guid, string, string, string>(newCtx.DatabaseGuid, string.Empty, seederServerContext.TargetServerName, "RegisterSeed:SeedCtx already exists");
					throw new SeedingAnotherServerException(seederServerContext.TargetServerName, newCtx.TargetServerName);
				}
				this.activeSeeds[newCtx.DatabaseGuid] = newCtx;
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x00075370 File Offset: 0x00073570
		public void DeregisterSeed(SeederServerContext oldCtx)
		{
			SeederServerContext seederServerContext = null;
			SourceSeedTable.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "DeregisterSeed {0}", oldCtx.DatabaseGuid);
			lock (this.locker)
			{
				if (this.activeSeeds.TryGetValue(oldCtx.DatabaseGuid, out seederServerContext))
				{
					if (seederServerContext == oldCtx)
					{
						this.activeSeeds[oldCtx.DatabaseGuid] = null;
						SourceSeedTable.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "DeregisterSeed {0} successful.", oldCtx.DatabaseGuid);
					}
					else
					{
						SourceSeedTable.Tracer.TraceError<Guid>((long)this.GetHashCode(), "DeregisterSeed {0} ignored mismatached ctx.", oldCtx.DatabaseGuid);
					}
				}
			}
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x00075430 File Offset: 0x00073630
		public void CancelSeedingIfAppropriate(SourceSeedTable.CancelReason reason, Guid dbGuid)
		{
			SeederServerContext seederServerContext = null;
			lock (this.locker)
			{
				if (this.activeSeeds.TryGetValue(dbGuid, out seederServerContext) && seederServerContext != null)
				{
					SourceSeedTable.Tracer.TraceDebug<string, SourceSeedTable.CancelReason>((long)this.GetHashCode(), "CancelSeeding {0} : {1}", seederServerContext.DatabaseName, reason);
					if (reason == SourceSeedTable.CancelReason.ConfigChanged && seederServerContext.IsCatalogSeed)
					{
						SourceSeedTable.Tracer.TraceDebug<string>((long)this.GetHashCode(), "CancelSeeding skipped for {0} because catalog is seeding", seederServerContext.DatabaseName);
						return;
					}
					this.activeSeeds[seederServerContext.DatabaseGuid] = null;
				}
			}
			if (seederServerContext != null)
			{
				LocalizedString message;
				if (reason == SourceSeedTable.CancelReason.CopyFailed)
				{
					message = ReplayStrings.CancelSeedingDueToFailed(seederServerContext.DatabaseName, Environment.MachineName);
				}
				else
				{
					message = ReplayStrings.CancelSeedingDueToConfigChangeOrServiceShutdown(seederServerContext.DatabaseName, Environment.MachineName, reason.ToString());
				}
				seederServerContext.CancelSeeding(message);
			}
		}

		// Token: 0x04000AE7 RID: 2791
		private static readonly Trace Tracer = ExTraceGlobals.SeederServerTracer;

		// Token: 0x04000AE8 RID: 2792
		private static SourceSeedTable s_mgr = new SourceSeedTable();

		// Token: 0x04000AE9 RID: 2793
		private Dictionary<Guid, SeederServerContext> activeSeeds = new Dictionary<Guid, SeederServerContext>();

		// Token: 0x04000AEA RID: 2794
		private object locker = new object();

		// Token: 0x020002B9 RID: 697
		public enum CancelReason
		{
			// Token: 0x04000AEC RID: 2796
			CopyRemoved = 1,
			// Token: 0x04000AED RID: 2797
			CopySuspended,
			// Token: 0x04000AEE RID: 2798
			CopyFailed,
			// Token: 0x04000AEF RID: 2799
			ConfigChanged,
			// Token: 0x04000AF0 RID: 2800
			ServiceShutdown
		}
	}
}
