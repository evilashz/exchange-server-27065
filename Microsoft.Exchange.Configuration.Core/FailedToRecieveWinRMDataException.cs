using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Core.LocStrings;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000031 RID: 49
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToRecieveWinRMDataException : WinRMDataExchangeException
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00007448 File Offset: 0x00005648
		public FailedToRecieveWinRMDataException(string identity) : base(Strings.FailedToReceiveWinRMData(identity))
		{
			this.identity = identity;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000745D File Offset: 0x0000565D
		public FailedToRecieveWinRMDataException(string identity, Exception innerException) : base(Strings.FailedToReceiveWinRMData(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007473 File Offset: 0x00005673
		protected FailedToRecieveWinRMDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000749D File Offset: 0x0000569D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000074B8 File Offset: 0x000056B8
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x040000C6 RID: 198
		private readonly string identity;
	}
}
