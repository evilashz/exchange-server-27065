using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A1A RID: 2586
	[Serializable]
	public sealed class MigrationBatchError : MigrationError
	{
		// Token: 0x17001A24 RID: 6692
		// (get) Token: 0x06005F25 RID: 24357 RVA: 0x00191991 File Offset: 0x0018FB91
		// (set) Token: 0x06005F26 RID: 24358 RVA: 0x00191999 File Offset: 0x0018FB99
		public int RowIndex { get; internal set; }

		// Token: 0x06005F27 RID: 24359 RVA: 0x001919A2 File Offset: 0x0018FBA2
		public override string ToString()
		{
			return ServerStrings.MigrationBatchErrorString(this.RowIndex, base.EmailAddress, base.LocalizedErrorMessage);
		}
	}
}
