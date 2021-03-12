using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011C7 RID: 4551
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DialPlanAssociatedWithAutoAttendantException : LocalizedException
	{
		// Token: 0x0600B8CB RID: 47307 RVA: 0x002A517F File Offset: 0x002A337F
		public DialPlanAssociatedWithAutoAttendantException(string s) : base(Strings.DialPlanAssociatedWithAutoAttendantException(s))
		{
			this.s = s;
		}

		// Token: 0x0600B8CC RID: 47308 RVA: 0x002A5194 File Offset: 0x002A3394
		public DialPlanAssociatedWithAutoAttendantException(string s, Exception innerException) : base(Strings.DialPlanAssociatedWithAutoAttendantException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B8CD RID: 47309 RVA: 0x002A51AA File Offset: 0x002A33AA
		protected DialPlanAssociatedWithAutoAttendantException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B8CE RID: 47310 RVA: 0x002A51D4 File Offset: 0x002A33D4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A2C RID: 14892
		// (get) Token: 0x0600B8CF RID: 47311 RVA: 0x002A51EF File Offset: 0x002A33EF
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006447 RID: 25671
		private readonly string s;
	}
}
