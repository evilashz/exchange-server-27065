using System;
using System.ComponentModel;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000635 RID: 1589
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("PublicFoldersListView", "Microsoft.Exchange.Management.ControlPanel.Client.OrgSettings.js")]
	public class PublicFoldersListView : ListView
	{
		// Token: 0x170026F2 RID: 9970
		// (get) Token: 0x060045E9 RID: 17897 RVA: 0x000D3768 File Offset: 0x000D1968
		// (set) Token: 0x060045EA RID: 17898 RVA: 0x000D3770 File Offset: 0x000D1970
		[DefaultValue(false)]
		public virtual bool ShowAll { get; set; }

		// Token: 0x060045EB RID: 17899 RVA: 0x000D377C File Offset: 0x000D197C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ComponentBinding componentBinding = new ComponentBinding(this, "showAll");
			componentBinding.Name = "ShowAll";
			base.FilterParameters.Add(componentBinding);
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x000D37B3 File Offset: 0x000D19B3
		public PublicFoldersListView()
		{
			base.SearchTextBox.SearchButtonImageId = new CommandSprite.SpriteId?(CommandSprite.SpriteId.Start);
		}
	}
}
