using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200022D RID: 557
	internal class ModuleUninitializer : Stack
	{
		// Token: 0x06000126 RID: 294 RVA: 0x0000DDAC File Offset: 0x0000D1AC
		[SecuritySafeCritical]
		internal void AddHandler(EventHandler handler)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				Monitor.Enter(ModuleUninitializer.@lock, ref flag);
				RuntimeHelpers.PrepareDelegate(handler);
				this.Push(handler);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(ModuleUninitializer.@lock);
				}
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000E3D4 File Offset: 0x0000D7D4
		[SecuritySafeCritical]
		private ModuleUninitializer()
		{
			EventHandler value = new EventHandler(this.SingletonDomainUnload);
			AppDomain.CurrentDomain.DomainUnload += value;
			AppDomain.CurrentDomain.ProcessExit += value;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000DE0C File Offset: 0x0000D20C
		[PrePrepareMethod]
		[SecurityCritical]
		private void SingletonDomainUnload(object source, EventArgs arguments)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				Monitor.Enter(ModuleUninitializer.@lock, ref flag);
				using (IEnumerator enumerator = this.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						((EventHandler)enumerator.Current)(source, arguments);
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(ModuleUninitializer.@lock);
				}
			}
		}

		// Token: 0x0400036B RID: 875
		private static object @lock = new object();

		// Token: 0x0400036C RID: 876
		internal static ModuleUninitializer _ModuleUninitializer = new ModuleUninitializer();
	}
}
