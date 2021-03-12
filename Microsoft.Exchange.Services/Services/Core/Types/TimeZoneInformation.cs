using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000B0C RID: 2828
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class TimeZoneInformation : OptionsPropertyChangeTracker
	{
		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06005048 RID: 20552 RVA: 0x001096E6 File Offset: 0x001078E6
		// (set) Token: 0x06005049 RID: 20553 RVA: 0x001096EE File Offset: 0x001078EE
		[DataMember]
		public string TimeZoneId
		{
			get
			{
				return this.timeZoneId;
			}
			set
			{
				this.timeZoneId = value;
				base.TrackPropertyChanged("TimeZoneId");
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x0600504A RID: 20554 RVA: 0x00109702 File Offset: 0x00107902
		// (set) Token: 0x0600504B RID: 20555 RVA: 0x0010970A File Offset: 0x0010790A
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
				base.TrackPropertyChanged("DisplayName");
			}
		}

		// Token: 0x04002CE0 RID: 11488
		private string timeZoneId;

		// Token: 0x04002CE1 RID: 11489
		private string displayName;
	}
}
