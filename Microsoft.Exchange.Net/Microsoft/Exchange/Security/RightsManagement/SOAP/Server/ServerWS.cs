using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Server
{
	// Token: 0x020009E5 RID: 2533
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[WebServiceBinding(Name = "ServerSoap", Namespace = "http://microsoft.com/DRM/ServerService")]
	internal class ServerWS : WsAsyncProxyWrapper
	{
		// Token: 0x06003741 RID: 14145 RVA: 0x0008C808 File Offset: 0x0008AA08
		public ServerWS()
		{
			base.Url = "https://localhost/_wmcs/licensing/server.asmx";
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06003742 RID: 14146 RVA: 0x0008C81B File Offset: 0x0008AA1B
		// (set) Token: 0x06003743 RID: 14147 RVA: 0x0008C823 File Offset: 0x0008AA23
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

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x06003744 RID: 14148 RVA: 0x0008C82C File Offset: 0x0008AA2C
		// (remove) Token: 0x06003745 RID: 14149 RVA: 0x0008C864 File Offset: 0x0008AA64
		public event GetLicensorCertificateCompletedEventHandler GetLicensorCertificateCompleted;

		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x06003746 RID: 14150 RVA: 0x0008C89C File Offset: 0x0008AA9C
		// (remove) Token: 0x06003747 RID: 14151 RVA: 0x0008C8D4 File Offset: 0x0008AAD4
		public event FindServiceLocationsCompletedEventHandler FindServiceLocationsCompleted;

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x06003748 RID: 14152 RVA: 0x0008C90C File Offset: 0x0008AB0C
		// (remove) Token: 0x06003749 RID: 14153 RVA: 0x0008C944 File Offset: 0x0008AB44
		public event GetServerInfoCompletedEventHandler GetServerInfoCompleted;

		// Token: 0x0600374A RID: 14154 RVA: 0x0008C97C File Offset: 0x0008AB7C
		[SoapHeader("VersionDataValue", Direction = SoapHeaderDirection.InOut)]
		[SoapDocumentMethod("http://microsoft.com/DRM/ServerService/GetLicensorCertificate", RequestNamespace = "http://microsoft.com/DRM/ServerService", ResponseNamespace = "http://microsoft.com/DRM/ServerService", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public LicensorCertChain GetLicensorCertificate()
		{
			object[] array = base.Invoke("GetLicensorCertificate", new object[0]);
			return (LicensorCertChain)array[0];
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x0008C9A3 File Offset: 0x0008ABA3
		public IAsyncResult BeginGetLicensorCertificate(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetLicensorCertificate", new object[0], callback, asyncState);
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x0008C9B8 File Offset: 0x0008ABB8
		public LicensorCertChain EndGetLicensorCertificate(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (LicensorCertChain)array[0];
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x0008C9D5 File Offset: 0x0008ABD5
		public void GetLicensorCertificateAsync()
		{
			this.GetLicensorCertificateAsync(null);
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x0008C9DE File Offset: 0x0008ABDE
		public void GetLicensorCertificateAsync(object userState)
		{
			if (this.GetLicensorCertificateOperationCompleted == null)
			{
				this.GetLicensorCertificateOperationCompleted = new SendOrPostCallback(this.OnGetLicensorCertificateOperationCompleted);
			}
			base.InvokeAsync("GetLicensorCertificate", new object[0], this.GetLicensorCertificateOperationCompleted, userState);
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x0008CA14 File Offset: 0x0008AC14
		private void OnGetLicensorCertificateOperationCompleted(object arg)
		{
			if (this.GetLicensorCertificateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetLicensorCertificateCompleted(this, new GetLicensorCertificateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x0008CA5C File Offset: 0x0008AC5C
		[SoapHeader("VersionDataValue", Direction = SoapHeaderDirection.InOut)]
		[SoapDocumentMethod("http://microsoft.com/DRM/ServerService/FindServiceLocations", RequestNamespace = "http://microsoft.com/DRM/ServerService", ResponseNamespace = "http://microsoft.com/DRM/ServerService", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public ServiceLocationResponse[] FindServiceLocations(ServiceLocationRequest[] ServiceNames)
		{
			object[] array = base.Invoke("FindServiceLocations", new object[]
			{
				ServiceNames
			});
			return (ServiceLocationResponse[])array[0];
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x0008CA8C File Offset: 0x0008AC8C
		public IAsyncResult BeginFindServiceLocations(ServiceLocationRequest[] ServiceNames, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindServiceLocations", new object[]
			{
				ServiceNames
			}, callback, asyncState);
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x0008CAB4 File Offset: 0x0008ACB4
		public ServiceLocationResponse[] EndFindServiceLocations(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ServiceLocationResponse[])array[0];
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x0008CAD1 File Offset: 0x0008ACD1
		public void FindServiceLocationsAsync(ServiceLocationRequest[] ServiceNames)
		{
			this.FindServiceLocationsAsync(ServiceNames, null);
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x0008CADC File Offset: 0x0008ACDC
		public void FindServiceLocationsAsync(ServiceLocationRequest[] ServiceNames, object userState)
		{
			if (this.FindServiceLocationsOperationCompleted == null)
			{
				this.FindServiceLocationsOperationCompleted = new SendOrPostCallback(this.OnFindServiceLocationsOperationCompleted);
			}
			base.InvokeAsync("FindServiceLocations", new object[]
			{
				ServiceNames
			}, this.FindServiceLocationsOperationCompleted, userState);
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x0008CB24 File Offset: 0x0008AD24
		private void OnFindServiceLocationsOperationCompleted(object arg)
		{
			if (this.FindServiceLocationsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindServiceLocationsCompleted(this, new FindServiceLocationsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x0008CB6C File Offset: 0x0008AD6C
		[SoapDocumentMethod("http://microsoft.com/DRM/ServerService/GetServerInfo", RequestNamespace = "http://microsoft.com/DRM/ServerService", ResponseNamespace = "http://microsoft.com/DRM/ServerService", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetServerInfo(ServerInfoRequest[] requests)
		{
			object[] array = base.Invoke("GetServerInfo", new object[]
			{
				requests
			});
			return (XmlNode)array[0];
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x0008CB9C File Offset: 0x0008AD9C
		public IAsyncResult BeginGetServerInfo(ServerInfoRequest[] requests, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetServerInfo", new object[]
			{
				requests
			}, callback, asyncState);
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x0008CBC4 File Offset: 0x0008ADC4
		public XmlNode EndGetServerInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x0008CBE1 File Offset: 0x0008ADE1
		public void GetServerInfoAsync(ServerInfoRequest[] requests)
		{
			this.GetServerInfoAsync(requests, null);
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x0008CBEC File Offset: 0x0008ADEC
		public void GetServerInfoAsync(ServerInfoRequest[] requests, object userState)
		{
			if (this.GetServerInfoOperationCompleted == null)
			{
				this.GetServerInfoOperationCompleted = new SendOrPostCallback(this.OnGetServerInfoOperationCompleted);
			}
			base.InvokeAsync("GetServerInfo", new object[]
			{
				requests
			}, this.GetServerInfoOperationCompleted, userState);
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x0008CC34 File Offset: 0x0008AE34
		private void OnGetServerInfoOperationCompleted(object arg)
		{
			if (this.GetServerInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetServerInfoCompleted(this, new GetServerInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x0008CC79 File Offset: 0x0008AE79
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x04002EFA RID: 12026
		private VersionData versionDataValueField;

		// Token: 0x04002EFB RID: 12027
		private SendOrPostCallback GetLicensorCertificateOperationCompleted;

		// Token: 0x04002EFC RID: 12028
		private SendOrPostCallback FindServiceLocationsOperationCompleted;

		// Token: 0x04002EFD RID: 12029
		private SendOrPostCallback GetServerInfoOperationCompleted;
	}
}
