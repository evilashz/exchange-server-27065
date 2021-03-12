using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics.Components.ContentFilter;

namespace Microsoft.Exchange.Data.Transport.Interop
{
	// Token: 0x02000005 RID: 5
	[ComVisible(false)]
	internal sealed class ComProxy : IDisposable
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002260 File Offset: 0x00000460
		public ComProxy(Guid guidClass)
		{
			this.comObject = ComProxy.CreateComObject(guidClass);
			if (this.comObject == null)
			{
				throw new InvalidOperationException("CreateComObject() returned a null proxy.");
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002288 File Offset: 0x00000488
		~ComProxy()
		{
			ExTraceGlobals.ComInteropTracer.TraceDebug(0L, "Finalizer called on ComProxy object that wasn't disposed");
			this.Dispose();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022C8 File Offset: 0x000004C8
		private static IComInvoke CreateComObject(Guid guidClass)
		{
			Guid riid = new Guid("786E6730-5D95-3D9D-951B-5C9ABD1E158D");
			return (IComInvoke)UnsafeNativeMethods.CoCreateInstance(guidClass, null, 4U, riid);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022EF File Offset: 0x000004EF
		private static void Release(IComInvoke comObject)
		{
			Marshal.ReleaseComObject(comObject);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022F8 File Offset: 0x000004F8
		public void Invoke(ComProxy.AsyncCompletionCallback asyncComplete, ComArguments propertyBag, MailItem mailItem)
		{
			if (this.comObject == null)
			{
				throw new InvalidOperationException("comObject cannot be null when invoking ComProxy object");
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException("ComProxy");
			}
			InvocationContext callback = new InvocationContext(asyncComplete, propertyBag, mailItem);
			this.comObject.ComAsyncInvoke(callback);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002340 File Offset: 0x00000540
		public void Dispose()
		{
			if (!this.disposed)
			{
				if (this.comObject != null)
				{
					ComProxy.Release(this.comObject);
					this.comObject = null;
				}
				this.disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000006 RID: 6
		private bool disposed;

		// Token: 0x04000007 RID: 7
		private IComInvoke comObject;

		// Token: 0x02000006 RID: 6
		// (Invoke) Token: 0x06000014 RID: 20
		[ComVisible(false)]
		public delegate void AsyncCompletionCallback(ComArguments propertyBag);
	}
}
