using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001F2 RID: 498
	internal class NamedProperty : IEquatable<NamedProperty>
	{
		// Token: 0x06000A97 RID: 2711 RVA: 0x000206CF File Offset: 0x0001E8CF
		public NamedProperty()
		{
			this.kind = NamedPropertyKind.Null;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000206E2 File Offset: 0x0001E8E2
		public NamedProperty(Guid guid, string name)
		{
			Util.ThrowOnNullArgument(name, "name");
			if (name.Length > 256)
			{
				throw new ArgumentException("name");
			}
			this.kind = NamedPropertyKind.String;
			this.guid = guid;
			this.name = name;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00020722 File Offset: 0x0001E922
		public NamedProperty(Guid guid, uint id)
		{
			this.kind = NamedPropertyKind.Id;
			this.guid = guid;
			this.id = id;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00020740 File Offset: 0x0001E940
		public bool IsMapiNamespace
		{
			get
			{
				return this.guid.Equals(NamedProperty.PS_MAPI);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00020760 File Offset: 0x0001E960
		public NamedPropertyKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00020768 File Offset: 0x0001E968
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00020770 File Offset: 0x0001E970
		public string Name
		{
			get
			{
				if (this.kind != NamedPropertyKind.String)
				{
					throw new InvalidOperationException(string.Format("Accessing NamedProperty.Name when it is not a NamedPropertyKind.String [NamedPropertyKind = {0}].", this.kind));
				}
				return this.name;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0002079C File Offset: 0x0001E99C
		public uint Id
		{
			get
			{
				if (this.kind != NamedPropertyKind.Id)
				{
					throw new InvalidOperationException(string.Format("Accessing NamedProperty.Id when it is not a NamedPropertyKind.Id [NamedPropertyKind = {0}].", this.kind));
				}
				return this.id;
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000207C8 File Offset: 0x0001E9C8
		public override int GetHashCode()
		{
			return this.kind.GetHashCode() ^ this.guid.GetHashCode() ^ this.id.GetHashCode() ^ ((this.name != null) ? this.name.GetHashCode() : 0);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00020820 File Offset: 0x0001EA20
		public override bool Equals(object obj)
		{
			NamedProperty namedProperty = obj as NamedProperty;
			return namedProperty != null && this.Equals(namedProperty);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00020840 File Offset: 0x0001EA40
		public bool Equals(NamedProperty v)
		{
			return this.kind == v.kind && this.guid == v.guid && this.id == v.id && this.name == v.name;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00020890 File Offset: 0x0001EA90
		public override string ToString()
		{
			NamedPropertyKind namedPropertyKind = this.kind;
			switch (namedPropertyKind)
			{
			case NamedPropertyKind.Id:
				return string.Format("NamedProperty: [Kind: {0}, Guid: {1}, Id: {2}]", this.kind, this.guid, this.id);
			case NamedPropertyKind.String:
				return string.Format("NamedProperty: [Kind: {0}, Guid: {1}, Name: {2}]", this.kind, this.guid, this.name);
			default:
				if (namedPropertyKind == NamedPropertyKind.Null)
				{
					return string.Format("NamedProperty: [Kind: Null]", new object[0]);
				}
				return "NamedProperty: [Kind: Invalid]";
			}
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00020923 File Offset: 0x0001EB23
		internal static NamedProperty CreateMAPINamedProperty(PropertyId propertyId)
		{
			return new NamedProperty(NamedProperty.PS_MAPI, (uint)propertyId);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00020930 File Offset: 0x0001EB30
		internal static NamedProperty Parse(Reader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			NamedPropertyKind namedPropertyKind = (NamedPropertyKind)reader.ReadByte();
			if (namedPropertyKind == NamedPropertyKind.Null)
			{
				return new NamedProperty();
			}
			Guid guid = reader.ReadGuid();
			if (namedPropertyKind == NamedPropertyKind.String)
			{
				StringFlags flags = StringFlags.IncludeNull | StringFlags.Sized;
				return new NamedProperty(guid, reader.ReadUnicodeString(flags));
			}
			if (namedPropertyKind == NamedPropertyKind.Id)
			{
				return new NamedProperty(guid, reader.ReadUInt32());
			}
			throw new BufferParseException("Invalid named property kind");
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00020998 File Offset: 0x0001EB98
		internal void Serialize(Writer writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteByte((byte)this.kind);
			if (this.kind == NamedPropertyKind.Null)
			{
				return;
			}
			writer.WriteGuid(this.guid);
			if (this.kind == NamedPropertyKind.String)
			{
				StringFlags stringFlags = StringFlags.IncludeNull | StringFlags.Sized;
				if (this.guid.Equals(NamedProperty.PSETID_InternetHeaders))
				{
					stringFlags |= StringFlags.SevenBitAscii;
				}
				writer.WriteUnicodeString(this.name, stringFlags);
				return;
			}
			if (this.kind == NamedPropertyKind.Id)
			{
				writer.WriteUInt32(this.id);
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00020A20 File Offset: 0x0001EC20
		internal void SerializeForActions(Writer writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteGuid(this.guid);
			writer.WriteByte((byte)this.kind);
			if (this.kind == NamedPropertyKind.String)
			{
				writer.WriteUnicodeString(this.name, StringFlags.IncludeNull);
				return;
			}
			if (this.kind == NamedPropertyKind.Id)
			{
				writer.WriteUInt32(this.id);
			}
		}

		// Token: 0x04000499 RID: 1177
		internal const int MaxNameLength = 256;

		// Token: 0x0400049A RID: 1178
		internal static readonly uint MinimumSize = 1U;

		// Token: 0x0400049B RID: 1179
		private static readonly Guid PSETID_InternetHeaders = new Guid("{00020386-0000-0000-C000-000000000046}");

		// Token: 0x0400049C RID: 1180
		private static readonly Guid PS_MAPI = new Guid("{00020328-0000-0000-C000-000000000046}");

		// Token: 0x0400049D RID: 1181
		private readonly NamedPropertyKind kind;

		// Token: 0x0400049E RID: 1182
		private readonly Guid guid;

		// Token: 0x0400049F RID: 1183
		private readonly string name;

		// Token: 0x040004A0 RID: 1184
		private readonly uint id;
	}
}
