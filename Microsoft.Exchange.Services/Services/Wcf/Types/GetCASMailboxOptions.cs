using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AA1 RID: 2721
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCASMailboxOptions : OptionsPropertyChangeTracker
	{
		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06004CD8 RID: 19672 RVA: 0x001068F4 File Offset: 0x00104AF4
		// (set) Token: 0x06004CD9 RID: 19673 RVA: 0x001068FC File Offset: 0x00104AFC
		[DataMember]
		public bool ActiveSyncDebugLogging
		{
			get
			{
				return this.activeSyncDebugLogging;
			}
			set
			{
				this.activeSyncDebugLogging = value;
				base.TrackPropertyChanged("ActiveSyncDebugLogging");
			}
		}

		// Token: 0x04002B6C RID: 11116
		private bool activeSyncDebugLogging;
	}
}
