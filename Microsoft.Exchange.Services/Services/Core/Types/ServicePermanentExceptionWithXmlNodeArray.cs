using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200075A RID: 1882
	internal abstract class ServicePermanentExceptionWithXmlNodeArray : ServicePermanentException, IProvideXmlNodeArray
	{
		// Token: 0x06003842 RID: 14402 RVA: 0x000C71EF File Offset: 0x000C53EF
		public ServicePermanentExceptionWithXmlNodeArray(Enum messageId) : base(messageId)
		{
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x000C7203 File Offset: 0x000C5403
		public ServicePermanentExceptionWithXmlNodeArray(Enum messageId, Exception innerException) : base(messageId, innerException)
		{
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x000C7218 File Offset: 0x000C5418
		protected XmlElement SerializeObjectToXml(object serializationObject, XmlSerializer serializer)
		{
			XmlElement documentElement;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream))
				{
					serializer.Serialize(xmlWriter, serializationObject);
				}
				memoryStream.Seek(0L, SeekOrigin.Begin);
				using (XmlReader xmlReader = SafeXmlFactory.CreateSafeXmlReader(memoryStream))
				{
					SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
					safeXmlDocument.Load(xmlReader);
					documentElement = safeXmlDocument.DocumentElement;
				}
			}
			return documentElement;
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06003845 RID: 14405 RVA: 0x000C72AC File Offset: 0x000C54AC
		XmlNodeArray IProvideXmlNodeArray.NodeArray
		{
			get
			{
				return this.xmlNodeArray;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06003846 RID: 14406 RVA: 0x000C72B4 File Offset: 0x000C54B4
		internal XmlNodeArray NodeArray
		{
			get
			{
				return this.xmlNodeArray;
			}
		}

		// Token: 0x04001F3E RID: 7998
		private XmlNodeArray xmlNodeArray = new XmlNodeArray();
	}
}
