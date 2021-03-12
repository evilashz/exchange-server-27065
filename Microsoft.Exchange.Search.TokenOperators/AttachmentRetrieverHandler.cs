using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000003 RID: 3
	internal class AttachmentRetrieverHandler
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000223F File Offset: 0x0000043F
		internal AttachmentRetrieverHandler(IAttachmentRetrieverProducer producer, SearchConfig searchConfig, Trace tracer, int tracingContext, bool isTransport)
		{
			this.producer = producer;
			this.searchConfig = searchConfig;
			this.tracer = tracer;
			this.tracingContext = tracingContext;
			if (isTransport)
			{
				this.ErrorMessageIndex = -1;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000226F File Offset: 0x0000046F
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002277 File Offset: 0x00000477
		internal int ErrorMessageIndex { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002280 File Offset: 0x00000480
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002288 File Offset: 0x00000488
		internal long TotalBytesProcessed { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002291 File Offset: 0x00000491
		internal long TotalAttachmentsRead
		{
			get
			{
				return (long)this.totalNumberOfAttachments;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000229A File Offset: 0x0000049A
		private int ErrorCodeIndex
		{
			get
			{
				return this.producer.ErrorCodeIndex;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022A7 File Offset: 0x000004A7
		private int LastAttemptTimeIndex
		{
			get
			{
				return this.producer.LastAttemptTimeIndex;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022B4 File Offset: 0x000004B4
		private int AttachmentFileNamesIndex
		{
			get
			{
				return this.producer.AttachmentFileNamesIndex;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022C1 File Offset: 0x000004C1
		private int AttachmentsIndex
		{
			get
			{
				return this.producer.AttachmentsIndex;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022CE File Offset: 0x000004CE
		private IUpdateableRecord Holder
		{
			get
			{
				return this.producer.Holder;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022DB File Offset: 0x000004DB
		private string FlowIdentifier
		{
			get
			{
				return this.producer.FlowIdentifier;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022E8 File Offset: 0x000004E8
		private Trace Tracer
		{
			get
			{
				return this.tracer;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022F0 File Offset: 0x000004F0
		private int TracingContext
		{
			get
			{
				return this.tracingContext;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022F8 File Offset: 0x000004F8
		internal void ProcessItemForAttachments(Guid correlationId, Guid mdbGuid, Guid mailboxGuid, IRetrieverItem item, bool itemIsAnnotated, string currentPath)
		{
			this.totalNumberOfAttachments = 0;
			this.TotalBytesProcessed = 0L;
			this.sortedAttachments = new SortedDictionary<int, List<Uri>>();
			this.itemTracingId = currentPath;
			this.processingAnnotatedItem = itemIsAnnotated;
			this.ProcessItemForAttachments(correlationId, mdbGuid, mailboxGuid, item, 1, currentPath);
			foreach (int key in this.sortedAttachments.Keys)
			{
				foreach (Uri uri in this.sortedAttachments[key])
				{
					ExTraceGlobals.RetrieverOperatorTracer.TraceDebug<string>((long)this.TracingContext, "Adding Uri for:  {0}", uri.AbsolutePath);
					((IUpdateableListField<Uri>)this.Holder[this.AttachmentsIndex]).Add(uri);
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023FC File Offset: 0x000005FC
		private void ProcessItemForAttachments(Guid correlationId, Guid mdbGuid, Guid mailboxGuid, IRetrieverItem item, int level, string currentPath)
		{
			if (level > this.searchConfig.MaxAttachmentDepth)
			{
				this.MarkupRecordForAttachmentFailure();
				return;
			}
			Dictionary<string, IRetrieverItem> dictionary = new Dictionary<string, IRetrieverItem>(item.AttachmentCollection.Count);
			foreach (IRetrieverAttachmentHandle retrieverAttachmentHandle in item.AttachmentCollection)
			{
				if (this.totalNumberOfAttachments >= this.searchConfig.MaxAttachmentCount)
				{
					this.MarkupRecordForAttachmentFailure();
					break;
				}
				using (IRetrieverAttachment retrieverAttachment = item.AttachmentCollection.Open(retrieverAttachmentHandle))
				{
					if (retrieverAttachment == null)
					{
						this.MarkupRecordForAttachmentFailure();
					}
					else if (retrieverAttachment.Size > this.searchConfig.MaxAttachmentSize)
					{
						this.MarkupRecordForAttachmentFailure();
					}
					else if (retrieverAttachment.IsImage && !this.searchConfig.ProcessImages)
					{
						if (this.searchConfig.MarkSkippedImagesAsPartiallyProcessed)
						{
							this.MarkupRecordForAttachmentFailure();
						}
					}
					else if (!retrieverAttachment.IsInline && retrieverAttachment.IsSupportedAttachMethod)
					{
						string text = string.Format("{0}.{1}", currentPath, retrieverAttachmentHandle.AttachNumber);
						Uri attachmentUri = ExchangeStream.GenerateNewAttachmentUri(this.FlowIdentifier, mdbGuid, mailboxGuid, correlationId, text, retrieverAttachment.FileName ?? string.Empty);
						this.Tracer.TraceDebug((long)this.TracingContext, "Processing Attachment:");
						this.Tracer.TraceDebug<string>((long)this.TracingContext, "                  path:  {0}", text);
						this.Tracer.TraceDebug<string>((long)this.TracingContext, "                  name:  {0}", retrieverAttachment.FileName ?? string.Empty);
						this.Tracer.TraceDebug<long>((long)this.TracingContext, "                  size:  {0}", retrieverAttachment.Size);
						IRetrieverItemAttachment retrieverItemAttachment = retrieverAttachment as IRetrieverItemAttachment;
						if (retrieverItemAttachment != null)
						{
							this.totalNumberOfAttachments++;
							if (this.processingAnnotatedItem && this.searchConfig.UseMdmFlow && !this.searchConfig.SkipMdmGeneration)
							{
								continue;
							}
							using (DisposeGuard disposeGuard = default(DisposeGuard))
							{
								IRetrieverItem item2 = retrieverItemAttachment.GetItem();
								if (item2 == null)
								{
									this.MarkupRecordForRightsManagmentFailure(text);
									continue;
								}
								disposeGuard.Add<IRetrieverItem>(item2);
								Stream itemPropertiesStream = this.GetItemPropertiesStream(item2);
								ItemAttachmentWrapper itemAttachmentWrapper = new ItemAttachmentWrapper(itemPropertiesStream);
								AttachmentStream attachmentStream = new AttachmentStream(itemAttachmentWrapper);
								disposeGuard.Add<AttachmentStream>(attachmentStream);
								disposeGuard.Add<ItemAttachmentWrapper>(itemAttachmentWrapper);
								disposeGuard.Add<Stream>(itemPropertiesStream);
								this.TotalBytesProcessed += itemPropertiesStream.Length;
								ItemDepot.Instance.DepositItem(this.FlowIdentifier, attachmentStream, text);
								ItemDepot.Instance.DepositItem(this.FlowIdentifier, itemAttachmentWrapper, text + "ItemAttachmentWrapper");
								if (item2.AttachmentCollection.Count > 0)
								{
									dictionary.Add(text, item2);
								}
								else
								{
									item2.Dispose();
								}
								disposeGuard.Success();
								itemPropertiesStream.Dispose();
								this.AddAttachmentUriToSortedCollection(level, attachmentUri);
								continue;
							}
						}
						if (!string.IsNullOrEmpty(retrieverAttachment.FileName))
						{
							((IUpdateableListField<string>)this.Holder[this.AttachmentFileNamesIndex]).Add(retrieverAttachment.FileName);
						}
						IRetrieverStreamAttachment retrieverStreamAttachment = retrieverAttachment as IRetrieverStreamAttachment;
						if (retrieverStreamAttachment != null)
						{
							this.totalNumberOfAttachments++;
							if (!this.processingAnnotatedItem)
							{
								using (DisposeGuard disposeGuard2 = default(DisposeGuard))
								{
									StreamAttachmentWrapper streamAttachmentWrapper = new StreamAttachmentWrapper(retrieverStreamAttachment);
									AttachmentStream attachmentStream2 = new AttachmentStream(streamAttachmentWrapper);
									disposeGuard2.Add<StreamAttachmentWrapper>(streamAttachmentWrapper);
									disposeGuard2.Add<AttachmentStream>(attachmentStream2);
									this.TotalBytesProcessed += attachmentStream2.Length;
									ItemDepot.Instance.DepositItem(this.FlowIdentifier, attachmentStream2, text);
									ItemDepot.Instance.DepositItem(this.FlowIdentifier, streamAttachmentWrapper, text + "StreamAttachmentWrapper");
									disposeGuard2.Success();
									this.AddAttachmentUriToSortedCollection(level, attachmentUri);
								}
							}
						}
					}
				}
			}
			try
			{
				foreach (string text2 in dictionary.Keys)
				{
					this.ProcessItemForAttachments(correlationId, mdbGuid, mailboxGuid, dictionary[text2], level + 1, text2);
				}
			}
			finally
			{
				foreach (IDisposable disposable in dictionary.Values)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000290C File Offset: 0x00000B0C
		private void MarkupRecordForAttachmentFailure()
		{
			ManagedProperties.SetAsPartiallyProcessed(this.Holder);
			((IUpdateableInt32Field)this.Holder[this.ErrorCodeIndex]).Int32Value = EvaluationErrorsHelper.MakePermanentError(EvaluationErrors.AttachmentLimitReached);
			if (this.LastAttemptTimeIndex != -1)
			{
				((IUpdateableDateTimeField)this.Holder[this.LastAttemptTimeIndex]).DateTimeValue = Util.NormalizeDateTime(DateTime.UtcNow);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002974 File Offset: 0x00000B74
		private void MarkupRecordForRightsManagmentFailure(string attachmentPath)
		{
			ManagedProperties.SetAsPartiallyProcessed(this.Holder);
			((IUpdateableInt32Field)this.Holder[this.ErrorCodeIndex]).Int32Value = EvaluationErrorsHelper.MakePermanentError(EvaluationErrors.RightsManagementFailure);
			if (this.LastAttemptTimeIndex != -1)
			{
				((IUpdateableDateTimeField)this.Holder[this.LastAttemptTimeIndex]).DateTimeValue = Util.NormalizeDateTime(DateTime.UtcNow);
			}
			if (this.ErrorMessageIndex != -1)
			{
				IUpdateableStringField updateableStringField = (IUpdateableStringField)this.Holder[this.ErrorMessageIndex];
				string stringValue = string.IsNullOrEmpty(updateableStringField.StringValue) ? attachmentPath : string.Format("{0} {1}", updateableStringField.StringValue, attachmentPath);
				updateableStringField.StringValue = stringValue;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002A28 File Offset: 0x00000C28
		private Stream GetItemPropertiesStream(IRetrieverItem item)
		{
			Stream stream = new LohFriendlyStream(1024);
			foreach (IRetrieverPropertyDefinition retrieverPropertyDefinition in item.EmbeddedMessageProperties)
			{
				if (!this.processingAnnotatedItem || (retrieverPropertyDefinition.Attributes & PropertyDefinitionAttribute.PartOfAttachmentAnnotation) != PropertyDefinitionAttribute.PartOfAttachmentAnnotation)
				{
					object obj = null;
					try
					{
						obj = item.GetValue(retrieverPropertyDefinition);
					}
					catch (Exception ex)
					{
						this.Tracer.TraceDebug<string, string, string>((long)this.TracingContext, "[{0}] [Attachment GET] {1} : {2}", this.itemTracingId, retrieverPropertyDefinition.Name, ex.Message);
						throw;
					}
					try
					{
						List<string> list = obj as List<string>;
						if (list != null)
						{
							using (List<string>.Enumerator enumerator2 = list.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									string value = enumerator2.Current;
									this.AppendIndexedStringValue(stream, value, retrieverPropertyDefinition);
								}
								continue;
							}
						}
						this.AppendIndexedStringValue(stream, obj as string, retrieverPropertyDefinition);
					}
					catch (Exception ex2)
					{
						this.Tracer.TraceDebug<string, string, string>((long)this.TracingContext, "[{0}] [Attachment SET] {1} : {2}", this.itemTracingId, retrieverPropertyDefinition.Name, ex2.Message);
						throw;
					}
				}
			}
			stream.Position = 0L;
			return stream;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002B80 File Offset: 0x00000D80
		private void AppendIndexedStringValue(Stream target, string value, IRetrieverPropertyDefinition property)
		{
			if (!string.IsNullOrEmpty(value))
			{
				int maxByteCount = AttachmentRetrieverHandler.defaultEncoding.GetMaxByteCount(value.Length);
				BufferPoolCollection.BufferSize bufferSize = BufferPoolCollection.BufferSize.Size1K;
				byte[] array = null;
				if (BufferPoolCollection.AutoCleanupCollection.TryMatchBufferSize(maxByteCount, out bufferSize))
				{
					BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(bufferSize);
					try
					{
						array = bufferPool.Acquire();
						int bytes = AttachmentRetrieverHandler.defaultEncoding.GetBytes(value, 0, value.Length, array, 0);
						target.Write(array, 0, bytes);
						goto IL_A7;
					}
					finally
					{
						bufferPool.Release(array);
					}
				}
				array = AttachmentRetrieverHandler.defaultEncoding.GetBytes(value);
				target.Write(array, 0, array.Length);
				this.Tracer.TracePerformance<int, int>((long)Thread.CurrentThread.ManagedThreadId, "Allocated new byte array of size {0} to convert string of length {1}", array.Length, value.Length);
				IL_A7:
				target.Write(AttachmentRetrieverHandler.space, 0, AttachmentRetrieverHandler.space.Length);
				if ((property.Attributes & PropertyDefinitionAttribute.SkipValueTracing) == PropertyDefinitionAttribute.None)
				{
					this.Tracer.TraceDebug<string, string, string>((long)this.TracingContext, "[{0}] [Attachment SET] {1} : {2}", this.itemTracingId, property.Name, value);
					return;
				}
				this.Tracer.TraceDebug<string, string, string>((long)this.TracingContext, "[{0}] [Attachment SET] {1} : {2}", this.itemTracingId, property.Name, "-Value Set-");
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002CB0 File Offset: 0x00000EB0
		private void AddAttachmentUriToSortedCollection(int level, Uri attachmentUri)
		{
			if (!this.sortedAttachments.ContainsKey(level))
			{
				this.sortedAttachments[level] = new List<Uri>(4);
			}
			this.sortedAttachments[level].Add(attachmentUri);
		}

		// Token: 0x04000008 RID: 8
		private static Encoding defaultEncoding = Encoding.UTF8;

		// Token: 0x04000009 RID: 9
		private static byte[] space = AttachmentRetrieverHandler.defaultEncoding.GetBytes(" ");

		// Token: 0x0400000A RID: 10
		private readonly IAttachmentRetrieverProducer producer;

		// Token: 0x0400000B RID: 11
		private readonly SearchConfig searchConfig;

		// Token: 0x0400000C RID: 12
		private readonly Trace tracer;

		// Token: 0x0400000D RID: 13
		private readonly int tracingContext;

		// Token: 0x0400000E RID: 14
		private int totalNumberOfAttachments;

		// Token: 0x0400000F RID: 15
		private SortedDictionary<int, List<Uri>> sortedAttachments;

		// Token: 0x04000010 RID: 16
		private string itemTracingId;

		// Token: 0x04000011 RID: 17
		private bool processingAnnotatedItem;
	}
}
