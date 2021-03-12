using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000777 RID: 1911
	[Serializable]
	public class RuleParseException : StoragePermanentException
	{
		// Token: 0x060048C0 RID: 18624 RVA: 0x00131755 File Offset: 0x0012F955
		public RuleParseException(string message) : base(ServerStrings.RuleParseError(message))
		{
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x00131763 File Offset: 0x0012F963
		public RuleParseException(LocalizedString message, Exception e) : base(message, e)
		{
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x0013176D File Offset: 0x0012F96D
		protected RuleParseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
