using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class CommonConfigurationBase : ICommonConfiguration
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002404 File Offset: 0x00000604
		public virtual bool OutlookActivityProcessingEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000240B File Offset: 0x0000060B
		public virtual TimeSpan OutlookActivityProcessingCutoffWindow
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002412 File Offset: 0x00000612
		public virtual bool OutlookActivityProcessingEnabledInEba
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002419 File Offset: 0x00000619
		public virtual bool PersistedLabelsEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
