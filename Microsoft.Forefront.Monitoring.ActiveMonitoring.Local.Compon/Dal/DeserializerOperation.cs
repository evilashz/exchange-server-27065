using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x02000058 RID: 88
	public abstract class DeserializerOperation : DalProbeOperation
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000EFAD File Offset: 0x0000D1AD
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000EFB5 File Offset: 0x0000D1B5
		[XmlAttribute]
		public string Type { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000EFBE File Offset: 0x0000D1BE
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000EFC6 File Offset: 0x0000D1C6
		[XmlAttribute]
		public string AdditionalTypes { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000EFCF File Offset: 0x0000D1CF
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000EFD7 File Offset: 0x0000D1D7
		[XmlAnyElement]
		public XElement DataObject { get; set; }

		// Token: 0x06000254 RID: 596 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
		public override void Execute(IDictionary<string, object> variables)
		{
			if (string.IsNullOrEmpty(base.Return))
			{
				throw new ArgumentNullException("Return");
			}
			Type parameterType = DalProbeOperation.ResolveDataType(this.Type);
			Type[] additionalTypes = null;
			if (!string.IsNullOrEmpty(this.AdditionalTypes))
			{
				string[] source = this.AdditionalTypes.Split(new char[0]);
				additionalTypes = source.Select(new Func<string, Type>(DalProbeOperation.ResolveDataType)).ToArray<Type>();
			}
			variables[base.Return] = this.DeserializedValue(parameterType, additionalTypes);
		}

		// Token: 0x06000255 RID: 597
		protected abstract object DeserializedValue(Type parameterType, Type[] additionalTypes);
	}
}
