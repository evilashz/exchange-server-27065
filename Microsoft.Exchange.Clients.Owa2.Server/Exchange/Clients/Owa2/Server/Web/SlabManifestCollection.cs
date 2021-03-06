using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200049B RID: 1179
	public class SlabManifestCollection
	{
		// Token: 0x06002819 RID: 10265 RVA: 0x00093980 File Offset: 0x00091B80
		public SlabManifestCollection(string owaVersion)
		{
			this.owaVersion = owaVersion;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x0009399C File Offset: 0x00091B9C
		public void Add(LayoutType layout, SlabManifest slabManifest)
		{
			string name = slabManifest.Type.Name;
			Dictionary<string, Dictionary<LayoutType, SlabManifest>> dictionary = this.slabManifests;
			dictionary = new Dictionary<string, Dictionary<LayoutType, SlabManifest>>(dictionary);
			Dictionary<LayoutType, SlabManifest> dictionary2;
			if (!dictionary.TryGetValue(name, out dictionary2))
			{
				dictionary2 = new Dictionary<LayoutType, SlabManifest>();
			}
			else
			{
				dictionary2 = new Dictionary<LayoutType, SlabManifest>(dictionary2);
			}
			dictionary2.Add(layout, slabManifest);
			dictionary[name] = dictionary2;
			this.slabManifests = dictionary;
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000939F4 File Offset: 0x00091BF4
		public IEnumerable<string> GetCodeScriptResources(SlabManifestType slabManifestType, LayoutType layout, string[] features, bool includeBootResourcesOnly)
		{
			IEnumerable<Slab> slabs = this.GetSlabs(slabManifestType, layout, features, includeBootResourcesOnly);
			List<string> list = new List<string>();
			foreach (Slab slab in slabs)
			{
				if (slab.PackagedSources.Any<SlabSourceFile>())
				{
					foreach (SlabSourceFile slabSourceFile in slab.PackagedSources)
					{
						list.Add(slabSourceFile.Name);
					}
				}
				else
				{
					foreach (SlabSourceFile slabSourceFile2 in slab.Sources)
					{
						list.Add(slabSourceFile2.Name);
					}
				}
			}
			return list.Distinct(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x00093C44 File Offset: 0x00091E44
		public IEnumerable<string> GetThemedStyles(SlabManifestType slabManifestType, LayoutType layout, string[] features, bool includeBootSlabsOnly)
		{
			IEnumerable<Slab> slabs = this.GetSlabs(slabManifestType, layout, features, includeBootSlabsOnly);
			IEnumerable<string> source = from slab in slabs
			from style in slab.Styles
			where slab.Styles != null && !style.IsSprite() && style.IsForLayout(layout) && !style.IsNotThemed()
			select style.Name;
			return source.Distinct(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x00093D54 File Offset: 0x00091F54
		public IEnumerable<string> GetThemedSpriteStyles(SlabManifestType slabManifestType, LayoutType layout, string[] features, bool includeBootSlabsOnly)
		{
			IEnumerable<Slab> slabs = this.GetSlabs(slabManifestType, layout, features, includeBootSlabsOnly);
			IEnumerable<string> source = from slab in slabs
			from style in slab.Styles
			where slab.Styles != null && style.IsSprite() && style.IsForLayout(layout) && !style.IsNotThemed()
			select style.Name;
			return source.Distinct(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x00093E54 File Offset: 0x00092054
		public IEnumerable<string> GetNonThemedStyles(SlabManifestType slabManifestType, LayoutType layout, string[] features, bool includeBootSlabsOnly)
		{
			IEnumerable<Slab> slabs = this.GetSlabs(slabManifestType, layout, features, includeBootSlabsOnly);
			IEnumerable<string> source = from slab in slabs
			from style in slab.Styles
			where slab.Styles != null && style.IsForLayout(layout) && style.IsNotThemed()
			select style.Name;
			return source.Distinct(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x0009406C File Offset: 0x0009226C
		public IEnumerable<string> GetThemedImages(SlabManifestType slabManifestType, LayoutType layout, string[] features, bool includeBootSlabsOnly)
		{
			IEnumerable<Slab> slabs = this.GetSlabs(slabManifestType, layout, features, includeBootSlabsOnly);
			IEnumerable<string> source = from slab in slabs
			from image in slab.Images
			where slab.Images != null && image.IsThemed() && image.IsForLayout(layout)
			select image.Name;
			return source.Distinct(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x0009416C File Offset: 0x0009236C
		public IEnumerable<string> GetNonThemedImages(SlabManifestType slabManifestType, LayoutType layout, string[] features, bool includeBootSlabsOnly)
		{
			IEnumerable<Slab> slabs = this.GetSlabs(slabManifestType, layout, features, includeBootSlabsOnly);
			IEnumerable<string> source = from slab in slabs
			from image in slab.Images
			where slab.Images != null && !image.IsThemed() && image.IsForLayout(layout)
			select image.Name;
			return source.Distinct(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x00094378 File Offset: 0x00092578
		public IEnumerable<string> GetFonts(SlabManifestType slabManifestType, LayoutType layout, string[] features, bool includeBootSlabsOnly)
		{
			IEnumerable<Slab> slabs = this.GetSlabs(slabManifestType, layout, features, includeBootSlabsOnly);
			IEnumerable<string> source = from slab in slabs
			from font in slab.Fonts
			where slab.Fonts != null && font.IsForLayout(layout)
			select font.Name;
			return source.Distinct(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000945A8 File Offset: 0x000927A8
		public IEnumerable<string> GetLocalizedStringsScriptResources(SlabManifestType slabManifestType, LayoutType layout, string[] features, bool includeBootSlabsOnly)
		{
			IEnumerable<Slab> slabs = this.GetSlabs(slabManifestType, layout, features, includeBootSlabsOnly);
			IEnumerable<string> first = from slab in slabs
			from stringSource in slab.PackagedStrings
			where stringSource.IsStandard()
			select stringSource.Name;
			IEnumerable<string> second = from slab in slabs
			from stringSource in slab.Strings
			where stringSource.IsStandard() && !slab.PackagedStrings.Any<SlabStringFile>()
			select stringSource.Name;
			return first.Union(second).Distinct(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x00094708 File Offset: 0x00092908
		public IEnumerable<string> GetLocalizedExtStringsScriptResources(SlabManifestType slabManifestType, LayoutType layout, string[] features, bool includeBootSlabsOnly)
		{
			IEnumerable<Slab> slabs = this.GetSlabs(slabManifestType, layout, features, includeBootSlabsOnly);
			IEnumerable<string> source = from slab in slabs
			from stringSource in slab.Strings
			where stringSource.IsExtensibility()
			select stringSource.Name;
			return source.Distinct(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000947B1 File Offset: 0x000929B1
		public string GetSlabsJson(SlabManifestType slabManifestType, string[] features, LayoutType layout)
		{
			return this.GetSlabManifest(slabManifestType, layout).GetSlabsJson(features, layout);
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x000947C2 File Offset: 0x000929C2
		public static SlabManifestCollection Create(string owaVersion)
		{
			return new SlabManifestCollection(owaVersion);
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x000947CC File Offset: 0x000929CC
		public SlabManifest GetSlabManifest(SlabManifestType slabManifestType, LayoutType layout)
		{
			if (this.owaVersion == null)
			{
				return this.slabManifests[slabManifestType.Name][layout];
			}
			string name = slabManifestType.Name;
			Dictionary<LayoutType, SlabManifest> dictionary;
			if (!this.slabManifests.TryGetValue(name, out dictionary))
			{
				dictionary = new Dictionary<LayoutType, SlabManifest>();
				string rootPath = FolderConfiguration.Instance.RootPath;
				string manifestDiskRelativeFolderPath = ResourcePathBuilderUtilities.GetManifestDiskRelativeFolderPath(this.owaVersion);
				string text = name + ".xml";
				if (File.Exists(Path.Combine(rootPath, manifestDiskRelativeFolderPath, text)))
				{
					SlabManifest slabManifest = SlabManifestLoader.Load(rootPath, text, new Action<string, Exception>(SlabManifestCollection.LogManifestExceptionToEventLogs), manifestDiskRelativeFolderPath);
					slabManifest.Type = slabManifestType;
					foreach (LayoutType key in SlabManifestCollection.layoutTypes)
					{
						dictionary.Add(key, slabManifest);
					}
				}
				foreach (LayoutType layoutType in SlabManifestCollection.layoutTypes)
				{
					string text2 = string.Concat(new object[]
					{
						name.ToLowerInvariant(),
						".",
						layoutType,
						".xml"
					});
					if (File.Exists(Path.Combine(rootPath, manifestDiskRelativeFolderPath, text2)))
					{
						SlabManifest slabManifest2 = SlabManifestLoader.Load(rootPath, text2, new Action<string, Exception>(SlabManifestCollection.LogManifestExceptionToEventLogs), manifestDiskRelativeFolderPath);
						slabManifest2.Type = slabManifestType;
						dictionary.Add(layoutType, slabManifest2);
					}
				}
				Dictionary<string, Dictionary<LayoutType, SlabManifest>> dictionary2 = new Dictionary<string, Dictionary<LayoutType, SlabManifest>>(this.slabManifests);
				if (!dictionary2.ContainsKey(name))
				{
					dictionary2[slabManifestType.Name] = dictionary;
				}
				this.slabManifests = dictionary2;
			}
			return dictionary[layout];
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x00094998 File Offset: 0x00092B98
		private static void LogManifestExceptionToEventLogs(string fileName, Exception exception)
		{
			ExTraceGlobals.CoreTracer.TraceError<Type, string, Exception>(0L, "Exception {0} loading manifest {1}. Details: {2}", exception.GetType(), fileName, exception);
			OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_OwaManifestInvalid, string.Empty, new object[]
			{
				fileName,
				exception
			});
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x000949E8 File Offset: 0x00092BE8
		private IEnumerable<Slab> GetSlabs(SlabManifestType slabManifestType, LayoutType layout, string[] features, bool includeBootSlabsOnly)
		{
			List<Slab> list = new List<Slab>();
			if (includeBootSlabsOnly)
			{
				Slab slab = this.GetSlabManifest(slabManifestType, layout).GetBootSlabDefinition().GetSlab(features, layout);
				if (slab != null)
				{
					list.Add(slab);
				}
			}
			else
			{
				list = (from kv in this.GetSlabManifest(slabManifestType, layout).GetSlabs(features, layout)
				select kv.Value).ToList<Slab>();
			}
			return list;
		}

		// Token: 0x0400173C RID: 5948
		private static readonly IEnumerable<LayoutType> layoutTypes = Enum.GetValues(typeof(LayoutType)).Cast<LayoutType>();

		// Token: 0x0400173D RID: 5949
		private Dictionary<string, Dictionary<LayoutType, SlabManifest>> slabManifests = new Dictionary<string, Dictionary<LayoutType, SlabManifest>>();

		// Token: 0x0400173E RID: 5950
		private readonly string owaVersion;
	}
}
