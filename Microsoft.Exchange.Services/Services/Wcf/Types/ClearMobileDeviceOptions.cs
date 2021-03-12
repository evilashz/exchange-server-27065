using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AE5 RID: 2789
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ClearMobileDeviceOptions : OptionsPropertyChangeTracker
	{
		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06004F5C RID: 20316 RVA: 0x00108921 File Offset: 0x00106B21
		// (set) Token: 0x06004F5D RID: 20317 RVA: 0x00108929 File Offset: 0x00106B29
		[DataMember]
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
				base.TrackPropertyChanged("Cancel");
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06004F5E RID: 20318 RVA: 0x0010893D File Offset: 0x00106B3D
		// (set) Token: 0x06004F5F RID: 20319 RVA: 0x00108945 File Offset: 0x00106B45
		[DataMember]
		public Identity Identity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
				base.TrackPropertyChanged("Identity");
			}
		}

		// Token: 0x04002C64 RID: 11364
		private bool cancel;

		// Token: 0x04002C65 RID: 11365
		private Identity identity;
	}
}
