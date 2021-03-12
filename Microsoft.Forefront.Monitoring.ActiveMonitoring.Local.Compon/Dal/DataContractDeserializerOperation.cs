using System;
using System.Runtime.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x02000061 RID: 97
	public class DataContractDeserializerOperation : DeserializerOperation
	{
		// Token: 0x0600027D RID: 637 RVA: 0x0000F7A4 File Offset: 0x0000D9A4
		protected override object DeserializedValue(Type parameterType, Type[] additionalTypes)
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(parameterType, additionalTypes);
			return dataContractSerializer.ReadObject(base.DataObject.CreateReader());
		}
	}
}
