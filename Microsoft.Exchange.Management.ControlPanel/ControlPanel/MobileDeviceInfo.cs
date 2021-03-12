using System;
using System.Runtime.Serialization;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004F4 RID: 1268
	[DataContract]
	public class MobileDeviceInfo
	{
		// Token: 0x06003D5E RID: 15710 RVA: 0x000B8740 File Offset: 0x000B6940
		public MobileDeviceInfo(MobileDeviceConfiguration configuration)
		{
			this.Identity = ((ADObjectId)configuration.Identity).ToIdentity();
			this.DeviceType = configuration.DeviceType;
			this.DeviceModel = configuration.DeviceModel;
			this.DeviceAccessState = ((configuration.Status == DeviceRemoteWipeStatus.DeviceOk) ? LocalizedDescriptionAttribute.FromEnum(configuration.DeviceAccessState.GetType(), configuration.DeviceAccessState) : LocalizedDescriptionAttribute.FromEnum(configuration.Status.GetType(), configuration.Status));
			this.DevicePhoneNumber_LtrSpan = (string.IsNullOrEmpty(configuration.DevicePhoneNumber) ? Strings.NotAvailable : string.Format("<span dir=\"ltr\">{0}</span>", HttpUtility.HtmlEncode(configuration.DevicePhoneNumber)));
			this.IsRemoteWipeSupported = configuration.IsRemoteWipeSupported;
			this.DeviceStatusIsOK = (configuration.Status == DeviceRemoteWipeStatus.DeviceOk);
			this.ClientType = configuration.ClientType.ToString();
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x000B8836 File Offset: 0x000B6A36
		internal MobileDeviceInfo()
		{
		}

		// Token: 0x1700241F RID: 9247
		// (get) Token: 0x06003D60 RID: 15712 RVA: 0x000B883E File Offset: 0x000B6A3E
		// (set) Token: 0x06003D61 RID: 15713 RVA: 0x000B8846 File Offset: 0x000B6A46
		[DataMember]
		public Identity Identity { get; set; }

		// Token: 0x17002420 RID: 9248
		// (get) Token: 0x06003D62 RID: 15714 RVA: 0x000B884F File Offset: 0x000B6A4F
		// (set) Token: 0x06003D63 RID: 15715 RVA: 0x000B8857 File Offset: 0x000B6A57
		[DataMember]
		public string DeviceType { get; set; }

		// Token: 0x17002421 RID: 9249
		// (get) Token: 0x06003D64 RID: 15716 RVA: 0x000B8860 File Offset: 0x000B6A60
		// (set) Token: 0x06003D65 RID: 15717 RVA: 0x000B8868 File Offset: 0x000B6A68
		[DataMember]
		public string DeviceModel { get; set; }

		// Token: 0x17002422 RID: 9250
		// (get) Token: 0x06003D66 RID: 15718 RVA: 0x000B8871 File Offset: 0x000B6A71
		// (set) Token: 0x06003D67 RID: 15719 RVA: 0x000B8879 File Offset: 0x000B6A79
		[DataMember]
		public string DeviceAccessState { get; set; }

		// Token: 0x17002423 RID: 9251
		// (get) Token: 0x06003D68 RID: 15720 RVA: 0x000B8882 File Offset: 0x000B6A82
		// (set) Token: 0x06003D69 RID: 15721 RVA: 0x000B888A File Offset: 0x000B6A8A
		[DataMember]
		public string DevicePhoneNumber_LtrSpan { get; set; }

		// Token: 0x17002424 RID: 9252
		// (get) Token: 0x06003D6A RID: 15722 RVA: 0x000B8893 File Offset: 0x000B6A93
		// (set) Token: 0x06003D6B RID: 15723 RVA: 0x000B889B File Offset: 0x000B6A9B
		[DataMember]
		public bool IsRemoteWipeSupported { get; set; }

		// Token: 0x17002425 RID: 9253
		// (get) Token: 0x06003D6C RID: 15724 RVA: 0x000B88A4 File Offset: 0x000B6AA4
		// (set) Token: 0x06003D6D RID: 15725 RVA: 0x000B88AC File Offset: 0x000B6AAC
		[DataMember]
		public bool DeviceStatusIsOK { get; set; }

		// Token: 0x17002426 RID: 9254
		// (get) Token: 0x06003D6E RID: 15726 RVA: 0x000B88B5 File Offset: 0x000B6AB5
		// (set) Token: 0x06003D6F RID: 15727 RVA: 0x000B88BD File Offset: 0x000B6ABD
		[DataMember]
		public string PendingCommand { get; set; }

		// Token: 0x17002427 RID: 9255
		// (get) Token: 0x06003D70 RID: 15728 RVA: 0x000B88C6 File Offset: 0x000B6AC6
		// (set) Token: 0x06003D71 RID: 15729 RVA: 0x000B88CE File Offset: 0x000B6ACE
		[DataMember]
		public string ClientType { get; set; }

		// Token: 0x17002428 RID: 9256
		// (get) Token: 0x06003D72 RID: 15730 RVA: 0x000B88D7 File Offset: 0x000B6AD7
		// (set) Token: 0x06003D73 RID: 15731 RVA: 0x000B88DF File Offset: 0x000B6ADF
		public string StatusDisplayString { get; set; }

		// Token: 0x06003D74 RID: 15732 RVA: 0x000B88E8 File Offset: 0x000B6AE8
		public override bool Equals(object obj)
		{
			MobileDeviceInfo mobileDeviceInfo = obj as MobileDeviceInfo;
			return mobileDeviceInfo != null && mobileDeviceInfo.Identity == this.Identity;
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x000B8912 File Offset: 0x000B6B12
		public override int GetHashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x04002800 RID: 10240
		private const string PhoneNumberLtrFmt = "<span dir=\"ltr\">{0}</span>";
	}
}
