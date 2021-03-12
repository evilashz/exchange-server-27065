using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011C5 RID: 4549
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DialPlanAssociatedWithServerException : LocalizedException
	{
		// Token: 0x0600B8C2 RID: 47298 RVA: 0x002A50D8 File Offset: 0x002A32D8
		public DialPlanAssociatedWithServerException(string s) : base(Strings.DialPlanAssociatedWithServerException(s))
		{
			this.s = s;
		}

		// Token: 0x0600B8C3 RID: 47299 RVA: 0x002A50ED File Offset: 0x002A32ED
		public DialPlanAssociatedWithServerException(string s, Exception innerException) : base(Strings.DialPlanAssociatedWithServerException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B8C4 RID: 47300 RVA: 0x002A5103 File Offset: 0x002A3303
		protected DialPlanAssociatedWithServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B8C5 RID: 47301 RVA: 0x002A512D File Offset: 0x002A332D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A2B RID: 14891
		// (get) Token: 0x0600B8C6 RID: 47302 RVA: 0x002A5148 File Offset: 0x002A3348
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006446 RID: 25670
		private readonly string s;
	}
}
