using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000CB RID: 203
	public enum AdmissionType
	{
		// Token: 0x04000331 RID: 817
		[EnumMember]
		[XmlEnum]
		ucLocked,
		// Token: 0x04000332 RID: 818
		[XmlEnum]
		[EnumMember]
		ucClosedAuthenticated,
		// Token: 0x04000333 RID: 819
		[XmlEnum]
		[EnumMember]
		ucOpenAuthenticated,
		// Token: 0x04000334 RID: 820
		[XmlEnum]
		[EnumMember]
		ucAnonymous
	}
}
