using System;
using System.ComponentModel;
using System.Security.Principal;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001E1 RID: 481
	public sealed class OwaLiveIDIdentity : OwaClientSecurityContextIdentity
	{
		// Token: 0x06000F67 RID: 3943 RVA: 0x0005F7A4 File Offset: 0x0005D9A4
		private OwaLiveIDIdentity(SecurityIdentifier userSid, bool hasAcceptedAccruals) : base(userSid)
		{
			this.hasAcceptedAccruals = hasAcceptedAccruals;
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0005F7B4 File Offset: 0x0005D9B4
		public bool HasAcceptedAccruals
		{
			get
			{
				return this.hasAcceptedAccruals;
			}
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0005F7BC File Offset: 0x0005D9BC
		public static OwaClientSecurityContextIdentity CreateFromLiveIDIdentity(LiveIDIdentity liveIDIdentity)
		{
			if (liveIDIdentity == null)
			{
				throw new ArgumentNullException("liveIDIdentity");
			}
			OwaLiveIDIdentity owaLiveIDIdentity = new OwaLiveIDIdentity(liveIDIdentity.Sid, liveIDIdentity.HasAcceptedAccruals);
			owaLiveIDIdentity.userOrganizationProperties = liveIDIdentity.UserOrganizationProperties;
			owaLiveIDIdentity.DomainName = SmtpAddress.Parse(liveIDIdentity.MemberName).Domain;
			try
			{
				ClientSecurityContext clientSecurityContext = liveIDIdentity.CreateClientSecurityContext();
				owaLiveIDIdentity.UpgradePartialIdentity(clientSecurityContext, liveIDIdentity.PrincipalName, string.Empty);
			}
			catch (AuthzException ex)
			{
				if (ex.InnerException is Win32Exception)
				{
					OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ErrorCreatingClientContext, string.Empty, new object[]
					{
						owaLiveIDIdentity.UserSid.ToString(),
						ex.ToString()
					});
					throw new OwaCreateClientSecurityContextFailedException("There was a problem creating the Client Security Context.");
				}
				throw;
			}
			return owaLiveIDIdentity;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0005F888 File Offset: 0x0005DA88
		public override void Refresh(OwaIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			base.Refresh(identity);
			OwaLiveIDIdentity owaLiveIDIdentity = identity as OwaLiveIDIdentity;
			if (owaLiveIDIdentity != null)
			{
				this.hasAcceptedAccruals = owaLiveIDIdentity.HasAcceptedAccruals;
			}
		}

		// Token: 0x04000A5E RID: 2654
		private bool hasAcceptedAccruals;
	}
}
