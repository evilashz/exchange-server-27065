using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Datacenter;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x0200000D RID: 13
	internal class EhfAdminAccountSynchronizer : EhfSynchronizer
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00003658 File Offset: 0x00001858
		public EhfAdminAccountSynchronizer(EhfTargetConnection ehfConnection, EhfSyncErrorTracker errorTracker) : base(ehfConnection)
		{
			if (errorTracker.CriticalTransientFailureCount != 0 || errorTracker.PermanentFailureCount != 0 || errorTracker.AllTransientFailuresCount != 0)
			{
				throw new ArgumentException(string.Format("There should not be any failures before the sync cycle starts. Critical {0}; Permanent {1} AllTransientFailures {2}", errorTracker.CriticalTransientFailureCount, errorTracker.PermanentFailureCount, errorTracker.AllTransientFailuresCount));
			}
			EhfAdminAccountSynchronizer.cycleCount++;
			base.DiagSession.Tracer.TraceDebug<int>((long)base.DiagSession.GetHashCode(), "Creating EhfAdminAccountSynchronizer ({0})", EhfAdminAccountSynchronizer.cycleCount);
			this.errorTracker = errorTracker;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003710 File Offset: 0x00001910
		public static TypeSynchronizer CreateTypeSynchronizer()
		{
			return new TypeSynchronizer(null, new PreDecorate(EhfAdminAccountSynchronizer.LoadAdminGroups), null, null, null, null, "Tenant Admins", Schema.Query.QueryUsgMailboxAndOrganization, null, SearchScope.Subtree, EhfAdminAccountSynchronizer.TenantAdminsRoleGroupSyncAttributes, null, false);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003748 File Offset: 0x00001948
		public override bool FlushBatches()
		{
			if (this.adminAccountChange.Count == 0 && this.groupsToRemove.Count == 0)
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "{0}: No admin changes to Sync to FOSE", new object[]
				{
					EhfAdminAccountSynchronizer.cycleCount
				});
				return true;
			}
			this.InvokeRemoveGroups();
			if (this.adminAccountChange.Count != 0)
			{
				Exception ex;
				EhfADAdapter configADAdapter = base.ADAdapter.GetConfigADAdapter(base.DiagSession, out ex);
				if (configADAdapter == null)
				{
					base.DiagSession.LogAndTraceError("Could not create a LDAP connection to the Configuration naming context. Details {0}", new object[]
					{
						ex
					});
					base.DiagSession.EventLog.LogEvent(EdgeSyncEventLogConstants.Tuple_EhfAdminSyncFailedToConnectToConfigNamingContext, null, new object[]
					{
						ex.Message
					});
					base.EhfConnection.AbortSyncCycle(ex);
					return false;
				}
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "{0} : Changes to <{1}> tenant(s) detected. Checking if sync is required.", new object[]
				{
					EhfAdminAccountSynchronizer.cycleCount.ToString(),
					this.adminAccountChange.Count.ToString()
				});
				foreach (KeyValuePair<string, EhfAdminSyncChangeBuilder> keyValuePair in this.adminAccountChange)
				{
					this.AbortSyncCycleIfTooManyFailures();
					EhfAdminSyncChangeBuilder value = keyValuePair.Value;
					if (value.ChangeExists)
					{
						EhfCompanyAdmins ehfCompanyAdmins = value.Flush(configADAdapter);
						if (ehfCompanyAdmins == null)
						{
							this.errorTracker.AddCriticalFailure();
						}
						else if (ehfCompanyAdmins.IsSyncRequired)
						{
							base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "AdminSync: {0}", new object[]
							{
								ehfCompanyAdmins
							});
							if (ehfCompanyAdmins.CompanyId != 0)
							{
								this.InvokeSyncAdminAccountsAndSyncGroupUsers(ehfCompanyAdmins, configADAdapter);
							}
							else
							{
								base.DiagSession.LogAndTraceError("Not syncing {0} since companyId is not set", new object[]
								{
									ehfCompanyAdmins.TenantOU
								});
								if (!ehfCompanyAdmins.PerimeterConfigNotReplicatedOrIsDeleted)
								{
									this.errorTracker.AddTransientFailure(ehfCompanyAdmins.EhfCompanyIdentity, new EhfAdminAccountSynchronizer.EhfAdminSyncTransientException("PerimeterConfig object does not have Ehf CompanyId set."), string.Empty);
								}
								else
								{
									this.errorTracker.AddCriticalFailure();
								}
							}
						}
						else
						{
							base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "No adminsync is required for: {0}", new object[]
							{
								keyValuePair.Key
							});
						}
						value.ClearCachedChanges();
					}
				}
				this.adminAccountChange.Clear();
			}
			return true;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000039C4 File Offset: 0x00001BC4
		public void HandleGroupChangedEvent(ExSearchResultEntry entry)
		{
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "GroupChanged: {0}", new object[]
			{
				entry.DistinguishedName
			});
			EhfAdminSyncChangeBuilder adminBuilderForChange = this.GetAdminBuilderForChange(entry);
			if (adminBuilderForChange != null)
			{
				this.ThrowIfAdminSyncNotEnabled(adminBuilderForChange.TenantOU, entry);
				adminBuilderForChange.AddGroupMembershipChange(entry);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003A14 File Offset: 0x00001C14
		public void HandleGroupAddedEvent(ExSearchResultEntry entry)
		{
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "GroupAdded: {0}", new object[]
			{
				entry.DistinguishedName
			});
			EhfAdminSyncChangeBuilder adminBuilderForChange = this.GetAdminBuilderForChange(entry);
			if (adminBuilderForChange != null)
			{
				this.ThrowIfAdminSyncNotEnabled(adminBuilderForChange.TenantOU, entry);
				adminBuilderForChange.AddGroupAdditionChange(entry);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003A64 File Offset: 0x00001C64
		public void HandleGroupDeletedEvent(ExSearchResultEntry entry)
		{
			if (EhfAdminAccountSynchronizer.IsEventForDeletedOrganization(entry, base.DiagSession))
			{
				throw new InvalidOperationException("Change entry " + entry.DistinguishedName + " is for a deleted organization. The entry should have been ignored from PreDecorate.");
			}
			EhfAdminSyncChangeBuilder adminBuilderForChange = this.GetAdminBuilderForChange(entry);
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Encountered a DELETE rolegroup event. ObjectGuid: <{0}>; Company: <{1}>", new object[]
			{
				entry.GetObjectGuid(),
				adminBuilderForChange.TenantOU
			});
			if (adminBuilderForChange != null)
			{
				adminBuilderForChange.HandleGroupDeletedEvent(entry);
			}
			if (!EhfWellKnownGroup.IsWellKnownPartnerGroupDN(entry.DistinguishedName))
			{
				return;
			}
			Guid externalDirectoryObjectId;
			if (EhfCompanyAdmins.TryGetExternalDirectoryObjectId(entry, base.DiagSession, out externalDirectoryObjectId))
			{
				this.AddGroupToDeleteGroupsBatch(externalDirectoryObjectId);
				return;
			}
			base.DiagSession.LogAndTraceError("Could not find the ExternalDirectoryObjectId for well known partner group {0}", new object[]
			{
				entry.DistinguishedName
			});
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003B28 File Offset: 0x00001D28
		public void HandleWlidChangedEvent(ExSearchResultEntry entry)
		{
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "WLIDChanged: {0}", new object[]
			{
				entry.DistinguishedName
			});
			EhfAdminSyncChangeBuilder adminBuilderForChange = this.GetAdminBuilderForChange(entry);
			if (adminBuilderForChange != null)
			{
				this.ThrowIfAdminSyncNotEnabled(adminBuilderForChange.TenantOU, entry);
				adminBuilderForChange.AddWlidChanges(entry);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003B76 File Offset: 0x00001D76
		public void HandleWlidAddedEvent(ExSearchResultEntry entry)
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003B78 File Offset: 0x00001D78
		public void HandleWlidDeletedEvent(ExSearchResultEntry entry)
		{
			if (!EhfAdminAccountSynchronizer.IsEventForDeletedOrganization(entry, base.DiagSession))
			{
				EhfAdminSyncChangeBuilder adminBuilderForChange = this.GetAdminBuilderForChange(entry);
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Encountered a DELETE mailbox event. ObjectGuid: <{0}>; Company: <{1}>", new object[]
				{
					entry.GetObjectGuid(),
					adminBuilderForChange.TenantOU
				});
				if (adminBuilderForChange != null)
				{
					adminBuilderForChange.HandleWlidDeletedEvent(entry);
					return;
				}
			}
			else
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Ignoring the WLID delete event '{0}' for a deleted organization", new object[]
				{
					entry.DistinguishedName
				});
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public void HandleOrganizationDeletedEvent(ExSearchResultEntry entry)
		{
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Ignoring the Organization Delete event for org {0}", new object[]
			{
				entry.DistinguishedName
			});
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003C28 File Offset: 0x00001E28
		public void HandleOrganizationChangedEvent(ExSearchResultEntry entry)
		{
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Organization changed: <{0}>", new object[]
			{
				entry.DistinguishedName
			});
			EhfAdminSyncChangeBuilder adminBuilderForChange = this.GetAdminBuilderForChange(entry);
			if (adminBuilderForChange != null)
			{
				this.ThrowIfAdminSyncNotEnabled(adminBuilderForChange.TenantOU, entry);
				adminBuilderForChange.HandleOrganizationChangedEvent(entry);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003C78 File Offset: 0x00001E78
		public void HandleOrganizationAddedEvent(ExSearchResultEntry entry)
		{
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Organization created: <{0}>", new object[]
			{
				entry.DistinguishedName
			});
			EhfAdminSyncChangeBuilder adminBuilderForChange = this.GetAdminBuilderForChange(entry);
			if (adminBuilderForChange != null)
			{
				this.ThrowIfAdminSyncNotEnabled(adminBuilderForChange.TenantOU, entry);
				adminBuilderForChange.HandleOrganizationAddedEvent(entry);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003CC6 File Offset: 0x00001EC6
		public override void ClearBatches()
		{
			this.adminAccountChange.Clear();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003CD4 File Offset: 0x00001ED4
		public bool FilterOrLoadFullEntry(ExSearchResultEntry entry)
		{
			bool flag = false;
			string tenantOU;
			bool isOrgChange;
			return this.TryGetTenantOrgUnitDNForChange(entry, out tenantOU, out isOrgChange, ref flag) && !this.IgnoreChange(entry, tenantOU, isOrgChange, ref flag) && (flag || EhfSynchronizer.LoadFullEntry(entry, EhfAdminAccountSynchronizer.AdminSyncAllAttributes, base.EhfConnection));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003D20 File Offset: 0x00001F20
		public void LogAndAbortSyncCycle()
		{
			string message = string.Format(CultureInfo.InvariantCulture, "The EHFAdminsync encountered <{0}> transient failures and <{1}> permanent failures. Aborting the sync cycle", new object[]
			{
				this.errorTracker.AllTransientFailuresCount,
				this.errorTracker.PermanentFailureCount
			});
			base.EhfConnection.AbortSyncCycle(new EdgeSyncCycleFailedException(message));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003D7C File Offset: 0x00001F7C
		private static bool LoadAdminGroups(ExSearchResultEntry entry, Connection sourceConnection, TargetConnection targetConnection, object state)
		{
			EhfRecipientTargetConnection ehfRecipientTargetConnection = (EhfRecipientTargetConnection)targetConnection;
			return ehfRecipientTargetConnection.AdminAccountSynchronizer.FilterOrLoadFullEntry(entry);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003D9C File Offset: 0x00001F9C
		private static bool IsEventForDeletedOrganization(ExSearchResultEntry entry, EdgeSyncDiag diagSession)
		{
			string distinguishedName;
			return EhfAdminAccountSynchronizer.TryGetOrganizationUnit(entry, diagSession, out distinguishedName) && ExSearchResultEntry.IsDeletedDN(distinguishedName);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003DBC File Offset: 0x00001FBC
		private static bool TryGetOrganizationUnit(ExSearchResultEntry entry, EdgeSyncDiag diagSession, out string exchOURoot)
		{
			exchOURoot = string.Empty;
			DirectoryAttribute directoryAttribute;
			if (entry.Attributes.TryGetValue("msExchOURoot", out directoryAttribute) && directoryAttribute != null)
			{
				exchOURoot = (string)directoryAttribute[0];
				return true;
			}
			diagSession.LogAndTraceError("Could not find ExchOURoot for {0}. Every object is expected to contain this attribute.", new object[]
			{
				entry.DistinguishedName
			});
			return false;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003E18 File Offset: 0x00002018
		private static void ClassifyFailedResponse<T>(Dictionary<T, ErrorInfo> response, ref int transientFailureCount, ref int permanentFailureCount, ref bool hasCriticalError, StringBuilder logBuilder)
		{
			logBuilder.Append("[");
			foreach (KeyValuePair<T, ErrorInfo> keyValuePair in response)
			{
				logBuilder.AppendFormat(CultureInfo.InstalledUICulture, "<{0}: {1} {2} {3}>", new object[]
				{
					keyValuePair.Key,
					keyValuePair.Value.Code,
					keyValuePair.Value.ErrorType,
					EhfAdminAccountSynchronizer.FormatAdditionalData(keyValuePair.Value.Data)
				});
				if (keyValuePair.Value.ErrorType == ErrorType.Transient)
				{
					transientFailureCount++;
					if (keyValuePair.Value.Code != ErrorCode.InvalidFormat)
					{
						hasCriticalError = true;
					}
				}
				else
				{
					permanentFailureCount++;
				}
			}
			logBuilder.Append("]");
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003F14 File Offset: 0x00002114
		private static string FormatAdditionalData(Dictionary<string, string> additionalData)
		{
			if (additionalData == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AdditionalData: ");
			foreach (KeyValuePair<string, string> keyValuePair in additionalData)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "({0}),({1}) ", new object[]
				{
					keyValuePair.Key,
					keyValuePair.Value
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003FAC File Offset: 0x000021AC
		private static bool TryGetOURootFromDN(string dn, out string tenantOU, out bool isOrgChange)
		{
			tenantOU = string.Empty;
			isOrgChange = false;
			ADObjectId adobjectId = new ADObjectId(dn);
			int num = adobjectId.Depth - adobjectId.DomainId.Depth;
			if (num < EhfAdminAccountSynchronizer.OrganizationUnitDepth)
			{
				return false;
			}
			ADObjectId adobjectId2;
			if (num == EhfAdminAccountSynchronizer.OrganizationUnitDepth)
			{
				adobjectId2 = adobjectId;
				isOrgChange = true;
			}
			else
			{
				adobjectId2 = adobjectId.AncestorDN(num - EhfAdminAccountSynchronizer.OrganizationUnitDepth);
			}
			if (!EhfAdminAccountSynchronizer.IsOrganizationUnitDN(adobjectId2))
			{
				return false;
			}
			tenantOU = adobjectId2.DistinguishedName;
			return true;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004018 File Offset: 0x00002218
		private static bool IsOrganizationUnitDN(ADObjectId tenantObjectId)
		{
			if (!tenantObjectId.Rdn.Prefix.Equals("OU", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			AdName rdn = tenantObjectId.Parent.Rdn;
			return rdn.Prefix.Equals("OU", StringComparison.OrdinalIgnoreCase) && rdn.UnescapedName.Equals("Microsoft Exchange Hosted Organizations");
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004078 File Offset: 0x00002278
		private static bool TryGetFlagsValue(string flagsAttrName, ExSearchResultEntry entry, out int flagValue)
		{
			flagValue = 0;
			DirectoryAttribute attribute = entry.GetAttribute(flagsAttrName);
			if (attribute == null)
			{
				return false;
			}
			string text = (string)attribute[0];
			return !string.IsNullOrEmpty(text) && int.TryParse(text, out flagValue);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000040B8 File Offset: 0x000022B8
		private bool IgnoreChange(ExSearchResultEntry entry, string tenantOU, bool isOrgChange, ref bool fullLoadComplete)
		{
			EhfAdminSyncChangeBuilder ehfAdminSyncChangeBuilder;
			if (this.adminAccountChange.TryGetValue(tenantOU, out ehfAdminSyncChangeBuilder) && ehfAdminSyncChangeBuilder.IsFullTenantAdminSyncRequired())
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Complete adminsync is required for the org <{0}>. Ignoring the change <{1}>", new object[]
				{
					tenantOU,
					entry.DistinguishedName
				});
				return true;
			}
			bool flag;
			if (!this.syncEnabledConfigCache.TryGetValue(tenantOU, out flag))
			{
				ExSearchResultEntry exSearchResultEntry;
				if (isOrgChange && entry.Attributes.ContainsKey("msExchProvisioningFlags"))
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.High, "AdminSyncEnabled value for <{0}> is present in the organization change entry ", new object[]
					{
						tenantOU
					});
					exSearchResultEntry = entry;
				}
				else
				{
					exSearchResultEntry = base.ADAdapter.ReadObjectEntry(tenantOU, false, EhfAdminAccountSynchronizer.AdminSyncAllAttributes);
					if (exSearchResultEntry == null)
					{
						base.DiagSession.LogAndTraceError("Failed to read the organization unit entry for <{0}>. Ignoring the entry <{1}>", new object[]
						{
							tenantOU,
							entry.DistinguishedName
						});
						return true;
					}
					if (isOrgChange)
					{
						fullLoadComplete = true;
					}
				}
				OrganizationProvisioningFlags provisioningFlags = this.GetProvisioningFlags(exSearchResultEntry);
				flag = ((provisioningFlags & OrganizationProvisioningFlags.EhfAdminAccountSyncEnabled) == OrganizationProvisioningFlags.EhfAdminAccountSyncEnabled);
				this.AddToSyncEnabledCache(tenantOU, flag);
				base.DiagSession.Tracer.TraceDebug<bool, string>((long)this.GetHashCode(), "AdminSyncEnabled is set to '{0}' for tenant <{1}>", flag, tenantOU);
			}
			else
			{
				base.DiagSession.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "AdminSyncEnabled value for <{0}> is present in cache. Change entry <{1}>", tenantOU, entry.DistinguishedName);
			}
			if (!flag)
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "AdminSync is disabled for tenant <{0}>. Ignoring the change <{1}>", new object[]
				{
					tenantOU,
					entry.DistinguishedName
				});
				return true;
			}
			return false;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004228 File Offset: 0x00002428
		private void AddToSyncEnabledCache(string tenantOU, bool syncEnabled)
		{
			if (!this.IsSyncEnabledCacheFull())
			{
				this.syncEnabledConfigCache.Add(tenantOU, syncEnabled);
				return;
			}
			base.DiagSession.Tracer.TraceDebug<string, bool>((long)this.GetHashCode(), "SyncEnabled cache is full. Not adding item {0}:{1} to cache", tenantOU, syncEnabled);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000425E File Offset: 0x0000245E
		private bool IsSyncEnabledCacheFull()
		{
			return this.syncEnabledConfigCache.Count >= 20000;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004278 File Offset: 0x00002478
		private bool TryGetTenantOrgUnitDNForChange(ExSearchResultEntry entry, out string tenantOU, out bool isOrgChange, ref bool fullLoadComplete)
		{
			tenantOU = null;
			isOrgChange = false;
			if (entry.IsDeleted)
			{
				if (!entry.Attributes.ContainsKey("msExchOURoot"))
				{
					if (!EhfSynchronizer.LoadFullEntry(entry, EhfAdminAccountSynchronizer.AdminSyncAllAttributes, base.EhfConnection))
					{
						return false;
					}
					fullLoadComplete = true;
				}
				else
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Delete change <{0}> already contains OURoot. Using that.", new object[]
					{
						entry.DistinguishedName
					});
				}
				if (!EhfAdminAccountSynchronizer.TryGetOrganizationUnit(entry, base.DiagSession, out tenantOU))
				{
					base.DiagSession.LogAndTraceError("Could not extract OURoot attribute from deleted change entry <{0}>. Ignoring the change.", new object[]
					{
						entry.DistinguishedName
					});
					return false;
				}
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Extracted the OURoot <{0}> for the deleted change from the change entry <{1}>", new object[]
				{
					tenantOU,
					entry.DistinguishedName
				});
			}
			else if (!EhfAdminAccountSynchronizer.TryGetOURootFromDN(entry.DistinguishedName, out tenantOU, out isOrgChange))
			{
				base.DiagSession.LogAndTraceError("Failed to get the OrganizationUnit Root from the DN of <{0}>. Ignoring the item.", new object[]
				{
					entry.DistinguishedName
				});
				return false;
			}
			if (string.IsNullOrEmpty(tenantOU))
			{
				base.DiagSession.LogAndTraceError("OrganizationUnit DN for <{0}> is null or emtpy. Ignoring the item.", new object[]
				{
					entry.DistinguishedName
				});
				return false;
			}
			if (ExSearchResultEntry.IsDeletedDN(tenantOU))
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Change <{0}> is for a deleted organization <{1}>. Ignoring the item", new object[]
				{
					entry.DistinguishedName,
					tenantOU
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000043DC File Offset: 0x000025DC
		private OrganizationProvisioningFlags GetProvisioningFlags(ExSearchResultEntry entry)
		{
			int result;
			if (EhfAdminAccountSynchronizer.TryGetFlagsValue("msExchProvisioningFlags", entry, out result))
			{
				return (OrganizationProvisioningFlags)result;
			}
			return OrganizationProvisioningFlags.None;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000043FC File Offset: 0x000025FC
		private EhfAdminSyncChangeBuilder GetAdminBuilderForChange(ExSearchResultEntry entry)
		{
			string text;
			if (!EhfAdminAccountSynchronizer.TryGetOrganizationUnit(entry, base.DiagSession, out text))
			{
				return null;
			}
			EhfAdminSyncChangeBuilder ehfAdminSyncChangeBuilder;
			if (!this.adminAccountChange.TryGetValue(text, out ehfAdminSyncChangeBuilder))
			{
				DirectoryAttribute attribute = entry.GetAttribute("msExchCU");
				if (attribute == null)
				{
					base.DiagSession.LogAndTraceError("Could not find ConfigUnitDN for {0}. Every object is expected to contain this attribute.", new object[]
					{
						entry.DistinguishedName
					});
					return null;
				}
				string tenantConfigUnitDN = (string)attribute[0];
				ehfAdminSyncChangeBuilder = new EhfAdminSyncChangeBuilder(text, tenantConfigUnitDN, base.EhfConnection);
				this.adminAccountChange.Add(text, ehfAdminSyncChangeBuilder);
			}
			return ehfAdminSyncChangeBuilder;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000044EC File Offset: 0x000026EC
		private void InvokeSyncAdminAccountsAndSyncGroupUsers(EhfCompanyAdmins admin, EhfADAdapter configADAdapter)
		{
			bool syncAdminAccountsCompleted = true;
			bool syncAdminAgentCompleted = true;
			bool syncHelpdeskAgentCompleted = true;
			if (admin.HasLocalAdminChanges)
			{
				FailedAdminAccounts syncAdminAccountsResponse = null;
				FaultException syncAdminException = null;
				string syncAdminsOperation = EhfAdminAccountSynchronizer.SyncAdminsOperation;
				base.InvokeProvisioningService(syncAdminsOperation, delegate
				{
					syncAdminAccountsResponse = this.ProvisioningService.SyncAdminAccounts(admin.GetLocalAdminsToSync(this.DiagSession), out syncAdminException);
				}, 1);
				if (syncAdminException != null)
				{
					this.HandleOperationLevelException(syncAdminException, syncAdminsOperation, admin.EhfCompanyIdentity);
				}
				else
				{
					this.ProcessSyncAdminAccountsResponse(syncAdminAccountsResponse, admin, syncAdminsOperation);
				}
				syncAdminAccountsCompleted = (syncAdminException == null);
			}
			if (admin.HasPartnerAdminGroupChanges)
			{
				syncAdminAgentCompleted = this.InvokeSyncGroupUsers(admin.EhfCompanyIdentity, admin.AdminAgent, admin.TenantOU);
				syncHelpdeskAgentCompleted = this.InvokeSyncGroupUsers(admin.EhfCompanyIdentity, admin.HelpdeskAgent, admin.TenantOU);
			}
			this.UpdateSyncStateInAD(admin, configADAdapter, syncAdminAccountsCompleted, syncAdminAgentCompleted, syncHelpdeskAgentCompleted);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000461C File Offset: 0x0000281C
		private void HandleOperationLevelException(FaultException adminSyncException, string operationName, EhfCompanyIdentity companyIdentity)
		{
			FaultException<InvalidCompanyFault> faultException = adminSyncException as FaultException<InvalidCompanyFault>;
			FaultException<InternalFault> faultException2 = adminSyncException as FaultException<InternalFault>;
			if (faultException != null)
			{
				if (faultException.Detail.Code == InvalidCompanyCode.CompanyDoesNotExist || faultException.Detail.ErrorType == ErrorType.Transient)
				{
					this.HandleFaultAsTransientFailure<InvalidCompanyFault>(companyIdentity, operationName, faultException.Detail.Code.ToString(), faultException, false);
					return;
				}
				this.HandleFaultAsPermanentFailure<InvalidCompanyFault>(operationName, faultException.Detail.Code.ToString(), faultException);
				return;
			}
			else
			{
				if (faultException2 != null)
				{
					this.HandleFaultAsTransientFailure<InternalFault>(companyIdentity, operationName, faultException2.Detail.Code.ToString(), faultException2, true);
					return;
				}
				throw new InvalidOperationException("Encountered unexpected fault exception.", adminSyncException);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000046C4 File Offset: 0x000028C4
		private void ProcessSyncAdminAccountsResponse(FailedAdminAccounts syncAdminAccountsResponse, EhfCompanyAdmins requestAdmins, string operationName)
		{
			if (syncAdminAccountsResponse == null)
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Successfully completed SyncAdminAccounts operation. Sync details: <{0}>", new object[]
				{
					requestAdmins
				});
				return;
			}
			int num = 0;
			int num2 = 0;
			bool hasCriticalError = false;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "Tenant: <{0}> ", new object[]
			{
				requestAdmins.TenantOU
			});
			stringBuilder.Append("SyncAdminAccountUserErrors: ");
			if (syncAdminAccountsResponse.FailedUsers != null)
			{
				EhfAdminAccountSynchronizer.ClassifyFailedResponse<string>(syncAdminAccountsResponse.FailedUsers, ref num, ref num2, ref hasCriticalError, stringBuilder);
			}
			stringBuilder.Append(" SyncAdminAccountGroupErrors: ");
			if (syncAdminAccountsResponse.FailedGroups != null)
			{
				EhfAdminAccountSynchronizer.ClassifyFailedResponse<Guid>(syncAdminAccountsResponse.FailedGroups, ref num, ref num2, ref hasCriticalError, stringBuilder);
			}
			string text = stringBuilder.ToString();
			this.HandleOperationFailureCounts(requestAdmins.EhfCompanyIdentity, operationName, (num > 0) ? 1 : 0, (num2 > 0) ? 1 : 0, text, hasCriticalError);
			this.LogAdminSyncOperationFailure(operationName, num, num2, text);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000047E8 File Offset: 0x000029E8
		private bool InvokeSyncGroupUsers(EhfCompanyIdentity companyIdentity, EhfWellKnownGroup partnerGroup, string tenantOU)
		{
			string syncPartnerAdminGroupOperation = EhfAdminAccountSynchronizer.SyncPartnerAdminGroupOperation;
			if (partnerGroup == null)
			{
				return true;
			}
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Syncing partner admin group {0} for tenant {1}", new object[]
			{
				partnerGroup.ExternalDirectoryObjectId,
				tenantOU
			});
			string[] members = partnerGroup.GetWlidsOfGroupMembers(2000, base.DiagSession);
			Dictionary<string, ErrorInfo> syncGroupUsersResponse = null;
			FaultException syncGroupsException = null;
			base.InvokeProvisioningService(syncPartnerAdminGroupOperation, delegate
			{
				syncGroupUsersResponse = this.ProvisioningService.SyncGroupUsers(companyIdentity.EhfCompanyId, partnerGroup.ExternalDirectoryObjectId, members, out syncGroupsException);
			}, 1);
			if (syncGroupsException != null)
			{
				FaultException<InvalidGroupFault> faultException = syncGroupsException as FaultException<InvalidGroupFault>;
				if (faultException != null)
				{
					if (faultException.Detail.Code == InvalidGroupCode.GroupDoesNotExist)
					{
						base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "SyncGroupUsers returned GroupDoesNotExist fault while trying to sync an empty group <{0}> in company <{1}>.", new object[]
						{
							partnerGroup.WellKnownGroupName,
							tenantOU
						});
					}
					else if (faultException.Detail.Code == InvalidGroupCode.GroupBelongsToDifferentCompany || faultException.Detail.ErrorType == ErrorType.Transient)
					{
						this.HandleFaultAsTransientFailure<InvalidGroupFault>(companyIdentity, syncPartnerAdminGroupOperation, faultException.Detail.Code.ToString(), faultException, faultException.Detail.Code != InvalidGroupCode.GroupBelongsToDifferentCompany);
					}
					else
					{
						this.HandleFaultAsPermanentFailure<InvalidGroupFault>(syncPartnerAdminGroupOperation, faultException.Detail.Code.ToString(), faultException);
					}
				}
				else
				{
					this.HandleOperationLevelException(syncGroupsException, syncPartnerAdminGroupOperation, companyIdentity);
				}
				return false;
			}
			if (syncGroupUsersResponse == null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in members)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(value);
				}
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Successfully completed SyncGroupUsers operation for partnerGroup <{0} : {1}>. Details: <{2}>; Members={3}", new object[]
				{
					partnerGroup.WellKnownGroupName,
					tenantOU,
					partnerGroup,
					stringBuilder.ToString()
				});
			}
			else
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("SyncGroupUsers: ");
				int num = 0;
				int num2 = 0;
				bool hasCriticalError = false;
				EhfAdminAccountSynchronizer.ClassifyFailedResponse<string>(syncGroupUsersResponse, ref num, ref num2, ref hasCriticalError, stringBuilder2);
				string text = stringBuilder2.ToString();
				this.HandleOperationFailureCounts(companyIdentity, syncPartnerAdminGroupOperation, (num > 0) ? 1 : 0, (num2 > 0) ? 1 : 0, text, hasCriticalError);
				this.LogAdminSyncOperationFailure(syncPartnerAdminGroupOperation, num, num2, text);
			}
			return true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004A8C File Offset: 0x00002C8C
		private void HandleFaultAsPermanentFailure<TFault>(string operationName, string faultCode, FaultException<TFault> exception) where TFault : AdminServiceFault
		{
			this.errorTracker.AddPermanentFailure();
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			string format = "{0}({1})";
			object[] array = new object[2];
			array[0] = faultCode;
			object[] array2 = array;
			int num = 1;
			TFault detail = exception.Detail;
			array2[num] = detail.ErrorType;
			string exceptionReason = string.Format(invariantCulture, format, array);
			ExEventLog.EventTuple tuple_EhfAdminSyncPermanentFailure = EdgeSyncEventLogConstants.Tuple_EhfAdminSyncPermanentFailure;
			base.EventLogAndTraceException(operationName, tuple_EhfAdminSyncPermanentFailure, exception, exceptionReason);
			base.EhfConnection.PerfCounterHandler.OnOperationCommunicationFailure(operationName);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004B00 File Offset: 0x00002D00
		private void HandleFaultAsTransientFailure<TFault>(EhfCompanyIdentity companyIdentity, string operationName, string faultCode, FaultException<TFault> exception, bool isCriticalError) where TFault : AdminServiceFault
		{
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			string format = "{0}({1})";
			object[] array = new object[2];
			array[0] = faultCode;
			object[] array2 = array;
			int num = 1;
			TFault detail = exception.Detail;
			array2[num] = detail.ErrorType;
			string exceptionReason = string.Format(invariantCulture, format, array);
			this.HandleFaultAsTransientFailure(companyIdentity, operationName, exception, isCriticalError, exceptionReason);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004B54 File Offset: 0x00002D54
		private void HandleFaultAsTransientFailure(EhfCompanyIdentity companyIdentity, string operationName, Exception exception, bool isCriticalError, string exceptionReason)
		{
			ExEventLog.EventTuple tuple_EhfAdminSyncTransientFailure = EdgeSyncEventLogConstants.Tuple_EhfAdminSyncTransientFailure;
			if (!this.errorTracker.HasTransientFailure)
			{
				base.EventLogAndTraceException(operationName, tuple_EhfAdminSyncTransientFailure, exception, exceptionReason);
			}
			else
			{
				base.LogAndTraceException(operationName, exception, exceptionReason);
			}
			if (isCriticalError || companyIdentity == null)
			{
				this.errorTracker.AddCriticalFailure();
			}
			else
			{
				this.errorTracker.AddTransientFailure(companyIdentity, exception, operationName);
			}
			base.EhfConnection.PerfCounterHandler.OnOperationTransientFailure(operationName);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004BC0 File Offset: 0x00002DC0
		private void AddGroupToDeleteGroupsBatch(Guid externalDirectoryObjectId)
		{
			base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Adding Group <{0}> to Delete batch", new object[]
			{
				externalDirectoryObjectId
			});
			this.groupsToRemove.Add(externalDirectoryObjectId);
			if (this.groupsToRemove.Count == this.GetDeleteGroupsEffectiveBatchSize())
			{
				this.InvokeRemoveGroups();
				this.groupsToRemove.Clear();
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004C20 File Offset: 0x00002E20
		private int GetDeleteGroupsEffectiveBatchSize()
		{
			int num = Math.Min(base.Config.EhfSyncAppConfig.BatchSize, 1000);
			if (num <= 0)
			{
				num = 1;
			}
			return num;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004C88 File Offset: 0x00002E88
		private void InvokeRemoveGroups()
		{
			if (this.groupsToRemove.Count != 0)
			{
				if (this.groupsToRemove.Count > this.GetDeleteGroupsEffectiveBatchSize())
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "RemoveGroups batch size should not be greater than '{0}', but the batch size is '{1}'", new object[]
					{
						this.GetDeleteGroupsEffectiveBatchSize(),
						this.groupsToRemove.Count
					}));
				}
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Removing groups {0}", new object[]
				{
					this.groupsToRemove.Count
				});
				Dictionary<Guid, RemoveGroupErrorInfo> removeGroupsResponse = null;
				string removeGroupsOperation = EhfAdminAccountSynchronizer.RemoveGroupsOperation;
				FaultException removeGroupsException = null;
				base.InvokeProvisioningService(removeGroupsOperation, delegate
				{
					removeGroupsResponse = this.ProvisioningService.RemoveGroups(this.groupsToRemove.ToArray<Guid>(), out removeGroupsException);
				}, this.groupsToRemove.Count);
				if (removeGroupsException != null)
				{
					this.HandleOperationLevelException(removeGroupsException, removeGroupsOperation, null);
					return;
				}
				this.ProcessRemoveGroupsResponse(removeGroupsResponse, removeGroupsOperation);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004D88 File Offset: 0x00002F88
		private void ProcessRemoveGroupsResponse(Dictionary<Guid, RemoveGroupErrorInfo> removeGroupsResponse, string operationName)
		{
			foreach (Guid guid in this.groupsToRemove)
			{
				if (removeGroupsResponse == null || !removeGroupsResponse.ContainsKey(guid))
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Successfully removed partner group <{0}>.", new object[]
					{
						guid
					});
				}
			}
			if (removeGroupsResponse != null)
			{
				int num = 0;
				int num2 = 0;
				foreach (KeyValuePair<Guid, RemoveGroupErrorInfo> keyValuePair in removeGroupsResponse)
				{
					RemoveGroupErrorInfo value = keyValuePair.Value;
					if (value.Code == RemoveGroupErrorCode.GroupDoesNotExist)
					{
						base.DiagSession.LogAndTraceError("Attempted to remove a partner group <{0}> which does not exists. Ignoring the item.", new object[]
						{
							keyValuePair.Key
						});
					}
					else
					{
						base.DiagSession.LogAndTraceError("Encountered <{0}>:<{1}> InternalServerError while trying to remove a partner group <{2}>.", new object[]
						{
							keyValuePair.Value.Code,
							keyValuePair.Value.ErrorType,
							keyValuePair.Key
						});
						if (value.ErrorType == ErrorType.Transient)
						{
							num++;
						}
						else
						{
							num2++;
						}
					}
				}
				string empty = string.Empty;
				this.HandleOperationFailureCounts(null, operationName, num, num2, empty, true);
				this.LogAdminSyncOperationFailure(operationName, num, num2, empty);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004F18 File Offset: 0x00003118
		private void HandleOperationFailureCounts(EhfCompanyIdentity companyIdentity, string operationName, int operationTransientFailuresCount, int operationPermanentFailuresCount, string failureDetails, bool hasCriticalError)
		{
			if (operationTransientFailuresCount != 0 || operationPermanentFailuresCount != 0)
			{
				base.EhfConnection.PerfCounterHandler.OnPerEntryFailures(operationName, operationTransientFailuresCount, operationPermanentFailuresCount);
			}
			if (operationTransientFailuresCount > 0)
			{
				if (hasCriticalError || companyIdentity == null)
				{
					this.errorTracker.AddCriticalFailure();
				}
				else
				{
					this.errorTracker.AddTransientFailure(companyIdentity, new EhfAdminAccountSynchronizer.EhfAdminSyncTransientException(failureDetails), operationName);
				}
			}
			if (operationPermanentFailuresCount > 0)
			{
				this.errorTracker.AddPermanentFailure();
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004F7C File Offset: 0x0000317C
		private void LogAdminSyncOperationFailure(string operationName, int transientFailure, int permanentFailure, string details)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "AdminSync operation <{0}> failed with <{1}> transient failure(s) and <{2}> permanent failure(s). Details: {3}", new object[]
			{
				operationName,
				transientFailure,
				permanentFailure,
				details
			});
			Exception exception = new EhfAdminAccountSynchronizer.EhfAdminSyncTransientResponseException(text);
			if (transientFailure > 0 && this.errorTracker.AllTransientFailuresCount == 1)
			{
				ExEventLog.EventTuple tuple_EhfAdminSyncTransientFailure = EdgeSyncEventLogConstants.Tuple_EhfAdminSyncTransientFailure;
				base.EventLogAndTraceException(operationName, tuple_EhfAdminSyncTransientFailure, exception, details);
				return;
			}
			if (permanentFailure > 0)
			{
				ExEventLog.EventTuple tuple_EhfAdminSyncPermanentFailure = EdgeSyncEventLogConstants.Tuple_EhfAdminSyncPermanentFailure;
				base.EventLogAndTraceException(operationName, tuple_EhfAdminSyncPermanentFailure, exception, details);
				return;
			}
			base.DiagSession.LogAndTraceError(text, new object[0]);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005016 File Offset: 0x00003216
		private void AbortSyncCycleIfTooManyFailures()
		{
			if (this.errorTracker.HasTooManyFailures)
			{
				this.LogAndAbortSyncCycle();
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000502B File Offset: 0x0000322B
		private void ThrowIfAdminSyncNotEnabled(string dn, ExSearchResultEntry entry)
		{
			if (this.IsSyncEnabledCacheFull())
			{
				return;
			}
			if (!this.syncEnabledConfigCache[dn])
			{
				throw new InvalidOperationException(string.Format("Adminsync is not enabled for tenant <{0}>, but still got a change entry <{1}>", dn, entry.DistinguishedName));
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000505C File Offset: 0x0000325C
		private void UpdateSyncStateInAD(EhfCompanyAdmins admins, EhfADAdapter configADAdapter, bool syncAdminAccountsCompleted, bool syncAdminAgentCompleted, bool syncHelpdeskAgentCompleted)
		{
			EhfAdminSyncState ehfAdminSyncState = EhfAdminSyncState.Create(admins, syncAdminAccountsCompleted, syncAdminAccountsCompleted, syncAdminAgentCompleted, syncHelpdeskAgentCompleted, base.EhfConnection);
			if (ehfAdminSyncState.IsEmpty)
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Admin state is up to date for <{0}>. No need to update the state.", new object[]
				{
					admins.TenantOU
				});
				return;
			}
			try
			{
				configADAdapter.SetAttributeValues(admins.EhfCompanyIdentity.EhfCompanyGuid, ehfAdminSyncState.GetStatesToUpdate());
			}
			catch (ExDirectoryException exception)
			{
				this.HandleFaultAsTransientFailure(admins.EhfCompanyIdentity, "Update Sync State", exception, true, string.Empty);
			}
		}

		// Token: 0x0400001C RID: 28
		private const int MaxCacheSize = 20000;

		// Token: 0x0400001D RID: 29
		private static readonly int OrganizationUnitDepth = new ADObjectId("OU=foo.com,OU=Microsoft Exchange Hosted Organizations").Depth + 1;

		// Token: 0x0400001E RID: 30
		private static readonly string[] TenantAdminsRoleGroupSyncAttributes = new string[]
		{
			"member",
			"msExchWindowsLiveID",
			"msExchProvisioningFlags"
		};

		// Token: 0x0400001F RID: 31
		private static readonly string[] TenantAdminsRoleGroupExtraAttributes = new string[]
		{
			"msExchOURoot",
			"msExchCU",
			"msExchExternalDirectoryObjectId"
		};

		// Token: 0x04000020 RID: 32
		private static readonly string[] AdminSyncAllAttributes = EhfSynchronizer.CombineArrays<string>(EhfAdminAccountSynchronizer.TenantAdminsRoleGroupSyncAttributes, EhfAdminAccountSynchronizer.TenantAdminsRoleGroupExtraAttributes);

		// Token: 0x04000021 RID: 33
		private static readonly string SyncAdminsOperation = "Sync Local Tenant Admins";

		// Token: 0x04000022 RID: 34
		private static readonly string SyncPartnerAdminGroupOperation = "Sync Partner Admin Groups";

		// Token: 0x04000023 RID: 35
		private static readonly string RemoveGroupsOperation = "Remove Admin Groups";

		// Token: 0x04000024 RID: 36
		private static int cycleCount;

		// Token: 0x04000025 RID: 37
		private Dictionary<string, EhfAdminSyncChangeBuilder> adminAccountChange = new Dictionary<string, EhfAdminSyncChangeBuilder>();

		// Token: 0x04000026 RID: 38
		private Dictionary<string, bool> syncEnabledConfigCache = new Dictionary<string, bool>();

		// Token: 0x04000027 RID: 39
		private HashSet<Guid> groupsToRemove = new HashSet<Guid>();

		// Token: 0x04000028 RID: 40
		private EhfSyncErrorTracker errorTracker;

		// Token: 0x0200000E RID: 14
		private class EhfAdminSyncTransientResponseException : Exception
		{
			// Token: 0x06000083 RID: 131 RVA: 0x0000518B File Offset: 0x0000338B
			public EhfAdminSyncTransientResponseException(string message) : base(message)
			{
			}
		}

		// Token: 0x0200000F RID: 15
		private class EhfAdminSyncTransientException : Exception
		{
			// Token: 0x06000084 RID: 132 RVA: 0x00005194 File Offset: 0x00003394
			public EhfAdminSyncTransientException(string message) : base(message)
			{
			}
		}
	}
}
