using System;
using System.Globalization;
using System.Threading;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200054A RID: 1354
	internal class ThreadCultureSwitch : IDisposable
	{
		// Token: 0x06003FA1 RID: 16289 RVA: 0x000BFEB1 File Offset: 0x000BE0B1
		public ThreadCultureSwitch() : this(CultureInfo.InstalledUICulture)
		{
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x000BFEC0 File Offset: 0x000BE0C0
		public ThreadCultureSwitch(CultureInfo newCulture)
		{
			this.thread = Thread.CurrentThread;
			this.previousCulture = this.thread.CurrentCulture;
			this.previousUICulture = this.thread.CurrentUICulture;
			this.thread.CurrentCulture = newCulture;
			this.thread.CurrentUICulture = newCulture;
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x000BFF18 File Offset: 0x000BE118
		public void Dispose()
		{
			this.thread.CurrentCulture = this.previousCulture;
			this.thread.CurrentUICulture = this.previousUICulture;
		}

		// Token: 0x0400293C RID: 10556
		private Thread thread;

		// Token: 0x0400293D RID: 10557
		private CultureInfo previousCulture;

		// Token: 0x0400293E RID: 10558
		private CultureInfo previousUICulture;
	}
}
