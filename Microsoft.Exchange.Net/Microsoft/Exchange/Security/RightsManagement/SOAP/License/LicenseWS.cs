using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.License
{
	// Token: 0x020009CA RID: 2506
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "LicenseSoap", Namespace = "http://microsoft.com/DRM/LicensingService")]
	internal class LicenseWS : WsAsyncProxyWrapper
	{
		// Token: 0x060036A3 RID: 13987 RVA: 0x0008BCBD File Offset: 0x00089EBD
		public LicenseWS()
		{
			base.Url = "https://localhost/_wmcs/licensing/license.asmx";
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x060036A4 RID: 13988 RVA: 0x0008BCD0 File Offset: 0x00089ED0
		// (set) Token: 0x060036A5 RID: 13989 RVA: 0x0008BCD8 File Offset: 0x00089ED8
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

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x060036A6 RID: 13990 RVA: 0x0008BCE4 File Offset: 0x00089EE4
		// (remove) Token: 0x060036A7 RID: 13991 RVA: 0x0008BD1C File Offset: 0x00089F1C
		public event AcquireLicenseCompletedEventHandler AcquireLicenseCompleted;

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x060036A8 RID: 13992 RVA: 0x0008BD54 File Offset: 0x00089F54
		// (remove) Token: 0x060036A9 RID: 13993 RVA: 0x0008BD8C File Offset: 0x00089F8C
		public event AcquirePreLicenseCompletedEventHandler AcquirePreLicenseCompleted;

		// Token: 0x060036AA RID: 13994 RVA: 0x0008BDC4 File Offset: 0x00089FC4
		[SoapDocumentMethod("http://microsoft.com/DRM/LicensingService/AcquireLicense", RequestNamespace = "http://microsoft.com/DRM/LicensingService", ResponseNamespace = "http://microsoft.com/DRM/LicensingService", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("VersionDataValue", Direction = SoapHeaderDirection.InOut)]
		public AcquireLicenseResponse[] AcquireLicense(AcquireLicenseParams[] RequestParams)
		{
			object[] array = base.Invoke("AcquireLicense", new object[]
			{
				RequestParams
			});
			return (AcquireLicenseResponse[])array[0];
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x0008BDF4 File Offset: 0x00089FF4
		public IAsyncResult BeginAcquireLicense(AcquireLicenseParams[] RequestParams, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AcquireLicense", new object[]
			{
				RequestParams
			}, callback, asyncState);
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x0008BE1C File Offset: 0x0008A01C
		public AcquireLicenseResponse[] EndAcquireLicense(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AcquireLicenseResponse[])array[0];
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x0008BE39 File Offset: 0x0008A039
		public void AcquireLicenseAsync(AcquireLicenseParams[] RequestParams)
		{
			this.AcquireLicenseAsync(RequestParams, null);
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x0008BE44 File Offset: 0x0008A044
		public void AcquireLicenseAsync(AcquireLicenseParams[] RequestParams, object userState)
		{
			if (this.AcquireLicenseOperationCompleted == null)
			{
				this.AcquireLicenseOperationCompleted = new SendOrPostCallback(this.OnAcquireLicenseOperationCompleted);
			}
			base.InvokeAsync("AcquireLicense", new object[]
			{
				RequestParams
			}, this.AcquireLicenseOperationCompleted, userState);
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x0008BE8C File Offset: 0x0008A08C
		private void OnAcquireLicenseOperationCompleted(object arg)
		{
			if (this.AcquireLicenseCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AcquireLicenseCompleted(this, new AcquireLicenseCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x0008BED4 File Offset: 0x0008A0D4
		[SoapDocumentMethod("http://microsoft.com/DRM/LicensingService/AcquirePreLicense", RequestNamespace = "http://microsoft.com/DRM/LicensingService", ResponseNamespace = "http://microsoft.com/DRM/LicensingService", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("VersionDataValue", Direction = SoapHeaderDirection.InOut)]
		public AcquirePreLicenseResponse[] AcquirePreLicense(AcquirePreLicenseParams[] RequestParams)
		{
			object[] array = base.Invoke("AcquirePreLicense", new object[]
			{
				RequestParams
			});
			return (AcquirePreLicenseResponse[])array[0];
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x0008BF04 File Offset: 0x0008A104
		public IAsyncResult BeginAcquirePreLicense(AcquirePreLicenseParams[] RequestParams, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AcquirePreLicense", new object[]
			{
				RequestParams
			}, callback, asyncState);
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x0008BF2C File Offset: 0x0008A12C
		public AcquirePreLicenseResponse[] EndAcquirePreLicense(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AcquirePreLicenseResponse[])array[0];
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x0008BF49 File Offset: 0x0008A149
		public void AcquirePreLicenseAsync(AcquirePreLicenseParams[] RequestParams)
		{
			this.AcquirePreLicenseAsync(RequestParams, null);
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x0008BF54 File Offset: 0x0008A154
		public void AcquirePreLicenseAsync(AcquirePreLicenseParams[] RequestParams, object userState)
		{
			if (this.AcquirePreLicenseOperationCompleted == null)
			{
				this.AcquirePreLicenseOperationCompleted = new SendOrPostCallback(this.OnAcquirePreLicenseOperationCompleted);
			}
			base.InvokeAsync("AcquirePreLicense", new object[]
			{
				RequestParams
			}, this.AcquirePreLicenseOperationCompleted, userState);
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x0008BF9C File Offset: 0x0008A19C
		private void OnAcquirePreLicenseOperationCompleted(object arg)
		{
			if (this.AcquirePreLicenseCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AcquirePreLicenseCompleted(this, new AcquirePreLicenseCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x0008BFE1 File Offset: 0x0008A1E1
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x04002EC9 RID: 11977
		private VersionData versionDataValueField;

		// Token: 0x04002ECA RID: 11978
		private SendOrPostCallback AcquireLicenseOperationCompleted;

		// Token: 0x04002ECB RID: 11979
		private SendOrPostCallback AcquirePreLicenseOperationCompleted;
	}
}
