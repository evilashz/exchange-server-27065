using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel.DataContracts
{
	// Token: 0x020000F9 RID: 249
	[DataContract]
	public class CallerIdItem
	{
		// Token: 0x06001ED0 RID: 7888 RVA: 0x0005C72C File Offset: 0x0005A92C
		public CallerIdItem(CallerIdItem taskCallerIdItem)
		{
			this.CallerIdType = (int)taskCallerIdItem.CallerIdType;
			this.GALContactLegacyDN = taskCallerIdItem.GALContactLegacyDN;
			this.PersonalContactStoreId = taskCallerIdItem.PersonalContactStoreId;
			this.PhoneNumber = taskCallerIdItem.PhoneNumber;
			this.DisplayName = taskCallerIdItem.DisplayName;
			this.PersonaEmailAddress = taskCallerIdItem.PersonaEmailAddress;
		}

		// Token: 0x170019E5 RID: 6629
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x0005C787 File Offset: 0x0005A987
		// (set) Token: 0x06001ED2 RID: 7890 RVA: 0x0005C78F File Offset: 0x0005A98F
		[DataMember]
		public int CallerIdType { get; set; }

		// Token: 0x170019E6 RID: 6630
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x0005C798 File Offset: 0x0005A998
		// (set) Token: 0x06001ED4 RID: 7892 RVA: 0x0005C7A0 File Offset: 0x0005A9A0
		[DataMember]
		public string GALContactLegacyDN { get; set; }

		// Token: 0x170019E7 RID: 6631
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x0005C7A9 File Offset: 0x0005A9A9
		// (set) Token: 0x06001ED6 RID: 7894 RVA: 0x0005C7B1 File Offset: 0x0005A9B1
		[DataMember]
		public string PersonalContactStoreId { get; set; }

		// Token: 0x170019E8 RID: 6632
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x0005C7BA File Offset: 0x0005A9BA
		// (set) Token: 0x06001ED8 RID: 7896 RVA: 0x0005C7C2 File Offset: 0x0005A9C2
		[DataMember]
		public string PhoneNumber { get; set; }

		// Token: 0x170019E9 RID: 6633
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x0005C7CB File Offset: 0x0005A9CB
		// (set) Token: 0x06001EDA RID: 7898 RVA: 0x0005C7D3 File Offset: 0x0005A9D3
		[DataMember]
		public string PersonaEmailAddress { get; set; }

		// Token: 0x170019EA RID: 6634
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x0005C7DC File Offset: 0x0005A9DC
		// (set) Token: 0x06001EDC RID: 7900 RVA: 0x0005C7E4 File Offset: 0x0005A9E4
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x06001EDD RID: 7901 RVA: 0x0005C7F0 File Offset: 0x0005A9F0
		public CallerIdItem ToTaskObject()
		{
			return new CallerIdItem((CallerIdItemType)this.CallerIdType, this.PhoneNumber, this.GALContactLegacyDN, this.PersonalContactStoreId, this.PersonaEmailAddress, this.DisplayName);
		}
	}
}
