using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000189 RID: 393
	internal static class MultiTenantTransport
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00045C90 File Offset: 0x00043E90
		public static bool MultiTenancyEnabled
		{
			get
			{
				return VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled || DatacenterRegistry.IsForefrontForOffice() || DatacenterRegistry.IsPartnerHostedOnly();
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00045CC4 File Offset: 0x00043EC4
		private static AlertingTracer AlertingTracer
		{
			get
			{
				if (MultiTenantTransport.alertingTracer == null)
				{
					MultiTenantTransport.alertingTracer = new AlertingTracer(MultiTenantTransport.Tracer, typeof(MultiTenantTransport).Name);
				}
				return MultiTenantTransport.alertingTracer;
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00045CF0 File Offset: 0x00043EF0
		public static bool ContainsDirectionalityHeader(HeaderList headers)
		{
			Header header = headers.FindFirst("X-MS-Exchange-Organization-MessageDirectionality");
			return header != null;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00045D10 File Offset: 0x00043F10
		public static bool TryParseDirectionality(string directionalityStr, out MailDirectionality directionality)
		{
			directionality = MailDirectionality.Undefined;
			if (!string.IsNullOrEmpty(directionalityStr))
			{
				if (directionalityStr.Equals(MultiTenantTransport.OriginatingStr, StringComparison.OrdinalIgnoreCase))
				{
					directionality = MailDirectionality.Originating;
					MultiTenantTransport.Tracer.TraceDebug(0L, "Parsed Originating directionality");
				}
				else if (directionalityStr.Equals(MultiTenantTransport.IncomingStr, StringComparison.OrdinalIgnoreCase))
				{
					directionality = MailDirectionality.Incoming;
					MultiTenantTransport.Tracer.TraceDebug(0L, "Parsed Incoming directionality");
				}
				else
				{
					MultiTenantTransport.Tracer.TraceError<string>(0L, "Invalid directionality header '{0}'", directionalityStr);
				}
			}
			else
			{
				MultiTenantTransport.Tracer.TraceError(0L, "Directionality header is present but empty");
			}
			return directionality != MailDirectionality.Undefined;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00045DA0 File Offset: 0x00043FA0
		public static MailDirectionality GetDirectionalityFromHeader(TransportMailItem mailItem)
		{
			Header header = mailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-MessageDirectionality");
			MailDirectionality mailDirectionality;
			if (header == null || !MultiTenantTransport.TryParseDirectionality(Util.GetHeaderValue(header), out mailDirectionality) || mailDirectionality == MailDirectionality.Undefined)
			{
				mailDirectionality = (MultilevelAuth.IsInternalMail(mailItem.RootPart.Headers) ? MailDirectionality.Originating : MailDirectionality.Incoming);
			}
			return mailDirectionality;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00045DF0 File Offset: 0x00043FF0
		public static OrganizationId GetOrganizationId(MailItem mailItem)
		{
			return MultiTenantTransport.GetOrganizationIdDelegate(mailItem);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00045DFD File Offset: 0x00043FFD
		public static MailDirectionality GetDirectionality(MailItem mailItem)
		{
			return MultiTenantTransport.GetDirectionalityDelegate(mailItem);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00045E64 File Offset: 0x00044064
		public static ADOperationResult TryGetExternalOrgId(OrganizationId orgId, out Guid externalOrgId)
		{
			externalOrgId = Guid.Empty;
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				if (MultiTenantTransport.MultiTenancyEnabled)
				{
					externalOrgId = MultiTenantTransport.SafeTenantId;
				}
				return ADOperationResult.Success;
			}
			ExchangeConfigurationUnit configUnitPassedToDelegate = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId), 260, "TryGetExternalOrgId", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\transport\\MultiTenantTransport.cs");
				configUnitPassedToDelegate = tenantOrTopologyConfigurationSession.Read<ExchangeConfigurationUnit>(orgId.ConfigurationUnit);
			});
			if (!adoperationResult.Succeeded)
			{
				return adoperationResult;
			}
			if (configUnitPassedToDelegate == null || !Guid.TryParse(configUnitPassedToDelegate.ExternalDirectoryOrganizationId, out externalOrgId))
			{
				return new ADOperationResult(ADOperationErrorCode.PermanentError, null);
			}
			return ADOperationResult.Success;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00045F30 File Offset: 0x00044130
		public static ADOperationResult TryGetOrganizationId(RoutingAddress address, out OrganizationId orgId)
		{
			MultiTenantTransport.<>c__DisplayClass4 CS$<>8__locals1 = new MultiTenantTransport.<>c__DisplayClass4();
			MultiTenantTransport.<>c__DisplayClass4 CS$<>8__locals2 = CS$<>8__locals1;
			OrganizationId localOrgId;
			orgId = (localOrgId = null);
			CS$<>8__locals2.localOrgId = localOrgId;
			if (!MultiTenantTransport.MultiTenancyEnabled)
			{
				orgId = OrganizationId.ForestWideOrgId;
				return ADOperationResult.Success;
			}
			ADOperationResult success;
			try
			{
				SmtpDomain domain = SmtpDomain.GetDomainPart(address);
				if (domain == null)
				{
					orgId = null;
					MultiTenantTransport.TraceAttributionError("Cannot get organization id for address without a domain: '{0}'", new object[]
					{
						address
					});
				}
				else
				{
					ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
					{
						CS$<>8__locals1.localOrgId = ADSessionSettings.FromTenantAcceptedDomain(domain.Domain).GetCurrentOrganizationIdPopulated();
					});
					if (!adoperationResult.Succeeded)
					{
						MultiTenantTransport.TraceAttributionError("Error {0} attributing based on domain {1}", new object[]
						{
							adoperationResult.Exception,
							address
						});
						return adoperationResult;
					}
				}
				orgId = CS$<>8__locals1.localOrgId;
				success = ADOperationResult.Success;
			}
			finally
			{
				if (orgId == null)
				{
					MultiTenantTransport.TraceAttributionError("Attributing to first org since domain lookup failed for {0}", new object[]
					{
						address
					});
					orgId = OrganizationId.ForestWideOrgId;
				}
			}
			return success;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000460D8 File Offset: 0x000442D8
		public static ADOperationResult TryGetOrganizationId(Guid externalOrgId, out OrganizationId orgId, string exoAccountForest = null, string exoTenantContainer = null)
		{
			MultiTenantTransport.<>c__DisplayClassb CS$<>8__locals1 = new MultiTenantTransport.<>c__DisplayClassb();
			CS$<>8__locals1.externalOrgId = externalOrgId;
			CS$<>8__locals1.exoAccountForest = exoAccountForest;
			CS$<>8__locals1.exoTenantContainer = exoTenantContainer;
			ADOperationResult adoperationResult = ADOperationResult.Success;
			orgId = OrganizationId.ForestWideOrgId;
			if (!MultiTenantTransport.MultiTenancyEnabled || CS$<>8__locals1.externalOrgId == MultiTenantTransport.SafeTenantId || CS$<>8__locals1.externalOrgId == Guid.Empty)
			{
				return adoperationResult;
			}
			ADOperationResult result;
			try
			{
				MultiTenantTransport.<>c__DisplayClasse CS$<>8__locals2 = new MultiTenantTransport.<>c__DisplayClasse();
				CS$<>8__locals2.CS$<>8__localsc = CS$<>8__locals1;
				MultiTenantTransport.<>c__DisplayClasse CS$<>8__locals3 = CS$<>8__locals2;
				OrganizationId localOrgId;
				orgId = (localOrgId = null);
				CS$<>8__locals3.localOrgId = localOrgId;
				if (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.UseTenantPartitionToCreateOrganizationId.Enabled && !DatacenterRegistry.IsForefrontForOffice() && !string.IsNullOrEmpty(CS$<>8__locals1.exoAccountForest) && !string.IsNullOrEmpty(CS$<>8__locals1.exoTenantContainer))
				{
					adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
					{
						CS$<>8__locals2.localOrgId = ADSessionSettings.FromTenantForestAndCN(CS$<>8__locals2.CS$<>8__localsc.exoAccountForest, CS$<>8__locals2.CS$<>8__localsc.exoTenantContainer).GetCurrentOrganizationIdPopulated();
					});
					if (adoperationResult.Succeeded && CS$<>8__locals2.localOrgId != null)
					{
						orgId = CS$<>8__locals2.localOrgId;
						return adoperationResult;
					}
					MultiTenantTransport.TraceAttributionError("Error {0} reading org from EXO Account Forest: {1} and EXO Tenant Container: {2}", new object[]
					{
						adoperationResult.Exception,
						CS$<>8__locals1.exoAccountForest ?? "<NULL>",
						CS$<>8__locals1.exoTenantContainer ?? "<NULL>"
					});
				}
				adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					CS$<>8__locals2.localOrgId = ADSessionSettings.FromExternalDirectoryOrganizationId(CS$<>8__locals2.CS$<>8__localsc.externalOrgId).GetCurrentOrganizationIdPopulated();
					if (CS$<>8__locals2.localOrgId == null)
					{
						MultiTenantTransport.TraceAttributionError("ADSessionSettings has null CurrentOrganizationId", new object[0]);
						throw new InvalidOperationException("ADSessionSettings has null CurrentOrganizationId");
					}
				});
				if (!adoperationResult.Succeeded)
				{
					MultiTenantTransport.TraceAttributionError("Error {0} reading org from external org id {1}", new object[]
					{
						adoperationResult.Exception,
						CS$<>8__locals1.externalOrgId
					});
				}
				orgId = CS$<>8__locals2.localOrgId;
				result = adoperationResult;
			}
			finally
			{
				if (orgId == null)
				{
					MultiTenantTransport.TraceAttributionError("Org Id is null for external org id {0}. Attributing to First Org", new object[]
					{
						CS$<>8__locals1.externalOrgId
					});
					orgId = OrganizationId.ForestWideOrgId;
				}
			}
			return result;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x000462C8 File Offset: 0x000444C8
		public static ADOperationResult TryCreateADRecipientCache(TransportMailItem tmi)
		{
			if (!MultiTenantTransport.MultiTenancyEnabled || tmi.ExternalOrganizationId == Guid.Empty || tmi.ExternalOrganizationId == MultiTenantTransport.SafeTenantId)
			{
				MultiTenantTransport.UpdateADRecipientCacheAndOrganizationScope(tmi, OrganizationId.ForestWideOrgId);
				return ADOperationResult.Success;
			}
			if (tmi.ADRecipientCache != null && tmi.ADRecipientCache.OrganizationId != null)
			{
				return ADOperationResult.Success;
			}
			OrganizationId orgId;
			ADOperationResult adoperationResult = MultiTenantTransport.TryGetOrganizationId(tmi.ExternalOrganizationId, out orgId, tmi.ExoAccountForest, tmi.ExoTenantContainer);
			if (!adoperationResult.Succeeded)
			{
				return adoperationResult;
			}
			MultiTenantTransport.UpdateADRecipientCacheAndOrganizationScope(tmi, orgId);
			return ADOperationResult.Success;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00046364 File Offset: 0x00044564
		public static ADOperationResult TryUpdateScopeAndDirectionality(TransportMailItem tmi, MailDirectionality directionality, Guid externalOrgId, string exoAccountForest, string exoTenantContainer)
		{
			OrganizationId orgId;
			ADOperationResult adoperationResult = MultiTenantTransport.TryGetOrganizationId(externalOrgId, out orgId, null, null);
			if (!adoperationResult.Succeeded)
			{
				return adoperationResult;
			}
			tmi.ExternalOrganizationId = externalOrgId;
			tmi.Directionality = directionality;
			tmi.ExoAccountForest = exoAccountForest;
			tmi.ExoTenantContainer = exoTenantContainer;
			MultiTenantTransport.UpdateADRecipientCacheAndOrganizationScope(tmi, orgId);
			return ADOperationResult.Success;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x000463B0 File Offset: 0x000445B0
		public static ADOperationResult TryUpdateScopeAndDirectionalityFromOrgId(TransportMailItem mailItem, MailDirectionality directionality, OrganizationId orgId)
		{
			mailItem.Directionality = directionality;
			if (OrganizationId.ForestWideOrgId.Equals(orgId))
			{
				mailItem.ExternalOrganizationId = MultiTenantTransport.SafeTenantId;
				return ADOperationResult.Success;
			}
			Guid externalOrganizationId;
			ADOperationResult adoperationResult = MultiTenantTransport.TryGetExternalOrgId(orgId, out externalOrganizationId);
			if (adoperationResult.Succeeded)
			{
				mailItem.ExternalOrganizationId = externalOrganizationId;
				return ADOperationResult.Success;
			}
			return adoperationResult;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00046404 File Offset: 0x00044604
		public static ADOperationResult TryAttributeMessageUsingHeaders(TransportMailItem mailItem)
		{
			mailItem.Directionality = MultiTenantTransport.GetDirectionalityFromHeader(mailItem);
			Guid externalOrganizationId;
			if (MultiTenantTransport.TryGetExternalOrganizationIdFromHeader(mailItem, out externalOrganizationId))
			{
				mailItem.ExternalOrganizationId = externalOrganizationId;
				return ADOperationResult.Success;
			}
			return MultiTenantTransport.TryAttributeFromDomain(mailItem);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0004643A File Offset: 0x0004463A
		public static ADOperationResult TryAttributeReplayMessage(TransportMailItem mailItem)
		{
			mailItem.Directionality = MultiTenantTransport.GetDirectionalityFromHeader(mailItem);
			return MultiTenantTransport.TryAttributeFromDomain(mailItem);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0004644E File Offset: 0x0004464E
		public static ADOperationResult TryAttributePickupMessage(TransportMailItem mailItem)
		{
			mailItem.Directionality = MailDirectionality.Incoming;
			return MultiTenantTransport.TryAttributeFromDomain(mailItem);
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00046460 File Offset: 0x00044660
		public static ADOperationResult TryAttributeFromDomain(TransportMailItem mailItem)
		{
			ADOperationResult adoperationResult = ADOperationResult.Success;
			Guid empty = Guid.Empty;
			if (mailItem.Directionality == MailDirectionality.Originating)
			{
				adoperationResult = MultiTenantTransport.TryGetExternalOrganizationIdForSender(mailItem.RootPart.Headers, mailItem.From, out empty);
			}
			else if (mailItem.Recipients != null && mailItem.Recipients.Count > 0)
			{
				adoperationResult = MultiTenantTransport.TryGetExternalOrganizationIdForRecipient(mailItem.Recipients[0], out empty);
			}
			if (!adoperationResult.Succeeded)
			{
				MultiTenantTransport.TraceAttributionError(string.Format("Error {0} attributing from domain. {1}", adoperationResult.Exception, MultiTenantTransport.ToString(mailItem)), new object[0]);
				return adoperationResult;
			}
			mailItem.ExternalOrganizationId = empty;
			return ADOperationResult.Success;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x000464FE File Offset: 0x000446FE
		public static ADOperationResult TryAttributeProxiedClientSubmission(TransportMailItem mailItem)
		{
			mailItem.Directionality = MailDirectionality.Originating;
			return MultiTenantTransport.TryAttributeFromDomain(mailItem);
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00046510 File Offset: 0x00044710
		public static void UpdateADRecipientCacheAndOrganizationScope(TransportMailItem mailItem, OrganizationId orgId)
		{
			if (mailItem.ADRecipientCache != null && mailItem.ADRecipientCache.OrganizationId.Equals(orgId))
			{
				return;
			}
			mailItem.ADRecipientCache = MultiTenantTransport.CreateRecipientCache(orgId);
			MultiTenantTransport.Tracer.TraceDebug(0L, "Created recipient cache for scope '{0}'", new object[]
			{
				MultiTenantTransport.ToTrace(orgId)
			});
			MultiTenantTransport.UpdateOrganizationScope(mailItem);
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00046570 File Offset: 0x00044770
		public static void UpdateOrganizationScope(TransportMailItem mailItem)
		{
			ADRecipientCache<TransportMiniRecipient> adrecipientCache = mailItem.ADRecipientCache;
			if (adrecipientCache == null)
			{
				throw new ArgumentNullException("scopedRecipientCache");
			}
			OrganizationId organizationId = adrecipientCache.OrganizationId;
			if (organizationId == null)
			{
				throw new InvalidOperationException("UpdateScope() called for mail item with null OrganizationId in recipient cache");
			}
			Guid value;
			if (organizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				value = Guid.Empty;
			}
			else
			{
				value = organizationId.ConfigurationUnit.ObjectGuid;
				if (value.Equals(Guid.Empty))
				{
					throw new InvalidOperationException(string.Format("Empty ObjectGuid for config unit '{0}'", organizationId.ConfigurationUnit));
				}
			}
			mailItem.OrganizationScope = new Guid?(value);
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00046600 File Offset: 0x00044800
		public static void TraceAttributionError(string format, params object[] args)
		{
			try
			{
				string stackTrace = Environment.StackTrace;
				string text = ((args == null || args.Length == 0) ? format : string.Format(format, args)) + stackTrace;
				SmtpCommand.EventLogger.LogEvent(TransportEventLogConstants.Tuple_MessageAttributionFailed, string.Empty, new object[]
				{
					text
				});
				MultiTenantTransport.AlertingTracer.TraceError(0, text, new object[0]);
			}
			catch (FormatException)
			{
				MultiTenantTransport.AlertingTracer.TraceError(0, "Error Logging", new object[0]);
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00046694 File Offset: 0x00044894
		public static string ToString(TransportMailItem mailItem)
		{
			if (mailItem == null)
			{
				return "mailItem is <null>";
			}
			string format = "From: {0}, To:{1}, Directionality: {2}, ExternalOrgId: {3}, OrgId: {4}, ";
			object[] array = new object[5];
			array[0] = mailItem.From;
			array[1] = string.Join(",", from r in mailItem.Recipients
			select r.ToString());
			array[2] = mailItem.Directionality;
			array[3] = mailItem.ExternalOrganizationId;
			array[4] = mailItem.OrganizationId;
			return string.Format(format, array);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00046724 File Offset: 0x00044924
		private static object ToTrace(OrganizationId orgId)
		{
			ADObjectId result;
			if (!(orgId == null))
			{
				if ((result = orgId.OrganizationalUnit) == null)
				{
					return "<ForestWideOrgId>";
				}
			}
			else
			{
				result = "<null>";
			}
			return result;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00046744 File Offset: 0x00044944
		private static TransportMailItem GetTransportMailItem(MailItem mailItem)
		{
			ITransportMailItemWrapperFacade transportMailItemWrapperFacade = (ITransportMailItemWrapperFacade)mailItem;
			return (TransportMailItem)transportMailItemWrapperFacade.TransportMailItem;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00046763 File Offset: 0x00044963
		private static ADRecipientCache<TransportMiniRecipient> CreateRecipientCache(OrganizationId orgId)
		{
			return new ADRecipientCache<TransportMiniRecipient>(TransportMiniRecipientSchema.Properties, 0, orgId);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00046774 File Offset: 0x00044974
		private static ADOperationResult TryGetExternalOrganizationIdForSender(HeaderList headers, RoutingAddress p1Sender, out Guid externalOrgId)
		{
			OrganizationId organizationId = null;
			externalOrgId = Guid.Empty;
			RoutingAddress routingAddress;
			ADOperationResult adoperationResult;
			if (Util.TryGetP2Sender(headers, out routingAddress))
			{
				adoperationResult = MultiTenantTransport.TryGetOrganizationId(routingAddress, out organizationId);
				if (!adoperationResult.Succeeded)
				{
					MultiTenantTransport.TraceAttributionError("Error {0} when trying to get OrgId from sender P2 domain {1}", new object[]
					{
						adoperationResult.Exception,
						routingAddress
					});
					return adoperationResult;
				}
			}
			if (organizationId == null && p1Sender.IsValid && p1Sender != RoutingAddress.NullReversePath)
			{
				adoperationResult = MultiTenantTransport.TryGetOrganizationId(p1Sender, out organizationId);
				if (!adoperationResult.Succeeded)
				{
					MultiTenantTransport.TraceAttributionError("Error {0} when trying to get OrgId from sender P1 domain {1}", new object[]
					{
						adoperationResult.Exception,
						p1Sender
					});
					return adoperationResult;
				}
			}
			if (organizationId != null)
			{
				adoperationResult = MultiTenantTransport.TryGetExternalOrgId(organizationId, out externalOrgId);
				if (!adoperationResult.Succeeded)
				{
					MultiTenantTransport.TraceAttributionError("Error {0} when trying to get Ext OrgId from orgId {1}", new object[]
					{
						adoperationResult.Exception,
						organizationId
					});
				}
			}
			else
			{
				organizationId = OrganizationId.ForestWideOrgId;
				externalOrgId = MultiTenantTransport.SafeTenantId;
				adoperationResult = ADOperationResult.Success;
			}
			return adoperationResult;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00046880 File Offset: 0x00044A80
		private static ADOperationResult TryGetExternalOrganizationIdForRecipient(MailRecipient recipient, out Guid externalOrgId)
		{
			OrganizationId orgId;
			ADOperationResult adoperationResult = MultiTenantTransport.TryGetOrganizationId(recipient.Email, out orgId);
			externalOrgId = Guid.Empty;
			if (!adoperationResult.Succeeded)
			{
				MultiTenantTransport.Tracer.TraceError<string, RoutingAddress>(0L, "Error {0} when trying to get OrgId from recipient {1}", adoperationResult.Exception.ToString(), recipient.Email);
				return adoperationResult;
			}
			return MultiTenantTransport.TryGetExternalOrgId(orgId, out externalOrgId);
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000468DC File Offset: 0x00044ADC
		private static bool TryGetExternalOrganizationIdFromHeader(TransportMailItem mailItem, out Guid externalOrganizationId)
		{
			externalOrganizationId = Guid.Empty;
			Header header = mailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-Id");
			return header != null && Guid.TryParse(header.Value, out externalOrganizationId) && externalOrganizationId != Guid.Empty;
		}

		// Token: 0x04000924 RID: 2340
		public static readonly Guid SafeTenantId = new Guid("5afe0b00-7697-4969-b663-5eab37d5f47e");

		// Token: 0x04000925 RID: 2341
		public static readonly string OriginatingStr = MailDirectionality.Originating.ToString();

		// Token: 0x04000926 RID: 2342
		public static readonly string IncomingStr = MailDirectionality.Incoming.ToString();

		// Token: 0x04000927 RID: 2343
		public static Func<MailItem, OrganizationId> GetOrganizationIdDelegate = (MailItem mailItem) => MultiTenantTransport.GetTransportMailItem(mailItem).OrganizationId;

		// Token: 0x04000928 RID: 2344
		public static Func<MailItem, MailDirectionality> GetDirectionalityDelegate = (MailItem mailItem) => MultiTenantTransport.GetTransportMailItem(mailItem).Directionality;

		// Token: 0x04000929 RID: 2345
		private static readonly Trace Tracer = ExTraceGlobals.GeneralTracer;

		// Token: 0x0400092A RID: 2346
		private static AlertingTracer alertingTracer;
	}
}
