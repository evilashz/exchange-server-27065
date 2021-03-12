using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ReportingWebService.PowerShell
{
	// Token: 0x0200000D RID: 13
	internal class ExchangeCommandResolver : DisposeTrackableBase, IPSCommandResolver
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002978 File Offset: 0x00000B78
		public ExchangeCommandResolver() : this(ExchangeCommandResolver.powerShellSnapin)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002990 File Offset: 0x00000B90
		public ExchangeCommandResolver(IEnumerable<string> snapIns)
		{
			RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
			PSSnapInException ex = null;
			Exception ex2 = null;
			IEnumerable<string> enumerable = (from s in snapIns
			select s.ToLower()).Distinct<string>();
			foreach (string name in enumerable)
			{
				try
				{
					runspaceConfiguration.AddPSSnapIn(name, out ex);
					ex2 = ex;
				}
				catch (PSArgumentException ex3)
				{
					ex2 = ex3;
				}
				if (ex2 != null)
				{
					ReportingWebServiceEventLogConstants.Tuple_LoadReportingschemaFailed.LogEvent(new object[]
					{
						ex2.Message
					});
					ServiceDiagnostics.ThrowError(ReportingErrorCode.ErrorSchemaInitializationFail, Strings.ErrorSchemaInitializationFail, ex2);
				}
			}
			this.runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration);
			if (this.runspace.RunspaceStateInfo.State != RunspaceState.Opened)
			{
				this.runspace.Open();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A90 File Offset: 0x00000C90
		public ReadOnlyCollection<PSTypeName> GetOutputType(string commandName)
		{
			base.CheckDisposed();
			Collection<PSObject> collection = this.runspace.CreatePipeline(string.Format("{0} {1}", "get-command", commandName)).Invoke();
			return ((CommandInfo)collection[0].BaseObject).OutputType;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002ADA File Offset: 0x00000CDA
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.runspace != null)
			{
				this.runspace.Dispose();
				this.runspace = null;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002AF9 File Offset: 0x00000CF9
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ExchangeCommandResolver>(this);
		}

		// Token: 0x04000031 RID: 49
		private static List<string> powerShellSnapin = new List<string>
		{
			"Microsoft.Exchange.Management.PowerShell.E2010"
		};

		// Token: 0x04000032 RID: 50
		private Runspace runspace;
	}
}
