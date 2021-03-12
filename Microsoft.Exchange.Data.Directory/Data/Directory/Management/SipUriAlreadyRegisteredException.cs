using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000AD1 RID: 2769
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SipUriAlreadyRegisteredException : LocalizedException
	{
		// Token: 0x060080DA RID: 32986 RVA: 0x001A5FCD File Offset: 0x001A41CD
		public SipUriAlreadyRegisteredException(string sipUri, string user) : base(DirectoryStrings.SipUriAlreadyRegistered(sipUri, user))
		{
			this.sipUri = sipUri;
			this.user = user;
		}

		// Token: 0x060080DB RID: 32987 RVA: 0x001A5FEA File Offset: 0x001A41EA
		public SipUriAlreadyRegisteredException(string sipUri, string user, Exception innerException) : base(DirectoryStrings.SipUriAlreadyRegistered(sipUri, user), innerException)
		{
			this.sipUri = sipUri;
			this.user = user;
		}

		// Token: 0x060080DC RID: 32988 RVA: 0x001A6008 File Offset: 0x001A4208
		protected SipUriAlreadyRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sipUri = (string)info.GetValue("sipUri", typeof(string));
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x060080DD RID: 32989 RVA: 0x001A605D File Offset: 0x001A425D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sipUri", this.sipUri);
			info.AddValue("user", this.user);
		}

		// Token: 0x17002EED RID: 12013
		// (get) Token: 0x060080DE RID: 32990 RVA: 0x001A6089 File Offset: 0x001A4289
		public string SipUri
		{
			get
			{
				return this.sipUri;
			}
		}

		// Token: 0x17002EEE RID: 12014
		// (get) Token: 0x060080DF RID: 32991 RVA: 0x001A6091 File Offset: 0x001A4291
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x040055C7 RID: 21959
		private readonly string sipUri;

		// Token: 0x040055C8 RID: 21960
		private readonly string user;
	}
}
