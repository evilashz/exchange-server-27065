using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Ceres.CoreServices.Services.Package;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x02000004 RID: 4
	internal class DictionaryPackageManager : IPackageManager, IDictionary<string, IPackage>, ICollection<KeyValuePair<string, IPackage>>, IEnumerable<KeyValuePair<string, IPackage>>, IEnumerable
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002BDB File Offset: 0x00000DDB
		public DictionaryPackageManager(string[] rootPath)
		{
			this.rootDirectories = rootPath;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002BEA File Offset: 0x00000DEA
		public int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002BF1 File Offset: 0x00000DF1
		public IDictionary<string, IPackage> Packages
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public ICollection<string> Keys
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002BFB File Offset: 0x00000DFB
		public ICollection<IPackage> Values
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002C02 File Offset: 0x00000E02
		public bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700000E RID: 14
		IPackage IDictionary<string, IPackage>.this[string key]
		{
			get
			{
				return this.Get(key);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700000F RID: 15
		public IPackage this[AssemblyName packageName]
		{
			get
			{
				return this.Get(packageName.FullName);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002C27 File Offset: 0x00000E27
		public IEnumerable<IPackage> GetBundles(Predicate<IPackage> filter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002C2E File Offset: 0x00000E2E
		public IEnumerable<IPackage> SelectPackages(Predicate<IPackage> filter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002C35 File Offset: 0x00000E35
		public IEnumerable<IPackage> SelectPackages(Predicate<IPackage> filter, SelectOptions options)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C3C File Offset: 0x00000E3C
		public IEnumerable<IPackage> SelectPackages(AssemblyName partialPackageName, Predicate<IPackage> filter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002C43 File Offset: 0x00000E43
		public IEnumerable<IPackage> SelectPackages(AssemblyName partialPackageName, Predicate<IPackage> filter, SelectOptions options)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002C4A File Offset: 0x00000E4A
		public IEnumerable<IPackage> SelectPackages(string simpleNamePattern, Predicate<IPackage> filter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002C51 File Offset: 0x00000E51
		public IEnumerable<IPackage> SelectPackages(string simpleNamePattern, Predicate<IPackage> filter, SelectOptions options)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002E30 File Offset: 0x00001030
		public IEnumerable<IPackage> SelectPackages(string simpleNamePattern)
		{
			string bundleFile = simpleNamePattern + ".dll";
			foreach (string rootDirectory in this.rootDirectories)
			{
				if (File.Exists(Path.Combine(rootDirectory, bundleFile)))
				{
					Assembly assembly = Assembly.LoadFrom(Path.Combine(rootDirectory, bundleFile));
					yield return new DictionaryPackageManager.DictionaryPackage(assembly);
				}
			}
			yield break;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002F64 File Offset: 0x00001164
		public IEnumerable<IPackage> SelectPackages(string simpleNamePattern, SelectOptions options)
		{
			if (options == 1)
			{
				IPackage pkg;
				this.Packages.TryGetValue(simpleNamePattern, out pkg);
				yield return pkg;
				yield break;
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002F8F File Offset: 0x0000118F
		public long RegisterDeploymentCallback(Action<DeploymentNotificationAction> callback, Predicate<IPackage> filter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002F96 File Offset: 0x00001196
		public long RegisterDeploymentCallback(Action<DeploymentNotificationAction> callback, AssemblyName partialPackageName, Predicate<IPackage> filter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002F9D File Offset: 0x0000119D
		public long RegisterDeploymentCallback(Action<DeploymentNotificationAction> callback, string simpleNamePattern, Predicate<IPackage> filter)
		{
			return -1L;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002FA1 File Offset: 0x000011A1
		public bool UnRegisterDeploymentCallback(long deploymentCallbackId)
		{
			return true;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002FA4 File Offset: 0x000011A4
		public void Deploy(AssemblyName name, string path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002FAB File Offset: 0x000011AB
		public void Deploy(AssemblyName name, Stream stream)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002FB2 File Offset: 0x000011B2
		public void Deploy(IPackage packageWrapper)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002FB9 File Offset: 0x000011B9
		public void UnDeploy(AssemblyName name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002FC0 File Offset: 0x000011C0
		public void UnDeploy(IPackage packageWrapper)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002FC7 File Offset: 0x000011C7
		public IEnumerator<KeyValuePair<string, IPackage>> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002FCE File Offset: 0x000011CE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002FD6 File Offset: 0x000011D6
		public void Add(KeyValuePair<string, IPackage> item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002FDD File Offset: 0x000011DD
		public void Clear()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002FE4 File Offset: 0x000011E4
		public bool Contains(KeyValuePair<string, IPackage> item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002FEB File Offset: 0x000011EB
		public void CopyTo(KeyValuePair<string, IPackage>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002FF2 File Offset: 0x000011F2
		public bool Remove(KeyValuePair<string, IPackage> item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002FF9 File Offset: 0x000011F9
		public bool ContainsKey(string key)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003000 File Offset: 0x00001200
		public void Add(string key, IPackage value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003007 File Offset: 0x00001207
		public bool Remove(string key)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000300E File Offset: 0x0000120E
		public bool TryGetValue(string key, out IPackage value)
		{
			value = this.Get(key);
			return true;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000301C File Offset: 0x0000121C
		private IPackage Get(string bundleIdFullName)
		{
			string path = bundleIdFullName + ".dll";
			foreach (string path2 in this.rootDirectories)
			{
				string text = Path.Combine(path2, path);
				if (File.Exists(text))
				{
					Assembly assembly = Assembly.LoadFrom(text);
					return new DictionaryPackageManager.DictionaryPackage(assembly);
				}
			}
			throw new ArgumentException(string.Concat(new string[]
			{
				"Bundle: ",
				bundleIdFullName,
				" could not be found (Search Paths: ",
				string.Join(";", this.rootDirectories),
				")"
			}));
		}

		// Token: 0x0400001B RID: 27
		private readonly string[] rootDirectories;

		// Token: 0x02000005 RID: 5
		private class DictionaryPackage : IPackage
		{
			// Token: 0x0600003F RID: 63 RVA: 0x000030C2 File Offset: 0x000012C2
			public DictionaryPackage(Assembly assembly)
			{
				this.assembly = assembly;
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000040 RID: 64 RVA: 0x000030D1 File Offset: 0x000012D1
			public int PropertyCount
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000041 RID: 65 RVA: 0x000030D8 File Offset: 0x000012D8
			public IEnumerable<string> Resources
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000042 RID: 66 RVA: 0x000030DF File Offset: 0x000012DF
			public IEnumerable<IPackage> ReferencedPackages
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000043 RID: 67 RVA: 0x000030E6 File Offset: 0x000012E6
			public IEnumerable<IPackage> AllReferencedPackages
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000044 RID: 68 RVA: 0x000030ED File Offset: 0x000012ED
			public IEnumerable<AssemblyName> References
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000045 RID: 69 RVA: 0x000030F4 File Offset: 0x000012F4
			public IEnumerable<IPackage> Dependencies
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000046 RID: 70 RVA: 0x000030FB File Offset: 0x000012FB
			public IEnumerable<IPackage> AllDependencies
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000047 RID: 71 RVA: 0x00003102 File Offset: 0x00001302
			public IEnumerable<AssemblyName> DependencyIdentifiers
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000048 RID: 72 RVA: 0x00003109 File Offset: 0x00001309
			public int DependencyCount
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000049 RID: 73 RVA: 0x00003110 File Offset: 0x00001310
			public AssemblyName Name
			{
				get
				{
					return this.assembly.GetName();
				}
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x0600004A RID: 74 RVA: 0x0000311D File Offset: 0x0000131D
			public string Description
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x0600004B RID: 75 RVA: 0x00003124 File Offset: 0x00001324
			public IDictionary<string, string> Properties
			{
				get
				{
					this.GetPropertiesIfNeeded();
					return this.properties;
				}
			}

			// Token: 0x1700001C RID: 28
			public string this[string propertyName]
			{
				get
				{
					this.GetPropertiesIfNeeded();
					string result;
					this.properties.TryGetValue(propertyName, out result);
					return result;
				}
			}

			// Token: 0x0600004D RID: 77 RVA: 0x00003157 File Offset: 0x00001357
			public bool HasProperty(string propertyName)
			{
				return this.Properties.ContainsKey(propertyName);
			}

			// Token: 0x0600004E RID: 78 RVA: 0x00003165 File Offset: 0x00001365
			public bool HasPropertyWithValue(string propertyName, string propertyValue)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600004F RID: 79 RVA: 0x0000316C File Offset: 0x0000136C
			public Stream OpenResource(string resourceName)
			{
				return this.assembly.GetManifestResourceStream(resourceName);
			}

			// Token: 0x06000050 RID: 80 RVA: 0x0000317A File Offset: 0x0000137A
			public bool HasResource(string resourceName)
			{
				this.GetResourcesIfNeeded();
				return this.resourceNames.Contains(resourceName);
			}

			// Token: 0x06000051 RID: 81 RVA: 0x0000318E File Offset: 0x0000138E
			public void Unpack(string path)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000052 RID: 82 RVA: 0x00003195 File Offset: 0x00001395
			public string UnpackedPath()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000053 RID: 83 RVA: 0x0000319C File Offset: 0x0000139C
			public string UnpackedPath(string resourceName)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000054 RID: 84 RVA: 0x000031A3 File Offset: 0x000013A3
			private void GetResourcesIfNeeded()
			{
				if (this.resourceNames != null)
				{
					return;
				}
				this.resourceNames = this.assembly.GetManifestResourceNames().ToList<string>();
			}

			// Token: 0x06000055 RID: 85 RVA: 0x000031C4 File Offset: 0x000013C4
			private void GetPropertiesIfNeeded()
			{
				if (this.properties != null)
				{
					return;
				}
				IDictionary<string, string> dictionary = new Dictionary<string, string>();
				IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(this.assembly);
				foreach (CustomAttributeData customAttributeData in customAttributes)
				{
					if (customAttributeData.Constructor.DeclaringType.FullName.Equals(typeof(PackagePropertyAttribute).FullName))
					{
						string value = (customAttributeData.ConstructorArguments[1].Value == null) ? null : customAttributeData.ConstructorArguments[1].Value.ToString();
						dictionary.Add(customAttributeData.ConstructorArguments[0].Value.ToString(), value);
					}
				}
				this.properties = dictionary;
			}

			// Token: 0x0400001C RID: 28
			private readonly Assembly assembly;

			// Token: 0x0400001D RID: 29
			private IDictionary<string, string> properties;

			// Token: 0x0400001E RID: 30
			private IList<string> resourceNames;
		}
	}
}
