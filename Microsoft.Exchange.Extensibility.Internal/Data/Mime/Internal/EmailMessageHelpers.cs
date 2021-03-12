using System;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.Data.Mime.Internal
{
	// Token: 0x02000048 RID: 72
	internal sealed class EmailMessageHelpers
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x00010C44 File Offset: 0x0000EE44
		public static bool IsAppleDoubleAttachment(Attachment attachment)
		{
			return attachment.IsAppleDouble;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00010C4C File Offset: 0x0000EE4C
		public static bool IsEmbeddedMessageAttachment(Attachment attachment)
		{
			return attachment.IsEmbeddedMessage;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00010C54 File Offset: 0x0000EE54
		public static int GetRenderingPosition(Attachment attachment)
		{
			return attachment.RenderingPosition;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00010C5C File Offset: 0x0000EE5C
		public static byte[] GetAttachRendering(Attachment attachment)
		{
			return attachment.AttachRendering;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00010C64 File Offset: 0x0000EE64
		public static string GetAttachContentID(Attachment attachment)
		{
			return attachment.AttachContentID;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00010C6C File Offset: 0x0000EE6C
		public static string GetAttachContentLocation(Attachment attachment)
		{
			return attachment.AttachContentLocation;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00010C74 File Offset: 0x0000EE74
		public static Charset GetTnefTextCharset(EmailMessage emailMessage)
		{
			return emailMessage.TnefTextCharset;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00010C7C File Offset: 0x0000EE7C
		public static bool TryGetTnefBinaryCharset(EmailMessage emailMessage, out Charset charset)
		{
			return emailMessage.TryGetTnefBinaryCharset(out charset);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00010C85 File Offset: 0x0000EE85
		public static int GetAttachmentFlags(Attachment attachment)
		{
			return attachment.AttachmentFlags;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00010C8D File Offset: 0x0000EE8D
		public static bool GetAttachHidden(Attachment attachment)
		{
			return attachment.AttachHidden;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00010C95 File Offset: 0x0000EE95
		public static string GetHeaderValue(Header header)
		{
			return Utility.GetHeaderValue(header);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00010C9D File Offset: 0x0000EE9D
		public static string GenerateFileName(ref int sequenceNumber)
		{
			return Attachment.FileNameGenerator.GenerateFileName(ref sequenceNumber);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00010CA5 File Offset: 0x0000EEA5
		public static bool IsGeneratedFileName(string name)
		{
			return Attachment.FileNameGenerator.IsGeneratedFileName(name);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00010CAD File Offset: 0x0000EEAD
		public static bool IsPublicFolderReplicationMessage(EmailMessage message)
		{
			return message.IsPublicFolderReplicationMessage;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00010CB5 File Offset: 0x0000EEB5
		public static string RemoveMimeHeaderComments(string headerValue)
		{
			return Utility.RemoveMimeHeaderComments(headerValue);
		}

		// Token: 0x04000349 RID: 841
		public const TnefComplianceStatus BannedTnefComplianceViolations = ~(TnefComplianceStatus.InvalidAttributeChecksum | TnefComplianceStatus.InvalidMessageCodepage | TnefComplianceStatus.InvalidDate);

		// Token: 0x0400034A RID: 842
		public static readonly TnefNameTag TnefNameTagIsClassified = TnefPropertyBag.TnefNameTagIsClassified;

		// Token: 0x0400034B RID: 843
		public static readonly TnefNameTag TnefNameTagClassification = TnefPropertyBag.TnefNameTagClassification;

		// Token: 0x0400034C RID: 844
		public static readonly TnefNameTag TnefNameTagClassificationDescription = TnefPropertyBag.TnefNameTagClassificationDescription;

		// Token: 0x0400034D RID: 845
		public static readonly TnefNameTag TnefNameTagClassificationGuid = TnefPropertyBag.TnefNameTagClassificationGuid;

		// Token: 0x0400034E RID: 846
		public static readonly TnefNameTag TnefNameTagClassificationKeep = TnefPropertyBag.TnefNameTagClassificationKeep;
	}
}
