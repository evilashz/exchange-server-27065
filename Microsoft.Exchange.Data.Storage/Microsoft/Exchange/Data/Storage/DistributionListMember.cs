using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004E1 RID: 1249
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DistributionListMember : IDistributionListMember, IRecipientBase
	{
		// Token: 0x06003698 RID: 13976 RVA: 0x000DCC20 File Offset: 0x000DAE20
		internal DistributionListMember(DistributionList distributionList, Participant participant)
		{
			if (participant == null)
			{
				throw new ArgumentNullException("participant");
			}
			this.distributionList = distributionList;
			this.participant = participant;
			this.memberStatus = MemberStatus.Normal;
			this.mainEntryId = ParticipantEntryId.FromParticipant(participant, ParticipantEntryIdConsumer.DLMemberList);
			if (this.mainEntryId == null)
			{
				throw new InvalidParticipantException(ServerStrings.ExOperationNotSupportedForRoutingType("DistributionList.Add", participant.RoutingType), ParticipantValidationStatus.OperationNotSupportedForRoutingType);
			}
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000DCC8A File Offset: 0x000DAE8A
		internal DistributionListMember(DistributionList distributionList, ParticipantEntryId mainEntryId, OneOffParticipantEntryId oneOffEntryId, byte[] extraBytes) : this(distributionList, mainEntryId, oneOffEntryId)
		{
			if (extraBytes != null)
			{
				this.extraBytes = extraBytes;
			}
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000DCCA4 File Offset: 0x000DAEA4
		internal DistributionListMember(DistributionList distributionList, ParticipantEntryId mainEntryId, OneOffParticipantEntryId oneOffEntryId)
		{
			this.distributionList = distributionList;
			this.memberStatus = MemberStatus.Normal;
			Participant.Builder builder = new Participant.Builder();
			ADParticipantEntryId adparticipantEntryId = mainEntryId as ADParticipantEntryId;
			if (adparticipantEntryId != null)
			{
				builder.SetPropertiesFrom(adparticipantEntryId);
				if (oneOffEntryId != null)
				{
					builder.DisplayName = oneOffEntryId.EmailDisplayName;
					if (!string.IsNullOrEmpty(oneOffEntryId.EmailAddress) && Participant.RoutingTypeEquals(oneOffEntryId.EmailAddressType, "SMTP"))
					{
						builder[ParticipantSchema.SmtpAddress] = oneOffEntryId.EmailAddress;
					}
				}
				this.participant = builder.ToParticipant();
			}
			else
			{
				StoreParticipantEntryId storeParticipantEntryId = mainEntryId as StoreParticipantEntryId;
				if (storeParticipantEntryId != null && oneOffEntryId != null)
				{
					builder.SetPropertiesFrom(oneOffEntryId);
					builder.SetPropertiesFrom(storeParticipantEntryId);
					this.participant = builder.ToParticipant();
				}
				else
				{
					OneOffParticipantEntryId oneOffParticipantEntryId = mainEntryId as OneOffParticipantEntryId;
					if (oneOffParticipantEntryId == null)
					{
						oneOffParticipantEntryId = oneOffEntryId;
						this.memberStatus = MemberStatus.Demoted;
					}
					if (oneOffParticipantEntryId != null)
					{
						builder.SetPropertiesFrom(oneOffParticipantEntryId);
						this.participant = builder.ToParticipant();
					}
					else
					{
						this.memberStatus = MemberStatus.Unrecognized;
					}
				}
			}
			if (this.mainEntryId == null)
			{
				this.mainEntryId = mainEntryId;
			}
			if (this.oneOffEntryId == null)
			{
				this.oneOffEntryId = oneOffEntryId;
			}
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Loaded a {1} DL member \"{0}\". MainEntryId=\"{2}\", OneOffEntryId=\"{3}\"", new object[]
			{
				this.participant,
				this.memberStatus,
				this.mainEntryId,
				this.oneOffEntryId
			});
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x0600369B RID: 13979 RVA: 0x000DCDEF File Offset: 0x000DAFEF
		public ParticipantEntryId MainEntryId
		{
			get
			{
				return this.mainEntryId;
			}
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x0600369C RID: 13980 RVA: 0x000DCDF7 File Offset: 0x000DAFF7
		internal OneOffParticipantEntryId OneOffEntryId
		{
			get
			{
				if (this.oneOffEntryId == null && this.participant != null)
				{
					this.oneOffEntryId = new OneOffParticipantEntryId(this.participant);
				}
				return this.oneOffEntryId;
			}
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x0600369D RID: 13981 RVA: 0x000DCE26 File Offset: 0x000DB026
		// (set) Token: 0x0600369E RID: 13982 RVA: 0x000DCE37 File Offset: 0x000DB037
		internal byte[] ExtraBytes
		{
			get
			{
				return this.extraBytes ?? Array<byte>.Empty;
			}
			set
			{
				this.extraBytes = value;
			}
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000DCE40 File Offset: 0x000DB040
		internal static DistributionListMember CopyFrom(DistributionList distributionList, DistributionListMember member)
		{
			if (member != null)
			{
				return new DistributionListMember(distributionList, member.mainEntryId, member.oneOffEntryId);
			}
			if (member.Participant != null)
			{
				return new DistributionListMember(distributionList, member.Participant);
			}
			throw new NotSupportedException(ServerStrings.ExCantCopyBadAlienDLMember);
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x060036A0 RID: 13984 RVA: 0x000DCE8F File Offset: 0x000DB08F
		public DistributionList DistributionList
		{
			get
			{
				return this.distributionList;
			}
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x000DCE97 File Offset: 0x000DB097
		public MemberStatus MemberStatus
		{
			get
			{
				return this.memberStatus;
			}
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x060036A2 RID: 13986 RVA: 0x000DCE9F File Offset: 0x000DB09F
		public RecipientId Id
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000DCEA6 File Offset: 0x000DB0A6
		public bool? IsDistributionList()
		{
			return this.mainEntryId.IsDL;
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x060036A4 RID: 13988 RVA: 0x000DCEB3 File Offset: 0x000DB0B3
		public Participant Participant
		{
			get
			{
				return this.participant;
			}
		}

		// Token: 0x04001D2C RID: 7468
		private readonly DistributionList distributionList;

		// Token: 0x04001D2D RID: 7469
		private readonly ParticipantEntryId mainEntryId;

		// Token: 0x04001D2E RID: 7470
		private readonly MemberStatus memberStatus;

		// Token: 0x04001D2F RID: 7471
		private OneOffParticipantEntryId oneOffEntryId;

		// Token: 0x04001D30 RID: 7472
		private byte[] extraBytes;

		// Token: 0x04001D31 RID: 7473
		private readonly Participant participant;
	}
}
