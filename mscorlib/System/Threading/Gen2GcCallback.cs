using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004E1 RID: 1249
	internal sealed class Gen2GcCallback : CriticalFinalizerObject
	{
		// Token: 0x06003BB6 RID: 15286 RVA: 0x000E1140 File Offset: 0x000DF340
		[SecuritySafeCritical]
		public Gen2GcCallback()
		{
		}

		// Token: 0x06003BB7 RID: 15287 RVA: 0x000E1148 File Offset: 0x000DF348
		public static void Register(Func<object, bool> callback, object targetObj)
		{
			Gen2GcCallback gen2GcCallback = new Gen2GcCallback();
			gen2GcCallback.Setup(callback, targetObj);
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x000E1163 File Offset: 0x000DF363
		[SecuritySafeCritical]
		private void Setup(Func<object, bool> callback, object targetObj)
		{
			this.m_callback = callback;
			this.m_weakTargetObj = GCHandle.Alloc(targetObj, GCHandleType.Weak);
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x000E117C File Offset: 0x000DF37C
		[SecuritySafeCritical]
		protected override void Finalize()
		{
			try
			{
				if (this.m_weakTargetObj.IsAllocated)
				{
					object target = this.m_weakTargetObj.Target;
					if (target == null)
					{
						this.m_weakTargetObj.Free();
					}
					else
					{
						try
						{
							if (!this.m_callback(target))
							{
								return;
							}
						}
						catch
						{
						}
						if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
						{
							GC.ReRegisterForFinalize(this);
						}
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x0400191C RID: 6428
		private Func<object, bool> m_callback;

		// Token: 0x0400191D RID: 6429
		private GCHandle m_weakTargetObj;
	}
}
