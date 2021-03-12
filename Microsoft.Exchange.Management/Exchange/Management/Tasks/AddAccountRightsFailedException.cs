using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E0E RID: 3598
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AddAccountRightsFailedException : LocalizedException
	{
		// Token: 0x0600A543 RID: 42307 RVA: 0x00285B0D File Offset: 0x00283D0D
		public AddAccountRightsFailedException(string account, uint err) : base(Strings.AddAccountRightsFailedException(account, err))
		{
			this.account = account;
			this.err = err;
		}

		// Token: 0x0600A544 RID: 42308 RVA: 0x00285B2A File Offset: 0x00283D2A
		public AddAccountRightsFailedException(string account, uint err, Exception innerException) : base(Strings.AddAccountRightsFailedException(account, err), innerException)
		{
			this.account = account;
			this.err = err;
		}

		// Token: 0x0600A545 RID: 42309 RVA: 0x00285B48 File Offset: 0x00283D48
		protected AddAccountRightsFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.account = (string)info.GetValue("account", typeof(string));
			this.err = (uint)info.GetValue("err", typeof(uint));
		}

		// Token: 0x0600A546 RID: 42310 RVA: 0x00285B9D File Offset: 0x00283D9D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("account", this.account);
			info.AddValue("err", this.err);
		}

		// Token: 0x17003628 RID: 13864
		// (get) Token: 0x0600A547 RID: 42311 RVA: 0x00285BC9 File Offset: 0x00283DC9
		public string Account
		{
			get
			{
				return this.account;
			}
		}

		// Token: 0x17003629 RID: 13865
		// (get) Token: 0x0600A548 RID: 42312 RVA: 0x00285BD1 File Offset: 0x00283DD1
		public uint Err
		{
			get
			{
				return this.err;
			}
		}

		// Token: 0x04005F8E RID: 24462
		private readonly string account;

		// Token: 0x04005F8F RID: 24463
		private readonly uint err;
	}
}
