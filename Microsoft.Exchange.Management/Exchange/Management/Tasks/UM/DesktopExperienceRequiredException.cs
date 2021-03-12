using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02001201 RID: 4609
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DesktopExperienceRequiredException : LocalizedException
	{
		// Token: 0x0600B9DB RID: 47579 RVA: 0x002A67DB File Offset: 0x002A49DB
		public DesktopExperienceRequiredException(string serverFqdn) : base(Strings.DesktopExperienceRequiredException(serverFqdn))
		{
			this.serverFqdn = serverFqdn;
		}

		// Token: 0x0600B9DC RID: 47580 RVA: 0x002A67F0 File Offset: 0x002A49F0
		public DesktopExperienceRequiredException(string serverFqdn, Exception innerException) : base(Strings.DesktopExperienceRequiredException(serverFqdn), innerException)
		{
			this.serverFqdn = serverFqdn;
		}

		// Token: 0x0600B9DD RID: 47581 RVA: 0x002A6806 File Offset: 0x002A4A06
		protected DesktopExperienceRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverFqdn = (string)info.GetValue("serverFqdn", typeof(string));
		}

		// Token: 0x0600B9DE RID: 47582 RVA: 0x002A6830 File Offset: 0x002A4A30
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverFqdn", this.serverFqdn);
		}

		// Token: 0x17003A54 RID: 14932
		// (get) Token: 0x0600B9DF RID: 47583 RVA: 0x002A684B File Offset: 0x002A4A4B
		public string ServerFqdn
		{
			get
			{
				return this.serverFqdn;
			}
		}

		// Token: 0x0400646F RID: 25711
		private readonly string serverFqdn;
	}
}
