using System;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000172 RID: 370
	internal class DDIVMockRbacPrincipal : RbacPrincipal, IDisposable
	{
		// Token: 0x06002222 RID: 8738 RVA: 0x00066EAC File Offset: 0x000650AC
		public DDIVMockRbacPrincipal() : base(Activator.CreateInstance(typeof(DDIVMockedExchangeRunspaceConfiguration), true) as ExchangeRunspaceConfiguration, "MockRbacPrincipal")
		{
			HttpContext.Current = new HttpContext(new SimpleWorkerRequest("/", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), string.Empty, string.Empty, new StringWriter()));
			this.originalPrincipal = Thread.CurrentPrincipal;
			this.SetCurrentThreadPrincipal();
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x00066F14 File Offset: 0x00065114
		void IDisposable.Dispose()
		{
			Thread.CurrentPrincipal = this.originalPrincipal;
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x00066F21 File Offset: 0x00065121
		protected override bool IsInRole(string rbacQuery, out bool canCache, ADRawEntry adRawEntry)
		{
			canCache = true;
			return true;
		}

		// Token: 0x04001D62 RID: 7522
		private IPrincipal originalPrincipal;
	}
}
