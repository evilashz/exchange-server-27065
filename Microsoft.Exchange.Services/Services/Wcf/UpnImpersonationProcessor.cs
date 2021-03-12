using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DB3 RID: 3507
	internal sealed class UpnImpersonationProcessor : ImpersonationProcessorBase
	{
		// Token: 0x0600593E RID: 22846 RVA: 0x00116AE8 File Offset: 0x00114CE8
		internal UpnImpersonationProcessor(string impersonatedUPN, AuthZClientInfo impersonatingClientInfo, IIdentity impersonatingIdentity) : base(impersonatingClientInfo, impersonatingIdentity)
		{
			this.impersonatedUPN = impersonatedUPN;
			if (string.IsNullOrEmpty(this.impersonatedUPN) || !SmtpAddress.IsValidSmtpAddress(this.impersonatedUPN))
			{
				throw this.CreateUserIdentitySearchFailedException(null);
			}
			ADSessionSettings sessionSettings = Directory.SessionSettingsFromAddress(this.impersonatedUPN);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 54, ".ctor", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\wcf\\HeaderProcessors\\UpnImpersonationProcessor.cs");
			ADRawEntry[] array = tenantOrRootOrgRecipientSession.FindRecipient(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.UserPrincipalName, this.impersonatedUPN), null, 1, UpnImpersonationProcessor.PropertySet);
			if (array.Length == 0)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "UpnImpersonationProcessor.CreateADRecipientSessionContext. Failed to look up user by UPN {0}.", this.impersonatedUPN);
				throw this.CreateUserIdentitySearchFailedException(null);
			}
			this.impersonatedSmtpAddress = ((SmtpAddress)array[0][ADRecipientSchema.PrimarySmtpAddress]).ToString();
			this.impersonatedSid = (SecurityIdentifier)array[0][ADMailboxRecipientSchema.Sid];
			if (string.IsNullOrEmpty(this.impersonatedSmtpAddress) && this.impersonatedSid == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "UpnImpersonationProcessor.CreateADRecipientSessionContext. Failed to look up user by UPN {0}.", this.impersonatedUPN);
				throw this.CreateUserIdentitySearchFailedException(null);
			}
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x00116C08 File Offset: 0x00114E08
		protected override ADRecipientSessionContext CreateADRecipientSessionContext()
		{
			ADRecipientSessionContext result;
			try
			{
				if (!string.IsNullOrEmpty(this.impersonatedSmtpAddress))
				{
					result = ADRecipientSessionContext.CreateFromSmtpAddress(this.impersonatedSmtpAddress);
				}
				else
				{
					result = ADRecipientSessionContext.CreateFromSidInRootOrg(this.impersonatedSid);
				}
			}
			catch (ADConfigurationException innerException)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "UpnImpersonationProcessor.Ctor. Failed to look up user by UPN {0}.", this.impersonatedUPN);
				throw this.CreateUserIdentitySearchFailedException(innerException);
			}
			return result;
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x00116C70 File Offset: 0x00114E70
		protected override UserIdentity GetImpersonatedUserIdentity(ADRecipientSessionContext adRecipientSessionContext)
		{
			if (!string.IsNullOrEmpty(this.impersonatedSmtpAddress))
			{
				return ADIdentityInformationCache.Singleton.GetUserIdentity(this.impersonatedSmtpAddress, adRecipientSessionContext);
			}
			return ADIdentityInformationCache.Singleton.GetUserIdentity(this.impersonatedSid, adRecipientSessionContext);
		}

		// Token: 0x06005941 RID: 22849 RVA: 0x00116CA2 File Offset: 0x00114EA2
		protected override ServicePermanentException CreateUserIdentitySearchFailedException(Exception innerException)
		{
			if (innerException != null)
			{
				return new InvalidUserPrincipalNameException(innerException);
			}
			return new InvalidUserPrincipalNameException();
		}

		// Token: 0x04003175 RID: 12661
		private readonly string impersonatedUPN;

		// Token: 0x04003176 RID: 12662
		private readonly string impersonatedSmtpAddress;

		// Token: 0x04003177 RID: 12663
		private readonly SecurityIdentifier impersonatedSid;

		// Token: 0x04003178 RID: 12664
		private static readonly PropertyDefinition[] PropertySet = new PropertyDefinition[]
		{
			ADRecipientSchema.PrimarySmtpAddress,
			ADMailboxRecipientSchema.Sid
		};
	}
}
