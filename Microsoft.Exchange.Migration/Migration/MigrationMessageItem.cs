using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000CC RID: 204
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationMessageItem : MigrationStoreObject, IMigrationMessageItem, IMigrationStoreObject, IDisposable, IPropertyBag, IReadOnlyPropertyBag, IMigrationAttachmentMessage
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002DE44 File Offset: 0x0002C044
		internal MigrationMessageItem(MessageItem message)
		{
			this.Message = message;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002DE53 File Offset: 0x0002C053
		internal MigrationMessageItem(MigrationDataProvider dataProvider, StoreObjectId id)
		{
			this.Initialize(dataProvider, id, MigrationStoreObject.IdPropertyDefinition);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002DE68 File Offset: 0x0002C068
		internal MigrationMessageItem(MigrationDataProvider dataProvider, StoreObjectId id, PropertyDefinition[] propertyDefinitions)
		{
			this.Initialize(dataProvider, id, propertyDefinitions);
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0002DE79 File Offset: 0x0002C079
		public override string Name
		{
			get
			{
				return this.Message.ClassName;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0002DE86 File Offset: 0x0002C086
		protected override StoreObject StoreObject
		{
			get
			{
				return this.Message;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0002DE8E File Offset: 0x0002C08E
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0002DE96 File Offset: 0x0002C096
		private MessageItem Message { get; set; }

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002DE9F File Offset: 0x0002C09F
		public override void OpenAsReadWrite()
		{
			this.Message.OpenAsReadWrite();
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002DEAC File Offset: 0x0002C0AC
		public override void Save(SaveMode saveMode)
		{
			this.Message.Save(saveMode);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002DEBB File Offset: 0x0002C0BB
		public IMigrationAttachment CreateAttachment(string name)
		{
			base.CheckDisposed();
			return MigrationMessageHelper.CreateAttachment(this.Message, name);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002DECF File Offset: 0x0002C0CF
		public bool TryGetAttachment(string name, PropertyOpenMode openMode, out IMigrationAttachment attachment)
		{
			base.CheckDisposed();
			return MigrationMessageHelper.TryGetAttachment(this.Message, name, openMode, out attachment);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002DEE5 File Offset: 0x0002C0E5
		public IMigrationAttachment GetAttachment(string name, PropertyOpenMode openMode)
		{
			base.CheckDisposed();
			return MigrationMessageHelper.GetAttachment(this.Message, name, openMode);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002DEFA File Offset: 0x0002C0FA
		public void DeleteAttachment(string name)
		{
			base.CheckDisposed();
			MigrationMessageHelper.DeleteAttachment(this.Message, name);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002DF10 File Offset: 0x0002C110
		public override XElement GetDiagnosticInfo(ICollection<PropertyDefinition> properties, MigrationDiagnosticArgument argument)
		{
			base.CheckDisposed();
			XElement diagnosticInfo = base.GetDiagnosticInfo(properties, argument);
			diagnosticInfo.Add(MigrationMessageHelper.GetAttachmentDiagnosticInfo(this.Message, argument));
			return diagnosticInfo;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002DF3F File Offset: 0x0002C13F
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.Message != null)
			{
				this.Message.Dispose();
				this.Message = null;
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002DF5E File Offset: 0x0002C15E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationMessageItem>(this);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002DF68 File Offset: 0x0002C168
		private void Initialize(MigrationDataProvider dataProvider, StoreObjectId id, PropertyDefinition[] properties)
		{
			bool flag = false;
			try
			{
				MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
				MigrationUtil.ThrowOnNullArgument(id, "id");
				MigrationUtil.ThrowOnNullArgument(properties, "properties");
				this.Message = MessageItem.Bind(dataProvider.MailboxSession, id, properties);
				flag = true;
			}
			catch (ArgumentException ex)
			{
				MigrationLogger.Log(MigrationEventType.Error, ex, "Encountered an argument exception when trying to find message with id={0}", new object[]
				{
					id.ToString()
				});
				throw new ObjectNotFoundException(ServerStrings.ExItemNotFound, ex);
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
		}
	}
}
