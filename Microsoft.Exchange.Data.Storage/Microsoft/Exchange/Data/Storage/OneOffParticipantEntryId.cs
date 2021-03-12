using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000923 RID: 2339
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OneOffParticipantEntryId : ParticipantEntryId
	{
		// Token: 0x0600576C RID: 22380 RVA: 0x001679B4 File Offset: 0x00165BB4
		internal OneOffParticipantEntryId(Participant participant) : this(participant.DisplayName, participant.EmailAddress, participant.RoutingType)
		{
			bool valueOrDefault = participant.GetValueOrDefault<bool>(ParticipantSchema.SendRichInfo);
			uint num = (uint)participant.GetValueOrDefault<int>(ParticipantSchema.SendInternetEncoding);
			if ((num & 4286709759U) != 0U)
			{
				num = 0U;
			}
			this.flags = ((this.flags & ~(OneOffFlag.NoTnef | OneOffFlag.SendInternetEncodingMask)) | (valueOrDefault ? OneOffFlag.Simple : OneOffFlag.NoTnef) | (OneOffFlag)(num & 8257536U));
		}

		// Token: 0x0600576D RID: 22381 RVA: 0x00167A22 File Offset: 0x00165C22
		internal OneOffParticipantEntryId(string displayName, string address, string addressType) : this(displayName, addressType, address, (OneOffFlag)2147549184U)
		{
		}

		// Token: 0x0600576E RID: 22382 RVA: 0x00167A34 File Offset: 0x00165C34
		internal OneOffParticipantEntryId(string displayName, string addressType, string address, OneOffFlag flags)
		{
			EnumValidator.AssertValid<OneOffFlag>(flags);
			this.emailDisplayName = Util.NullIf<string>(displayName, string.Empty);
			this.emailAddressType = Util.NullIf<string>(addressType, string.Empty);
			this.emailAddress = Util.NullIf<string>(address, string.Empty);
			this.flags = flags;
		}

		// Token: 0x0600576F RID: 22383 RVA: 0x00167A8C File Offset: 0x00165C8C
		internal OneOffParticipantEntryId(ParticipantEntryId.Reader reader)
		{
			this.flags = (OneOffFlag)reader.ReadUInt32();
			Encoding encoding = ((this.flags & (OneOffFlag)2147483648U) == (OneOffFlag)2147483648U) ? Encoding.Unicode : CTSGlobals.AsciiEncoding;
			this.emailDisplayName = Util.NullIf<string>(reader.ReadZString(encoding), string.Empty);
			this.emailAddressType = Util.NullIf<string>(reader.ReadZString(encoding), string.Empty);
			this.emailAddress = Util.NullIf<string>(reader.ReadZString(encoding), string.Empty);
			reader.EnsureEnd();
			if (this.emailAddress == "Unknown")
			{
				this.emailAddress = null;
			}
		}

		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x06005770 RID: 22384 RVA: 0x00167B2F File Offset: 0x00165D2F
		internal string EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
		}

		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x06005771 RID: 22385 RVA: 0x00167B37 File Offset: 0x00165D37
		internal string EmailAddressType
		{
			get
			{
				return this.emailAddressType;
			}
		}

		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x06005772 RID: 22386 RVA: 0x00167B3F File Offset: 0x00165D3F
		internal string EmailDisplayName
		{
			get
			{
				return this.emailDisplayName;
			}
		}

		// Token: 0x06005773 RID: 22387 RVA: 0x00167B48 File Offset: 0x00165D48
		public override string ToString()
		{
			return string.Format("OneOff: \"{0}\" {1} [{2}]; {3}", new object[]
			{
				this.emailDisplayName,
				this.emailAddress,
				this.emailAddressType,
				this.flags
			});
		}

		// Token: 0x06005774 RID: 22388 RVA: 0x00167B90 File Offset: 0x00165D90
		internal static OneOffParticipantEntryId TryFromParticipant(Participant participant, ParticipantEntryIdConsumer consumer)
		{
			if (!(participant.Origin is OneOffParticipantOrigin))
			{
				Participant participant2 = participant.ChangeOrigin(new OneOffParticipantOrigin());
				if (!participant.AreAddressesEqual(participant2))
				{
					return null;
				}
				participant = participant2;
			}
			if (participant.RoutingType != null)
			{
				return new OneOffParticipantEntryId(participant);
			}
			return null;
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x00167BDC File Offset: 0x00165DDC
		internal override IEnumerable<PropValue> GetParticipantProperties()
		{
			List<PropValue> list = Participant.ListCoreProperties(this.emailDisplayName, this.emailAddress, this.emailAddressType);
			if ((this.flags & OneOffFlag.NoTnef) != OneOffFlag.NoTnef)
			{
				list.Add(new PropValue(ParticipantSchema.SendRichInfo, (this.flags & OneOffFlag.NoTnef) == OneOffFlag.Simple));
			}
			if ((this.flags & OneOffFlag.SendInternetEncodingMask) != OneOffFlag.Simple)
			{
				list.Add(new PropValue(ParticipantSchema.SendInternetEncoding, (int)(this.flags & OneOffFlag.SendInternetEncodingMask)));
			}
			return list;
		}

		// Token: 0x06005776 RID: 22390 RVA: 0x00167C68 File Offset: 0x00165E68
		internal Participant ToParticipant()
		{
			return new Participant(this.EmailDisplayName, this.EmailAddress, this.EmailAddressType);
		}

		// Token: 0x06005777 RID: 22391 RVA: 0x00167C84 File Offset: 0x00165E84
		protected override void Serialize(ParticipantEntryId.Writer writer)
		{
			writer.WriteEntryHeader(ParticipantEntryId.OneOffProviderGuid);
			writer.Write((uint)this.flags);
			Encoding encoding = ((this.flags & (OneOffFlag)2147483648U) == (OneOffFlag)2147483648U) ? Encoding.Unicode : CTSGlobals.AsciiEncoding;
			writer.WriteZString(this.emailDisplayName ?? string.Empty, encoding);
			writer.WriteZString(this.emailAddressType ?? string.Empty, encoding);
			writer.WriteZString(this.emailAddress ?? string.Empty, encoding);
		}

		// Token: 0x04002EAC RID: 11948
		private const string OutlookPlaceholderForMissingAddress = "Unknown";

		// Token: 0x04002EAD RID: 11949
		private const OneOffFlag DefaultOneOffFlag = OneOffFlag.NoTnef | OneOffFlag.Unicode;

		// Token: 0x04002EAE RID: 11950
		private readonly string emailAddress;

		// Token: 0x04002EAF RID: 11951
		private readonly string emailDisplayName;

		// Token: 0x04002EB0 RID: 11952
		private readonly string emailAddressType;

		// Token: 0x04002EB1 RID: 11953
		private readonly OneOffFlag flags;
	}
}
