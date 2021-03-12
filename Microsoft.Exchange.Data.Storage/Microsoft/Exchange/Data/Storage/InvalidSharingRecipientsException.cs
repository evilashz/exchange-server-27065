using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D9D RID: 3485
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class InvalidSharingRecipientsException : StoragePermanentException
	{
		// Token: 0x060077DC RID: 30684 RVA: 0x0021122C File Offset: 0x0020F42C
		internal InvalidSharingRecipientsException(string[] invalidRecipients, Exception innerException) : base(ServerStrings.InvalidSharingRecipientsException, innerException)
		{
			List<InvalidRecipient> list = new List<InvalidRecipient>(invalidRecipients.Length);
			foreach (string smtpAddress in invalidRecipients)
			{
				list.Add(new InvalidRecipient(smtpAddress, InvalidRecipientResponseCodeType.OtherError, null));
			}
			this.InvalidRecipients = list.ToArray();
		}

		// Token: 0x060077DD RID: 30685 RVA: 0x0021127C File Offset: 0x0020F47C
		internal InvalidSharingRecipientsException(InvalidRecipient[] invalidRecipients) : this(invalidRecipients, null)
		{
		}

		// Token: 0x060077DE RID: 30686 RVA: 0x00211286 File Offset: 0x0020F486
		internal InvalidSharingRecipientsException(InvalidRecipient[] invalidRecipients, InvalidSharingRecipientsResolution resolution) : base(ServerStrings.InvalidSharingRecipientsException)
		{
			this.InvalidRecipients = invalidRecipients;
			this.InvalidRecipientsResolution = resolution;
		}

		// Token: 0x1700200A RID: 8202
		// (get) Token: 0x060077DF RID: 30687 RVA: 0x002112A1 File Offset: 0x0020F4A1
		// (set) Token: 0x060077E0 RID: 30688 RVA: 0x002112A9 File Offset: 0x0020F4A9
		public InvalidRecipient[] InvalidRecipients { get; private set; }

		// Token: 0x1700200B RID: 8203
		// (get) Token: 0x060077E1 RID: 30689 RVA: 0x002112B2 File Offset: 0x0020F4B2
		// (set) Token: 0x060077E2 RID: 30690 RVA: 0x002112BA File Offset: 0x0020F4BA
		public InvalidSharingRecipientsResolution InvalidRecipientsResolution { get; private set; }

		// Token: 0x060077E3 RID: 30691 RVA: 0x002112C4 File Offset: 0x0020F4C4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.AppendLine(base.ToString());
			stringBuilder.AppendLine("InvalidRecipients:");
			foreach (InvalidRecipient invalidRecipient in this.InvalidRecipients)
			{
				stringBuilder.AppendLine(invalidRecipient.ToString());
			}
			stringBuilder.AppendLine("InvalidRecipientsResolution:");
			if (this.InvalidRecipientsResolution != null)
			{
				stringBuilder.AppendLine(this.InvalidRecipientsResolution.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
