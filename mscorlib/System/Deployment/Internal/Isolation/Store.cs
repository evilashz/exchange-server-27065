using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000681 RID: 1665
	internal class Store
	{
		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06004EC2 RID: 20162 RVA: 0x001182E4 File Offset: 0x001164E4
		public IStore InternalStore
		{
			get
			{
				return this._pStore;
			}
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x001182EC File Offset: 0x001164EC
		public Store(IStore pStore)
		{
			if (pStore == null)
			{
				throw new ArgumentNullException("pStore");
			}
			this._pStore = pStore;
		}

		// Token: 0x06004EC4 RID: 20164 RVA: 0x0011830C File Offset: 0x0011650C
		[SecuritySafeCritical]
		public uint[] Transact(StoreTransactionOperation[] operations)
		{
			if (operations == null || operations.Length == 0)
			{
				throw new ArgumentException("operations");
			}
			uint[] array = new uint[operations.Length];
			int[] rgResults = new int[operations.Length];
			this._pStore.Transact(new IntPtr(operations.Length), operations, array, rgResults);
			return array;
		}

		// Token: 0x06004EC5 RID: 20165 RVA: 0x00118354 File Offset: 0x00116554
		[SecuritySafeCritical]
		public IDefinitionIdentity BindReferenceToAssemblyIdentity(uint Flags, IReferenceIdentity ReferenceIdentity, uint cDeploymentsToIgnore, IDefinitionIdentity[] DefinitionIdentity_DeploymentsToIgnore)
		{
			Guid iid_IDefinitionIdentity = IsolationInterop.IID_IDefinitionIdentity;
			object obj = this._pStore.BindReferenceToAssembly(Flags, ReferenceIdentity, cDeploymentsToIgnore, DefinitionIdentity_DeploymentsToIgnore, ref iid_IDefinitionIdentity);
			return (IDefinitionIdentity)obj;
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x00118380 File Offset: 0x00116580
		[SecuritySafeCritical]
		public void CalculateDelimiterOfDeploymentsBasedOnQuota(uint dwFlags, uint cDeployments, IDefinitionAppId[] rgpIDefinitionAppId_Deployments, ref StoreApplicationReference InstallerReference, ulong ulonglongQuota, ref uint Delimiter, ref ulong SizeSharedWithExternalDeployment, ref ulong SizeConsumedByInputDeploymentArray)
		{
			IntPtr zero = IntPtr.Zero;
			this._pStore.CalculateDelimiterOfDeploymentsBasedOnQuota(dwFlags, new IntPtr((long)((ulong)cDeployments)), rgpIDefinitionAppId_Deployments, ref InstallerReference, ulonglongQuota, ref zero, ref SizeSharedWithExternalDeployment, ref SizeConsumedByInputDeploymentArray);
			Delimiter = (uint)zero.ToInt64();
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x001183BC File Offset: 0x001165BC
		[SecuritySafeCritical]
		public ICMS BindReferenceToAssemblyManifest(uint Flags, IReferenceIdentity ReferenceIdentity, uint cDeploymentsToIgnore, IDefinitionIdentity[] DefinitionIdentity_DeploymentsToIgnore)
		{
			Guid iid_ICMS = IsolationInterop.IID_ICMS;
			object obj = this._pStore.BindReferenceToAssembly(Flags, ReferenceIdentity, cDeploymentsToIgnore, DefinitionIdentity_DeploymentsToIgnore, ref iid_ICMS);
			return (ICMS)obj;
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x001183E8 File Offset: 0x001165E8
		[SecuritySafeCritical]
		public ICMS GetAssemblyManifest(uint Flags, IDefinitionIdentity DefinitionIdentity)
		{
			Guid iid_ICMS = IsolationInterop.IID_ICMS;
			object assemblyInformation = this._pStore.GetAssemblyInformation(Flags, DefinitionIdentity, ref iid_ICMS);
			return (ICMS)assemblyInformation;
		}

		// Token: 0x06004EC9 RID: 20169 RVA: 0x00118414 File Offset: 0x00116614
		[SecuritySafeCritical]
		public IDefinitionIdentity GetAssemblyIdentity(uint Flags, IDefinitionIdentity DefinitionIdentity)
		{
			Guid iid_IDefinitionIdentity = IsolationInterop.IID_IDefinitionIdentity;
			object assemblyInformation = this._pStore.GetAssemblyInformation(Flags, DefinitionIdentity, ref iid_IDefinitionIdentity);
			return (IDefinitionIdentity)assemblyInformation;
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x0011843D File Offset: 0x0011663D
		public StoreAssemblyEnumeration EnumAssemblies(Store.EnumAssembliesFlags Flags)
		{
			return this.EnumAssemblies(Flags, null);
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x00118448 File Offset: 0x00116648
		[SecuritySafeCritical]
		public StoreAssemblyEnumeration EnumAssemblies(Store.EnumAssembliesFlags Flags, IReferenceIdentity refToMatch)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY));
			object obj = this._pStore.EnumAssemblies((uint)Flags, refToMatch, ref guidOfType);
			return new StoreAssemblyEnumeration((IEnumSTORE_ASSEMBLY)obj);
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x00118480 File Offset: 0x00116680
		[SecuritySafeCritical]
		public StoreAssemblyFileEnumeration EnumFiles(Store.EnumAssemblyFilesFlags Flags, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));
			object obj = this._pStore.EnumFiles((uint)Flags, Assembly, ref guidOfType);
			return new StoreAssemblyFileEnumeration((IEnumSTORE_ASSEMBLY_FILE)obj);
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x001184B8 File Offset: 0x001166B8
		[SecuritySafeCritical]
		public StoreAssemblyFileEnumeration EnumPrivateFiles(Store.EnumApplicationPrivateFiles Flags, IDefinitionAppId Application, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));
			object obj = this._pStore.EnumPrivateFiles((uint)Flags, Application, Assembly, ref guidOfType);
			return new StoreAssemblyFileEnumeration((IEnumSTORE_ASSEMBLY_FILE)obj);
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x001184F4 File Offset: 0x001166F4
		[SecuritySafeCritical]
		public IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE EnumInstallationReferences(Store.EnumAssemblyInstallReferenceFlags Flags, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE));
			object obj = this._pStore.EnumInstallationReferences((uint)Flags, Assembly, ref guidOfType);
			return (IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE)obj;
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x00118528 File Offset: 0x00116728
		[SecuritySafeCritical]
		public Store.IPathLock LockAssemblyPath(IDefinitionIdentity asm)
		{
			IntPtr c;
			string path = this._pStore.LockAssemblyPath(0U, asm, out c);
			return new Store.AssemblyPathLock(this._pStore, c, path);
		}

		// Token: 0x06004ED0 RID: 20176 RVA: 0x00118554 File Offset: 0x00116754
		[SecuritySafeCritical]
		public Store.IPathLock LockApplicationPath(IDefinitionAppId app)
		{
			IntPtr c;
			string path = this._pStore.LockApplicationPath(0U, app, out c);
			return new Store.ApplicationPathLock(this._pStore, c, path);
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x00118580 File Offset: 0x00116780
		[SecuritySafeCritical]
		public ulong QueryChangeID(IDefinitionIdentity asm)
		{
			return this._pStore.QueryChangeID(asm);
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x0011859C File Offset: 0x0011679C
		[SecuritySafeCritical]
		public StoreCategoryEnumeration EnumCategories(Store.EnumCategoriesFlags Flags, IReferenceIdentity CategoryMatch)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY));
			object obj = this._pStore.EnumCategories((uint)Flags, CategoryMatch, ref guidOfType);
			return new StoreCategoryEnumeration((IEnumSTORE_CATEGORY)obj);
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x001185D4 File Offset: 0x001167D4
		public StoreSubcategoryEnumeration EnumSubcategories(Store.EnumSubcategoriesFlags Flags, IDefinitionIdentity CategoryMatch)
		{
			return this.EnumSubcategories(Flags, CategoryMatch, null);
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x001185E0 File Offset: 0x001167E0
		[SecuritySafeCritical]
		public StoreSubcategoryEnumeration EnumSubcategories(Store.EnumSubcategoriesFlags Flags, IDefinitionIdentity Category, string SearchPattern)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_SUBCATEGORY));
			object obj = this._pStore.EnumSubcategories((uint)Flags, Category, SearchPattern, ref guidOfType);
			return new StoreSubcategoryEnumeration((IEnumSTORE_CATEGORY_SUBCATEGORY)obj);
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x0011861C File Offset: 0x0011681C
		[SecuritySafeCritical]
		public StoreCategoryInstanceEnumeration EnumCategoryInstances(Store.EnumCategoryInstancesFlags Flags, IDefinitionIdentity Category, string SubCat)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_INSTANCE));
			object obj = this._pStore.EnumCategoryInstances((uint)Flags, Category, SubCat, ref guidOfType);
			return new StoreCategoryInstanceEnumeration((IEnumSTORE_CATEGORY_INSTANCE)obj);
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x00118658 File Offset: 0x00116858
		[SecurityCritical]
		public byte[] GetDeploymentProperty(Store.GetPackagePropertyFlags Flags, IDefinitionAppId Deployment, StoreApplicationReference Reference, Guid PropertySet, string PropertyName)
		{
			BLOB blob = default(BLOB);
			byte[] array = null;
			try
			{
				this._pStore.GetDeploymentProperty((uint)Flags, Deployment, ref Reference, ref PropertySet, PropertyName, out blob);
				array = new byte[blob.Size];
				Marshal.Copy(blob.BlobData, array, 0, (int)blob.Size);
			}
			finally
			{
				blob.Dispose();
			}
			return array;
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x001186C0 File Offset: 0x001168C0
		[SecuritySafeCritical]
		public StoreDeploymentMetadataEnumeration EnumInstallerDeployments(Guid InstallerId, string InstallerName, string InstallerMetadata, IReferenceAppId DeploymentFilter)
		{
			StoreApplicationReference storeApplicationReference = new StoreApplicationReference(InstallerId, InstallerName, InstallerMetadata);
			object obj = this._pStore.EnumInstallerDeploymentMetadata(0U, ref storeApplicationReference, DeploymentFilter, ref IsolationInterop.IID_IEnumSTORE_DEPLOYMENT_METADATA);
			return new StoreDeploymentMetadataEnumeration((IEnumSTORE_DEPLOYMENT_METADATA)obj);
		}

		// Token: 0x06004ED8 RID: 20184 RVA: 0x001186FC File Offset: 0x001168FC
		[SecuritySafeCritical]
		public StoreDeploymentMetadataPropertyEnumeration EnumInstallerDeploymentProperties(Guid InstallerId, string InstallerName, string InstallerMetadata, IDefinitionAppId Deployment)
		{
			StoreApplicationReference storeApplicationReference = new StoreApplicationReference(InstallerId, InstallerName, InstallerMetadata);
			object obj = this._pStore.EnumInstallerDeploymentMetadataProperties(0U, ref storeApplicationReference, Deployment, ref IsolationInterop.IID_IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY);
			return new StoreDeploymentMetadataPropertyEnumeration((IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY)obj);
		}

		// Token: 0x040021AC RID: 8620
		private IStore _pStore;

		// Token: 0x02000C20 RID: 3104
		[Flags]
		public enum EnumAssembliesFlags
		{
			// Token: 0x040036AE RID: 13998
			Nothing = 0,
			// Token: 0x040036AF RID: 13999
			VisibleOnly = 1,
			// Token: 0x040036B0 RID: 14000
			MatchServicing = 2,
			// Token: 0x040036B1 RID: 14001
			ForceLibrarySemantics = 4
		}

		// Token: 0x02000C21 RID: 3105
		[Flags]
		public enum EnumAssemblyFilesFlags
		{
			// Token: 0x040036B3 RID: 14003
			Nothing = 0,
			// Token: 0x040036B4 RID: 14004
			IncludeInstalled = 1,
			// Token: 0x040036B5 RID: 14005
			IncludeMissing = 2
		}

		// Token: 0x02000C22 RID: 3106
		[Flags]
		public enum EnumApplicationPrivateFiles
		{
			// Token: 0x040036B7 RID: 14007
			Nothing = 0,
			// Token: 0x040036B8 RID: 14008
			IncludeInstalled = 1,
			// Token: 0x040036B9 RID: 14009
			IncludeMissing = 2
		}

		// Token: 0x02000C23 RID: 3107
		[Flags]
		public enum EnumAssemblyInstallReferenceFlags
		{
			// Token: 0x040036BB RID: 14011
			Nothing = 0
		}

		// Token: 0x02000C24 RID: 3108
		public interface IPathLock : IDisposable
		{
			// Token: 0x17001334 RID: 4916
			// (get) Token: 0x06006F41 RID: 28481
			string Path { get; }
		}

		// Token: 0x02000C25 RID: 3109
		private class AssemblyPathLock : Store.IPathLock, IDisposable
		{
			// Token: 0x06006F42 RID: 28482 RVA: 0x0017E6EE File Offset: 0x0017C8EE
			public AssemblyPathLock(IStore s, IntPtr c, string path)
			{
				this._pSourceStore = s;
				this._pLockCookie = c;
				this._path = path;
			}

			// Token: 0x06006F43 RID: 28483 RVA: 0x0017E716 File Offset: 0x0017C916
			[SecuritySafeCritical]
			private void Dispose(bool fDisposing)
			{
				if (fDisposing)
				{
					GC.SuppressFinalize(this);
				}
				if (this._pLockCookie != IntPtr.Zero)
				{
					this._pSourceStore.ReleaseAssemblyPath(this._pLockCookie);
					this._pLockCookie = IntPtr.Zero;
				}
			}

			// Token: 0x06006F44 RID: 28484 RVA: 0x0017E750 File Offset: 0x0017C950
			~AssemblyPathLock()
			{
				this.Dispose(false);
			}

			// Token: 0x06006F45 RID: 28485 RVA: 0x0017E780 File Offset: 0x0017C980
			void IDisposable.Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x17001335 RID: 4917
			// (get) Token: 0x06006F46 RID: 28486 RVA: 0x0017E789 File Offset: 0x0017C989
			public string Path
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x040036BC RID: 14012
			private IStore _pSourceStore;

			// Token: 0x040036BD RID: 14013
			private IntPtr _pLockCookie = IntPtr.Zero;

			// Token: 0x040036BE RID: 14014
			private string _path;
		}

		// Token: 0x02000C26 RID: 3110
		private class ApplicationPathLock : Store.IPathLock, IDisposable
		{
			// Token: 0x06006F47 RID: 28487 RVA: 0x0017E791 File Offset: 0x0017C991
			public ApplicationPathLock(IStore s, IntPtr c, string path)
			{
				this._pSourceStore = s;
				this._pLockCookie = c;
				this._path = path;
			}

			// Token: 0x06006F48 RID: 28488 RVA: 0x0017E7B9 File Offset: 0x0017C9B9
			[SecuritySafeCritical]
			private void Dispose(bool fDisposing)
			{
				if (fDisposing)
				{
					GC.SuppressFinalize(this);
				}
				if (this._pLockCookie != IntPtr.Zero)
				{
					this._pSourceStore.ReleaseApplicationPath(this._pLockCookie);
					this._pLockCookie = IntPtr.Zero;
				}
			}

			// Token: 0x06006F49 RID: 28489 RVA: 0x0017E7F4 File Offset: 0x0017C9F4
			~ApplicationPathLock()
			{
				this.Dispose(false);
			}

			// Token: 0x06006F4A RID: 28490 RVA: 0x0017E824 File Offset: 0x0017CA24
			void IDisposable.Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x17001336 RID: 4918
			// (get) Token: 0x06006F4B RID: 28491 RVA: 0x0017E82D File Offset: 0x0017CA2D
			public string Path
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x040036BF RID: 14015
			private IStore _pSourceStore;

			// Token: 0x040036C0 RID: 14016
			private IntPtr _pLockCookie = IntPtr.Zero;

			// Token: 0x040036C1 RID: 14017
			private string _path;
		}

		// Token: 0x02000C27 RID: 3111
		[Flags]
		public enum EnumCategoriesFlags
		{
			// Token: 0x040036C3 RID: 14019
			Nothing = 0
		}

		// Token: 0x02000C28 RID: 3112
		[Flags]
		public enum EnumSubcategoriesFlags
		{
			// Token: 0x040036C5 RID: 14021
			Nothing = 0
		}

		// Token: 0x02000C29 RID: 3113
		[Flags]
		public enum EnumCategoryInstancesFlags
		{
			// Token: 0x040036C7 RID: 14023
			Nothing = 0
		}

		// Token: 0x02000C2A RID: 3114
		[Flags]
		public enum GetPackagePropertyFlags
		{
			// Token: 0x040036C9 RID: 14025
			Nothing = 0
		}
	}
}
