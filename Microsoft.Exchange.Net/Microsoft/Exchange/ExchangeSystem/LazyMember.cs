using System;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000B04 RID: 2820
	internal class LazyMember<T>
	{
		// Token: 0x06003CA5 RID: 15525 RVA: 0x0009DF6C File Offset: 0x0009C16C
		public LazyMember(Func<T> initializationDelegate)
		{
			this.initializationDelegate = initializationDelegate;
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06003CA6 RID: 15526 RVA: 0x0009DF88 File Offset: 0x0009C188
		public T Member
		{
			get
			{
				if (!this.initialized)
				{
					lock (this.lockObject)
					{
						if (!this.initialized)
						{
							this.lazyMember = this.initializationDelegate();
							this.initialized = true;
						}
					}
				}
				return this.lazyMember;
			}
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x0009DFF0 File Offset: 0x0009C1F0
		public override string ToString()
		{
			string result = null;
			if (this.Member != null)
			{
				T member = this.Member;
				result = member.ToString();
			}
			return result;
		}

		// Token: 0x0400354A RID: 13642
		private T lazyMember;

		// Token: 0x0400354B RID: 13643
		private Func<T> initializationDelegate;

		// Token: 0x0400354C RID: 13644
		private object lockObject = new object();

		// Token: 0x0400354D RID: 13645
		private bool initialized;
	}
}
