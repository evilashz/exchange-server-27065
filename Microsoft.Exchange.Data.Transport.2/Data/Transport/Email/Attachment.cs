using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000BB RID: 187
	[DebuggerDisplay("Type: {ContentType} Name: {FileName}")]
	public class Attachment
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00009631 File Offset: 0x00007831
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00009651 File Offset: 0x00007851
		internal AttachmentCookie Cookie
		{
			get
			{
				if (this.cookie.MessageImplementation == null)
				{
					throw new InvalidOperationException(EmailMessageStrings.AttachmentRemovedFromMessage);
				}
				return this.cookie;
			}
			set
			{
				this.cookie = value;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000965A File Offset: 0x0000785A
		internal Attachment(EmailMessage message)
		{
			this.message = message;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00009669 File Offset: 0x00007869
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00009682 File Offset: 0x00007882
		public string ContentType
		{
			get
			{
				this.ThrowIfInvalid();
				return this.message.Attachment_GetContentType(this.cookie);
			}
			set
			{
				this.ThrowIfInvalid();
				this.message.Attachment_SetContentType(this.cookie, value);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000969C File Offset: 0x0000789C
		public bool IsOleAttachment
		{
			get
			{
				this.ThrowIfInvalid();
				return AttachmentMethod.AttachOle == this.message.Attachment_GetAttachmentMethod(this.cookie);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x000096B8 File Offset: 0x000078B8
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x000096E4 File Offset: 0x000078E4
		public AttachmentType AttachmentType
		{
			get
			{
				this.ThrowIfInvalid();
				InternalAttachmentType internalAttachmentType = this.message.Attachment_GetAttachmentType(this.cookie);
				if (internalAttachmentType != InternalAttachmentType.Regular)
				{
					return AttachmentType.Inline;
				}
				return AttachmentType.Regular;
			}
			internal set
			{
				this.ThrowIfInvalid();
				InternalAttachmentType attachmentType = (value == AttachmentType.Regular) ? InternalAttachmentType.Regular : InternalAttachmentType.Related;
				this.message.Attachment_SetAttachmentType(this.cookie, attachmentType);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00009711 File Offset: 0x00007911
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000972A File Offset: 0x0000792A
		public EmailMessage EmbeddedMessage
		{
			get
			{
				this.ThrowIfInvalid();
				return this.message.Attachment_GetEmbeddedMessage(this.cookie);
			}
			set
			{
				this.ThrowIfInvalid();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this.IsEmbeddedMessage)
				{
					throw new InvalidOperationException(EmailMessageStrings.CannotSetEmbeddedMessageForNonMessageRfc822Attachment);
				}
				this.message.Attachment_SetEmbeddedMessage(this.cookie, value);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00009765 File Offset: 0x00007965
		public MimePart MimePart
		{
			get
			{
				this.ThrowIfInvalid();
				return this.message.Attachment_GetMimePart(this.cookie);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000977E File Offset: 0x0000797E
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00009797 File Offset: 0x00007997
		public string FileName
		{
			get
			{
				this.ThrowIfInvalid();
				return this.message.Attachment_GetFileName(this.cookie);
			}
			set
			{
				this.ThrowIfInvalid();
				this.message.Attachment_SetFileName(this.cookie, value);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x000097B1 File Offset: 0x000079B1
		internal bool IsEmbeddedMessage
		{
			get
			{
				this.ThrowIfInvalid();
				return this.AttachmentMethod == AttachmentMethod.EmbeddedMessage && null != this.EmbeddedMessage;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x000097D0 File Offset: 0x000079D0
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x000097E9 File Offset: 0x000079E9
		internal string ContentDisposition
		{
			get
			{
				this.ThrowIfInvalid();
				return this.message.Attachment_GetContentDisposition(this.cookie);
			}
			set
			{
				this.ThrowIfInvalid();
				this.message.Attachment_SetContentDisposition(this.cookie, value);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00009803 File Offset: 0x00007A03
		internal AttachmentMethod AttachmentMethod
		{
			get
			{
				return this.message.Attachment_GetAttachmentMethod(this.cookie);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00009816 File Offset: 0x00007A16
		internal bool IsAppleDouble
		{
			get
			{
				return this.message.Attachment_IsAppleDouble(this.cookie);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00009829 File Offset: 0x00007A29
		internal int RenderingPosition
		{
			get
			{
				this.ThrowIfInvalid();
				return this.message.Attachment_GetRenderingPosition(this.cookie);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00009842 File Offset: 0x00007A42
		internal byte[] AttachRendering
		{
			get
			{
				this.ThrowIfInvalid();
				return this.message.Attachment_GetAttachRendering(this.cookie);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000985B File Offset: 0x00007A5B
		internal string AttachContentID
		{
			get
			{
				this.ThrowIfInvalid();
				return this.message.Attachment_GetAttachContentID(this.cookie);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00009874 File Offset: 0x00007A74
		internal string AttachContentLocation
		{
			get
			{
				this.ThrowIfInvalid();
				return this.message.Attachment_GetAttachContentLocation(this.cookie);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000988D File Offset: 0x00007A8D
		internal int AttachmentFlags
		{
			get
			{
				this.ThrowIfInvalid();
				return this.message.Attachment_GetAttachmentFlags(this.cookie);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x000098A6 File Offset: 0x00007AA6
		internal bool AttachHidden
		{
			get
			{
				this.ThrowIfInvalid();
				return this.message.Attachment_GetAttachHidden(this.cookie);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x000098BF File Offset: 0x00007ABF
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x000098C7 File Offset: 0x00007AC7
		internal string AltContentID
		{
			get
			{
				return this.altContentID;
			}
			set
			{
				this.altContentID = value;
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000098D0 File Offset: 0x00007AD0
		public override int GetHashCode()
		{
			return this.message.Attachment_GetHashCode(this.cookie);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000098E3 File Offset: 0x00007AE3
		public Stream GetContentReadStream()
		{
			this.ThrowIfInvalid();
			return this.message.Attachment_GetContentReadStream(this.cookie);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000098FC File Offset: 0x00007AFC
		public bool TryGetContentReadStream(out Stream result)
		{
			this.ThrowIfInvalid();
			return this.message.Attachment_TryGetContentReadStream(this.cookie, out result);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00009916 File Offset: 0x00007B16
		public Stream GetContentWriteStream()
		{
			this.ThrowIfInvalid();
			return this.message.Attachment_GetContentWriteStream(this.cookie);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000992F File Offset: 0x00007B2F
		internal void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.message.Attachment_Dispose(this.cookie);
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00009951 File Offset: 0x00007B51
		private void ThrowIfInvalid()
		{
			if (this.message == null)
			{
				throw new InvalidOperationException(EmailMessageStrings.AttachmentRemovedFromMessage);
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException("Attachment");
			}
		}

		// Token: 0x04000261 RID: 609
		internal const string DefaultSubject = "No Subject";

		// Token: 0x04000262 RID: 610
		private EmailMessage message;

		// Token: 0x04000263 RID: 611
		private bool disposed;

		// Token: 0x04000264 RID: 612
		private AttachmentCookie cookie;

		// Token: 0x04000265 RID: 613
		private string altContentID;

		// Token: 0x020000BC RID: 188
		internal static class FileNameGenerator
		{
			// Token: 0x0600043A RID: 1082 RVA: 0x0000997C File Offset: 0x00007B7C
			internal static string GenerateFileName(ref int sequenceNumber)
			{
				int num = Interlocked.Increment(ref sequenceNumber);
				return string.Format(CultureInfo.InvariantCulture, Attachment.FileNameGenerator.fileNameFormat, new object[]
				{
					num
				});
			}

			// Token: 0x0600043B RID: 1083 RVA: 0x000099B0 File Offset: 0x00007BB0
			internal static bool IsGeneratedFileName(string fileName)
			{
				if (fileName == null)
				{
					return false;
				}
				if (fileName.Length != 8)
				{
					return false;
				}
				if (!fileName.StartsWith("ATT", StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				for (int i = "ATT".Length; i < fileName.Length; i++)
				{
					if (!char.IsNumber(fileName[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04000266 RID: 614
			private const string FileNamePrefix = "ATT";

			// Token: 0x04000267 RID: 615
			private const int FileNameLength = 8;

			// Token: 0x04000268 RID: 616
			private static readonly int fileNameSequenceNumberLength = 8 - "ATT".Length;

			// Token: 0x04000269 RID: 617
			private static string fileNameFormat = "ATT{0:d" + Attachment.FileNameGenerator.fileNameSequenceNumberLength.ToString() + "}";
		}
	}
}
