using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000376 RID: 886
	public static class ServiceInstanceMapSerializer
	{
		// Token: 0x06001F13 RID: 7955 RVA: 0x0008625C File Offset: 0x0008445C
		public static string ConvertServiceInstanceMapToXml(ServiceInstanceMapValue map)
		{
			if (map != null)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(ServiceInstanceMapValue));
				using (StringWriter stringWriter = new StringWriter())
				{
					xmlSerializer.Serialize(stringWriter, map);
					return stringWriter.ToString();
				}
			}
			return string.Empty;
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000862B4 File Offset: 0x000844B4
		public static ServiceInstanceMapValue ConvertXmlToServiceInstanceMap(string xml)
		{
			if (string.IsNullOrEmpty(xml))
			{
				return null;
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ServiceInstanceMapValue));
			ServiceInstanceMapValue result;
			using (StringReader stringReader = new StringReader(xml))
			{
				try
				{
					result = (xmlSerializer.Deserialize(stringReader) as ServiceInstanceMapValue);
				}
				catch (InvalidOperationException ex)
				{
					if (ex.InnerException is XmlException)
					{
						throw new InvalidServiceInstanceMapXmlFormatException(ex.InnerException.Message, ex);
					}
					if (ex.InnerException is InvalidOperationException)
					{
						throw new InvalidServiceInstanceMapXmlFormatException(string.Format("{0} {1}", ex.InnerException.Message, ex.Message), ex);
					}
					throw;
				}
			}
			return result;
		}
	}
}
