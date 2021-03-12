using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa2.Server.Core;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x020004A0 RID: 1184
	internal class UserResourcesFinder
	{
		// Token: 0x0600286B RID: 10347 RVA: 0x000958E8 File Offset: 0x00093AE8
		internal static Slab CreateUserBootSlab(SlabManifestType manifestType, LayoutType layout, string owaVersion)
		{
			BootSlabDefinition bootSlabDefinition = SlabManifestCollectionFactory.GetInstance(owaVersion).GetSlabManifest(manifestType, layout).GetBootSlabDefinition();
			UserContext userContext = (manifestType == SlabManifestType.Anonymous || manifestType == SlabManifestType.GenericMail) ? null : UserContextManager.GetUserContext(HttpContext.Current);
			string[] enabledFeatures = UserResourcesFinder.GetEnabledFeatures(manifestType, userContext);
			return bootSlabDefinition.GetSlab(enabledFeatures, layout);
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x00095938 File Offset: 0x00093B38
		internal static ResourceBase[] GetNonThemedUserDataEmbededLinks(Slab bootSlab, string owaVersion)
		{
			int num = (bootSlab.PackagedStrings.Length > 0) ? bootSlab.PackagedStrings.Length : bootSlab.Strings.Length;
			ResourceBase[] array = new ResourceBase[num + 1];
			if (bootSlab.PackagedStrings.Any<SlabStringFile>())
			{
				for (int i = 0; i < bootSlab.PackagedStrings.Length; i++)
				{
					array[i] = new LocalizedStringsScriptResource(bootSlab.PackagedStrings[i].Name, ResourceTarget.Any, owaVersion);
				}
			}
			else
			{
				for (int j = 0; j < bootSlab.Strings.Length; j++)
				{
					array[j] = new LocalizedStringsScriptResource(bootSlab.Strings[j].Name, ResourceTarget.Any, owaVersion);
				}
			}
			array[array.Length - 1] = new GlobalizeCultureScriptResource(ResourceTarget.Any, owaVersion);
			return array;
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000959EC File Offset: 0x00093BEC
		public static ResourceBase[] GetUserDataEmbeddedLinks(Slab bootSlab, string owaVersion)
		{
			IEnumerable<ResourceBase> nonThemedUserDataEmbededLinks = UserResourcesFinder.GetNonThemedUserDataEmbededLinks(bootSlab, owaVersion);
			ThemeStyleResource[] userDataEmbededStylesLinks = UserResourcesFinder.GetUserDataEmbededStylesLinks(bootSlab, owaVersion);
			return nonThemedUserDataEmbededLinks.Union(userDataEmbededStylesLinks).ToArray<ResourceBase>();
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x00095A18 File Offset: 0x00093C18
		public static string GetResourcesHash(ResourceBase[] resources, IPageContext context, bool bootResources, string owaVersion)
		{
			List<byte[]> list = new List<byte[]>();
			foreach (ResourceBase resourceBase in resources)
			{
				list.Add(Encoding.ASCII.GetBytes(resourceBase.GetResourcePath(context, bootResources).Replace(owaVersion, string.Empty)));
			}
			return Convert.ToBase64String(AppCacheManifestHandlerBase.CalculateHashOnHashes(list));
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x00095A70 File Offset: 0x00093C70
		public static ThemeStyleResource[] GetUserDataEmbededStylesLinks(Slab bootSlab, string owaVersion)
		{
			List<ThemeStyleResource> list = new List<ThemeStyleResource>();
			foreach (SlabStyleFile style in bootSlab.Styles)
			{
				list.Add(ThemeStyleResource.FromSlabStyle(style, owaVersion, ThemeManagerFactory.GetInstance(owaVersion).ShouldSkipThemeFolder));
			}
			return list.ToArray();
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x00095ABC File Offset: 0x00093CBC
		public static ResourceBase[] GetUserDataEmbeddedLinks(SlabManifestType manifestType, LayoutType layout, string owaVersion)
		{
			Slab bootSlab = UserResourcesFinder.CreateUserBootSlab(manifestType, layout, owaVersion);
			return UserResourcesFinder.GetUserDataEmbeddedLinks(bootSlab, owaVersion);
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x00095ADC File Offset: 0x00093CDC
		public static string GetEnabledFlightedFeaturesJsonArray(SlabManifestType type, IUserContext userContext, FlightedFeatureScope scope)
		{
			HashSet<string> source = new HashSet<string>();
			if (userContext != null)
			{
				source = UserResourcesFinder.GetEnabledFlightedFeatures(type, userContext, scope);
			}
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(string[]));
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataContractJsonSerializer.WriteObject(memoryStream, source.ToArray<string>());
				memoryStream.Close();
				@string = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			return @string;
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x00095B54 File Offset: 0x00093D54
		private static string[] GetEnabledFeatures(SlabManifestType type, UserContext userContext)
		{
			if (type == SlabManifestType.GenericMail || type == SlabManifestType.Anonymous || userContext == null)
			{
				return new string[0];
			}
			return userContext.FeaturesManager.GetClientEnabledFeatures();
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x00095B7B File Offset: 0x00093D7B
		private static HashSet<string> GetEnabledFlightedFeatures(SlabManifestType type, IUserContext userContext, FlightedFeatureScope scope)
		{
			if (type == SlabManifestType.GenericMail || type == SlabManifestType.Anonymous)
			{
				return new HashSet<string>();
			}
			return userContext.FeaturesManager.GetEnabledFlightedFeatures(scope);
		}
	}
}
