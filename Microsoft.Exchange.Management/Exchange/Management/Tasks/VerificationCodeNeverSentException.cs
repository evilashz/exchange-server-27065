using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FBB RID: 4027
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class VerificationCodeNeverSentException : LocalizedException
	{
		// Token: 0x0600AD7E RID: 44414 RVA: 0x00291C7F File Offset: 0x0028FE7F
		public VerificationCodeNeverSentException(string phonenumber) : base(Strings.ErrorVerificationCodeNeverSent(phonenumber))
		{
			this.phonenumber = phonenumber;
		}

		// Token: 0x0600AD7F RID: 44415 RVA: 0x00291C94 File Offset: 0x0028FE94
		public VerificationCodeNeverSentException(string phonenumber, Exception innerException) : base(Strings.ErrorVerificationCodeNeverSent(phonenumber), innerException)
		{
			this.phonenumber = phonenumber;
		}

		// Token: 0x0600AD80 RID: 44416 RVA: 0x00291CAA File Offset: 0x0028FEAA
		protected VerificationCodeNeverSentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.phonenumber = (string)info.GetValue("phonenumber", typeof(string));
		}

		// Token: 0x0600AD81 RID: 44417 RVA: 0x00291CD4 File Offset: 0x0028FED4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("phonenumber", this.phonenumber);
		}

		// Token: 0x170037AF RID: 14255
		// (get) Token: 0x0600AD82 RID: 44418 RVA: 0x00291CEF File Offset: 0x0028FEEF
		public string Phonenumber
		{
			get
			{
				return this.phonenumber;
			}
		}

		// Token: 0x04006115 RID: 24853
		private readonly string phonenumber;
	}
}
