using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004A4 RID: 1188
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContactFoldersEnumerator : IEnumerable<IStorePropertyBag>, IEnumerable
	{
		// Token: 0x060034C7 RID: 13511 RVA: 0x000D5685 File Offset: 0x000D3885
		public ContactFoldersEnumerator(IMailboxSession session) : this(session, new XSOFactory())
		{
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000D5693 File Offset: 0x000D3893
		public ContactFoldersEnumerator(IMailboxSession session, ContactFoldersEnumeratorOptions enumerateOptions) : this(session, new XSOFactory(), enumerateOptions, new PropertyDefinition[0])
		{
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000D56A8 File Offset: 0x000D38A8
		public ContactFoldersEnumerator(IMailboxSession session, IXSOFactory xsoFactory) : this(session, xsoFactory, ContactFoldersEnumeratorOptions.None, new PropertyDefinition[0])
		{
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000D56B9 File Offset: 0x000D38B9
		public ContactFoldersEnumerator(IMailboxSession session, IXSOFactory xsoFactory, ContactFoldersEnumeratorOptions enumerateOptions, params PropertyDefinition[] additionalProperties) : this(session, xsoFactory, DefaultFolderType.Root, enumerateOptions, additionalProperties)
		{
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000D56C8 File Offset: 0x000D38C8
		public ContactFoldersEnumerator(IMailboxSession session, IXSOFactory xsoFactory, DefaultFolderType parentFolderScope, ContactFoldersEnumeratorOptions enumerateOptions, params PropertyDefinition[] additionalProperties)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(xsoFactory, "xsoFactory");
			EnumValidator.ThrowIfInvalid<ContactFoldersEnumeratorOptions>(enumerateOptions, "enumerateOptions");
			this.session = session;
			this.xsoFactory = xsoFactory;
			this.enumerateOptions = enumerateOptions;
			this.additionalProperties = additionalProperties;
			this.parentFolderScope = ((parentFolderScope != DefaultFolderType.None) ? parentFolderScope : DefaultFolderType.Root);
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x060034CC RID: 13516 RVA: 0x000D5729 File Offset: 0x000D3929
		private bool ShouldIncludeParentFolder
		{
			get
			{
				return (this.enumerateOptions & ContactFoldersEnumeratorOptions.IncludeParentFolder) != ContactFoldersEnumeratorOptions.None;
			}
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000D5739 File Offset: 0x000D3939
		private bool ShouldSkipHiddenFolders
		{
			get
			{
				return (this.enumerateOptions & ContactFoldersEnumeratorOptions.SkipHiddenFolders) != ContactFoldersEnumeratorOptions.None;
			}
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x060034CE RID: 13518 RVA: 0x000D5749 File Offset: 0x000D3949
		private bool ShouldSkipDeletedFolders
		{
			get
			{
				return (this.enumerateOptions & ContactFoldersEnumeratorOptions.SkipDeletedFolders) != ContactFoldersEnumeratorOptions.None;
			}
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x000D5759 File Offset: 0x000D3959
		private StoreObjectId DeletedItemsFolderId
		{
			get
			{
				if (this.deletedItemsFolderId == null)
				{
					this.deletedItemsFolderId = this.session.GetDefaultFolderId(DefaultFolderType.DeletedItems);
				}
				return this.deletedItemsFolderId;
			}
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x060034D0 RID: 13520 RVA: 0x000D577C File Offset: 0x000D397C
		private PropertyDefinition[] FolderPropertiesToBeLoaded
		{
			get
			{
				PropertyDefinition[] result;
				if (this.additionalProperties != null && this.additionalProperties.Length > 0)
				{
					result = PropertyDefinitionCollection.Merge<PropertyDefinition>(ContactFoldersEnumerator.DefaultFolderProperties, this.additionalProperties);
				}
				else
				{
					result = ContactFoldersEnumerator.DefaultFolderProperties;
				}
				return result;
			}
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000D5AC0 File Offset: 0x000D3CC0
		public IEnumerator<IStorePropertyBag> GetEnumerator()
		{
			ContactFoldersEnumerator.DeletedItemsFolderEnumerationState deletedItemsFolderEnumerationState = new ContactFoldersEnumerator.DeletedItemsFolderEnumerationState();
			using (IFolder rootFolder = this.xsoFactory.BindToFolder(this.session, this.session.GetDefaultFolderId(this.parentFolderScope)))
			{
				if (this.ShouldIncludeParentFolder)
				{
					rootFolder.Load(this.FolderPropertiesToBeLoaded);
					if (this.ShouldEnumerateFolder(rootFolder, deletedItemsFolderEnumerationState))
					{
						yield return rootFolder;
					}
				}
				using (IQueryResult subFoldersQuery = rootFolder.IFolderQuery(FolderQueryFlags.DeepTraversal, null, null, this.FolderPropertiesToBeLoaded))
				{
					IStorePropertyBag[] folders = subFoldersQuery.GetPropertyBags(100);
					while (folders.Length > 0)
					{
						foreach (IStorePropertyBag folder in folders)
						{
							if (this.ShouldEnumerateFolder(folder, deletedItemsFolderEnumerationState))
							{
								yield return folder;
							}
						}
						folders = subFoldersQuery.GetPropertyBags(100);
					}
				}
			}
			yield break;
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000D5ADC File Offset: 0x000D3CDC
		private bool ShouldEnumerateFolder(IStorePropertyBag folder, ContactFoldersEnumerator.DeletedItemsFolderEnumerationState deletedItemsFolderEnumerationState)
		{
			object obj = folder.TryGetProperty(FolderSchema.Id);
			object obj2 = folder.TryGetProperty(StoreObjectSchema.ContainerClass);
			string valueOrDefault = folder.GetValueOrDefault<string>(FolderSchema.DisplayName, string.Empty);
			if (obj is PropertyError || obj2 is PropertyError)
			{
				ContactFoldersEnumerator.Tracer.TraceDebug<string, object, object>((long)this.GetHashCode(), "Skiping bogus folder (DisplayName:{0}) without ID ({1}) or container class ({2})", valueOrDefault, obj, obj2);
				return false;
			}
			if (this.ShouldSkipDeletedFolders && this.IsDeletedFolder(folder, deletedItemsFolderEnumerationState))
			{
				ContactFoldersEnumerator.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "Skiping deleted folder - ID:{0}, DisplayName:{1}.", obj, valueOrDefault);
				return false;
			}
			if (!ObjectClass.IsContactsFolder((string)obj2))
			{
				ContactFoldersEnumerator.Tracer.TraceDebug<object, object, string>((long)this.GetHashCode(), "Skiping non-contact folder - ID:{0}, ContainerClass:{1}, DisplayName:{2}.", obj, obj2, valueOrDefault);
				return false;
			}
			if (this.ShouldSkipHiddenFolders && folder.TryGetProperty(FolderSchema.IsHidden) is bool && (bool)folder.TryGetProperty(FolderSchema.IsHidden))
			{
				ContactFoldersEnumerator.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "Skiping hidden folder - ID:{0}, DisplayName:{1}.", obj, valueOrDefault);
				return false;
			}
			ContactFoldersEnumerator.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "Enumerating folder - ID:{0}, DisplayName:{1}.", obj, valueOrDefault);
			return true;
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000D5BF0 File Offset: 0x000D3DF0
		private bool IsDeletedFolder(IStorePropertyBag folder, ContactFoldersEnumerator.DeletedItemsFolderEnumerationState deletedItemsFolderEnumerationState)
		{
			if (deletedItemsFolderEnumerationState.IsAlreadyEnumerated)
			{
				return false;
			}
			object obj = folder.TryGetProperty(FolderSchema.FolderHierarchyDepth);
			if (!(obj is int) || (int)obj < 0)
			{
				return false;
			}
			int num = (int)obj;
			StoreObjectId objectId = ((VersionedId)folder.TryGetProperty(FolderSchema.Id)).ObjectId;
			if (deletedItemsFolderEnumerationState.NotEnumeratedYet)
			{
				if (objectId.Equals(this.DeletedItemsFolderId))
				{
					deletedItemsFolderEnumerationState.MarkDeletedItemsFolderEncountered(num);
				}
				return false;
			}
			if (num == deletedItemsFolderEnumerationState.DeletedItemsFolderDepth)
			{
				deletedItemsFolderEnumerationState.MarkDeletedItemsFolderEnumerationDone();
				return false;
			}
			return true;
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000D5C74 File Offset: 0x000D3E74
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generic version of GetEnumerator.");
		}

		// Token: 0x04001C15 RID: 7189
		private static readonly Trace Tracer = ExTraceGlobals.ContactFoldersEnumeratorTracer;

		// Token: 0x04001C16 RID: 7190
		private static readonly PropertyDefinition[] DefaultFolderProperties = new PropertyDefinition[]
		{
			StoreObjectSchema.ContainerClass,
			FolderSchema.DisplayName,
			FolderSchema.Id,
			FolderSchema.IsHidden,
			FolderSchema.FolderHierarchyDepth
		};

		// Token: 0x04001C17 RID: 7191
		private readonly IMailboxSession session;

		// Token: 0x04001C18 RID: 7192
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04001C19 RID: 7193
		private readonly ContactFoldersEnumeratorOptions enumerateOptions;

		// Token: 0x04001C1A RID: 7194
		private readonly DefaultFolderType parentFolderScope;

		// Token: 0x04001C1B RID: 7195
		private readonly PropertyDefinition[] additionalProperties;

		// Token: 0x04001C1C RID: 7196
		private StoreObjectId deletedItemsFolderId;

		// Token: 0x020004A5 RID: 1189
		private sealed class DeletedItemsFolderEnumerationState
		{
			// Token: 0x060034D6 RID: 13526 RVA: 0x000D5CCC File Offset: 0x000D3ECC
			public DeletedItemsFolderEnumerationState()
			{
				this.currentPhase = ContactFoldersEnumerator.DeletedItemsFolderEnumerationState.EnumerationPhase.NotEnumeratedYet;
			}

			// Token: 0x17001079 RID: 4217
			// (get) Token: 0x060034D7 RID: 13527 RVA: 0x000D5CDB File Offset: 0x000D3EDB
			public bool IsAlreadyEnumerated
			{
				get
				{
					return this.currentPhase == ContactFoldersEnumerator.DeletedItemsFolderEnumerationState.EnumerationPhase.EnumerationDone;
				}
			}

			// Token: 0x1700107A RID: 4218
			// (get) Token: 0x060034D8 RID: 13528 RVA: 0x000D5CE6 File Offset: 0x000D3EE6
			public bool NotEnumeratedYet
			{
				get
				{
					return this.currentPhase == ContactFoldersEnumerator.DeletedItemsFolderEnumerationState.EnumerationPhase.NotEnumeratedYet;
				}
			}

			// Token: 0x1700107B RID: 4219
			// (get) Token: 0x060034D9 RID: 13529 RVA: 0x000D5CF1 File Offset: 0x000D3EF1
			public bool IsUnderEnumeration
			{
				get
				{
					return this.currentPhase == ContactFoldersEnumerator.DeletedItemsFolderEnumerationState.EnumerationPhase.UnderEnumeration;
				}
			}

			// Token: 0x1700107C RID: 4220
			// (get) Token: 0x060034DA RID: 13530 RVA: 0x000D5CFC File Offset: 0x000D3EFC
			public int DeletedItemsFolderDepth
			{
				get
				{
					this.AssertCurrentPhase(ContactFoldersEnumerator.DeletedItemsFolderEnumerationState.EnumerationPhase.UnderEnumeration);
					return this.deletedItemsFolderDepth.Value;
				}
			}

			// Token: 0x060034DB RID: 13531 RVA: 0x000D5D10 File Offset: 0x000D3F10
			public void MarkDeletedItemsFolderEncountered(int folderHierarchyDepth)
			{
				Util.ThrowOnArgumentOutOfRangeOnLessThan(folderHierarchyDepth, 0, "folderHierarchyDepth");
				this.AssertCurrentPhase(ContactFoldersEnumerator.DeletedItemsFolderEnumerationState.EnumerationPhase.NotEnumeratedYet);
				this.deletedItemsFolderDepth = new int?(folderHierarchyDepth);
				this.currentPhase = ContactFoldersEnumerator.DeletedItemsFolderEnumerationState.EnumerationPhase.UnderEnumeration;
			}

			// Token: 0x060034DC RID: 13532 RVA: 0x000D5D38 File Offset: 0x000D3F38
			public void MarkDeletedItemsFolderEnumerationDone()
			{
				this.AssertCurrentPhase(ContactFoldersEnumerator.DeletedItemsFolderEnumerationState.EnumerationPhase.UnderEnumeration);
				this.currentPhase = ContactFoldersEnumerator.DeletedItemsFolderEnumerationState.EnumerationPhase.EnumerationDone;
			}

			// Token: 0x060034DD RID: 13533 RVA: 0x000D5D48 File Offset: 0x000D3F48
			private void AssertCurrentPhase(ContactFoldersEnumerator.DeletedItemsFolderEnumerationState.EnumerationPhase phaseTobeAsserted)
			{
			}

			// Token: 0x04001C1D RID: 7197
			private ContactFoldersEnumerator.DeletedItemsFolderEnumerationState.EnumerationPhase currentPhase;

			// Token: 0x04001C1E RID: 7198
			private int? deletedItemsFolderDepth;

			// Token: 0x020004A6 RID: 1190
			private enum EnumerationPhase
			{
				// Token: 0x04001C20 RID: 7200
				NotEnumeratedYet,
				// Token: 0x04001C21 RID: 7201
				UnderEnumeration,
				// Token: 0x04001C22 RID: 7202
				EnumerationDone
			}
		}
	}
}
