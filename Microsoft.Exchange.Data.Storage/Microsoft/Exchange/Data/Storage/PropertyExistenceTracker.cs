using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C3E RID: 3134
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PropertyExistenceTracker : SmartPropertyDefinition
	{
		// Token: 0x06006EF2 RID: 28402 RVA: 0x001DD730 File Offset: 0x001DB930
		public PropertyExistenceTracker(NativeStorePropertyDefinition trackedProperty) : base(trackedProperty.Name + "_Tracker", trackedProperty.Type, PropertyFlags.None, new PropertyDefinitionConstraint[0], new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.PropertyExistenceTracker, PropertyDependencyType.NeedForRead)
		})
		{
			this.bitFlag = PropertyExistenceTracker.GetBitFlag(trackedProperty);
			if (this.bitFlag == -1L)
			{
				throw new ArgumentException(string.Format("{0} not in Tracked property list", trackedProperty));
			}
		}

		// Token: 0x06006EF3 RID: 28403 RVA: 0x001DD7A0 File Offset: 0x001DB9A0
		public static long GetBitFlag(PropertyDefinition property)
		{
			for (int i = 0; i < PropertyExistenceTracker.TrackedProperties.Length; i++)
			{
				if (property == PropertyExistenceTracker.TrackedProperties[i])
				{
					return 1L << (i & 31);
				}
			}
			return -1L;
		}

		// Token: 0x06006EF4 RID: 28404 RVA: 0x001DD7D4 File Offset: 0x001DB9D4
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			long valueOrDefault = propertyBag.GetValueOrDefault<long>(InternalSchema.PropertyExistenceTracker);
			return (valueOrDefault & this.bitFlag) == this.bitFlag;
		}

		// Token: 0x04004229 RID: 16937
		public static readonly NativeStorePropertyDefinition[] TrackedProperties = new NativeStorePropertyDefinition[]
		{
			InternalSchema.XmlExtractedAddresses,
			InternalSchema.XmlExtractedContacts,
			InternalSchema.XmlExtractedEmails,
			InternalSchema.XmlExtractedKeywords,
			InternalSchema.XmlExtractedMeetings,
			InternalSchema.XmlExtractedPhones,
			InternalSchema.XmlExtractedTasks,
			InternalSchema.XmlExtractedUrls,
			InternalSchema.MapiReplyToNames,
			InternalSchema.MapiReplyToBlob
		};

		// Token: 0x0400422A RID: 16938
		private readonly long bitFlag;
	}
}
