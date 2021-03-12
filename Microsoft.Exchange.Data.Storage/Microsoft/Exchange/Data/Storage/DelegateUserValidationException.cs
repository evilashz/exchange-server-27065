using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000729 RID: 1833
	[Serializable]
	public class DelegateUserValidationException : StoragePermanentException
	{
		// Token: 0x060047C7 RID: 18375 RVA: 0x00130519 File Offset: 0x0012E719
		public DelegateUserValidationException(LocalizedString message, DelegateValidationProblem problem) : base(message)
		{
			EnumValidator.ThrowIfInvalid<DelegateValidationProblem>(problem, "problem");
			this.problem = problem;
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x00130534 File Offset: 0x0012E734
		protected DelegateUserValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.problem = (DelegateValidationProblem)info.GetValue("problem", typeof(DelegateValidationProblem));
		}

		// Token: 0x060047C9 RID: 18377 RVA: 0x0013055E File Offset: 0x0012E75E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("problem", this.problem);
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x060047CA RID: 18378 RVA: 0x0013057E File Offset: 0x0012E77E
		public DelegateValidationProblem Problem
		{
			get
			{
				return this.problem;
			}
		}

		// Token: 0x0400272F RID: 10031
		private const string ProblemLabel = "problem";

		// Token: 0x04002730 RID: 10032
		private DelegateValidationProblem problem;
	}
}
