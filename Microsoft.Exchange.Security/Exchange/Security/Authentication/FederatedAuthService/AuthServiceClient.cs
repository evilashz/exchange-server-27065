using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Security;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200005A RID: 90
	[GeneratedCode("System.ServiceModel", "3.0.0.0")]
	internal class AuthServiceClient : ClientBase<IAuthService>, IAuthService
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x00017278 File Offset: 0x00015478
		internal void AddRef()
		{
			Interlocked.Increment(ref this.refCount);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00017288 File Offset: 0x00015488
		internal void Release()
		{
			if (Interlocked.Decrement(ref this.refCount) == 0L)
			{
				bool flag = false;
				try
				{
					if (base.State != CommunicationState.Faulted)
					{
						base.Close();
						flag = true;
					}
				}
				catch (TimeoutException ex)
				{
					ExTraceGlobals.AuthenticationTracer.TraceDebug<string>((long)this.GetHashCode(), "AuthServiceClient.Release() times out: {0}", ex.Message);
				}
				catch (CommunicationException ex2)
				{
					ExTraceGlobals.AuthenticationTracer.TraceDebug<string>((long)this.GetHashCode(), "AuthServiceClient.Release() has CommunicationException: {0}", ex2.Message);
				}
				finally
				{
					if (!flag)
					{
						base.Abort();
					}
					((IDisposable)this).Dispose();
				}
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00017334 File Offset: 0x00015534
		internal AuthServiceClient()
		{
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0001733C File Offset: 0x0001553C
		internal AuthServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00017345 File Offset: 0x00015545
		internal AuthServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0001734F File Offset: 0x0001554F
		internal AuthServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00017359 File Offset: 0x00015559
		internal AuthServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00017364 File Offset: 0x00015564
		public IntPtr LogonUserFederationCreds(uint clientProcessId, byte[] remoteUserName, byte[] remotePassword, string remoteOrganizationContext, bool syncLocalAD, string application, string userAgent, string userAddress, out string iisLogMsg)
		{
			return this.LogonUserFederationCreds(clientProcessId, remoteUserName, remotePassword, remoteOrganizationContext, syncLocalAD, application, userAgent, userAddress, Guid.Empty, out iisLogMsg);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001738C File Offset: 0x0001558C
		public IntPtr LogonUserFederationCreds(uint clientProcessId, byte[] remoteUserName, byte[] remotePassword, string remoteOrganizationContext, bool syncLocalAD, string application, string userAgent, string userAddress, Guid requestId, out string iisLogMsg)
		{
			IAsyncResult ar = base.Channel.BeginLogonUserFederationCredsAsync(clientProcessId, remoteUserName, remotePassword, remoteOrganizationContext, syncLocalAD, application, userAgent, userAddress, requestId, null, null);
			return base.Channel.EndLogonUserFederationCredsAsync(out iisLogMsg, ar);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000173C4 File Offset: 0x000155C4
		public AuthStatus LogonCommonAccessTokenFederationCredsTest(uint clientProcessId, byte[] remoteUserName, byte[] remotePassword, AuthOptions options, string remoteOrganizationContext, string userEndpoint, string userAgent, string userAddress, Guid requestId, bool? preferOfflineOrgId, TestFailoverFlags testFailOver, out string commonAccessToken, out string iisLogMsg)
		{
			return base.Channel.LogonCommonAccessTokenFederationCredsTest(clientProcessId, remoteUserName, remotePassword, options, remoteOrganizationContext, userEndpoint, userAgent, userAddress, requestId, preferOfflineOrgId, testFailOver, out commonAccessToken, out iisLogMsg);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000173F4 File Offset: 0x000155F4
		public IAsyncResult BeginLogonUserFederationCredsAsync(uint clientProcessId, byte[] remoteUserName, byte[] remotePassword, string remoteOrganizationContext, bool syncLocalAD, string application, string userAgent, string userAddress, AsyncCallback callback, object asyncState)
		{
			return this.BeginLogonUserFederationCredsAsync(clientProcessId, remoteUserName, remotePassword, remoteOrganizationContext, syncLocalAD, application, userAgent, userAddress, Guid.Empty, callback, asyncState);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00017420 File Offset: 0x00015620
		public IAsyncResult BeginLogonUserFederationCredsAsync(uint clientProcessId, byte[] remoteUserName, byte[] remotePassword, string remoteOrganizationContext, bool syncLocalAD, string application, string userAgent, string userAddress, Guid requestId, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginLogonUserFederationCredsAsync(clientProcessId, remoteUserName, remotePassword, remoteOrganizationContext, syncLocalAD, application, userAgent, userAddress, requestId, callback, asyncState);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0001744B File Offset: 0x0001564B
		public IntPtr EndLogonUserFederationCredsAsync(out string iisLogMsg, IAsyncResult result)
		{
			return base.Channel.EndLogonUserFederationCredsAsync(out iisLogMsg, result);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0001745C File Offset: 0x0001565C
		public AuthStatus LogonCommonAccessTokenFederationCreds(uint clientProcessId, byte[] remoteUserName, byte[] remotePassword, AuthOptions options, string remoteOrganizationContext, string application, string userAgent, string userAddress, out string commonAccessToken, out string iisLogMsg)
		{
			return this.LogonCommonAccessTokenFederationCreds(clientProcessId, remoteUserName, remotePassword, options, remoteOrganizationContext, application, userAgent, userAddress, Guid.Empty, out commonAccessToken, out iisLogMsg);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00017488 File Offset: 0x00015688
		public AuthStatus LogonCommonAccessTokenFederationCreds(uint clientProcessId, byte[] remoteUserName, byte[] remotePassword, AuthOptions options, string remoteOrganizationContext, string application, string userAgent, string userAddress, Guid requestId, out string commonAccessToken, out string iisLogMsg)
		{
			IAsyncResult ar = base.Channel.BeginLogonCommonAccessTokenFederationCredsAsync(clientProcessId, remoteUserName, remotePassword, options, remoteOrganizationContext, application, userAgent, userAddress, requestId, null, null);
			return base.Channel.EndLogonCommonAccessTokenFederationCredsAsync(out commonAccessToken, out iisLogMsg, ar);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000174C4 File Offset: 0x000156C4
		public IAsyncResult BeginLogonCommonAccessTokenFederationCredsAsync(uint clientProcessId, byte[] remoteUserName, byte[] remotePassword, AuthOptions options, string remoteOrganizationContext, string application, string userAgent, string userAddress, AsyncCallback callback, object asyncState)
		{
			return this.BeginLogonCommonAccessTokenFederationCredsAsync(clientProcessId, remoteUserName, remotePassword, options, remoteOrganizationContext, application, userAgent, userAddress, Guid.Empty, callback, asyncState);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000174F0 File Offset: 0x000156F0
		public IAsyncResult BeginLogonCommonAccessTokenFederationCredsAsync(uint clientProcessId, byte[] remoteUserName, byte[] remotePassword, AuthOptions options, string remoteOrganizationContext, string application, string userAgent, string userAddress, Guid requestId, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginLogonCommonAccessTokenFederationCredsAsync(clientProcessId, remoteUserName, remotePassword, options, remoteOrganizationContext, application, userAgent, userAddress, requestId, callback, asyncState);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001751B File Offset: 0x0001571B
		public AuthStatus EndLogonCommonAccessTokenFederationCredsAsync(out string commonAccessToken, out string iisLogMsg, IAsyncResult result)
		{
			return base.Channel.EndLogonCommonAccessTokenFederationCredsAsync(out commonAccessToken, out iisLogMsg, result);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001752B File Offset: 0x0001572B
		public bool IsNego2AuthEnabledForDomain(string domain)
		{
			return base.Channel.IsNego2AuthEnabledForDomain(domain);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00017539 File Offset: 0x00015739
		public IAsyncResult BeginIsNego2AuthEnabledForDomainAsync(string domain, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginIsNego2AuthEnabledForDomainAsync(domain, callback, asyncState);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00017549 File Offset: 0x00015749
		public bool EndIsNego2AuthEnabledForDomainAsync(IAsyncResult result)
		{
			return base.Channel.EndIsNego2AuthEnabledForDomainAsync(result);
		}

		// Token: 0x0400028E RID: 654
		private long refCount;
	}
}
