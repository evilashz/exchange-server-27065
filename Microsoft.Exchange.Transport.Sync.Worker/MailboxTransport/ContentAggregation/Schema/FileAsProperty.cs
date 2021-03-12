using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Properties.XSO;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x02000232 RID: 562
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FileAsProperty : ContactProperty<string>
	{
		// Token: 0x0600144D RID: 5197 RVA: 0x00049CA4 File Offset: 0x00047EA4
		public FileAsProperty(IXSOPropertyManager propertyManager) : base(propertyManager, new PropertyDefinition[]
		{
			FileAsProperty.FileAsXSOProperty,
			FileAsProperty.FileAsIdXSOProperty
		})
		{
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00049CD0 File Offset: 0x00047ED0
		public override string ReadProperty(Item item)
		{
			SyncUtilities.ThrowIfArgumentNull("item", item);
			return SyncUtilities.SafeGetProperty<string>(item, FileAsProperty.FileAsXSOProperty);
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x00049CE8 File Offset: 0x00047EE8
		public override void WriteProperty(Item item, string value)
		{
			SyncUtilities.ThrowIfArgumentNull("item", item);
			if (value == null)
			{
				if (base.IsItemNew(item))
				{
					item[FileAsProperty.FileAsIdXSOProperty] = FileAsMapping.LastCommaFirst;
					return;
				}
			}
			else
			{
				item[FileAsProperty.FileAsXSOProperty] = value;
			}
		}

		// Token: 0x04000AB7 RID: 2743
		private static readonly PropertyDefinition FileAsXSOProperty = ContactBaseSchema.FileAs;

		// Token: 0x04000AB8 RID: 2744
		private static readonly PropertyDefinition FileAsIdXSOProperty = ContactSchema.FileAsId;
	}
}
