using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AEC RID: 2796
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MobileDeviceStatisticsOptions : OptionsPropertyChangeTracker
	{
		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06004FBC RID: 20412 RVA: 0x00108E79 File Offset: 0x00107079
		// (set) Token: 0x06004FBD RID: 20413 RVA: 0x00108E81 File Offset: 0x00107081
		[DataMember]
		public bool ActiveSync
		{
			get
			{
				return this.activeSyncEnabled;
			}
			set
			{
				this.activeSyncEnabled = value;
				base.TrackPropertyChanged("ActiveSync");
			}
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06004FBE RID: 20414 RVA: 0x00108E95 File Offset: 0x00107095
		// (set) Token: 0x06004FBF RID: 20415 RVA: 0x00108E9D File Offset: 0x0010709D
		[DataMember]
		public bool GetMailboxLog
		{
			get
			{
				return this.getMailboxLog;
			}
			set
			{
				this.getMailboxLog = value;
				base.TrackPropertyChanged("GetMailboxLog");
			}
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06004FC0 RID: 20416 RVA: 0x00108EB1 File Offset: 0x001070B1
		// (set) Token: 0x06004FC1 RID: 20417 RVA: 0x00108EB9 File Offset: 0x001070B9
		[DataMember]
		public bool ShowRecoveryPassword
		{
			get
			{
				return this.showRecoveryPwd;
			}
			set
			{
				this.showRecoveryPwd = value;
				base.TrackPropertyChanged("ShowRecoveryPassword");
			}
		}

		// Token: 0x04002C8A RID: 11402
		private bool activeSyncEnabled;

		// Token: 0x04002C8B RID: 11403
		private bool getMailboxLog;

		// Token: 0x04002C8C RID: 11404
		private bool showRecoveryPwd;
	}
}
