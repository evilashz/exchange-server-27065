using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x0200090A RID: 2314
	[Serializable]
	internal class TooManyResultsException : LocalizedException
	{
		// Token: 0x06005211 RID: 21009 RVA: 0x001534DD File Offset: 0x001516DD
		public TooManyResultsException(string identity, LocalizedString localizedMessage, Exception innerException, IEnumerable<string> matches) : base(localizedMessage, innerException)
		{
			this.identity = identity;
			this.matches = new List<string>(matches.Count<string>());
			this.matches.AddRange(matches);
		}

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x06005212 RID: 21010 RVA: 0x0015350D File Offset: 0x0015170D
		public string[] Matches
		{
			get
			{
				return this.matches.ToArray();
			}
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x0015351C File Offset: 0x0015171C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append(HybridStrings.ErrorTooManyMatchingResults(this.identity));
			stringBuilder.Append(":\r\n");
			foreach (string value in this.matches)
			{
				stringBuilder.AppendLine(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002FEB RID: 12267
		private List<string> matches;

		// Token: 0x04002FEC RID: 12268
		private readonly string identity;
	}
}
