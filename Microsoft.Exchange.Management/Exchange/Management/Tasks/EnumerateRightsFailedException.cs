using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E10 RID: 3600
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EnumerateRightsFailedException : LocalizedException
	{
		// Token: 0x0600A550 RID: 42320 RVA: 0x00285CF2 File Offset: 0x00283EF2
		public EnumerateRightsFailedException(string account, uint err) : base(Strings.EnumerateRightsFailedException(account, err))
		{
			this.account = account;
			this.err = err;
		}

		// Token: 0x0600A551 RID: 42321 RVA: 0x00285D0F File Offset: 0x00283F0F
		public EnumerateRightsFailedException(string account, uint err, Exception innerException) : base(Strings.EnumerateRightsFailedException(account, err), innerException)
		{
			this.account = account;
			this.err = err;
		}

		// Token: 0x0600A552 RID: 42322 RVA: 0x00285D30 File Offset: 0x00283F30
		protected EnumerateRightsFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.account = (string)info.GetValue("account", typeof(string));
			this.err = (uint)info.GetValue("err", typeof(uint));
		}

		// Token: 0x0600A553 RID: 42323 RVA: 0x00285D85 File Offset: 0x00283F85
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("account", this.account);
			info.AddValue("err", this.err);
		}

		// Token: 0x1700362D RID: 13869
		// (get) Token: 0x0600A554 RID: 42324 RVA: 0x00285DB1 File Offset: 0x00283FB1
		public string Account
		{
			get
			{
				return this.account;
			}
		}

		// Token: 0x1700362E RID: 13870
		// (get) Token: 0x0600A555 RID: 42325 RVA: 0x00285DB9 File Offset: 0x00283FB9
		public uint Err
		{
			get
			{
				return this.err;
			}
		}

		// Token: 0x04005F93 RID: 24467
		private readonly string account;

		// Token: 0x04005F94 RID: 24468
		private readonly uint err;
	}
}
