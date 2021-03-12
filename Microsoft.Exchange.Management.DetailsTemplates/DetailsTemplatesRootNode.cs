using System;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementConsole;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000019 RID: 25
	[NodeType("3B7B0725-B474-48cd-B59F-B5C2CA2EA8C2", Description = "Details Templates root node")]
	public sealed class DetailsTemplatesRootNode : ExchangeScopeNode
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000054D4 File Offset: 0x000036D4
		public DetailsTemplatesRootNode()
		{
			base.DisplayName = Strings.RootNodeDisplayName;
			base.Icon = Icons.DetailsTemplate;
			base.ViewDescriptions.Add(ExchangeFormView.CreateViewDescription(typeof(DetailsTemplatesResultPane)));
			base.HelpTopic = HelpId.DetailsTemplateRootNode.ToString();
			if (WinformsHelper.IsRemoteEnabled())
			{
				base.RegisterConnectionToPSServerAction();
			}
		}

		// Token: 0x04000047 RID: 71
		public const string NodeGuid = "3B7B0725-B474-48cd-B59F-B5C2CA2EA8C2";

		// Token: 0x04000048 RID: 72
		public const string NodeDescription = "Details Templates root node";
	}
}
