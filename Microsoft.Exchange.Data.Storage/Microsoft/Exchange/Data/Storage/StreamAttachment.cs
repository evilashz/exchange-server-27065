using System;
using System.IO;
using System.Security.Principal;
using System.Xml;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Security.RightsManagement.Protectors;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200035B RID: 859
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class StreamAttachment : StreamAttachmentBase, IStreamAttachment, IAttachment, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06002648 RID: 9800 RVA: 0x0009924C File Offset: 0x0009744C
		internal StreamAttachment(CoreAttachment coreAttachment) : base(coreAttachment)
		{
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x00099261 File Offset: 0x00097461
		public static bool TryOpenRestrictedContent(StreamAttachment sourceAttachment, out Stream result)
		{
			return StreamAttachment.TryOpenRestrictedContent(sourceAttachment, OrganizationId.ForestWideOrgId, out result);
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x0009926F File Offset: 0x0009746F
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<StreamAttachment>(this);
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x00099277 File Offset: 0x00097477
		internal Stream GetRawContentStream(PropertyOpenMode openMode)
		{
			base.CheckDisposed("GetRawContentStream");
			return base.GetContentStream(openMode);
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x0009928B File Offset: 0x0009748B
		internal Stream TryGetRawContentStream(PropertyOpenMode openMode)
		{
			base.CheckDisposed("TryGetRawContentStream");
			return base.TryGetContentStream(openMode);
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000992A0 File Offset: 0x000974A0
		public Stream GetReadOnlyApplefileStream()
		{
			base.CheckDisposed("GetReadOnlyApplefileStream");
			if (this.IsFlaggedAsMacAttachment)
			{
				object obj = base.TryGetProperty(InternalSchema.AttachmentMacInfo);
				if (obj is byte[])
				{
					return new MemoryStream(obj as byte[], false);
				}
				if (PropertyError.IsPropertyValueTooBig(obj))
				{
					return this.OpenPropertyStream(InternalSchema.AttachmentMacInfo, PropertyOpenMode.ReadOnly);
				}
				using (Stream rawContentStream = this.GetRawContentStream(PropertyOpenMode.ReadOnly))
				{
					if (this.IsMacAttachmentInternal(rawContentStream))
					{
						using (DisposeGuard disposeGuard = default(DisposeGuard))
						{
							Stream stream = disposeGuard.Add<MemoryStream>(new MemoryStream());
							string text = null;
							byte[] array = null;
							MimeAppleTranscoder.MacBinToApplefile(rawContentStream, stream, out text, out array);
							disposeGuard.Success();
							return stream;
						}
					}
					throw new CorruptDataException(ServerStrings.ConversionFailedInvalidMacBin);
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x00099384 File Offset: 0x00097584
		public override Stream GetContentStream()
		{
			base.CheckDisposed("GetContentStream");
			PropertyOpenMode openMode = base.CalculateOpenMode();
			return this.GetContentStream(openMode);
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x000993AC File Offset: 0x000975AC
		public override Stream GetContentStream(PropertyOpenMode openMode)
		{
			base.CheckDisposed("GetContentStream");
			Stream contentStream = base.GetContentStream(openMode);
			if (!this.IsMacAttachmentInternal(contentStream))
			{
				return contentStream;
			}
			if (openMode == PropertyOpenMode.ReadOnly)
			{
				return MimeAppleTranscoder.ExtractDataFork(contentStream);
			}
			throw new InvalidOperationException("Unable to get write stream for mac attachment.");
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x000993EC File Offset: 0x000975EC
		public override Stream TryGetContentStream(PropertyOpenMode openMode)
		{
			base.CheckDisposed("TryGetContentStream");
			Stream stream = base.TryGetContentStream(openMode);
			if (stream == null || !this.IsMacAttachmentInternal(stream))
			{
				return stream;
			}
			if (openMode == PropertyOpenMode.ReadOnly)
			{
				return MimeAppleTranscoder.ExtractDataFork(stream);
			}
			throw new InvalidOperationException("Unable to get write stream for mac attachment.");
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x00099430 File Offset: 0x00097630
		public byte[] LoadAttachmentThumbnail()
		{
			base.CheckDisposed("LoadAttachmentThumbnail");
			base.Load(new PropertyDefinition[]
			{
				InternalSchema.ImageThumbnail
			});
			byte[] result;
			try
			{
				using (Stream stream = this.OpenPropertyStream(InternalSchema.ImageThumbnail, PropertyOpenMode.ReadOnly))
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						stream.CopyTo(memoryStream);
						result = memoryStream.ToArray();
					}
				}
			}
			catch (ObjectNotFoundException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x000994C4 File Offset: 0x000976C4
		public byte[] LoadAttachmentThumbnailSalientRegions()
		{
			base.CheckDisposed("LoadAttachmentThumbnailSalientRegions");
			base.Load(new PropertyDefinition[]
			{
				InternalSchema.ImageThumbnailSalientRegions
			});
			return base.GetValueOrDefault<byte[]>(InternalSchema.ImageThumbnailSalientRegions, null);
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000994FE File Offset: 0x000976FE
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			base.PropertyBag[InternalSchema.AttachMethod] = 1;
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x0009951C File Offset: 0x0009771C
		protected override PropertyTagPropertyDefinition ContentStreamProperty
		{
			get
			{
				return InternalSchema.AttachDataBin;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x00099524 File Offset: 0x00097724
		public bool IsMacAttachment
		{
			get
			{
				base.CheckDisposed("IsMacAttachment.get");
				if (this.isMacAttachment != null)
				{
					return this.isMacAttachment.Value;
				}
				this.isMacAttachment = new bool?(false);
				if (this.IsFlaggedAsMacAttachment)
				{
					using (Stream contentStream = base.GetContentStream(PropertyOpenMode.ReadOnly))
					{
						if (contentStream != null)
						{
							this.isMacAttachment = new bool?(MimeAppleTranscoder.IsMacBinStream(contentStream));
						}
					}
				}
				return this.isMacAttachment.Value;
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x000995AC File Offset: 0x000977AC
		private bool IsFlaggedAsMacAttachment
		{
			get
			{
				byte[] valueOrDefault = base.GetValueOrDefault<byte[]>(InternalSchema.AttachEncoding);
				if (valueOrDefault == null || valueOrDefault.Length != ConvertUtils.OidMacBinary.Length)
				{
					return false;
				}
				for (int i = 0; i < valueOrDefault.Length; i++)
				{
					if (valueOrDefault[i] != ConvertUtils.OidMacBinary[i])
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000995F3 File Offset: 0x000977F3
		private bool IsMacAttachmentInternal(Stream attachStream)
		{
			this.isMacAttachment = new bool?(false);
			if (this.IsFlaggedAsMacAttachment && attachStream != null)
			{
				this.isMacAttachment = new bool?(MimeAppleTranscoder.IsMacBinStream(attachStream));
			}
			return this.isMacAttachment.Value;
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x00099628 File Offset: 0x00097828
		// (set) Token: 0x06002659 RID: 9817 RVA: 0x00099654 File Offset: 0x00097854
		public override string ContentType
		{
			get
			{
				base.CheckDisposed("ContentType::get");
				if (this.IsFlaggedAsMacAttachment)
				{
					return base.GetValueOrDefault<string>(InternalSchema.AttachmentMacContentType, "application/octet-stream");
				}
				return base.ContentType;
			}
			set
			{
				base.CheckDisposed("ContentType::set");
				if (this.IsFlaggedAsMacAttachment)
				{
					base[InternalSchema.AttachmentMacContentType] = value;
					return;
				}
				base.ContentType = value;
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x0009967D File Offset: 0x0009787D
		public override AttachmentType AttachmentType
		{
			get
			{
				base.CheckDisposed("AttachmentType::get");
				return AttachmentType.Stream;
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x0009968C File Offset: 0x0009788C
		public int ImageThumbnailHeight
		{
			get
			{
				base.CheckDisposed("AttachmentType::ImageThumbnailHeight");
				base.Load(new PropertyDefinition[]
				{
					InternalSchema.ImageThumbnailHeight
				});
				return base.GetValueOrDefault<int>(InternalSchema.ImageThumbnailHeight, 0);
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x000996C8 File Offset: 0x000978C8
		public int ImageThumbnailWidth
		{
			get
			{
				base.CheckDisposed("AttachmentType::ImageThumbnailWidth");
				base.Load(new PropertyDefinition[]
				{
					InternalSchema.ImageThumbnailWidth
				});
				return base.GetValueOrDefault<int>(InternalSchema.ImageThumbnailWidth, 0);
			}
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000999D8 File Offset: 0x00097BD8
		public static Stream OpenRestrictedAttachment(StreamAttachment sourceAttachment, OrganizationId orgId, string userIdentity, SecurityIdentifier userSid, RecipientTypeDetails userType, out UseLicenseAndUsageRights validatedUseLicenseAndUsageRights, out bool acquiredNewLicense)
		{
			StreamAttachment.<>c__DisplayClass2 CS$<>8__locals1 = new StreamAttachment.<>c__DisplayClass2();
			CS$<>8__locals1.sourceAttachment = sourceAttachment;
			CS$<>8__locals1.orgId = orgId;
			CS$<>8__locals1.userIdentity = userIdentity;
			CS$<>8__locals1.userSid = userSid;
			CS$<>8__locals1.userType = userType;
			Util.ThrowOnNullArgument(CS$<>8__locals1.sourceAttachment, "sourceAttachment");
			Util.ThrowOnNullArgument(CS$<>8__locals1.orgId, "orgId");
			Util.ThrowOnNullArgument(CS$<>8__locals1.userIdentity, "userIdentity");
			Util.ThrowOnNullArgument(CS$<>8__locals1.userSid, "userSid");
			if (!Enum.IsDefined(typeof(RecipientTypeDetails), CS$<>8__locals1.userType))
			{
				throw new EnumArgumentException("userType");
			}
			CS$<>8__locals1.cachedServerUseLicense = null;
			if (!PropertyError.IsPropertyNotFound(CS$<>8__locals1.sourceAttachment.TryGetProperty(AttachmentSchema.DRMServerLicenseCompressed)))
			{
				using (Stream stream = CS$<>8__locals1.sourceAttachment.OpenPropertyStream(AttachmentSchema.DRMServerLicenseCompressed, PropertyOpenMode.ReadOnly))
				{
					CS$<>8__locals1.cachedServerUseLicense = DrmEmailCompression.DecompressUseLicense(stream);
				}
			}
			StreamAttachment.<>c__DisplayClass2 CS$<>8__locals2 = CS$<>8__locals1;
			int? valueAsNullable = CS$<>8__locals1.sourceAttachment.PropertyBag.GetValueAsNullable<int>(AttachmentSchema.DRMRights);
			CS$<>8__locals2.cachedUsageRights = ((valueAsNullable != null) ? new ContentRight?((ContentRight)valueAsNullable.GetValueOrDefault()) : null);
			CS$<>8__locals1.cachedExpiryTime = CS$<>8__locals1.sourceAttachment.PropertyBag.GetValueAsNullable<ExDateTime>(AttachmentSchema.DRMExpiryTime);
			CS$<>8__locals1.cachedDrmPropsSignature = CS$<>8__locals1.sourceAttachment.PropertyBag.GetValueOrDefault<byte[]>(AttachmentSchema.DRMPropsSignature);
			CS$<>8__locals1.item = CS$<>8__locals1.sourceAttachment.CoreAttachment.ParentCollection.ContainerItem;
			if (string.IsNullOrEmpty(CS$<>8__locals1.cachedServerUseLicense) || CS$<>8__locals1.cachedUsageRights == null || CS$<>8__locals1.cachedExpiryTime == null || CS$<>8__locals1.cachedDrmPropsSignature == null)
			{
				string valueOrDefault = CS$<>8__locals1.item.PropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
				if (ObjectClass.IsMessage(valueOrDefault, false))
				{
					CS$<>8__locals1.cachedServerUseLicense = CS$<>8__locals1.item.PropertyBag.GetValueOrDefault<string>(MessageItemSchema.DRMServerLicense, string.Empty);
					StreamAttachment.<>c__DisplayClass2 CS$<>8__locals3 = CS$<>8__locals1;
					int? valueAsNullable2 = CS$<>8__locals1.item.PropertyBag.GetValueAsNullable<int>(MessageItemSchema.DRMRights);
					CS$<>8__locals3.cachedUsageRights = ((valueAsNullable2 != null) ? new ContentRight?((ContentRight)valueAsNullable2.GetValueOrDefault()) : null);
					CS$<>8__locals1.cachedExpiryTime = CS$<>8__locals1.item.PropertyBag.GetValueAsNullable<ExDateTime>(MessageItemSchema.DRMExpiryTime);
					CS$<>8__locals1.cachedDrmPropsSignature = CS$<>8__locals1.item.PropertyBag.GetValueOrDefault<byte[]>(MessageItemSchema.DRMPropsSignature);
				}
			}
			CS$<>8__locals1.unprotectionSuccess = false;
			CS$<>8__locals1.useLicenseAndUsageRights = null;
			CS$<>8__locals1.validCachedLicense = false;
			Stream unprotectedAttachment;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				StreamAttachment.<>c__DisplayClass4 CS$<>8__locals4 = new StreamAttachment.<>c__DisplayClass4();
				CS$<>8__locals4.unprotectedAttachment = disposeGuard.Add<Stream>(Streams.CreateTemporaryStorageStream());
				CS$<>8__locals4.decryptorHandle = null;
				using (Stream inputStream = CS$<>8__locals1.sourceAttachment.GetContentStream(PropertyOpenMode.ReadOnly))
				{
					try
					{
						MsgToRpMsgConverter.CallRM(delegate
						{
							CS$<>8__locals1.unprotectionSuccess = ProtectorsManager.Instance.Unprotect(delegate(string protectedDocumentIssuanceLicense)
							{
								string valueOrDefault2 = CS$<>8__locals1.item.PropertyBag.GetValueOrDefault<string>(ItemSchema.InternetMessageId, string.Empty);
								bool flag = string.IsNullOrEmpty(valueOrDefault2);
								RmsClientManagerContext context = new RmsClientManagerContext(CS$<>8__locals1.orgId, flag ? RmsClientManagerContext.ContextId.AttachmentFileName : RmsClientManagerContext.ContextId.MessageId, flag ? CS$<>8__locals1.sourceAttachment.FileName : valueOrDefault2, null);
								if (!string.IsNullOrEmpty(CS$<>8__locals1.cachedServerUseLicense) && CS$<>8__locals1.cachedUsageRights != null && CS$<>8__locals1.cachedExpiryTime != null && CS$<>8__locals1.cachedDrmPropsSignature != null)
								{
									try
									{
										CS$<>8__locals4.decryptorHandle = RmsClientManager.VerifyDRMPropsSignatureAndGetDecryptor(context, CS$<>8__locals1.userSid, CS$<>8__locals1.userType, CS$<>8__locals1.userIdentity, CS$<>8__locals1.cachedUsageRights.Value, CS$<>8__locals1.cachedExpiryTime.Value, CS$<>8__locals1.cachedDrmPropsSignature, CS$<>8__locals1.cachedServerUseLicense, protectedDocumentIssuanceLicense, UsageRightsSignatureVerificationOptions.LookupSidHistory, StreamAttachment.EmptySidList);
										CS$<>8__locals1.validCachedLicense = true;
										Uri licensingUri = null;
										XmlNode[] array = null;
										bool flag2;
										RmsClientManager.GetLicensingUri(CS$<>8__locals1.orgId, protectedDocumentIssuanceLicense, out licensingUri, out array, out flag2);
										CS$<>8__locals1.useLicenseAndUsageRights = new UseLicenseAndUsageRights(CS$<>8__locals1.cachedServerUseLicense, CS$<>8__locals1.cachedUsageRights.Value, CS$<>8__locals1.cachedExpiryTime.Value, CS$<>8__locals1.cachedDrmPropsSignature, CS$<>8__locals1.orgId, protectedDocumentIssuanceLicense, licensingUri);
									}
									catch (BadDRMPropsSignatureException)
									{
									}
								}
								if (CS$<>8__locals1.useLicenseAndUsageRights == null)
								{
									CS$<>8__locals1.useLicenseAndUsageRights = RmsClientManager.AcquireUseLicenseAndUsageRights(context, protectedDocumentIssuanceLicense, CS$<>8__locals1.userIdentity, CS$<>8__locals1.userSid, CS$<>8__locals1.userType);
									if (CS$<>8__locals4.decryptorHandle != null)
									{
										CS$<>8__locals4.decryptorHandle.Close();
										CS$<>8__locals4.decryptorHandle = null;
									}
									RmsClientManager.BindUseLicenseForDecryption(context, CS$<>8__locals1.useLicenseAndUsageRights.LicensingUri, CS$<>8__locals1.useLicenseAndUsageRights.UseLicense, CS$<>8__locals1.useLicenseAndUsageRights.PublishingLicense, out CS$<>8__locals4.decryptorHandle);
								}
								return CS$<>8__locals4.decryptorHandle;
							}, CS$<>8__locals1.sourceAttachment.FileName, inputStream, CS$<>8__locals4.unprotectedAttachment);
						}, ServerStrings.FailedToUnprotectAttachment(CS$<>8__locals1.sourceAttachment.FileName));
					}
					finally
					{
						if (CS$<>8__locals4.decryptorHandle != null)
						{
							CS$<>8__locals4.decryptorHandle.Close();
							CS$<>8__locals4.decryptorHandle = null;
						}
					}
					if (!CS$<>8__locals1.unprotectionSuccess)
					{
						validatedUseLicenseAndUsageRights = null;
						acquiredNewLicense = false;
						return null;
					}
				}
				if (!CS$<>8__locals1.useLicenseAndUsageRights.UsageRights.IsUsageRightGranted(ContentRight.View))
				{
					throw new RightsManagementPermanentException(RightsManagementFailureCode.UserRightNotGranted, ServerStrings.NotEnoughPermissionsToPerformOperation);
				}
				acquiredNewLicense = !CS$<>8__locals1.validCachedLicense;
				validatedUseLicenseAndUsageRights = CS$<>8__locals1.useLicenseAndUsageRights;
				CS$<>8__locals4.unprotectedAttachment.Position = 0L;
				disposeGuard.Success();
				unprotectedAttachment = CS$<>8__locals4.unprotectedAttachment;
			}
			return unprotectedAttachment;
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x00099EAC File Offset: 0x000980AC
		public static Stream OpenRestrictedContent(StreamAttachment sourceAttachment, OrganizationId orgId)
		{
			Util.ThrowOnNullArgument(sourceAttachment, "sourceAttachment");
			Util.ThrowOnNullArgument(orgId, "orgId");
			ICoreItem item = sourceAttachment.CoreAttachment.ParentCollection.ContainerItem;
			string serverUseLicense = item.PropertyBag.TryGetProperty(InternalSchema.DRMServerLicense) as string;
			if (string.IsNullOrEmpty(serverUseLicense))
			{
				return null;
			}
			string publishLicense = item.PropertyBag.TryGetProperty(InternalSchema.DrmPublishLicense) as string;
			if (string.IsNullOrEmpty(publishLicense))
			{
				return null;
			}
			SafeRightsManagementHandle decryptorHandle = null;
			Stream result;
			try
			{
				MsgToRpMsgConverter.CallRM(delegate
				{
					Uri licenseUri = null;
					XmlNode[] array = null;
					bool flag;
					RmsClientManager.GetLicensingUri(orgId, publishLicense, out licenseUri, out array, out flag);
					string valueOrDefault = item.PropertyBag.GetValueOrDefault<string>(ItemSchema.InternetMessageId, string.Empty);
					RmsClientManager.BindUseLicenseForDecryption(new RmsClientManagerContext(orgId, string.IsNullOrEmpty(valueOrDefault) ? RmsClientManagerContext.ContextId.AttachmentFileName : RmsClientManagerContext.ContextId.MessageId, string.IsNullOrEmpty(valueOrDefault) ? sourceAttachment.FileName : valueOrDefault, null), licenseUri, serverUseLicense, publishLicense, out decryptorHandle);
				}, ServerStrings.FailedToUnprotectAttachment(sourceAttachment.FileName));
				result = StreamAttachment.OpenRestrictedContentInternal(sourceAttachment, publishLicense, decryptorHandle);
			}
			finally
			{
				if (decryptorHandle != null)
				{
					decryptorHandle.Close();
				}
			}
			return result;
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x00099FD4 File Offset: 0x000981D4
		public static bool TryOpenRestrictedContent(StreamAttachment sourceAttachment, OrganizationId orgId, out Stream result)
		{
			try
			{
				result = StreamAttachment.OpenRestrictedContent(sourceAttachment, orgId);
				return result != null;
			}
			catch (ArgumentException)
			{
			}
			catch (RightsManagementPermanentException)
			{
			}
			catch (RightsManagementTransientException)
			{
			}
			result = null;
			return false;
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x0009A098 File Offset: 0x00098298
		private static Stream OpenRestrictedContentInternal(StreamAttachment sourceAttachment, string issuanceLicense, SafeRightsManagementHandle decryptorHandle)
		{
			string fileName = sourceAttachment.FileName;
			if (string.IsNullOrEmpty(fileName) || fileName.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
			{
				return null;
			}
			bool flag = false;
			Stream result = Streams.CreateTemporaryStorageStream();
			Stream result2;
			try
			{
				using (Stream inputStream = sourceAttachment.GetContentStream(PropertyOpenMode.ReadOnly))
				{
					bool unprotectionSuccess = false;
					MsgToRpMsgConverter.CallRM(delegate
					{
						unprotectionSuccess = ProtectorsManager.Instance.Unprotect(fileName, inputStream, result, issuanceLicense, decryptorHandle);
					}, ServerStrings.FailedToUnprotectAttachment(sourceAttachment.FileName));
					if (!unprotectionSuccess)
					{
						return null;
					}
				}
				result.Position = 0L;
				flag = true;
				result2 = result;
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(result);
				}
			}
			return result2;
		}

		// Token: 0x040016EE RID: 5870
		internal const int AttachMethod = 1;

		// Token: 0x040016EF RID: 5871
		private static readonly SecurityIdentifier[] EmptySidList = new SecurityIdentifier[0];

		// Token: 0x040016F0 RID: 5872
		private bool? isMacAttachment = null;
	}
}
