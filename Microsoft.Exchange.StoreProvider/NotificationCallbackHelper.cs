using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001FB RID: 507
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NotificationCallbackHelper
	{
		// Token: 0x06000849 RID: 2121 RVA: 0x0002B06C File Offset: 0x0002926C
		private NotificationCallbackHelper()
		{
			this.callbackDictionaryArray = new NotificationCallbackHelper.CallbackDictionary[NotificationCallbackHelper.CallbackDictionaryArraySize];
			for (int i = 0; i < this.callbackDictionaryArray.Length; i++)
			{
				this.callbackDictionaryArray[i] = new NotificationCallbackHelper.CallbackDictionary();
			}
			this.onNotifyDelegate = new NotificationCallbackHelper.OnNotifyDelegate(this.OnNotify);
			this.intPtrOnNotifyDelegate = Marshal.GetFunctionPointerForDelegate(this.onNotifyDelegate);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0002B0D4 File Offset: 0x000292D4
		internal unsafe int OnNotify(ulong notificationCallbackId, int cNotifications, IntPtr iNotifications)
		{
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(31))
				{
					ComponentTrace<MapiNetTags>.Trace<ulong, int>(0, 31, (long)this.GetHashCode(), "NotificationCallbackHelper.OnNotify params: notificationCallbackId={0}, cNotifications={1}", notificationCallbackId, cNotifications);
				}
				if (iNotifications != IntPtr.Zero && cNotifications > 0)
				{
					NotificationHelper notificationHelper = this.GetNotificationHelper(notificationCallbackId);
					if (notificationHelper != null)
					{
						NOTIFICATION* ptr = (NOTIFICATION*)((void*)iNotifications);
						for (int i = 0; i < cNotifications; i++)
						{
							MapiNotification notification = MapiNotification.Create(ptr + i);
							notificationHelper.OnNotify(notification);
						}
					}
				}
			}
			catch (Exception e)
			{
				return Marshal.GetHRForException(e);
			}
			return 0;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0002B16C File Offset: 0x0002936C
		public IntPtr IntPtrOnNotifyDelegate
		{
			get
			{
				return this.intPtrOnNotifyDelegate;
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0002B174 File Offset: 0x00029374
		public ulong RegisterNotificationHelper(NotificationHelper notificationHelper)
		{
			ulong num = (ulong)Interlocked.Increment(ref this.nextNotificationCallbackId);
			this.GetCallbackDictionary(num).Add(num, notificationHelper);
			return num;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0002B19C File Offset: 0x0002939C
		public void UnregisterNotificationHelper(ulong notificationCallbackId)
		{
			this.GetCallbackDictionary(notificationCallbackId).Remove(notificationCallbackId);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0002B1AB File Offset: 0x000293AB
		private NotificationCallbackHelper.CallbackDictionary GetCallbackDictionary(ulong notificationCallbackId)
		{
			return this.callbackDictionaryArray[(int)(checked((IntPtr)(notificationCallbackId % unchecked((ulong)NotificationCallbackHelper.CallbackDictionaryArraySize))))];
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0002B1BD File Offset: 0x000293BD
		private NotificationHelper GetNotificationHelper(ulong notificationCallbackId)
		{
			return this.GetCallbackDictionary(notificationCallbackId).GetNotificationHelper(notificationCallbackId);
		}

		// Token: 0x040009C0 RID: 2496
		private static uint CallbackDictionaryArraySize = (uint)(4 * Environment.ProcessorCount);

		// Token: 0x040009C1 RID: 2497
		private readonly NotificationCallbackHelper.CallbackDictionary[] callbackDictionaryArray;

		// Token: 0x040009C2 RID: 2498
		private long nextNotificationCallbackId;

		// Token: 0x040009C3 RID: 2499
		private readonly NotificationCallbackHelper.OnNotifyDelegate onNotifyDelegate;

		// Token: 0x040009C4 RID: 2500
		private readonly IntPtr intPtrOnNotifyDelegate;

		// Token: 0x040009C5 RID: 2501
		public static readonly NotificationCallbackHelper Instance = new NotificationCallbackHelper();

		// Token: 0x020001FC RID: 508
		private class CallbackDictionary
		{
			// Token: 0x06000851 RID: 2129 RVA: 0x0002B1E4 File Offset: 0x000293E4
			public void Add(ulong notificationCallbackId, NotificationHelper notificationHelper)
			{
				try
				{
					this.callbackLock.EnterWriteLock();
					this.callbacks.Add(notificationCallbackId, notificationHelper);
				}
				finally
				{
					try
					{
						this.callbackLock.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
			}

			// Token: 0x06000852 RID: 2130 RVA: 0x0002B238 File Offset: 0x00029438
			public void Remove(ulong notificationCallbackId)
			{
				NotificationHelper notificationHelper = null;
				try
				{
					this.callbackLock.EnterWriteLock();
					if (this.callbacks.TryGetValue(notificationCallbackId, out notificationHelper))
					{
						this.callbacks.Remove(notificationCallbackId);
					}
				}
				finally
				{
					try
					{
						this.callbackLock.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
				if (notificationHelper != null)
				{
					notificationHelper.Dispose();
				}
			}

			// Token: 0x06000853 RID: 2131 RVA: 0x0002B2A8 File Offset: 0x000294A8
			public NotificationHelper GetNotificationHelper(ulong notificationCallbackId)
			{
				try
				{
					this.callbackLock.EnterReadLock();
					NotificationHelper result;
					if (this.callbacks.TryGetValue(notificationCallbackId, out result))
					{
						return result;
					}
				}
				finally
				{
					try
					{
						this.callbackLock.ExitReadLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
				return null;
			}

			// Token: 0x040009C6 RID: 2502
			private readonly Dictionary<ulong, NotificationHelper> callbacks = new Dictionary<ulong, NotificationHelper>(1);

			// Token: 0x040009C7 RID: 2503
			private readonly ReaderWriterLockSlim callbackLock = new ReaderWriterLockSlim();
		}

		// Token: 0x020001FD RID: 509
		// (Invoke) Token: 0x06000856 RID: 2134
		internal delegate int OnNotifyDelegate(ulong notificationCallbackId, int cNotifications, IntPtr iNotifications);
	}
}
