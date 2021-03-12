using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FBC RID: 4028
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class VerificationCodeUnmatchException : LocalizedException
	{
		// Token: 0x0600AD83 RID: 44419 RVA: 0x00291CF7 File Offset: 0x0028FEF7
		public VerificationCodeUnmatchException(string passcode) : base(Strings.ErrorVerificationCodeUnmatch(passcode))
		{
			this.passcode = passcode;
		}

		// Token: 0x0600AD84 RID: 44420 RVA: 0x00291D0C File Offset: 0x0028FF0C
		public VerificationCodeUnmatchException(string passcode, Exception innerException) : base(Strings.ErrorVerificationCodeUnmatch(passcode), innerException)
		{
			this.passcode = passcode;
		}

		// Token: 0x0600AD85 RID: 44421 RVA: 0x00291D22 File Offset: 0x0028FF22
		protected VerificationCodeUnmatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.passcode = (string)info.GetValue("passcode", typeof(string));
		}

		// Token: 0x0600AD86 RID: 44422 RVA: 0x00291D4C File Offset: 0x0028FF4C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("passcode", this.passcode);
		}

		// Token: 0x170037B0 RID: 14256
		// (get) Token: 0x0600AD87 RID: 44423 RVA: 0x00291D67 File Offset: 0x0028FF67
		public string Passcode
		{
			get
			{
				return this.passcode;
			}
		}

		// Token: 0x04006116 RID: 24854
		private readonly string passcode;
	}
}
