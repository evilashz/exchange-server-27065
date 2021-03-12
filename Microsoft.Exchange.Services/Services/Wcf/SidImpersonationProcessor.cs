using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DB1 RID: 3505
	internal sealed class SidImpersonationProcessor : ImpersonationProcessorBase
	{
		// Token: 0x06005936 RID: 22838 RVA: 0x0011690C File Offset: 0x00114B0C
		internal SidImpersonationProcessor(string impersonatedSidString, AuthZClientInfo impersonatingClientInfo, IIdentity impersonatingIdentity) : base(impersonatingClientInfo, impersonatingIdentity)
		{
			this.impersonatedSidString = impersonatedSidString;
			try
			{
				this.impersonatedSid = ADIdentityInformationCache.CreateSid(this.impersonatedSidString);
			}
			catch (InvalidSidException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, InvalidSidException>(0L, "SidImpersonationProcessor.Ctor. InvalidSidException encountered.  Sid: {0}, Exception {1}", this.impersonatedSidString, ex);
				throw this.CreateUserIdentitySearchFailedException(ex);
			}
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x0011696C File Offset: 0x00114B6C
		protected override ADRecipientSessionContext CreateADRecipientSessionContext()
		{
			ADRecipientSessionContext result;
			try
			{
				result = ADRecipientSessionContext.CreateFromSidInRootOrg(this.impersonatedSid);
			}
			catch (ADConfigurationException innerException)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "SidImpersonationProcessor.CreateADRecipientSessionContext. Failed to look up user by SID {0}", this.impersonatedSidString);
				throw this.CreateUserIdentitySearchFailedException(innerException);
			}
			return result;
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x001169B8 File Offset: 0x00114BB8
		protected override UserIdentity GetImpersonatedUserIdentity(ADRecipientSessionContext adRecipientSessionContext)
		{
			return ADIdentityInformationCache.Singleton.GetUserIdentity(this.impersonatedSid, adRecipientSessionContext);
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x001169CB File Offset: 0x00114BCB
		protected override ServicePermanentException CreateUserIdentitySearchFailedException(Exception innerException)
		{
			return new InvalidUserSidException(innerException);
		}

		// Token: 0x04003171 RID: 12657
		private readonly string impersonatedSidString;

		// Token: 0x04003172 RID: 12658
		private SecurityIdentifier impersonatedSid;
	}
}
