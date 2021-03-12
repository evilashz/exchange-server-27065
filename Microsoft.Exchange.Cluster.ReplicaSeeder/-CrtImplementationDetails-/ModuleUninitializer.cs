using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000108 RID: 264
	internal class ModuleUninitializer : Stack
	{
		// Token: 0x06000094 RID: 148 RVA: 0x000035C8 File Offset: 0x000029C8
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

		// Token: 0x06000096 RID: 150 RVA: 0x00003BF0 File Offset: 0x00002FF0
		[SecuritySafeCritical]
		private ModuleUninitializer()
		{
			EventHandler value = new EventHandler(this.SingletonDomainUnload);
			AppDomain.CurrentDomain.DomainUnload += value;
			AppDomain.CurrentDomain.ProcessExit += value;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003628 File Offset: 0x00002A28
		[SecurityCritical]
		[PrePrepareMethod]
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

		// Token: 0x04000244 RID: 580
		private static object @lock = new object();

		// Token: 0x04000245 RID: 581
		internal static ModuleUninitializer _ModuleUninitializer = new ModuleUninitializer();
	}
}
