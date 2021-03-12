using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Hygiene.Data.Directory;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x020001F2 RID: 498
	internal class RecipientProvisioningDefinition
	{
		// Token: 0x06000EC0 RID: 3776 RVA: 0x000248CC File Offset: 0x00022ACC
		public RecipientProvisioningDefinition(string extensionXml)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(extensionXml);
			this.ExtensionNode = xmlDocument.DocumentElement;
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x000248F8 File Offset: 0x00022AF8
		public TimeSpan AllowableLatency
		{
			get
			{
				return TimeSpan.Parse(this.GetRecipientAttribute("allowableLatency", false));
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x0002490B File Offset: 0x00022B0B
		public string RecipientType
		{
			get
			{
				return this.GetRecipientAttribute("recipientType", false).ToLower();
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0002491E File Offset: 0x00022B1E
		public string NamePrefix
		{
			get
			{
				return this.GetRecipientAttribute("namePrefix", false);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x0002492C File Offset: 0x00022B2C
		public bool GenerateUniqueUser
		{
			get
			{
				return this.GetRecipientAttribute("generateUniqueUser", true).ToLower() != "false";
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00024961 File Offset: 0x00022B61
		public string RunAsUser
		{
			get
			{
				return (string)this.Users.First((ADUser user) => Regex.IsMatch((string)user.WindowsLiveID, "admin", RegexOptions.IgnoreCase)).WindowsLiveID;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00024998 File Offset: 0x00022B98
		public string RunAsUserPassword
		{
			get
			{
				SecureString loginPassword = this.ProbeOrganization.LoginPassword;
				if (loginPassword == null)
				{
					throw new ArgumentException("The probe organization does not have a password specified.");
				}
				IntPtr ptr = Marshal.SecureStringToBSTR(loginPassword);
				string result;
				try
				{
					result = Marshal.PtrToStringBSTR(ptr);
				}
				finally
				{
					Marshal.FreeBSTR(ptr);
				}
				return result;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x000249E8 File Offset: 0x00022BE8
		public bool CleanupRecipient
		{
			get
			{
				return this.GetRecipientAttribute("cleanupRecipient", true).ToLower() != "false";
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00024A08 File Offset: 0x00022C08
		public bool AddLicense
		{
			get
			{
				bool result;
				bool.TryParse(this.GetRecipientAttribute("addLicense", true) ?? "false", out result);
				return result;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00024A33 File Offset: 0x00022C33
		public ITenantRecipientSession ProbeSession
		{
			get
			{
				if (this.probeSession == null)
				{
					if (this.ProbeOrganization == null)
					{
						throw new InvalidOperationException("The probe organization should always have a value.");
					}
					this.probeSession = new FfoTenantRecipientSession(this.ProbeOrganization.ProbeOrganizationId);
				}
				return this.probeSession;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00024A6C File Offset: 0x00022C6C
		public string Endpoint
		{
			get
			{
				switch (ProbeOrganizationInfo.FindEnvironment(this.RunAsUser))
				{
				case ProbeEnvironment.Test:
					return "https://provisioningapi.msol-test.com/provisioningwebservice.svc";
				case ProbeEnvironment.Dogfood:
					return "https://provisioningapi.ccsctp.com/provisioningwebservice.svc";
				case ProbeEnvironment.Production:
					return "https://provisioningapi.microsoftonline.com/provisioningwebservice.svc";
				default:
					throw new ArgumentException("The windows live Id for the administrator does not have an expected suffix (e.g. @msol-test.com).");
				}
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00024AB8 File Offset: 0x00022CB8
		public ProbeOrganizationInfo ProbeOrganization
		{
			get
			{
				if (this.probeOrganization == null)
				{
					string recipientAttribute = this.GetRecipientAttribute("featureTag", false);
					GlobalConfigSession globalConfigSession = new GlobalConfigSession();
					IEnumerable<ProbeOrganizationInfo> probeOrganizations = globalConfigSession.GetProbeOrganizations(recipientAttribute);
					if (probeOrganizations == null || !probeOrganizations.Any<ProbeOrganizationInfo>())
					{
						throw new ArgumentException("Cannot find any test tenant with feature tag = " + recipientAttribute);
					}
					this.probeOrganization = probeOrganizations.First<ProbeOrganizationInfo>();
				}
				return this.probeOrganization;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00024B16 File Offset: 0x00022D16
		// (set) Token: 0x06000ECD RID: 3789 RVA: 0x00024B1E File Offset: 0x00022D1E
		private XmlNode ExtensionNode { get; set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00024B27 File Offset: 0x00022D27
		private IEnumerable<ADUser> Users
		{
			get
			{
				if (this.users == null)
				{
					this.users = this.ProbeSession.Find<ADUser>(null, QueryScope.SubTree, null, null, int.MaxValue);
				}
				return this.users;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00024B51 File Offset: 0x00022D51
		private XmlNode RecipientNode
		{
			get
			{
				if (this.recipientNode == null)
				{
					this.recipientNode = this.ExtensionNode.SelectSingleNode("WorkContext/Recipient");
				}
				return this.recipientNode;
			}
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x00024B78 File Offset: 0x00022D78
		private string GetRecipientAttribute(string attributeName, bool optional = false)
		{
			XmlAttribute xmlAttribute = this.RecipientNode.Attributes[attributeName];
			if (!optional && xmlAttribute == null)
			{
				throw new ArgumentException(string.Format("Attribute cannot be found for name {0}.", attributeName));
			}
			if (xmlAttribute != null)
			{
				return xmlAttribute.Value;
			}
			return string.Empty;
		}

		// Token: 0x04000701 RID: 1793
		private const string AllowableLatencyAttribute = "allowableLatency";

		// Token: 0x04000702 RID: 1794
		private const string RecipientTypeAttribute = "recipientType";

		// Token: 0x04000703 RID: 1795
		private const string NamePrefixAttribute = "namePrefix";

		// Token: 0x04000704 RID: 1796
		private const string GenerateUniqueUserAttribute = "generateUniqueUser";

		// Token: 0x04000705 RID: 1797
		private const string CleanUpRecipientAttribute = "cleanupRecipient";

		// Token: 0x04000706 RID: 1798
		private const string AddLicenseAttribute = "addLicense";

		// Token: 0x04000707 RID: 1799
		private const string TestEnvironmentProvisioningEndopint = "https://provisioningapi.msol-test.com/provisioningwebservice.svc";

		// Token: 0x04000708 RID: 1800
		private const string DogfoodEnvironmentProvisioningEndpoint = "https://provisioningapi.ccsctp.com/provisioningwebservice.svc";

		// Token: 0x04000709 RID: 1801
		private const string ProductionEnvironmentProvisioningEndpoint = "https://provisioningapi.microsoftonline.com/provisioningwebservice.svc";

		// Token: 0x0400070A RID: 1802
		private XmlNode recipientNode;

		// Token: 0x0400070B RID: 1803
		private ProbeOrganizationInfo probeOrganization;

		// Token: 0x0400070C RID: 1804
		private ITenantRecipientSession probeSession;

		// Token: 0x0400070D RID: 1805
		private IEnumerable<ADUser> users;
	}
}
