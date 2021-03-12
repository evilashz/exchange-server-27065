using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Hygiene.Data;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x02000069 RID: 105
	public class ReflectionParameter
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00010986 File Offset: 0x0000EB86
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0001098E File Offset: 0x0000EB8E
		[XmlAttribute]
		public string Value { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00010997 File Offset: 0x0000EB97
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0001099F File Offset: 0x0000EB9F
		[XmlAttribute]
		public string Type { get; set; }

		// Token: 0x060002C0 RID: 704 RVA: 0x000109A8 File Offset: 0x0000EBA8
		public object Evaluate(IDictionary<string, object> variables)
		{
			object obj = DalProbeOperation.GetValue(this.Value, variables);
			if (obj != null && this.Type != null)
			{
				Type type = DalProbeOperation.ResolveDataType(this.Type);
				if (type != null && !type.IsInstanceOfType(obj))
				{
					obj = DalHelper.ConvertFromStoreObject(obj, type);
				}
			}
			return obj;
		}
	}
}
