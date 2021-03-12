using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001030 RID: 4144
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoSmtpAddressForRecipientIdException : LocalizedException
	{
		// Token: 0x0600AFA8 RID: 44968 RVA: 0x00294AF2 File Offset: 0x00292CF2
		public NoSmtpAddressForRecipientIdException(string recipId) : base(Strings.NoSmtpAddressForRecipientId(recipId))
		{
			this.recipId = recipId;
		}

		// Token: 0x0600AFA9 RID: 44969 RVA: 0x00294B07 File Offset: 0x00292D07
		public NoSmtpAddressForRecipientIdException(string recipId, Exception innerException) : base(Strings.NoSmtpAddressForRecipientId(recipId), innerException)
		{
			this.recipId = recipId;
		}

		// Token: 0x0600AFAA RID: 44970 RVA: 0x00294B1D File Offset: 0x00292D1D
		protected NoSmtpAddressForRecipientIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipId = (string)info.GetValue("recipId", typeof(string));
		}

		// Token: 0x0600AFAB RID: 44971 RVA: 0x00294B47 File Offset: 0x00292D47
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipId", this.recipId);
		}

		// Token: 0x17003805 RID: 14341
		// (get) Token: 0x0600AFAC RID: 44972 RVA: 0x00294B62 File Offset: 0x00292D62
		public string RecipId
		{
			get
			{
				return this.recipId;
			}
		}

		// Token: 0x0400616B RID: 24939
		private readonly string recipId;
	}
}
