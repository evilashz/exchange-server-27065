using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200027E RID: 638
	[ClientScriptResource("ListSource", "Microsoft.Exchange.Management.ControlPanel.Client.List.js")]
	public class ListSource : DataBoundControl, IScriptControl
	{
		// Token: 0x17001CC6 RID: 7366
		// (get) Token: 0x060029D5 RID: 10709 RVA: 0x00083758 File Offset: 0x00081958
		// (set) Token: 0x060029D6 RID: 10710 RVA: 0x00083760 File Offset: 0x00081960
		public string[] DataFields { get; set; }

		// Token: 0x17001CC7 RID: 7367
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x00083769 File Offset: 0x00081969
		// (set) Token: 0x060029D8 RID: 10712 RVA: 0x00083771 File Offset: 0x00081971
		public string JsonData { get; protected set; }

		// Token: 0x060029D9 RID: 10713 RVA: 0x0008377A File Offset: 0x0008197A
		public ListSource()
		{
			base.RequiresDataBinding = true;
			this.JsonData = string.Empty;
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x00083794 File Offset: 0x00081994
		protected override void PerformDataBinding(IEnumerable data)
		{
			base.PerformDataBinding(data);
			ArrayList arrayList = new ArrayList();
			PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties(data);
			foreach (object obj in data)
			{
				if (this.DataFields != null && this.DataFields.Length == 1 && this.DataFields[0] == "ToString()")
				{
					arrayList.Add(obj.ToString());
				}
				else
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					foreach (object obj2 in listItemProperties)
					{
						PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
						if (this.DataFields == null || this.DataFields.Length <= 0 || this.DataFields.Contains(propertyDescriptor.Name))
						{
							object value = propertyDescriptor.GetValue(obj);
							if (value != null)
							{
								if (ListSource.knownTypes.Contains(value.GetType()))
								{
									dictionary[propertyDescriptor.Name] = value;
								}
								else
								{
									dictionary[propertyDescriptor.Name] = value.ToString();
								}
							}
							else
							{
								dictionary[propertyDescriptor.Name] = null;
							}
						}
					}
					arrayList.Add(dictionary);
				}
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			this.JsonData = javaScriptSerializer.Serialize(arrayList);
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x00083938 File Offset: 0x00081B38
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
			writer.AddAttribute(HtmlTextWriterAttribute.Value, this.JsonData);
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x0008396A File Offset: 0x00081B6A
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this.Page.IsPostBack)
			{
				this.JsonData = this.Context.Request[this.UniqueID];
			}
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x0008399C File Offset: 0x00081B9C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (base.RequiresDataBinding && string.IsNullOrEmpty(this.DataSourceID) && this.DataSource != null)
			{
				this.DataBind();
			}
			ScriptManager.GetCurrent(this.Page).RegisterScriptControl<ListSource>(this);
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000839D9 File Offset: 0x00081BD9
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x17001CC8 RID: 7368
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x000839FB File Offset: 0x00081BFB
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Input;
			}
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000839FF File Offset: 0x00081BFF
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			return this.GetScriptDescriptors();
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x00083A08 File Offset: 0x00081C08
		protected virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor(base.GetType().Name, this.ClientID);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x00083A38 File Offset: 0x00081C38
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x040020F6 RID: 8438
		private static Type[] knownTypes = new Type[]
		{
			typeof(bool),
			typeof(char),
			typeof(int),
			typeof(float),
			typeof(string),
			typeof(Guid),
			typeof(DateTime),
			typeof(Identity)
		};
	}
}
