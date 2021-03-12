using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000235 RID: 565
	internal class BudgetWrapperBase<T> : DisposableWrapper<T> where T : class, IDisposable
	{
		// Token: 0x06001E03 RID: 7683 RVA: 0x0003DD5C File Offset: 0x0003BF5C
		public BudgetWrapperBase(T wrappedObject, bool ownsObject, Func<IDisposable> createCostHandleDelegate, Action<uint> chargeDelegate) : base(wrappedObject, ownsObject)
		{
			this.CreateCostHandle = createCostHandleDelegate;
			this.Charge = chargeDelegate;
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x0003DD75 File Offset: 0x0003BF75
		// (set) Token: 0x06001E05 RID: 7685 RVA: 0x0003DD7D File Offset: 0x0003BF7D
		public Func<IDisposable> CreateCostHandle { get; private set; }

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x0003DD86 File Offset: 0x0003BF86
		// (set) Token: 0x06001E07 RID: 7687 RVA: 0x0003DD8E File Offset: 0x0003BF8E
		public Action<uint> Charge { get; private set; }
	}
}
