using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011B8 RID: 4536
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutoAttendantAlreadEnabledException : LocalizedException
	{
		// Token: 0x0600B881 RID: 47233 RVA: 0x002A4AB3 File Offset: 0x002A2CB3
		public AutoAttendantAlreadEnabledException(string s) : base(Strings.AutoAttendantAlreadEnabledException(s))
		{
			this.s = s;
		}

		// Token: 0x0600B882 RID: 47234 RVA: 0x002A4AC8 File Offset: 0x002A2CC8
		public AutoAttendantAlreadEnabledException(string s, Exception innerException) : base(Strings.AutoAttendantAlreadEnabledException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B883 RID: 47235 RVA: 0x002A4ADE File Offset: 0x002A2CDE
		protected AutoAttendantAlreadEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B884 RID: 47236 RVA: 0x002A4B08 File Offset: 0x002A2D08
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A1E RID: 14878
		// (get) Token: 0x0600B885 RID: 47237 RVA: 0x002A4B23 File Offset: 0x002A2D23
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006439 RID: 25657
		private readonly string s;
	}
}
