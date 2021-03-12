using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200003A RID: 58
	public static class CertificateHelper
	{
		// Token: 0x06001953 RID: 6483 RVA: 0x0004F880 File Offset: 0x0004DA80
		private static void GetOneItem(DataRow row, string serviceDomain, string host)
		{
			if (DBNull.Value.Equals(row[serviceDomain]))
			{
				row[serviceDomain] = host;
				return;
			}
			string text = (string)row[serviceDomain];
			if (text.IndexOf(host) == -1)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(text);
				stringBuilder.Append(",");
				stringBuilder.Append(host);
				row[serviceDomain] = stringBuilder.ToString();
			}
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0004F8F0 File Offset: 0x0004DAF0
		private static void UpdateServiceDomain(DataTable dataTable, DataObjectStore store, string serviceName)
		{
			DataRow row = dataTable.Rows[0];
			ADVirtualDirectory advirtualDirectory = (ADVirtualDirectory)store.GetDataObject(serviceName + "VirtualDirectory");
			if (advirtualDirectory != null && advirtualDirectory.ExternalUrl != null)
			{
				CertificateHelper.GetOneItem(row, serviceName + "ExternalDomain", advirtualDirectory.ExternalUrl.Host);
			}
			if (advirtualDirectory != null && advirtualDirectory.InternalUrl != null)
			{
				CertificateHelper.GetOneItem(row, serviceName + "InternalDomain", advirtualDirectory.InternalUrl.Host);
			}
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0004F97C File Offset: 0x0004DB7C
		private static void UpdateConfigDomain(DataTable dataTable, DataObjectStore store, string cfgName)
		{
			DataRow row = dataTable.Rows[0];
			PopImapAdConfiguration popImapAdConfiguration = (PopImapAdConfiguration)store.GetDataObject(cfgName + "Configuration");
			if (popImapAdConfiguration != null && !string.IsNullOrEmpty(popImapAdConfiguration.X509CertificateName))
			{
				CertificateHelper.GetOneItem(row, cfgName + "Domain", popImapAdConfiguration.X509CertificateName);
			}
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0004F9D4 File Offset: 0x0004DBD4
		public static void OnPreGetExchangeServer(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			Identity identity = dataRow["Identity"] as Identity;
			if (identity == null || string.IsNullOrEmpty(identity.RawIdentity))
			{
				throw new ArgumentException("identity");
			}
			string[] array = identity.RawIdentity.Split(new char[]
			{
				'\\'
			});
			if (array == null || array.Length < 2)
			{
				throw new ArgumentException("identity");
			}
			dataRow["Server"] = array[0];
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0004FA5C File Offset: 0x0004DC5C
		public static void GetServerListPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			inputRow["MaxMajorVersion"] = "15";
			inputRow["MinMajorVersion"] = "15";
			inputRow["ServerRole"] = ServerRole.ClientAccess.ToString() + "," + ServerRole.Mailbox.ToString();
			store.SetModifiedColumns(new List<string>
			{
				"MaxMajorVersion",
				"MinMajorVersion",
				"ServerRole"
			});
			ServerProperties.GetListPostAction(inputRow, dataTable, store);
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0004FAEA File Offset: 0x0004DCEA
		public static void GetOWAVirtualDirectoryPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			CertificateHelper.UpdateServiceDomain(dataTable, store, "Owa");
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0004FAF8 File Offset: 0x0004DCF8
		public static void GetMobileVirtualDirectoryPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			CertificateHelper.UpdateServiceDomain(dataTable, store, "Mobile");
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0004FB06 File Offset: 0x0004DD06
		public static void GetOABVirtualDirectoryPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			CertificateHelper.UpdateServiceDomain(dataTable, store, "OAB");
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0004FB14 File Offset: 0x0004DD14
		public static void GetWebServicesVirtualDirectoryPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			CertificateHelper.UpdateServiceDomain(dataTable, store, "WebServices");
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0004FB24 File Offset: 0x0004DD24
		public static void GetAutoDiscoverVirtualDirectoryPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow row = dataTable.Rows[0];
			ClientAccessServer clientAccessServer = (ClientAccessServer)store.GetDataObject("ClientAccessServer");
			if (clientAccessServer != null && clientAccessServer.AutoDiscoverServiceInternalUri != null)
			{
				CertificateHelper.GetOneItem(row, "AutoInternalDomain", clientAccessServer.AutoDiscoverServiceInternalUri.Host);
			}
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0004FB78 File Offset: 0x0004DD78
		public static void GetAutoDiscoverAcceptDomainPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow row = dataTable.Rows[0];
			object dataObject = store.GetDataObject("AcceptedDomainWholeObject");
			if (dataObject != null && dataObject is IEnumerable)
			{
				foreach (object obj in (dataObject as IEnumerable))
				{
					Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain acceptedDomain = obj as Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain;
					if (acceptedDomain != null)
					{
						CertificateHelper.GetOneItem(row, "AutoExternalDomain", "AutoDiscover." + acceptedDomain.DomainName.Domain);
					}
				}
			}
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0004FC1C File Offset: 0x0004DE1C
		public static void GetPOPSettingsPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			CertificateHelper.UpdateConfigDomain(dataTable, store, "Pop");
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0004FC2A File Offset: 0x0004DE2A
		public static void GetImapSettingsPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			CertificateHelper.UpdateConfigDomain(dataTable, store, "Imap");
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0004FC38 File Offset: 0x0004DE38
		public static void GetOutlookAnyWherePostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow row = dataTable.Rows[0];
			ADRpcHttpVirtualDirectory adrpcHttpVirtualDirectory = (ADRpcHttpVirtualDirectory)store.GetDataObject("RpcHttpVirtualDirectory");
			if (adrpcHttpVirtualDirectory != null && adrpcHttpVirtualDirectory.ExternalHostname != null)
			{
				CertificateHelper.GetOneItem(row, "OutlookDomain", adrpcHttpVirtualDirectory.ExternalHostname.HostnameString);
			}
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0004FC84 File Offset: 0x0004DE84
		public static void GetAcceptDomainPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				SmtpDomainWithSubdomains smtpDomainWithSubdomains = (SmtpDomainWithSubdomains)dataRow["AcceptDomainName"];
				if (smtpDomainWithSubdomains != null)
				{
					dataRow["AcceptedName"] = smtpDomainWithSubdomains.Domain;
				}
			}
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0004FCFC File Offset: 0x0004DEFC
		public static void OnPreNewSelfSignedCertificate(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			dataRow["PlainPassword"] = Guid.NewGuid().ToString().ConvertToSecureString();
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0004FD3C File Offset: 0x0004DF3C
		public static void OnPostExportSelfSignedCertificate(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			object dataObject = store.GetDataObject("BinaryFileDataObject");
			if (dataObject != null && dataObject is IEnumerable)
			{
				foreach (object obj in ((IEnumerable)dataObject))
				{
					BinaryFileDataObject binaryFileDataObject = (BinaryFileDataObject)obj;
					if (binaryFileDataObject != null)
					{
						dataRow["FileData"] = binaryFileDataObject.FileData;
						break;
					}
				}
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0004FDCC File Offset: 0x0004DFCC
		public static void GetListPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["ExDTNotAfter"] = ((ExDateTime)((DateTime)dataRow["NotAfter"])).ToShortDateString();
				CertificateHelper.TranslateStatusIntoShortDescription(dataRow);
			}
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0004FE4C File Offset: 0x0004E04C
		public static void GetSDOPostAction(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			DataRow dataRow = table.Rows[0];
			if (!DBNull.Value.Equals(dataRow["IsSelfSigned"]))
			{
				dataRow["CertType"] = (((bool)dataRow["IsSelfSigned"]) ? Strings.SelfSignedCertificate.ToString() : Strings.CASignedCertificate.ToString());
			}
			if (!DBNull.Value.Equals(dataRow["NotAfter"]))
			{
				dataRow["ExDTNotAfter"] = ((ExDateTime)((DateTime)dataRow["NotAfter"])).ToShortDateString();
			}
			CertificateHelper.TranslateStatusIntoShortDescription(dataRow);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0004FF08 File Offset: 0x0004E108
		public static void GetObjectPostAction(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			DataRow dataRow = table.Rows[0];
			dataRow["ExDTNotAfter"] = ((ExDateTime)((DateTime)dataRow["NotAfter"])).ToShortDateString();
			ExchangeCertificate exchangeCertificate = (ExchangeCertificate)store.GetDataObject("ExchangeCertificate");
			if (exchangeCertificate != null)
			{
				dataRow["SubjectName"] = exchangeCertificate.SubjectName.Name;
			}
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0004FF74 File Offset: 0x0004E174
		private static void TranslateStatusIntoShortDescription(DataRow row)
		{
			CertificateStatus key = (CertificateStatus)row["Status"];
			row["DisplayStatus"] = CertificateHelper.CertStatusToShortDescDict[key].ToString();
		}

		// Token: 0x04001ABB RID: 6843
		private static readonly Dictionary<CertificateStatus, LocalizedString> CertStatusToShortDescDict = new Dictionary<CertificateStatus, LocalizedString>
		{
			{
				CertificateStatus.DateInvalid,
				Strings.DateinvalidStatus
			},
			{
				CertificateStatus.Invalid,
				Strings.InvalidStatus
			},
			{
				CertificateStatus.PendingRequest,
				Strings.PendingRequestStatus
			},
			{
				CertificateStatus.RevocationCheckFailure,
				Strings.RevocationCheckFailureStatus
			},
			{
				CertificateStatus.Revoked,
				Strings.RevokedStatus
			},
			{
				CertificateStatus.Unknown,
				Strings.UnknownStatus
			},
			{
				CertificateStatus.Untrusted,
				Strings.UntrustedStatus
			},
			{
				CertificateStatus.Valid,
				Strings.ValidStatus
			}
		};
	}
}
