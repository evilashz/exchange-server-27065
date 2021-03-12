using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005B3 RID: 1459
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConversionItemParticipants : IConversionParticipantList
	{
		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x000F6955 File Offset: 0x000F4B55
		internal static HashSet<NativeStorePropertyDefinition> AllCacheProperties
		{
			get
			{
				return ConversionItemParticipants.cacheProperties;
			}
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x000F695C File Offset: 0x000F4B5C
		private static HashSet<NativeStorePropertyDefinition> GetCacheProperties(bool skipSkippable)
		{
			HashSet<NativeStorePropertyDefinition> hashSet = new HashSet<NativeStorePropertyDefinition>();
			foreach (ConversionItemParticipants.ParticipantDefinitionEntry participantDefinitionEntry in ConversionItemParticipants.ParticipantEntries)
			{
				if (!skipSkippable || !participantDefinitionEntry.IsSkippable)
				{
					EmbeddedParticipantProperty participantProperty = participantDefinitionEntry.ParticipantProperty;
					hashSet.Add(participantProperty.DisplayNamePropertyDefinition);
					hashSet.Add(participantProperty.EmailAddressPropertyDefinition);
					hashSet.Add(participantProperty.RoutingTypePropertyDefinition);
					hashSet.Add(participantProperty.EntryIdPropertyDefinition);
					if (participantProperty.SmtpAddressPropertyDefinition != null)
					{
						hashSet.Add(participantProperty.SmtpAddressPropertyDefinition);
					}
					if (participantProperty.SipUriPropertyDefinition != null)
					{
						hashSet.Add(participantProperty.SipUriPropertyDefinition);
					}
					if (participantProperty.SidPropertyDefinition != null)
					{
						hashSet.Add(participantProperty.SidPropertyDefinition);
					}
					if (participantProperty.GuidPropertyDefinition != null)
					{
						hashSet.Add(participantProperty.GuidPropertyDefinition);
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x000F6A30 File Offset: 0x000F4C30
		internal static bool IsAnyCacheProperty(NativeStorePropertyDefinition propertyDefinition)
		{
			return ConversionItemParticipants.AllCacheProperties.Contains(propertyDefinition);
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x000F6A3D File Offset: 0x000F4C3D
		internal static EmbeddedParticipantProperty GetEmbeddedParticipantProperty(ConversionItemParticipants.ParticipantIndex index)
		{
			return ConversionItemParticipants.ParticipantEntries[(int)index].ParticipantProperty;
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06003BFB RID: 15355 RVA: 0x000F6A4B File Offset: 0x000F4C4B
		internal HashSet<NativeStorePropertyDefinition> CacheProperties
		{
			get
			{
				if (!this.skipSkippable)
				{
					return ConversionItemParticipants.cacheProperties;
				}
				return ConversionItemParticipants.cachePropertiesSkipNonMime;
			}
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x000F6A60 File Offset: 0x000F4C60
		internal bool IsCacheProperty(NativeStorePropertyDefinition propertyDefinition)
		{
			return this.CacheProperties.Contains(propertyDefinition);
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x000F6A6E File Offset: 0x000F4C6E
		internal ConversionItemParticipants(MemoryPropertyBag propertyBag, bool skipSkippable)
		{
			this.propertyBag = propertyBag;
			this.skipSkippable = skipSkippable;
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x000F6A84 File Offset: 0x000F4C84
		internal object TryGetProperty(NativeStorePropertyDefinition property, object value)
		{
			return this.propertyBag[property];
		}

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06003BFF RID: 15359 RVA: 0x000F6A92 File Offset: 0x000F4C92
		public int Count
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x000F6A96 File Offset: 0x000F4C96
		public bool IsConversionParticipantAlwaysResolvable(int index)
		{
			return ConversionItemParticipants.ParticipantEntries[index].IsAlwaysResolvable;
		}

		// Token: 0x1700124E RID: 4686
		public Participant this[int index]
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<Participant>(ConversionItemParticipants.ParticipantEntries[index].ParticipantProperty);
			}
			set
			{
				this.propertyBag.SetOrDeleteProperty(ConversionItemParticipants.ParticipantEntries[index].ParticipantProperty, value);
			}
		}

		// Token: 0x1700124F RID: 4687
		internal Participant this[ConversionItemParticipants.ParticipantIndex index]
		{
			get
			{
				return this[(int)index];
			}
			set
			{
				this[(int)index] = value;
			}
		}

		// Token: 0x04001FD7 RID: 8151
		internal static readonly ConversionItemParticipants.ParticipantDefinitionEntry[] ParticipantEntries = new ConversionItemParticipants.ParticipantDefinitionEntry[]
		{
			new ConversionItemParticipants.ParticipantDefinitionEntry(InternalSchema.ReceivedBy, ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsAlwaysResolvable | ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsSkippable),
			new ConversionItemParticipants.ParticipantDefinitionEntry(InternalSchema.ReceivedRepresenting, ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsAlwaysResolvable | ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsSkippable),
			new ConversionItemParticipants.ParticipantDefinitionEntry(InternalSchema.From, ConversionItemParticipants.ParticipantDefinitionEntryFlags.None),
			new ConversionItemParticipants.ParticipantDefinitionEntry(InternalSchema.Sender, ConversionItemParticipants.ParticipantDefinitionEntryFlags.None),
			new ConversionItemParticipants.ParticipantDefinitionEntry(InternalSchema.OriginalFrom, ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsAlwaysResolvable),
			new ConversionItemParticipants.ParticipantDefinitionEntry(InternalSchema.OriginalSender, ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsAlwaysResolvable),
			new ConversionItemParticipants.ParticipantDefinitionEntry(InternalSchema.OriginalAuthor, ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsAlwaysResolvable),
			new ConversionItemParticipants.ParticipantDefinitionEntry(InternalSchema.ReadReceiptAddressee, ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsAlwaysResolvable),
			new ConversionItemParticipants.ParticipantDefinitionEntry(InternalSchema.ContactEmail1, ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsAlwaysResolvable),
			new ConversionItemParticipants.ParticipantDefinitionEntry(InternalSchema.ContactEmail2, ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsAlwaysResolvable),
			new ConversionItemParticipants.ParticipantDefinitionEntry(InternalSchema.ContactEmail3, ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsAlwaysResolvable)
		};

		// Token: 0x04001FD8 RID: 8152
		private static HashSet<NativeStorePropertyDefinition> cacheProperties = ConversionItemParticipants.GetCacheProperties(false);

		// Token: 0x04001FD9 RID: 8153
		private static HashSet<NativeStorePropertyDefinition> cachePropertiesSkipNonMime = ConversionItemParticipants.GetCacheProperties(true);

		// Token: 0x04001FDA RID: 8154
		protected readonly bool skipSkippable;

		// Token: 0x04001FDB RID: 8155
		protected MemoryPropertyBag propertyBag;

		// Token: 0x020005B4 RID: 1460
		internal enum ParticipantIndex
		{
			// Token: 0x04001FDD RID: 8157
			ReceivedBy,
			// Token: 0x04001FDE RID: 8158
			ReceivedRepresenting,
			// Token: 0x04001FDF RID: 8159
			From,
			// Token: 0x04001FE0 RID: 8160
			Sender,
			// Token: 0x04001FE1 RID: 8161
			OriginalFrom,
			// Token: 0x04001FE2 RID: 8162
			OriginalSender,
			// Token: 0x04001FE3 RID: 8163
			OriginalAuthor,
			// Token: 0x04001FE4 RID: 8164
			ReadReceipt,
			// Token: 0x04001FE5 RID: 8165
			ContactEmail1,
			// Token: 0x04001FE6 RID: 8166
			ContactEmail2,
			// Token: 0x04001FE7 RID: 8167
			ContactEmail3,
			// Token: 0x04001FE8 RID: 8168
			TotalItemParticipants
		}

		// Token: 0x020005B5 RID: 1461
		[Flags]
		internal enum ParticipantDefinitionEntryFlags
		{
			// Token: 0x04001FEA RID: 8170
			None = 0,
			// Token: 0x04001FEB RID: 8171
			IsAlwaysResolvable = 1,
			// Token: 0x04001FEC RID: 8172
			IsSkippable = 2
		}

		// Token: 0x020005B6 RID: 1462
		internal class ParticipantDefinitionEntry
		{
			// Token: 0x06003C06 RID: 15366 RVA: 0x000F6BB9 File Offset: 0x000F4DB9
			internal ParticipantDefinitionEntry(EmbeddedParticipantProperty participantDefinition, ConversionItemParticipants.ParticipantDefinitionEntryFlags flags)
			{
				this.participantProperty = participantDefinition;
				this.flags = flags;
			}

			// Token: 0x17001250 RID: 4688
			// (get) Token: 0x06003C07 RID: 15367 RVA: 0x000F6BCF File Offset: 0x000F4DCF
			internal EmbeddedParticipantProperty ParticipantProperty
			{
				get
				{
					return this.participantProperty;
				}
			}

			// Token: 0x17001251 RID: 4689
			// (get) Token: 0x06003C08 RID: 15368 RVA: 0x000F6BD7 File Offset: 0x000F4DD7
			internal bool IsAlwaysResolvable
			{
				get
				{
					return (this.flags & ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsAlwaysResolvable) == ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsAlwaysResolvable;
				}
			}

			// Token: 0x17001252 RID: 4690
			// (get) Token: 0x06003C09 RID: 15369 RVA: 0x000F6BE4 File Offset: 0x000F4DE4
			internal bool IsSkippable
			{
				get
				{
					return (this.flags & ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsSkippable) == ConversionItemParticipants.ParticipantDefinitionEntryFlags.IsSkippable;
				}
			}

			// Token: 0x04001FED RID: 8173
			private EmbeddedParticipantProperty participantProperty;

			// Token: 0x04001FEE RID: 8174
			private ConversionItemParticipants.ParticipantDefinitionEntryFlags flags;
		}
	}
}
