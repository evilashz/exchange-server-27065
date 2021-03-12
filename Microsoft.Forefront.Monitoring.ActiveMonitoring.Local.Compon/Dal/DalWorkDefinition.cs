using System;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x02000060 RID: 96
	public class DalWorkDefinition
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000F789 File Offset: 0x0000D989
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000F791 File Offset: 0x0000D991
		[XmlArrayItem("DeserializeDataContract", typeof(DataContractDeserializerOperation))]
		[XmlArrayItem("DalSave", typeof(SaveOperation))]
		[XmlArrayItem("DeserializeADObject", typeof(ADObjectDeserializerOperation))]
		[XmlArrayItem("New", typeof(ReflectionCreateOperation))]
		[XmlArrayItem("Assert", typeof(AssertOperation))]
		[XmlArrayItem("Deserialize", typeof(XmlDeserializerOperation))]
		[XmlArrayItem("Invoke", typeof(ReflectionInvokeOperation))]
		[XmlArrayItem("DalFind", typeof(FindOperation))]
		[XmlArrayItem("DalDelete", typeof(DeleteOperation))]
		[XmlArrayItem("DalTest", typeof(TestReadWriteOperation))]
		public DalProbeOperation[] Operations { get; set; }
	}
}
