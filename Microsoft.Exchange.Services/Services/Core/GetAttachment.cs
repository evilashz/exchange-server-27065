using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002FA RID: 762
	internal class GetAttachment : MultiStepServiceCommand<GetAttachmentRequest, AttachmentInfoResponseMessage>, IDisposable
	{
		// Token: 0x06001585 RID: 5509 RVA: 0x0006F5A0 File Offset: 0x0006D7A0
		public GetAttachment(CallContext callContext, GetAttachmentRequest request) : base(callContext, request)
		{
			this.attachmentIds = base.Request.AttachmentIds;
			this.responseShape = Global.ResponseShapeResolver.GetResponseShape<AttachmentResponseShape>(base.Request.ShapeName, base.Request.AttachmentShape, null);
			ServiceCommandBase.ThrowIfNullOrEmpty<AttachmentIdType>(this.attachmentIds, "attachmentIds", "GetAttachment::ctor");
			ServiceCommandBase.ThrowIfNull(this.responseShape, "responseShape", "GetAttachment::ctor");
			this.attachmentResponse = new GetAttachmentResponse();
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0006F63D File Offset: 0x0006D83D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0006F64C File Offset: 0x0006D84C
		internal bool IsPreviousResultStopBatchProcessingError(int currentIndex)
		{
			if (!this.isPreviousResultStopBatchProcessingError && currentIndex > 0)
			{
				ArrayOfResponseMessages responseMessages = this.attachmentResponse.ResponseMessages;
				ResponseMessage responseMessage = (ResponseMessage)responseMessages.Items.GetValue(currentIndex - 1);
				this.isPreviousResultStopBatchProcessingError = responseMessage.StopsBatchProcessing;
			}
			return this.isPreviousResultStopBatchProcessingError;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0006F697 File Offset: 0x0006D897
		internal override ServiceResult<AttachmentInfoResponseMessage> Execute()
		{
			return new ServiceResult<AttachmentInfoResponseMessage>(new AttachmentInfoResponseMessage(base.CurrentStep, this));
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0006F6AA File Offset: 0x0006D8AA
		internal override IExchangeWebMethodResponse GetResponse()
		{
			this.attachmentResponse.BuildForGetAttachmentResults(base.Results);
			return this.attachmentResponse;
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x0006F6C3 File Offset: 0x0006D8C3
		internal override int StepCount
		{
			get
			{
				return this.attachmentIds.Length;
			}
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0006F6D0 File Offset: 0x0006D8D0
		internal ServiceResult<AttachmentType> GetAttachmentResult(int itemIndex)
		{
			ServiceResult<AttachmentType> serviceResult = ExceptionHandler<AttachmentType>.Execute(new ExceptionHandler<AttachmentType>.CreateServiceResult(this.GetAttachmentFromRequest), itemIndex);
			base.LogServiceResultErrorAsAppropriate(serviceResult.Code, serviceResult.Error);
			return serviceResult;
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0006F704 File Offset: 0x0006D904
		private ServiceResult<AttachmentType> GetAttachmentFromRequest(int itemIndex)
		{
			AttachmentIdType attachmentIdType = this.attachmentIds[itemIndex];
			IdAndSession idAndSession = base.IdConverter.ConvertAttachmentIdToIdAndSessionReadOnly(attachmentIdType);
			AttachmentHierarchy attachmentHierarchy = null;
			bool flag = false;
			ServiceResult<AttachmentType> result = null;
			try
			{
				attachmentHierarchy = new AttachmentHierarchy(idAndSession, false, this.responseShape.ClientSupportsIrm);
				base.CallContext.AuthZBehavior.OnGetAttachment(attachmentHierarchy.RootItem.StoreObjectId);
				AttachmentHierarchyItem last = attachmentHierarchy.Last;
				AttachmentType attachmentType = this.CreateAttachmentType(attachmentIdType.Id, last);
				this.SuperSizeCheck(last.Attachment);
				object item;
				if (last.IsItemAttachment)
				{
					(attachmentType as ItemAttachmentType).Item = this.SerializeItemAttachment(idAndSession, last.XsoItem);
					item = attachmentHierarchy;
				}
				else
				{
					StreamAttachmentBase streamAttachmentBase = last.Attachment as StreamAttachmentBase;
					Stream contentStream;
					try
					{
						if (streamAttachmentBase is OleAttachment)
						{
							OleAttachment oleAttachment = (OleAttachment)streamAttachmentBase;
							contentStream = oleAttachment.ConvertToImage(ImageFormat.Jpeg);
						}
						else
						{
							contentStream = streamAttachmentBase.GetContentStream();
						}
					}
					catch (StoragePermanentException innerException)
					{
						throw new CannotOpenFileAttachmentException(innerException);
					}
					catch (StorageTransientException innerException2)
					{
						throw new CannotOpenFileAttachmentException(innerException2);
					}
					GetAttachment.AttachmentStreamWrapper attachmentStreamWrapper = new GetAttachment.AttachmentStreamWrapper(attachmentHierarchy, contentStream);
					item = attachmentStreamWrapper;
					(attachmentType as FileAttachmentType).ContentStream = contentStream;
				}
				result = new ServiceResult<AttachmentType>(attachmentType);
				this.requestItems.Add(item);
				flag = true;
			}
			finally
			{
				if (!flag && attachmentHierarchy != null)
				{
					attachmentHierarchy.Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0006F864 File Offset: 0x0006DA64
		public AttachmentType CreateAttachmentType(string attachmentId, AttachmentHierarchyItem attachment)
		{
			ServiceCommandBase.ThrowIfNullOrEmpty(attachmentId, "attachmentId", "GetAttachment:CreateAttachmentXmlNode");
			AttachmentType attachmentType;
			if (attachment.IsItemAttachment)
			{
				attachmentType = new ItemAttachmentType();
			}
			else
			{
				attachmentType = new FileAttachmentType();
			}
			attachmentType.AttachmentId = new AttachmentIdType(attachmentId);
			string text = attachment.Attachment.DisplayName;
			if (string.IsNullOrEmpty(text))
			{
				text = attachment.Attachment.FileName;
			}
			attachmentType.Name = text;
			if (!string.IsNullOrEmpty(attachment.Attachment.ContentType))
			{
				attachmentType.ContentType = attachment.Attachment.ContentType;
			}
			else
			{
				attachmentType.ContentType = attachment.Attachment.CalculatedContentType;
			}
			if (!string.IsNullOrEmpty(attachment.Attachment.ContentId))
			{
				attachmentType.ContentId = attachment.Attachment.ContentId;
			}
			if (attachment.Attachment.ContentLocation != null)
			{
				attachmentType.ContentLocation = attachment.Attachment.ContentLocation.ToString();
			}
			if (attachment.Attachment.IsInline)
			{
				attachmentType.IsInline = attachment.Attachment.IsInline;
			}
			return attachmentType;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0006F96C File Offset: 0x0006DB6C
		private ItemType SerializeItemAttachment(IdAndSession idAndSession, Item xsoItem)
		{
			if (IrmUtils.IsIrmEnabled(this.responseShape.ClientSupportsIrm, idAndSession.Session))
			{
				RightsManagedMessageItem rightsManagedMessageItem = xsoItem as RightsManagedMessageItem;
				if (rightsManagedMessageItem != null)
				{
					IrmUtils.DecryptForGetAttachment(idAndSession.Session, rightsManagedMessageItem);
				}
			}
			ToServiceObjectPropertyList toServiceObjectPropertyList = XsoDataConverter.GetToServiceObjectPropertyList(xsoItem, this.responseShape);
			xsoItem.Load(toServiceObjectPropertyList.GetPropertyDefinitions());
			toServiceObjectPropertyList.CharBuffer = this.CharBuffer;
			StoreObjectType objectType = ObjectClass.GetObjectType(xsoItem.ClassName);
			ItemType itemType = ItemType.CreateFromStoreObjectType(objectType);
			ServiceCommandBase.LoadServiceObject(itemType, xsoItem, idAndSession, this.responseShape, toServiceObjectPropertyList);
			return itemType;
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x0006F9F0 File Offset: 0x0006DBF0
		private void SuperSizeCheck(Attachment attachment)
		{
			long size = attachment.Size;
			if (size > (long)Global.GetAttachmentSizeLimit)
			{
				throw new MessageTooBigException(CoreResources.ErrorMessageSizeExceeded, null);
			}
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0006FA1C File Offset: 0x0006DC1C
		private void Dispose(bool isDisposing)
		{
			ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug<int, bool, bool>((long)this.GetHashCode(), "[GetAttachment:Dispose(bool)] Hashcode: {0}. IsDisposing: {1}, Already Disposed: {2}", this.GetHashCode(), isDisposing, this.isDisposed);
			if (!this.isDisposed)
			{
				if (isDisposing)
				{
					this.attachmentResponse = null;
					if (this.requestItems != null)
					{
						foreach (object obj in this.requestItems)
						{
							IDisposable disposable = obj as IDisposable;
							if (disposable != null)
							{
								disposable.Dispose();
							}
						}
						this.requestItems.Clear();
						this.requestItems = null;
						this.CharBuffer = null;
					}
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x04000EA0 RID: 3744
		private AttachmentIdType[] attachmentIds;

		// Token: 0x04000EA1 RID: 3745
		private AttachmentResponseShape responseShape;

		// Token: 0x04000EA2 RID: 3746
		private bool isDisposed;

		// Token: 0x04000EA3 RID: 3747
		internal char[] CharBuffer = new char[32768];

		// Token: 0x04000EA4 RID: 3748
		private List<object> requestItems = new List<object>();

		// Token: 0x04000EA5 RID: 3749
		private GetAttachmentResponse attachmentResponse;

		// Token: 0x04000EA6 RID: 3750
		private bool isPreviousResultStopBatchProcessingError;

		// Token: 0x020002FB RID: 763
		private class AttachmentStreamWrapper : IDisposable
		{
			// Token: 0x06001591 RID: 5521 RVA: 0x0006FAD8 File Offset: 0x0006DCD8
			public AttachmentStreamWrapper(AttachmentHierarchy attachmentHierarchy, Stream contentStream)
			{
				this.attachmentHierarchy = attachmentHierarchy;
				this.contentStream = contentStream;
			}

			// Token: 0x170002AB RID: 683
			// (get) Token: 0x06001592 RID: 5522 RVA: 0x0006FAEE File Offset: 0x0006DCEE
			public Stream ContentStream
			{
				get
				{
					return this.contentStream;
				}
			}

			// Token: 0x06001593 RID: 5523 RVA: 0x0006FAF8 File Offset: 0x0006DCF8
			public void Dispose()
			{
				if (!this.isDisposed)
				{
					if (this.contentStream != null)
					{
						this.contentStream.Dispose();
						this.contentStream = null;
					}
					if (this.attachmentHierarchy != null)
					{
						this.attachmentHierarchy.Dispose();
						this.attachmentHierarchy = null;
					}
					this.isDisposed = true;
				}
			}

			// Token: 0x04000EA7 RID: 3751
			private AttachmentHierarchy attachmentHierarchy;

			// Token: 0x04000EA8 RID: 3752
			private Stream contentStream;

			// Token: 0x04000EA9 RID: 3753
			private bool isDisposed;
		}
	}
}
