using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace msclr
{
	// Token: 0x020001D9 RID: 473
	internal class @lock : IDisposable
	{
		// Token: 0x060009DC RID: 2524 RVA: 0x0001C554 File Offset: 0x0001B954
		public @lock(object _object)
		{
			this.acquire(-1);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001C57C File Offset: 0x0001B97C
		private void ~lock()
		{
			this.release();
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001B590 File Offset: 0x0001A990
		[return: MarshalAs(UnmanagedType.U1)]
		public bool is_locked()
		{
			return this.m_locked;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001B5A4 File Offset: 0x0001A9A4
		public implicit operator string()
		{
			return (!this.m_locked) ? _detail_class._safe_false : _detail_class._safe_true;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0001B640 File Offset: 0x0001AA40
		public void acquire(TimeSpan _timeout)
		{
			if (!this.m_locked)
			{
				Monitor.TryEnter(this.m_object, _timeout, ref this.m_locked);
				if (!this.m_locked)
				{
					throw Marshal.GetExceptionForHR(-2147024638);
				}
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0001B604 File Offset: 0x0001AA04
		public void acquire()
		{
			if (!this.m_locked)
			{
				Monitor.TryEnter(this.m_object, -1, ref this.m_locked);
				if (!this.m_locked)
				{
					throw Marshal.GetExceptionForHR(-2147024638);
				}
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0001B5C8 File Offset: 0x0001A9C8
		public void acquire(int _timeout)
		{
			if (!this.m_locked)
			{
				Monitor.TryEnter(this.m_object, _timeout, ref this.m_locked);
				if (!this.m_locked)
				{
					throw Marshal.GetExceptionForHR(-2147024638);
				}
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0001B6B0 File Offset: 0x0001AAB0
		[return: MarshalAs(UnmanagedType.U1)]
		public bool try_acquire(TimeSpan _timeout)
		{
			if (!this.m_locked)
			{
				Monitor.TryEnter(this.m_object, _timeout, ref this.m_locked);
				if (!this.m_locked)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0001B67C File Offset: 0x0001AA7C
		[return: MarshalAs(UnmanagedType.U1)]
		public bool try_acquire(int _timeout)
		{
			if (!this.m_locked)
			{
				Monitor.TryEnter(this.m_object, _timeout, ref this.m_locked);
				if (!this.m_locked)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001B6E4 File Offset: 0x0001AAE4
		public void release()
		{
			if (this.m_locked)
			{
				Monitor.Exit(this.m_object);
				this.m_locked = false;
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0001C590 File Offset: 0x0001B990
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.release();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0001DAB4 File Offset: 0x0001CEB4
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000B86 RID: 2950
		private object m_object = _object;

		// Token: 0x04000B87 RID: 2951
		private bool m_locked = false;

		// Token: 0x020001ED RID: 493
		private struct is_not<System::Object,System::Threading::ReaderWriterLock>
		{
		}
	}
}
