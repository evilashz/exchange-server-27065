using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.ServerCertification
{
	// Token: 0x020009DE RID: 2526
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[WebServiceBinding(Name = "ServerCertificationWebServiceSoap", Namespace = "http://microsoft.com/DRM/CertificationService")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	internal class ServerCertificationWS : WsAsyncProxyWrapper
	{
		// Token: 0x06003717 RID: 14103 RVA: 0x0008C569 File Offset: 0x0008A769
		public ServerCertificationWS()
		{
			base.Url = "https://localhost/_wmcs/certification/ServerCertification.asmx";
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06003718 RID: 14104 RVA: 0x0008C57C File Offset: 0x0008A77C
		// (set) Token: 0x06003719 RID: 14105 RVA: 0x0008C584 File Offset: 0x0008A784
		public VersionData VersionDataValue
		{
			get
			{
				return this.versionDataValueField;
			}
			set
			{
				this.versionDataValueField = value;
			}
		}

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x0600371A RID: 14106 RVA: 0x0008C590 File Offset: 0x0008A790
		// (remove) Token: 0x0600371B RID: 14107 RVA: 0x0008C5C8 File Offset: 0x0008A7C8
		public event CertifyCompletedEventHandler CertifyCompleted;

		// Token: 0x0600371C RID: 14108 RVA: 0x0008C600 File Offset: 0x0008A800
		[SoapHeader("VersionDataValue", Direction = SoapHeaderDirection.InOut)]
		[SoapDocumentMethod("http://microsoft.com/DRM/CertificationService/Certify", RequestNamespace = "http://microsoft.com/DRM/CertificationService", ResponseNamespace = "http://microsoft.com/DRM/CertificationService", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public CertifyResponse Certify(CertifyParams requestParams)
		{
			object[] array = base.Invoke("Certify", new object[]
			{
				requestParams
			});
			return (CertifyResponse)array[0];
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x0008C630 File Offset: 0x0008A830
		public IAsyncResult BeginCertify(CertifyParams requestParams, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Certify", new object[]
			{
				requestParams
			}, callback, asyncState);
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x0008C658 File Offset: 0x0008A858
		public CertifyResponse EndCertify(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CertifyResponse)array[0];
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x0008C675 File Offset: 0x0008A875
		public void CertifyAsync(CertifyParams requestParams)
		{
			this.CertifyAsync(requestParams, null);
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x0008C680 File Offset: 0x0008A880
		public void CertifyAsync(CertifyParams requestParams, object userState)
		{
			if (this.CertifyOperationCompleted == null)
			{
				this.CertifyOperationCompleted = new SendOrPostCallback(this.OnCertifyOperationCompleted);
			}
			base.InvokeAsync("Certify", new object[]
			{
				requestParams
			}, this.CertifyOperationCompleted, userState);
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x0008C6C8 File Offset: 0x0008A8C8
		private void OnCertifyOperationCompleted(object arg)
		{
			if (this.CertifyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CertifyCompleted(this, new CertifyCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x0008C70D File Offset: 0x0008A90D
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x04002EEC RID: 12012
		private VersionData versionDataValueField;

		// Token: 0x04002EED RID: 12013
		private SendOrPostCallback CertifyOperationCompleted;
	}
}
