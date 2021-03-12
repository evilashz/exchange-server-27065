using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200003B RID: 59
	public class Certificates : SlabControl
	{
		// Token: 0x06001969 RID: 6505 RVA: 0x00050034 File Offset: 0x0004E234
		protected override void OnLoad(EventArgs e)
		{
			WebServiceReference webServiceReference = new WebServiceReference("~/DDI/DDIService.svc?schema=CertificateServices&workflow=GetServerDropDown");
			try
			{
				PowerShellResults<JsonDictionary<object>> list = webServiceReference.GetList(null, null);
				if (list.Output != null && list.Output.Length > 0)
				{
					Microsoft.Exchange.Management.ControlPanel.WebControls.ListView listView = (Microsoft.Exchange.Management.ControlPanel.WebControls.ListView)this.FindControl("CertificateListView");
					listView.Views = new List<ListItem>();
					JsonDictionary<object>[] output = list.Output;
					for (int i = 0; i < output.Length; i++)
					{
						Dictionary<string, object> dictionary = output[i];
						listView.Views.Add(new ListItem((string)dictionary["Fqdn"], (string)dictionary["Fqdn"]));
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
