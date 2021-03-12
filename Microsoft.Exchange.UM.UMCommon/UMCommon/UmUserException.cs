using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001B2 RID: 434
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UmUserException : LocalizedException
	{
		// Token: 0x06000EA6 RID: 3750 RVA: 0x000355F1 File Offset: 0x000337F1
		public UmUserException(string exceptionText) : base(Strings.UmUserException(exceptionText))
		{
			this.exceptionText = exceptionText;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00035606 File Offset: 0x00033806
		public UmUserException(string exceptionText, Exception innerException) : base(Strings.UmUserException(exceptionText), innerException)
		{
			this.exceptionText = exceptionText;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003561C File Offset: 0x0003381C
		protected UmUserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exceptionText = (string)info.GetValue("exceptionText", typeof(string));
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00035646 File Offset: 0x00033846
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exceptionText", this.exceptionText);
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00035661 File Offset: 0x00033861
		public string ExceptionText
		{
			get
			{
				return this.exceptionText;
			}
		}

		// Token: 0x0400078E RID: 1934
		private readonly string exceptionText;
	}
}
