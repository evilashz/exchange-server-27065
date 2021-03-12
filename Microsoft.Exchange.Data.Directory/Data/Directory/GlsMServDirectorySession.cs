using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200013F RID: 319
	internal class GlsMServDirectorySession : IGlobalDirectorySession
	{
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x0003D611 File Offset: 0x0003B811
		internal static GlsLookupMode GlsLookupMode
		{
			get
			{
				return GlsMServDirectorySession.glsLookupMode.Value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0003D61D File Offset: 0x0003B81D
		internal static bool ShouldScanAllForests
		{
			get
			{
				return GlsMServDirectorySession.shouldScanAllForests.Value;
			}
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0003D648 File Offset: 0x0003B848
		internal GlsMServDirectorySession(string redirectFormat)
		{
			this.glsSession = new Lazy<GlsDirectorySession>(() => new GlsDirectorySession(), true);
			this.mservSession = new Lazy<MServDirectorySession>(() => new MServDirectorySession(redirectFormat), true);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0003D6D0 File Offset: 0x0003B8D0
		public string GetRedirectServer(string memberName)
		{
			string fqdn = null;
			this.ExecuteGlobalRead(delegate(IGlobalDirectorySession session)
			{
				fqdn = session.GetRedirectServer(memberName);
				return true;
			});
			return fqdn;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0003D728 File Offset: 0x0003B928
		public bool TryGetRedirectServer(string memberName, out string fqdn)
		{
			string outfqdn = null;
			bool result = this.ExecuteGlobalRead((IGlobalDirectorySession session) => session.TryGetRedirectServer(memberName, out outfqdn));
			fqdn = outfqdn;
			return result;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0003D784 File Offset: 0x0003B984
		public string GetRedirectServer(Guid orgGuid)
		{
			string fqdn = null;
			this.ExecuteGlobalRead(delegate(IGlobalDirectorySession session)
			{
				fqdn = session.GetRedirectServer(orgGuid);
				return true;
			});
			return fqdn;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0003D7DC File Offset: 0x0003B9DC
		public bool TryGetRedirectServer(Guid orgGuid, out string fqdn)
		{
			string outfqdn = null;
			bool result = this.ExecuteGlobalRead((IGlobalDirectorySession session) => session.TryGetRedirectServer(orgGuid, out outfqdn));
			fqdn = outfqdn;
			return result;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0003D83C File Offset: 0x0003BA3C
		public bool TryGetDomainFlag(string domainFqdn, GlsDomainFlags flag, out bool value)
		{
			bool outValue = false;
			bool result = this.ExecuteGlobalRead((IGlobalDirectorySession session) => session.TryGetDomainFlag(domainFqdn, flag, out outValue));
			value = outValue;
			return result;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0003D8C0 File Offset: 0x0003BAC0
		public void SetDomainFlag(string domainFqdn, GlsDomainFlags flag, bool value)
		{
			this.ExecuteGlobalWrite(delegate(IGlobalDirectorySession session)
			{
				session.SetDomainFlag(domainFqdn, flag, value);
			}, delegate(IGlobalDirectorySession session)
			{
				session.SetDomainFlag(domainFqdn, flag, !value);
			});
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0003D928 File Offset: 0x0003BB28
		public bool TryGetTenantFlag(Guid externalDirectoryOrganizationId, GlsTenantFlags tenantFlags, out bool value)
		{
			bool outValue = false;
			bool result = this.ExecuteGlobalRead((IGlobalDirectorySession session) => session.TryGetTenantFlag(externalDirectoryOrganizationId, tenantFlags, out outValue));
			value = outValue;
			return result;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0003D9AC File Offset: 0x0003BBAC
		public void SetTenantFlag(Guid externalDirectoryOrganizationId, GlsTenantFlags tenantFlags, bool value)
		{
			this.ExecuteGlobalWrite(delegate(IGlobalDirectorySession session)
			{
				session.SetTenantFlag(externalDirectoryOrganizationId, tenantFlags, value);
			}, delegate(IGlobalDirectorySession session)
			{
				session.SetTenantFlag(externalDirectoryOrganizationId, tenantFlags, !value);
			});
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0003DA34 File Offset: 0x0003BC34
		public void AddTenant(Guid externalDirectoryOrganizationId, string resourceForestFqdn, string accountForestFqdn, string smtpNextHopDomain, GlsTenantFlags tenantFlags, string tenantContainerCN)
		{
			this.ExecuteGlobalWrite(delegate(IGlobalDirectorySession session)
			{
				session.AddTenant(externalDirectoryOrganizationId, resourceForestFqdn, accountForestFqdn, smtpNextHopDomain, tenantFlags, tenantContainerCN);
			}, delegate(IGlobalDirectorySession session)
			{
				session.RemoveTenant(externalDirectoryOrganizationId);
			});
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0003DA92 File Offset: 0x0003BC92
		public void AddTenant(Guid externalDirectoryOrganizationId, CustomerType tenantType, string ffoRegion, string ffoVersion)
		{
			throw new NotSupportedException("AddTenant for FFO properties only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0003DAC0 File Offset: 0x0003BCC0
		public void AddMSAUser(string msaUserNetID, string msaUserMemberName, Guid externalDirectoryOrganizationId)
		{
			this.ExecuteGlobalWriteNoUndo(delegate(IGlobalDirectorySession session)
			{
				session.AddMSAUser(msaUserNetID, msaUserMemberName, externalDirectoryOrganizationId);
			}, true);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0003DB30 File Offset: 0x0003BD30
		public void UpdateTenant(Guid externalDirectoryOrganizationId, string resourceForestFqdn, string accountForestFqdn, string smtpNextHopDomain, GlsTenantFlags tenantFlags, string tenantContainerCN)
		{
			this.ExecuteGlobalWriteNoUndo(delegate(IGlobalDirectorySession session)
			{
				session.UpdateTenant(externalDirectoryOrganizationId, resourceForestFqdn, accountForestFqdn, smtpNextHopDomain, tenantFlags, tenantContainerCN);
			});
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0003DBA4 File Offset: 0x0003BDA4
		public void UpdateMSAUser(string msaUserNetID, string msaUserMemberName, Guid externalDirectoryOrganizationId)
		{
			this.ExecuteGlobalWriteNoUndo(delegate(IGlobalDirectorySession session)
			{
				session.UpdateMSAUser(msaUserNetID, msaUserMemberName, externalDirectoryOrganizationId);
			}, true);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0003DBF8 File Offset: 0x0003BDF8
		public void RemoveTenant(Guid externalDirectoryOrganizationId)
		{
			this.ExecuteGlobalWriteNoUndo(delegate(IGlobalDirectorySession session)
			{
				session.RemoveTenant(externalDirectoryOrganizationId);
			});
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0003DC3C File Offset: 0x0003BE3C
		public void RemoveMSAUser(string msaUserNetID)
		{
			this.ExecuteGlobalWriteNoUndo(delegate(IGlobalDirectorySession session)
			{
				session.RemoveMSAUser(msaUserNetID);
			}, true);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0003DC69 File Offset: 0x0003BE69
		public bool TryGetTenantType(Guid externalDirectoryOrganizationId, out CustomerType tenantType)
		{
			throw new NotSupportedException("TryGetTenantType only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0003DCCC File Offset: 0x0003BECC
		public bool TryGetTenantForestsByDomain(string domainFqdn, out Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string smtpNextHopDomain, out string tenantContainerCN, out bool dataFromOfflineService)
		{
			string outResourceForestFqdn = null;
			string outAccountForestFqdn = null;
			Guid outExternalDirectoryOrganizationId = Guid.Empty;
			string outsmtpNextHopDomain = null;
			string outTenantContainerCN = null;
			bool dataFromMserv = false;
			bool outDataFromOfflineService = false;
			bool result = this.ExecuteGlobalRead(delegate(IGlobalDirectorySession session)
			{
				dataFromMserv = (session is MServDirectorySession);
				return session.TryGetTenantForestsByDomain(domainFqdn, out outExternalDirectoryOrganizationId, out outResourceForestFqdn, out outAccountForestFqdn, out outsmtpNextHopDomain, out outTenantContainerCN, out outDataFromOfflineService);
			});
			resourceForestFqdn = outResourceForestFqdn;
			accountForestFqdn = outAccountForestFqdn;
			externalDirectoryOrganizationId = outExternalDirectoryOrganizationId;
			smtpNextHopDomain = outsmtpNextHopDomain;
			tenantContainerCN = outTenantContainerCN;
			dataFromOfflineService = outDataFromOfflineService;
			return result;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0003DD98 File Offset: 0x0003BF98
		public bool TryGetTenantForestsByOrgGuid(Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string tenantContainerCN, out bool dataFromOfflineService)
		{
			string outResourceForestFqdn = null;
			string outAccountForestFqdn = null;
			string outTenantContainerCN = null;
			bool outDataFromOfflineService = false;
			bool result = this.ExecuteGlobalRead((IGlobalDirectorySession session) => session.TryGetTenantForestsByOrgGuid(externalDirectoryOrganizationId, out outResourceForestFqdn, out outAccountForestFqdn, out outTenantContainerCN, out outDataFromOfflineService));
			resourceForestFqdn = outResourceForestFqdn;
			accountForestFqdn = outAccountForestFqdn;
			tenantContainerCN = outTenantContainerCN;
			dataFromOfflineService = outDataFromOfflineService;
			return result;
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0003DE34 File Offset: 0x0003C034
		public bool TryGetTenantForestsByMSAUserNetID(string msaUserNetID, out Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string tenantContainerCN)
		{
			string outResourceForestFqdn = null;
			string outAccountForestFqdn = null;
			string outTenantContainerCN = null;
			Guid outExternalDirectoryOrganizationId = Guid.Empty;
			bool result = this.ExecuteGlobalRead((IGlobalDirectorySession session) => session.TryGetTenantForestsByMSAUserNetID(msaUserNetID, out outExternalDirectoryOrganizationId, out outResourceForestFqdn, out outAccountForestFqdn, out outTenantContainerCN), true);
			externalDirectoryOrganizationId = outExternalDirectoryOrganizationId;
			resourceForestFqdn = outResourceForestFqdn;
			accountForestFqdn = outAccountForestFqdn;
			tenantContainerCN = outTenantContainerCN;
			return result;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0003DEC8 File Offset: 0x0003C0C8
		public bool TryGetMSAUserMemberName(string msaUserNetID, out string msaUserMemberName)
		{
			string outMSAUserMemberName = null;
			bool result = this.ExecuteGlobalRead((IGlobalDirectorySession session) => session.TryGetMSAUserMemberName(msaUserNetID, out outMSAUserMemberName), true);
			msaUserMemberName = outMSAUserMemberName;
			return result;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0003DF28 File Offset: 0x0003C128
		public void SetAccountForest(Guid externalDirectoryOrganizationId, string value, string tenantContainerCN = null)
		{
			this.ExecuteGlobalWriteNoUndo(delegate(IGlobalDirectorySession session)
			{
				session.SetAccountForest(externalDirectoryOrganizationId, value, tenantContainerCN);
			});
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0003DF80 File Offset: 0x0003C180
		public void SetResourceForest(Guid externalDirectoryOrganizationId, string value)
		{
			this.ExecuteGlobalWriteNoUndo(delegate(IGlobalDirectorySession session)
			{
				session.SetAccountForest(externalDirectoryOrganizationId, value, null);
			});
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0003DFB3 File Offset: 0x0003C1B3
		public void SetTenantVersion(Guid externalDirectoryOrganizationId, string newTenantVersion)
		{
			throw new NotSupportedException("SetTenantVersion only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0003DFBF File Offset: 0x0003C1BF
		public bool TryGetTenantDomains(Guid externalDirectoryOrganizationId, out string[] acceptedDomainFqdns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0003DFC6 File Offset: 0x0003C1C6
		public void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, bool isInitialDomain)
		{
			this.AddAcceptedDomain(externalDirectoryOrganizationId, domainFqdn, isInitialDomain, false, false);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0003DFF0 File Offset: 0x0003C1F0
		public void UpdateAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn)
		{
			this.ExecuteGlobalWriteNoUndo(delegate(IGlobalDirectorySession session)
			{
				session.UpdateAcceptedDomain(externalDirectoryOrganizationId, domainFqdn);
			});
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0003E068 File Offset: 0x0003C268
		public void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, bool isInitialDomain, bool nego2Enabled, bool oauth2ClientProfileEnabled)
		{
			this.ExecuteGlobalWrite(delegate(IGlobalDirectorySession session)
			{
				session.AddAcceptedDomain(externalDirectoryOrganizationId, domainFqdn, isInitialDomain, nego2Enabled, oauth2ClientProfileEnabled);
			}, delegate(IGlobalDirectorySession session)
			{
				session.RemoveAcceptedDomain(externalDirectoryOrganizationId, domainFqdn);
			});
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0003E0BE File Offset: 0x0003C2BE
		public void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, bool isInitialDomain, string ffoRegion, string ffoServiceVersion)
		{
			throw new NotSupportedException("AddAcceptedDomain for FFO properties only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0003E0CC File Offset: 0x0003C2CC
		public void RemoveAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn)
		{
			bool flag = GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.BothGLSAndMServ || GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.GlsOnly || GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.MServReadsDisabled;
			if (flag && this.glsSession.Value.DomainExists(domainFqdn))
			{
				this.glsSession.Value.RemoveAcceptedDomain(externalDirectoryOrganizationId, domainFqdn);
			}
			bool flag2 = GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.BothGLSAndMServ || GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.MServReadsDisabled || GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.MServOnly;
			if (flag2)
			{
				this.mservSession.Value.RemoveAcceptedDomain(externalDirectoryOrganizationId, domainFqdn);
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0003E14C File Offset: 0x0003C34C
		public void SetDomainVersion(Guid externalDirectoryOrganizationId, string domainFqdn, string newDomainVersion)
		{
			throw new NotSupportedException("SetDomainVersion only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0003E158 File Offset: 0x0003C358
		public IEnumerable<string> GetDomainNamesProvisionedByEXO(IEnumerable<SmtpDomain> domains)
		{
			throw new NotSupportedException("GetDomainNamesProvisionedByEXO only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0003E164 File Offset: 0x0003C364
		public IAsyncResult BeginGetFfoTenantAttributionPropertiesByDomain(SmtpDomain domain, object clientAsyncState, AsyncCallback clientCallback)
		{
			throw new NotSupportedException("BeginGetFfoTenantAttributionPropertiesByDomain only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0003E170 File Offset: 0x0003C370
		public bool TryEndGetFfoTenantAttributionPropertiesByDomain(IAsyncResult asyncResult, out string ffoRegion, out string ffoVersion, out Guid externalDirectoryOrganizationId, out string exoNextHop, out CustomerType tenantType, out DomainIPv6State ipv6Enabled, out string exoResourceForest, out string exoAccountForest, out string exoTenantContainer)
		{
			throw new NotSupportedException("TryEndGetFfoTenantAttributionPropertiesByDomain only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0003E17C File Offset: 0x0003C37C
		public IAsyncResult BeginGetFfoTenantAttributionPropertiesByOrgId(Guid externalDirectoryOrganizationId, object clientAsyncState, AsyncCallback clientCallback)
		{
			throw new NotSupportedException("BeginGetFfoTenantAttributionPropertiesByOrgId only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003E188 File Offset: 0x0003C388
		public bool TryEndGetFfoTenantAttributionPropertiesByOrgId(IAsyncResult asyncResult, out string ffoRegion, out string exoNextHop, out CustomerType tenantType, out string exoResourceForest, out string exoAccountForest, out string exoTenantContainer)
		{
			throw new NotSupportedException("TryEndGetFfoTenantAttributionPropertiesByOrgId only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003E194 File Offset: 0x0003C394
		public bool TryGetFfoTenantProvisioningProperties(Guid externalDirectoryOrganizationId, out string version, out CustomerType tenantType, out string region)
		{
			throw new NotSupportedException("TryGetFfoTenantProvisioningProperties only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003E1A0 File Offset: 0x0003C3A0
		public bool TenantExists(Guid externalDirectoryOrganizationId, Namespace namespaceToCheck)
		{
			throw new NotSupportedException("TenantExists only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003E1CC File Offset: 0x0003C3CC
		public bool MSAUserExists(string msaUserNetID)
		{
			bool msaUserExists = false;
			this.ExecuteGlobalRead(delegate(IGlobalDirectorySession session)
			{
				msaUserExists = session.MSAUserExists(msaUserNetID);
				return true;
			}, true);
			return msaUserExists;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0003E208 File Offset: 0x0003C408
		private static GlsLookupMode InitializeLookupMode()
		{
			string value = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(GlsMServDirectorySession.RegistryKey))
				{
					value = ((registryKey != null) ? ((string)registryKey.GetValue(GlsMServDirectorySession.GlobalDirectoryLookupTypeValue, null)) : null);
				}
			}
			catch (SecurityException ex)
			{
				ExTraceGlobals.GLSTracer.TraceError<string>(0L, "SecurityException: {0}", ex.Message);
			}
			catch (UnauthorizedAccessException ex2)
			{
				ExTraceGlobals.GLSTracer.TraceError<string>(0L, "UnauthorizedAccessException: {0}", ex2.Message);
			}
			GlsLookupMode result;
			if (Enum.TryParse<GlsLookupMode>(value, true, out result))
			{
				return result;
			}
			if (DatacenterRegistry.IsForefrontForOffice())
			{
				return GlsLookupMode.GlsOnly;
			}
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 694, "InitializeLookupMode", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\GlsMServDirectorySession.cs");
			ADSite localSite = topologyConfigurationSession.GetLocalSite();
			if (localSite.DistinguishedName.EndsWith("DC=extest,DC=microsoft,DC=com"))
			{
				return GlsLookupMode.MServOnly;
			}
			return GlsLookupMode.BothGLSAndMServ;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0003E300 File Offset: 0x0003C500
		private static bool InitializeScanMode()
		{
			string a = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(GlsMServDirectorySession.RegistryKey))
				{
					a = ((registryKey != null) ? ((string)registryKey.GetValue(GlsMServDirectorySession.GlobalDirectoryScanTypeValue, null)) : null);
				}
			}
			catch (SecurityException ex)
			{
				ExTraceGlobals.GLSTracer.TraceError<string>(0L, "SecurityException: {0}", ex.Message);
			}
			catch (UnauthorizedAccessException ex2)
			{
				ExTraceGlobals.GLSTracer.TraceError<string>(0L, "UnauthorizedAccessException: {0}", ex2.Message);
			}
			return a != "0";
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0003E3B4 File Offset: 0x0003C5B4
		private bool ExecuteGlobalRead(GlobalLookup lookup)
		{
			return this.ExecuteGlobalRead(lookup, false);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003E3C0 File Offset: 0x0003C5C0
		private bool ExecuteGlobalRead(GlobalLookup lookup, bool skipMServRead)
		{
			bool flag = false;
			bool flag2 = (GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.BothGLSAndMServ || GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.MServOnly) && !skipMServRead;
			bool flag3 = GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.BothGLSAndMServ || GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.GlsOnly || GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.MServReadsDisabled;
			if (flag3)
			{
				try
				{
					flag = lookup(this.glsSession.Value);
				}
				catch (GlsTenantNotFoundException ex)
				{
					if (!flag2)
					{
						throw;
					}
				}
				if (flag || !flag2)
				{
					return flag;
				}
			}
			return flag2 && lookup(this.mservSession.Value);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0003E450 File Offset: 0x0003C650
		private void ExecuteGlobalWriteNoUndo(GlobalWrite action)
		{
			this.ExecuteGlobalWrite(action, null, false);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003E45B File Offset: 0x0003C65B
		private void ExecuteGlobalWriteNoUndo(GlobalWrite action, bool skipWriteToMServ)
		{
			this.ExecuteGlobalWrite(action, null, skipWriteToMServ);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0003E466 File Offset: 0x0003C666
		private void ExecuteGlobalWrite(GlobalWrite action, GlobalWrite undoAction)
		{
			this.ExecuteGlobalWrite(action, undoAction, false);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0003E474 File Offset: 0x0003C674
		private void ExecuteGlobalWrite(GlobalWrite action, GlobalWrite undoAction, bool skipWriteToMServ)
		{
			bool flag = (GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.BothGLSAndMServ || GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.MServReadsDisabled || GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.MServOnly) && !skipWriteToMServ;
			bool flag2 = GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.BothGLSAndMServ || GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.GlsOnly || GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.MServReadsDisabled;
			if (flag2)
			{
				action(this.glsSession.Value);
			}
			if (flag)
			{
				bool flag3 = true;
				try
				{
					action(this.mservSession.Value);
					flag3 = false;
				}
				finally
				{
					if (flag3 && undoAction != null && flag2)
					{
						undoAction(this.glsSession.Value);
					}
				}
			}
		}

		// Token: 0x040006F7 RID: 1783
		private static readonly string RegistryKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x040006F8 RID: 1784
		private static readonly string GlobalDirectoryScanTypeValue = "GlobalDirectoryScanType";

		// Token: 0x040006F9 RID: 1785
		private static readonly string GlobalDirectoryLookupTypeValue = "GlobalDirectoryLookupType";

		// Token: 0x040006FA RID: 1786
		private static readonly Lazy<GlsLookupMode> glsLookupMode = new Lazy<GlsLookupMode>(new Func<GlsLookupMode>(GlsMServDirectorySession.InitializeLookupMode), LazyThreadSafetyMode.PublicationOnly);

		// Token: 0x040006FB RID: 1787
		private static readonly Lazy<bool> shouldScanAllForests = new Lazy<bool>(new Func<bool>(GlsMServDirectorySession.InitializeScanMode), LazyThreadSafetyMode.PublicationOnly);

		// Token: 0x040006FC RID: 1788
		private readonly Lazy<GlsDirectorySession> glsSession;

		// Token: 0x040006FD RID: 1789
		private readonly Lazy<MServDirectorySession> mservSession;
	}
}
