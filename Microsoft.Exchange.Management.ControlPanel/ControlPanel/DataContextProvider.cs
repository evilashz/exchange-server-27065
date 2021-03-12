using System;
using System.Collections.Generic;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000262 RID: 610
	public class DataContextProvider : ScriptControlBase
	{
		// Token: 0x0600290E RID: 10510 RVA: 0x000811D8 File Offset: 0x0007F3D8
		protected override void OnPreRender(EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.ViewModel))
			{
				if (this.ServiceUrl != null)
				{
					this["ServiceUrl"] = EcpUrl.ProcessUrl(this.ServiceUrl.ServiceUrl);
				}
				AttributeCollection bindingContextAttributes = this.GetBindingContextAttributes();
				bindingContextAttributes.Add("data-type", this.ViewModel);
				foreach (KeyValuePair<string, object> keyValuePair in this.dataContextProperties)
				{
					object value = keyValuePair.Value;
					string value2;
					if (value is string)
					{
						value2 = (string)value;
					}
					else if (value is bool)
					{
						value2 = value.ToString().ToLower();
					}
					else if (value is int || value is uint || value is long || value is ulong || value is float || value is double)
					{
						value2 = value.ToString();
					}
					else
					{
						value2 = "json:" + keyValuePair.Value.ToJsonString(DDIService.KnownTypes.Value);
					}
					bindingContextAttributes.Add("vm-" + keyValuePair.Key, value2);
				}
				if (bindingContextAttributes != base.Attributes)
				{
					List<string> list = new List<string>(base.Attributes.Count);
					foreach (object obj in base.Attributes.Keys)
					{
						string text = obj as string;
						if (text != null && text.StartsWith("vm-"))
						{
							bindingContextAttributes.Add(text, base.Attributes[text]);
							list.Add(text);
						}
					}
					foreach (string key in list)
					{
						base.Attributes.Remove(key);
					}
				}
			}
			base.OnPreRender(e);
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x00081408 File Offset: 0x0007F608
		private AttributeCollection GetBindingContextAttributes()
		{
			if (this.Page is BaseForm)
			{
				return this.Page.Form.Attributes;
			}
			return base.Attributes;
		}

		// Token: 0x17001C8C RID: 7308
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x0008142E File Offset: 0x0007F62E
		// (set) Token: 0x06002911 RID: 10513 RVA: 0x00081436 File Offset: 0x0007F636
		public WebServiceReference ServiceUrl { get; set; }

		// Token: 0x17001C8D RID: 7309
		// (get) Token: 0x06002912 RID: 10514 RVA: 0x0008143F File Offset: 0x0007F63F
		// (set) Token: 0x06002913 RID: 10515 RVA: 0x00081447 File Offset: 0x0007F647
		public string ViewModel { get; set; }

		// Token: 0x17001C8E RID: 7310
		protected object this[string propertyName]
		{
			get
			{
				object result = null;
				this.dataContextProperties.TryGetValue(propertyName, out result);
				return result;
			}
			set
			{
				this.dataContextProperties[propertyName] = value;
			}
		}

		// Token: 0x040020AF RID: 8367
		private Dictionary<string, object> dataContextProperties = new Dictionary<string, object>();
	}
}
