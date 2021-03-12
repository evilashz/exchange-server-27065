using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Psws.Probes
{
	// Token: 0x02000515 RID: 1301
	public class PswsBackEndProbe : PswsProbeBase
	{
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x000C3AFA File Offset: 0x000C1CFA
		protected override string ComponentId
		{
			get
			{
				return "PswsBackEndProbe";
			}
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x000C3B01 File Offset: 0x000C1D01
		public PswsBackEndProbe()
		{
			this.tokenInitialized = false;
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x000C3B14 File Offset: 0x000C1D14
		protected override void DoInitialize()
		{
			base.DoInitialize();
			if (!this.tokenInitialized)
			{
				CertificateValidationManager.RegisterCallback(this.ComponentId, (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true);
				this.CaculateCommonAccessToken();
				this.tokenInitialized = true;
			}
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x000C3B64 File Offset: 0x000C1D64
		protected override HttpWebRequest GetRequest()
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Entering GetRequest in PswsBackEndProbe", null, "GetRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsBackEndProbe.cs", 98);
			HttpWebRequest request = base.GetRequest();
			request.ContentType = "application/soap+xml;charset=UTF-8";
			request.KeepAlive = true;
			request.ServicePoint.Expect100Continue = false;
			request.UnsafeAuthenticatedConnectionSharing = true;
			CertificateValidationManager.SetComponentId(request, this.ComponentId);
			request.Headers.Add("X-CommonAccessToken", this.token.Serialize());
			if (this.tokenType == AccessTokenType.LiveIdBasic)
			{
				request.Headers.Add("X-WLID-MemberName", this.token.ExtensionData["MemberName"]);
			}
			request.Credentials = CredentialCache.DefaultNetworkCredentials.GetCredential(request.RequestUri, "Kerberos");
			WTFDiagnostics.TraceFunction(ExTraceGlobals.PswsTracer, base.TraceContext, "Leaving GetRequest in PswsBackEndProbe", null, "GetRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsBackEndProbe.cs", 121);
			return request;
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x000C3C54 File Offset: 0x000C1E54
		private void CaculateCommonAccessToken()
		{
			if (!Enum.TryParse<AccessTokenType>(base.Definition.Attributes["AccessTokenType"], out this.tokenType))
			{
				throw new ApplicationException(this.probeInfo + "Create PswsBackEndProbe without correct 'AccessTokenType'!");
			}
			AccessTokenType accessTokenType = this.tokenType;
			if (accessTokenType == AccessTokenType.LiveIdBasic)
			{
				this.token = CommonAccessTokenHelper.CreateLiveIdBasic(base.Definition.Account);
				return;
			}
			if (accessTokenType != AccessTokenType.CertificateSid)
			{
				throw new ApplicationException(this.probeInfo + "Unhandled AccessTokenType for PswsBackEndProbe : " + this.tokenType.ToString());
			}
			this.token = CommonAccessTokenHelper.CreateCertificateSid(base.Definition.Account);
		}

		// Token: 0x0400177E RID: 6014
		public const string AccessTokenTypeParameterName = "AccessTokenType";

		// Token: 0x0400177F RID: 6015
		private AccessTokenType tokenType;

		// Token: 0x04001780 RID: 6016
		private CommonAccessToken token;

		// Token: 0x04001781 RID: 6017
		private bool tokenInitialized;
	}
}
