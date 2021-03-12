using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.VariantConfiguration.DataLoad;
using Microsoft.Exchange.VariantConfiguration.Parser;
using Microsoft.Search.Platform.Parallax.DataLoad;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200000E RID: 14
	internal class FlightDependencyTransformation : IDataTransformation
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00003345 File Offset: 0x00001545
		internal FlightDependencyTransformation(IFlightReader flightReader)
		{
			if (flightReader == null)
			{
				throw new ArgumentNullException("flightReader");
			}
			this.flightReader = flightReader;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003364 File Offset: 0x00001564
		public string Transform(string dataSourceName, string input)
		{
			if (!dataSourceName.EndsWith(".settings.ini", StringComparison.OrdinalIgnoreCase))
			{
				return input;
			}
			IEnumerable<string> flightDependencies = FlightDependencyTransformation.GetFlightDependencies(input, dataSourceName);
			return this.AppendFlightDefinitions(input, flightDependencies);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003394 File Offset: 0x00001594
		internal static IEnumerable<string> GetFlightDependencies(string input, string dataSourceName)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			ConfigurationComponent configurationComponent;
			if (ConfigurationComponent.TryCreate(input, dataSourceName, out configurationComponent))
			{
				foreach (ConfigurationSection configurationSection in configurationComponent.Sections)
				{
					IEnumerable<string> flightDependencies = configurationSection.GetFlightDependencies();
					foreach (string item in flightDependencies)
					{
						hashSet.Add(item);
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000343C File Offset: 0x0000163C
		internal static string AddPrefixToFlightSectionName(string flightName, string flightContent)
		{
			flightName = Regex.Escape(flightName);
			return Regex.Replace(flightContent, string.Format("^\\[{0}\\]", flightName), string.Format("[{0}{1}]", "flt.", flightName), RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000346C File Offset: 0x0000166C
		internal string AppendFlightDefinitions(string input, IEnumerable<string> flightDependencies)
		{
			StringBuilder stringBuilder = new StringBuilder(input);
			stringBuilder.AppendLine();
			foreach (string flightName in flightDependencies)
			{
				stringBuilder.AppendLine(FlightDependencyTransformation.AddPrefixToFlightSectionName(flightName, this.flightReader.GetFlightContent(flightName)));
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000027 RID: 39
		private const string FlightPrefix = "flt.";

		// Token: 0x04000028 RID: 40
		private const string SettingsFileSuffix = ".settings.ini";

		// Token: 0x04000029 RID: 41
		private readonly IFlightReader flightReader;
	}
}
