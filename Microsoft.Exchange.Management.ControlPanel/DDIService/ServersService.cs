using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000478 RID: 1144
	public static class ServersService
	{
		// Token: 0x0600397E RID: 14718 RVA: 0x000AE95C File Offset: 0x000ACB5C
		public static void ConvertIpBinding(string fieldName, MultiValuedProperty<IPBinding> ipBinding, DataRow row)
		{
			MultiValuedProperty<Identity> multiValuedProperty = new MultiValuedProperty<Identity>();
			foreach (IPBinding ipbinding in ipBinding)
			{
				string text = ipbinding.Address.ToString() + ":" + ipbinding.Port.ToString();
				multiValuedProperty.Add(new Identity(text, text));
			}
			row[fieldName] = multiValuedProperty.ToArray();
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x000AE9E8 File Offset: 0x000ACBE8
		public static void OnPrePop3Setting(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			ServersService.OnPrePopImapSetting("Pop3", inputRow, dataTable, store);
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x000AE9F7 File Offset: 0x000ACBF7
		public static void OnPreImap4Setting(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			ServersService.OnPrePopImapSetting("Imap4", inputRow, dataTable, store);
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x000AEA08 File Offset: 0x000ACC08
		public static void OnPrePopImapSetting(string prefix, DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			if (!DBNull.Value.Equals(dataRow[prefix + "PreAuthenticatedConnectionTimeout"]))
			{
				inputRow[prefix + "PreAuthenticatedConnectionTimeout"] = EnhancedTimeSpan.FromSeconds(Convert.ToDouble(dataRow[prefix + "PreAuthenticatedConnectionTimeout"]));
			}
			if (!DBNull.Value.Equals(dataRow[prefix + "AuthenticatedConnectionTimeout"]))
			{
				inputRow[prefix + "AuthenticatedConnectionTimeout"] = EnhancedTimeSpan.FromSeconds(Convert.ToDouble(dataRow[prefix + "AuthenticatedConnectionTimeout"]));
			}
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000AEABE File Offset: 0x000ACCBE
		public static void OnPostPop3Setting(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			ServersService.OnPostPopImapSetting("Pop3", inputRow, dataTable, store);
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000AEACD File Offset: 0x000ACCCD
		public static void OnPostImap4Setting(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			ServersService.OnPostPopImapSetting("Imap4", inputRow, dataTable, store);
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x000AEADC File Offset: 0x000ACCDC
		public static void OnPostPopImapSetting(string prefix, DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow row = dataTable.Rows[0];
			object dataObject = store.GetDataObject(prefix + "AdConfig");
			if (dataObject != null && dataObject is PopImapAdConfiguration)
			{
				PopImapAdConfiguration popImapAdConfiguration = (PopImapAdConfiguration)dataObject;
				if (popImapAdConfiguration.UnencryptedOrTLSBindings != null)
				{
					ServersService.ConvertIpBinding(prefix + "UnencryptedOrTLSBindings", popImapAdConfiguration.UnencryptedOrTLSBindings, row);
				}
				if (popImapAdConfiguration.SSLBindings != null)
				{
					ServersService.ConvertIpBinding(prefix + "SSLBindings", popImapAdConfiguration.SSLBindings, row);
				}
			}
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000AEB58 File Offset: 0x000ACD58
		public static bool IsCurrentExchangeMajorVersion(ServerVersion version)
		{
			return version.Major == Server.CurrentExchangeMajorVersion;
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000AEB68 File Offset: 0x000ACD68
		public static int CountMountedCopy(object databaseCopies)
		{
			int num = 0;
			IEnumerable<object> enumerable = databaseCopies as IEnumerable<object>;
			if (enumerable != null)
			{
				foreach (object obj in enumerable)
				{
					DatabaseCopyStatusEntry databaseCopyStatusEntry = obj as DatabaseCopyStatusEntry;
					if (databaseCopyStatusEntry != null && databaseCopyStatusEntry.Status == CopyStatus.Mounted)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000AEBD4 File Offset: 0x000ACDD4
		public static string GenerateLocalPathString(LocalLongFullPath localLongFullPath)
		{
			if (localLongFullPath != null)
			{
				return localLongFullPath.PathName;
			}
			return string.Empty;
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000AEBEC File Offset: 0x000ACDEC
		private static void ProcessNetworkInfo(MultiValuedProperty<Identity> adapterGuids, ArrayList adapterDNSServers, NetworkConnectionInfo info)
		{
			if (info != null)
			{
				ArrayList arrayList = new ArrayList();
				adapterGuids.Add(new Identity(info.AdapterGuid.ToString(), info.Name));
				foreach (IPAddress ipaddress in info.DnsServers)
				{
					arrayList.Add(ipaddress.ToString());
				}
				adapterDNSServers.Add(arrayList.ToArray());
			}
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000AEC60 File Offset: 0x000ACE60
		public static void OnPostNetworkInfo(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			object dataObject = store.GetDataObject("NetworkInfo");
			ArrayList arrayList = new ArrayList();
			MultiValuedProperty<Identity> multiValuedProperty = new MultiValuedProperty<Identity>();
			multiValuedProperty.Add(new Identity(Guid.Empty.ToString(), Strings.DNSTypeAllIPV4));
			arrayList.Add(new string[0]);
			if (dataObject != null && dataObject is IEnumerable)
			{
				foreach (object obj in (dataObject as IEnumerable))
				{
					ServersService.ProcessNetworkInfo(multiValuedProperty, arrayList, obj as NetworkConnectionInfo);
				}
			}
			multiValuedProperty.Add(new Identity("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF", Strings.DNSTypeCustom));
			arrayList.Add(new string[0]);
			dataRow["AdapterDNSServers"] = arrayList.ToArray();
			dataRow["AdapterGuids"] = multiValuedProperty.ToArray();
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x000AED74 File Offset: 0x000ACF74
		public static void OnPostDNS(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			object obj = dataRow["ExternalDNSAdapterEnabled"];
			if (!DBNull.Value.Equals(obj) && !(bool)obj)
			{
				dataRow["ExternalDNSAdapterGuid"] = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";
			}
			obj = dataRow["InternalDNSAdapterEnabled"];
			if (!DBNull.Value.Equals(obj) && !(bool)obj)
			{
				dataRow["InternalDNSAdapterGuid"] = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";
			}
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000AEE04 File Offset: 0x000AD004
		public static void OnPostTransportLimits(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			if (!DBNull.Value.Equals(dataRow["MaxPerDomainOutboundConnections"]))
			{
				dataRow["MaxPerDomainOutboundConnections"] = DDIUtil.ConvertUnlimitedToString<int>(dataRow["MaxPerDomainOutboundConnections"], (int t) => t.ToString());
			}
			if (!DBNull.Value.Equals(dataRow["MaxOutboundConnections"]))
			{
				dataRow["MaxOutboundConnections"] = DDIUtil.ConvertUnlimitedToString<int>(dataRow["MaxOutboundConnections"], (int t) => t.ToString());
			}
			if (!DBNull.Value.Equals(dataRow["OutboundConnectionFailureRetryInterval"]))
			{
				dataRow["OutboundConnectionFailureRetryInterval"] = ((EnhancedTimeSpan)dataRow["OutboundConnectionFailureRetryInterval"]).ToString(TimeUnit.Second, 0);
			}
			if (!DBNull.Value.Equals(dataRow["TransientFailureRetryInterval"]))
			{
				dataRow["TransientFailureRetryInterval"] = ((EnhancedTimeSpan)dataRow["TransientFailureRetryInterval"]).ToString(TimeUnit.Minute, 2);
			}
			if (!DBNull.Value.Equals(dataRow["MessageExpirationTimeout"]))
			{
				dataRow["MessageExpirationTimeout"] = ((EnhancedTimeSpan)dataRow["MessageExpirationTimeout"]).ToString(TimeUnit.Day, 9);
			}
			if (!DBNull.Value.Equals(dataRow["DelayNotificationTimeout"]))
			{
				dataRow["DelayNotificationTimeout"] = ((EnhancedTimeSpan)dataRow["DelayNotificationTimeout"]).ToString(TimeUnit.Hour, 5);
			}
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000AEFA4 File Offset: 0x000AD1A4
		public static void OnPreTransportServerSetting(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			if (!DBNull.Value.Equals(dataRow["OutboundConnectionFailureRetryInterval"]))
			{
				inputRow["OutboundConnectionFailureRetryInterval"] = ((string)dataRow["OutboundConnectionFailureRetryInterval"]).FromTimeSpan(TimeUnit.Second);
			}
			if (!DBNull.Value.Equals(dataRow["TransientFailureRetryInterval"]))
			{
				inputRow["TransientFailureRetryInterval"] = ((string)dataRow["TransientFailureRetryInterval"]).FromTimeSpan(TimeUnit.Minute);
			}
			if (!DBNull.Value.Equals(dataRow["MessageExpirationTimeout"]))
			{
				inputRow["MessageExpirationTimeout"] = ((string)dataRow["MessageExpirationTimeout"]).FromTimeSpan(TimeUnit.Day);
			}
			if (!DBNull.Value.Equals(dataRow["DelayNotificationTimeout"]))
			{
				inputRow["DelayNotificationTimeout"] = ((string)dataRow["DelayNotificationTimeout"]).FromTimeSpan(TimeUnit.Hour);
			}
		}

		// Token: 0x040026A9 RID: 9897
		private const string DummyGuid = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";

		// Token: 0x040026AA RID: 9898
		private const string MessageExpirationTimeout = "MessageExpirationTimeout";

		// Token: 0x040026AB RID: 9899
		private const string DelayNotificationTimeout = "DelayNotificationTimeout";

		// Token: 0x040026AC RID: 9900
		private const string TransientFailureRetryInterval = "TransientFailureRetryInterval";

		// Token: 0x040026AD RID: 9901
		private const string OutboundConnectionFailureRetryInterval = "OutboundConnectionFailureRetryInterval";

		// Token: 0x040026AE RID: 9902
		private const string MaxPerDomainOutboundConnections = "MaxPerDomainOutboundConnections";

		// Token: 0x040026AF RID: 9903
		private const string MaxOutboundConnections = "MaxOutboundConnections";
	}
}
