using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000021 RID: 33
	internal interface IFailureEntry : IDocEntry, IEquatable<IDocEntry>, IEquatable<IFailureEntry>
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000092 RID: 146
		IIdentity ItemId { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000093 RID: 147
		EvaluationErrors ErrorCode { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000094 RID: 148
		LocalizedString ErrorDescription { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000095 RID: 149
		string AdditionalInfo { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000096 RID: 150
		bool IsPartiallyIndexed { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000097 RID: 151
		DateTime? LastAttemptTime { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000098 RID: 152
		int AttemptCount { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000099 RID: 153
		bool IsPermanentFailure { get; }
	}
}
