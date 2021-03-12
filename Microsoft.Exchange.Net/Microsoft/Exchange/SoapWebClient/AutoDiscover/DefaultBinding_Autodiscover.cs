using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Security;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x0200010D RID: 269
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[WebServiceBinding(Name = "DefaultBinding_Autodiscover", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[XmlInclude(typeof(AutodiscoverResponse))]
	[XmlInclude(typeof(AutodiscoverRequest))]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class DefaultBinding_Autodiscover : CustomSoapHttpClientProtocol
	{
		// Token: 0x06000706 RID: 1798 RVA: 0x00017DB3 File Offset: 0x00015FB3
		public DefaultBinding_Autodiscover()
		{
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000707 RID: 1799 RVA: 0x00017DBC File Offset: 0x00015FBC
		// (remove) Token: 0x06000708 RID: 1800 RVA: 0x00017DF4 File Offset: 0x00015FF4
		public event GetUserSettingsCompletedEventHandler GetUserSettingsCompleted;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000709 RID: 1801 RVA: 0x00017E2C File Offset: 0x0001602C
		// (remove) Token: 0x0600070A RID: 1802 RVA: 0x00017E64 File Offset: 0x00016064
		public event GetDomainSettingsCompletedEventHandler GetDomainSettingsCompleted;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600070B RID: 1803 RVA: 0x00017E9C File Offset: 0x0001609C
		// (remove) Token: 0x0600070C RID: 1804 RVA: 0x00017ED4 File Offset: 0x000160D4
		public event GetFederationInformationCompletedEventHandler GetFederationInformationCompleted;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600070D RID: 1805 RVA: 0x00017F0C File Offset: 0x0001610C
		// (remove) Token: 0x0600070E RID: 1806 RVA: 0x00017F44 File Offset: 0x00016144
		public event GetOrganizationRelationshipSettingsCompletedEventHandler GetOrganizationRelationshipSettingsCompleted;

		// Token: 0x0600070F RID: 1807 RVA: 0x00017F7C File Offset: 0x0001617C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetUserSettings", RequestElementName = "GetUserSettingsRequestMessage", RequestNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", ResponseElementName = "GetUserSettingsResponseMessage", ResponseNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestedServerVersionValue")]
		[return: XmlElement("Response", IsNullable = true)]
		public GetUserSettingsResponse GetUserSettings([XmlElement(IsNullable = true)] GetUserSettingsRequest Request)
		{
			object[] array = this.Invoke("GetUserSettings", new object[]
			{
				Request
			});
			return (GetUserSettingsResponse)array[0];
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00017FAC File Offset: 0x000161AC
		public IAsyncResult BeginGetUserSettings(GetUserSettingsRequest Request, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserSettings", new object[]
			{
				Request
			}, callback, asyncState);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00017FD4 File Offset: 0x000161D4
		public GetUserSettingsResponse EndGetUserSettings(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUserSettingsResponse)array[0];
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00017FF1 File Offset: 0x000161F1
		public void GetUserSettingsAsync(GetUserSettingsRequest Request)
		{
			this.GetUserSettingsAsync(Request, null);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00017FFC File Offset: 0x000161FC
		public void GetUserSettingsAsync(GetUserSettingsRequest Request, object userState)
		{
			if (this.GetUserSettingsOperationCompleted == null)
			{
				this.GetUserSettingsOperationCompleted = new SendOrPostCallback(this.OnGetUserSettingsOperationCompleted);
			}
			base.InvokeAsync("GetUserSettings", new object[]
			{
				Request
			}, this.GetUserSettingsOperationCompleted, userState);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00018044 File Offset: 0x00016244
		private void OnGetUserSettingsOperationCompleted(object arg)
		{
			if (this.GetUserSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserSettingsCompleted(this, new GetUserSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001808C File Offset: 0x0001628C
		[SoapHeader("RequestedServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetDomainSettings", RequestElementName = "GetDomainSettingsRequestMessage", RequestNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", ResponseElementName = "GetDomainSettingsResponseMessage", ResponseNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("Response", IsNullable = true)]
		public GetDomainSettingsResponse GetDomainSettings([XmlElement(IsNullable = true)] GetDomainSettingsRequest Request)
		{
			object[] array = this.Invoke("GetDomainSettings", new object[]
			{
				Request
			});
			return (GetDomainSettingsResponse)array[0];
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000180BC File Offset: 0x000162BC
		public IAsyncResult BeginGetDomainSettings(GetDomainSettingsRequest Request, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDomainSettings", new object[]
			{
				Request
			}, callback, asyncState);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000180E4 File Offset: 0x000162E4
		public GetDomainSettingsResponse EndGetDomainSettings(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetDomainSettingsResponse)array[0];
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00018101 File Offset: 0x00016301
		public void GetDomainSettingsAsync(GetDomainSettingsRequest Request)
		{
			this.GetDomainSettingsAsync(Request, null);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001810C File Offset: 0x0001630C
		public void GetDomainSettingsAsync(GetDomainSettingsRequest Request, object userState)
		{
			if (this.GetDomainSettingsOperationCompleted == null)
			{
				this.GetDomainSettingsOperationCompleted = new SendOrPostCallback(this.OnGetDomainSettingsOperationCompleted);
			}
			base.InvokeAsync("GetDomainSettings", new object[]
			{
				Request
			}, this.GetDomainSettingsOperationCompleted, userState);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00018154 File Offset: 0x00016354
		private void OnGetDomainSettingsOperationCompleted(object arg)
		{
			if (this.GetDomainSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDomainSettingsCompleted(this, new GetDomainSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001819C File Offset: 0x0001639C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestedServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetFederationInformation", RequestElementName = "GetFederationInformationRequestMessage", RequestNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", ResponseElementName = "GetFederationInformationResponseMessage", ResponseNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("Response", IsNullable = true)]
		public GetFederationInformationResponse GetFederationInformation([XmlElement(IsNullable = true)] GetFederationInformationRequest Request)
		{
			object[] array = this.Invoke("GetFederationInformation", new object[]
			{
				Request
			});
			return (GetFederationInformationResponse)array[0];
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000181CC File Offset: 0x000163CC
		public IAsyncResult BeginGetFederationInformation(GetFederationInformationRequest Request, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetFederationInformation", new object[]
			{
				Request
			}, callback, asyncState);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000181F4 File Offset: 0x000163F4
		public GetFederationInformationResponse EndGetFederationInformation(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetFederationInformationResponse)array[0];
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00018211 File Offset: 0x00016411
		public void GetFederationInformationAsync(GetFederationInformationRequest Request)
		{
			this.GetFederationInformationAsync(Request, null);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001821C File Offset: 0x0001641C
		public void GetFederationInformationAsync(GetFederationInformationRequest Request, object userState)
		{
			if (this.GetFederationInformationOperationCompleted == null)
			{
				this.GetFederationInformationOperationCompleted = new SendOrPostCallback(this.OnGetFederationInformationOperationCompleted);
			}
			base.InvokeAsync("GetFederationInformation", new object[]
			{
				Request
			}, this.GetFederationInformationOperationCompleted, userState);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00018264 File Offset: 0x00016464
		private void OnGetFederationInformationOperationCompleted(object arg)
		{
			if (this.GetFederationInformationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetFederationInformationCompleted(this, new GetFederationInformationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000182AC File Offset: 0x000164AC
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetOrganizationRelationshipSettings", RequestElementName = "GetOrganizationRelationshipSettingsRequestMessage", RequestNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", ResponseElementName = "GetOrganizationRelationshipSettingsResponseMessage", ResponseNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("RequestedServerVersionValue")]
		[return: XmlElement("Response", IsNullable = true)]
		public GetOrganizationRelationshipSettingsResponse GetOrganizationRelationshipSettings([XmlElement(IsNullable = true)] GetOrganizationRelationshipSettingsRequest Request)
		{
			object[] array = this.Invoke("GetOrganizationRelationshipSettings", new object[]
			{
				Request
			});
			return (GetOrganizationRelationshipSettingsResponse)array[0];
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000182DC File Offset: 0x000164DC
		public IAsyncResult BeginGetOrganizationRelationshipSettings(GetOrganizationRelationshipSettingsRequest Request, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetOrganizationRelationshipSettings", new object[]
			{
				Request
			}, callback, asyncState);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00018304 File Offset: 0x00016504
		public GetOrganizationRelationshipSettingsResponse EndGetOrganizationRelationshipSettings(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetOrganizationRelationshipSettingsResponse)array[0];
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00018321 File Offset: 0x00016521
		public void GetOrganizationRelationshipSettingsAsync(GetOrganizationRelationshipSettingsRequest Request)
		{
			this.GetOrganizationRelationshipSettingsAsync(Request, null);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001832C File Offset: 0x0001652C
		public void GetOrganizationRelationshipSettingsAsync(GetOrganizationRelationshipSettingsRequest Request, object userState)
		{
			if (this.GetOrganizationRelationshipSettingsOperationCompleted == null)
			{
				this.GetOrganizationRelationshipSettingsOperationCompleted = new SendOrPostCallback(this.OnGetOrganizationRelationshipSettingsOperationCompleted);
			}
			base.InvokeAsync("GetOrganizationRelationshipSettings", new object[]
			{
				Request
			}, this.GetOrganizationRelationshipSettingsOperationCompleted, userState);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00018374 File Offset: 0x00016574
		private void OnGetOrganizationRelationshipSettingsOperationCompleted(object arg)
		{
			if (this.GetOrganizationRelationshipSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetOrganizationRelationshipSettingsCompleted(this, new GetOrganizationRelationshipSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000183B9 File Offset: 0x000165B9
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000183C2 File Offset: 0x000165C2
		public DefaultBinding_Autodiscover(string componentId, RemoteCertificateValidationCallback remoteCertificateValidationCallback) : base(componentId, remoteCertificateValidationCallback, true)
		{
			base.Authenticator = SoapHttpClientAuthenticator.CreateAnonymous();
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x000183D8 File Offset: 0x000165D8
		internal override XmlNamespaceDefinition[] PredefinedNamespaces
		{
			get
			{
				return Constants.EwsNamespaces;
			}
		}

		// Token: 0x04000581 RID: 1409
		public RequestedServerVersion RequestedServerVersionValue;

		// Token: 0x04000582 RID: 1410
		public ServerVersionInfo ServerVersionInfoValue;

		// Token: 0x04000583 RID: 1411
		private SendOrPostCallback GetUserSettingsOperationCompleted;

		// Token: 0x04000584 RID: 1412
		private SendOrPostCallback GetDomainSettingsOperationCompleted;

		// Token: 0x04000585 RID: 1413
		private SendOrPostCallback GetFederationInformationOperationCompleted;

		// Token: 0x04000586 RID: 1414
		private SendOrPostCallback GetOrganizationRelationshipSettingsOperationCompleted;

		// Token: 0x0400058B RID: 1419
		internal static RequestedServerVersion Exchange2010RequestedServerVersion = new RequestedServerVersion
		{
			Text = new string[]
			{
				"Exchange2010"
			}
		};
	}
}
