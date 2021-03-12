using System;
using System.Collections.Generic;
using System.DirectoryServices;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Deployment.XforestTenantMigration
{
	// Token: 0x02000D7D RID: 3453
	[Serializable]
	public sealed class DirectoryObject
	{
		// Token: 0x1700292D RID: 10541
		// (get) Token: 0x06008487 RID: 33927 RVA: 0x0021DBDC File Offset: 0x0021BDDC
		public string ObjectClass
		{
			get
			{
				List<object> values = this.Properties["objectClass"].Values;
				return (string)values[values.Count - 1];
			}
		}

		// Token: 0x1700292E RID: 10542
		// (get) Token: 0x06008488 RID: 33928 RVA: 0x0021DC12 File Offset: 0x0021BE12
		// (set) Token: 0x06008489 RID: 33929 RVA: 0x0021DC2E File Offset: 0x0021BE2E
		public string DistinguishedName
		{
			get
			{
				return (string)this.Properties["distinguishedName"].Value;
			}
			set
			{
				if (this.Properties["distinguishedName"] != null)
				{
					this.Properties["distinguishedName"].Value = value;
				}
			}
		}

		// Token: 0x1700292F RID: 10543
		// (get) Token: 0x0600848A RID: 33930 RVA: 0x0021DC58 File Offset: 0x0021BE58
		// (set) Token: 0x0600848B RID: 33931 RVA: 0x0021DC60 File Offset: 0x0021BE60
		internal List<string> DependentObjects { get; set; }

		// Token: 0x17002930 RID: 10544
		// (get) Token: 0x0600848C RID: 33932 RVA: 0x0021DC69 File Offset: 0x0021BE69
		// (set) Token: 0x0600848D RID: 33933 RVA: 0x0021DC71 File Offset: 0x0021BE71
		internal List<string> DependedOnObjects { get; set; }

		// Token: 0x17002931 RID: 10545
		// (get) Token: 0x0600848E RID: 33934 RVA: 0x0021DC7A File Offset: 0x0021BE7A
		// (set) Token: 0x0600848F RID: 33935 RVA: 0x0021DC82 File Offset: 0x0021BE82
		public DirectoryPropertyCollection Properties { get; set; }

		// Token: 0x17002932 RID: 10546
		// (get) Token: 0x06008490 RID: 33936 RVA: 0x0021DC8B File Offset: 0x0021BE8B
		// (set) Token: 0x06008491 RID: 33937 RVA: 0x0021DC93 File Offset: 0x0021BE93
		internal DirectoryPropertyCollection DelayedProperties { get; set; }

		// Token: 0x17002933 RID: 10547
		// (get) Token: 0x06008492 RID: 33938 RVA: 0x0021DC9C File Offset: 0x0021BE9C
		public string ParentDistinguishedName
		{
			get
			{
				ADObjectId adobjectId = new ADObjectId(this.DistinguishedName);
				return adobjectId.AncestorDN(1).ToDNString();
			}
		}

		// Token: 0x06008493 RID: 33939 RVA: 0x0021DCC1 File Offset: 0x0021BEC1
		public DirectoryObject()
		{
			this.Properties = new DirectoryPropertyCollection();
			this.DependentObjects = new List<string>();
			this.DependedOnObjects = new List<string>();
			this.DelayedProperties = new DirectoryPropertyCollection();
		}

		// Token: 0x06008494 RID: 33940 RVA: 0x0021DCF5 File Offset: 0x0021BEF5
		public DirectoryObject(string searchRoot, SearchResult searchResult) : this()
		{
			if (searchRoot == null)
			{
				throw new ArgumentNullException("searchRoot");
			}
			if (searchResult == null)
			{
				throw new ArgumentNullException("searchResult");
			}
			this.Properties = new DirectoryPropertyCollection(searchRoot, searchResult.Properties);
		}

		// Token: 0x06008495 RID: 33941 RVA: 0x0021DD2C File Offset: 0x0021BF2C
		internal void Replace(string oldValue, string newValue)
		{
			this.DistinguishedName = this.DistinguishedName.ToString().Replace(oldValue, newValue);
			foreach (DirectoryProperty directoryProperty in this.Properties)
			{
				for (int i = 0; i < directoryProperty.Values.Count; i++)
				{
					if (directoryProperty[i].ToString().Contains(oldValue))
					{
						directoryProperty[i] = directoryProperty[i].ToString().Replace(oldValue, newValue);
					}
				}
			}
		}
	}
}
