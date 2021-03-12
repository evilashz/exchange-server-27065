using System;
using System.Globalization;
using System.Management.Automation.Host;
using System.Threading;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000923 RID: 2339
	internal class RemotePowershellHost : PSHost
	{
		// Token: 0x06005329 RID: 21289 RVA: 0x00157CFA File Offset: 0x00155EFA
		public RemotePowershellHost(PowershellHostUI hostUI)
		{
			this.hostUI = hostUI;
		}

		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x0600532A RID: 21290 RVA: 0x00157D34 File Offset: 0x00155F34
		public override CultureInfo CurrentCulture
		{
			get
			{
				return this.originalCultureInfo;
			}
		}

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x0600532B RID: 21291 RVA: 0x00157D3C File Offset: 0x00155F3C
		public override CultureInfo CurrentUICulture
		{
			get
			{
				return this.originalUICultureInfo;
			}
		}

		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x00157D44 File Offset: 0x00155F44
		public override Guid InstanceId
		{
			get
			{
				return this.myId;
			}
		}

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x0600532D RID: 21293 RVA: 0x00157D4C File Offset: 0x00155F4C
		public override string Name
		{
			get
			{
				return "HybridHost";
			}
		}

		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x0600532E RID: 21294 RVA: 0x00157D53 File Offset: 0x00155F53
		public override PSHostUserInterface UI
		{
			get
			{
				return this.hostUI;
			}
		}

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x0600532F RID: 21295 RVA: 0x00157D5B File Offset: 0x00155F5B
		public override Version Version
		{
			get
			{
				return new Version(1, 0, 0, 0);
			}
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x00157D66 File Offset: 0x00155F66
		public override void EnterNestedPrompt()
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x00157D72 File Offset: 0x00155F72
		public override void ExitNestedPrompt()
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x06005332 RID: 21298 RVA: 0x00157D7E File Offset: 0x00155F7E
		public override void NotifyBeginApplication()
		{
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x00157D80 File Offset: 0x00155F80
		public override void NotifyEndApplication()
		{
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x00157D82 File Offset: 0x00155F82
		public override void SetShouldExit(int exitCode)
		{
		}

		// Token: 0x040030B0 RID: 12464
		private CultureInfo originalCultureInfo = Thread.CurrentThread.CurrentCulture;

		// Token: 0x040030B1 RID: 12465
		private CultureInfo originalUICultureInfo = Thread.CurrentThread.CurrentUICulture;

		// Token: 0x040030B2 RID: 12466
		private readonly Guid myId = Guid.NewGuid();

		// Token: 0x040030B3 RID: 12467
		private PowershellHostUI hostUI;
	}
}
