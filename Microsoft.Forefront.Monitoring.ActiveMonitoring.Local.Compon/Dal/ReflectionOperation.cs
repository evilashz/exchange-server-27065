using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x02000066 RID: 102
	public abstract class ReflectionOperation : DalProbeOperation
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000107BA File Offset: 0x0000E9BA
		// (set) Token: 0x060002AD RID: 685 RVA: 0x000107C2 File Offset: 0x0000E9C2
		[XmlAttribute]
		public string Type { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002AE RID: 686 RVA: 0x000107CB File Offset: 0x0000E9CB
		// (set) Token: 0x060002AF RID: 687 RVA: 0x000107D3 File Offset: 0x0000E9D3
		[XmlArrayItem("Parameter")]
		public ReflectionParameter[] Parameters { get; set; }

		// Token: 0x060002B0 RID: 688 RVA: 0x000107F4 File Offset: 0x0000E9F4
		protected object[] GetParameterValues(IDictionary<string, object> variables)
		{
			if (this.Parameters == null)
			{
				return new object[0];
			}
			return (from parameter in this.Parameters
			select parameter.Evaluate(variables)).ToArray<object>();
		}
	}
}
