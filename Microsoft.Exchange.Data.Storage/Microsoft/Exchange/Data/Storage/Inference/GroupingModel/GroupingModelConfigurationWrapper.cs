using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Inference.GroupingModel
{
	// Token: 0x02000F61 RID: 3937
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GroupingModelConfigurationWrapper : IGroupingModelConfiguration
	{
		// Token: 0x060086C9 RID: 34505 RVA: 0x0024F3E4 File Offset: 0x0024D5E4
		public GroupingModelConfigurationWrapper(GroupingModelConfiguration modelConfiguration)
		{
			ArgumentValidator.ThrowIfNull("modelConfiguration", modelConfiguration);
			this.modelConfiguration = modelConfiguration;
		}

		// Token: 0x170023B6 RID: 9142
		// (get) Token: 0x060086CA RID: 34506 RVA: 0x0024F3FE File Offset: 0x0024D5FE
		public int CurrentVersion
		{
			get
			{
				return this.modelConfiguration.CurrentVersion;
			}
		}

		// Token: 0x170023B7 RID: 9143
		// (get) Token: 0x060086CB RID: 34507 RVA: 0x0024F40B File Offset: 0x0024D60B
		public int MinimumSupportedVersion
		{
			get
			{
				return this.modelConfiguration.MinimumSupportedVersion;
			}
		}

		// Token: 0x04005A21 RID: 23073
		private readonly GroupingModelConfiguration modelConfiguration;
	}
}
