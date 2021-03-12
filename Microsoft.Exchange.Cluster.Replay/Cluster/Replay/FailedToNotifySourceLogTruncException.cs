using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004C3 RID: 1219
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToNotifySourceLogTruncException : LogTruncationException
	{
		// Token: 0x06002DAA RID: 11690 RVA: 0x000C1EBD File Offset: 0x000C00BD
		public FailedToNotifySourceLogTruncException(string dbSrc, uint hresult, string optionalFriendlyError) : base(ReplayStrings.FailedToNotifySourceLogTrunc(dbSrc, hresult, optionalFriendlyError))
		{
			this.dbSrc = dbSrc;
			this.hresult = hresult;
			this.optionalFriendlyError = optionalFriendlyError;
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000C1EE7 File Offset: 0x000C00E7
		public FailedToNotifySourceLogTruncException(string dbSrc, uint hresult, string optionalFriendlyError, Exception innerException) : base(ReplayStrings.FailedToNotifySourceLogTrunc(dbSrc, hresult, optionalFriendlyError), innerException)
		{
			this.dbSrc = dbSrc;
			this.hresult = hresult;
			this.optionalFriendlyError = optionalFriendlyError;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000C1F14 File Offset: 0x000C0114
		protected FailedToNotifySourceLogTruncException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbSrc = (string)info.GetValue("dbSrc", typeof(string));
			this.hresult = (uint)info.GetValue("hresult", typeof(uint));
			this.optionalFriendlyError = (string)info.GetValue("optionalFriendlyError", typeof(string));
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000C1F89 File Offset: 0x000C0189
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbSrc", this.dbSrc);
			info.AddValue("hresult", this.hresult);
			info.AddValue("optionalFriendlyError", this.optionalFriendlyError);
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x000C1FC6 File Offset: 0x000C01C6
		public string DbSrc
		{
			get
			{
				return this.dbSrc;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x000C1FCE File Offset: 0x000C01CE
		public uint Hresult
		{
			get
			{
				return this.hresult;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000C1FD6 File Offset: 0x000C01D6
		public string OptionalFriendlyError
		{
			get
			{
				return this.optionalFriendlyError;
			}
		}

		// Token: 0x04001545 RID: 5445
		private readonly string dbSrc;

		// Token: 0x04001546 RID: 5446
		private readonly uint hresult;

		// Token: 0x04001547 RID: 5447
		private readonly string optionalFriendlyError;
	}
}
