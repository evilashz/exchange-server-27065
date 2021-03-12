using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000C6 RID: 198
	[DataContract]
	public abstract class SetVoiceMailBase : SetObjectProperties
	{
		// Token: 0x17001937 RID: 6455
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x0005984D File Offset: 0x00057A4D
		// (set) Token: 0x06001D16 RID: 7446 RVA: 0x00059869 File Offset: 0x00057A69
		[DataMember]
		public bool PinlessAccessToVoiceMailEnabled
		{
			get
			{
				return (bool)(base[UMMailboxSchema.PinlessAccessToVoiceMailEnabled] ?? false);
			}
			set
			{
				base[UMMailboxSchema.PinlessAccessToVoiceMailEnabled] = value;
			}
		}

		// Token: 0x17001938 RID: 6456
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x0005987C File Offset: 0x00057A7C
		// (set) Token: 0x06001D18 RID: 7448 RVA: 0x000598A2 File Offset: 0x00057AA2
		[DataMember]
		public string SMSNotificationOption
		{
			get
			{
				return ((UMSMSNotificationOptions)(base[UMMailboxSchema.UMSMSNotificationOption] ?? UMSMSNotificationOptions.None)).ToString();
			}
			set
			{
				base[UMMailboxSchema.UMSMSNotificationOption] = value;
			}
		}
	}
}
