using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x02000835 RID: 2101
	[DataContract]
	internal class ServerSnapshotStatus
	{
		// Token: 0x06002C95 RID: 11413 RVA: 0x00064F60 File Offset: 0x00063160
		public ServerSnapshotStatus(string serverIdentity)
		{
			this.ServerIdentity = serverIdentity;
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x00064F6F File Offset: 0x0006316F
		// (set) Token: 0x06002C97 RID: 11415 RVA: 0x00064F77 File Offset: 0x00063177
		[DataMember(IsRequired = true)]
		public string ServerIdentity { get; private set; }

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06002C98 RID: 11416 RVA: 0x00064F80 File Offset: 0x00063180
		// (set) Token: 0x06002C99 RID: 11417 RVA: 0x00064F88 File Offset: 0x00063188
		[DataMember(IsRequired = true)]
		public DateTime? TimeOfLastSuccess { get; set; }

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x00064F91 File Offset: 0x00063191
		// (set) Token: 0x06002C9B RID: 11419 RVA: 0x00064F99 File Offset: 0x00063199
		[DataMember(IsRequired = true)]
		public DateTime? TimeOfLastFailure { get; set; }

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x00064FA2 File Offset: 0x000631A2
		// (set) Token: 0x06002C9D RID: 11421 RVA: 0x00064FAA File Offset: 0x000631AA
		[DataMember(IsRequired = true)]
		public string LastError { get; set; }

		// Token: 0x06002C9E RID: 11422 RVA: 0x00064FB4 File Offset: 0x000631B4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "ServerIdentity: [{0}] TimeOfLastSuccess: [{1}] TimeOfLastError: [{2}] LastError: [{3}]", new object[]
			{
				this.ServerIdentity,
				(this.TimeOfLastSuccess != null) ? this.TimeOfLastSuccess.ToString() : string.Empty,
				(this.TimeOfLastFailure != null) ? this.TimeOfLastFailure.ToString() : string.Empty,
				(this.LastError != null) ? this.LastError : string.Empty
			});
		}
	}
}
