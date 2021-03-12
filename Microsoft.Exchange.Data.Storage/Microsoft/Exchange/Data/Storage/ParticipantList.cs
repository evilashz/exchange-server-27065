using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000853 RID: 2131
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ParticipantList : Collection<Participant>, IConversionParticipantList
	{
		// Token: 0x06004FE3 RID: 20451 RVA: 0x0014CD0E File Offset: 0x0014AF0E
		internal ParticipantList(PropertyBag propertyBag, AtomicStorePropertyDefinition participantsBlobStorePropertyDefinition, AtomicStorePropertyDefinition participantsNamesStorePropertyDefinition, AtomicStorePropertyDefinition participantCountStorePropertyDefinition, bool suppressCorruptDataException = false)
		{
			if (participantsBlobStorePropertyDefinition == null)
			{
				throw new ArgumentNullException("participantsBlobStorePropertyDefinition");
			}
			this.participantsBlobStorePropertyDefinition = participantsBlobStorePropertyDefinition;
			this.participantsNamesStorePropertyDefinition = participantsNamesStorePropertyDefinition;
			this.participantsCountStorePropertyDefinition = participantCountStorePropertyDefinition;
			this.propertyBag = propertyBag;
			this.Resync(suppressCorruptDataException);
		}

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x06004FE4 RID: 20452 RVA: 0x0014CD49 File Offset: 0x0014AF49
		// (set) Token: 0x06004FE5 RID: 20453 RVA: 0x0014CD57 File Offset: 0x0014AF57
		internal byte[] Blob
		{
			get
			{
				this.CalculateMapiProperties();
				return this.blob;
			}
			set
			{
				this.CalculateMapiProperties();
				this.blob = value;
				this.isPropertiesChanged = true;
				this.ParseMapiProperties();
			}
		}

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x06004FE6 RID: 20454 RVA: 0x0014CD73 File Offset: 0x0014AF73
		// (set) Token: 0x06004FE7 RID: 20455 RVA: 0x0014CD81 File Offset: 0x0014AF81
		internal string Names
		{
			get
			{
				this.CalculateMapiProperties();
				return this.names;
			}
			set
			{
				this.CalculateMapiProperties();
				this.names = value;
				this.isPropertiesChanged = true;
				this.ParseMapiProperties();
			}
		}

		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x06004FE8 RID: 20456 RVA: 0x0014CD9D File Offset: 0x0014AF9D
		internal bool IsDirty
		{
			get
			{
				return this.isPropertiesChanged || this.isListChanged || this.isCorrectionNeeded;
			}
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x0014CDB7 File Offset: 0x0014AFB7
		public bool IsConversionParticipantAlwaysResolvable(int index)
		{
			return true;
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x0014CDBA File Offset: 0x0014AFBA
		internal void Save()
		{
			this.isListChanged |= this.isCorrectionNeeded;
			this.UpdateNativeProperties();
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x0014CDD8 File Offset: 0x0014AFD8
		internal void Resync(bool suppressCorruptDataException)
		{
			if (!this.isListChanged && !this.isPropertiesChanged)
			{
				try
				{
					this.blob = this.propertyBag.GetValueOrDefault<byte[]>(this.participantsBlobStorePropertyDefinition);
					this.names = ((this.participantsNamesStorePropertyDefinition != null) ? this.propertyBag.GetValueOrDefault<string>(this.participantsNamesStorePropertyDefinition) : null);
					this.ParseMapiProperties();
				}
				catch (CorruptDataException)
				{
					if (!suppressCorruptDataException)
					{
						throw;
					}
					base.ClearItems();
					this.isListChanged = false;
					this.isPropertiesChanged = false;
				}
			}
			this.UpdateNativeProperties();
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x0014CE6C File Offset: 0x0014B06C
		protected override void InsertItem(int index, Participant participant)
		{
			ParticipantList.VerifyParticipant(participant);
			base.InsertItem(index, participant);
			this.isListChanged = true;
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x0014CE83 File Offset: 0x0014B083
		protected override void SetItem(int index, Participant participant)
		{
			ParticipantList.VerifyParticipant(participant);
			base.SetItem(index, participant);
			this.isListChanged = true;
		}

		// Token: 0x06004FEE RID: 20462 RVA: 0x0014CE9A File Offset: 0x0014B09A
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
			this.isListChanged = true;
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x0014CEAA File Offset: 0x0014B0AA
		protected override void ClearItems()
		{
			base.ClearItems();
			this.isListChanged = true;
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x0014CEB9 File Offset: 0x0014B0B9
		private static void VerifyParticipant(Participant participant)
		{
			if (participant.RoutingType == "MAPIPDL" || participant.RoutingType == null)
			{
				throw new InvalidParticipantException(ServerStrings.ExOperationNotSupportedForRoutingType("ParticipantList: Add/Insert/Replace", participant.RoutingType), ParticipantValidationStatus.OperationNotSupportedForRoutingType);
			}
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x0014CEF4 File Offset: 0x0014B0F4
		private void ParseMapiProperties()
		{
			base.Clear();
			if (this.blob != null)
			{
				IList<ParticipantEntryId> list;
				try
				{
					list = ParticipantEntryId.FromFlatEntryList(this.blob);
				}
				catch (CorruptDataException)
				{
					this.isCorrectionNeeded = true;
					throw;
				}
				string[] array = null;
				if (this.names != null)
				{
					array = this.names.Split(new char[]
					{
						';'
					});
					if (array.Length != list.Count)
					{
						array = null;
					}
				}
				int num = 0;
				foreach (ParticipantEntryId participantEntryId in list)
				{
					Participant.Builder builder = new Participant.Builder();
					string text = null;
					if (array != null)
					{
						text = array[num].Trim();
					}
					num++;
					if (participantEntryId is OneOffParticipantEntryId || participantEntryId is ADParticipantEntryId)
					{
						builder.SetPropertiesFrom(participantEntryId);
					}
					else
					{
						this.isCorrectionNeeded = true;
						if (text == null)
						{
							continue;
						}
						builder.EmailAddress = text;
						builder.RoutingType = "SMTP";
					}
					if (text != null)
					{
						builder.DisplayName = text;
					}
					base.Items.Add(builder.ToParticipant());
				}
			}
			this.isListChanged = false;
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x0014D024 File Offset: 0x0014B224
		private void CalculateMapiProperties()
		{
			if (!this.isListChanged && !this.isCorrectionNeeded)
			{
				return;
			}
			if (base.Count == 0)
			{
				this.names = null;
				this.blob = null;
			}
			else
			{
				ParticipantEntryId[] array = new ParticipantEntryId[base.Count];
				int num = 0;
				StringBuilder stringBuilder = new StringBuilder(base.Count * 16);
				foreach (Participant participant in this)
				{
					if (num != 0)
					{
						stringBuilder.Append(';');
					}
					array[num++] = ParticipantEntryId.FromParticipant(participant, ParticipantEntryIdConsumer.SupportsADParticipantEntryId);
					stringBuilder.Append(participant.DisplayName);
				}
				this.blob = ParticipantEntryId.ToFlatEntryList(array);
				this.names = stringBuilder.ToString();
			}
			this.isListChanged = false;
			this.isCorrectionNeeded = false;
			this.isPropertiesChanged = true;
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x0014D108 File Offset: 0x0014B308
		private void UpdateNativeProperties()
		{
			if (this.isListChanged)
			{
				this.CalculateMapiProperties();
			}
			if (this.isPropertiesChanged)
			{
				this.propertyBag.SetOrDeleteProperty(this.participantsBlobStorePropertyDefinition, this.blob);
				if (this.participantsNamesStorePropertyDefinition != null)
				{
					this.propertyBag.SetOrDeleteProperty(this.participantsNamesStorePropertyDefinition, this.names);
				}
				if (this.participantsCountStorePropertyDefinition != null)
				{
					if (base.Count == 0)
					{
						this.propertyBag.Delete(this.participantsCountStorePropertyDefinition);
					}
					else
					{
						this.propertyBag.SetProperty(this.participantsCountStorePropertyDefinition, base.Count);
					}
				}
				this.isPropertiesChanged = false;
			}
		}

		// Token: 0x04002B5C RID: 11100
		private readonly AtomicStorePropertyDefinition participantsNamesStorePropertyDefinition;

		// Token: 0x04002B5D RID: 11101
		private readonly AtomicStorePropertyDefinition participantsBlobStorePropertyDefinition;

		// Token: 0x04002B5E RID: 11102
		private readonly AtomicStorePropertyDefinition participantsCountStorePropertyDefinition;

		// Token: 0x04002B5F RID: 11103
		private readonly PropertyBag propertyBag;

		// Token: 0x04002B60 RID: 11104
		private bool isPropertiesChanged;

		// Token: 0x04002B61 RID: 11105
		private bool isListChanged;

		// Token: 0x04002B62 RID: 11106
		private bool isCorrectionNeeded;

		// Token: 0x04002B63 RID: 11107
		private byte[] blob;

		// Token: 0x04002B64 RID: 11108
		private string names;
	}
}
