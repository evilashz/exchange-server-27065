using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007E5 RID: 2021
	internal class LazyMember<T>
	{
		// Token: 0x06003B5C RID: 15196 RVA: 0x000CFFD6 File Offset: 0x000CE1D6
		public LazyMember(InitializeLazyMember<T> initializationDelegate)
		{
			this.initializationDelegate = initializationDelegate;
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x000CFFF0 File Offset: 0x000CE1F0
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
						}
						this.initialized = true;
					}
				}
				return this.lazyMember;
			}
		}

		// Token: 0x040020B8 RID: 8376
		private T lazyMember;

		// Token: 0x040020B9 RID: 8377
		private InitializeLazyMember<T> initializationDelegate;

		// Token: 0x040020BA RID: 8378
		private object lockObject = new object();

		// Token: 0x040020BB RID: 8379
		private bool initialized;
	}
}
