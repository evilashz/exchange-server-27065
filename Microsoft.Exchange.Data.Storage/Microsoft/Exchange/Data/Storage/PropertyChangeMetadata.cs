using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003F4 RID: 1012
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PropertyChangeMetadata
	{
		// Token: 0x06002E3A RID: 11834 RVA: 0x000BDB28 File Offset: 0x000BBD28
		static PropertyChangeMetadata()
		{
			PropertyChangeMetadata.PropertyNameToPropertyAndIndex = new Dictionary<string, PropertyChangeMetadata.PropertyAndIndex>();
			for (int i = 0; i < PropertyChangeMetadata.ListOfTrackedPropertyGroups.Length; i++)
			{
				PropertyChangeMetadata.PropertyGroup propertyGroup = PropertyChangeMetadata.ListOfTrackedPropertyGroups[i];
				if (propertyGroup != null)
				{
					StorePropertyDefinition storeProperty = propertyGroup.StoreProperty;
					if (storeProperty != null && !(storeProperty is NativeStorePropertyDefinition))
					{
						PropertyChangeMetadata.CheckAndAddProperty(storeProperty.Name, storeProperty, propertyGroup, i);
					}
					else if (propertyGroup.IsBitField)
					{
						PropertyChangeMetadata.CheckAndAddProperty(propertyGroup.Name, null, propertyGroup, i);
					}
					foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in PropertyChangeMetadata.ListOfTrackedPropertyGroups[i])
					{
						PropertyChangeMetadata.CheckAndAddProperty(nativeStorePropertyDefinition.Name, nativeStorePropertyDefinition, propertyGroup, i);
					}
				}
			}
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000BDCB4 File Offset: 0x000BBEB4
		public PropertyChangeMetadata() : this(new BitArray(PropertyChangeMetadata.ListOfTrackedPropertyGroups.Length), 0)
		{
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000BDCC9 File Offset: 0x000BBEC9
		private PropertyChangeMetadata(BitArray metadataBitArray, int flags = 0)
		{
			this.masterPropertyOverrideGroupsBitArray = metadataBitArray;
			this.flags = flags;
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06002E3D RID: 11837 RVA: 0x000BDCDF File Offset: 0x000BBEDF
		internal bool AreAllPropertiesExceptions
		{
			get
			{
				return (this.flags & 1) == 1;
			}
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000BDCEC File Offset: 0x000BBEEC
		public static PropertyChangeMetadata Parse(byte[] rawMetadata)
		{
			if (rawMetadata == null)
			{
				throw new ArgumentException("rawMetadata");
			}
			Exception innerException = null;
			try
			{
				if (rawMetadata.Length < 12)
				{
					ExTraceGlobals.CalendarSeriesTracer.TraceError(0L, "PropertyChangeMetadata::Parse. Byte stream is shorter than minimum raw metadata size.");
					throw new PropertyChangeMetadataFormatException(ServerStrings.PropertyChangeMetadataParseError);
				}
				using (MemoryStream memoryStream = new MemoryStream(rawMetadata))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						binaryReader.ReadInt32();
						int num = binaryReader.ReadInt32();
						int num2 = binaryReader.ReadInt32();
						byte[] array = binaryReader.ReadBytes(num2);
						if (array.Length < num2)
						{
							ExTraceGlobals.CalendarSeriesTracer.TraceError(0L, "PropertyChangeMetadata::Parse. Byte stream truncated. Not able to read serialized bits till the end.");
							throw new PropertyChangeMetadataFormatException(ServerStrings.PropertyChangeMetadataParseError);
						}
						byte[] array2 = array;
						if (num2 * 8 < PropertyChangeMetadata.ListOfTrackedPropertyGroups.Length)
						{
							array2 = new byte[PropertyChangeMetadata.TrackedPropertyGroupByteArraySize];
							Array.Copy(array, array2, array.Length);
						}
						return new PropertyChangeMetadata(new BitArray(array2), num);
					}
				}
			}
			catch (ArgumentException ex)
			{
				ExTraceGlobals.CalendarSeriesTracer.TraceError<ArgumentException>(0L, "PropertyChangeMetadata::Parse. Error parsing property change metadata. Ex: {0}", ex);
				innerException = ex;
			}
			catch (IOException ex2)
			{
				ExTraceGlobals.CalendarSeriesTracer.TraceError<IOException>(0L, "PropertyChangeMetadata::Parse. Error parsing property change metadata. Ex: {0}", ex2);
				innerException = ex2;
			}
			throw new PropertyChangeMetadataFormatException(ServerStrings.PropertyChangeMetadataParseError, innerException);
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000BDE44 File Offset: 0x000BC044
		public static bool IsPropertyTracked(StorePropertyDefinition property)
		{
			return PropertyChangeMetadata.PropertyNameToPropertyAndIndex.ContainsKey(property.Name);
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x000BDE58 File Offset: 0x000BC058
		public static bool TryGetTrackedPropertyForName(string propertyName, out StorePropertyDefinition property)
		{
			property = null;
			PropertyChangeMetadata.PropertyAndIndex propertyAndIndex;
			if (PropertyChangeMetadata.PropertyNameToPropertyAndIndex.TryGetValue(propertyName, out propertyAndIndex))
			{
				property = propertyAndIndex.Property;
				return true;
			}
			return false;
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000BDE83 File Offset: 0x000BC083
		public static PropertyChangeMetadata.PropertyGroup GetGroupForProperty(StorePropertyDefinition property)
		{
			return PropertyChangeMetadata.GetGroupForPropertyName(property.Name);
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000BDE90 File Offset: 0x000BC090
		public static PropertyChangeMetadata.PropertyGroup GetGroupForPropertyName(string propertyName)
		{
			int num;
			if (!PropertyChangeMetadata.TryGetMetadataIndexForProperty(propertyName, out num))
			{
				return null;
			}
			return PropertyChangeMetadata.ListOfTrackedPropertyGroups[num];
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000BDEB0 File Offset: 0x000BC0B0
		public static PropertyChangeMetadata Merge(PropertyChangeMetadata metadata1, PropertyChangeMetadata metadata2)
		{
			if (metadata1 == null && metadata2 == null)
			{
				return null;
			}
			metadata1 = (metadata1 ?? new PropertyChangeMetadata());
			metadata2 = (metadata2 ?? new PropertyChangeMetadata());
			PropertyChangeMetadata propertyChangeMetadata = new PropertyChangeMetadata();
			BitArray bitArray = propertyChangeMetadata.masterPropertyOverrideGroupsBitArray;
			BitArray bitArray2 = metadata1.masterPropertyOverrideGroupsBitArray;
			BitArray bitArray3 = metadata2.masterPropertyOverrideGroupsBitArray;
			int length = Math.Max(Math.Max(((ICollection)bitArray2).Count, ((ICollection)bitArray3).Count), ((ICollection)bitArray).Count);
			bitArray2.Length = length;
			bitArray3.Length = length;
			bitArray.Length = length;
			bitArray.Or(bitArray2);
			bitArray.Or(bitArray3);
			propertyChangeMetadata.flags = (metadata1.flags | metadata2.flags);
			return propertyChangeMetadata;
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000BDF54 File Offset: 0x000BC154
		public byte[] ToByteArray()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(1);
					binaryWriter.Write(this.flags);
					byte[] array = new byte[PropertyChangeMetadata.GetByteArraySize(this.masterPropertyOverrideGroupsBitArray.Length)];
					((ICollection)this.masterPropertyOverrideGroupsBitArray).CopyTo(array, 0);
					binaryWriter.Write(array.Length);
					binaryWriter.Write(array);
					binaryWriter.Flush();
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000BDFFD File Offset: 0x000BC1FD
		public IEnumerable<PropertyChangeMetadata.PropertyGroup> GetOverriddenGroups()
		{
			if (this.AreAllPropertiesExceptions)
			{
				return from propGroup in PropertyChangeMetadata.ListOfTrackedPropertyGroups
				where propGroup != null
				select propGroup;
			}
			return this.GetOverriddenGroupsFromBitMapOverride();
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x000BE1A8 File Offset: 0x000BC3A8
		public IEnumerable<NativeStorePropertyDefinition> GetTrackedNonOverrideNativeStorePropertyDefinitions()
		{
			if (this.AreAllPropertiesExceptions)
			{
				return PropertyChangeMetadata.EmptyNativeStorePropertyList;
			}
			return from propertyAndIndex in PropertyChangeMetadata.PropertyNameToPropertyAndIndex
			let nativeProperty = propertyAndIndex.Value.Property as NativeStorePropertyDefinition
			where nativeProperty != null && !this.masterPropertyOverrideGroupsBitArray[propertyAndIndex.Value.Index]
			select nativeProperty;
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000BE250 File Offset: 0x000BC450
		public IEnumerable<string> GetTrackedNonOverrideStorePropertyNames()
		{
			if (this.AreAllPropertiesExceptions)
			{
				return PropertyChangeMetadata.EmptyStorePropertyNameList;
			}
			return from nameAndIndexPair in PropertyChangeMetadata.PropertyNameToPropertyAndIndex
			where nameAndIndexPair.Key != null && !this.masterPropertyOverrideGroupsBitArray[nameAndIndexPair.Value.Index]
			select nameAndIndexPair.Key;
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000BE2A3 File Offset: 0x000BC4A3
		public void MarkAllAsException()
		{
			this.flags |= 1;
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000BE2B4 File Offset: 0x000BC4B4
		public bool IsMasterPropertyOverride(string propertyName)
		{
			int index;
			return !PropertyChangeMetadata.TryGetMetadataIndexForProperty(propertyName, out index) || this.AreAllPropertiesExceptions || this.masterPropertyOverrideGroupsBitArray[index];
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000BE2E4 File Offset: 0x000BC4E4
		public void MarkAsMasterPropertyOverride(string propertyName)
		{
			int index;
			if (PropertyChangeMetadata.TryGetMetadataIndexForProperty(propertyName, out index))
			{
				this.masterPropertyOverrideGroupsBitArray[index] = true;
			}
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000BE308 File Offset: 0x000BC508
		private static void CheckAndAddProperty(string name, StorePropertyDefinition property, PropertyChangeMetadata.PropertyGroup propertyGroup, int index)
		{
			if (propertyGroup.IsBitField)
			{
				PropertyFlags propertyFlags = propertyGroup.ContainerStoreProperty.PropertyFlags;
			}
			PropertyChangeMetadata.PropertyNameToPropertyAndIndex.Add(name, new PropertyChangeMetadata.PropertyAndIndex
			{
				Property = property,
				Index = index
			});
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000BE34D File Offset: 0x000BC54D
		private static int GetByteArraySize(int bitCount)
		{
			return (bitCount - 1) / 8 + 1;
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000BE356 File Offset: 0x000BC556
		private static bool TryGetMetadataIndexForProperty(string propertyName, out int index)
		{
			if (PropertyChangeMetadata.PropertyNameToPropertyAndIndex.ContainsKey(propertyName))
			{
				index = PropertyChangeMetadata.PropertyNameToPropertyAndIndex[propertyName].Index;
				return true;
			}
			index = -1;
			return false;
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000BE51C File Offset: 0x000BC71C
		private IEnumerable<PropertyChangeMetadata.PropertyGroup> GetOverriddenGroupsFromBitMapOverride()
		{
			int bitmapLength = this.masterPropertyOverrideGroupsBitArray.Length;
			int referenceGroupsLength = PropertyChangeMetadata.ListOfTrackedPropertyGroups.Length;
			int groupCount;
			if (bitmapLength < referenceGroupsLength)
			{
				ExTraceGlobals.CalendarSeriesTracer.TraceError(0L, "PropertyChangeMetadata::GetOverridenGroups. Bitmap is shorter than minimum raw metadata size.");
				groupCount = bitmapLength;
			}
			else
			{
				groupCount = referenceGroupsLength;
				if (referenceGroupsLength < bitmapLength)
				{
					ExTraceGlobals.CalendarSeriesTracer.TraceError(0L, "PropertyChangeMetadata::GetOverridenGroups. Bitmap truncated. Not able to read serialized bits till the end.");
				}
			}
			for (int groupIndex = 0; groupIndex < groupCount; groupIndex++)
			{
				PropertyChangeMetadata.PropertyGroup group = PropertyChangeMetadata.ListOfTrackedPropertyGroups[groupIndex];
				if (group != null && this.masterPropertyOverrideGroupsBitArray[groupIndex])
				{
					yield return group;
				}
			}
			yield break;
		}

		// Token: 0x0400192C RID: 6444
		private const int CurrentVersion = 1;

		// Token: 0x0400192D RID: 6445
		private const int MinimumRawMetadataSize = 12;

		// Token: 0x0400192E RID: 6446
		private const int BitsPerByte = 8;

		// Token: 0x0400192F RID: 6447
		private const int AllPropertiesAreExceptionsFlag = 1;

		// Token: 0x04001930 RID: 6448
		private static readonly Dictionary<string, PropertyChangeMetadata.PropertyAndIndex> PropertyNameToPropertyAndIndex;

		// Token: 0x04001931 RID: 6449
		private static readonly NativeStorePropertyDefinition[] EmptyNativeStorePropertyList = Array<NativeStorePropertyDefinition>.Empty;

		// Token: 0x04001932 RID: 6450
		private static readonly string[] EmptyStorePropertyNameList = Array<string>.Empty;

		// Token: 0x04001933 RID: 6451
		private static readonly PropertyChangeMetadata.PropertyGroup[] ListOfTrackedPropertyGroups = new PropertyChangeMetadata.PropertyGroup[]
		{
			PropertyChangeMetadata.PropertyGroup.Subject,
			PropertyChangeMetadata.PropertyGroup.Body,
			PropertyChangeMetadata.PropertyGroup.Location,
			PropertyChangeMetadata.PropertyGroup.ReminderIsSet,
			PropertyChangeMetadata.PropertyGroup.ReminderTime,
			PropertyChangeMetadata.PropertyGroup.FreeBusy,
			PropertyChangeMetadata.PropertyGroup.Attachments,
			PropertyChangeMetadata.PropertyGroup.Color,
			PropertyChangeMetadata.PropertyGroup.Sensitivity,
			PropertyChangeMetadata.PropertyGroup.Importance,
			PropertyChangeMetadata.PropertyGroup.Categories,
			PropertyChangeMetadata.PropertyGroup.Response,
			null,
			PropertyChangeMetadata.PropertyGroup.DisallowNewTimeProposal,
			PropertyChangeMetadata.PropertyGroup.IsMeeting,
			PropertyChangeMetadata.PropertyGroup.IsCancelled,
			PropertyChangeMetadata.PropertyGroup.IsForward
		};

		// Token: 0x04001934 RID: 6452
		private static readonly int TrackedPropertyGroupByteArraySize = PropertyChangeMetadata.GetByteArraySize(PropertyChangeMetadata.ListOfTrackedPropertyGroups.Length);

		// Token: 0x04001935 RID: 6453
		private readonly BitArray masterPropertyOverrideGroupsBitArray;

		// Token: 0x04001936 RID: 6454
		private int flags;

		// Token: 0x020003F5 RID: 1013
		internal struct PropertyAndIndex
		{
			// Token: 0x0400193B RID: 6459
			internal StorePropertyDefinition Property;

			// Token: 0x0400193C RID: 6460
			internal int Index;
		}

		// Token: 0x020003F6 RID: 1014
		public class PropertyGroup : IEnumerable<NativeStorePropertyDefinition>, IEnumerable
		{
			// Token: 0x06002E55 RID: 11861 RVA: 0x000BE539 File Offset: 0x000BC739
			private PropertyGroup(string name, params StorePropertyDefinition[] properties)
			{
				this.AddProperties(properties);
				this.Name = name;
			}

			// Token: 0x06002E56 RID: 11862 RVA: 0x000BE55C File Offset: 0x000BC75C
			private PropertyGroup(StorePropertyDefinition property)
			{
				this.AddProperties(new StorePropertyDefinition[]
				{
					property
				});
				this.StoreProperty = property;
				this.Name = property.Name;
			}

			// Token: 0x06002E57 RID: 11863 RVA: 0x000BE59F File Offset: 0x000BC79F
			private PropertyGroup(string propertyName, StorePropertyDefinition property, StorePropertyDefinition containerProperty, int offset)
			{
				this.Name = propertyName;
				this.StoreProperty = property;
				this.ContainerStoreProperty = containerProperty;
				this.ContainerFlag = offset;
			}

			// Token: 0x17000EC8 RID: 3784
			// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000BE5CF File Offset: 0x000BC7CF
			// (set) Token: 0x06002E59 RID: 11865 RVA: 0x000BE5D7 File Offset: 0x000BC7D7
			public StorePropertyDefinition StoreProperty { get; private set; }

			// Token: 0x17000EC9 RID: 3785
			// (get) Token: 0x06002E5A RID: 11866 RVA: 0x000BE5E0 File Offset: 0x000BC7E0
			// (set) Token: 0x06002E5B RID: 11867 RVA: 0x000BE5E8 File Offset: 0x000BC7E8
			public string Name { get; private set; }

			// Token: 0x17000ECA RID: 3786
			// (get) Token: 0x06002E5C RID: 11868 RVA: 0x000BE5F1 File Offset: 0x000BC7F1
			// (set) Token: 0x06002E5D RID: 11869 RVA: 0x000BE5F9 File Offset: 0x000BC7F9
			public StorePropertyDefinition ContainerStoreProperty { get; private set; }

			// Token: 0x17000ECB RID: 3787
			// (get) Token: 0x06002E5E RID: 11870 RVA: 0x000BE602 File Offset: 0x000BC802
			// (set) Token: 0x06002E5F RID: 11871 RVA: 0x000BE60A File Offset: 0x000BC80A
			public int ContainerFlag { get; private set; }

			// Token: 0x17000ECC RID: 3788
			// (get) Token: 0x06002E60 RID: 11872 RVA: 0x000BE613 File Offset: 0x000BC813
			public bool IsBitField
			{
				get
				{
					return this.ContainerStoreProperty != null;
				}
			}

			// Token: 0x06002E61 RID: 11873 RVA: 0x000BE621 File Offset: 0x000BC821
			public IEnumerator<NativeStorePropertyDefinition> GetEnumerator()
			{
				return this.properties.GetEnumerator();
			}

			// Token: 0x06002E62 RID: 11874 RVA: 0x000BE633 File Offset: 0x000BC833
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.properties.GetEnumerator();
			}

			// Token: 0x06002E63 RID: 11875 RVA: 0x000BE648 File Offset: 0x000BC848
			private void AddProperties(params StorePropertyDefinition[] propertiesToAdd)
			{
				ICollection<NativeStorePropertyDefinition> nativePropertyDefinitions = StorePropertyDefinition.GetNativePropertyDefinitions<StorePropertyDefinition>(PropertyDependencyType.NeedForRead, propertiesToAdd);
				this.properties.AddRange(nativePropertyDefinitions);
			}

			// Token: 0x0400193D RID: 6461
			public static readonly PropertyChangeMetadata.PropertyGroup Attachments = new PropertyChangeMetadata.PropertyGroup(InternalSchema.MapiHasAttachment);

			// Token: 0x0400193E RID: 6462
			public static readonly PropertyChangeMetadata.PropertyGroup Body = new PropertyChangeMetadata.PropertyGroup("Body", new StorePropertyDefinition[]
			{
				InternalSchema.TextBody,
				InternalSchema.HtmlBody,
				InternalSchema.RtfBody
			});

			// Token: 0x0400193F RID: 6463
			public static readonly PropertyChangeMetadata.PropertyGroup Categories = new PropertyChangeMetadata.PropertyGroup(InternalSchema.Categories);

			// Token: 0x04001940 RID: 6464
			public static readonly PropertyChangeMetadata.PropertyGroup Color = new PropertyChangeMetadata.PropertyGroup(InternalSchema.AppointmentColor);

			// Token: 0x04001941 RID: 6465
			public static readonly PropertyChangeMetadata.PropertyGroup DisallowNewTimeProposal = new PropertyChangeMetadata.PropertyGroup(InternalSchema.DisallowNewTimeProposal);

			// Token: 0x04001942 RID: 6466
			public static readonly PropertyChangeMetadata.PropertyGroup EndTime = new PropertyChangeMetadata.PropertyGroup("EndTime", new StorePropertyDefinition[]
			{
				CalendarItemBaseSchema.ClipEndTime,
				CalendarItemInstanceSchema.EndTime
			});

			// Token: 0x04001943 RID: 6467
			public static readonly PropertyChangeMetadata.PropertyGroup FreeBusy = new PropertyChangeMetadata.PropertyGroup(InternalSchema.FreeBusyStatus);

			// Token: 0x04001944 RID: 6468
			public static readonly PropertyChangeMetadata.PropertyGroup Importance = new PropertyChangeMetadata.PropertyGroup("Importance", new StorePropertyDefinition[]
			{
				InternalSchema.MapiImportance,
				InternalSchema.MapiPriority
			});

			// Token: 0x04001945 RID: 6469
			public static readonly PropertyChangeMetadata.PropertyGroup Location = new PropertyChangeMetadata.PropertyGroup("Location", CalendarItemProperties.EnhancedLocationPropertyDefinitions.Concat(new NativeStorePropertyDefinition[]
			{
				InternalSchema.Location,
				InternalSchema.OldLocation,
				InternalSchema.LidWhere,
				InternalSchema.LocationAddressInternal
			}).ToArray<StorePropertyDefinition>());

			// Token: 0x04001946 RID: 6470
			public static readonly PropertyChangeMetadata.PropertyGroup ReminderIsSet = new PropertyChangeMetadata.PropertyGroup("Reminder", new StorePropertyDefinition[]
			{
				InternalSchema.ReminderIsSetInternal
			});

			// Token: 0x04001947 RID: 6471
			public static readonly PropertyChangeMetadata.PropertyGroup ReminderTime = new PropertyChangeMetadata.PropertyGroup("ReminderTime", new StorePropertyDefinition[]
			{
				InternalSchema.ReminderMinutesBeforeStartInternal
			});

			// Token: 0x04001948 RID: 6472
			public static readonly PropertyChangeMetadata.PropertyGroup Response = new PropertyChangeMetadata.PropertyGroup("Response", new StorePropertyDefinition[]
			{
				CalendarItemBaseSchema.ResponseType
			});

			// Token: 0x04001949 RID: 6473
			public static readonly PropertyChangeMetadata.PropertyGroup Sensitivity = new PropertyChangeMetadata.PropertyGroup("Sensitivity", new StorePropertyDefinition[]
			{
				InternalSchema.MapiSensitivity,
				InternalSchema.Privacy
			});

			// Token: 0x0400194A RID: 6474
			public static readonly PropertyChangeMetadata.PropertyGroup StartTime = new PropertyChangeMetadata.PropertyGroup("StartTime", new StorePropertyDefinition[]
			{
				CalendarItemBaseSchema.ClipStartTime,
				CalendarItemInstanceSchema.StartTime
			});

			// Token: 0x0400194B RID: 6475
			public static readonly PropertyChangeMetadata.PropertyGroup IsMeeting = new PropertyChangeMetadata.PropertyGroup(CalendarItemBaseSchema.IsMeeting.Name, CalendarItemBaseSchema.IsMeeting, InternalSchema.AppointmentStateInternal, 1);

			// Token: 0x0400194C RID: 6476
			public static readonly PropertyChangeMetadata.PropertyGroup IsCancelled = new PropertyChangeMetadata.PropertyGroup("IsCancelled", null, InternalSchema.AppointmentStateInternal, 4);

			// Token: 0x0400194D RID: 6477
			public static readonly PropertyChangeMetadata.PropertyGroup IsForward = new PropertyChangeMetadata.PropertyGroup("IsForward", null, InternalSchema.AppointmentStateInternal, 8);

			// Token: 0x0400194E RID: 6478
			public static readonly PropertyChangeMetadata.PropertyGroup Subject = new PropertyChangeMetadata.PropertyGroup(InternalSchema.Subject);

			// Token: 0x0400194F RID: 6479
			private readonly List<NativeStorePropertyDefinition> properties = new List<NativeStorePropertyDefinition>();
		}
	}
}
