using System;
using System.Globalization;
using System.Management.Automation.Host;
using System.Threading;

namespace Microsoft.Exchange.Management.Deployment.HybridConfigurationDetection
{
	// Token: 0x02000034 RID: 52
	internal class RemotePowershellHost : PSHost
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00004EEC File Offset: 0x000030EC
		public RemotePowershellHost(PowershellHostUI hostUI)
		{
			this.hostUI = hostUI;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004F26 File Offset: 0x00003126
		public override CultureInfo CurrentCulture
		{
			get
			{
				return this.originalCultureInfo;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004F2E File Offset: 0x0000312E
		public override CultureInfo CurrentUICulture
		{
			get
			{
				return this.originalUICultureInfo;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004F36 File Offset: 0x00003136
		public override Guid InstanceId
		{
			get
			{
				return this.myId;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004F3E File Offset: 0x0000313E
		public override string Name
		{
			get
			{
				return "HybridHost";
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004F45 File Offset: 0x00003145
		public override PSHostUserInterface UI
		{
			get
			{
				return this.hostUI;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004F4D File Offset: 0x0000314D
		public override Version Version
		{
			get
			{
				return new Version(1, 0, 0, 0);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004F58 File Offset: 0x00003158
		public override void EnterNestedPrompt()
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004F64 File Offset: 0x00003164
		public override void ExitNestedPrompt()
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004F70 File Offset: 0x00003170
		public override void NotifyBeginApplication()
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004F72 File Offset: 0x00003172
		public override void NotifyEndApplication()
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004F74 File Offset: 0x00003174
		public override void SetShouldExit(int exitCode)
		{
		}

		// Token: 0x040000BA RID: 186
		private readonly Guid myId = Guid.NewGuid();

		// Token: 0x040000BB RID: 187
		private CultureInfo originalCultureInfo = Thread.CurrentThread.CurrentCulture;

		// Token: 0x040000BC RID: 188
		private CultureInfo originalUICultureInfo = Thread.CurrentThread.CurrentUICulture;

		// Token: 0x040000BD RID: 189
		private PowershellHostUI hostUI;
	}
}
