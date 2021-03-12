using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.GroupMailbox.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200008B RID: 139
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GroupMailboxCollectionBuilder : IGroupMailboxCollectionBuilder
	{
		// Token: 0x0600036F RID: 879 RVA: 0x00010D6C File Offset: 0x0000EF6C
		public GroupMailboxCollectionBuilder(IRecipientSession adSession, IGroupsLogger logger)
		{
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			ArgumentValidator.ThrowIfNull("logger", logger);
			this.adSession = adSession;
			this.logger = logger;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00010D98 File Offset: 0x0000EF98
		private IRecipientSession AdSessionToPreferredDc
		{
			get
			{
				ADServerInfo adserverInfo;
				if (this.tenantPreferredAdSession == null && GroupMailboxAccessLayerHelper.GetDomainControllerAffinityForOrganization(this.adSession.SessionSettings.CurrentOrganizationId, out adserverInfo))
				{
					this.tenantPreferredAdSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(adserverInfo.Fqdn, true, this.adSession.ConsistencyMode, this.adSession.SessionSettings, 90, "AdSessionToPreferredDc", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\Groups\\GroupMailboxCollectionBuilder.cs");
				}
				return this.tenantPreferredAdSession;
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00010FC0 File Offset: 0x0000F1C0
		public List<GroupMailbox> BuildGroupMailboxes(string[] externalIds)
		{
			List<GroupMailbox> groups = new List<GroupMailbox>(externalIds.Length);
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					if (GroupMailboxCollectionBuilder.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						GroupMailboxCollectionBuilder.Tracer.TraceDebug<string>((long)this.GetHashCode(), "GroupMailboxCollectionBuilder.BuildGroupMailboxes - Resolving Group ADUser object for ExternalIds={0}", string.Join(",", externalIds));
					}
					ITenantRecipientSession tenantAdSession = this.adSession as ITenantRecipientSession;
					if (tenantAdSession == null)
					{
						return;
					}
					Result<ADRawEntry>[] array = this.ExecuteAdQueryAndHandleAdExceptions<Result<ADRawEntry>[]>(() => tenantAdSession.FindByExternalDirectoryObjectIds(externalIds, GroupMailboxCollectionBuilder.GroupPropertiesToRead));
					for (int i = 0; i < array.Length; i++)
					{
						Result<ADRawEntry> result = array[i];
						if (result.Error != null || result.Data == null)
						{
							GroupMailbox item;
							if (this.TryResolveGroupFromTenantPreferredDc(externalIds[i], out item))
							{
								GroupMailboxCollectionBuilder.Tracer.TraceDebug<string>((long)this.GetHashCode(), "GroupMailboxCollectionBuilder.BuildGroupMailboxes - Found AAD Group in EXODS using tenant preferred DC. ExternalDirectoryObjectId={0}", externalIds[i]);
								groups.Add(item);
							}
							else
							{
								this.logger.LogTrace("GroupMailboxCollectionBuilder.BuildGroupMailboxes - Unable to find AAD Group in EXODS using tenant preferred DC. ExternalDirectoryObjectId={0}. Error={1}", new object[]
								{
									externalIds[i],
									result.Error
								});
							}
						}
						else
						{
							GroupMailboxCollectionBuilder.Tracer.TraceDebug<string>((long)this.GetHashCode(), "GroupMailboxCollectionBuilder.BuildGroupMailboxes - Found AAD Group in EXODS using batch AD query. ExternalDirectoryObjectId={0}", externalIds[i]);
							groups.Add(this.BuildGroupMailbox(result.Data));
						}
					}
				}, (Exception e) => GrayException.IsSystemGrayException(e));
			}
			catch (GrayException exception)
			{
				this.logger.LogException(exception, "GroupMailboxCollectionBuilder.BuildGroupMailboxes - Error reading groups from AD.", new object[0]);
			}
			catch (LocalizedException exception2)
			{
				this.logger.LogException(exception2, "GroupMailboxCollectionBuilder.BuildGroupMailboxes - Error reading groups from AD.", new object[0]);
			}
			return groups;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00011084 File Offset: 0x0000F284
		private T ExecuteAdQueryAndHandleAdExceptions<T>(Func<T> query)
		{
			T result = default(T);
			Exception ex = null;
			try
			{
				result = query();
			}
			catch (ADTransientException ex2)
			{
				ex = ex2;
			}
			catch (ADExternalException ex3)
			{
				ex = ex3;
			}
			catch (ADOperationException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				this.logger.LogException(ex, "Caught exception while querying AD.", new object[0]);
			}
			return result;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00011128 File Offset: 0x0000F328
		private bool TryResolveGroupFromTenantPreferredDc(string externalId, out GroupMailbox groupMailbox)
		{
			groupMailbox = null;
			IRecipientSession preferredDcSession = this.AdSessionToPreferredDc;
			if (preferredDcSession == null)
			{
				return false;
			}
			if (this.adSession.LastUsedDc == preferredDcSession.DomainController)
			{
				return false;
			}
			ADUser group = this.ExecuteAdQueryAndHandleAdExceptions<ADUser>(() => preferredDcSession.FindADUserByExternalDirectoryObjectId(externalId));
			if (group == null)
			{
				return false;
			}
			this.ExecuteAdQueryAndHandleAdExceptions<bool>(delegate
			{
				this.EnsureGroupIsCached(group);
				return true;
			});
			groupMailbox = this.BuildGroupMailbox(group);
			return true;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000111CC File Offset: 0x0000F3CC
		private void EnsureGroupIsCached(ADUser group)
		{
			ProxyAddress proxyAddress = new SmtpProxyAddress(group.PrimarySmtpAddress.ToString(), true);
			ADUser aduser = this.AdSessionToPreferredDc.FindByProxyAddress(proxyAddress) as ADUser;
			OWAMiniRecipient owaminiRecipient = this.AdSessionToPreferredDc.FindMiniRecipientByProxyAddress<OWAMiniRecipient>(proxyAddress, OWAMiniRecipientSchema.AdditionalProperties);
			this.logger.LogTrace("Queried AD for group. ExternalId={0}, ProxyAddress={1}, DomainController={2}, FoundADUser={3}, FoundOwaMiniRecipient={4}", new object[]
			{
				group.ExternalDirectoryObjectId,
				proxyAddress,
				group.OriginatingServer,
				aduser != null,
				owaminiRecipient != null
			});
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001126C File Offset: 0x0000F46C
		private GroupMailbox BuildGroupMailbox(ADRawEntry adEntry)
		{
			MailboxAssociation association = new MailboxAssociation
			{
				IsMember = true,
				JoinDate = ExDateTime.UtcNow
			};
			GroupMailboxLocator locator = new GroupMailboxLocator(this.adSession, adEntry[ADRecipientSchema.ExternalDirectoryObjectId] as string, adEntry[ADRecipientSchema.LegacyExchangeDN] as string);
			GroupMailboxBuilder groupMailboxBuilder = new GroupMailboxBuilder(locator);
			return groupMailboxBuilder.BuildFromAssociation(association).BuildFromDirectory(adEntry).Mailbox;
		}

		// Token: 0x040005E2 RID: 1506
		private static readonly Trace Tracer = ExTraceGlobals.ModernGroupsTracer;

		// Token: 0x040005E3 RID: 1507
		private static readonly PropertyDefinition[] AdditionalGroupPropertiesToRead = new PropertyDefinition[]
		{
			ADRecipientSchema.ExternalDirectoryObjectId,
			ADRecipientSchema.LegacyExchangeDN
		};

		// Token: 0x040005E4 RID: 1508
		private static readonly PropertyDefinition[] GroupPropertiesToRead = GroupMailboxBuilder.AllADProperties.Union(GroupMailboxCollectionBuilder.AdditionalGroupPropertiesToRead).ToArray<PropertyDefinition>();

		// Token: 0x040005E5 RID: 1509
		private readonly IGroupsLogger logger;

		// Token: 0x040005E6 RID: 1510
		private readonly IRecipientSession adSession;

		// Token: 0x040005E7 RID: 1511
		private IRecipientSession tenantPreferredAdSession;
	}
}
