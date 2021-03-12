using System;
using Microsoft.WindowsAzure.ActiveDirectory;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public sealed class AADPresentationObjectFactory
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00005A88 File Offset: 0x00003C88
		public static AADDirectoryObjectPresentationObject Create(DirectoryObject directoryObject)
		{
			Group group = directoryObject as Group;
			if (group != null)
			{
				return new AADGroupPresentationObject(group);
			}
			User user = directoryObject as User;
			if (user != null)
			{
				return new AADUserPresentationObject(user);
			}
			return new AADDirectoryObjectPresentationObject(directoryObject);
		}
	}
}
