using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004C1 RID: 1217
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToOpenLogTruncContextException : LogTruncationException
	{
		// Token: 0x06002D9B RID: 11675 RVA: 0x000C1C22 File Offset: 0x000BFE22
		public FailedToOpenLogTruncContextException(string dbSrc, uint hresult, string optionalFriendlyError) : base(ReplayStrings.FailedToOpenLogTruncContext(dbSrc, hresult, optionalFriendlyError))
		{
			this.dbSrc = dbSrc;
			this.hresult = hresult;
			this.optionalFriendlyError = optionalFriendlyError;
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000C1C4C File Offset: 0x000BFE4C
		public FailedToOpenLogTruncContextException(string dbSrc, uint hresult, string optionalFriendlyError, Exception innerException) : base(ReplayStrings.FailedToOpenLogTruncContext(dbSrc, hresult, optionalFriendlyError), innerException)
		{
			this.dbSrc = dbSrc;
			this.hresult = hresult;
			this.optionalFriendlyError = optionalFriendlyError;
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000C1C78 File Offset: 0x000BFE78
		protected FailedToOpenLogTruncContextException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbSrc = (string)info.GetValue("dbSrc", typeof(string));
			this.hresult = (uint)info.GetValue("hresult", typeof(uint));
			this.optionalFriendlyError = (string)info.GetValue("optionalFriendlyError", typeof(string));
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000C1CED File Offset: 0x000BFEED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbSrc", this.dbSrc);
			info.AddValue("hresult", this.hresult);
			info.AddValue("optionalFriendlyError", this.optionalFriendlyError);
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06002D9F RID: 11679 RVA: 0x000C1D2A File Offset: 0x000BFF2A
		public string DbSrc
		{
			get
			{
				return this.dbSrc;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x000C1D32 File Offset: 0x000BFF32
		public uint Hresult
		{
			get
			{
				return this.hresult;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x000C1D3A File Offset: 0x000BFF3A
		public string OptionalFriendlyError
		{
			get
			{
				return this.optionalFriendlyError;
			}
		}

		// Token: 0x0400153E RID: 5438
		private readonly string dbSrc;

		// Token: 0x0400153F RID: 5439
		private readonly uint hresult;

		// Token: 0x04001540 RID: 5440
		private readonly string optionalFriendlyError;
	}
}
