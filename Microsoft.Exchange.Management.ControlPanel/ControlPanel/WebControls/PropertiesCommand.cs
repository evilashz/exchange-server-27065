using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000638 RID: 1592
	public class PropertiesCommand : WizardCommand
	{
		// Token: 0x06004601 RID: 17921 RVA: 0x000D396C File Offset: 0x000D1B6C
		public PropertiesCommand() : base(null, CommandSprite.SpriteId.ToolBarProperties)
		{
			this.SelectionMode = SelectionMode.RequiresSingleSelection;
			this.DefaultCommand = true;
			this.Name = "Edit";
			this.SingleInstance = true;
			base.ImageAltText = Strings.PropertiesCommandText;
		}

		// Token: 0x170026FC RID: 9980
		// (get) Token: 0x06004602 RID: 17922 RVA: 0x000D39A7 File Offset: 0x000D1BA7
		// (set) Token: 0x06004603 RID: 17923 RVA: 0x000D39AF File Offset: 0x000D1BAF
		[DefaultValue(true)]
		public override bool DefaultCommand
		{
			get
			{
				return base.DefaultCommand;
			}
			set
			{
				base.DefaultCommand = value;
			}
		}
	}
}
