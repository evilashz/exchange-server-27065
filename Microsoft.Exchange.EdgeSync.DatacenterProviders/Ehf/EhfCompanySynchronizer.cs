using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Datacenter;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x0200001A RID: 26
	internal class EhfCompanySynchronizer : EhfSynchronizer
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00007E49 File Offset: 0x00006049
		public EhfCompanySynchronizer(EhfTargetConnection ehfConnection) : base(ehfConnection)
		{
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00007E54 File Offset: 0x00006054
		private EhfConfigTargetConnection EhfConfigConnection
		{
			get
			{
				EhfConfigTargetConnection ehfConfigTargetConnection = base.EhfConnection as EhfConfigTargetConnection;
				if (ehfConfigTargetConnection == null)
				{
					throw new InvalidOperationException("EHfCompanySynchronizer should be using EhfConfigTargetConnection. But it is using connection of type " + base.EhfConnection.GetType());
				}
				return ehfConfigTargetConnection;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007E8C File Offset: 0x0000608C
		public static TypeSynchronizer CreateTypeSynchronizer()
		{
			return new TypeSynchronizer(null, new PreDecorate(EhfCompanySynchronizer.LoadPerimeterSettings), null, null, null, null, "Perimeter Settings", Schema.Query.QueryPerimeterSettings, null, SearchScope.Subtree, EhfCompanySynchronizer.PerimeterSettingsSyncAttributes, null, false);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007EC4 File Offset: 0x000060C4
		public static bool TryGetEhfAdminSyncState(string configUnitDN, EhfADAdapter adAdapter, EhfTargetConnection targetConnection, string missingIdAction, out EhfAdminSyncState ehfAdminSyncState)
		{
			ehfAdminSyncState = null;
			string[] adminSyncPerimeterSettingsAttributes = EhfCompanySynchronizer.AdminSyncPerimeterSettingsAttributes;
			ExSearchResultEntry exSearchResultEntry;
			if (!EhfCompanySynchronizer.TryGetPerimeterConfigEntry(configUnitDN, adAdapter, targetConnection.DiagSession, missingIdAction, adminSyncPerimeterSettingsAttributes, out exSearchResultEntry))
			{
				return false;
			}
			EhfCompanyIdentity ehfCompanyIdentity = EhfCompanySynchronizer.GetEhfCompanyIdentity(configUnitDN, targetConnection.DiagSession, missingIdAction, exSearchResultEntry);
			ehfAdminSyncState = EhfAdminSyncState.Create(ehfCompanyIdentity, exSearchResultEntry, targetConnection);
			return true;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007F0C File Offset: 0x0000610C
		public void CreateOrModifyEhfCompany(ExSearchResultEntry entry)
		{
			EhfCompanyItem ehfCompanyItem = EhfCompanyItem.CreateForActive(entry, base.DiagSession, base.Config.ResellerId);
			if (ehfCompanyItem.PreviouslySynced)
			{
				if (!ehfCompanyItem.EhfConfigSyncEnabled)
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Sync to EHF is disabled for <{0}>. Trying to disable and then delete the company. CompanyId:<{1}>; CompanyGuid:<{2}>;", new object[]
					{
						ehfCompanyItem.DistinguishedName,
						ehfCompanyItem.CompanyId,
						ehfCompanyItem.GetCompanyGuid()
					});
					this.AddCompanyToDisableBatch(ehfCompanyItem);
					return;
				}
				this.AddCompanyToUpdateBatch(ehfCompanyItem);
				this.CacheEhfCompanyId(ehfCompanyItem);
				return;
			}
			else
			{
				if (!ehfCompanyItem.EhfConfigSyncEnabled)
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Sync to EHF is disabled for <{0}>. Trying to delete the company if it is already present in EHF. CompanyGuid:<{1}>;", new object[]
					{
						ehfCompanyItem.DistinguishedName,
						ehfCompanyItem.GetCompanyGuid()
					});
					this.DeleteEhfCompanyWithoutId(ehfCompanyItem);
					return;
				}
				this.AddCompanyToCreateBatch(ehfCompanyItem);
				return;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007FE0 File Offset: 0x000061E0
		public void DeleteEhfCompany(ExSearchResultEntry entry)
		{
			EhfCompanyItem ehfCompanyItem = EhfCompanyItem.CreateForTombstone(entry, base.DiagSession);
			if (ehfCompanyItem.PreviouslySynced)
			{
				this.AddCompanyToDisableBatch(ehfCompanyItem);
				return;
			}
			this.DeleteEhfCompanyWithoutId(ehfCompanyItem);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00008014 File Offset: 0x00006214
		public bool TryGetEhfCompanyIdentity(string configUnitDN, string missingIdAction, out EhfCompanyIdentity companyIdentity)
		{
			companyIdentity = null;
			if (this.configUnitsToCompanyIdentities != null && this.configUnitsToCompanyIdentities.TryGetValue(configUnitDN, out companyIdentity))
			{
				base.DiagSession.Tracer.TraceDebug<int, Guid, string>((long)base.DiagSession.GetHashCode(), "Successully retrieved cached EHF company ID {0} and company Guid {1} for ConfigUnit root DN <{2}>", companyIdentity.EhfCompanyId, companyIdentity.EhfCompanyGuid, configUnitDN);
				return true;
			}
			if (EhfCompanySynchronizer.TryGetEhfCompanyIdentity(configUnitDN, base.ADAdapter, base.EhfConnection, missingIdAction, out companyIdentity) && companyIdentity.EhfCompanyId != 0)
			{
				this.CacheEhfCompanyIdentity(configUnitDN, companyIdentity);
				base.DiagSession.Tracer.TraceDebug<int, Guid, string>((long)base.DiagSession.GetHashCode(), "Successully retrieved EHF company ID {0} and companyGuid {1} for ConfigUnit root DN <{2}>", companyIdentity.EhfCompanyId, companyIdentity.EhfCompanyGuid, configUnitDN);
				return true;
			}
			return false;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000080C8 File Offset: 0x000062C8
		public override void ClearBatches()
		{
			this.companiesToCreate = null;
			this.companiesToUpdate = null;
			this.companiesToDisable = null;
			this.companiesToDelete = null;
			this.companiesToCreateLast = null;
			this.configUnitsToCompanyIdentities = null;
			base.ClearBatches();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000080FC File Offset: 0x000062FC
		public override bool FlushBatches()
		{
			this.InvokeEhfDisableCompanies();
			this.InvokeEhfDeleteCompanies();
			this.handledAllDeletedCompanies = true;
			if (this.companiesToCreateLast != null)
			{
				foreach (EhfCompanyItem company in this.companiesToCreateLast)
				{
					this.AddCompanyToCreateBatch(company);
				}
				this.companiesToCreateLast = null;
			}
			for (int i = 0; i < 2; i++)
			{
				this.InvokeEhfCreateCompanies();
				this.InvokeEhfUpdateCompanies();
			}
			return base.FlushBatches();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00008190 File Offset: 0x00006390
		private static bool LoadPerimeterSettings(ExSearchResultEntry entry, Connection sourceConnection, TargetConnection targetConnection, object state)
		{
			return EhfSynchronizer.LoadFullEntry(entry, EhfCompanySynchronizer.PerimeterSettingsAllAttributes, (EhfTargetConnection)targetConnection);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000081A4 File Offset: 0x000063A4
		private static bool TryGetPerimeterConfigEntry(string configUnitDN, EhfADAdapter adAdapter, EdgeSyncDiag diagSession, string missingIdAction, string[] attributes, out ExSearchResultEntry perimeterSettingsEntry)
		{
			ADObjectId perimeterConfigObjectIdFromConfigUnitId = EhfTargetConnection.GetPerimeterConfigObjectIdFromConfigUnitId(new ADObjectId(configUnitDN));
			perimeterSettingsEntry = adAdapter.ReadObjectEntry(perimeterConfigObjectIdFromConfigUnitId.DistinguishedName, false, attributes);
			if (perimeterSettingsEntry == null)
			{
				diagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Unable to read Perimeter Settings object for ConfigUnit root DN <{0}>; {1}", new object[]
				{
					configUnitDN,
					missingIdAction
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000081F4 File Offset: 0x000063F4
		private static EhfCompanyIdentity GetEhfCompanyIdentity(string configUnitDN, EdgeSyncDiag diagSession, string missingIdAction, ExSearchResultEntry perimeterSettingsEntry)
		{
			int companyId;
			string text;
			if (!EhfCompanyItem.TryGetEhfCompanyId(perimeterSettingsEntry, diagSession, out companyId, out text))
			{
				if (string.IsNullOrEmpty(text))
				{
					diagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "EHF company ID is not set for tenant organization with ConfigUnit root DN <{0}>; {1}", new object[]
					{
						configUnitDN,
						missingIdAction
					});
				}
				else
				{
					diagSession.LogAndTraceError("Failure occurred while retrieving EHF company ID for tenant organization with ConfigUnit root DN <{0}>; {1}; failure details: {2}", new object[]
					{
						configUnitDN,
						missingIdAction,
						text
					});
				}
			}
			Guid objectGuid = perimeterSettingsEntry.GetObjectGuid();
			return new EhfCompanyIdentity(companyId, objectGuid);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00008268 File Offset: 0x00006468
		private static bool TryGetEhfCompanyIdentity(string configUnitDN, EhfADAdapter adAdapter, EhfTargetConnection targetConnection, string missingIdAction, out EhfCompanyIdentity ehfCompanyIdentity)
		{
			ehfCompanyIdentity = null;
			string[] perimeterSettingsCompanyIdentityAttributes = EhfCompanySynchronizer.PerimeterSettingsCompanyIdentityAttributes;
			ExSearchResultEntry perimeterSettingsEntry;
			if (!EhfCompanySynchronizer.TryGetPerimeterConfigEntry(configUnitDN, adAdapter, targetConnection.DiagSession, missingIdAction, perimeterSettingsCompanyIdentityAttributes, out perimeterSettingsEntry))
			{
				return false;
			}
			ehfCompanyIdentity = EhfCompanySynchronizer.GetEhfCompanyIdentity(configUnitDN, targetConnection.DiagSession, missingIdAction, perimeterSettingsEntry);
			return true;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000082A8 File Offset: 0x000064A8
		private void AddCompanyToCreateBatch(EhfCompanyItem company)
		{
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Adding company <{0}> to Create batch", new object[]
			{
				company.DistinguishedName
			});
			if (base.AddItemToBatch<EhfCompanyItem>(company, ref this.companiesToCreate))
			{
				this.InvokeEhfCreateCompanies();
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000082EC File Offset: 0x000064EC
		private void AddCompanyToUpdateBatch(EhfCompanyItem company)
		{
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Adding company <{0}> to Update batch", new object[]
			{
				company.DistinguishedName
			});
			if (base.AddItemToBatch<EhfCompanyItem>(company, ref this.companiesToUpdate))
			{
				this.InvokeEhfUpdateCompanies();
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00008330 File Offset: 0x00006530
		private void AddCompanyToDisableBatch(EhfCompanyItem company)
		{
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Adding company <{0}> to Disable batch", new object[]
			{
				company.DistinguishedName
			});
			if (base.AddItemToBatch<EhfCompanyItem>(company, ref this.companiesToDisable))
			{
				this.InvokeEhfDisableCompanies();
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00008374 File Offset: 0x00006574
		private void AddCompanyToDeleteBatch(EhfCompanyItem company)
		{
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Adding company <{0}> to Delete batch", new object[]
			{
				company.DistinguishedName
			});
			if (base.AddItemToBatch<EhfCompanyItem>(company, ref this.companiesToDelete))
			{
				this.InvokeEhfDeleteCompanies();
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000083E4 File Offset: 0x000065E4
		private void InvokeEhfCreateCompanies()
		{
			if (this.companiesToCreate == null || this.companiesToCreate.Count == 0)
			{
				return;
			}
			CompanyResponseInfoSet responseSet = null;
			base.InvokeProvisioningService("Create Company", delegate
			{
				responseSet = this.ProvisioningService.CreateCompanies(this.companiesToCreate);
			}, this.companiesToCreate.Count);
			List<EhfCompanyItem> list = new List<EhfCompanyItem>(base.Config.EhfSyncAppConfig.BatchSize);
			int num = 0;
			int permanentFailureCount = 0;
			for (int i = 0; i < responseSet.ResponseInfo.Length; i++)
			{
				CompanyResponseInfo companyResponseInfo = responseSet.ResponseInfo[i];
				EhfCompanyItem ehfCompanyItem = this.companiesToCreate[i];
				bool flag = false;
				if (companyResponseInfo.Status == ResponseStatus.Success)
				{
					flag = true;
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Successfully created EHF company: DN=<{0}>; Name=<{1}>; GUID=<{2}>; EHF-ID=<{3}>", new object[]
					{
						ehfCompanyItem.DistinguishedName,
						ehfCompanyItem.Company.Name,
						companyResponseInfo.CompanyGuid.Value,
						companyResponseInfo.CompanyId
					});
				}
				else if (companyResponseInfo.Fault.Id == FaultId.CompanyExistsUnderThisReseller)
				{
					Guid companyGuid = ehfCompanyItem.GetCompanyGuid();
					if (companyResponseInfo.CompanyGuid.Value.Equals(companyGuid))
					{
						flag = true;
						base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Attempted to create a company that already exists: DN=<{0}>; Name=<{1}>; GUID=<{2}>; EHF-ID=<{3}>", new object[]
						{
							ehfCompanyItem.DistinguishedName,
							ehfCompanyItem.Company.Name,
							companyGuid,
							companyResponseInfo.CompanyId
						});
					}
					else if (!this.handledAllDeletedCompanies)
					{
						base.AddItemToLazyList<EhfCompanyItem>(ehfCompanyItem, ref this.companiesToCreateLast);
						base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Attempted to create a company with the name that already exists but a different GUID: DN=<{0}>; Name=<{1}>; AD-GUID=<{2}>; EHF-GUID=<{3}>; EHF-ID=<{4}>", new object[]
						{
							ehfCompanyItem.DistinguishedName,
							ehfCompanyItem.Company.Name,
							companyGuid,
							companyResponseInfo.CompanyGuid.Value,
							companyResponseInfo.CompanyId
						});
					}
					else
					{
						this.HandleFailedCompany("Create Company", ehfCompanyItem, companyResponseInfo, ref num, ref permanentFailureCount);
					}
				}
				else
				{
					this.HandleFailedCompany("Create Company", ehfCompanyItem, companyResponseInfo, ref num, ref permanentFailureCount);
				}
				if (flag)
				{
					EhfADResultCode ehfADResultCode = ehfCompanyItem.TryStoreEhfCompanyId(companyResponseInfo.CompanyId, base.ADAdapter);
					switch (ehfADResultCode)
					{
					case EhfADResultCode.Failure:
						num++;
						break;
					case EhfADResultCode.NoSuchObject:
						base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Unable to store EHF company ID {0} in AD for Perimeter Settings object <{1}> because the AD object has been deleted; ignoring the object", new object[]
						{
							companyResponseInfo.CompanyId,
							ehfCompanyItem.DistinguishedName
						});
						break;
					case EhfADResultCode.Success:
						list.Add(ehfCompanyItem);
						this.CacheEhfCompanyId(ehfCompanyItem);
						break;
					default:
						throw new InvalidOperationException("Unexpected EhfADResultCode " + ehfADResultCode);
					}
				}
				if (!ehfCompanyItem.EventLogAndTryStoreSyncErrors(base.ADAdapter))
				{
					num++;
				}
			}
			base.HandlePerEntryFailureCounts("Create Company", this.companiesToCreate.Count, num, permanentFailureCount, true);
			this.companiesToCreate.Clear();
			foreach (EhfCompanyItem ehfCompanyItem2 in list)
			{
				base.DiagSession.Tracer.TraceDebug<int, string>((long)base.DiagSession.GetHashCode(), "Adding newly-created company ID=<{0}> DN=<{1}> to the update batch", ehfCompanyItem2.CompanyId, ehfCompanyItem2.DistinguishedName);
				this.AddCompanyToUpdateBatch(ehfCompanyItem2);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000087BC File Offset: 0x000069BC
		private void InvokeEhfUpdateCompanies()
		{
			if (this.companiesToUpdate == null || this.companiesToUpdate.Count == 0)
			{
				return;
			}
			CompanyResponseInfoSet responseSet = null;
			base.InvokeProvisioningService("Update Company", delegate
			{
				responseSet = this.ProvisioningService.UpdateCompanies(this.companiesToUpdate);
			}, this.companiesToUpdate.Count);
			int num = 0;
			int permanentFailureCount = 0;
			List<EhfCompanyItem> list = new List<EhfCompanyItem>();
			for (int i = 0; i < responseSet.ResponseInfo.Length; i++)
			{
				CompanyResponseInfo companyResponseInfo = responseSet.ResponseInfo[i];
				EhfCompanyItem ehfCompanyItem = this.companiesToUpdate[i];
				if (companyResponseInfo.Status == ResponseStatus.Success)
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Successfully updated EHF company: DN=<{0}>; GUID=<{1}>; EHF-ID=<{2}>", new object[]
					{
						ehfCompanyItem.DistinguishedName,
						companyResponseInfo.CompanyGuid.Value,
						companyResponseInfo.CompanyId
					});
					if (ehfCompanyItem.TryUpdateAdminSyncEnabledFlag(base.ADAdapter, false) != EhfADResultCode.Success)
					{
						num++;
					}
				}
				else if (companyResponseInfo.Fault.Id == FaultId.CompanyDoesNotExist)
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Attempted to update a company that does not exists in EHF: DN=<{0}>; GUID=<{1}>; EHF-ID=<{2}>", new object[]
					{
						ehfCompanyItem.DistinguishedName,
						ehfCompanyItem.GetCompanyGuid(),
						companyResponseInfo.CompanyId
					});
					list.Add(ehfCompanyItem);
				}
				else
				{
					this.HandleFailedCompany("Update Company", ehfCompanyItem, companyResponseInfo, ref num, ref permanentFailureCount);
				}
				if (!ehfCompanyItem.EventLogAndTryStoreSyncErrors(base.ADAdapter))
				{
					num++;
				}
			}
			base.HandlePerEntryFailureCounts("Create Company", this.companiesToUpdate.Count, num, permanentFailureCount, false);
			foreach (EhfCompanyItem ehfCompanyItem2 in this.companiesToUpdate)
			{
				if (ehfCompanyItem2.IsForcedDomainSyncRequired())
				{
					this.EhfConfigConnection.AddDomainsForNewCompany(ehfCompanyItem2);
				}
			}
			this.companiesToUpdate.Clear();
			foreach (EhfCompanyItem ehfCompanyItem3 in list)
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Trying to re-create the company <{0}>. CompanyId:<{1}>", new object[]
				{
					ehfCompanyItem3.DistinguishedName,
					ehfCompanyItem3.CompanyId
				});
				ehfCompanyItem3.ADEntry.Attributes.Remove("msExchTenantPerimeterSettingsOrgID");
				EhfCompanyItem company = EhfCompanyItem.CreateForActive(ehfCompanyItem3.ADEntry, base.DiagSession, base.Config.ResellerId);
				this.AddCompanyToCreateBatch(company);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00008AAC File Offset: 0x00006CAC
		private void InvokeEhfDisableCompanies()
		{
			if (this.companiesToDisable == null || this.companiesToDisable.Count == 0)
			{
				return;
			}
			CompanyResponseInfoSet responseSet = null;
			base.InvokeProvisioningService("Disable Company", delegate
			{
				responseSet = this.ProvisioningService.DisableCompanies(this.companiesToDisable);
			}, this.companiesToDisable.Count);
			List<EhfCompanyItem> list = new List<EhfCompanyItem>(base.Config.EhfSyncAppConfig.BatchSize);
			int num = 0;
			int permanentFailureCount = 0;
			for (int i = 0; i < responseSet.ResponseInfo.Length; i++)
			{
				CompanyResponseInfo companyResponseInfo = responseSet.ResponseInfo[i];
				EhfCompanyItem ehfCompanyItem = this.companiesToDisable[i];
				if (companyResponseInfo.Status == ResponseStatus.Success)
				{
					list.Add(ehfCompanyItem);
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Successfully disabled EHF company: DN=<{0}>; GUID=<{1}>; EHF-ID=<{2}>", new object[]
					{
						ehfCompanyItem.DistinguishedName,
						companyResponseInfo.CompanyGuid.Value,
						companyResponseInfo.CompanyId
					});
				}
				else if (companyResponseInfo.Fault.Id == FaultId.CompanyDoesNotExist)
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Attempted to disable a company that does not exist; ignoring the company: DN=<{0}>; GUID=<{1}>; EHF-ID=<{2}>", new object[]
					{
						ehfCompanyItem.DistinguishedName,
						ehfCompanyItem.GetCompanyGuid(),
						ehfCompanyItem.CompanyId
					});
					if (!ehfCompanyItem.ADEntry.IsDeleted)
					{
						if (ehfCompanyItem.TryClearEhfCompanyIdAndDisableAdminSync(base.ADAdapter) == EhfADResultCode.Failure)
						{
							num++;
						}
						else
						{
							this.RemoveCompanyItemFromCache(ehfCompanyItem);
						}
					}
				}
				else
				{
					this.HandleFailedCompany("Disable Company", ehfCompanyItem, companyResponseInfo, ref num, ref permanentFailureCount);
				}
				if (!ehfCompanyItem.EventLogAndTryStoreSyncErrors(base.ADAdapter))
				{
					throw new InvalidOperationException("EventLogAndTryStoreSyncErrors() returned false for a deleted object");
				}
			}
			base.HandlePerEntryFailureCounts("Disable Company", this.companiesToDisable.Count, num, permanentFailureCount, false);
			this.companiesToDisable.Clear();
			foreach (EhfCompanyItem ehfCompanyItem2 in list)
			{
				base.DiagSession.Tracer.TraceDebug<int, string>((long)base.DiagSession.GetHashCode(), "Adding disabled company ID=<{0}> DN=<{1}> to the delete batch", ehfCompanyItem2.CompanyId, ehfCompanyItem2.DistinguishedName);
				this.AddCompanyToDeleteBatch(ehfCompanyItem2);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00008D10 File Offset: 0x00006F10
		private void RemoveCompanyItemFromCache(EhfCompanyItem company)
		{
			if (this.configUnitsToCompanyIdentities != null)
			{
				this.configUnitsToCompanyIdentities.Remove(company.GetConfigUnitDN());
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00008D58 File Offset: 0x00006F58
		private void InvokeEhfDeleteCompanies()
		{
			if (this.companiesToDelete == null || this.companiesToDelete.Count == 0)
			{
				return;
			}
			CompanyResponseInfoSet responseSet = null;
			base.InvokeProvisioningService("Delete Company", delegate
			{
				responseSet = this.ProvisioningService.DeleteCompanies(this.companiesToDelete);
			}, this.companiesToDelete.Count);
			int num = 0;
			int permanentFailureCount = 0;
			for (int i = 0; i < responseSet.ResponseInfo.Length; i++)
			{
				CompanyResponseInfo companyResponseInfo = responseSet.ResponseInfo[i];
				EhfCompanyItem ehfCompanyItem = this.companiesToDelete[i];
				bool flag = false;
				if (companyResponseInfo.Status == ResponseStatus.Success)
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Successfully deleted EHF company: DN=<{0}>; GUID=<{1}>; EHF-ID=<{2}>", new object[]
					{
						ehfCompanyItem.DistinguishedName,
						companyResponseInfo.CompanyGuid.Value,
						companyResponseInfo.CompanyId
					});
					flag = true;
				}
				else if (companyResponseInfo.Fault.Id == FaultId.CompanyDoesNotExist)
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Attempted to delete a company that does not exist; ignoring the company: DN=<{0}>; GUID=<{1}>; EHF-ID=<{2}>", new object[]
					{
						ehfCompanyItem.DistinguishedName,
						ehfCompanyItem.GetCompanyGuid(),
						ehfCompanyItem.CompanyId
					});
					flag = true;
				}
				else
				{
					this.HandleFailedCompany("Delete Company", ehfCompanyItem, companyResponseInfo, ref num, ref permanentFailureCount);
					if (!ehfCompanyItem.ADEntry.IsDeleted && ehfCompanyItem.TryUpdateAdminSyncEnabledFlag(base.ADAdapter, true) == EhfADResultCode.Failure)
					{
						num++;
					}
				}
				if (!ehfCompanyItem.ADEntry.IsDeleted && flag)
				{
					if (ehfCompanyItem.TryClearEhfCompanyIdAndDisableAdminSync(base.ADAdapter) == EhfADResultCode.Failure)
					{
						num++;
					}
					else
					{
						this.RemoveCompanyItemFromCache(ehfCompanyItem);
					}
				}
				if (!ehfCompanyItem.EventLogAndTryStoreSyncErrors(base.ADAdapter))
				{
					throw new InvalidOperationException("EventLogAndTryStoreSyncErrors() returned false for a deleted object");
				}
			}
			base.HandlePerEntryFailureCounts("Delete Company", this.companiesToDelete.Count, num, permanentFailureCount, false);
			this.companiesToDelete.Clear();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00008F80 File Offset: 0x00007180
		private void DeleteEhfCompanyWithoutId(EhfCompanyItem companyItem)
		{
			Company company = null;
			bool companyExists = false;
			Guid companyGuid = companyItem.GetCompanyGuid();
			base.InvokeProvisioningService("Get Company By Guid", delegate
			{
				companyExists = this.ProvisioningService.TryGetCompanyByGuid(companyGuid, out company);
			}, 1);
			if (!companyExists)
			{
				if (!companyItem.EventLogAndTryStoreSyncErrors(base.ADAdapter))
				{
					throw new InvalidOperationException("EventLogAndTryStoreSyncErrors() returned false for a deleted object");
				}
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "EHF company for AD object <{0}> (GUID=<{1}>) has never been created; ignoring the deleted entry", new object[]
				{
					companyItem.DistinguishedName,
					companyGuid
				});
				return;
			}
			else
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "EHF company for AD object <{0}> (GUID=<{1}>) has been retrieved from EHF by GUID", new object[]
				{
					companyItem.DistinguishedName,
					companyGuid
				});
				companyItem.RecoverLostCompanyId(company);
				if (company.IsEnabled)
				{
					this.AddCompanyToDisableBatch(companyItem);
					return;
				}
				this.AddCompanyToDeleteBatch(companyItem);
				return;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00009078 File Offset: 0x00007278
		private void HandleFailedCompany(string operationName, EhfCompanyItem company, CompanyResponseInfo response, ref int transientFailureCount, ref int permanentFailureCount)
		{
			if (response.Status == ResponseStatus.TransientFailure)
			{
				transientFailureCount++;
			}
			else
			{
				permanentFailureCount++;
			}
			string text = (company.Company != null) ? company.Company.Name : "not available";
			string responseTargetValuesString = EhfProvisioningService.GetResponseTargetValuesString(response);
			EdgeSyncDiag diagSession = base.DiagSession;
			string messageFormat = "Failed to {0}: FaultId={1}; FaultType={2}; FaultDetail={3}; Target={4}; TargetValues=({5}); DN=({6}); AD-Name={7}; EHF-Name={8}; AD-GUID=({9}); EHF-GUID=({10}); EHF-ID={11}";
			object[] array = new object[12];
			array[0] = operationName;
			array[1] = response.Fault.Id;
			array[2] = response.Status;
			array[3] = (response.Fault.Detail ?? "null");
			array[4] = response.Target;
			array[5] = responseTargetValuesString;
			array[6] = company.DistinguishedName;
			array[7] = text;
			array[8] = (response.CompanyName ?? "null");
			array[9] = company.GetCompanyGuid();
			object[] array2 = array;
			int num = 10;
			Guid? companyGuid = response.CompanyGuid;
			array2[num] = ((companyGuid != null) ? companyGuid.GetValueOrDefault() : "null");
			array[11] = response.CompanyId;
			company.AddSyncError(diagSession.LogAndTraceError(messageFormat, array));
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00009191 File Offset: 0x00007391
		private void CacheEhfCompanyIdentity(string configUnitDN, int ehfCompanyId, Guid ehfCompanyGuid)
		{
			this.CacheEhfCompanyIdentity(configUnitDN, new EhfCompanyIdentity(ehfCompanyId, ehfCompanyGuid));
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000091A4 File Offset: 0x000073A4
		private void CacheEhfCompanyIdentity(string configUnitDN, EhfCompanyIdentity identity)
		{
			if (identity.EhfCompanyId == 0)
			{
				throw new ArgumentOutOfRangeException("ehfCompanyId", identity.EhfCompanyId, "Company ID must be valid");
			}
			if (identity.EhfCompanyGuid == Guid.Empty)
			{
				throw new ArgumentException("ehfCompanyGuid cannot be empty", "ehfCompanyGuid");
			}
			if (this.configUnitsToCompanyIdentities == null)
			{
				this.configUnitsToCompanyIdentities = new Dictionary<string, EhfCompanyIdentity>();
			}
			this.configUnitsToCompanyIdentities[configUnitDN] = identity;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00009216 File Offset: 0x00007416
		private void CacheEhfCompanyId(EhfCompanyItem companyItem)
		{
			this.CacheEhfCompanyIdentity(companyItem.GetConfigUnitDN(), companyItem.CompanyId, companyItem.GetCompanyGuid());
		}

		// Token: 0x04000061 RID: 97
		internal const string CreateCompanyOperation = "Create Company";

		// Token: 0x04000062 RID: 98
		internal const string UpdateCompanyOperation = "Update Company";

		// Token: 0x04000063 RID: 99
		internal const string DisableCompanyOperation = "Disable Company";

		// Token: 0x04000064 RID: 100
		internal const string DeleteCompanyOperation = "Delete Company";

		// Token: 0x04000065 RID: 101
		internal const string GetCompanyByGuidOperation = "Get Company By Guid";

		// Token: 0x04000066 RID: 102
		private static readonly string[] PerimeterSettingsSyncAttributes = new string[]
		{
			"msExchTenantPerimeterSettingsFlags",
			"msExchTenantPerimeterSettingsGatewayIPAddresses",
			"msExchTenantPerimeterSettingsInternalServerIPAddresses"
		};

		// Token: 0x04000067 RID: 103
		private static readonly string[] PerimeterSettingsExtraAttributes = new string[]
		{
			"msExchTenantPerimeterSettingsOrgID",
			"msExchTransportInboundSettings",
			"objectGUID",
			"msExchEdgeSyncCookies",
			"msExchOURoot"
		};

		// Token: 0x04000068 RID: 104
		private static readonly string[] PerimeterSettingsCompanyIdentityAttributes = new string[]
		{
			"objectGUID",
			"msExchTenantPerimeterSettingsOrgID"
		};

		// Token: 0x04000069 RID: 105
		private static readonly string[] AdminSyncPerimeterSettingsAttributes = new string[]
		{
			"objectGUID",
			"msExchTenantPerimeterSettingsOrgID",
			"msExchTargetServerAdmins",
			"msExchTargetServerViewOnlyAdmins",
			"msExchTargetServerPartnerAdmins",
			"msExchTargetServerPartnerViewOnlyAdmins"
		};

		// Token: 0x0400006A RID: 106
		private static readonly string[] PerimeterSettingsAllAttributes = EhfSynchronizer.CombineArrays<string>(EhfCompanySynchronizer.PerimeterSettingsSyncAttributes, EhfCompanySynchronizer.PerimeterSettingsExtraAttributes);

		// Token: 0x0400006B RID: 107
		private List<EhfCompanyItem> companiesToCreate;

		// Token: 0x0400006C RID: 108
		private List<EhfCompanyItem> companiesToUpdate;

		// Token: 0x0400006D RID: 109
		private List<EhfCompanyItem> companiesToDisable;

		// Token: 0x0400006E RID: 110
		private List<EhfCompanyItem> companiesToDelete;

		// Token: 0x0400006F RID: 111
		private List<EhfCompanyItem> companiesToCreateLast;

		// Token: 0x04000070 RID: 112
		private bool handledAllDeletedCompanies;

		// Token: 0x04000071 RID: 113
		private Dictionary<string, EhfCompanyIdentity> configUnitsToCompanyIdentities;
	}
}
