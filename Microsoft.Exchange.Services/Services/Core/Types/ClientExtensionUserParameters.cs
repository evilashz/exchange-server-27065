using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000722 RID: 1826
	[XmlType(TypeName = "ClientExtensionUserParametersType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ClientExtensionUserParameters
	{
		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x0600376D RID: 14189 RVA: 0x000C57B3 File Offset: 0x000C39B3
		// (set) Token: 0x0600376E RID: 14190 RVA: 0x000C57BB File Offset: 0x000C39BB
		[XmlAttribute]
		public string UserId { get; set; }

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x0600376F RID: 14191 RVA: 0x000C57C4 File Offset: 0x000C39C4
		// (set) Token: 0x06003770 RID: 14192 RVA: 0x000C57CC File Offset: 0x000C39CC
		[XmlAttribute]
		public bool EnabledOnly { get; set; }

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06003771 RID: 14193 RVA: 0x000C57D5 File Offset: 0x000C39D5
		// (set) Token: 0x06003772 RID: 14194 RVA: 0x000C57DD File Offset: 0x000C39DD
		[XmlArrayItem("String", IsNullable = false)]
		public string[] UserEnabledExtensions { get; set; }

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x000C57E6 File Offset: 0x000C39E6
		// (set) Token: 0x06003774 RID: 14196 RVA: 0x000C57EE File Offset: 0x000C39EE
		[XmlArrayItem("String", IsNullable = false)]
		public string[] UserDisabledExtensions { get; set; }

		// Token: 0x06003775 RID: 14197 RVA: 0x000C57F8 File Offset: 0x000C39F8
		internal bool IsEnabledByUser(string extensionId)
		{
			if (this.UserEnabledExtensions == null || this.UserEnabledExtensions.Length == 0)
			{
				return false;
			}
			if (this.userEnabledExtensionsHashSet == null)
			{
				this.userEnabledExtensionsHashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				foreach (string extensionId2 in this.UserEnabledExtensions)
				{
					this.userEnabledExtensionsHashSet.Add(ExtensionDataHelper.FormatExtensionId(extensionId2));
				}
			}
			return this.userEnabledExtensionsHashSet.Contains(ExtensionDataHelper.FormatExtensionId(extensionId));
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000C5870 File Offset: 0x000C3A70
		internal bool IsDisabledByUser(string extensionId)
		{
			if (this.UserDisabledExtensions == null || this.UserDisabledExtensions.Length == 0)
			{
				return false;
			}
			if (this.userDisabledExtensionsHashSet == null)
			{
				this.userDisabledExtensionsHashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				foreach (string extensionId2 in this.UserDisabledExtensions)
				{
					this.userDisabledExtensionsHashSet.Add(ExtensionDataHelper.FormatExtensionId(extensionId2));
				}
			}
			return this.userDisabledExtensionsHashSet.Contains(ExtensionDataHelper.FormatExtensionId(extensionId));
		}

		// Token: 0x04001EC7 RID: 7879
		private HashSet<string> userEnabledExtensionsHashSet;

		// Token: 0x04001EC8 RID: 7880
		private HashSet<string> userDisabledExtensionsHashSet;
	}
}
