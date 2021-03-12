using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F27 RID: 3879
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GlobalLocatorServiceWCFExceptionThrown : LocalizedException
	{
		// Token: 0x0600AAB8 RID: 43704 RVA: 0x0028DE9D File Offset: 0x0028C09D
		public GlobalLocatorServiceWCFExceptionThrown(string e) : base(Strings.messageGlobalLocatorServiceWCFExceptionThrown(e))
		{
			this.e = e;
		}

		// Token: 0x0600AAB9 RID: 43705 RVA: 0x0028DEB2 File Offset: 0x0028C0B2
		public GlobalLocatorServiceWCFExceptionThrown(string e, Exception innerException) : base(Strings.messageGlobalLocatorServiceWCFExceptionThrown(e), innerException)
		{
			this.e = e;
		}

		// Token: 0x0600AABA RID: 43706 RVA: 0x0028DEC8 File Offset: 0x0028C0C8
		protected GlobalLocatorServiceWCFExceptionThrown(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.e = (string)info.GetValue("e", typeof(string));
		}

		// Token: 0x0600AABB RID: 43707 RVA: 0x0028DEF2 File Offset: 0x0028C0F2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("e", this.e);
		}

		// Token: 0x17003739 RID: 14137
		// (get) Token: 0x0600AABC RID: 43708 RVA: 0x0028DF0D File Offset: 0x0028C10D
		public string E
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x0400609F RID: 24735
		private readonly string e;
	}
}
