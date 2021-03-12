using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011DB RID: 4571
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DefaultAutoAttendantInDialPlanException : LocalizedException
	{
		// Token: 0x0600B923 RID: 47395 RVA: 0x002A575F File Offset: 0x002A395F
		public DefaultAutoAttendantInDialPlanException(string s) : base(Strings.DefaultAutoAttendantInDialPlanException(s))
		{
			this.s = s;
		}

		// Token: 0x0600B924 RID: 47396 RVA: 0x002A5774 File Offset: 0x002A3974
		public DefaultAutoAttendantInDialPlanException(string s, Exception innerException) : base(Strings.DefaultAutoAttendantInDialPlanException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B925 RID: 47397 RVA: 0x002A578A File Offset: 0x002A398A
		protected DefaultAutoAttendantInDialPlanException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B926 RID: 47398 RVA: 0x002A57B4 File Offset: 0x002A39B4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A34 RID: 14900
		// (get) Token: 0x0600B927 RID: 47399 RVA: 0x002A57CF File Offset: 0x002A39CF
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x0400644F RID: 25679
		private readonly string s;
	}
}
