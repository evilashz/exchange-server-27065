using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x02000068 RID: 104
	public class ReflectionInvokeOperation : ReflectionOperation
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0001088F File Offset: 0x0000EA8F
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x00010897 File Offset: 0x0000EA97
		[XmlAttribute]
		public string This { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x000108A0 File Offset: 0x0000EAA0
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x000108A8 File Offset: 0x0000EAA8
		[XmlAttribute]
		public string Method { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000108B1 File Offset: 0x0000EAB1
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x000108B9 File Offset: 0x0000EAB9
		[XmlAttribute]
		public string ParameterTypes { get; set; }

		// Token: 0x060002BA RID: 698 RVA: 0x000108C4 File Offset: 0x0000EAC4
		public override void Execute(IDictionary<string, object> variables)
		{
			object obj = null;
			if (!string.IsNullOrEmpty(this.This))
			{
				obj = DalProbeOperation.GetValue(this.This, variables);
			}
			Type type = (obj != null) ? obj.GetType() : DalProbeOperation.ResolveDataType(base.Type);
			object[] parameterValues = base.GetParameterValues(variables);
			object value = type.InvokeMember(this.Method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, new DalProbeBinder(), obj, parameterValues);
			if (!string.IsNullOrEmpty(base.Return))
			{
				variables[base.Return] = value;
			}
			for (int i = 0; i < parameterValues.Length; i++)
			{
				if (DalProbeOperation.IsVariable(base.Parameters[i].Value))
				{
					variables[base.Parameters[i].Value] = parameterValues[i];
				}
			}
		}
	}
}
