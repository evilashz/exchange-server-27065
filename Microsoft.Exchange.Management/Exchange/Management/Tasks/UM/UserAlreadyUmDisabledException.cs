using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011AC RID: 4524
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UserAlreadyUmDisabledException : LocalizedException
	{
		// Token: 0x0600B84A RID: 47178 RVA: 0x002A4688 File Offset: 0x002A2888
		public UserAlreadyUmDisabledException(string s) : base(Strings.ExceptionUserAlreadyUmDisabled(s))
		{
			this.s = s;
		}

		// Token: 0x0600B84B RID: 47179 RVA: 0x002A469D File Offset: 0x002A289D
		public UserAlreadyUmDisabledException(string s, Exception innerException) : base(Strings.ExceptionUserAlreadyUmDisabled(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B84C RID: 47180 RVA: 0x002A46B3 File Offset: 0x002A28B3
		protected UserAlreadyUmDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B84D RID: 47181 RVA: 0x002A46DD File Offset: 0x002A28DD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A17 RID: 14871
		// (get) Token: 0x0600B84E RID: 47182 RVA: 0x002A46F8 File Offset: 0x002A28F8
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006432 RID: 25650
		private readonly string s;
	}
}
