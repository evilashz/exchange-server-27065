using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001180 RID: 4480
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidIdentityException : LocalizedException
	{
		// Token: 0x0600B66E RID: 46702 RVA: 0x0029FD5A File Offset: 0x0029DF5A
		public InvalidIdentityException(string identity, string format) : base(Strings.InvalidIdentity(identity, format))
		{
			this.identity = identity;
			this.format = format;
		}

		// Token: 0x0600B66F RID: 46703 RVA: 0x0029FD77 File Offset: 0x0029DF77
		public InvalidIdentityException(string identity, string format, Exception innerException) : base(Strings.InvalidIdentity(identity, format), innerException)
		{
			this.identity = identity;
			this.format = format;
		}

		// Token: 0x0600B670 RID: 46704 RVA: 0x0029FD98 File Offset: 0x0029DF98
		protected InvalidIdentityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
			this.format = (string)info.GetValue("format", typeof(string));
		}

		// Token: 0x0600B671 RID: 46705 RVA: 0x0029FDED File Offset: 0x0029DFED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
			info.AddValue("format", this.format);
		}

		// Token: 0x1700398B RID: 14731
		// (get) Token: 0x0600B672 RID: 46706 RVA: 0x0029FE19 File Offset: 0x0029E019
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x1700398C RID: 14732
		// (get) Token: 0x0600B673 RID: 46707 RVA: 0x0029FE21 File Offset: 0x0029E021
		public string Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x040062F1 RID: 25329
		private readonly string identity;

		// Token: 0x040062F2 RID: 25330
		private readonly string format;
	}
}
