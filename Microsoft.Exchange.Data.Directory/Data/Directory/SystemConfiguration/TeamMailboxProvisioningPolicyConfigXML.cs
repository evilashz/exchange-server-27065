using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200042E RID: 1070
	[XmlType(TypeName = "TeamMailboxProvisioningPolicyConfig")]
	[Serializable]
	public sealed class TeamMailboxProvisioningPolicyConfigXML : XMLSerializableBase
	{
		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x0600302F RID: 12335 RVA: 0x000C2414 File Offset: 0x000C0614
		// (set) Token: 0x06003030 RID: 12336 RVA: 0x000C241C File Offset: 0x000C061C
		[XmlElement(ElementName = "AliasPrefix")]
		public string AliasPrefix { get; set; }

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x000C2425 File Offset: 0x000C0625
		// (set) Token: 0x06003032 RID: 12338 RVA: 0x000C242D File Offset: 0x000C062D
		[XmlElement(ElementName = "DefaultAliasPrefixEnabled")]
		public bool DefaultAliasPrefixEnabled { get; set; }
	}
}
