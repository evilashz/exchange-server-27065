using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C58 RID: 3160
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ExtendedFolderFlagsProperty : SmartPropertyDefinition
	{
		// Token: 0x06006F6E RID: 28526 RVA: 0x001DFD68 File Offset: 0x001DDF68
		internal ExtendedFolderFlagsProperty(ExtendedFolderFlagsProperty.FlagTag flag) : base("ExtendedFolderFlags", typeof(ExtendedFolderFlags), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.ExtendedFolderFlagsInternal, PropertyDependencyType.AllRead)
		})
		{
			this.flag = flag;
		}

		// Token: 0x06006F6F RID: 28527 RVA: 0x001DFDB0 File Offset: 0x001DDFB0
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			ExtendedFolderFlagsProperty.ParsedFlags parsedFlags = ExtendedFolderFlagsProperty.DecodeFolderFlags(propertyBag.GetValue(InternalSchema.ExtendedFolderFlagsInternal)) as ExtendedFolderFlagsProperty.ParsedFlags;
			if (parsedFlags == null)
			{
				parsedFlags = new ExtendedFolderFlagsProperty.ParsedFlags();
			}
			parsedFlags[this.flag] = BitConverter.GetBytes((int)value);
			propertyBag.SetValueWithFixup(InternalSchema.ExtendedFolderFlagsInternal, ExtendedFolderFlagsProperty.EncodeFolderFlags(parsedFlags));
		}

		// Token: 0x06006F70 RID: 28528 RVA: 0x001DFE08 File Offset: 0x001DE008
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object obj = ExtendedFolderFlagsProperty.DecodeFolderFlags(propertyBag.GetValue(InternalSchema.ExtendedFolderFlagsInternal));
			if (!(obj is ExtendedFolderFlagsProperty.ParsedFlags))
			{
				return obj;
			}
			ExtendedFolderFlagsProperty.ParsedFlags parsedFlags = (ExtendedFolderFlagsProperty.ParsedFlags)obj;
			if (parsedFlags.ContainsKey(this.flag))
			{
				return (ExtendedFolderFlags)BitConverter.ToInt32(parsedFlags[this.flag], 0);
			}
			return new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x06006F71 RID: 28529 RVA: 0x001DFE68 File Offset: 0x001DE068
		internal static object DecodeFolderFlags(object extendedFolderFlagsInternalPropertyValue)
		{
			if (extendedFolderFlagsInternalPropertyValue is byte[])
			{
				ExtendedFolderFlagsProperty.ParsedFlags parsedFlags = new ExtendedFolderFlagsProperty.ParsedFlags();
				using (ParticipantEntryId.Reader reader = new ParticipantEntryId.Reader((byte[])extendedFolderFlagsInternalPropertyValue))
				{
					while (!reader.IsEnd)
					{
						byte key = reader.ReadByte();
						if (!reader.IsEnd)
						{
							byte b = reader.ReadByte();
							if (reader.BytesRemaining >= (int)b)
							{
								parsedFlags[(ExtendedFolderFlagsProperty.FlagTag)key] = reader.ReadExactBytes((int)b);
								continue;
							}
						}
						return new PropertyError(InternalSchema.ExtendedFolderFlags, PropertyErrorCode.CorruptedData);
					}
				}
				return parsedFlags;
			}
			return extendedFolderFlagsInternalPropertyValue;
		}

		// Token: 0x06006F72 RID: 28530 RVA: 0x001DFEFC File Offset: 0x001DE0FC
		internal static byte[] EncodeFolderFlags(ExtendedFolderFlagsProperty.ParsedFlags flags)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				foreach (KeyValuePair<ExtendedFolderFlagsProperty.FlagTag, byte[]> keyValuePair in flags)
				{
					binaryWriter.Write((byte)keyValuePair.Key);
					binaryWriter.Write((byte)keyValuePair.Value.Length);
					binaryWriter.Write(keyValuePair.Value);
				}
			}
			return memoryStream.ToArray();
		}

		// Token: 0x04004394 RID: 17300
		private ExtendedFolderFlagsProperty.FlagTag flag;

		// Token: 0x02000C59 RID: 3161
		internal class ParsedFlags : SortedDictionary<ExtendedFolderFlagsProperty.FlagTag, byte[]>
		{
			// Token: 0x06006F73 RID: 28531 RVA: 0x001DFF98 File Offset: 0x001DE198
			internal ParsedFlags() : base(ExtendedFolderFlagsProperty.FlagTagComparer.Instance)
			{
			}
		}

		// Token: 0x02000C5A RID: 3162
		internal enum FlagTag : byte
		{
			// Token: 0x04004396 RID: 17302
			Flags = 1,
			// Token: 0x04004397 RID: 17303
			Clsid,
			// Token: 0x04004398 RID: 17304
			ToDoVersion = 5
		}

		// Token: 0x02000C5B RID: 3163
		internal class FlagTagComparer : IComparer<ExtendedFolderFlagsProperty.FlagTag>
		{
			// Token: 0x17001E12 RID: 7698
			// (get) Token: 0x06006F74 RID: 28532 RVA: 0x001DFFA5 File Offset: 0x001DE1A5
			internal static ExtendedFolderFlagsProperty.FlagTagComparer Instance
			{
				get
				{
					return ExtendedFolderFlagsProperty.FlagTagComparer.instance;
				}
			}

			// Token: 0x06006F75 RID: 28533 RVA: 0x001DFFAC File Offset: 0x001DE1AC
			public int Compare(ExtendedFolderFlagsProperty.FlagTag x, ExtendedFolderFlagsProperty.FlagTag y)
			{
				return (int)(x - y);
			}

			// Token: 0x04004399 RID: 17305
			private static readonly ExtendedFolderFlagsProperty.FlagTagComparer instance = new ExtendedFolderFlagsProperty.FlagTagComparer();
		}
	}
}
