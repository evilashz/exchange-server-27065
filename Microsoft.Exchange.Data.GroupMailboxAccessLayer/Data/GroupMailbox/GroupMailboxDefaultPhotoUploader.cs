using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GroupMailboxDefaultPhotoUploader
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00009024 File Offset: 0x00007224
		public GroupMailboxDefaultPhotoUploader(IRecipientSession adSession, IMailboxSession mailboxSession, ADUser group)
		{
			this.adSession = adSession;
			this.mailboxSession = mailboxSession;
			this.group = group;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00009044 File Offset: 0x00007244
		public static bool IsFlightEnabled(MailboxSession mailboxSession)
		{
			return mailboxSession.MailboxOwner.GetConfiguration().MailboxAssistants.GenerateGroupPhoto.Enabled;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00009070 File Offset: 0x00007270
		public static void ClearGroupPhoto(IRecipientSession adSession, IMailboxSession mailboxSession, ADUser group)
		{
			PhotoUploadPipeline photoUploadPipeline = new PhotoUploadPipeline(new PhotosConfiguration(ExchangeSetupContext.InstallPath), mailboxSession, adSession, GroupMailboxDefaultPhotoUploader.Tracer);
			PhotoRequest request = new PhotoRequest
			{
				TargetPrimarySmtpAddress = group.PrimarySmtpAddress.ToString(),
				UploadTo = group.ObjectId,
				Preview = true,
				UploadCommand = UploadCommand.Clear
			};
			photoUploadPipeline.Upload(request, Stream.Null);
			request = new PhotoRequest
			{
				TargetPrimarySmtpAddress = group.PrimarySmtpAddress.ToString(),
				UploadTo = group.ObjectId,
				Preview = false,
				UploadCommand = UploadCommand.Clear
			};
			photoUploadPipeline.Upload(request, Stream.Null);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009128 File Offset: 0x00007328
		public byte[] Upload()
		{
			return this.UploadAndUpdateVersion(1);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009134 File Offset: 0x00007334
		public void UploadIfOutdated()
		{
			int valueOrDefault = this.mailboxSession.Mailbox.GetValueOrDefault<int>(MailboxSchema.GroupMailboxGeneratedPhotoVersion, -1);
			byte[] array = this.ReadGroupMailboxPhoto();
			bool flag = false;
			if (valueOrDefault == -1 && array.Length == 0)
			{
				flag = true;
			}
			else if (valueOrDefault < 1 && array.Length > 0)
			{
				byte[] valueOrDefault2 = this.mailboxSession.Mailbox.GetValueOrDefault<byte[]>(MailboxSchema.GroupMailboxGeneratedPhotoSignature, null);
				byte[] photoSignature = GroupMailboxDefaultPhotoUploader.GetPhotoSignature(array);
				if (valueOrDefault2 != null && photoSignature.SequenceEqual(valueOrDefault2))
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.UploadAndUpdateVersion(1);
				return;
			}
			this.mailboxSession.Mailbox[MailboxSchema.GroupMailboxGeneratedPhotoVersion] = int.MaxValue;
			this.mailboxSession.Mailbox.Save();
			this.mailboxSession.Mailbox.Load();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000091F4 File Offset: 0x000073F4
		private static byte[] GetPhotoSignature(byte[] photo)
		{
			byte[] result;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				sha256Cng.Initialize();
				result = sha256Cng.ComputeHash(photo);
			}
			return result;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00009234 File Offset: 0x00007434
		private byte[] UploadAndUpdateVersion(int version)
		{
			this.InternalUploadPhoto();
			byte[] array = this.ReadGroupMailboxPhoto();
			this.mailboxSession.Mailbox[MailboxSchema.GroupMailboxGeneratedPhotoSignature] = GroupMailboxDefaultPhotoUploader.GetPhotoSignature(array);
			this.mailboxSession.Mailbox[MailboxSchema.GroupMailboxGeneratedPhotoVersion] = version;
			return array;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00009288 File Offset: 0x00007488
		private void InternalUploadPhoto()
		{
			PhotoUploadPipeline photoUploadPipeline = new PhotoUploadPipeline(new PhotosConfiguration(ExchangeSetupContext.InstallPath), this.mailboxSession, this.adSession, GroupMailboxDefaultPhotoUploader.Tracer);
			string text = this.group.DisplayName;
			if (string.IsNullOrWhiteSpace(text))
			{
				text = this.group.Name;
			}
			using (Stream stream = InitialsImageGenerator.GenerateAsStream(text, 1024))
			{
				PhotoRequest request = new PhotoRequest
				{
					TargetPrimarySmtpAddress = this.group.PrimarySmtpAddress.ToString(),
					UploadTo = this.group.ObjectId,
					Preview = true,
					RawUploadedPhoto = stream,
					UploadCommand = UploadCommand.Upload
				};
				photoUploadPipeline.Upload(request, Stream.Null);
				request = new PhotoRequest
				{
					TargetPrimarySmtpAddress = this.group.PrimarySmtpAddress.ToString(),
					UploadTo = this.group.ObjectId,
					Preview = false,
					UploadCommand = UploadCommand.Upload
				};
				photoUploadPipeline.Upload(request, Stream.Null);
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000093BC File Offset: 0x000075BC
		private byte[] ReadGroupMailboxPhoto()
		{
			MailboxPhotoReader mailboxPhotoReader = new MailboxPhotoReader(GroupMailboxDefaultPhotoUploader.Tracer);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(8192))
			{
				try
				{
					mailboxPhotoReader.Read(this.mailboxSession, UserPhotoSize.HR64x64, false, memoryStream, NullPerformanceDataLogger.Instance);
					result = memoryStream.ToArray();
				}
				catch (ObjectNotFoundException)
				{
					result = new byte[0];
				}
			}
			return result;
		}

		// Token: 0x04000088 RID: 136
		public const int PhotoVersion = 1;

		// Token: 0x04000089 RID: 137
		private const int GeneratedPhotoHeightWidth = 1024;

		// Token: 0x0400008A RID: 138
		private const int PhotoReadMemoryBufferSize = 8192;

		// Token: 0x0400008B RID: 139
		private static readonly Trace Tracer = ExTraceGlobals.GroupMailboxAccessLayerTracer;

		// Token: 0x0400008C RID: 140
		private readonly IRecipientSession adSession;

		// Token: 0x0400008D RID: 141
		private readonly IMailboxSession mailboxSession;

		// Token: 0x0400008E RID: 142
		private ADUser group;
	}
}
