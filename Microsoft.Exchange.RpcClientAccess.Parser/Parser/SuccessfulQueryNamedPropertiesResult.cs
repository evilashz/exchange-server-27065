using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200027D RID: 637
	internal sealed class SuccessfulQueryNamedPropertiesResult : RopResult
	{
		// Token: 0x06000DC7 RID: 3527 RVA: 0x00029AEC File Offset: 0x00027CEC
		internal SuccessfulQueryNamedPropertiesResult(PropertyId[] propertyIds, NamedProperty[] namedProperties) : base(RopId.QueryNamedProperties, ErrorCode.None, null)
		{
			Util.ThrowOnNullArgument(propertyIds, "propertyIds");
			Util.ThrowOnNullArgument(namedProperties, "namedProperties");
			if (propertyIds.Length != namedProperties.Length)
			{
				throw new ArgumentException("PropertyId[] and NamedProperty[] must have the same length.");
			}
			this.propertyIds = propertyIds;
			this.namedProperties = namedProperties;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00029B3A File Offset: 0x00027D3A
		internal SuccessfulQueryNamedPropertiesResult(Reader reader) : base(reader)
		{
			this.propertyIds = reader.ReadSizeAndPropertyIdArray();
			this.namedProperties = reader.ReadNamedPropertyArray((ushort)this.propertyIds.Length);
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00029B64 File Offset: 0x00027D64
		internal PropertyId[] PropertyIds
		{
			get
			{
				return this.propertyIds;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00029B6C File Offset: 0x00027D6C
		internal NamedProperty[] NamedProperties
		{
			get
			{
				return this.namedProperties;
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00029B74 File Offset: 0x00027D74
		internal static SuccessfulQueryNamedPropertiesResult Parse(Reader reader)
		{
			return new SuccessfulQueryNamedPropertiesResult(reader);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00029B7C File Offset: 0x00027D7C
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteCountedPropertyIds(this.propertyIds);
			for (int i = 0; i < this.namedProperties.Length; i++)
			{
				this.namedProperties[i].Serialize(writer);
			}
		}

		// Token: 0x04000728 RID: 1832
		private readonly PropertyId[] propertyIds;

		// Token: 0x04000729 RID: 1833
		private readonly NamedProperty[] namedProperties;
	}
}
