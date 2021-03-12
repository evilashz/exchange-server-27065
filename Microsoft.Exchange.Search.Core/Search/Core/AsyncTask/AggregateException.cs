using System;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.AsyncTask
{
	// Token: 0x02000048 RID: 72
	internal class AggregateException : OperationFailedException
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00002B60 File Offset: 0x00000D60
		internal AggregateException(params ComponentException[] innerExceptions)
		{
			Util.ThrowOnNullOrEmptyArgument<ComponentException>(innerExceptions, "innerExceptions");
			this.innerExceptions = new ReadOnlyCollection<ComponentException>(innerExceptions);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00002B7F File Offset: 0x00000D7F
		internal ReadOnlyCollection<ComponentException> InnerExceptions
		{
			get
			{
				return this.innerExceptions;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00002B88 File Offset: 0x00000D88
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendLine();
			for (int i = 0; i < this.innerExceptions.Count; i++)
			{
				stringBuilder.AppendFormat(" (Inner Exception #{0}) {1} <---", i, this.innerExceptions[i].ToString());
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400008D RID: 141
		private readonly ReadOnlyCollection<ComponentException> innerExceptions;
	}
}
