using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02001238 RID: 4664
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotResolveServerException : LocalizedException
	{
		// Token: 0x0600BBE9 RID: 48105 RVA: 0x002AB958 File Offset: 0x002A9B58
		public CouldNotResolveServerException(string server, Exception e) : base(HybridStrings.HybridCouldNotResolveServerException(server, e))
		{
			this.server = server;
			this.e = e;
		}

		// Token: 0x0600BBEA RID: 48106 RVA: 0x002AB975 File Offset: 0x002A9B75
		public CouldNotResolveServerException(string server, Exception e, Exception innerException) : base(HybridStrings.HybridCouldNotResolveServerException(server, e), innerException)
		{
			this.server = server;
			this.e = e;
		}

		// Token: 0x0600BBEB RID: 48107 RVA: 0x002AB994 File Offset: 0x002A9B94
		protected CouldNotResolveServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
			this.e = (Exception)info.GetValue("e", typeof(Exception));
		}

		// Token: 0x0600BBEC RID: 48108 RVA: 0x002AB9E9 File Offset: 0x002A9BE9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
			info.AddValue("e", this.e);
		}

		// Token: 0x17003B40 RID: 15168
		// (get) Token: 0x0600BBED RID: 48109 RVA: 0x002ABA15 File Offset: 0x002A9C15
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17003B41 RID: 15169
		// (get) Token: 0x0600BBEE RID: 48110 RVA: 0x002ABA1D File Offset: 0x002A9C1D
		public Exception E
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x04006606 RID: 26118
		private readonly string server;

		// Token: 0x04006607 RID: 26119
		private readonly Exception e;
	}
}
