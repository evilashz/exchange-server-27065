using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F23 RID: 3875
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthTestUserOnWrongSiteException : LocalizedException
	{
		// Token: 0x0600AAA1 RID: 43681 RVA: 0x0028DBC5 File Offset: 0x0028BDC5
		public CasHealthTestUserOnWrongSiteException(string userName, string foundOn, string shouldBeOn) : base(Strings.CasHealthTestUserOnWrongSite(userName, foundOn, shouldBeOn))
		{
			this.userName = userName;
			this.foundOn = foundOn;
			this.shouldBeOn = shouldBeOn;
		}

		// Token: 0x0600AAA2 RID: 43682 RVA: 0x0028DBEA File Offset: 0x0028BDEA
		public CasHealthTestUserOnWrongSiteException(string userName, string foundOn, string shouldBeOn, Exception innerException) : base(Strings.CasHealthTestUserOnWrongSite(userName, foundOn, shouldBeOn), innerException)
		{
			this.userName = userName;
			this.foundOn = foundOn;
			this.shouldBeOn = shouldBeOn;
		}

		// Token: 0x0600AAA3 RID: 43683 RVA: 0x0028DC14 File Offset: 0x0028BE14
		protected CasHealthTestUserOnWrongSiteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userName = (string)info.GetValue("userName", typeof(string));
			this.foundOn = (string)info.GetValue("foundOn", typeof(string));
			this.shouldBeOn = (string)info.GetValue("shouldBeOn", typeof(string));
		}

		// Token: 0x0600AAA4 RID: 43684 RVA: 0x0028DC89 File Offset: 0x0028BE89
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userName", this.userName);
			info.AddValue("foundOn", this.foundOn);
			info.AddValue("shouldBeOn", this.shouldBeOn);
		}

		// Token: 0x17003732 RID: 14130
		// (get) Token: 0x0600AAA5 RID: 43685 RVA: 0x0028DCC6 File Offset: 0x0028BEC6
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x17003733 RID: 14131
		// (get) Token: 0x0600AAA6 RID: 43686 RVA: 0x0028DCCE File Offset: 0x0028BECE
		public string FoundOn
		{
			get
			{
				return this.foundOn;
			}
		}

		// Token: 0x17003734 RID: 14132
		// (get) Token: 0x0600AAA7 RID: 43687 RVA: 0x0028DCD6 File Offset: 0x0028BED6
		public string ShouldBeOn
		{
			get
			{
				return this.shouldBeOn;
			}
		}

		// Token: 0x04006098 RID: 24728
		private readonly string userName;

		// Token: 0x04006099 RID: 24729
		private readonly string foundOn;

		// Token: 0x0400609A RID: 24730
		private readonly string shouldBeOn;
	}
}
