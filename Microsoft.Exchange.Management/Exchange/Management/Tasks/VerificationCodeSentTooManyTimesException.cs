using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FBA RID: 4026
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class VerificationCodeSentTooManyTimesException : LocalizedException
	{
		// Token: 0x0600AD79 RID: 44409 RVA: 0x00291C07 File Offset: 0x0028FE07
		public VerificationCodeSentTooManyTimesException(string phonenumber) : base(Strings.ErrorVerificationCodeSentTooManyTimes(phonenumber))
		{
			this.phonenumber = phonenumber;
		}

		// Token: 0x0600AD7A RID: 44410 RVA: 0x00291C1C File Offset: 0x0028FE1C
		public VerificationCodeSentTooManyTimesException(string phonenumber, Exception innerException) : base(Strings.ErrorVerificationCodeSentTooManyTimes(phonenumber), innerException)
		{
			this.phonenumber = phonenumber;
		}

		// Token: 0x0600AD7B RID: 44411 RVA: 0x00291C32 File Offset: 0x0028FE32
		protected VerificationCodeSentTooManyTimesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.phonenumber = (string)info.GetValue("phonenumber", typeof(string));
		}

		// Token: 0x0600AD7C RID: 44412 RVA: 0x00291C5C File Offset: 0x0028FE5C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("phonenumber", this.phonenumber);
		}

		// Token: 0x170037AE RID: 14254
		// (get) Token: 0x0600AD7D RID: 44413 RVA: 0x00291C77 File Offset: 0x0028FE77
		public string Phonenumber
		{
			get
			{
				return this.phonenumber;
			}
		}

		// Token: 0x04006114 RID: 24852
		private readonly string phonenumber;
	}
}
