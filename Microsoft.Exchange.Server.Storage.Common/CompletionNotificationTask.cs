using System;
using System.Threading;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000018 RID: 24
	internal class CompletionNotificationTask<T> : Task<T>
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00005599 File Offset: 0x00003799
		public CompletionNotificationTask(CompletionNotificationTask<T>.CompletionNotificationCallback callback, T context, IoCompletionPort completionPort, uint idleTimeout, bool autoStart) : base(null, context, ThreadPriority.Normal, 0, autoStart ? TaskFlags.AutoStart : TaskFlags.None)
		{
			base.CallbackDelegate = new Task<T>.TaskCallback(this.NotificationListener);
			this.notificationCallback = callback;
			this.completionPort = completionPort;
			this.idleTimeout = idleTimeout;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000055D5 File Offset: 0x000037D5
		public override void Stop()
		{
			base.CheckDisposed();
			base.Stop();
			this.completionPort.PostQueuedCompletionStatus(uint.MaxValue, uint.MaxValue, IntPtr.Zero);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000055F8 File Offset: 0x000037F8
		private void NotificationListener(TaskExecutionDiagnosticsProxy diagnosticsContext, T context, Func<bool> shouldCallbackContinue)
		{
			ThreadManager.MarkCurrentThreadAsLongRunning();
			while (shouldCallbackContinue())
			{
				uint num = 0U;
				UIntPtr uintPtr = new UIntPtr(0U);
				int data = 0;
				bool queuedCompletionStatus = this.completionPort.GetQueuedCompletionStatus(out num, out uintPtr, out data, this.idleTimeout);
				if (queuedCompletionStatus)
				{
					this.notificationCallback(context, shouldCallbackContinue, num, uintPtr.ToUInt32(), data);
				}
				else
				{
					this.notificationCallback(context, shouldCallbackContinue, 4294967294U, uint.MaxValue, 0);
				}
				if (uintPtr.ToUInt32() == 4294967295U && num == 4294967294U)
				{
					return;
				}
			}
		}

		// Token: 0x040002FD RID: 765
		public const uint TaskCompletionKey = 4294967295U;

		// Token: 0x040002FE RID: 766
		private IoCompletionPort completionPort;

		// Token: 0x040002FF RID: 767
		private uint idleTimeout;

		// Token: 0x04000300 RID: 768
		private CompletionNotificationTask<T>.CompletionNotificationCallback notificationCallback;

		// Token: 0x02000019 RID: 25
		public enum TaksNotifications : uint
		{
			// Token: 0x04000302 RID: 770
			Timeout = 4294967294U,
			// Token: 0x04000303 RID: 771
			Exit
		}

		// Token: 0x0200001A RID: 26
		// (Invoke) Token: 0x06000258 RID: 600
		public delegate void CompletionNotificationCallback(T context, Func<bool> shouldCallbackContinue, uint notification, uint completionKey, int data);
	}
}
