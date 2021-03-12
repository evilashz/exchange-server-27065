using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000550 RID: 1360
	[Serializable]
	public sealed class ReplicationCheckResult : ConfigurableObject, IEquatable<ReplicationCheckResult>
	{
		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x000C448F File Offset: 0x000C268F
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ReplicationCheckResult.schema;
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06003077 RID: 12407 RVA: 0x000C4496 File Offset: 0x000C2696
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000C449D File Offset: 0x000C269D
		internal ReplicationCheckResult(ReplicationCheckResultEnum result) : base(new SimpleProviderPropertyBag())
		{
			this.Value = result;
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06003079 RID: 12409 RVA: 0x000C44B1 File Offset: 0x000C26B1
		// (set) Token: 0x0600307A RID: 12410 RVA: 0x000C44C3 File Offset: 0x000C26C3
		public ReplicationCheckResultEnum Value
		{
			get
			{
				return (ReplicationCheckResultEnum)this[ReplicationCheckResultSchema.Value];
			}
			private set
			{
				this[ReplicationCheckResultSchema.Value] = value;
			}
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000C44D8 File Offset: 0x000C26D8
		public override string ToString()
		{
			string result = string.Empty;
			switch (this.Value)
			{
			case ReplicationCheckResultEnum.Undefined:
				result = Strings.ReplicationCheckResultUndefined;
				break;
			case ReplicationCheckResultEnum.Passed:
				result = Strings.ReplicationCheckResultPassed;
				break;
			case ReplicationCheckResultEnum.Warning:
				result = Strings.ReplicationCheckResultWarning;
				break;
			case ReplicationCheckResultEnum.Failed:
				result = Strings.ReplicationCheckResultFailed;
				break;
			default:
				throw new ReplicationCheckResultToStringCaseNotHandled(this.Value);
			}
			return result;
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000C454B File Offset: 0x000C274B
		bool IEquatable<ReplicationCheckResult>.Equals(ReplicationCheckResult other)
		{
			return this.Value == other.Value;
		}

		// Token: 0x04002279 RID: 8825
		private static ReplicationCheckResultSchema schema = ObjectSchema.GetInstance<ReplicationCheckResultSchema>();
	}
}
