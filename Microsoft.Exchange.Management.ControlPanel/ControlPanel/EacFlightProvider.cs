using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Flighting;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.CsmSdk;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001F0 RID: 496
	public class EacFlightProvider : FlightProvider
	{
		// Token: 0x06002636 RID: 9782 RVA: 0x00075A44 File Offset: 0x00073C44
		public EacFlightProvider()
		{
			if (!EacFlightProvider.initialized)
			{
				lock (EacFlightProvider.locker)
				{
					if (!EacFlightProvider.initialized)
					{
						EacFlightProvider.eacUrlToSectionMap = EacFlightUtility.EacGlobalSnapshot.Eac.GetObjectsOfType<IUrl>().ToDictionary((KeyValuePair<string, IUrl> pair) => pair.Value.Url, (KeyValuePair<string, IUrl> pair) => pair.Key, StringComparer.OrdinalIgnoreCase);
						EacFlightProvider.eacRewriteToSectionMap = EacFlightUtility.EacGlobalSnapshot.Eac.GetObjectsOfType<IUrlMapping>().ToDictionary((KeyValuePair<string, IUrlMapping> pair) => pair.Value.Url, (KeyValuePair<string, IUrlMapping> pair) => pair.Key, StringComparer.OrdinalIgnoreCase);
						EacFlightProvider.initialized = true;
					}
				}
			}
		}

		// Token: 0x17001BD3 RID: 7123
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x00075B5C File Offset: 0x00073D5C
		public static EacFlightProvider Instance
		{
			get
			{
				return (EacFlightProvider)FlightProvider.Instance;
			}
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x00075B68 File Offset: 0x00073D68
		public override bool IsFeatureEnabled(string eacFeatureName)
		{
			VariantConfigurationSnapshot snapshot = eacFeatureName.StartsWith("Global.") ? EacFlightUtility.EacGlobalSnapshot : EacFlightUtility.GetSnapshotForCurrentUser();
			return snapshot.GetFeature(eacFeatureName).Enabled;
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00075BB0 File Offset: 0x00073DB0
		public override string[] GetAllEnabledFeatures()
		{
			return (from pair in this.GetAllFeatures()
			where pair.Value
			select pair.Key).ToArray<string>();
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x00075C0C File Offset: 0x00073E0C
		public Dictionary<string, bool> GetAllFeatures()
		{
			VariantConfigurationSnapshot snapshotForCurrentUser = EacFlightUtility.GetSnapshotForCurrentUser();
			return EacFlightUtility.GetAllEacRelatedFeatures(snapshotForCurrentUser);
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x00075C28 File Offset: 0x00073E28
		public string GetRewriteUrl(string url)
		{
			string text = null;
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			url = url.Trim().Replace('\\', '/');
			if (EacFlightProvider.eacRewriteToSectionMap.ContainsKey(url))
			{
				VariantConfigurationSnapshot snapshotForCurrentUser = EacFlightUtility.GetSnapshotForCurrentUser();
				IUrlMapping @object = snapshotForCurrentUser.Eac.GetObject<IUrlMapping>(EacFlightProvider.eacRewriteToSectionMap[url]);
				text = @object.RemapTo;
				if (text != null)
				{
					text = text.Trim();
				}
				if (text == string.Empty)
				{
					text = null;
				}
			}
			return text;
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x00075CA4 File Offset: 0x00073EA4
		public bool IsUrlEnabled(string url)
		{
			bool result = true;
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			url = url.Trim().Replace('\\', '/');
			if (EacFlightProvider.eacUrlToSectionMap.ContainsKey(url))
			{
				VariantConfigurationSnapshot snapshotForCurrentUser = EacFlightUtility.GetSnapshotForCurrentUser();
				IUrl @object = snapshotForCurrentUser.Eac.GetObject<IUrl>(EacFlightProvider.eacUrlToSectionMap[url]);
				result = @object.Enabled;
			}
			return result;
		}

		// Token: 0x04001F50 RID: 8016
		private static IDictionary<string, string> eacUrlToSectionMap;

		// Token: 0x04001F51 RID: 8017
		private static IDictionary<string, string> eacRewriteToSectionMap;

		// Token: 0x04001F52 RID: 8018
		private static volatile bool initialized = false;

		// Token: 0x04001F53 RID: 8019
		private static object locker = new object();
	}
}
