using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008AB RID: 2219
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ConversationPropertyAggregationStrategy
	{
		// Token: 0x060052E4 RID: 21220 RVA: 0x0015A31F File Offset: 0x0015851F
		public static PropertyAggregationStrategy CreatePriorityPropertyAggregation(StorePropertyDefinition sourceProperty)
		{
			return new PropertyAggregationStrategy.SingleValuePropertyAggregation(ItemSelectionStrategy.CreateSingleSourceProperty(sourceProperty));
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x0015A32C File Offset: 0x0015852C
		public static PropertyAggregationStrategy CreateAnyTruePropertyAggregation(StorePropertyDefinition sourceProperty)
		{
			return new ConversationPropertyAggregationStrategy.AnyTruePropertyAggregation(new StorePropertyDefinition[]
			{
				sourceProperty
			});
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x0015A34A File Offset: 0x0015854A
		public static PropertyAggregationStrategy CreateZeroValuePropertyAggregation()
		{
			return new ConversationPropertyAggregationStrategy.ZeroValueAggregation(Array<StorePropertyDefinition>.Empty);
		}

		// Token: 0x04002D38 RID: 11576
		public static readonly PropertyAggregationStrategy CreationTimeProperty = new PropertyAggregationStrategy.CreationTimeAggregation();

		// Token: 0x04002D39 RID: 11577
		public static readonly PropertyAggregationStrategy ConversationIdProperty = new ConversationPropertyAggregationStrategy.ConversationIdAggregation();

		// Token: 0x04002D3A RID: 11578
		public static readonly PropertyAggregationStrategy ConversationTopicProperty = ConversationPropertyAggregationStrategy.CreatePriorityPropertyAggregation(ItemSchema.NormalizedSubject);

		// Token: 0x04002D3B RID: 11579
		public static readonly PropertyAggregationStrategy InstanceKeyProperty = ConversationPropertyAggregationStrategy.CreatePriorityPropertyAggregation(ItemSchema.InstanceKey);

		// Token: 0x04002D3C RID: 11580
		public static readonly PropertyAggregationStrategy PreviewProperty = ConversationPropertyAggregationStrategy.CreatePriorityPropertyAggregation(ItemSchema.Preview);

		// Token: 0x04002D3D RID: 11581
		public static readonly PropertyAggregationStrategy ItemCountProperty = new ConversationPropertyAggregationStrategy.ItemCountAggregation();

		// Token: 0x04002D3E RID: 11582
		public static readonly PropertyAggregationStrategy SizeProperty = new ConversationPropertyAggregationStrategy.ConversationSizeAggregation();

		// Token: 0x04002D3F RID: 11583
		public static readonly PropertyAggregationStrategy LastDeliveryTimeProperty = new ConversationPropertyAggregationStrategy.LastDeliveryTimeAggregation();

		// Token: 0x04002D40 RID: 11584
		public static readonly PropertyAggregationStrategy ImportanceProperty = new ConversationPropertyAggregationStrategy.ImportancePropertyAggregation();

		// Token: 0x04002D41 RID: 11585
		public static readonly PropertyAggregationStrategy TotalItemLikesProperty = ConversationPropertyAggregationStrategy.CreateZeroValuePropertyAggregation();

		// Token: 0x04002D42 RID: 11586
		public static readonly PropertyAggregationStrategy ConversationLikesProperty = ConversationPropertyAggregationStrategy.CreateZeroValuePropertyAggregation();

		// Token: 0x04002D43 RID: 11587
		public static readonly PropertyAggregationStrategy DirectParticipantsProperty = new ConversationPropertyAggregationStrategy.DirectParticipantsAggregation();

		// Token: 0x04002D44 RID: 11588
		public static readonly PropertyAggregationStrategy HasAttachmentsProperty = ConversationPropertyAggregationStrategy.CreateAnyTruePropertyAggregation(ItemSchema.HasAttachment);

		// Token: 0x04002D45 RID: 11589
		public static readonly PropertyAggregationStrategy HasIrmProperty = new ConversationPropertyAggregationStrategy.HasIrmAggregation();

		// Token: 0x04002D46 RID: 11590
		public static readonly PropertyAggregationStrategy DraftItemIdsProperty = new ConversationPropertyAggregationStrategy.DraftItemIdsAggregation();

		// Token: 0x04002D47 RID: 11591
		public static readonly PropertyAggregationStrategy IconIndexProperty = new ConversationPropertyAggregationStrategy.IconIndexAggregation();

		// Token: 0x04002D48 RID: 11592
		public static readonly PropertyAggregationStrategy UnreadCountProperty = new ConversationPropertyAggregationStrategy.UnreadCountAggregation();

		// Token: 0x04002D49 RID: 11593
		public static readonly PropertyAggregationStrategy FlagStatusProperty = new ConversationPropertyAggregationStrategy.FlagStatusAggregation();

		// Token: 0x04002D4A RID: 11594
		public static readonly PropertyAggregationStrategy RichContentProperty = new ConversationPropertyAggregationStrategy.RichContentAggregation();

		// Token: 0x020008AC RID: 2220
		private sealed class ZeroValueAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060052E8 RID: 21224 RVA: 0x0015A437 File Offset: 0x00158637
			public ZeroValueAggregation(StorePropertyDefinition[] dependencies) : base(dependencies)
			{
			}

			// Token: 0x060052E9 RID: 21225 RVA: 0x0015A440 File Offset: 0x00158640
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				value = 0;
				return true;
			}
		}

		// Token: 0x020008AD RID: 2221
		private sealed class ItemCountAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060052EA RID: 21226 RVA: 0x0015A44B File Offset: 0x0015864B
			public ItemCountAggregation() : base(new StorePropertyDefinition[0])
			{
			}

			// Token: 0x060052EB RID: 21227 RVA: 0x0015A459 File Offset: 0x00158659
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				value = context.Sources.Count;
				return true;
			}
		}

		// Token: 0x020008AE RID: 2222
		private sealed class DirectParticipantsAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060052EC RID: 21228 RVA: 0x0015A470 File Offset: 0x00158670
			public DirectParticipantsAggregation() : base(new StorePropertyDefinition[]
			{
				ItemSchema.From,
				ItemSchema.Sender
			})
			{
			}

			// Token: 0x060052ED RID: 21229 RVA: 0x0015A49C File Offset: 0x0015869C
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				Dictionary<string, Participant> dictionary = new Dictionary<string, Participant>();
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					Participant valueOrDefault = storePropertyBag.GetValueOrDefault<Participant>(ItemSchema.From, null);
					if (valueOrDefault == null)
					{
						valueOrDefault = storePropertyBag.GetValueOrDefault<Participant>(ItemSchema.Sender, null);
					}
					if (!(valueOrDefault == null))
					{
						string text = string.IsNullOrEmpty(valueOrDefault.DisplayName) ? valueOrDefault.ToString(AddressFormat.Rfc822Smtp) : valueOrDefault.DisplayName;
						if (!string.IsNullOrEmpty(text) && !dictionary.ContainsKey(text))
						{
							dictionary.Add(text, valueOrDefault);
						}
					}
				}
				if (dictionary.Count == 0)
				{
					value = null;
					return false;
				}
				value = dictionary.Values.ToArray<Participant>();
				return true;
			}
		}

		// Token: 0x020008AF RID: 2223
		private class AnyTruePropertyAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060052EE RID: 21230 RVA: 0x0015A56C File Offset: 0x0015876C
			public AnyTruePropertyAggregation(params StorePropertyDefinition[] dependencies) : base(dependencies)
			{
			}

			// Token: 0x060052EF RID: 21231 RVA: 0x0015A578 File Offset: 0x00158778
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				bool flag = false;
				foreach (IStorePropertyBag source in context.Sources)
				{
					if (this.GetBoolValue(source))
					{
						flag = true;
						break;
					}
				}
				value = flag;
				return true;
			}

			// Token: 0x060052F0 RID: 21232 RVA: 0x0015A5D8 File Offset: 0x001587D8
			public virtual bool GetBoolValue(IStorePropertyBag source)
			{
				return source.GetValueOrDefault<bool>(base.Dependencies[0].Property, false);
			}
		}

		// Token: 0x020008B0 RID: 2224
		private sealed class HasIrmAggregation : ConversationPropertyAggregationStrategy.AnyTruePropertyAggregation
		{
			// Token: 0x060052F1 RID: 21233 RVA: 0x0015A5F0 File Offset: 0x001587F0
			public HasIrmAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.Flags
			})
			{
			}

			// Token: 0x060052F2 RID: 21234 RVA: 0x0015A614 File Offset: 0x00158814
			public override bool GetBoolValue(IStorePropertyBag source)
			{
				MessageFlags valueOrDefault = source.GetValueOrDefault<MessageFlags>(base.Dependencies[0].Property, MessageFlags.None);
				return (MessageFlags.IsRestricted & valueOrDefault) == MessageFlags.IsRestricted;
			}
		}

		// Token: 0x020008B1 RID: 2225
		private sealed class ConversationIdAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060052F3 RID: 21235 RVA: 0x0015A644 File Offset: 0x00158844
			public ConversationIdAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.ConversationId,
				InternalSchema.ItemClass
			})
			{
			}

			// Token: 0x060052F4 RID: 21236 RVA: 0x0015A670 File Offset: 0x00158870
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				ConversationId conversationId = null;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					ConversationId valueOrDefault = storePropertyBag.GetValueOrDefault<ConversationId>(InternalSchema.ConversationId, null);
					if (conversationId != null && valueOrDefault != null && !valueOrDefault.Equals(conversationId))
					{
						throw new ArgumentException("sources", "Property bag collection should have same conversationId");
					}
					if (conversationId == null)
					{
						conversationId = valueOrDefault;
					}
				}
				value = conversationId;
				return true;
			}
		}

		// Token: 0x020008B2 RID: 2226
		private sealed class ConversationSizeAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060052F5 RID: 21237 RVA: 0x0015A6F0 File Offset: 0x001588F0
			public ConversationSizeAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.Size
			})
			{
			}

			// Token: 0x060052F6 RID: 21238 RVA: 0x0015A714 File Offset: 0x00158914
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				int num = 0;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					int valueOrDefault = storePropertyBag.GetValueOrDefault<int>(InternalSchema.Size, 0);
					num += valueOrDefault;
				}
				value = num;
				return true;
			}
		}

		// Token: 0x020008B3 RID: 2227
		internal sealed class LastDeliveryTimeAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060052F7 RID: 21239 RVA: 0x0015A778 File Offset: 0x00158978
			public LastDeliveryTimeAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.ReceivedTime
			})
			{
			}

			// Token: 0x060052F8 RID: 21240 RVA: 0x0015A79C File Offset: 0x0015899C
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				ExDateTime exDateTime = ExDateTime.MinValue;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					ExDateTime valueOrDefault = storePropertyBag.GetValueOrDefault<ExDateTime>(InternalSchema.ReceivedTime, ExDateTime.MinValue);
					if (valueOrDefault > exDateTime)
					{
						exDateTime = valueOrDefault;
					}
				}
				value = exDateTime;
				return true;
			}
		}

		// Token: 0x020008B4 RID: 2228
		private sealed class ImportancePropertyAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060052F9 RID: 21241 RVA: 0x0015A810 File Offset: 0x00158A10
			public ImportancePropertyAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.Importance
			})
			{
			}

			// Token: 0x060052FA RID: 21242 RVA: 0x0015A834 File Offset: 0x00158A34
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				Importance importance = Importance.Low;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					Importance valueOrDefault = storePropertyBag.GetValueOrDefault<Importance>(InternalSchema.Importance, Importance.Normal);
					if (valueOrDefault > importance)
					{
						importance = valueOrDefault;
						if (importance == Importance.High)
						{
							break;
						}
					}
				}
				value = importance;
				return true;
			}
		}

		// Token: 0x020008B5 RID: 2229
		private sealed class DraftItemIdsAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060052FB RID: 21243 RVA: 0x0015A89C File Offset: 0x00158A9C
			public DraftItemIdsAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.IsDraft
			})
			{
			}

			// Token: 0x060052FC RID: 21244 RVA: 0x0015A8C0 File Offset: 0x00158AC0
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				List<StoreObjectId> list = new List<StoreObjectId>(context.Sources.Count);
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					if (storePropertyBag.GetValueOrDefault<bool>(InternalSchema.IsDraft, false))
					{
						byte[] valueOrDefault = storePropertyBag.GetValueOrDefault<byte[]>(InternalSchema.EntryId, null);
						if (valueOrDefault != null)
						{
							StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(valueOrDefault, StoreObjectType.Unknown);
							if (storeObjectId != null)
							{
								list.Add(storeObjectId);
							}
						}
					}
				}
				value = list.ToArray();
				return true;
			}
		}

		// Token: 0x020008B6 RID: 2230
		private sealed class IconIndexAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060052FD RID: 21245 RVA: 0x0015A958 File Offset: 0x00158B58
			public IconIndexAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.IconIndex
			})
			{
			}

			// Token: 0x060052FE RID: 21246 RVA: 0x0015A97C File Offset: 0x00158B7C
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				IconIndex iconIndex = IconIndex.Default;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					IconIndex valueOrDefault = storePropertyBag.GetValueOrDefault<IconIndex>(InternalSchema.IconIndex, IconIndex.Default);
					if (valueOrDefault != IconIndex.Default && valueOrDefault != iconIndex)
					{
						iconIndex = valueOrDefault;
					}
				}
				value = iconIndex;
				return true;
			}
		}

		// Token: 0x020008B7 RID: 2231
		private sealed class UnreadCountAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060052FF RID: 21247 RVA: 0x0015A9E4 File Offset: 0x00158BE4
			public UnreadCountAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.IsRead
			})
			{
			}

			// Token: 0x06005300 RID: 21248 RVA: 0x0015AA08 File Offset: 0x00158C08
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				int num = 0;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					if (!storePropertyBag.GetValueOrDefault<bool>(InternalSchema.IsRead, true))
					{
						num++;
					}
				}
				value = num;
				return true;
			}
		}

		// Token: 0x020008B8 RID: 2232
		private sealed class FlagStatusAggregation : PropertyAggregationStrategy
		{
			// Token: 0x06005301 RID: 21249 RVA: 0x0015AA6C File Offset: 0x00158C6C
			public FlagStatusAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.FlagStatus
			})
			{
			}

			// Token: 0x06005302 RID: 21250 RVA: 0x0015AA90 File Offset: 0x00158C90
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				FlagStatus flagStatus = FlagStatus.NotFlagged;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					FlagStatus valueOrDefault = storePropertyBag.GetValueOrDefault<FlagStatus>(InternalSchema.FlagStatus, FlagStatus.NotFlagged);
					if (valueOrDefault > flagStatus)
					{
						flagStatus = valueOrDefault;
						if (flagStatus == FlagStatus.Flagged)
						{
							break;
						}
					}
				}
				value = flagStatus;
				return true;
			}
		}

		// Token: 0x020008B9 RID: 2233
		private sealed class RichContentAggregation : PropertyAggregationStrategy
		{
			// Token: 0x06005303 RID: 21251 RVA: 0x0015AAF8 File Offset: 0x00158CF8
			public RichContentAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.RichContent
			})
			{
			}

			// Token: 0x06005304 RID: 21252 RVA: 0x0015AB1C File Offset: 0x00158D1C
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				List<short> list = new List<short>(context.Sources.Count);
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					short valueOrDefault = storePropertyBag.GetValueOrDefault<short>(InternalSchema.RichContent, 0);
					list.Add(valueOrDefault);
				}
				value = list.ToArray();
				return true;
			}
		}
	}
}
