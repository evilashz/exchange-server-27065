using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020001EA RID: 490
	public class FederationCodeBehind
	{
		// Token: 0x06002600 RID: 9728 RVA: 0x00074B12 File Offset: 0x00072D12
		public static void InitializateForGettingState(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			dataTable.Rows[0]["FederationDisabled"] = false;
			dataTable.Rows[0]["FederationPartialDisabled"] = false;
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x00074B4C File Offset: 0x00072D4C
		public static void SetIfFederatedDisable(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			if (!Datacenter.IsMultiTenancyEnabled() && store.GetDataObject("FederationTrust") == null)
			{
				dataTable.Rows[0]["FederationDisabled"] = true;
			}
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x00074B80 File Offset: 0x00072D80
		public static void SetIfFederatedPartialDisable(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			if (store.GetDataObject("FederatedOrganizationIdentifier") != null && !(bool)store.GetValue("FederatedOrganizationIdentifier", "Enabled") && store.GetValue("FederatedOrganizationIdentifier", "AccountNamespace") != null)
			{
				dataTable.Rows[0]["FederationDisabled"] = true;
				dataTable.Rows[0]["FederationPartialDisabled"] = true;
			}
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x00074BFC File Offset: 0x00072DFC
		public static void ExtractAccountNamespaceAndSharingDomains(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			if (store.GetDataObject("FederatedOrganizationIdentifier") == null)
			{
				return;
			}
			FederatedOrganizationIdWithDomainStatus federatedOrganizationIdWithDomainStatus = (FederatedOrganizationIdWithDomainStatus)store.GetDataObject("FederatedOrganizationIdentifier");
			SmtpDomain smtpDomain = (federatedOrganizationIdWithDomainStatus.AccountNamespace == null) ? null : new SmtpDomain(FederatedOrganizationId.RemoveHybridConfigurationWellKnownSubDomain(federatedOrganizationIdWithDomainStatus.AccountNamespace.Domain));
			MultiValuedProperty<FederatedDomain> domains = federatedOrganizationIdWithDomainStatus.Domains;
			dataTable.Rows[0]["HasAccountNamespace"] = false;
			dataTable.Rows[0]["HasFederatedDomains"] = false;
			if (smtpDomain != null)
			{
				List<object> list = new List<object>();
				dataTable.Rows[0]["HasAccountNamespace"] = true;
				dataTable.Rows[0]["Name"] = federatedOrganizationIdWithDomainStatus.AccountNamespace.Domain;
				foreach (FederatedDomain federatedDomain in domains)
				{
					if (federatedDomain.Domain.Domain.Equals(smtpDomain.Domain, StringComparison.InvariantCultureIgnoreCase))
					{
						dataTable.Rows[0]["AccountNamespace"] = (SharingDomain)federatedDomain;
					}
					else
					{
						list.Add((SharingDomain)federatedDomain);
					}
				}
				if (list.Count > 0)
				{
					dataTable.Rows[0]["SharingEnabledDomains"] = list;
					dataTable.Rows[0]["HasFederatedDomains"] = true;
				}
			}
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x00074D90 File Offset: 0x00072F90
		public static void ExtractPendingAccountNamespaceAndSharingDomains(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			if (store.GetDataObject("PendingFederatedDomain") == null)
			{
				return;
			}
			PendingFederatedDomain pendingFederatedDomain = store.GetDataObject("PendingFederatedDomain") as PendingFederatedDomain;
			SmtpDomain pendingAccountNamespace = pendingFederatedDomain.PendingAccountNamespace;
			SmtpDomain[] pendingDomains = pendingFederatedDomain.PendingDomains;
			if (dataTable.Rows[0]["HasAccountNamespace"] != DBNull.Value && !(bool)dataTable.Rows[0]["HasAccountNamespace"] && pendingAccountNamespace != null)
			{
				dataTable.Rows[0]["AccountNamespace"] = (SharingDomain)pendingAccountNamespace;
			}
			List<object> list = new List<object>();
			if (dataTable.Rows[0]["SharingEnabledDomains"] != DBNull.Value)
			{
				foreach (object item in ((IEnumerable)dataTable.Rows[0]["SharingEnabledDomains"]))
				{
					list.Add(item);
				}
			}
			foreach (SmtpDomain smtpDomain in pendingDomains)
			{
				if (!list.Contains((SharingDomain)smtpDomain))
				{
					list.Add((SharingDomain)smtpDomain);
				}
			}
			if (list.Count > 0)
			{
				dataTable.Rows[0]["SharingEnabledDomains"] = list;
				dataTable.Rows[0]["HasFederatedDomains"] = true;
			}
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x00074F20 File Offset: 0x00073120
		public static void PreparePendingDomains(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			List<SmtpDomain> list = new List<SmtpDomain>();
			if (dataTable.Rows[0]["AddedSharingEnabledDomains"] != DBNull.Value)
			{
				foreach (object obj in ((IEnumerable)dataTable.Rows[0]["AddedSharingEnabledDomains"]))
				{
					SmtpDomain item = (SmtpDomain)obj;
					list.Add(item);
				}
			}
			row["PendingDomains"] = list.ToArray();
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x00074FC4 File Offset: 0x000731C4
		public static void GenerateAddAndRemoveCollection(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			if (store.GetDataObject("FederatedOrganizationIdentifier") == null)
			{
				return;
			}
			List<object> list = new List<object>();
			if (dataTable.Rows[0]["SharingEnabledDomains"] != DBNull.Value)
			{
				foreach (object obj in ((IEnumerable)dataTable.Rows[0]["SharingEnabledDomains"]))
				{
					SharingDomain sharingDomain = (SharingDomain)obj;
					list.Add((SmtpDomain)sharingDomain);
				}
			}
			FederatedOrganizationIdWithDomainStatus federatedOrganizationIdWithDomainStatus = (FederatedOrganizationIdWithDomainStatus)store.GetDataObject("FederatedOrganizationIdentifier");
			SmtpDomain smtpDomain = (federatedOrganizationIdWithDomainStatus.AccountNamespace == null) ? null : new SmtpDomain(FederatedOrganizationId.RemoveHybridConfigurationWellKnownSubDomain(federatedOrganizationIdWithDomainStatus.AccountNamespace.Domain));
			if (smtpDomain == null && dataTable.Rows[0]["AccountNamespace"] != DBNull.Value)
			{
				smtpDomain = (SmtpDomain)((SharingDomain)dataTable.Rows[0]["AccountNamespace"]);
			}
			if (smtpDomain != null)
			{
				list.Remove(smtpDomain);
			}
			MultiValuedProperty<FederatedDomain> multiValuedProperty = (MultiValuedProperty<FederatedDomain>)store.GetValue("FederatedOrganizationIdentifier", "Domains");
			List<object> list2 = new List<object>();
			if (multiValuedProperty != null)
			{
				foreach (FederatedDomain federatedDomain in multiValuedProperty)
				{
					if (!federatedDomain.Domain.Domain.Equals(smtpDomain.Domain, StringComparison.InvariantCultureIgnoreCase))
					{
						SmtpDomain item = new SmtpDomain(federatedDomain.Domain.Domain);
						if (list.Contains(item))
						{
							list.Remove(item);
						}
						else
						{
							list2.Add(item);
						}
					}
				}
			}
			dataTable.Rows[0]["AddedSharingEnabledDomains"] = list;
			dataTable.Rows[0]["RemovedSharingEnabledDomains"] = list2;
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000751C4 File Offset: 0x000733C4
		public static void AddDomainProof(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			string domainName = (string)dataTable.Rows[0]["DomainName"];
			string proof = (string)dataTable.Rows[0]["Proof"];
			SharingDomain sharingDomain = new SharingDomain();
			sharingDomain.DomainName = domainName;
			sharingDomain.SharingStatus = 3;
			sharingDomain.Proof = proof;
			sharingDomain.RawIdentity = sharingDomain.DomainName;
			if (dataTable.Rows[0]["GetFederatedDomainProofResult"] == DBNull.Value)
			{
				dataTable.Rows[0]["GetFederatedDomainProofResult"] = new List<SharingDomain>();
			}
			List<SharingDomain> list = (List<SharingDomain>)dataTable.Rows[0]["GetFederatedDomainProofResult"];
			list.Add(sharingDomain);
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x0007528C File Offset: 0x0007348C
		public static void AddDomainProofForFailedAccountNamespace(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			SharingDomain sharingDomain = (SharingDomain)dataTable.Rows[0]["AccountNamespace"];
			string proof = (string)dataTable.Rows[0]["Proof"];
			sharingDomain.Proof = proof;
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000752D8 File Offset: 0x000734D8
		public static void AddDomainProofForFailedDomain(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			SharingDomain sharingDomain = (SharingDomain)dataTable.Rows[0]["domain"];
			string proof = (string)dataTable.Rows[0]["Proof"];
			sharingDomain.Proof = proof;
			sharingDomain.RawIdentity = sharingDomain.DomainName;
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x00075330 File Offset: 0x00073530
		public static void CalculateStringForEnabledUI(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			if (store.GetValue("FederatedOrganizationIdentifier", "AccountNamespace") == null)
			{
				dataTable.Rows[0]["EnabledStringType"] = 0;
				return;
			}
			if (((SmtpDomain[])store.GetValue("PendingFederatedDomain", "PendingDomains")).Length > 0)
			{
				dataTable.Rows[0]["EnabledStringType"] = 1;
				return;
			}
			dataTable.Rows[0]["EnabledStringType"] = 2;
		}
	}
}
