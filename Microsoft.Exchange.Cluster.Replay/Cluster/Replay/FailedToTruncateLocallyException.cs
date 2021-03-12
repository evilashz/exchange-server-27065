using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004C4 RID: 1220
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToTruncateLocallyException : LogTruncationException
	{
		// Token: 0x06002DB1 RID: 11697 RVA: 0x000C1FDE File Offset: 0x000C01DE
		public FailedToTruncateLocallyException(uint hresult, string optionalFriendlyError) : base(ReplayStrings.FailedToTruncateLocallyException(hresult, optionalFriendlyError))
		{
			this.hresult = hresult;
			this.optionalFriendlyError = optionalFriendlyError;
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x000C2000 File Offset: 0x000C0200
		public FailedToTruncateLocallyException(uint hresult, string optionalFriendlyError, Exception innerException) : base(ReplayStrings.FailedToTruncateLocallyException(hresult, optionalFriendlyError), innerException)
		{
			this.hresult = hresult;
			this.optionalFriendlyError = optionalFriendlyError;
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x000C2024 File Offset: 0x000C0224
		protected FailedToTruncateLocallyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.hresult = (uint)info.GetValue("hresult", typeof(uint));
			this.optionalFriendlyError = (string)info.GetValue("optionalFriendlyError", typeof(string));
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x000C2079 File Offset: 0x000C0279
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("hresult", this.hresult);
			info.AddValue("optionalFriendlyError", this.optionalFriendlyError);
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x000C20A5 File Offset: 0x000C02A5
		public uint Hresult
		{
			get
			{
				return this.hresult;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x000C20AD File Offset: 0x000C02AD
		public string OptionalFriendlyError
		{
			get
			{
				return this.optionalFriendlyError;
			}
		}

		// Token: 0x04001548 RID: 5448
		private readonly uint hresult;

		// Token: 0x04001549 RID: 5449
		private readonly string optionalFriendlyError;
	}
}
