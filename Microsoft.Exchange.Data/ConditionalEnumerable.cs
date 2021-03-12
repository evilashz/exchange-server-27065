using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001EE RID: 494
	internal sealed class ConditionalEnumerable<T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06001113 RID: 4371 RVA: 0x00033DDD File Offset: 0x00031FDD
		public ConditionalEnumerable(IEnumerable<T> conditionalEnumerable, IEnumerable<T> secondEnumerable)
		{
			if (conditionalEnumerable == null)
			{
				throw new ArgumentNullException("conditionalEnumerable");
			}
			if (secondEnumerable == null)
			{
				throw new ArgumentNullException("secondEnumerable");
			}
			this.conditionalEnumerable = conditionalEnumerable;
			this.secondEnumerable = secondEnumerable;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00033E0F File Offset: 0x0003200F
		public IEnumerator<T> GetEnumerator()
		{
			return new ConditionalEnumerator<T>(this.conditionalEnumerable, this.secondEnumerable);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00033E22 File Offset: 0x00032022
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ConditionalEnumerator<T>(this.conditionalEnumerable, this.secondEnumerable);
		}

		// Token: 0x04000A8B RID: 2699
		private IEnumerable<T> conditionalEnumerable;

		// Token: 0x04000A8C RID: 2700
		private IEnumerable<T> secondEnumerable;
	}
}
