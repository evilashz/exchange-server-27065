using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000251 RID: 593
	internal sealed class SuccessfulGetEffectiveRightsResult : RopResult
	{
		// Token: 0x06000CD1 RID: 3281 RVA: 0x00027F73 File Offset: 0x00026173
		internal SuccessfulGetEffectiveRightsResult(Rights effectiveRights) : base(RopId.GetEffectiveRights, ErrorCode.None, null)
		{
			this.effectiveRights = effectiveRights;
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00027F86 File Offset: 0x00026186
		internal SuccessfulGetEffectiveRightsResult(Reader reader) : base(reader)
		{
			this.effectiveRights = (Rights)reader.ReadUInt32();
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00027F9B File Offset: 0x0002619B
		internal Rights EffectiveRights
		{
			get
			{
				return this.effectiveRights;
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00027FA3 File Offset: 0x000261A3
		internal static SuccessfulGetEffectiveRightsResult Parse(Reader reader)
		{
			return new SuccessfulGetEffectiveRightsResult(reader);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00027FAB File Offset: 0x000261AB
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32((uint)this.effectiveRights);
		}

		// Token: 0x040006F2 RID: 1778
		private Rights effectiveRights;
	}
}
