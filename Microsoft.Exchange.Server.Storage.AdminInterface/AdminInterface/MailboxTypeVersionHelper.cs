using System;
using Microsoft.Exchange.Data.MailboxSignature;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x0200006D RID: 109
	internal static class MailboxTypeVersionHelper
	{
		// Token: 0x0600023B RID: 571 RVA: 0x00010BB4 File Offset: 0x0000EDB4
		internal static Mailbox.MailboxTypeVersion Parse(MailboxSignatureSectionMetadata metadata, byte[] buffer, ref int offset)
		{
			int posMax = offset + metadata.Length;
			MailboxInfo.MailboxType dword = (MailboxInfo.MailboxType)ParseSerialize.GetDword(buffer, ref offset, posMax);
			MailboxInfo.MailboxTypeDetail dword2 = (MailboxInfo.MailboxTypeDetail)ParseSerialize.GetDword(buffer, ref offset, posMax);
			uint dword3 = ParseSerialize.GetDword(buffer, ref offset, posMax);
			return new Mailbox.MailboxTypeVersion(dword, dword2, dword3);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00010BF0 File Offset: 0x0000EDF0
		public static int Serialize(Context context, Mailbox mailbox, byte[] buffer, int offset)
		{
			uint version;
			bool assertCondition;
			bool mailboxTypeVersion = Mailbox.GetMailboxTypeVersion(context, mailbox.SharedState.MailboxType, mailbox.SharedState.MailboxTypeDetail, out version, out assertCondition);
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(mailboxTypeVersion, "Requested to serilized MailboxTypeVersion for unversioned mailbox type.");
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(assertCondition, "Requested to serilized versioned mailbox type that is not allowed on this server.");
			return MailboxTypeVersionHelper.Serialize(mailbox.SharedState.MailboxType, mailbox.SharedState.MailboxTypeDetail, version, buffer, offset);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00010C54 File Offset: 0x0000EE54
		public static int Serialize(MailboxInfo.MailboxType mailboxType, MailboxInfo.MailboxTypeDetail mailboxTypeDetail, uint version, byte[] buffer, int offset)
		{
			int num = offset;
			ParseSerialize.SetDword(buffer, ref num, (uint)mailboxType);
			ParseSerialize.SetDword(buffer, ref num, (uint)mailboxTypeDetail);
			ParseSerialize.SetDword(buffer, ref num, version);
			return num - offset;
		}

		// Token: 0x0400022F RID: 559
		internal const short RequiredVersion = 1;
	}
}
