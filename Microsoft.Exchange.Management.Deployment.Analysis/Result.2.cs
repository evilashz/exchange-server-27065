using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000035 RID: 53
	public class Result<T> : Result
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00007878 File Offset: 0x00005A78
		public Result(T value) : base(null, null, null, default(ExDateTime), default(ExDateTime))
		{
			this.value = value;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000078A8 File Offset: 0x00005AA8
		public Result(Exception exception) : base(null, null, exception, default(ExDateTime), default(ExDateTime))
		{
			this.value = default(T);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000078DC File Offset: 0x00005ADC
		public Result(Result<T> toCopy, AnalysisMember source, Result parent, ExDateTime startTime, ExDateTime stopTime) : base(source, parent, toCopy.Exception, startTime, stopTime)
		{
			this.value = toCopy.ValueOrDefault;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000078FC File Offset: 0x00005AFC
		public static Result<T> Default
		{
			get
			{
				return new Result<T>(default(T));
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00007917 File Offset: 0x00005B17
		public static Result<T> Failure
		{
			get
			{
				return new Result<T>(new FailureException());
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007923 File Offset: 0x00005B23
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

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007945 File Offset: 0x00005B45
		public override object ValueAsObject
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007954 File Offset: 0x00005B54
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

		// Token: 0x06000195 RID: 405 RVA: 0x00007979 File Offset: 0x00005B79
		public static Result<T> FailureBecause(string reason)
		{
			return new Result<T>(new FailureException(reason));
		}

		// Token: 0x04000085 RID: 133
		private readonly T value;
	}
}
