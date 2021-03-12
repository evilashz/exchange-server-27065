using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000010 RID: 16
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidOAuthLinkedAccountException : LocalizedException
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000063C8 File Offset: 0x000045C8
		public InvalidOAuthLinkedAccountException(string partnerApplication, string linkedAccount) : base(SecurityStrings.InvalidOAuthLinkedAccountException(partnerApplication, linkedAccount))
		{
			this.partnerApplication = partnerApplication;
			this.linkedAccount = linkedAccount;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000063E5 File Offset: 0x000045E5
		public InvalidOAuthLinkedAccountException(string partnerApplication, string linkedAccount, Exception innerException) : base(SecurityStrings.InvalidOAuthLinkedAccountException(partnerApplication, linkedAccount), innerException)
		{
			this.partnerApplication = partnerApplication;
			this.linkedAccount = linkedAccount;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00006404 File Offset: 0x00004604
		protected InvalidOAuthLinkedAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.partnerApplication = (string)info.GetValue("partnerApplication", typeof(string));
			this.linkedAccount = (string)info.GetValue("linkedAccount", typeof(string));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00006459 File Offset: 0x00004659
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("partnerApplication", this.partnerApplication);
			info.AddValue("linkedAccount", this.linkedAccount);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00006485 File Offset: 0x00004685
		public string PartnerApplication
		{
			get
			{
				return this.partnerApplication;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000648D File Offset: 0x0000468D
		public string LinkedAccount
		{
			get
			{
				return this.linkedAccount;
			}
		}

		// Token: 0x04000124 RID: 292
		private readonly string partnerApplication;

		// Token: 0x04000125 RID: 293
		private readonly string linkedAccount;
	}
}
