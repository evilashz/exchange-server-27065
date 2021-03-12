using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000012 RID: 18
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FopePolicyRuleHasWordsThatExceedMaximumLengthException : LocalizedException
	{
		// Token: 0x0600037A RID: 890 RVA: 0x0000CBFD File Offset: 0x0000ADFD
		public FopePolicyRuleHasWordsThatExceedMaximumLengthException(int ruleId, int maxWordLength) : base(CoreStrings.FopePolicyRuleHasWordsThatExceedMaximumLength(ruleId, maxWordLength))
		{
			this.ruleId = ruleId;
			this.maxWordLength = maxWordLength;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000CC1A File Offset: 0x0000AE1A
		public FopePolicyRuleHasWordsThatExceedMaximumLengthException(int ruleId, int maxWordLength, Exception innerException) : base(CoreStrings.FopePolicyRuleHasWordsThatExceedMaximumLength(ruleId, maxWordLength), innerException)
		{
			this.ruleId = ruleId;
			this.maxWordLength = maxWordLength;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000CC38 File Offset: 0x0000AE38
		protected FopePolicyRuleHasWordsThatExceedMaximumLengthException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
			this.maxWordLength = (int)info.GetValue("maxWordLength", typeof(int));
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000CC8D File Offset: 0x0000AE8D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
			info.AddValue("maxWordLength", this.maxWordLength);
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000CCB9 File Offset: 0x0000AEB9
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000CCC1 File Offset: 0x0000AEC1
		public int MaxWordLength
		{
			get
			{
				return this.maxWordLength;
			}
		}

		// Token: 0x04000359 RID: 857
		private readonly int ruleId;

		// Token: 0x0400035A RID: 858
		private readonly int maxWordLength;
	}
}
