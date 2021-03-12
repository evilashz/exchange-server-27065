using System;
using System.Collections.Generic;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x02000067 RID: 103
	public class ReflectionCreateOperation : ReflectionOperation
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x00010844 File Offset: 0x0000EA44
		public override void Execute(IDictionary<string, object> variables)
		{
			Type type = DalProbeOperation.ResolveDataType(base.Type);
			object[] parameterValues = base.GetParameterValues(variables);
			object value = Activator.CreateInstance(type, parameterValues);
			if (!string.IsNullOrEmpty(base.Return))
			{
				variables[base.Return] = value;
			}
		}
	}
}
