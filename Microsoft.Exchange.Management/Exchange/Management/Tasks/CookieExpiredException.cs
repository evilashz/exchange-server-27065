using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010CA RID: 4298
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CookieExpiredException : LocalizedException
	{
		// Token: 0x0600B2E7 RID: 45799 RVA: 0x0029A7C5 File Offset: 0x002989C5
		public CookieExpiredException(Guid oldDc, Guid newDc) : base(Strings.CookieExpiredException(oldDc, newDc))
		{
			this.oldDc = oldDc;
			this.newDc = newDc;
		}

		// Token: 0x0600B2E8 RID: 45800 RVA: 0x0029A7E2 File Offset: 0x002989E2
		public CookieExpiredException(Guid oldDc, Guid newDc, Exception innerException) : base(Strings.CookieExpiredException(oldDc, newDc), innerException)
		{
			this.oldDc = oldDc;
			this.newDc = newDc;
		}

		// Token: 0x0600B2E9 RID: 45801 RVA: 0x0029A800 File Offset: 0x00298A00
		protected CookieExpiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.oldDc = (Guid)info.GetValue("oldDc", typeof(Guid));
			this.newDc = (Guid)info.GetValue("newDc", typeof(Guid));
		}

		// Token: 0x0600B2EA RID: 45802 RVA: 0x0029A855 File Offset: 0x00298A55
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("oldDc", this.oldDc);
			info.AddValue("newDc", this.newDc);
		}

		// Token: 0x170038DC RID: 14556
		// (get) Token: 0x0600B2EB RID: 45803 RVA: 0x0029A88B File Offset: 0x00298A8B
		public Guid OldDc
		{
			get
			{
				return this.oldDc;
			}
		}

		// Token: 0x170038DD RID: 14557
		// (get) Token: 0x0600B2EC RID: 45804 RVA: 0x0029A893 File Offset: 0x00298A93
		public Guid NewDc
		{
			get
			{
				return this.newDc;
			}
		}

		// Token: 0x04006242 RID: 25154
		private readonly Guid oldDc;

		// Token: 0x04006243 RID: 25155
		private readonly Guid newDc;
	}
}
