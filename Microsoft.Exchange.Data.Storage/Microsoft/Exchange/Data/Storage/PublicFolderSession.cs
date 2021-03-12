using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.MailboxRules;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;
using Microsoft.Mapi.Security;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000018 RID: 24
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PublicFolderSession : StoreSession, IPublicFolderSession, IStoreSession, IDisposable
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00014344 File Offset: 0x00012544
		public PublicFolderCOWSession CowSession
		{
			get
			{
				PublicFolderCOWSession result;
				using (base.CheckDisposed("PublicFolderCOWSession::get"))
				{
					result = this.copyOnWriteNotification;
				}
				return result;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00014390 File Offset: 0x00012590
		private static int PerMailboxHierarchyAccessUserLimit
		{
			get
			{
				if (PublicFolderSession.perMailboxHierarchyAccessUserLimit == null)
				{
					PublicFolderSession.perMailboxHierarchyAccessUserLimit = new int?(StoreSession.GetConfigFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PublicFolder", "PerMailboxActiveUserLimit", 2000, (int x) => x > 0));
				}
				return PublicFolderSession.perMailboxHierarchyAccessUserLimit.Value;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000143F0 File Offset: 0x000125F0
		public static PublicFolderSession Open(IExchangePrincipal connectAsPrincipal, Guid publicFolderMailboxGuid, WindowsPrincipal authenticatedUser, CultureInfo cultureInfo, string clientInfoString)
		{
			Util.ThrowOnNullArgument(connectAsPrincipal, "connectAsPrincipal");
			Util.ThrowOnNullArgument(authenticatedUser, "authenticatedUser");
			ExchangePrincipal exchangePrincipal = PublicFolderSession.GetPublicFolderMailboxPrincipal(connectAsPrincipal.MailboxInfo.OrganizationId, publicFolderMailboxGuid);
			return new PublicFolderSession(connectAsPrincipal.MailboxInfo.OrganizationId, connectAsPrincipal.LegacyDn, new Participant(connectAsPrincipal), exchangePrincipal, PublicFolderSession.IsHierarchyMailboxForUser(connectAsPrincipal.MailboxInfo.OrganizationId, exchangePrincipal.MailboxInfo.MailboxGuid, connectAsPrincipal.MailboxInfo.MailboxGuid, connectAsPrincipal.DefaultPublicFolderMailbox), (WindowsIdentity)authenticatedUser.Identity, null, OpenMailboxSessionFlags.None, cultureInfo, clientInfoString, null)
			{
				LogonType = LogonType.Delegated
			};
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00014488 File Offset: 0x00012688
		public static PublicFolderSession Open(IExchangePrincipal connectAsPrincipal, Guid publicFolderMailboxGuid, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString)
		{
			Util.ThrowOnNullArgument(connectAsPrincipal, "connectAsPrincipal");
			Util.ThrowOnNullArgument(clientSecurityContext, "clientSecurityContext");
			ExchangePrincipal exchangePrincipal = PublicFolderSession.GetPublicFolderMailboxPrincipal(connectAsPrincipal.MailboxInfo.OrganizationId, publicFolderMailboxGuid);
			return new PublicFolderSession(connectAsPrincipal.MailboxInfo.OrganizationId, connectAsPrincipal.LegacyDn, new Participant(connectAsPrincipal), exchangePrincipal, PublicFolderSession.IsHierarchyMailboxForUser(connectAsPrincipal.MailboxInfo.OrganizationId, exchangePrincipal.MailboxInfo.MailboxGuid, connectAsPrincipal.MailboxInfo.MailboxGuid, connectAsPrincipal.DefaultPublicFolderMailbox), null, PublicFolderSession.FromClientSecurityContext(connectAsPrincipal.MailboxInfo.OrganizationId, clientSecurityContext), OpenMailboxSessionFlags.None, cultureInfo, clientInfoString, null)
			{
				LogonType = LogonType.Delegated
			};
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00014528 File Offset: 0x00012728
		public static PublicFolderSession Open(IExchangePrincipal connectAsPrincipal, IExchangePrincipal publicFolderMailboxPrincipal, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString)
		{
			Util.ThrowOnNullArgument(connectAsPrincipal, "connectAsPrincipal");
			Util.ThrowOnNullArgument(publicFolderMailboxPrincipal, "publicFolderMailboxPrincipal");
			return new PublicFolderSession(connectAsPrincipal.MailboxInfo.OrganizationId, connectAsPrincipal.LegacyDn, new Participant(connectAsPrincipal), publicFolderMailboxPrincipal, PublicFolderSession.IsHierarchyMailboxForUser(connectAsPrincipal.MailboxInfo.OrganizationId, publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid, connectAsPrincipal.MailboxInfo.MailboxGuid, connectAsPrincipal.DefaultPublicFolderMailbox), null, PublicFolderSession.FromClientSecurityContext(connectAsPrincipal.MailboxInfo.OrganizationId, clientSecurityContext), OpenMailboxSessionFlags.None, cultureInfo, clientInfoString, null)
			{
				LogonType = LogonType.Delegated
			};
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000145B4 File Offset: 0x000127B4
		public static PublicFolderSession Open(string connectAsUserLegacyDn, MiniRecipient connectAsMiniRecipient, IExchangePrincipal publicFolderMailboxPrincipal, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString)
		{
			Util.ThrowOnNullOrEmptyArgument(connectAsUserLegacyDn, "connectAsUserLegacyDn");
			Util.ThrowOnNullArgument(connectAsMiniRecipient, "connectAsMiniRecipient");
			Util.ThrowOnNullArgument(publicFolderMailboxPrincipal, "publicFolderMailboxPrincipal");
			return new PublicFolderSession(publicFolderMailboxPrincipal.MailboxInfo.OrganizationId, connectAsUserLegacyDn, new Participant(connectAsMiniRecipient), publicFolderMailboxPrincipal, PublicFolderSession.IsHierarchyMailboxForUser(publicFolderMailboxPrincipal.MailboxInfo.OrganizationId, publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid, connectAsMiniRecipient.ExchangeGuid, connectAsMiniRecipient.DefaultPublicFolderMailbox), null, PublicFolderSession.FromClientSecurityContext(connectAsMiniRecipient.OrganizationId, clientSecurityContext), OpenMailboxSessionFlags.None, cultureInfo, clientInfoString, null)
			{
				LogonType = LogonType.Delegated
			};
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00014640 File Offset: 0x00012840
		public static PublicFolderSession Open(string connectAsUserLegacyDn, Participant connectAsParticipant, Guid connectAsParticipantExchangeGuid, ADObjectId connectAsParticipantDefaultPublicFolderMailbox, IExchangePrincipal publicFolderMailboxPrincipal, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString)
		{
			Util.ThrowOnNullOrEmptyArgument(connectAsUserLegacyDn, "connectAsUserLegacyDn");
			Util.ThrowOnNullArgument(connectAsParticipant, "connectAsParticipant");
			Util.ThrowOnNullArgument(publicFolderMailboxPrincipal, "publicFolderMailboxPrincipal");
			return new PublicFolderSession(publicFolderMailboxPrincipal.MailboxInfo.OrganizationId, connectAsUserLegacyDn, connectAsParticipant, publicFolderMailboxPrincipal, PublicFolderSession.IsHierarchyMailboxForUser(publicFolderMailboxPrincipal.MailboxInfo.OrganizationId, publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid, connectAsParticipantExchangeGuid, connectAsParticipantDefaultPublicFolderMailbox), null, PublicFolderSession.FromClientSecurityContext(publicFolderMailboxPrincipal.MailboxInfo.OrganizationId, clientSecurityContext), OpenMailboxSessionFlags.None, cultureInfo, clientInfoString, null)
			{
				LogonType = LogonType.Delegated
			};
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000146C8 File Offset: 0x000128C8
		public static PublicFolderSession Open(IExchangePrincipal connectAsPrincipal, IExchangePrincipal publicFolderMailboxPrincipal, WindowsPrincipal authenticatedUser, CultureInfo cultureInfo, string clientInfoString)
		{
			Util.ThrowOnNullArgument(connectAsPrincipal, "connectAsPrincipal");
			Util.ThrowOnNullArgument(publicFolderMailboxPrincipal, "publicFolderMailboxPrincipal");
			Util.ThrowOnNullArgument(authenticatedUser, "authenticatedUser");
			return new PublicFolderSession(connectAsPrincipal.MailboxInfo.OrganizationId, connectAsPrincipal.LegacyDn, new Participant(connectAsPrincipal), publicFolderMailboxPrincipal, PublicFolderSession.IsHierarchyMailboxForUser(connectAsPrincipal.MailboxInfo.OrganizationId, publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid, connectAsPrincipal.MailboxInfo.MailboxGuid, connectAsPrincipal.DefaultPublicFolderMailbox), (WindowsIdentity)authenticatedUser.Identity, null, OpenMailboxSessionFlags.None, cultureInfo, clientInfoString, null)
			{
				LogonType = LogonType.Delegated
			};
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0001475C File Offset: 0x0001295C
		public static PublicFolderSession OpenAsAdmin(OrganizationId organizationId, IExchangePrincipal connectAsPrincipal, Guid publicFolderMailboxGuid, WindowsPrincipal authenticatedUser, CultureInfo cultureInfo, string clientInfoString, IBudget budget)
		{
			Util.ThrowOnNullArgument(organizationId, "organizationId");
			ExchangePrincipal exchangePrincipal = PublicFolderSession.GetPublicFolderMailboxPrincipal(organizationId, publicFolderMailboxGuid);
			return new PublicFolderSession(organizationId, (connectAsPrincipal != null) ? connectAsPrincipal.LegacyDn : null, (connectAsPrincipal != null) ? new Participant(connectAsPrincipal) : null, exchangePrincipal, false, (authenticatedUser != null) ? ((WindowsIdentity)authenticatedUser.Identity) : null, null, OpenMailboxSessionFlags.RequestAdminAccess, cultureInfo, clientInfoString, budget)
			{
				LogonType = ((connectAsPrincipal != null) ? LogonType.DelegatedAdmin : LogonType.Admin)
			};
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000147C4 File Offset: 0x000129C4
		public static PublicFolderSession OpenAsAdmin(string connectAsUserLegacyDn, MiniRecipient connectAsMiniRecipient, IExchangePrincipal publicFolderMailboxPrincipal, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString)
		{
			Util.ThrowOnNullOrEmptyArgument(connectAsUserLegacyDn, "connectAsUserLegacyDn");
			Util.ThrowOnNullArgument(connectAsMiniRecipient, "connectAsMiniRecipient");
			Util.ThrowOnNullArgument(publicFolderMailboxPrincipal, "publicFolderMailboxPrincipal");
			return new PublicFolderSession(publicFolderMailboxPrincipal.MailboxInfo.OrganizationId, connectAsUserLegacyDn, new Participant(connectAsMiniRecipient), publicFolderMailboxPrincipal, PublicFolderSession.IsHierarchyMailboxForUser(publicFolderMailboxPrincipal.MailboxInfo.OrganizationId, publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid, connectAsMiniRecipient.ExchangeGuid, connectAsMiniRecipient.DefaultPublicFolderMailbox), null, PublicFolderSession.FromClientSecurityContext(connectAsMiniRecipient.OrganizationId, clientSecurityContext), OpenMailboxSessionFlags.RequestAdminAccess, cultureInfo, clientInfoString, null)
			{
				LogonType = LogonType.DelegatedAdmin
			};
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00014850 File Offset: 0x00012A50
		public static PublicFolderSession OpenAsAdmin(IExchangePrincipal connectAsPrincipal, IExchangePrincipal publicFolderMailboxPrincipal, WindowsPrincipal authenticatedUser, CultureInfo cultureInfo, string clientInfoString, IBudget budget)
		{
			Util.ThrowOnNullArgument(publicFolderMailboxPrincipal, "publicFolderMailboxPrincipal");
			return new PublicFolderSession(publicFolderMailboxPrincipal.MailboxInfo.OrganizationId, (connectAsPrincipal != null) ? connectAsPrincipal.LegacyDn : null, (connectAsPrincipal != null) ? new Participant(connectAsPrincipal) : null, publicFolderMailboxPrincipal, false, (authenticatedUser != null) ? ((WindowsIdentity)authenticatedUser.Identity) : null, null, OpenMailboxSessionFlags.RequestAdminAccess, cultureInfo, clientInfoString, budget)
			{
				LogonType = ((connectAsPrincipal != null) ? LogonType.DelegatedAdmin : LogonType.Admin)
			};
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000148BC File Offset: 0x00012ABC
		public static PublicFolderSession OpenAsTransport(IExchangePrincipal publicFolderMailboxPrincipal, OpenTransportSessionFlags flags)
		{
			Util.ThrowOnNullArgument(publicFolderMailboxPrincipal, "publicFolderMailboxPrincipal");
			EnumValidator.ThrowIfInvalid<OpenTransportSessionFlags>(flags, "flags");
			OpenMailboxSessionFlags openMailboxSessionFlags = OpenMailboxSessionFlags.RequestTransportAccess;
			switch (flags)
			{
			case OpenTransportSessionFlags.OpenForQuotaMessageDelivery:
				openMailboxSessionFlags |= OpenMailboxSessionFlags.OpenForQuotaMessageDelivery;
				break;
			case OpenTransportSessionFlags.OpenForNormalMessageDelivery:
				openMailboxSessionFlags |= OpenMailboxSessionFlags.OpenForNormalMessageDelivery;
				break;
			case OpenTransportSessionFlags.OpenForSpecialMessageDelivery:
				openMailboxSessionFlags |= OpenMailboxSessionFlags.OpenForSpecialMessageDelivery;
				break;
			}
			return new PublicFolderSession(publicFolderMailboxPrincipal.MailboxInfo.OrganizationId, null, null, publicFolderMailboxPrincipal, false, null, null, openMailboxSessionFlags, CultureInfo.InvariantCulture, "Client=Hub Transport", null)
			{
				LogonType = LogonType.Transport
			};
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00014938 File Offset: 0x00012B38
		public static PublicFolderSession OpenAsTransport(OrganizationId organizationId, Guid publicFolderMailboxGuid)
		{
			Util.ThrowOnNullArgument(organizationId, "organizationId");
			ExchangePrincipal exchangePrincipal = PublicFolderSession.GetPublicFolderMailboxPrincipal(organizationId, publicFolderMailboxGuid);
			return new PublicFolderSession(organizationId, null, null, exchangePrincipal, false, null, null, OpenMailboxSessionFlags.RequestTransportAccess, CultureInfo.InvariantCulture, "Client=Hub Transport", null)
			{
				LogonType = LogonType.Transport
			};
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0001497C File Offset: 0x00012B7C
		public static PublicFolderSession OpenAsMRS(IExchangePrincipal publicFolderMailboxPrincipal, string clientInfoString, OpenMailboxSessionFlags additionalMailboxSessionFlags)
		{
			Util.ThrowOnNullArgument(publicFolderMailboxPrincipal, "publicFolderMailboxPrincipal");
			additionalMailboxSessionFlags |= (OpenMailboxSessionFlags.RequestAdminAccess | OpenMailboxSessionFlags.RequestLocalRpcConnection | OpenMailboxSessionFlags.OverrideHomeMdb | OpenMailboxSessionFlags.DisconnectedMailbox | OpenMailboxSessionFlags.MoveUser);
			return new PublicFolderSession(publicFolderMailboxPrincipal.MailboxInfo.OrganizationId, null, null, publicFolderMailboxPrincipal, false, null, null, additionalMailboxSessionFlags, CultureInfo.InvariantCulture, clientInfoString, null)
			{
				LogonType = LogonType.SystemService
			};
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000149C4 File Offset: 0x00012BC4
		public static PublicFolderSession OpenAsSearch(IExchangePrincipal publicFolderMailboxPrincipal, string clientInfoString, bool isReadOnly)
		{
			Util.ThrowOnNullArgument(publicFolderMailboxPrincipal, "publicFolderMailboxPrincipal");
			OpenMailboxSessionFlags openMailboxSessionFlags = OpenMailboxSessionFlags.RequestAdminAccess | OpenMailboxSessionFlags.OverrideHomeMdb | OpenMailboxSessionFlags.ContentIndexing;
			if (isReadOnly)
			{
				openMailboxSessionFlags |= OpenMailboxSessionFlags.ReadOnly;
			}
			return new PublicFolderSession(publicFolderMailboxPrincipal.MailboxInfo.OrganizationId, null, null, publicFolderMailboxPrincipal, false, null, null, openMailboxSessionFlags, CultureInfo.InvariantCulture, clientInfoString, null)
			{
				LogonType = LogonType.SystemService
			};
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00014A14 File Offset: 0x00012C14
		public static bool CheckIfPublicFolderMailboxLockedForMigration(OrganizationId organizationId)
		{
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Mrs.PublicFolderMailboxesMigration.Enabled)
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.NonCacheSessionFactory.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 733, "CheckIfPublicFolderMailboxLockedForMigration", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\obj\\amd64\\PublicFolderSession.cs");
				Organization orgContainer = tenantOrTopologyConfigurationSession.GetOrgContainer();
				return orgContainer.Heuristics.HasFlag(HeuristicsFlags.PublicFolderMailboxesLockedForNewConnections);
			}
			return false;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00014AAC File Offset: 0x00012CAC
		public static PublicFoldersDeployment GetPublicFoldersDeploymentType(OrganizationId organizationId)
		{
			Util.ThrowOnNullArgument(organizationId, "organizationId");
			PublicFoldersDeployment publicfoldersDeploymentType = PublicFoldersDeployment.None;
			DirectoryHelper.DoAdCallAndTranslateExceptions(delegate
			{
				publicfoldersDeploymentType = TenantPublicFolderConfigurationCache.Instance.GetValue(organizationId).PublicFoldersDeploymentType;
			}, "PublicFolderSession::GetPublicFoldersDeploymentType");
			return publicfoldersDeploymentType;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00014B3C File Offset: 0x00012D3C
		public static bool TryGetPrimaryHierarchyMailboxGuid(OrganizationId organizationId, out Guid mailboxGuid)
		{
			Util.ThrowOnNullArgument(organizationId, "organizationId");
			Guid primaryHierarchyMailboxGuid = Guid.Empty;
			DirectoryHelper.DoAdCallAndTranslateExceptions(delegate
			{
				PublicFolderInformation hierarchyMailboxInformation = TenantPublicFolderConfigurationCache.Instance.GetValue(organizationId).GetHierarchyMailboxInformation();
				if (hierarchyMailboxInformation.Type == PublicFolderInformation.HierarchyType.MailboxGuid)
				{
					primaryHierarchyMailboxGuid = hierarchyMailboxInformation.HierarchyMailboxGuid;
				}
			}, "PublicFolderSession.TryGetPrimaryHierarchyMailboxGuid");
			mailboxGuid = primaryHierarchyMailboxGuid;
			return primaryHierarchyMailboxGuid != Guid.Empty;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00014BF4 File Offset: 0x00012DF4
		public static bool TryGetHierarchyMailboxGuidForUser(OrganizationId organizationId, Guid userMailboxGuid, ADObjectId defaultPublicFolderMailbox, out Guid publicFolderMailboxGuid)
		{
			Util.ThrowOnNullArgument(organizationId, "organizationId");
			publicFolderMailboxGuid = Guid.Empty;
			if (PublicFolderSession.CheckIfPublicFolderMailboxLockedForMigration(organizationId))
			{
				return false;
			}
			Guid userHierarchyMailboxGuid = Guid.Empty;
			if (userMailboxGuid != Guid.Empty)
			{
				DirectoryHelper.DoAdCallAndTranslateExceptions(delegate
				{
					TenantPublicFolderConfiguration value = TenantPublicFolderConfigurationCache.Instance.GetValue(organizationId);
					PublicFolderInformation hierarchyMailboxInformation = value.GetHierarchyMailboxInformation();
					if (hierarchyMailboxInformation.Type == PublicFolderInformation.HierarchyType.MailboxGuid)
					{
						userHierarchyMailboxGuid = value.GetHierarchyMailboxGuidForUser(userMailboxGuid, defaultPublicFolderMailbox);
					}
				}, "PublicFolderSession.TryGetHierarchyMailboxGuidForUser");
			}
			publicFolderMailboxGuid = userHierarchyMailboxGuid;
			return publicFolderMailboxGuid != Guid.Empty;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00014CEC File Offset: 0x00012EEC
		public static bool TryGetContentMailboxGuid(OrganizationId organizationId, out Guid mailboxGuid)
		{
			Util.ThrowOnNullArgument(organizationId, "organizationId");
			mailboxGuid = Guid.Empty;
			if (PublicFolderSession.CheckIfPublicFolderMailboxLockedForMigration(organizationId))
			{
				return false;
			}
			Guid publicFolderContentMailboxGuid = Guid.Empty;
			DirectoryHelper.DoAdCallAndTranslateExceptions(delegate
			{
				TenantPublicFolderConfigurationCache.Instance.Clear();
				Guid[] contentMailboxGuids = TenantPublicFolderConfigurationCache.Instance.GetValue(organizationId).GetContentMailboxGuids();
				if (contentMailboxGuids.Length > 0)
				{
					publicFolderContentMailboxGuid = contentMailboxGuids[0];
				}
			}, "PublicFolderSession.TryGetContentMailboxGuid");
			mailboxGuid = publicFolderContentMailboxGuid;
			return mailboxGuid != Guid.Empty;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00014D70 File Offset: 0x00012F70
		public static bool TryGetPublicFolderMailboxPrincipal(OrganizationId organizationId, Guid publicFolderMailboxGuid, bool ignoreCache, out ExchangePrincipal principal)
		{
			Util.ThrowOnNullArgument(organizationId, "organizationId");
			principal = null;
			if (PublicFolderSession.CheckIfPublicFolderMailboxLockedForMigration(organizationId))
			{
				return false;
			}
			Guid guid;
			if (PublicFolderSession.TryGetPrimaryHierarchyMailboxGuid(organizationId, out guid))
			{
				try
				{
					MultiValueKey key = new MultiValueKey(new object[]
					{
						organizationId,
						(publicFolderMailboxGuid == PublicFolderSession.HierarchyMailboxGuidAlias) ? guid : publicFolderMailboxGuid
					});
					if (ignoreCache)
					{
						ExchangePrincipalCache.Instance.Remove(key);
					}
					ExchangePrincipal exchangePrincipal = ExchangePrincipalCache.Instance.Get(key);
					if (exchangePrincipal.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox)
					{
						principal = exchangePrincipal;
						return true;
					}
				}
				catch (ObjectNotFoundException arg)
				{
					ExTraceGlobals.SessionTracer.TraceError<ObjectNotFoundException>((long)organizationId.GetHashCode(), "PublicFolderSession::TryGetPublicFolderMailboxPrincipal: Exception {0}", arg);
				}
			}
			principal = null;
			return false;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00014E38 File Offset: 0x00013038
		private static ExchangePrincipal GetPublicFolderMailboxPrincipal(OrganizationId organizationId, Guid publicFolderMailboxGuid)
		{
			ExchangePrincipal result;
			if (PublicFolderSession.TryGetPublicFolderMailboxPrincipal(organizationId, publicFolderMailboxGuid, true, out result))
			{
				return result;
			}
			throw new ObjectNotFoundException((publicFolderMailboxGuid == Guid.Empty) ? PublicFolderSession.GetNoPublicFoldersProvisionedError(organizationId) : ServerStrings.PublicFolderMailboxNotFound);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00014E74 File Offset: 0x00013074
		private static bool IsHierarchyMailboxForUser(OrganizationId organizationId, Guid publicFolderMailboxGuid, Guid userMailboxGuid, ADObjectId defaultPublicFolderMailbox)
		{
			Guid empty = Guid.Empty;
			return PublicFolderSession.TryGetHierarchyMailboxGuidForUser(organizationId, userMailboxGuid, defaultPublicFolderMailbox, out empty) && publicFolderMailboxGuid == empty;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00014E9C File Offset: 0x0001309C
		private static bool IsGrayException(Exception exception)
		{
			return !(exception is StorageTransientException) && !(exception is StoragePermanentException) && GrayException.IsGrayException(exception);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00014EEC File Offset: 0x000130EC
		private PublicFolderSession(OrganizationId organizationId, string connectAsUserLegacyDn, Participant connectAsParticipant, IExchangePrincipal publicFolderMailboxPrincipal, bool isPublicFolderHierarchyAccessForUser, WindowsIdentity windowsIdentity, ClientIdentityInfo clientIdentityInfo, OpenMailboxSessionFlags flags, CultureInfo cultureInfo, string clientInfoString, IBudget budget) : base(cultureInfo, clientInfoString, budget)
		{
			base.IsMoveUser = flags.HasFlag(OpenMailboxSessionFlags.MoveUser);
			this.isContentIndexing = flags.HasFlag(OpenMailboxSessionFlags.ContentIndexing);
			if (flags.HasFlag(OpenMailboxSessionFlags.ReadOnly))
			{
				base.Capabilities = base.Capabilities.CloneAndExtendCapabilities(SessionCapabilitiesFlags.ReadOnly);
			}
			if (!base.IsMoveUser && !this.isContentIndexing)
			{
				Util.ThrowOnNullArgument(organizationId, "organizationId");
				if (PublicFolderSession.CheckIfPublicFolderMailboxLockedForMigration(organizationId))
				{
					throw new MailboxInTransitException(ServerStrings.PublicFoldersCannotBeAccessedDuringCompletion);
				}
			}
			Util.ThrowOnNullArgument(publicFolderMailboxPrincipal, "publicFolderMailboxPrincipal");
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.publicFolderMailboxPrincipal = publicFolderMailboxPrincipal;
				if (windowsIdentity != null && clientIdentityInfo != null)
				{
					throw new ArgumentException("Both windowsIdentity and clientIdentityInfo should not be used");
				}
				if (windowsIdentity != null)
				{
					this.identity = windowsIdentity;
				}
				else if (clientIdentityInfo != null)
				{
					this.identity = clientIdentityInfo;
				}
				else
				{
					this.identity = WindowsIdentity.GetCurrent();
					this.disposeWindowsIdentity = true;
				}
				ExTraceGlobals.SessionTracer.Information((long)this.GetHashCode(), "PublicFolderSession.Initialize");
				this.connectAsParticipant = connectAsParticipant;
				OpenStoreFlag openStoreFlag = OpenStoreFlag.TakeOwnership | OpenStoreFlag.MailboxGuid;
				if (!base.IsMoveUser)
				{
					openStoreFlag |= OpenStoreFlag.PublicFolderSubsystem;
				}
				base.StoreFlag = openStoreFlag;
				this.connectFlag = (ConnectFlag.LowMemoryFootprint | ConnectFlag.UseRpcContextPool);
				if (flags.HasFlag(OpenMailboxSessionFlags.RequestExchangeRpcServer) || StoreSession.TestRequestExchangeRpcServer)
				{
					this.connectFlag |= ConnectFlag.ConnectToExchangeRpcServerOnly;
				}
				if (flags.HasFlag(OpenMailboxSessionFlags.RequestTransportAccess))
				{
					this.connectFlag |= ConnectFlag.UseTransportPrivilege;
					if (flags.HasFlag(OpenMailboxSessionFlags.OpenForQuotaMessageDelivery))
					{
						base.StoreFlag |= OpenStoreFlag.DeliverQuotaMessage;
					}
					if (flags.HasFlag(OpenMailboxSessionFlags.OpenForNormalMessageDelivery))
					{
						base.StoreFlag |= OpenStoreFlag.DeliverNormalMessage;
					}
					if (flags.HasFlag(OpenMailboxSessionFlags.OpenForSpecialMessageDelivery))
					{
						base.StoreFlag |= OpenStoreFlag.DeliverSpecialMessage;
					}
				}
				if (flags.HasFlag(OpenMailboxSessionFlags.RequestAdminAccess))
				{
					this.connectFlag |= ConnectFlag.UseAdminPrivilege;
					base.StoreFlag |= OpenStoreFlag.UseAdminPrivilege;
					if (flags.HasFlag(OpenMailboxSessionFlags.OverrideHomeMdb))
					{
						base.StoreFlag |= OpenStoreFlag.OverrideHomeMdb;
					}
					if (flags.HasFlag(OpenMailboxSessionFlags.DisconnectedMailbox))
					{
						base.StoreFlag |= OpenStoreFlag.DisconnectedMailbox;
					}
					if (flags.HasFlag(OpenMailboxSessionFlags.UseRecoveryDatabase))
					{
						base.StoreFlag |= OpenStoreFlag.RestoreDatabase;
					}
					if (flags.HasFlag(OpenMailboxSessionFlags.RequestLocalRpcConnection))
					{
						this.connectFlag |= ConnectFlag.LocalRpcOnly;
					}
					if (string.IsNullOrWhiteSpace(connectAsUserLegacyDn))
					{
						this.TryGetServiceUserLegacyDn(out this.userLegacyDn);
					}
					else
					{
						this.connectFlag |= ConnectFlag.UseDelegatedAuthPrivilege;
						this.userLegacyDn = connectAsUserLegacyDn;
					}
				}
				else if ((flags & OpenMailboxSessionFlags.RequestTransportAccess) == OpenMailboxSessionFlags.RequestTransportAccess)
				{
					this.connectFlag |= ConnectFlag.UseTransportPrivilege;
					this.TryGetServiceUserLegacyDn(out this.userLegacyDn);
				}
				else
				{
					this.connectFlag |= ConnectFlag.UseDelegatedAuthPrivilege;
					this.userLegacyDn = connectAsUserLegacyDn;
				}
				base.SetMailboxStoreObject(null);
				this.InternalConnect();
				if (!base.IsMoveUser && !this.isContentIndexing)
				{
					Guid a;
					this.isPrimaryHierarchySession = (PublicFolderSession.TryGetPrimaryHierarchyMailboxGuid(this.OrganizationId, out a) && a == this.MailboxGuid);
				}
				bool flag = base.IsMoveUser || this.isContentIndexing || (flags.HasFlag(OpenMailboxSessionFlags.RequestAdminAccess) && (clientInfoString.StartsWith("Client=TBA", StringComparison.OrdinalIgnoreCase) || clientInfoString.StartsWith("Client=PublicFolderSystem", StringComparison.OrdinalIgnoreCase)));
				this.ProhibitFolderRuleEvaluation = flag;
				if (!flag)
				{
					this.copyOnWriteNotification = PublicFolderCOWSession.Create(this);
					if ((flags & OpenMailboxSessionFlags.RequestTransportAccess) != OpenMailboxSessionFlags.RequestTransportAccess && (flags & OpenMailboxSessionFlags.RequestAdminAccess) != OpenMailboxSessionFlags.RequestAdminAccess && (clientInfoString.StartsWith("Client=MSExchangeRPC", StringComparison.OrdinalIgnoreCase) || clientInfoString.StartsWith("Client=WebServices", StringComparison.OrdinalIgnoreCase)))
					{
						if (isPublicFolderHierarchyAccessForUser)
						{
							this.publicFolderMailboxSynchronizerReference = PublicFolderMailboxSynchronizerManager.Instance.GetPublicFolderMailboxSynchronizer(this.MailboxPrincipal, this.IsPrimaryHierarchySession, true);
							this.IssueAlertIfNeeded();
						}
						else
						{
							this.publicFolderMailboxSynchronizerReference = PublicFolderMailboxSynchronizerManager.Instance.GetPublicFolderMailboxSynchronizer(this.MailboxPrincipal, this.IsPrimaryHierarchySession, false);
						}
					}
				}
				DirectoryHelper.DoAdCallAndTranslateExceptions(delegate
				{
					OrganizationContentConversionProperties organizationContentConversionProperties;
					if (OrganizationContentConversionCache.TryGetOrganizationContentConversionProperties(this.OrganizationId, out organizationContentConversionProperties))
					{
						base.PreferredInternetCodePageForShiftJis = organizationContentConversionProperties.PreferredInternetCodePageForShiftJis;
						base.RequiredCoverage = organizationContentConversionProperties.RequiredCharsetCoverage;
					}
				}, "PublicFolderSession::PublicFolderSession");
				disposeGuard.Success();
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000153D0 File Offset: 0x000135D0
		private void IssueAlertIfNeeded()
		{
			int activeHierarchyAccessReferenceCount = PublicFolderMailboxSynchronizerManager.Instance.GetActiveHierarchyAccessReferenceCount(this.MailboxPrincipal);
			if (activeHierarchyAccessReferenceCount > PublicFolderSession.PerMailboxHierarchyAccessUserLimit && ExDateTime.UtcNow > PublicFolderMailboxSynchronizerManager.Instance.GetAlertIssuedTime(this.MailboxPrincipal) + PublicFolderSession.minTimeToRaiseAlert)
			{
				this.RaiseAlarm(activeHierarchyAccessReferenceCount);
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00015424 File Offset: 0x00013624
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.disposeWindowsIdentity)
				{
					Util.DisposeIfPresent(this.identity as WindowsIdentity);
					this.identity = null;
				}
				if (this.copyOnWriteNotification != null)
				{
					this.copyOnWriteNotification.Dispose();
					this.copyOnWriteNotification = null;
				}
				if (this.publicFolderMailboxSynchronizerReference != null)
				{
					this.publicFolderMailboxSynchronizerReference.Dispose();
					this.publicFolderMailboxSynchronizerReference = null;
				}
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00015490 File Offset: 0x00013690
		public override DisposeTracker GetDisposeTracker()
		{
			DisposeTracker result;
			using (base.CreateSessionGuard("GetDisposeTracker"))
			{
				result = DisposeTracker.Get<PublicFolderSession>(this);
			}
			return result;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000154D4 File Offset: 0x000136D4
		public override void Connect()
		{
			using (base.CheckDisposed("Connect"))
			{
				ExTraceGlobals.SessionTracer.Information((long)this.GetHashCode(), "PublicFolderSession.Connect");
				if (base.IsConnected)
				{
					ExTraceGlobals.SessionTracer.TraceError<Type, int>((long)this.GetHashCode(), "PublicFolderSession.Connect: Object type = {0}, hashcode = {1} trying to call Connect when already connected", base.GetType(), this.GetHashCode());
					throw new ConnectionFailedPermanentException(ServerStrings.ExAlreadyConnected);
				}
				if (base.IsDead)
				{
					base.SetMailboxStoreObject(null);
					this.InternalConnect();
					base.IsDead = false;
				}
				base.IsConnected = true;
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0001557C File Offset: 0x0001377C
		public override void Disconnect()
		{
			using (base.CheckDisposed("Disconnect"))
			{
				ExTraceGlobals.SessionTracer.Information((long)this.GetHashCode(), "PublicFolderSession.Disconnect");
				if (!base.IsConnected)
				{
					ExTraceGlobals.SessionTracer.TraceError<Guid>((long)this.GetHashCode(), "PublicFolderSession.Disconnect: Public Folder Mailbox {0} has not been connected yet.", this.MailboxGuid);
					throw new ConnectionFailedPermanentException(ServerStrings.ExNotConnected);
				}
				ExTraceGlobals.SessionTracer.TraceDebug<Guid>((long)this.GetHashCode(), "PublicFolderSession.Disconnect: Disconnect succeeded. MailboxGuid = {0}.", this.MailboxGuid);
				base.IsConnected = false;
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00015620 File Offset: 0x00013820
		public override StoreObjectId GetDefaultFolderId(DefaultFolderType defaultFolderType)
		{
			using (base.CheckDisposed("GetDefaultFolderId"))
			{
				if (ExEnvironment.IsTest)
				{
					StoreObjectType foldertype;
					switch (defaultFolderType)
					{
					case DefaultFolderType.Calendar:
						foldertype = StoreObjectType.CalendarFolder;
						goto IL_73;
					case DefaultFolderType.Contacts:
						foldertype = StoreObjectType.ContactsFolder;
						goto IL_73;
					case DefaultFolderType.DeletedItems:
						break;
					case DefaultFolderType.Drafts:
					case DefaultFolderType.Inbox:
						foldertype = StoreObjectType.Folder;
						goto IL_73;
					default:
						if (defaultFolderType == DefaultFolderType.Tasks)
						{
							foldertype = StoreObjectType.TasksFolder;
							goto IL_73;
						}
						if (defaultFolderType == DefaultFolderType.SearchFolders)
						{
							return this.GetTombstonesRootFolderId();
						}
						break;
					}
					throw new ObjectNotFoundException(ServerStrings.DefaultFolderNotFoundInPublicFolderMailbox(defaultFolderType.ToString()));
					IL_73:
					using (Folder folder = Folder.Create(this, this.GetIpmSubtreeFolderId(), foldertype, Enum.GetName(typeof(DefaultFolderType), defaultFolderType), CreateMode.OpenIfExists))
					{
						folder.Save(SaveMode.NoConflictResolution);
						folder.Load();
						return folder.StoreObjectId;
					}
				}
				throw new NotImplementedException();
			}
			StoreObjectId result;
			return result;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00015718 File Offset: 0x00013918
		public override bool TryFixDefaultFolderId(DefaultFolderType defaultFolderType, out StoreObjectId id)
		{
			bool result;
			using (base.CheckDisposed("TryFixDefaultFolderId"))
			{
				id = null;
				result = false;
			}
			return result;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00015758 File Offset: 0x00013958
		public bool IsWellKnownFolder(StoreObjectId id)
		{
			return this.GetPublicFolderRootId().Equals(id) || this.GetIpmSubtreeFolderId().Equals(id) || this.GetNonIpmSubtreeFolderId().Equals(id) || this.GetEFormsRegistryFolderId().Equals(id) || this.GetDumpsterRootFolderId().Equals(id) || this.GetTombstonesRootFolderId().Equals(id) || this.GetAsyncDeleteStateFolderId().Equals(id) || this.GetInternalSubmissionFolderId().Equals(id);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000157D8 File Offset: 0x000139D8
		protected override MapiStore ForceOpen(MapiStore linkedStore)
		{
			using (base.CreateSessionGuard("ForceOpen"))
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00015814 File Offset: 0x00013A14
		protected override ObjectAccessGuard CheckObjectState(string methodName)
		{
			ObjectAccessGuard objectAccessGuard = base.CheckObjectState(methodName);
			bool flag = false;
			ObjectAccessGuard result;
			try
			{
				if (!base.IsConnected)
				{
					ExTraceGlobals.SessionTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "PublicFolderSession.{0}. The public folder session is not connected. IsConnected = {1}.", methodName, base.IsConnected);
					throw new InvalidOperationException("The public folder session is not connected");
				}
				flag = true;
				result = objectAccessGuard;
			}
			finally
			{
				if (!flag)
				{
					objectAccessGuard.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000159B4 File Offset: 0x00013BB4
		internal override MailboxEvaluationResult EvaluateFolderRules(ICoreItem item, ProxyAddress recipientProxyAddress)
		{
			MailboxEvaluationResult result2;
			using (this.CheckObjectState("EvaluateFolderRules"))
			{
				if (this.ProhibitFolderRuleEvaluation || item.Origin == Origin.Existing)
				{
					result2 = null;
				}
				else
				{
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PublicFolderRule", RegistryKeyPermissionCheck.ReadSubTree))
					{
						if (registryKey != null)
						{
							object value = registryKey.GetValue("Disabled");
							if (value != null && value is int && (int)value == 1)
							{
								ExTraceGlobals.SessionTracer.TraceDebug(0L, "Public folder rule evaluation is disabled with registry key override!");
								return null;
							}
						}
					}
					MailboxEvaluationResult result = null;
					try
					{
						GrayException.MapAndReportGrayExceptions(delegate()
						{
							StoreObjectId valueOrDefault = item.PropertyBag.GetValueOrDefault<StoreObjectId>(InternalSchema.ParentItemId);
							if (valueOrDefault == null || this.GetInternalSubmissionFolderId() == valueOrDefault)
							{
								return;
							}
							item.PropertyBag.Load(InternalSchema.ContentConversionProperties);
							bool valueOrDefault2 = item.PropertyBag.GetValueOrDefault<bool>(InternalSchema.IsAssociated);
							BodyFormat format = item.Body.Format;
							if (valueOrDefault2)
							{
								return;
							}
							int valueOrDefault3 = item.PropertyBag.GetValueOrDefault<int>(InternalSchema.Size);
							using (DisposeGuard disposeGuard = default(DisposeGuard))
							{
								PFRuleEvaluationContext pfruleEvaluationContext = PFRuleEvaluationContext.Create(valueOrDefault, recipientProxyAddress, item, (long)valueOrDefault3, this);
								disposeGuard.Add<PFRuleEvaluationContext>(pfruleEvaluationContext);
								RuleEvaluator ruleEvaluator = new RuleEvaluator(pfruleEvaluationContext);
								result = ruleEvaluator.Evaluate();
								disposeGuard.Success();
							}
						}, new GrayException.IsGrayExceptionDelegate(PublicFolderSession.IsGrayException));
					}
					catch (GrayException arg)
					{
						ExTraceGlobals.SessionTracer.TraceError<GrayException>((long)this.GetHashCode(), "PublicFolderSession::EvaluateFolderRules: GrayException {0}", arg);
					}
					result2 = result;
				}
			}
			return result2;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00015B5C File Offset: 0x00013D5C
		internal override void ExecuteFolderRulesOnAfter(MailboxEvaluationResult evaluationResult)
		{
			using (this.CheckObjectState("ExecuteFolderRulesOnAfter"))
			{
				if (evaluationResult != null)
				{
					try
					{
						GrayException.MapAndReportGrayExceptions(delegate()
						{
							evaluationResult.Context.Message.Load(InternalSchema.ContentConversionProperties);
							evaluationResult.Context.DeliveredMessage = evaluationResult.Context.Message;
							evaluationResult.Execute(ExecutionStage.OnPublicFolderAfter);
						}, new GrayException.IsGrayExceptionDelegate(PublicFolderSession.IsGrayException));
					}
					catch (GrayException arg)
					{
						ExTraceGlobals.SessionTracer.TraceError<GrayException>((long)this.GetHashCode(), "PublicFolderSession::ExecuteFolderRulesOnAfter: GrayException {0}", arg);
					}
				}
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00015C10 File Offset: 0x00013E10
		internal override FolderRuleEvaluationStatus ExecuteFolderRulesOnBefore(MailboxEvaluationResult evaluationResult)
		{
			FolderRuleEvaluationStatus result;
			using (this.CheckObjectState("ExecuteFolderRulesOnBefore"))
			{
				FolderRuleEvaluationStatus folderRuleEvaluationStatus = FolderRuleEvaluationStatus.Continue;
				if (evaluationResult != null)
				{
					try
					{
						if (evaluationResult.TargetFolder == null)
						{
							folderRuleEvaluationStatus = FolderRuleEvaluationStatus.InterruptWithException;
							foreach (FolderEvaluationResult folderEvaluationResult in evaluationResult.FolderResults)
							{
								if (folderEvaluationResult.WorkItems.Count > 0)
								{
									folderRuleEvaluationStatus = FolderRuleEvaluationStatus.InterruptSilently;
									break;
								}
							}
						}
						GrayException.MapAndReportGrayExceptions(delegate()
						{
							evaluationResult.Execute(ExecutionStage.OnPublicFolderBefore);
						}, new GrayException.IsGrayExceptionDelegate(PublicFolderSession.IsGrayException));
					}
					catch (GrayException arg)
					{
						ExTraceGlobals.SessionTracer.TraceError<GrayException>((long)this.GetHashCode(), "PublicFolderSession::ExecuteFolderRulesOnBefore: GrayException {0}", arg);
					}
				}
				result = folderRuleEvaluationStatus;
			}
			return result;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00015D2E File Offset: 0x00013F2E
		public StoreObjectId GetPublicFolderRootId()
		{
			return this.GetFolderId(() => base.Mailbox.MapiStore.GetRootFolderEntryId(), "GetPublicFolderRootId");
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00015D59 File Offset: 0x00013F59
		public StoreObjectId GetIpmSubtreeFolderId()
		{
			return this.GetFolderId(() => base.Mailbox.MapiStore.GetDeferredActionFolderEntryId(), "GetIpmSubtreeFolderId");
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00015D84 File Offset: 0x00013F84
		public StoreObjectId GetNonIpmSubtreeFolderId()
		{
			return this.GetFolderId(() => base.Mailbox.MapiStore.GetSpoolerQueueFolderEntryId(), "GetNonIpmSubtreeFolderId");
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00015DAF File Offset: 0x00013FAF
		public StoreObjectId GetEFormsRegistryFolderId()
		{
			return this.GetFolderId(() => base.Mailbox.MapiStore.GetIpmSubtreeFolderEntryId(), "GetEFormsRegistryFolderId");
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00015DDA File Offset: 0x00013FDA
		public StoreObjectId GetAsyncDeleteStateFolderId()
		{
			return this.GetFolderId(() => base.Mailbox.MapiStore.GetSentItemsFolderEntryId(), "GetAsyncDeleteStateFolderId");
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00015E05 File Offset: 0x00014005
		public StoreObjectId GetDumpsterRootFolderId()
		{
			return this.GetFolderId(() => base.Mailbox.MapiStore.GetInboxFolderEntryId(), "GetDumpsterRootFolderId");
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00015E30 File Offset: 0x00014030
		public StoreObjectId GetInternalSubmissionFolderId()
		{
			return this.GetFolderId(() => base.Mailbox.MapiStore.GetDeletedItemsFolderEntryId(), "GetInternalSubmissionFolderId");
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00015E5B File Offset: 0x0001405B
		public StoreObjectId GetTombstonesRootFolderId()
		{
			return this.GetFolderId(() => base.Mailbox.MapiStore.GetOutboxFolderEntryId(), "GetTombstonesRootFolderId");
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00015E74 File Offset: 0x00014074
		public static string ConvertToLegacyDN(string contentMailboxInfo, string entryId)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}CN={1}/CN={2}", new object[]
			{
				"/CN=Mail Public Folder/CN=Version_1_0/",
				contentMailboxInfo,
				entryId
			});
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00015EA8 File Offset: 0x000140A8
		public static LocalizedString GetNoPublicFoldersProvisionedError(OrganizationId organizationId)
		{
			if (organizationId.ConfigurationUnit != null)
			{
				return ServerStrings.PublicFoldersNotEnabledForTenant(organizationId.OrganizationalUnit.Name);
			}
			return ServerStrings.PublicFoldersNotEnabledForEnterprise;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00015EC8 File Offset: 0x000140C8
		internal override MapiProp GetMapiProp(StoreObjectId id, OpenEntryFlags flags)
		{
			return base.GetMapiProp(IdConverter.IsFolderId(id) ? base.IdConverter.GetSessionSpecificId(id) : id, flags);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00015EE8 File Offset: 0x000140E8
		public override ADSessionSettings GetADSessionSettings()
		{
			ADSessionSettings result;
			using (base.CreateSessionGuard("GetADSessionSettings"))
			{
				ADSessionSettings adsessionSettings = this.publicFolderMailboxPrincipal.MailboxInfo.OrganizationId.ToADSessionSettings();
				adsessionSettings.AccountingObject = base.AccountingObject;
				result = adsessionSettings;
			}
			return result;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00015F48 File Offset: 0x00014148
		internal override void CheckDeleteItemFlags(DeleteItemFlags flags)
		{
			using (base.CreateSessionGuard("CheckDeleteItemFlags"))
			{
				base.CheckDeleteItemFlags(flags);
				if (flags == DeleteItemFlags.MoveToDeletedItems)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00015F94 File Offset: 0x00014194
		internal override bool OnBeforeFolderChange(FolderChangeOperation operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, CallbackContext callbackContext)
		{
			bool result;
			using (base.CreateSessionGuard("OnBeforeFolderChange"))
			{
				base.OnBeforeFolderChange(operation, flags, sourceSession, destinationSession, sourceFolderId, destinationFolderId, itemIds, callbackContext);
				if (this.copyOnWriteNotification != null)
				{
					result = this.copyOnWriteNotification.OnBeforeFolderChange(operation, flags, sourceSession, destinationSession, sourceFolderId, destinationFolderId, itemIds, callbackContext);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00016008 File Offset: 0x00014208
		internal override GroupOperationResult GetCallbackResults()
		{
			GroupOperationResult callbackResults;
			using (base.CreateSessionGuard("GetCallbackResults"))
			{
				if (this.copyOnWriteNotification != null)
				{
					callbackResults = this.copyOnWriteNotification.GetCallbackResults();
				}
				else
				{
					callbackResults = base.GetCallbackResults();
				}
			}
			return callbackResults;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0001606C File Offset: 0x0001426C
		public DisposableFrame GetRestrictedOperationToken()
		{
			DisposableFrame result;
			using (base.CheckDisposed("GetRestrictedOperationToken"))
			{
				this.restrictedOperationOverride = true;
				result = new DisposableFrame(delegate()
				{
					this.restrictedOperationOverride = false;
				});
			}
			return result;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000160C8 File Offset: 0x000142C8
		internal override void ValidateOperation(FolderChangeOperation folderOperation, StoreObjectId folderId)
		{
			using (this.CheckObjectState("ValidateOperation"))
			{
				if (!this.IsSystemOperation())
				{
					base.ValidateOperation(folderOperation, folderId);
					bool flag;
					switch (folderOperation)
					{
					case FolderChangeOperation.Copy:
						break;
					case FolderChangeOperation.Move:
						flag = (this.IsPrimaryHierarchySession && !this.IsWellKnownFolder(folderId));
						goto IL_57;
					default:
						if (folderOperation != FolderChangeOperation.Empty)
						{
							flag = this.IsPrimaryHierarchySession;
							goto IL_57;
						}
						break;
					}
					flag = false;
					IL_57:
					if (!flag)
					{
						throw new AccessDeniedException(ServerStrings.PublicFolderOperationDenied("FolderChangeOperation", folderOperation.ToString()));
					}
				}
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001616C File Offset: 0x0001436C
		internal override bool IsValidOperation(ICoreObject coreObject, PropertyDefinition property, out PropertyErrorCode? error)
		{
			bool result;
			using (base.CheckDisposed("IsValidOperation"))
			{
				Util.ThrowOnNullArgument(coreObject, "coreObject");
				Util.ThrowOnNullArgument(property, "property");
				error = null;
				if (this.IsSystemOperation())
				{
					result = true;
				}
				else
				{
					if (PublicFolderSession.SystemProperties.Contains(property))
					{
						error = new PropertyErrorCode?(PropertyErrorCode.AccessDenied);
					}
					if (this.GetTombstonesRootFolderId().Equals(coreObject.StoreObjectId) && property == CoreFolderSchema.AclTableAndSecurityDescriptor)
					{
						error = new PropertyErrorCode?(PropertyErrorCode.AccessDenied);
					}
					result = (error == null);
				}
			}
			return result;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001621C File Offset: 0x0001441C
		internal bool IsSystemOperation()
		{
			bool result;
			using (base.CheckDisposed("IsSystemOperation"))
			{
				if (this.restrictedOperationOverride || base.IsMoveUser || this.isContentIndexing)
				{
					result = true;
				}
				else if (base.LogonType == LogonType.Admin)
				{
					result = (base.ClientInfoString.StartsWith("Client=Management", StringComparison.OrdinalIgnoreCase) || base.ClientInfoString.StartsWith("Client=TBA", StringComparison.OrdinalIgnoreCase));
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x000162A8 File Offset: 0x000144A8
		public override OrganizationId OrganizationId
		{
			get
			{
				OrganizationId organizationId;
				using (base.CheckDisposed("OrganizationId.get"))
				{
					organizationId = this.publicFolderMailboxPrincipal.MailboxInfo.OrganizationId;
				}
				return organizationId;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000162F4 File Offset: 0x000144F4
		public override IExchangePrincipal MailboxOwner
		{
			get
			{
				IExchangePrincipal result;
				using (base.CheckDisposed("MailboxOwner.get"))
				{
					result = this.publicFolderMailboxPrincipal;
				}
				return result;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00016338 File Offset: 0x00014538
		public IExchangePrincipal MailboxPrincipal
		{
			get
			{
				IExchangePrincipal result;
				using (base.CheckDisposed("MailboxPrincipal.get"))
				{
					result = this.publicFolderMailboxPrincipal;
				}
				return result;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0001637C File Offset: 0x0001457C
		public bool IsPrimaryHierarchySession
		{
			get
			{
				bool result;
				using (base.CheckDisposed("IsPrimaryHierarchySession.get"))
				{
					result = this.isPrimaryHierarchySession;
				}
				return result;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002CA RID: 714 RVA: 0x000163C0 File Offset: 0x000145C0
		public override bool IsPublicFolderSession
		{
			get
			{
				bool result;
				using (base.CheckDisposed("IsPublicFolderSession.get"))
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002CB RID: 715 RVA: 0x000163FC File Offset: 0x000145FC
		public override string DisplayAddress
		{
			get
			{
				string legacyDn;
				using (base.CheckDisposed("DisplayAddress.get"))
				{
					legacyDn = this.publicFolderMailboxPrincipal.LegacyDn;
				}
				return legacyDn;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00016444 File Offset: 0x00014644
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00016488 File Offset: 0x00014688
		public override ExTimeZone ExTimeZone
		{
			get
			{
				ExTimeZone result;
				using (base.CheckDisposed("ExTimeZone.get"))
				{
					result = this.exTimeZone;
				}
				return result;
			}
			set
			{
				using (base.CheckDisposed("ExTimeZone.set"))
				{
					Util.ThrowOnNullArgument(value, "ExTimeZone");
					this.exTimeZone = value;
				}
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000164D4 File Offset: 0x000146D4
		public override Guid MailboxGuid
		{
			get
			{
				Guid mailboxGuid;
				using (base.CheckDisposed("MailboxGuid.get"))
				{
					mailboxGuid = this.publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid;
				}
				return mailboxGuid;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00016520 File Offset: 0x00014720
		public override Guid MdbGuid
		{
			get
			{
				Guid databaseGuid;
				using (base.CheckDisposed("MdbGuid.get"))
				{
					databaseGuid = this.publicFolderMailboxPrincipal.MailboxInfo.GetDatabaseGuid();
				}
				return databaseGuid;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0001656C File Offset: 0x0001476C
		public override string ServerFullyQualifiedDomainName
		{
			get
			{
				string serverFqdn;
				using (base.CheckDisposed("ServerFullyQualifiedDomainName.get"))
				{
					serverFqdn = this.publicFolderMailboxPrincipal.MailboxInfo.Location.ServerFqdn;
				}
				return serverFqdn;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x000165BC File Offset: 0x000147BC
		public override string GccResourceIdentifier
		{
			get
			{
				string result;
				using (base.CheckDisposed("GccResourceIdentifier.get"))
				{
					if (this.globalCriminalComplianceResourceName == null)
					{
						string str = (this.ServerFullyQualifiedDomainName == null) ? "[unknown]" : this.ServerFullyQualifiedDomainName.ToString();
						this.globalCriminalComplianceResourceName = "<Public Folders in server " + str + ">";
					}
					result = this.globalCriminalComplianceResourceName;
				}
				return result;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00016638 File Offset: 0x00014838
		public Participant ConnectAsParticipant
		{
			get
			{
				Participant result;
				using (base.CheckDisposed("ConnectAsParticipant.get"))
				{
					result = this.connectAsParticipant;
				}
				return result;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0001667C File Offset: 0x0001487C
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00016684 File Offset: 0x00014884
		internal bool ProhibitFolderRuleEvaluation { get; set; }

		// Token: 0x060002D5 RID: 725 RVA: 0x00016690 File Offset: 0x00014890
		internal static ClientIdentityInfo FromClientSecurityContext(OrganizationId organizationId, ClientSecurityContext clientSecurityContext)
		{
			Util.ThrowOnNullArgument(clientSecurityContext, "clientSecurityContext");
			Util.ThrowOnNullArgument(organizationId, "organizationId");
			if (clientSecurityContext.ClientContextHandle == null)
			{
				throw new ArgumentException("clientSecurityContext.ClientContextHandle");
			}
			if (clientSecurityContext.UserSid.IsWellKnown(WellKnownSidType.LocalSystemSid))
			{
				return new ClientIdentityInfo(clientSecurityContext.ClientContextHandle.DangerousGetHandle(), clientSecurityContext.UserSid, new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null));
			}
			ClientIdentityInfo clientIdentityInfo = StoreSession.FromAuthZContext(organizationId.ToADSessionSettings(), clientSecurityContext.ClientContextHandle);
			if (clientIdentityInfo == null)
			{
				throw new ObjectNotFoundException(ServerStrings.UserCannotBeFoundFromContext(Marshal.GetLastWin32Error()));
			}
			return clientIdentityInfo;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0001671C File Offset: 0x0001491C
		private void InternalConnect()
		{
			try
			{
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					MapiStore mapiStore = null;
					bool flag = false;
					try
					{
						if (this != null)
						{
							this.BeginMapiCall();
							this.BeginServerHealthCall();
							flag = true;
						}
						if (StorageGlobals.MapiTestHookBeforeCall != null)
						{
							StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
						}
						byte[] tenantHint = StoreSession.GetTenantHint(this.publicFolderMailboxPrincipal);
						WindowsIdentity windowsIdentity = this.identity as WindowsIdentity;
						if (windowsIdentity != null)
						{
							mapiStore = MapiStore.OpenMailbox(this.ServerFullyQualifiedDomainName, this.userLegacyDn, this.MailboxGuid, this.MdbGuid, null, null, null, this.connectFlag, base.StoreFlag, this.InternalPreferedCulture, windowsIdentity, base.ClientInfoString, tenantHint);
						}
						else
						{
							mapiStore = MapiStore.OpenMailbox(this.ServerFullyQualifiedDomainName, this.userLegacyDn, this.MailboxGuid, this.MdbGuid, null, null, null, this.connectFlag, base.StoreFlag, this.InternalPreferedCulture, this.identity as ClientIdentityInfo, base.ClientInfoString, tenantHint);
						}
						disposeGuard.Add<MapiStore>(mapiStore);
					}
					catch (MapiPermanentException ex)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex, this, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("PublicFolderSession.InternalConnect: ServerFullyQualifiedDomainName = {0}, UserLegacyDN = {1}.", this.ServerFullyQualifiedDomainName, this.userLegacyDn),
							ex
						});
					}
					catch (MapiRetryableException ex2)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.userLegacyDn), ex2, this, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("PublicFolderSession.InternalConnect: ServerFullyQualifiedDomainName = {0}, UserLegacyDN = {1}.", this.ServerFullyQualifiedDomainName, this.userLegacyDn),
							ex2
						});
					}
					finally
					{
						try
						{
							if (this != null)
							{
								this.EndMapiCall();
								if (flag)
								{
									this.EndServerHealthCall();
								}
							}
						}
						finally
						{
							if (StorageGlobals.MapiTestHookAfterCall != null)
							{
								StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
							}
						}
					}
					base.SetMailboxStoreObject(MailboxStoreObject.Bind(this, mapiStore, MailboxSchema.Instance.AllProperties));
					disposeGuard.Success();
				}
				base.IsConnected = true;
			}
			finally
			{
				ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), "PublicFolderSession.InternalConnect: Operation {0}. UserLegacyDN = {1}, MailboxGuid = {2}, ServerFullyQualifiedDomainName = {3}.", new object[]
				{
					base.IsConnected ? "succeeded" : "failed",
					this.userLegacyDn,
					this.MailboxGuid,
					this.ServerFullyQualifiedDomainName
				});
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000169F8 File Offset: 0x00014BF8
		private bool TryGetServiceUserLegacyDn(out string serviceUserLegacyDn)
		{
			serviceUserLegacyDn = null;
			if (!this.publicFolderMailboxPrincipal.MailboxInfo.IsRemote)
			{
				serviceUserLegacyDn = Server.GetSystemAttendantLegacyDN(LegacyDN.Parse(this.publicFolderMailboxPrincipal.MailboxInfo.Location.ServerLegacyDn)).ToString();
				return true;
			}
			return false;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00016A38 File Offset: 0x00014C38
		private StoreObjectId GetFolderId(PublicFolderSession.MapiEntryIdDelegator getMapiEntryId, string methodName)
		{
			StoreObjectId result;
			using (this.CheckObjectState(methodName))
			{
				ExTraceGlobals.SessionTracer.TraceDebug<string, Mailbox>((long)this.GetHashCode(), "PublicFolderSession.{0}: Mailbox = {1}", methodName, base.Mailbox);
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					result = StoreObjectId.FromProviderSpecificIdOrNull(getMapiEntryId());
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenFolder, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("PublicFolderSession.{0}", methodName),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenFolder, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("PublicFolderSession.{0}", methodName),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00016B90 File Offset: 0x00014D90
		private void RaiseAlarm(int hierarchyConnectionCount)
		{
			EventNotificationItem eventNotificationItem = new EventNotificationItem(ExchangeComponent.PublicFolders.Name, "PublicFolderMailboxConnectionCount", string.Empty, ResultSeverityLevel.Error);
			eventNotificationItem.CustomProperties["OrganizationId"] = this.OrganizationId.ToString();
			eventNotificationItem.CustomProperties["MailboxDisplayName"] = this.MailboxPrincipal.MailboxInfo.DisplayName;
			eventNotificationItem.CustomProperties["MailboxGuid"] = this.MailboxGuid.ToString();
			eventNotificationItem.CustomProperties["HierarchyConnectionCount"] = hierarchyConnectionCount.ToString();
			eventNotificationItem.CustomProperties["TotalConnectionCount"] = PublicFolderMailboxSynchronizerManager.Instance.GetActiveReferenceCount(this.MailboxPrincipal).ToString();
			eventNotificationItem.StateAttribute1 = this.MailboxPrincipal.MailboxInfo.DisplayName;
			eventNotificationItem.StateAttribute2 = hierarchyConnectionCount.ToString();
			eventNotificationItem.StateAttribute3 = ((this.OrganizationId.OrganizationalUnit != null) ? this.OrganizationId.OrganizationalUnit.Name : string.Empty);
			try
			{
				eventNotificationItem.Publish(false);
				PublicFolderMailboxSynchronizerManager.Instance.SetAlertIssuedTime(this.MailboxPrincipal, ExDateTime.UtcNow);
			}
			catch (UnauthorizedAccessException arg)
			{
				ExTraceGlobals.SessionTracer.TraceError<UnauthorizedAccessException>((long)this.GetHashCode(), "PublicFolderSession::IssueAlert: UnauthorizedAccessException {0}", arg);
			}
			catch (EventLogNotFoundException arg2)
			{
				ExTraceGlobals.SessionTracer.TraceError<EventLogNotFoundException>((long)this.GetHashCode(), "PublicFolderSession::IssueAlert: EventLogNotFoundException {0}", arg2);
			}
		}

		// Token: 0x040000A9 RID: 169
		internal const string SyncMailPublicFolderGuid = "e71f13d1-0178-42a7-8c47-24206de84a77";

		// Token: 0x040000AA RID: 170
		private const string MailEnabledPublicFolderLegacyDNPrefix = "/CN=Mail Public Folder/CN=Version_1_0/";

		// Token: 0x040000AB RID: 171
		private const string RegKeyStringPublicFolderRule = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PublicFolderRule";

		// Token: 0x040000AC RID: 172
		private const string RegValueStringPublicFolderRuleDisabled = "Disabled";

		// Token: 0x040000AD RID: 173
		internal const string RegKeyPublicFolder = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PublicFolder";

		// Token: 0x040000AE RID: 174
		private const string RegValuePerMailboxActiveUserLimit = "PerMailboxActiveUserLimit";

		// Token: 0x040000AF RID: 175
		private const int DefaultPerMailboxActiveUserLimit = 2000;

		// Token: 0x040000B0 RID: 176
		public static Guid HierarchyMailboxGuidAlias = Guid.Empty;

		// Token: 0x040000B1 RID: 177
		private static readonly TimeSpan minTimeToRaiseAlert = EnhancedTimeSpan.FromDays(1.0);

		// Token: 0x040000B2 RID: 178
		internal static readonly ReadOnlyCollection<PropertyDefinition> SystemProperties = new ReadOnlyCollection<PropertyDefinition>(new PropertyDefinition[]
		{
			InternalSchema.DisablePerUserRead,
			InternalSchema.ProxyGuid,
			InternalSchema.MailEnabled,
			InternalSchema.OverallAgeLimit,
			InternalSchema.RetentionAgeLimit,
			InternalSchema.PfQuotaStyle,
			InternalSchema.PfOverHardQuotaLimit,
			InternalSchema.PfStorageQuota,
			InternalSchema.PfMsgSizeLimit,
			InternalSchema.EformsLocaleId,
			InternalSchema.DeletedItemsEntryId,
			InternalSchema.ReplicaListBinary,
			InternalSchema.LastMovedTimeStamp,
			InternalSchema.PublicFolderDumpsterHolderEntryId
		});

		// Token: 0x040000B3 RID: 179
		private IExchangePrincipal publicFolderMailboxPrincipal;

		// Token: 0x040000B4 RID: 180
		private Participant connectAsParticipant;

		// Token: 0x040000B5 RID: 181
		private ExTimeZone exTimeZone = ExTimeZone.UtcTimeZone;

		// Token: 0x040000B6 RID: 182
		private string globalCriminalComplianceResourceName;

		// Token: 0x040000B7 RID: 183
		private bool restrictedOperationOverride;

		// Token: 0x040000B8 RID: 184
		private readonly bool isContentIndexing;

		// Token: 0x040000B9 RID: 185
		private PublicFolderCOWSession copyOnWriteNotification;

		// Token: 0x040000BA RID: 186
		private PublicFolderMailboxSynchronizerReference publicFolderMailboxSynchronizerReference;

		// Token: 0x040000BB RID: 187
		private readonly bool isPrimaryHierarchySession;

		// Token: 0x040000BC RID: 188
		private bool disposeWindowsIdentity;

		// Token: 0x040000BD RID: 189
		private static int? perMailboxHierarchyAccessUserLimit;

		// Token: 0x02000019 RID: 25
		// (Invoke) Token: 0x060002E7 RID: 743
		private delegate byte[] MapiEntryIdDelegator();

		// Token: 0x0200001A RID: 26
		// (Invoke) Token: 0x060002EB RID: 747
		private delegate void MethodDelegator();
	}
}
