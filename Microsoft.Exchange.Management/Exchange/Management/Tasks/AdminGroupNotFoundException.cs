using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E17 RID: 3607
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdminGroupNotFoundException : LocalizedException
	{
		// Token: 0x0600A576 RID: 42358 RVA: 0x00286141 File Offset: 0x00284341
		public AdminGroupNotFoundException(string adminGroup) : base(Strings.AdminGroupNotFoundException(adminGroup))
		{
			this.adminGroup = adminGroup;
		}

		// Token: 0x0600A577 RID: 42359 RVA: 0x00286156 File Offset: 0x00284356
		public AdminGroupNotFoundException(string adminGroup, Exception innerException) : base(Strings.AdminGroupNotFoundException(adminGroup), innerException)
		{
			this.adminGroup = adminGroup;
		}

		// Token: 0x0600A578 RID: 42360 RVA: 0x0028616C File Offset: 0x0028436C
		protected AdminGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.adminGroup = (string)info.GetValue("adminGroup", typeof(string));
		}

		// Token: 0x0600A579 RID: 42361 RVA: 0x00286196 File Offset: 0x00284396
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("adminGroup", this.adminGroup);
		}

		// Token: 0x17003637 RID: 13879
		// (get) Token: 0x0600A57A RID: 42362 RVA: 0x002861B1 File Offset: 0x002843B1
		public string AdminGroup
		{
			get
			{
				return this.adminGroup;
			}
		}

		// Token: 0x04005F9D RID: 24477
		private readonly string adminGroup;
	}
}
