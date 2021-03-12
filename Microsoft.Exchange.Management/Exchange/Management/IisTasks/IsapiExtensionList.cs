using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Management.Metabase;

namespace Microsoft.Exchange.Management.IisTasks
{
	// Token: 0x02000412 RID: 1042
	internal sealed class IsapiExtensionList : IDisposable
	{
		// Token: 0x06002466 RID: 9318 RVA: 0x00090E4C File Offset: 0x0008F04C
		public IsapiExtensionList(string hostName)
		{
			string iisDirectoryEntryPath = string.Format("IIS://{0}/W3SVC", hostName);
			try
			{
				this.rootEntry = IisUtility.CreateIISDirectoryEntry(iisDirectoryEntryPath);
				this.restrictionList = this.rootEntry.Properties["WebSvcExtRestrictionList"];
				this.extensionMap = new List<IsapiExtensionList.ExtensionMapUnit>(this.restrictionList.Count + 5);
				for (int i = 0; i < this.restrictionList.Count; i++)
				{
					string text = this.restrictionList[i] as string;
					if (text != null)
					{
						IsapiExtension isapiExtension = IsapiExtension.Parse(text);
						if (isapiExtension != null)
						{
							this.extensionMap.Add(new IsapiExtensionList.ExtensionMapUnit(isapiExtension, i));
						}
					}
				}
			}
			catch (Exception)
			{
				this.Dispose();
				throw;
			}
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x00090F0C File Offset: 0x0008F10C
		~IsapiExtensionList()
		{
			this.Dispose();
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x00090F38 File Offset: 0x0008F138
		public void Dispose()
		{
			this.extensionMap = null;
			this.restrictionList = null;
			if (this.rootEntry != null)
			{
				this.rootEntry.Dispose();
				this.rootEntry = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06002469 RID: 9321 RVA: 0x00090F68 File Offset: 0x0008F168
		public int Count
		{
			get
			{
				return this.extensionMap.Count;
			}
		}

		// Token: 0x17000ABE RID: 2750
		public IsapiExtension this[int i]
		{
			get
			{
				return this.extensionMap[i].Extension;
			}
			set
			{
				IsapiExtensionList.ExtensionMapUnit extensionMapUnit = this.extensionMap[i];
				this.extensionMap[i] = new IsapiExtensionList.ExtensionMapUnit(value, extensionMapUnit.RestrictionListIndex);
				this.restrictionList[this.extensionMap[i].RestrictionListIndex] = value.ToMetabaseString();
			}
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x00090FDD File Offset: 0x0008F1DD
		public void Add(IsapiExtension extension)
		{
			this.restrictionList.Add(extension.ToMetabaseString());
			this.extensionMap.Add(new IsapiExtensionList.ExtensionMapUnit(extension, this.restrictionList.Count - 1));
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x00091010 File Offset: 0x0008F210
		public void Add(bool allow, string physicalPath, bool uiDeletable, string groupID, string description)
		{
			bool flag = false;
			for (int i = 0; i < this.extensionMap.Count; i++)
			{
				IsapiExtensionList.ExtensionMapUnit extensionMapUnit = this.extensionMap[i];
				if (string.Compare(extensionMapUnit.Extension.PhysicalPath, physicalPath, true, CultureInfo.InvariantCulture) == 0 && string.Compare(extensionMapUnit.Extension.GroupID, groupID, true, CultureInfo.InvariantCulture) == 0)
				{
					extensionMapUnit.Extension.Allow = allow;
					extensionMapUnit.Extension.UIDeletable = uiDeletable;
					extensionMapUnit.Extension.Description = description;
					flag = true;
				}
			}
			if (!flag)
			{
				IsapiExtension isapiExtension = new IsapiExtension(physicalPath, groupID, description, allow, uiDeletable);
				this.restrictionList.Add(isapiExtension.ToMetabaseString());
				this.extensionMap.Add(new IsapiExtensionList.ExtensionMapUnit(isapiExtension, this.restrictionList.Count - 1));
			}
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000910E4 File Offset: 0x0008F2E4
		public void RemoveAt(int i)
		{
			this.restrictionList.RemoveAt(this.extensionMap[i].RestrictionListIndex);
			this.extensionMap.RemoveAt(i);
			for (int j = 0; j < this.extensionMap.Count; j++)
			{
				IsapiExtensionList.ExtensionMapUnit value = this.extensionMap[j];
				if (value.RestrictionListIndex > i)
				{
					value.RestrictionListIndex--;
					this.extensionMap[j] = value;
				}
			}
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x00091162 File Offset: 0x0008F362
		public void CommitChanges()
		{
			this.rootEntry.CommitChanges();
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x00091170 File Offset: 0x0008F370
		public bool Exists(string groupID, string physicalPath)
		{
			List<int> list = this.FindMatchingExtensions(groupID, physicalPath, true);
			return list.Count > 0;
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x00091190 File Offset: 0x0008F390
		private List<int> FindMatchingExtensions(string groupID, string path, bool fullPath)
		{
			if (path == null || groupID == null)
			{
				return new List<int>(0);
			}
			List<int> list = new List<int>(this.extensionMap.Count);
			for (int i = 0; i < this.extensionMap.Count; i++)
			{
				if (string.Compare(groupID, this.extensionMap[i].Extension.GroupID, true, CultureInfo.InvariantCulture) == 0)
				{
					string text = this.extensionMap[i].Extension.PhysicalPath;
					if (!fullPath)
					{
						text = Path.GetFileName(text);
					}
					if (string.Compare(path, text, true, CultureInfo.InvariantCulture) == 0)
					{
						list.Add(i);
					}
				}
			}
			return list;
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x0009122D File Offset: 0x0008F42D
		public List<int> FindMatchingExtensions(string groupID, string executableName)
		{
			return this.FindMatchingExtensions(groupID, executableName, false);
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x00091238 File Offset: 0x0008F438
		public void RemoveByExecutable(string executableName)
		{
			for (int i = this.extensionMap.Count - 1; i >= 0; i--)
			{
				if (string.Compare(this.extensionMap[i].Extension.PhysicalPath, executableName, true, CultureInfo.InvariantCulture) == 0)
				{
					this.RemoveAt(i);
				}
			}
		}

		// Token: 0x04001CE4 RID: 7396
		private const string webServicesUri = "IIS://{0}/W3SVC";

		// Token: 0x04001CE5 RID: 7397
		private const int extraCapacity = 5;

		// Token: 0x04001CE6 RID: 7398
		private List<IsapiExtensionList.ExtensionMapUnit> extensionMap;

		// Token: 0x04001CE7 RID: 7399
		private PropertyValueCollection restrictionList;

		// Token: 0x04001CE8 RID: 7400
		private DirectoryEntry rootEntry;

		// Token: 0x02000413 RID: 1043
		private struct ExtensionMapUnit
		{
			// Token: 0x06002474 RID: 9332 RVA: 0x00091288 File Offset: 0x0008F488
			internal ExtensionMapUnit(IsapiExtension extension, int restrictionListIndex)
			{
				this.Extension = extension;
				this.RestrictionListIndex = restrictionListIndex;
			}

			// Token: 0x04001CE9 RID: 7401
			internal IsapiExtension Extension;

			// Token: 0x04001CEA RID: 7402
			internal int RestrictionListIndex;
		}
	}
}
