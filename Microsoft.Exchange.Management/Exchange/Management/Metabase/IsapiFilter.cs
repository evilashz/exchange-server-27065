using System;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004CC RID: 1228
	internal static class IsapiFilter
	{
		// Token: 0x06002AA1 RID: 10913 RVA: 0x000AAE10 File Offset: 0x000A9010
		public static DirectoryEntry CreateIsapiFilter(string parent, string localPath, string filterName, out bool created)
		{
			created = false;
			DirectoryEntry directoryEntry = null;
			DirectoryEntry result;
			try
			{
				bool flag;
				using (DirectoryEntry directoryEntry2 = IisUtility.FindOrCreateWebObject(parent, "Filters", "IIsFilters", out flag))
				{
					if (flag)
					{
						directoryEntry2.CommitChanges();
						IisUtility.CommitMetabaseChanges(IisUtility.ServerFromWebSite(IisUtility.WebSiteFromMetabasePath(directoryEntry2.Path)));
					}
					directoryEntry = IisUtility.FindOrCreateWebObject(directoryEntry2.Path, filterName, "IIsFilter", out flag);
					if (flag || (directoryEntry.Properties["FilterPath"].Value is string && (string)directoryEntry.Properties["FilterPath"].Value != localPath))
					{
						directoryEntry.Properties["FilterPath"].Value = localPath;
						flag = true;
					}
					string text = (string)directoryEntry2.Properties["FilterLoadOrder"].Value;
					if (text == null || text.Length == 0)
					{
						directoryEntry2.Properties["FilterLoadOrder"].Value = filterName;
					}
					else if (!IsapiFilter.IsFilterInLoadOrder(text, filterName))
					{
						StringBuilder stringBuilder = new StringBuilder(text);
						stringBuilder.Append(",");
						stringBuilder.Append(filterName);
						directoryEntry2.Properties["FilterLoadOrder"].Value = stringBuilder.ToString();
					}
					directoryEntry2.CommitChanges();
					IisUtility.CommitMetabaseChanges(IisUtility.ServerFromWebSite(IisUtility.WebSiteFromMetabasePath(directoryEntry2.Path)));
					created = flag;
					result = directoryEntry;
				}
			}
			catch (Exception)
			{
				if (directoryEntry != null)
				{
					directoryEntry.Close();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000AAFB4 File Offset: 0x000A91B4
		private static bool IsFilterInLoadOrder(string loadOrder, string filterName)
		{
			string[] array = loadOrder.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (string.Compare(array[i], filterName, true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000AAFF8 File Offset: 0x000A91F8
		public static void RemoveIsapiFilter(string parent, string name)
		{
			DirectoryEntry directoryEntry = null;
			DirectoryEntry directoryEntry2 = null;
			try
			{
				directoryEntry = IisUtility.FindWebObject(parent, "Filters", "IIsFilters");
				directoryEntry2 = directoryEntry.Children.Find(name, string.Empty);
				directoryEntry.Children.Remove(directoryEntry2);
				directoryEntry.CommitChanges();
				IisUtility.CommitMetabaseChanges(IisUtility.ServerFromWebSite(IisUtility.WebSiteFromMetabasePath(directoryEntry.Path)));
			}
			catch (WebObjectNotFoundException)
			{
			}
			catch (DirectoryNotFoundException)
			{
				if (directoryEntry == null || directoryEntry2 != null)
				{
					throw;
				}
			}
			catch (Exception innerException)
			{
				if (directoryEntry2 != null)
				{
					throw new IsapiFilterNotRemovedException(parent, name, innerException);
				}
				throw;
			}
			finally
			{
				if (directoryEntry2 != null)
				{
					directoryEntry2.Close();
				}
				using (directoryEntry)
				{
					if (directoryEntry != null)
					{
						IsapiFilter.RemoveFilterFromLoadOrder(directoryEntry, name);
					}
				}
			}
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000AB0D8 File Offset: 0x000A92D8
		private static void RemoveFilterFromLoadOrder(DirectoryEntry filterContainer, string name)
		{
			string text = (string)filterContainer.Properties["FilterLoadOrder"].Value;
			if (text == null || text.Length == 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = text.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (string.Compare(array[i], name, true, CultureInfo.InvariantCulture) != 0)
				{
					stringBuilder.Append(array[i]);
					stringBuilder.Append(",");
				}
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			filterContainer.Properties["FilterLoadOrder"].Value = stringBuilder.ToString();
			filterContainer.CommitChanges();
			IisUtility.CommitMetabaseChanges(IisUtility.ServerFromWebSite(IisUtility.WebSiteFromMetabasePath(filterContainer.Path)));
		}

		// Token: 0x04001FDD RID: 8157
		private const string filterContainerName = "Filters";
	}
}
