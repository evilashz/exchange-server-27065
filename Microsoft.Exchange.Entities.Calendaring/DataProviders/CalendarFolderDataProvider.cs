using System;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.Translators;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.Calendaring.DataProviders
{
	// Token: 0x0200001B RID: 27
	internal class CalendarFolderDataProvider : StorageFolderDataProvider<IStoreSession, Calendar, ICalendarFolder>
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00003247 File Offset: 0x00001447
		public CalendarFolderDataProvider(IStorageEntitySetScope<IStoreSession> parentScope, StoreId parentFolderId) : base(parentScope, ExTraceGlobals.CalendarDataProviderTracer)
		{
			this.ParentFolderId = parentFolderId;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000325C File Offset: 0x0000145C
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003264 File Offset: 0x00001464
		public StoreId ParentFolderId { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000326D File Offset: 0x0000146D
		protected override IStorageTranslator<ICalendarFolder, Calendar> Translator
		{
			get
			{
				return CalendarTranslator.Instance;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003274 File Offset: 0x00001474
		public override void Delete(StoreId id, DeleteItemFlags flags)
		{
			try
			{
				base.Delete(id, flags);
			}
			catch (CannotMoveDefaultFolderException innerException)
			{
				throw new CannotDeleteDefaultCalendarException(innerException);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000032A4 File Offset: 0x000014A4
		protected internal override ICalendarFolder BindToStoreObject(StoreId id)
		{
			return base.XsoFactory.BindToCalendarFolder(base.Session, id);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000032B8 File Offset: 0x000014B8
		protected override ICalendarFolder CreateNewStoreObject()
		{
			return base.XsoFactory.CreateCalendarFolder(base.Session, this.ParentFolderId);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000032E0 File Offset: 0x000014E0
		protected override void SaveAndCheckForConflicts(ICalendarFolder storeObject, SaveMode saveMode)
		{
			string displayName = storeObject.DisplayName;
			try
			{
				FolderSaveResult folderSaveResult = storeObject.Save(saveMode);
				switch (folderSaveResult.OperationResult)
				{
				case OperationResult.Failed:
					throw new CalendarFolderUpdateFailedException(folderSaveResult.Exception);
				case OperationResult.PartiallySucceeded:
					if (folderSaveResult.PropertyErrors.Length == 1 && folderSaveResult.PropertyErrors[0].PropertyDefinition == FolderSchema.DisplayName && folderSaveResult.PropertyErrors[0].PropertyErrorCode == PropertyErrorCode.FolderNameConflict)
					{
						throw new CalendarNameAlreadyInUseException(displayName, folderSaveResult.Exception);
					}
					throw new CalendarFolderUpdateFailedException(folderSaveResult.Exception);
				}
			}
			catch (ObjectValidationException ex)
			{
				IMailboxSession mailboxSession = base.Session as IMailboxSession;
				bool flag = mailboxSession != null && mailboxSession.IsDefaultFolderType(storeObject.Id) == DefaultFolderType.Calendar;
				bool flag2;
				if (flag)
				{
					flag2 = (ex.Errors.FirstOrDefault((StoreObjectValidationError x) => x.PropertyDefinition == FolderSchema.DisplayName) != null);
				}
				else
				{
					flag2 = false;
				}
				bool flag3 = flag2;
				if (flag3)
				{
					throw new CannotRenameDefaultCalendarException(ex);
				}
				throw;
			}
			catch (ObjectExistedException innerException)
			{
				throw new CalendarNameAlreadyInUseException(displayName, innerException);
			}
		}
	}
}
