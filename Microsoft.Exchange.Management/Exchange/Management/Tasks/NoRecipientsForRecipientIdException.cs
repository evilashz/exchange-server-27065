using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200102F RID: 4143
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoRecipientsForRecipientIdException : LocalizedException
	{
		// Token: 0x0600AFA3 RID: 44963 RVA: 0x00294A7A File Offset: 0x00292C7A
		public NoRecipientsForRecipientIdException(string recipId) : base(Strings.NoRecipientsForRecipientId(recipId))
		{
			this.recipId = recipId;
		}

		// Token: 0x0600AFA4 RID: 44964 RVA: 0x00294A8F File Offset: 0x00292C8F
		public NoRecipientsForRecipientIdException(string recipId, Exception innerException) : base(Strings.NoRecipientsForRecipientId(recipId), innerException)
		{
			this.recipId = recipId;
		}

		// Token: 0x0600AFA5 RID: 44965 RVA: 0x00294AA5 File Offset: 0x00292CA5
		protected NoRecipientsForRecipientIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipId = (string)info.GetValue("recipId", typeof(string));
		}

		// Token: 0x0600AFA6 RID: 44966 RVA: 0x00294ACF File Offset: 0x00292CCF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipId", this.recipId);
		}

		// Token: 0x17003804 RID: 14340
		// (get) Token: 0x0600AFA7 RID: 44967 RVA: 0x00294AEA File Offset: 0x00292CEA
		public string RecipId
		{
			get
			{
				return this.recipId;
			}
		}

		// Token: 0x0400616A RID: 24938
		private readonly string recipId;
	}
}
