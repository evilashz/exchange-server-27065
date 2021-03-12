using System;
using System.Collections.Generic;
using System.Web.Hosting;

namespace Microsoft.Exchange.Services
{
	// Token: 0x02000026 RID: 38
	internal class ShutdownHandler : IRegisteredObject
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x00009E0E File Offset: 0x0000800E
		private ShutdownHandler()
		{
			HostingEnvironment.RegisterObject(this);
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00009E27 File Offset: 0x00008027
		internal static ShutdownHandler Singleton
		{
			get
			{
				return ShutdownHandler.singleton;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00009E30 File Offset: 0x00008030
		internal void Add(IDisposable disposable)
		{
			lock (this.disposableList)
			{
				this.disposableList.Add(disposable);
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00009E78 File Offset: 0x00008078
		void IRegisteredObject.Stop(bool immediate)
		{
			try
			{
				lock (this.disposableList)
				{
					foreach (IDisposable disposable in this.disposableList)
					{
						disposable.Dispose();
					}
					this.disposableList.Clear();
				}
			}
			finally
			{
				HostingEnvironment.UnregisterObject(this);
			}
		}

		// Token: 0x040001BB RID: 443
		private static ShutdownHandler singleton = new ShutdownHandler();

		// Token: 0x040001BC RID: 444
		private List<IDisposable> disposableList = new List<IDisposable>();
	}
}
