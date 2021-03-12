using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011FC RID: 4604
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PropertyNotSupportedOnLegacyObjectException : LocalizedException
	{
		// Token: 0x0600B9C3 RID: 47555 RVA: 0x002A65C6 File Offset: 0x002A47C6
		public PropertyNotSupportedOnLegacyObjectException(string user, string propname) : base(Strings.PropertyNotSupportedOnLegacyObjectException(user, propname))
		{
			this.user = user;
			this.propname = propname;
		}

		// Token: 0x0600B9C4 RID: 47556 RVA: 0x002A65E3 File Offset: 0x002A47E3
		public PropertyNotSupportedOnLegacyObjectException(string user, string propname, Exception innerException) : base(Strings.PropertyNotSupportedOnLegacyObjectException(user, propname), innerException)
		{
			this.user = user;
			this.propname = propname;
		}

		// Token: 0x0600B9C5 RID: 47557 RVA: 0x002A6604 File Offset: 0x002A4804
		protected PropertyNotSupportedOnLegacyObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
			this.propname = (string)info.GetValue("propname", typeof(string));
		}

		// Token: 0x0600B9C6 RID: 47558 RVA: 0x002A6659 File Offset: 0x002A4859
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
			info.AddValue("propname", this.propname);
		}

		// Token: 0x17003A50 RID: 14928
		// (get) Token: 0x0600B9C7 RID: 47559 RVA: 0x002A6685 File Offset: 0x002A4885
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x17003A51 RID: 14929
		// (get) Token: 0x0600B9C8 RID: 47560 RVA: 0x002A668D File Offset: 0x002A488D
		public string Propname
		{
			get
			{
				return this.propname;
			}
		}

		// Token: 0x0400646B RID: 25707
		private readonly string user;

		// Token: 0x0400646C RID: 25708
		private readonly string propname;
	}
}
