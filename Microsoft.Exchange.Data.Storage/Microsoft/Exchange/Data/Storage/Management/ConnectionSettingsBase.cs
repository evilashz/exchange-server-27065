using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A14 RID: 2580
	[Serializable]
	public abstract class ConnectionSettingsBase : ConfigurableObject, IXmlSerializable
	{
		// Token: 0x06005ECC RID: 24268 RVA: 0x00190680 File Offset: 0x0018E880
		public ConnectionSettingsBase() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x17001A07 RID: 6663
		// (get) Token: 0x06005ECD RID: 24269
		public abstract MigrationType Type { get; }

		// Token: 0x17001A08 RID: 6664
		// (get) Token: 0x06005ECE RID: 24270 RVA: 0x0019068D File Offset: 0x0018E88D
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x06005ECF RID: 24271 RVA: 0x00190694 File Offset: 0x0018E894
		public static implicit operator ConnectionSettingsBase(string xml)
		{
			Dictionary<string, Func<string, object>> dictionary = new Dictionary<string, Func<string, object>>
			{
				{
					typeof(ExchangeConnectionSettings).Name,
					new Func<string, object>(MigrationXmlSerializer.Deserialize<ExchangeConnectionSettings>)
				},
				{
					typeof(IMAPConnectionSettings).Name,
					new Func<string, object>(MigrationXmlSerializer.Deserialize<IMAPConnectionSettings>)
				}
			};
			ConnectionSettingsBase result;
			using (StringReader stringReader = new StringReader(xml))
			{
				using (XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(stringReader))
				{
					xmlTextReader.MoveToContent();
					Func<string, object> func;
					if (!dictionary.TryGetValue(xmlTextReader.LocalName, out func))
					{
						throw new UnknownConnectionSettingsTypeException(xmlTextReader.LocalName);
					}
					result = (ConnectionSettingsBase)func(xml);
				}
			}
			return result;
		}

		// Token: 0x06005ED0 RID: 24272
		public abstract ConnectionSettingsBase CloneForPresentation();

		// Token: 0x06005ED1 RID: 24273 RVA: 0x00190764 File Offset: 0x0018E964
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x06005ED2 RID: 24274 RVA: 0x00190767 File Offset: 0x0018E967
		public override string ToString()
		{
			return MigrationXmlSerializer.Serialize(this);
		}

		// Token: 0x06005ED3 RID: 24275
		public abstract void ReadXml(XmlReader reader);

		// Token: 0x06005ED4 RID: 24276
		public abstract void WriteXml(XmlWriter writer);
	}
}
