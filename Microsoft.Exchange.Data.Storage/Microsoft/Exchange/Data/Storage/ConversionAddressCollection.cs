using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005B7 RID: 1463
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ConversionAddressCollection
	{
		// Token: 0x06003C0A RID: 15370 RVA: 0x000F6BF1 File Offset: 0x000F4DF1
		protected ConversionAddressCollection(bool useSimpleDisplayName, bool ewsOutboundMimeConversion)
		{
			this.participantLists = new List<IConversionParticipantList>();
			this.useSimpleDisplayName = useSimpleDisplayName;
			this.ewsOutboundMimeConversion = ewsOutboundMimeConversion;
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x000F6C12 File Offset: 0x000F4E12
		internal void AddParticipantList(IConversionParticipantList list)
		{
			this.participantLists.Add(list);
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x000F6C20 File Offset: 0x000F4E20
		protected ConversionAddressCollection.ParticipantResolutionList CreateResolutionList()
		{
			ConversionAddressCollection.ParticipantResolutionList participantResolutionList = new ConversionAddressCollection.ParticipantResolutionList();
			foreach (IConversionParticipantList conversionParticipantList in this.participantLists)
			{
				int count = conversionParticipantList.Count;
				for (int num = 0; num != count; num++)
				{
					Participant participant = null;
					Participant participant2 = conversionParticipantList[num];
					if (conversionParticipantList.IsConversionParticipantAlwaysResolvable(num) || this.CanResolveParticipant(participant2))
					{
						participant = participant2;
					}
					if (!this.disableLengthValidation && participant2 != null && (participant2.ValidationStatus == ParticipantValidationStatus.EmailAddressTooBig || participant2.ValidationStatus == ParticipantValidationStatus.RoutingTypeTooBig))
					{
						StorageGlobals.ContextTraceError(ExTraceGlobals.CcInboundMimeTracer, "ConversionAddressCollection::CreateResolutionList: participant validation failed.");
						throw new ConversionFailedException(ConversionFailureReason.ExceedsLimit);
					}
					participantResolutionList.AddParticipantForResolution(participant);
				}
			}
			return participantResolutionList;
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x000F6CFC File Offset: 0x000F4EFC
		protected void ResolveParticipants(ConversionAddressCollection.ParticipantResolutionList resolutionList)
		{
			Participant.Job job = resolutionList.CreateResolutionJob();
			if (this.ewsOutboundMimeConversion)
			{
				PropertyDefinition propertyDefinition;
				Participant.BatchBuilder.Execute(job, new Participant.BatchBuilder[]
				{
					Participant.BatchBuilder.ConvertRoutingType(this.TargetResolutionType, out propertyDefinition),
					Participant.BatchBuilder.RequestAllProperties(),
					Participant.BatchBuilder.CopyPropertiesFromInput(),
					Participant.BatchBuilder.GetPropertiesFromAD(this.GetRecipientCache(job.Count), null),
					this.useSimpleDisplayName ? Participant.BatchBuilder.ReplaceProperty(ParticipantSchema.DisplayName, ParticipantSchema.SimpleDisplayName) : null
				});
			}
			else
			{
				PropertyDefinition propertyDefinition;
				Participant.BatchBuilder.Execute(job, new Participant.BatchBuilder[]
				{
					Participant.BatchBuilder.ConvertRoutingType(this.TargetResolutionType, out propertyDefinition),
					Participant.BatchBuilder.RequestAllProperties(),
					Participant.BatchBuilder.GetPropertiesFromAD(this.GetRecipientCache(job.Count), null),
					Participant.BatchBuilder.CopyPropertiesFromInput(),
					this.useSimpleDisplayName ? Participant.BatchBuilder.ReplaceProperty(ParticipantSchema.DisplayName, ParticipantSchema.SimpleDisplayName) : null
				});
			}
			job.ApplyResults();
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x000F6DE0 File Offset: 0x000F4FE0
		protected void SetResolvedParticipants(ConversionAddressCollection.ParticipantResolutionList participantList)
		{
			foreach (IConversionParticipantList conversionParticipantList in this.participantLists)
			{
				int count = conversionParticipantList.Count;
				for (int num = 0; num != count; num++)
				{
					Participant nextResolvedParticipant = participantList.GetNextResolvedParticipant();
					if (nextResolvedParticipant != null)
					{
						if (nextResolvedParticipant.ValidationStatus == ParticipantValidationStatus.NoError)
						{
							conversionParticipantList[num] = nextResolvedParticipant;
						}
						else
						{
							StorageGlobals.ContextTraceError<Participant, Participant>(ExTraceGlobals.CcGenericTracer, "The resolved Participant is invalid. The source Participant will be used instead\r\n\tSource: {0}\r\n\tResolved: {1}", conversionParticipantList[num], nextResolvedParticipant);
						}
					}
				}
			}
		}

		// Token: 0x06003C0F RID: 15375
		protected abstract IADRecipientCache GetRecipientCache(int count);

		// Token: 0x06003C10 RID: 15376
		protected abstract bool CanResolveParticipant(Participant participant);

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06003C11 RID: 15377
		protected abstract string TargetResolutionType { get; }

		// Token: 0x04001FEF RID: 8175
		private List<IConversionParticipantList> participantLists;

		// Token: 0x04001FF0 RID: 8176
		protected bool disableLengthValidation;

		// Token: 0x04001FF1 RID: 8177
		private readonly bool useSimpleDisplayName;

		// Token: 0x04001FF2 RID: 8178
		protected bool ewsOutboundMimeConversion;

		// Token: 0x020005B8 RID: 1464
		internal enum ParticipantListId
		{
			// Token: 0x04001FF4 RID: 8180
			Recipients,
			// Token: 0x04001FF5 RID: 8181
			TnefRecipients,
			// Token: 0x04001FF6 RID: 8182
			ReplyTo,
			// Token: 0x04001FF7 RID: 8183
			ItemAddressProperties
		}

		// Token: 0x020005B9 RID: 1465
		protected class ParticipantResolutionList
		{
			// Token: 0x06003C12 RID: 15378 RVA: 0x000F6E7C File Offset: 0x000F507C
			internal ParticipantResolutionList()
			{
				this.originalParticipantList = new List<Participant>();
				this.resolvedParticipantList = new List<Participant>();
			}

			// Token: 0x06003C13 RID: 15379 RVA: 0x000F6E9A File Offset: 0x000F509A
			internal void AddParticipantForResolution(Participant participant)
			{
				this.originalParticipantList.Add(participant);
			}

			// Token: 0x06003C14 RID: 15380 RVA: 0x000F6EA8 File Offset: 0x000F50A8
			private void AddResolvedParticipant(Result<Participant> result)
			{
				this.resolvedParticipantList.Add(result.Data);
			}

			// Token: 0x06003C15 RID: 15381 RVA: 0x000F6EBC File Offset: 0x000F50BC
			internal Participant.Job CreateResolutionJob()
			{
				Participant.Job job = new Participant.Job(this.originalParticipantList.Count);
				foreach (Participant input in this.originalParticipantList)
				{
					job.Add(new Participant.JobItem(input, new Action<Result<Participant>>(this.AddResolvedParticipant)));
				}
				return job;
			}

			// Token: 0x06003C16 RID: 15382 RVA: 0x000F6F34 File Offset: 0x000F5134
			internal Participant GetNextResolvedParticipant()
			{
				return this.resolvedParticipantList[this.resolvedIndex++];
			}

			// Token: 0x04001FF8 RID: 8184
			private readonly List<Participant> originalParticipantList;

			// Token: 0x04001FF9 RID: 8185
			private readonly List<Participant> resolvedParticipantList;

			// Token: 0x04001FFA RID: 8186
			private int resolvedIndex;
		}
	}
}
