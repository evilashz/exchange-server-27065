using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011B9 RID: 4537
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutoAttendantAlreadDisabledException : LocalizedException
	{
		// Token: 0x0600B886 RID: 47238 RVA: 0x002A4B2B File Offset: 0x002A2D2B
		public AutoAttendantAlreadDisabledException(string s) : base(Strings.AutoAttendantAlreadDisabledException(s))
		{
			this.s = s;
		}

		// Token: 0x0600B887 RID: 47239 RVA: 0x002A4B40 File Offset: 0x002A2D40
		public AutoAttendantAlreadDisabledException(string s, Exception innerException) : base(Strings.AutoAttendantAlreadDisabledException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B888 RID: 47240 RVA: 0x002A4B56 File Offset: 0x002A2D56
		protected AutoAttendantAlreadDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B889 RID: 47241 RVA: 0x002A4B80 File Offset: 0x002A2D80
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A1F RID: 14879
		// (get) Token: 0x0600B88A RID: 47242 RVA: 0x002A4B9B File Offset: 0x002A2D9B
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x0400643A RID: 25658
		private readonly string s;
	}
}
