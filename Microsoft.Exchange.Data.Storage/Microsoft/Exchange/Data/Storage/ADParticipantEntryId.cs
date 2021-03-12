using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000922 RID: 2338
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ADParticipantEntryId : ParticipantEntryId
	{
		// Token: 0x06005763 RID: 22371 RVA: 0x00167683 File Offset: 0x00165883
		internal ADParticipantEntryId(string legacyDN, LegacyRecipientDisplayType? legacyRecipientDisplayType, bool useWabFormat)
		{
			this.legacyDN = legacyDN;
			this.legacyRecipientDisplayType = legacyRecipientDisplayType;
			this.flags = ADParticipantEntryId.ReplaceObjectTypeInformation(ParticipantEntryId.WabEntryFlag.HomeFax | ParticipantEntryId.WabEntryFlag.OtherFax | ParticipantEntryId.WabEntryFlag.Outlook, ref this.legacyRecipientDisplayType, this.legacyDN);
			this.useWabFormat = useWabFormat;
		}

		// Token: 0x06005764 RID: 22372 RVA: 0x001676BC File Offset: 0x001658BC
		internal ADParticipantEntryId(ParticipantEntryId.WabEntryFlag? flags, ParticipantEntryId.Reader reader)
		{
			uint num = reader.ReadUInt32();
			if (num != 1U)
			{
				throw new NotSupportedException(ServerStrings.ExUnsupportedABProvider("Exchange WAB", num.ToString()));
			}
			this.legacyRecipientDisplayType = new LegacyRecipientDisplayType?((LegacyRecipientDisplayType)reader.ReadUInt32());
			if (flags == null && this.legacyRecipientDisplayType == LegacyRecipientDisplayType.MailUser)
			{
				this.legacyRecipientDisplayType = null;
			}
			this.legacyDN = reader.ReadZString(CTSGlobals.AsciiEncoding);
			this.flags = ADParticipantEntryId.ReplaceObjectTypeInformation(flags ?? ParticipantEntryId.WabEntryFlag.Envelope, ref this.legacyRecipientDisplayType, this.legacyDN);
			this.useWabFormat = (flags != null);
			reader.EnsureEnd();
		}

		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x06005765 RID: 22373 RVA: 0x00167788 File Offset: 0x00165988
		internal override bool? IsDL
		{
			get
			{
				if (this.legacyRecipientDisplayType == null)
				{
					return null;
				}
				return new bool?((byte)(this.flags & ParticipantEntryId.WabEntryFlag.ObjectTypeMask) == 6);
			}
		}

		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x06005766 RID: 22374 RVA: 0x001677C1 File Offset: 0x001659C1
		internal string LegacyDN
		{
			get
			{
				return this.legacyDN;
			}
		}

		// Token: 0x06005767 RID: 22375 RVA: 0x001677CC File Offset: 0x001659CC
		public override string ToString()
		{
			return string.Format("AD contact{0}: \"{1}\"", (this.IsDL == true) ? " (DL)" : string.Empty, this.legacyDN);
		}

		// Token: 0x06005768 RID: 22376 RVA: 0x00167811 File Offset: 0x00165A11
		internal static ADParticipantEntryId TryFromParticipant(Participant participant, ParticipantEntryIdConsumer consumer)
		{
			if ((consumer & ParticipantEntryIdConsumer.SupportsADParticipantEntryId) != ParticipantEntryIdConsumer.SupportsNone && participant.RoutingType == "EX")
			{
				return new ADParticipantEntryId(participant.EmailAddress, participant.GetValueAsNullable<LegacyRecipientDisplayType>(ParticipantSchema.DisplayType), (consumer & ParticipantEntryIdConsumer.SupportsWindowsAddressBookEnvelope) != ParticipantEntryIdConsumer.SupportsNone);
			}
			return null;
		}

		// Token: 0x06005769 RID: 22377 RVA: 0x0016784C File Offset: 0x00165A4C
		internal override IEnumerable<PropValue> GetParticipantProperties()
		{
			List<PropValue> list = new List<PropValue>();
			list.Add(new PropValue(ParticipantSchema.EmailAddress, this.legacyDN));
			list.Add(new PropValue(ParticipantSchema.RoutingType, "EX"));
			if (this.legacyRecipientDisplayType != null)
			{
				list.Add(new PropValue(ParticipantSchema.DisplayType, this.legacyRecipientDisplayType));
			}
			return list;
		}

		// Token: 0x0600576A RID: 22378 RVA: 0x001678B8 File Offset: 0x00165AB8
		protected override void Serialize(ParticipantEntryId.Writer writer)
		{
			if (this.useWabFormat && this.legacyRecipientDisplayType != null)
			{
				writer.WriteEntryHeader(ParticipantEntryId.WabProviderGuid);
				writer.Write((byte)this.flags);
			}
			writer.WriteEntryHeader(ParticipantEntryId.ExchangeProviderGuid);
			writer.Write(1U);
			writer.Write((uint)(this.legacyRecipientDisplayType ?? LegacyRecipientDisplayType.MailUser));
			writer.WriteZString(this.legacyDN, CTSGlobals.AsciiEncoding);
		}

		// Token: 0x0600576B RID: 22379 RVA: 0x00167938 File Offset: 0x00165B38
		private static ParticipantEntryId.WabEntryFlag ReplaceObjectTypeInformation(ParticipantEntryId.WabEntryFlag input, ref LegacyRecipientDisplayType? legacyRecipientDisplayType, string legacyDN)
		{
			LegacyRecipientDisplayType valueOrDefault = legacyRecipientDisplayType.GetValueOrDefault();
			ParticipantEntryId.WabEntryFlag wabEntryFlag;
			if (legacyRecipientDisplayType != null)
			{
				switch (valueOrDefault)
				{
				case LegacyRecipientDisplayType.MailUser:
				case LegacyRecipientDisplayType.Forum:
				case LegacyRecipientDisplayType.RemoteMailUser:
					goto IL_33;
				case LegacyRecipientDisplayType.DistributionList:
				case LegacyRecipientDisplayType.DynamicDistributionList:
					wabEntryFlag = ParticipantEntryId.WabEntryFlag.DirectoryDL;
					goto IL_61;
				}
				ExTraceGlobals.StorageTracer.TraceDebug<string, LegacyRecipientDisplayType?>(0L, "Cannot construct ADParticipantEntryId (legDN=\"{0}\") with DisplayType={1}. Defaulting to MailUser.", legacyDN, legacyRecipientDisplayType);
				legacyRecipientDisplayType = new LegacyRecipientDisplayType?(LegacyRecipientDisplayType.MailUser);
			}
			IL_33:
			wabEntryFlag = ParticipantEntryId.WabEntryFlag.DirectoryPerson;
			IL_61:
			return (input & (ParticipantEntryId.WabEntryFlag.HomeFax | ParticipantEntryId.WabEntryFlag.OtherFax | ParticipantEntryId.WabEntryFlag.EmailIndex1 | ParticipantEntryId.WabEntryFlag.Outlook)) | wabEntryFlag;
		}

		// Token: 0x04002EA8 RID: 11944
		private readonly ParticipantEntryId.WabEntryFlag flags;

		// Token: 0x04002EA9 RID: 11945
		private readonly string legacyDN;

		// Token: 0x04002EAA RID: 11946
		private readonly LegacyRecipientDisplayType? legacyRecipientDisplayType;

		// Token: 0x04002EAB RID: 11947
		private readonly bool useWabFormat;
	}
}
