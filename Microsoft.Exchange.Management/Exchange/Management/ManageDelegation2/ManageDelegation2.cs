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
using Microsoft.Exchange.SoapWebClient;

namespace Microsoft.Exchange.Management.ManageDelegation2
{
	// Token: 0x0200033F RID: 831
	[WebServiceBinding(Name = "ManageDelegation2Soap", Namespace = "http://domains.live.com/Service/ManageDelegation2/V1.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class ManageDelegation2 : CustomSoapHttpClientProtocol
	{
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x0007E661 File Offset: 0x0007C861
		internal override XmlNamespaceDefinition[] PredefinedNamespaces
		{
			get
			{
				return ManageDelegation2.predefinedNamespaces;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x0007E668 File Offset: 0x0007C868
		protected override bool SuppressMustUnderstand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x0007E66B File Offset: 0x0007C86B
		public ManageDelegation2(string componentId, RemoteCertificateValidationCallback remoteCertificateValidationCallback) : base(componentId, remoteCertificateValidationCallback, true)
		{
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0007E676 File Offset: 0x0007C876
		public ManageDelegation2()
		{
			base.Url = "https://domains-dev.live-int.com/service/ManageDelegation2.asmx";
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06001C77 RID: 7287 RVA: 0x0007E68C File Offset: 0x0007C88C
		// (remove) Token: 0x06001C78 RID: 7288 RVA: 0x0007E6C4 File Offset: 0x0007C8C4
		public event CreateAppIdCompletedEventHandler CreateAppIdCompleted;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06001C79 RID: 7289 RVA: 0x0007E6FC File Offset: 0x0007C8FC
		// (remove) Token: 0x06001C7A RID: 7290 RVA: 0x0007E734 File Offset: 0x0007C934
		public event UpdateAppIdCertificateCompletedEventHandler UpdateAppIdCertificateCompleted;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06001C7B RID: 7291 RVA: 0x0007E76C File Offset: 0x0007C96C
		// (remove) Token: 0x06001C7C RID: 7292 RVA: 0x0007E7A4 File Offset: 0x0007C9A4
		public event UpdateAppIdPropertiesCompletedEventHandler UpdateAppIdPropertiesCompleted;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06001C7D RID: 7293 RVA: 0x0007E7DC File Offset: 0x0007C9DC
		// (remove) Token: 0x06001C7E RID: 7294 RVA: 0x0007E814 File Offset: 0x0007CA14
		public event AddUriCompletedEventHandler AddUriCompleted;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06001C7F RID: 7295 RVA: 0x0007E84C File Offset: 0x0007CA4C
		// (remove) Token: 0x06001C80 RID: 7296 RVA: 0x0007E884 File Offset: 0x0007CA84
		public event RemoveUriCompletedEventHandler RemoveUriCompleted;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06001C81 RID: 7297 RVA: 0x0007E8BC File Offset: 0x0007CABC
		// (remove) Token: 0x06001C82 RID: 7298 RVA: 0x0007E8F4 File Offset: 0x0007CAF4
		public event ReserveDomainCompletedEventHandler ReserveDomainCompleted;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06001C83 RID: 7299 RVA: 0x0007E92C File Offset: 0x0007CB2C
		// (remove) Token: 0x06001C84 RID: 7300 RVA: 0x0007E964 File Offset: 0x0007CB64
		public event ReleaseDomainCompletedEventHandler ReleaseDomainCompleted;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06001C85 RID: 7301 RVA: 0x0007E99C File Offset: 0x0007CB9C
		// (remove) Token: 0x06001C86 RID: 7302 RVA: 0x0007E9D4 File Offset: 0x0007CBD4
		public event GetDomainInfoCompletedEventHandler GetDomainInfoCompleted;

		// Token: 0x06001C87 RID: 7303 RVA: 0x0007EA0C File Offset: 0x0007CC0C
		[SoapHeader("DomainOwnershipProofHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation2/V1.0/CreateAppId", RequestNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("Security")]
		public AppIdInfo CreateAppId(string uri, [XmlArrayItem(IsNullable = false)] Property[] properties)
		{
			object[] array = this.Invoke("CreateAppId", new object[]
			{
				uri,
				properties
			});
			return (AppIdInfo)array[0];
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x0007EA40 File Offset: 0x0007CC40
		public IAsyncResult BeginCreateAppId(string uri, Property[] properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateAppId", new object[]
			{
				uri,
				properties
			}, callback, asyncState);
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0007EA6C File Offset: 0x0007CC6C
		public AppIdInfo EndCreateAppId(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AppIdInfo)array[0];
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0007EA89 File Offset: 0x0007CC89
		public void CreateAppIdAsync(string uri, Property[] properties)
		{
			this.CreateAppIdAsync(uri, properties, null);
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0007EA94 File Offset: 0x0007CC94
		public void CreateAppIdAsync(string uri, Property[] properties, object userState)
		{
			if (this.CreateAppIdOperationCompleted == null)
			{
				this.CreateAppIdOperationCompleted = new SendOrPostCallback(this.OnCreateAppIdOperationCompleted);
			}
			base.InvokeAsync("CreateAppId", new object[]
			{
				uri,
				properties
			}, this.CreateAppIdOperationCompleted, userState);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0007EAE0 File Offset: 0x0007CCE0
		private void OnCreateAppIdOperationCompleted(object arg)
		{
			if (this.CreateAppIdCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateAppIdCompleted(this, new CreateAppIdCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0007EB28 File Offset: 0x0007CD28
		[SoapHttpClientTraceExtension]
		[SoapHeader("Security")]
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation2/V1.0/UpdateAppIdCertificate", RequestNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void UpdateAppIdCertificate(string appId, string newCertificate)
		{
			this.Invoke("UpdateAppIdCertificate", new object[]
			{
				appId,
				newCertificate
			});
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0007EB54 File Offset: 0x0007CD54
		public IAsyncResult BeginUpdateAppIdCertificate(string appId, string newCertificate, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateAppIdCertificate", new object[]
			{
				appId,
				newCertificate
			}, callback, asyncState);
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0007EB7F File Offset: 0x0007CD7F
		public void EndUpdateAppIdCertificate(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0007EB89 File Offset: 0x0007CD89
		public void UpdateAppIdCertificateAsync(string appId, string newCertificate)
		{
			this.UpdateAppIdCertificateAsync(appId, newCertificate, null);
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0007EB94 File Offset: 0x0007CD94
		public void UpdateAppIdCertificateAsync(string appId, string newCertificate, object userState)
		{
			if (this.UpdateAppIdCertificateOperationCompleted == null)
			{
				this.UpdateAppIdCertificateOperationCompleted = new SendOrPostCallback(this.OnUpdateAppIdCertificateOperationCompleted);
			}
			base.InvokeAsync("UpdateAppIdCertificate", new object[]
			{
				appId,
				newCertificate
			}, this.UpdateAppIdCertificateOperationCompleted, userState);
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x0007EBE0 File Offset: 0x0007CDE0
		private void OnUpdateAppIdCertificateOperationCompleted(object arg)
		{
			if (this.UpdateAppIdCertificateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateAppIdCertificateCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x0007EC20 File Offset: 0x0007CE20
		[SoapHttpClientTraceExtension]
		[SoapHeader("Security")]
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation2/V1.0/UpdateAppIdProperties", RequestNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void UpdateAppIdProperties(string appId, [XmlArrayItem(IsNullable = false)] Property[] properties)
		{
			this.Invoke("UpdateAppIdProperties", new object[]
			{
				appId,
				properties
			});
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x0007EC4C File Offset: 0x0007CE4C
		public IAsyncResult BeginUpdateAppIdProperties(string appId, Property[] properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateAppIdProperties", new object[]
			{
				appId,
				properties
			}, callback, asyncState);
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x0007EC77 File Offset: 0x0007CE77
		public void EndUpdateAppIdProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x0007EC81 File Offset: 0x0007CE81
		public void UpdateAppIdPropertiesAsync(string appId, Property[] properties)
		{
			this.UpdateAppIdPropertiesAsync(appId, properties, null);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x0007EC8C File Offset: 0x0007CE8C
		public void UpdateAppIdPropertiesAsync(string appId, Property[] properties, object userState)
		{
			if (this.UpdateAppIdPropertiesOperationCompleted == null)
			{
				this.UpdateAppIdPropertiesOperationCompleted = new SendOrPostCallback(this.OnUpdateAppIdPropertiesOperationCompleted);
			}
			base.InvokeAsync("UpdateAppIdProperties", new object[]
			{
				appId,
				properties
			}, this.UpdateAppIdPropertiesOperationCompleted, userState);
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x0007ECD8 File Offset: 0x0007CED8
		private void OnUpdateAppIdPropertiesOperationCompleted(object arg)
		{
			if (this.UpdateAppIdPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateAppIdPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x0007ED18 File Offset: 0x0007CF18
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation2/V1.0/AddUri", RequestNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("Security")]
		[SoapHeader("DomainOwnershipProofHeaderValue")]
		[SoapHttpClientTraceExtension]
		public void AddUri(string appId, string uri)
		{
			this.Invoke("AddUri", new object[]
			{
				appId,
				uri
			});
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x0007ED44 File Offset: 0x0007CF44
		public IAsyncResult BeginAddUri(string appId, string uri, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddUri", new object[]
			{
				appId,
				uri
			}, callback, asyncState);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x0007ED6F File Offset: 0x0007CF6F
		public void EndAddUri(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x0007ED79 File Offset: 0x0007CF79
		public void AddUriAsync(string appId, string uri)
		{
			this.AddUriAsync(appId, uri, null);
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x0007ED84 File Offset: 0x0007CF84
		public void AddUriAsync(string appId, string uri, object userState)
		{
			if (this.AddUriOperationCompleted == null)
			{
				this.AddUriOperationCompleted = new SendOrPostCallback(this.OnAddUriOperationCompleted);
			}
			base.InvokeAsync("AddUri", new object[]
			{
				appId,
				uri
			}, this.AddUriOperationCompleted, userState);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x0007EDD0 File Offset: 0x0007CFD0
		private void OnAddUriOperationCompleted(object arg)
		{
			if (this.AddUriCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddUriCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x0007EE10 File Offset: 0x0007D010
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation2/V1.0/RemoveUri", RequestNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("Security")]
		public void RemoveUri(string appId, string uri)
		{
			this.Invoke("RemoveUri", new object[]
			{
				appId,
				uri
			});
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0007EE3C File Offset: 0x0007D03C
		public IAsyncResult BeginRemoveUri(string appId, string uri, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveUri", new object[]
			{
				appId,
				uri
			}, callback, asyncState);
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0007EE67 File Offset: 0x0007D067
		public void EndRemoveUri(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0007EE71 File Offset: 0x0007D071
		public void RemoveUriAsync(string appId, string uri)
		{
			this.RemoveUriAsync(appId, uri, null);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0007EE7C File Offset: 0x0007D07C
		public void RemoveUriAsync(string appId, string uri, object userState)
		{
			if (this.RemoveUriOperationCompleted == null)
			{
				this.RemoveUriOperationCompleted = new SendOrPostCallback(this.OnRemoveUriOperationCompleted);
			}
			base.InvokeAsync("RemoveUri", new object[]
			{
				appId,
				uri
			}, this.RemoveUriOperationCompleted, userState);
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0007EEC8 File Offset: 0x0007D0C8
		private void OnRemoveUriOperationCompleted(object arg)
		{
			if (this.RemoveUriCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveUriCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0007EF08 File Offset: 0x0007D108
		[SoapHeader("Security")]
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation2/V1.0/ReserveDomain", RequestNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("DomainOwnershipProofHeaderValue")]
		[SoapHttpClientTraceExtension]
		public void ReserveDomain(string appId, string domainName, string programId)
		{
			this.Invoke("ReserveDomain", new object[]
			{
				appId,
				domainName,
				programId
			});
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0007EF38 File Offset: 0x0007D138
		public IAsyncResult BeginReserveDomain(string appId, string domainName, string programId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ReserveDomain", new object[]
			{
				appId,
				domainName,
				programId
			}, callback, asyncState);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0007EF68 File Offset: 0x0007D168
		public void EndReserveDomain(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0007EF72 File Offset: 0x0007D172
		public void ReserveDomainAsync(string appId, string domainName, string programId)
		{
			this.ReserveDomainAsync(appId, domainName, programId, null);
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0007EF80 File Offset: 0x0007D180
		public void ReserveDomainAsync(string appId, string domainName, string programId, object userState)
		{
			if (this.ReserveDomainOperationCompleted == null)
			{
				this.ReserveDomainOperationCompleted = new SendOrPostCallback(this.OnReserveDomainOperationCompleted);
			}
			base.InvokeAsync("ReserveDomain", new object[]
			{
				appId,
				domainName,
				programId
			}, this.ReserveDomainOperationCompleted, userState);
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0007EFD0 File Offset: 0x0007D1D0
		private void OnReserveDomainOperationCompleted(object arg)
		{
			if (this.ReserveDomainCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ReserveDomainCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0007F010 File Offset: 0x0007D210
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation2/V1.0/ReleaseDomain", RequestNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("Security")]
		[SoapHttpClientTraceExtension]
		public void ReleaseDomain(string appId, string domainName)
		{
			this.Invoke("ReleaseDomain", new object[]
			{
				appId,
				domainName
			});
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0007F03C File Offset: 0x0007D23C
		public IAsyncResult BeginReleaseDomain(string appId, string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ReleaseDomain", new object[]
			{
				appId,
				domainName
			}, callback, asyncState);
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0007F067 File Offset: 0x0007D267
		public void EndReleaseDomain(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0007F071 File Offset: 0x0007D271
		public void ReleaseDomainAsync(string appId, string domainName)
		{
			this.ReleaseDomainAsync(appId, domainName, null);
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0007F07C File Offset: 0x0007D27C
		public void ReleaseDomainAsync(string appId, string domainName, object userState)
		{
			if (this.ReleaseDomainOperationCompleted == null)
			{
				this.ReleaseDomainOperationCompleted = new SendOrPostCallback(this.OnReleaseDomainOperationCompleted);
			}
			base.InvokeAsync("ReleaseDomain", new object[]
			{
				appId,
				domainName
			}, this.ReleaseDomainOperationCompleted, userState);
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0007F0C8 File Offset: 0x0007D2C8
		private void OnReleaseDomainOperationCompleted(object arg)
		{
			if (this.ReleaseDomainCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ReleaseDomainCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0007F108 File Offset: 0x0007D308
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation2/V1.0/GetDomainInfo", RequestNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("Security")]
		public DomainInfo GetDomainInfo(string appId, string domainName)
		{
			object[] array = this.Invoke("GetDomainInfo", new object[]
			{
				appId,
				domainName
			});
			return (DomainInfo)array[0];
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0007F13C File Offset: 0x0007D33C
		public IAsyncResult BeginGetDomainInfo(string appId, string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDomainInfo", new object[]
			{
				appId,
				domainName
			}, callback, asyncState);
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0007F168 File Offset: 0x0007D368
		public DomainInfo EndGetDomainInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DomainInfo)array[0];
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0007F185 File Offset: 0x0007D385
		public void GetDomainInfoAsync(string appId, string domainName)
		{
			this.GetDomainInfoAsync(appId, domainName, null);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0007F190 File Offset: 0x0007D390
		public void GetDomainInfoAsync(string appId, string domainName, object userState)
		{
			if (this.GetDomainInfoOperationCompleted == null)
			{
				this.GetDomainInfoOperationCompleted = new SendOrPostCallback(this.OnGetDomainInfoOperationCompleted);
			}
			base.InvokeAsync("GetDomainInfo", new object[]
			{
				appId,
				domainName
			}, this.GetDomainInfoOperationCompleted, userState);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0007F1DC File Offset: 0x0007D3DC
		private void OnGetDomainInfoOperationCompleted(object arg)
		{
			if (this.GetDomainInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDomainInfoCompleted(this, new GetDomainInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0007F221 File Offset: 0x0007D421
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x04001844 RID: 6212
		private const string Namespace = "http://domains.live.com/Service/ManageDelegation2/V1.0";

		// Token: 0x04001845 RID: 6213
		private static readonly XmlNamespaceDefinition[] predefinedNamespaces = new XmlNamespaceDefinition[]
		{
			new XmlNamespaceDefinition("md", "http://domains.live.com/Service/ManageDelegation2/V1.0")
		};

		// Token: 0x04001846 RID: 6214
		public WSSecurityHeader Security;

		// Token: 0x04001847 RID: 6215
		public DomainOwnershipProofHeader DomainOwnershipProofHeaderValue;

		// Token: 0x04001848 RID: 6216
		private SendOrPostCallback CreateAppIdOperationCompleted;

		// Token: 0x04001849 RID: 6217
		private SendOrPostCallback UpdateAppIdCertificateOperationCompleted;

		// Token: 0x0400184A RID: 6218
		private SendOrPostCallback UpdateAppIdPropertiesOperationCompleted;

		// Token: 0x0400184B RID: 6219
		private SendOrPostCallback AddUriOperationCompleted;

		// Token: 0x0400184C RID: 6220
		private SendOrPostCallback RemoveUriOperationCompleted;

		// Token: 0x0400184D RID: 6221
		private SendOrPostCallback ReserveDomainOperationCompleted;

		// Token: 0x0400184E RID: 6222
		private SendOrPostCallback ReleaseDomainOperationCompleted;

		// Token: 0x0400184F RID: 6223
		private SendOrPostCallback GetDomainInfoOperationCompleted;
	}
}
