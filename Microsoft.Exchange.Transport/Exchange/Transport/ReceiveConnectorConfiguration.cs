using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002E5 RID: 741
	internal class ReceiveConnectorConfiguration : IDiagnosable
	{
		// Token: 0x06002107 RID: 8455 RVA: 0x0007D20B File Offset: 0x0007B40B
		protected ReceiveConnectorConfiguration(List<ReceiveConnector> connectors)
		{
			if (connectors == null)
			{
				throw new ArgumentNullException("connectors");
			}
			this.connectors = connectors;
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x0007D228 File Offset: 0x0007B428
		public List<ReceiveConnector> Connectors
		{
			get
			{
				return this.connectors;
			}
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x0007D230 File Offset: 0x0007B430
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "ReceiveConnectors";
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x0007D24C File Offset: 0x0007B44C
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement("ReceiveConnectors", new XElement("count", this.connectors.Count));
			foreach (ReceiveConnector receiveConnector in this.connectors)
			{
				XElement xelement2 = new XElement("ReceiveConnector", new XElement("id", receiveConnector.propertyBag[ADObjectSchema.Id]));
				xelement.Add(xelement2);
				ProviderPropertyDefinition[] array = new ProviderPropertyDefinition[receiveConnector.propertyBag.Keys.Count];
				receiveConnector.propertyBag.Keys.CopyTo(array, 0);
				Array.Sort<ProviderPropertyDefinition>(array, (ProviderPropertyDefinition a, ProviderPropertyDefinition b) => a.Name.CompareTo(b.Name));
				foreach (ProviderPropertyDefinition providerPropertyDefinition in array)
				{
					object obj = receiveConnector.propertyBag[providerPropertyDefinition];
					XElement xelement3;
					if (obj != null && obj is MultiValuedPropertyBase)
					{
						xelement3 = new XElement(providerPropertyDefinition.Name);
						XElement xelement4 = new XElement("Values");
						xelement3.Add(xelement4);
						int num = 0;
						foreach (object content in ((IEnumerable)obj))
						{
							xelement4.Add(new XElement("value", content));
							num++;
						}
						xelement3.AddFirst(new XElement("count", num));
					}
					else
					{
						string expandedName = (providerPropertyDefinition.Name.Length > 1) ? (char.ToLower(providerPropertyDefinition.Name[0]) + providerPropertyDefinition.Name.Substring(1)) : providerPropertyDefinition.Name.ToLowerInvariant();
						string text = (obj == null) ? null : obj.ToString();
						if (string.IsNullOrEmpty(text))
						{
							text = null;
						}
						xelement3 = new XElement(expandedName, text);
					}
					xelement2.Add(xelement3);
				}
			}
			return xelement;
		}

		// Token: 0x0400114A RID: 4426
		protected readonly List<ReceiveConnector> connectors;

		// Token: 0x020002E6 RID: 742
		public class Builder : ConfigurationLoader<ReceiveConnectorConfiguration, ReceiveConnectorConfiguration.Builder>.SimpleBuilder<ReceiveConnector>
		{
			// Token: 0x17000A81 RID: 2689
			// (set) Token: 0x0600210C RID: 8460 RVA: 0x0007D4DC File Offset: 0x0007B6DC
			public TransportServerConfiguration Server
			{
				set
				{
					base.RootId = value.TransportServer.Id;
				}
			}

			// Token: 0x0600210D RID: 8461 RVA: 0x0007D4EF File Offset: 0x0007B6EF
			protected override ReceiveConnectorConfiguration BuildCache(List<ReceiveConnector> connectors)
			{
				return new ReceiveConnectorConfiguration(connectors);
			}

			// Token: 0x0600210E RID: 8462 RVA: 0x0007D4F7 File Offset: 0x0007B6F7
			protected override ADOperationResult TryRegisterChangeNotification<TConfigObject>(Func<ADObjectId> rootIdGetter, out ADNotificationRequestCookie cookie)
			{
				return TransportADNotificationAdapter.TryRegisterNotifications(rootIdGetter, new ADNotificationCallback(base.Reload), new TransportADNotificationAdapter.TransportADNotificationRegister(TransportADNotificationAdapter.Instance.RegisterForLocalServerReceiveConnectorNotifications), 3, out cookie);
			}
		}
	}
}
