using System;
using System.Collections;
using System.DirectoryServices;
using System.IO;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004A6 RID: 1190
	internal class DeleteVirtualDirectory
	{
		// Token: 0x06002A04 RID: 10756 RVA: 0x000A6B40 File Offset: 0x000A4D40
		public DeleteVirtualDirectory()
		{
			this.Parent = "IIS://localhost/W3SVC/1/Root";
			this.DeleteApplicationPool = false;
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x000A6B5C File Offset: 0x000A4D5C
		public void Initialize()
		{
			this.checkPoints = new Stack();
			if (this.Parent == null)
			{
				throw new ArgumentNullException(this.Parent, Strings.ErrorParentIISPathNull);
			}
			if (this.Name == null)
			{
				throw new ArgumentNullException(this.Name, Strings.ErrorVirtualDirectoryNameNull);
			}
			if (this.localPath != null && this.localPath.EndsWith("/"))
			{
				this.localPath = this.localPath.Substring(0, this.localPath.Length - 1);
			}
			if (this.Parent != null && this.Parent.Trim().EndsWith("/"))
			{
				this.Parent = this.Parent.Substring(0, this.Parent.Length - 1);
			}
			if (this.Name.IndexOfAny(IisUtility.GetReservedUriCharacters()) >= 0 || this.Name.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
			{
				throw new ArgumentException(Strings.ErrorInvalidCharacterInVirtualDirectoryName(this.Name));
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06002A06 RID: 10758 RVA: 0x000A6C63 File Offset: 0x000A4E63
		// (set) Token: 0x06002A07 RID: 10759 RVA: 0x000A6C6B File Offset: 0x000A4E6B
		public virtual string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x000A6C74 File Offset: 0x000A4E74
		// (set) Token: 0x06002A09 RID: 10761 RVA: 0x000A6C7C File Offset: 0x000A4E7C
		public string Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x000A6C85 File Offset: 0x000A4E85
		// (set) Token: 0x06002A0B RID: 10763 RVA: 0x000A6C8D File Offset: 0x000A4E8D
		public virtual bool DeleteApplicationPool
		{
			get
			{
				return this.deleteApplicationPool;
			}
			set
			{
				this.deleteApplicationPool = value;
			}
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x000A6C98 File Offset: 0x000A4E98
		private void SaveLocalPath()
		{
			DirectoryEntry directoryEntry;
			try
			{
				directoryEntry = IisUtility.FindWebObject(this.Parent, this.Name, "IIsWebVirtualDir");
			}
			catch (WebObjectNotFoundException)
			{
				return;
			}
			this.serverName = IisUtility.GetHostName(this.parent);
			if (WmiWrapper.IsDirectoryExisting(this.serverName, (string)directoryEntry.Properties["Path"].Value))
			{
				this.localPath = (string)directoryEntry.Properties["Path"].Value;
			}
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000A6D2C File Offset: 0x000A4F2C
		private void SaveAppPoolName()
		{
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(this.Parent))
			{
				DirectoryEntry directoryEntry2 = IisUtility.FindWebDirObject(this.Parent, this.Name);
				if (!string.Equals((string)directoryEntry.Properties["AppRoot"].Value, (string)directoryEntry2.Properties["AppRoot"].Value, StringComparison.InvariantCultureIgnoreCase) && !string.Equals((string)directoryEntry.Properties["AppPoolId"].Value, (string)directoryEntry2.Properties["AppPoolId"].Value, StringComparison.InvariantCultureIgnoreCase))
				{
					this.applicationPool = (string)directoryEntry2.Properties["AppPoolId"].Value;
				}
			}
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000A6E0C File Offset: 0x000A500C
		public void Execute()
		{
			this.checkPoints.Clear();
			this.SaveLocalPath();
			this.SaveAppPoolName();
			try
			{
				IisUtility.FindWebDirObject(this.Parent, this.Name);
				IisUtility.DeleteWebDirObject(this.Parent, this.Name);
				this.checkPoints.Push(DeleteVirtualDirectory.CheckPoint.VirtualDirectoryDeleted);
			}
			catch (WebObjectNotFoundException)
			{
			}
			string hostName = IisUtility.GetHostName(this.parent);
			if (this.applicationPool != null && IisUtility.IsSupportedIisVersion(hostName))
			{
				if (!this.DeleteApplicationPool)
				{
					if (!IisUtility.ApplicationPoolIsEmpty(this.applicationPool, hostName))
					{
						return;
					}
				}
				try
				{
					string appPoolRootPath = IisUtility.GetAppPoolRootPath(hostName);
					IisUtility.FindWebObject(appPoolRootPath, this.applicationPool, "IIsApplicationPool");
					IisUtility.DeleteApplicationPool(hostName, this.applicationPool);
					this.checkPoints.Push(DeleteVirtualDirectory.CheckPoint.ApplicationPoolDeleted);
				}
				catch (WebObjectNotFoundException)
				{
				}
			}
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000A6EF4 File Offset: 0x000A50F4
		public static void DeleteFromMetabase(string webSiteRoot, string virtualDirectoryName, ICollection childVirtualDirectoryNames)
		{
			if (childVirtualDirectoryNames != null)
			{
				string text = string.Format("{0}/{1}", webSiteRoot, virtualDirectoryName);
				foreach (object obj in childVirtualDirectoryNames)
				{
					string text2 = (string)obj;
					if (IisUtility.WebDirObjectExists(text, text2))
					{
						DeleteVirtualDirectory deleteVirtualDirectory = new DeleteVirtualDirectory();
						deleteVirtualDirectory.Name = text2;
						deleteVirtualDirectory.Parent = text;
						deleteVirtualDirectory.Initialize();
						deleteVirtualDirectory.Execute();
					}
				}
			}
			if (IisUtility.Exists(webSiteRoot, virtualDirectoryName, "IIsWebVirtualDir"))
			{
				DeleteVirtualDirectory deleteVirtualDirectory2 = new DeleteVirtualDirectory();
				deleteVirtualDirectory2.Name = virtualDirectoryName;
				deleteVirtualDirectory2.Parent = webSiteRoot;
				deleteVirtualDirectory2.Initialize();
				deleteVirtualDirectory2.Execute();
			}
		}

		// Token: 0x04001ED2 RID: 7890
		private Stack checkPoints;

		// Token: 0x04001ED3 RID: 7891
		private string serverName;

		// Token: 0x04001ED4 RID: 7892
		private string localPath;

		// Token: 0x04001ED5 RID: 7893
		private string applicationPool;

		// Token: 0x04001ED6 RID: 7894
		private string name;

		// Token: 0x04001ED7 RID: 7895
		private string parent;

		// Token: 0x04001ED8 RID: 7896
		private bool deleteApplicationPool;

		// Token: 0x020004A7 RID: 1191
		protected enum CheckPoint
		{
			// Token: 0x04001EDA RID: 7898
			VirtualDirectoryDeleted,
			// Token: 0x04001EDB RID: 7899
			ApplicationPoolDeleted
		}
	}
}
