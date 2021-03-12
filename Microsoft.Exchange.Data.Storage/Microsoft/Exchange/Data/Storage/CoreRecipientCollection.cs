using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000655 RID: 1621
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CoreRecipientCollection : DisposableObject, IEnumerable<CoreRecipient>, IEnumerable, ILocationIdentifierController
	{
		// Token: 0x06004360 RID: 17248 RVA: 0x0011D9D8 File Offset: 0x0011BBD8
		internal CoreRecipientCollection(ICoreItem coreItem)
		{
			bool flag = false;
			try
			{
				this.recipientTable = new RecipientTable(coreItem);
				this.coreItem = coreItem;
				if (this.CoreItem.Session != null)
				{
					this.recipientTable.BuildRecipientCollection(new Action<IList<NativeStorePropertyDefinition>, object[]>(this.AddRecipientFromTable));
					this.nextRecipientRowId = this.recipientList.Count;
					for (int i = this.recipientList.Count - 1; i >= 0; i--)
					{
						CoreRecipient coreRecipient = this.recipientList[i];
						if (coreRecipient.Participant == null)
						{
							this.RemoveAt(i, !this.CoreItem.IsReadOnly);
						}
					}
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					base.Dispose();
				}
			}
		}

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x06004361 RID: 17249 RVA: 0x0011DAA8 File Offset: 0x0011BCA8
		public int Count
		{
			get
			{
				this.CheckDisposed(null);
				return this.recipientList.Count;
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06004362 RID: 17250 RVA: 0x0011DABC File Offset: 0x0011BCBC
		public LocationIdentifierHelper LocationIdentifierHelperInstance
		{
			get
			{
				this.CheckDisposed(null);
				return this.coreItem.LocationIdentifierHelperInstance;
			}
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x06004363 RID: 17251 RVA: 0x0011DAD0 File Offset: 0x0011BCD0
		internal ICoreItem CoreItem
		{
			get
			{
				this.CheckDisposed(null);
				return this.coreItem;
			}
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06004364 RID: 17252 RVA: 0x0011DADF File Offset: 0x0011BCDF
		internal bool IsDirty
		{
			get
			{
				this.CheckDisposed(null);
				return this.recipientTable.IsDirty;
			}
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x0011DAF4 File Offset: 0x0011BCF4
		public void Remove(int rowId)
		{
			this.CheckDisposed(null);
			if (rowId < 0)
			{
				throw new ArgumentOutOfRangeException("rowId", rowId, "RowId cannot be negative");
			}
			int index = 0;
			if (this.TryFindRecipient(rowId, out index))
			{
				this.RemoveAt(index);
			}
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x0011DB38 File Offset: 0x0011BD38
		public CoreRecipient CreateOrReplace(int rowId)
		{
			this.CheckDisposed(null);
			if (rowId < 0)
			{
				throw new ArgumentOutOfRangeException("rowId", rowId, "RowId cannot be negative");
			}
			int index = 0;
			CoreRecipient coreRecipient2;
			if (this.TryFindRecipient(rowId, out index))
			{
				CoreRecipient coreRecipient = this.recipientList[index];
				coreRecipient.OnRemoveRecipient();
				coreRecipient2 = new CoreRecipient(this.recipientTable, rowId);
				this.recipientList[index] = coreRecipient2;
			}
			else
			{
				coreRecipient2 = new CoreRecipient(this.recipientTable, rowId);
				this.recipientList.Insert(index, coreRecipient2);
				if (rowId >= this.nextRecipientRowId)
				{
					this.nextRecipientRowId = rowId + 1;
				}
			}
			return coreRecipient2;
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x0011DBD0 File Offset: 0x0011BDD0
		public void Clear()
		{
			this.CheckDisposed(null);
			for (int i = this.recipientList.Count - 1; i >= 0; i--)
			{
				this.RemoveAt(i);
			}
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x0011DC03 File Offset: 0x0011BE03
		public IEnumerator<CoreRecipient> GetEnumerator()
		{
			this.CheckDisposed(null);
			return this.recipientList.GetEnumerator();
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x0011DC17 File Offset: 0x0011BE17
		IEnumerator IEnumerable.GetEnumerator()
		{
			this.CheckDisposed(null);
			return this.GetEnumerator();
		}

		// Token: 0x0600436A RID: 17258 RVA: 0x0011DC26 File Offset: 0x0011BE26
		internal bool Contains(CoreRecipient coreRecipient)
		{
			this.CheckDisposed(null);
			return this.IndexOf(coreRecipient) != -1;
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x0011DC3C File Offset: 0x0011BE3C
		internal int IndexOf(CoreRecipient coreRecipient)
		{
			this.CheckDisposed(null);
			return this.recipientList.IndexOf(coreRecipient);
		}

		// Token: 0x0600436C RID: 17260 RVA: 0x0011DC54 File Offset: 0x0011BE54
		internal int IndexOf(RecipientId id)
		{
			this.CheckDisposed(null);
			for (int i = 0; i < this.recipientList.Count; i++)
			{
				CoreRecipient coreRecipient = this.recipientList[i];
				if (coreRecipient.Id.Equals(id))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x0011DC9C File Offset: 0x0011BE9C
		internal CoreRecipient GetCoreRecipient(int index)
		{
			this.CheckDisposed(null);
			return this.recipientList[index];
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x0011DCB1 File Offset: 0x0011BEB1
		internal bool FindRemovedRecipient(Participant participant, out CoreRecipient recipient)
		{
			this.CheckDisposed(null);
			return this.recipientTable.FindRemovedRecipient(participant, out recipient);
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x0011DCC7 File Offset: 0x0011BEC7
		internal void RemoveAt(int index)
		{
			this.RemoveAt(index, true);
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x0011DCD4 File Offset: 0x0011BED4
		private void RemoveAt(int index, bool trackRecipientChanges)
		{
			this.CheckDisposed(null);
			CoreRecipient coreRecipient = this.recipientList[index];
			this.recipientList.RemoveAt(index);
			if (trackRecipientChanges)
			{
				coreRecipient.OnRemoveRecipient();
			}
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x0011DD0C File Offset: 0x0011BF0C
		internal void CopyRecipientsFrom(CoreRecipientCollection recipientCollection)
		{
			this.CheckDisposed(null);
			foreach (CoreRecipient sourceCoreRecipient in recipientCollection.recipientList)
			{
				this.CreateCoreRecipient(sourceCoreRecipient);
			}
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x0011DD64 File Offset: 0x0011BF64
		internal void Save()
		{
			this.CheckDisposed(null);
			this.LookupMandatoryPropertiesIfNeeded();
			this.recipientTable.Save();
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x0011DD80 File Offset: 0x0011BF80
		internal CoreRecipient CreateCoreRecipient(CoreRecipient sourceCoreRecipient)
		{
			this.CheckDisposed(null);
			CoreRecipient coreRecipient = new CoreRecipient(sourceCoreRecipient, this.nextRecipientRowId++, this.recipientTable.RecipientChangeTracker, this.recipientTable.ExTimeZone);
			this.recipientList.Add(coreRecipient);
			return coreRecipient;
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x0011DDD0 File Offset: 0x0011BFD0
		internal CoreRecipient CreateCoreRecipient(CoreRecipient.SetDefaultPropertiesDelegate setDefaultPropertiesDelegate, Participant participant)
		{
			this.CheckDisposed(null);
			CoreRecipient coreRecipient = new CoreRecipient(this.recipientTable, this.nextRecipientRowId++, setDefaultPropertiesDelegate, participant);
			this.recipientList.Add(coreRecipient);
			return coreRecipient;
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x0011DE10 File Offset: 0x0011C010
		internal void FilterRecipients(Predicate<CoreRecipient> shouldKeepRecipient)
		{
			for (int i = this.recipientList.Count - 1; i >= 0; i--)
			{
				if (!shouldKeepRecipient(this.recipientList[i]))
				{
					this.RemoveAt(i);
				}
			}
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x0011DE50 File Offset: 0x0011C050
		internal void LoadAdditionalParticipantProperties(PropertyDefinition[] keyProperties)
		{
			this.CheckDisposed(null);
			if (StandaloneFuzzing.IsEnabled)
			{
				return;
			}
			Participant.Job job = new Participant.Job(this.Count);
			foreach (CoreRecipient coreRecipient in this)
			{
				if (coreRecipient.Participant == null)
				{
					throw new InvalidOperationException("The Participant is not present. This recipient has not been fully formed.");
				}
				job.Add(new Participant.JobItem((coreRecipient.Participant.RoutingType == "EX") ? coreRecipient.Participant : null));
			}
			this.ExecuteJob(job, keyProperties);
			for (int i = 0; i < this.Count; i++)
			{
				if (job[i].Result != null && job[i].Error == null)
				{
					Participant participant = job[i].Result.ToParticipant();
					if (participant.ValidationStatus == ParticipantValidationStatus.NoError)
					{
						this.recipientList[i].InternalUpdateParticipant(participant);
					}
				}
			}
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x0011DF58 File Offset: 0x0011C158
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CoreRecipientCollection>(this);
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x0011DF60 File Offset: 0x0011C160
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.recipientTable != null)
			{
				this.recipientTable.Dispose();
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06004379 RID: 17273 RVA: 0x0011DF80 File Offset: 0x0011C180
		private bool TryFindRecipient(int rowId, out int indexOfInsertionPoint)
		{
			indexOfInsertionPoint = 0;
			int num = Math.Min(rowId, this.recipientList.Count - 1);
			int i;
			for (i = num; i >= 0; i--)
			{
				if (this.recipientList[i].RowId == rowId)
				{
					indexOfInsertionPoint = i;
					return true;
				}
				if (this.recipientList[i].RowId < rowId)
				{
					indexOfInsertionPoint = i + 1;
					return false;
				}
			}
			indexOfInsertionPoint = Math.Max(0, i);
			return false;
		}

		// Token: 0x0600437A RID: 17274 RVA: 0x0011DFEE File Offset: 0x0011C1EE
		private void LookupMandatoryPropertiesIfNeeded()
		{
			this.LoadAdditionalParticipantProperties(CoreRecipientCollection.AdditionalParticipantProperties);
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x0011DFFC File Offset: 0x0011C1FC
		private void AddRecipientFromTable(IList<NativeStorePropertyDefinition> propertyDefinitions, object[] values)
		{
			CoreRecipient item = new CoreRecipient(this.recipientTable, propertyDefinitions, values);
			this.recipientList.Add(item);
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x0011E024 File Offset: 0x0011C224
		internal void ExecuteJob(Participant.Job job, PropertyDefinition[] keyProperties)
		{
			ADSessionSettings adsessionSettings = Participant.BatchBuilder.GetADSessionSettings(this.CoreItem);
			Participant.BatchBuilder.Execute(job, new Participant.BatchBuilder[]
			{
				Participant.BatchBuilder.RequestAllProperties(),
				Participant.BatchBuilder.CopyPropertiesFromInput(),
				Participant.BatchBuilder.GetPropertiesFromAD(null, adsessionSettings, keyProperties)
			});
		}

		// Token: 0x040024B9 RID: 9401
		private static readonly PropertyDefinition[] AdditionalParticipantProperties = new PropertyDefinition[]
		{
			ParticipantSchema.IsDistributionList
		};

		// Token: 0x040024BA RID: 9402
		private readonly IList<CoreRecipient> recipientList = new List<CoreRecipient>();

		// Token: 0x040024BB RID: 9403
		private readonly RecipientTable recipientTable;

		// Token: 0x040024BC RID: 9404
		private readonly ICoreItem coreItem;

		// Token: 0x040024BD RID: 9405
		private int nextRecipientRowId;
	}
}
