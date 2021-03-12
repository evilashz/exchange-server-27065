using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics.Components.Tasks;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x02000697 RID: 1687
	internal sealed class DataMart
	{
		// Token: 0x06003BDD RID: 15325 RVA: 0x000FFFB4 File Offset: 0x000FE1B4
		static DataMart()
		{
			DataMart.ReportingCmdletKeyRoot = string.Format(CultureInfo.InvariantCulture, "SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\Cmdlet\\Reporting", new object[]
			{
				"v15"
			});
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x00100004 File Offset: 0x000FE204
		private DataMart()
		{
			this.dataMartKeyMapping = new Dictionary<DataMartType, string>
			{
				{
					DataMartType.Tenants,
					"DataMartTenants"
				},
				{
					DataMartType.TenantsScaled,
					"DataMartTenantsScaled"
				},
				{
					DataMartType.Transport,
					"DataMartTransport"
				},
				{
					DataMartType.C3,
					"DataMartC3"
				},
				{
					DataMartType.Manageability,
					"DataMartManageability"
				},
				{
					DataMartType.EngineeringFundamentals,
					"DataMartEngineeringFundamentals"
				},
				{
					DataMartType.Datacenter,
					"DataMartDatacenter"
				},
				{
					DataMartType.AM,
					"DataMartAM"
				},
				{
					DataMartType.OspExo,
					"DataMartOspExo"
				},
				{
					DataMartType.TenantSecurity,
					"DataMartTenantSecurity"
				},
				{
					DataMartType.ExoOutlook,
					"DataMartExoOutlook"
				}
			};
			this.dataMartServerMapping = new Dictionary<DataMartType, string>(this.dataMartKeyMapping.Count);
			this.dataMartDatabaseMapping = new Dictionary<DataMartType, string>(this.dataMartKeyMapping.Count);
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x001000D8 File Offset: 0x000FE2D8
		public static DataMart Instance
		{
			get
			{
				if (DataMart.instance == null)
				{
					lock (DataMart.SyncRoot)
					{
						if (DataMart.instance == null)
						{
							DataMart.instance = new DataMart();
						}
					}
				}
				return DataMart.instance;
			}
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x00100138 File Offset: 0x000FE338
		public string GetConnectionString(DataMartType dataMartType, bool backup = false)
		{
			string result;
			try
			{
				string text = this.dataMartKeyMapping[dataMartType];
				string key = string.Format(CultureInfo.InvariantCulture, "{0}Server", new object[]
				{
					text
				});
				string text2 = DataMart.GetConfiguration(dataMartType, this.dataMartServerMapping, key);
				if (backup)
				{
					text2 = this.GetBackupServer(dataMartType);
					if (string.IsNullOrEmpty(text2))
					{
						return string.Empty;
					}
				}
				else
				{
					string text3 = Environment.MachineName.Substring(0, 3).ToUpper();
					if (text2.ToUpper().StartsWith("CDM-TENANTDS."))
					{
						text2 = string.Format(CultureInfo.InvariantCulture, "{0}.{1}.exmgmt.local", new object[]
						{
							"CDM-TENANTDS",
							text3
						});
					}
					else if (text2.ToUpper().StartsWith("CDM-TENANTDS-SCALED."))
					{
						text2 = string.Format(CultureInfo.InvariantCulture, "{0}.{1}.exmgmt.local", new object[]
						{
							"CDM-TENANTDS-SCALED",
							text3
						});
					}
				}
				string key2 = string.Format(CultureInfo.InvariantCulture, "{0}Database", new object[]
				{
					text
				});
				string configuration = DataMart.GetConfiguration(dataMartType, this.dataMartDatabaseMapping, key2);
				if (DataMart.connectionTimeout == -1)
				{
					DataMart.connectionTimeout = DataMart.LoadSettingFromRegistry<int>("SQLConnectionTimeout");
					DataMart.ValidateIntegerInRange("SQLConnectionTimeout", DataMart.connectionTimeout, 1, 180);
				}
				string text4 = string.Format(CultureInfo.InvariantCulture, "Server={0};Database={1};Integrated Security=SSPI;Connection Timeout={2}", new object[]
				{
					text2,
					configuration,
					DataMart.connectionTimeout
				});
				result = text4;
			}
			catch (DataMartConfigurationException ex)
			{
				ExTraceGlobals.LogTracer.TraceError<DataMartConfigurationException>(0L, "Load data mart configuration error: {0}", ex);
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_DataMartConfigurationError, new string[]
				{
					ex.Message
				});
				throw;
			}
			return result;
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x00100314 File Offset: 0x000FE514
		private string GetCFRBackupDataCenterNameByRegion(string regionKey, string serversKey, string localDataCenterName)
		{
			string text = DataMart.LoadSettingFromRegistry<string>(regionKey);
			if (!text.Contains(localDataCenterName))
			{
				return string.Empty;
			}
			string text2 = DataMart.LoadSettingFromRegistry<string>(serversKey);
			List<string> list = new List<string>(text2.Split(new char[]
			{
				','
			}));
			list.Remove(localDataCenterName);
			if (list.Count == 0)
			{
				return string.Empty;
			}
			Random random = new Random();
			return list[random.Next(list.Count)];
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x00100388 File Offset: 0x000FE588
		private string GetCFRBackupDataServerName(DataMartType dataMartType)
		{
			string localDataCenterName = Environment.MachineName.Substring(0, 3).ToUpper();
			string text = string.Empty;
			string result = string.Empty;
			try
			{
				text = this.GetCFRBackupDataCenterNameByRegion("DataMartTenantsRegionNAM", "DataMartTenantsServersNAM", localDataCenterName);
				if (string.IsNullOrEmpty(text))
				{
					text = this.GetCFRBackupDataCenterNameByRegion("DataMartTenantsRegionEUR", "DataMartTenantsServersEUR", localDataCenterName);
				}
				if (string.IsNullOrEmpty(text))
				{
					text = this.GetCFRBackupDataCenterNameByRegion("DataMartTenantsRegionAPC", "DataMartTenantsServersAPC", localDataCenterName);
				}
				if (string.IsNullOrEmpty(text))
				{
					result = DataMart.LoadSettingFromRegistry<string>("DataMartTenantsGlobalBackupServer");
				}
			}
			catch (Exception)
			{
				return string.Empty;
			}
			string text2 = string.Empty;
			switch (dataMartType)
			{
			case DataMartType.Tenants:
				text2 = "cdm-tenantds";
				break;
			case DataMartType.TenantsScaled:
				text2 = "cdm-tenantds-scaled";
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}.{1}.exmgmt.local", new object[]
				{
					text2,
					text
				});
			}
			return result;
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x00100484 File Offset: 0x000FE684
		private string GetBackupServer(DataMartType dataMartType)
		{
			switch (dataMartType)
			{
			case DataMartType.Tenants:
			case DataMartType.TenantsScaled:
				return this.GetCFRBackupDataServerName(dataMartType);
			default:
				return null;
			}
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06003BE4 RID: 15332 RVA: 0x001004AC File Offset: 0x000FE6AC
		public int DefaultReportResultSize
		{
			get
			{
				DataMart.defaultReportResultSize = DataMart.LoadSettingFromRegistry<int>("DefaultReportResultSize");
				if (DataMart.defaultReportResultSize <= 0)
				{
					DataMart.defaultReportResultSize = 1000;
				}
				return DataMart.defaultReportResultSize;
			}
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06003BE5 RID: 15333 RVA: 0x001004D4 File Offset: 0x000FE6D4
		public bool IsTableFunctionQueryDisabled
		{
			get
			{
				DataMart.isTableFunctionQueryDisabled = DataMart.LoadSettingFromRegistry<int>("DisableTableFunctionQuery");
				return DataMart.isTableFunctionQueryDisabled == 1;
			}
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x001004F0 File Offset: 0x000FE6F0
		private static string GetConfiguration(DataMartType dataMartType, Dictionary<DataMartType, string> mapping, string key)
		{
			if (!mapping.ContainsKey(dataMartType))
			{
				lock (DataMart.SyncRoot)
				{
					if (!mapping.ContainsKey(dataMartType))
					{
						string value = DataMart.LoadSettingFromRegistry<string>(key);
						DataMart.ValidateNotEmptyString(key, value);
						mapping[dataMartType] = value;
					}
				}
			}
			return mapping[dataMartType];
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x00100558 File Offset: 0x000FE758
		private static void ValidateNotEmptyString(string key, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new DataMartConfigurationException(Strings.EmptyStringConfiguration(key));
			}
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x0010056E File Offset: 0x000FE76E
		private static void ValidateIntegerInRange(string key, int value, int min, int max)
		{
			if (value > max || value < min)
			{
				throw new DataMartConfigurationException(Strings.InvalidIntegerConfiguration(key, value, min, max));
			}
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x00100588 File Offset: 0x000FE788
		private static TValue LoadSettingFromRegistry<TValue>(string key)
		{
			TValue result = default(TValue);
			Exception ex = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(DataMart.ReportingCmdletKeyRoot))
				{
					object obj = null;
					if (registryKey != null)
					{
						obj = registryKey.GetValue(key);
					}
					if (registryKey == null || obj == null)
					{
						throw new DataMartConfigurationException(Strings.RegistryKeyNotFound(key, DataMart.ReportingCmdletKeyRoot));
					}
					result = (TValue)((object)obj);
				}
			}
			catch (SecurityException ex2)
			{
				ex = ex2;
			}
			catch (InvalidCastException ex3)
			{
				ex = ex3;
			}
			catch (FormatException ex4)
			{
				ex = ex4;
			}
			catch (IOException ex5)
			{
				ex = ex5;
			}
			catch (UnauthorizedAccessException ex6)
			{
				ex = ex6;
			}
			if (ex != null)
			{
				ExTraceGlobals.LogTracer.TraceError<Exception>(0L, "Error occurred when reading settings from registry. Exception: {0}", ex);
				throw new DataMartConfigurationException(Strings.FailedLoadRegistryKey(key, ex.Message), ex);
			}
			return result;
		}

		// Token: 0x040026F1 RID: 9969
		public const int MinConnectionTimeout = 1;

		// Token: 0x040026F2 RID: 9970
		public const int MaxConnectionTimeout = 180;

		// Token: 0x040026F3 RID: 9971
		private const string ReportingCmdletKeyRootFormat = "SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\Cmdlet\\Reporting";

		// Token: 0x040026F4 RID: 9972
		private const string ServerKeyFormat = "{0}Server";

		// Token: 0x040026F5 RID: 9973
		private const string DatabaseKeyFormat = "{0}Database";

		// Token: 0x040026F6 RID: 9974
		private const string ConnectionTimeoutKey = "SQLConnectionTimeout";

		// Token: 0x040026F7 RID: 9975
		private const string DefaultReportResultSizeKey = "DefaultReportResultSize";

		// Token: 0x040026F8 RID: 9976
		private const string DisableTableFunctionQueryKey = "DisableTableFunctionQuery";

		// Token: 0x040026F9 RID: 9977
		private const string DataMartTenantsRegionAPCKey = "DataMartTenantsRegionAPC";

		// Token: 0x040026FA RID: 9978
		private const string DataMartTenantsRegionEURKey = "DataMartTenantsRegionEUR";

		// Token: 0x040026FB RID: 9979
		private const string DataMartTenantsRegionNAMKey = "DataMartTenantsRegionNAM";

		// Token: 0x040026FC RID: 9980
		private const string DataMartTenantsServersAPCKey = "DataMartTenantsServersAPC";

		// Token: 0x040026FD RID: 9981
		private const string DataMartTenantsServersEURKey = "DataMartTenantsServersEUR";

		// Token: 0x040026FE RID: 9982
		private const string DataMartTenantsServersNAMKey = "DataMartTenantsServersNAM";

		// Token: 0x040026FF RID: 9983
		private const string DataMartTenantsServersGlobalBackupKey = "DataMartTenantsGlobalBackupServer";

		// Token: 0x04002700 RID: 9984
		private const string ConnectionStringFormat = "Server={0};Database={1};Integrated Security=SSPI;Connection Timeout={2}";

		// Token: 0x04002701 RID: 9985
		private const string CFRServerCNameFormat = "{0}.{1}.exmgmt.local";

		// Token: 0x04002702 RID: 9986
		private static readonly string ReportingCmdletKeyRoot;

		// Token: 0x04002703 RID: 9987
		private static readonly object SyncRoot = new object();

		// Token: 0x04002704 RID: 9988
		private readonly Dictionary<DataMartType, string> dataMartKeyMapping;

		// Token: 0x04002705 RID: 9989
		private readonly Dictionary<DataMartType, string> dataMartServerMapping;

		// Token: 0x04002706 RID: 9990
		private readonly Dictionary<DataMartType, string> dataMartDatabaseMapping;

		// Token: 0x04002707 RID: 9991
		private static int connectionTimeout = -1;

		// Token: 0x04002708 RID: 9992
		private static int defaultReportResultSize = -1;

		// Token: 0x04002709 RID: 9993
		private static int isTableFunctionQueryDisabled = 0;

		// Token: 0x0400270A RID: 9994
		private static volatile DataMart instance;
	}
}
