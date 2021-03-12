using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000583 RID: 1411
	[ClientScriptResource("WebServiceListSource", "Microsoft.Exchange.Management.ControlPanel.Client.List.js")]
	public class WebServiceListSource : ListSource, INamingContainer
	{
		// Token: 0x1700255C RID: 9564
		// (get) Token: 0x0600416C RID: 16748 RVA: 0x000C7837 File Offset: 0x000C5A37
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public BindingCollection FilterParameters
		{
			get
			{
				return this.filterParameters;
			}
		}

		// Token: 0x1700255D RID: 9565
		// (get) Token: 0x0600416D RID: 16749 RVA: 0x000C783F File Offset: 0x000C5A3F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public BindingCollection SortParameters
		{
			get
			{
				return this.sortParameters;
			}
		}

		// Token: 0x1700255E RID: 9566
		// (get) Token: 0x0600416E RID: 16750 RVA: 0x000C7847 File Offset: 0x000C5A47
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public BindingCollection DDIParameters
		{
			get
			{
				return this.ddiParameters;
			}
		}

		// Token: 0x1700255F RID: 9567
		// (get) Token: 0x0600416F RID: 16751 RVA: 0x000C784F File Offset: 0x000C5A4F
		// (set) Token: 0x06004170 RID: 16752 RVA: 0x000C7857 File Offset: 0x000C5A57
		public WebServiceMethod RefreshWebServiceMethod { get; private set; }

		// Token: 0x17002560 RID: 9568
		// (get) Token: 0x06004171 RID: 16753 RVA: 0x000C7860 File Offset: 0x000C5A60
		// (set) Token: 0x06004172 RID: 16754 RVA: 0x000C7868 File Offset: 0x000C5A68
		[UrlProperty("*.svc")]
		[DefaultValue(null)]
		public WebServiceReference ServiceUrl { get; set; }

		// Token: 0x17002561 RID: 9569
		// (get) Token: 0x06004173 RID: 16755 RVA: 0x000C7871 File Offset: 0x000C5A71
		// (set) Token: 0x06004174 RID: 16756 RVA: 0x000C7879 File Offset: 0x000C5A79
		[DefaultValue(false)]
		public bool SupportAsyncGetList { get; set; }

		// Token: 0x17002562 RID: 9570
		// (get) Token: 0x06004175 RID: 16757 RVA: 0x000C7882 File Offset: 0x000C5A82
		// (set) Token: 0x06004176 RID: 16758 RVA: 0x000C788A File Offset: 0x000C5A8A
		[DefaultValue(false)]
		public bool ClientSort { get; set; }

		// Token: 0x17002563 RID: 9571
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x000C7893 File Offset: 0x000C5A93
		// (set) Token: 0x06004178 RID: 16760 RVA: 0x000C789B File Offset: 0x000C5A9B
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] RefreshAfter { get; set; }

		// Token: 0x17002564 RID: 9572
		// (get) Token: 0x06004179 RID: 16761 RVA: 0x000C78A4 File Offset: 0x000C5AA4
		// (set) Token: 0x0600417A RID: 16762 RVA: 0x000C78AC File Offset: 0x000C5AAC
		[DefaultValue(null)]
		public string RefreshCookieName { get; set; }

		// Token: 0x0600417B RID: 16763 RVA: 0x000C78B8 File Offset: 0x000C5AB8
		public WebServiceListSource()
		{
			this.RefreshWebServiceMethod = new WebServiceMethod();
			this.RefreshWebServiceMethod.ID = "Refresh";
			this.Controls.Add(this.RefreshWebServiceMethod);
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x000C7918 File Offset: 0x000C5B18
		protected override void OnPreRender(EventArgs e)
		{
			if (this.ServiceUrl != null)
			{
				this.RefreshWebServiceMethod.Method = "GetList";
				this.RefreshWebServiceMethod.ServiceUrl = this.ServiceUrl;
				this.RefreshWebServiceMethod.ParameterNames = WebServiceParameterNames.GetList;
				this.UpdateParameters();
			}
			base.OnPreRender(e);
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x000C7968 File Offset: 0x000C5B68
		public void UpdateParameters()
		{
			if (this.RefreshWebServiceMethod != null)
			{
				this.RefreshWebServiceMethod.Parameters.Clear();
				DataContractBinding dataContractBinding = new DataContractBinding();
				foreach (Binding binding in this.FilterParameters)
				{
					dataContractBinding.Bindings.Add(binding.Name, binding);
				}
				if (DDIService.UseDDIService(this.ServiceUrl))
				{
					foreach (Binding binding2 in this.DDIParameters)
					{
						dataContractBinding.Bindings.Add(binding2.Name, binding2);
					}
				}
				this.RefreshWebServiceMethod.Parameters.Add(dataContractBinding);
				DataContractBinding dataContractBinding2 = new DataContractBinding();
				foreach (Binding binding3 in this.SortParameters)
				{
					dataContractBinding2.Bindings.Add(binding3.Name, binding3);
				}
				this.RefreshWebServiceMethod.Parameters.Add(dataContractBinding2);
			}
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x000C7AB8 File Offset: 0x000C5CB8
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
			ScriptControlDescriptor scriptControlDescriptor = (ScriptControlDescriptor)scriptDescriptors.First<ScriptDescriptor>();
			scriptControlDescriptor.AddProperty("ServiceUrl", EcpUrl.ProcessUrl(this.ServiceUrl.ServiceUrl));
			scriptControlDescriptor.AddProperty("SupportAsyncGetList", this.SupportAsyncGetList, true);
			scriptControlDescriptor.AddProperty("ClientSort", this.ClientSort, true);
			if (this.RefreshCookieName != null)
			{
				scriptControlDescriptor.AddProperty("RefreshCookieName", this.RefreshCookieName);
			}
			if (!this.RefreshAfter.IsNullOrEmpty())
			{
				scriptControlDescriptor.AddProperty("RefreshAfter", this.RefreshAfter);
			}
			scriptControlDescriptor.AddComponentProperty("RefreshWebServiceMethod", this.RefreshWebServiceMethod.ClientID);
			return scriptDescriptors;
		}

		// Token: 0x04002B4E RID: 11086
		private BindingCollection filterParameters = new BindingCollection();

		// Token: 0x04002B4F RID: 11087
		private BindingCollection sortParameters = new BindingCollection();

		// Token: 0x04002B50 RID: 11088
		private BindingCollection ddiParameters = new BindingCollection();
	}
}
