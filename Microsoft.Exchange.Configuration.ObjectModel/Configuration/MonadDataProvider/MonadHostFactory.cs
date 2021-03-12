using System;
using System.Management.Automation.Host;
using System.Threading;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001D8 RID: 472
	internal class MonadHostFactory : PSHostFactory
	{
		// Token: 0x06001118 RID: 4376 RVA: 0x00034550 File Offset: 0x00032750
		public static MonadHostFactory GetInstance()
		{
			if (MonadHostFactory.instance == null)
			{
				MonadHostFactory.instance = new MonadHostFactory();
			}
			return MonadHostFactory.instance;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00034568 File Offset: 0x00032768
		public override PSHost CreatePSHost()
		{
			return new MonadHost(Thread.CurrentThread.CurrentCulture, Thread.CurrentThread.CurrentUICulture);
		}

		// Token: 0x040003C4 RID: 964
		private static MonadHostFactory instance;
	}
}
