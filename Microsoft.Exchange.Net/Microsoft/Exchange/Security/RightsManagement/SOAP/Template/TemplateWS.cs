using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Template
{
	// Token: 0x020009F3 RID: 2547
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[WebServiceBinding(Name = "TemplateDistributionWebServiceSoap", Namespace = "http://microsoft.com/DRM/TemplateDistributionService")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	internal class TemplateWS : WsAsyncProxyWrapper
	{
		// Token: 0x06003786 RID: 14214 RVA: 0x0008CDBB File Offset: 0x0008AFBB
		public TemplateWS()
		{
			base.Url = "http://localhost/_wmcs/licensing/TemplateDistribution.asmx";
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06003787 RID: 14215 RVA: 0x0008CDCE File Offset: 0x0008AFCE
		// (set) Token: 0x06003788 RID: 14216 RVA: 0x0008CDD6 File Offset: 0x0008AFD6
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

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x06003789 RID: 14217 RVA: 0x0008CDE0 File Offset: 0x0008AFE0
		// (remove) Token: 0x0600378A RID: 14218 RVA: 0x0008CE18 File Offset: 0x0008B018
		public event AcquireTemplateInformationCompletedEventHandler AcquireTemplateInformationCompleted;

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x0600378B RID: 14219 RVA: 0x0008CE50 File Offset: 0x0008B050
		// (remove) Token: 0x0600378C RID: 14220 RVA: 0x0008CE88 File Offset: 0x0008B088
		public event AcquireTemplatesCompletedEventHandler AcquireTemplatesCompleted;

		// Token: 0x0600378D RID: 14221 RVA: 0x0008CEC0 File Offset: 0x0008B0C0
		[SoapDocumentMethod("http://microsoft.com/DRM/TemplateDistributionService/AcquireTemplateInformation", RequestNamespace = "http://microsoft.com/DRM/TemplateDistributionService", ResponseNamespace = "http://microsoft.com/DRM/TemplateDistributionService", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("VersionDataValue", Direction = SoapHeaderDirection.InOut)]
		public TemplateInformation AcquireTemplateInformation()
		{
			object[] array = base.Invoke("AcquireTemplateInformation", new object[0]);
			return (TemplateInformation)array[0];
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x0008CEE7 File Offset: 0x0008B0E7
		public IAsyncResult BeginAcquireTemplateInformation(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AcquireTemplateInformation", new object[0], callback, asyncState);
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x0008CEFC File Offset: 0x0008B0FC
		public TemplateInformation EndAcquireTemplateInformation(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (TemplateInformation)array[0];
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x0008CF19 File Offset: 0x0008B119
		public void AcquireTemplateInformationAsync()
		{
			this.AcquireTemplateInformationAsync(null);
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x0008CF22 File Offset: 0x0008B122
		public void AcquireTemplateInformationAsync(object userState)
		{
			if (this.AcquireTemplateInformationOperationCompleted == null)
			{
				this.AcquireTemplateInformationOperationCompleted = new SendOrPostCallback(this.OnAcquireTemplateInformationOperationCompleted);
			}
			base.InvokeAsync("AcquireTemplateInformation", new object[0], this.AcquireTemplateInformationOperationCompleted, userState);
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x0008CF58 File Offset: 0x0008B158
		private void OnAcquireTemplateInformationOperationCompleted(object arg)
		{
			if (this.AcquireTemplateInformationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AcquireTemplateInformationCompleted(this, new AcquireTemplateInformationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x0008CFA0 File Offset: 0x0008B1A0
		[SoapHeader("VersionDataValue", Direction = SoapHeaderDirection.InOut)]
		[SoapDocumentMethod("http://microsoft.com/DRM/TemplateDistributionService/AcquireTemplates", RequestNamespace = "http://microsoft.com/DRM/TemplateDistributionService", ResponseNamespace = "http://microsoft.com/DRM/TemplateDistributionService", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public GuidTemplate[] AcquireTemplates(string[] guids)
		{
			object[] array = base.Invoke("AcquireTemplates", new object[]
			{
				guids
			});
			return (GuidTemplate[])array[0];
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x0008CFD0 File Offset: 0x0008B1D0
		public IAsyncResult BeginAcquireTemplates(string[] guids, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AcquireTemplates", new object[]
			{
				guids
			}, callback, asyncState);
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x0008CFF8 File Offset: 0x0008B1F8
		public GuidTemplate[] EndAcquireTemplates(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GuidTemplate[])array[0];
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x0008D015 File Offset: 0x0008B215
		public void AcquireTemplatesAsync(string[] guids)
		{
			this.AcquireTemplatesAsync(guids, null);
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x0008D020 File Offset: 0x0008B220
		public void AcquireTemplatesAsync(string[] guids, object userState)
		{
			if (this.AcquireTemplatesOperationCompleted == null)
			{
				this.AcquireTemplatesOperationCompleted = new SendOrPostCallback(this.OnAcquireTemplatesOperationCompleted);
			}
			base.InvokeAsync("AcquireTemplates", new object[]
			{
				guids
			}, this.AcquireTemplatesOperationCompleted, userState);
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x0008D068 File Offset: 0x0008B268
		private void OnAcquireTemplatesOperationCompleted(object arg)
		{
			if (this.AcquireTemplatesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AcquireTemplatesCompleted(this, new AcquireTemplatesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x0008D0AD File Offset: 0x0008B2AD
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x04002F28 RID: 12072
		private VersionData versionDataValueField;

		// Token: 0x04002F29 RID: 12073
		private SendOrPostCallback AcquireTemplateInformationOperationCompleted;

		// Token: 0x04002F2A RID: 12074
		private SendOrPostCallback AcquireTemplatesOperationCompleted;
	}
}
