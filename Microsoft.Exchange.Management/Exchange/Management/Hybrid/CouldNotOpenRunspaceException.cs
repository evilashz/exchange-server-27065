using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02001239 RID: 4665
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotOpenRunspaceException : LocalizedException
	{
		// Token: 0x0600BBEF RID: 48111 RVA: 0x002ABA25 File Offset: 0x002A9C25
		public CouldNotOpenRunspaceException(Exception e) : base(HybridStrings.HybridCouldNotOpenRunspaceException(e))
		{
			this.e = e;
		}

		// Token: 0x0600BBF0 RID: 48112 RVA: 0x002ABA3A File Offset: 0x002A9C3A
		public CouldNotOpenRunspaceException(Exception e, Exception innerException) : base(HybridStrings.HybridCouldNotOpenRunspaceException(e), innerException)
		{
			this.e = e;
		}

		// Token: 0x0600BBF1 RID: 48113 RVA: 0x002ABA50 File Offset: 0x002A9C50
		protected CouldNotOpenRunspaceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.e = (Exception)info.GetValue("e", typeof(Exception));
		}

		// Token: 0x0600BBF2 RID: 48114 RVA: 0x002ABA7A File Offset: 0x002A9C7A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("e", this.e);
		}

		// Token: 0x17003B42 RID: 15170
		// (get) Token: 0x0600BBF3 RID: 48115 RVA: 0x002ABA95 File Offset: 0x002A9C95
		public Exception E
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x04006608 RID: 26120
		private readonly Exception e;
	}
}
