using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000213 RID: 531
	public class HybridConfigurationWizardServiceCodeBehind
	{
		// Token: 0x17001BF9 RID: 7161
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x00079F14 File Offset: 0x00078114
		private static HybridFeature[] DefaultFeatures
		{
			get
			{
				if (HybridConfigurationWizardServiceCodeBehind.defaultFeatures == null)
				{
					HybridConfigurationWizardServiceCodeBehind.defaultFeatures = new HybridFeature[]
					{
						HybridFeature.FreeBusy,
						HybridFeature.MoveMailbox,
						HybridFeature.Mailtips,
						HybridFeature.MessageTracking,
						HybridFeature.OwaRedirection,
						HybridFeature.OnlineArchive,
						HybridFeature.SecureMail,
						HybridFeature.Photos
					};
				}
				return HybridConfigurationWizardServiceCodeBehind.defaultFeatures;
			}
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x00079F58 File Offset: 0x00078158
		public static void PrepareFeatureParams(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			bool flag = inputRow["CentralizedTransport"] != DBNull.Value && (bool)inputRow["CentralizedTransport"];
			inputRow["Features"] = (dataTable.Rows[0]["Features"] = (flag ? HybridConfigurationWizardServiceCodeBehind.DefaultFeatures.Concat(new HybridFeature[]
			{
				HybridFeature.CentralizedTransport
			}).ToArray<HybridFeature>() : HybridConfigurationWizardServiceCodeBehind.DefaultFeatures));
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x00079FD4 File Offset: 0x000781D4
		public static void GetServiceInstanceParam(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			string value = HttpContext.Current.Request.QueryString["ServiceInstance"];
			inputRow["IncomingServiceInstance"] = (dataTable.Rows[0]["IncomingServiceInstance"] = value);
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x0007A020 File Offset: 0x00078220
		public static void SetGallatinFlag(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			string a = dataTable.Rows[0]["ServiceInstance"] as string;
			string a2 = inputRow["ServiceInstance"] as string;
			inputRow["IsGallatin"] = (dataTable.Rows[0]["IsGallatin"] = (string.Equals(a, "1") || string.Equals(a2, "1")));
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x0007A0A0 File Offset: 0x000782A0
		public static void ClearOutputForOlderVersion(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			HybridConfiguration hybridConfiguration = store.GetDataObject("HybridConfiguration") as HybridConfiguration;
			IntraOrganizationConfiguration intraOrganizationConfiguration = store.GetDataObject("IntraOrganizationConfiguration") as IntraOrganizationConfiguration;
			bool flag = hybridConfiguration != null && DDIHelper.IsLegacyObject(hybridConfiguration);
			if (flag)
			{
				dataTable.Rows.Clear();
				store.UpdateDataObject("HybridConfiguration", null);
				DataRow row = dataTable.NewRow();
				dataTable.Rows.Add(row);
			}
			dataTable.Rows[0]["NeedUpgrade"] = flag;
			if (object.Equals(dataTable.Rows[0]["IncomingServiceInstance"], "1"))
			{
				inputRow["IsGallatin"] = (dataTable.Rows[0]["IsGallatin"] = true);
				if (OrganizationCache.RestrictIOCToSP1OrGreaterGallatin && intraOrganizationConfiguration.DeploymentIsCompleteIOCReady == false)
				{
					dataTable.Rows[0]["GallatinBlock"] = true;
					return;
				}
			}
			else if (!OrganizationCache.RestrictIOCToSP1OrGreaterWorldWide || intraOrganizationConfiguration.DeploymentIsCompleteIOCReady == true)
			{
				dataTable.Rows[0]["WWoAuth"] = true;
			}
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x0007A22C File Offset: 0x0007842C
		public static void GetDomainOptions(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			List<string> cloudDomainNames = null;
			string text = inputRow["CloudDomains"] as string;
			if (text != null)
			{
				cloudDomainNames = (from cloudDomain in text.Split(new char[]
				{
					','
				})
				select cloudDomain.Trim()).ToList<string>();
			}
			IEnumerable<object> source = store.GetDataObject("AcceptedDomain") as IEnumerable<object>;
			IEnumerable<object> enumerable = from domain in source
			where HybridConfigurationWizardServiceCodeBehind.IsSelectableDomain((Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain)domain, cloudDomainNames)
			select ((Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain)domain).DomainName.SmtpDomain;
			int num = enumerable.Count<object>();
			if (num != 0)
			{
				inputRow["DomainOptions"] = (dataTable.Rows[0]["DomainOptions"] = enumerable);
			}
			if (num == 1)
			{
				store.SetModifiedColumns(new List<string>
				{
					"Domains"
				});
				inputRow["Domains"] = (dataTable.Rows[0]["Domains"] = enumerable.First<object>().ToString());
			}
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x0007A368 File Offset: 0x00078568
		public static void ExpireServiceInstanceCache(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			OrganizationCache.ExpireEntry("ServiceInstance");
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x0007A374 File Offset: 0x00078574
		private static bool IsSelectableDomain(Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain domain, List<string> optionList)
		{
			SmtpDomain smtpDomain = domain.DomainName.SmtpDomain;
			return domain.DomainType != AcceptedDomainType.ExternalRelay && smtpDomain != null && (optionList == null || optionList.Contains(smtpDomain.ToString(), StringComparer.OrdinalIgnoreCase));
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x0007A3FC File Offset: 0x000785FC
		public static void FilterOutFederatedDomains(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			IEnumerable<object> source = (IEnumerable<object>)inputRow["DomainOptions"];
			object obj = dataRow["FederatedDomains"];
			string[] value = new string[0];
			if (obj != DBNull.Value)
			{
				List<string> federatedDomainNames = (from domain in (MultiValuedProperty<FederatedDomain>)obj
				select domain.Domain.Domain).ToList<string>();
				value = (from smtpDomain in source
				where !federatedDomainNames.Contains(((SmtpDomain)smtpDomain).Domain)
				select ((SmtpDomain)smtpDomain).Domain).ToArray<string>();
			}
			else
			{
				value = (from smtpDomain in source
				select ((SmtpDomain)smtpDomain).Domain).ToArray<string>();
			}
			inputRow["DomainsNeedProofing"] = (dataRow["DomainsNeedProofing"] = value);
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x0007A500 File Offset: 0x00078700
		public static void AddDomainProof(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			string domainName = (string)dataRow["domain"];
			string text = dataRow["Proof"] as string;
			if (text != null)
			{
				List<SharingDomain> list = dataRow["SharingDomains"] as List<SharingDomain>;
				if (list == null)
				{
					list = (dataRow["SharingDomains"] = new List<SharingDomain>());
				}
				list.Add(new SharingDomain
				{
					DomainName = domainName,
					Proof = text,
					SharingStatus = 3
				});
			}
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x0007A5A8 File Offset: 0x000787A8
		public static void FindValidCertExistingOnEveryServer(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			List<MailCertificate> list = dataRow["MailCertificateOptions"] as List<MailCertificate>;
			IEnumerable<object> enumerable = store.GetDataObject("ExchangeCertificates") as IEnumerable<object>;
			if (list == null)
			{
				list = (dataRow["MailCertificateOptions"] = new List<MailCertificate>());
				using (IEnumerator<object> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						ExchangeCertificate exchangeCertificate = (ExchangeCertificate)obj;
						if (HybridConfigurationWizardServiceCodeBehind.IsValidCertificate(exchangeCertificate))
						{
							list.Add(new MailCertificate
							{
								FriendlyName = exchangeCertificate.FriendlyName,
								Thumbprint = exchangeCertificate.Thumbprint,
								Issuer = exchangeCertificate.Issuer,
								Subject = exchangeCertificate.Subject
							});
						}
					}
					return;
				}
			}
			IEnumerable<string> source = from cert in enumerable
			where HybridConfigurationWizardServiceCodeBehind.IsValidCertificate((ExchangeCertificate)cert)
			select ((ExchangeCertificate)cert).Thumbprint;
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (!source.Contains(list[i].Thumbprint))
				{
					list.RemoveAt(i);
				}
			}
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x0007A704 File Offset: 0x00078904
		public static string CreateTlsCertificateName(string issuer, string subject)
		{
			return string.Format("<I>{0}<S>{1}", issuer, subject);
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x0007A714 File Offset: 0x00078914
		public static string GetIssuer(object tlsCertificateName)
		{
			SmtpX509Identifier smtpX509Identifier = tlsCertificateName as SmtpX509Identifier;
			if (smtpX509Identifier == null)
			{
				return string.Empty;
			}
			return smtpX509Identifier.CertificateIssuer;
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x0007A738 File Offset: 0x00078938
		public static string GetSubject(object tlsCertificateName)
		{
			SmtpX509Identifier smtpX509Identifier = tlsCertificateName as SmtpX509Identifier;
			if (smtpX509Identifier == null)
			{
				return string.Empty;
			}
			return smtpX509Identifier.CertificateSubject;
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x0007A75C File Offset: 0x0007895C
		public static void AddCertificates(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			List<MailCertificate> list = new List<MailCertificate>();
			IEnumerable<object> enumerable = store.GetDataObject("ExchangeCertificates") as IEnumerable<object>;
			foreach (object obj in enumerable)
			{
				ExchangeCertificate exchangeCertificate = (ExchangeCertificate)obj;
				if (HybridConfigurationWizardServiceCodeBehind.IsValidCertificate(exchangeCertificate))
				{
					list.Add(new MailCertificate
					{
						FriendlyName = exchangeCertificate.FriendlyName,
						Thumbprint = exchangeCertificate.Thumbprint,
						Subject = exchangeCertificate.Subject,
						Issuer = exchangeCertificate.Issuer
					});
				}
			}
			DataRow dataRow = dataTable.NewRow();
			dataTable.Rows.Add(dataRow);
			dataRow["MailCertificateOptions"] = list;
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x0007A828 File Offset: 0x00078A28
		public static void RefreshHybridUrl(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			OrganizationCache.ExpireEntry("EntHasTargetDeliveryDomain");
			OrganizationCache.ExpireEntry("EntTargetDeliveryDomain");
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x0007A83E File Offset: 0x00078A3E
		private static bool IsValidCertificate(ExchangeCertificate cert)
		{
			return !cert.IsSelfSigned && cert.Status == CertificateStatus.Valid;
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x0007A853 File Offset: 0x00078A53
		public static object MergeServerNames(object receivingServers, object sendingServers)
		{
			return ((object[])receivingServers).Union((object[])sendingServers);
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x0007A868 File Offset: 0x00078A68
		public static bool GetIsCentralizedTransportEnabled(object features)
		{
			bool result = false;
			if (features != DBNull.Value)
			{
				MultiValuedProperty<HybridFeature> multiValuedProperty = (MultiValuedProperty<HybridFeature>)features;
				result = multiValuedProperty.Contains(HybridFeature.CentralizedTransport);
			}
			return result;
		}

		// Token: 0x04001FB8 RID: 8120
		private static HybridFeature[] defaultFeatures;
	}
}
