using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Handler;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x0200007B RID: 123
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MessageAdaptor : BaseObject, IMessage, IDisposable, WatsonHelper.IProvideWatsonReportData
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x00021B40 File Offset: 0x0001FD40
		internal MessageAdaptor(ReferenceCount<CoreItem> referenceCoreItem, MessageAdaptor.Options options, Encoding string8Encoding, bool wantUnicode, Logon logon = null)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.options = options;
				this.string8Encoding = string8Encoding;
				this.wantUnicode = wantUnicode;
				this.logon = logon;
				if (!this.options.IsEmbedded && Activity.Current != null)
				{
					this.watsonReportActionGuard = Activity.Current.RegisterWatsonReportDataProviderAndGetGuard(WatsonReportActionType.MessageAdaptor, this);
				}
				this.referenceCoreItem = referenceCoreItem;
				this.referenceCoreItem.AddRef();
				this.bestBodyCoreObjectProperties = new BestBodyCoreObjectProperties(this.referenceCoreItem.ReferencedObject, this.referenceCoreItem.ReferencedObject.PropertyBag, this.string8Encoding, new Func<BodyReadConfiguration, System.IO.Stream>(this.GetBodyConversionStreamCallback));
				disposeGuard.Success();
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00021C10 File Offset: 0x0001FE10
		internal MessageAdaptor(BestBodyCoreObjectProperties bestBodyCoreObjectProperties, ReferenceCount<CoreItem> referenceCoreItem, MessageAdaptor.Options options, Encoding string8Encoding, bool wantUnicode, Logon logon = null)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.options = options;
				this.string8Encoding = string8Encoding;
				this.wantUnicode = wantUnicode;
				this.logon = logon;
				if (!this.options.IsEmbedded && Activity.Current != null)
				{
					this.watsonReportActionGuard = Activity.Current.RegisterWatsonReportDataProviderAndGetGuard(WatsonReportActionType.MessageAdaptor, this);
				}
				this.referenceCoreItem = referenceCoreItem;
				this.referenceCoreItem.AddRef();
				this.bestBodyCoreObjectProperties = bestBodyCoreObjectProperties;
				disposeGuard.Success();
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00021CB0 File Offset: 0x0001FEB0
		public IPropertyBag PropertyBag
		{
			get
			{
				base.CheckDisposed();
				if (this.propertyBag == null)
				{
					this.propertyBag = new CoreItemPropertyBag(new CorePropertyBagAdaptor(this.bestBodyCoreObjectProperties, this.referenceCoreItem.ReferencedObject.PropertyBag, this.referenceCoreItem.ReferencedObject, ClientSideProperties.MessageInstance, PropertyConverter.Message, this.options.DownloadBodyOption, this.string8Encoding, this.wantUnicode, this.options.IsUpload, this.options.IsFastTransferCopyProperties), this.options.SendEntryId);
				}
				return this.propertyBag;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00021D44 File Offset: 0x0001FF44
		public ReferenceCount<CoreItem> ReferenceCoreItem
		{
			get
			{
				base.CheckDisposed();
				return this.referenceCoreItem;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00021D52 File Offset: 0x0001FF52
		public bool IsAssociated
		{
			get
			{
				base.CheckDisposed();
				return MessageAdaptor.IsAssociatedMessage(this);
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00021D60 File Offset: 0x0001FF60
		public static bool IsAssociatedMessage(IMessage message)
		{
			AnnotatedPropertyValue annotatedProperty = message.PropertyBag.GetAnnotatedProperty(PropertyTag.MessageFlags);
			return !annotatedProperty.PropertyValue.IsError && ((int)annotatedProperty.PropertyValue.Value & 64) == 64;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00021FD0 File Offset: 0x000201D0
		public IEnumerable<IRecipient> GetRecipients()
		{
			base.CheckDisposed();
			if (!this.options.IsReadOnly)
			{
				throw new InvalidOperationException("Cannot iterate through recipients unless it is a readonly message.");
			}
			IEnumerable<CoreRecipient> recipients = this.referenceCoreItem.ReferencedObject.Recipients;
			if (!this.options.IsEmbedded)
			{
				recipients = recipients.ToArray<CoreRecipient>();
			}
			foreach (CoreRecipient recipient in recipients)
			{
				yield return new RecipientAdaptor(recipient, this.referenceCoreItem.ReferencedObject, this.string8Encoding, this.wantUnicode);
			}
			yield break;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00021FF0 File Offset: 0x000201F0
		public IRecipient CreateRecipient()
		{
			base.CheckDisposed();
			if (this.options.IsReadOnly)
			{
				throw new InvalidOperationException("Cannot CreateRecipient on readonly messages.");
			}
			int count = this.referenceCoreItem.ReferencedObject.Recipients.Count;
			CoreRecipient coreRecipient = this.referenceCoreItem.ReferencedObject.Recipients.CreateOrReplace(count);
			return new RecipientAdaptor(coreRecipient, this.referenceCoreItem.ReferencedObject, this.string8Encoding, this.wantUnicode);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00022065 File Offset: 0x00020265
		public void RemoveRecipient(int rowId)
		{
			this.referenceCoreItem.ReferencedObject.Recipients.Remove(rowId);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0002227C File Offset: 0x0002047C
		public IEnumerable<IAttachmentHandle> GetAttachments()
		{
			base.CheckDisposed();
			if (!this.options.IsReadOnly)
			{
				throw new InvalidOperationException("Cannot iterate through attachments unless it is a readonly message.");
			}
			CoreAttachmentCollection coreAttachmentCollection = this.referenceCoreItem.ReferencedObject.AttachmentCollection;
			foreach (AttachmentHandle attachmentHandle in coreAttachmentCollection)
			{
				yield return new MessageAdaptor.AttachmentHandleAdaptor(coreAttachmentCollection, attachmentHandle, this.string8Encoding, this.wantUnicode, this.options.IsUpload);
			}
			yield break;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0002229C File Offset: 0x0002049C
		public IAttachment CreateAttachment()
		{
			base.CheckDisposed();
			if (this.options.IsReadOnly)
			{
				throw new InvalidOperationException("Cannot CreateAttachment on readonly messages.");
			}
			IAttachment result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreAttachment coreAttachment = this.referenceCoreItem.ReferencedObject.AttachmentCollection.Create(AttachmentType.Stream);
				coreAttachment.PropertyBag.Load(CoreObjectSchema.AllPropertiesOnStore);
				this.referenceCoreItem.ReferencedObject.PropertyBag[CoreItemSchema.MapiHasAttachment] = true;
				disposeGuard.Add<CoreAttachment>(coreAttachment);
				ReferenceCount<CoreAttachment> referenceCount = new ReferenceCount<CoreAttachment>(coreAttachment);
				try
				{
					AttachmentAdaptor attachmentAdaptor = new AttachmentAdaptor(referenceCount, false, this.string8Encoding, this.wantUnicode, false);
					disposeGuard.Success();
					result = attachmentAdaptor;
				}
				finally
				{
					referenceCount.Release();
				}
			}
			return result;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0002237C File Offset: 0x0002057C
		public void Save()
		{
			base.CheckDisposed();
			this.referenceCoreItem.ReferencedObject.SaveFlags |= (PropertyBagSaveFlags.IgnoreMapiComputedErrors | PropertyBagSaveFlags.IgnoreAccessDeniedErrors);
			CoreItem referencedObject = this.referenceCoreItem.ReferencedObject;
			this.bestBodyCoreObjectProperties.ResetBody();
			bool flag = false;
			if (this.logon != null && !this.options.IsEmbedded)
			{
				try
				{
					flag = TeamMailboxExecutionHelper.SaveChangesToLinkedDocumentLibraryIfNecessary(referencedObject, this.logon);
				}
				catch (StoragePermanentException e)
				{
					TeamMailboxExecutionHelper.LogServerFailures(referencedObject, this.logon, e);
					throw;
				}
				catch (StorageTransientException e2)
				{
					TeamMailboxExecutionHelper.LogServerFailures(referencedObject, this.logon, e2);
					throw;
				}
			}
			if (flag)
			{
				((MailboxSession)referencedObject.Session).TryToSyncSiteMailboxNow();
				return;
			}
			IContentIndexingSession contentIndexingSession = referencedObject.Session.ContentIndexingSession;
			if (contentIndexingSession != null)
			{
				contentIndexingSession.EnableWordBreak = true;
			}
			try
			{
				referencedObject.Body.ResetBodyFormat();
				ConflictResolutionResult conflictResolutionResult = referencedObject.Save(SaveMode.ResolveConflicts);
				if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					Feature.Stubbed(65889, "Handle message conflicts in FX Upload");
					throw new RopExecutionException("Failed to save message due to conflicts.", (ErrorCode)2147746057U);
				}
			}
			finally
			{
				if (contentIndexingSession != null)
				{
					contentIndexingSession.EnableWordBreak = false;
				}
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000224A8 File Offset: 0x000206A8
		public void SetLongTermId(StoreLongTermId longTermId)
		{
			CoreItem coreItem = (this.referenceCoreItem == null) ? null : this.referenceCoreItem.ReferencedObject;
			if (coreItem == null || coreItem.Session == null || !coreItem.Session.IsMoveUser)
			{
				throw new RopExecutionException("Should not be called in MoMT scenarios.", (ErrorCode)2147746050U);
			}
			coreItem.PropertyBag.SetProperty(MessageItemSchema.LTID, longTermId.ToBytes());
			coreItem.Flush(SaveMode.NoConflictResolution);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00022514 File Offset: 0x00020714
		string WatsonHelper.IProvideWatsonReportData.GetWatsonReportString()
		{
			base.CheckDisposed();
			return string.Format("Subject: {0}\r\nReceive time: {1}", this.referenceCoreItem.ReferencedObject.PropertyBag.GetValueOrDefault<string>(CoreItemSchema.Subject, string.Empty), this.referenceCoreItem.ReferencedObject.PropertyBag.GetValueOrDefault<object>(CoreItemSchema.ReceivedTime));
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0002256A File Offset: 0x0002076A
		protected override void InternalDispose()
		{
			this.bestBodyCoreObjectProperties.ResetBody();
			this.referenceCoreItem.Release();
			base.InternalDispose();
			Util.DisposeIfPresent(this.watsonReportActionGuard);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00022594 File Offset: 0x00020794
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MessageAdaptor>(this);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0002259C File Offset: 0x0002079C
		private System.IO.Stream GetBodyConversionStreamCallback(BodyReadConfiguration readConfiguration)
		{
			return this.referenceCoreItem.ReferencedObject.Body.OpenReadStream(readConfiguration);
		}

		// Token: 0x040001C9 RID: 457
		private const int AssociatedFlag = 64;

		// Token: 0x040001CA RID: 458
		private readonly IDisposable watsonReportActionGuard;

		// Token: 0x040001CB RID: 459
		private readonly MessageAdaptor.Options options;

		// Token: 0x040001CC RID: 460
		private readonly Encoding string8Encoding;

		// Token: 0x040001CD RID: 461
		private readonly bool wantUnicode;

		// Token: 0x040001CE RID: 462
		private readonly Logon logon;

		// Token: 0x040001CF RID: 463
		private readonly ReferenceCount<CoreItem> referenceCoreItem;

		// Token: 0x040001D0 RID: 464
		private readonly BestBodyCoreObjectProperties bestBodyCoreObjectProperties;

		// Token: 0x040001D1 RID: 465
		private IPropertyBag propertyBag;

		// Token: 0x0200007C RID: 124
		internal struct Options
		{
			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060004D2 RID: 1234 RVA: 0x000225B4 File Offset: 0x000207B4
			public bool SkipMessagesInConflict
			{
				get
				{
					return this.SendEntryId;
				}
			}

			// Token: 0x040001D2 RID: 466
			public bool IsReadOnly;

			// Token: 0x040001D3 RID: 467
			public bool IsEmbedded;

			// Token: 0x040001D4 RID: 468
			public bool SendEntryId;

			// Token: 0x040001D5 RID: 469
			public DownloadBodyOption DownloadBodyOption;

			// Token: 0x040001D6 RID: 470
			public bool IsUpload;

			// Token: 0x040001D7 RID: 471
			public bool IsFastTransferCopyProperties;
		}

		// Token: 0x0200007D RID: 125
		private sealed class AttachmentHandleAdaptor : IAttachmentHandle
		{
			// Token: 0x060004D3 RID: 1235 RVA: 0x000225BC File Offset: 0x000207BC
			public AttachmentHandleAdaptor(CoreAttachmentCollection attachmentCollection, AttachmentHandle attachmentHandle, Encoding string8Encoding, bool wantUnicode, bool isUpload)
			{
				this.attachmentCollection = attachmentCollection;
				this.attachmentHandle = attachmentHandle;
				this.string8Encoding = string8Encoding;
				this.wantUnicode = wantUnicode;
				this.isUpload = isUpload;
			}

			// Token: 0x060004D4 RID: 1236 RVA: 0x000225EC File Offset: 0x000207EC
			public IAttachment GetAttachment()
			{
				IAttachment result;
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					CoreAttachment coreAttachment = this.attachmentCollection.Open(this.attachmentHandle, CoreObjectSchema.AllPropertiesOnStore);
					disposeGuard.Add<CoreAttachment>(coreAttachment);
					ReferenceCount<CoreAttachment> referenceCount = new ReferenceCount<CoreAttachment>(coreAttachment);
					try
					{
						AttachmentAdaptor attachmentAdaptor = new AttachmentAdaptor(referenceCount, true, this.string8Encoding, this.wantUnicode, this.isUpload);
						disposeGuard.Success();
						result = attachmentAdaptor;
					}
					finally
					{
						referenceCount.Release();
					}
				}
				return result;
			}

			// Token: 0x040001D8 RID: 472
			private readonly CoreAttachmentCollection attachmentCollection;

			// Token: 0x040001D9 RID: 473
			private readonly AttachmentHandle attachmentHandle;

			// Token: 0x040001DA RID: 474
			private readonly Encoding string8Encoding;

			// Token: 0x040001DB RID: 475
			private readonly bool wantUnicode;

			// Token: 0x040001DC RID: 476
			private readonly bool isUpload;
		}
	}
}
