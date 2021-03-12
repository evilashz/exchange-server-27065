using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Common
{
	// Token: 0x0200000C RID: 12
	internal class SipCultureInfoBase : CultureInfo
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003404 File Offset: 0x00001604
		internal SipCultureInfoBase(CultureInfo parent, string segmentID) : base(parent.Name)
		{
			this.parent = parent;
			this.segmentID = segmentID;
			this.name = parent.Name;
			this.sipName = string.Format(CultureInfo.InvariantCulture, "{0}-x-{1}", new object[]
			{
				parent.IsNeutralCulture ? parent.Name : parent.Parent.Name,
				segmentID
			});
			this.description = string.Format(CultureInfo.InvariantCulture, "Role-Based Culture ({0})", new object[]
			{
				this.name
			});
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000349C File Offset: 0x0000169C
		public override string Name
		{
			get
			{
				if (!this.useSipName)
				{
					return this.name;
				}
				return this.sipName;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000034B3 File Offset: 0x000016B3
		public override CultureInfo Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000034BB File Offset: 0x000016BB
		public override string EnglishName
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000034C3 File Offset: 0x000016C3
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000034CB File Offset: 0x000016CB
		internal virtual bool UseSipName
		{
			get
			{
				return this.useSipName;
			}
			set
			{
				this.useSipName = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000034D4 File Offset: 0x000016D4
		internal virtual string SipName
		{
			get
			{
				return this.sipName;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000034DC File Offset: 0x000016DC
		internal virtual string SipSegmentID
		{
			get
			{
				return this.segmentID;
			}
		}

		// Token: 0x0400001B RID: 27
		protected string description;

		// Token: 0x0400001C RID: 28
		protected string name;

		// Token: 0x0400001D RID: 29
		protected string sipName;

		// Token: 0x0400001E RID: 30
		protected bool useSipName;

		// Token: 0x0400001F RID: 31
		protected string segmentID;

		// Token: 0x04000020 RID: 32
		protected CultureInfo parent;
	}
}
