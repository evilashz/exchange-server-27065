using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200020D RID: 525
	internal class XsoContentProperty : XsoProperty, IContentProperty, IMIMEDataProperty, IMIMERelatedProperty, IProperty
	{
		// Token: 0x0600143A RID: 5178 RVA: 0x00074A9C File Offset: 0x00072C9C
		public XsoContentProperty(PropertyType type) : base(null, type)
		{
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x00074AA6 File Offset: 0x00072CA6
		public XsoContentProperty() : base(null, PropertyType.ReadWrite)
		{
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00074AB0 File Offset: 0x00072CB0
		public Stream Body
		{
			get
			{
				Item item = base.XsoItem as Item;
				if (item == null)
				{
					return null;
				}
				long num;
				IList<AttachmentLink> list;
				return BodyConversionUtilities.ConvertToBodyStream(item, -1L, out num, out list);
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00074ADC File Offset: 0x00072CDC
		public bool IsOnSMIMEMessage
		{
			get
			{
				MessageItem messageItem = base.XsoItem as MessageItem;
				return messageItem != null && ObjectClass.IsSmime(messageItem.ClassName);
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00074B05 File Offset: 0x00072D05
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x00074B26 File Offset: 0x00072D26
		public Stream MIMEData
		{
			get
			{
				if (this.mimeData == null)
				{
					this.mimeData = BodyUtility.ConvertItemToMime(base.XsoItem);
				}
				return this.mimeData;
			}
			set
			{
				throw new NotImplementedException("set_MIMEData");
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00074B32 File Offset: 0x00072D32
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x00074B3A File Offset: 0x00072D3A
		public long MIMESize { get; set; }

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00074B43 File Offset: 0x00072D43
		public bool RtfPresent
		{
			get
			{
				return this.actualBody != null && this.actualBody.Format == Microsoft.Exchange.Data.Storage.BodyFormat.ApplicationRtf;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x00074B60 File Offset: 0x00072D60
		public long Size
		{
			get
			{
				Item item = base.XsoItem as Item;
				if (item == null)
				{
					return 0L;
				}
				if (string.Equals(item.ClassName, "IPM.Note.SMIME", StringComparison.OrdinalIgnoreCase))
				{
					string text = Strings.SMIMENotSupportedBodyText.ToString(item.Session.PreferedCulture);
					return (long)text.Length;
				}
				if (this.actualBody.Format == Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain)
				{
					return this.actualBody.Size / 2L;
				}
				return this.actualBody.Size;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00074BDB File Offset: 0x00072DDB
		public bool IsIrmErrorMessage
		{
			get
			{
				return this.isIrmErrorMessage;
			}
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x00074BE4 File Offset: 0x00072DE4
		public virtual void PreProcessProperty()
		{
			Item item = (Item)base.XsoItem;
			if (BodyConversionUtilities.IsMessageRestrictedAndDecoded(item))
			{
				this.actualBody = ((RightsManagedMessageItem)item).ProtectedBody;
			}
			else
			{
				this.actualBody = item.Body;
			}
			this.originalItem = null;
			this.isIrmErrorMessage = false;
			if (BodyConversionUtilities.IsIRMFailedToDecode(item))
			{
				MessageItem messageItem = this.CreateIrmErrorMessage();
				if (messageItem != null)
				{
					base.XsoItem = messageItem;
					this.originalItem = item;
					this.isIrmErrorMessage = true;
				}
			}
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x00074C5C File Offset: 0x00072E5C
		public virtual void PostProcessProperty()
		{
			this.actualBody = null;
			if (this.originalItem != null)
			{
				MessageItem messageItem = (MessageItem)base.XsoItem;
				if (messageItem != null)
				{
					messageItem.Dispose();
				}
				base.XsoItem = this.originalItem;
				this.originalItem = null;
			}
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00074CF4 File Offset: 0x00072EF4
		public Stream GetData(BodyType type, long truncationSize, out long totalDataSize, out IEnumerable<AirSyncAttachmentInfo> attachments)
		{
			Item item = base.XsoItem as Item;
			attachments = null;
			if (item == null)
			{
				totalDataSize = 0L;
				return null;
			}
			IList<AttachmentLink> list = null;
			Stream stream;
			if (string.Equals(item.ClassName, "IPM.Note.SMIME", StringComparison.OrdinalIgnoreCase) && truncationSize != 0L)
			{
				switch (type)
				{
				case BodyType.PlainText:
				{
					string s = Strings.SMIMENotSupportedBodyText.ToString(item.Session.PreferedCulture);
					stream = new MemoryStream(Encoding.UTF8.GetBytes(s));
					totalDataSize = stream.Length;
					return stream;
				}
				case BodyType.Html:
				{
					string s = XsoContentProperty.GetSMIMENotSupportedBodyHtml(item.Session);
					stream = new MemoryStream(Encoding.UTF8.GetBytes(s));
					totalDataSize = stream.Length;
					return stream;
				}
				}
			}
			switch (type)
			{
			case BodyType.None:
				throw new ConversionException("Invalid body type requested");
			case BodyType.PlainText:
				stream = BodyConversionUtilities.ConvertToPlainTextStream(item, truncationSize, out totalDataSize, out list);
				break;
			case BodyType.Html:
				stream = BodyConversionUtilities.ConvertHtmlStream(item, truncationSize, out totalDataSize, out list);
				break;
			case BodyType.Rtf:
				return this.GetRtfData(truncationSize, out totalDataSize, out list);
			case BodyType.Mime:
				throw new ConversionException("Invalid body type requested");
			default:
				stream = null;
				totalDataSize = 0L;
				break;
			}
			if (list != null)
			{
				attachments = from attachmentLink in list
				select new AirSyncAttachmentInfo
				{
					AttachmentId = attachmentLink.AttachmentId,
					IsInline = (attachmentLink.IsMarkedInline ?? attachmentLink.IsOriginallyInline),
					ContentId = attachmentLink.ContentId
				};
			}
			return stream;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x00074E34 File Offset: 0x00073034
		public BodyType GetNativeType()
		{
			if (!(base.XsoItem is Item))
			{
				return BodyType.None;
			}
			BodyType result;
			switch (this.actualBody.Format)
			{
			case Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain:
				result = BodyType.PlainText;
				break;
			case Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml:
				result = BodyType.Html;
				break;
			case Microsoft.Exchange.Data.Storage.BodyFormat.ApplicationRtf:
				result = BodyType.Rtf;
				break;
			default:
				throw new ConversionException("Unknown BodyFormat implemented by XSO");
			}
			return result;
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00074E8C File Offset: 0x0007308C
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			Item item = (Item)base.XsoItem;
			IContentProperty contentProperty = (IContentProperty)srcProperty;
			switch (contentProperty.GetNativeType())
			{
			case BodyType.PlainText:
				break;
			case BodyType.Html:
				goto IL_D3;
			case BodyType.Rtf:
				if (contentProperty.Body.Length > 0L)
				{
					using (Stream stream = XsoContentProperty.OpenBodyWriteStream(item, Microsoft.Exchange.Data.Storage.BodyFormat.ApplicationRtf))
					{
						try
						{
							StreamHelper.CopyStreamWithBase64Conversion(contentProperty.Body, stream, -1, false);
						}
						catch (FormatException innerException)
						{
							throw new AirSyncPermanentException(StatusCode.Sync_ServerError, innerException, false)
							{
								ErrorStringForProtocolLogger = "RtfToBase64StreamError"
							};
						}
						return;
					}
				}
				using (TextWriter textWriter = item.Body.OpenTextWriter(Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain))
				{
					textWriter.Write(string.Empty);
					return;
				}
				break;
			default:
				goto IL_F8;
			}
			using (Stream stream2 = XsoContentProperty.OpenBodyWriteStream(item, Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain))
			{
				StreamHelper.CopyStream(contentProperty.Body, stream2);
				return;
			}
			IL_D3:
			using (Stream stream3 = XsoContentProperty.OpenBodyWriteStream(item, Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml))
			{
				StreamHelper.CopyStream(contentProperty.Body, stream3);
				return;
			}
			IL_F8:
			throw new ConversionException("Source body property does not have Rtf or Text body present");
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00074FDC File Offset: 0x000731DC
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			Item item = (Item)base.XsoItem;
			using (TextWriter textWriter = item.Body.OpenTextWriter(Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain))
			{
				textWriter.Write(string.Empty);
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0007502C File Offset: 0x0007322C
		public override void Unbind()
		{
			this.mimeData = null;
			base.Unbind();
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0007503C File Offset: 0x0007323C
		private static Stream OpenBodyWriteStream(Item item, Microsoft.Exchange.Data.Storage.BodyFormat bodyFormat)
		{
			BodyWriteConfiguration configuration = new BodyWriteConfiguration(bodyFormat, "utf-8");
			return item.Body.OpenWriteStream(configuration);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x00075064 File Offset: 0x00073264
		private static string GetSMIMENotSupportedBodyHtml(StoreSession session)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<html>");
			stringBuilder.Append("<body>");
			stringBuilder.Append("<font color=\"red\">");
			stringBuilder.Append(Strings.SMIMENotSupportedBodyText.ToString(session.PreferedCulture));
			stringBuilder.Append("</font>");
			stringBuilder.Append("</body>");
			stringBuilder.Append("</html>");
			return stringBuilder.ToString();
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x000750E0 File Offset: 0x000732E0
		private Stream GetRtfData(long truncationSize, out long totalDataSize, out IList<AttachmentLink> attachmentLinks)
		{
			if (truncationSize == 0L)
			{
				totalDataSize = this.Size;
				attachmentLinks = null;
				return XsoContentProperty.emptyStream;
			}
			Item item = (Item)base.XsoItem;
			item.Load();
			return BodyConversionUtilities.ConvertToRtfStream(item, truncationSize, out totalDataSize, out attachmentLinks);
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x00075120 File Offset: 0x00073320
		private MessageItem CreateIrmErrorMessage()
		{
			RightsManagedMessageItem rightsManagedMessageItem = ((Item)base.XsoItem) as RightsManagedMessageItem;
			if (rightsManagedMessageItem == null)
			{
				return null;
			}
			bool flag = false;
			MessageItem messageItem = MessageItem.CreateInMemory(StoreObjectSchema.ContentConversionProperties);
			if (messageItem == null)
			{
				AirSyncDiagnostics.TraceError(ExTraceGlobals.XsoTracer, null, "Failed to create in memory message item");
				return null;
			}
			try
			{
				Item.CopyItemContent(rightsManagedMessageItem, messageItem);
				using (TextWriter textWriter = messageItem.Body.OpenTextWriter(Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain))
				{
					RightsManagementFailureCode failureCode = rightsManagedMessageItem.DecryptionStatus.FailureCode;
					if (failureCode > RightsManagementFailureCode.PreLicenseAcquisitionFailed)
					{
						switch (failureCode)
						{
						case RightsManagementFailureCode.FailedToExtractTargetUriFromMex:
						case RightsManagementFailureCode.FailedToDownloadMexData:
							textWriter.Write(Strings.IRMReachNotConfiguredBodyText.ToString(rightsManagedMessageItem.Session.PreferedCulture));
							goto IL_1F7;
						case RightsManagementFailureCode.GetServerInfoFailed:
							goto IL_1AF;
						case RightsManagementFailureCode.InternalLicensingDisabled:
						case RightsManagementFailureCode.ExternalLicensingDisabled:
							break;
						default:
							switch (failureCode)
							{
							case RightsManagementFailureCode.ServerRightNotGranted:
								textWriter.Write(Strings.IRMServerNotConfiguredBodyText.ToString(rightsManagedMessageItem.Session.PreferedCulture));
								goto IL_1F7;
							case RightsManagementFailureCode.InvalidLicensee:
								goto IL_149;
							case RightsManagementFailureCode.FeatureDisabled:
								break;
							case RightsManagementFailureCode.NotSupported:
							case RightsManagementFailureCode.MissingLicense:
							case RightsManagementFailureCode.InvalidLicensingLocation:
								goto IL_1AF;
							case RightsManagementFailureCode.CorruptData:
								textWriter.Write(Strings.IRMCorruptProtectedMessageBodyText.ToString(rightsManagedMessageItem.Session.PreferedCulture));
								goto IL_1F7;
							case RightsManagementFailureCode.ExpiredLicense:
								textWriter.Write(Strings.IRMLicenseExpiredBodyText.ToString(rightsManagedMessageItem.Session.PreferedCulture));
								goto IL_1F7;
							default:
								goto IL_1AF;
							}
							break;
						}
						flag = true;
						goto IL_1F7;
					}
					if (failureCode == RightsManagementFailureCode.UserRightNotGranted)
					{
						textWriter.Write(Strings.IRMNoViewRightsBodyText.ToString(rightsManagedMessageItem.Session.PreferedCulture));
						goto IL_1F7;
					}
					if (failureCode != RightsManagementFailureCode.PreLicenseAcquisitionFailed)
					{
						goto IL_1AF;
					}
					IL_149:
					textWriter.Write(Strings.IRMPreLicensingFailureBodyText.ToString(rightsManagedMessageItem.Session.PreferedCulture));
					goto IL_1F7;
					IL_1AF:
					AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.XsoTracer, null, "IRM decryption status {0}", rightsManagedMessageItem.DecryptionStatus.FailureCode.ToString());
					textWriter.Write(Strings.IRMServerNotAvailableBodyText.ToString(rightsManagedMessageItem.Session.PreferedCulture));
					IL_1F7:;
				}
			}
			catch (Exception)
			{
				if (messageItem != null)
				{
					flag = true;
				}
				throw;
			}
			finally
			{
				if (flag)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
			return messageItem;
		}

		// Token: 0x04000C5A RID: 3162
		private static readonly Stream emptyStream = new MemoryStream(0);

		// Token: 0x04000C5B RID: 3163
		private Stream mimeData;

		// Token: 0x04000C5C RID: 3164
		private bool isIrmErrorMessage;

		// Token: 0x04000C5D RID: 3165
		protected Body actualBody;

		// Token: 0x04000C5E RID: 3166
		protected Item originalItem;
	}
}
