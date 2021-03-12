using System;
using System.Collections;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000121 RID: 289
	internal sealed class FormsRegistryLoader
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00043667 File Offset: 0x00041867
		public Hashtable Registries
		{
			get
			{
				return this.registries;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0004366F File Offset: 0x0004186F
		public bool HasCustomForm
		{
			get
			{
				return this.loadedCustomRegistryFiles.Count > 0;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0004367F File Offset: 0x0004187F
		public ClientMappingList BaseClientMappings
		{
			get
			{
				return new ClientMappingList((ClientMapping[])this.baseExperienceClientMappings.ToArray(typeof(ClientMapping)));
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000436A0 File Offset: 0x000418A0
		public void LoadRegistries(string directory)
		{
			ExTraceGlobals.FormsRegistryCallTracer.TraceDebug<string>((long)this.GetHashCode(), "FormsRegistryLoader.LoadRegistries directory = {0}", directory);
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			if (!Directory.Exists(directory))
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_FormsFolderNotFound, string.Empty, new object[]
				{
					directory
				});
				throw new OwaInvalidInputException("Forms directory not found", null, this);
			}
			foreach (string text in Directory.GetDirectories(directory))
			{
				string text2 = Path.Combine(text, "registry.xml").Replace('/', '\\');
				if (!File.Exists(text2))
				{
					ExTraceGlobals.FormsRegistryDataTracer.TraceDebug<string>((long)this.GetHashCode(), "Registry file not found. file = {0}", text2);
				}
				else
				{
					int num = text.LastIndexOf('\\');
					if (num >= 0)
					{
						text = text.Substring(num + 1);
					}
					FormsRegistryParser formsRegistryParser = new FormsRegistryParser();
					try
					{
						formsRegistryParser.Load(text2, text);
					}
					catch (Exception)
					{
						if (formsRegistryParser.Registry.IsCustomRegistry)
						{
							goto IL_251;
						}
						throw;
					}
					if (this.registries.ContainsKey(formsRegistryParser.Registry.Name))
					{
						OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_FormsRegistryReDefinition, string.Empty, new object[]
						{
							formsRegistryParser.Registry.Name
						});
						if (formsRegistryParser.Registry.IsCustomRegistry)
						{
							goto IL_251;
						}
						if (!((FormsRegistry)this.registries[formsRegistryParser.Registry.Name]).IsCustomRegistry)
						{
							throw new OwaInvalidInputException(string.Format("Duplicate registry found ('{0}')", formsRegistryParser.Registry.Name), null, this);
						}
						this.registries.Remove(formsRegistryParser.Registry.Name);
						hashtable.Remove(formsRegistryParser.Registry.Name);
						hashtable2.Remove(formsRegistryParser.Registry.Name);
						this.loadedCustomRegistryFiles.Remove(formsRegistryParser.Registry.Name);
					}
					if (formsRegistryParser.Registry.HasCustomForm)
					{
						this.loadedCustomRegistryFiles.Add(formsRegistryParser.Registry.Name, text2);
					}
					this.registries.Add(formsRegistryParser.Registry.Name, formsRegistryParser.Registry);
					hashtable.Add(formsRegistryParser.Registry.Name, formsRegistryParser.ClientMappings);
					hashtable2.Add(formsRegistryParser.Registry.Name, formsRegistryParser.BaseClientMappings);
				}
				IL_251:;
			}
			if (this.loadedCustomRegistryFiles.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this.loadedCustomRegistryFiles.Values)
				{
					string value = (string)obj;
					stringBuilder.AppendLine(value);
				}
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_CustomizationFormsRegistryLoadSuccessfully, string.Empty, new object[]
				{
					stringBuilder.ToString()
				});
			}
			IDictionaryEnumerator enumerator2 = this.registries.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				FormsRegistry formsRegistry = (FormsRegistry)enumerator2.Value;
				ArrayList arrayList = (ArrayList)hashtable[formsRegistry.Name];
				if (formsRegistry.InheritsFrom.Length > 0)
				{
					FormsRegistry formsRegistry2 = formsRegistry;
					while (formsRegistry2.InheritsFrom.Length > 0)
					{
						if (!this.registries.ContainsKey(formsRegistry.InheritsFrom))
						{
							OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_FormsRegistryNotFound, string.Empty, new object[]
							{
								formsRegistry.Name,
								formsRegistry.InheritsFrom
							});
							throw new OwaInvalidInputException(string.Format("Forms registry {0} inherits forms registry {1} that could not be found", formsRegistry.Name, formsRegistry.InheritsFrom), null, this);
						}
						formsRegistry2 = (FormsRegistry)this.registries[formsRegistry2.InheritsFrom];
						arrayList.AddRange((ArrayList)hashtable[formsRegistry2.Name]);
					}
					ArrayList arrayList2 = (ArrayList)hashtable2[formsRegistry2.Name];
					for (int j = 0; j < arrayList2.Count; j++)
					{
						ClientMapping clientMapping = ClientMapping.Copy((ClientMapping)arrayList2[j]);
						ExTraceGlobals.FormsRegistryDataTracer.TraceDebug<ClientMapping>((long)this.GetHashCode(), "Copied client mapping form inherited registry. ClientMapping = {0}", clientMapping);
						clientMapping.Experience.FormsRegistry = formsRegistry;
						this.baseExperienceClientMappings.Add(clientMapping);
					}
				}
				else
				{
					int k;
					for (k = 0; k < arrayList.Count; k++)
					{
						ClientMapping clientMapping2 = (ClientMapping)arrayList[k];
						if (formsRegistry.BaseExperience == clientMapping2.Experience.Name)
						{
							ExTraceGlobals.FormsRegistryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Registry '{0}' Verified existence of base experience '{1}'", formsRegistry.Name, formsRegistry.BaseExperience);
							break;
						}
					}
					if (k == arrayList.Count)
					{
						OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_FormsRegistryMissingBaseExperience, string.Empty, new object[]
						{
							formsRegistry.Name,
							formsRegistry.BaseExperience
						});
						throw new OwaInvalidInputException(string.Format("Registry {0} does not define the specified base experience named {1}", formsRegistry.Name, formsRegistry.BaseExperience), null, this);
					}
					ArrayList c = (ArrayList)hashtable2[formsRegistry.Name];
					this.baseExperienceClientMappings.AddRange(c);
				}
				arrayList.Sort(new ClientMappingComparer(this.Registries));
				formsRegistry.ClientMappingList = new ClientMappingList((ClientMapping[])arrayList.ToArray(typeof(ClientMapping)));
			}
			ExTraceGlobals.FormsRegistryDataTracer.TraceDebug<int>((long)this.GetHashCode(), "Total client mappings for all base experiences: {0}", this.baseExperienceClientMappings.Count);
			this.baseExperienceClientMappings.Sort(new ClientMappingComparer(this.Registries));
		}

		// Token: 0x04000707 RID: 1799
		private const string RegistryFileName = "registry.xml";

		// Token: 0x04000708 RID: 1800
		private Hashtable registries = new Hashtable();

		// Token: 0x04000709 RID: 1801
		private ArrayList baseExperienceClientMappings = new ArrayList();

		// Token: 0x0400070A RID: 1802
		private Hashtable loadedCustomRegistryFiles = new Hashtable();
	}
}
