using System;
using System.Collections.Generic;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000573 RID: 1395
	[ClientScriptResource("CollectionViewer", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	[ControlValueProperty("Value")]
	public class CollectionViewer : ScriptControlBase, INamingContainer, IScriptControl
	{
		// Token: 0x06004103 RID: 16643 RVA: 0x000C6297 File Offset: 0x000C4497
		public CollectionViewer() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x1700253B RID: 9531
		// (get) Token: 0x06004104 RID: 16644 RVA: 0x000C62AC File Offset: 0x000C44AC
		// (set) Token: 0x06004105 RID: 16645 RVA: 0x000C62B4 File Offset: 0x000C44B4
		public bool WrapItems { get; set; }

		// Token: 0x1700253C RID: 9532
		// (get) Token: 0x06004106 RID: 16646 RVA: 0x000C62BD File Offset: 0x000C44BD
		// (set) Token: 0x06004107 RID: 16647 RVA: 0x000C62C5 File Offset: 0x000C44C5
		public string DisplayProperty
		{
			get
			{
				return this.displayProperty;
			}
			set
			{
				this.displayProperty = value;
			}
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x000C62D0 File Offset: 0x000C44D0
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			return new ScriptControlDescriptor[]
			{
				this.GetScriptDescriptor()
			};
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x000C62EE File Offset: 0x000C44EE
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x000C62FC File Offset: 0x000C44FC
		private ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("CollectionViewer", this.ClientID);
			if (string.IsNullOrEmpty(this.DisplayProperty))
			{
				scriptControlDescriptor.AddScriptProperty("DisplayProperty", "function($_){ return $_; }");
			}
			else
			{
				scriptControlDescriptor.AddScriptProperty("DisplayProperty", "function($_){ return $_." + this.DisplayProperty + "; }");
			}
			scriptControlDescriptor.AddProperty("WrapItems", this.WrapItems);
			return scriptControlDescriptor;
		}

		// Token: 0x04002B23 RID: 11043
		private string displayProperty = string.Empty;
	}
}
