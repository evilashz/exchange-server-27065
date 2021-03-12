using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200102E RID: 4142
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MoreThanOneRecipientForRecipientIdException : LocalizedException
	{
		// Token: 0x0600AF9E RID: 44958 RVA: 0x00294A02 File Offset: 0x00292C02
		public MoreThanOneRecipientForRecipientIdException(string recipId) : base(Strings.MoreThanOneRecipientForRecipientId(recipId))
		{
			this.recipId = recipId;
		}

		// Token: 0x0600AF9F RID: 44959 RVA: 0x00294A17 File Offset: 0x00292C17
		public MoreThanOneRecipientForRecipientIdException(string recipId, Exception innerException) : base(Strings.MoreThanOneRecipientForRecipientId(recipId), innerException)
		{
			this.recipId = recipId;
		}

		// Token: 0x0600AFA0 RID: 44960 RVA: 0x00294A2D File Offset: 0x00292C2D
		protected MoreThanOneRecipientForRecipientIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipId = (string)info.GetValue("recipId", typeof(string));
		}

		// Token: 0x0600AFA1 RID: 44961 RVA: 0x00294A57 File Offset: 0x00292C57
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipId", this.recipId);
		}

		// Token: 0x17003803 RID: 14339
		// (get) Token: 0x0600AFA2 RID: 44962 RVA: 0x00294A72 File Offset: 0x00292C72
		public string RecipId
		{
			get
			{
				return this.recipId;
			}
		}

		// Token: 0x04006169 RID: 24937
		private readonly string recipId;
	}
}
