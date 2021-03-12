using System;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000665 RID: 1637
	public class StringArrayModalEditor : SimpleEntryEditor<InlineEditor>
	{
		// Token: 0x06004735 RID: 18229 RVA: 0x000D80A4 File Offset: 0x000D62A4
		public StringArrayModalEditor()
		{
			base.EditControl.InputWaterMarkText = Strings.InlineEditorInputWaterMarkText;
			base.EditControl.CssClass = "inlineEditorFormlet";
		}
	}
}
