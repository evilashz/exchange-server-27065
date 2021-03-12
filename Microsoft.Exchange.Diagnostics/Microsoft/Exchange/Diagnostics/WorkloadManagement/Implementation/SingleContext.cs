using System;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation
{
	// Token: 0x02000201 RID: 513
	internal class SingleContext : IContextPlugin
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0003D4DC File Offset: 0x0003B6DC
		public static IContextPlugin Singleton
		{
			get
			{
				if (SingleContext.singleton == null)
				{
					SingleContext.singleton = new SingleContext();
				}
				return SingleContext.singleton;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0003D4F4 File Offset: 0x0003B6F4
		// (set) Token: 0x06000F12 RID: 3858 RVA: 0x0003D538 File Offset: 0x0003B738
		public Guid? LocalId
		{
			get
			{
				foreach (IContextPlugin contextPlugin in SingleContext.pluginChain)
				{
					if (contextPlugin.IsContextPresent)
					{
						return contextPlugin.LocalId;
					}
				}
				return null;
			}
			set
			{
				foreach (IContextPlugin contextPlugin in SingleContext.pluginChain)
				{
					contextPlugin.LocalId = value;
				}
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0003D564 File Offset: 0x0003B764
		public bool IsContextPresent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0003D568 File Offset: 0x0003B768
		public void SetId()
		{
			foreach (IContextPlugin contextPlugin in SingleContext.pluginChain)
			{
				contextPlugin.SetId();
			}
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0003D594 File Offset: 0x0003B794
		public bool CheckId()
		{
			foreach (IContextPlugin contextPlugin in SingleContext.pluginChain)
			{
				if (contextPlugin.IsContextPresent)
				{
					return contextPlugin.CheckId();
				}
			}
			return false;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0003D5D0 File Offset: 0x0003B7D0
		public void Clear()
		{
			foreach (IContextPlugin contextPlugin in SingleContext.pluginChain)
			{
				contextPlugin.Clear();
			}
			DebugContext.Clear();
		}

		// Token: 0x04000AB1 RID: 2737
		private static readonly IContextPlugin[] pluginChain = new IContextPlugin[]
		{
			HttpCallContext.Singleton,
			DotNetCallContext.Singleton
		};

		// Token: 0x04000AB2 RID: 2738
		private static IContextPlugin singleton = null;
	}
}
