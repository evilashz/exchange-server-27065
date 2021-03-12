using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Mserve;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000155 RID: 341
	internal class MServDirectorySession : IGlobalDirectorySession
	{
		// Token: 0x06000E96 RID: 3734 RVA: 0x00046104 File Offset: 0x00044304
		static MServDirectorySession()
		{
			MServDirectorySession.InitializePartnerIdMap();
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0004612B File Offset: 0x0004432B
		internal MServDirectorySession(string redirectFormat)
		{
			this.redirectFormat = redirectFormat;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0004613C File Offset: 0x0004433C
		public string GetRedirectServer(string memberName)
		{
			bool flag;
			return this.GetRedirectServerFromMemberName(memberName, out flag, true);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00046154 File Offset: 0x00044354
		public bool TryGetRedirectServer(string memberName, out string fqdn)
		{
			bool flag;
			fqdn = this.GetRedirectServerFromMemberName(memberName, out flag, false);
			return flag || string.Empty != fqdn;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00046180 File Offset: 0x00044380
		public string GetRedirectServer(Guid orgGuid)
		{
			string address = string.Format("43BA6209CC0F4542958F65F8BF1CDED6@{0}.exchangereserved", orgGuid.ToString());
			int partnerId = MServDirectorySession.ReadMservEntry(address);
			bool flag;
			return this.GetRedirectServerFromPartnerId(partnerId, out flag, true);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x000461B8 File Offset: 0x000443B8
		public bool TryGetRedirectServer(Guid orgGuid, out string fqdn)
		{
			string address = string.Format("43BA6209CC0F4542958F65F8BF1CDED6@{0}.exchangereserved", orgGuid.ToString());
			int partnerId = MServDirectorySession.ReadMservEntry(address);
			bool flag;
			fqdn = this.GetRedirectServerFromPartnerId(partnerId, out flag, false);
			return flag || string.Empty != fqdn;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00046204 File Offset: 0x00044404
		public bool TryGetDomainFlag(string domainFqdn, GlsDomainFlags flag, out bool value)
		{
			string address = MServDirectorySession.EntryIdForGlsDomainFlag(domainFqdn, flag);
			int num = MServDirectorySession.ReadMservEntry(address);
			if (num == -1)
			{
				value = false;
			}
			else
			{
				value = (num > 0);
			}
			return true;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00046230 File Offset: 0x00044430
		public void SetDomainFlag(string domainFqdn, GlsDomainFlags flag, bool value)
		{
			string address = MServDirectorySession.EntryIdForGlsDomainFlag(domainFqdn, flag);
			MServDirectorySession.RemoveMserveEntry(address, value ? 0 : 1);
			MServDirectorySession.AddMserveEntry(address, value ? 1 : 0);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00046261 File Offset: 0x00044461
		public bool TryGetTenantFlag(Guid externalDirectoryOrganizationId, GlsTenantFlags tenantFlags, out bool value)
		{
			value = false;
			return false;
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00046267 File Offset: 0x00044467
		public void SetTenantFlag(Guid externalDirectoryOrganizationId, GlsTenantFlags tenantFlags, bool value)
		{
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00046269 File Offset: 0x00044469
		public void AddTenant(Guid externalDirectoryOrganizationId, string resourceForestFqdn, string accountForestFqdn, string smtpNextHopDomain, GlsTenantFlags tenantFlags, string tenantContainerCN)
		{
			this.UpdateTenantMServEntry(externalDirectoryOrganizationId, false);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00046273 File Offset: 0x00044473
		public void AddTenant(Guid externalDirectoryOrganizationId, CustomerType tenantType, string ffoRegion, string ffoVersion)
		{
			throw new NotSupportedException("AddTenant for FFO properties only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0004627F File Offset: 0x0004447F
		public void AddMSAUser(string msaUserNetID, string msaUserMemberName, Guid externalDirectoryOrganizationId)
		{
			throw new NotSupportedException("AddUser only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0004628B File Offset: 0x0004448B
		public void UpdateTenant(Guid externalDirectoryOrganizationId, string resourceForestFqdn, string accountForestFqdn, string smtpNextHopDomain, GlsTenantFlags tenantFlags, string tenantContainerCN)
		{
			this.UpdateTenantMServEntry(externalDirectoryOrganizationId, true);
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00046295 File Offset: 0x00044495
		public void UpdateMSAUser(string msaUserNetID, string msaUserMemberName, Guid externalDirectoryOrganizationId)
		{
			throw new NotSupportedException("UpdateUser only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x000462A4 File Offset: 0x000444A4
		public void RemoveTenant(Guid externalDirectoryOrganizationId)
		{
			int partnerId = this.GetLocalSite().PartnerId;
			string[] array = new string[]
			{
				string.Format("43BA6209CC0F4542958F65F8BF1CDED6@{0}.exchangereserved", externalDirectoryOrganizationId.ToString())
			};
			foreach (string address in array)
			{
				MServDirectorySession.RemoveMserveEntry(address, partnerId);
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00046304 File Offset: 0x00044504
		public void RemoveMSAUser(string msaUserNetID)
		{
			throw new NotSupportedException("RemoveUser only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00046310 File Offset: 0x00044510
		public bool TryGetTenantType(Guid externalDirectoryOrganizationId, out CustomerType tenantType)
		{
			throw new NotSupportedException("TryGetTenantType only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0004631C File Offset: 0x0004451C
		public bool TryGetTenantForestsByDomain(string domainFqdn, out Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string smtpNextHopDomain, out string tenantContainerCN)
		{
			bool flag;
			return this.TryGetTenantForestsByDomain(domainFqdn, out externalDirectoryOrganizationId, out resourceForestFqdn, out accountForestFqdn, out smtpNextHopDomain, out tenantContainerCN, out flag);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0004633C File Offset: 0x0004453C
		public bool TryGetTenantForestsByDomain(string domainFqdn, out Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string smtpNextHopDomain, out string tenantContainerCN, out bool dataFromOfflineService)
		{
			string partnerIdEntryKey = string.Format("E5CB63F56E8B4b69A1F70C192276D6AD@{0}", domainFqdn);
			resourceForestFqdn = this.GetResourceForestFqdnFromMservKey(partnerIdEntryKey);
			externalDirectoryOrganizationId = Guid.Empty;
			smtpNextHopDomain = string.Empty;
			accountForestFqdn = resourceForestFqdn;
			tenantContainerCN = null;
			dataFromOfflineService = false;
			return resourceForestFqdn != null;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00046388 File Offset: 0x00044588
		public bool TryGetTenantForestsByOrgGuid(Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string tenantContainerCN, out bool dataFromOfflineService)
		{
			string partnerIdEntryKey = string.Format("43BA6209CC0F4542958F65F8BF1CDED6@{0}.exchangereserved", externalDirectoryOrganizationId.ToString());
			resourceForestFqdn = this.GetResourceForestFqdnFromMservKey(partnerIdEntryKey);
			accountForestFqdn = resourceForestFqdn;
			tenantContainerCN = null;
			dataFromOfflineService = false;
			return resourceForestFqdn != null;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x000463CA File Offset: 0x000445CA
		public bool TryGetTenantForestsByMSAUserNetID(string msaUserNetID, out Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string tenantContainerCN)
		{
			throw new NotSupportedException("TryGetTenantForestsByMSAUserNetID only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x000463D6 File Offset: 0x000445D6
		public bool TryGetMSAUserMemberName(string msaUserNetID, out string msaUserMemberName)
		{
			throw new NotSupportedException("TryGetMSAUserMemberName only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x000463E4 File Offset: 0x000445E4
		private string GetResourceForestFqdnFromMservKey(string partnerIdEntryKey)
		{
			string result = null;
			int partnerId = MServDirectorySession.ReadMservEntry(partnerIdEntryKey);
			if (!this.TryGetForestFqdnFromPartnerId(partnerId, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00046408 File Offset: 0x00044608
		public void SetAccountForest(Guid externalDirectoryOrganizationId, string value, string tenantContainerCN = null)
		{
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0004640A File Offset: 0x0004460A
		public void SetResourceForest(Guid externalDirectoryOrganizationId, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(value);
			}
			ExTraceGlobals.MServTracer.TraceDebug<Guid, string>(0L, "SetResourceForest({0}, {1}) is a NO OP in MSERV, not making any changes", externalDirectoryOrganizationId, value);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0004642E File Offset: 0x0004462E
		public void SetTenantVersion(Guid externalDirectoryOrganizationId, string newTenantVersion)
		{
			throw new NotSupportedException("SetTenantVersion only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0004643A File Offset: 0x0004463A
		public bool TryGetTenantDomains(Guid externalDirectoryOrganizationId, out string[] acceptedDomainFqdns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00046441 File Offset: 0x00044641
		public void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, bool isInitialDomain)
		{
			this.AddAcceptedDomain(externalDirectoryOrganizationId, domainFqdn, isInitialDomain, false, false);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0004644E File Offset: 0x0004464E
		public void UpdateAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn)
		{
			this.UpdateAcceptedDomainMservEntry(externalDirectoryOrganizationId, domainFqdn, true);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00046459 File Offset: 0x00044659
		public void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, bool isInitialDomain, bool nego2Enabled, bool oauth2ClientProfileEnabled)
		{
			this.UpdateAcceptedDomainMservEntry(externalDirectoryOrganizationId, domainFqdn, false);
			if (nego2Enabled)
			{
				this.SetDomainFlag(domainFqdn, GlsDomainFlags.Nego2Enabled, nego2Enabled);
			}
			if (oauth2ClientProfileEnabled)
			{
				this.SetDomainFlag(domainFqdn, GlsDomainFlags.OAuth2ClientProfileEnabled, oauth2ClientProfileEnabled);
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00046480 File Offset: 0x00044680
		public void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, bool isInitialDomain, string ffoRegion, string ffoServiceVersion)
		{
			throw new NotSupportedException("AddAcceptedDomain for FFO properties only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0004648C File Offset: 0x0004468C
		public void RemoveAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn)
		{
			int partnerId = this.GetLocalSite().PartnerId;
			Tuple<string, int>[] array = new Tuple<string, int>[]
			{
				new Tuple<string, int>(string.Format("21668DE042684883B19BCB376E3BE474@{0}", domainFqdn), partnerId),
				new Tuple<string, int>(string.Format("ade5142cfe3d4ff19fed54a7f6087a98@{0}", domainFqdn), 0),
				new Tuple<string, int>(string.Format("0f01471e875a455a80c59def2a36ee3f@{0}", domainFqdn), 0),
				new Tuple<string, int>(string.Format("E5CB63F56E8B4b69A1F70C192276D6AD@{0}", domainFqdn), partnerId)
			};
			foreach (Tuple<string, int> tuple in array)
			{
				MServDirectorySession.RemoveMserveEntry(tuple.Item1, tuple.Item2);
			}
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0004652C File Offset: 0x0004472C
		public void SetDomainVersion(Guid externalDirectoryOrganizationId, string domainFqdn, string newDomainVersion)
		{
			throw new NotSupportedException("SetDomainVersion only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00046538 File Offset: 0x00044738
		public IEnumerable<string> GetDomainNamesProvisionedByEXO(IEnumerable<SmtpDomain> domains)
		{
			throw new NotSupportedException("GetDomainNamesProvisionedByEXO only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00046544 File Offset: 0x00044744
		public IAsyncResult BeginGetFfoTenantAttributionPropertiesByDomain(SmtpDomain domain, object clientAsyncState, AsyncCallback clientCallback)
		{
			throw new NotSupportedException("BeginGetFfoTenantAttributionPropertiesByDomain only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00046550 File Offset: 0x00044750
		public bool TryEndGetFfoTenantAttributionPropertiesByDomain(IAsyncResult asyncResult, out string ffoRegion, out string ffoVersion, out Guid externalDirectoryOrganizationId, out string exoNextHop, out CustomerType tenantType, out DomainIPv6State ipv6Enabled, out string exoResourceForest, out string exoAccountForest, out string exoTenantContainer)
		{
			throw new NotSupportedException("TryEndGetFfoTenantAttributionPropertiesByDomain only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0004655C File Offset: 0x0004475C
		public IAsyncResult BeginGetFfoTenantAttributionPropertiesByOrgId(Guid externalDirectoryOrganizationId, object clientAsyncState, AsyncCallback clientCallback)
		{
			throw new NotSupportedException("BeginGetFfoTenantAttributionPropertiesByOrgId only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00046568 File Offset: 0x00044768
		public bool TryEndGetFfoTenantAttributionPropertiesByOrgId(IAsyncResult asyncResult, out string ffoRegion, out string exoNextHop, out CustomerType tenantType, out string exoResourceForest, out string exoAccountForest, out string exoTenantContainer)
		{
			throw new NotSupportedException("TryEndGetFfoTenantAttributionPropertiesByOrgId only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00046574 File Offset: 0x00044774
		public bool TryGetFfoTenantProvisioningProperties(Guid externalDirectoryOrganizationId, out string version, out CustomerType tenantType, out string region)
		{
			throw new NotSupportedException("TryGetFfoTenantProvisioningProperties only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00046580 File Offset: 0x00044780
		public bool TenantExists(Guid externalDirectoryOrganizationId, Namespace namespaceToCheck)
		{
			throw new NotSupportedException("TenantExists only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0004658C File Offset: 0x0004478C
		public bool MSAUserExists(string msaUserNetID)
		{
			throw new NotSupportedException("MSAUserExists only supported directly through GlsDirectorySession");
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00046598 File Offset: 0x00044798
		private static void InitializePartnerIdMap()
		{
			MServDirectorySession.partnerIdToForestMap = new Dictionary<int, string>();
			MServDirectorySession.partnerIdToForestMap.Add(51003, "APCPRD01.prod.exchangelabs.com");
			MServDirectorySession.partnerIdToForestMap.Add(51012, "APCPRD02.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51021, "APCPRD03.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51022, "APCPRD04.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51023, "APCPRD05.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51024, "APCPRD06.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51025, "APCPRD07.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51026, "APCPRD08.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51002, "EURPRD01.prod.exchangelabs.com");
			MServDirectorySession.partnerIdToForestMap.Add(51007, "EURPRD02.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51013, "EURPRD03.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51014, "EURPRD04.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51015, "EURPRD05.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51016, "EURPRD06.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51017, "EURPRD07.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51000, "PROD.exchangelabs.com");
			MServDirectorySession.partnerIdToForestMap.Add(51004, "NAMPRD02.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51008, "NAMPRD03.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51009, "NAMPRD04.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51010, "NAMPRD05.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51011, "NAMPRD06.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51018, "NAMPRD07.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51019, "NAMPRD08.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51020, "NAMPRD09.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51028, "LAMPRD80.prod.outlook.com");
			MServDirectorySession.partnerIdToForestMap.Add(51005, "NAMSDF01.sdf.exchangelabs.com");
			string[] multiStringValueFromRegistry = Globals.GetMultiStringValueFromRegistry("PartnerIdToForestMappings", 0);
			foreach (string text in multiStringValueFromRegistry)
			{
				string[] array2 = text.Split(new char[]
				{
					':'
				});
				int num = -1;
				SmtpDomain smtpDomain;
				if (array2.Length != 2 || !int.TryParse(array2[0], out num) || !SmtpDomain.TryParse(array2[1], out smtpDomain))
				{
					ExTraceGlobals.MServTracer.TraceError<string>(0L, "Could not parse PartnerId registry override {0}", text);
				}
				else
				{
					ExTraceGlobals.MServTracer.TraceDebug<int, string>(0L, "Adding registry override: {0} -> {1}", num, array2[1]);
					MServDirectorySession.partnerIdToForestMap[num] = array2[1];
				}
			}
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00046850 File Offset: 0x00044A50
		private static List<RecipientSyncOperation> SyncToMserv(string address, RecipientSyncOperation operation)
		{
			int num = 0;
			int partnerId = operation.PartnerId;
			string text = (operation.AddedEntries.Count > 0) ? "Add" : ((operation.RemovedEntries.Count > 0) ? "Remove" : "Read");
			ExTraceGlobals.FaultInjectionTracer.TraceTest(4156960061U);
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2546347325U);
			List<RecipientSyncOperation> result;
			for (;;)
			{
				bool flag = false;
				int tickCount = Environment.TickCount;
				Exception ex = null;
				List<RecipientSyncOperation> list = null;
				MserveWebService mserveWebService = null;
				try
				{
					mserveWebService = EdgeSyncMservConnector.CreateDefaultMserveWebService(null);
					mserveWebService.TrackDuplicatedAddEntries = true;
					flag = true;
					ExTraceGlobals.MServTracer.TraceDebug<string, string, int>(0L, "Executing {0} for {1} with PartnerId = {2}", text, address, operation.PartnerId);
					mserveWebService.Synchronize(operation);
					list = mserveWebService.Synchronize();
					result = list;
				}
				catch (InvalidMserveRequestException ex2)
				{
					ex = ex2;
					throw new MServTransientException(DirectoryStrings.TransientMservError(ex2.Message));
				}
				catch (MserveException ex3)
				{
					ex = (ex3.InnerException ?? ex3);
					if (!MserveWebService.IsTransientException(ex3) && (ex3.InnerException == null || (!(ex3.InnerException is WebException) && !(ex3.InnerException is IOException) && !(ex3.InnerException is HttpWebRequestException) && !(ex3.InnerException is DownloadTimeoutException))))
					{
						throw new MServPermanentException(DirectoryStrings.PermanentMservError(ex.Message));
					}
					num++;
					ExTraceGlobals.MServTracer.TraceWarning(0L, "Attempt {0}: got transient exception {1} for {2} ({3})", new object[]
					{
						num,
						ex3.InnerException,
						flag ? text : "MServeWebService creation",
						address
					});
					if (num < MServDirectorySession.retriesAllowed)
					{
						continue;
					}
					throw new MServTransientException(DirectoryStrings.TransientMservError(ex.Message));
				}
				finally
				{
					if (list != null && list.Count > 0 && text == "Read")
					{
						partnerId = list[0].PartnerId;
					}
					string failure = string.Empty;
					int num2 = Environment.TickCount - tickCount;
					if (ex != null)
					{
						failure = ((ex.InnerException == null) ? ex.Message : ex.InnerException.ToString());
					}
					string diagnosticHeader = string.Empty;
					string ipAddress = string.Empty;
					string transactionId = string.Empty;
					if (mserveWebService != null)
					{
						diagnosticHeader = (mserveWebService.LastResponseDiagnosticInfo ?? string.Empty);
						ipAddress = (mserveWebService.LastIpUsed ?? string.Empty);
						transactionId = (mserveWebService.LastResponseTransactionId ?? string.Empty);
					}
					MservProtocolLog.BeginAppend(text, (ex == null) ? "Success" : "Failure", (long)num2, failure, address, partnerId.ToString(), ipAddress, diagnosticHeader, transactionId);
				}
				break;
			}
			return result;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00046B24 File Offset: 0x00044D24
		internal static int ReadMservEntry(string address)
		{
			List<RecipientSyncOperation> list = MServDirectorySession.SyncToMserv(address, new RecipientSyncOperation
			{
				ReadEntries = 
				{
					address
				}
			});
			if (list.Count <= 0)
			{
				return -1;
			}
			return list[0].PartnerId;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00046B64 File Offset: 0x00044D64
		internal static bool AddMserveEntry(string address, int value)
		{
			RecipientSyncOperation recipientSyncOperation = new RecipientSyncOperation();
			recipientSyncOperation.AddedEntries.Add(address);
			recipientSyncOperation.PartnerId = value;
			MServDirectorySession.SyncToMserv(address, recipientSyncOperation);
			return recipientSyncOperation.Synchronized;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00046B98 File Offset: 0x00044D98
		internal static bool RemoveMserveEntry(string address, int value)
		{
			try
			{
				if (MServDirectorySession.RemoveMserveEntryWithExactValue(address, value))
				{
					return true;
				}
			}
			catch (MServPermanentException)
			{
				ExTraceGlobals.MServTracer.TraceWarning<string, int>(0L, "Could not remove {0} with PartnerId {1}, will read the actual value now", address, value);
				value = MServDirectorySession.ReadMservEntry(address);
				if (value == -1)
				{
					ExTraceGlobals.MServTracer.TraceWarning<string>(0L, "Entry {0} does not exist, no need to remove", address);
					return true;
				}
			}
			if (!MServDirectorySession.RemoveMserveEntryWithExactValue(address, value))
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_CannotDeleteMServEntry, address, new object[]
				{
					address
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00046C24 File Offset: 0x00044E24
		private static bool RemoveMserveEntryWithExactValue(string address, int value)
		{
			RecipientSyncOperation recipientSyncOperation = new RecipientSyncOperation();
			recipientSyncOperation.RemovedEntries.Add(address);
			recipientSyncOperation.PartnerId = value;
			MServDirectorySession.SyncToMserv(address, recipientSyncOperation);
			return recipientSyncOperation.Synchronized;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00046C58 File Offset: 0x00044E58
		private static string EntryIdForGlsDomainFlag(string domainFqdn, GlsDomainFlags flag)
		{
			switch (flag)
			{
			case GlsDomainFlags.Nego2Enabled:
				return string.Format("ade5142cfe3d4ff19fed54a7f6087a98@{0}", domainFqdn);
			case GlsDomainFlags.OAuth2ClientProfileEnabled:
				return string.Format("0f01471e875a455a80c59def2a36ee3f@{0}", domainFqdn);
			default:
				throw new ArgumentOutOfRangeException("flag");
			}
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00046C9C File Offset: 0x00044E9C
		private static void CleanupLegacyEntries(Guid externalDirectoryOrganizationId, string domainFqdn, int partnerId)
		{
			Tuple<string, int>[] array = new Tuple<string, int>[]
			{
				new Tuple<string, int>(string.Format("7f66cd009b304aeda37ffdeea1733ff6@{0}", domainFqdn), partnerId),
				new Tuple<string, int>(string.Format("3da19c7b44a74bd3896daaf008594b6c@{0}.exchangereserved", externalDirectoryOrganizationId.ToString()), partnerId)
			};
			foreach (Tuple<string, int> tuple in array)
			{
				MServDirectorySession.RemoveMserveEntry(tuple.Item1, tuple.Item2);
			}
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00046D14 File Offset: 0x00044F14
		private string GetRedirectServerFromMemberName(string memberName, out bool alreadyInTheRightForest, bool throwExceptionsOnTenantNotFound)
		{
			string address = string.Format("E5CB63F56E8B4b69A1F70C192276D6AD@{0}", GlsDirectorySession.ParseMemberName(memberName).Domain);
			int partnerId = MServDirectorySession.ReadMservEntry(address);
			return this.GetRedirectServerFromPartnerId(partnerId, out alreadyInTheRightForest, throwExceptionsOnTenantNotFound);
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00046D4C File Offset: 0x00044F4C
		private string GetRedirectServerFromPartnerId(int partnerId, out bool alreadyInTheRightForest, bool throwExceptionsOnTenantNotFound)
		{
			alreadyInTheRightForest = false;
			if (partnerId == -1 || partnerId < 50000 || partnerId > 59999)
			{
				if (!throwExceptionsOnTenantNotFound)
				{
					return string.Empty;
				}
				if (partnerId == -1)
				{
					throw new MServTenantNotFoundException(DirectoryStrings.TenantNotFoundInMservError(partnerId.ToString()));
				}
				throw new InvalidOperationException(string.Format("The partner id {0} is out of range", partnerId));
			}
			else
			{
				if (partnerId == this.GetLocalSite().PartnerId)
				{
					ExTraceGlobals.MServTracer.TraceDebug((long)this.GetHashCode(), string.Format("The partner id {0} is the same as the current site id", partnerId));
					alreadyInTheRightForest = true;
				}
				if (string.IsNullOrEmpty(this.redirectFormat))
				{
					throw new ArgumentNullException("redirectFormat");
				}
				return string.Format(CultureInfo.InvariantCulture, this.redirectFormat, new object[]
				{
					partnerId
				});
			}
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00046E10 File Offset: 0x00045010
		internal bool TryGetForestFqdnFromPartnerId(int partnerId, out string forestFqdn)
		{
			if (MServDirectorySession.partnerIdToForestMap.TryGetValue(partnerId, out forestFqdn))
			{
				ExTraceGlobals.MServTracer.TraceDebug((long)this.GetHashCode(), string.Format("The partner id {0} mapped to forest {1}", partnerId, forestFqdn));
				return true;
			}
			ADSite adsite = this.GetLocalSite();
			if (adsite != null && partnerId == adsite.PartnerId)
			{
				ExTraceGlobals.MServTracer.TraceDebug((long)this.GetHashCode(), string.Format("The partner id {0} is the same as the current site id", partnerId));
				forestFqdn = PartitionId.LocalForest.ForestFQDN;
				return true;
			}
			return false;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00046E94 File Offset: 0x00045094
		internal bool TryGetPartnerIdFromForestFqdn(string forestFqdn, out int partnerId)
		{
			foreach (KeyValuePair<int, string> keyValuePair in MServDirectorySession.partnerIdToForestMap)
			{
				if (keyValuePair.Value.Equals(forestFqdn, StringComparison.OrdinalIgnoreCase))
				{
					partnerId = keyValuePair.Key;
					ExTraceGlobals.MServTracer.TraceDebug((long)this.GetHashCode(), string.Format("The partner id {0} mapped to forest {1}", partnerId, forestFqdn));
					return true;
				}
			}
			if (PartitionId.LocalForest.ForestFQDN.Equals(forestFqdn, StringComparison.OrdinalIgnoreCase))
			{
				ADSite adsite = this.GetLocalSite();
				if (adsite != null)
				{
					partnerId = adsite.PartnerId;
					ExTraceGlobals.MServTracer.TraceDebug((long)this.GetHashCode(), string.Format("The partner id {0} is the same as the current site id", partnerId));
					return true;
				}
			}
			partnerId = -1;
			return false;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00046F70 File Offset: 0x00045170
		private void UpdateTenantMServEntry(Guid externalDirectoryOrganizationId, bool allowOverwrite)
		{
			string mservEntryKey = string.Format("43BA6209CC0F4542958F65F8BF1CDED6@{0}.exchangereserved", externalDirectoryOrganizationId.ToString());
			this.UpdateMservEntry(externalDirectoryOrganizationId, allowOverwrite, mservEntryKey);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00046FA0 File Offset: 0x000451A0
		private void UpdateAcceptedDomainMservEntry(Guid externalDirectoryOrganizationId, string domainFqdn, bool allowOverwrite)
		{
			string mservEntryKey = string.Format("E5CB63F56E8B4b69A1F70C192276D6AD@{0}", domainFqdn);
			int partnerId = this.UpdateMservEntry(externalDirectoryOrganizationId, allowOverwrite, mservEntryKey);
			string mservEntryKey2 = string.Format("21668DE042684883B19BCB376E3BE474@{0}", domainFqdn);
			this.UpdateMservEntry(externalDirectoryOrganizationId, allowOverwrite, mservEntryKey2);
			MServDirectorySession.CleanupLegacyEntries(externalDirectoryOrganizationId, domainFqdn, partnerId);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00046FE4 File Offset: 0x000451E4
		private int UpdateMservEntry(Guid externalDirectoryOrganizationId, bool allowOverwrite, string mservEntryKey)
		{
			int partnerId = this.GetLocalSite().PartnerId;
			int num = MServDirectorySession.ReadMservEntry(mservEntryKey);
			if (num == -1)
			{
				MServDirectorySession.AddMserveEntry(mservEntryKey, partnerId);
			}
			else if (num != partnerId)
			{
				if (!allowOverwrite)
				{
					throw new MServPermanentException(DirectoryStrings.TenantAlreadyExistsInMserv(externalDirectoryOrganizationId, num, partnerId));
				}
				MServDirectorySession.RemoveMserveEntry(mservEntryKey, num);
				MServDirectorySession.AddMserveEntry(mservEntryKey, partnerId);
			}
			return num;
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00047038 File Offset: 0x00045238
		private ADSite GetLocalSite()
		{
			if (MServDirectorySession.localSite == null)
			{
				lock (MServDirectorySession.adSiteLockObject)
				{
					if (MServDirectorySession.localSite == null)
					{
						this.ReadLocalSiteAndResetSiteRefreshTime();
					}
					goto IL_77;
				}
			}
			if (MServDirectorySession.lastLocalSiteRefresh.AddHours((double)MServDirectorySession.localSiteRefreshHours) < ExDateTime.Now)
			{
				try
				{
					if (Monitor.TryEnter(MServDirectorySession.adSiteLockObject))
					{
						this.ReadLocalSiteAndResetSiteRefreshTime();
					}
				}
				finally
				{
					if (Monitor.IsEntered(MServDirectorySession.adSiteLockObject))
					{
						Monitor.Exit(MServDirectorySession.adSiteLockObject);
					}
				}
			}
			IL_77:
			return MServDirectorySession.localSite;
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x000470E0 File Offset: 0x000452E0
		private void ReadLocalSiteAndResetSiteRefreshTime()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1056, "ReadLocalSiteAndResetSiteRefreshTime", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\MServDirectorySession.cs");
			MServDirectorySession.localSite = topologyConfigurationSession.GetLocalSite();
			MServDirectorySession.lastLocalSiteRefresh = ExDateTime.Now;
		}

		// Token: 0x04000873 RID: 2163
		private const string DomainEntryAddressFormatStartNewOrganization = "21668DE042684883B19BCB376E3BE474@{0}";

		// Token: 0x04000874 RID: 2164
		internal const string DomainEntryAddressFormatMinorPartnerId = "7f66cd009b304aeda37ffdeea1733ff6@{0}";

		// Token: 0x04000875 RID: 2165
		internal const string DomainEntryAddressFormatMinorPartnerIdForOrgGuid = "3da19c7b44a74bd3896daaf008594b6c@{0}.exchangereserved";

		// Token: 0x04000876 RID: 2166
		internal const string PartnerIdRegistryOverride = "PartnerIdToForestMappings";

		// Token: 0x04000877 RID: 2167
		private const uint MServTransientExceptionLid = 4156960061U;

		// Token: 0x04000878 RID: 2168
		private const uint MServPermanentExceptionLid = 2546347325U;

		// Token: 0x04000879 RID: 2169
		private readonly string redirectFormat;

		// Token: 0x0400087A RID: 2170
		private static int retriesAllowed = 3;

		// Token: 0x0400087B RID: 2171
		private static int localSiteRefreshHours = 1;

		// Token: 0x0400087C RID: 2172
		private static ADSite localSite;

		// Token: 0x0400087D RID: 2173
		private static object adSiteLockObject = new object();

		// Token: 0x0400087E RID: 2174
		private static ExDateTime lastLocalSiteRefresh = ExDateTime.MinValue;

		// Token: 0x0400087F RID: 2175
		private static Dictionary<int, string> partnerIdToForestMap;
	}
}
