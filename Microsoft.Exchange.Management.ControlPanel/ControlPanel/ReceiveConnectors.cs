using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000055 RID: 85
	public class ReceiveConnectors : SlabControl
	{
		// Token: 0x06001A01 RID: 6657 RVA: 0x0005368C File Offset: 0x0005188C
		protected override void OnLoad(EventArgs e)
		{
			WebServiceReference webServiceReference = new WebServiceReference("~/DDI/DDIService.svc?schema=ReceiveConnectors&workflow=GetServerDropDown");
			try
			{
				PowerShellResults<JsonDictionary<object>> list = webServiceReference.GetList(null, null);
				if (list.Output != null && list.Output.Length > 0)
				{
					Microsoft.Exchange.Management.ControlPanel.WebControls.ListView listView = (Microsoft.Exchange.Management.ControlPanel.WebControls.ListView)this.FindControl("ReceiveConnectorsListView");
					listView.Views = new List<ListItem>();
					JsonDictionary<object>[] output = list.Output;
					for (int i = 0; i < output.Length; i++)
					{
						Dictionary<string, object> dictionary = output[i];
						listView.Views.Add(new ListItem((string)dictionary["Fqdn"], (string)dictionary["ServerData"]));
					}
				}
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			base.OnLoad(e);
		}
	}
}
