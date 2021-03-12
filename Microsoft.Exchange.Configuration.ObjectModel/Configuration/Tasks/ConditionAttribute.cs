using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000048 RID: 72
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal abstract class ConditionAttribute : Attribute
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		public ConditionAttribute.EvaluationType EvaluationStrategy
		{
			get
			{
				return this.evaluationStrategy;
			}
			set
			{
				this.evaluationStrategy = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000C9F9 File Offset: 0x0000ABF9
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0000CA01 File Offset: 0x0000AC01
		public bool ExpectedResult
		{
			get
			{
				return this.expectedResult;
			}
			set
			{
				this.expectedResult = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000CA0A File Offset: 0x0000AC0A
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000CA12 File Offset: 0x0000AC12
		public LocalizedString FailureDescription
		{
			get
			{
				return this.failureDescription;
			}
			set
			{
				this.failureDescription = value;
			}
		}

		// Token: 0x040000D1 RID: 209
		private ConditionAttribute.EvaluationType evaluationStrategy = ConditionAttribute.EvaluationType.Contextual;

		// Token: 0x040000D2 RID: 210
		private bool expectedResult = true;

		// Token: 0x040000D3 RID: 211
		private LocalizedString failureDescription = Strings.GenericConditionFailure;

		// Token: 0x02000049 RID: 73
		public enum EvaluationType
		{
			// Token: 0x040000D5 RID: 213
			Global,
			// Token: 0x040000D6 RID: 214
			Local,
			// Token: 0x040000D7 RID: 215
			Contextual
		}
	}
}
