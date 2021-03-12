using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004A3 RID: 1187
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class JetErrorFileIOBeyondEOFException : TransientException
	{
		// Token: 0x06002CE9 RID: 11497 RVA: 0x000C04AD File Offset: 0x000BE6AD
		public JetErrorFileIOBeyondEOFException(string pageno) : base(ReplayStrings.JetErrorFileIOBeyondEOFException(pageno))
		{
			this.pageno = pageno;
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000C04C2 File Offset: 0x000BE6C2
		public JetErrorFileIOBeyondEOFException(string pageno, Exception innerException) : base(ReplayStrings.JetErrorFileIOBeyondEOFException(pageno), innerException)
		{
			this.pageno = pageno;
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000C04D8 File Offset: 0x000BE6D8
		protected JetErrorFileIOBeyondEOFException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.pageno = (string)info.GetValue("pageno", typeof(string));
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000C0502 File Offset: 0x000BE702
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("pageno", this.pageno);
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x000C051D File Offset: 0x000BE71D
		public string Pageno
		{
			get
			{
				return this.pageno;
			}
		}

		// Token: 0x04001504 RID: 5380
		private readonly string pageno;
	}
}
