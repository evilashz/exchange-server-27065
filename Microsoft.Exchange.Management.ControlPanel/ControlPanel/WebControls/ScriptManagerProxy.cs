using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200064D RID: 1613
	[ToolboxData("<{0}:ScriptManagerProxy runat=\"server\" />")]
	public class ScriptManagerProxy : ScriptManagerProxy
	{
		// Token: 0x0600466E RID: 18030 RVA: 0x000D52A7 File Offset: 0x000D34A7
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (ToolkitScriptManager.CacheScriptBuckets == null)
			{
				throw new InvalidOperationException("ToolkitScriptManager must be put in this page at first.");
			}
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x000D52C4 File Offset: 0x000D34C4
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (base.Scripts.Count > 0)
			{
				string fullName = typeof(ScriptManagerProxy).Assembly.FullName;
				foreach (ScriptReference scriptReference in base.Scripts)
				{
					if (string.IsNullOrEmpty(scriptReference.Assembly))
					{
						scriptReference.Assembly = fullName;
					}
				}
				ToolkitScriptManager.ExpandAndSort(base.Scripts);
			}
		}
	}
}
