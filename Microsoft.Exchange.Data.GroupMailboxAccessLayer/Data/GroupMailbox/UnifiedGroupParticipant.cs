using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000052 RID: 82
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UnifiedGroupParticipant
	{
		// Token: 0x0600029D RID: 669 RVA: 0x0000FFBA File Offset: 0x0000E1BA
		private UnifiedGroupParticipant(ADRawEntry participantEntry)
		{
			ArgumentValidator.ThrowIfNull("participantEntry", participantEntry);
			this.participantEntry = participantEntry;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000FFDC File Offset: 0x0000E1DC
		public bool IsOwner
		{
			get
			{
				return this.isOwner;
			}
			internal set
			{
				this.isOwner = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000FFE5 File Offset: 0x0000E1E5
		public ADObjectId Id
		{
			get
			{
				return (ADObjectId)this.participantEntry[ADObjectSchema.Id];
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000FFFC File Offset: 0x0000E1FC
		public string DisplayName
		{
			get
			{
				return (string)this.participantEntry[ADRecipientSchema.DisplayName];
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00010013 File Offset: 0x0000E213
		public string Title
		{
			get
			{
				return (string)this.participantEntry[ADUserSchema.Title];
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0001002A File Offset: 0x0000E22A
		public string Alias
		{
			get
			{
				return (string)this.participantEntry[ADRecipientSchema.Alias];
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00010041 File Offset: 0x0000E241
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this.participantEntry[ADRecipientSchema.PrimarySmtpAddress];
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00010058 File Offset: 0x0000E258
		public string PrimarySmtpAddressToString
		{
			get
			{
				return this.PrimarySmtpAddress.ToString();
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0001007C File Offset: 0x0000E27C
		public string SipAddress
		{
			get
			{
				string result;
				if ((result = this.sipAddress) == null)
				{
					result = (this.sipAddress = ADPersonToContactConverter.GetSipUri(this.participantEntry));
				}
				return result;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000100A7 File Offset: 0x0000E2A7
		public string ExternalDirectoryObjectId
		{
			get
			{
				return (string)this.participantEntry[ADRecipientSchema.ExternalDirectoryObjectId];
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000100BE File Offset: 0x0000E2BE
		internal static UnifiedGroupParticipant CreateFromADRawEntry(ADRawEntry entry)
		{
			return new UnifiedGroupParticipant(entry);
		}

		// Token: 0x0400016E RID: 366
		internal static readonly ADPropertyDefinition[] DefaultMemberProperties = new ADPropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADRecipientSchema.DisplayName,
			ADUserSchema.Title,
			ADRecipientSchema.Alias,
			ADUserSchema.RTCSIPPrimaryUserAddress,
			ADRecipientSchema.EmailAddresses,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.ExternalDirectoryObjectId
		};

		// Token: 0x0400016F RID: 367
		private ADRawEntry participantEntry;

		// Token: 0x04000170 RID: 368
		private string sipAddress;

		// Token: 0x04000171 RID: 369
		private bool isOwner;
	}
}
