using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x020001A2 RID: 418
	internal class PropertyGroupMapping
	{
		// Token: 0x0600084C RID: 2124 RVA: 0x0001D0C4 File Offset: 0x0001B2C4
		public PropertyGroupMapping(int mappingId, IList<IList<AnnotatedPropertyTag>> propGroups)
		{
			Util.ThrowOnNullArgument(propGroups, "propGroups");
			this.mappingId = mappingId;
			this.propGroups = propGroups;
			for (int i = 0; i < propGroups.Count; i++)
			{
				IList<AnnotatedPropertyTag> list = propGroups[i];
				foreach (AnnotatedPropertyTag annotatedPropertyTag in list)
				{
					if (annotatedPropertyTag.PropertyTag == PropertyTag.MessageRecipients)
					{
						this.recipientsGroupIndex = i;
					}
					else if (annotatedPropertyTag.PropertyTag == PropertyTag.MessageAttachments)
					{
						this.attachmentsGroupIndex = i;
					}
				}
				if (this.recipientsGroupIndex != -1 && this.attachmentsGroupIndex != -1)
				{
					return;
				}
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0001D198 File Offset: 0x0001B398
		public int MappingId
		{
			get
			{
				return this.mappingId;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0001D1A0 File Offset: 0x0001B3A0
		public int GroupCount
		{
			get
			{
				return this.propGroups.Count;
			}
		}

		// Token: 0x17000160 RID: 352
		public IList<AnnotatedPropertyTag> this[int groupIndex]
		{
			get
			{
				return this.propGroups[groupIndex];
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0001D1BB File Offset: 0x0001B3BB
		public int RecipientsGroupIndex
		{
			get
			{
				return this.recipientsGroupIndex;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0001D1C3 File Offset: 0x0001B3C3
		public int AttachmentsGroupIndex
		{
			get
			{
				return this.attachmentsGroupIndex;
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001D1CC File Offset: 0x0001B3CC
		public static PropertyGroupMapping Deserialize(byte[] buffer)
		{
			PropertyGroupMapping result;
			using (BufferReader bufferReader = Reader.CreateBufferReader(buffer))
			{
				ulong num = bufferReader.ReadUInt64();
				if (0L != 0L)
				{
					throw new BufferParseException("Error parsing property group mapping - invalid mapping ID");
				}
				int num2 = bufferReader.ReadInt32();
				if (num2 <= 0 || num2 > 64)
				{
					throw new BufferParseException("Error parsing property group mapping - invalid group count");
				}
				List<IList<AnnotatedPropertyTag>> list = new List<IList<AnnotatedPropertyTag>>(num2);
				for (int i = 0; i < num2; i++)
				{
					int num3 = bufferReader.ReadInt32();
					if (num3 < 0 || num3 > 1000)
					{
						throw new BufferParseException("Error parsing property group mapping - invalid property count");
					}
					List<AnnotatedPropertyTag> list2 = new List<AnnotatedPropertyTag>(num3);
					for (int j = 0; j < num3; j++)
					{
						PropertyTag propertyTag = new PropertyTag(bufferReader.ReadUInt32());
						NamedProperty namedProperty = null;
						if (propertyTag.IsNamedProperty)
						{
							Guid guid = bufferReader.ReadGuid();
							uint num4 = bufferReader.ReadUInt32();
							if (num4 == 0U)
							{
								namedProperty = new NamedProperty(guid, bufferReader.ReadUInt32());
							}
							else
							{
								if (num4 != 1U)
								{
									throw new BufferParseException("Error parsing property group mapping - invalid named property kind");
								}
								namedProperty = new NamedProperty(guid, bufferReader.ReadUnicodeString(StringFlags.Sized32));
							}
						}
						if (propertyTag.PropertyId != PropertyId.Null)
						{
							list2.Add(new AnnotatedPropertyTag(propertyTag, namedProperty));
						}
					}
					list.Add(new ReadOnlyCollection<AnnotatedPropertyTag>(list2));
				}
				result = new PropertyGroupMapping((int)num, new ReadOnlyCollection<IList<AnnotatedPropertyTag>>(list));
			}
			return result;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001D334 File Offset: 0x0001B534
		public bool IsValidPropGroupIndex(int propGroupIndex)
		{
			return propGroupIndex >= -1 && propGroupIndex < this.GroupCount;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001D348 File Offset: 0x0001B548
		public byte[] Serialize()
		{
			uint num = 0U;
			using (CountWriter countWriter = new CountWriter())
			{
				this.Serialize(countWriter);
				num = (uint)countWriter.Position;
			}
			byte[] array = new byte[num];
			using (Writer writer = new BufferWriter(array))
			{
				this.Serialize(writer);
			}
			return array;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001D3B8 File Offset: 0x0001B5B8
		public bool IsPropertyInAnyGroup(PropertyTag propertyTag)
		{
			if (this.propertiesInAnyNumberedGroup == null)
			{
				this.propertiesInAnyNumberedGroup = new HashSet<PropertyTag>();
				for (int i = 0; i < this.propGroups.Count; i++)
				{
					IList<AnnotatedPropertyTag> list = this.propGroups[i];
					for (int j = 0; j < list.Count; j++)
					{
						this.propertiesInAnyNumberedGroup.Add(list[j].PropertyTag);
					}
				}
			}
			return this.propertiesInAnyNumberedGroup.Contains(propertyTag);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001D430 File Offset: 0x0001B630
		private void Serialize(Writer writer)
		{
			writer.WriteUInt64((ulong)this.mappingId);
			writer.WriteInt32(this.propGroups.Count);
			for (int i = 0; i < this.propGroups.Count; i++)
			{
				IList<AnnotatedPropertyTag> list = this.propGroups[i];
				writer.WriteInt32(list.Count);
				for (int j = 0; j < list.Count; j++)
				{
					AnnotatedPropertyTag annotatedPropertyTag = list[j];
					writer.WritePropertyTag(annotatedPropertyTag.PropertyTag);
					if (annotatedPropertyTag.PropertyTag.IsNamedProperty)
					{
						writer.WriteGuid(annotatedPropertyTag.NamedProperty.Guid);
						writer.WriteUInt32((uint)annotatedPropertyTag.NamedProperty.Kind);
						if (annotatedPropertyTag.NamedProperty.Kind == NamedPropertyKind.String)
						{
							writer.WriteUnicodeString(annotatedPropertyTag.NamedProperty.Name, StringFlags.Sized32);
						}
						else if (annotatedPropertyTag.NamedProperty.Kind == NamedPropertyKind.Id)
						{
							writer.WriteUInt32(annotatedPropertyTag.NamedProperty.Id);
						}
					}
				}
			}
		}

		// Token: 0x040003F1 RID: 1009
		public const int OtherGroupIndex = -1;

		// Token: 0x040003F2 RID: 1010
		public static byte[] SerializedFakeMapping = new PropertyGroupMapping(-1, new IList<AnnotatedPropertyTag>[0]).Serialize();

		// Token: 0x040003F3 RID: 1011
		private int mappingId;

		// Token: 0x040003F4 RID: 1012
		private IList<IList<AnnotatedPropertyTag>> propGroups;

		// Token: 0x040003F5 RID: 1013
		private int recipientsGroupIndex = -1;

		// Token: 0x040003F6 RID: 1014
		private int attachmentsGroupIndex = -1;

		// Token: 0x040003F7 RID: 1015
		private HashSet<PropertyTag> propertiesInAnyNumberedGroup;
	}
}
