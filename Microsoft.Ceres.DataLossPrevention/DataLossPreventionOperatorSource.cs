using System;
using Microsoft.Ceres.CoreServices.Services.Node;
using Microsoft.Ceres.DataLossPrevention.Crawl;
using Microsoft.Ceres.DataLossPrevention.Query;
using Microsoft.Ceres.Evaluation.Operators;
using Microsoft.Ceres.Evaluation.Operators.PlugIns;

namespace Microsoft.Ceres.DataLossPrevention
{
	// Token: 0x02000010 RID: 16
	[DynamicComponent]
	public class DataLossPreventionOperatorSource : NamedPlugInSource<OperatorBase>
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00003BAC File Offset: 0x00001DAC
		protected override void AddPlugIns()
		{
			base.Add(typeof(DLPClassificationOperator), () => new DLPClassificationOperator());
			base.Add(typeof(DLPQuerySecurityOperator), () => new DLPQuerySecurityOperator());
			base.Add(typeof(DLPQuerySensitiveResultTranslationOperator), () => new DLPQuerySensitiveResultTranslationOperator());
			base.Add(typeof(DLPQuerySensitiveTypeTranslationOperator), () => new DLPQuerySensitiveTypeTranslationOperator());
		}
	}
}
