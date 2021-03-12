using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects
{
	// Token: 0x02000092 RID: 146
	internal class CoreObjectProperties : IStorageObjectProperties
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x00028C54 File Offset: 0x00026E54
		public CoreObjectProperties(ICorePropertyBag corePropertyBag)
		{
			Util.ThrowOnNullArgument(corePropertyBag, "corePropertyBag");
			this.corePropertyBag = corePropertyBag;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00028C6E File Offset: 0x00026E6E
		protected ICorePropertyBag CorePropertyBag
		{
			get
			{
				return this.corePropertyBag;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00028C76 File Offset: 0x00026E76
		public ICollection<PropertyDefinition> AllFoundProperties
		{
			get
			{
				return this.corePropertyBag.AllFoundProperties;
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00028C83 File Offset: 0x00026E83
		public virtual void SetProperty(PropertyDefinition propertyDefinition, object value)
		{
			this.corePropertyBag.SetProperty(propertyDefinition, value);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00028C92 File Offset: 0x00026E92
		public virtual void DeleteProperty(PropertyDefinition propertyDefinition)
		{
			this.corePropertyBag.Delete(propertyDefinition);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00028CA0 File Offset: 0x00026EA0
		public virtual Stream OpenStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
		{
			return this.corePropertyBag.OpenPropertyStream(propertyDefinition, openMode);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00028CAF File Offset: 0x00026EAF
		public void Load(ICollection<PropertyDefinition> propertiesToLoad)
		{
			this.corePropertyBag.Load(propertiesToLoad);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00028CBD File Offset: 0x00026EBD
		public virtual object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			return this.corePropertyBag.TryGetProperty(propertyDefinition);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00028CCB File Offset: 0x00026ECB
		public FolderSecurity.AclTableAndSecurityDescriptorProperty GetAclTableAndSecurityDescriptor()
		{
			return AclModifyTable.ReadAclTableAndSecurityDescriptor(this.corePropertyBag);
		}

		// Token: 0x04000274 RID: 628
		private readonly ICorePropertyBag corePropertyBag;
	}
}
