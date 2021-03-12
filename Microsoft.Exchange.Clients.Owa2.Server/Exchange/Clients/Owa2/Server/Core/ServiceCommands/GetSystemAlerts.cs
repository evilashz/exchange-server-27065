using System;
using System.Configuration;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Online.BOX.Shell;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x0200031E RID: 798
	internal class GetSystemAlerts : ServiceCommand<Alert[]>
	{
		// Token: 0x06001A93 RID: 6803 RVA: 0x000634B8 File Offset: 0x000616B8
		public GetSystemAlerts(CallContext callContext) : base(callContext)
		{
			OwsLogRegistry.Register("GetSystemAlerts", typeof(GetSystemAlerts.GetSystemAlertsMetadata), new Type[0]);
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x000634DC File Offset: 0x000616DC
		protected override Alert[] InternalExecute()
		{
			string text = HttpContext.Current.Request.Headers["RPSOrgIdPUID"];
			string userPuid = string.IsNullOrEmpty(text) ? HttpContext.Current.Request.Headers["RPSPUID"] : text;
			string principalName = ((LiveIDIdentity)Thread.CurrentPrincipal.Identity).PrincipalName;
			string shellServiceUrl = string.Empty;
			string trackingGuid = Guid.NewGuid().ToString();
			Alert[] alerts;
			try
			{
				using (ShellServiceClient shellServiceClient = new ShellServiceClient("MsOnlineShellService_EndPointConfiguration"))
				{
					string certificateThumbprint = ConfigurationManager.AppSettings["MsOnlineShellService_CertThumbprint"];
					shellServiceClient.ClientCredentials.ClientCertificate.Certificate = TlsCertificateInfo.FindCertByThumbprint(certificateThumbprint);
					shellServiceUrl = shellServiceClient.Endpoint.Address.Uri.AbsoluteUri;
					GetAlertRequest getAlertRequest = new GetAlertRequest
					{
						WorkloadId = WorkloadAuthenticationId.Exchange,
						UserPuid = userPuid,
						UserPrincipalName = principalName,
						TrackingGuid = trackingGuid,
						CultureName = Thread.CurrentThread.CurrentUICulture.Name
					};
					alerts = shellServiceClient.GetAlerts(getAlertRequest);
				}
			}
			catch (Exception)
			{
				this.LogExceptionFromO365ShellService(principalName, userPuid, shellServiceUrl, trackingGuid);
				throw;
			}
			return alerts;
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x000636A0 File Offset: 0x000618A0
		private void LogExceptionFromO365ShellService(string userPrincipalName, string userPuid, string shellServiceUrl, string trackingGuid)
		{
			SimulatedWebRequestContext.ExecuteWithoutUserContext("GetSystemAlerts", delegate(RequestDetailsLogger logger)
			{
				logger.ActivityScope.SetProperty(GetSystemAlerts.GetSystemAlertsMetadata.UserPrincipalName, userPrincipalName);
				logger.ActivityScope.SetProperty(GetSystemAlerts.GetSystemAlertsMetadata.UserPuid, userPuid);
				logger.ActivityScope.SetProperty(GetSystemAlerts.GetSystemAlertsMetadata.ShellServiceUrl, shellServiceUrl);
				logger.ActivityScope.SetProperty(GetSystemAlerts.GetSystemAlertsMetadata.TrackingGuid, trackingGuid);
			});
		}

		// Token: 0x04000EB6 RID: 3766
		private const string EventId = "GetSystemAlerts";

		// Token: 0x0200031F RID: 799
		internal enum GetSystemAlertsMetadata
		{
			// Token: 0x04000EB8 RID: 3768
			[DisplayName("GetSystemAlerts")]
			GetSystemAlertsId,
			// Token: 0x04000EB9 RID: 3769
			[DisplayName("ShellServiceUrl")]
			ShellServiceUrl,
			// Token: 0x04000EBA RID: 3770
			[DisplayName("UserPuid")]
			UserPuid,
			// Token: 0x04000EBB RID: 3771
			[DisplayName("UserPrincipalName")]
			UserPrincipalName,
			// Token: 0x04000EBC RID: 3772
			[DisplayName("TrackingGuid")]
			TrackingGuid
		}
	}
}
