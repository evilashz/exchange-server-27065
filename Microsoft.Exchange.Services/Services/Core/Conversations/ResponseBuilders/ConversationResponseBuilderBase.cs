using System;

namespace Microsoft.Exchange.Services.Core.Conversations.ResponseBuilders
{
	// Token: 0x020003A9 RID: 937
	internal abstract class ConversationResponseBuilderBase<T>
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0009730C File Offset: 0x0009550C
		// (set) Token: 0x06001A5D RID: 6749 RVA: 0x00097314 File Offset: 0x00095514
		private protected T Response { protected get; private set; }

		// Token: 0x06001A5E RID: 6750 RVA: 0x0009731D File Offset: 0x0009551D
		public T Build()
		{
			this.Response = this.BuildSkeleton();
			this.BuildConversationProperties();
			this.BuildNodes();
			return this.Response;
		}

		// Token: 0x06001A5F RID: 6751
		protected abstract T BuildSkeleton();

		// Token: 0x06001A60 RID: 6752
		protected abstract void BuildConversationProperties();

		// Token: 0x06001A61 RID: 6753
		protected abstract void BuildNodes();
	}
}
