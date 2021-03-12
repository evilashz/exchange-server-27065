using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011C3 RID: 4547
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DialPlanChangeException : LocalizedException
	{
		// Token: 0x0600B8B9 RID: 47289 RVA: 0x002A5031 File Offset: 0x002A3231
		public DialPlanChangeException(string s) : base(Strings.DialPlanChangeException(s))
		{
			this.s = s;
		}

		// Token: 0x0600B8BA RID: 47290 RVA: 0x002A5046 File Offset: 0x002A3246
		public DialPlanChangeException(string s, Exception innerException) : base(Strings.DialPlanChangeException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B8BB RID: 47291 RVA: 0x002A505C File Offset: 0x002A325C
		protected DialPlanChangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B8BC RID: 47292 RVA: 0x002A5086 File Offset: 0x002A3286
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A2A RID: 14890
		// (get) Token: 0x0600B8BD RID: 47293 RVA: 0x002A50A1 File Offset: 0x002A32A1
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006445 RID: 25669
		private readonly string s;
	}
}
