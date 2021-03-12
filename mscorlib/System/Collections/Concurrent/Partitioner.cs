using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Collections.Concurrent
{
	// Token: 0x02000484 RID: 1156
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public abstract class Partitioner<TSource>
	{
		// Token: 0x0600386F RID: 14447
		[__DynamicallyInvokable]
		public abstract IList<IEnumerator<TSource>> GetPartitions(int partitionCount);

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06003870 RID: 14448 RVA: 0x000D7DEA File Offset: 0x000D5FEA
		[__DynamicallyInvokable]
		public virtual bool SupportsDynamicPartitions
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x000D7DED File Offset: 0x000D5FED
		[__DynamicallyInvokable]
		public virtual IEnumerable<TSource> GetDynamicPartitions()
		{
			throw new NotSupportedException(Environment.GetResourceString("Partitioner_DynamicPartitionsNotSupported"));
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x000D7DFE File Offset: 0x000D5FFE
		[__DynamicallyInvokable]
		protected Partitioner()
		{
		}
	}
}
