using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Inference.GroupingModel
{
	// Token: 0x02000F60 RID: 3936
	public class GroupingModelConfiguration
	{
		// Token: 0x170023B4 RID: 9140
		// (get) Token: 0x060086BF RID: 34495 RVA: 0x0024F297 File Offset: 0x0024D497
		// (set) Token: 0x060086C0 RID: 34496 RVA: 0x0024F29F File Offset: 0x0024D49F
		[XmlElement]
		public int CurrentVersion { get; set; }

		// Token: 0x170023B5 RID: 9141
		// (get) Token: 0x060086C1 RID: 34497 RVA: 0x0024F2A8 File Offset: 0x0024D4A8
		// (set) Token: 0x060086C2 RID: 34498 RVA: 0x0024F2B0 File Offset: 0x0024D4B0
		[XmlElement]
		public int MinimumSupportedVersion { get; set; }

		// Token: 0x060086C3 RID: 34499 RVA: 0x0024F2B9 File Offset: 0x0024D4B9
		public IGroupingModelConfiguration AsReadOnly()
		{
			return new GroupingModelConfigurationWrapper(this);
		}

		// Token: 0x060086C4 RID: 34500 RVA: 0x0024F2C4 File Offset: 0x0024D4C4
		public static GroupingModelConfiguration LoadFrom(string groupingModelConfigurationDefinition)
		{
			GroupingModelConfiguration result;
			using (TextReader textReader = new StringReader(groupingModelConfigurationDefinition))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(GroupingModelConfiguration));
				result = (GroupingModelConfiguration)xmlSerializer.Deserialize(textReader);
			}
			return result;
		}

		// Token: 0x060086C5 RID: 34501 RVA: 0x0024F314 File Offset: 0x0024D514
		public static GroupingModelConfiguration LoadFromFile()
		{
			string filepath = Path.Combine(ExchangeSetupContext.BinPath, "GroupingModelConfiguration.Xml");
			return GroupingModelConfiguration.LoadFromFile(filepath);
		}

		// Token: 0x060086C6 RID: 34502 RVA: 0x0024F338 File Offset: 0x0024D538
		public static GroupingModelConfiguration LoadFromFile(string filepath)
		{
			GroupingModelConfiguration result;
			using (XmlReader xmlReader = new XmlTextReader(filepath))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(GroupingModelConfiguration));
				result = (GroupingModelConfiguration)xmlSerializer.Deserialize(xmlReader);
			}
			return result;
		}

		// Token: 0x060086C7 RID: 34503 RVA: 0x0024F388 File Offset: 0x0024D588
		internal static string WriteTo(GroupingModelConfiguration groupingModelConfiguration)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (TextWriter textWriter = new StringWriter(stringBuilder))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(GroupingModelConfiguration));
				xmlSerializer.Serialize(textWriter, groupingModelConfiguration);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04005A1E RID: 23070
		public const string FileName = "GroupingModelConfiguration.Xml";
	}
}
