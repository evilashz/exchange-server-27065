using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200064C RID: 1612
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CoreRecipient : IValidatable
	{
		// Token: 0x060042D6 RID: 17110 RVA: 0x0011C5F0 File Offset: 0x0011A7F0
		internal CoreRecipient(RecipientTable recipientTable, int rowId, CoreRecipient.SetDefaultPropertiesDelegate setDefaultPropertiesDelegate, Participant participant) : this(recipientTable)
		{
			setDefaultPropertiesDelegate(this);
			this.Participant = participant;
			this.PropertyBag[InternalSchema.RecipientBaseParticipant] = participant;
			this.propertyBag[InternalSchema.RowId] = rowId;
			this.EndInitialization();
			this.OnAddRecipient();
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x0011C647 File Offset: 0x0011A847
		internal CoreRecipient(RecipientTable recipientTable, IList<NativeStorePropertyDefinition> propertyDefinitions, object[] propValues) : this(recipientTable)
		{
			this.propertyBag.PreLoadStoreProperty<NativeStorePropertyDefinition>(propertyDefinitions, propValues);
			this.Participant = this.PropertyBag.GetValueOrDefault<Participant>(InternalSchema.RecipientBaseParticipant);
			this.EndInitialization();
			this.propertyBag.ClearChangeInfo();
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x0011C684 File Offset: 0x0011A884
		internal CoreRecipient(RecipientTable recipientTable, int rowId) : this(recipientTable)
		{
			this.propertyBag[InternalSchema.RowId] = rowId;
			this.Participant = this.PropertyBag.GetValueOrDefault<Participant>(InternalSchema.RecipientBaseParticipant);
			this.EndInitialization();
			this.OnAddRecipient();
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x0011C6D0 File Offset: 0x0011A8D0
		internal CoreRecipient(CoreRecipient sourceCoreRecipient, int rowId, IRecipientChangeTracker destinationRecipientChangeTracker, ExTimeZone destinationTimeZone)
		{
			this.participant = sourceCoreRecipient.participant;
			this.recipientChangeTracker = destinationRecipientChangeTracker;
			this.propertyBag = new CoreRecipient.CoreRecipientPropertyBag(sourceCoreRecipient.propertyBag, this);
			this.propertyBag.ExTimeZone = destinationTimeZone;
			this.propertyBag[InternalSchema.RowId] = rowId;
			this.EndInitialization();
			this.OnAddRecipient();
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x0011C737 File Offset: 0x0011A937
		private CoreRecipient(RecipientTable recipientTable)
		{
			this.recipientChangeTracker = recipientTable.RecipientChangeTracker;
			this.propertyBag = new CoreRecipient.CoreRecipientPropertyBag(this);
			this.propertyBag.ExTimeZone = recipientTable.ExTimeZone;
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x060042DB RID: 17115 RVA: 0x0011C768 File Offset: 0x0011A968
		public ICorePropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x060042DC RID: 17116 RVA: 0x0011C770 File Offset: 0x0011A970
		public int RowId
		{
			get
			{
				return this.PropertyBag.GetValueOrDefault<int>(InternalSchema.RowId, -1);
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x060042DD RID: 17117 RVA: 0x0011C783 File Offset: 0x0011A983
		bool IValidatable.ValidateAllProperties
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x060042DE RID: 17118 RVA: 0x0011C786 File Offset: 0x0011A986
		Schema IValidatable.Schema
		{
			get
			{
				return RecipientSchema.Instance;
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x0011C78D File Offset: 0x0011A98D
		internal RecipientId Id
		{
			get
			{
				if (this.id == null)
				{
					this.id = new RecipientId(CoreRecipient.unicodeEncoding.GetBytes(this.GetStringId()));
				}
				return this.id;
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x060042E0 RID: 17120 RVA: 0x0011C7B8 File Offset: 0x0011A9B8
		// (set) Token: 0x060042E1 RID: 17121 RVA: 0x0011C7C0 File Offset: 0x0011A9C0
		internal Participant Participant
		{
			get
			{
				return this.participant;
			}
			private set
			{
				this.participant = value;
			}
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x060042E2 RID: 17122 RVA: 0x0011C7C9 File Offset: 0x0011A9C9
		// (set) Token: 0x060042E3 RID: 17123 RVA: 0x0011C7D6 File Offset: 0x0011A9D6
		internal RecipientItemType RecipientItemType
		{
			get
			{
				return MapiUtil.MapiRecipientTypeToRecipientItemType(this.MapiRecipientType);
			}
			set
			{
				EnumValidator.ThrowIfInvalid<RecipientItemType>(value, "value");
				this.MapiRecipientType = MapiUtil.RecipientItemTypeToMapiRecipientType(value, this.Submitted);
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x060042E4 RID: 17124 RVA: 0x0011C7F8 File Offset: 0x0011A9F8
		// (set) Token: 0x060042E5 RID: 17125 RVA: 0x0011C824 File Offset: 0x0011AA24
		internal bool Submitted
		{
			get
			{
				RecipientType mapiRecipientType = this.MapiRecipientType;
				return mapiRecipientType != RecipientType.Unknown && (this.MapiRecipientType & RecipientType.Submitted) != RecipientType.Orig;
			}
			set
			{
				if (value)
				{
					this.MapiRecipientType |= RecipientType.Submitted;
					return;
				}
				this.MapiRecipientType &= (RecipientType)2147483647;
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x060042E6 RID: 17126 RVA: 0x0011C850 File Offset: 0x0011AA50
		// (set) Token: 0x060042E7 RID: 17127 RVA: 0x0011C8A1 File Offset: 0x0011AAA1
		private RecipientType MapiRecipientType
		{
			get
			{
				int? valueAsNullable = this.PropertyBag.GetValueAsNullable<int>(InternalSchema.RecipientType);
				RecipientType? recipientType = (valueAsNullable != null) ? new RecipientType?((RecipientType)valueAsNullable.GetValueOrDefault()) : null;
				if (recipientType == null)
				{
					return RecipientType.Unknown;
				}
				return recipientType.GetValueOrDefault();
			}
			set
			{
				this.PropertyBag[InternalSchema.RecipientType] = (int)value;
			}
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x0011C8B9 File Offset: 0x0011AAB9
		public bool TryValidateRecipient()
		{
			if (this.Participant != null)
			{
				throw new InvalidOperationException("Cannot update a participant that has already been initialized.");
			}
			this.Participant = this.PropertyBag.GetValueOrDefault<Participant>(InternalSchema.RecipientBaseParticipant);
			return this.Participant != null;
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x0011C8F6 File Offset: 0x0011AAF6
		void IValidatable.Validate(ValidationContext context, IList<StoreObjectValidationError> validationErrors)
		{
			Validation.ValidateProperties(context, this, this.propertyBag, validationErrors);
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x0011C906 File Offset: 0x0011AB06
		internal MemoryPropertyBag GetMemoryPropertyBag()
		{
			return this.propertyBag;
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x0011C90E File Offset: 0x0011AB0E
		internal void SetUnchanged()
		{
			this.recipientState = CoreRecipient.RecipientState.Unchanged;
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x0011C917 File Offset: 0x0011AB17
		internal void InternalUpdateParticipant(Participant newParticipant)
		{
			this.CheckCanUpdateParticipant(newParticipant);
			this.Participant = newParticipant;
			this.PropertyBag[InternalSchema.RecipientBaseParticipant] = this.Participant;
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x0011C940 File Offset: 0x0011AB40
		internal void SetRowId(int rowId)
		{
			CoreRecipient.RecipientState recipientState = this.recipientState;
			this.recipientState = CoreRecipient.RecipientState.Uninitialized;
			try
			{
				this.PropertyBag.SetProperty(InternalSchema.RowId, rowId);
			}
			finally
			{
				this.recipientState = recipientState;
			}
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x0011C98C File Offset: 0x0011AB8C
		internal void GetCharsetDetectionData(StringBuilder stringBuilder)
		{
			foreach (StorePropertyDefinition propertyDefinition in RecipientSchema.Instance.DetectCodepageProperties)
			{
				string text = this.PropertyBag.TryGetProperty(propertyDefinition) as string;
				if (text != null)
				{
					stringBuilder.AppendLine(text);
				}
			}
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x0011C9F4 File Offset: 0x0011ABF4
		internal void OnRemoveRecipient()
		{
			switch (this.recipientState)
			{
			case CoreRecipient.RecipientState.Unchanged:
				this.recipientChangeTracker.RemoveUnchangedRecipient(this);
				break;
			case CoreRecipient.RecipientState.Added:
				this.recipientChangeTracker.RemoveAddedRecipient(this);
				break;
			case CoreRecipient.RecipientState.Modified:
				this.recipientChangeTracker.RemoveModifiedRecipient(this);
				break;
			}
			this.recipientState = CoreRecipient.RecipientState.Removed;
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x0011CA54 File Offset: 0x0011AC54
		private void OnAddRecipient()
		{
			this.recipientState = CoreRecipient.RecipientState.Added;
			bool flag = false;
			this.recipientChangeTracker.AddRecipient(this, out flag);
			if (flag)
			{
				this.recipientState = CoreRecipient.RecipientState.Modified;
			}
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x0011CA84 File Offset: 0x0011AC84
		private void OnModifyRecipient()
		{
			switch (this.recipientState)
			{
			case CoreRecipient.RecipientState.Uninitialized:
			case CoreRecipient.RecipientState.Added:
			case CoreRecipient.RecipientState.Modified:
				break;
			case CoreRecipient.RecipientState.Unchanged:
				this.recipientChangeTracker.OnModifyRecipient(this);
				this.recipientState = CoreRecipient.RecipientState.Modified;
				break;
			case CoreRecipient.RecipientState.Removed:
				throw new InvalidOperationException(ServerStrings.ExCannotModifyRemovedRecipient);
			default:
				return;
			}
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x0011CAD6 File Offset: 0x0011ACD6
		private void EndInitialization()
		{
			this.recipientState = CoreRecipient.RecipientState.Unchanged;
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x0011CAE0 File Offset: 0x0011ACE0
		private string GetStringId()
		{
			return string.Concat(new object[]
			{
				this.MapiRecipientType,
				":",
				this.Participant.RoutingType,
				":",
				this.Participant.EmailAddress
			});
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x0011CB34 File Offset: 0x0011AD34
		private void CheckCanUpdateParticipant(Participant newParticipant)
		{
			if (!newParticipant.AreAddressesEqual(this.Participant))
			{
				throw new InvalidOperationException("Participant on a RecipientBase can be updated only to the one with the same address");
			}
		}

		// Token: 0x04002494 RID: 9364
		internal const int InvalidRowId = -1;

		// Token: 0x04002495 RID: 9365
		private static readonly UnicodeEncoding unicodeEncoding = new UnicodeEncoding();

		// Token: 0x04002496 RID: 9366
		private readonly CoreRecipient.CoreRecipientPropertyBag propertyBag;

		// Token: 0x04002497 RID: 9367
		private readonly IRecipientChangeTracker recipientChangeTracker;

		// Token: 0x04002498 RID: 9368
		private RecipientId id;

		// Token: 0x04002499 RID: 9369
		private Participant participant;

		// Token: 0x0400249A RID: 9370
		private CoreRecipient.RecipientState recipientState;

		// Token: 0x0200064D RID: 1613
		internal enum RecipientState : byte
		{
			// Token: 0x0400249C RID: 9372
			Uninitialized,
			// Token: 0x0400249D RID: 9373
			Unchanged,
			// Token: 0x0400249E RID: 9374
			Added,
			// Token: 0x0400249F RID: 9375
			Modified,
			// Token: 0x040024A0 RID: 9376
			Removed
		}

		// Token: 0x0200064E RID: 1614
		// (Invoke) Token: 0x060042F7 RID: 17143
		internal delegate void SetDefaultPropertiesDelegate(CoreRecipient coreRecipient);

		// Token: 0x02000654 RID: 1620
		internal class CoreRecipientPropertyBag : MemoryPropertyBag, ICorePropertyBag, ILocationIdentifierSetter
		{
			// Token: 0x06004356 RID: 17238 RVA: 0x0011D8ED File Offset: 0x0011BAED
			internal CoreRecipientPropertyBag(CoreRecipient coreRecipient)
			{
				this.coreRecipient = coreRecipient;
				base.SetAllPropertiesLoaded();
			}

			// Token: 0x06004357 RID: 17239 RVA: 0x0011D902 File Offset: 0x0011BB02
			internal CoreRecipientPropertyBag(CoreRecipient.CoreRecipientPropertyBag propertyBag, CoreRecipient coreRecipient) : base(propertyBag)
			{
				this.coreRecipient = coreRecipient;
			}

			// Token: 0x06004358 RID: 17240 RVA: 0x0011D914 File Offset: 0x0011BB14
			T ICorePropertyBag.GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition)
			{
				return base.GetValueOrDefault<T>(propertyDefinition, default(T));
			}

			// Token: 0x06004359 RID: 17241 RVA: 0x0011D931 File Offset: 0x0011BB31
			T ICorePropertyBag.GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition, T defaultValue)
			{
				return base.GetValueOrDefault<T>(propertyDefinition, defaultValue);
			}

			// Token: 0x0600435A RID: 17242 RVA: 0x0011D93B File Offset: 0x0011BB3B
			T? ICorePropertyBag.GetValueAsNullable<T>(StorePropertyDefinition propertyDefinition)
			{
				return base.GetValueAsNullable<T>(propertyDefinition);
			}

			// Token: 0x0600435B RID: 17243 RVA: 0x0011D944 File Offset: 0x0011BB44
			public Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
			{
				throw new NotSupportedException("Streams are not supported on Recipients.");
			}

			// Token: 0x0600435C RID: 17244 RVA: 0x0011D950 File Offset: 0x0011BB50
			public void Reload()
			{
				throw new NotSupportedException("Reload is not supported.");
			}

			// Token: 0x0600435D RID: 17245 RVA: 0x0011D95C File Offset: 0x0011BB5C
			protected override void SetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
			{
				this.CheckCanModify(propertyDefinition);
				base.SetValidatedStoreProperty(propertyDefinition, propertyValue);
				this.coreRecipient.OnModifyRecipient();
			}

			// Token: 0x0600435E RID: 17246 RVA: 0x0011D978 File Offset: 0x0011BB78
			protected override void DeleteStoreProperty(StorePropertyDefinition propertyDefinition)
			{
				this.CheckCanModify(propertyDefinition);
				base.DeleteStoreProperty(propertyDefinition);
				this.coreRecipient.OnModifyRecipient();
			}

			// Token: 0x0600435F RID: 17247 RVA: 0x0011D994 File Offset: 0x0011BB94
			private void CheckCanModify(StorePropertyDefinition propertyDefinition)
			{
				if (!(propertyDefinition is PropertyTagPropertyDefinition) || (this.coreRecipient.recipientState != CoreRecipient.RecipientState.Uninitialized && propertyDefinition == InternalSchema.RowId))
				{
					throw PropertyError.ToException(new PropertyError[]
					{
						new PropertyError(propertyDefinition, PropertyErrorCode.NotSupported)
					});
				}
			}

			// Token: 0x040024B8 RID: 9400
			private readonly CoreRecipient coreRecipient;
		}
	}
}
