using System;
using System.Collections;
using System.DirectoryServices;
using System.IO;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004A5 RID: 1189
	internal class CreateVirtualDirectory
	{
		// Token: 0x060029ED RID: 10733 RVA: 0x000A67DE File Offset: 0x000A49DE
		public CreateVirtualDirectory()
		{
			this.Parent = "IIS://localhost/W3SVC/1/Root";
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000A6800 File Offset: 0x000A4A00
		public void Initialize()
		{
			if (this.Parent == null)
			{
				throw new ArgumentNullException(this.Parent, Strings.ErrorParentIISPathNull);
			}
			if (this.Name == null)
			{
				throw new ArgumentNullException(this.Name, Strings.ErrorVirtualDirectoryNameNull);
			}
			if (this.LocalPath != null && this.LocalPath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
			{
				this.LocalPath = null;
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

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x060029EF RID: 10735 RVA: 0x000A68E5 File Offset: 0x000A4AE5
		// (set) Token: 0x060029F0 RID: 10736 RVA: 0x000A68ED File Offset: 0x000A4AED
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

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x060029F1 RID: 10737 RVA: 0x000A68F6 File Offset: 0x000A4AF6
		// (set) Token: 0x060029F2 RID: 10738 RVA: 0x000A68FE File Offset: 0x000A4AFE
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

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x060029F3 RID: 10739 RVA: 0x000A6907 File Offset: 0x000A4B07
		// (set) Token: 0x060029F4 RID: 10740 RVA: 0x000A690F File Offset: 0x000A4B0F
		public string LocalPath
		{
			get
			{
				return this.localPath;
			}
			set
			{
				this.localPath = value;
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x060029F5 RID: 10741 RVA: 0x000A6918 File Offset: 0x000A4B18
		// (set) Token: 0x060029F6 RID: 10742 RVA: 0x000A6920 File Offset: 0x000A4B20
		public string ApplicationPool
		{
			get
			{
				return this.applicationPool;
			}
			set
			{
				this.applicationPool = value;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x000A6929 File Offset: 0x000A4B29
		// (set) Token: 0x060029F8 RID: 10744 RVA: 0x000A6931 File Offset: 0x000A4B31
		public MetabasePropertyTypes.AppPoolIdentityType AppPoolIdentityType
		{
			get
			{
				return this.appPoolIdentityType;
			}
			set
			{
				this.appPoolIdentityType = value;
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060029F9 RID: 10745 RVA: 0x000A693A File Offset: 0x000A4B3A
		// (set) Token: 0x060029FA RID: 10746 RVA: 0x000A6942 File Offset: 0x000A4B42
		public MetabasePropertyTypes.ManagedPipelineMode AppPoolManagedPipelineMode
		{
			get
			{
				return this.appPoolManagedPipelineMode;
			}
			set
			{
				this.appPoolManagedPipelineMode = value;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060029FB RID: 10747 RVA: 0x000A694B File Offset: 0x000A4B4B
		// (set) Token: 0x060029FC RID: 10748 RVA: 0x000A6953 File Offset: 0x000A4B53
		public int AppPoolQueueLength
		{
			get
			{
				return this.appPoolQueueLength;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("application pool cannot be 0 or negative.");
				}
				this.appPoolQueueLength = value;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x060029FD RID: 10749 RVA: 0x000A696B File Offset: 0x000A4B6B
		// (set) Token: 0x060029FE RID: 10750 RVA: 0x000A6973 File Offset: 0x000A4B73
		public long MaximumMemory { get; set; }

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060029FF RID: 10751 RVA: 0x000A697C File Offset: 0x000A4B7C
		// (set) Token: 0x06002A00 RID: 10752 RVA: 0x000A6984 File Offset: 0x000A4B84
		public long MaximumPrivateMemory { get; set; }

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x000A698D File Offset: 0x000A4B8D
		// (set) Token: 0x06002A02 RID: 10754 RVA: 0x000A6995 File Offset: 0x000A4B95
		public ICollection CustomizedVDirProperties
		{
			get
			{
				return this.customizedVDirProperties;
			}
			set
			{
				this.customizedVDirProperties = value;
			}
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x000A69A0 File Offset: 0x000A4BA0
		public void Execute()
		{
			DirectoryEntry directoryEntry = IisUtility.CreateWebDirObject(this.Parent, this.LocalPath, this.Name);
			if (this.CustomizedVDirProperties != null)
			{
				IisUtility.SetProperties(directoryEntry, this.CustomizedVDirProperties);
			}
			directoryEntry.CommitChanges();
			string hostName = IisUtility.GetHostName(this.Parent);
			IisUtility.CommitMetabaseChanges(hostName);
			if (this.ApplicationPool != null && IisUtility.IsSupportedIisVersion(hostName))
			{
				string appPoolRootPath = IisUtility.GetAppPoolRootPath(hostName);
				if (!IisUtility.Exists(appPoolRootPath, this.ApplicationPool, "IIsApplicationPool"))
				{
					using (DirectoryEntry directoryEntry2 = IisUtility.CreateApplicationPool(hostName, this.ApplicationPool))
					{
						IisUtility.SetProperty(directoryEntry2, "AppPoolIdentityType", (int)this.AppPoolIdentityType, true);
						IisUtility.SetProperty(directoryEntry2, "managedPipelineMode", (int)this.AppPoolManagedPipelineMode, true);
						if (this.AppPoolQueueLength != 0)
						{
							IisUtility.SetProperty(directoryEntry2, "AppPoolQueueLength", this.AppPoolQueueLength, true);
						}
						directoryEntry2.CommitChanges();
						IisUtility.CommitMetabaseChanges(hostName);
						using (ServerManager serverManager = new ServerManager())
						{
							ApplicationPool applicationPool = serverManager.ApplicationPools[this.ApplicationPool];
							applicationPool.ProcessModel.LoadUserProfile = true;
							if (this.MaximumMemory != 0L)
							{
								applicationPool.Recycling.PeriodicRestart.Memory = this.MaximumMemory;
							}
							if (this.MaximumPrivateMemory != 0L)
							{
								applicationPool.Recycling.PeriodicRestart.PrivateMemory = this.MaximumPrivateMemory;
							}
							serverManager.CommitChanges();
						}
					}
				}
				IisUtility.AssignApplicationPool(directoryEntry, this.ApplicationPool);
			}
		}

		// Token: 0x04001EC8 RID: 7880
		private string name;

		// Token: 0x04001EC9 RID: 7881
		private string parent;

		// Token: 0x04001ECA RID: 7882
		private string localPath;

		// Token: 0x04001ECB RID: 7883
		private string applicationPool;

		// Token: 0x04001ECC RID: 7884
		private MetabasePropertyTypes.AppPoolIdentityType appPoolIdentityType = MetabasePropertyTypes.AppPoolIdentityType.NetworkService;

		// Token: 0x04001ECD RID: 7885
		private MetabasePropertyTypes.ManagedPipelineMode appPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Classic;

		// Token: 0x04001ECE RID: 7886
		private int appPoolQueueLength;

		// Token: 0x04001ECF RID: 7887
		private ICollection customizedVDirProperties;
	}
}
