using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Search.Platform.Parallax.DataLoad;
using Microsoft.Search.Platform.Parallax.Util.IniFormat;
using Microsoft.Search.Platform.Parallax.Util.IniFormat.FileModel;

namespace Microsoft.Exchange.VariantConfiguration.DataLoad
{
	// Token: 0x02000009 RID: 9
	internal class FlightReader : IFlightReader
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000275C File Offset: 0x0000095C
		internal FlightReader(IDataSourceReader dataSourceReader, IDataTransformation transformation, IEnumerable<string> inputDataSources)
		{
			if (dataSourceReader == null)
			{
				throw new ArgumentNullException("dataSourceReader");
			}
			if (transformation == null)
			{
				throw new ArgumentNullException("transformation");
			}
			if (inputDataSources == null)
			{
				throw new ArgumentNullException("inputDataSources");
			}
			this.dataSourceReader = dataSourceReader;
			this.transformation = transformation;
			this.flightMap = FlightReader.CreateFlightMap(this.dataSourceReader, this.transformation, inputDataSources);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027BF File Offset: 0x000009BF
		public string GetFlightContent(string flightName)
		{
			return FlightReader.GetFlightContent(flightName, this.dataSourceReader, this.transformation, this.flightMap);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002804 File Offset: 0x00000A04
		internal static Dictionary<string, string> CreateFlightMap(IDataSourceReader dataSourceReader, IDataTransformation transformation, IEnumerable<string> inputDataSources)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			foreach (string text in inputDataSources)
			{
				IniFileModel iniFileModel;
				if (FlightReader.TryCreateFileModel(text, dataSourceReader, transformation, out iniFileModel))
				{
					foreach (Section section in iniFileModel.Sections.Values)
					{
						if (section.Parameters.Any((ParameterAssignmentRule param) => string.Equals(param.ParameterName, "_meta.type", StringComparison.OrdinalIgnoreCase) && string.Equals(param.Value, "Microsoft.Exchange.Flighting.IFlight", StringComparison.OrdinalIgnoreCase)))
						{
							dictionary[section.Name] = text;
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000028E0 File Offset: 0x00000AE0
		internal static string GetFlightContent(string flightName, IDataSourceReader dataSourceReader, IDataTransformation transformation, Dictionary<string, string> flightMap)
		{
			if (string.IsNullOrWhiteSpace(flightName))
			{
				throw new ArgumentNullException("dataSource");
			}
			if (flightMap.ContainsKey(flightName))
			{
				string dataSourceName = flightMap[flightName];
				IniFileModel iniFileModel;
				if (FlightReader.TryCreateFileModel(dataSourceName, dataSourceReader, transformation, out iniFileModel) && iniFileModel.Sections.ContainsKey(flightName))
				{
					IniFileModel iniFileModel2 = new IniFileModel();
					iniFileModel2.AddSection(iniFileModel.Sections[flightName]);
					return iniFileModel2.Serialize();
				}
			}
			return string.Join(Environment.NewLine, new string[]
			{
				string.Format("[{0}]", flightName),
				string.Format("{0}={1}", "_meta.type", "Microsoft.Exchange.Flighting.IFlight")
			});
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002984 File Offset: 0x00000B84
		private static bool TryCreateFileModel(string dataSourceName, IDataSourceReader dataSourceReader, IDataTransformation transformation, out IniFileModel fileModel)
		{
			if (dataSourceReader.CanGetContentReader(dataSourceName))
			{
				using (TextReader textReader = dataSourceReader.GetContentReader(dataSourceName)())
				{
					try
					{
						string text = transformation.Transform(dataSourceName, textReader.ReadToEnd());
						fileModel = IniFileModel.CreateFromString(dataSourceName, text);
						return true;
					}
					catch (IniParseException)
					{
					}
				}
			}
			fileModel = null;
			return false;
		}

		// Token: 0x0400000D RID: 13
		private const string IFlightTypeName = "Microsoft.Exchange.Flighting.IFlight";

		// Token: 0x0400000E RID: 14
		private const string TypeParameterName = "_meta.type";

		// Token: 0x0400000F RID: 15
		private readonly IDataSourceReader dataSourceReader;

		// Token: 0x04000010 RID: 16
		private readonly IDataTransformation transformation;

		// Token: 0x04000011 RID: 17
		private readonly Dictionary<string, string> flightMap;
	}
}
