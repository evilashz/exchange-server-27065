using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.DelegatedAuthentication.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.DelegatedAuthentication
{
	// Token: 0x02000010 RID: 16
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DelegatedServerErrorException : LocalizedException
	{
		// Token: 0x0600005F RID: 95 RVA: 0x000045D8 File Offset: 0x000027D8
		public DelegatedServerErrorException(Exception ex) : base(Strings.DelegatedServerErrorException(ex))
		{
			this.ex = ex;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000045ED File Offset: 0x000027ED
		public DelegatedServerErrorException(Exception ex, Exception innerException) : base(Strings.DelegatedServerErrorException(ex), innerException)
		{
			this.ex = ex;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004603 File Offset: 0x00002803
		protected DelegatedServerErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000462D File Offset: 0x0000282D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00004648 File Offset: 0x00002848
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x04000057 RID: 87
		private readonly Exception ex;
	}
}
