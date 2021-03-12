using System;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000044 RID: 68
	internal class Result<T> : Result
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x0000787B File Offset: 0x00005A7B
		public Result(T value)
		{
			this.value = value;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000788A File Offset: 0x00005A8A
		public Result(Exception exception) : base(exception)
		{
			this.value = default(T);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000078A0 File Offset: 0x00005AA0
		public Result(Func<T> valueFunction)
		{
			try
			{
				this.value = valueFunction();
			}
			catch (Exception exception)
			{
				this.value = default(T);
				base.Exception = exception;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000078E8 File Offset: 0x00005AE8
		public static Result<T> Default
		{
			get
			{
				return new Result<T>(default(T));
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00007903 File Offset: 0x00005B03
		public static Result<T> Failure
		{
			get
			{
				return new Result<T>(new FailureException());
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000790F File Offset: 0x00005B0F
		public T Value
		{
			get
			{
				if (base.HasException)
				{
					throw new AccessedFailedResultException(base.Source, base.Exception);
				}
				return this.value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00007931 File Offset: 0x00005B31
		public override object ValueAsObject
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00007940 File Offset: 0x00005B40
		public T ValueOrDefault
		{
			get
			{
				if (base.HasException)
				{
					return default(T);
				}
				return this.value;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007965 File Offset: 0x00005B65
		public static Result<T> FailureBecause(string reason)
		{
			return new Result<T>(new FailureException(reason));
		}

		// Token: 0x0400012D RID: 301
		private readonly T value;
	}
}
