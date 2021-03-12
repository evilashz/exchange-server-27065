using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001E6 RID: 486
	[RequiredScript(typeof(ExtensionWizardProperties))]
	[RequiredScript(typeof(BaseForm))]
	[RequiredScript(typeof(ExtensionWizardForm))]
	[RequiredScript(typeof(UploadPackageStep))]
	[ClientScriptResource("InstallExtensionFromURLSlab", "Microsoft.Exchange.Management.ControlPanel.Client.Extension.js")]
	public class InstallExtensionFromURLSlab : SlabControl, IScriptControl
	{
		// Token: 0x060025CC RID: 9676 RVA: 0x0007439E File Offset: 0x0007259E
		public InstallExtensionFromURLSlab()
		{
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x17001BB6 RID: 7094
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x000743AC File Offset: 0x000725AC
		// (set) Token: 0x060025CE RID: 9678 RVA: 0x000743B4 File Offset: 0x000725B4
		public string ServiceUrl { get; set; }

		// Token: 0x17001BB7 RID: 7095
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x000743BD File Offset: 0x000725BD
		// (set) Token: 0x060025D0 RID: 9680 RVA: 0x000743C5 File Offset: 0x000725C5
		public string InstallMarketplaceAssetID { get; set; }

		// Token: 0x17001BB8 RID: 7096
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x000743CE File Offset: 0x000725CE
		// (set) Token: 0x060025D2 RID: 9682 RVA: 0x000743D6 File Offset: 0x000725D6
		public string MarketplaceQueryMarket { get; set; }

		// Token: 0x17001BB9 RID: 7097
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x000743DF File Offset: 0x000725DF
		// (set) Token: 0x060025D4 RID: 9684 RVA: 0x000743E7 File Offset: 0x000725E7
		public string Scope { get; set; }

		// Token: 0x17001BBA RID: 7098
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x000743F0 File Offset: 0x000725F0
		// (set) Token: 0x060025D6 RID: 9686 RVA: 0x000743F8 File Offset: 0x000725F8
		public string Etoken { get; set; }

		// Token: 0x17001BBB RID: 7099
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x00074401 File Offset: 0x00072601
		// (set) Token: 0x060025D8 RID: 9688 RVA: 0x00074409 File Offset: 0x00072609
		public string DeploymentId { get; set; }

		// Token: 0x17001BBC RID: 7100
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x00074412 File Offset: 0x00072612
		public string MarketplaceServicesUrl
		{
			get
			{
				return ExtensionUtility.MarketplaceServicesUrl;
			}
		}

		// Token: 0x17001BBD RID: 7101
		// (get) Token: 0x060025DA RID: 9690 RVA: 0x00074419 File Offset: 0x00072619
		public string MarketplacePageBaseUrl
		{
			get
			{
				return ExtensionUtility.MarketplaceLandingPageUrl;
			}
		}

		// Token: 0x17001BBE RID: 7102
		// (get) Token: 0x060025DB RID: 9691 RVA: 0x00074420 File Offset: 0x00072620
		public string Clid
		{
			get
			{
				return ExtensionUtility.Clid;
			}
		}

		// Token: 0x17001BBF RID: 7103
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x00074427 File Offset: 0x00072627
		public string FullVersion
		{
			get
			{
				return ExtensionUtility.ClientFullVersion;
			}
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x00074430 File Offset: 0x00072630
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.InstallMarketplaceAssetID = this.Context.Request.QueryString["AssetID"];
			this.MarketplaceQueryMarket = this.Context.Request.QueryString["LC"];
			this.Scope = this.Context.Request.QueryString["Scope"];
			this.DeploymentId = this.Context.Request.QueryString["DeployId"];
			this.Etoken = this.GetClientTokenParameter(this.Context.Request.RawUrl);
			if (!string.IsNullOrWhiteSpace(this.InstallMarketplaceAssetID) && !string.IsNullOrWhiteSpace(this.MarketplaceQueryMarket))
			{
				return;
			}
			EcpEventLogConstants.Tuple_MissingRequiredParameterDetected.LogPeriodicEvent(EcpEventLogExtensions.GetPeriodicKeyPerUser(), new object[]
			{
				EcpEventLogExtensions.GetUserNameToLog(),
				this.Context.GetRequestUrlForLog(),
				(this.InstallMarketplaceAssetID != null) ? this.InstallMarketplaceAssetID : string.Empty,
				(this.MarketplaceQueryMarket != null) ? this.MarketplaceQueryMarket : string.Empty
			});
			ErrorHandlingUtil.TransferToErrorPage("badofficecallback");
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x00074562 File Offset: 0x00072762
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptManager.GetCurrent(this.Page).RegisterScriptControl<InstallExtensionFromURLSlab>(this);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0007457C File Offset: 0x0007277C
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			base.Render(writer);
			ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000745AC File Offset: 0x000727AC
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

		// Token: 0x060025E1 RID: 9697 RVA: 0x000745F9 File Offset: 0x000727F9
		public IEnumerable<ScriptReference> GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x00074608 File Offset: 0x00072808
		private void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddProperty("ServiceUrl", this.ServiceUrl);
			descriptor.AddProperty("MarketplaceQueryMarket", this.MarketplaceQueryMarket);
			descriptor.AddProperty("InstallMarketplaceAssetID", this.InstallMarketplaceAssetID);
			descriptor.AddProperty("DeploymentID", this.DeploymentId);
			descriptor.AddProperty("MarketplaceServicesUrl", this.MarketplaceServicesUrl);
			descriptor.AddProperty("MarketplacePageBaseUrl", this.MarketplacePageBaseUrl);
			descriptor.AddProperty("MarketplaceClid", this.Clid);
			descriptor.AddProperty("FullVersion", this.FullVersion);
			descriptor.AddProperty("Scope", this.Scope);
			descriptor.AddProperty("InstallEtoken", this.Etoken);
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000746C0 File Offset: 0x000728C0
		private string GetClientTokenParameter(string url)
		{
			string result = string.Empty;
			int num = url.IndexOf("ClientToken=", StringComparison.OrdinalIgnoreCase);
			if (num > 0 && (url[num - 1] == '&' || url[num - 1] == '?'))
			{
				int num2 = num + "ClientToken=".Length;
				int num3 = url.IndexOf('&', num2);
				int length = (num3 > 0) ? (num3 - num2) : (url.Length - num2);
				result = url.Substring(num2, length);
			}
			return result;
		}
	}
}
