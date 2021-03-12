using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200002E RID: 46
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class EcpContentPage : EcpPage, IScriptControl, IThemable
	{
		// Token: 0x060018EE RID: 6382 RVA: 0x0004E477 File Offset: 0x0004C677
		public EcpContentPage()
		{
			this.ShowHelp = true;
		}

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x0004E486 File Offset: 0x0004C686
		// (set) Token: 0x060018F0 RID: 6384 RVA: 0x0004E48E File Offset: 0x0004C68E
		[DefaultValue(true)]
		public bool ShowHelp { get; protected set; }

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0004E497 File Offset: 0x0004C697
		// (set) Token: 0x060018F2 RID: 6386 RVA: 0x0004E49F File Offset: 0x0004C69F
		[DefaultValue(false)]
		public bool SkipXssFilter { get; set; }

		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0004E4A8 File Offset: 0x0004C6A8
		// (set) Token: 0x060018F4 RID: 6388 RVA: 0x0004E4B0 File Offset: 0x0004C6B0
		public string IncludeCssFiles { get; set; }

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0004E4B9 File Offset: 0x0004C6B9
		// (set) Token: 0x060018F6 RID: 6390 RVA: 0x0004E4C1 File Offset: 0x0004C6C1
		[DefaultValue(false)]
		public bool SkipBEParamCheck { get; set; }

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0004E4CA File Offset: 0x0004C6CA
		protected ScriptManager ScriptManager
		{
			get
			{
				if (this.scriptManager == null)
				{
					this.scriptManager = ScriptManager.GetCurrent(this);
				}
				return this.scriptManager;
			}
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0004E4E8 File Offset: 0x0004C6E8
		public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			ClientScriptResourceAttribute clientScriptResourceAttribute = (ClientScriptResourceAttribute)TypeDescriptor.GetAttributes(this)[typeof(ClientScriptResourceAttribute)];
			if (!clientScriptResourceAttribute.ComponentType.IsNullOrBlank())
			{
				ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor(clientScriptResourceAttribute.ComponentType, "aspnetForm");
				this.BuildScriptDescriptor(scriptControlDescriptor);
				return new ScriptDescriptor[]
				{
					scriptControlDescriptor
				};
			}
			return null;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0004E543 File Offset: 0x0004C743
		public virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0004E550 File Offset: 0x0004C750
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!this.SkipBEParamCheck)
			{
				this.Context.ThrowIfViewOptionsWithBEParam(base.FeatureSet);
			}
			base.ClientScript.RegisterStartupScript(typeof(EcpContentPage), "HideLoadingGif", "ContentPageUtil.HideLoadingGif();", true);
			if (this.Page.Master != null)
			{
				MasterPage master = this.Page.Master;
				while (master.Master != null)
				{
					master = master.Master;
				}
				CommonMaster commonMaster = master as CommonMaster;
				if (commonMaster != null)
				{
					commonMaster.FeatureSet = base.FeatureSet;
				}
			}
			if (!string.IsNullOrEmpty(this.IncludeCssFiles))
			{
				((CommonMaster)this.Page.Master).AddCssFiles(this.IncludeCssFiles);
			}
			if (base.IsPostBack)
			{
				this.Context.CheckCanaryForPostBack("ecpCanary");
			}
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0004E620 File Offset: 0x0004C820
		protected override void OnPreInit(EventArgs e)
		{
			bool showHelp;
			if (bool.TryParse(base.Request.QueryString["showhelp"], out showHelp))
			{
				this.ShowHelp = showHelp;
			}
			base.OnPreInit(e);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0004E65C File Offset: 0x0004C85C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.ScriptManager.RegisterScriptControl<EcpContentPage>(this);
			HttpCookie canaryCookie = HttpContext.Current.GetCanaryCookie();
			string hiddenFieldInitialValue = (canaryCookie == null) ? null : canaryCookie.Value;
			ScriptManager.RegisterHiddenField(this, "ecpCanary", hiddenFieldInitialValue);
			base.ClientScript.RegisterOnSubmitStatement(typeof(EcpContentPage), "SuppressSubmit", "return false;\r\n");
			if (this.SkipXssFilter)
			{
				base.Response.Headers.Add("X-XSS-Protection", "0");
			}
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0004E6E1 File Offset: 0x0004C8E1
		protected override void Render(HtmlTextWriter writer)
		{
			if (!base.DesignMode && !this.ScriptManager.IsInAsyncPostBack)
			{
				this.ScriptManager.RegisterScriptDescriptors(this);
			}
			base.Render(writer);
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0004E70B File Offset: 0x0004C90B
		protected virtual void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0004E70D File Offset: 0x0004C90D
		protected override void SavePageStateToPersistenceMedium(object state)
		{
			if (this.EnableViewState)
			{
				base.SavePageStateToPersistenceMedium(state);
			}
		}

		// Token: 0x04001A8B RID: 6795
		public const string FormClientID = "aspnetForm";

		// Token: 0x04001A8C RID: 6796
		private const string EcpCanaryId = "ecpCanary";

		// Token: 0x04001A8D RID: 6797
		private ScriptManager scriptManager;
	}
}
