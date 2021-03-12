using System;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200015C RID: 348
	internal class StateLockRemote
	{
		// Token: 0x06000E11 RID: 3601 RVA: 0x0003D37D File Offset: 0x0003B57D
		public StateLockRemote(string prefix, string dbName, IStateIO stateIO)
		{
			this.m_stateIO = stateIO;
			this.m_prefix = prefix;
			this.m_databaseName = dbName;
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0003D39A File Offset: 0x0003B59A
		public void EnterSuspend()
		{
			this.SuspendWanted = true;
			if (!this.SuspendWanted)
			{
				ExTraceGlobals.StateLockTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: EnterSuspend(): Error occurred while setting SuspendWanted flag.", this.m_databaseName);
				throw new SuspendWantedWriteFailedException();
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0003D3CD File Offset: 0x0003B5CD
		public bool TryLeaveSuspend()
		{
			this.SuspendWanted = false;
			return !this.SuspendWanted;
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0003D3E0 File Offset: 0x0003B5E0
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x0003D40D File Offset: 0x0003B60D
		internal bool SuspendWanted
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool(string.Format("{0}SuspendWanted", this.m_prefix), false, out result);
				return result;
			}
			private set
			{
				this.m_stateIO.WriteBool(string.Format("{0}SuspendWanted", this.m_prefix), value, true);
			}
		}

		// Token: 0x040005CE RID: 1486
		private const string SuspendWantedFormat = "{0}SuspendWanted";

		// Token: 0x040005CF RID: 1487
		private IStateIO m_stateIO;

		// Token: 0x040005D0 RID: 1488
		private string m_databaseName;

		// Token: 0x040005D1 RID: 1489
		private string m_prefix;
	}
}
