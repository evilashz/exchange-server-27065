using System;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementConsole;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.Edge.SystemManager
{
	// Token: 0x0200000A RID: 10
	[NodeType("2D9FF279-CFC5-46ce-8815-683C0B972E94", Description = "Queue Viewer node")]
	public sealed class QueueViewerNode : ExchangeScopeNode
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00004154 File Offset: 0x00002354
		public QueueViewerNode()
		{
			base.DisplayName = Strings.QueueViewer;
			base.Icon = Icons.QueueViewerTool;
			base.ViewDescriptions.Add(ExchangeFormView.CreateViewDescription(typeof(QueueViewerResultPane)));
			base.EnabledStandardVerbs |= 64;
			base.HelpTopic = HelpId.QueueViewerNode.ToString();
			if (WinformsHelper.IsRemoteEnabled())
			{
				base.RegisterConnectionToPSServerAction();
				return;
			}
			Globals.InitializeMultiPerfCounterInstance("EMC");
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000041D8 File Offset: 0x000023D8
		public override void InitializeView(Control control, IProgress status)
		{
			Binding binding = new Binding("CaptionText", this, "DisplayName");
			binding.ControlUpdateMode = ControlUpdateMode.Never;
			binding.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
			((QueueViewerResultPane)control).DataBindings.Add(binding);
			QueueViewerResultPane queueViewerResultPane = (QueueViewerResultPane)control;
			queueViewerResultPane.SetDatasourcesOnView();
			base.InitializeView(control, status);
		}

		// Token: 0x04000015 RID: 21
		public const string NodeGuid = "2D9FF279-CFC5-46ce-8815-683C0B972E94";

		// Token: 0x04000016 RID: 22
		public const string NodeDescription = "Queue Viewer node";
	}
}
