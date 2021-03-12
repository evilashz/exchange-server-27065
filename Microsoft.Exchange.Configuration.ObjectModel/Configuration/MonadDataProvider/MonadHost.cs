using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics.Components.Monad;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001D0 RID: 464
	internal class MonadHost : RunspaceHost
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x00032B90 File Offset: 0x00030D90
		internal MonadHost(CultureInfo currentCulture, CultureInfo currentUICulture)
		{
			this.currentCulture = currentCulture;
			this.currentUICulture = currentUICulture;
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x00032BB1 File Offset: 0x00030DB1
		public static string ServerVersion
		{
			get
			{
				return "Exchange Management Console:" + MonadHost.version;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00032BC2 File Offset: 0x00030DC2
		public override CultureInfo CurrentCulture
		{
			get
			{
				return this.currentCulture;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x00032BCA File Offset: 0x00030DCA
		public override CultureInfo CurrentUICulture
		{
			get
			{
				return this.currentUICulture;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x00032BD2 File Offset: 0x00030DD2
		public override Guid InstanceId
		{
			get
			{
				return this.instanceID;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00032BDA File Offset: 0x00030DDA
		public override string Name
		{
			get
			{
				return "Exchange Management Console";
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x00032BE1 File Offset: 0x00030DE1
		public override Version Version
		{
			get
			{
				return MonadHost.version;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x00032BE8 File Offset: 0x00030DE8
		// (set) Token: 0x06001092 RID: 4242 RVA: 0x00032BFE File Offset: 0x00030DFE
		protected MonadConnection Connection
		{
			get
			{
				if (this.connection == null)
				{
					throw new NotSupportedException();
				}
				return this.connection;
			}
			set
			{
				this.connection = value;
			}
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00032C08 File Offset: 0x00030E08
		public static void InitializeMonadHostConnection(RunspaceHost runspaceHost, MonadConnection connection)
		{
			MonadHost monadHost = runspaceHost as MonadHost;
			if (monadHost == null)
			{
				return;
			}
			monadHost.connection = connection;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00032C27 File Offset: 0x00030E27
		public override void Deactivate()
		{
			this.connection = null;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00032C30 File Offset: 0x00030E30
		public override void NotifyBeginApplication()
		{
			ExTraceGlobals.HostTracer.Information((long)this.GetHashCode(), "MonadHost.NotifyBeginApplication()");
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00032C48 File Offset: 0x00030E48
		public override void NotifyEndApplication()
		{
			ExTraceGlobals.HostTracer.Information((long)this.GetHashCode(), "MonadHost.NotifyEndApplication()");
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00032C60 File Offset: 0x00030E60
		public override void SetShouldExit(int exitCode)
		{
			ExTraceGlobals.HostTracer.Information((long)this.GetHashCode(), "MonadHost.SetShouldExit()");
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00032C78 File Offset: 0x00030E78
		public override void EnterNestedPrompt()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00032C7F File Offset: 0x00030E7F
		public override void ExitNestedPrompt()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00032C86 File Offset: 0x00030E86
		public override Dictionary<string, PSObject> Prompt(string caption, string message, Collection<FieldDescription> descriptions)
		{
			return this.Connection.InteractionHandler.Prompt(caption, message, descriptions);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00032C9B File Offset: 0x00030E9B
		public override int PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices, int defaultChoice)
		{
			ExTraceGlobals.HostTracer.Information<string, int>((long)this.GetHashCode(), "MonadHost.PromptForChoice({0}, {1})", message, defaultChoice);
			return (int)this.Connection.InteractionHandler.ShowConfirmationDialog(message, (ConfirmationChoice)defaultChoice);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00032CC9 File Offset: 0x00030EC9
		public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
		{
			ExTraceGlobals.HostTracer.Information<string>((long)this.GetHashCode(), "MonadHost.Write({0})", value);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00032CE2 File Offset: 0x00030EE2
		public override void Write(string value)
		{
			ExTraceGlobals.HostTracer.Information<string>((long)this.GetHashCode(), "MonadGhostUI.Write({0})", value);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00032CFB File Offset: 0x00030EFB
		public override void WriteDebugLine(string message)
		{
			ExTraceGlobals.HostTracer.Information<string>((long)this.GetHashCode(), "MonadGhostUI.WriteDebugLine({0})", message);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00032D14 File Offset: 0x00030F14
		public override void WriteLine(string value)
		{
			ExTraceGlobals.HostTracer.Information<string>((long)this.GetHashCode(), "MonadGhostUI.WriteLine({0})", value);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00032D2D File Offset: 0x00030F2D
		public override void WriteVerboseLine(string message)
		{
			this.Connection.InteractionHandler.ReportVerboseOutput(message);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00032D40 File Offset: 0x00030F40
		public override void WriteErrorLine(string message)
		{
		}

		// Token: 0x04000393 RID: 915
		private const string name = "Exchange Management Console";

		// Token: 0x04000394 RID: 916
		private static readonly Version version = Assembly.GetExecutingAssembly().GetName().Version;

		// Token: 0x04000395 RID: 917
		private readonly Guid instanceID = Guid.NewGuid();

		// Token: 0x04000396 RID: 918
		private CultureInfo currentCulture;

		// Token: 0x04000397 RID: 919
		private CultureInfo currentUICulture;

		// Token: 0x04000398 RID: 920
		private MonadConnection connection;
	}
}
