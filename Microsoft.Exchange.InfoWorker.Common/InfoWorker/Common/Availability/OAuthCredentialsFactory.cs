using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000B7 RID: 183
	internal static class OAuthCredentialsFactory
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x000118A0 File Offset: 0x0000FAA0
		public static OAuthCredentials CreateAsApp(InternalClientContext clientContext, RequestLogger requestLogger)
		{
			ArgumentValidator.ThrowIfNull("clientContext", clientContext);
			OrganizationId organizationId = clientContext.OrganizationId;
			string domain = clientContext.ADUser.PrimarySmtpAddress.Domain;
			string text = FaultInjection.TraceTest<string>((FaultInjection.LIDs)2743479613U);
			if (!string.IsNullOrEmpty(text))
			{
				domain = SmtpAddress.Parse(text).Domain;
				organizationId = OrganizationId.FromAcceptedDomain(domain);
			}
			OAuthCredentials oauthCredentialsForAppToken = OAuthCredentials.GetOAuthCredentialsForAppToken(organizationId, domain);
			OAuthCredentialsFactory.SetCredentialsProperties(oauthCredentialsForAppToken, clientContext, requestLogger);
			return oauthCredentialsForAppToken;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00011914 File Offset: 0x0000FB14
		public static OAuthCredentials Create(InternalClientContext clientContext, RequestLogger requestLogger)
		{
			ArgumentValidator.ThrowIfNull("clientContext", clientContext);
			OrganizationId organizationId = clientContext.OrganizationId;
			ADUser aduser = clientContext.ADUser;
			string text = FaultInjection.TraceTest<string>((FaultInjection.LIDs)2743479613U);
			if (!string.IsNullOrEmpty(text))
			{
				SmtpAddress smtpAddress = SmtpAddress.Parse(text);
				IRecipientSession recipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromTenantAcceptedDomain(smtpAddress.Domain), 68, "Create", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\RequestDispatch\\OAuthCredentialsFactory.cs");
				aduser = (recipientSession.FindByProxyAddress(ProxyAddress.Parse(text)) as ADUser);
				organizationId = aduser.OrganizationId;
			}
			OAuthCredentials oauthCredentialsForAppActAsToken = OAuthCredentials.GetOAuthCredentialsForAppActAsToken(organizationId, aduser, null);
			OAuthCredentialsFactory.SetCredentialsProperties(oauthCredentialsForAppActAsToken, clientContext, requestLogger);
			return oauthCredentialsForAppActAsToken;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000119AC File Offset: 0x0000FBAC
		private static void SetCredentialsProperties(OAuthCredentials creds, InternalClientContext clientContext, RequestLogger requestLogger)
		{
			creds.Tracer = new OAuthCredentialsFactory.OAuthOutboundTracer(requestLogger);
			string messageId = clientContext.MessageId;
			Guid value;
			if (messageId.StartsWith(OAuthCredentialsFactory.messagePrefix) && messageId.Length > OAuthCredentialsFactory.messagePrefixLength && Guid.TryParse(messageId.Substring(OAuthCredentialsFactory.messagePrefixLength), out value))
			{
				creds.ClientRequestId = new Guid?(value);
			}
		}

		// Token: 0x04000294 RID: 660
		private static string messagePrefix = "urn:uuid:";

		// Token: 0x04000295 RID: 661
		private static int messagePrefixLength = OAuthCredentialsFactory.messagePrefix.Length;

		// Token: 0x020000B8 RID: 184
		public sealed class OAuthOutboundTracer : DefaultOutboundTracer
		{
			// Token: 0x06000428 RID: 1064 RVA: 0x00011A21 File Offset: 0x0000FC21
			public OAuthOutboundTracer(RequestLogger requestLogger)
			{
				this.logger = requestLogger;
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x00011A30 File Offset: 0x0000FC30
			protected override void LogWarning2(int hashCode, string formatString, params object[] args)
			{
				this.logger.AppendToLog<string>("OAuth", string.Format(formatString, args));
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x00011A49 File Offset: 0x0000FC49
			protected override void LogError2(int hashCode, string formatString, params object[] args)
			{
				this.logger.AppendToLog<string>("OAuth", string.Format(formatString, args));
			}

			// Token: 0x04000296 RID: 662
			private readonly RequestLogger logger;
		}
	}
}
