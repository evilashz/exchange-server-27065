using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Oab.Probes
{
	// Token: 0x0200023D RID: 573
	public abstract class OabBaseLocalProbe : ProbeWorkItem
	{
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0006B52D File Offset: 0x0006972D
		// (set) Token: 0x06000FFE RID: 4094 RVA: 0x0006B535 File Offset: 0x00069735
		public HttpWebRequestUtility WebRequestUtil { get; set; }

		// Token: 0x06000FFF RID: 4095 RVA: 0x0006B544 File Offset: 0x00069744
		protected HttpWebRequest GetRequest()
		{
			this.WebRequestUtil = new HttpWebRequestUtility(base.TraceContext);
			HttpWebRequest httpWebRequest = this.WebRequestUtil.CreateBasicHttpWebRequest(base.Definition.Endpoint, false);
			httpWebRequest.ContentType = "text/xml";
			httpWebRequest.Method = "GET";
			httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
			bool flag = false;
			if (base.Definition.Attributes.ContainsKey("TrustAnySslCertificate") && bool.TryParse(base.Definition.Attributes["TrustAnySslCertificate"], out flag) && flag)
			{
				string componentId = "OAB_AM_Probe";
				RemoteCertificateValidationCallback callback = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
				CertificateValidationManager.SetComponentId(httpWebRequest, componentId);
				CertificateValidationManager.RegisterCallback(componentId, callback);
			}
			return httpWebRequest;
		}

		// Token: 0x04000C0B RID: 3083
		public const string TrustAnySslCertificateParameterName = "TrustAnySslCertificate";
	}
}
