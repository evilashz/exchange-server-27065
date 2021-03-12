using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects
{
	// Token: 0x02000091 RID: 145
	internal interface IStorageObjectProperties
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060005D8 RID: 1496
		ICollection<PropertyDefinition> AllFoundProperties { get; }

		// Token: 0x060005D9 RID: 1497
		object TryGetProperty(PropertyDefinition propertyDefinition);

		// Token: 0x060005DA RID: 1498
		void SetProperty(PropertyDefinition propertyDefinition, object value);

		// Token: 0x060005DB RID: 1499
		void DeleteProperty(PropertyDefinition propertyDefinition);

		// Token: 0x060005DC RID: 1500
		Stream OpenStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode);

		// Token: 0x060005DD RID: 1501
		void Load(ICollection<PropertyDefinition> propertiesToLoad);

		// Token: 0x060005DE RID: 1502
		FolderSecurity.AclTableAndSecurityDescriptorProperty GetAclTableAndSecurityDescriptor();
	}
}
