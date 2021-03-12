using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Microsoft.Exchange.VariantConfiguration.DataLoad
{
	// Token: 0x0200000A RID: 10
	internal class ResourcesDataSourceReader : IDataSourceReader
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000029F4 File Offset: 0x00000BF4
		internal ResourcesDataSourceReader(Assembly resourcesAssembly)
		{
			if (resourcesAssembly == null)
			{
				throw new ArgumentNullException("resourcesAssembly");
			}
			this.resourcesAssembly = resourcesAssembly;
			this.ResourceNames = resourcesAssembly.GetManifestResourceNames();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002A23 File Offset: 0x00000C23
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002A2B File Offset: 0x00000C2B
		internal IEnumerable<string> ResourceNames { get; private set; }

		// Token: 0x0600002B RID: 43 RVA: 0x00002A68 File Offset: 0x00000C68
		public Func<TextReader> GetContentReader(string dataSource)
		{
			if (string.IsNullOrEmpty(dataSource))
			{
				throw new ArgumentNullException("dataSource");
			}
			string adjustedDataSource = this.ResourceNames.FirstOrDefault((string name) => string.Equals(name, dataSource, StringComparison.OrdinalIgnoreCase));
			if (string.IsNullOrEmpty(adjustedDataSource))
			{
				throw new ArgumentException(string.Format("Data source '{0}' could not be read from resources", dataSource));
			}
			return () => new StreamReader(this.resourcesAssembly.GetManifestResourceStream(adjustedDataSource));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002AED File Offset: 0x00000CED
		public bool CanGetContentReader(string dataSource)
		{
			return this.ResourceNames.Contains(dataSource, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x04000013 RID: 19
		private readonly Assembly resourcesAssembly;
	}
}
