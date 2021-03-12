using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	public class CredentialRecords
	{
		// Token: 0x06000183 RID: 387 RVA: 0x000084A7 File Offset: 0x000066A7
		public CredentialRecords()
		{
			this.credentialRecords = new MultiValuedProperty<CredentialRecord>();
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000084BA File Offset: 0x000066BA
		// (set) Token: 0x06000185 RID: 389 RVA: 0x000084C2 File Offset: 0x000066C2
		public MultiValuedProperty<CredentialRecord> Records
		{
			get
			{
				return this.credentialRecords;
			}
			set
			{
				this.credentialRecords = value;
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000084CB File Offset: 0x000066CB
		public override string ToString()
		{
			return "Number of credentials " + this.Records.Count;
		}

		// Token: 0x0400010A RID: 266
		private MultiValuedProperty<CredentialRecord> credentialRecords;
	}
}
