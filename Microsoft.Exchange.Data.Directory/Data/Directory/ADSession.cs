using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationCache;
using Microsoft.Exchange.Data.Directory.ValidationRules;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200007B RID: 123
	[Serializable]
	internal static class ADSession
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x0001FE40 File Offset: 0x0001E040
		public static byte[] EncodePasswordForLdap(SecureString password)
		{
			byte[] array = new byte[(password.Length + 2) * 2];
			int num = 34;
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(password);
				int num2 = 0;
				array[num2++] = (byte)(num & 255);
				array[num2++] = (byte)(num >> 8 & 255);
				Marshal.Copy(intPtr, array, num2, password.Length * 2);
				num2 += password.Length * 2;
				array[num2++] = (byte)(num & 255);
				array[num2++] = (byte)(num >> 8 & 255);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
				gchandle.Free();
			}
			return array;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001FF0C File Offset: 0x0001E10C
		public static bool IsBoundToAdam
		{
			get
			{
				return TopologyProvider.IsAdamTopology();
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001FF13 File Offset: 0x0001E113
		public static void SetAdminTopologyMode()
		{
			if (!ADSession.isAdminModeEnabled)
			{
				ExTraceGlobals.ADTopologyTracer.TraceDebug(0L, "A process tried to set the Topology Mode to Admin, but this mode has been disabled");
				return;
			}
			TopologyProvider.SetProcessTopologyMode(true, true);
			if (ADSession.IsBoundToAdam)
			{
				ADSessionSettings.ClearProcessADContext();
				ConnectionPoolManager.ForceRebuild();
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001FF46 File Offset: 0x0001E146
		public static void DisableAdminTopologyMode()
		{
			if (TopologyProvider.IsAdminMode)
			{
				ExTraceGlobals.ADTopologyTracer.TraceError(0L, "A process tried to disable Admin Topology Mode after that mode was already set");
				throw new ADOperationException(DirectoryStrings.ExceptionUnableToDisableAdminTopologyMode);
			}
			ADSession.isAdminModeEnabled = false;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001FF74 File Offset: 0x0001E174
		public static string GetConfigDC(string partitionFqdn, string serverName)
		{
			if (!Globals.IsDatacenter)
			{
				ExTraceGlobals.ADTopologyTracer.TraceDebug<string, string>(0L, "ADSession::GetConfigDC '{0}' will use local forest '{1}'", partitionFqdn, TopologyProvider.LocalForestFqdn);
				partitionFqdn = TopologyProvider.LocalForestFqdn;
			}
			string fqdn;
			using (ServiceTopologyProvider serviceTopologyProvider = new ServiceTopologyProvider(serverName))
			{
				ADServerInfo configDCInfo = serviceTopologyProvider.GetConfigDCInfo(partitionFqdn, false);
				if (configDCInfo == null)
				{
					throw new ADTransientException(DirectoryStrings.ExceptionADTopologyUnexpectedError(serverName, string.Empty));
				}
				fqdn = configDCInfo.Fqdn;
			}
			return fqdn;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001FFF0 File Offset: 0x0001E1F0
		public static string GetSharedConfigDC()
		{
			string configDC;
			using (ServiceTopologyProvider serviceTopologyProvider = new ServiceTopologyProvider())
			{
				configDC = serviceTopologyProvider.GetConfigDC(false);
			}
			return configDC;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00020028 File Offset: 0x0001E228
		public static void SetSharedConfigDC(string partitionFqdn, string serverName, int port)
		{
			using (ServiceTopologyProvider serviceTopologyProvider = new ServiceTopologyProvider())
			{
				serviceTopologyProvider.SetConfigDC(partitionFqdn, serverName, port);
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00020060 File Offset: 0x0001E260
		public static void SetCurrentConfigDC(string serverName, string partitionFqdn)
		{
			if (string.IsNullOrEmpty(serverName))
			{
				throw new ArgumentNullException("serverName");
			}
			if (string.IsNullOrEmpty(partitionFqdn))
			{
				throw new ArgumentNullException("partitionFqdn");
			}
			TopologyProvider instance = TopologyProvider.GetInstance();
			instance.SetConfigDC(partitionFqdn, serverName, instance.DefaultDCPort);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000200A7 File Offset: 0x0001E2A7
		public static string GetCurrentConfigDCForLocalForest()
		{
			return ADSession.GetCurrentConfigDC(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000200B4 File Offset: 0x0001E2B4
		public static string GetCurrentConfigDC(string partitionFqdn)
		{
			ADSessionSettings adsessionSettings;
			if (PartitionId.IsLocalForestPartition(partitionFqdn))
			{
				adsessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			}
			else
			{
				adsessionSettings = ADSessionSettings.FromAccountPartitionRootOrgScopeSet(new PartitionId(partitionFqdn));
			}
			Fqdn fqdn = adsessionSettings.ServerSettings.ConfigurationDomainController(partitionFqdn);
			if (fqdn != null)
			{
				return fqdn.ToString();
			}
			return TopologyProvider.GetInstance().GetConfigDC(partitionFqdn);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00020101 File Offset: 0x0001E301
		public static string GetCurrentConfigDC(PartitionId partitionId)
		{
			if (partitionId != null)
			{
				return ADSession.GetCurrentConfigDC(partitionId.ForestFQDN);
			}
			return ADSession.GetCurrentConfigDCForLocalForest();
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00020120 File Offset: 0x0001E320
		private static void CopySettableSessionProperties(IDirectorySession oldSession, IDirectorySession newSession)
		{
			if (oldSession != null)
			{
				newSession.UseConfigNC = oldSession.UseConfigNC;
				newSession.UseGlobalCatalog = oldSession.UseGlobalCatalog;
				newSession.EnforceDefaultScope = oldSession.EnforceDefaultScope;
				newSession.SkipRangedAttributes = oldSession.SkipRangedAttributes;
				if (object.Equals(oldSession.SessionSettings.PartitionId, newSession.SessionSettings.PartitionId))
				{
					newSession.LinkResolutionServer = oldSession.LinkResolutionServer;
				}
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00020189 File Offset: 0x0001E389
		internal static void CopySettableSessionPropertiesAndSettings(IDirectorySession oldSession, IDirectorySession newSession)
		{
			if (oldSession != null)
			{
				ADSession.CopySettableSessionProperties(oldSession, newSession);
				ADSessionSettings.CloneSettableProperties(oldSession.SessionSettings, newSession.SessionSettings);
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000201A6 File Offset: 0x0001E3A6
		internal static IDirectorySession RescopeSessionToTenantSubTree(IDirectorySession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (session.ConfigScope == ConfigScopes.TenantLocal)
			{
				return ADSession.CreateScopedSession(session, ADSessionSettings.RescopeToSubtree(session.SessionSettings));
			}
			return session;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000201D4 File Offset: 0x0001E3D4
		internal static IDirectorySession CreateScopedSession(IDirectorySession session, ADSessionSettings underSessionSettings)
		{
			bool flag = object.Equals(session.SessionSettings.PartitionId, underSessionSettings.PartitionId);
			IConfigurationSession configurationSession = session as IConfigurationSession;
			IDirectorySession directorySession;
			if (configurationSession != null)
			{
				directorySession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(flag ? configurationSession.DomainController : null, configurationSession.ReadOnly, configurationSession.ConsistencyMode, flag ? configurationSession.NetworkCredential : null, underSessionSettings, 395, "CreateScopedSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\ADSession.cs");
			}
			else
			{
				IRecipientSession recipientSession = session as IRecipientSession;
				if (recipientSession.SessionSettings.IncludeSoftDeletedObjects)
				{
					underSessionSettings.IncludeSoftDeletedObjects = true;
					directorySession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(flag ? recipientSession.DomainController : null, flag ? recipientSession.SearchRoot : null, recipientSession.Lcid, recipientSession.ReadOnly, recipientSession.ConsistencyMode, flag ? recipientSession.NetworkCredential : null, underSessionSettings, recipientSession.ConfigScope, 410, "CreateScopedSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\ADSession.cs");
				}
				else
				{
					directorySession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(flag ? recipientSession.DomainController : null, flag ? recipientSession.SearchRoot : null, recipientSession.Lcid, recipientSession.ReadOnly, recipientSession.ConsistencyMode, flag ? recipientSession.NetworkCredential : null, underSessionSettings, 422, "CreateScopedSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\ADSession.cs");
				}
				if (recipientSession.IsReducedRecipientSession())
				{
					directorySession = DirectorySessionFactory.Default.GetReducedRecipientSession((IRecipientSession)directorySession, 434, "CreateScopedSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\ADSession.cs");
				}
			}
			ADSession.CopySettableSessionProperties(session, directorySession);
			return directorySession;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00020340 File Offset: 0x0001E540
		internal static bool IsTenantIdentity(ADObjectId id, string partitionFqdn)
		{
			if (ADSession.IsBoundToAdam || id.DomainId == null)
			{
				return false;
			}
			if (!string.Equals(id.GetPartitionId().ForestFQDN, partitionFqdn, StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException("Object partition FQDN doesn't match partitionFqdn parameter.");
			}
			ADObjectId configurationNamingContext = ADSession.GetConfigurationNamingContext(partitionFqdn);
			if (id.Equals(configurationNamingContext))
			{
				return false;
			}
			ADObjectId domainNamingContext = ADSession.GetDomainNamingContext(partitionFqdn);
			if (id.Equals(domainNamingContext))
			{
				return false;
			}
			ADObjectId configurationUnitsRoot = ADSession.GetConfigurationUnitsRoot(partitionFqdn);
			if (id.IsDescendantOf(configurationUnitsRoot))
			{
				return true;
			}
			ADObjectId hostedOrganizationsRoot = ADSession.GetHostedOrganizationsRoot(partitionFqdn);
			return id.IsDescendantOf(hostedOrganizationsRoot) && !id.Equals(hostedOrganizationsRoot);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000203CF File Offset: 0x0001E5CF
		internal static bool IsLdapFilterError(ADOperationException ex)
		{
			return ex.InnerException != null && ex.InnerException is LdapException && ((LdapException)ex.InnerException).ErrorCode == 87;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000203FC File Offset: 0x0001E5FC
		public static bool TryVerifyIsWithinScopes(ADRawEntry obj, ADScope readScope, IList<ADScopeCollection> writeScopes, ADScopeCollection exclusiveScopes, IList<ValidationRule> validationRules, bool emptyObjectSessionOnException, out ADScopeException exception)
		{
			return ADSession.TryVerifyIsWithinScopes(obj, readScope, writeScopes, exclusiveScopes, validationRules, emptyObjectSessionOnException, ConfigScopes.None, out exception);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00020410 File Offset: 0x0001E610
		internal static bool TryVerifyIsWithinScopes(ADRawEntry obj, ADScope readScope, IList<ADScopeCollection> writeScopes, ADScopeCollection exclusiveScopes, IList<ValidationRule> validationRules, bool emptyObjectSessionOnException, ConfigScopes sessionScopeHint, out ADScopeException exception)
		{
			if (readScope == null)
			{
				throw new ArgumentNullException("readScope");
			}
			if (writeScopes == null)
			{
				throw new ArgumentNullException("writeScopes");
			}
			exception = null;
			bool flag;
			if (!ADSession.IsWithinScope(obj, readScope, out flag))
			{
				if (!flag || sessionScopeHint != ConfigScopes.RootOrg || ADSession.IsTenantIdentity(obj.Id, obj.Id.GetPartitionId().ForestFQDN))
				{
					if (obj is ADObject && emptyObjectSessionOnException)
					{
						((ADObject)obj).m_Session = null;
					}
					exception = new ADScopeException(DirectoryStrings.ErrorNotInReadScope(obj.Id.ToString()));
					return false;
				}
				ExTraceGlobals.ScopeVerificationTracer.TraceDebug<ADObjectId>(0L, "ADSession::TryVerifyIsWithinScopes Allowing unfilterable object '{0}' in RootOrg-scoped session to bypass filter verification", obj.Id);
			}
			bool flag2 = false;
			if (exclusiveScopes != null)
			{
				foreach (ADScope scope in exclusiveScopes)
				{
					if (ADSession.IsWithinScope(obj, scope))
					{
						flag2 = true;
						break;
					}
				}
			}
			foreach (ADScopeCollection adscopeCollection in writeScopes)
			{
				bool flag3 = false;
				foreach (ADScope adscope in adscopeCollection)
				{
					bool flag4 = false;
					bool flag5 = false;
					bool flag6 = false;
					if (adscope is RbacScope)
					{
						RbacScope rbacScope = (RbacScope)adscope;
						flag4 = rbacScope.Exclusive;
						flag5 = rbacScope.IsFromEndUserRole;
						flag6 = (rbacScope.ScopeType == ScopeType.Self);
					}
					if (!flag2 && flag4)
					{
						ExTraceGlobals.ScopeVerificationTracer.TraceDebug(0L, "ADSession::TryVerifyIsWithinScopes Ignoring scope ScopeRoot '{0}', ScopeFilter '{1}', IsWithinExclusiveScope '{2}', IsExclusive '{3}'", new object[]
						{
							(adscope.Root == null) ? "<null>" : adscope.Root.ToDNString(),
							(adscope.Filter == null) ? "<null>" : adscope.Filter.ToString(),
							flag2,
							flag4
						});
					}
					else
					{
						ADScope adscope2 = adscope;
						if (flag2 && !flag4)
						{
							if (!flag5)
							{
								ExTraceGlobals.ScopeVerificationTracer.TraceDebug(0L, "ADSession::TryVerifyIsWithinScopes Ignoring scope ScopeRoot '{0}', ScopeFilter '{1}', IsWithinExclusiveScope '{2}', IsExclusive '{3}'", new object[]
								{
									(adscope2.Root == null) ? "<null>" : adscope2.Root.ToDNString(),
									(adscope2.Filter == null) ? "<null>" : adscope2.Filter.ToString(),
									flag2,
									flag4
								});
								continue;
							}
							if (!flag6)
							{
								if (((RbacScope)adscope2).SelfFilter == null)
								{
									exception = new ADScopeException(DirectoryStrings.ExArgumentNullException("RbacScope.SelfFilter"));
									return false;
								}
								adscope2 = new RbacScope(ScopeType.Self)
								{
									Root = ((RbacScope)adscope2).SelfRoot,
									Filter = ((RbacScope)adscope2).SelfFilter
								};
							}
						}
						if (ADSession.IsWithinScope(obj, adscope2))
						{
							flag3 = true;
							break;
						}
					}
				}
				if (!flag3)
				{
					if (obj is ADObject && emptyObjectSessionOnException)
					{
						((ADObject)obj).m_Session = null;
					}
					exception = new ADScopeException(DirectoryStrings.ErrorNoWriteScope(obj.Id.ToString()));
					return false;
				}
			}
			if (validationRules != null)
			{
				RuleValidationException ex = null;
				foreach (ValidationRule validationRule in validationRules)
				{
					if (!validationRule.TryValidate(obj, out ex))
					{
						exception = ex;
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000207EC File Offset: 0x0001E9EC
		public static bool TryVerifyIsWithinScopes(ADRawEntry obj, ADScope readScope, IList<ADScopeCollection> writeScopes, ADScopeCollection exclusiveScopes, bool emptyObjectSessionOnException, out ADScopeException exception)
		{
			return ADSession.TryVerifyIsWithinScopes(obj, readScope, writeScopes, exclusiveScopes, null, emptyObjectSessionOnException, out exception);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000207FC File Offset: 0x0001E9FC
		public static string StringFromWkGuid(Guid wkGuid, string containerDN)
		{
			return string.Format("<WKGUID={0},{1}>", HexConverter.ByteArrayToHexString(wkGuid.ToByteArray()), containerDN);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00020818 File Offset: 0x0001EA18
		public static void VerifyIsWithinScopes(ADRawEntry obj, ADScope readScope, IList<ADScopeCollection> writeScopes, ADScopeCollection invalidScopes, IList<ValidationRule> validationRules, bool emptyObjectSessionOnException)
		{
			ADScopeException ex;
			if (!ADSession.TryVerifyIsWithinScopes(obj, readScope, writeScopes, invalidScopes, validationRules, emptyObjectSessionOnException, out ex))
			{
				throw ex;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00020838 File Offset: 0x0001EA38
		public static void VerifyIsWithinScopes(ADRawEntry obj, ADScope readScope, IList<ADScopeCollection> writeScopes, ADScopeCollection invalidScopes, bool emptyObjectSessionOnException)
		{
			ADScopeException ex;
			if (!ADSession.TryVerifyIsWithinScopes(obj, readScope, writeScopes, invalidScopes, emptyObjectSessionOnException, out ex))
			{
				throw ex;
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00020858 File Offset: 0x0001EA58
		internal static bool IsWithinScope(ADRawEntry obj, ADScope scope)
		{
			bool flag;
			return ADSession.IsWithinScope(obj, scope, out flag);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0002086E File Offset: 0x0001EA6E
		internal static bool IsWithinScope(ADRawEntry obj, ADScope scope, out bool outOfScopeBecauseOfFilter)
		{
			return ADDataSession.IsWithinScope(obj, scope, out outOfScopeBecauseOfFilter);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00020878 File Offset: 0x0001EA78
		internal static string GetAttributeNameWithRange(string ldapDisplayName, string lowerRange, string upperRange)
		{
			return string.Format("{0};Range={1}-{2}", ldapDisplayName, lowerRange, upperRange);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00020887 File Offset: 0x0001EA87
		internal static ADObjectId GetDomainNamingContext(string domainController, NetworkCredential credential)
		{
			return ADSession.GetNamingContext(ADSession.ADNamingContext.Domain, domainController, credential);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00020891 File Offset: 0x0001EA91
		internal static ADObjectId GetSchemaNamingContext(string domainController, NetworkCredential credential)
		{
			return ADSession.GetNamingContext(ADSession.ADNamingContext.Schema, domainController, credential);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0002089B File Offset: 0x0001EA9B
		internal static ADObjectId GetConfigurationNamingContext(string domainController, NetworkCredential credential)
		{
			return ADSession.GetNamingContext(ADSession.ADNamingContext.Config, domainController, credential);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000208A5 File Offset: 0x0001EAA5
		internal static ADObjectId GetRootDomainNamingContext(string domainController, NetworkCredential credential)
		{
			return ADSession.GetNamingContext(ADSession.ADNamingContext.RootDomain, domainController, credential);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000208B0 File Offset: 0x0001EAB0
		private static ADObjectId GetNamingContext(ADSession.ADNamingContext context, string domainController, NetworkCredential credential)
		{
			PooledLdapConnection pooledLdapConnection = null;
			ADObjectId result = null;
			try
			{
				string partitionFqdn = Globals.IsMicrosoftHostedOnly ? ADServerSettings.GetPartitionFqdnFromADServerFqdn(domainController) : TopologyProvider.LocalForestFqdn;
				pooledLdapConnection = ConnectionPoolManager.GetConnection(ConnectionType.DomainController, partitionFqdn, credential, domainController, 389);
				switch (context)
				{
				case ADSession.ADNamingContext.RootDomain:
					result = ADObjectId.ParseExtendedDN(pooledLdapConnection.ADServerInfo.RootDomainNC);
					break;
				case ADSession.ADNamingContext.Domain:
					result = ADObjectId.ParseExtendedDN(pooledLdapConnection.ADServerInfo.WritableNC);
					break;
				case (ADSession.ADNamingContext)3:
					break;
				case ADSession.ADNamingContext.Config:
					result = ADObjectId.ParseExtendedDN(pooledLdapConnection.ADServerInfo.ConfigNC);
					break;
				default:
					if (context == ADSession.ADNamingContext.Schema)
					{
						result = ADObjectId.ParseExtendedDN(pooledLdapConnection.ADServerInfo.SchemaNC);
					}
					break;
				}
			}
			finally
			{
				if (pooledLdapConnection != null)
				{
					pooledLdapConnection.ReturnToPool();
				}
			}
			return result;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0002096C File Offset: 0x0001EB6C
		internal static ADObjectId GetConfigurationNamingContextForLocalForest()
		{
			return ADSession.GetConfigurationNamingContext(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00020978 File Offset: 0x0001EB78
		internal static ADObjectId GetConfigurationNamingContext(string partitionFqdn)
		{
			if (!Globals.IsDatacenter)
			{
				ExTraceGlobals.ADTopologyTracer.TraceDebug<string, string>(0L, "ADSession::GetConfigurationNamingContext '{0}' will use local forest '{1}'", partitionFqdn, TopologyProvider.LocalForestFqdn);
				partitionFqdn = TopologyProvider.LocalForestFqdn;
			}
			return TopologyProvider.GetInstance().GetConfigurationNamingContext(partitionFqdn);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000209AA File Offset: 0x0001EBAA
		internal static ADObjectId GetSchemaNamingContextForLocalForest()
		{
			return ADSession.GetSchemaNamingContext(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000209B6 File Offset: 0x0001EBB6
		internal static ADObjectId GetSchemaNamingContext(string partitionFqdn)
		{
			if (!Globals.IsDatacenter)
			{
				ExTraceGlobals.ADTopologyTracer.TraceDebug<string, string>(0L, "ADSession::GetSchemaNamingContext '{0}' will use local forest '{1}'", partitionFqdn, TopologyProvider.LocalForestFqdn);
				partitionFqdn = TopologyProvider.LocalForestFqdn;
			}
			return TopologyProvider.GetInstance().GetSchemaNamingContext(partitionFqdn);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000209E8 File Offset: 0x0001EBE8
		internal static ADObjectId GetDomainNamingContextForLocalForest()
		{
			return ADSession.GetDomainNamingContext(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000209F4 File Offset: 0x0001EBF4
		internal static ADObjectId GetDomainNamingContext(string partitionFqdn)
		{
			if (!Globals.IsDatacenter)
			{
				ExTraceGlobals.ADTopologyTracer.TraceDebug<string, string>(0L, "ADSession::GetDomainNamingContext '{0}' will use local forest '{1}'", partitionFqdn, TopologyProvider.LocalForestFqdn);
				partitionFqdn = TopologyProvider.LocalForestFqdn;
			}
			return TopologyProvider.GetInstance().GetDomainNamingContext(partitionFqdn);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00020A26 File Offset: 0x0001EC26
		internal static ADObjectId GetRootDomainNamingContext(string partitionFqdn)
		{
			if (!Globals.IsDatacenter)
			{
				ExTraceGlobals.ADTopologyTracer.TraceDebug<string, string>(0L, "ADSession::GetRootDomainNamingContext '{0}' will use local forest '{1}'", partitionFqdn, TopologyProvider.LocalForestFqdn);
				partitionFqdn = TopologyProvider.LocalForestFqdn;
			}
			return TopologyProvider.GetInstance().GetRootDomainNamingContext(partitionFqdn);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00020A58 File Offset: 0x0001EC58
		internal static ADObjectId GetRootDomainNamingContextForLocalForest()
		{
			return ADSession.GetRootDomainNamingContext(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00020A64 File Offset: 0x0001EC64
		internal static ADObjectId GetDeletedObjectsContainer(ADObjectId namingContextId)
		{
			if (namingContextId == null)
			{
				throw new ArgumentNullException("namingContextId");
			}
			return namingContextId.GetChildId("Deleted Objects");
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00020A7F File Offset: 0x0001EC7F
		internal static ADObjectId GetHostedOrganizationsRootForLocalForest()
		{
			return ADSession.GetHostedOrganizationsRoot(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00020A8B File Offset: 0x0001EC8B
		internal static ADObjectId GetHostedOrganizationsRoot(string partitionFqdn)
		{
			return ADSession.GetRootDomainNamingContext(partitionFqdn).GetChildId("OU", "Microsoft Exchange Hosted Organizations");
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00020AA2 File Offset: 0x0001ECA2
		internal static ADObjectId GetMicrosoftExchangeRoot(ADObjectId configNC)
		{
			if (configNC == null)
			{
				throw new ArgumentNullException("configNC");
			}
			return configNC.GetDescendantId(new ADObjectId("CN=Microsoft Exchange,CN=Services"));
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00020AC4 File Offset: 0x0001ECC4
		public static object[][] ConvertToView(ADRawEntry[] recipients, IEnumerable<PropertyDefinition> properties)
		{
			if (recipients == null)
			{
				return null;
			}
			List<PropertyDefinition> list = new List<PropertyDefinition>(properties);
			PropertyDefinition[] propertyDefinitions = list.ToArray();
			object[][] array = new object[recipients.Length][];
			for (int i = 0; i < recipients.Length; i++)
			{
				array[i] = recipients[i].GetProperties(propertyDefinitions);
			}
			return array;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00020B08 File Offset: 0x0001ED08
		internal static bool IsTenantConfigObjectInCorrectNC(ADObjectId tenantObjectId)
		{
			return tenantObjectId == null || tenantObjectId.DomainId == null || ADSessionSettings.IsForefrontObject(tenantObjectId) || tenantObjectId.ToDNString().IndexOf("cn=configuration,dc=", StringComparison.OrdinalIgnoreCase) < 0 || !ADSession.IsTenantConfigInDomainNC(tenantObjectId.GetPartitionId().ForestFQDN);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00020B48 File Offset: 0x0001ED48
		internal static bool Diag_GetRegistryBool(RegistryKey regkey, string key, bool defaultValue)
		{
			int? num = null;
			if (regkey != null)
			{
				num = (regkey.GetValue(key) as int?);
			}
			if (num == null)
			{
				return defaultValue;
			}
			return Convert.ToBoolean(num.Value);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00020B89 File Offset: 0x0001ED89
		internal static ADObjectId GetConfigurationUnitsRootForLocalForest()
		{
			return ADSession.GetConfigurationUnitsRoot(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00020B98 File Offset: 0x0001ED98
		internal static ADObjectId GetConfigurationUnitsRoot(string partitionFqdn)
		{
			ADObjectId adobjectId = ADSession.IsTenantConfigInDomainNC(partitionFqdn) ? ADSession.GetRootDomainNamingContext(partitionFqdn) : ADSession.GetMicrosoftExchangeRoot(ADSession.GetConfigurationNamingContext(partitionFqdn));
			return adobjectId.GetChildId("CN", ADObject.ConfigurationUnits);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00020BD4 File Offset: 0x0001EDD4
		internal static bool IsTenantConfigInDomainNC(string partitionFqdn)
		{
			TenantCULocation tenantCULocation = InternalDirectoryRootOrganizationCache.GetTenantCULocation(partitionFqdn);
			if (tenantCULocation == TenantCULocation.Undefined)
			{
				if (Globals.IsDatacenter)
				{
					ADSystemConfigurationSession.GetRootOrgContainer(partitionFqdn, null, null);
					tenantCULocation = InternalDirectoryRootOrganizationCache.GetTenantCULocation(partitionFqdn);
				}
				else
				{
					tenantCULocation = TenantCULocation.ConfigNC;
					if (PartitionId.IsLocalForestPartition(partitionFqdn))
					{
						InternalDirectoryRootOrganizationCache.InitializeForestModeFlagForSetup(partitionFqdn, tenantCULocation);
					}
				}
			}
			return tenantCULocation == TenantCULocation.DomainNC;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00020C1C File Offset: 0x0001EE1C
		internal static void InitializeForestModeFlagForLocalForest()
		{
			string localForestFqdn = TopologyProvider.LocalForestFqdn;
			try
			{
				ADSession.IsTenantConfigInDomainNC(localForestFqdn);
			}
			catch (OrgContainerNotFoundException)
			{
				if (Globals.IsDatacenter)
				{
					if (Globals.IsMicrosoftHostedOnly)
					{
						InternalDirectoryRootOrganizationCache.InitializeForestModeFlagForSetup(localForestFqdn, TenantCULocation.DomainNC);
					}
					else
					{
						InternalDirectoryRootOrganizationCache.InitializeForestModeFlagForSetup(localForestFqdn, TenantCULocation.ConfigNC);
					}
				}
				else
				{
					InternalDirectoryRootOrganizationCache.InitializeForestModeFlagForSetup(localForestFqdn, TenantCULocation.ConfigNC);
				}
			}
			finally
			{
				InternalDirectoryRootOrganizationCache.GetTenantCULocation(localForestFqdn);
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00020C88 File Offset: 0x0001EE88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ShouldFilterSoftDeleteObject(ADSessionSettings sessionSettings, ADObjectId id)
		{
			return !sessionSettings.IncludeSoftDeletedObjects && !sessionSettings.IncludeInactiveMailbox && -1 != id.DistinguishedName.IndexOf(",OU=Soft Deleted Objects,", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00020CB3 File Offset: 0x0001EEB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ShouldFilterCNFObject(ADSessionSettings sessionSettings, ADObjectId id)
		{
			return !sessionSettings.IncludeCNFObject && ADSession.IsCNFObject(id);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00020CC5 File Offset: 0x0001EEC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsCNFObject(ADObjectId id)
		{
			return id.DistinguishedName.IndexOf("\\0ACNF", StringComparison.OrdinalIgnoreCase) > 0;
		}

		// Token: 0x04000266 RID: 614
		private const string CollisionObjectSig = "\\0ACNF";

		// Token: 0x04000267 RID: 615
		private static bool isAdminModeEnabled = true;

		// Token: 0x0200007C RID: 124
		internal enum ADNamingContext
		{
			// Token: 0x04000269 RID: 617
			RootDomain = 1,
			// Token: 0x0400026A RID: 618
			Domain,
			// Token: 0x0400026B RID: 619
			Config = 4,
			// Token: 0x0400026C RID: 620
			Schema = 8
		}
	}
}
