using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001CF RID: 463
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DownCastEnumerator<TFrom, TTo> : IEnumerator<TTo>, IDisposable, IEnumerator where TTo : TFrom
	{
		// Token: 0x060018A2 RID: 6306 RVA: 0x000784C4 File Offset: 0x000766C4
		internal DownCastEnumerator(IEnumerator<TFrom> fromEnumerator)
		{
			this.fromEnumerator = fromEnumerator;
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x000784D3 File Offset: 0x000766D3
		public TTo Current
		{
			get
			{
				this.CheckDisposed("Current<T>::get");
				return (TTo)((object)this.fromEnumerator.Current);
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x000784F5 File Offset: 0x000766F5
		object IEnumerator.Current
		{
			get
			{
				this.CheckDisposed("Current<T>::get");
				return this.Current;
			}
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0007850D File Offset: 0x0007670D
		public void Reset()
		{
			this.CheckDisposed("Current<T>::get");
			this.fromEnumerator.Reset();
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00078525 File Offset: 0x00076725
		public bool MoveNext()
		{
			this.CheckDisposed("Current<T>::get");
			return this.fromEnumerator.MoveNext();
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0007853D File Offset: 0x0007673D
		private void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00078558 File Offset: 0x00076758
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00078567 File Offset: 0x00076767
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0007857F File Offset: 0x0007677F
		private void InternalDispose(bool disposing)
		{
			if (disposing && this.fromEnumerator != null)
			{
				this.fromEnumerator.Dispose();
			}
		}

		// Token: 0x04000CE4 RID: 3300
		private IEnumerator<TFrom> fromEnumerator;

		// Token: 0x04000CE5 RID: 3301
		private bool isDisposed;
	}
}
