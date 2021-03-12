using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000298 RID: 664
	public class DeliveryReportPage : BaseForm
	{
		// Token: 0x06002B39 RID: 11065 RVA: 0x00086AAC File Offset: 0x00084CAC
		protected override void OnLoad(EventArgs e)
		{
			this.wrapperPanel.PreRender += this.WrapperPanel_PreRender;
			string text = base.Request.QueryString["HelpId"];
			if (!string.IsNullOrEmpty(text))
			{
				base.HelpId = text;
			}
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x00086AF8 File Offset: 0x00084CF8
		private void WrapperPanel_PreRender(object sender, EventArgs e)
		{
			if (this.deliveryReportProperties.ObjectIdentity != null)
			{
				this.SetupFilterBindings();
				string strA = base.Request.QueryString["isowa"];
				string text = this.GetDeliveryReportUrl();
				if (string.Compare(strA, "n", true) == 0 || !RbacPrincipal.Current.RbacConfiguration.ExecutingUserIsAllowedOWA)
				{
					NavigateCommand navigateCommand = this.deliveryReportDetailsPane.Commands[0] as NavigateCommand;
					navigateCommand.NavigateUrl = "mailto:?subject={0}&body={1}";
					text += "?isowa=n";
				}
				else
				{
					this.recipientSummary.IsOWA = true;
				}
				this.recipientSummary.DeliveryReportUrl = text;
				this.recipientSummary.DeliveryStatusDataSourceRefreshMethod = this.deliveryStatusDataSource.RefreshWebServiceMethod;
				return;
			}
			this.wrapperPanel.Visible = false;
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x00086BC8 File Offset: 0x00084DC8
		private void SetupFilterBindings()
		{
			StaticBinding staticBinding = new StaticBinding();
			staticBinding.Name = "Identity";
			RecipientMessageTrackingReportId recipientMessageTrackingReportId = RecipientMessageTrackingReportId.Parse(this.deliveryReportProperties.ObjectIdentity);
			staticBinding.Value = recipientMessageTrackingReportId.MessageTrackingReportId;
			if (this.wrapperPanel == null)
			{
				this.wrapperPanel = (DockPanel)this.deliveryReportProperties.FindControl("wrapperPanel");
			}
			this.deliveryStatusDataSource = (WebServiceListSource)this.wrapperPanel.Controls[0].FindControl("deliveryStatusDataSource");
			this.deliveryStatusDataSource.FilterParameters.Add(staticBinding);
			ComponentBinding componentBinding = new ComponentBinding(this.recipientSummary, "Status");
			componentBinding.Name = "RecipientStatus";
			this.deliveryStatusDataSource.FilterParameters.Add(componentBinding);
			if (!string.IsNullOrEmpty(recipientMessageTrackingReportId.Recipient))
			{
				StaticBinding staticBinding2 = new StaticBinding();
				staticBinding2.Name = "Recipients";
				staticBinding2.Value = recipientMessageTrackingReportId.Recipient;
				this.deliveryStatusDataSource.FilterParameters.Add(staticBinding2);
				return;
			}
			ComponentBinding componentBinding2 = new ComponentBinding(this.toAddress, "value");
			componentBinding2.Name = "Recipients";
			this.deliveryStatusDataSource.FilterParameters.Add(componentBinding2);
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x00086CF8 File Offset: 0x00084EF8
		private string GetDeliveryReportUrl()
		{
			string text = (string)base.Cache["DeliveryReportUrl"];
			if (text != null)
			{
				return text;
			}
			lock (this.cacheLock)
			{
				text = (string)base.Cache["DeliveryReportUrl"];
				if (text == null)
				{
					Uri ecpexternalUrl = this.GetECPExternalUrl();
					if (ecpexternalUrl != null)
					{
						text = new Uri(ecpexternalUrl, this.Context.GetRequestUrlAbsolutePath()).ToString();
					}
					else
					{
						text = EcpUrl.ResolveClientUrl(this.Context.GetRequestUrl().GetLeftPart(UriPartial.Path));
					}
					if (text != null)
					{
						base.Cache["DeliveryReportUrl"] = text;
					}
				}
			}
			return text;
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x00086DBC File Offset: 0x00084FBC
		private Uri GetECPExternalUrl()
		{
			Uri result = null;
			RbacPrincipal rbacPrincipal = RbacPrincipal.Current;
			if (rbacPrincipal.IsInRole("Mailbox"))
			{
				ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromDirectoryObjectId((IRecipientSession)((RecipientObjectResolver)RecipientObjectResolver.Instance).CreateAdSession(), rbacPrincipal.ExecutingUserId, RemotingOptions.LocalConnectionsOnly);
				try
				{
					result = FrontEndLocator.GetFrontEndEcpUrl(exchangePrincipal);
				}
				catch (ServerNotFoundException)
				{
				}
			}
			return result;
		}

		// Token: 0x04002182 RID: 8578
		private const string CacheKey = "DeliveryReportUrl";

		// Token: 0x04002183 RID: 8579
		protected DeliveryReportProperties deliveryReportProperties;

		// Token: 0x04002184 RID: 8580
		protected WebServiceListSource deliveryStatusDataSource;

		// Token: 0x04002185 RID: 8581
		protected DockPanel wrapperPanel;

		// Token: 0x04002186 RID: 8582
		protected DeliveryReportSummary recipientSummary;

		// Token: 0x04002187 RID: 8583
		protected RecipientPickerControl toAddress;

		// Token: 0x04002188 RID: 8584
		protected DetailsPane deliveryReportDetailsPane;

		// Token: 0x04002189 RID: 8585
		private object cacheLock = new object();
	}
}
