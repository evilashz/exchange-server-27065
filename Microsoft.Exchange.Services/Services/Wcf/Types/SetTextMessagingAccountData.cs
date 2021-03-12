using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A90 RID: 2704
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetTextMessagingAccountData : OptionsPropertyChangeTracker
	{
		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x06004C74 RID: 19572 RVA: 0x00106495 File Offset: 0x00104695
		// (set) Token: 0x06004C75 RID: 19573 RVA: 0x0010649D File Offset: 0x0010469D
		[DataMember]
		public string CountryRegionId
		{
			get
			{
				return this.countryRegionId;
			}
			set
			{
				this.countryRegionId = value;
				base.TrackPropertyChanged("CountryRegionId");
			}
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x06004C76 RID: 19574 RVA: 0x001064B1 File Offset: 0x001046B1
		// (set) Token: 0x06004C77 RID: 19575 RVA: 0x001064B9 File Offset: 0x001046B9
		[DataMember]
		public int MobileOperatorId
		{
			get
			{
				return this.mobileOperatorId;
			}
			set
			{
				this.mobileOperatorId = value;
				base.TrackPropertyChanged("MobileOperatorId");
			}
		}

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x06004C78 RID: 19576 RVA: 0x001064CD File Offset: 0x001046CD
		// (set) Token: 0x06004C79 RID: 19577 RVA: 0x001064D5 File Offset: 0x001046D5
		[DataMember]
		public string NotificationPhoneNumber
		{
			get
			{
				return this.notificationPhoneNumber;
			}
			set
			{
				this.notificationPhoneNumber = value;
				base.TrackPropertyChanged("NotificationPhoneNumber");
			}
		}

		// Token: 0x04002B46 RID: 11078
		private string countryRegionId;

		// Token: 0x04002B47 RID: 11079
		private int mobileOperatorId;

		// Token: 0x04002B48 RID: 11080
		private string notificationPhoneNumber;
	}
}
