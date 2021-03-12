using System;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PropTags
{
	// Token: 0x0200000A RID: 10
	public struct StorePropTag : IComparable<StorePropTag>, IEquatable<StorePropTag>
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000034BF File Offset: 0x000016BF
		public StorePropTag(uint propTag, StorePropInfo propertyInfo, PropertyType externalType, ObjectType baseObjectType)
		{
			this = new StorePropTag(propTag, propertyInfo, externalType, baseObjectType, true);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000034CD File Offset: 0x000016CD
		public StorePropTag(uint propTag, StorePropInfo propertyInfo, ObjectType baseObjectType)
		{
			this = new StorePropTag(propTag, propertyInfo, (PropertyType)(propTag & 65535U), baseObjectType, true);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000034E1 File Offset: 0x000016E1
		public StorePropTag(ushort propId, PropertyType propType, StorePropInfo propertyInfo, PropertyType externalType, ObjectType baseObjectType)
		{
			this = new StorePropTag(StorePropTag.BuildNumPropTag(propId, propType), propertyInfo, externalType, baseObjectType, true);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000034F6 File Offset: 0x000016F6
		public StorePropTag(ushort propId, PropertyType propType, StorePropInfo propertyInfo, ObjectType baseObjectType)
		{
			this = new StorePropTag(StorePropTag.BuildNumPropTag(propId, propType), propertyInfo, propType, baseObjectType, true);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000350A File Offset: 0x0000170A
		private StorePropTag(uint propTag, ObjectType baseObjectType)
		{
			this = new StorePropTag(propTag, null, (PropertyType)(propTag & 65535U), baseObjectType, false);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000351E File Offset: 0x0000171E
		private StorePropTag(uint propTag, PropertyType externalType, ObjectType baseObjectType)
		{
			this = new StorePropTag(propTag, null, externalType, baseObjectType, false);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000352B File Offset: 0x0000172B
		private StorePropTag(StorePropTag originalTag, PropertyType newPropType)
		{
			this = new StorePropTag(StorePropTag.BuildNumPropTag(originalTag.PropId, newPropType), originalTag.propertyInfo, originalTag.externalType, originalTag.baseObjectType, false);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003556 File Offset: 0x00001756
		private StorePropTag(StorePropTag originalTag, ObjectType newBaseObjectType)
		{
			this = new StorePropTag(originalTag.PropTag, originalTag.propertyInfo, originalTag.externalType, newBaseObjectType, false);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003575 File Offset: 0x00001775
		private StorePropTag(uint propTag, StorePropInfo propertyInfo, PropertyType externalType, ObjectType baseObjectType, bool checkInfo)
		{
			this.propTag = propTag;
			this.externalType = externalType;
			this.baseObjectType = baseObjectType;
			this.propertyInfo = propertyInfo;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003594 File Offset: 0x00001794
		public uint PropTag
		{
			get
			{
				return this.propTag;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000359C File Offset: 0x0000179C
		public ushort PropId
		{
			get
			{
				return (ushort)(this.propTag >> 16);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000035A8 File Offset: 0x000017A8
		public PropertyType PropType
		{
			get
			{
				return (PropertyType)(this.propTag & 65535U);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000035B7 File Offset: 0x000017B7
		public PropertyType ExternalType
		{
			get
			{
				return this.externalType;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000035BF File Offset: 0x000017BF
		public uint ExternalTag
		{
			get
			{
				return (uint)((int)this.PropId << 16 | (int)this.externalType);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000035D1 File Offset: 0x000017D1
		public bool IsNamedProperty
		{
			get
			{
				return this.propTag >= 2147483648U;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000035E3 File Offset: 0x000017E3
		public bool IsMultiValued
		{
			get
			{
				return 4096 == (ushort)(this.PropType & (PropertyType)12288);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000035F9 File Offset: 0x000017F9
		public bool IsMultiValueInstance
		{
			get
			{
				return 12288 == (ushort)(this.PropType & (PropertyType)12288);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000360F File Offset: 0x0000180F
		public ulong GroupMask
		{
			get
			{
				if (this.propertyInfo == null)
				{
					return 9223372036854775808UL;
				}
				return this.propertyInfo.GroupMask;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000362E File Offset: 0x0000182E
		public string DescriptiveName
		{
			get
			{
				if (this.propertyInfo == null)
				{
					return null;
				}
				return this.propertyInfo.DescriptiveName;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003645 File Offset: 0x00001845
		public StorePropName PropName
		{
			get
			{
				if (this.propertyInfo != null)
				{
					return this.propertyInfo.PropName;
				}
				if (this.IsNamedProperty)
				{
					return StorePropName.Invalid;
				}
				return new StorePropName(StorePropName.UnnamedPropertyNamespaceGuid, (uint)this.PropId);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003679 File Offset: 0x00001879
		public StorePropInfo PropInfo
		{
			get
			{
				return this.propertyInfo;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003681 File Offset: 0x00001881
		public ObjectType BaseObjectType
		{
			get
			{
				return this.baseObjectType;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003689 File Offset: 0x00001889
		public static StorePropTag CreateWithoutInfo(ushort propId, PropertyType propType)
		{
			return new StorePropTag(StorePropTag.BuildNumPropTag(propId, propType), ObjectType.Invalid);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003698 File Offset: 0x00001898
		public static StorePropTag CreateWithoutInfo(ushort propId, PropertyType propType, ObjectType baseObjectType)
		{
			return new StorePropTag(StorePropTag.BuildNumPropTag(propId, propType), baseObjectType);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000036A7 File Offset: 0x000018A7
		public static StorePropTag CreateWithoutInfo(ushort propId, PropertyType propType, PropertyType externalPropertyType, ObjectType baseObjectType)
		{
			return new StorePropTag(StorePropTag.BuildNumPropTag(propId, propType), externalPropertyType, baseObjectType);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000036B7 File Offset: 0x000018B7
		public static StorePropTag CreateWithoutInfo(uint propTag)
		{
			return new StorePropTag(propTag, ObjectType.Invalid);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000036C0 File Offset: 0x000018C0
		public static StorePropTag CreateWithoutInfo(uint propTag, ObjectType baseObjectType)
		{
			return new StorePropTag(propTag, baseObjectType);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000036C9 File Offset: 0x000018C9
		public static uint BuildNumPropTag(ushort propId, PropertyType propType)
		{
			return (uint)((int)propId << 16 | (int)propType);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000036D1 File Offset: 0x000018D1
		public static bool operator ==(StorePropTag tag1, StorePropTag tag2)
		{
			return tag1.Equals(tag2);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000036DB File Offset: 0x000018DB
		public static bool operator !=(StorePropTag tag1, StorePropTag tag2)
		{
			return !tag1.Equals(tag2);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000036E8 File Offset: 0x000018E8
		[Obsolete]
		public static bool operator ==(StorePropTag tag1, object tag2)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000036EF File Offset: 0x000018EF
		[Obsolete]
		public static bool operator !=(StorePropTag tag1, object tag2)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000036F6 File Offset: 0x000018F6
		[Obsolete]
		public static bool operator ==(object tag1, StorePropTag tag2)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000036FD File Offset: 0x000018FD
		[Obsolete]
		public static bool operator !=(object tag1, StorePropTag tag2)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003704 File Offset: 0x00001904
		public bool IsCategory(int category)
		{
			return this.propertyInfo != null && this.propertyInfo.IsCategory(category);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000371C File Offset: 0x0000191C
		public StorePropTag ChangeType(PropertyType propType)
		{
			return new StorePropTag(this, propType);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000372A File Offset: 0x0000192A
		public StorePropTag ChangeBaseObjectTypeForTest(ObjectType newBaseObjectType)
		{
			return new StorePropTag(this, newBaseObjectType);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003738 File Offset: 0x00001938
		public StorePropTag ConvertToError()
		{
			return this.ChangeType(PropertyType.Error);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003742 File Offset: 0x00001942
		public bool Equals(StorePropTag other)
		{
			return this.PropTag == other.PropTag;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003753 File Offset: 0x00001953
		public override bool Equals(object other)
		{
			return other is StorePropTag && this.Equals((StorePropTag)other);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000376C File Offset: 0x0000196C
		public int CompareTo(StorePropTag other)
		{
			return this.PropTag.CompareTo(other.PropTag);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000378E File Offset: 0x0000198E
		public override int GetHashCode()
		{
			return (int)this.PropTag;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003798 File Offset: 0x00001998
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(32);
			this.AppendToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000037BA File Offset: 0x000019BA
		public void AppendToString(StringBuilder sb)
		{
			this.AppendToString(sb, false);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000037C4 File Offset: 0x000019C4
		public void AppendToString(StringBuilder sb, bool includeDetails)
		{
			if (this.propertyInfo != null && this.propertyInfo.DescriptiveName != null)
			{
				sb.Append(this.propertyInfo.DescriptiveName);
				if (includeDetails || this.IsNamedProperty)
				{
					sb.Append(":");
				}
			}
			if (includeDetails || this.propertyInfo == null || this.propertyInfo.DescriptiveName == null || this.IsNamedProperty)
			{
				sb.Append(this.PropId.ToString("X4"));
				if (includeDetails || this.propertyInfo == null || this.propertyInfo.DescriptiveName == null)
				{
					sb.Append(":");
					sb.Append(PropertyTypeHelper.PropertyTypeToString(this.PropType));
				}
			}
			if (includeDetails && this.propertyInfo != null && this.IsNamedProperty)
			{
				sb.Append("(");
				this.propertyInfo.PropName.AppendToString(sb);
				sb.Append(")");
			}
		}

		// Token: 0x04000062 RID: 98
		public static readonly StorePropTag Invalid = default(StorePropTag);

		// Token: 0x04000063 RID: 99
		private readonly uint propTag;

		// Token: 0x04000064 RID: 100
		private readonly PropertyType externalType;

		// Token: 0x04000065 RID: 101
		private readonly ObjectType baseObjectType;

		// Token: 0x04000066 RID: 102
		private readonly StorePropInfo propertyInfo;
	}
}
