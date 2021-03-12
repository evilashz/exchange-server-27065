using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000039 RID: 57
	internal class StoreDriverSubmitContext : PoisonContext
	{
		// Token: 0x06000143 RID: 323 RVA: 0x0000650C File Offset: 0x0000470C
		public StoreDriverSubmitContext(string poisonId) : base(MessageProcessingSource.StoreDriverSubmit)
		{
			this.poisonId = poisonId;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000651C File Offset: 0x0000471C
		public override string ToString()
		{
			return this.poisonId;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006524 File Offset: 0x00004724
		public override int GetHashCode()
		{
			return this.poisonId.GetHashCode();
		}

		// Token: 0x040000AD RID: 173
		private string poisonId;
	}
}
