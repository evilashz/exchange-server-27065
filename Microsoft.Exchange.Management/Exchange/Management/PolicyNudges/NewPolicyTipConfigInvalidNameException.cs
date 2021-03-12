using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.PolicyNudges
{
	// Token: 0x02001177 RID: 4471
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NewPolicyTipConfigInvalidNameException : LocalizedException
	{
		// Token: 0x0600B63B RID: 46651 RVA: 0x0029F726 File Offset: 0x0029D926
		public NewPolicyTipConfigInvalidNameException(string supportedLocalesString, string supportedActionsString) : base(Strings.NewPolicyTipConfigInvalidName(supportedLocalesString, supportedActionsString))
		{
			this.supportedLocalesString = supportedLocalesString;
			this.supportedActionsString = supportedActionsString;
		}

		// Token: 0x0600B63C RID: 46652 RVA: 0x0029F743 File Offset: 0x0029D943
		public NewPolicyTipConfigInvalidNameException(string supportedLocalesString, string supportedActionsString, Exception innerException) : base(Strings.NewPolicyTipConfigInvalidName(supportedLocalesString, supportedActionsString), innerException)
		{
			this.supportedLocalesString = supportedLocalesString;
			this.supportedActionsString = supportedActionsString;
		}

		// Token: 0x0600B63D RID: 46653 RVA: 0x0029F764 File Offset: 0x0029D964
		protected NewPolicyTipConfigInvalidNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.supportedLocalesString = (string)info.GetValue("supportedLocalesString", typeof(string));
			this.supportedActionsString = (string)info.GetValue("supportedActionsString", typeof(string));
		}

		// Token: 0x0600B63E RID: 46654 RVA: 0x0029F7B9 File Offset: 0x0029D9B9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("supportedLocalesString", this.supportedLocalesString);
			info.AddValue("supportedActionsString", this.supportedActionsString);
		}

		// Token: 0x1700397C RID: 14716
		// (get) Token: 0x0600B63F RID: 46655 RVA: 0x0029F7E5 File Offset: 0x0029D9E5
		public string SupportedLocalesString
		{
			get
			{
				return this.supportedLocalesString;
			}
		}

		// Token: 0x1700397D RID: 14717
		// (get) Token: 0x0600B640 RID: 46656 RVA: 0x0029F7ED File Offset: 0x0029D9ED
		public string SupportedActionsString
		{
			get
			{
				return this.supportedActionsString;
			}
		}

		// Token: 0x040062E2 RID: 25314
		private readonly string supportedLocalesString;

		// Token: 0x040062E3 RID: 25315
		private readonly string supportedActionsString;
	}
}
