using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000357 RID: 855
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class StreamAttachmentBase : Attachment, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x06002612 RID: 9746 RVA: 0x000985F9 File Offset: 0x000967F9
		internal StreamAttachmentBase(CoreAttachment coreAttachment) : base(coreAttachment)
		{
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x00098602 File Offset: 0x00096802
		public virtual Stream GetContentStream()
		{
			base.CheckDisposed("GetContentStream");
			return base.PropertyBag.OpenPropertyStream(this.ContentStreamProperty, base.CalculateOpenMode());
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x00098626 File Offset: 0x00096826
		public virtual Stream GetContentStream(PropertyOpenMode openMode)
		{
			base.CheckDisposed("GetContentStream");
			EnumValidator.ThrowIfInvalid<PropertyOpenMode>(openMode, "openMode");
			return base.PropertyBag.OpenPropertyStream(this.ContentStreamProperty, openMode);
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x00098650 File Offset: 0x00096850
		public virtual Stream TryGetContentStream(PropertyOpenMode openMode)
		{
			base.CheckDisposed("TryGetContentStream");
			EnumValidator.ThrowIfInvalid<PropertyOpenMode>(openMode, "openMode");
			Stream result;
			try
			{
				result = base.PropertyBag.OpenPropertyStream(this.ContentStreamProperty, openMode);
			}
			catch (ObjectNotFoundException)
			{
				if (openMode != PropertyOpenMode.ReadOnly)
				{
					throw;
				}
				result = null;
			}
			return result;
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06002616 RID: 9750
		protected abstract PropertyTagPropertyDefinition ContentStreamProperty { get; }

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x000986A4 File Offset: 0x000968A4
		protected override Schema Schema
		{
			get
			{
				return StreamAttachmentBaseSchema.Instance;
			}
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x000986AC File Offset: 0x000968AC
		private static string GetExtensionByContentType(string contentType)
		{
			if (StandaloneFuzzing.IsEnabled)
			{
				return string.Empty;
			}
			string text = ExtensionToContentTypeMapper.Instance.GetExtensionByContentType(contentType);
			if (!string.IsNullOrEmpty(text))
			{
				text = "." + text;
			}
			return text;
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x000986E7 File Offset: 0x000968E7
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			this.OnBeforeSaveUpdateAttachSize();
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x000986F8 File Offset: 0x000968F8
		protected virtual void OnBeforeSaveUpdateAttachSize()
		{
			if (base.MapiAttach != null)
			{
				return;
			}
			base.Load(new PropertyDefinition[]
			{
				InternalSchema.AttachSize,
				this.ContentStreamProperty
			});
			int num = base.GetValueOrDefault<int>(InternalSchema.AttachSize);
			if (num != 0)
			{
				return;
			}
			object obj = base.TryGetProperty(this.ContentStreamProperty);
			PropertyError propertyError = obj as PropertyError;
			if (propertyError != null)
			{
				if (!PropertyError.IsPropertyValueTooBig(propertyError))
				{
					goto IL_8B;
				}
				try
				{
					using (Stream stream = this.OpenPropertyStream(this.ContentStreamProperty, PropertyOpenMode.ReadOnly))
					{
						num = (int)stream.Length;
					}
					goto IL_8B;
				}
				catch (ObjectNotFoundException)
				{
					goto IL_8B;
				}
			}
			num = ((byte[])obj).Length;
			IL_8B:
			base[InternalSchema.AttachSize] = num;
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000987C0 File Offset: 0x000969C0
		internal static void CoreObjectUpdateStreamAttachmentName(CoreAttachment coreAttachment)
		{
			ICorePropertyBag propertyBag = coreAttachment.PropertyBag;
			string text = (propertyBag.TryGetProperty(InternalSchema.AttachLongFileName) as string) ?? (propertyBag.TryGetProperty(InternalSchema.AttachFileName) as string);
			string text2 = propertyBag.TryGetProperty(InternalSchema.DisplayName) as string;
			string text3 = propertyBag.TryGetProperty(InternalSchema.AttachExtension) as string;
			if (!string.IsNullOrEmpty(text))
			{
				text = Attachment.TrimFilename(text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text2 = Attachment.TrimFilename(text2);
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text3 = '.' + Attachment.TrimFilename(text3);
			}
			string text4 = null;
			string text5 = null;
			string text6 = null;
			string text7 = null;
			Attachment.TryFindFileExtension(text, out text5, out text4);
			Attachment.TryFindFileExtension(text2, out text7, out text6);
			if (!string.IsNullOrEmpty(text5))
			{
				text3 = text5;
			}
			if (!string.IsNullOrEmpty(text7) && string.Compare(text3, text7, StringComparison.OrdinalIgnoreCase) != 0)
			{
				text6 += text7;
			}
			if (string.IsNullOrEmpty(text4))
			{
				text4 = Attachment.GenerateFilename();
			}
			if (EmailMessageHelpers.IsGeneratedFileName(text4) && string.IsNullOrEmpty(text3))
			{
				string text8 = propertyBag.TryGetProperty(InternalSchema.AttachMimeTag) as string;
				if (!string.IsNullOrEmpty(text8))
				{
					text3 = StreamAttachmentBase.GetExtensionByContentType(text8);
				}
			}
			if (string.IsNullOrEmpty(text6))
			{
				text6 = text4;
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text = text4 + text3;
				text2 = text6 + text3;
			}
			else
			{
				text = text4;
				text2 = text6;
				propertyBag.Delete(InternalSchema.AttachExtension);
			}
			propertyBag[InternalSchema.AttachLongFileName] = text;
			propertyBag[InternalSchema.DisplayName] = text2;
			propertyBag[InternalSchema.AttachExtension] = (text3 ?? string.Empty);
			bool flag = false;
			ICoreItem containerItem = coreAttachment.ParentCollection.ContainerItem;
			if (containerItem != null)
			{
				containerItem.PropertyBag.Load(new PropertyDefinition[]
				{
					InternalSchema.IsAssociated
				});
				flag = containerItem.PropertyBag.GetValueOrDefault<bool>(InternalSchema.IsAssociated, false);
			}
			if (!flag)
			{
				propertyBag[InternalSchema.AttachFileName] = Attachment.Make8x3FileName(text, coreAttachment != null && coreAttachment.Session != null && coreAttachment.Session.IsMoveUser);
			}
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x000989C1 File Offset: 0x00096BC1
		internal static void CoreObjectUpdateImageThumbnail(CoreAttachment coreAttachment)
		{
		}

		// Token: 0x040016DE RID: 5854
		private static HashSet<string> SupportedThumbnailTypes = new HashSet<string>(new string[]
		{
			".bmp",
			".gif",
			".jpg",
			".png"
		}, StringComparer.OrdinalIgnoreCase);

		// Token: 0x02000358 RID: 856
		private class ImageThumbnailGenerationSettings
		{
			// Token: 0x0600261E RID: 9758 RVA: 0x00098A2C File Offset: 0x00096C2C
			private ImageThumbnailGenerationSettings()
			{
				this.maxSizeOfImageToConvert = StoreSession.GetConfigFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\MailboxRole\\AttachmentImageThumbnail", "MaxSizeOfImageToConvertInKB", 2048, (int i) => i <= 5120);
				this.maxSideInPixels = StoreSession.GetConfigFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\MailboxRole\\AttachmentImageThumbnail", "MaxSideSizeInPixels", 480, (int i) => i <= 480);
				this.maxThumbnailsPerEmail = StoreSession.GetConfigFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\MailboxRole\\AttachmentImageThumbnail", "MaxThumbnailsPerEmail", 30, (int i) => i <= 30);
				this.disableThumbnailGeneration = (StoreSession.GetConfigFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\MailboxRole\\AttachmentImageThumbnail", "DisableThumbnailGeneration", 0, null) != 0);
				this.bypassFlightForThumbnailGeneration = (StoreSession.GetConfigFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\MailboxRole\\AttachmentImageThumbnail", "BypassFlightForThumbnailGeneration", 0, null) != 0);
			}

			// Token: 0x17000CBA RID: 3258
			// (get) Token: 0x0600261F RID: 9759 RVA: 0x00098B1B File Offset: 0x00096D1B
			public static StreamAttachmentBase.ImageThumbnailGenerationSettings Instance
			{
				get
				{
					return StreamAttachmentBase.ImageThumbnailGenerationSettings.instance.Value;
				}
			}

			// Token: 0x06002620 RID: 9760 RVA: 0x00098B28 File Offset: 0x00096D28
			public bool IsFeatureEnabled(IExchangePrincipal mailboxOwner, IRecipientSession recipientSession)
			{
				if (this.disableThumbnailGeneration)
				{
					return false;
				}
				if (this.bypassFlightForThumbnailGeneration)
				{
					return true;
				}
				if (mailboxOwner == null || recipientSession == null)
				{
					return false;
				}
				ADUser aduser = null;
				try
				{
					aduser = (DirectoryHelper.ReadADRecipient(mailboxOwner.MailboxInfo.MailboxGuid, mailboxOwner.MailboxInfo.IsArchive, recipientSession) as ADUser);
				}
				catch (DataValidationException)
				{
					return false;
				}
				catch (DataSourceOperationException)
				{
					return false;
				}
				catch (DataSourceTransientException)
				{
					return false;
				}
				if (aduser == null)
				{
					return false;
				}
				VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(aduser.GetContext(null), null, null);
				return snapshot.DataStorage.StorageAttachmentImageAnalysis.Enabled;
			}

			// Token: 0x17000CBB RID: 3259
			// (get) Token: 0x06002621 RID: 9761 RVA: 0x00098BD8 File Offset: 0x00096DD8
			public int MaxSizeOfImageToConvertInKB
			{
				get
				{
					return this.maxSizeOfImageToConvert;
				}
			}

			// Token: 0x17000CBC RID: 3260
			// (get) Token: 0x06002622 RID: 9762 RVA: 0x00098BE0 File Offset: 0x00096DE0
			public int MaxSideInPixels
			{
				get
				{
					return this.maxSideInPixels;
				}
			}

			// Token: 0x17000CBD RID: 3261
			// (get) Token: 0x06002623 RID: 9763 RVA: 0x00098BE8 File Offset: 0x00096DE8
			public int MaxThumbnailsPerEmail
			{
				get
				{
					return this.maxThumbnailsPerEmail;
				}
			}

			// Token: 0x040016DF RID: 5855
			private const string AttachmentImageThumbnail = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\MailboxRole\\AttachmentImageThumbnail";

			// Token: 0x040016E0 RID: 5856
			private static readonly Lazy<StreamAttachmentBase.ImageThumbnailGenerationSettings> instance = new Lazy<StreamAttachmentBase.ImageThumbnailGenerationSettings>(() => new StreamAttachmentBase.ImageThumbnailGenerationSettings());

			// Token: 0x040016E1 RID: 5857
			private readonly int maxSizeOfImageToConvert;

			// Token: 0x040016E2 RID: 5858
			private readonly int maxSideInPixels;

			// Token: 0x040016E3 RID: 5859
			private readonly int maxThumbnailsPerEmail;

			// Token: 0x040016E4 RID: 5860
			private readonly bool disableThumbnailGeneration;

			// Token: 0x040016E5 RID: 5861
			private readonly bool bypassFlightForThumbnailGeneration;
		}
	}
}
