using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200007C RID: 124
	public class ConfigurationDocument
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000091AB File Offset: 0x000073AB
		public static int TraceTypesCount
		{
			get
			{
				return Enum.GetValues(typeof(TraceType)).Length / 2;
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000091C4 File Offset: 0x000073C4
		public static ConfigurationDocument LoadFromFile(string configFileName)
		{
			ConfigurationDocument configurationDocument = new ConfigurationDocument();
			return ConfigurationDocument.LoadFromFile(configFileName, configurationDocument, new ConfigurationDocument.LineProcessor(configurationDocument.ProcessLine));
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000091EC File Offset: 0x000073EC
		public static ConfigurationDocument LoadFaultInjectionFromFile(string configFileName)
		{
			ConfigurationDocument configurationDocument = new ConfigurationDocument();
			return ConfigurationDocument.LoadFromFile(configFileName, configurationDocument, new ConfigurationDocument.LineProcessor(configurationDocument.ProcessFaultInjectionLine));
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00009214 File Offset: 0x00007414
		public void GetEnabledTypes(BitArray destArray, bool addToExisting)
		{
			for (int i = 0; i < this.enabledTraceTypes.Length; i++)
			{
				if (addToExisting)
				{
					if (this.enabledTraceTypes[i])
					{
						destArray[i] = true;
					}
				}
				else
				{
					destArray[i] = this.enabledTraceTypes[i];
				}
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00009265 File Offset: 0x00007465
		public List<TraceComponentInfo> EnabledComponentsList
		{
			get
			{
				return this.enabledComponentsList;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000926D File Offset: 0x0000746D
		internal List<TraceComponentInfo> BypassFilterEnabledComponentsList
		{
			get
			{
				return this.bypassFilterComponentsList;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00009275 File Offset: 0x00007475
		internal Dictionary<string, List<string>> CustomParameters
		{
			get
			{
				return this.customParameters;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000927D File Offset: 0x0000747D
		internal FaultInjectionConfig FaultInjectionConfig
		{
			get
			{
				return this.faultInjectionConfig;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00009285 File Offset: 0x00007485
		internal bool FilteredTracingEnabled
		{
			get
			{
				return this.filteredTracingEnabled;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000928D File Offset: 0x0000748D
		internal bool InMemoryTracingEnabled
		{
			get
			{
				return this.inMemoryTracingEnabled;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00009295 File Offset: 0x00007495
		internal bool ConsoleTracingEnabled
		{
			get
			{
				return this.consoleTracingEnabled;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000929D File Offset: 0x0000749D
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x000092A5 File Offset: 0x000074A5
		internal uint FileContentHash { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002BA RID: 698 RVA: 0x000092AE File Offset: 0x000074AE
		internal bool SystemDiagnosticsTracingEnabled
		{
			get
			{
				return this.systemDiagnosticsTracingEnabled;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000092B8 File Offset: 0x000074B8
		private static ConfigurationDocument LoadFromFile(string path, ConfigurationDocument configDoc, ConfigurationDocument.LineProcessor processLine)
		{
			try
			{
				if (File.Exists(path))
				{
					configDoc.LoadLines(path, processLine);
				}
				else
				{
					InternalBypassTrace.TracingConfigurationTracer.TraceDebug(0, 0L, "File {0} does not exist", new object[]
					{
						path
					});
				}
			}
			catch (IOException)
			{
				configDoc = new ConfigurationDocument();
				InternalBypassTrace.TracingConfigurationTracer.TraceError(0, 0L, "Clearing trace settings due to IOException opening file {0}", new object[]
				{
					path
				});
			}
			catch (UnauthorizedAccessException)
			{
				InternalBypassTrace.TracingConfigurationTracer.TraceError(0, 0L, "Trace settings unchanged due to UnauthorizedAccessException opening file {0}", new object[]
				{
					path
				});
			}
			return configDoc;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00009360 File Offset: 0x00007560
		protected void ProcessLine(string line)
		{
			string text = null;
			TraceComponentInfo traceComponentInfo = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			InternalBypassTrace.TracingConfigurationTracer.TraceDebug(34351, 0L, "Processing line: {0}", new object[]
			{
				line
			});
			for (;;)
			{
				bool flag7 = false;
				string nextLexem = this.GetNextLexem(ref line, ref flag7);
				if (flag7)
				{
					break;
				}
				if (string.IsNullOrEmpty(nextLexem))
				{
					goto IL_1C4;
				}
				if (nextLexem == ",")
				{
					goto Block_3;
				}
				if (nextLexem == ":")
				{
					goto Block_4;
				}
				if (string.Equals(nextLexem, "bypassFilter", StringComparison.OrdinalIgnoreCase))
				{
					flag4 = true;
				}
				else
				{
					text = nextLexem;
				}
			}
			this.ReportError("Low level scanning error in the statement header.", new object[0]);
			goto IL_1C4;
			Block_3:
			this.ReportError("Bogus comma in the statement header.", new object[0]);
			goto IL_1C4;
			Block_4:
			if (string.IsNullOrEmpty(text))
			{
				this.ReportError("Colon is not preceded with component name.", new object[0]);
			}
			else if (text.Equals("TraceTypes", StringComparison.OrdinalIgnoreCase) || text.Equals("TraceLevels", StringComparison.OrdinalIgnoreCase))
			{
				flag = true;
			}
			else if (text.Equals("FilteredTracing", StringComparison.OrdinalIgnoreCase))
			{
				flag2 = true;
			}
			else if (text.Equals("InMemoryTracing", StringComparison.OrdinalIgnoreCase))
			{
				flag3 = true;
			}
			else if (text.Equals("ConsoleTracing", StringComparison.OrdinalIgnoreCase))
			{
				flag5 = true;
			}
			else if (text.Equals("SystemDiagnosticsTracing", StringComparison.OrdinalIgnoreCase))
			{
				flag6 = true;
			}
			else
			{
				traceComponentInfo = this.CreateComponentIfNeccessary(text, flag4);
				if (traceComponentInfo == null)
				{
					List<string> list = null;
					if (!this.customParameters.TryGetValue(text, out list))
					{
						list = new List<string>();
						this.customParameters[text] = list;
					}
					InternalBypassTrace.TracingConfigurationTracer.TraceDebug(0, 0L, "Added line as a custom parameter name,value = {0},{1}", new object[]
					{
						text,
						line
					});
					list.Add(line.Trim());
				}
			}
			IL_1C4:
			if (flag)
			{
				BitArray bitArray = new BitArray(ConfigurationDocument.TraceTypesCount + 1);
				if (this.ReadTraceTypes(line, ref bitArray))
				{
					this.enabledTraceTypes = bitArray;
					return;
				}
			}
			else if (flag2)
			{
				if (line.Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase))
				{
					this.filteredTracingEnabled = true;
					return;
				}
			}
			else if (flag3)
			{
				if (line.Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase))
				{
					this.inMemoryTracingEnabled = true;
					return;
				}
			}
			else if (flag5)
			{
				if (line.Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase))
				{
					this.consoleTracingEnabled = this.GetConsoleTracingEnabled();
					return;
				}
			}
			else if (flag6)
			{
				if (line.Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase))
				{
					this.systemDiagnosticsTracingEnabled = true;
					return;
				}
			}
			else
			{
				if (traceComponentInfo != null)
				{
					this.AddParsedComponentFromConfigFile(traceComponentInfo, line, flag4);
					return;
				}
				this.ReportError("Line could not be parsed", new object[0]);
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000095FC File Offset: 0x000077FC
		protected void ProcessFaultInjectionLine(string line)
		{
			InternalBypassTrace.FaultInjectionConfigurationTracer.TraceDebug(50735, 0L, "Processing line: {0}", new object[]
			{
				line
			});
			string text = null;
			FaultInjectionType type = FaultInjectionType.None;
			string empty = string.Empty;
			uint key = 0U;
			List<string> parameters = null;
			for (;;)
			{
				bool flag = false;
				string nextLexem = this.GetNextLexem(ref line, ref flag);
				if (flag)
				{
					break;
				}
				if (string.IsNullOrEmpty(nextLexem))
				{
					goto IL_A4;
				}
				if (nextLexem == ",")
				{
					if (!this.ReadFaultInjectionTagComponent(ref line, ref empty))
					{
						goto Block_4;
					}
				}
				else
				{
					if (nextLexem == ":")
					{
						goto IL_A4;
					}
					text = nextLexem;
				}
			}
			this.ReportError("Low level scanning error in the statement header.", new object[0]);
			return;
			Block_4:
			this.ReportError("Failed to read fault injection tag component.", new object[0]);
			return;
			IL_A4:
			if (string.IsNullOrEmpty(text))
			{
				this.ReportError("Colon is not preceded with component name.", new object[0]);
				return;
			}
			TraceComponentInfo traceComponentInfo = this.CreateComponentIfNeccessary(text, true);
			if (traceComponentInfo == null)
			{
				this.ReportError("Not a component.", new object[0]);
				return;
			}
			if (!this.ReadFaultInjectionType(ref line, ref type))
			{
				this.ReportError("Failed to read fault injection type.", new object[0]);
				return;
			}
			if (!this.ReadFaultInjectionLid(ref line, ref key))
			{
				this.ReportError("Failed to read fault injection LID.", new object[0]);
				return;
			}
			if (!this.ReadFaultInjectionParameters(ref line, ref parameters))
			{
				this.ReportError("Failed to read fault injection parameters.", new object[0]);
				return;
			}
			lock (this.FaultInjectionConfig)
			{
				FaultInjectionTagComponentConfig faultInjectionTagComponentConfig = null;
				FaultInjectionComponentConfig faultInjectionComponentConfig = null;
				if (!this.FaultInjectionConfig.TryGetValue(traceComponentInfo.ComponentGuid, out faultInjectionTagComponentConfig))
				{
					faultInjectionTagComponentConfig = new FaultInjectionTagComponentConfig();
					this.FaultInjectionConfig.Add(traceComponentInfo.ComponentGuid, faultInjectionTagComponentConfig);
				}
				if (!faultInjectionTagComponentConfig.TryGetValue(empty, out faultInjectionComponentConfig))
				{
					faultInjectionComponentConfig = new FaultInjectionComponentConfig();
					faultInjectionTagComponentConfig.Add(empty, faultInjectionComponentConfig);
				}
				faultInjectionComponentConfig[key] = new FaultInjectionPointConfig(type, parameters);
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000097D4 File Offset: 0x000079D4
		protected void LoadLines(string configFileName, ConfigurationDocument.LineProcessor processLine)
		{
			Stream stream = null;
			InternalBypassTrace.TracingConfigurationTracer.TraceDebug(0, 0L, "Trying to load trace file: {0}", new object[]
			{
				configFileName
			});
			int num = 20;
			while (num >= 0 && stream == null)
			{
				try
				{
					stream = this.GetStreamFromFile(configFileName);
					break;
				}
				catch (FileNotFoundException)
				{
					InternalBypassTrace.TracingConfigurationTracer.TraceError(0, 0L, "File does not exist", new object[0]);
					throw;
				}
				catch (UnauthorizedAccessException)
				{
					InternalBypassTrace.TracingConfigurationTracer.TraceError(0, 0L, "Failed to load file, UnauthorizedAccessException", new object[0]);
					if (num == 0)
					{
						throw;
					}
				}
				catch (SecurityException)
				{
					InternalBypassTrace.TracingConfigurationTracer.TraceError(0, 0L, "Failed to load file, SecurityException", new object[0]);
					if (num == 0)
					{
						throw;
					}
				}
				catch (IOException)
				{
					InternalBypassTrace.TracingConfigurationTracer.TraceError(0, 0L, "Failed to load file, IOException", new object[0]);
					if (num == 0)
					{
						throw;
					}
				}
				InternalBypassTrace.TracingConfigurationTracer.TraceError(0, 0L, "Retrying file load in 500 ms", new object[0]);
				Thread.Sleep(500);
				num--;
			}
			byte[] array;
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				array = binaryReader.ReadBytes((int)stream.Length);
			}
			this.FileContentHash = TraceConfigSync.ComputeContentHash(array);
			using (TextReader textReader = new StringReader(ConfigurationDocument.Encoding.GetString(array, 0, array.Length)))
			{
				string processedLine;
				while ((processedLine = textReader.ReadLine()) != null)
				{
					this.currentLine++;
					processLine(processedLine);
				}
			}
			InternalBypassTrace.TracingConfigurationTracer.TraceDebug(0, 0L, "Done reading config file: {0}, {1} lines were processed", new object[]
			{
				configFileName,
				this.currentLine
			});
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000099B8 File Offset: 0x00007BB8
		private void AddParsedComponentFromConfigFile(TraceComponentInfo componentInfo, string currentLine, bool bypassFilterModifierSet)
		{
			Dictionary<int, TraceTagInfo> dictionary = new Dictionary<int, TraceTagInfo>();
			List<TraceComponentInfo> list = bypassFilterModifierSet ? this.bypassFilterComponentsList : this.enabledComponentsList;
			Dictionary<string, TraceComponentInfo> dictionary2 = bypassFilterModifierSet ? this.bypassFilterEnabledComponentsIndex : this.enabledComponentsIndex;
			if (this.ReadComponentTags(currentLine, componentInfo.PrettyName, dictionary))
			{
				if (!dictionary2.ContainsKey(componentInfo.PrettyName))
				{
					list.Add(componentInfo);
					dictionary2.Add(componentInfo.PrettyName, componentInfo);
				}
				componentInfo.TagInfoList = new TraceTagInfo[dictionary.Count];
				dictionary.Values.CopyTo(componentInfo.TagInfoList, 0);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00009A44 File Offset: 0x00007C44
		private string GetNextLexem(ref string line, ref bool error)
		{
			line = line.TrimStart(new char[0]);
			if (string.IsNullOrEmpty(line))
			{
				return string.Empty;
			}
			if (line[0] == ':' || line[0] == ',')
			{
				string result = new string(line[0], 1);
				line = line.Substring(1);
				return result;
			}
			char c = line[0];
			if (c == '_' || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
			{
				int i = 1;
				while (i < line.Length)
				{
					c = line[i++];
					if (c != '_' && c != '.' && c != '-' && (c < 'A' || c > 'Z') && (c < 'a' || c > 'z') && (c < '0' || c > '9'))
					{
						i--;
						break;
					}
				}
				string result2 = line.Substring(0, i);
				line = line.Substring(i);
				return result2;
			}
			if (c == '-' || c == '+' || char.IsDigit(c))
			{
				int j = 1;
				while (j < line.Length)
				{
					if (!char.IsDigit(line, j++))
					{
						j--;
						break;
					}
				}
				string result3 = line.Substring(0, j);
				line = line.Substring(j);
				return result3;
			}
			error = true;
			line = line.Substring(1);
			return string.Empty;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00009B90 File Offset: 0x00007D90
		private string GetNextString(ref string line)
		{
			line = line.TrimStart(new char[0]);
			if (string.IsNullOrEmpty(line))
			{
				return string.Empty;
			}
			int num = 0;
			int num2 = 0;
			int i = 0;
			bool flag = false;
			while (i < line.Length)
			{
				if (line[i] == '"')
				{
					if (flag)
					{
						num2++;
						i--;
						break;
					}
					flag = true;
					i = (num = i + 1);
				}
				else if (flag)
				{
					i++;
				}
				else if (char.IsWhiteSpace(line[i++]))
				{
					i--;
					break;
				}
			}
			string result = line.Substring(num, i);
			line = line.Substring(num + i + num2);
			return result;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00009C30 File Offset: 0x00007E30
		private TraceComponentInfo CreateComponentIfNeccessary(string name, bool bypassFilterList)
		{
			Dictionary<string, TraceComponentInfo> dictionary = bypassFilterList ? this.bypassFilterEnabledComponentsIndex : this.enabledComponentsIndex;
			if (dictionary.ContainsKey(name))
			{
				return dictionary[name];
			}
			TraceComponentInfo traceComponentInfo = null;
			if (AvailableTraces.InnerDictionary.TryGetValue(name, out traceComponentInfo))
			{
				return new TraceComponentInfo(traceComponentInfo.PrettyName, traceComponentInfo.ComponentGuid, null);
			}
			InternalBypassTrace.TracingConfigurationTracer.TraceDebug(0, 0L, "{0} not a known component (may be custom parameter)", new object[]
			{
				name
			});
			return null;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00009CA4 File Offset: 0x00007EA4
		protected bool ReadTraceTypes(string line, ref BitArray traceTypes)
		{
			for (;;)
			{
				bool flag = false;
				string nextLexem = this.GetNextLexem(ref line, ref flag);
				if (flag)
				{
					break;
				}
				if (string.IsNullOrEmpty(nextLexem))
				{
					return true;
				}
				if (!(nextLexem == ","))
				{
					if (nextLexem == ":")
					{
						goto Block_3;
					}
					TraceType traceTypeByName = this.GetTraceTypeByName(nextLexem);
					if (traceTypeByName != TraceType.None)
					{
						traceTypes[(int)traceTypeByName] = true;
					}
				}
			}
			this.ReportError("Low level scanning error in the trace types statement.", new object[0]);
			return false;
			Block_3:
			this.ReportError("Bogus colon in the trace types statement.", new object[0]);
			return false;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00009D20 File Offset: 0x00007F20
		protected bool ReadFaultInjectionType(ref string line, ref FaultInjectionType faultInjectionType)
		{
			string nextLexem;
			for (;;)
			{
				bool flag = false;
				nextLexem = this.GetNextLexem(ref line, ref flag);
				if (flag)
				{
					break;
				}
				if (string.IsNullOrEmpty(nextLexem))
				{
					return true;
				}
				if (!(nextLexem == ","))
				{
					goto Block_2;
				}
			}
			this.ReportError("Low level scanning error in the trace types statement.", new object[0]);
			return false;
			Block_2:
			if (nextLexem == ":")
			{
				this.ReportError("Bogus colon in the trace types statement.", new object[0]);
				return false;
			}
			faultInjectionType = this.GetFaultInjectionTypeByName(nextLexem);
			return true;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00009D90 File Offset: 0x00007F90
		protected bool ReadFaultInjectionTagComponent(ref string line, ref string tagComponent)
		{
			int num = line.IndexOf(':');
			if (num >= 0)
			{
				tagComponent = line.Substring(0, num);
				line = line.Substring(num);
				return true;
			}
			this.ReportError("Bogus tagComponent definitions.", new object[0]);
			return false;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00009DD8 File Offset: 0x00007FD8
		protected bool ReadFaultInjectionLid(ref string line, ref uint faultInjectionLid)
		{
			string nextLexem;
			for (;;)
			{
				bool flag = false;
				nextLexem = this.GetNextLexem(ref line, ref flag);
				if (flag)
				{
					break;
				}
				if (string.IsNullOrEmpty(nextLexem))
				{
					return true;
				}
				if (!(nextLexem == ","))
				{
					goto Block_2;
				}
			}
			this.ReportError("Low level scanning error in the trace types statement.", new object[0]);
			return false;
			Block_2:
			if (nextLexem == ":")
			{
				this.ReportError("Bogus colon in the trace types statement.", new object[0]);
				return false;
			}
			try
			{
				faultInjectionLid = uint.Parse(nextLexem);
			}
			catch (FormatException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00009E60 File Offset: 0x00008060
		protected bool ReadFaultInjectionParameters(ref string line, ref List<string> faultInjectionParameters)
		{
			faultInjectionParameters = new List<string>();
			for (;;)
			{
				string nextString = this.GetNextString(ref line);
				if (string.IsNullOrEmpty(nextString))
				{
					break;
				}
				faultInjectionParameters.Add(nextString);
			}
			return true;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00009E90 File Offset: 0x00008090
		private bool ReadComponentTags(string line, string componentName, Dictionary<int, TraceTagInfo> tags)
		{
			for (;;)
			{
				bool flag = false;
				string nextLexem = this.GetNextLexem(ref line, ref flag);
				if (flag)
				{
					break;
				}
				if (string.IsNullOrEmpty(nextLexem))
				{
					return true;
				}
				if (!(nextLexem == ","))
				{
					if (nextLexem == ":")
					{
						goto Block_3;
					}
					if (string.Compare(nextLexem, "All", StringComparison.CurrentCultureIgnoreCase) != 0)
					{
						bool flag2 = false;
						TraceTagInfo[] tagInfoList = AvailableTraces.InnerDictionary[componentName].TagInfoList;
						TraceTagInfo[] array = tagInfoList;
						int i = 0;
						while (i < array.Length)
						{
							TraceTagInfo traceTagInfo = array[i];
							if (traceTagInfo != null && string.Compare(nextLexem, traceTagInfo.PrettyName, StringComparison.CurrentCultureIgnoreCase) == 0)
							{
								flag2 = true;
								if (!tags.ContainsKey(traceTagInfo.NumericValue))
								{
									tags.Add(traceTagInfo.NumericValue, traceTagInfo);
									break;
								}
								break;
							}
							else
							{
								i++;
							}
						}
						if (!flag2)
						{
							this.ReportError("Unrecognized tag will be ignored: {0}", new object[]
							{
								nextLexem
							});
						}
					}
				}
			}
			this.ReportError("Low level scanning error in the list of tags.", new object[0]);
			return false;
			Block_3:
			this.ReportError("Bogus colon in the list of tags.", new object[0]);
			return false;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00009F94 File Offset: 0x00008194
		protected TraceType GetTraceTypeByName(string typeName)
		{
			TraceType result;
			try
			{
				result = (TraceType)Enum.Parse(typeof(TraceType), typeName, true);
			}
			catch (ArgumentException)
			{
				this.ReportError("Invalid trace type: {0}", new object[]
				{
					typeName
				});
				result = TraceType.None;
			}
			return result;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00009FE8 File Offset: 0x000081E8
		protected FaultInjectionType GetFaultInjectionTypeByName(string typeName)
		{
			FaultInjectionType result;
			try
			{
				result = (FaultInjectionType)Enum.Parse(typeof(FaultInjectionType), typeName, true);
			}
			catch (ArgumentException)
			{
				this.ReportError("Bogus string for fault injection: {0}", new object[]
				{
					typeName
				});
				result = FaultInjectionType.None;
			}
			return result;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A03C File Offset: 0x0000823C
		protected void ReportError(string errorMsgFmt, params object[] args)
		{
			InternalBypassTrace.TracingConfigurationTracer.TraceError(0, 0L, errorMsgFmt, args);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A04D File Offset: 0x0000824D
		private Stream GetStreamFromFile(string path)
		{
			return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000A058 File Offset: 0x00008258
		private bool GetConsoleTracingEnabled()
		{
			return Environment.UserInteractive;
		}

		// Token: 0x04000286 RID: 646
		private BitArray enabledTraceTypes = new BitArray(ConfigurationDocument.TraceTypesCount + 1);

		// Token: 0x04000287 RID: 647
		private List<TraceComponentInfo> enabledComponentsList = new List<TraceComponentInfo>();

		// Token: 0x04000288 RID: 648
		private List<TraceComponentInfo> bypassFilterComponentsList = new List<TraceComponentInfo>();

		// Token: 0x04000289 RID: 649
		private int currentLine;

		// Token: 0x0400028A RID: 650
		protected Dictionary<string, TraceComponentInfo> enabledComponentsIndex = new Dictionary<string, TraceComponentInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400028B RID: 651
		protected Dictionary<string, TraceComponentInfo> bypassFilterEnabledComponentsIndex = new Dictionary<string, TraceComponentInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400028C RID: 652
		protected Dictionary<string, List<string>> customParameters = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400028D RID: 653
		private FaultInjectionConfig faultInjectionConfig = new FaultInjectionConfig();

		// Token: 0x0400028E RID: 654
		protected bool filteredTracingEnabled;

		// Token: 0x0400028F RID: 655
		protected bool inMemoryTracingEnabled;

		// Token: 0x04000290 RID: 656
		private bool consoleTracingEnabled;

		// Token: 0x04000291 RID: 657
		private bool systemDiagnosticsTracingEnabled;

		// Token: 0x04000292 RID: 658
		internal static readonly Encoding Encoding = Encoding.ASCII;

		// Token: 0x0200007D RID: 125
		// (Invoke) Token: 0x060002D1 RID: 721
		protected delegate void LineProcessor(string processedLine);
	}
}
