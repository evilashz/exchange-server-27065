using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E0A RID: 3594
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DomainUsersGroupNotFoundException : LocalizedException
	{
		// Token: 0x0600A52D RID: 42285 RVA: 0x0028587D File Offset: 0x00283A7D
		public DomainUsersGroupNotFoundException(string sid) : base(Strings.DomainUsersGroupNotFoundException(sid))
		{
			this.sid = sid;
		}

		// Token: 0x0600A52E RID: 42286 RVA: 0x00285892 File Offset: 0x00283A92
		public DomainUsersGroupNotFoundException(string sid, Exception innerException) : base(Strings.DomainUsersGroupNotFoundException(sid), innerException)
		{
			this.sid = sid;
		}

		// Token: 0x0600A52F RID: 42287 RVA: 0x002858A8 File Offset: 0x00283AA8
		protected DomainUsersGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sid = (string)info.GetValue("sid", typeof(string));
		}

		// Token: 0x0600A530 RID: 42288 RVA: 0x002858D2 File Offset: 0x00283AD2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sid", this.sid);
		}

		// Token: 0x17003622 RID: 13858
		// (get) Token: 0x0600A531 RID: 42289 RVA: 0x002858ED File Offset: 0x00283AED
		public string Sid
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x04005F88 RID: 24456
		private readonly string sid;
	}
}
