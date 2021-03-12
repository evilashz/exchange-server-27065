using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using Microsoft.com.IPC.WSService;

namespace Microsoft.com.IPC.WSServerLicensingService
{
	// Token: 0x02000A0E RID: 2574
	[GeneratedCode("System.ServiceModel", "3.0.0.0")]
	[DebuggerStepThrough]
	public class WSServerLicensingServiceClient : ClientBase<IWSServerLicensingService>, IWSServerLicensingService
	{
		// Token: 0x06003810 RID: 14352 RVA: 0x0008D6CA File Offset: 0x0008B8CA
		public WSServerLicensingServiceClient()
		{
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x0008D6D2 File Offset: 0x0008B8D2
		public WSServerLicensingServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x0008D6DB File Offset: 0x0008B8DB
		public WSServerLicensingServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x0008D6E5 File Offset: 0x0008B8E5
		public WSServerLicensingServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x0008D6EF File Offset: 0x0008B8EF
		public WSServerLicensingServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x140000BD RID: 189
		// (add) Token: 0x06003815 RID: 14357 RVA: 0x0008D6FC File Offset: 0x0008B8FC
		// (remove) Token: 0x06003816 RID: 14358 RVA: 0x0008D734 File Offset: 0x0008B934
		public event EventHandler<AcquireServerLicenseCompletedEventArgs> AcquireServerLicenseCompleted;

		// Token: 0x06003817 RID: 14359 RVA: 0x0008D769 File Offset: 0x0008B969
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		AcquireServerLicenseResponseMessage IWSServerLicensingService.AcquireServerLicense(AcquireServerLicenseRequestMessage request)
		{
			return base.Channel.AcquireServerLicense(request);
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x0008D778 File Offset: 0x0008B978
		public BatchLicenseResponses AcquireServerLicense(ref VersionData VersionData, XrmlCertificateChain GroupIdentityCredential, XrmlCertificateChain IssuanceLicense, LicenseeIdentity[] LicenseeIdentities)
		{
			AcquireServerLicenseResponseMessage acquireServerLicenseResponseMessage = ((IWSServerLicensingService)this).AcquireServerLicense(new AcquireServerLicenseRequestMessage
			{
				VersionData = VersionData,
				GroupIdentityCredential = GroupIdentityCredential,
				IssuanceLicense = IssuanceLicense,
				LicenseeIdentities = LicenseeIdentities
			});
			VersionData = acquireServerLicenseResponseMessage.VersionData;
			return acquireServerLicenseResponseMessage.AcquireServerLicenseResponses;
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x0008D7BF File Offset: 0x0008B9BF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		IAsyncResult IWSServerLicensingService.BeginAcquireServerLicense(AcquireServerLicenseRequestMessage request, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginAcquireServerLicense(request, callback, asyncState);
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x0008D7D0 File Offset: 0x0008B9D0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginAcquireServerLicense(VersionData VersionData, XrmlCertificateChain GroupIdentityCredential, XrmlCertificateChain IssuanceLicense, LicenseeIdentity[] LicenseeIdentities, AsyncCallback callback, object asyncState)
		{
			return ((IWSServerLicensingService)this).BeginAcquireServerLicense(new AcquireServerLicenseRequestMessage
			{
				VersionData = VersionData,
				GroupIdentityCredential = GroupIdentityCredential,
				IssuanceLicense = IssuanceLicense,
				LicenseeIdentities = LicenseeIdentities
			}, callback, asyncState);
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x0008D80B File Offset: 0x0008BA0B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		AcquireServerLicenseResponseMessage IWSServerLicensingService.EndAcquireServerLicense(IAsyncResult result)
		{
			return base.Channel.EndAcquireServerLicense(result);
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x0008D81C File Offset: 0x0008BA1C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public VersionData EndAcquireServerLicense(IAsyncResult result, out BatchLicenseResponses AcquireServerLicenseResponses)
		{
			AcquireServerLicenseResponseMessage acquireServerLicenseResponseMessage = ((IWSServerLicensingService)this).EndAcquireServerLicense(result);
			AcquireServerLicenseResponses = acquireServerLicenseResponseMessage.AcquireServerLicenseResponses;
			return acquireServerLicenseResponseMessage.VersionData;
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x0008D840 File Offset: 0x0008BA40
		private IAsyncResult OnBeginAcquireServerLicense(object[] inValues, AsyncCallback callback, object asyncState)
		{
			VersionData versionData = (VersionData)inValues[0];
			XrmlCertificateChain groupIdentityCredential = (XrmlCertificateChain)inValues[1];
			XrmlCertificateChain issuanceLicense = (XrmlCertificateChain)inValues[2];
			LicenseeIdentity[] licenseeIdentities = (LicenseeIdentity[])inValues[3];
			return this.BeginAcquireServerLicense(versionData, groupIdentityCredential, issuanceLicense, licenseeIdentities, callback, asyncState);
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x0008D880 File Offset: 0x0008BA80
		private object[] OnEndAcquireServerLicense(IAsyncResult result)
		{
			BatchLicenseResponses defaultValueForInitialization = base.GetDefaultValueForInitialization<BatchLicenseResponses>();
			VersionData versionData = this.EndAcquireServerLicense(result, out defaultValueForInitialization);
			return new object[]
			{
				defaultValueForInitialization,
				versionData
			};
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x0008D8B0 File Offset: 0x0008BAB0
		private void OnAcquireServerLicenseCompleted(object state)
		{
			if (this.AcquireServerLicenseCompleted != null)
			{
				ClientBase<IWSServerLicensingService>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IWSServerLicensingService>.InvokeAsyncCompletedEventArgs)state;
				this.AcquireServerLicenseCompleted(this, new AcquireServerLicenseCompletedEventArgs(invokeAsyncCompletedEventArgs.Results, invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x0008D8F5 File Offset: 0x0008BAF5
		public void AcquireServerLicenseAsync(VersionData VersionData, XrmlCertificateChain GroupIdentityCredential, XrmlCertificateChain IssuanceLicense, LicenseeIdentity[] LicenseeIdentities)
		{
			this.AcquireServerLicenseAsync(VersionData, GroupIdentityCredential, IssuanceLicense, LicenseeIdentities, null);
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x0008D904 File Offset: 0x0008BB04
		public void AcquireServerLicenseAsync(VersionData VersionData, XrmlCertificateChain GroupIdentityCredential, XrmlCertificateChain IssuanceLicense, LicenseeIdentity[] LicenseeIdentities, object userState)
		{
			if (this.onBeginAcquireServerLicenseDelegate == null)
			{
				this.onBeginAcquireServerLicenseDelegate = new ClientBase<IWSServerLicensingService>.BeginOperationDelegate(this.OnBeginAcquireServerLicense);
			}
			if (this.onEndAcquireServerLicenseDelegate == null)
			{
				this.onEndAcquireServerLicenseDelegate = new ClientBase<IWSServerLicensingService>.EndOperationDelegate(this.OnEndAcquireServerLicense);
			}
			if (this.onAcquireServerLicenseCompletedDelegate == null)
			{
				this.onAcquireServerLicenseCompletedDelegate = new SendOrPostCallback(this.OnAcquireServerLicenseCompleted);
			}
			base.InvokeAsync(this.onBeginAcquireServerLicenseDelegate, new object[]
			{
				VersionData,
				GroupIdentityCredential,
				IssuanceLicense,
				LicenseeIdentities
			}, this.onEndAcquireServerLicenseDelegate, this.onAcquireServerLicenseCompletedDelegate, userState);
		}

		// Token: 0x04002F72 RID: 12146
		private ClientBase<IWSServerLicensingService>.BeginOperationDelegate onBeginAcquireServerLicenseDelegate;

		// Token: 0x04002F73 RID: 12147
		private ClientBase<IWSServerLicensingService>.EndOperationDelegate onEndAcquireServerLicenseDelegate;

		// Token: 0x04002F74 RID: 12148
		private SendOrPostCallback onAcquireServerLicenseCompletedDelegate;
	}
}
