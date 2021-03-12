using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000209 RID: 521
	internal class ManagedFolderPropertyInformation : PropertyInformation
	{
		// Token: 0x06000D94 RID: 3476 RVA: 0x00043C28 File Offset: 0x00041E28
		public ManagedFolderPropertyInformation() : base("ManagedFolderInformation", ServiceXml.GetFullyQualifiedName("ManagedFolderInformation"), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2007, ManagedFolderPropertyInformation.GetPropertyDefinitions(), new PropertyUri(PropertyUriEnum.ManagedFolderInformation), new PropertyCommand.CreatePropertyCommand(ManagedFolderInformationProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands)
		{
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00043C70 File Offset: 0x00041E70
		private static PropertyDefinition[] GetPropertyDefinitions()
		{
			return new PropertyDefinition[]
			{
				FolderSchema.AdminFolderFlags,
				FolderSchema.ELCPolicyIds,
				FolderSchema.ELCFolderComment,
				FolderSchema.FolderQuota,
				FolderSchema.FolderSize,
				FolderSchema.FolderHomePageUrl
			};
		}

		// Token: 0x0200020A RID: 522
		internal enum ManagedFolderInfoIndex
		{
			// Token: 0x04000AA4 RID: 2724
			AdminFolderFlags,
			// Token: 0x04000AA5 RID: 2725
			ELCPolicyIds,
			// Token: 0x04000AA6 RID: 2726
			ELCFolderComment,
			// Token: 0x04000AA7 RID: 2727
			FolderQuota,
			// Token: 0x04000AA8 RID: 2728
			FolderSize,
			// Token: 0x04000AA9 RID: 2729
			FolderHomePageUrl
		}
	}
}
