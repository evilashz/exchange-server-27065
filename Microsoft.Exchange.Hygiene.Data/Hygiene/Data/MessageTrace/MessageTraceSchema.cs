using System;
using System.Data.SqlTypes;
using System.Net;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200017F RID: 383
	internal class MessageTraceSchema
	{
		// Token: 0x04000716 RID: 1814
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = CommonMessageTraceSchema.OrganizationalUnitRootProperty;

		// Token: 0x04000717 RID: 1815
		internal static readonly HygienePropertyDefinition ClientMessageIdProperty = new HygienePropertyDefinition("ClientMessageId", typeof(string));

		// Token: 0x04000718 RID: 1816
		internal static readonly HygienePropertyDefinition ExMessageIdProperty = CommonMessageTraceSchema.ExMessageIdProperty;

		// Token: 0x04000719 RID: 1817
		internal static readonly HygienePropertyDefinition DirectionProperty = new HygienePropertyDefinition("Direction", typeof(MailDirection));

		// Token: 0x0400071A RID: 1818
		internal static readonly HygienePropertyDefinition FromEmailPrefixProperty = new HygienePropertyDefinition("FromEmailPrefix", typeof(string));

		// Token: 0x0400071B RID: 1819
		internal static readonly HygienePropertyDefinition FromEmailDomainProperty = new HygienePropertyDefinition("FromEmailDomain", typeof(string));

		// Token: 0x0400071C RID: 1820
		internal static readonly HygienePropertyDefinition IPAddressProperty = new HygienePropertyDefinition("IPAddress", typeof(IPAddress));

		// Token: 0x0400071D RID: 1821
		internal static readonly HygienePropertyDefinition IP1Property = new HygienePropertyDefinition("IP1", typeof(byte?));

		// Token: 0x0400071E RID: 1822
		internal static readonly HygienePropertyDefinition IP2Property = new HygienePropertyDefinition("IP2", typeof(byte?));

		// Token: 0x0400071F RID: 1823
		internal static readonly HygienePropertyDefinition IP3Property = new HygienePropertyDefinition("IP3", typeof(byte?));

		// Token: 0x04000720 RID: 1824
		internal static readonly HygienePropertyDefinition IP4Property = new HygienePropertyDefinition("IP4", typeof(byte?));

		// Token: 0x04000721 RID: 1825
		internal static readonly HygienePropertyDefinition IP5Property = new HygienePropertyDefinition("IP5", typeof(byte?));

		// Token: 0x04000722 RID: 1826
		internal static readonly HygienePropertyDefinition IP6Property = new HygienePropertyDefinition("IP6", typeof(byte?));

		// Token: 0x04000723 RID: 1827
		internal static readonly HygienePropertyDefinition IP7Property = new HygienePropertyDefinition("IP7", typeof(byte?));

		// Token: 0x04000724 RID: 1828
		internal static readonly HygienePropertyDefinition IP8Property = new HygienePropertyDefinition("IP8", typeof(byte?));

		// Token: 0x04000725 RID: 1829
		internal static readonly HygienePropertyDefinition IP9Property = new HygienePropertyDefinition("IP9", typeof(byte?));

		// Token: 0x04000726 RID: 1830
		internal static readonly HygienePropertyDefinition IP10Property = new HygienePropertyDefinition("IP10", typeof(byte?));

		// Token: 0x04000727 RID: 1831
		internal static readonly HygienePropertyDefinition IP11Property = new HygienePropertyDefinition("IP11", typeof(byte?));

		// Token: 0x04000728 RID: 1832
		internal static readonly HygienePropertyDefinition IP12Property = new HygienePropertyDefinition("IP12", typeof(byte?));

		// Token: 0x04000729 RID: 1833
		internal static readonly HygienePropertyDefinition IP13Property = new HygienePropertyDefinition("IP13", typeof(byte?));

		// Token: 0x0400072A RID: 1834
		internal static readonly HygienePropertyDefinition IP14Property = new HygienePropertyDefinition("IP14", typeof(byte?));

		// Token: 0x0400072B RID: 1835
		internal static readonly HygienePropertyDefinition IP15Property = new HygienePropertyDefinition("IP15", typeof(byte?));

		// Token: 0x0400072C RID: 1836
		internal static readonly HygienePropertyDefinition IP16Property = new HygienePropertyDefinition("IP16", typeof(byte?));

		// Token: 0x0400072D RID: 1837
		internal static readonly HygienePropertyDefinition ReceivedTimeProperty = new HygienePropertyDefinition("ReceivedTime", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400072E RID: 1838
		internal static readonly HygienePropertyDefinition StartTimeQueryProperty = new HygienePropertyDefinition("startTime", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400072F RID: 1839
		internal static readonly HygienePropertyDefinition EndTimeQueryProperty = new HygienePropertyDefinition("endTime", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
