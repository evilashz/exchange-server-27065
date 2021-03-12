using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001CA RID: 458
	internal class AggregatedConversationLoader
	{
		// Token: 0x06000C6D RID: 3181 RVA: 0x00040C49 File Offset: 0x0003EE49
		private AggregatedConversationLoader()
		{
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00040C51 File Offset: 0x0003EE51
		private static AggregatedConversationLoader Instance
		{
			get
			{
				if (AggregatedConversationLoader.instance == null)
				{
					AggregatedConversationLoader.instance = new AggregatedConversationLoader();
				}
				return AggregatedConversationLoader.instance;
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00040C6C File Offset: 0x0003EE6C
		internal static void LoadProperties(ConversationType conversation, IStorePropertyBag aggregatedProperties, MailboxSession session, PropertyUriEnum[] properties)
		{
			foreach (PropertyUriEnum key in properties)
			{
				AggregatedConversationLoader.AggregatedConversationConverter converter = AggregatedConversationLoader.AggregatedConversationConverters[key].Converter;
				object propertyValue = converter.ConvertProperty(aggregatedProperties, session);
				AggregatedConversationLoader.AggregatedConversationConverters[key].Setter(conversation, propertyValue);
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00040CC4 File Offset: 0x0003EEC4
		internal static HashSet<PropertyDefinition> GetAggregatedConversationDependencies(PropertyUriEnum[] properties)
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			foreach (PropertyUriEnum key in properties)
			{
				AggregatedConversationLoader.AggregatedConversationConverter converter = AggregatedConversationLoader.AggregatedConversationConverters[key].Converter;
				foreach (ApplicationAggregatedProperty item in converter.Dependencies)
				{
					hashSet.Add(item);
				}
			}
			return hashSet;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00040E84 File Offset: 0x0003F084
		// Note: this type is marked as 'beforefieldinit'.
		static AggregatedConversationLoader()
		{
			Dictionary<PropertyUriEnum, AggregatedConversationLoader.AggregatedConversationConversion> dictionary = new Dictionary<PropertyUriEnum, AggregatedConversationLoader.AggregatedConversationConversion>();
			dictionary.Add(PropertyUriEnum.ConversationId, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.ConversationIdConverter(), delegate(ConversationType c, object p)
			{
				c.ConversationId = (ItemId)p;
			}));
			dictionary.Add(PropertyUriEnum.Topic, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.StringValueConverter(AggregatedConversationSchema.Topic), delegate(ConversationType c, object p)
			{
				c.ConversationTopic = (string)p;
			}));
			dictionary.Add(PropertyUriEnum.ConversationPreview, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.StringValueConverter(AggregatedConversationSchema.Preview), delegate(ConversationType c, object p)
			{
				c.Preview = (string)p;
			}));
			dictionary.Add(PropertyUriEnum.ConversationHasAttachments, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.BoolConverter(AggregatedConversationSchema.HasAttachments), delegate(ConversationType c, object p)
			{
				c.HasAttachments = new bool?((bool)p);
				c.HasAttachmentsSpecified = true;
			}));
			dictionary.Add(PropertyUriEnum.ConversationHasIrm, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.BoolConverter(AggregatedConversationSchema.HasIrm), delegate(ConversationType c, object p)
			{
				c.HasIrm = new bool?((bool)p);
				c.HasIrmSpecified = true;
			}));
			dictionary.Add(PropertyUriEnum.ConversationUniqueSenders, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.ConversationSendersConverter(), delegate(ConversationType c, object p)
			{
				c.UniqueSenders = (string[])p;
			}));
			dictionary.Add(PropertyUriEnum.ConversationLastDeliveryTime, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.DateTimeConverter(AggregatedConversationSchema.LastDeliveryTime), delegate(ConversationType c, object p)
			{
				c.LastDeliveryTime = (string)p;
			}));
			dictionary.Add(PropertyUriEnum.ConversationLastModifiedTime, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.DateTimeConverter(AggregatedConversationSchema.LastDeliveryTime), delegate(ConversationType c, object p)
			{
				c.LastModifiedTime = (string)p;
			}));
			dictionary.Add(PropertyUriEnum.ConversationGlobalMessageCount, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.IntValueConverter(AggregatedConversationSchema.ItemCount), delegate(ConversationType c, object p)
			{
				c.MessageCount = (c.GlobalMessageCount = new int?((int)p));
				c.MessageCountSpecified = (c.GlobalMessageCountSpecified = true);
			}));
			dictionary.Add(PropertyUriEnum.ConversationSize, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.IntValueConverter(AggregatedConversationSchema.Size), delegate(ConversationType c, object p)
			{
				c.Size = new int?((int)p);
			}));
			dictionary.Add(PropertyUriEnum.ConversationGlobalItemIds, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.ConversationItemIdsConverter(), delegate(ConversationType c, object p)
			{
				c.ItemIds = (c.GlobalItemIds = (BaseItemId[])p);
			}));
			dictionary.Add(PropertyUriEnum.ConversationUnreadCount, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.ZeroConverter(), delegate(ConversationType c, object p)
			{
				c.UnreadCount = new int?((int)p);
				c.UnreadCountSpecified = true;
			}));
			dictionary.Add(PropertyUriEnum.ConversationGlobalUnreadCount, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.ZeroConverter(), delegate(ConversationType c, object p)
			{
				c.GlobalUnreadCount = new int?((int)p);
				c.GlobalUnreadCountSpecified = true;
			}));
			dictionary.Add(PropertyUriEnum.ConversationInstanceKey, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.ByteArrayConverter(AggregatedConversationSchema.InstanceKey), delegate(ConversationType c, object p)
			{
				c.InstanceKey = (byte[])p;
			}));
			dictionary.Add(PropertyUriEnum.ConversationItemClasses, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.ConversationItemClassesConverter(), delegate(ConversationType c, object p)
			{
				c.ItemClasses = (string[])p;
			}));
			dictionary.Add(PropertyUriEnum.ConversationImportance, new AggregatedConversationLoader.AggregatedConversationConversion(new AggregatedConversationLoader.ConversationImportanceConverter(), delegate(ConversationType c, object p)
			{
				c.Importance = (ImportanceType)p;
			}));
			AggregatedConversationLoader.AggregatedConversationConverters = dictionary;
		}

		// Token: 0x04000A3A RID: 2618
		private static AggregatedConversationLoader instance;

		// Token: 0x04000A3B RID: 2619
		private static Dictionary<PropertyUriEnum, AggregatedConversationLoader.AggregatedConversationConversion> AggregatedConversationConverters;

		// Token: 0x020001CB RID: 459
		private class AggregatedConversationConversion
		{
			// Token: 0x1700017F RID: 383
			// (get) Token: 0x06000C82 RID: 3202 RVA: 0x000411E7 File Offset: 0x0003F3E7
			// (set) Token: 0x06000C83 RID: 3203 RVA: 0x000411EF File Offset: 0x0003F3EF
			internal AggregatedConversationLoader.AggregatedConversationConverter Converter { get; private set; }

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x06000C84 RID: 3204 RVA: 0x000411F8 File Offset: 0x0003F3F8
			// (set) Token: 0x06000C85 RID: 3205 RVA: 0x00041200 File Offset: 0x0003F400
			internal AggregatedConversationPropertySetter Setter { get; private set; }

			// Token: 0x06000C86 RID: 3206 RVA: 0x00041209 File Offset: 0x0003F409
			internal AggregatedConversationConversion(AggregatedConversationLoader.AggregatedConversationConverter converter, AggregatedConversationPropertySetter setter)
			{
				this.Converter = converter;
				this.Setter = setter;
			}
		}

		// Token: 0x020001CC RID: 460
		private abstract class AggregatedConversationConverter
		{
			// Token: 0x17000181 RID: 385
			// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0004121F File Offset: 0x0003F41F
			// (set) Token: 0x06000C88 RID: 3208 RVA: 0x00041227 File Offset: 0x0003F427
			internal ApplicationAggregatedProperty[] Dependencies { get; set; }

			// Token: 0x06000C89 RID: 3209 RVA: 0x00041230 File Offset: 0x0003F430
			public AggregatedConversationConverter(ApplicationAggregatedProperty[] dependencies)
			{
				this.Dependencies = dependencies;
			}

			// Token: 0x06000C8A RID: 3210
			public abstract object ConvertProperty(IStorePropertyBag aggregatedConversation, MailboxSession session);
		}

		// Token: 0x020001CD RID: 461
		private class ConversationIdConverter : AggregatedConversationLoader.AggregatedConversationConverter
		{
			// Token: 0x06000C8B RID: 3211 RVA: 0x00041240 File Offset: 0x0003F440
			public ConversationIdConverter() : base(new ApplicationAggregatedProperty[]
			{
				AggregatedConversationSchema.Id
			})
			{
			}

			// Token: 0x06000C8C RID: 3212 RVA: 0x00041264 File Offset: 0x0003F464
			public override object ConvertProperty(IStorePropertyBag aggregatedConversation, MailboxSession session)
			{
				ConversationId valueOrDefault = aggregatedConversation.GetValueOrDefault<ConversationId>(AggregatedConversationSchema.Id, null);
				string id = IdConverter.ConversationIdToEwsId(session.MailboxGuid, valueOrDefault);
				return new ItemId(id, null);
			}
		}

		// Token: 0x020001CE RID: 462
		private class ConversationItemIdsConverter : AggregatedConversationLoader.AggregatedConversationConverter
		{
			// Token: 0x06000C8D RID: 3213 RVA: 0x00041294 File Offset: 0x0003F494
			public ConversationItemIdsConverter() : base(new ApplicationAggregatedProperty[]
			{
				AggregatedConversationSchema.ItemIds
			})
			{
			}

			// Token: 0x06000C8E RID: 3214 RVA: 0x000412B8 File Offset: 0x0003F4B8
			public override object ConvertProperty(IStorePropertyBag aggregatedConversation, MailboxSession session)
			{
				MailboxId mailboxId = new MailboxId(session);
				StoreObjectId[] valueOrDefault = aggregatedConversation.GetValueOrDefault<StoreObjectId[]>(AggregatedConversationSchema.ItemIds, null);
				BaseItemId[] array = new BaseItemId[valueOrDefault.Length];
				for (int i = 0; i < valueOrDefault.Length; i++)
				{
					ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(valueOrDefault[i], mailboxId, null);
					array[i] = new ItemId(concatenatedId.Id, concatenatedId.ChangeKey);
				}
				return array;
			}
		}

		// Token: 0x020001CF RID: 463
		private class ConversationItemClassesConverter : AggregatedConversationLoader.AggregatedConversationConverter
		{
			// Token: 0x06000C8F RID: 3215 RVA: 0x00041314 File Offset: 0x0003F514
			public ConversationItemClassesConverter() : base(new ApplicationAggregatedProperty[]
			{
				AggregatedConversationSchema.ItemClasses
			})
			{
			}

			// Token: 0x06000C90 RID: 3216 RVA: 0x00041337 File Offset: 0x0003F537
			public override object ConvertProperty(IStorePropertyBag aggregatedConversation, MailboxSession session)
			{
				return aggregatedConversation.GetValueOrDefault<string[]>(AggregatedConversationSchema.ItemClasses, null);
			}
		}

		// Token: 0x020001D0 RID: 464
		private class ConversationImportanceConverter : AggregatedConversationLoader.AggregatedConversationConverter
		{
			// Token: 0x06000C91 RID: 3217 RVA: 0x00041345 File Offset: 0x0003F545
			public ConversationImportanceConverter() : base(new ApplicationAggregatedProperty[0])
			{
			}

			// Token: 0x06000C92 RID: 3218 RVA: 0x00041353 File Offset: 0x0003F553
			public override object ConvertProperty(IStorePropertyBag aggregatedConversation, MailboxSession session)
			{
				return (ImportanceType)aggregatedConversation.GetValueOrDefault<Importance>(AggregatedConversationSchema.Importance, Importance.Normal);
			}
		}

		// Token: 0x020001D1 RID: 465
		private class ConversationSendersConverter : AggregatedConversationLoader.AggregatedConversationConverter
		{
			// Token: 0x06000C93 RID: 3219 RVA: 0x00041368 File Offset: 0x0003F568
			public ConversationSendersConverter() : base(new ApplicationAggregatedProperty[]
			{
				AggregatedConversationSchema.DirectParticipants
			})
			{
			}

			// Token: 0x06000C94 RID: 3220 RVA: 0x0004138C File Offset: 0x0003F58C
			public override object ConvertProperty(IStorePropertyBag aggregatedConversation, MailboxSession session)
			{
				Participant[] valueOrDefault = aggregatedConversation.GetValueOrDefault<Participant[]>(AggregatedConversationSchema.DirectParticipants, null);
				string[] array = null;
				if (valueOrDefault != null && valueOrDefault.Length > 0)
				{
					array = new string[valueOrDefault.Length];
					for (int i = 0; i < valueOrDefault.Length; i++)
					{
						string text = string.IsNullOrEmpty(valueOrDefault[i].DisplayName) ? valueOrDefault[i].ToString(AddressFormat.Rfc822Smtp) : valueOrDefault[i].DisplayName;
						array[i] = text;
					}
				}
				return array;
			}
		}

		// Token: 0x020001D2 RID: 466
		private class DateTimeConverter : AggregatedConversationLoader.AggregatedConversationConverter
		{
			// Token: 0x06000C95 RID: 3221 RVA: 0x000413F0 File Offset: 0x0003F5F0
			public DateTimeConverter(ApplicationAggregatedProperty property) : base(new ApplicationAggregatedProperty[]
			{
				property
			})
			{
			}

			// Token: 0x06000C96 RID: 3222 RVA: 0x00041410 File Offset: 0x0003F610
			public override object ConvertProperty(IStorePropertyBag aggregatedConversation, MailboxSession session)
			{
				ExDateTime valueOrDefault = aggregatedConversation.GetValueOrDefault<ExDateTime>(base.Dependencies[0], ExDateTime.Now);
				return ExDateTimeConverter.ToOffsetXsdDateTime(valueOrDefault, valueOrDefault.TimeZone);
			}
		}

		// Token: 0x020001D3 RID: 467
		private class StringValueConverter : AggregatedConversationLoader.AggregatedConversationConverter
		{
			// Token: 0x06000C97 RID: 3223 RVA: 0x00041440 File Offset: 0x0003F640
			public StringValueConverter(ApplicationAggregatedProperty property) : base(new ApplicationAggregatedProperty[]
			{
				property
			})
			{
			}

			// Token: 0x06000C98 RID: 3224 RVA: 0x0004145F File Offset: 0x0003F65F
			public override object ConvertProperty(IStorePropertyBag aggregatedConversation, MailboxSession session)
			{
				return aggregatedConversation.GetValueOrDefault<string>(base.Dependencies[0], string.Empty);
			}
		}

		// Token: 0x020001D4 RID: 468
		private class BoolConverter : AggregatedConversationLoader.AggregatedConversationConverter
		{
			// Token: 0x06000C99 RID: 3225 RVA: 0x00041474 File Offset: 0x0003F674
			public BoolConverter(ApplicationAggregatedProperty property) : base(new ApplicationAggregatedProperty[]
			{
				property
			})
			{
			}

			// Token: 0x06000C9A RID: 3226 RVA: 0x00041493 File Offset: 0x0003F693
			public override object ConvertProperty(IStorePropertyBag aggregatedConversation, MailboxSession session)
			{
				return aggregatedConversation.GetValueOrDefault<bool>(base.Dependencies[0], false);
			}
		}

		// Token: 0x020001D5 RID: 469
		private class IntValueConverter : AggregatedConversationLoader.AggregatedConversationConverter
		{
			// Token: 0x06000C9B RID: 3227 RVA: 0x000414AC File Offset: 0x0003F6AC
			public IntValueConverter(ApplicationAggregatedProperty property) : base(new ApplicationAggregatedProperty[]
			{
				property
			})
			{
			}

			// Token: 0x06000C9C RID: 3228 RVA: 0x000414CB File Offset: 0x0003F6CB
			public override object ConvertProperty(IStorePropertyBag aggregatedConversation, MailboxSession session)
			{
				return aggregatedConversation.GetValueOrDefault<int>(base.Dependencies[0], 0);
			}
		}

		// Token: 0x020001D6 RID: 470
		private class ByteArrayConverter : AggregatedConversationLoader.AggregatedConversationConverter
		{
			// Token: 0x06000C9D RID: 3229 RVA: 0x000414E4 File Offset: 0x0003F6E4
			public ByteArrayConverter(ApplicationAggregatedProperty property) : base(new ApplicationAggregatedProperty[]
			{
				property
			})
			{
			}

			// Token: 0x06000C9E RID: 3230 RVA: 0x00041503 File Offset: 0x0003F703
			public override object ConvertProperty(IStorePropertyBag aggregatedConversation, MailboxSession session)
			{
				return aggregatedConversation.GetValueOrDefault<byte[]>(base.Dependencies[0], null);
			}
		}

		// Token: 0x020001D7 RID: 471
		private class ZeroConverter : AggregatedConversationLoader.AggregatedConversationConverter
		{
			// Token: 0x06000C9F RID: 3231 RVA: 0x00041514 File Offset: 0x0003F714
			public ZeroConverter() : base(new ApplicationAggregatedProperty[0])
			{
			}

			// Token: 0x06000CA0 RID: 3232 RVA: 0x00041522 File Offset: 0x0003F722
			public override object ConvertProperty(IStorePropertyBag aggregatedConversation, MailboxSession session)
			{
				return 0;
			}
		}
	}
}
