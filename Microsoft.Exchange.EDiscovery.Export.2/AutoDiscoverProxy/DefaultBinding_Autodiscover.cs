using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200003D RID: 61
	[DebuggerStepThrough]
	[XmlInclude(typeof(AutodiscoverRequest))]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "DefaultBinding_Autodiscover", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[XmlInclude(typeof(AutodiscoverResponse))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class DefaultBinding_Autodiscover : SoapHttpClientProtocol, IServiceBinding
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00007F18 File Offset: 0x00006118
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00007F20 File Offset: 0x00006120
		public OpenAsAdminOrSystemServiceType OpenAsAdminOrSystemService { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00007F29 File Offset: 0x00006129
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00007F31 File Offset: 0x00006131
		public Action Action { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00007F3A File Offset: 0x0000613A
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00007F42 File Offset: 0x00006142
		public To To { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00007F4B File Offset: 0x0000614B
		// (set) Token: 0x0600024A RID: 586 RVA: 0x00007F53 File Offset: 0x00006153
		public ServiceHttpContext HttpContext { get; set; }

		// Token: 0x0600024B RID: 587 RVA: 0x00007F5C File Offset: 0x0000615C
		protected override XmlWriter GetWriterForMessage(SoapClientMessage message, int bufferSize)
		{
			if (this.Action != null)
			{
				message.Headers.Add(this.Action);
			}
			if (this.To != null)
			{
				message.Headers.Add(this.To);
			}
			if (this.OpenAsAdminOrSystemService != null)
			{
				this.OpenAsAdminOrSystemService.BudgetType = 1;
				this.OpenAsAdminOrSystemService.BudgetTypeSpecified = true;
				message.Headers.Add(this.OpenAsAdminOrSystemService);
			}
			return base.GetWriterForMessage(message, bufferSize);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00007FD8 File Offset: 0x000061D8
		protected override XmlReader GetReaderForMessage(SoapClientMessage message, int bufferSize)
		{
			XmlReader readerForMessage = base.GetReaderForMessage(message, bufferSize);
			XmlTextReader xmlTextReader = readerForMessage as XmlTextReader;
			if (xmlTextReader != null)
			{
				xmlTextReader.Normalization = false;
				xmlTextReader.DtdProcessing = DtdProcessing.Ignore;
				xmlTextReader.XmlResolver = null;
			}
			return new AutoDiscoverClientXmlReader(readerForMessage);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00008014 File Offset: 0x00006214
		protected override WebRequest GetWebRequest(Uri url)
		{
			WebRequest webRequest = base.GetWebRequest(url);
			if (this.HttpContext != null)
			{
				this.HttpContext.SetRequestHttpHeaders(webRequest);
			}
			return webRequest;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008040 File Offset: 0x00006240
		protected override WebResponse GetWebResponse(WebRequest request)
		{
			WebResponse webResponse = base.GetWebResponse(request);
			if (this.HttpContext != null)
			{
				this.HttpContext.UpdateContextFromResponse(webResponse);
			}
			return webResponse;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00008072 File Offset: 0x00006272
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000807A File Offset: 0x0000627A
		public RequestedServerVersion RequestedServerVersionValue
		{
			get
			{
				return this.requestedServerVersionValueField;
			}
			set
			{
				this.requestedServerVersionValueField = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00008083 File Offset: 0x00006283
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000808B File Offset: 0x0000628B
		public ServerVersionInfo ServerVersionInfoValue
		{
			get
			{
				return this.serverVersionInfoValueField;
			}
			set
			{
				this.serverVersionInfoValueField = value;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000254 RID: 596 RVA: 0x00008094 File Offset: 0x00006294
		// (remove) Token: 0x06000255 RID: 597 RVA: 0x000080CC File Offset: 0x000062CC
		public event GetUserSettingsCompletedEventHandler GetUserSettingsCompleted;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000256 RID: 598 RVA: 0x00008104 File Offset: 0x00006304
		// (remove) Token: 0x06000257 RID: 599 RVA: 0x0000813C File Offset: 0x0000633C
		public event GetDomainSettingsCompletedEventHandler GetDomainSettingsCompleted;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000258 RID: 600 RVA: 0x00008174 File Offset: 0x00006374
		// (remove) Token: 0x06000259 RID: 601 RVA: 0x000081AC File Offset: 0x000063AC
		public event GetFederationInformationCompletedEventHandler GetFederationInformationCompleted;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600025A RID: 602 RVA: 0x000081E4 File Offset: 0x000063E4
		// (remove) Token: 0x0600025B RID: 603 RVA: 0x0000821C File Offset: 0x0000641C
		public event GetOrganizationRelationshipSettingsCompletedEventHandler GetOrganizationRelationshipSettingsCompleted;

		// Token: 0x0600025C RID: 604 RVA: 0x00008254 File Offset: 0x00006454
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetUserSettings", RequestElementName = "GetUserSettingsRequestMessage", RequestNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", ResponseElementName = "GetUserSettingsResponseMessage", ResponseNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("RequestedServerVersionValue")]
		[return: XmlElement("Response", IsNullable = true)]
		public GetUserSettingsResponse GetUserSettings([XmlElement(IsNullable = true)] GetUserSettingsRequest Request)
		{
			object[] array = base.Invoke("GetUserSettings", new object[]
			{
				Request
			});
			return (GetUserSettingsResponse)array[0];
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00008284 File Offset: 0x00006484
		public IAsyncResult BeginGetUserSettings(GetUserSettingsRequest Request, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserSettings", new object[]
			{
				Request
			}, callback, asyncState);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000082AC File Offset: 0x000064AC
		public GetUserSettingsResponse EndGetUserSettings(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUserSettingsResponse)array[0];
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000082C9 File Offset: 0x000064C9
		public void GetUserSettingsAsync(GetUserSettingsRequest Request)
		{
			this.GetUserSettingsAsync(Request, null);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000082D4 File Offset: 0x000064D4
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

		// Token: 0x06000261 RID: 609 RVA: 0x0000831C File Offset: 0x0000651C
		private void OnGetUserSettingsOperationCompleted(object arg)
		{
			if (this.GetUserSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserSettingsCompleted(this, new GetUserSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00008364 File Offset: 0x00006564
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetDomainSettings", RequestElementName = "GetDomainSettingsRequestMessage", RequestNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", ResponseElementName = "GetDomainSettingsResponseMessage", ResponseNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestedServerVersionValue")]
		[return: XmlElement("Response", IsNullable = true)]
		public GetDomainSettingsResponse GetDomainSettings([XmlElement(IsNullable = true)] GetDomainSettingsRequest Request)
		{
			object[] array = base.Invoke("GetDomainSettings", new object[]
			{
				Request
			});
			return (GetDomainSettingsResponse)array[0];
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00008394 File Offset: 0x00006594
		public IAsyncResult BeginGetDomainSettings(GetDomainSettingsRequest Request, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDomainSettings", new object[]
			{
				Request
			}, callback, asyncState);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000083BC File Offset: 0x000065BC
		public GetDomainSettingsResponse EndGetDomainSettings(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetDomainSettingsResponse)array[0];
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000083D9 File Offset: 0x000065D9
		public void GetDomainSettingsAsync(GetDomainSettingsRequest Request)
		{
			this.GetDomainSettingsAsync(Request, null);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000083E4 File Offset: 0x000065E4
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

		// Token: 0x06000267 RID: 615 RVA: 0x0000842C File Offset: 0x0000662C
		private void OnGetDomainSettingsOperationCompleted(object arg)
		{
			if (this.GetDomainSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDomainSettingsCompleted(this, new GetDomainSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00008474 File Offset: 0x00006674
		[SoapHeader("RequestedServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetFederationInformation", RequestElementName = "GetFederationInformationRequestMessage", RequestNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", ResponseElementName = "GetFederationInformationResponseMessage", ResponseNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Response", IsNullable = true)]
		public GetFederationInformationResponse GetFederationInformation([XmlElement(IsNullable = true)] GetFederationInformationRequest Request)
		{
			object[] array = base.Invoke("GetFederationInformation", new object[]
			{
				Request
			});
			return (GetFederationInformationResponse)array[0];
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000084A4 File Offset: 0x000066A4
		public IAsyncResult BeginGetFederationInformation(GetFederationInformationRequest Request, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetFederationInformation", new object[]
			{
				Request
			}, callback, asyncState);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000084CC File Offset: 0x000066CC
		public GetFederationInformationResponse EndGetFederationInformation(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetFederationInformationResponse)array[0];
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000084E9 File Offset: 0x000066E9
		public void GetFederationInformationAsync(GetFederationInformationRequest Request)
		{
			this.GetFederationInformationAsync(Request, null);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000084F4 File Offset: 0x000066F4
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

		// Token: 0x0600026D RID: 621 RVA: 0x0000853C File Offset: 0x0000673C
		private void OnGetFederationInformationOperationCompleted(object arg)
		{
			if (this.GetFederationInformationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetFederationInformationCompleted(this, new GetFederationInformationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008584 File Offset: 0x00006784
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetOrganizationRelationshipSettings", RequestElementName = "GetOrganizationRelationshipSettingsRequestMessage", RequestNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", ResponseElementName = "GetOrganizationRelationshipSettingsResponseMessage", ResponseNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("RequestedServerVersionValue")]
		[return: XmlElement("Response", IsNullable = true)]
		public GetOrganizationRelationshipSettingsResponse GetOrganizationRelationshipSettings([XmlElement(IsNullable = true)] GetOrganizationRelationshipSettingsRequest Request)
		{
			object[] array = base.Invoke("GetOrganizationRelationshipSettings", new object[]
			{
				Request
			});
			return (GetOrganizationRelationshipSettingsResponse)array[0];
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000085B4 File Offset: 0x000067B4
		public IAsyncResult BeginGetOrganizationRelationshipSettings(GetOrganizationRelationshipSettingsRequest Request, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetOrganizationRelationshipSettings", new object[]
			{
				Request
			}, callback, asyncState);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000085DC File Offset: 0x000067DC
		public GetOrganizationRelationshipSettingsResponse EndGetOrganizationRelationshipSettings(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetOrganizationRelationshipSettingsResponse)array[0];
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000085F9 File Offset: 0x000067F9
		public void GetOrganizationRelationshipSettingsAsync(GetOrganizationRelationshipSettingsRequest Request)
		{
			this.GetOrganizationRelationshipSettingsAsync(Request, null);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008604 File Offset: 0x00006804
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

		// Token: 0x06000273 RID: 627 RVA: 0x0000864C File Offset: 0x0000684C
		private void OnGetOrganizationRelationshipSettingsOperationCompleted(object arg)
		{
			if (this.GetOrganizationRelationshipSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetOrganizationRelationshipSettingsCompleted(this, new GetOrganizationRelationshipSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00008691 File Offset: 0x00006891
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x040000CB RID: 203
		private RequestedServerVersion requestedServerVersionValueField;

		// Token: 0x040000CC RID: 204
		private ServerVersionInfo serverVersionInfoValueField;

		// Token: 0x040000CD RID: 205
		private SendOrPostCallback GetUserSettingsOperationCompleted;

		// Token: 0x040000CE RID: 206
		private SendOrPostCallback GetDomainSettingsOperationCompleted;

		// Token: 0x040000CF RID: 207
		private SendOrPostCallback GetFederationInformationOperationCompleted;

		// Token: 0x040000D0 RID: 208
		private SendOrPostCallback GetOrganizationRelationshipSettingsOperationCompleted;
	}
}
