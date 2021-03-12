using System;
using System.Xml;
using Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x020001EE RID: 494
	internal class OrganizationInitializationDefinition
	{
		// Token: 0x06000EA2 RID: 3746 RVA: 0x000239C8 File Offset: 0x00021BC8
		public OrganizationInitializationDefinition(string extensionXml)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(extensionXml);
			this.ExtensionNode = xmlDocument.DocumentElement;
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x000239F4 File Offset: 0x00021BF4
		public string DomainPrefix
		{
			get
			{
				return this.GetOrganizationAttribute("DomainPrefix", true) ?? Guid.NewGuid().ToString().Replace("-", string.Empty);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00023A34 File Offset: 0x00021C34
		public int TimeoutInMinutes
		{
			get
			{
				int result;
				int.TryParse(this.GetOrganizationAttribute("TimeoutWaitInMinutes", true) ?? "7", out result);
				return result;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00023A60 File Offset: 0x00021C60
		public bool WaitForEXOProperties
		{
			get
			{
				bool result;
				bool.TryParse(this.GetOrganizationAttribute("WaitForEXOProperties", true) ?? "true", out result);
				return result;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00023A8C File Offset: 0x00021C8C
		public bool WaitForDeprovisioning
		{
			get
			{
				bool result;
				bool.TryParse(this.GetOrganizationAttribute("WaitForDeprovisioning", true) ?? "true", out result);
				return result;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x00023AB7 File Offset: 0x00021CB7
		public string CustomerType
		{
			get
			{
				return this.GetOrganizationAttribute("CustomerType", true) ?? "FilteringOnly";
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00023ACE File Offset: 0x00021CCE
		public string FeatureTag
		{
			get
			{
				return this.GetOrganizationAttribute("FeatureTag", false);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x00023ADC File Offset: 0x00021CDC
		public string Version
		{
			get
			{
				return this.GetOrganizationAttribute("Version", true);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00023AEC File Offset: 0x00021CEC
		public Guid TenantId
		{
			get
			{
				string input = this.GetOrganizationAttribute("TenantId", true) ?? Guid.Empty.ToString();
				Guid result;
				Guid.TryParse(input, out result);
				return result;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x00023B28 File Offset: 0x00021D28
		public CompanyManagerProvider CompanyManager
		{
			get
			{
				if (this.companyManager == null)
				{
					this.companyManager = new CompanyManagerProvider
					{
						Proxy = null,
						Timeout = (int)TimeSpan.FromMinutes(3.0).TotalMilliseconds
					};
				}
				return this.companyManager;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x00023B74 File Offset: 0x00021D74
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x00023B7C File Offset: 0x00021D7C
		private XmlNode ExtensionNode { get; set; }

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00023B85 File Offset: 0x00021D85
		private XmlNode OrganizationNode
		{
			get
			{
				return this.ExtensionNode.SelectSingleNode("WorkContext/Organization");
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00023B98 File Offset: 0x00021D98
		private string GetOrganizationAttribute(string attributeName, bool optional = false)
		{
			XmlAttribute xmlAttribute = this.OrganizationNode.Attributes[attributeName];
			if (!optional && xmlAttribute == null)
			{
				throw new ArgumentException(string.Format("Attribute cannot be found for name {0}.", attributeName));
			}
			if (xmlAttribute != null)
			{
				return xmlAttribute.Value;
			}
			return null;
		}

		// Token: 0x040006F1 RID: 1777
		private const string FeatureTagAttribute = "FeatureTag";

		// Token: 0x040006F2 RID: 1778
		private const string DomainPrefixAttribute = "DomainPrefix";

		// Token: 0x040006F3 RID: 1779
		private const string CustomerTypeAttribute = "CustomerType";

		// Token: 0x040006F4 RID: 1780
		private const string VersionAttribute = "Version";

		// Token: 0x040006F5 RID: 1781
		private const string TimeoutWaitInMinutesAttribute = "TimeoutWaitInMinutes";

		// Token: 0x040006F6 RID: 1782
		private const string WaitForEXOPropertiesAttribute = "WaitForEXOProperties";

		// Token: 0x040006F7 RID: 1783
		private const string WaitForDeprovisioningAttribute = "WaitForDeprovisioning";

		// Token: 0x040006F8 RID: 1784
		private const string TenantIdAttribute = "TenantId";

		// Token: 0x040006F9 RID: 1785
		private CompanyManagerProvider companyManager;
	}
}
