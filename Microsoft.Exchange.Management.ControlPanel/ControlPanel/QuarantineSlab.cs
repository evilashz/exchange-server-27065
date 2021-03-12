using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200036F RID: 879
	[ClientScriptResource("QuarantineSlab", "Microsoft.Exchange.Management.ControlPanel.Client.Quarantine.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class QuarantineSlab : SlabControl, IScriptControl
	{
		// Token: 0x06003016 RID: 12310 RVA: 0x00092894 File Offset: 0x00090A94
		public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			ClientScriptResourceAttribute clientScriptResourceAttribute = (ClientScriptResourceAttribute)TypeDescriptor.GetAttributes(this)[typeof(ClientScriptResourceAttribute)];
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor(clientScriptResourceAttribute.ComponentType, this.ClientID);
			this.BuildScriptDescriptor(scriptControlDescriptor);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000928E1 File Offset: 0x00090AE1
		public virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000928EE File Offset: 0x00090AEE
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptManager.GetCurrent(this.Page).RegisterScriptControl<QuarantineSlab>(this);
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x00092908 File Offset: 0x00090B08
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			base.Render(writer);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x00092940 File Offset: 0x00090B40
		private void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddComponentProperty("ListView", this.quarantineListView.ClientID, true);
		}

		// Token: 0x0400234D RID: 9037
		protected ListView quarantineListView;
	}
}
