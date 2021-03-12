using System;
using System.Runtime.CompilerServices;
using System.Threading.NetCore;

namespace System.Threading
{
	// Token: 0x02000503 RID: 1283
	internal sealed class TimerHolder
	{
		// Token: 0x06003D2D RID: 15661 RVA: 0x000E3B21 File Offset: 0x000E1D21
		public TimerHolder(object timer)
		{
			this.m_timer = timer;
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x000E3B30 File Offset: 0x000E1D30
		~TimerHolder()
		{
			if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
			{
				if (Timer.UseNetCoreTimer)
				{
					this.NetCoreTimer.Close();
				}
				else
				{
					this.NetFxTimer.Close();
				}
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x000E3B8C File Offset: 0x000E1D8C
		private TimerQueueTimer NetFxTimer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (TimerQueueTimer)this.m_timer;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06003D30 RID: 15664 RVA: 0x000E3B99 File Offset: 0x000E1D99
		private TimerQueueTimer NetCoreTimer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (TimerQueueTimer)this.m_timer;
			}
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x000E3BA6 File Offset: 0x000E1DA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Change(uint dueTime, uint period)
		{
			if (!Timer.UseNetCoreTimer)
			{
				return this.NetFxTimer.Change(dueTime, period);
			}
			return this.NetCoreTimer.Change(dueTime, period);
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x000E3BCA File Offset: 0x000E1DCA
		public void Close()
		{
			if (Timer.UseNetCoreTimer)
			{
				this.NetCoreTimer.Close();
			}
			else
			{
				this.NetFxTimer.Close();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x000E3BF4 File Offset: 0x000E1DF4
		public bool Close(WaitHandle notifyObject)
		{
			bool result = Timer.UseNetCoreTimer ? this.NetCoreTimer.Close(notifyObject) : this.NetFxTimer.Close(notifyObject);
			GC.SuppressFinalize(this);
			return result;
		}

		// Token: 0x0400198A RID: 6538
		private object m_timer;
	}
}
