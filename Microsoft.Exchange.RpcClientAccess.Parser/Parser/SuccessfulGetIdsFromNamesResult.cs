using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000253 RID: 595
	internal sealed class SuccessfulGetIdsFromNamesResult : RopResult
	{
		// Token: 0x06000CDC RID: 3292 RVA: 0x0002803A File Offset: 0x0002623A
		internal SuccessfulGetIdsFromNamesResult(PropertyId[] propertyIds) : base(RopId.GetIdsFromNames, ErrorCode.None, null)
		{
			if (propertyIds == null)
			{
				throw new ArgumentNullException("propertyIds");
			}
			this.propertyIds = propertyIds;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0002805B File Offset: 0x0002625B
		internal SuccessfulGetIdsFromNamesResult(Reader reader) : base(reader)
		{
			this.propertyIds = reader.ReadSizeAndPropertyIdArray();
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00028070 File Offset: 0x00026270
		public PropertyId[] PropertyIds
		{
			get
			{
				return this.propertyIds;
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00028078 File Offset: 0x00026278
		internal static SuccessfulGetIdsFromNamesResult Parse(Reader reader)
		{
			return new SuccessfulGetIdsFromNamesResult(reader);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00028080 File Offset: 0x00026280
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteCountedPropertyIds(this.propertyIds);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00028095 File Offset: 0x00026295
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" IDs=[");
			Util.AppendToString<PropertyId>(stringBuilder, this.propertyIds);
			stringBuilder.Append("]");
		}

		// Token: 0x040006F4 RID: 1780
		private readonly PropertyId[] propertyIds;
	}
}
