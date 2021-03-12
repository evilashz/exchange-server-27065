using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BB7 RID: 2999
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class JunkEmailValidationException : StoragePermanentException
	{
		// Token: 0x06006B1E RID: 27422 RVA: 0x001C8802 File Offset: 0x001C6A02
		public JunkEmailValidationException(string value, JunkEmailCollection.ValidationProblem problem) : base(ServerStrings.JunkEmailValidationError(value))
		{
			EnumValidator.ThrowIfInvalid<JunkEmailCollection.ValidationProblem>(problem, "problem");
			this.problem = problem;
		}

		// Token: 0x17001D24 RID: 7460
		// (get) Token: 0x06006B1F RID: 27423 RVA: 0x001C8822 File Offset: 0x001C6A22
		public JunkEmailCollection.ValidationProblem Problem
		{
			get
			{
				return this.problem;
			}
		}

		// Token: 0x04003D19 RID: 15641
		private JunkEmailCollection.ValidationProblem problem;
	}
}
