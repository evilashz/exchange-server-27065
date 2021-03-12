using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C4D RID: 3149
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ConversationIdFromIndexProperty : SmartPropertyDefinition
	{
		// Token: 0x06006F47 RID: 28487 RVA: 0x001DF044 File Offset: 0x001DD244
		internal ConversationIdFromIndexProperty() : base("ConversationId", typeof(ConversationId), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.ItemClass, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.ConversationTopic, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.ConversationIndexTracking, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.ConversationIndex, PropertyDependencyType.AllRead)
		})
		{
		}

		// Token: 0x06006F48 RID: 28488 RVA: 0x001DF0AC File Offset: 0x001DD2AC
		protected sealed override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			ConversationId conversationId = value as ConversationId;
			if (conversationId == null)
			{
				throw new ArgumentException("value", "Must be a non-null ConversationId instance");
			}
			ConversationIndex conversationIndex;
			if (!ConversationIndex.TryCreate(propertyBag.GetValueOrDefault<byte[]>(InternalSchema.ConversationIndex), out conversationIndex))
			{
				conversationIndex = ConversationIndex.Create(conversationId);
			}
			else
			{
				conversationIndex = conversationIndex.UpdateGuid(new Guid(conversationId.GetBytes()));
			}
			byte[] array = conversationIndex.ToByteArray();
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass);
			if (valueOrDefault != null && ObjectClass.IsOfClass(valueOrDefault, "IPM.ConversationAction"))
			{
				array[0] = 1;
				array[1] = (array[2] = (array[3] = (array[4] = (array[5] = 0))));
			}
			propertyBag.SetValueWithFixup(InternalSchema.ConversationIndex, array);
		}

		// Token: 0x06006F49 RID: 28489 RVA: 0x001DF164 File Offset: 0x001DD364
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass);
			byte[] valueOrDefault = propertyBag.GetValueOrDefault<byte[]>(InternalSchema.ConversationIndex);
			if (valueOrDefault == null)
			{
				return null;
			}
			ConversationIndex conversationIndex;
			if (!ConversationIndex.TryCreate(valueOrDefault, out conversationIndex))
			{
				return null;
			}
			bool? valueAsNullable = propertyBag.GetValueAsNullable<bool>(InternalSchema.ConversationIndexTracking);
			if (valueAsNullable == null || !valueAsNullable.Value)
			{
				string topic = propertyBag.GetValueOrDefault<string>(InternalSchema.ConversationTopic) ?? string.Empty;
				byte[] bytes = this.ComputeHashTopic(topic);
				return ConversationId.Create(bytes);
			}
			return ConversationId.Create(conversationIndex.Guid);
		}

		// Token: 0x06006F4A RID: 28490 RVA: 0x001DF1F2 File Offset: 0x001DD3F2
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return this.NativeFilterToConversationIdBasedSmartFilter(filter, InternalSchema.MapiConversationId);
		}

		// Token: 0x06006F4B RID: 28491 RVA: 0x001DF200 File Offset: 0x001DD400
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return this.ConversationIdBasedSmartFilterToNativeFilter(filter, InternalSchema.MapiConversationId);
		}

		// Token: 0x06006F4C RID: 28492 RVA: 0x001DF20E File Offset: 0x001DD40E
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return InternalSchema.MapiConversationId;
		}

		// Token: 0x17001E09 RID: 7689
		// (get) Token: 0x06006F4D RID: 28493 RVA: 0x001DF215 File Offset: 0x001DD415
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x06006F4E RID: 28494 RVA: 0x001DF218 File Offset: 0x001DD418
		internal byte[] ComputeHashTopic(string topic)
		{
			byte[] result;
			using (Md5Hasher md5Hasher = new Md5Hasher())
			{
				byte[] bytes = Encoding.Unicode.GetBytes(topic.ToUpper());
				byte[] array = md5Hasher.ComputeHash(bytes);
				result = array;
			}
			return result;
		}

		// Token: 0x06006F4F RID: 28495 RVA: 0x001DF264 File Offset: 0x001DD464
		internal static bool CheckExclusionList(string itemClass)
		{
			if (!string.IsNullOrEmpty(itemClass))
			{
				if (ObjectClass.IsOfClass(itemClass, "IPM.Activity"))
				{
					return true;
				}
				if (ObjectClass.IsOfClass(itemClass, "IPM.Appointment"))
				{
					return true;
				}
				if (ObjectClass.IsOfClass(itemClass, "IPM.Contact"))
				{
					return true;
				}
				if (ObjectClass.IsOfClass(itemClass, "IPM.ContentClassDef"))
				{
					return true;
				}
				if (ObjectClass.IsOfClass(itemClass, "IPM.DistList"))
				{
					return true;
				}
				if (ObjectClass.IsOfClass(itemClass, "IPM.Document"))
				{
					return true;
				}
				if (ObjectClass.IsOfClass(itemClass, "IPM.Microsoft.ScheduleData.FreeBusy"))
				{
					return true;
				}
				if (ObjectClass.IsOfClass(itemClass, "IPM.InfoPathForm"))
				{
					return true;
				}
				if (ObjectClass.IsOfClass(itemClass, "IPM.InkNotes"))
				{
					return true;
				}
				if (ObjectClass.IsOfClass(itemClass, "IPM.StickyNote"))
				{
					return true;
				}
				if (ObjectClass.IsOfClass(itemClass, "IPM.AuditLog"))
				{
					return true;
				}
			}
			return false;
		}
	}
}
