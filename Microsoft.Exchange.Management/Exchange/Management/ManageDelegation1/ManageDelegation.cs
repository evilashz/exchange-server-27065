using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Security;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.SoapWebClient;

namespace Microsoft.Exchange.Management.ManageDelegation1
{
	// Token: 0x0200033B RID: 827
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "ManageDelegationSoap", Namespace = "http://domains.live.com/Service/ManageDelegation/V1.0")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class ManageDelegation : CustomSoapHttpClientProtocol
	{
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0007CD50 File Offset: 0x0007AF50
		internal override XmlNamespaceDefinition[] PredefinedNamespaces
		{
			get
			{
				return ManageDelegation.predefinedNamespaces;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x0007CD57 File Offset: 0x0007AF57
		protected override bool SuppressMustUnderstand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x0007CD5A File Offset: 0x0007AF5A
		public ManageDelegation(string componentId, RemoteCertificateValidationCallback remoteCertificateValidationCallback) : base(componentId, remoteCertificateValidationCallback, true)
		{
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x0007CD65 File Offset: 0x0007AF65
		public ManageDelegation()
		{
			base.Url = "https://domains-tst.live-int.com/service/managedelegation.asmx";
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06001C0D RID: 7181 RVA: 0x0007CD78 File Offset: 0x0007AF78
		// (remove) Token: 0x06001C0E RID: 7182 RVA: 0x0007CDB0 File Offset: 0x0007AFB0
		public event CreateAppIdCompletedEventHandler CreateAppIdCompleted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06001C0F RID: 7183 RVA: 0x0007CDE8 File Offset: 0x0007AFE8
		// (remove) Token: 0x06001C10 RID: 7184 RVA: 0x0007CE20 File Offset: 0x0007B020
		public event UpdateAppIdCertificateCompletedEventHandler UpdateAppIdCertificateCompleted;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06001C11 RID: 7185 RVA: 0x0007CE58 File Offset: 0x0007B058
		// (remove) Token: 0x06001C12 RID: 7186 RVA: 0x0007CE90 File Offset: 0x0007B090
		public event UpdateAppIdPropertiesCompletedEventHandler UpdateAppIdPropertiesCompleted;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06001C13 RID: 7187 RVA: 0x0007CEC8 File Offset: 0x0007B0C8
		// (remove) Token: 0x06001C14 RID: 7188 RVA: 0x0007CF00 File Offset: 0x0007B100
		public event AddUriCompletedEventHandler AddUriCompleted;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06001C15 RID: 7189 RVA: 0x0007CF38 File Offset: 0x0007B138
		// (remove) Token: 0x06001C16 RID: 7190 RVA: 0x0007CF70 File Offset: 0x0007B170
		public event RemoveUriCompletedEventHandler RemoveUriCompleted;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06001C17 RID: 7191 RVA: 0x0007CFA8 File Offset: 0x0007B1A8
		// (remove) Token: 0x06001C18 RID: 7192 RVA: 0x0007CFE0 File Offset: 0x0007B1E0
		public event ReserveDomainCompletedEventHandler ReserveDomainCompleted;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06001C19 RID: 7193 RVA: 0x0007D018 File Offset: 0x0007B218
		// (remove) Token: 0x06001C1A RID: 7194 RVA: 0x0007D050 File Offset: 0x0007B250
		public event ReleaseDomainCompletedEventHandler ReleaseDomainCompleted;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06001C1B RID: 7195 RVA: 0x0007D088 File Offset: 0x0007B288
		// (remove) Token: 0x06001C1C RID: 7196 RVA: 0x0007D0C0 File Offset: 0x0007B2C0
		public event GetDomainInfoCompletedEventHandler GetDomainInfoCompleted;

		// Token: 0x06001C1D RID: 7197 RVA: 0x0007D0F8 File Offset: 0x0007B2F8
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation/V1.0/CreateAppId", RequestNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public AppIdInfo CreateAppId(string certificate, Property[] properties)
		{
			object[] array = this.Invoke("CreateAppId", new object[]
			{
				certificate,
				properties
			});
			return (AppIdInfo)array[0];
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x0007D12C File Offset: 0x0007B32C
		public IAsyncResult BeginCreateAppId(string certificate, Property[] properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateAppId", new object[]
			{
				certificate,
				properties
			}, callback, asyncState);
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x0007D158 File Offset: 0x0007B358
		public AppIdInfo EndCreateAppId(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AppIdInfo)array[0];
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x0007D175 File Offset: 0x0007B375
		public void CreateAppIdAsync(string certificate, Property[] properties)
		{
			this.CreateAppIdAsync(certificate, properties, null);
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x0007D180 File Offset: 0x0007B380
		public void CreateAppIdAsync(string certificate, Property[] properties, object userState)
		{
			if (this.CreateAppIdOperationCompleted == null)
			{
				this.CreateAppIdOperationCompleted = new SendOrPostCallback(this.OnCreateAppIdOperationCompleted);
			}
			base.InvokeAsync("CreateAppId", new object[]
			{
				certificate,
				properties
			}, this.CreateAppIdOperationCompleted, userState);
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x0007D1CC File Offset: 0x0007B3CC
		private void OnCreateAppIdOperationCompleted(object arg)
		{
			if (this.CreateAppIdCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateAppIdCompleted(this, new CreateAppIdCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x0007D214 File Offset: 0x0007B414
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation/V1.0/UpdateAppIdCertificate", RequestNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHttpClientTraceExtension]
		public void UpdateAppIdCertificate(string appId, string appIdAdminKey, string newCertificate)
		{
			this.Invoke("UpdateAppIdCertificate", new object[]
			{
				appId,
				appIdAdminKey,
				newCertificate
			});
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0007D244 File Offset: 0x0007B444
		public IAsyncResult BeginUpdateAppIdCertificate(string appId, string appIdAdminKey, string newCertificate, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateAppIdCertificate", new object[]
			{
				appId,
				appIdAdminKey,
				newCertificate
			}, callback, asyncState);
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x0007D274 File Offset: 0x0007B474
		public void EndUpdateAppIdCertificate(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0007D27E File Offset: 0x0007B47E
		public void UpdateAppIdCertificateAsync(string appId, string appIdAdminKey, string newCertificate)
		{
			this.UpdateAppIdCertificateAsync(appId, appIdAdminKey, newCertificate, null);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0007D28C File Offset: 0x0007B48C
		public void UpdateAppIdCertificateAsync(string appId, string appIdAdminKey, string newCertificate, object userState)
		{
			if (this.UpdateAppIdCertificateOperationCompleted == null)
			{
				this.UpdateAppIdCertificateOperationCompleted = new SendOrPostCallback(this.OnUpdateAppIdCertificateOperationCompleted);
			}
			base.InvokeAsync("UpdateAppIdCertificate", new object[]
			{
				appId,
				appIdAdminKey,
				newCertificate
			}, this.UpdateAppIdCertificateOperationCompleted, userState);
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0007D2DC File Offset: 0x0007B4DC
		private void OnUpdateAppIdCertificateOperationCompleted(object arg)
		{
			if (this.UpdateAppIdCertificateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateAppIdCertificateCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0007D31C File Offset: 0x0007B51C
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation/V1.0/UpdateAppIdProperties", RequestNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void UpdateAppIdProperties(string appId, Property[] properties)
		{
			this.Invoke("UpdateAppIdProperties", new object[]
			{
				appId,
				properties
			});
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0007D348 File Offset: 0x0007B548
		public IAsyncResult BeginUpdateAppIdProperties(string appId, Property[] properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateAppIdProperties", new object[]
			{
				appId,
				properties
			}, callback, asyncState);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0007D373 File Offset: 0x0007B573
		public void EndUpdateAppIdProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0007D37D File Offset: 0x0007B57D
		public void UpdateAppIdPropertiesAsync(string appId, Property[] properties)
		{
			this.UpdateAppIdPropertiesAsync(appId, properties, null);
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x0007D388 File Offset: 0x0007B588
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

		// Token: 0x06001C2E RID: 7214 RVA: 0x0007D3D4 File Offset: 0x0007B5D4
		private void OnUpdateAppIdPropertiesOperationCompleted(object arg)
		{
			if (this.UpdateAppIdPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateAppIdPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x0007D414 File Offset: 0x0007B614
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation/V1.0/AddUri", RequestNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHttpClientTraceExtension]
		public void AddUri(string ownerAppId, string uri)
		{
			this.Invoke("AddUri", new object[]
			{
				ownerAppId,
				uri
			});
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x0007D440 File Offset: 0x0007B640
		public IAsyncResult BeginAddUri(string ownerAppId, string uri, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddUri", new object[]
			{
				ownerAppId,
				uri
			}, callback, asyncState);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0007D46B File Offset: 0x0007B66B
		public void EndAddUri(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0007D475 File Offset: 0x0007B675
		public void AddUriAsync(string ownerAppId, string uri)
		{
			this.AddUriAsync(ownerAppId, uri, null);
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x0007D480 File Offset: 0x0007B680
		public void AddUriAsync(string ownerAppId, string uri, object userState)
		{
			if (this.AddUriOperationCompleted == null)
			{
				this.AddUriOperationCompleted = new SendOrPostCallback(this.OnAddUriOperationCompleted);
			}
			base.InvokeAsync("AddUri", new object[]
			{
				ownerAppId,
				uri
			}, this.AddUriOperationCompleted, userState);
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x0007D4CC File Offset: 0x0007B6CC
		private void OnAddUriOperationCompleted(object arg)
		{
			if (this.AddUriCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddUriCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x0007D50C File Offset: 0x0007B70C
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation/V1.0/RemoveUri", RequestNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHttpClientTraceExtension]
		public void RemoveUri(string ownerAppId, string uri)
		{
			this.Invoke("RemoveUri", new object[]
			{
				ownerAppId,
				uri
			});
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0007D538 File Offset: 0x0007B738
		public IAsyncResult BeginRemoveUri(string ownerAppId, string uri, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveUri", new object[]
			{
				ownerAppId,
				uri
			}, callback, asyncState);
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0007D563 File Offset: 0x0007B763
		public void EndRemoveUri(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0007D56D File Offset: 0x0007B76D
		public void RemoveUriAsync(string ownerAppId, string uri)
		{
			this.RemoveUriAsync(ownerAppId, uri, null);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0007D578 File Offset: 0x0007B778
		public void RemoveUriAsync(string ownerAppId, string uri, object userState)
		{
			if (this.RemoveUriOperationCompleted == null)
			{
				this.RemoveUriOperationCompleted = new SendOrPostCallback(this.OnRemoveUriOperationCompleted);
			}
			base.InvokeAsync("RemoveUri", new object[]
			{
				ownerAppId,
				uri
			}, this.RemoveUriOperationCompleted, userState);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0007D5C4 File Offset: 0x0007B7C4
		private void OnRemoveUriOperationCompleted(object arg)
		{
			if (this.RemoveUriCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveUriCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0007D604 File Offset: 0x0007B804
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation/V1.0/ReserveDomain", RequestNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHttpClientTraceExtension]
		public void ReserveDomain(string ownerAppId, string domainName, string programId)
		{
			this.Invoke("ReserveDomain", new object[]
			{
				ownerAppId,
				domainName,
				programId
			});
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0007D634 File Offset: 0x0007B834
		public IAsyncResult BeginReserveDomain(string ownerAppId, string domainName, string programId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ReserveDomain", new object[]
			{
				ownerAppId,
				domainName,
				programId
			}, callback, asyncState);
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0007D664 File Offset: 0x0007B864
		public void EndReserveDomain(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x0007D66E File Offset: 0x0007B86E
		public void ReserveDomainAsync(string ownerAppId, string domainName, string programId)
		{
			this.ReserveDomainAsync(ownerAppId, domainName, programId, null);
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0007D67C File Offset: 0x0007B87C
		public void ReserveDomainAsync(string ownerAppId, string domainName, string programId, object userState)
		{
			if (this.ReserveDomainOperationCompleted == null)
			{
				this.ReserveDomainOperationCompleted = new SendOrPostCallback(this.OnReserveDomainOperationCompleted);
			}
			base.InvokeAsync("ReserveDomain", new object[]
			{
				ownerAppId,
				domainName,
				programId
			}, this.ReserveDomainOperationCompleted, userState);
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x0007D6CC File Offset: 0x0007B8CC
		private void OnReserveDomainOperationCompleted(object arg)
		{
			if (this.ReserveDomainCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ReserveDomainCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x0007D70C File Offset: 0x0007B90C
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation/V1.0/ReleaseDomain", RequestNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void ReleaseDomain(string ownerAppId, string domainName)
		{
			this.Invoke("ReleaseDomain", new object[]
			{
				ownerAppId,
				domainName
			});
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x0007D738 File Offset: 0x0007B938
		public IAsyncResult BeginReleaseDomain(string ownerAppId, string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ReleaseDomain", new object[]
			{
				ownerAppId,
				domainName
			}, callback, asyncState);
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x0007D763 File Offset: 0x0007B963
		public void EndReleaseDomain(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x0007D76D File Offset: 0x0007B96D
		public void ReleaseDomainAsync(string ownerAppId, string domainName)
		{
			this.ReleaseDomainAsync(ownerAppId, domainName, null);
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x0007D778 File Offset: 0x0007B978
		public void ReleaseDomainAsync(string ownerAppId, string domainName, object userState)
		{
			if (this.ReleaseDomainOperationCompleted == null)
			{
				this.ReleaseDomainOperationCompleted = new SendOrPostCallback(this.OnReleaseDomainOperationCompleted);
			}
			base.InvokeAsync("ReleaseDomain", new object[]
			{
				ownerAppId,
				domainName
			}, this.ReleaseDomainOperationCompleted, userState);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x0007D7C4 File Offset: 0x0007B9C4
		private void OnReleaseDomainOperationCompleted(object arg)
		{
			if (this.ReleaseDomainCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ReleaseDomainCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0007D804 File Offset: 0x0007BA04
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://domains.live.com/Service/ManageDelegation/V1.0/GetDomainInfo", RequestNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", ResponseNamespace = "http://domains.live.com/Service/ManageDelegation/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public DomainInfo GetDomainInfo(string ownerAppId, string domainName)
		{
			object[] array = this.Invoke("GetDomainInfo", new object[]
			{
				ownerAppId,
				domainName
			});
			return (DomainInfo)array[0];
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x0007D838 File Offset: 0x0007BA38
		public IAsyncResult BeginGetDomainInfo(string ownerAppId, string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDomainInfo", new object[]
			{
				ownerAppId,
				domainName
			}, callback, asyncState);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x0007D864 File Offset: 0x0007BA64
		public DomainInfo EndGetDomainInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DomainInfo)array[0];
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x0007D881 File Offset: 0x0007BA81
		public void GetDomainInfoAsync(string ownerAppId, string domainName)
		{
			this.GetDomainInfoAsync(ownerAppId, domainName, null);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0007D88C File Offset: 0x0007BA8C
		public void GetDomainInfoAsync(string ownerAppId, string domainName, object userState)
		{
			if (this.GetDomainInfoOperationCompleted == null)
			{
				this.GetDomainInfoOperationCompleted = new SendOrPostCallback(this.OnGetDomainInfoOperationCompleted);
			}
			base.InvokeAsync("GetDomainInfo", new object[]
			{
				ownerAppId,
				domainName
			}, this.GetDomainInfoOperationCompleted, userState);
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x0007D8D8 File Offset: 0x0007BAD8
		private void OnGetDomainInfoOperationCompleted(object arg)
		{
			if (this.GetDomainInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDomainInfoCompleted(this, new GetDomainInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0007D91D File Offset: 0x0007BB1D
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x04001829 RID: 6185
		private const string Namespace = "http://domains.live.com/Service/ManageDelegation/V1.0";

		// Token: 0x0400182A RID: 6186
		private static readonly XmlNamespaceDefinition[] predefinedNamespaces = new XmlNamespaceDefinition[]
		{
			new XmlNamespaceDefinition("md", "http://domains.live.com/Service/ManageDelegation/V1.0")
		};

		// Token: 0x0400182B RID: 6187
		private SendOrPostCallback CreateAppIdOperationCompleted;

		// Token: 0x0400182C RID: 6188
		private SendOrPostCallback UpdateAppIdCertificateOperationCompleted;

		// Token: 0x0400182D RID: 6189
		private SendOrPostCallback UpdateAppIdPropertiesOperationCompleted;

		// Token: 0x0400182E RID: 6190
		private SendOrPostCallback AddUriOperationCompleted;

		// Token: 0x0400182F RID: 6191
		private SendOrPostCallback RemoveUriOperationCompleted;

		// Token: 0x04001830 RID: 6192
		private SendOrPostCallback ReserveDomainOperationCompleted;

		// Token: 0x04001831 RID: 6193
		private SendOrPostCallback ReleaseDomainOperationCompleted;

		// Token: 0x04001832 RID: 6194
		private SendOrPostCallback GetDomainInfoOperationCompleted;
	}
}
