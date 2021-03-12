using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Hygiene.Data.Directory;

namespace Microsoft.Forefront.Reporting.Common
{
	// Token: 0x02000015 RID: 21
	public class QueryCompiler
	{
		// Token: 0x0600003D RID: 61 RVA: 0x0000253C File Offset: 0x0000073C
		internal QueryCompiler(OnDemandQueryType queryType, string queryStringIn, PIIHashingDelegate piiHashingDelegate, LSHDelegate lshDelegate)
		{
			if (string.IsNullOrWhiteSpace(queryStringIn))
			{
				throw new InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode.EmptySearchDefinition);
			}
			this.QueryString = queryStringIn;
			this.Type = queryType;
			this.QueryTimeRange = new List<Tuple<DateTime, DateTime>>();
			if (piiHashingDelegate != null)
			{
				this.HashEnable = true;
				this.hashingDelegate = piiHashingDelegate;
				this.lshDelegate = lshDelegate;
			}
			else
			{
				this.HashEnable = false;
			}
			this.Compile();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000259F File Offset: 0x0000079F
		internal QueryCompiler(OnDemandQueryType queryType, string queryStringIn) : this(queryType, queryStringIn, null, null)
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000025AB File Offset: 0x000007AB
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000025B3 File Offset: 0x000007B3
		internal List<Tuple<DateTime, DateTime>> QueryTimeRange { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000025BC File Offset: 0x000007BC
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000025C4 File Offset: 0x000007C4
		internal string CompiledCode { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000025CD File Offset: 0x000007CD
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000025D5 File Offset: 0x000007D5
		internal string PreFilteringCode { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000025DE File Offset: 0x000007DE
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000025E6 File Offset: 0x000007E6
		internal string QueryString { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000025EF File Offset: 0x000007EF
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000025F7 File Offset: 0x000007F7
		internal bool HashEnable { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002600 File Offset: 0x00000800
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002608 File Offset: 0x00000808
		internal OnDemandQueryType Type { get; private set; }

		// Token: 0x0600004B RID: 75 RVA: 0x00002614 File Offset: 0x00000814
		internal static void ValidateQuery(Guid requestID, PropertyAccessDelegate propertyDelegate)
		{
			Guid a = (Guid)propertyDelegate(OnDemandQueryRequestSchema.TenantId);
			if (a == Constants.TenantIDForSystemCount)
			{
				return;
			}
			new QueryCompiler((OnDemandQueryType)propertyDelegate(OnDemandQueryRequestSchema.QueryType), (string)propertyDelegate(OnDemandQueryRequestSchema.QueryDefinition));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002666 File Offset: 0x00000866
		internal string GetPIIHash(string valueString)
		{
			if (this.HashEnable)
			{
				return this.hashingDelegate(valueString);
			}
			return valueString;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000267E File Offset: 0x0000087E
		internal string GetLSH(string valueString)
		{
			if (this.HashEnable)
			{
				return this.lshDelegate(valueString);
			}
			return valueString;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000026D8 File Offset: 0x000008D8
		private void Compile()
		{
			QueryGroupField queryGroupField = new QueryGroupField(this.QueryString, this);
			string arg = queryGroupField.Compile();
			switch (this.Type)
			{
			case OnDemandQueryType.DLP:
				this.CompiledCode = string.Format("if(CFRTools.IsDLP(tra_etr) && {0})", arg);
				goto IL_AA;
			case OnDemandQueryType.Rule:
				this.CompiledCode = string.Format("if(CFRTools.IsTransportRule(tra_etr) && {0})", arg);
				goto IL_AA;
			case OnDemandQueryType.AntiSpam:
				this.CompiledCode = string.Format("if(CFRTools.IsSpam(sfa_sum) && {0})", arg);
				goto IL_AA;
			case OnDemandQueryType.AntiVirus:
				this.CompiledCode = string.Format("if(CFRTools.IsMalware(ama_sum) && {0})", arg);
				goto IL_AA;
			}
			if (!queryGroupField.HasOptionalCriterion)
			{
				throw new InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode.DoesNotMeetMinimalQueryRequirement);
			}
			this.CompiledCode = string.Format("if{0}", arg);
			IL_AA:
			string arg2 = string.Join(" || ", from range in this.QueryTimeRange
			select string.Format("(originTimeTicks >= {0} && originTimeTicks <= {1})", range.Item1.Ticks, range.Item2.Ticks));
			this.PreFilteringCode = string.Format("if({0})", arg2);
		}

		// Token: 0x04000062 RID: 98
		internal const string MsgStatusHolderString = "%MsgStatus%";

		// Token: 0x04000063 RID: 99
		private PIIHashingDelegate hashingDelegate;

		// Token: 0x04000064 RID: 100
		private LSHDelegate lshDelegate;
	}
}
