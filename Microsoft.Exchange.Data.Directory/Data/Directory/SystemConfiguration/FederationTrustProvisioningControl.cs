using System;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200047F RID: 1151
	internal class FederationTrustProvisioningControl
	{
		// Token: 0x06003393 RID: 13203 RVA: 0x000CFB8C File Offset: 0x000CDD8C
		private static string GetKeyValue(string key, string federationControls)
		{
			if (string.IsNullOrEmpty(federationControls))
			{
				return string.Empty;
			}
			int num = federationControls.IndexOf(key + FederationTrustProvisioningControl.valueSeparator);
			if (0 > num)
			{
				return string.Empty;
			}
			int num2 = num + key.Length + 1;
			if (federationControls.Length <= num2)
			{
				return string.Empty;
			}
			int num3 = federationControls.IndexOf(FederationTrustProvisioningControl.controlSeparator, num2);
			int num4 = num3 - num2;
			if (0 <= num3 && 0 < num4)
			{
				return federationControls.Substring(num2, num4);
			}
			return federationControls.Substring(num2);
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x000CFC08 File Offset: 0x000CDE08
		private static string PutKeyValue(string key, string value, string federationControls)
		{
			if (string.IsNullOrEmpty(federationControls))
			{
				if (!string.IsNullOrEmpty(value))
				{
					return key + FederationTrustProvisioningControl.valueSeparator + value + FederationTrustProvisioningControl.controlSeparator;
				}
				return string.Empty;
			}
			else
			{
				string[] array = federationControls.ToUpper().Split(FederationTrustProvisioningControl.controlSeparatorList, StringSplitOptions.RemoveEmptyEntries);
				if (array != null && array.Length != 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					bool flag = false;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].StartsWith(key))
						{
							if (!flag)
							{
								flag = true;
								if (!string.IsNullOrEmpty(value))
								{
									stringBuilder.Append(key + FederationTrustProvisioningControl.valueSeparator + value + FederationTrustProvisioningControl.controlSeparator);
								}
							}
						}
						else
						{
							stringBuilder.Append(array[i] + FederationTrustProvisioningControl.controlSeparator);
						}
					}
					if (!flag && !string.IsNullOrEmpty(value))
					{
						stringBuilder.Append(key + FederationTrustProvisioningControl.valueSeparator + value + FederationTrustProvisioningControl.controlSeparator);
					}
					return stringBuilder.ToString();
				}
				if (!string.IsNullOrEmpty(value))
				{
					return key + FederationTrustProvisioningControl.valueSeparator + value + FederationTrustProvisioningControl.controlSeparator;
				}
				return string.Empty;
			}
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x000CFCFC File Offset: 0x000CDEFC
		public static string GetNamespaceProvisioner(string federationControls)
		{
			string keyValue = FederationTrustProvisioningControl.GetKeyValue("NAMESPACEPROVISIONER", federationControls);
			if (!string.IsNullOrEmpty(keyValue))
			{
				return keyValue;
			}
			return "EXTERNALPROCESS";
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x000CFD24 File Offset: 0x000CDF24
		public static string PutNamespaceProvisioner(string namespaceProvisioner, string federationControls)
		{
			return FederationTrustProvisioningControl.PutKeyValue("NAMESPACEPROVISIONER", namespaceProvisioner, federationControls);
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000CFD32 File Offset: 0x000CDF32
		public static string GetAdministratorProvisioningId(string federationControls)
		{
			return FederationTrustProvisioningControl.GetKeyValue("ADMINKEY", federationControls);
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x000CFD3F File Offset: 0x000CDF3F
		public static string PutAdministratorProvisioningId(string administrationProvisioiningKey, string federationControls)
		{
			return FederationTrustProvisioningControl.PutKeyValue("ADMINKEY", administrationProvisioiningKey, federationControls);
		}

		// Token: 0x040023BB RID: 9147
		internal const string WindowsLiveDomainServices = "WINDOWSLIVEDOMAINSERVICES";

		// Token: 0x040023BC RID: 9148
		internal const string WindowsLiveDomainServices2 = "WINDOWSLIVEDOMAINSERVICES2";

		// Token: 0x040023BD RID: 9149
		internal const string ExternalProcess = "EXTERNALPROCESS";

		// Token: 0x040023BE RID: 9150
		private const string AdminKeyKeyword = "ADMINKEY";

		// Token: 0x040023BF RID: 9151
		private const string NamespaceProvisionerKeyword = "NAMESPACEPROVISIONER";

		// Token: 0x040023C0 RID: 9152
		private static readonly char[] controlSeparatorList = new char[]
		{
			';'
		};

		// Token: 0x040023C1 RID: 9153
		private static readonly string controlSeparator = ";";

		// Token: 0x040023C2 RID: 9154
		private static readonly string valueSeparator = "=";
	}
}
