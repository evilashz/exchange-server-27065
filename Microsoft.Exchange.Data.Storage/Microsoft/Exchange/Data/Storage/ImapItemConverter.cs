using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005DD RID: 1501
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ImapItemConverter : IImapItemConverter
	{
		// Token: 0x06003DC0 RID: 15808 RVA: 0x000FF058 File Offset: 0x000FD258
		public ImapItemConverter(Item itemIn, OutboundConversionOptions options)
		{
			StorageGlobals.ContextTraceInformation(ExTraceGlobals.CcOutboundMimeTracer, "ImapItemConverter::ctor.");
			using (DisposeGuard disposeGuard = this.Guard())
			{
				Util.ThrowOnNullArgument(itemIn, "itemIn");
				Util.ThrowOnNullArgument(options, "options");
				Util.ThrowOnNullOrEmptyArgument(options.ImceaEncapsulationDomain, "options.ImceaEncapsulationDomain");
				if (!ItemConversion.IsItemClassConvertibleToMime(itemIn.ClassName))
				{
					StorageGlobals.ContextTraceError<string>(ExTraceGlobals.CcOutboundMimeTracer, "ImapItemConverter::CheckItemType: wrong item type, {0}", itemIn.ClassName);
					throw new WrongObjectTypeException(ServerStrings.ConversionInvalidItemType(itemIn.ClassName));
				}
				this.itemIn = itemIn;
				this.options = options;
				using (StorageGlobals.SetTraceContext(this.options))
				{
					using (StorageGlobals.SetTraceContext(this.itemIn))
					{
						if (this.options.GenerateMimeSkeleton)
						{
							PropertyError propertyError = this.itemIn.TryGetProperty(InternalSchema.MimeSkeleton) as PropertyError;
							if (propertyError != null && propertyError.PropertyErrorCode == PropertyErrorCode.NotFound)
							{
								if (this.itemIn.IsReadOnly)
								{
									this.itemIn.OpenAsReadWrite();
								}
								this.itemNeedsSave = true;
								using (Stream stream = this.itemIn.OpenPropertyStream(InternalSchema.MimeSkeleton, PropertyOpenMode.Create))
								{
									using (Stream stream2 = new MimeStreamWriter.MimeTextStream(null))
									{
										using (ItemToMimeConverter itemToMimeConverter = new ItemToMimeConverter(itemIn, options, ConverterFlags.GenerateSkeleton))
										{
											using (MimeStreamWriter mimeStreamWriter = new MimeStreamWriter(stream2, stream, itemToMimeConverter.GetItemMimeEncodingOptions(this.options), MimeStreamWriter.Flags.ForceMime))
											{
												ConversionLimitsTracker limits = new ConversionLimitsTracker(this.options.Limits);
												itemToMimeConverter.ConvertItemToMime(mimeStreamWriter, limits);
											}
										}
									}
								}
							}
						}
						ItemToMimeConverter itemToMimeConverter2 = new ItemToMimeConverter(itemIn, options, ConverterFlags.None);
						this.mimeProvider = IImapMimeProvider.CreateInstance(itemToMimeConverter2);
						this.itemEncodingOptions = itemToMimeConverter2.GetItemMimeEncodingOptions(this.options);
					}
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x000FF2FC File Offset: 0x000FD4FC
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ImapItemConverter>(this);
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x000FF304 File Offset: 0x000FD504
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
			if (disposing)
			{
				if (this.mimeProvider != null)
				{
					this.mimeProvider.Dispose();
				}
				foreach (MimeDocument mimeDocument in this.ownedSkeletonList)
				{
					mimeDocument.Dispose();
				}
				this.ownedSkeletonList.Clear();
			}
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x000FF380 File Offset: 0x000FD580
		public override bool ItemNeedsSave
		{
			get
			{
				this.CheckDisposed(null);
				return this.itemNeedsSave;
			}
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x000FF390 File Offset: 0x000FD590
		public override MimePartInfo GetMimeStructure()
		{
			StorageGlobals.ContextTraceInformation(ExTraceGlobals.CcOutboundMimeTracer, "ImapItemConverter::GetMimeStructure.");
			this.CheckDisposed(null);
			MimePartInfo result;
			using (StorageGlobals.SetTraceContext(this.options))
			{
				using (StorageGlobals.SetTraceContext(this.itemIn))
				{
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Starting item (ImapItemConverter.GetMimeStructure)");
					MimePartInfo mimeSkeleton = this.GetMimeSkeleton();
					if (mimeSkeleton.IsBodySizeComputed)
					{
						this.GetHeaders();
					}
					else
					{
						using (MimeStreamWriter mimeStreamWriter = new MimeStreamWriter((MimeStreamWriter.Flags)6, this.itemEncodingOptions))
						{
							this.WriteMimePart(this.mimeProvider, mimeStreamWriter, this.options, mimeSkeleton, ItemToMimeConverter.MimeFlags.WriteMessageHeaders);
						}
					}
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing item (ImapItemConverter.GetMimeStructure)");
					result = mimeSkeleton;
				}
			}
			return result;
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x000FF474 File Offset: 0x000FD674
		public override void GetBody(Stream outStream)
		{
			StorageGlobals.ContextTraceInformation(ExTraceGlobals.CcOutboundMimeTracer, "ImapItemConverter::GetBody.1");
			this.CheckDisposed(null);
			if (outStream == null)
			{
				throw new ArgumentNullException("outStream");
			}
			using (StorageGlobals.SetTraceContext(this.options))
			{
				using (StorageGlobals.SetTraceContext(this.itemIn))
				{
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Starting item (ImapItemConverter.GetBody.1)");
					MimePartInfo mimeSkeleton = this.GetMimeSkeleton();
					using (Stream stream = new StreamWrapper(outStream, false))
					{
						using (MimeStreamWriter mimeStreamWriter = new MimeStreamWriter(stream, this.itemEncodingOptions, (MimeStreamWriter.Flags)6))
						{
							this.WriteMimePart(this.mimeProvider, mimeStreamWriter, this.options, mimeSkeleton, ItemToMimeConverter.MimeFlags.WriteMessageHeaders);
						}
					}
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing item (ImapItemConverter.GetBody.1)");
				}
			}
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x000FF578 File Offset: 0x000FD778
		public override bool GetBody(Stream outStream, uint[] indices)
		{
			StorageGlobals.ContextTraceInformation(ExTraceGlobals.CcOutboundMimeTracer, "ImapItemConverter::GetBody.2");
			this.CheckDisposed(null);
			bool result;
			using (StorageGlobals.SetTraceContext(this.options))
			{
				using (StorageGlobals.SetTraceContext(this.itemIn))
				{
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Starting item (ImapItemConverter.GetBody.2)");
					bool flag = false;
					if (indices == null || indices.Length == 0)
					{
						this.GetBody(outStream);
						flag = true;
					}
					else
					{
						List<IDisposable> disposeList = null;
						MimePartInfo partInfo = null;
						Item item = null;
						try
						{
							EncodingOptions encodingOptions;
							IImapMimeProvider subpartConverter = this.GetSubpartConverter(indices, out partInfo, out item, out encodingOptions, out disposeList);
							if (subpartConverter != null)
							{
								using (Stream stream = new StreamWrapper(outStream, false))
								{
									using (MimeStreamWriter mimeStreamWriter = new MimeStreamWriter(stream, encodingOptions, (MimeStreamWriter.Flags)3))
									{
										this.WriteMimePart(subpartConverter, mimeStreamWriter, this.options, partInfo, ItemToMimeConverter.MimeFlags.SkipMessageHeaders);
									}
								}
								flag = true;
							}
						}
						finally
						{
							this.DisposeAll(disposeList);
						}
					}
					if (!flag)
					{
						int num = indices.Length - 1;
						if (indices[num] == 1U)
						{
							uint[] array = new uint[num];
							Array.Copy(indices, array, num);
							flag = this.GetText(outStream, array, true);
						}
					}
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing item (ImapItemConverter.GetBody.2)");
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x000FF6E0 File Offset: 0x000FD8E0
		public override void GetText(Stream outStream)
		{
			this.GetText(outStream, false);
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x000FF6EC File Offset: 0x000FD8EC
		private bool GetText(Stream outStream, bool isSinglePartOnly)
		{
			StorageGlobals.ContextTraceInformation(ExTraceGlobals.CcOutboundMimeTracer, "ImapItemConverter::GetText.1");
			this.CheckDisposed(null);
			if (outStream == null)
			{
				throw new ArgumentNullException("outStream");
			}
			using (StorageGlobals.SetTraceContext(this.options))
			{
				using (StorageGlobals.SetTraceContext(this.itemIn))
				{
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Starting item (ImapItemConverter.GetText.1)");
					MimePartInfo mimeSkeleton = this.GetMimeSkeleton();
					if (isSinglePartOnly && mimeSkeleton.IsMultipart)
					{
						return false;
					}
					using (Stream stream = new StreamWrapper(outStream, false))
					{
						using (MimeStreamWriter mimeStreamWriter = new MimeStreamWriter(stream, this.itemEncodingOptions, (MimeStreamWriter.Flags)3))
						{
							this.WriteMimePart(this.mimeProvider, mimeStreamWriter, this.options, mimeSkeleton, ItemToMimeConverter.MimeFlags.SkipMessageHeaders);
						}
					}
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing item (ImapItemConverter.GetText.1)");
				}
			}
			return true;
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x000FF804 File Offset: 0x000FDA04
		public override bool GetText(Stream outStream, uint[] indices)
		{
			return this.GetText(outStream, indices, false);
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x000FF810 File Offset: 0x000FDA10
		private bool GetText(Stream outStream, uint[] indices, bool isSinglePartOnly)
		{
			StorageGlobals.ContextTraceInformation(ExTraceGlobals.CcOutboundMimeTracer, "ImapItemConverter::GetText.2");
			this.CheckDisposed(null);
			bool result;
			using (StorageGlobals.SetTraceContext(this.options))
			{
				using (StorageGlobals.SetTraceContext(this.itemIn))
				{
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Starting item (ImapItemConverter.GetText.2)");
					bool flag = false;
					if (indices == null || indices.Length == 0)
					{
						flag = this.GetText(outStream, isSinglePartOnly);
					}
					else
					{
						List<IDisposable> disposeList = null;
						MimePartInfo mimePartInfo = null;
						Item item = null;
						EncodingOptions encodingOptions = null;
						try
						{
							IImapMimeProvider imapMimeProvider = this.GetSubpartConverter(indices, out mimePartInfo, out item, out encodingOptions, out disposeList);
							if (imapMimeProvider != null)
							{
								imapMimeProvider = this.GetAttachedItemConverter(ref mimePartInfo, ref item, ref encodingOptions, disposeList);
								if (imapMimeProvider != null && (!mimePartInfo.IsMultipart || !isSinglePartOnly))
								{
									using (Stream stream = new StreamWrapper(outStream, false))
									{
										using (MimeStreamWriter mimeStreamWriter = new MimeStreamWriter(stream, encodingOptions, MimeStreamWriter.Flags.SkipHeaders))
										{
											this.WriteMimePart(imapMimeProvider, mimeStreamWriter, this.options, mimePartInfo, ItemToMimeConverter.MimeFlags.SkipMessageHeaders);
										}
									}
									flag = true;
								}
							}
						}
						finally
						{
							this.DisposeAll(disposeList);
						}
					}
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing item (ImapItemConverter.GetText.2)");
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x000FF96C File Offset: 0x000FDB6C
		public override MimePartHeaders GetHeaders()
		{
			StorageGlobals.ContextTraceInformation(ExTraceGlobals.CcOutboundMimeTracer, "ImapItemConverter::GetHeaders.1");
			this.CheckDisposed(null);
			MimePartHeaders headers;
			using (StorageGlobals.SetTraceContext(this.options))
			{
				using (StorageGlobals.SetTraceContext(this.itemIn))
				{
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Starting item (ImapItemConverter.GetHeaders.1)");
					MimePartInfo mimeSkeleton = this.GetMimeSkeleton();
					if (mimeSkeleton.Headers == null || mimeSkeleton.Headers.Count == 0)
					{
						using (MimeStreamWriter mimeStreamWriter = new MimeStreamWriter((MimeStreamWriter.Flags)6, this.itemEncodingOptions))
						{
							this.WriteMimePart(this.mimeProvider, mimeStreamWriter, this.options, mimeSkeleton, ItemToMimeConverter.MimeFlags.WriteMessageHeaders | ItemToMimeConverter.MimeFlags.SkipContent);
						}
					}
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing item (ImapItemConverter.GetHeaders.1)");
					headers = mimeSkeleton.Headers;
				}
			}
			return headers;
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x000FFA58 File Offset: 0x000FDC58
		public override MimePartHeaders GetHeaders(uint[] indices)
		{
			StorageGlobals.ContextTraceInformation(ExTraceGlobals.CcOutboundMimeTracer, "ImapItemConverter::GetHeaders.2");
			this.CheckDisposed(null);
			MimePartHeaders result;
			using (StorageGlobals.SetTraceContext(this.options))
			{
				using (StorageGlobals.SetTraceContext(this.itemIn))
				{
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Starting item (ImapItemConverter.GetHeaders.2)");
					if (indices == null || indices.Length == 0)
					{
						StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing item (ImapItemConverter.GetHeaders.2)");
						result = this.GetHeaders();
					}
					else
					{
						MimePartInfo mimePartInfo = this.FindMimePart(indices);
						if (mimePartInfo != null)
						{
							if (mimePartInfo.ContentType != MimePartContentType.ItemAttachment)
							{
								StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing item (ImapItemConverter.GetHeaders.2)");
								return null;
							}
							mimePartInfo = mimePartInfo.AttachedItemStructure;
							if (mimePartInfo != null && mimePartInfo.Headers != null && mimePartInfo.Headers.Count != 0)
							{
								StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing item (ImapItemConverter.GetHeaders.2)");
								return mimePartInfo.Headers;
							}
						}
						mimePartInfo = this.GetMimeSkeleton();
						List<IDisposable> disposeList = null;
						Item item = null;
						try
						{
							EncodingOptions encodingOptions;
							if (this.GetSubpartConverter(indices, out mimePartInfo, out item, out encodingOptions, out disposeList) == null)
							{
								StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing item (ImapItemConverter.GetHeaders.2)");
								result = null;
							}
							else
							{
								IImapMimeProvider attachedItemConverter = this.GetAttachedItemConverter(ref mimePartInfo, ref item, ref encodingOptions, disposeList);
								if (attachedItemConverter == null)
								{
									StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing item (ImapItemConverter.GetHeaders.2)");
									result = null;
								}
								else
								{
									using (MimeStreamWriter mimeStreamWriter = new MimeStreamWriter((MimeStreamWriter.Flags)6, encodingOptions))
									{
										this.WriteMimePart(attachedItemConverter, mimeStreamWriter, this.options, mimePartInfo, ItemToMimeConverter.MimeFlags.WriteMessageHeaders | ItemToMimeConverter.MimeFlags.SkipContent);
									}
									StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing item (ImapItemConverter.GetHeaders.2)");
									result = mimePartInfo.Headers;
								}
							}
						}
						finally
						{
							this.DisposeAll(disposeList);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x000FFC54 File Offset: 0x000FDE54
		public override MimePartHeaders GetMime(uint[] indices)
		{
			StorageGlobals.ContextTraceInformation(ExTraceGlobals.CcOutboundMimeTracer, "ImapItemConverter::GetMime.");
			this.CheckDisposed(null);
			if (indices == null || indices.Length == 0)
			{
				return null;
			}
			MimePartHeaders result;
			using (StorageGlobals.SetTraceContext(this.options))
			{
				using (StorageGlobals.SetTraceContext(this.itemIn))
				{
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Starting item (ImapItemConverter.GetMime)");
					MimePartHeaders mimePartHeaders = null;
					MimePartInfo mimePartInfo = this.FindMimePart(indices);
					if (mimePartInfo != null && mimePartInfo.Headers != null && mimePartInfo.Headers.Count != 0)
					{
						mimePartHeaders = mimePartInfo.Headers;
					}
					else
					{
						mimePartInfo = this.GetMimeSkeleton();
						List<IDisposable> disposeList = null;
						Item item = null;
						EncodingOptions encodingOptions = null;
						try
						{
							IImapMimeProvider subpartConverter = this.GetSubpartConverter(indices, out mimePartInfo, out item, out encodingOptions, out disposeList);
							if (subpartConverter != null)
							{
								using (MimeStreamWriter mimeStreamWriter = new MimeStreamWriter(MimeStreamWriter.Flags.ProduceMimeStructure, encodingOptions))
								{
									this.WriteMimePart(subpartConverter, mimeStreamWriter, this.options, mimePartInfo, ItemToMimeConverter.MimeFlags.SkipContent);
								}
								mimePartHeaders = mimePartInfo.Headers;
							}
						}
						finally
						{
							this.DisposeAll(disposeList);
						}
					}
					StorageGlobals.ContextTracePfd(ExTraceGlobals.CcPFDTracer, "Finishing item (ImapItemConverter.GetMime)");
					result = mimePartHeaders;
				}
			}
			return result;
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x000FFDD8 File Offset: 0x000FDFD8
		private void WriteMimePart(IImapMimeProvider mimeProvider, MimeStreamWriter writer, OutboundConversionOptions options, MimePartInfo partInfo, ItemToMimeConverter.MimeFlags conversionFlags)
		{
			try
			{
				ConvertUtils.CallCts(ExTraceGlobals.CcOutboundMimeTracer, "ImapItemConverter::WriteMimePart", ServerStrings.ConversionCorruptContent, delegate
				{
					ConversionLimitsTracker limits = new ConversionLimitsTracker(options.Limits);
					mimeProvider.WriteMimePart(writer, limits, partInfo, conversionFlags);
				});
			}
			catch (StoragePermanentException exc)
			{
				ItemConversion.SaveFailedItem(this.itemIn, options, exc);
				throw;
			}
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x000FFE60 File Offset: 0x000FE060
		private MimePartInfo FindMimePart(uint[] indices)
		{
			MimePartInfo mimePartInfo = this.GetMimeSkeleton();
			int num = indices.Length;
			for (int num2 = 0; num2 != num; num2++)
			{
				int num3 = (int)(indices[num2] - 1U);
				if (num3 < 0)
				{
					throw new ArgumentException("indices");
				}
				if (mimePartInfo.ContentType == MimePartContentType.ItemAttachment)
				{
					mimePartInfo = mimePartInfo.AttachedItemStructure;
					if (mimePartInfo == null)
					{
						return null;
					}
				}
				if (mimePartInfo.Children == null || mimePartInfo.Children.Count <= num3)
				{
					return null;
				}
				mimePartInfo = mimePartInfo.Children[num3];
			}
			return mimePartInfo;
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x000FFED8 File Offset: 0x000FE0D8
		private IImapMimeProvider GetSubpartConverter(uint[] indices, out MimePartInfo part, out Item item, out EncodingOptions encodingOptions, out List<IDisposable> disposeList)
		{
			part = null;
			item = null;
			disposeList = null;
			encodingOptions = null;
			List<IDisposable> list = new List<IDisposable>();
			IImapMimeProvider result;
			try
			{
				IImapMimeProvider attachedItemConverter = this.mimeProvider;
				Item item2 = this.itemIn;
				EncodingOptions encodingOptions2 = this.itemEncodingOptions;
				MimePartInfo mimePartInfo = this.GetMimeSkeleton();
				for (int num = 0; num != indices.Length; num++)
				{
					if (mimePartInfo.ContentType == MimePartContentType.ItemAttachment)
					{
						attachedItemConverter = this.GetAttachedItemConverter(ref mimePartInfo, ref item2, ref encodingOptions2, list);
						if (attachedItemConverter == null)
						{
							return null;
						}
					}
					int num2 = (int)(indices[num] - 1U);
					if (num2 < 0)
					{
						throw new ArgumentException("indices");
					}
					if (mimePartInfo.Children == null || mimePartInfo.Children.Count <= num2)
					{
						return null;
					}
					mimePartInfo = mimePartInfo.Children[num2];
				}
				part = mimePartInfo;
				item = item2;
				encodingOptions = encodingOptions2;
				disposeList = list;
				list = null;
				result = attachedItemConverter;
			}
			finally
			{
				this.DisposeAll(list);
			}
			return result;
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000FFFC0 File Offset: 0x000FE1C0
		private IImapMimeProvider GetAttachedItemConverter(ref MimePartInfo part, ref Item item, ref EncodingOptions itemEncodingOptions, List<IDisposable> disposeList)
		{
			if (part.ContentType != MimePartContentType.ItemAttachment)
			{
				return null;
			}
			IImapMimeProvider imapMimeProvider;
			if (part.SmimePart == null)
			{
				Attachment attachment = item.AttachmentCollection.Open(part.AttachmentId, InternalSchema.ContentConversionProperties);
				disposeList.Add(attachment);
				ItemAttachment itemAttachment = attachment as ItemAttachment;
				if (itemAttachment == null)
				{
					StorageGlobals.ContextTraceError(ExTraceGlobals.CcOutboundMimeTracer, "ImapItemConverter::GetAttachedItemConverter: attachment is not an item.");
					ExAssert.RetailAssert(false, "ImapItemConverter::GetAttachedItemConverter: attachment is not an item.");
					return null;
				}
				item = ConvertUtils.OpenAttachedItem(itemAttachment);
				if (item == null)
				{
					return null;
				}
				disposeList.Add(item);
				ItemToMimeConverter itemToMimeConverter = new ItemToMimeConverter(item, this.options, ConverterFlags.IsEmbeddedMessage);
				imapMimeProvider = IImapMimeProvider.CreateInstance(itemToMimeConverter);
				disposeList.Add(imapMimeProvider);
				itemEncodingOptions = itemToMimeConverter.GetItemMimeEncodingOptions(this.options);
				if (part.AttachedItemStructure != null)
				{
					itemToMimeConverter.SetSkeletonAndSmimeDoc(part.AttachedItemStructure.Skeleton, part.AttachedItemStructure.SmimeDocument);
				}
			}
			else
			{
				imapMimeProvider = IImapMimeProvider.CreateInstance((MimePart)part.SmimePart.FirstChild, part.SmimeDocument);
				disposeList.Add(imapMimeProvider);
				itemEncodingOptions = this.itemEncodingOptions;
			}
			if (part.AttachedItemStructure == null)
			{
				part.AttachedItemStructure = imapMimeProvider.CalculateMimeStructure(Charset.GetCharset(itemEncodingOptions.CharsetName));
			}
			part = part.AttachedItemStructure;
			return imapMimeProvider;
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x00100118 File Offset: 0x000FE318
		private MimePartInfo GetMimeSkeleton()
		{
			if (this.structure == null)
			{
				ConvertUtils.CallCts(ExTraceGlobals.CcOutboundMimeTracer, "ImapItemConvetrer::GetMimeSkeleton", ServerStrings.ConversionCorruptContent, delegate
				{
					this.structure = this.mimeProvider.CalculateMimeStructure(Charset.GetCharset(this.itemEncodingOptions.CharsetName));
				});
			}
			return this.structure;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x0010015C File Offset: 0x000FE35C
		private void DisposeAll(List<IDisposable> disposeList)
		{
			if (disposeList == null)
			{
				return;
			}
			for (int i = disposeList.Count - 1; i >= 0; i--)
			{
				IImapMimeProvider imapMimeProvider = disposeList[i] as IImapMimeProvider;
				if (imapMimeProvider != null)
				{
					this.ownedSkeletonList.AddRange(imapMimeProvider.ExtractSkeletons());
				}
				disposeList[i].Dispose();
			}
		}

		// Token: 0x04002128 RID: 8488
		private Item itemIn;

		// Token: 0x04002129 RID: 8489
		private OutboundConversionOptions options;

		// Token: 0x0400212A RID: 8490
		private IImapMimeProvider mimeProvider;

		// Token: 0x0400212B RID: 8491
		private MimePartInfo structure;

		// Token: 0x0400212C RID: 8492
		private EncodingOptions itemEncodingOptions;

		// Token: 0x0400212D RID: 8493
		private bool itemNeedsSave;

		// Token: 0x0400212E RID: 8494
		private List<MimeDocument> ownedSkeletonList = new List<MimeDocument>();
	}
}
