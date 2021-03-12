using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FC8 RID: 4040
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TransportRuleLimitTooSmallException : LocalizedException
	{
		// Token: 0x0600ADBE RID: 44478 RVA: 0x00292260 File Offset: 0x00290460
		public TransportRuleLimitTooSmallException(int newValue, int ruleCount) : base(Strings.ErrorTransportRuleLimitTooSmall(newValue, ruleCount))
		{
			this.newValue = newValue;
			this.ruleCount = ruleCount;
		}

		// Token: 0x0600ADBF RID: 44479 RVA: 0x0029227D File Offset: 0x0029047D
		public TransportRuleLimitTooSmallException(int newValue, int ruleCount, Exception innerException) : base(Strings.ErrorTransportRuleLimitTooSmall(newValue, ruleCount), innerException)
		{
			this.newValue = newValue;
			this.ruleCount = ruleCount;
		}

		// Token: 0x0600ADC0 RID: 44480 RVA: 0x0029229C File Offset: 0x0029049C
		protected TransportRuleLimitTooSmallException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.newValue = (int)info.GetValue("newValue", typeof(int));
			this.ruleCount = (int)info.GetValue("ruleCount", typeof(int));
		}

		// Token: 0x0600ADC1 RID: 44481 RVA: 0x002922F1 File Offset: 0x002904F1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("newValue", this.newValue);
			info.AddValue("ruleCount", this.ruleCount);
		}

		// Token: 0x170037BB RID: 14267
		// (get) Token: 0x0600ADC2 RID: 44482 RVA: 0x0029231D File Offset: 0x0029051D
		public int NewValue
		{
			get
			{
				return this.newValue;
			}
		}

		// Token: 0x170037BC RID: 14268
		// (get) Token: 0x0600ADC3 RID: 44483 RVA: 0x00292325 File Offset: 0x00290525
		public int RuleCount
		{
			get
			{
				return this.ruleCount;
			}
		}

		// Token: 0x04006121 RID: 24865
		private readonly int newValue;

		// Token: 0x04006122 RID: 24866
		private readonly int ruleCount;
	}
}
