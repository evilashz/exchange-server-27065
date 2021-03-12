using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000379 RID: 889
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RecipientBase : IRecipientBase, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x06002734 RID: 10036 RVA: 0x0009D478 File Offset: 0x0009B678
		internal RecipientBase(CoreRecipient coreRecipient)
		{
			this.coreRecipient = coreRecipient;
			this.propertyBag = new RecipientBase.RecipientBasePropertyBag(this.coreRecipient.GetMemoryPropertyBag());
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x0009D49D File Offset: 0x0009B69D
		public RecipientId Id
		{
			get
			{
				return this.coreRecipient.Id;
			}
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x0009D4AA File Offset: 0x0009B6AA
		public bool? IsDistributionList()
		{
			return this.Participant.GetValueAsNullable<bool>(ParticipantSchema.IsDistributionList);
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x0009D4BC File Offset: 0x0009B6BC
		public Participant Participant
		{
			get
			{
				return this.coreRecipient.Participant;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x0009D4C9 File Offset: 0x0009B6C9
		public Schema Schema
		{
			get
			{
				return RecipientSchema.Instance;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x0009D4D0 File Offset: 0x0009B6D0
		public bool IsDirty
		{
			get
			{
				return this.PropertyBag.IsDirty;
			}
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x0009D4DD File Offset: 0x0009B6DD
		public bool IsPropertyDirty(PropertyDefinition propertyDefinition)
		{
			return this.PropertyBag.IsPropertyDirty(propertyDefinition);
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x0009D4EB File Offset: 0x0009B6EB
		public void Load()
		{
			this.Load(null);
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x0009D4F4 File Offset: 0x0009B6F4
		public void Load(ICollection<PropertyDefinition> propertyDefinitions)
		{
			this.PropertyBag.Load(propertyDefinitions);
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x0009D502 File Offset: 0x0009B702
		public Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000D07 RID: 3335
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return this.PropertyBag[propertyDefinition];
			}
			set
			{
				this.PropertyBag[propertyDefinition] = value;
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x0009D526 File Offset: 0x0009B726
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			return this.PropertyBag.GetProperties<PropertyDefinition>(propertyDefinitionArray);
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x0009D534 File Offset: 0x0009B734
		public void SetProperties(ICollection<PropertyDefinition> propertyDefinitionArray, object[] propertyValuesArray)
		{
			if (propertyDefinitionArray == null || propertyValuesArray == null)
			{
				throw new ArgumentException(ServerStrings.PropertyDefinitionsValuesNotMatch);
			}
			if (propertyDefinitionArray.Count != propertyValuesArray.Length)
			{
				throw new ArgumentException(ServerStrings.PropertyDefinitionsValuesNotMatch);
			}
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitionArray)
			{
				this[propertyDefinition] = propertyValuesArray[num++];
			}
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x0009D5B8 File Offset: 0x0009B7B8
		public void Delete(PropertyDefinition propertyDefinition)
		{
			this.PropertyBag.Delete(propertyDefinition);
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x0009D5C6 File Offset: 0x0009B7C6
		public object TryGetProperty(PropertyDefinition property)
		{
			return this.PropertyBag.TryGetProperty(property);
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x0009D5D4 File Offset: 0x0009B7D4
		public T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue)
		{
			return this.PropertyBag.GetValueOrDefault<T>(propertyDefinition, defaultValue);
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x0009D5E3 File Offset: 0x0009B7E3
		public void SetOrDeleteProperty(PropertyDefinition propertyDefinition, object propertyValue)
		{
			this.PropertyBag.SetOrDeleteProperty(propertyDefinition, propertyValue);
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x0009D5F2 File Offset: 0x0009B7F2
		private void SetProperty(PropertyDefinition propertyDefinition, object value)
		{
			this.PropertyBag.SetProperty(propertyDefinition, value);
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x0009D601 File Offset: 0x0009B801
		internal T? GetValueAsNullable<T>(StorePropertyDefinition propertyDefinition) where T : struct
		{
			return this.PropertyBag.GetValueAsNullable<T>(propertyDefinition);
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x0009D610 File Offset: 0x0009B810
		internal T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition)
		{
			return this.GetValueOrDefault<T>(propertyDefinition, default(T));
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x0009D62D File Offset: 0x0009B82D
		internal T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition, T defaultValue)
		{
			return this.PropertyBag.GetValueOrDefault<T>(propertyDefinition, defaultValue);
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x0600274A RID: 10058 RVA: 0x0009D63C File Offset: 0x0009B83C
		// (set) Token: 0x0600274B RID: 10059 RVA: 0x0009D64A File Offset: 0x0009B84A
		internal RecipientFlags RecipientFlags
		{
			get
			{
				return this.GetValueOrDefault<RecipientFlags>(InternalSchema.RecipientFlags, RecipientFlags.Sendable);
			}
			set
			{
				EnumValidator.AssertValid<RecipientFlags>(value);
				this[InternalSchema.RecipientFlags] = (int)value;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x0009D663 File Offset: 0x0009B863
		// (set) Token: 0x0600274D RID: 10061 RVA: 0x0009D670 File Offset: 0x0009B870
		public bool Submitted
		{
			get
			{
				return this.coreRecipient.Submitted;
			}
			set
			{
				this.coreRecipient.Submitted = value;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x0009D67E File Offset: 0x0009B87E
		// (set) Token: 0x0600274F RID: 10063 RVA: 0x0009D68B File Offset: 0x0009B88B
		public RecipientItemType RecipientItemType
		{
			get
			{
				return this.coreRecipient.RecipientItemType;
			}
			set
			{
				this.coreRecipient.RecipientItemType = value;
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x0009D699 File Offset: 0x0009B899
		internal CoreRecipient CoreRecipient
		{
			get
			{
				return this.coreRecipient;
			}
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x0009D6A1 File Offset: 0x0009B8A1
		internal bool HasFlags(RecipientFlags flags)
		{
			return (this.RecipientFlags & flags) == flags;
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x0009D6B0 File Offset: 0x0009B8B0
		public bool? IsGroupMailbox()
		{
			if (!(this.Participant == null))
			{
				return this.Participant.GetValueAsNullable<bool>(ParticipantSchema.IsGroupMailbox);
			}
			return null;
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x0009D6E5 File Offset: 0x0009B8E5
		public string SmtpAddress()
		{
			if (!(this.Participant == null))
			{
				return this.Participant.GetValueOrDefault<string>(ParticipantSchema.SmtpAddress, string.Empty);
			}
			return string.Empty;
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06002754 RID: 10068 RVA: 0x0009D710 File Offset: 0x0009B910
		private PropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x0009D718 File Offset: 0x0009B918
		protected static void SetDefaultRecipientBaseProperties(CoreRecipient coreRecipient)
		{
			coreRecipient.PropertyBag[InternalSchema.RecipientFlags] = 1;
		}

		// Token: 0x0400173F RID: 5951
		private readonly CoreRecipient coreRecipient;

		// Token: 0x04001740 RID: 5952
		private readonly RecipientBase.RecipientBasePropertyBag propertyBag;

		// Token: 0x04001741 RID: 5953
		internal static readonly IList<PropertyDefinition> ImmutableProperties = new ReadOnlyCollection<PropertyDefinition>(new PropertyDefinition[]
		{
			InternalSchema.DisplayName,
			InternalSchema.EmailAddress,
			InternalSchema.AddrType,
			InternalSchema.EntryId
		});

		// Token: 0x0200037B RID: 891
		private sealed class RecipientBasePropertyBag : ProxyPropertyBag
		{
			// Token: 0x06002761 RID: 10081 RVA: 0x0009D7FB File Offset: 0x0009B9FB
			internal RecipientBasePropertyBag(PropertyBag propertyBag) : base(propertyBag)
			{
			}

			// Token: 0x06002762 RID: 10082 RVA: 0x0009D804 File Offset: 0x0009BA04
			protected override void SetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
			{
				this.CheckCanModify(propertyDefinition);
				base.SetValidatedStoreProperty(propertyDefinition, propertyValue);
			}

			// Token: 0x06002763 RID: 10083 RVA: 0x0009D815 File Offset: 0x0009BA15
			protected override void DeleteStoreProperty(StorePropertyDefinition propertyDefinition)
			{
				this.CheckCanModify(propertyDefinition);
				base.DeleteStoreProperty(propertyDefinition);
			}

			// Token: 0x06002764 RID: 10084 RVA: 0x0009D828 File Offset: 0x0009BA28
			private void CheckCanModify(StorePropertyDefinition propertyDefinition)
			{
				if (RecipientBase.ImmutableProperties.Contains(propertyDefinition))
				{
					throw PropertyError.ToException(new PropertyError[]
					{
						new PropertyError(propertyDefinition, PropertyErrorCode.NotSupported)
					});
				}
			}
		}
	}
}
