using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000253 RID: 595
	internal class DataCenterRedirectionTarget : RedirectionTarget
	{
		// Token: 0x0600119E RID: 4510 RVA: 0x0004D75B File Offset: 0x0004B95B
		public override RedirectionTarget.ResultSet GetForCallAnsweringCall(UMRecipient user, IRoutingContext context)
		{
			this.ValidateUserArgs(user, context);
			return this.GetBackEndBrickRedirectionTarget(user.ADRecipient as ADUser, context);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0004D777 File Offset: 0x0004B977
		public override RedirectionTarget.ResultSet GetForSubscriberAccessCall(UMRecipient user, IRoutingContext context)
		{
			this.ValidateUserArgs(user, context);
			return this.GetBackEndBrickRedirectionTarget(user.ADRecipient as ADUser, context);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0004D794 File Offset: 0x0004B994
		public override RedirectionTarget.ResultSet GetForNonUserSpecificCall(OrganizationId orgId, IRoutingContext context)
		{
			ValidateArgument.NotNull(orgId, "orgId");
			ValidateArgument.NotNull(context, "context");
			ADUser organizationMailboxForRouting = GrammarMailboxFileStore.GetOrganizationMailboxForRouting(orgId);
			if (organizationMailboxForRouting == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "GetForNonUserSpecificCall: failed to find E15 org mailbox", new object[0]);
				return this.HandleNoOrgMailboxForRouting(context);
			}
			return this.GetBackEndBrickRedirectionTarget(organizationMailboxForRouting, context);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0004D880 File Offset: 0x0004BA80
		protected virtual RedirectionTarget.ResultSet GetBackEndBrickRedirectionTarget(ADUser user, IRoutingContext context)
		{
			string text = null;
			try
			{
				using (MailboxServerLocator mbxLocator = this.InvokeWithStopwatch<MailboxServerLocator>("new MailboxServerLocator", () => MailboxServerLocator.Create(user.Database.ObjectGuid, user.PrimarySmtpAddress.Domain, user.Database.PartitionFQDN)))
				{
					try
					{
						IAsyncResult asyncResult = this.InvokeWithStopwatch<IAsyncResult>("MailboxLocator.BeginGetServer", () => mbxLocator.BeginGetServer(null, null));
						BackEndServer backEndServer = this.InvokeWithStopwatch<BackEndServer>("MailboxLocator.EndGetServer", delegate
						{
							asyncResult.AsyncWaitHandle.WaitOne();
							return mbxLocator.EndGetServer(asyncResult);
						});
						CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "GetBackEndBrickRedirectionTarget: MailboxServerLocator returned server {0}", new object[]
						{
							backEndServer.Fqdn
						});
						if (backEndServer.IsE15OrHigher)
						{
							text = backEndServer.Fqdn;
						}
						else
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "GetBackEndBrickRedirectionTarget: legacy server in local forest", new object[0]);
							using (UMRecipient umrecipient = new UMRecipient(user))
							{
								return this.GetLocalForestLegacyRedirectionTarget(umrecipient, context);
							}
						}
					}
					catch (RemoteForestDownLevelServerException)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "GetBackEndBrickRedirectionTarget: legacy server in remote forest", new object[0]);
						return this.GetRemoteForestLegacyRedirectionTarget(context);
					}
				}
			}
			catch (Exception ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "GetBackEndBrickRedirectionTarget: failed with exception {0}", new object[]
				{
					ex
				});
				throw;
			}
			if (text == null)
			{
				throw CallRejectedException.Create(Strings.ErrorLookingUpActiveMailboxServer(user.DistinguishedName, context.CallId), CallEndingReason.ADError, null, new object[0]);
			}
			string text2;
			int port;
			this.GetRoutingInformation(context.TenantGuid, text, context.IsSecuredCall, out text2, out port);
			return new RedirectionTarget.ResultSet(RouterUtils.GetRedirectContactUri(context.RequestUriOfCall, context.RoutingHelper, text2, port, context.IsSecuredCall ? TransportParameter.Tls : TransportParameter.Tcp, context.TenantGuid.ToString()), text2, port);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0004DAF4 File Offset: 0x0004BCF4
		protected virtual RedirectionTarget.ResultSet HandleNoOrgMailboxForRouting(IRoutingContext context)
		{
			throw CallRejectedException.Create(Strings.NoGrammarCapableMailbox(context.TenantGuid.ToString(), context.CallId), CallEndingReason.NoOrgMailboxFound, null, new object[0]);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0004DB34 File Offset: 0x0004BD34
		protected void ValidateUserArgs(UMRecipient recipient, IRoutingContext context)
		{
			ValidateArgument.NotNull(recipient, "recipient");
			ValidateArgument.NotNull(context, "context");
			ADUser aduser = recipient.ADRecipient as ADUser;
			ExAssert.RetailAssert(aduser != null, "Recipient is not ADUser");
			if (aduser.Database == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "ValidateUserArgs: user database property is null", new object[0]);
			}
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0004DB94 File Offset: 0x0004BD94
		protected void GetRoutingInformation(Guid orgId, string serverFqdn, bool isSecuredCall, out string routingFqdn, out int routingPort)
		{
			routingPort = 5063;
			routingFqdn = serverFqdn;
			string text;
			int num;
			if (Utils.RunningInTestMode && this.GetRoutingFqdnAndPort(orgId, serverFqdn, out text, out num))
			{
				routingFqdn = text;
				routingPort = num;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "GetRoutingInformation: routing fqdn is {0}", new object[]
			{
				routingFqdn
			});
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0004DBE8 File Offset: 0x0004BDE8
		protected bool GetRoutingFqdnAndPort(Guid tenantGuid, string serverFqdn, out string routingFqdn, out int routingPort)
		{
			routingFqdn = null;
			routingPort = 0;
			ADTopologyLookup adtopologyLookup = ADTopologyLookup.CreateTenantResourceForestLookup(tenantGuid);
			Server serverFromName = adtopologyLookup.GetServerFromName(serverFqdn);
			if (serverFromName == null || !serverFromName.IsUnifiedMessagingServer)
			{
				throw CallRejectedException.Create(Strings.UMServerNotFoundinAD(serverFqdn), CallEndingReason.ADError, null, new object[0]);
			}
			UMServer umserver = new UMServer(serverFromName);
			if (umserver.ExternalHostFqdn == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "GetRoutingHostFqdnAndPort: ExternalHostFqdn is null", new object[0]);
				return false;
			}
			routingFqdn = umserver.ExternalHostFqdn.ToString();
			routingPort = umserver.SipTlsListeningPort;
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "GetRoutingHostFqdnAndPort: ExternalHostFQDN = '{0}'  port = '{1}'", new object[]
			{
				routingFqdn,
				routingPort
			});
			return true;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0004DC94 File Offset: 0x0004BE94
		protected virtual RedirectionTarget.ResultSet GetLocalForestLegacyRedirectionTarget(UMRecipient recipient, IRoutingContext context)
		{
			throw CallRejectedException.Create(Strings.LegacyMailboxesNotSupported(context.TenantGuid.ToString(), context.CallId), CallEndingReason.UnsupportedRequest, null, new object[0]);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0004DCD4 File Offset: 0x0004BED4
		protected virtual RedirectionTarget.ResultSet GetRemoteForestLegacyRedirectionTarget(IRoutingContext context)
		{
			throw CallRejectedException.Create(Strings.LegacyMailboxesNotSupported(context.TenantGuid.ToString(), context.CallId), CallEndingReason.UnsupportedRequest, null, new object[0]);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0004DD11 File Offset: 0x0004BF11
		protected T InvokeWithStopwatch<T>(string operationName, Func<T> func)
		{
			return this.latencyStopwatch.Invoke<T>(operationName, func);
		}

		// Token: 0x04000BF2 RID: 3058
		private LatencyStopwatch latencyStopwatch = new LatencyStopwatch();
	}
}
