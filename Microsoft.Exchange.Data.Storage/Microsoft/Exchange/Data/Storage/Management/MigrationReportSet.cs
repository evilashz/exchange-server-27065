using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A34 RID: 2612
	[Serializable]
	public class MigrationReportSet : ConfigurableObject, IXmlSerializable
	{
		// Token: 0x06005FDD RID: 24541 RVA: 0x00194D1F File Offset: 0x00192F1F
		public MigrationReportSet(DateTime creationTimeUTC, string successUrl, string errorUrl = null) : this()
		{
			this.CreationTimeUTC = creationTimeUTC;
			this.SuccessUrl = successUrl;
			this.ErrorUrl = errorUrl;
		}

		// Token: 0x06005FDE RID: 24542 RVA: 0x00194D3C File Offset: 0x00192F3C
		public MigrationReportSet() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06005FDF RID: 24543 RVA: 0x00194D4C File Offset: 0x00192F4C
		public static implicit operator MigrationReportSet(string xml)
		{
			return (MigrationReportSet)MigrationXmlSerializer.Deserialize(xml, typeof(MigrationReportSet));
		}

		// Token: 0x17001A5F RID: 6751
		// (get) Token: 0x06005FE0 RID: 24544 RVA: 0x00194D70 File Offset: 0x00192F70
		// (set) Token: 0x06005FE1 RID: 24545 RVA: 0x00194D82 File Offset: 0x00192F82
		public DateTime CreationTimeUTC
		{
			get
			{
				return (DateTime)this[MigrationReportSet.MigrationReportSetSchema.CreationTimeUTC];
			}
			private set
			{
				this[MigrationReportSet.MigrationReportSetSchema.CreationTimeUTC] = value;
			}
		}

		// Token: 0x17001A60 RID: 6752
		// (get) Token: 0x06005FE2 RID: 24546 RVA: 0x00194D95 File Offset: 0x00192F95
		// (set) Token: 0x06005FE3 RID: 24547 RVA: 0x00194DA7 File Offset: 0x00192FA7
		public string SuccessUrl
		{
			get
			{
				return (string)this[MigrationReportSet.MigrationReportSetSchema.SuccessUrl];
			}
			private set
			{
				this[MigrationReportSet.MigrationReportSetSchema.SuccessUrl] = value;
			}
		}

		// Token: 0x17001A61 RID: 6753
		// (get) Token: 0x06005FE4 RID: 24548 RVA: 0x00194DB5 File Offset: 0x00192FB5
		// (set) Token: 0x06005FE5 RID: 24549 RVA: 0x00194DC7 File Offset: 0x00192FC7
		public string ErrorUrl
		{
			get
			{
				return (string)this[MigrationReportSet.MigrationReportSetSchema.ErrorUrl];
			}
			private set
			{
				this[MigrationReportSet.MigrationReportSetSchema.ErrorUrl] = value;
			}
		}

		// Token: 0x17001A62 RID: 6754
		// (get) Token: 0x06005FE6 RID: 24550 RVA: 0x00194DD5 File Offset: 0x00192FD5
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001A63 RID: 6755
		// (get) Token: 0x06005FE7 RID: 24551 RVA: 0x00194DDC File Offset: 0x00192FDC
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MigrationReportSet.schema;
			}
		}

		// Token: 0x06005FE8 RID: 24552 RVA: 0x00194DE3 File Offset: 0x00192FE3
		public static bool TryCreate(XmlReader reader, out MigrationReportSet report)
		{
			report = null;
			if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "MigrationReportSet")
			{
				report = new MigrationReportSet();
				report.Initialize(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06005FE9 RID: 24553 RVA: 0x00194E15 File Offset: 0x00193015
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x06005FEA RID: 24554 RVA: 0x00194E18 File Offset: 0x00193018
		public void ReadXml(XmlReader reader)
		{
			if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "MigrationReportSet")
			{
				this.Initialize(reader);
			}
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x00194E3C File Offset: 0x0019303C
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("MigrationReportSet");
			writer.WriteAttributeString(MigrationReportSet.MigrationReportSetSchema.SuccessUrl.Name, this.SuccessUrl);
			writer.WriteAttributeString(MigrationReportSet.MigrationReportSetSchema.ErrorUrl.Name, this.ErrorUrl);
			writer.WriteAttributeString(MigrationReportSet.MigrationReportSetSchema.CreationTimeUTC.Name, this.CreationTimeUTC.Ticks.ToString());
			writer.WriteEndElement();
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x00194EAC File Offset: 0x001930AC
		public override string ToString()
		{
			return Strings.MigrationReportSetString(this.CreationTimeUTC.ToString(), this.SuccessUrl, this.ErrorUrl);
		}

		// Token: 0x06005FED RID: 24557 RVA: 0x00194EE4 File Offset: 0x001930E4
		private void Initialize(XmlReader reader)
		{
			this.SuccessUrl = reader[MigrationReportSet.MigrationReportSetSchema.SuccessUrl.Name];
			this.ErrorUrl = reader[MigrationReportSet.MigrationReportSetSchema.ErrorUrl.Name];
			string text = reader[MigrationReportSet.MigrationReportSetSchema.CreationTimeUTC.Name];
			try
			{
				long ticks = long.Parse(text);
				this.CreationTimeUTC = new DateTime(ticks);
			}
			catch (ArgumentException innerException)
			{
				throw new MigrationDataCorruptionException("cannot parse xml date:" + text, innerException);
			}
			catch (FormatException innerException2)
			{
				throw new MigrationDataCorruptionException("cannot parse xml date:" + text, innerException2);
			}
			catch (OverflowException innerException3)
			{
				throw new MigrationDataCorruptionException("cannot parse xml date:" + text, innerException3);
			}
		}

		// Token: 0x0400364A RID: 13898
		public const string RootSerializedTag = "MigrationReportSet";

		// Token: 0x0400364B RID: 13899
		private static ObjectSchema schema = ObjectSchema.GetInstance<MigrationReportSet.MigrationReportSetSchema>();

		// Token: 0x02000A35 RID: 2613
		private class MigrationReportSetSchema : SimpleProviderObjectSchema
		{
			// Token: 0x0400364C RID: 13900
			public static readonly SimpleProviderPropertyDefinition CreationTimeUTC = new SimpleProviderPropertyDefinition("CreationTimeUTC", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.TaskPopulated, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400364D RID: 13901
			public static readonly SimpleProviderPropertyDefinition ErrorUrl = new SimpleProviderPropertyDefinition("ErrorUrl", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400364E RID: 13902
			public static readonly SimpleProviderPropertyDefinition SuccessUrl = new SimpleProviderPropertyDefinition("SuccessUrl", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
