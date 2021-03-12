﻿using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DB2 RID: 3506
	internal sealed class SmtpAddressImpersonationProcessor : ImpersonationProcessorBase
	{
		// Token: 0x0600593A RID: 22842 RVA: 0x001169D3 File Offset: 0x00114BD3
		internal SmtpAddressImpersonationProcessor(string impersonatedSmtpAddress, bool requirePrimarySmtpAddress, AuthZClientInfo impersonatingClientInfo, IIdentity impersonatingIdentity) : base(impersonatingClientInfo, impersonatingIdentity)
		{
			this.impersonatedSmtpAddress = impersonatedSmtpAddress;
			this.requirePrimarySmtpAddress = requirePrimarySmtpAddress;
			if (!SmtpAddress.IsValidSmtpAddress(this.impersonatedSmtpAddress))
			{
				throw this.CreateUserIdentitySearchFailedException(new InvalidSmtpAddressException());
			}
		}

		// Token: 0x0600593B RID: 22843 RVA: 0x00116A08 File Offset: 0x00114C08
		protected override ADRecipientSessionContext CreateADRecipientSessionContext()
		{
			ADRecipientSessionContext result;
			try
			{
				result = ADRecipientSessionContext.CreateFromSmtpAddress(this.impersonatedSmtpAddress);
			}
			catch (ADConfigurationException innerException)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "SmtpAddressImpersonationProcessor.CreateADRecipientSessionContext. Failed to look up user by smtp address {0}", this.impersonatedSmtpAddress);
				throw this.CreateUserIdentitySearchFailedException(innerException);
			}
			return result;
		}

		// Token: 0x0600593C RID: 22844 RVA: 0x00116A54 File Offset: 0x00114C54
		protected override UserIdentity GetImpersonatedUserIdentity(ADRecipientSessionContext adRecipientSessionContext)
		{
			UserIdentity userIdentity = ADIdentityInformationCache.Singleton.GetUserIdentity(this.impersonatedSmtpAddress, adRecipientSessionContext);
			if (this.requirePrimarySmtpAddress)
			{
				SmtpAddress primarySmtpAddress = userIdentity.Recipient.PrimarySmtpAddress;
				if (!string.Equals(this.impersonatedSmtpAddress, userIdentity.Recipient.PrimarySmtpAddress.ToString(), StringComparison.OrdinalIgnoreCase))
				{
					throw new NonPrimarySmtpAddressException(userIdentity.Recipient.PrimarySmtpAddress.ToString());
				}
			}
			return userIdentity;
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x00116ACE File Offset: 0x00114CCE
		protected override ServicePermanentException CreateUserIdentitySearchFailedException(Exception innerException)
		{
			return new NonExistentMailboxException((CoreResources.IDs)4088802584U, this.impersonatedSmtpAddress);
		}

		// Token: 0x04003173 RID: 12659
		private readonly string impersonatedSmtpAddress;

		// Token: 0x04003174 RID: 12660
		private readonly bool requirePrimarySmtpAddress;
	}
}
