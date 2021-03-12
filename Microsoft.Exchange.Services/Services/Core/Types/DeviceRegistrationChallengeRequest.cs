using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000415 RID: 1045
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class DeviceRegistrationChallengeRequest : BaseRequest
	{
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x0009FA47 File Offset: 0x0009DC47
		// (set) Token: 0x06001DF3 RID: 7667 RVA: 0x0009FA4F File Offset: 0x0009DC4F
		[DataMember(Name = "AppId", IsRequired = true)]
		public string AppId { get; set; }

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x0009FA58 File Offset: 0x0009DC58
		// (set) Token: 0x06001DF5 RID: 7669 RVA: 0x0009FA60 File Offset: 0x0009DC60
		[DataMember(Name = "DeviceNotificationId", IsRequired = true)]
		public string DeviceNotificationId { get; set; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x0009FA69 File Offset: 0x0009DC69
		// (set) Token: 0x06001DF7 RID: 7671 RVA: 0x0009FA71 File Offset: 0x0009DC71
		[DataMember(Name = "ClientWatermark", IsRequired = true)]
		public string ClientWatermark { get; set; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x0009FA7A File Offset: 0x0009DC7A
		// (set) Token: 0x06001DF9 RID: 7673 RVA: 0x0009FA82 File Offset: 0x0009DC82
		[DataMember(Name = "DeviceNotificationType", IsRequired = true)]
		public string DeviceNotificationType { get; set; }

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x0009FA8C File Offset: 0x0009DC8C
		public PushNotificationPlatform Platform
		{
			get
			{
				if (this.platform == null)
				{
					PushNotificationPlatform pushNotificationPlatform;
					this.platform = new PushNotificationPlatform?(Enum.TryParse<PushNotificationPlatform>(this.DeviceNotificationType, out pushNotificationPlatform) ? pushNotificationPlatform : PushNotificationPlatform.None);
				}
				return this.platform.Value;
			}
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x0009FACF File Offset: 0x0009DCCF
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new RequestDeviceRegistrationChallenge(callContext, this);
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x0009FAD8 File Offset: 0x0009DCD8
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x0009FADB File Offset: 0x0009DCDB
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}

		// Token: 0x04001358 RID: 4952
		private PushNotificationPlatform? platform;
	}
}
