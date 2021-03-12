using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Data.Storage.ReliableActions
{
	// Token: 0x02000B0F RID: 2831
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ActionQueueProperty : SmartPropertyDefinition
	{
		// Token: 0x060066C1 RID: 26305 RVA: 0x001B3CD8 File Offset: 0x001B1ED8
		public ActionQueueProperty(NativeStorePropertyDefinition rawQueueProperty, NativeStorePropertyDefinition queueHasDataFlagProperty) : base("ActionQueue", typeof(ActionInfo[]), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(rawQueueProperty, PropertyDependencyType.AllRead),
			new PropertyDependency(queueHasDataFlagProperty, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.LastExecutedCalendarInteropAction, PropertyDependencyType.AllRead)
		})
		{
			ArgumentValidator.ThrowIfNull("rawQueueProperty", rawQueueProperty);
			ArgumentValidator.ThrowIfNull("queueHasDataFlagProperty", queueHasDataFlagProperty);
			this.rawQueueProperty = rawQueueProperty;
			this.queueHasDataFlagProperty = queueHasDataFlagProperty;
		}

		// Token: 0x060066C2 RID: 26306 RVA: 0x001B3D50 File Offset: 0x001B1F50
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			ActionInfo[] array = (ActionInfo[])value;
			bool flag = array.IsNullOrEmpty<ActionInfo>();
			propertyBag.SetValue(this.queueHasDataFlagProperty, !flag);
			byte[] propertyValue = null;
			if (!flag)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					JsonConverter.Serialize<ActionInfo[]>(array, memoryStream, null);
					propertyValue = memoryStream.ToArray();
				}
				propertyBag.SetValue(InternalSchema.LastExecutedCalendarInteropAction, array.Last<ActionInfo>().Id);
			}
			propertyBag.SetOrDeleteProperty(this.rawQueueProperty, propertyValue);
		}

		// Token: 0x060066C3 RID: 26307 RVA: 0x001B3DE4 File Offset: 0x001B1FE4
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			byte[] largeBinaryProperty = propertyBag.Context.StoreObject.PropertyBag.GetLargeBinaryProperty(this.rawQueueProperty);
			if (largeBinaryProperty.IsNullOrEmpty<byte>())
			{
				return ActionQueueProperty.EmptyQueue;
			}
			object result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(largeBinaryProperty))
				{
					result = JsonConverter.Deserialize<ActionInfo[]>(memoryStream, null);
				}
			}
			catch (SerializationException ex)
			{
				result = new PropertyError(this, PropertyErrorCode.CorruptedData, ex.ToString());
			}
			return result;
		}

		// Token: 0x060066C4 RID: 26308 RVA: 0x001B3E68 File Offset: 0x001B2068
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(this.rawQueueProperty);
			propertyBag.SetValue(this.queueHasDataFlagProperty, false);
		}

		// Token: 0x04003A43 RID: 14915
		private static readonly ActionInfo[] EmptyQueue = Array<ActionInfo>.Empty;

		// Token: 0x04003A44 RID: 14916
		private readonly NativeStorePropertyDefinition queueHasDataFlagProperty;

		// Token: 0x04003A45 RID: 14917
		private readonly NativeStorePropertyDefinition rawQueueProperty;
	}
}
