using System;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.Pipeline
{
	// Token: 0x020000B2 RID: 178
	internal sealed class PipelineConfigAdapter : IConfigAdapter
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x0001187A File Offset: 0x0000FA7A
		internal PipelineConfigAdapter(IPipelineComponentConfig config)
		{
			Util.ThrowOnNullArgument(config, "config");
			this.config = config;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00011894 File Offset: 0x0000FA94
		public string GetSetting(string key)
		{
			return this.config[key];
		}

		// Token: 0x0400027B RID: 635
		private readonly IPipelineComponentConfig config;
	}
}
