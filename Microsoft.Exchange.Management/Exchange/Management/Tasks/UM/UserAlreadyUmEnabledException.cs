using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011AB RID: 4523
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UserAlreadyUmEnabledException : LocalizedException
	{
		// Token: 0x0600B845 RID: 47173 RVA: 0x002A4610 File Offset: 0x002A2810
		public UserAlreadyUmEnabledException(string s) : base(Strings.ExceptionUserAlreadyUmEnabled(s))
		{
			this.s = s;
		}

		// Token: 0x0600B846 RID: 47174 RVA: 0x002A4625 File Offset: 0x002A2825
		public UserAlreadyUmEnabledException(string s, Exception innerException) : base(Strings.ExceptionUserAlreadyUmEnabled(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B847 RID: 47175 RVA: 0x002A463B File Offset: 0x002A283B
		protected UserAlreadyUmEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B848 RID: 47176 RVA: 0x002A4665 File Offset: 0x002A2865
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A16 RID: 14870
		// (get) Token: 0x0600B849 RID: 47177 RVA: 0x002A4680 File Offset: 0x002A2880
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006431 RID: 25649
		private readonly string s;
	}
}
