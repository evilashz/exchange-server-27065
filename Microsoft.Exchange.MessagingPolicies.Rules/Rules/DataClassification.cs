using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200000E RID: 14
	public class DataClassification
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003AD5 File Offset: 0x00001CD5
		public DataClassification(string id)
		{
			this.Id = id;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003AE4 File Offset: 0x00001CE4
		public DataClassification()
		{
			this.Id = string.Empty;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003AF7 File Offset: 0x00001CF7
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003AFF File Offset: 0x00001CFF
		public string Id { get; set; }

		// Token: 0x040000A3 RID: 163
		public const string Name = "DataClassification";
	}
}
