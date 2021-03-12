using System;

namespace Microsoft.Exchange.Server.Storage.PropTags
{
	// Token: 0x02000077 RID: 119
	public static class AddressInfoTags
	{
		// Token: 0x04001DD9 RID: 7641
		public static readonly StorePropTag[] SentRepresenting = new StorePropTag[]
		{
			PropTag.Message.SentRepresentingEntryId,
			PropTag.Message.SentRepresentingSearchKey,
			PropTag.Message.SentRepresentingAddressType,
			PropTag.Message.SentRepresentingEmailAddress,
			PropTag.Message.SentRepresentingName,
			PropTag.Message.SentRepresentingSimpleDisplayName,
			PropTag.Message.SentRepresentingFlags,
			PropTag.Message.SentRepresentingOrgAddressType,
			PropTag.Message.SentRepresentingOrgEmailAddr,
			PropTag.Message.SentRepresentingSID,
			PropTag.Message.SentRepresentingGuid
		};

		// Token: 0x04001DDA RID: 7642
		public static readonly StorePropTag[] Sender = new StorePropTag[]
		{
			PropTag.Message.SenderEntryId,
			PropTag.Message.SenderSearchKey,
			PropTag.Message.SenderAddressType,
			PropTag.Message.SenderEmailAddress,
			PropTag.Message.SenderName,
			PropTag.Message.SenderSimpleDisplayName,
			PropTag.Message.SenderFlags,
			PropTag.Message.SenderOrgAddressType,
			PropTag.Message.SenderOrgEmailAddr,
			PropTag.Message.SenderSID,
			PropTag.Message.SenderGuid
		};

		// Token: 0x04001DDB RID: 7643
		public static readonly StorePropTag[] OriginalSentRepresenting = new StorePropTag[]
		{
			PropTag.Message.OriginalSentRepresentingEntryId,
			PropTag.Message.OriginalSentRepresentingSearchKey,
			PropTag.Message.OriginalSentRepresentingAddressType,
			PropTag.Message.OriginalSentRepresentingEmailAddress,
			PropTag.Message.OriginalSentRepresentingName,
			PropTag.Message.OriginalSentRepresentingSimpleDisplayName,
			PropTag.Message.OriginalSentRepresentingFlags,
			PropTag.Message.OriginalSentRepresentingOrgAddressType,
			PropTag.Message.OriginalSentRepresentingOrgEmailAddr,
			PropTag.Message.OriginalSentRepresentingSid,
			PropTag.Message.OriginalSentRepresentingGuid
		};

		// Token: 0x04001DDC RID: 7644
		public static readonly StorePropTag[] OriginalSender = new StorePropTag[]
		{
			PropTag.Message.OriginalSenderEntryId,
			PropTag.Message.OriginalSenderSearchKey,
			PropTag.Message.OriginalSenderAddressType,
			PropTag.Message.OriginalSenderEmailAddress,
			PropTag.Message.OriginalSenderName,
			PropTag.Message.OriginalSenderSimpleDisplayName,
			PropTag.Message.OriginalSenderFlags,
			PropTag.Message.OriginalSenderOrgAddressType,
			PropTag.Message.OriginalSenderOrgEmailAddr,
			PropTag.Message.OriginalSenderSid,
			PropTag.Message.OriginalSenderGuid
		};

		// Token: 0x04001DDD RID: 7645
		public static readonly StorePropTag[] ReceivedRepresenting = new StorePropTag[]
		{
			PropTag.Message.ReceivedRepresentingEntryId,
			PropTag.Message.ReceivedRepresentingSearchKey,
			PropTag.Message.ReceivedRepresentingAddressType,
			PropTag.Message.ReceivedRepresentingEmailAddress,
			PropTag.Message.ReceivedRepresentingName,
			PropTag.Message.ReceivedRepresentingSimpleDisplayName,
			PropTag.Message.RcvdRepresentingFlags,
			PropTag.Message.RcvdRepresentingOrgAddressType,
			PropTag.Message.RcvdRepresentingOrgEmailAddr,
			PropTag.Message.RcvdRepresentingSid,
			PropTag.Message.ReceivedRepresentingGuid
		};

		// Token: 0x04001DDE RID: 7646
		public static readonly StorePropTag[] ReceivedBy = new StorePropTag[]
		{
			PropTag.Message.ReceivedByEntryId,
			PropTag.Message.ReceivedBySearchKey,
			PropTag.Message.ReceivedByAddressType,
			PropTag.Message.ReceivedByEmailAddress,
			PropTag.Message.ReceivedByName,
			PropTag.Message.ReceivedBySimpleDisplayName,
			PropTag.Message.RcvdByFlags,
			PropTag.Message.RcvdByOrgAddressType,
			PropTag.Message.RcvdByOrgEmailAddr,
			PropTag.Message.RcvdBySid,
			PropTag.Message.ReceivedByGuid
		};

		// Token: 0x04001DDF RID: 7647
		public static readonly StorePropTag[] Creator = new StorePropTag[]
		{
			PropTag.Message.CreatorEntryId,
			StorePropTag.Invalid,
			PropTag.Message.CreatorAddressType,
			PropTag.Message.CreatorEmailAddr,
			PropTag.Message.CreatorName,
			PropTag.Message.CreatorSimpleDisplayName,
			PropTag.Message.CreatorFlags,
			PropTag.Message.CreatorOrgAddressType,
			PropTag.Message.CreatorOrgEmailAddr,
			PropTag.Message.CreatorSID,
			PropTag.Message.CreatorGuid
		};

		// Token: 0x04001DE0 RID: 7648
		public static readonly StorePropTag[] LastModifier = new StorePropTag[]
		{
			PropTag.Message.LastModifierEntryId,
			StorePropTag.Invalid,
			PropTag.Message.LastModifierAddressType,
			PropTag.Message.LastModifierEmailAddr,
			PropTag.Message.LastModifierName,
			PropTag.Message.LastModifierSimpleDisplayName,
			PropTag.Message.LastModifierFlags,
			PropTag.Message.LastModifierOrgAddressType,
			PropTag.Message.LastModifierOrgEmailAddr,
			PropTag.Message.LastModifierSid,
			PropTag.Message.LastModifierGuid
		};

		// Token: 0x04001DE1 RID: 7649
		public static readonly StorePropTag[] ReadReceipt = new StorePropTag[]
		{
			PropTag.Message.ReadReceiptEntryId,
			PropTag.Message.ReadReceiptSearchKey,
			PropTag.Message.ReadReceiptAddressType,
			PropTag.Message.ReadReceiptEmailAddress,
			PropTag.Message.ReadReceiptDisplayName,
			PropTag.Message.ReadReceiptSimpleDisplayName,
			PropTag.Message.ReadReceiptFlags,
			PropTag.Message.ReadReceiptOrgAddressType,
			PropTag.Message.ReadReceiptOrgEmailAddr,
			PropTag.Message.ReadReceiptSid,
			PropTag.Message.ReadReceiptGuid
		};

		// Token: 0x04001DE2 RID: 7650
		public static readonly StorePropTag[] Report = new StorePropTag[]
		{
			PropTag.Message.ReportEntryId,
			PropTag.Message.ReportSearchKey,
			PropTag.Message.ReportAddressType,
			PropTag.Message.ReportEmailAddress,
			PropTag.Message.ReportDisplayName,
			PropTag.Message.ReportSimpleDisplayName,
			PropTag.Message.ReportFlags,
			PropTag.Message.ReportOrgAddressType,
			PropTag.Message.ReportOrgEmailAddr,
			PropTag.Message.ReportSid,
			PropTag.Message.ReportGuid
		};

		// Token: 0x04001DE3 RID: 7651
		public static readonly StorePropTag[] Originator = new StorePropTag[]
		{
			PropTag.Message.OriginatorEntryId,
			PropTag.Message.OriginatorSearchKey,
			PropTag.Message.OriginatorAddressType,
			PropTag.Message.OriginatorEmailAddress,
			PropTag.Message.OriginatorName,
			PropTag.Message.OriginatorSimpleDisplayName,
			PropTag.Message.OriginatorFlags,
			PropTag.Message.OriginatorOrgAddressType,
			PropTag.Message.OriginatorOrgEmailAddr,
			PropTag.Message.OriginatorSid,
			PropTag.Message.OriginatorGuid
		};

		// Token: 0x04001DE4 RID: 7652
		public static readonly StorePropTag[] OriginalAuthor = new StorePropTag[]
		{
			PropTag.Message.OriginalAuthorEntryId,
			PropTag.Message.OriginalAuthorSearchKey,
			PropTag.Message.OriginalAuthorAddressType,
			PropTag.Message.OriginalAuthorEmailAddress,
			PropTag.Message.OriginalAuthorName,
			PropTag.Message.OriginalAuthorSimpleDispName,
			PropTag.Message.OriginalAuthorFlags,
			PropTag.Message.OriginalAuthorOrgAddressType,
			PropTag.Message.OriginalAuthorOrgEmailAddr,
			PropTag.Message.OriginalAuthorSid,
			PropTag.Message.OriginalAuthorGuid
		};

		// Token: 0x04001DE5 RID: 7653
		public static readonly StorePropTag[] ReportDestination = new StorePropTag[]
		{
			PropTag.Message.ReportDestinationEntryId,
			PropTag.Message.ReportDestinationSearchKey,
			PropTag.Message.ReportDestinationAddressType,
			PropTag.Message.ReportDestinationEmailAddress,
			PropTag.Message.ReportDestinationName,
			PropTag.Message.ReportDestinationSimpleDisplayName,
			PropTag.Message.ReportDestinationFlags,
			PropTag.Message.ReportDestinationOrgEmailType,
			PropTag.Message.ReportDestinationOrgEmailAddr,
			PropTag.Message.ReportDestinationSid,
			PropTag.Message.ReportDestinationGuid
		};

		// Token: 0x04001DE6 RID: 7654
		public static readonly StorePropTag[][] AddressInfoTagList = new StorePropTag[][]
		{
			AddressInfoTags.SentRepresenting,
			AddressInfoTags.Sender,
			AddressInfoTags.OriginalSentRepresenting,
			AddressInfoTags.OriginalSender,
			AddressInfoTags.ReceivedRepresenting,
			AddressInfoTags.ReceivedBy,
			AddressInfoTags.Creator,
			AddressInfoTags.LastModifier,
			AddressInfoTags.ReadReceipt,
			AddressInfoTags.Report,
			AddressInfoTags.Originator,
			AddressInfoTags.OriginalAuthor,
			AddressInfoTags.ReportDestination
		};

		// Token: 0x02000078 RID: 120
		public enum AddressInfoElementIndex
		{
			// Token: 0x04001DE8 RID: 7656
			EntryId,
			// Token: 0x04001DE9 RID: 7657
			SearchKey,
			// Token: 0x04001DEA RID: 7658
			AddressType,
			// Token: 0x04001DEB RID: 7659
			EmailAddress,
			// Token: 0x04001DEC RID: 7660
			DisplayName,
			// Token: 0x04001DED RID: 7661
			SimpleDisplayName,
			// Token: 0x04001DEE RID: 7662
			Flags,
			// Token: 0x04001DEF RID: 7663
			OriginalAddressType,
			// Token: 0x04001DF0 RID: 7664
			OriginalEmailAddress,
			// Token: 0x04001DF1 RID: 7665
			Sid,
			// Token: 0x04001DF2 RID: 7666
			Guid
		}

		// Token: 0x02000079 RID: 121
		public enum AddressInfoType
		{
			// Token: 0x04001DF4 RID: 7668
			SentRepresenting,
			// Token: 0x04001DF5 RID: 7669
			Sender,
			// Token: 0x04001DF6 RID: 7670
			OriginalSentRepresenting,
			// Token: 0x04001DF7 RID: 7671
			OriginalSender,
			// Token: 0x04001DF8 RID: 7672
			ReceivedRepresenting,
			// Token: 0x04001DF9 RID: 7673
			ReceivedBy,
			// Token: 0x04001DFA RID: 7674
			Creator,
			// Token: 0x04001DFB RID: 7675
			LastModifier,
			// Token: 0x04001DFC RID: 7676
			ReadReceipt,
			// Token: 0x04001DFD RID: 7677
			Report,
			// Token: 0x04001DFE RID: 7678
			Originator,
			// Token: 0x04001DFF RID: 7679
			OriginalAuthor,
			// Token: 0x04001E00 RID: 7680
			ReportDestination
		}

		// Token: 0x0200007A RID: 122
		public enum AlternateAddressInfoType
		{
			// Token: 0x04001E02 RID: 7682
			SentRepresenting,
			// Token: 0x04001E03 RID: 7683
			Sender = 0,
			// Token: 0x04001E04 RID: 7684
			OriginalSentRepresenting = 2,
			// Token: 0x04001E05 RID: 7685
			OriginalSender = 2,
			// Token: 0x04001E06 RID: 7686
			ReceivedRepresenting = 4,
			// Token: 0x04001E07 RID: 7687
			ReceivedBy = 4,
			// Token: 0x04001E08 RID: 7688
			Creator = 6,
			// Token: 0x04001E09 RID: 7689
			LastModifier = 6,
			// Token: 0x04001E0A RID: 7690
			ReadReceipt = 8,
			// Token: 0x04001E0B RID: 7691
			Report,
			// Token: 0x04001E0C RID: 7692
			Originator = 7,
			// Token: 0x04001E0D RID: 7693
			OriginalAuthor = 7,
			// Token: 0x04001E0E RID: 7694
			ReportDestination = 12
		}
	}
}
