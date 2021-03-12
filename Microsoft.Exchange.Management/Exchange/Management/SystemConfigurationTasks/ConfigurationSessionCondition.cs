using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000935 RID: 2357
	[Serializable]
	internal abstract class ConfigurationSessionCondition : Condition
	{
		// Token: 0x060053EE RID: 21486 RVA: 0x0015AF20 File Offset: 0x00159120
		protected ConfigurationSessionCondition(IConfigurationSession session)
		{
			this.session = session;
		}

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x060053EF RID: 21487 RVA: 0x0015AF2F File Offset: 0x0015912F
		// (set) Token: 0x060053F0 RID: 21488 RVA: 0x0015AF37 File Offset: 0x00159137
		protected IConfigurationSession Session
		{
			get
			{
				return this.session;
			}
			set
			{
				this.session = value;
			}
		}

		// Token: 0x04003107 RID: 12551
		private IConfigurationSession session;
	}
}
