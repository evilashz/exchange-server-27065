using System;
using System.ComponentModel;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x0200006C RID: 108
	internal sealed class RegistryWatcher
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00006790 File Offset: 0x00004990
		public RegistryWatcher(string keyName, bool watchSubtree)
		{
			this.watchSubtree = watchSubtree;
			this.keyName = keyName;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000067BC File Offset: 0x000049BC
		public bool IsChanged()
		{
			return this.IsChanged(0, null);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000067C8 File Offset: 0x000049C8
		public bool IsChanged(int timeout, WaitHandle cancelEvent)
		{
			if (this.registryKey.IsInvalid || this.registryKey.IsClosed)
			{
				SafeRegistryHandle safeRegistryHandle;
				DiagnosticsNativeMethods.ErrorCode errorCode = DiagnosticsNativeMethods.RegOpenKeyEx(SafeRegistryHandle.LocalMachine, this.keyName, 0, 131097, out safeRegistryHandle);
				if (DiagnosticsNativeMethods.ErrorCode.FileNotFound == errorCode)
				{
					return false;
				}
				if (errorCode != DiagnosticsNativeMethods.ErrorCode.Success)
				{
					throw new Win32Exception((int)errorCode);
				}
				try
				{
					this.rwl.EnterWriteLock();
					if (this.registryKey.IsInvalid || this.registryKey.IsClosed)
					{
						this.registryKey = safeRegistryHandle;
						this.notifyEvent = new AutoResetEvent(false);
						errorCode = this.RegisterKeyChangeNotification(this.registryKey, this.watchSubtree, DiagnosticsNativeMethods.RegistryNotifications.ChangeName | DiagnosticsNativeMethods.RegistryNotifications.ChangeAttributes | DiagnosticsNativeMethods.RegistryNotifications.LastSet, this.notifyEvent, true);
						if (errorCode != DiagnosticsNativeMethods.ErrorCode.Success)
						{
							this.notifyEvent.Dispose();
							this.notifyEvent = null;
							this.registryKey.Dispose();
							throw new Win32Exception((int)errorCode);
						}
						return true;
					}
					else
					{
						safeRegistryHandle.Dispose();
					}
				}
				finally
				{
					this.rwl.ExitWriteLock();
				}
			}
			try
			{
				this.rwl.EnterReadLock();
				bool flag = false;
				if (cancelEvent != null)
				{
					if (WaitHandle.WaitAny(new WaitHandle[]
					{
						this.notifyEvent,
						cancelEvent
					}, timeout) == 0)
					{
						flag = true;
					}
				}
				else if (this.notifyEvent != null)
				{
					flag = this.notifyEvent.WaitOne(timeout);
				}
				if (!flag)
				{
					return false;
				}
			}
			finally
			{
				try
				{
					this.rwl.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			try
			{
				this.rwl.EnterWriteLock();
				DiagnosticsNativeMethods.ErrorCode errorCode = this.RegisterKeyChangeNotification(this.registryKey, this.watchSubtree, DiagnosticsNativeMethods.RegistryNotifications.ChangeName | DiagnosticsNativeMethods.RegistryNotifications.ChangeAttributes | DiagnosticsNativeMethods.RegistryNotifications.LastSet, this.notifyEvent, true);
				if (errorCode != DiagnosticsNativeMethods.ErrorCode.Success)
				{
					this.notifyEvent.Dispose();
					this.notifyEvent = null;
					this.registryKey.Dispose();
					if (DiagnosticsNativeMethods.ErrorCode.KeyDeleted != errorCode)
					{
						throw new Win32Exception((int)errorCode);
					}
					errorCode = DiagnosticsNativeMethods.RegOpenKeyEx(SafeRegistryHandle.LocalMachine, this.keyName, 0, 131097, out this.registryKey);
					if (DiagnosticsNativeMethods.ErrorCode.FileNotFound != errorCode && errorCode != DiagnosticsNativeMethods.ErrorCode.Success)
					{
						throw new Win32Exception((int)errorCode);
					}
					if (!this.registryKey.IsInvalid && !this.registryKey.IsClosed)
					{
						this.notifyEvent = new AutoResetEvent(false);
						errorCode = this.RegisterKeyChangeNotification(this.registryKey, this.watchSubtree, DiagnosticsNativeMethods.RegistryNotifications.ChangeName | DiagnosticsNativeMethods.RegistryNotifications.ChangeAttributes | DiagnosticsNativeMethods.RegistryNotifications.LastSet, this.notifyEvent, true);
						if (errorCode != DiagnosticsNativeMethods.ErrorCode.Success)
						{
							this.notifyEvent.Dispose();
							this.notifyEvent = null;
							this.registryKey.Dispose();
							throw new Win32Exception((int)errorCode);
						}
					}
				}
			}
			finally
			{
				try
				{
					this.rwl.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return true;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00006A5C File Offset: 0x00004C5C
		public string KeyName
		{
			get
			{
				return this.keyName;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006A64 File Offset: 0x00004C64
		private DiagnosticsNativeMethods.ErrorCode RegisterKeyChangeNotification(SafeRegistryHandle key, bool watchSubtree, DiagnosticsNativeMethods.RegistryNotifications notifyFilter, AutoResetEvent notifyEvent, bool asynchronous)
		{
			return DiagnosticsNativeMethods.RegNotifyChangeKeyValue(this.registryKey, watchSubtree, notifyFilter, notifyEvent.SafeWaitHandle, asynchronous);
		}

		// Token: 0x04000215 RID: 533
		private readonly bool watchSubtree;

		// Token: 0x04000216 RID: 534
		private readonly string keyName;

		// Token: 0x04000217 RID: 535
		private SafeRegistryHandle registryKey = new SafeRegistryHandle();

		// Token: 0x04000218 RID: 536
		private AutoResetEvent notifyEvent;

		// Token: 0x04000219 RID: 537
		private ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();
	}
}
