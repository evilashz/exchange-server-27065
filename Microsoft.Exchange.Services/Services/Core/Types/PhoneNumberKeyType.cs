using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005C4 RID: 1476
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum PhoneNumberKeyType
	{
		// Token: 0x04001AB0 RID: 6832
		AssistantPhone,
		// Token: 0x04001AB1 RID: 6833
		BusinessFax,
		// Token: 0x04001AB2 RID: 6834
		BusinessPhone,
		// Token: 0x04001AB3 RID: 6835
		BusinessPhone2,
		// Token: 0x04001AB4 RID: 6836
		Callback,
		// Token: 0x04001AB5 RID: 6837
		CarPhone,
		// Token: 0x04001AB6 RID: 6838
		CompanyMainPhone,
		// Token: 0x04001AB7 RID: 6839
		HomeFax,
		// Token: 0x04001AB8 RID: 6840
		HomePhone,
		// Token: 0x04001AB9 RID: 6841
		HomePhone2,
		// Token: 0x04001ABA RID: 6842
		Isdn,
		// Token: 0x04001ABB RID: 6843
		MobilePhone,
		// Token: 0x04001ABC RID: 6844
		OtherFax,
		// Token: 0x04001ABD RID: 6845
		OtherTelephone,
		// Token: 0x04001ABE RID: 6846
		Pager,
		// Token: 0x04001ABF RID: 6847
		PrimaryPhone,
		// Token: 0x04001AC0 RID: 6848
		RadioPhone,
		// Token: 0x04001AC1 RID: 6849
		Telex,
		// Token: 0x04001AC2 RID: 6850
		TtyTddPhone
	}
}
