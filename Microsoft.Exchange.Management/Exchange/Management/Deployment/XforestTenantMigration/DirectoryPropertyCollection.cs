using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace Microsoft.Exchange.Management.Deployment.XforestTenantMigration
{
	// Token: 0x02000D80 RID: 3456
	[Serializable]
	public sealed class DirectoryPropertyCollection : List<DirectoryProperty>
	{
		// Token: 0x060084AE RID: 33966 RVA: 0x0021E0A3 File Offset: 0x0021C2A3
		public DirectoryPropertyCollection()
		{
		}

		// Token: 0x060084AF RID: 33967 RVA: 0x0021E0AC File Offset: 0x0021C2AC
		public DirectoryPropertyCollection(string searchRoot, ResultPropertyCollection properties)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			foreach (object obj in properties)
			{
				DictionaryEntry value = (DictionaryEntry)obj;
				base.Add(new DirectoryProperty(searchRoot, value));
			}
		}

		// Token: 0x1700293E RID: 10558
		public DirectoryProperty this[string name]
		{
			get
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new ArgumentNullException("name");
				}
				return this.FirstOrDefault((DirectoryProperty p) => p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
			}
		}

		// Token: 0x060084B1 RID: 33969 RVA: 0x0021E17C File Offset: 0x0021C37C
		public bool Contains(string propertyName)
		{
			return this[propertyName] != null;
		}
	}
}
