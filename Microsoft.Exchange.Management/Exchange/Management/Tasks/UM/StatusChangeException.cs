using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011C2 RID: 4546
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class StatusChangeException : LocalizedException
	{
		// Token: 0x0600B8B4 RID: 47284 RVA: 0x002A4FB9 File Offset: 0x002A31B9
		public StatusChangeException(string s) : base(Strings.StatusChangeException(s))
		{
			this.s = s;
		}

		// Token: 0x0600B8B5 RID: 47285 RVA: 0x002A4FCE File Offset: 0x002A31CE
		public StatusChangeException(string s, Exception innerException) : base(Strings.StatusChangeException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B8B6 RID: 47286 RVA: 0x002A4FE4 File Offset: 0x002A31E4
		protected StatusChangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B8B7 RID: 47287 RVA: 0x002A500E File Offset: 0x002A320E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A29 RID: 14889
		// (get) Token: 0x0600B8B8 RID: 47288 RVA: 0x002A5029 File Offset: 0x002A3229
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006444 RID: 25668
		private readonly string s;
	}
}
