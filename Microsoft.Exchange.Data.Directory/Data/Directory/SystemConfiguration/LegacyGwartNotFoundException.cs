using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A9E RID: 2718
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LegacyGwartNotFoundException : MandatoryContainerNotFoundException
	{
		// Token: 0x06007FE3 RID: 32739 RVA: 0x001A49B3 File Offset: 0x001A2BB3
		public LegacyGwartNotFoundException(string gwartName, string adminGroupName) : base(DirectoryStrings.LegacyGwartNotFoundException(gwartName, adminGroupName))
		{
			this.gwartName = gwartName;
			this.adminGroupName = adminGroupName;
		}

		// Token: 0x06007FE4 RID: 32740 RVA: 0x001A49D0 File Offset: 0x001A2BD0
		public LegacyGwartNotFoundException(string gwartName, string adminGroupName, Exception innerException) : base(DirectoryStrings.LegacyGwartNotFoundException(gwartName, adminGroupName), innerException)
		{
			this.gwartName = gwartName;
			this.adminGroupName = adminGroupName;
		}

		// Token: 0x06007FE5 RID: 32741 RVA: 0x001A49F0 File Offset: 0x001A2BF0
		protected LegacyGwartNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.gwartName = (string)info.GetValue("gwartName", typeof(string));
			this.adminGroupName = (string)info.GetValue("adminGroupName", typeof(string));
		}

		// Token: 0x06007FE6 RID: 32742 RVA: 0x001A4A45 File Offset: 0x001A2C45
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("gwartName", this.gwartName);
			info.AddValue("adminGroupName", this.adminGroupName);
		}

		// Token: 0x17002EC2 RID: 11970
		// (get) Token: 0x06007FE7 RID: 32743 RVA: 0x001A4A71 File Offset: 0x001A2C71
		public string GwartName
		{
			get
			{
				return this.gwartName;
			}
		}

		// Token: 0x17002EC3 RID: 11971
		// (get) Token: 0x06007FE8 RID: 32744 RVA: 0x001A4A79 File Offset: 0x001A2C79
		public string AdminGroupName
		{
			get
			{
				return this.adminGroupName;
			}
		}

		// Token: 0x0400559C RID: 21916
		private readonly string gwartName;

		// Token: 0x0400559D RID: 21917
		private readonly string adminGroupName;
	}
}
