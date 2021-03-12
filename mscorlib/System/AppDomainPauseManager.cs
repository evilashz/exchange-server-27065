using System;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x0200009C RID: 156
	[SecurityCritical]
	internal class AppDomainPauseManager
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x0001D5E0 File Offset: 0x0001B7E0
		[SecurityCritical]
		public AppDomainPauseManager()
		{
			AppDomainPauseManager.isPaused = false;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0001D5FC File Offset: 0x0001B7FC
		internal static AppDomainPauseManager Instance
		{
			[SecurityCritical]
			get
			{
				return AppDomainPauseManager.instance;
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001D603 File Offset: 0x0001B803
		[SecurityCritical]
		public void Pausing()
		{
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001D605 File Offset: 0x0001B805
		[SecurityCritical]
		public void Paused()
		{
			if (AppDomainPauseManager.ResumeEvent == null)
			{
				AppDomainPauseManager.ResumeEvent = new ManualResetEvent(false);
			}
			else
			{
				AppDomainPauseManager.ResumeEvent.Reset();
			}
			Timer.Pause();
			AppDomainPauseManager.isPaused = true;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001D633 File Offset: 0x0001B833
		[SecurityCritical]
		public void Resuming()
		{
			AppDomainPauseManager.isPaused = false;
			AppDomainPauseManager.ResumeEvent.Set();
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001D648 File Offset: 0x0001B848
		[SecurityCritical]
		public void Resumed()
		{
			Timer.Resume();
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0001D64F File Offset: 0x0001B84F
		internal static bool IsPaused
		{
			[SecurityCritical]
			get
			{
				return AppDomainPauseManager.isPaused;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0001D658 File Offset: 0x0001B858
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x0001D65F File Offset: 0x0001B85F
		internal static ManualResetEvent ResumeEvent { [SecurityCritical] get; [SecurityCritical] set; }

		// Token: 0x040003A3 RID: 931
		private static readonly AppDomainPauseManager instance = new AppDomainPauseManager();

		// Token: 0x040003A4 RID: 932
		private static volatile bool isPaused;
	}
}
