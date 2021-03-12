using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200018D RID: 397
	public sealed class ReconciliationCookie
	{
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x00027C3B File Offset: 0x00025E3B
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x00027C43 File Offset: 0x00025E43
		public int Version { get; private set; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00027C4C File Offset: 0x00025E4C
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x00027C54 File Offset: 0x00025E54
		public string DCHostName { get; private set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x00027C5D File Offset: 0x00025E5D
		// (set) Token: 0x06000CCC RID: 3276 RVA: 0x00027C65 File Offset: 0x00025E65
		public Guid InvocationId { get; private set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00027C6E File Offset: 0x00025E6E
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x00027C76 File Offset: 0x00025E76
		public long HighestCommittedUsn { get; private set; }

		// Token: 0x06000CCF RID: 3279 RVA: 0x00027C7F File Offset: 0x00025E7F
		public ReconciliationCookie(int version, string dcHostName, Guid invocationId, long highestCommittedUsn)
		{
			this.Version = version;
			this.DCHostName = dcHostName;
			this.InvocationId = invocationId;
			this.HighestCommittedUsn = highestCommittedUsn;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00027CA4 File Offset: 0x00025EA4
		public ReconciliationCookie(string value)
		{
			string[] array = value.Split(new char[]
			{
				';'
			});
			if (array.Length == 4)
			{
				this.Version = Convert.ToInt32(array[0]);
				this.DCHostName = array[1];
				this.InvocationId = new Guid(array[2]);
				this.HighestCommittedUsn = Convert.ToInt64(array[3]);
				return;
			}
			throw new FormatException(string.Format("Incorrect format for ReconciliationCookie: {0}", value));
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00027D15 File Offset: 0x00025F15
		public static ReconciliationCookie Parse(string value)
		{
			return new ReconciliationCookie(value);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00027D20 File Offset: 0x00025F20
		public override string ToString()
		{
			return string.Format("{0};{1};{2};{3}", new object[]
			{
				this.Version,
				this.DCHostName,
				this.InvocationId,
				this.HighestCommittedUsn
			});
		}
	}
}
