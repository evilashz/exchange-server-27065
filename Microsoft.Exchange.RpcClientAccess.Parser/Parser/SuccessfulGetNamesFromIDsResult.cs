using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000256 RID: 598
	internal sealed class SuccessfulGetNamesFromIDsResult : RopResult
	{
		// Token: 0x06000CEE RID: 3310 RVA: 0x000281C1 File Offset: 0x000263C1
		internal SuccessfulGetNamesFromIDsResult(NamedProperty[] namedProperties) : base(RopId.GetNamesFromIDs, ErrorCode.None, null)
		{
			if (namedProperties == null)
			{
				throw new ArgumentNullException("namedProperties");
			}
			this.namedProperties = namedProperties;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x000281E2 File Offset: 0x000263E2
		internal SuccessfulGetNamesFromIDsResult(Reader reader) : base(reader)
		{
			this.namedProperties = reader.ReadSizeAndNamedPropertyArray();
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x000281F7 File Offset: 0x000263F7
		public NamedProperty[] NamedProperties
		{
			get
			{
				return this.namedProperties;
			}
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x000281FF File Offset: 0x000263FF
		internal static SuccessfulGetNamesFromIDsResult Parse(Reader reader)
		{
			return new SuccessfulGetNamesFromIDsResult(reader);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00028207 File Offset: 0x00026407
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteCountedNamedProperties(this.namedProperties);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0002821C File Offset: 0x0002641C
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			if (this.namedProperties != null)
			{
				stringBuilder.Append(" Names=[");
				Util.AppendToString<NamedProperty>(stringBuilder, this.namedProperties);
				stringBuilder.Append("]");
			}
		}

		// Token: 0x040006F7 RID: 1783
		private readonly NamedProperty[] namedProperties;
	}
}
