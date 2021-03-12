using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Publish
{
	// Token: 0x020009D4 RID: 2516
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "PublishSoap", Namespace = "http://microsoft.com/DRM/PublishingService")]
	internal class PublishWS : WsAsyncProxyWrapper
	{
		// Token: 0x060036E4 RID: 14052 RVA: 0x0008C150 File Offset: 0x0008A350
		public PublishWS()
		{
			base.Url = "https://localhost/_wmcs/licensing/publish.asmx";
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x060036E5 RID: 14053 RVA: 0x0008C163 File Offset: 0x0008A363
		// (set) Token: 0x060036E6 RID: 14054 RVA: 0x0008C16B File Offset: 0x0008A36B
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

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x060036E7 RID: 14055 RVA: 0x0008C174 File Offset: 0x0008A374
		// (remove) Token: 0x060036E8 RID: 14056 RVA: 0x0008C1AC File Offset: 0x0008A3AC
		public event AcquireIssuanceLicenseCompletedEventHandler AcquireIssuanceLicenseCompleted;

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x060036E9 RID: 14057 RVA: 0x0008C1E4 File Offset: 0x0008A3E4
		// (remove) Token: 0x060036EA RID: 14058 RVA: 0x0008C21C File Offset: 0x0008A41C
		public event GetClientLicensorCertCompletedEventHandler GetClientLicensorCertCompleted;

		// Token: 0x060036EB RID: 14059 RVA: 0x0008C254 File Offset: 0x0008A454
		[SoapHeader("VersionDataValue", Direction = SoapHeaderDirection.InOut)]
		[SoapDocumentMethod("http://microsoft.com/DRM/PublishingService/AcquireIssuanceLicense", RequestNamespace = "http://microsoft.com/DRM/PublishingService", ResponseNamespace = "http://microsoft.com/DRM/PublishingService", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public AcquireIssuanceLicenseResponse[] AcquireIssuanceLicense(AcquireIssuanceLicenseParams[] RequestParams)
		{
			object[] array = base.Invoke("AcquireIssuanceLicense", new object[]
			{
				RequestParams
			});
			return (AcquireIssuanceLicenseResponse[])array[0];
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x0008C284 File Offset: 0x0008A484
		public IAsyncResult BeginAcquireIssuanceLicense(AcquireIssuanceLicenseParams[] RequestParams, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AcquireIssuanceLicense", new object[]
			{
				RequestParams
			}, callback, asyncState);
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x0008C2AC File Offset: 0x0008A4AC
		public AcquireIssuanceLicenseResponse[] EndAcquireIssuanceLicense(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AcquireIssuanceLicenseResponse[])array[0];
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x0008C2C9 File Offset: 0x0008A4C9
		public void AcquireIssuanceLicenseAsync(AcquireIssuanceLicenseParams[] RequestParams)
		{
			this.AcquireIssuanceLicenseAsync(RequestParams, null);
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x0008C2D4 File Offset: 0x0008A4D4
		public void AcquireIssuanceLicenseAsync(AcquireIssuanceLicenseParams[] RequestParams, object userState)
		{
			if (this.AcquireIssuanceLicenseOperationCompleted == null)
			{
				this.AcquireIssuanceLicenseOperationCompleted = new SendOrPostCallback(this.OnAcquireIssuanceLicenseOperationCompleted);
			}
			base.InvokeAsync("AcquireIssuanceLicense", new object[]
			{
				RequestParams
			}, this.AcquireIssuanceLicenseOperationCompleted, userState);
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x0008C31C File Offset: 0x0008A51C
		private void OnAcquireIssuanceLicenseOperationCompleted(object arg)
		{
			if (this.AcquireIssuanceLicenseCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AcquireIssuanceLicenseCompleted(this, new AcquireIssuanceLicenseCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x0008C364 File Offset: 0x0008A564
		[SoapHeader("VersionDataValue", Direction = SoapHeaderDirection.InOut)]
		[SoapDocumentMethod("http://microsoft.com/DRM/PublishingService/GetClientLicensorCert", RequestNamespace = "http://microsoft.com/DRM/PublishingService", ResponseNamespace = "http://microsoft.com/DRM/PublishingService", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public GetClientLicensorCertResponse[] GetClientLicensorCert(GetClientLicensorCertParams[] RequestParams)
		{
			object[] array = base.Invoke("GetClientLicensorCert", new object[]
			{
				RequestParams
			});
			return (GetClientLicensorCertResponse[])array[0];
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x0008C394 File Offset: 0x0008A594
		public IAsyncResult BeginGetClientLicensorCert(GetClientLicensorCertParams[] RequestParams, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetClientLicensorCert", new object[]
			{
				RequestParams
			}, callback, asyncState);
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x0008C3BC File Offset: 0x0008A5BC
		public GetClientLicensorCertResponse[] EndGetClientLicensorCert(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetClientLicensorCertResponse[])array[0];
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x0008C3D9 File Offset: 0x0008A5D9
		public void GetClientLicensorCertAsync(GetClientLicensorCertParams[] RequestParams)
		{
			this.GetClientLicensorCertAsync(RequestParams, null);
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x0008C3E4 File Offset: 0x0008A5E4
		public void GetClientLicensorCertAsync(GetClientLicensorCertParams[] RequestParams, object userState)
		{
			if (this.GetClientLicensorCertOperationCompleted == null)
			{
				this.GetClientLicensorCertOperationCompleted = new SendOrPostCallback(this.OnGetClientLicensorCertOperationCompleted);
			}
			base.InvokeAsync("GetClientLicensorCert", new object[]
			{
				RequestParams
			}, this.GetClientLicensorCertOperationCompleted, userState);
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x0008C42C File Offset: 0x0008A62C
		private void OnGetClientLicensorCertOperationCompleted(object arg)
		{
			if (this.GetClientLicensorCertCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetClientLicensorCertCompleted(this, new GetClientLicensorCertCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x0008C471 File Offset: 0x0008A671
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x04002EDE RID: 11998
		private VersionData versionDataValueField;

		// Token: 0x04002EDF RID: 11999
		private SendOrPostCallback AcquireIssuanceLicenseOperationCompleted;

		// Token: 0x04002EE0 RID: 12000
		private SendOrPostCallback GetClientLicensorCertOperationCompleted;
	}
}
