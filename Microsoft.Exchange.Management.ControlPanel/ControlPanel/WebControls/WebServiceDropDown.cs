using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200067F RID: 1663
	[ClientScriptResource("WebServiceDropDown", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[TargetControlType(typeof(DropDownList))]
	public class WebServiceDropDown : DropDownList, IScriptControl, INamingContainer
	{
		// Token: 0x17002795 RID: 10133
		// (get) Token: 0x060047EE RID: 18414 RVA: 0x000DADFB File Offset: 0x000D8FFB
		// (set) Token: 0x060047EF RID: 18415 RVA: 0x000DAE03 File Offset: 0x000D9003
		public WebServiceReference ServiceUrl { get; set; }

		// Token: 0x17002796 RID: 10134
		// (get) Token: 0x060047F0 RID: 18416 RVA: 0x000DAE0C File Offset: 0x000D900C
		// (set) Token: 0x060047F1 RID: 18417 RVA: 0x000DAE14 File Offset: 0x000D9014
		[DefaultValue("GetList")]
		public string WebServiceMethodName { get; set; }

		// Token: 0x17002797 RID: 10135
		// (get) Token: 0x060047F2 RID: 18418 RVA: 0x000DAE1D File Offset: 0x000D901D
		// (set) Token: 0x060047F3 RID: 18419 RVA: 0x000DAE25 File Offset: 0x000D9025
		[DefaultValue(null)]
		public string SortProperty { get; set; }

		// Token: 0x17002798 RID: 10136
		// (get) Token: 0x060047F4 RID: 18420 RVA: 0x000DAE2E File Offset: 0x000D902E
		// (set) Token: 0x060047F5 RID: 18421 RVA: 0x000DAE36 File Offset: 0x000D9036
		[DefaultValue(SortDirection.Ascending)]
		public SortDirection SortDirection { get; set; }

		// Token: 0x17002799 RID: 10137
		// (get) Token: 0x060047F6 RID: 18422 RVA: 0x000DAE3F File Offset: 0x000D903F
		// (set) Token: 0x060047F7 RID: 18423 RVA: 0x000DAE47 File Offset: 0x000D9047
		public string InitialValue { get; set; }

		// Token: 0x1700279A RID: 10138
		// (get) Token: 0x060047F8 RID: 18424 RVA: 0x000DAE50 File Offset: 0x000D9050
		// (set) Token: 0x060047F9 RID: 18425 RVA: 0x000DAE58 File Offset: 0x000D9058
		public WebServiceMethod RefreshWebServiceMethod { get; private set; }

		// Token: 0x1700279B RID: 10139
		// (get) Token: 0x060047FA RID: 18426 RVA: 0x000DAE61 File Offset: 0x000D9061
		// (set) Token: 0x060047FB RID: 18427 RVA: 0x000DAE69 File Offset: 0x000D9069
		public string RefreshProperties { get; set; }

		// Token: 0x060047FC RID: 18428 RVA: 0x000DAE72 File Offset: 0x000D9072
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x060047FD RID: 18429 RVA: 0x000DAE7C File Offset: 0x000D907C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			string text = base.Attributes["SetRoles"];
			this.hasWebServicePermission = ((string.IsNullOrEmpty(text) || LoginUtil.IsInRoles(this.Context.User, text.Split(new char[]
			{
				','
			}))) && null != this.ServiceUrl);
			if (this.hasWebServicePermission && !string.IsNullOrEmpty(this.RefreshProperties))
			{
				this.RefreshWebServiceMethod = new WebServiceMethod();
				this.RefreshWebServiceMethod.ID = "WebServiceDropDownRefresh";
				this.Controls.Add(this.RefreshWebServiceMethod);
			}
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x000DAF23 File Offset: 0x000D9123
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute("role", "combobox");
			base.AddAttributesToRender(writer);
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x000DAF3C File Offset: 0x000D913C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.hasWebServicePermission)
			{
				if (this.RefreshWebServiceMethod == null)
				{
					SortOptions sortOptions = null;
					MethodInfo method = this.ServiceUrl.ServiceType.GetMethod(this.WebServiceMethodName ?? "GetList");
					if (!string.IsNullOrEmpty(this.SortProperty))
					{
						sortOptions = new SortOptions();
						sortOptions.PropertyName = this.SortProperty;
						sortOptions.Direction = this.SortDirection;
					}
					try
					{
						this.resutls = (PowerShellResults)method.Invoke(this.ServiceUrl.ServiceInstance, new object[]
						{
							null,
							sortOptions
						});
						goto IL_D6;
					}
					catch (TargetInvocationException ex)
					{
						throw ex.InnerException;
					}
				}
				this.RefreshWebServiceMethod.Method = (this.WebServiceMethodName ?? "GetList");
				this.RefreshWebServiceMethod.ServiceUrl = this.ServiceUrl;
				this.RefreshWebServiceMethod.ParameterNames = WebServiceParameterNames.GetList;
				this.UpdateParameters();
			}
			IL_D6:
			ScriptManager.GetCurrent(this.Page).RegisterScriptControl<WebServiceDropDown>(this);
		}

		// Token: 0x06004800 RID: 18432 RVA: 0x000DB040 File Offset: 0x000D9240
		protected override void Render(HtmlTextWriter writer)
		{
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
			base.Render(writer);
			if (this.RefreshWebServiceMethod != null)
			{
				this.RefreshWebServiceMethod.RenderControl(writer);
			}
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x000DB078 File Offset: 0x000D9278
		public void UpdateParameters()
		{
			if (this.RefreshWebServiceMethod != null)
			{
				this.RefreshWebServiceMethod.Parameters.Clear();
				DataContractBinding item = new DataContractBinding();
				this.RefreshWebServiceMethod.Parameters.Add(item);
				DataContractBinding dataContractBinding = new DataContractBinding();
				if (!string.IsNullOrEmpty(this.SortProperty))
				{
					Binding binding = new StaticBinding
					{
						Name = "PropertyName",
						Value = this.SortProperty
					};
					dataContractBinding.Bindings.Add(binding.Name, binding);
					Binding binding2 = new StaticBinding
					{
						Name = "Direction",
						Value = this.SortDirection
					};
					dataContractBinding.Bindings.Add(binding2.Name, binding2);
				}
				this.RefreshWebServiceMethod.Parameters.Add(dataContractBinding);
			}
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x000DB14C File Offset: 0x000D934C
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("WebServiceDropDown", this.ClientID);
			if (this.resutls != null)
			{
				scriptControlDescriptor.AddScriptProperty("Results", this.resutls.ToJsonString(null));
			}
			if (!string.IsNullOrEmpty(this.InitialValue))
			{
				scriptControlDescriptor.AddProperty("InitialValue", this.InitialValue);
			}
			if (this.RefreshWebServiceMethod != null)
			{
				scriptControlDescriptor.AddProperty("RefreshProperties", this.RefreshProperties);
				scriptControlDescriptor.AddComponentProperty("RefreshWebServiceMethod", this.RefreshWebServiceMethod);
			}
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x000DB1DD File Offset: 0x000D93DD
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(typeof(EllipsisLabel));
		}

		// Token: 0x04003045 RID: 12357
		private PowerShellResults resutls;

		// Token: 0x04003046 RID: 12358
		private bool hasWebServicePermission;
	}
}
