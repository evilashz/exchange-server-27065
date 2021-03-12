using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.RpcClientAccess.Handler;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000065 RID: 101
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FolderAdaptor : BaseObject, IFolder, IMessageIterator, IMessageIteratorClient, IDisposable, WatsonHelper.IProvideWatsonReportData
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x0001E1D2 File Offset: 0x0001C3D2
		public static FolderAdaptor CreateNew(StoreObjectId parentFolderId, Logon logon, StoreSession storeSession, FastTransferCopyFlag fastTransferCopyFlag, Encoding string8Encoding, bool wantUnicode, bool isUpload)
		{
			return new FolderAdaptor(parentFolderId, logon, storeSession, fastTransferCopyFlag, string8Encoding, wantUnicode, isUpload);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001E1E3 File Offset: 0x0001C3E3
		public static FolderAdaptor Create(ReferenceCount<CoreFolder> referenceCoreFolder, Logon logon, FastTransferCopyFlag fastTransferCopyFlag, Encoding string8Encoding, bool wantUnicode, bool isUpload)
		{
			return new FolderAdaptor(referenceCoreFolder, logon, fastTransferCopyFlag, string8Encoding, wantUnicode, isUpload);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001E1F4 File Offset: 0x0001C3F4
		private FolderAdaptor(StoreObjectId parentFolderId, Logon logon, bool isNewFolder, FastTransferCopyFlag fastTransferCopyFlag, Encoding string8Encoding, bool wantUnicode, bool isUpload)
		{
			this.parentFolderId = parentFolderId;
			this.isNewFolder = isNewFolder;
			this.fastTransferCopyFlag = fastTransferCopyFlag;
			this.string8Encoding = string8Encoding;
			this.wantUnicode = wantUnicode;
			this.isUpload = isUpload;
			this.logonObject = logon;
			if (Activity.Current != null)
			{
				this.watsonReportActionGuard = Activity.Current.RegisterWatsonReportDataProviderAndGetGuard(WatsonReportActionType.FolderAdaptor, this);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001E255 File Offset: 0x0001C455
		private FolderAdaptor(StoreObjectId parentFolderId, Logon logon, StoreSession storeSession, FastTransferCopyFlag fastTransferCopyFlag, Encoding string8Encoding, bool wantUnicode, bool isUpload) : this(parentFolderId, logon, true, fastTransferCopyFlag, string8Encoding, wantUnicode, isUpload)
		{
			this.memoryPropertyBag = new MemoryPropertyBag(new SessionAdaptor(storeSession));
			this.storeSession = storeSession;
			this.referenceFolder = null;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001E288 File Offset: 0x0001C488
		private FolderAdaptor(ReferenceCount<CoreFolder> referenceFolder, Logon logon, FastTransferCopyFlag fastTransferCopyFlag, Encoding string8Encoding, bool wantUnicode, bool isUpload) : this(referenceFolder.ReferencedObject.Id.ObjectId, logon, false, fastTransferCopyFlag, string8Encoding, wantUnicode, isUpload)
		{
			PublicLogon publicLogon = logon as PublicLogon;
			this.folderPropertyBagAdaptor = new FolderPropertyBagAdaptor(referenceFolder.ReferencedObject.PropertyBag, referenceFolder.ReferencedObject, this.string8Encoding, this.wantUnicode, this.isUpload, publicLogon != null && !publicLogon.IsPrimaryHierarchyLogon);
			this.memoryPropertyBag = null;
			this.referenceFolder = referenceFolder;
			this.referenceFolder.AddRef();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001E312 File Offset: 0x0001C512
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.watsonReportActionGuard);
			if (this.referenceFolder != null)
			{
				this.referenceFolder.Release();
			}
			base.InternalDispose();
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001E339 File Offset: 0x0001C539
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FolderAdaptor>(this);
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001E341 File Offset: 0x0001C541
		public IPropertyBag PropertyBag
		{
			get
			{
				base.CheckDisposed();
				if (this.isNewFolder)
				{
					return this.memoryPropertyBag;
				}
				return this.folderPropertyBagAdaptor;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001E368 File Offset: 0x0001C568
		public void Save()
		{
			base.CheckDisposed();
			PublicLogon publicLogon = this.logonObject as PublicLogon;
			if (this.isNewFolder)
			{
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					string value = this.memoryPropertyBag.GetAnnotatedProperty(PropertyTag.DisplayName).PropertyValue.GetValue<string>();
					IEnumerable<PropertyValue> propertyValues = FolderAdaptor.GetPropertyValues(this.memoryPropertyBag.GetAnnotatedProperties());
					bool flag = publicLogon != null && !publicLogon.IsPrimaryHierarchyLogon;
					CoreFolder coreFolder;
					if (flag)
					{
						StoreId folderId = PublicFolderOperations.CreateFolder(publicLogon, value, null, this.parentFolderId, CreateFolderFlags.OpenIfExists);
						FolderAdaptor.SetProperties(publicLogon, propertyValues, folderId);
						coreFolder = CoreFolder.Bind(this.storeSession, folderId);
						disposeGuard.Add<CoreFolder>(coreFolder);
					}
					else
					{
						coreFolder = CoreFolder.Create(this.storeSession, this.parentFolderId, false, value, CreateMode.OpenIfExists);
						disposeGuard.Add<CoreFolder>(coreFolder);
						PropertyTag[] array = (from prop in propertyValues
						select prop.PropertyTag).ToArray<PropertyTag>();
						NativeStorePropertyDefinition[] array2;
						MEDSPropertyTranslator.TryGetPropertyDefinitionsFromPropertyTags(this.storeSession, this.storeSession.Mailbox.CoreObject.PropertyBag, array, out array2);
						for (int i = 0; i < array2.Length; i++)
						{
							NativeStorePropertyDefinition nativeStorePropertyDefinition = array2[i];
							if (nativeStorePropertyDefinition != null)
							{
								coreFolder.PropertyBag.SetProperty(nativeStorePropertyDefinition, MEDSPropertyTranslator.TranslatePropertyValue(this.storeSession, this.memoryPropertyBag.GetAnnotatedProperty(array[i]).PropertyValue));
							}
						}
						this.SafeSaveFolder(coreFolder);
					}
					if (this.folderPropertyBagAdaptor != null)
					{
						throw new InvalidOperationException("folderPropertyBagAdaptor is not Null but this is a new folder.");
					}
					FolderPropertyBagAdaptor folderPropertyBagAdaptor = new FolderPropertyBagAdaptor(coreFolder.PropertyBag, coreFolder, this.string8Encoding, this.wantUnicode, this.isUpload, flag);
					disposeGuard.Success();
					this.isNewFolder = false;
					this.referenceFolder = new ReferenceCount<CoreFolder>(coreFolder);
					this.folderPropertyBagAdaptor = folderPropertyBagAdaptor;
				}
				return;
			}
			if (publicLogon != null && !publicLogon.IsPrimaryHierarchyLogon)
			{
				IEnumerable<PropertyValue> propertyValues2 = FolderAdaptor.GetPropertyValues(this.folderPropertyBagAdaptor.GetInterceptedProperties());
				FolderAdaptor.SetProperties(publicLogon, propertyValues2, this.referenceFolder.ReferencedObject.Id);
				this.referenceFolder.ReferencedObject.PropertyBag.Load(CoreObjectSchema.AllPropertiesOnStore);
				return;
			}
			this.SafeSaveFolder(this.referenceFolder.ReferencedObject);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001E5CC File Offset: 0x0001C7CC
		public IEnumerable<IMessage> GetContents()
		{
			return this.GetContents(ItemQueryType.None);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001E5D5 File Offset: 0x0001C7D5
		public IEnumerable<IMessage> GetAssociatedContents()
		{
			return this.GetContents(ItemQueryType.Associated);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001E67C File Offset: 0x0001C87C
		public IEnumerable<IFolder> GetFolders()
		{
			base.CheckDisposed();
			this.CheckActionDisallowedForNewFolder();
			return this.EnumerateContents<IFolder>((CoreFolder folder) => folder.QueryExecutor.FolderQuery(FolderQueryFlags.None, null, null, new PropertyDefinition[]
			{
				CoreFolderSchema.Id
			}), delegate(StoreId folderId)
			{
				ReferenceCount<CoreFolder> referenceCount = ReferenceCount<CoreFolder>.Assign(CoreFolder.Bind(this.referenceFolder.ReferencedObject.Session, folderId, CoreObjectSchema.AllPropertiesOnStore));
				IFolder result;
				try
				{
					result = FolderAdaptor.Create(referenceCount, this.logonObject, this.fastTransferCopyFlag, this.string8Encoding, this.wantUnicode, this.isUpload);
				}
				finally
				{
					referenceCount.Release();
				}
				return result;
			});
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001E6C4 File Offset: 0x0001C8C4
		public IFolder CreateFolder()
		{
			base.CheckDisposed();
			this.CheckActionDisallowedForNewFolder();
			return FolderAdaptor.CreateNew(StoreObjectId.FromProviderSpecificId(this.folderPropertyBagAdaptor.GetAnnotatedProperty(PropertyTag.EntryId).PropertyValue.GetValue<byte[]>()), this.logonObject, this.referenceFolder.ReferencedObject.Session, this.fastTransferCopyFlag, this.string8Encoding, this.wantUnicode, false);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001E730 File Offset: 0x0001C930
		public IMessage CreateMessage(bool isAssociatedMessage)
		{
			base.CheckDisposed();
			this.CheckActionDisallowedForNewFolder();
			this.referenceFolder.ReferencedObject.PropertyBag.Load(CoreObjectSchema.AllPropertiesOnStore);
			ReferenceCount<CoreItem> referenceCount = ReferenceCount<CoreItem>.Assign(CoreItem.Create(this.referenceFolder.ReferencedObject.Session, this.referenceFolder.ReferencedObject.Id, isAssociatedMessage ? CreateMessageType.Associated : CreateMessageType.Normal));
			IMessage result;
			try
			{
				referenceCount.ReferencedObject.PropertyBag.Load(CoreObjectSchema.AllPropertiesOnStore);
				result = new MessageAdaptor(referenceCount, new MessageAdaptor.Options
				{
					IsReadOnly = false,
					IsEmbedded = false
				}, this.string8Encoding, this.wantUnicode, null);
			}
			finally
			{
				referenceCount.Release();
			}
			return result;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0001E7F4 File Offset: 0x0001C9F4
		public bool IsContentAvailable
		{
			get
			{
				base.CheckDisposed();
				this.CheckActionDisallowedForNewFolder();
				return !(this.referenceFolder.ReferencedObject.Session is PublicFolderSession) || this.referenceFolder.ReferencedObject.IsContentAvailable();
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001E82C File Offset: 0x0001CA2C
		public string[] GetReplicaDatabases(out ushort localSiteDatabaseCount)
		{
			base.CheckDisposed();
			this.CheckActionDisallowedForNewFolder();
			localSiteDatabaseCount = 0;
			if (!this.IsContentAvailable)
			{
				return new string[]
				{
					PublicLogon.GetReplicaServerInfo(this.referenceFolder.ReferencedObject, false).Value.Replicas[0]
				};
			}
			throw new InvalidOperationException("Should not be asking for database replicas if content is available");
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001E88C File Offset: 0x0001CA8C
		public StoreLongTermId GetLongTermId()
		{
			base.CheckDisposed();
			this.CheckActionDisallowedForNewFolder();
			return StoreLongTermId.Parse(this.referenceFolder.ReferencedObject.Session.IdConverter.GetLongTermIdFromId(this.referenceFolder.ReferencedObject.Session.IdConverter.GetFidFromId(this.referenceFolder.ReferencedObject.Id.ObjectId)));
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001EB00 File Offset: 0x0001CD00
		IEnumerator<IMessage> IMessageIterator.GetMessages()
		{
			IEnumerable<IMessage> messages = this.GetContents();
			foreach (IMessage message in messages)
			{
				yield return message;
			}
			messages = this.GetAssociatedContents();
			foreach (IMessage message2 in messages)
			{
				yield return message2;
			}
			yield break;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001EB1C File Offset: 0x0001CD1C
		IMessage IMessageIteratorClient.UploadMessage(bool isAssociatedMessage)
		{
			return this.CreateMessage(isAssociatedMessage);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001EB4C File Offset: 0x0001CD4C
		private static void SetProperties(PublicLogon publicLogon, IEnumerable<PropertyValue> propValues, StoreId folderId)
		{
			IEnumerable<PropertyValue> source = from prop in propValues
			where prop.Value != null && (!prop.CanGetValue<string>() || !string.IsNullOrEmpty(prop.GetValue<string>()))
			select prop;
			PublicFolderOperations.SetProperties(publicLogon, folderId, source.ToArray<PropertyValue>(), true);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001EBA4 File Offset: 0x0001CDA4
		private static IEnumerable<PropertyValue> GetPropertyValues(IEnumerable<AnnotatedPropertyValue> annotatedProperties)
		{
			return from annotatedProperty in annotatedProperties
			select annotatedProperty.PropertyValue into prop
			where !prop.IsError
			select prop;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001EBF6 File Offset: 0x0001CDF6
		private void CheckActionDisallowedForNewFolder()
		{
			if (this.isNewFolder)
			{
				throw new InvalidOperationException("This is new folder.");
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001EC0C File Offset: 0x0001CE0C
		private IMessage OpenMessageForDownload(StoreId id, DownloadBodyOption downloadBodyOption)
		{
			IMessage result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreItem coreItem = CoreItem.Bind(this.referenceFolder.ReferencedObject.Session, id, CoreObjectSchema.AllPropertiesOnStore);
				disposeGuard.Add<CoreItem>(coreItem);
				ReferenceCount<CoreItem> referenceCount = new ReferenceCount<CoreItem>(coreItem);
				try
				{
					MessageAdaptor messageAdaptor = new MessageAdaptor(referenceCount, new MessageAdaptor.Options
					{
						IsReadOnly = true,
						IsEmbedded = false,
						DownloadBodyOption = downloadBodyOption,
						IsUpload = this.isUpload
					}, this.string8Encoding, this.wantUnicode, null);
					disposeGuard.Success();
					result = messageAdaptor;
				}
				finally
				{
					referenceCount.Release();
				}
			}
			return result;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001ED14 File Offset: 0x0001CF14
		private IEnumerable<IMessage> GetContents(ItemQueryType itemQueryType)
		{
			base.CheckDisposed();
			return this.EnumerateContents<IMessage>((CoreFolder folder) => folder.QueryExecutor.ItemQuery(itemQueryType, null, null, new PropertyDefinition[]
			{
				CoreItemSchema.Id
			}), (StoreId messageId) => this.OpenMessageForDownload(messageId, DownloadBodyOption.RtfOnly));
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001ED5C File Offset: 0x0001CF5C
		private void SafeSaveFolder(CoreFolder folder)
		{
			FolderSaveResult folderSaveResult = folder.Save(SaveMode.NoConflictResolution);
			bool flag = folder.Session != null && folder.Session.IsMoveUser;
			if (folderSaveResult.OperationResult == OperationResult.Failed)
			{
				throw new RopExecutionException(string.Format("Save on the folder failed. Result = {0}", folderSaveResult), ExceptionTranslator.ErrorFromXsoException(folderSaveResult.Exception));
			}
			folder.PropertyBag.Load(CoreObjectSchema.AllPropertiesOnStore);
			if (folderSaveResult.OperationResult == OperationResult.PartiallySucceeded)
			{
				if (flag)
				{
					foreach (PropertyError propertyError in folderSaveResult.PropertyErrors)
					{
						if (propertyError.PropertyErrorCode == PropertyErrorCode.SetStoreComputedPropertyError)
						{
							if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.ErrorTrace))
							{
								ExTraceGlobals.FolderTracer.TraceError<int, PropertyError, FolderSaveResult>(0, Activity.TraceId, "Save on the folder failed with property error. Total error count: {0}. PropertyError = {1}, Exception: {2}", folderSaveResult.PropertyErrors.Length, propertyError, folderSaveResult);
							}
						}
						else
						{
							PropertyTagPropertyDefinition propertyTagPropertyDefinition = propertyError.PropertyDefinition as PropertyTagPropertyDefinition;
							if (propertyTagPropertyDefinition == null || propertyTagPropertyDefinition.PropertyTag != 1070137602U || propertyError.PropertyErrorCode != PropertyErrorCode.AccessDenied)
							{
								throw folderSaveResult.ToException();
							}
							ExTraceGlobals.FolderTracer.TraceError(0, Activity.TraceId, "Ignoring error to save LastConflict property due to AccessDenied error for move user");
						}
					}
				}
				if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.FolderTracer.TraceError<FolderSaveResult>(0, Activity.TraceId, "Save on the folder failed with {0}", folderSaveResult);
					foreach (PropertyError arg in folderSaveResult.PropertyErrors)
					{
						ExTraceGlobals.FolderTracer.TraceError<PropertyError>(0, Activity.TraceId, "Save on the folder failed with property error. PropertyError = {0}", arg);
					}
				}
				return;
			}
			if (flag && folderSaveResult.OperationResult != OperationResult.Succeeded)
			{
				throw folderSaveResult.ToException();
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001F0F0 File Offset: 0x0001D2F0
		private IEnumerable<T> EnumerateContents<T>(Func<CoreFolder, QueryResult> queryExecution, Func<StoreId, T> openObject)
		{
			using (QueryResult queryResult = queryExecution(this.referenceFolder.ReferencedObject))
			{
				object[][] currentRow = queryResult.GetRows(512);
				while (currentRow.Length > 0)
				{
					for (int currentRowIndex = 0; currentRowIndex < currentRow.Length; currentRowIndex++)
					{
						yield return openObject((StoreId)currentRow[currentRowIndex][0]);
					}
					currentRow = queryResult.GetRows(512);
				}
			}
			yield break;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001F11C File Offset: 0x0001D31C
		string WatsonHelper.IProvideWatsonReportData.GetWatsonReportString()
		{
			base.CheckDisposed();
			return string.Format("DisplayName: {0}\r\nCreation time: {1}", this.referenceFolder.ReferencedObject.PropertyBag.GetValueOrDefault<string>(CoreFolderSchema.DisplayName, string.Empty), this.referenceFolder.ReferencedObject.PropertyBag.GetValueAsNullable<ExDateTime>(CoreObjectSchema.CreationTime));
		}

		// Token: 0x0400016C RID: 364
		private readonly StoreSession storeSession;

		// Token: 0x0400016D RID: 365
		private readonly Logon logonObject;

		// Token: 0x0400016E RID: 366
		private readonly MemoryPropertyBag memoryPropertyBag;

		// Token: 0x0400016F RID: 367
		private readonly StoreObjectId parentFolderId;

		// Token: 0x04000170 RID: 368
		private readonly FastTransferCopyFlag fastTransferCopyFlag;

		// Token: 0x04000171 RID: 369
		private readonly IDisposable watsonReportActionGuard;

		// Token: 0x04000172 RID: 370
		private readonly Encoding string8Encoding;

		// Token: 0x04000173 RID: 371
		private readonly bool wantUnicode;

		// Token: 0x04000174 RID: 372
		private readonly bool isUpload;

		// Token: 0x04000175 RID: 373
		private bool isNewFolder;

		// Token: 0x04000176 RID: 374
		private FolderPropertyBagAdaptor folderPropertyBagAdaptor;

		// Token: 0x04000177 RID: 375
		private ReferenceCount<CoreFolder> referenceFolder;
	}
}
