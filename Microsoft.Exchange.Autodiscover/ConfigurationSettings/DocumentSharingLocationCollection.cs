using System;
using System.Collections.Generic;
using Microsoft.Exchange.Autodiscover.WCF;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x02000041 RID: 65
	public class DocumentSharingLocationCollection : List<DocumentSharingLocation>
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00008894 File Offset: 0x00006A94
		public void Discover(string alias)
		{
			string documentTypesSupportedForSharing = DocumentSharingLocationCollection.webConfiguration.Member.DocumentTypesSupportedForSharing;
			if (string.IsNullOrEmpty(documentTypesSupportedForSharing))
			{
				return;
			}
			FileExtensionCollection fileExtensionCollection = new FileExtensionCollection();
			foreach (string item in documentTypesSupportedForSharing.Split(new char[]
			{
				' '
			}))
			{
				fileExtensionCollection.Add(item);
			}
			if (fileExtensionCollection.Count == 0)
			{
				return;
			}
			string mySiteServiceUrlTemplate = DocumentSharingLocationCollection.webConfiguration.Member.MySiteServiceUrlTemplate;
			string mySiteLocationUrlTemplate = DocumentSharingLocationCollection.webConfiguration.Member.MySiteLocationUrlTemplate;
			if (!string.IsNullOrEmpty(mySiteServiceUrlTemplate) && !string.IsNullOrEmpty(mySiteLocationUrlTemplate))
			{
				DocumentSharingLocation item2 = new DocumentSharingLocation(string.Format(mySiteServiceUrlTemplate, alias), string.Format(mySiteLocationUrlTemplate, alias), "My Site", fileExtensionCollection, true, true, true, true);
				base.Add(item2);
			}
			string projectSiteServiceUrl = DocumentSharingLocationCollection.webConfiguration.Member.ProjectSiteServiceUrl;
			string projectSiteLocationUrl = DocumentSharingLocationCollection.webConfiguration.Member.ProjectSiteLocationUrl;
			if (!string.IsNullOrEmpty(projectSiteServiceUrl) && !string.IsNullOrEmpty(projectSiteLocationUrl))
			{
				DocumentSharingLocation item3 = new DocumentSharingLocation(projectSiteServiceUrl, projectSiteLocationUrl, "Project Site", fileExtensionCollection, false, false, false, false);
				base.Add(item3);
			}
		}

		// Token: 0x040001A7 RID: 423
		private static LazyMember<AutodiscoverWebConfiguration> webConfiguration = new LazyMember<AutodiscoverWebConfiguration>(() => new AutodiscoverWebConfiguration());
	}
}
