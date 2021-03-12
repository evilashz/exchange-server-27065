using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004AF RID: 1199
	[ClientScriptResource("UMCallDataRecord", "Microsoft.Exchange.Management.ControlPanel.Client.UnifiedMessaging.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class UMCallDataRecord : SlabControl, IScriptControl
	{
		// Token: 0x06003B59 RID: 15193 RVA: 0x000B34A4 File Offset: 0x000B16A4
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			BindingCollection filterParameters = this.listViewDataSource.FilterParameters;
			if (this.pickerUMMailboxToSearch != null)
			{
				filterParameters.Add(new ComponentBinding(this.pickerUMMailboxToSearch, "value")
				{
					Name = "Mailbox"
				});
			}
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x000B34F0 File Offset: 0x000B16F0
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Command command = this.listView.Commands.FindCommandByName("Refresh");
			command.Visible = false;
			ScriptManager current = ScriptManager.GetCurrent(this.Page);
			current.RegisterScriptControl<UMCallDataRecord>(this);
			base.EnsureID();
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x000B353A File Offset: 0x000B173A
		protected override void Render(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			base.Render(writer);
			writer.RenderEndTag();
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x000B3574 File Offset: 0x000B1774
		protected void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
			foreach (object obj in base.Attributes.Keys)
			{
				string text = (string)obj;
				writer.AddAttribute(text, base.Attributes[text]);
			}
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x000B3604 File Offset: 0x000B1804
		public virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x000B3614 File Offset: 0x000B1814
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

		// Token: 0x06003B5F RID: 15199 RVA: 0x000B3664 File Offset: 0x000B1864
		private void BuildScriptDescriptor(ScriptControlDescriptor descriptor)
		{
			descriptor.AddComponentProperty("ListView", this.listView.ClientID, true);
			descriptor.AddComponentProperty("ListViewDataSource", this.listViewDataSource.ClientID, true);
			descriptor.AddComponentProperty("ListViewRefreshMethod", this.listViewDataSource.RefreshWebServiceMethod.ClientID, true);
			if (this.pickerUMMailboxToSearch != null)
			{
				descriptor.AddComponentProperty("UMMailboxPicker", this.pickerUMMailboxToSearch.ClientID, true);
			}
		}

		// Token: 0x04002766 RID: 10086
		protected EcpSingleSelect pickerUMMailboxToSearch;

		// Token: 0x04002767 RID: 10087
		protected WebServiceListSource listViewDataSource;

		// Token: 0x04002768 RID: 10088
		protected ListView listView;
	}
}
