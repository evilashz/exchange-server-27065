using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000218 RID: 536
	public class ClientCertificateCriteria
	{
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x000312C6 File Offset: 0x0002F4C6
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x000312CE File Offset: 0x0002F4CE
		public StoreLocation StoreLocation { get; internal set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x000312D7 File Offset: 0x0002F4D7
		// (set) Token: 0x06001102 RID: 4354 RVA: 0x000312DF File Offset: 0x0002F4DF
		public StoreName StoreName { get; internal set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x000312E8 File Offset: 0x0002F4E8
		// (set) Token: 0x06001104 RID: 4356 RVA: 0x000312F0 File Offset: 0x0002F4F0
		public X509FindType FindType { get; internal set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x000312F9 File Offset: 0x0002F4F9
		// (set) Token: 0x06001106 RID: 4358 RVA: 0x00031301 File Offset: 0x0002F501
		public string FindValue { get; internal set; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x0003130A File Offset: 0x0002F50A
		// (set) Token: 0x06001108 RID: 4360 RVA: 0x00031312 File Offset: 0x0002F512
		public string TransportCertificateName { get; internal set; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x0003131B File Offset: 0x0002F51B
		// (set) Token: 0x0600110A RID: 4362 RVA: 0x00031323 File Offset: 0x0002F523
		public string TransportCertificateFqdn { get; internal set; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x0003132C File Offset: 0x0002F52C
		// (set) Token: 0x0600110C RID: 4364 RVA: 0x00031334 File Offset: 0x0002F534
		internal WildcardMatchType TransportWildcardMatchType { get; set; }

		// Token: 0x0600110D RID: 4365 RVA: 0x00031340 File Offset: 0x0002F540
		public static ClientCertificateCriteria FromXml(XmlNode workContext, out bool validCertificate)
		{
			XmlElement xmlElement = workContext as XmlElement;
			ClientCertificateCriteria clientCertificateCriteria = null;
			validCertificate = false;
			if (xmlElement != null)
			{
				clientCertificateCriteria = new ClientCertificateCriteria();
				validCertificate = true;
				XmlNode xmlNode = workContext.SelectSingleNode("StoreLocation");
				if (xmlNode != null)
				{
					clientCertificateCriteria.StoreLocation = Utils.GetEnumValue<StoreLocation>(xmlNode.InnerText, "ClientCertificate StoreLocation");
				}
				xmlNode = workContext.SelectSingleNode("StoreName");
				if (xmlNode != null)
				{
					clientCertificateCriteria.StoreName = Utils.GetEnumValue<StoreName>(xmlNode.InnerText, "ClientCertificate StoreName");
				}
				xmlNode = workContext.SelectSingleNode("FindType");
				if (xmlNode != null)
				{
					clientCertificateCriteria.FindType = Utils.GetEnumValue<X509FindType>(xmlNode.InnerText, "ClientCertificate FindType");
				}
				xmlNode = workContext.SelectSingleNode("FindValue");
				if (xmlNode != null)
				{
					clientCertificateCriteria.FindValue = Utils.CheckNullOrWhiteSpace(xmlNode.InnerText, "ClientCertificate FindValue");
				}
				xmlNode = workContext.SelectSingleNode("TransportCertificateName");
				if (xmlNode != null)
				{
					clientCertificateCriteria.TransportCertificateName = Utils.CheckNullOrWhiteSpace(xmlNode.InnerText, "ClientCertificate TransportCertificateName");
				}
				xmlNode = workContext.SelectSingleNode("TransportCertificateFqdn");
				if (xmlNode != null)
				{
					clientCertificateCriteria.TransportCertificateFqdn = Utils.CheckNullOrWhiteSpace(xmlNode.InnerText, "ClientCertificate TransportCertificateFqdn");
				}
				xmlNode = workContext.SelectSingleNode("TransportWildcardMatchType");
				if (xmlNode != null)
				{
					clientCertificateCriteria.TransportWildcardMatchType = Utils.GetEnumValue<WildcardMatchType>(xmlNode.InnerText, "ClientCertificate TransportWildcardMatchType");
				}
			}
			return clientCertificateCriteria;
		}
	}
}
