using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa2.Server.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000326 RID: 806
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetThemes : ServiceCommand<ThemeSelectionInfoType>
	{
		// Token: 0x06001AC1 RID: 6849 RVA: 0x0006513F File Offset: 0x0006333F
		public GetThemes(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00065148 File Offset: 0x00063348
		protected override ThemeSelectionInfoType InternalExecute()
		{
			ISet<string> set = new HashSet<string>();
			UserContext userContext = UserContextManager.GetUserContext(HttpContext.Current);
			IEnumerable<ThemeStyleResource> userMouseThemedResources = this.GetUserMouseThemedResources(userContext);
			foreach (ThemeStyleResource themeStyleResource in userMouseThemedResources)
			{
				string item = Globals.FormatURIForCDN(themeStyleResource.StyleDirectory) + themeStyleResource.ResourceName;
				set.Add(item);
			}
			string currentOwaVersion = userContext.CurrentOwaVersion;
			return new ThemeSelectionInfoType
			{
				Themes = ThemeManagerFactory.GetInstance(currentOwaVersion).Themes,
				CssPaths = set.Distinct<string>().ToArray<string>(),
				ThemePath = ResourcePathBuilderUtilities.GetThemeResourcesRelativeFolderPath(ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(currentOwaVersion))
			};
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000652A4 File Offset: 0x000634A4
		private IEnumerable<ThemeStyleResource> GetUserMouseThemedResources(UserContext userContext)
		{
			if (userContext == null || userContext.FeaturesManager == null)
			{
				return new ThemeStyleResource[0];
			}
			List<ThemeStyleResource> list = new List<ThemeStyleResource>();
			string owaVersion = userContext.CurrentOwaVersion;
			SlabManifest slabManifest = SlabManifestCollectionFactory.GetInstance(owaVersion).GetSlabManifest(SlabManifestType.Standard, LayoutType.Mouse);
			IDictionary<string, Slab> slabs = slabManifest.GetSlabs(userContext.FeaturesManager.GetClientEnabledFeatures(), LayoutType.Mouse);
			Slab bootSlab = null;
			if (slabs.ContainsKey("boot"))
			{
				bootSlab = slabs["boot"];
				list.AddRange(UserResourcesFinder.GetUserDataEmbededStylesLinks(bootSlab, owaVersion));
			}
			IEnumerable<SlabStyleFile> source = (from p in slabManifest.GetSlabs(userContext.FeaturesManager.GetClientEnabledFeatures(), LayoutType.Mouse)
			where p.Value != bootSlab
			select p).SelectMany((KeyValuePair<string, Slab> p) => p.Value.Styles);
			IEnumerable<ThemeStyleResource> collection = from style in source
			where style.IsSprite()
			select new ThemeStyleResource(style.Name, ResourceTarget.MouseOnly, owaVersion, ThemeManagerFactory.GetInstance(owaVersion).ShouldSkipThemeFolder);
			list.AddRange(collection);
			IEnumerable<LocalizedThemeStyleResource> collection2 = from style in source
			where !style.IsSprite()
			select new LocalizedThemeStyleResource(style.Name, ResourceTarget.MouseOnly, owaVersion, ThemeManagerFactory.GetInstance(owaVersion).ShouldSkipThemeFolder);
			list.AddRange(collection2);
			return list;
		}
	}
}
