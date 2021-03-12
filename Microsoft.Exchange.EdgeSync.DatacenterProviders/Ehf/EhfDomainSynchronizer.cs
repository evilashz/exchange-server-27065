using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Datacenter;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x02000020 RID: 32
	internal class EhfDomainSynchronizer : EhfSynchronizer
	{
		// Token: 0x06000160 RID: 352 RVA: 0x0000A258 File Offset: 0x00008458
		public EhfDomainSynchronizer(EhfTargetConnection ehfConnection) : base(ehfConnection)
		{
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000A264 File Offset: 0x00008464
		public static TypeSynchronizer CreateTypeSynchronizer()
		{
			return new TypeSynchronizer(null, new PreDecorate(EhfDomainSynchronizer.LoadAcceptedDomain), null, null, null, null, "Accepted Domains", Schema.Query.QueryHostedAcceptedDomains, null, SearchScope.Subtree, EhfDomainSynchronizer.AcceptedDomainSyncAttributes, null, false);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000A29A File Offset: 0x0000849A
		protected bool HandledAllDeletedDomains
		{
			get
			{
				return this.handledAllDeletedDomains;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000A2A4 File Offset: 0x000084A4
		public void CreateOrDeleteEhfDomain(ExSearchResultEntry entry)
		{
			EhfDomainItem ehfDomainItem = EhfDomainItem.CreateForActive(entry, base.DiagSession);
			if (ehfDomainItem.AcceptedDomainType == AcceptedDomainType.Authoritative)
			{
				this.AddDomainToCreateBatch(ehfDomainItem);
				return;
			}
			this.AddDomainToDisableBatch(ehfDomainItem);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000A2D8 File Offset: 0x000084D8
		public void DeleteEhfDomain(ExSearchResultEntry entry)
		{
			EhfDomainItem domain = EhfDomainItem.CreateForTombstone(entry, base.DiagSession);
			this.AddDomainToDisableBatch(domain);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000A2FC File Offset: 0x000084FC
		public void CreateEhfDomainsForNewCompany(string transportSettingsDN, int ehfCompanyId, Guid ehfCompanyGuid)
		{
			if (ehfCompanyGuid == Guid.Empty)
			{
				throw new ArgumentException("Ehf company Guid is empty for company " + transportSettingsDN, "ehfCompanyGuid");
			}
			IEnumerable<ExSearchResultEntry> enumerable = base.ADAdapter.PagedScan(transportSettingsDN, Schema.Query.QueryAcceptedDomains, EhfDomainSynchronizer.AcceptedDomainAllAttributes);
			foreach (ExSearchResultEntry entry in enumerable)
			{
				EhfDomainSynchronizer.AddAttributeToChangeEntry(entry, "msEdgeSyncEhfCompanyGuid", ehfCompanyGuid.ToString());
				this.CreateEhfDomainForNewCompany(entry, ehfCompanyId);
			}
			if (!this.SetForcedDomainSyncOnLastDomain(ehfCompanyGuid))
			{
				try
				{
					EhfDomainItem.ClearForceDomainSyncFlagFromPerimeterConfig(base.ADAdapter, ehfCompanyGuid);
				}
				catch (ExDirectoryException ex)
				{
					if (ex.ResultCode == ResultCode.NoSuchObject)
					{
						base.DiagSession.LogAndTraceException(ex, "NoSuchObject error occurred while trying to clear the ForceDomainSync flag in Perimeter Settings for Company <{0}>:<{1}>", new object[]
						{
							transportSettingsDN,
							ehfCompanyGuid
						});
					}
					else
					{
						base.DiagSession.LogAndTraceException(ex, "Exception occurred while trying to clear the ForceDomainSync flag in Perimeter Settings for company <{0}>:<{1}>", new object[]
						{
							transportSettingsDN,
							ehfCompanyGuid
						});
						base.DiagSession.EventLog.LogEvent(EdgeSyncEventLogConstants.Tuple_EhfFailedToClearForceDomainSyncFlagFromCompanySync, null, new object[]
						{
							transportSettingsDN,
							ex.Message
						});
					}
				}
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000A454 File Offset: 0x00008654
		public override void ClearBatches()
		{
			this.domainsToCreate = null;
			this.domainsToDisable = null;
			this.domainsToDelete = null;
			this.domainsToCreateLast = null;
			base.ClearBatches();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000A478 File Offset: 0x00008678
		public override bool FlushBatches()
		{
			this.InvokeEhfDisableDomains();
			this.InvokeEhfDeleteDomains();
			this.handledAllDeletedDomains = true;
			if (this.domainsToCreateLast != null)
			{
				foreach (EhfDomainItem domain in this.domainsToCreateLast)
				{
					this.AddDomainToCreateBatch(domain);
				}
				this.domainsToCreateLast = null;
			}
			this.InvokeEhfCreateDomains();
			return base.FlushBatches();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000A4FC File Offset: 0x000086FC
		public virtual void HandleAddedDomain(ExSearchResultEntry entry)
		{
			this.CreateOrDeleteEhfDomain(entry);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000A505 File Offset: 0x00008705
		public virtual void HandleModifiedDomain(ExSearchResultEntry entry)
		{
			this.CreateOrDeleteEhfDomain(entry);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000A50E File Offset: 0x0000870E
		public virtual void HandleDeletedDomain(ExSearchResultEntry entry)
		{
			this.DeleteEhfDomain(entry);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000A518 File Offset: 0x00008718
		protected void HandleFailedDomain(string operationName, EhfDomainItem domain, DomainResponseInfo response, ref int transientFailureCount, ref int permanentFailureCount)
		{
			if (response.Status == ResponseStatus.TransientFailure)
			{
				transientFailureCount++;
			}
			else
			{
				permanentFailureCount++;
			}
			string responseTargetValuesString = EhfProvisioningService.GetResponseTargetValuesString(response);
			EdgeSyncDiag diagSession = base.DiagSession;
			string messageFormat = "Failed to {0}: FaultId={1}; FaultType={2}; FaultDetail={3}; Target={4}; TargetValues=({5}); DN=({6}); Name={7}; AD-GUID=({8}); EHF-GUID=({9}); EHF-Company-ID={10}";
			object[] array = new object[11];
			array[0] = operationName;
			array[1] = response.Fault.Id;
			array[2] = response.Status;
			array[3] = (response.Fault.Detail ?? "null");
			array[4] = response.Target;
			array[5] = responseTargetValuesString;
			array[6] = domain.DistinguishedName;
			array[7] = domain.Name;
			array[8] = domain.GetDomainGuid();
			object[] array2 = array;
			int num = 9;
			Guid? domainGuid = response.DomainGuid;
			array2[num] = ((domainGuid != null) ? domainGuid.GetValueOrDefault() : "null");
			array[10] = domain.Domain.CompanyId;
			domain.AddSyncError(diagSession.LogAndTraceError(messageFormat, array));
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000A610 File Offset: 0x00008810
		protected void AddDomainToCreateBatch(EhfDomainItem domain)
		{
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Adding domain <{0}> to Create batch", new object[]
			{
				domain.DistinguishedName
			});
			if (base.AddItemToBatch<EhfDomainItem>(domain, ref this.domainsToCreate))
			{
				this.InvokeEhfCreateDomains();
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000A654 File Offset: 0x00008854
		protected void AddDomainToDisableBatch(EhfDomainItem domain)
		{
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Adding domain <{0}>:<{1}> to Disable batch", new object[]
			{
				domain.DistinguishedName,
				domain.Domain.Name
			});
			if (base.AddItemToBatch<EhfDomainItem>(domain, ref this.domainsToDisable))
			{
				this.InvokeEhfDisableDomains();
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000A6A6 File Offset: 0x000088A6
		protected void AddDomainToCreateLastList(EhfDomainItem domain)
		{
			base.AddItemToLazyList<EhfDomainItem>(domain, ref this.domainsToCreateLast);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000A6B5 File Offset: 0x000088B5
		protected virtual EhfDomainItem CreateDomainItemForNewCompany(ExSearchResultEntry entry, int ehfCompanyId)
		{
			return EhfDomainItem.CreateIfAuthoritative(entry, ehfCompanyId, base.DiagSession);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000A6C4 File Offset: 0x000088C4
		protected virtual void HandleCreateDomainResponse(DomainResponseInfoSet responseSet, List<EhfDomainItem> domainsTriedToCreate)
		{
			int num = 0;
			int permanentFailureCount = 0;
			for (int i = 0; i < responseSet.ResponseInfo.Length; i++)
			{
				DomainResponseInfo domainResponseInfo = responseSet.ResponseInfo[i];
				EhfDomainItem ehfDomainItem = domainsTriedToCreate[i];
				bool flag = false;
				if (domainResponseInfo.Status == ResponseStatus.Success)
				{
					flag = true;
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Successfully created EHF domain: DN=<{0}>; Name=<{1}>; GUID=<{2}>; EHF-Company-ID=<{3}>", new object[]
					{
						ehfDomainItem.DistinguishedName,
						ehfDomainItem.Domain.Name,
						domainResponseInfo.DomainGuid.Value,
						ehfDomainItem.Domain.CompanyId
					});
				}
				else if (domainResponseInfo.Fault.Id == FaultId.DomainExistUnderThisCompany || domainResponseInfo.Fault.Id == FaultId.DomainExistOutsideThisCompany)
				{
					Guid domainGuid = ehfDomainItem.GetDomainGuid();
					if (domainResponseInfo.Fault.Id == FaultId.DomainExistUnderThisCompany && domainResponseInfo.DomainGuid.Value.Equals(domainGuid))
					{
						flag = true;
						base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Attempted to create a domain that already exists: DN=<{0}>; Name=<{1}>; GUID=<{2}>; EHF-Company-ID=<{3}>", new object[]
						{
							ehfDomainItem.DistinguishedName,
							ehfDomainItem.Domain.Name,
							domainGuid,
							ehfDomainItem.Domain.CompanyId
						});
					}
					else if (!this.handledAllDeletedDomains)
					{
						base.AddItemToLazyList<EhfDomainItem>(ehfDomainItem, ref this.domainsToCreateLast);
						EdgeSyncDiag diagSession = base.DiagSession;
						EdgeSyncLoggingLevel level = EdgeSyncLoggingLevel.Low;
						string messageFormat = "Attempted to create a domain with the name that already exists but a different GUID: DN=<{0}>; Name=<{1}>; AD-GUID=<{2}>; EHF-GUID=<{3}>; EHF-Company-ID=<{4}>";
						object[] array = new object[5];
						array[0] = ehfDomainItem.DistinguishedName;
						array[1] = ehfDomainItem.Name;
						array[2] = domainGuid;
						object[] array2 = array;
						int num2 = 3;
						Guid? domainGuid2 = domainResponseInfo.DomainGuid;
						array2[num2] = ((domainGuid2 != null) ? domainGuid2.GetValueOrDefault() : "null");
						array[4] = ehfDomainItem.Domain.CompanyId;
						diagSession.LogAndTraceInfo(level, messageFormat, array);
					}
					else
					{
						this.HandleFailedDomain("Create Domain", ehfDomainItem, domainResponseInfo, ref num, ref permanentFailureCount);
					}
				}
				else
				{
					this.HandleFailedDomain("Create Domain", ehfDomainItem, domainResponseInfo, ref num, ref permanentFailureCount);
				}
				if (flag && ehfDomainItem.TryClearForceDomainSyncFlagFromPerimeterConfig(base.ADAdapter) == EhfADResultCode.Failure)
				{
					num++;
				}
				if (!ehfDomainItem.EventLogAndTryStoreSyncErrors(base.ADAdapter))
				{
					num++;
				}
			}
			base.HandlePerEntryFailureCounts("Create Domain", domainsTriedToCreate.Count, num, permanentFailureCount, false);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000A92C File Offset: 0x00008B2C
		private static bool LoadAcceptedDomain(ExSearchResultEntry entry, Connection sourceConnection, TargetConnection targetConnection, object state)
		{
			EhfConfigTargetConnection ehfConfigTargetConnection = (EhfConfigTargetConnection)targetConnection;
			if (!EhfSynchronizer.LoadFullEntry(entry, EhfDomainSynchronizer.AcceptedDomainAllAttributes, ehfConfigTargetConnection))
			{
				return false;
			}
			if (!EhfDomainSynchronizer.ValidateDomainName(entry, ehfConfigTargetConnection.DiagSession))
			{
				return false;
			}
			string configUnitDN = null;
			if (!EhfDomainSynchronizer.TryGetConfigUnit(entry, ehfConfigTargetConnection.DiagSession, out configUnitDN))
			{
				return false;
			}
			EhfCompanyIdentity ehfCompanyIdentity;
			if (!ehfConfigTargetConnection.TryGetEhfCompanyIdentity(configUnitDN, "ignoring the object", out ehfCompanyIdentity))
			{
				return false;
			}
			EhfDomainSynchronizer.AddAttributeToChangeEntry(entry, "msExchTenantPerimeterSettingsOrgID", ehfCompanyIdentity.EhfCompanyId.ToString());
			EhfDomainSynchronizer.AddAttributeToChangeEntry(entry, "msEdgeSyncEhfCompanyGuid", ehfCompanyIdentity.EhfCompanyGuid.ToString());
			return true;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		private static void AddAttributeToChangeEntry(ExSearchResultEntry entry, string attributeName, string attributeValue)
		{
			DirectoryAttribute value = new DirectoryAttribute(attributeName, attributeValue);
			entry.Attributes.Add(attributeName, value);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		private static bool ValidateDomainName(ExSearchResultEntry entry, EdgeSyncDiag diagSession)
		{
			DirectoryAttribute attribute = entry.GetAttribute("msExchAcceptedDomainName");
			string text = null;
			if (attribute != null)
			{
				text = (string)attribute[0];
			}
			if (string.IsNullOrEmpty(text))
			{
				diagSession.LogAndTraceError("Accepted domain object with DN <{0}> does not contain domain name; ignoring the object", new object[]
				{
					entry.DistinguishedName
				});
				return false;
			}
			if (text.StartsWith("*", StringComparison.Ordinal))
			{
				diagSession.LogAndTraceError("Wildcard accepted domain <{0}> with DN <{1}> cannot be sync'd", new object[]
				{
					text,
					entry.DistinguishedName
				});
				return false;
			}
			if (text.Contains("_"))
			{
				diagSession.LogAndTraceError("Accepted domain <{0}> with DN <{1}> cannot be sync'd because it contains one or more underscore characters", new object[]
				{
					text,
					entry.DistinguishedName
				});
				return false;
			}
			if (!text.Contains("."))
			{
				diagSession.LogAndTraceError("Accepted domain <{0}> with DN <{1}> cannot be sync'd because it is a top level domain", new object[]
				{
					text,
					entry.DistinguishedName
				});
				return false;
			}
			if (text.Length > 255)
			{
				diagSession.LogAndTraceError("Accepted domain name <{0}> with DN <{1}> is too long; ignoring the domain", new object[]
				{
					text,
					entry.DistinguishedName
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000AB04 File Offset: 0x00008D04
		private static bool TryGetConfigUnit(ExSearchResultEntry entry, EdgeSyncDiag diagSession, out string configUnitDN)
		{
			configUnitDN = null;
			DirectoryAttribute attribute = entry.GetAttribute("msExchCU");
			if (attribute != null)
			{
				configUnitDN = (string)attribute[0];
			}
			if (string.IsNullOrEmpty(configUnitDN))
			{
				diagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "ConfigUnit root DN is not specified for accepted domain with DN <{0}>; ignoring the object", new object[]
				{
					entry.DistinguishedName
				});
				return false;
			}
			if (ExSearchResultEntry.IsDeletedDN(configUnitDN))
			{
				diagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "ConfigUnit root DN <{0}> for accepted domain with DN <{1}> indicates that tenant organization is deleted; ignoring the object", new object[]
				{
					configUnitDN,
					entry.DistinguishedName
				});
				configUnitDN = null;
				return false;
			}
			return true;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000AB8C File Offset: 0x00008D8C
		private void CreateEhfDomainForNewCompany(ExSearchResultEntry entry, int ehfCompanyId)
		{
			EhfDomainItem ehfDomainItem = this.CreateDomainItemForNewCompany(entry, ehfCompanyId);
			if (ehfDomainItem == null)
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Ignoring domain <{0}> for newly-created EHF company <{1}>", new object[]
				{
					entry.DistinguishedName,
					ehfCompanyId
				});
				return;
			}
			if (!EhfDomainSynchronizer.ValidateDomainName(entry, base.DiagSession))
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Ignoring unsupported domain <{0}> for newly-created EHF company <{1}>", new object[]
				{
					entry.DistinguishedName,
					ehfCompanyId
				});
				return;
			}
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Creating domain <{0}> for newly-created EHF company <{1}>", new object[]
			{
				entry.DistinguishedName,
				ehfCompanyId
			});
			this.AddDomainToCreateBatch(ehfDomainItem);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000AC40 File Offset: 0x00008E40
		private void AddDomainToDeleteBatch(EhfDomainItem domain)
		{
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Adding domain <{0}>:<{1}> to Delete batch", new object[]
			{
				domain.DistinguishedName,
				domain.Domain.Name
			});
			if (base.AddItemToBatch<EhfDomainItem>(domain, ref this.domainsToDelete))
			{
				this.InvokeEhfDeleteDomains();
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000ACC0 File Offset: 0x00008EC0
		protected void InvokeEhfCreateDomains()
		{
			if (this.domainsToCreate == null || this.domainsToCreate.Count == 0)
			{
				return;
			}
			DomainResponseInfoSet responseSet = null;
			base.InvokeProvisioningService("Create Domain", delegate
			{
				responseSet = this.ProvisioningService.CreateDomains(this.domainsToCreate);
			}, this.domainsToCreate.Count);
			List<EhfDomainItem> domainsTriedToCreate = new List<EhfDomainItem>(this.domainsToCreate);
			this.domainsToCreate.Clear();
			this.HandleCreateDomainResponse(responseSet, domainsTriedToCreate);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000AD68 File Offset: 0x00008F68
		private void InvokeEhfDisableDomains()
		{
			if (this.domainsToDisable == null || this.domainsToDisable.Count == 0)
			{
				return;
			}
			DomainResponseInfoSet responseSet = null;
			base.InvokeProvisioningService("Disable Domain", delegate
			{
				responseSet = this.ProvisioningService.DisableDomains(this.domainsToDisable);
			}, this.domainsToDisable.Count);
			List<EhfDomainItem> list = new List<EhfDomainItem>(base.Config.EhfSyncAppConfig.BatchSize);
			int transientFailureCount = 0;
			int permanentFailureCount = 0;
			for (int i = 0; i < responseSet.ResponseInfo.Length; i++)
			{
				DomainResponseInfo domainResponseInfo = responseSet.ResponseInfo[i];
				EhfDomainItem ehfDomainItem = this.domainsToDisable[i];
				if (domainResponseInfo.Status == ResponseStatus.Success)
				{
					list.Add(ehfDomainItem);
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Successfully disabled EHF domain: DN=<{0}>; GUID=<{1}>; EHF-Company-ID=<{2}>", new object[]
					{
						ehfDomainItem.DistinguishedName,
						domainResponseInfo.DomainGuid.Value,
						ehfDomainItem.Domain.CompanyId
					});
				}
				else if (domainResponseInfo.Fault.Id == FaultId.DomainDoesNotExist)
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Attempted to disable a domain that does not exist; ignoring the domain: DN=<{0}>; GUID=<{1}>; EHF-Company-ID=<{2}>; DomainName = <{3}>", new object[]
					{
						ehfDomainItem.DistinguishedName,
						ehfDomainItem.GetDomainGuid(),
						ehfDomainItem.Domain.CompanyId,
						ehfDomainItem.Name
					});
				}
				else if (domainResponseInfo.Fault.Id == FaultId.DomainExistOutsideThisCompany)
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Attempted to disable a domain that belongs to a different company under this reseller; ignoring the domain: DN=<{0}>; GUID=<{1}>; EHF-Company-ID=<{2}>", new object[]
					{
						ehfDomainItem.DistinguishedName,
						ehfDomainItem.GetDomainGuid(),
						ehfDomainItem.Domain.CompanyId
					});
				}
				else if (domainResponseInfo.Fault.Id == FaultId.AccessDenied)
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Attempted to disable a domain that belongs to a different company outside of this reseller; ignoring the domain: DN=<{0}>; GUID=<{1}>; EHF-Company-ID=<{2}>", new object[]
					{
						ehfDomainItem.DistinguishedName,
						ehfDomainItem.GetDomainGuid(),
						ehfDomainItem.Domain.CompanyId
					});
				}
				else
				{
					this.HandleFailedDomain("Disable Domain", ehfDomainItem, domainResponseInfo, ref transientFailureCount, ref permanentFailureCount);
				}
				if (!ehfDomainItem.EventLogAndTryStoreSyncErrors(base.ADAdapter))
				{
					throw new InvalidOperationException("EventLogAndTryStoreSyncErrors() returned false for a deleted object");
				}
			}
			base.HandlePerEntryFailureCounts("Disable Domain", this.domainsToDisable.Count, transientFailureCount, permanentFailureCount, false);
			this.domainsToDisable.Clear();
			foreach (EhfDomainItem ehfDomainItem2 in list)
			{
				base.DiagSession.Tracer.TraceDebug<string, string>((long)base.DiagSession.GetHashCode(), "Adding disabled domain Name=<{0}> DN=<{1}> to the delete batch", ehfDomainItem2.Name, ehfDomainItem2.DistinguishedName);
				this.AddDomainToDeleteBatch(ehfDomainItem2);
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000B0A8 File Offset: 0x000092A8
		private void InvokeEhfDeleteDomains()
		{
			if (this.domainsToDelete == null || this.domainsToDelete.Count == 0)
			{
				return;
			}
			DomainResponseInfoSet responseSet = null;
			base.InvokeProvisioningService("Delete Domain", delegate
			{
				responseSet = this.ProvisioningService.DeleteDomains(this.domainsToDelete);
			}, this.domainsToDelete.Count);
			int transientFailureCount = 0;
			int permanentFailureCount = 0;
			for (int i = 0; i < responseSet.ResponseInfo.Length; i++)
			{
				DomainResponseInfo domainResponseInfo = responseSet.ResponseInfo[i];
				EhfDomainItem ehfDomainItem = this.domainsToDelete[i];
				if (domainResponseInfo.Status == ResponseStatus.Success)
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Successfully deleted EHF domain: DN=<{0}>; GUID=<{1}>; EHF-Company-ID=<{2}>", new object[]
					{
						ehfDomainItem.DistinguishedName,
						domainResponseInfo.DomainGuid.Value,
						ehfDomainItem.Domain.CompanyId
					});
				}
				else if (domainResponseInfo.Fault.Id == FaultId.DomainDoesNotExist)
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Attempted to delete a domain that does not exist; ignoring the domain: DN=<{0}>; GUID=<{1}>; EHF-Company-ID=<{2}>", new object[]
					{
						ehfDomainItem.DistinguishedName,
						ehfDomainItem.GetDomainGuid(),
						ehfDomainItem.Domain.CompanyId
					});
				}
				else if (domainResponseInfo.Fault.Id == FaultId.DomainExistOutsideThisCompany)
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Attempted to delete a domain that belongs to a different company under this reseller; ignoring the domain: DN=<{0}>; GUID=<{1}>; EHF-Company-ID=<{2}>", new object[]
					{
						ehfDomainItem.DistinguishedName,
						ehfDomainItem.GetDomainGuid(),
						ehfDomainItem.Domain.CompanyId
					});
				}
				else if (domainResponseInfo.Fault.Id == FaultId.AccessDenied)
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Attempted to delete a domain that belongs to a different company outside of this reseller; ignoring the domain: DN=<{0}>; GUID=<{1}>; EHF-Company-ID=<{2}>", new object[]
					{
						ehfDomainItem.DistinguishedName,
						ehfDomainItem.GetDomainGuid(),
						ehfDomainItem.Domain.CompanyId
					});
				}
				else
				{
					this.HandleFailedDomain("Delete Domain", ehfDomainItem, domainResponseInfo, ref transientFailureCount, ref permanentFailureCount);
				}
				if (!ehfDomainItem.EventLogAndTryStoreSyncErrors(base.ADAdapter))
				{
					throw new InvalidOperationException("EventLogAndTryStoreSyncErrors() returned false for a deleted object");
				}
			}
			base.HandlePerEntryFailureCounts("Delete Domain", this.domainsToDelete.Count, transientFailureCount, permanentFailureCount, false);
			this.domainsToDelete.Clear();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000B318 File Offset: 0x00009518
		private bool SetForcedDomainSyncOnLastDomain(Guid ehfCompanyGuid)
		{
			if (this.domainsToCreate == null)
			{
				return false;
			}
			for (int i = this.domainsToCreate.Count - 1; i >= 0; i--)
			{
				if (this.domainsToCreate[i].SetForcedDomainSync(ehfCompanyGuid))
				{
					return true;
				}
			}
			if (this.domainsToCreateLast != null)
			{
				for (int j = this.domainsToCreateLast.Count - 1; j >= 0; j--)
				{
					if (this.domainsToCreateLast[j].SetForcedDomainSync(ehfCompanyGuid))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000089 RID: 137
		internal const string CreateDomainOperation = "Create Domain";

		// Token: 0x0400008A RID: 138
		internal const string DisableDomainOperation = "Disable Domain";

		// Token: 0x0400008B RID: 139
		internal const string DeleteDomainOperation = "Delete Domain";

		// Token: 0x0400008C RID: 140
		private static readonly string[] AcceptedDomainSyncAttributes = new string[]
		{
			"msExchAcceptedDomainName",
			"msExchAcceptedDomainFlags",
			"msExchTransportResellerSettingsLink"
		};

		// Token: 0x0400008D RID: 141
		private static readonly string[] AcceptedDomainExtraAttributes = new string[]
		{
			"msExchCU",
			"objectGUID",
			"msExchTransportInboundSettings",
			"msExchEdgeSyncCookies"
		};

		// Token: 0x0400008E RID: 142
		private static readonly string[] AcceptedDomainAllAttributes = EhfSynchronizer.CombineArrays<string>(EhfDomainSynchronizer.AcceptedDomainSyncAttributes, EhfDomainSynchronizer.AcceptedDomainExtraAttributes);

		// Token: 0x0400008F RID: 143
		private List<EhfDomainItem> domainsToCreate;

		// Token: 0x04000090 RID: 144
		private List<EhfDomainItem> domainsToDisable;

		// Token: 0x04000091 RID: 145
		private List<EhfDomainItem> domainsToDelete;

		// Token: 0x04000092 RID: 146
		private List<EhfDomainItem> domainsToCreateLast;

		// Token: 0x04000093 RID: 147
		private bool handledAllDeletedDomains;
	}
}
