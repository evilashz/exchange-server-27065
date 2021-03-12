using System;
using System.Threading;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200037B RID: 891
	internal class TimerState : IDisposable
	{
		// Token: 0x060023B6 RID: 9142 RVA: 0x000A76F8 File Offset: 0x000A58F8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x000A7707 File Offset: 0x000A5907
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.M_thisTimer != null)
			{
				this.M_thisTimer.Dispose();
				this.M_thisTimer = null;
			}
		}

		// Token: 0x04000F4A RID: 3914
		internal ShipControl M_thisShip;

		// Token: 0x04000F4B RID: 3915
		internal Timer M_thisTimer;
	}
}
