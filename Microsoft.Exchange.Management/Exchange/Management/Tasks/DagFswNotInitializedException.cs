using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200104C RID: 4172
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagFswNotInitializedException : LocalizedException
	{
		// Token: 0x0600B039 RID: 45113 RVA: 0x002959D8 File Offset: 0x00293BD8
		public DagFswNotInitializedException(string ex) : base(Strings.DagFswNotInitializedException(ex))
		{
			this.ex = ex;
		}

		// Token: 0x0600B03A RID: 45114 RVA: 0x002959ED File Offset: 0x00293BED
		public DagFswNotInitializedException(string ex, Exception innerException) : base(Strings.DagFswNotInitializedException(ex), innerException)
		{
			this.ex = ex;
		}

		// Token: 0x0600B03B RID: 45115 RVA: 0x00295A03 File Offset: 0x00293C03
		protected DagFswNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ex = (string)info.GetValue("ex", typeof(string));
		}

		// Token: 0x0600B03C RID: 45116 RVA: 0x00295A2D File Offset: 0x00293C2D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x17003826 RID: 14374
		// (get) Token: 0x0600B03D RID: 45117 RVA: 0x00295A48 File Offset: 0x00293C48
		public string Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x0400618C RID: 24972
		private readonly string ex;
	}
}
