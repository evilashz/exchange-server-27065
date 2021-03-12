using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001B6 RID: 438
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeServerNotValidException : ExchangeServerNotFoundException
	{
		// Token: 0x06000EBA RID: 3770 RVA: 0x000357D1 File Offset: 0x000339D1
		public ExchangeServerNotValidException(string name) : base(Strings.ExceptionExchangeServerNotValid(name))
		{
			this.name = name;
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x000357EB File Offset: 0x000339EB
		public ExchangeServerNotValidException(string name, Exception innerException) : base(Strings.ExceptionExchangeServerNotValid(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00035806 File Offset: 0x00033A06
		protected ExchangeServerNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00035830 File Offset: 0x00033A30
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x0003584B File Offset: 0x00033A4B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000792 RID: 1938
		private readonly string name;
	}
}
