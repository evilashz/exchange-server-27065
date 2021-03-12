using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001C2 RID: 450
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class AggregatedUserConfigurationPart : IReadableUserConfiguration, IDisposable
	{
		// Token: 0x0600183F RID: 6207 RVA: 0x00076A01 File Offset: 0x00074C01
		private AggregatedUserConfigurationPart(AggregatedUserConfigurationPart.MementoClass memento)
		{
			this.memento = memento;
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00076A10 File Offset: 0x00074C10
		private AggregatedUserConfigurationPart(IUserConfiguration config)
		{
			this.memento = new AggregatedUserConfigurationPart.MementoClass
			{
				ConfigurationName = config.ConfigurationName,
				DataTypes = config.DataTypes,
				FolderId = config.FolderId,
				Id = config.Id,
				VersionedId = config.VersionedId,
				LastModifiedTime = config.LastModifiedTime.ToBinary()
			};
			if ((config.DataTypes & UserConfigurationTypes.Dictionary) != (UserConfigurationTypes)0)
			{
				using (StringWriter stringWriter = new StringWriter())
				{
					using (XmlWriter xmlWriter = AggregatedUserConfigurationPart.InternalGetXmlWriter(stringWriter))
					{
						config.GetConfigurationDictionary().WriteXml(xmlWriter);
						xmlWriter.Flush();
						this.memento.DictionaryXmlString = stringWriter.ToString();
					}
				}
			}
			if ((config.DataTypes & UserConfigurationTypes.XML) != (UserConfigurationTypes)0)
			{
				using (Stream xmlStream = config.GetXmlStream())
				{
					using (StreamReader streamReader = new StreamReader(xmlStream))
					{
						this.memento.XmlString = streamReader.ReadToEnd();
					}
				}
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x00076B50 File Offset: 0x00074D50
		public AggregatedUserConfigurationPart.MementoClass Memento
		{
			get
			{
				return this.memento;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x00076B58 File Offset: 0x00074D58
		public string ConfigurationName
		{
			get
			{
				return this.Memento.ConfigurationName;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x00076B65 File Offset: 0x00074D65
		public UserConfigurationTypes DataTypes
		{
			get
			{
				return this.Memento.DataTypes;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00076B72 File Offset: 0x00074D72
		public StoreObjectId FolderId
		{
			get
			{
				return this.Memento.FolderId;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x00076B7F File Offset: 0x00074D7F
		public StoreObjectId Id
		{
			get
			{
				return this.Memento.Id;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00076B8C File Offset: 0x00074D8C
		public VersionedId VersionedId
		{
			get
			{
				return this.Memento.VersionedId;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x00076B99 File Offset: 0x00074D99
		public ExDateTime LastModifiedTime
		{
			get
			{
				return ExDateTime.FromBinary(this.Memento.LastModifiedTime);
			}
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00076BAB File Offset: 0x00074DAB
		public static AggregatedUserConfigurationPart FromMemento(AggregatedUserConfigurationPart.MementoClass memento)
		{
			return new AggregatedUserConfigurationPart(memento);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00076BB3 File Offset: 0x00074DB3
		public static AggregatedUserConfigurationPart FromConfiguration(IUserConfiguration configuration)
		{
			return new AggregatedUserConfigurationPart(configuration);
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00076BBB File Offset: 0x00074DBB
		public void Dispose()
		{
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00076BC0 File Offset: 0x00074DC0
		public IDictionary GetDictionary()
		{
			IDictionary result;
			using (StringReader stringReader = new StringReader(this.Memento.DictionaryXmlString))
			{
				using (XmlReader xmlReader = AggregatedUserConfigurationPart.InternalGetXmlReader(stringReader))
				{
					ConfigurationDictionary configurationDictionary = new ConfigurationDictionary();
					configurationDictionary.ReadXml(xmlReader);
					result = configurationDictionary;
				}
			}
			return result;
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00076C28 File Offset: 0x00074E28
		public Stream GetXmlStream()
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(this.Memento.XmlString);
			streamWriter.Flush();
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return memoryStream;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00076C64 File Offset: 0x00074E64
		private static XmlWriter InternalGetXmlWriter(TextWriter writer)
		{
			return new XmlTextWriter(writer);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00076C6C File Offset: 0x00074E6C
		private static XmlReader InternalGetXmlReader(TextReader reader)
		{
			return SafeXmlFactory.CreateSafeXmlTextReader(reader);
		}

		// Token: 0x04000CBD RID: 3261
		private readonly AggregatedUserConfigurationPart.MementoClass memento;

		// Token: 0x020001C3 RID: 451
		[DataContract]
		public class MementoClass
		{
			// Token: 0x170007A0 RID: 1952
			// (get) Token: 0x0600184F RID: 6223 RVA: 0x00076C74 File Offset: 0x00074E74
			// (set) Token: 0x06001850 RID: 6224 RVA: 0x00076C7C File Offset: 0x00074E7C
			[DataMember]
			public string ConfigurationName { get; set; }

			// Token: 0x170007A1 RID: 1953
			// (get) Token: 0x06001851 RID: 6225 RVA: 0x00076C85 File Offset: 0x00074E85
			// (set) Token: 0x06001852 RID: 6226 RVA: 0x00076C8D File Offset: 0x00074E8D
			[DataMember]
			public UserConfigurationTypes DataTypes { get; set; }

			// Token: 0x170007A2 RID: 1954
			// (get) Token: 0x06001853 RID: 6227 RVA: 0x00076C96 File Offset: 0x00074E96
			// (set) Token: 0x06001854 RID: 6228 RVA: 0x00076C9E File Offset: 0x00074E9E
			[DataMember]
			public StoreObjectId FolderId { get; set; }

			// Token: 0x170007A3 RID: 1955
			// (get) Token: 0x06001855 RID: 6229 RVA: 0x00076CA7 File Offset: 0x00074EA7
			// (set) Token: 0x06001856 RID: 6230 RVA: 0x00076CAF File Offset: 0x00074EAF
			[DataMember]
			public StoreObjectId Id { get; set; }

			// Token: 0x170007A4 RID: 1956
			// (get) Token: 0x06001857 RID: 6231 RVA: 0x00076CB8 File Offset: 0x00074EB8
			// (set) Token: 0x06001858 RID: 6232 RVA: 0x00076CC0 File Offset: 0x00074EC0
			[DataMember]
			public VersionedId VersionedId { get; set; }

			// Token: 0x170007A5 RID: 1957
			// (get) Token: 0x06001859 RID: 6233 RVA: 0x00076CC9 File Offset: 0x00074EC9
			// (set) Token: 0x0600185A RID: 6234 RVA: 0x00076CD1 File Offset: 0x00074ED1
			[DataMember]
			public long LastModifiedTime { get; set; }

			// Token: 0x170007A6 RID: 1958
			// (get) Token: 0x0600185B RID: 6235 RVA: 0x00076CDA File Offset: 0x00074EDA
			// (set) Token: 0x0600185C RID: 6236 RVA: 0x00076CE2 File Offset: 0x00074EE2
			[DataMember]
			public string DictionaryXmlString { get; set; }

			// Token: 0x170007A7 RID: 1959
			// (get) Token: 0x0600185D RID: 6237 RVA: 0x00076CEB File Offset: 0x00074EEB
			// (set) Token: 0x0600185E RID: 6238 RVA: 0x00076CF3 File Offset: 0x00074EF3
			[DataMember]
			public string XmlString { get; set; }
		}
	}
}
