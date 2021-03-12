using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000659 RID: 1625
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DefaultFolderValidator
	{
		// Token: 0x06004383 RID: 17283 RVA: 0x0011E0F8 File Offset: 0x0011C2F8
		internal DefaultFolderValidator(params IValidator[] validators)
		{
			if (validators != null)
			{
				this.validators = validators;
			}
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x0011E10A File Offset: 0x0011C30A
		internal void SetProperties(DefaultFolderContext context, Folder folder)
		{
			this.SetPropertiesInternal(context, folder);
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x0011E114 File Offset: 0x0011C314
		internal virtual bool EnsureIsValid(DefaultFolderContext context, Folder folder)
		{
			return this.EnsureIsValid(context, folder.PropertyBag);
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x0011E124 File Offset: 0x0011C324
		internal virtual bool EnsureIsValid(DefaultFolderContext context, StoreObjectId folderId, Dictionary<string, DefaultFolderManager.FolderData> folderDataDictionary)
		{
			string text = Convert.ToBase64String(folderId.ProviderLevelItemId);
			DefaultFolderManager.FolderData folderData;
			if (folderDataDictionary != null && folderDataDictionary.TryGetValue(text, out folderData))
			{
				return this.EnsureIsValid(context, folderData.PropertyBag);
			}
			return this.BindAndValidateFolder(context, folderId, text);
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x0011E1E8 File Offset: 0x0011C3E8
		protected bool BindAndValidateFolder(DefaultFolderContext context, StoreObjectId folderId, string idString)
		{
			bool result2;
			try
			{
				bool result = false;
				context.Session.BypassAuditing(delegate
				{
					using (Folder folder = Folder.Bind(context.Session, folderId, DefaultFolderInfo.InboxOrConfigurationFolderProperties))
					{
						result = this.EnsureIsValid(context, folder);
					}
				});
				result2 = result;
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.DefaultFoldersTracer.TraceDebug<string>((long)this.GetHashCode(), "FolderValidationStrategy::EnsureIsValid. The folder is missing. FolderId = {0}.", idString);
				result2 = false;
			}
			return result2;
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x0011E278 File Offset: 0x0011C478
		internal bool EnsureIsValid(DefaultFolderContext context, PropertyBag propertyBag)
		{
			return !string.IsNullOrEmpty(propertyBag.TryGetProperty(InternalSchema.DisplayName) as string) && this.ValidateInternal(context, propertyBag);
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x0011E29C File Offset: 0x0011C49C
		protected virtual bool ValidateInternal(DefaultFolderContext context, PropertyBag propertyBag)
		{
			if (this.validators == null)
			{
				return true;
			}
			foreach (IValidator validator in this.validators)
			{
				if (!validator.Validate(context, propertyBag))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x0011E2E0 File Offset: 0x0011C4E0
		protected virtual void SetPropertiesInternal(DefaultFolderContext context, Folder folder)
		{
			foreach (IValidator validator in this.validators)
			{
				validator.SetProperties(context, folder);
			}
		}

		// Token: 0x040024C2 RID: 9410
		internal static NullDefaultFolderValidator NullValidator = new NullDefaultFolderValidator();

		// Token: 0x040024C3 RID: 9411
		internal static DefaultFolderValidator MessageFolderGenericTypeValidator = new DefaultFolderValidator(new IValidator[]
		{
			new CompositeValidator(new IValidator[]
			{
				new MatchMapiFolderType(FolderType.Generic),
				new MatchContainerClass("IPF.Note")
			})
		});

		// Token: 0x040024C4 RID: 9412
		internal static DefaultFolderValidator FolderGenericTypeValidator = new DefaultFolderValidator(new IValidator[]
		{
			new MatchMapiFolderType(FolderType.Generic)
		});

		// Token: 0x040024C5 RID: 9413
		private IValidator[] validators;
	}
}
