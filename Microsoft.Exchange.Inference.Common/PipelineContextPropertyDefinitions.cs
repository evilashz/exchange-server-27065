using System;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000039 RID: 57
	internal static class PipelineContextPropertyDefinitions
	{
		// Token: 0x040000E5 RID: 229
		public static readonly PropertyDefinition TrainingConfiguration = new SimplePropertyDefinition("TrainingConfiguration", typeof(ITrainingConfiguration));
	}
}
