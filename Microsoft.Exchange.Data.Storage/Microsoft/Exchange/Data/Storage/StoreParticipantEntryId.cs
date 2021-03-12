using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000924 RID: 2340
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class StoreParticipantEntryId : ParticipantEntryId
	{
		// Token: 0x06005778 RID: 22392 RVA: 0x00167D0C File Offset: 0x00165F0C
		internal StoreParticipantEntryId(ParticipantEntryId.Reader reader)
		{
			uint num = reader.ReadUInt32();
			if (num != 3U)
			{
				throw new NotSupportedException(ServerStrings.ExUnsupportedABProvider("OLABP", num.ToString()));
			}
			uint num2 = reader.ReadUInt32();
			this.isMapiPDL = (num2 == 5U);
			this.emailAddressIndex = StoreParticipantEntryId.IndexToEmailAddressIndex(reader.ReadUInt32());
			if (reader.ReadUInt32() != 70U)
			{
				throw new NotSupportedException(ServerStrings.ExInvalidParticipantEntryId);
			}
			this.ltEntryId = reader.ReadLTEntryId();
			if (reader.BytesRemaining != 3)
			{
				reader.EnsureEnd();
			}
		}

		// Token: 0x06005779 RID: 22393 RVA: 0x00167D9D File Offset: 0x00165F9D
		internal StoreParticipantEntryId(ParticipantEntryId.WabEntryFlag flags, ParticipantEntryId.Reader reader)
		{
			EnumValidator.AssertValid<ParticipantEntryId.WabEntryFlag>(flags);
			this.isMapiPDL = StoreParticipantEntryId.WabEntryFlagToMapiPDL(flags);
			this.emailAddressIndex = StoreParticipantEntryId.WabEntryFlagToEmailAddressIndex(flags);
			this.ltEntryId = reader.ReadLTEntryId();
			this.useWabFormat = true;
			reader.EnsureEnd();
		}

		// Token: 0x0600577A RID: 22394 RVA: 0x00167DDC File Offset: 0x00165FDC
		private StoreParticipantEntryId(StoreObjectId itemId, bool isMapiPDL, EmailAddressIndex emailIndex, bool useWabFormat)
		{
			EnumValidator.AssertValid<EmailAddressIndex>(emailIndex);
			using (ParticipantEntryId.Reader reader = new ParticipantEntryId.Reader(itemId.ProviderLevelItemId))
			{
				this.ltEntryId = reader.ReadLTEntryId();
				reader.EnsureEnd();
			}
			this.isMapiPDL = isMapiPDL;
			this.emailAddressIndex = emailIndex;
			this.useWabFormat = useWabFormat;
		}

		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x0600577B RID: 22395 RVA: 0x00167E48 File Offset: 0x00166048
		internal EmailAddressIndex EmailAddressIndex
		{
			get
			{
				return this.emailAddressIndex;
			}
		}

		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x0600577C RID: 22396 RVA: 0x00167E50 File Offset: 0x00166050
		internal override bool? IsDL
		{
			get
			{
				return new bool?(this.isMapiPDL);
			}
		}

		// Token: 0x0600577D RID: 22397 RVA: 0x00167E60 File Offset: 0x00166060
		public override string ToString()
		{
			return string.Format("Store contact{0}: 0x{1:X} / 0x{2:X}", (this.IsDL == true) ? " (DL)" : string.Empty, this.ltEntryId.FolderId.GlobCntLong, this.ltEntryId.MessageId.GlobCntLong);
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x00167ECC File Offset: 0x001660CC
		internal static StoreParticipantEntryId TryFromParticipant(Participant participant, ParticipantEntryIdConsumer consumer)
		{
			if ((consumer & ParticipantEntryIdConsumer.SupportsStoreParticipantEntryId) != ParticipantEntryIdConsumer.SupportsNone || (participant.RoutingType == "MAPIPDL" && (consumer & ParticipantEntryIdConsumer.SupportsStoreParticipantEntryIdForPDLs) != ParticipantEntryIdConsumer.SupportsNone))
			{
				StoreParticipantOrigin storeParticipantOrigin = participant.Origin as StoreParticipantOrigin;
				if (storeParticipantOrigin != null)
				{
					return new StoreParticipantEntryId(storeParticipantOrigin.OriginItemId, participant.RoutingType == "MAPIPDL", storeParticipantOrigin.EmailAddressIndex, (consumer & ParticipantEntryIdConsumer.SupportsWindowsAddressBookEnvelope) != ParticipantEntryIdConsumer.SupportsNone);
				}
				if (participant.RoutingType == "MAPIPDL")
				{
					ExTraceGlobals.StorageTracer.TraceDebug<Participant>((long)participant.GetHashCode(), "Cannot create an entry id: ContactDL should have StoreParticipantOrigin: {0}", participant);
				}
			}
			return null;
		}

		// Token: 0x0600577F RID: 22399 RVA: 0x00167F59 File Offset: 0x00166159
		internal override IEnumerable<PropValue> GetParticipantProperties()
		{
			return this.GetParticipantOrigin().GetProperties();
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x00167F66 File Offset: 0x00166166
		internal override ParticipantOrigin GetParticipantOrigin()
		{
			return new StoreParticipantOrigin(this.ToUniqueItemId(), this.emailAddressIndex);
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x00167F7C File Offset: 0x0016617C
		internal StoreObjectId ToUniqueItemId()
		{
			StoreObjectId result;
			using (ParticipantEntryId.Writer writer = new ParticipantEntryId.Writer())
			{
				writer.Write(this.ltEntryId);
				result = StoreObjectId.FromProviderSpecificId(writer.GetBytes(), (this.IsDL == true) ? StoreObjectType.DistributionList : StoreObjectType.Contact);
			}
			return result;
		}

		// Token: 0x06005782 RID: 22402 RVA: 0x00167FE8 File Offset: 0x001661E8
		protected override void Serialize(ParticipantEntryId.Writer writer)
		{
			if (this.useWabFormat)
			{
				writer.WriteEntryHeader(ParticipantEntryId.WabProviderGuid);
				writer.Write((byte)(StoreParticipantEntryId.ToWabEntryFlag(this.emailAddressIndex, this.isMapiPDL) | ParticipantEntryId.WabEntryFlag.Outlook));
				writer.Write(this.ltEntryId);
				return;
			}
			writer.WriteEntryHeader(ParticipantEntryId.OlabProviderGuid);
			writer.Write(3U);
			writer.Write(this.isMapiPDL ? 5U : 4U);
			writer.Write(StoreParticipantEntryId.EmailAddressIndexToIndex(this.emailAddressIndex));
			writer.Write(70);
			writer.Write(this.ltEntryId);
		}

		// Token: 0x06005783 RID: 22403 RVA: 0x0016807C File Offset: 0x0016627C
		private static uint EmailAddressIndexToIndex(EmailAddressIndex emailAddressIndex)
		{
			switch (emailAddressIndex)
			{
			case EmailAddressIndex.None:
				return 255U;
			case EmailAddressIndex.Email1:
				return 0U;
			case EmailAddressIndex.Email2:
				return 1U;
			case EmailAddressIndex.Email3:
				return 2U;
			case EmailAddressIndex.BusinessFax:
				return 3U;
			case EmailAddressIndex.HomeFax:
				return 4U;
			case EmailAddressIndex.OtherFax:
				return 5U;
			default:
				throw new ArgumentException();
			}
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x001680C8 File Offset: 0x001662C8
		private static ParticipantEntryId.WabEntryFlag ToWabEntryFlag(EmailAddressIndex emailAddressIndex, bool isMapiPDL)
		{
			switch (emailAddressIndex)
			{
			case EmailAddressIndex.None:
				return (ParticipantEntryId.WabEntryFlag)((isMapiPDL ? 4 : 3) | 48);
			case EmailAddressIndex.Email1:
				return (ParticipantEntryId.WabEntryFlag)67;
			case EmailAddressIndex.Email2:
				return (ParticipantEntryId.WabEntryFlag)83;
			case EmailAddressIndex.Email3:
				return (ParticipantEntryId.WabEntryFlag)99;
			case EmailAddressIndex.BusinessFax:
				return ParticipantEntryId.WabEntryFlag.ContactPerson;
			case EmailAddressIndex.HomeFax:
				return (ParticipantEntryId.WabEntryFlag)19;
			case EmailAddressIndex.OtherFax:
				return (ParticipantEntryId.WabEntryFlag)35;
			default:
				throw new ArgumentException();
			}
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x00168120 File Offset: 0x00166320
		private static EmailAddressIndex IndexToEmailAddressIndex(uint index)
		{
			switch (index)
			{
			case 0U:
				return EmailAddressIndex.Email1;
			case 1U:
				return EmailAddressIndex.Email2;
			case 2U:
				return EmailAddressIndex.Email3;
			case 3U:
				return EmailAddressIndex.BusinessFax;
			case 4U:
				return EmailAddressIndex.HomeFax;
			case 5U:
				return EmailAddressIndex.OtherFax;
			default:
				if (index != 255U)
				{
					throw new CorruptDataException(ServerStrings.ExInvalidParticipantEntryId);
				}
				return EmailAddressIndex.None;
			}
		}

		// Token: 0x06005786 RID: 22406 RVA: 0x00168170 File Offset: 0x00166370
		private static EmailAddressIndex WabEntryFlagToEmailAddressIndex(ParticipantEntryId.WabEntryFlag wabEntryFlag)
		{
			ParticipantEntryId.WabEntryFlag wabEntryFlag2 = wabEntryFlag & ParticipantEntryId.WabEntryFlag.EmailIndexMask;
			if (wabEntryFlag2 <= ParticipantEntryId.WabEntryFlag.OtherFax)
			{
				if (wabEntryFlag2 == ParticipantEntryId.WabEntryFlag.Envelope)
				{
					return EmailAddressIndex.BusinessFax;
				}
				if (wabEntryFlag2 == ParticipantEntryId.WabEntryFlag.HomeFax)
				{
					return EmailAddressIndex.HomeFax;
				}
				if (wabEntryFlag2 == ParticipantEntryId.WabEntryFlag.OtherFax)
				{
					return EmailAddressIndex.OtherFax;
				}
			}
			else if (wabEntryFlag2 <= ParticipantEntryId.WabEntryFlag.EmailIndex1)
			{
				if (wabEntryFlag2 == ParticipantEntryId.WabEntryFlag.NoEmailIndex)
				{
					return EmailAddressIndex.None;
				}
				if (wabEntryFlag2 == ParticipantEntryId.WabEntryFlag.EmailIndex1)
				{
					return EmailAddressIndex.Email1;
				}
			}
			else
			{
				if (wabEntryFlag2 == ParticipantEntryId.WabEntryFlag.EmailIndex2)
				{
					return EmailAddressIndex.Email2;
				}
				if (wabEntryFlag2 == ParticipantEntryId.WabEntryFlag.EmailIndex3)
				{
					return EmailAddressIndex.Email3;
				}
			}
			throw new CorruptDataException(ServerStrings.ExInvalidParticipantEntryId);
		}

		// Token: 0x06005787 RID: 22407 RVA: 0x001681D0 File Offset: 0x001663D0
		private static bool WabEntryFlagToMapiPDL(ParticipantEntryId.WabEntryFlag wabEntryFlag)
		{
			switch ((byte)(wabEntryFlag & ParticipantEntryId.WabEntryFlag.ObjectTypeMask))
			{
			case 3:
				return false;
			case 4:
				return true;
			default:
				throw new CorruptDataException(ServerStrings.ExInvalidParticipantEntryId);
			}
		}

		// Token: 0x04002EB2 RID: 11954
		private const uint TypeDL = 5U;

		// Token: 0x04002EB3 RID: 11955
		private const uint TypeContact = 4U;

		// Token: 0x04002EB4 RID: 11956
		private const uint OlabpVersion = 3U;

		// Token: 0x04002EB5 RID: 11957
		private readonly EmailAddressIndex emailAddressIndex;

		// Token: 0x04002EB6 RID: 11958
		private readonly ParticipantEntryId.LTEntryId ltEntryId;

		// Token: 0x04002EB7 RID: 11959
		private readonly bool useWabFormat;

		// Token: 0x04002EB8 RID: 11960
		private readonly bool isMapiPDL;
	}
}
