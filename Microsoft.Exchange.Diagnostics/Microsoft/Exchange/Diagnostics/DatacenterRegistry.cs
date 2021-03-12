using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000119 RID: 281
	public static class DatacenterRegistry
	{
		// Token: 0x06000822 RID: 2082 RVA: 0x00020C8D File Offset: 0x0001EE8D
		internal static bool IsMicrosoftHostedOnly()
		{
			if (DatacenterRegistry.isMicrosoftHostedOnly == null)
			{
				DatacenterRegistry.isMicrosoftHostedOnly = new bool?(DatacenterRegistry.CheckBooleanValue("DatacenterMode"));
			}
			return DatacenterRegistry.isMicrosoftHostedOnly.Value;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00020CB9 File Offset: 0x0001EEB9
		internal static bool TreatPreReqErrorsAsWarnings()
		{
			if (DatacenterRegistry.treatPreReqErrorsAsWarningsKey == null)
			{
				DatacenterRegistry.treatPreReqErrorsAsWarningsKey = new bool?(DatacenterRegistry.CheckBooleanValue("TreatPreReqErrorsAsWarnings"));
			}
			return DatacenterRegistry.treatPreReqErrorsAsWarningsKey.Value;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00020CE5 File Offset: 0x0001EEE5
		internal static bool IsForefrontForOffice()
		{
			if (DatacenterRegistry.isFfoDatacenter == null)
			{
				DatacenterRegistry.isFfoDatacenter = new bool?(DatacenterRegistry.CheckBooleanValue("ForefrontForOfficeMode"));
			}
			return DatacenterRegistry.isFfoDatacenter.Value;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00020D11 File Offset: 0x0001EF11
		internal static bool IsForefrontForOfficeDeployment()
		{
			if (DatacenterRegistry.isFfoDatacenterDeployment == null)
			{
				DatacenterRegistry.isFfoDatacenterDeployment = new bool?(DatacenterRegistry.CheckBooleanValue("FfoDeploymentMode"));
			}
			return DatacenterRegistry.isFfoDatacenterDeployment.Value;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00020D40 File Offset: 0x0001EF40
		internal static bool IsGallatinDatacenter()
		{
			if (DatacenterRegistry.isGallatinDatacenter == null)
			{
				object obj = DatacenterRegistry.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeLabs", "ServiceName");
				string a = (obj != null) ? obj.ToString() : string.Empty;
				DatacenterRegistry.isGallatinDatacenter = new bool?(string.Equals(a, "GALLATIN", StringComparison.InvariantCultureIgnoreCase));
			}
			return DatacenterRegistry.isGallatinDatacenter.Value;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00020D9C File Offset: 0x0001EF9C
		internal static bool IsFFOGallatinDatacenter()
		{
			if (DatacenterRegistry.isFFOGallatinDatacenter == null)
			{
				object obj = DatacenterRegistry.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeLabs", "ServiceName");
				string a = (obj != null) ? obj.ToString() : string.Empty;
				DatacenterRegistry.isFFOGallatinDatacenter = new bool?(string.Equals(a, "FopePRODcn", StringComparison.InvariantCultureIgnoreCase));
			}
			return DatacenterRegistry.isFFOGallatinDatacenter.Value;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00020DF8 File Offset: 0x0001EFF8
		internal static string GetForefrontRegion()
		{
			if (DatacenterRegistry.ffoRegionValue == null)
			{
				object obj = DatacenterRegistry.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeLabs", "Region");
				DatacenterRegistry.ffoRegionValue = ((obj != null) ? obj.ToString() : string.Empty);
			}
			return DatacenterRegistry.ffoRegionValue;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00020E38 File Offset: 0x0001F038
		internal static string GetForefrontRegionServiceInstance()
		{
			if (DatacenterRegistry.ffoRegionServiceInstanceValue == null)
			{
				object obj = DatacenterRegistry.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeLabs", "RegionServiceInstance");
				DatacenterRegistry.ffoRegionServiceInstanceValue = ((obj != null) ? obj.ToString() : string.Empty);
			}
			return DatacenterRegistry.ffoRegionServiceInstanceValue;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00020E78 File Offset: 0x0001F078
		internal static string GetForefrontRegionTag()
		{
			if (DatacenterRegistry.ffoRegionTagValue == null)
			{
				object obj = DatacenterRegistry.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeLabs", "RegionTag");
				DatacenterRegistry.ffoRegionTagValue = ((obj != null) ? obj.ToString() : string.Empty);
			}
			return DatacenterRegistry.ffoRegionTagValue;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00020EB8 File Offset: 0x0001F0B8
		internal static string GetForefrontServiceTag()
		{
			if (DatacenterRegistry.ffoServiceTagValue == null)
			{
				object obj = DatacenterRegistry.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeLabs", "ServiceTag");
				DatacenterRegistry.ffoServiceTagValue = ((obj != null) ? obj.ToString() : string.Empty);
			}
			return DatacenterRegistry.ffoServiceTagValue;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00020EF8 File Offset: 0x0001F0F8
		internal static string GetForefrontDatacenter()
		{
			if (DatacenterRegistry.ffoDatacenterValue == null)
			{
				object obj = DatacenterRegistry.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeLabs", "Datacenter");
				if (obj != null)
				{
					DatacenterRegistry.ffoDatacenterValue = obj.ToString();
				}
				else
				{
					DatacenterRegistry.ffoDatacenterValue = string.Empty;
				}
			}
			return DatacenterRegistry.ffoDatacenterValue;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00020F3C File Offset: 0x0001F13C
		internal static string GetForefrontDomainDBSite()
		{
			if (DatacenterRegistry.ffoDomainDBSiteValue == null)
			{
				object obj = DatacenterRegistry.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeLabs", "DomainDBSite");
				if (obj != null)
				{
					DatacenterRegistry.ffoDomainDBSiteValue = obj.ToString();
				}
				else
				{
					DatacenterRegistry.ffoDomainDBSiteValue = string.Empty;
				}
			}
			return DatacenterRegistry.ffoDomainDBSiteValue;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00020F7F File Offset: 0x0001F17F
		internal static bool IsDatacenterDedicated()
		{
			if (DatacenterRegistry.isDatacenterDedicated == null)
			{
				DatacenterRegistry.isDatacenterDedicated = new bool?(DatacenterRegistry.CheckBooleanValue("DatacenterDedicated"));
			}
			return DatacenterRegistry.isDatacenterDedicated.Value;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00020FAC File Offset: 0x0001F1AC
		internal static string GetForefrontFopeGlobalSite()
		{
			if (DatacenterRegistry.ffoFopeGlobalSiteValue == null)
			{
				object obj = DatacenterRegistry.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeLabs", "FopeGlobalSite");
				if (obj != null)
				{
					DatacenterRegistry.ffoFopeGlobalSiteValue = obj.ToString();
				}
				else
				{
					DatacenterRegistry.ffoFopeGlobalSiteValue = string.Empty;
				}
			}
			return DatacenterRegistry.ffoFopeGlobalSiteValue;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00020FF0 File Offset: 0x0001F1F0
		internal static string GetForefrontAlertEmail()
		{
			if (DatacenterRegistry.ffoAlertEmail == null)
			{
				object obj = DatacenterRegistry.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeLabs", "AlertEmail");
				if (obj != null)
				{
					DatacenterRegistry.ffoAlertEmail = obj.ToString();
				}
				else
				{
					DatacenterRegistry.ffoAlertEmail = string.Empty;
				}
			}
			return DatacenterRegistry.ffoAlertEmail;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00021034 File Offset: 0x0001F234
		internal static string GetForefrontArbitrationServiceUrl()
		{
			if (DatacenterRegistry.ffoArbitrationServiceUrlValue == null)
			{
				object obj = DatacenterRegistry.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeLabs", "ArbitrationServiceUrl");
				if (obj != null)
				{
					DatacenterRegistry.ffoArbitrationServiceUrlValue = obj.ToString();
				}
				else
				{
					DatacenterRegistry.ffoArbitrationServiceUrlValue = string.Empty;
				}
			}
			return DatacenterRegistry.ffoArbitrationServiceUrlValue;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00021078 File Offset: 0x0001F278
		internal static bool IsPartnerHostedOnly()
		{
			if (DatacenterRegistry.isPartnerHostedOnly == null)
			{
				object obj = DatacenterRegistry.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15", "PartnerHostedMode");
				DatacenterRegistry.isPartnerHostedOnly = new bool?(obj is int && (int)obj == 1);
			}
			return DatacenterRegistry.isPartnerHostedOnly.Value;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x000210CC File Offset: 0x0001F2CC
		internal static void CreatePartnerHostedRegistryKey()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15"))
			{
				registryKey.SetValue("PartnerHostedMode", 1, RegistryValueKind.DWord);
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00021118 File Offset: 0x0001F318
		internal static void RemovePartnerHostedRegistryKey()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15", true))
			{
				registryKey.DeleteValue("PartnerHostedMode", false);
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00021160 File Offset: 0x0001F360
		internal static bool IsDualWriteAllowed()
		{
			if (DatacenterRegistry.isFfoDualWriteAllowed == null)
			{
				if (ExEnvironment.IsTest)
				{
					DatacenterRegistry.isFfoDualWriteAllowed = new bool?(DatacenterRegistry.CheckBooleanValue("FfoDualWriteAllowed"));
				}
				else
				{
					DatacenterRegistry.isFfoDualWriteAllowed = new bool?(true);
				}
			}
			return DatacenterRegistry.isFfoDualWriteAllowed.Value;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000211A0 File Offset: 0x0001F3A0
		private static bool CheckBooleanValue(string valueName)
		{
			object obj = DatacenterRegistry.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeLabs", valueName);
			if (obj == null)
			{
				return false;
			}
			if (obj is int)
			{
				bool result;
				switch ((int)obj)
				{
				case 0:
					result = false;
					break;
				case 1:
					result = true;
					break;
				default:
					throw new DatacenterInvalidRegistryException();
				}
				return result;
			}
			throw new DatacenterInvalidRegistryException();
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000211F4 File Offset: 0x0001F3F4
		private static object ReadRegistryKey(string keyPath, string valueName)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(keyPath))
			{
				if (registryKey != null)
				{
					return registryKey.GetValue(valueName, null);
				}
			}
			return null;
		}

		// Token: 0x04000580 RID: 1408
		private const string PartnerHostedKeyPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x04000581 RID: 1409
		private const string PartnerHostedValueName = "PartnerHostedMode";

		// Token: 0x04000582 RID: 1410
		private const string MicrosoftHostedKeyPath = "SOFTWARE\\Microsoft\\ExchangeLabs";

		// Token: 0x04000583 RID: 1411
		private const string MicrosoftHostedValueName = "DatacenterMode";

		// Token: 0x04000584 RID: 1412
		private const string MicrosoftHostedServiceName = "ServiceName";

		// Token: 0x04000585 RID: 1413
		private const string MicrosoftDatacenterDedicatedValueName = "DatacenterDedicated";

		// Token: 0x04000586 RID: 1414
		private const string TreatPreReqErrorsAsWarningsKey = "TreatPreReqErrorsAsWarnings";

		// Token: 0x04000587 RID: 1415
		private const string FfoDualWriteAllowedValueName = "FfoDualWriteAllowed";

		// Token: 0x04000588 RID: 1416
		private const string FfoValueName = "ForefrontForOfficeMode";

		// Token: 0x04000589 RID: 1417
		private const string FfoDeploymentValueName = "FfoDeploymentMode";

		// Token: 0x0400058A RID: 1418
		private const string FfoRegion = "Region";

		// Token: 0x0400058B RID: 1419
		private const string FfoRegionServiceInstance = "RegionServiceInstance";

		// Token: 0x0400058C RID: 1420
		private const string FfoRegionTag = "RegionTag";

		// Token: 0x0400058D RID: 1421
		private const string FfoServiceTag = "ServiceTag";

		// Token: 0x0400058E RID: 1422
		private const string FfoDatacenter = "Datacenter";

		// Token: 0x0400058F RID: 1423
		private const string FfoDomainDBSite = "DomainDBSite";

		// Token: 0x04000590 RID: 1424
		private const string FfoFopeGlobalSite = "FopeGlobalSite";

		// Token: 0x04000591 RID: 1425
		private const string FfoAlertEmail = "AlertEmail";

		// Token: 0x04000592 RID: 1426
		private const string FfoArbitrationServiceUrl = "ArbitrationServiceUrl";

		// Token: 0x04000593 RID: 1427
		private static bool? isFfoDualWriteAllowed = null;

		// Token: 0x04000594 RID: 1428
		private static bool? isFfoDatacenter = null;

		// Token: 0x04000595 RID: 1429
		private static bool? isFfoDatacenterDeployment = null;

		// Token: 0x04000596 RID: 1430
		private static bool? isGallatinDatacenter = null;

		// Token: 0x04000597 RID: 1431
		private static bool? isFFOGallatinDatacenter = null;

		// Token: 0x04000598 RID: 1432
		private static string ffoRegionValue = null;

		// Token: 0x04000599 RID: 1433
		private static string ffoRegionServiceInstanceValue = null;

		// Token: 0x0400059A RID: 1434
		private static string ffoRegionTagValue = null;

		// Token: 0x0400059B RID: 1435
		private static string ffoServiceTagValue = null;

		// Token: 0x0400059C RID: 1436
		private static string ffoDatacenterValue = null;

		// Token: 0x0400059D RID: 1437
		private static string ffoDomainDBSiteValue = null;

		// Token: 0x0400059E RID: 1438
		private static string ffoFopeGlobalSiteValue = null;

		// Token: 0x0400059F RID: 1439
		private static string ffoAlertEmail = null;

		// Token: 0x040005A0 RID: 1440
		private static string ffoArbitrationServiceUrlValue = null;

		// Token: 0x040005A1 RID: 1441
		private static bool? isMicrosoftHostedOnly;

		// Token: 0x040005A2 RID: 1442
		private static bool? treatPreReqErrorsAsWarningsKey;

		// Token: 0x040005A3 RID: 1443
		private static bool? isDatacenterDedicated;

		// Token: 0x040005A4 RID: 1444
		private static bool? isPartnerHostedOnly;
	}
}
