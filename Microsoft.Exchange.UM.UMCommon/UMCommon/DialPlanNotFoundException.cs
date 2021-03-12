using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001B7 RID: 439
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DialPlanNotFoundException : LocalizedException
	{
		// Token: 0x06000EBF RID: 3775 RVA: 0x00035853 File Offset: 0x00033A53
		public DialPlanNotFoundException(string s) : base(Strings.DialPlanNotFound(s))
		{
			this.s = s;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00035868 File Offset: 0x00033A68
		public DialPlanNotFoundException(string s, Exception innerException) : base(Strings.DialPlanNotFound(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003587E File Offset: 0x00033A7E
		protected DialPlanNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x000358A8 File Offset: 0x00033AA8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x000358C3 File Offset: 0x00033AC3
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04000793 RID: 1939
		private readonly string s;
	}
}
