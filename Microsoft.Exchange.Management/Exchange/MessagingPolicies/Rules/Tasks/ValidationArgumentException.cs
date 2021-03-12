using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B5D RID: 2909
	[Serializable]
	internal class ValidationArgumentException : ArgumentException
	{
		// Token: 0x06006AE6 RID: 27366 RVA: 0x001B65F6 File Offset: 0x001B47F6
		public ValidationArgumentException(LocalizedString localizedString, Exception innerException = null) : base(localizedString, innerException)
		{
			this.localizedString = localizedString;
		}

		// Token: 0x06006AE7 RID: 27367 RVA: 0x001B660C File Offset: 0x001B480C
		protected ValidationArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17002138 RID: 8504
		// (get) Token: 0x06006AE8 RID: 27368 RVA: 0x001B6616 File Offset: 0x001B4816
		public LocalizedString LocalizedString
		{
			get
			{
				return this.localizedString;
			}
		}

		// Token: 0x040036D5 RID: 14037
		private readonly LocalizedString localizedString;
	}
}
