using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011DE RID: 4574
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DialPlanNotFoundException : LocalizedException
	{
		// Token: 0x0600B935 RID: 47413 RVA: 0x002A59BE File Offset: 0x002A3BBE
		public DialPlanNotFoundException(string s) : base(Strings.ExceptionDialPlanNotFound(s))
		{
			this.s = s;
		}

		// Token: 0x0600B936 RID: 47414 RVA: 0x002A59D3 File Offset: 0x002A3BD3
		public DialPlanNotFoundException(string s, Exception innerException) : base(Strings.ExceptionDialPlanNotFound(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B937 RID: 47415 RVA: 0x002A59E9 File Offset: 0x002A3BE9
		protected DialPlanNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B938 RID: 47416 RVA: 0x002A5A13 File Offset: 0x002A3C13
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A3A RID: 14906
		// (get) Token: 0x0600B939 RID: 47417 RVA: 0x002A5A2E File Offset: 0x002A3C2E
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006455 RID: 25685
		private readonly string s;
	}
}
