using System;
using System.Runtime.Remoting.Messaging;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation
{
	// Token: 0x020001FC RID: 508
	internal class DotNetCallContext : IContextPlugin
	{
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0003D167 File Offset: 0x0003B367
		public static IContextPlugin Singleton
		{
			get
			{
				return DotNetCallContext.singleton;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0003D16E File Offset: 0x0003B36E
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x0003D17F File Offset: 0x0003B37F
		public Guid? LocalId
		{
			get
			{
				return (Guid?)CallContext.LogicalGetData("MSExchangeLocalId");
			}
			set
			{
				if (value != null)
				{
					CallContext.LogicalSetData("MSExchangeLocalId", value);
					return;
				}
				CallContext.FreeNamedDataSlot("MSExchangeLocalId");
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0003D1A5 File Offset: 0x0003B3A5
		public bool IsContextPresent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003D1A8 File Offset: 0x0003B3A8
		public void SetId()
		{
			CallContext.LogicalSetData("SingleContextIdKey", Environment.CurrentManagedThreadId);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003D1C0 File Offset: 0x0003B3C0
		public bool CheckId()
		{
			object obj = CallContext.LogicalGetData("SingleContextIdKey");
			return obj != null && (int)obj == Environment.CurrentManagedThreadId;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003D1EA File Offset: 0x0003B3EA
		public void Clear()
		{
			CallContext.FreeNamedDataSlot("MSExchangeLocalId");
			CallContext.FreeNamedDataSlot("SingleContextIdKey");
		}

		// Token: 0x04000AAA RID: 2730
		private static IContextPlugin singleton = new DotNetCallContext();
	}
}
