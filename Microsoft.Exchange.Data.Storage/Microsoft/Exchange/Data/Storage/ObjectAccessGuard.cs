using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000290 RID: 656
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct ObjectAccessGuard : IDisposable
	{
		// Token: 0x06001B52 RID: 6994 RVA: 0x0007E2B4 File Offset: 0x0007C4B4
		public static ObjectAccessGuard Create(ObjectThreadTracker objectThreadTracker, string methodName)
		{
			return new ObjectAccessGuard(objectThreadTracker, methodName);
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0007E2C0 File Offset: 0x0007C4C0
		private ObjectAccessGuard(ObjectThreadTracker objectThreadTracker, string methodName)
		{
			this.isDisposed = false;
			this.objectThreadTracker = null;
			this.testHook = null;
			if (ObjectAccessGuard.testHookFactory.Value != null)
			{
				this.testHook = ObjectAccessGuard.testHookFactory.Value(objectThreadTracker, methodName);
				return;
			}
			if (objectThreadTracker == null)
			{
				throw new ArgumentNullException("objectThreadTracker");
			}
			this.objectThreadTracker = objectThreadTracker;
			this.objectThreadTracker.Enter(methodName);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0007E327 File Offset: 0x0007C527
		internal static IDisposable SetTestHook(Func<ObjectThreadTracker, string, IDisposable> factory)
		{
			return ObjectAccessGuard.testHookFactory.SetTestHook(factory);
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0007E334 File Offset: 0x0007C534
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0007E33D File Offset: 0x0007C53D
		private void Dispose(bool disposing)
		{
			if (disposing && !this.isDisposed)
			{
				this.isDisposed = true;
				if (this.testHook != null)
				{
					this.testHook.Dispose();
					return;
				}
				if (this.objectThreadTracker != null)
				{
					this.objectThreadTracker.Exit();
				}
			}
		}

		// Token: 0x040012F9 RID: 4857
		private static Hookable<Func<ObjectThreadTracker, string, IDisposable>> testHookFactory = Hookable<Func<ObjectThreadTracker, string, IDisposable>>.Create(true, null);

		// Token: 0x040012FA RID: 4858
		private bool isDisposed;

		// Token: 0x040012FB RID: 4859
		private ObjectThreadTracker objectThreadTracker;

		// Token: 0x040012FC RID: 4860
		private IDisposable testHook;
	}
}
