using System;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000041 RID: 65
	internal sealed class FolderSecurityDescriptorConversion : PropertyConversion
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x00017EA8 File Offset: 0x000160A8
		internal FolderSecurityDescriptorConversion() : base(PropertyTag.NTSecurityDescriptor, PropertyTag.AclTableAndSecurityDescriptor)
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00017EBC File Offset: 0x000160BC
		protected override object ConvertValueFromClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSecurityDescriptor = storageObjectProperties.GetAclTableAndSecurityDescriptor();
			RawSecurityDescriptor securityDescriptor = SecurityDescriptorForTransfer.ExtractSecurityDescriptor((byte[])propertyValue);
			return AclModifyTable.BuildAclTableBlob(session, securityDescriptor, (aclTableAndSecurityDescriptor.FreeBusySecurityDescriptor != null) ? aclTableAndSecurityDescriptor.FreeBusySecurityDescriptor.ToRawSecurityDescriptorThrow() : null);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00017EFC File Offset: 0x000160FC
		protected override object ConvertValueToClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSecurityDescriptorProperty = FolderSecurity.AclTableAndSecurityDescriptorProperty.Parse((byte[])propertyValue);
			return SecurityDescriptorForTransfer.FormatSecurityDescriptor(aclTableAndSecurityDescriptorProperty.SecurityDescriptor.ToRawSecurityDescriptorThrow());
		}
	}
}
