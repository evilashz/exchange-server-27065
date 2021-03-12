using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000040 RID: 64
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncDiagnostics : IDiagnosable
	{
		// Token: 0x06000342 RID: 834 RVA: 0x000164B8 File Offset: 0x000146B8
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "SyncManager";
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000164C0 File Offset: 0x000146C0
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement("SyncManager");
			if (string.IsNullOrEmpty(parameters.Argument) || string.Equals(parameters.Argument, "help", StringComparison.OrdinalIgnoreCase))
			{
				xelement.Add(new XElement("help", "Supported argument(s): basic, verbose, info, help, sub-components:databasemanager, dispatcher, hubpicker. Specifying a specific component  will only return that information in basic or verbose mode as specified. E.g. basic databasemanager dispatcher.  That will only return basic information for the databasemanager and dispatcher.  It will not return anything for the hubpicker"));
			}
			else
			{
				SyncDiagnosticMode diagnosticMode = this.GetDiagnosticMode(parameters.Argument);
				bool flag = 0 <= parameters.Argument.IndexOf("databasemanager", StringComparison.OrdinalIgnoreCase);
				bool flag2 = 0 <= parameters.Argument.IndexOf("dispatchmanager", StringComparison.OrdinalIgnoreCase);
				bool flag3 = 0 <= parameters.Argument.IndexOf("hubpicker", StringComparison.OrdinalIgnoreCase);
				bool flag4 = flag || flag2 || flag3;
				if (!flag4 || flag)
				{
					xelement.Add(DataAccessLayer.GetDatabaseHandlerDiagnosticInfo(diagnosticMode));
				}
				if (!flag4 || flag2)
				{
					xelement.Add(DataAccessLayer.GetDispatchManagerDiagnosticInfo(diagnosticMode));
				}
				if (!flag4 || flag3)
				{
					xelement.Add(StatefulHubPicker.Instance.GetDiagnosticInfo());
				}
			}
			return xelement;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000165BF File Offset: 0x000147BF
		internal void Register()
		{
			ProcessAccessManager.RegisterComponent(this);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000165C7 File Offset: 0x000147C7
		internal void Unregister()
		{
			ProcessAccessManager.UnregisterComponent(this);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000165CF File Offset: 0x000147CF
		private SyncDiagnosticMode GetDiagnosticMode(string argument)
		{
			if (argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1)
			{
				return SyncDiagnosticMode.Verbose;
			}
			if (argument.IndexOf("info", StringComparison.OrdinalIgnoreCase) != -1)
			{
				return SyncDiagnosticMode.Info;
			}
			return SyncDiagnosticMode.Basic;
		}

		// Token: 0x040001D5 RID: 469
		private const string ProcessAccessManagerComponentName = "SyncManager";
	}
}
