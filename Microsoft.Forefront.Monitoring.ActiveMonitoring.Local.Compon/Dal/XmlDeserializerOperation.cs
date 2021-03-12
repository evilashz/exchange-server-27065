using System;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x0200006D RID: 109
	public class XmlDeserializerOperation : DeserializerOperation
	{
		// Token: 0x060002DE RID: 734 RVA: 0x00011038 File Offset: 0x0000F238
		protected override object DeserializedValue(Type parameterType, Type[] additionalTypes)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(parameterType, additionalTypes);
			return xmlSerializer.Deserialize(base.DataObject.CreateReader());
		}
	}
}
