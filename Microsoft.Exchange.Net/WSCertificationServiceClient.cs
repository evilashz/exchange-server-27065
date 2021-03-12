using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using Microsoft.com.IPC.WSService;

// Token: 0x02000A03 RID: 2563
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "3.0.0.0")]
public class WSCertificationServiceClient : ClientBase<IWSCertificationService>, IWSCertificationService
{
	// Token: 0x060037D9 RID: 14297 RVA: 0x0008D2BE File Offset: 0x0008B4BE
	public WSCertificationServiceClient()
	{
	}

	// Token: 0x060037DA RID: 14298 RVA: 0x0008D2C6 File Offset: 0x0008B4C6
	public WSCertificationServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
	{
	}

	// Token: 0x060037DB RID: 14299 RVA: 0x0008D2CF File Offset: 0x0008B4CF
	public WSCertificationServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x060037DC RID: 14300 RVA: 0x0008D2D9 File Offset: 0x0008B4D9
	public WSCertificationServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x060037DD RID: 14301 RVA: 0x0008D2E3 File Offset: 0x0008B4E3
	public WSCertificationServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
	{
	}

	// Token: 0x140000BC RID: 188
	// (add) Token: 0x060037DE RID: 14302 RVA: 0x0008D2F0 File Offset: 0x0008B4F0
	// (remove) Token: 0x060037DF RID: 14303 RVA: 0x0008D328 File Offset: 0x0008B528
	public event EventHandler<CertifyCompletedEventArgs> CertifyCompleted;

	// Token: 0x060037E0 RID: 14304 RVA: 0x0008D35D File Offset: 0x0008B55D
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	CertifyResponseMessage IWSCertificationService.Certify(CertifyRequestMessage request)
	{
		return base.Channel.Certify(request);
	}

	// Token: 0x060037E1 RID: 14305 RVA: 0x0008D36C File Offset: 0x0008B56C
	public XrmlCertificateChain Certify(ref VersionData VersionData, XrmlCertificateChain MachineCertificate)
	{
		CertifyResponseMessage certifyResponseMessage = ((IWSCertificationService)this).Certify(new CertifyRequestMessage
		{
			VersionData = VersionData,
			MachineCertificate = MachineCertificate
		});
		VersionData = certifyResponseMessage.VersionData;
		return certifyResponseMessage.GroupIdentityCredential;
	}

	// Token: 0x060037E2 RID: 14306 RVA: 0x0008D3A4 File Offset: 0x0008B5A4
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	IAsyncResult IWSCertificationService.BeginCertify(CertifyRequestMessage request, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginCertify(request, callback, asyncState);
	}

	// Token: 0x060037E3 RID: 14307 RVA: 0x0008D3B4 File Offset: 0x0008B5B4
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public IAsyncResult BeginCertify(VersionData VersionData, XrmlCertificateChain MachineCertificate, AsyncCallback callback, object asyncState)
	{
		return ((IWSCertificationService)this).BeginCertify(new CertifyRequestMessage
		{
			VersionData = VersionData,
			MachineCertificate = MachineCertificate
		}, callback, asyncState);
	}

	// Token: 0x060037E4 RID: 14308 RVA: 0x0008D3DF File Offset: 0x0008B5DF
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	CertifyResponseMessage IWSCertificationService.EndCertify(IAsyncResult result)
	{
		return base.Channel.EndCertify(result);
	}

	// Token: 0x060037E5 RID: 14309 RVA: 0x0008D3F0 File Offset: 0x0008B5F0
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public VersionData EndCertify(IAsyncResult result, out XrmlCertificateChain GroupIdentityCredential)
	{
		CertifyResponseMessage certifyResponseMessage = ((IWSCertificationService)this).EndCertify(result);
		GroupIdentityCredential = certifyResponseMessage.GroupIdentityCredential;
		return certifyResponseMessage.VersionData;
	}

	// Token: 0x060037E6 RID: 14310 RVA: 0x0008D414 File Offset: 0x0008B614
	private IAsyncResult OnBeginCertify(object[] inValues, AsyncCallback callback, object asyncState)
	{
		VersionData versionData = (VersionData)inValues[0];
		XrmlCertificateChain machineCertificate = (XrmlCertificateChain)inValues[1];
		return this.BeginCertify(versionData, machineCertificate, callback, asyncState);
	}

	// Token: 0x060037E7 RID: 14311 RVA: 0x0008D440 File Offset: 0x0008B640
	private object[] OnEndCertify(IAsyncResult result)
	{
		XrmlCertificateChain defaultValueForInitialization = base.GetDefaultValueForInitialization<XrmlCertificateChain>();
		VersionData versionData = this.EndCertify(result, out defaultValueForInitialization);
		return new object[]
		{
			defaultValueForInitialization,
			versionData
		};
	}

	// Token: 0x060037E8 RID: 14312 RVA: 0x0008D470 File Offset: 0x0008B670
	private void OnCertifyCompleted(object state)
	{
		if (this.CertifyCompleted != null)
		{
			ClientBase<IWSCertificationService>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IWSCertificationService>.InvokeAsyncCompletedEventArgs)state;
			this.CertifyCompleted(this, new CertifyCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
		}
	}

	// Token: 0x060037E9 RID: 14313 RVA: 0x0008D4B5 File Offset: 0x0008B6B5
	public void CertifyAsync(VersionData VersionData, XrmlCertificateChain MachineCertificate)
	{
		this.CertifyAsync(VersionData, MachineCertificate, null);
	}

	// Token: 0x060037EA RID: 14314 RVA: 0x0008D4C0 File Offset: 0x0008B6C0
	public void CertifyAsync(VersionData VersionData, XrmlCertificateChain MachineCertificate, object userState)
	{
		if (this.onBeginCertifyDelegate == null)
		{
			this.onBeginCertifyDelegate = new ClientBase<IWSCertificationService>.BeginOperationDelegate(this.OnBeginCertify);
		}
		if (this.onEndCertifyDelegate == null)
		{
			this.onEndCertifyDelegate = new ClientBase<IWSCertificationService>.EndOperationDelegate(this.OnEndCertify);
		}
		if (this.onCertifyCompletedDelegate == null)
		{
			this.onCertifyCompletedDelegate = new SendOrPostCallback(this.OnCertifyCompleted);
		}
		base.InvokeAsync(this.onBeginCertifyDelegate, new object[]
		{
			VersionData,
			MachineCertificate
		}, this.onEndCertifyDelegate, this.onCertifyCompletedDelegate, userState);
	}

	// Token: 0x04002F59 RID: 12121
	private ClientBase<IWSCertificationService>.BeginOperationDelegate onBeginCertifyDelegate;

	// Token: 0x04002F5A RID: 12122
	private ClientBase<IWSCertificationService>.EndOperationDelegate onEndCertifyDelegate;

	// Token: 0x04002F5B RID: 12123
	private SendOrPostCallback onCertifyCompletedDelegate;
}
