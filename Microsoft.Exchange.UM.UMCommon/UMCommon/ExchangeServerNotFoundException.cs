using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001B5 RID: 437
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeServerNotFoundException : LocalizedException
	{
		// Token: 0x06000EB5 RID: 3765 RVA: 0x00035759 File Offset: 0x00033959
		public ExchangeServerNotFoundException(string s) : base(Strings.ExceptionExchangeServerNotFound(s))
		{
			this.s = s;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0003576E File Offset: 0x0003396E
		public ExchangeServerNotFoundException(string s, Exception innerException) : base(Strings.ExceptionExchangeServerNotFound(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00035784 File Offset: 0x00033984
		protected ExchangeServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x000357AE File Offset: 0x000339AE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x000357C9 File Offset: 0x000339C9
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04000791 RID: 1937
		private readonly string s;
	}
}
