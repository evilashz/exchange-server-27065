using System;
using System.Collections.Generic;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001F1 RID: 497
	internal static class EacFlightUtility
	{
		// Token: 0x17001BD4 RID: 7124
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x00075D1C File Offset: 0x00073F1C
		public static VariantConfigurationSnapshot EacGlobalSnapshot
		{
			get
			{
				if (EacFlightUtility.eacGlobalSnapshot == null)
				{
					lock (EacFlightUtility.eacGlobalSnapshotLocker)
					{
						if (EacFlightUtility.eacGlobalSnapshot == null)
						{
							EacFlightUtility.eacGlobalSnapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
						}
					}
				}
				return EacFlightUtility.eacGlobalSnapshot;
			}
		}

		// Token: 0x17001BD5 RID: 7125
		// (get) Token: 0x06002645 RID: 9797 RVA: 0x00075D84 File Offset: 0x00073F84
		public static string[] FeaturePrefixs
		{
			get
			{
				return new string[]
				{
					"Global.",
					"Eac."
				};
			}
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x00075DA9 File Offset: 0x00073FA9
		internal static IFeature GetFeature(this VariantConfigurationSnapshot snapshot, string eacSecion)
		{
			return EacFlightUtility.GetObjectForEac<IFeature>(snapshot, eacSecion);
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00075DB4 File Offset: 0x00073FB4
		private static T GetObjectForEac<T>(VariantConfigurationSnapshot snapshot, string eacSecion) where T : class, ISettings
		{
			if (eacSecion == null)
			{
				throw new ArgumentNullException("eacSection");
			}
			int num = eacSecion.IndexOf('.');
			if (num == -1 || num == eacSecion.Length - 1)
			{
				throw new ArgumentException(string.Format("Incorrect section name: {0}, the valid EAC feature name should be 'Eac.XXX', 'Global.XXX'", eacSecion));
			}
			string text = eacSecion.Substring(0, num + 1);
			string id = eacSecion.Substring(num + 1);
			string a;
			if ((a = text) != null)
			{
				if (a == "Global.")
				{
					return snapshot.Global.GetObject<T>(id);
				}
				if (a == "Eac.")
				{
					return snapshot.Eac.GetObject<T>(id);
				}
			}
			throw new ArgumentException(string.Format("Incorrect section name: {0}, the valid EAC feature name should be 'Eac.XXX', 'Global.XXX'", eacSecion));
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x00075E60 File Offset: 0x00074060
		public static VariantConfigurationSnapshot GetSnapshotForCurrentUser()
		{
			bool flag;
			return EacFlightUtility.GetSnapshotForCurrentUser(out flag);
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x00075E74 File Offset: 0x00074074
		internal static VariantConfigurationSnapshot GetSnapshotForCurrentUser(out bool isGlobal)
		{
			RbacPrincipal current = RbacPrincipal.GetCurrent(false);
			if (current != null && current.RbacConfiguration != null && current.RbacConfiguration.ConfigurationSettings != null && current.RbacConfiguration.ConfigurationSettings.VariantConfigurationSnapshot != null)
			{
				isGlobal = false;
				return current.RbacConfiguration.ConfigurationSettings.VariantConfigurationSnapshot;
			}
			isGlobal = true;
			return EacFlightUtility.EacGlobalSnapshot;
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x00075ED0 File Offset: 0x000740D0
		internal static Dictionary<string, bool> GetAllEacRelatedFeatures(VariantConfigurationSnapshot snapshot)
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			foreach (KeyValuePair<string, IFeature> keyValuePair in snapshot.Global.GetObjectsOfType<IFeature>())
			{
				dictionary.Add("Global." + keyValuePair.Key, keyValuePair.Value.Enabled);
			}
			foreach (KeyValuePair<string, IFeature> keyValuePair2 in snapshot.Eac.GetObjectsOfType<IFeature>())
			{
				dictionary.Add("Eac." + keyValuePair2.Key, keyValuePair2.Value.Enabled);
			}
			return dictionary;
		}

		// Token: 0x04001F5A RID: 8026
		public const string GlobalSettingsPrefix = "Global.";

		// Token: 0x04001F5B RID: 8027
		public const string EacSettingsPrefix = "Eac.";

		// Token: 0x04001F5C RID: 8028
		public const char FeatureNameSeperator = '.';

		// Token: 0x04001F5D RID: 8029
		private static volatile VariantConfigurationSnapshot eacGlobalSnapshot;

		// Token: 0x04001F5E RID: 8030
		private static object eacGlobalSnapshotLocker = new object();
	}
}
